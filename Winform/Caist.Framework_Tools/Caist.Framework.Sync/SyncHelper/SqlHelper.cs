using Caist.Framework.DataAccess;
using Caist.Framework.IdGenerator;
using SyncModel;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace SyncDataAccess
{
    public class SqlHelper : IHelper
    {
        public SqlHelper()
        {
        }

        public DataTable GetDataTable(string sql, string connstr)
        {
            DataTable dt = new DataTable();
            try
            {
                using (var conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.CommandType = CommandType.Text;
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(dt);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public bool InsertData(string sql, string connstr)
        {
            bool flag = false;
            try
            {
                using (var conn = new SqlConnection(connstr))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(sql, conn);
                    command.CommandType = CommandType.Text;
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }



        public async Task<DataTable> GetDataTableAsync(string sql, string connStr, DataEmun dbType)
        {
            using (var conn = Connect.GetConn(dbType.ToString(), connStr))
            {
                return await conn.GetDataTableAsync(sql, null, null, 60);
            }
        }

        public async Task<bool> InsertDataAsync(string sql, string connStr, DataEmun dbType)
        {
            using (var conn = Connect.GetConn(dbType.ToString(), connStr))
            {
                return await conn.InsertAsync(sql) > 0;
            }
        }

        public async Task<bool> ExcuteAsync(string sql, string connStr, DataEmun dbType)
        {
            using (var conn = Connect.GetConn(dbType.ToString(), connStr))
            {
                return await conn.ExcuteSQLAsync(sql, null) > 0;
            }
        }
        public async Task<DataTable> GetTableFields(string tableName, string connStr, DataEmun dbType)
        {
            string sql = $"select LOWER(column_name)as column_name from information_schema.columns where table_name='{tableName}' order by column_name;";
            return await GetDataTableAsync(sql, connStr, dbType);
        }

        /// <summary>
        /// 将DataTable数据批量插入到数据库中。
        /// </summary>
        /// <param name="dbm">相关参数实体</param>
        /// <param name="dataTable">插入的数据源</param>
        /// <param name="batchSize">批量大小</param>
        public void InsertBulk(DataBaseModel dbm, DataTable dataTable, int batchSize = 10000)
        {
            using (var conn = Connect.GetConn(dbm.TargetDBType.ToString(), dbm.TargetDBConnStr))
            {
                conn.Open();
                using (SqlBulkCopy bulk = new SqlBulkCopy((SqlConnection)conn, SqlBulkCopyOptions.Default, null))
                {
                    bulk.BatchSize = batchSize;
                    bulk.DestinationTableName = dbm.TargetTable;
                    bulk.ColumnMappings.Clear();
                    var dict = GetSameFields(dbm);
                    foreach (var item in dict)
                    {
                        bulk.ColumnMappings.Add(item.Key, item.Value);
                    }
                    foreach (var item in dbm.TableFields)//添加配置的不同字段集合
                    {
                        bulk.ColumnMappings.Add(item.Split(',')[0], item.Split(',')[1]);
                    }
                    if (bulk.ColumnMappings.Count == 0)
                    {
                        foreach (DataColumn column in dataTable.Columns)
                        {
                            bulk.ColumnMappings.Add(column.ColumnName, column.ColumnName.ToLower());
                        }
                    }
                    bulk.NotifyAfter = 100;
                    bulk.WriteToServer(dataTable);
                }
                dataTable.Dispose();
            }
        }

        private Dictionary<string, string> GetSameFields(DataBaseModel dbm)
        {
            Dictionary<string, string> res = new Dictionary<string, string>();
            if (!string.IsNullOrWhiteSpace(dbm.SourceTable))//源表可能没有设定指定的来源表（有多个数据表来源）
            {
                var dtSource = GetTableFields(dbm.SourceTable, dbm.SourceDBConnStr, dbm.SourceDBType).Result;
                var dtTarget = GetTableFields(dbm.TargetTable, dbm.TargetDBConnStr, dbm.TargetDBType).Result;
                for (var i = 0; i < dtSource.Rows.Count; i++)
                {
                    var drs = dtTarget.Select($"column_name = '{dtSource.Rows[i]["column_name"].ToString().ToLower()}'");
                    if (drs.Length > 0)
                    {
                        res.Add(dtSource.Rows[i][0].ToString(), dtTarget.Rows[i][0].ToString());
                    }
                }
            }
            return res;
        }
    }
}
