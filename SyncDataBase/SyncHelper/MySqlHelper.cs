using MySql.Data.MySqlClient;
using System.Data;

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
            catch (System.Exception)
            {

                throw;
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
            catch (System.Exception)
            {
                throw;
            }
            return flag;
        }
    }
}
