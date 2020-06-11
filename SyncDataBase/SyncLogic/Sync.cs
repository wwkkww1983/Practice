using Caist.Framework.DataAccess;
using Newtonsoft.Json;
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
        //一次执行数据条数
        readonly int _count = Convert.ToInt32(Common.GetConfigValue("MaxOnceData"));

        #region 同步方法

        //public bool SyncData(ref string res)
        //{
        //    try
        //    {
        //        string txt = FileOperation.ReadText(Common.GetConfigValue("ConfigPath"));
        //        if (txt.HasValue())
        //        {
        //            var models = JsonConvert.DeserializeObject<List<DataBaseModel>>(txt);
        //            return Excute(models);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        res = ex.Message;
        //    }
        //    return false;
        //}

        //private bool Excute(List<DataBaseModel> models)
        //{
        //    bool flag = false;
        //    try
        //    {
        //        foreach (var item in models)
        //        {
        //            SqlToData(out flag, item);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return flag;
        //}

        //private void SqlToData(out bool flag, DataBaseModel item)
        //{
        //    string sql = string.Empty;
        //    string sameFields = string.Empty;
        //    if (item.SourceSql.HasValue())//多表的情况
        //    {
        //        sql = item.SourceSql;
        //        DeleteTargetTableData(item);
        //    }
        //    else
        //    {
        //        //生成查询SQL
        //        sql = item.ToSelectSql();
        //        //找到相同的字段
        //        sameFields = FindSameFieldsForSingle(item);
        //        sql = sql.Replace("(placeholder)", sameFields);
        //        //增量插入查询
        //        if (item.SyncPartial)
        //        {
        //            string point = GetPoint(item);
        //            if (point.HasValue())
        //            {
        //                sql += $" where {item.FlagField} > '{point.Replace("\r\n", string.Empty)}'";
        //            }
        //        }
        //        else
        //        {
        //            DeleteTargetTableData(item);
        //        }
        //    }
        //    var dt = Query(sql, item.SourceDBConnStr, item.SourceDBType);
        //    sql = item.ToInsertSql();
        //    if (item.SourceSql.HasValue())
        //    {
        //        sameFields = FindSameFieldsForMultiple(dt);
        //    }
        //    sql = sql.Replace("(placeholder)", sameFields);
        //    flag = ExcuteInsertSql(sql, dt, item);
        //    if (item.SyncPartial && item.FlagField.HasValue())
        //    {
        //        SavePoint(item, dt);
        //    }
        //}

        //#region 查找相同字段名(源数据单表的情况下)
        //private string FindSameFieldsForSingle(DataBaseModel baseModel)
        //{
        //    string strSqlSource = string.Empty;
        //    string strSqlTarget = string.Empty;
        //    switch (baseModel.SourceDBType)
        //    {
        //        case DataEmun.SQLServer:
        //        case DataEmun.MySQL:
        //            strSqlSource = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{baseModel.SourceTable}';";
        //            strSqlTarget = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{baseModel.TargetTable}';";
        //            break;
        //        case DataEmun.Oracle:
        //            break;
        //        default:
        //            break;
        //    }
        //    var dtSource = Query(strSqlSource, baseModel.SourceDBConnStr, baseModel.SourceDBType);
        //    var dtTarget = Query(strSqlTarget, baseModel.TargetDBConnStr, baseModel.TargetDBType);
        //    return CompareDataTable(dtSource, dtTarget);
        //}

        //private string CompareDataTable(DataTable dtSource, DataTable dtTarget)
        //{
        //    string str = string.Empty;
        //    StringBuilder res = new StringBuilder();
        //    foreach (DataRow dataRow in dtSource.Rows)
        //    {
        //        var drs = dtTarget.Select($"column_name = '{dataRow["column_name"].ToString().ToLower()}'");
        //        if (drs.Length > 0)
        //        {
        //            res.Append($"{dataRow["column_name"]},");
        //        }
        //    }
        //    if (res.Length > 0)
        //    {
        //        str = res.ToString().TrimEnd(',');
        //    }
        //    return str;
        //}
        //#endregion

        //#region 查找目标字段表名(源数据多表的情况下)
        //private string FindSameFieldsForMultiple(DataTable dt)
        //{
        //    //根据源table表来获取字段
        //    string res = string.Empty;
        //    StringBuilder str = new StringBuilder();
        //    if (dt.HasData())
        //    {
        //        foreach (DataColumn dc in dt.Columns)
        //        {
        //            str.Append(dc.ColumnName + ",");
        //        }
        //        res = str.ToString().TrimEnd(',');
        //    }
        //    return res;
        //}

        ////private string GetTableFields(DataTable dtTarget)
        ////{
        ////    string str = string.Empty;
        ////    StringBuilder res = new StringBuilder();
        ////    foreach (DataRow dataRow in dtTarget.Rows)
        ////    {
        ////        res.Append($"{dataRow["column_name"]},");
        ////    }
        ////    if (res.Length > 0)
        ////    {
        ////        str = res.ToString().TrimEnd(',');
        ////    }
        ////    return str;
        ////}
        //#endregion

        //private static string GetPoint(DataBaseModel model)
        //{
        //    string path = GetPointPath(model);
        //    return FileOperation.ReadText(path);
        //}

        //private static string GetPointPath(DataBaseModel model)
        //{
        //    var dicty = Common.GetConfigValue("SyncPointsFolder");
        //    //数据库名+表名
        //    string path = Path.Combine(dicty, model.SourceDB + "_" + model.SourceTable + ".txt");
        //    return path;
        //}

        //private void SavePoint(DataBaseModel item, DataTable dt)
        //{
        //    if (dt.HasData())
        //    {
        //        var dv = new DataView(dt);
        //        dv.Sort = $"{item.FlagField} desc";
        //        dt = dv.ToTable();
        //        var point = dt.Rows[0][item.FlagField].ToString();
        //        string path = GetPointPath(item);
        //        FileOperation.WriteText(path, point);
        //    }
        //}

        //private bool DeleteTargetTableData(DataBaseModel item)
        //{
        //    string sql = string.Empty;
        //    switch (item.TargetDBType)
        //    {
        //        case DataEmun.SQLServer:
        //            sql = $"delete from {item.TargetDB}.dbo.{item.TargetTable};";
        //            break;
        //        case DataEmun.Oracle:
        //        case DataEmun.MySQL:
        //            sql = $"delete from {item.TargetDB}.{item.TargetTable};";
        //            break;
        //        default:
        //            break;
        //    }
        //    return ExcuteSQL(sql, item.TargetDBType, item.TargetDBConnStr);
        //}

        //private bool ExcuteInsertSql(string sql, DataTable dt, DataBaseModel item)
        //{
        //    bool flag = false;
        //    if (dt.HasData())
        //    {
        //        StringBuilder sb = new StringBuilder();
        //        StringBuilder strs = new StringBuilder();
        //        for (var i = 0; i < dt.Rows.Count; i++)
        //        {
        //            for (int j = 0; j < dt.Columns.Count; j++)
        //            {
        //                strs.Append($"'{dt.Rows[i][j]}',");
        //            }
        //            var strsql = string.Format(sql, strs.ToString().TrimEnd(','));
        //            strs.Clear();
        //            sb.Append(strsql + ";");
        //            if ((i > _count - 1 && i % _count == 0) || (i == dt.Rows.Count - 1))
        //            {
        //                if (sb.ToString().HasValue())
        //                {
        //                    flag = ExcuteSQL(sb.ToString(), item.TargetDBType, item.TargetDBConnStr);
        //                    sb.Clear();
        //                    if (!flag)
        //                    {
        //                        return flag;
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    return flag;
        //}

        //private bool ExcuteSQL(string sql, DataEmun dbType, string dbConnStr)
        //{
        //    bool flag = false;
        //    switch (dbType)
        //    {
        //        case DataEmun.MySQL:
        //            MySqlHelper mysqlhelper = new MySqlHelper();
        //            flag = mysqlhelper.InsertData(sql, dbConnStr);
        //            break;
        //        case DataEmun.SQLServer:
        //            SqlHelper helper = new SqlHelper();
        //            flag = helper.InsertData(sql, dbConnStr);
        //            break;
        //        case DataEmun.Oracle:
        //            break;
        //        default:
        //            break;
        //    }
        //    return flag;
        //}

        //private DataTable Query(string sql, string dbConnStr, DataEmun dataBaseType)
        //{
        //    var dt = new DataTable();
        //    switch (dataBaseType)
        //    {
        //        case DataEmun.MySQL:
        //            MySqlHelper mysqlhelper = new MySqlHelper();
        //            dt = mysqlhelper.GetDataTable(sql, dbConnStr);
        //            break;
        //        case DataEmun.SQLServer:
        //            SqlHelper helper = new SqlHelper();
        //            dt = helper.GetDataTable(sql, dbConnStr);
        //            break;
        //        case DataEmun.Oracle:
        //            break;
        //        default:
        //            break;
        //    }
        //    return dt;
        //}
        #endregion

        #region 异步方法

        public async Task<Tuple<string, bool>> SyncDataAsync()
        {
            var res = string.Empty;
            var flag = false;
            try
            {
                string txt = FileOperation.ReadText(Common.GetConfigValue("ConfigPath"));
                if (txt.HasValue())
                {
                    var models = JsonConvert.DeserializeObject<List<DataBaseModel>>(txt);
                    flag = await ExcuteAsync(models);
                }
            }
            catch (Exception ex)
            {
                res = $"{ex.Message}\r\n{ex.StackTrace}";
            }

            return Tuple.Create<string, bool>(res, flag);
        }

        private async Task<bool> ExcuteAsync(List<DataBaseModel> models)
        {
            bool flag = false;
            try
            {
                foreach (var item in models)
                {
                    flag = await SqlToDataAsync(item);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return flag;
        }

        private async Task<bool> SqlToDataAsync(DataBaseModel item)
        {
            string sql = string.Empty;
            string sameFields = string.Empty;
            bool flag = false;
            if (item.SourceSql.HasValue())//多表的情况
            {
                sql = item.SourceSql;
                await DeleteTargetTableDataAsync(item);
            }
            else
            {
                //生成查询SQL
                sql = item.ToSelectSql();
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
            sql = item.ToInsertSql();
            if (item.SourceSql.HasValue())
            {
                sameFields = FindSameFieldsForMultiple(dt);
            }
            sql = sql.Replace("(placeholder)", sameFields);
            flag = await ExcuteInsertSqlAsync(sql, dt, item);
            if (item.SyncPartial && item.FlagField.HasValue())
            {
                SavePoint(item, dt);
            }
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
                    strSqlSource = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{baseModel.SourceTable}';";
                    strSqlTarget = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{baseModel.TargetTable}';";
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
                StringBuilder sb = new StringBuilder();
                StringBuilder strs = new StringBuilder();
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
                            flag = await ExcuteSQLAsync(sb.ToString(), item.TargetDBConnStr, item.TargetDBType);
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

        private async Task<bool> ExcuteSQLAsync(string sql, string connStr, DataEmun dbType)
        {
            bool flag = false;
            switch (dbType)
            {
                case DataEmun.MySQL:
                    MySqlHelper mysqlhelper = new MySqlHelper();
                    flag = await mysqlhelper.ExcuteAsync(sql, connStr, DataEmun.MySQL);
                    break;
                case DataEmun.SQLServer:
                    SqlHelper helper = new SqlHelper();
                    flag = await helper.ExcuteAsync(sql, connStr, DataEmun.SQLServer);
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
            var dt = new DataTable();
            switch (dataBaseType)
            {
                case DataEmun.MySQL:
                    MySqlHelper mysqlhelper = new MySqlHelper();
                    dt = await mysqlhelper.GetDataTableAsync(sql, connStr, DataEmun.MySQL);
                    break;
                case DataEmun.SQLServer:
                    SqlHelper helper = new SqlHelper();
                    dt = await helper.GetDataTableAsync(sql, connStr, DataEmun.SQLServer);
                    break;
                case DataEmun.Oracle:
                    break;
                default:
                    break;
            }
            return dt;
        }
        #endregion
    }
}
