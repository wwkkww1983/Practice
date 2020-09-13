using Caist.Framework.DataAccess;
using MySql.Data.MySqlClient;
using SyncCommon;
using System.Data;
using System.Threading.Tasks;

namespace SyncDataAccess
{
    public class MySqlHelper : IHelper
    {
        public MySqlHelper()
        {
        }
        public DataTable GetDataTable(string sql, string connstr)
        {
            DataTable dt = new DataTable();

            try
            {
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    command.CommandType = CommandType.Text;
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
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
                using (var conn = new MySqlConnection(connstr))
                {
                    conn.Open();
                    MySqlCommand command = new MySqlCommand(sql, conn);
                    command.CommandType = CommandType.Text;
                    return command.ExecuteNonQuery() > 0;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            return flag;
        }


        public async Task<DataTable> GetDataTableAsync(string sql, string connStr, DataEmun dbType)
        {
            try
            {
                using (var conn = Connect.GetConn(dbType.ToString(), connStr))
                {
                    return await conn.GetDataTableAsync(sql);
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> InsertDataAsync(string sql, string connStr, DataEmun dbType)
        {
            try
            {
                using (var conn = Connect.GetConn(dbType.ToString(), connStr))
                {
                    return await conn.InsertAsync(sql) > 0;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> ExcuteAsync(string sql, string connStr, DataEmun dbType)
        {
            try
            {
                using (var conn = Connect.GetConn(dbType.ToString(), connStr))
                {
                    return await conn.ExcuteSQLAsync(sql, null) > 0;
                }
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
