using System.Data;
using System.Data.SqlClient;

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
    }
}
