using Caist.Framework.DataAccess;
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
                return await conn.GetDataTableAsync(sql);
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
    }
}
