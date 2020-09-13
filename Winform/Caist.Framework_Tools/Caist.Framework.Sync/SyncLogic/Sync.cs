using Caist.Framework.DataAccess.Cache;
using Caist.Framework.IdGenerator;
using Caist.Framework.ThreadPool;
using SyncDataAccess;
using SyncModel;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncLogic
{
    public class Sync
    {
        SqlHelper _helper = new SqlHelper();
        MySqlHelper _mysqlhelper = new MySqlHelper();
        //一次执行数据条数
        readonly int _count = Convert.ToInt32(Common.GetConfigValue("MaxOnceData"));
        DataBaseModel _baseModel;
        #region 异步方法

        public async Task<Tuple<string, bool>> SyncDataAsync()
        {
            var res = string.Empty;
            var flag = false;
            try
            {
                var models = SyncCache.GetConfigDataTable("syncData");
                flag = await ExcuteAsync(models);
            }
            catch (Exception ex)
            {
                res = $"--{_baseModel.SourceSql}--\r\n{ex.Message}\r\n{ex.StackTrace}";
            }

            return Tuple.Create<string, bool>(res, flag);
        }

        private async Task<bool> ExcuteAsync(List<DataBaseModel> models)
        {
            bool flag = false;
            foreach (var item in models)
            {
                _baseModel = item;
                flag = await SqlToDataAsync(item);
            }
            return flag;
        }

        private async Task<bool> SqlToDataAsync(DataBaseModel item)
        {
            string sql;
            string sameFields = string.Empty;
            bool flag;
            if (item.SourceSql.HasValue())//多表的情况
            {
                sql = item.SourceSql;
                await DeleteTargetTableDataAsync(item);
            }
            else
            {
                //生成查询SQL
                sql = ToSelectSql(item);
                //找到相同的字段
                sameFields = await FindSameFieldsForSingleAsync(item);
                sql = sql.Replace("(placeholder)", sameFields);
                //增量插入查询
                if (item.SyncPartial)
                {
                    string point = GetPoint(item);
                    if (point.HasValue())
                    {
                        sql += $" where {item.FlagField} > '{point.Replace("\r\n", string.Empty)}'";
                    }
                }
                else
                {
                    await DeleteTargetTableDataAsync(item);
                }
            }
            var dt = await QueryAsync(sql, item.SourceDBConnStr, item.SourceDBType);
            sql = ToInsertSql(item);
            if (item.SourceSql.HasValue())
            {
                sameFields = FindSameFieldsForMultiple(dt);
            }
            sql = sql.Replace("(placeholder)", sameFields);
            flag = await ExcuteInsertSqlAsync(sql, dt, item);
            return flag;
        }

        #region 查找相同字段名(源数据单表的情况下)
        private async Task<string> FindSameFieldsForSingleAsync(DataBaseModel baseModel)
        {
            string strSqlSource = string.Empty;
            string strSqlTarget = string.Empty;
            switch (baseModel.SourceDBType)
            {
                case DataEmun.SQLServer:
                case DataEmun.MySQL:
                    strSqlSource = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{baseModel.SourceTable}' order by column_name;";
                    strSqlTarget = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{baseModel.TargetTable}' order by column_name;";
                    break;
                case DataEmun.Oracle:
                    break;
                default:
                    break;
            }
            var dtSource = await QueryAsync(strSqlSource, baseModel.SourceDBConnStr, baseModel.SourceDBType);
            var dtTarget = await QueryAsync(strSqlTarget, baseModel.TargetDBConnStr, baseModel.TargetDBType);
            return CompareDataTable(dtSource, dtTarget);
        }

        private string CompareDataTable(DataTable dtSource, DataTable dtTarget)
        {
            string str = string.Empty;
            StringBuilder res = new StringBuilder();
            foreach (DataRow dataRow in dtSource.Rows)
            {
                var drs = dtTarget.Select($"column_name = '{dataRow["column_name"].ToString().ToLower()}'");
                if (drs.Length > 0)
                {
                    res.Append($"{dataRow["column_name"]},");
                }
            }
            if (res.Length > 0)
            {
                str = res.ToString().TrimEnd(',');
            }
            dtSource.Rows.Clear();
            dtTarget.Rows.Clear();
            res.Clear();
            return str;
        }
        #endregion

        #region 查找目标字段表名(源数据多表的情况下)
        private string FindSameFieldsForMultiple(DataTable dt)
        {
            //根据源table表来获取字段
            string res = string.Empty;
            StringBuilder str = new StringBuilder();
            if (dt.HasData())
            {
                foreach (DataColumn dc in dt.Columns)
                {
                    str.Append(dc.ColumnName + ",");
                }
                res = str.ToString().TrimEnd(',');
            }
            return res;
        }
        #endregion

        private string GetPoint(DataBaseModel model)
        {
            string path = GetPointPath(model);
            return FileOperation.ReadText(path);
        }

        private string GetPointPath(DataBaseModel model)
        {
            var dicty = Common.GetConfigValue("SyncPointsFolder");
            //数据库名+表名
            string path = Path.Combine(dicty, model.SourceDB + "_" + model.SourceTable + ".txt");
            return path;
        }

        private void SavePoint(DataBaseModel item, DataTable dt)
        {
            if (dt.HasData())
            {
                using (var dv = new DataView(dt))
                {
                    dv.Sort = $"{item.FlagField} desc";
                    dt = dv.ToTable();
                    var point = dt.Rows[0][item.FlagField].ToString();
                    string path = GetPointPath(item);
                    FileOperation.WriteText(path, point, false);
                }
            }
        }

        private async Task<bool> DeleteTargetTableDataAsync(DataBaseModel item)
        {
            string sql = string.Empty;
            switch (item.TargetDBType)
            {
                case DataEmun.SQLServer:
                    sql = $"truncate table {item.TargetDB}.dbo.{item.TargetTable};";
                    break;
                case DataEmun.Oracle:
                case DataEmun.MySQL:
                    sql = $"truncate table {item.TargetDB}.{item.TargetTable};";
                    break;
                default:
                    break;
            }
            return await ExcuteSQLAsync(sql, item.TargetDBConnStr, item.TargetDBType);
        }

        private async Task<bool> ExcuteInsertSqlAsync(string sql, DataTable dt, DataBaseModel item)
        {
            bool flag = false;
            if (dt.HasData())
            {
                if (dt.Rows.Count > 2000)
                {
                    _helper.InsertBulk(item, dt);
                    flag = true;
                }
                else
                {
                    StringBuilder sb = new StringBuilder();
                    StringBuilder sbValues = new StringBuilder();
                    for (var i = 0; i < dt.Rows.Count; i++)
                    {
                        for (int j = 0; j < dt.Columns.Count; j++)
                        {
                            sbValues.Append($"'{dt.Rows[i][j]}',");
                        }
                        var strsql = string.Format(sql, sbValues.ToString().TrimEnd(','));
                        sbValues.Clear();
                        sb.Append(strsql + ";");
                        if ((i > _count - 1 && i % _count == 0) || (i == dt.Rows.Count - 1))
                        {
                            if (sb.ToString().HasValue())
                            {
                                flag = await ExcuteSQLAsync(sb.ToString(), item.TargetDBConnStr, item.TargetDBType);
                                sb.Clear();
                                if (!flag)
                                {
                                    return flag;
                                }
                            }
                        }
                    }
                    if (item.SyncPartial && item.FlagField.HasValue())
                    {
                        SavePoint(item, dt);
                    }
                    sb.Clear();
                    sbValues.Clear();
                }
            }
            return flag;
        }

        private async Task<bool> ExcuteSQLAsync(string sql, string connStr, DataEmun dbType)
        {
            bool flag = false;
            switch (dbType)
            {
                case DataEmun.MySQL:
                    flag = await _mysqlhelper.ExcuteAsync(sql, connStr, DataEmun.MySQL);
                    break;
                case DataEmun.SQLServer:
                    flag = await _helper.ExcuteAsync(sql, connStr, DataEmun.SQLServer);
                    break;
                case DataEmun.Oracle:
                    break;
                default:
                    break;
            }
            return flag;
        }

        private async Task<DataTable> QueryAsync(string sql, string connStr, DataEmun dataBaseType)
        {
            DataTable dt = null;
            switch (dataBaseType)
            {
                case DataEmun.MySQL:
                    dt = await _mysqlhelper.GetDataTableAsync(sql, connStr, DataEmun.MySQL);
                    break;
                case DataEmun.SQLServer:
                    dt = await _helper.GetDataTableAsync(sql, connStr, DataEmun.SQLServer);
                    break;
                case DataEmun.Oracle:
                    break;
                default:
                    break;
            }
            return dt;
        }
        #endregion

        #region sql生成
        public string ToSelectSql(DataBaseModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"select (placeholder)");
            if (model.TableFields != null && model.TableFields.Length > 0)
            {
                string tempStr = "," + string.Join(",", model.TableFields.Select(p => p.Split(',')).ToDictionary(k => k[0], v => v[1]).Keys);
                sb.Append(tempStr);
            }
            switch (model.SourceDBType)
            {
                case DataEmun.SQLServer:
                    sb.Append($" from {model.SourceDB}.dbo.{model.SourceTable} ");
                    break;
                case DataEmun.Oracle://TODO:oracle 可能还得测试下
                case DataEmun.MySQL:
                    sb.Append($" from {model.SourceDB}.{model.SourceTable} ");
                    break;
                default:
                    break;
            }
            return sb.ToString();
        }

        public string ToInsertSql(DataBaseModel model)
        {
            StringBuilder sb = new StringBuilder();
            switch (model.TargetDBType)
            {
                case DataEmun.SQLServer:
                    sb.Append($"insert into {model.TargetDB}.dbo.{model.TargetTable}( (placeholder)");
                    break;
                case DataEmun.Oracle://TODO:oracle 可能还得测试下
                case DataEmun.MySQL:
                    sb.Append($"insert into {model.TargetDB}.{model.TargetTable}((placeholder)");
                    break;
                default:
                    break;
            }

            if (model.TableFields != null && model.TableFields.Length > 0)
            {
                string tempStr = "," + string.Join(",", model.TableFields.Select(p => p.Split(',')).ToDictionary(k => k[0], v => v[1]).Values);
                sb.Append(tempStr);
            }
            sb.Append(@") values({0})");
            return sb.ToString();
        }
        #endregion
    }
}
