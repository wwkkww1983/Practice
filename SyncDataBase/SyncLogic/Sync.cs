using Newtonsoft.Json;
using SyncCommon;
using SyncDataAccess;
using SyncModel;
using SyncUtil;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SyncLogic
{
    public class Sync
    {
        readonly int _count = Convert.ToInt32(Common.GetConfigValue("MaxOnceData"));
        public async Task<bool> SyncData()
        {
            string txt = FileOperation.ReadText(Common.GetConfigValue("ConfigPath"));
            if (txt.HasValue())
            {
                var models = JsonConvert.DeserializeObject<List<DataBaseModel>>(txt);
                return await Excute(models);
            }
            return false;
        }

        private async Task<bool> Excute(List<DataBaseModel> models)
        {
            bool flag = false;
            string sql = string.Empty;
            foreach (var item in models)
            {
                //生成查询SQL
                sql = item.ToSelectSql();
                //增量插入查询
                if (item.syncType)
                {
                    string point = GetPoint(item);
                    if (point.HasValue())
                    {
                        sql += $" where {item.flagField} > '{point.Replace("\r\n",string.Empty)}'";
                    }
                }
                else
                {
                    DeleteSourceTableData(item);
                }
                //TODO:替换动态查询连接
                var dt = Query(sql, item);
                sql = item.ToInsertSql();
                flag = ExcuteInsertSql(sql, dt, item);
                SavePoint(item, dt);
            }
            return flag;
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
            string path = Path.Combine(dicty, model.sourceDB + "_" + model.sourceTable + ".txt");
            return path;
        }

        private void SavePoint(DataBaseModel item, DataTable dt)
        {
            if (dt.HasData())
            {
                var dv = new DataView(dt);
                dv.Sort = $"{item.flagField} desc";
                dt = dv.ToTable();
                var point = dt.Rows[0][item.flagField].ToString();
                string path = GetPointPath(item);
                FileOperation.WriteText(path, point);
            }
        }

        private bool DeleteSourceTableData(DataBaseModel item)
        {
            string sql = string.Empty;
            switch (item.targetDBType)
            {
                case DataBaseType.SQLSERVER:
                    sql = $"delete from {item.targetDB}.dbo.{item.targetTable};";
                    break;
                case DataBaseType.ORACLE:
                case DataBaseType.MYSQL:
                    sql = $"delete from {item.targetDB}.{item.targetTable};";
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
            switch (item.targetDBType)
            {
                case DataBaseType.MYSQL:
                    MySqlHelper mysqlhelper = new MySqlHelper();
                    flag = mysqlhelper.InsertData(sql, item.targetDBConnStr);
                    break;
                case DataBaseType.SQLSERVER:
                    SqlHelper helper = new SqlHelper();
                    flag = helper.InsertData(sql, item.targetDBConnStr);
                    break;
                case DataBaseType.ORACLE:
                    break;
                default:
                    break;
            }
            return flag;
        }

        private DataTable Query(string sql, DataBaseModel item)
        {
            var dt = new DataTable();
            switch (item.sourceDBType)
            {
                case DataBaseType.MYSQL:
                    MySqlHelper mysqlhelper = new MySqlHelper();
                    dt = mysqlhelper.GetDataTable(sql, item.sourceDBConnStr);
                    break;
                case DataBaseType.SQLSERVER:
                    SqlHelper helper = new SqlHelper();
                    dt = helper.GetDataTable(sql, item.sourceDBConnStr);
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
