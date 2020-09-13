using MySql.Data.MySqlClient;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;

namespace Caist.Framework.DataAccess
{
    public class Connect
    {
        public static IDbConnection GetConn(string connStr)
        {
            IDbConnection connection = null;
            GetEnumDic<DataEmun>().Where(v => v.Key == connStr).ToList().ForEach(x => {
                
                switch ((DataEmun)Enum.Parse(typeof(DataEmun), x.Key))
                {
                    case DataEmun.SQLServer:
                        connection = new SqlConnection(ConfigurationManager.ConnectionStrings[connStr].ToString());
                        break;
                    case DataEmun.MySQL:
                        connection = new MySqlConnection(ConfigurationManager.ConnectionStrings[connStr].ToString());
                        break;
                    case DataEmun.Oracle:
                        connection = new OracleConnection(ConfigurationManager.ConnectionStrings[connStr].ToString());
                        break;
                    case DataEmun.SQLite:
                        connection = new SQLiteConnection(ConfigurationManager.ConnectionStrings[connStr].ToString());
                        break;
                    case DataEmun.Npgsql:
                        connection = new NpgsqlConnection(ConfigurationManager.ConnectionStrings[connStr].ToString());
                        break;
                }
            });
            return connection;
        }
        public static IDbConnection GetConn(string dbType, string connStr)
        {
            IDbConnection connection = null;
            GetEnumDic<DataEmun>().Where(v => v.Key == dbType).ToList().ForEach(x =>
            {

                switch ((DataEmun)Enum.Parse(typeof(DataEmun), x.Key))
                {
                    case DataEmun.SQLServer:
                        connection = new SqlConnection(connStr);
                        break;
                    case DataEmun.MySQL:
                        connection = new MySqlConnection(connStr);
                        break;
                    case DataEmun.Oracle:
                        connection = new OracleConnection(connStr);
                        break;
                    case DataEmun.SQLite:
                        connection = new SQLiteConnection(connStr);
                        break;
                    case DataEmun.Npgsql:
                        connection = new NpgsqlConnection(connStr);
                        break;
                }
            });
            return connection;
        }

        /// <summary>
        /// 将枚举转换成字典集合
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <returns></returns>
        public static Dictionary<string, int> GetEnumDic<T>()
        {

            Dictionary<string, int> resultList = new Dictionary<string, int>();
            Type type = typeof(T);
            var strList = GetNamesArr<T>().ToList();
            foreach (string key in strList)
            {
                string val = Enum.Format(type, Enum.Parse(type, key), "d");
                resultList.Add(key, int.Parse(val));
            }
            return resultList;
        }

        /// <summary>
        /// 获取枚举名称集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string[] GetNamesArr<T>()
        {
            return Enum.GetNames(typeof(T));
        }
    }

    public enum DataEmun
    {
        [Description("SQLServer")]
        SQLServer,
        [Description("MySQL")]
        MySQL,
        [Description("Oracle")]
        Oracle,
        [Description("SQLite")]
        SQLite,
        [Description("Npgsql")]
        Npgsql
    }
}
