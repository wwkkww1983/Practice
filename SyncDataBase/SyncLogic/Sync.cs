using Newtonsoft.Json;
using SyncCommon;
using SyncDataAccess;
using SyncModel;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace SyncLogic
{
    public class Sync
    {
        readonly int _count = Convert.ToInt32(Common.GetConfigValue("MaxOnceData"));
        public bool SyncData(ref string res)
        {
            try
            {
                string txt = FileOperation.ReadText(Common.GetConfigValue("ConfigPath"));
                if (txt.HasValue())
                {
                    var models = JsonConvert.DeserializeObject<List<DataBaseModel>>(txt);
                    return Excute(models);
                }
            }
            catch (Exception ex)
            {
                res = ex.Message;
            }
            return false;
        }

        private bool Excute(List<DataBaseModel> models)
        {
            bool flag = false;
            string sql = string.Empty;
            try
            {
                string sameFields = string.Empty;
                foreach (var item in models)
                {
                    //生成查询SQL
                    sql = item.ToSelectSql();
                    //找到相同的字段
                    sameFields = FindSameFields(item);
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
                        DeleteSourceTableData(item);
                    }
                    var dt = Query(sql, item, item.SourceDBType);
                    sql = item.ToInsertSql();
                    sql = sql.Replace("(placeholder)", sameFields);
                    flag = ExcuteInsertSql(sql, dt, item);
                    if (item.SyncPartial && item.FlagField.HasValue())
                    {
                        SavePoint(item, dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        private string FindSameFields(DataBaseModel baseModel)
        {
            string strSqlSource = string.Empty;
            string strSqlTarget = string.Empty;
            switch (baseModel.SourceDBType)
            {
                case DataBaseType.SQLSERVER:
                case DataBaseType.MYSQL:
                    strSqlSource = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{baseModel.SourceTable}';";
                    strSqlTarget = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{baseModel.TargetTable}';";
                    break;
                case DataBaseType.ORACLE:
                    break;
                default:
                    break;
            }
            var dtSource = Query(strSqlSource, baseModel, baseModel.SourceDBType);
            var dtTarget = Query(strSqlTarget, baseModel, baseModel.TargetDBType);
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
            return str;
        }

        private static string GetPoint(DataBaseModel model)
        {
            string path = GetPointPath(model);
            return FileOperation.ReadText(path);
        }

        private static string GetPointPath(DataBaseModel model)
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
                var dv = new DataView(dt);
                dv.Sort = $"{item.FlagField} desc";
                dt = dv.ToTable();
                var point = dt.Rows[0][item.FlagField].ToString();
                string path = GetPointPath(item);
                FileOperation.WriteText(path, point);
            }
        }

        private bool DeleteSourceTableData(DataBaseModel item)
        {
            string sql = string.Empty;
            switch (item.TargetDBType)
            {
                case DataBaseType.SQLSERVER:
                    sql = $"delete from {item.TargetDB}.dbo.{item.TargetTable};";
                    break;
                case DataBaseType.ORACLE:
                case DataBaseType.MYSQL:
                    sql = $"delete from {item.TargetDB}.{item.TargetTable};";
                    break;
                default:
                    break;
            }
            return ExcuteSQL(sql, item);
        }

        private bool ExcuteInsertSql(string sql, DataTable dt, DataBaseModel item)
        {
            bool flag = false;
            if (dt.HasData())
            {
                StringBuilder sb = new StringBuilder();
                StringBuilder strs = new StringBuilder();
                //strs.Append(sql);
                for (var i = 0; i < dt.Rows.Count; i++)
                {
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        strs.Append($"'{dt.Rows[i][j]}',");
                    }
                    var strsql = string.Format(sql, strs.ToString().TrimEnd(','));
                    strs.Clear();
                    sb.Append(strsql + ";");
                    if ((i > _count - 1 && i % _count == 0) || (i == dt.Rows.Count - 1))
                    {
                        if (sb.ToString().HasValue())
                        {
                            flag = ExcuteSQL(sb.ToString(), item);
                            sb.Clear();
                            if (!flag)
                            {
                                return flag;
                            }
                        }
                    }
                }
            }
            return flag;
        }

        private bool ExcuteSQL(string sql, DataBaseModel item)
        {
            bool flag = false;
            switch (item.TargetDBType)
            {
                case DataBaseType.MYSQL:
                    MySqlHelper mysqlhelper = new MySqlHelper();
                    flag = mysqlhelper.InsertData(sql, item.TargetDBConnStr);
                    break;
                case DataBaseType.SQLSERVER:
                    SqlHelper helper = new SqlHelper();
                    flag = helper.InsertData(sql, item.TargetDBConnStr);
                    break;
                case DataBaseType.ORACLE:
                    break;
                default:
                    break;
            }
            return flag;
        }

        private DataTable Query(string sql, DataBaseModel item, DataBaseType dataBaseType)
        {
            var dt = new DataTable();
            switch (dataBaseType)
            {
                case DataBaseType.MYSQL:
                    MySqlHelper mysqlhelper = new MySqlHelper();
                    dt = mysqlhelper.GetDataTable(sql, item.SourceDBConnStr);
                    break;
                case DataBaseType.SQLSERVER:
                    SqlHelper helper = new SqlHelper();
                    dt = helper.GetDataTable(sql, item.TargetDBConnStr);
                    break;
                case DataBaseType.ORACLE:
                    break;
                default:
                    break;
            }
            return dt;
        }
    }
}
