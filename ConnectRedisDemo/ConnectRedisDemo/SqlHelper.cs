using MySql.Data.MySqlClient;
using System;
using System.Configuration;
using System.Data;
using System.Text;

namespace ConnectRedisDemo
{
    public class SqlHelper
    {
        static string strContr = ConfigurationManager.ConnectionStrings["plcData"].ConnectionString;
    
        public static void SaveToMK_PLC_TONGFENG_VALUES()
        {


            IDbConnection conn = new MySqlConnection(strContr);


            string sql = "INSERT INTO mk_plc_tongfeng_values (id ,dict_Id ,sysId ,dict_value ,unit ,create_time ) VALUES(id, 'dict_Id', 'sysId', 'dict_value', 'unit', 'create_time')";
        }

        public static DataTable QuerySql(string sql)
        {
            var i = 0;
            DataTable dt = null;
            try
            {
                using (MySqlConnection connection = new MySqlConnection(strContr))
                {
                    try
                    {
                        connection.Open();
                        dt = new DataTable();
                        MySqlCommand command = new MySqlCommand(sql, connection);
                        MySqlDataAdapter msd = new MySqlDataAdapter(command);
                        msd.Fill(dt);
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return dt;
        }

        public static int ExcuteSql(StringBuilder sb)
        {
            var i = 0;
            try
            {
                var strContr = ConfigurationManager.ConnectionStrings["plcData"].ConnectionString;


                using (MySqlConnection connection = new MySqlConnection(strContr))
                {
                    try
                    {
                        connection.Open();
                        MySqlCommand command = new MySqlCommand();
                        command.Connection = connection;
                        command.CommandType = CommandType.Text;
                        command.CommandText = sb.ToString();

                        i = command.ExecuteNonQuery();
                    }
                    catch (System.Data.SqlClient.SqlException ex)
                    {
                    }
                    finally
                    {
                        connection.Close();
                    }
                    return i;
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
