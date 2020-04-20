using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ConnectRedisDemo
{
    class Program
    {
        #region 变量
        static Dictionary<string, string> _tableName = new Dictionary<string, string>();
        static string _keyPart = "RedisKeyPart".GetConfigValue();
        static int _count = 500;
        static string _sysId = string.Empty;
        static string _order = string.Empty;
        static string _filePath = string.Empty;
        static string _fileName = string.Empty; 
        #endregion
        static void Main(string[] args)
        {
            #region delete key test
            //ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1");
            //StackExchange.Redis.IDatabase db = redis.GetDatabase(Convert.ToInt32(0));
            //db.KeyDelete("king");  
            #endregion

            Start();
        }

        private static void Start()
        {
            Console.WriteLine("请输入要执行的编号：1、读取基础数据到表；2、从redisDB 读取到数据库；");
            var option = Console.ReadLine();
            while (option != "1" && option != "2")
            {
                Console.WriteLine("请输入要执行的编号：1、读取基础数据到表；2、从redisDB 读取到数据库；");
                option = Console.ReadLine();
            }

            if (option == "1")
            {
                Console.WriteLine("请输入文件绝对路径：");
                _filePath = Console.ReadLine();

                _fileName = _filePath.GetFileName();
                //Console.WriteLine("请输入sysId,从 mk_plc_systeminfo 获取：");
                //_sysId = Console.ReadLine();

                Console.WriteLine("请输入文件中键值对的顺序：1、名称在左边；2、名称在右边；");
                option = Console.ReadLine();
                while (option != "1" && option != "2")
                {
                    Console.WriteLine("请输入文件中键值对的顺序：1、名称在左边；2、名称在右边；");
                    option = Console.ReadLine();
                }
                _order = option;
                //写数据到 mk_plc_dictionary 基础数据表
                WritePLCData();
            }
            else
            {
                //初始化用哪张表
                InitTableName();
                //从redis读取数据并保存到数据库 mk_plc_tongfeng_values
                ReadRedis();
            }
            Console.WriteLine("success!!!");
            Console.ReadKey();
        }

        private static void InitTableName()
        {
            //string jsonfile = "JsonPath".GetConfigValue();
            //using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            //{
            //    using (JsonTextReader reader = new JsonTextReader(file))
            //    {
            //        var o = JToken.ReadFrom(reader).ToString();
            //        _tableName = JsonConvert.DeserializeObject<JsonModel>(o);
            //    }
            //}

            List<string> listStr = Common.ReadText("TextPath".GetConfigValue());
            _tableName = listStr.Select(p => p.Split(':')).ToDictionary(k => k[0], v => v[1]);
        }

        private static object ReadRedis()
        {
            DataTable listKeys = ReadDictionarys();
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("RedisDB".GetConfigValue());
            var dbs = "DBS".GetConfigValue();
            if (dbs.HasValue() && listKeys.Rows.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                var dbss = dbs.Split(',');
                string tempName = string.Empty;
                foreach (var item in dbss)//redis DB 数据库
                {
                    #region local test
                    //StackExchange.Redis.IDatabase db = redis.GetDatabase(0);
                    //while (true)
                    //{
                    //    foreach (DataRow dr in listKeys.Rows)
                    //    {
                    //        #region 筛选同IP段遍历
                    //        var values = db.ListRange("king");
                    //        if (values.Length == 0)
                    //            continue;
                    //        foreach (var v in values)//根据键值获取redis list
                    //        {
                    //            Console.WriteLine(v);
                    //        }
                    //        db.KeyDelete("king"); //移除键
                    //        #endregion
                    //    }
                    //}
                    #endregion
                    #region product 
                    StackExchange.Redis.IDatabase db = redis.GetDatabase(Convert.ToInt32(item));

                    while (true)
                    {
                        foreach (DataRow dr in listKeys.Rows)
                        {
                            #region 筛选同IP段遍历
                            var key = _keyPart + dr["dic_code"].ToString();
                            var values = db.ListRange(key);
                            if (values.Length == 0)
                                continue;
                            int i = 0;
                            foreach (var v in values)//根据键值获取redis list
                            {
                                i++;
                                var rm = JsonConvert.DeserializeObject<RedisModel>(v);
                                tempName = GetTempName(rm.Ip);
                                if (dr["ip"].ToString() == rm.Ip && tempName.HasValue())
                                {
                                    sb.AppendLine($"INSERT INTO mk_plc_{tempName}_values (id ,dict_Id ,sysId ,dict_value ,create_time ) VALUES('{Guid.NewGuid().ToString().Replace("-", string.Empty)}', '{dr["dict_Id"]}', '{dr["sysId"]}', '{rm.Value}', '{rm.SendTime}');");
                                }
                                if ((i > _count - 1 && i % _count == 0) || (i == values.Length))
                                {
                                    if (sb.ToString().HasValue())
                                    {
                                        SqlHelper.ExcuteSql(sb);
                                        sb.Clear();
                                    }
                                }
                            }
                            db.KeyDelete(key); //移除键
                            #endregion
                        }
                    } 
                    #endregion
                }
            }
            return 0;
        }

        //增加系统需修改此处
        private static string GetTempName(string ip)
        {
            var dit = _tableName.Where(p => p.Value.Contains(ip));
            if (dit != null)
            {
                return dit.FirstOrDefault().Key;
            }
            return string.Empty;
        }

        private static DataTable ReadDictionarys()
        {
            string sql = "select e.ip,e.sysId,e.sysName,d.dict_Id,d.dic_code from mk_plc_systeminfo e inner join mk_plc_dictionary d on e.sysId = d.sysId";
            return SqlHelper.QuerySql(sql);
        }

        #region 读取基础数据写入数据库mk_plc_dictionary
        //维护mk_plc_dictionary 基础表
        private static int WritePLCData()
        {
            _sysId = GetSysIdForIp();
            if (_sysId.HasValue())
            {
                //判断是否存在
                List<string> listStr = FilterData(Common.ReadText(_filePath));
                StringBuilder sb = new StringBuilder();
                foreach (var strs in listStr)
                {
                    var sts = strs.Split(':');
                    if (_order == "1")
                    {
                        sb.AppendLine($"INSERT INTO mk_plc_dictionary (dict_Id ,sysId ,dict_name ,dic_code ,create_time ) VALUES ('{Guid.NewGuid().ToString().Replace("-", string.Empty)}','{_sysId}','{sts[0]}','{sts[1]}','{System.DateTime.Now}');");
                    }
                    else
                    {
                        sb.AppendLine($"INSERT INTO mk_plc_dictionary (dict_Id ,sysId ,dict_name ,dic_code ,create_time ) VALUES ('{Guid.NewGuid().ToString().Replace("-", string.Empty)}','{_sysId}','{sts[1]}','{sts[0]}','{System.DateTime.Now}');");
                    }
                }
                if (sb.ToString().HasValue())
                {
                    return SqlHelper.ExcuteSql(sb);
                }
                else
                {
                    Console.WriteLine("重复导入！");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("数据库未配置或者文件名输入有误！");
                Console.ReadKey();
            }
            return 0;
        }

        /// <summary>
        /// 避免重复导入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private static List<string> FilterData(List<string> list)
        {
            var lists = string.Join("','", list.Select(p => p.Split(':')).ToDictionary(k => k[0], v => v[1]).Values);
            string sql = $"select sysId,dic_code from mk_plc_dictionary where sysId='{_sysId}' and dic_code in('{lists}')";
            var dt = SqlHelper.QuerySql(sql);
            if (dt.HasValue())
            {
                foreach (DataRow dr in dt.Rows)
                {
                    list.Remove(list.Find(p=>p.Contains(dr["dic_code"].ToString())));
                }
            }
            return list;
        }

        private static string GetSysIdForIp()
        {
            string sql = $"select sysId from mk_plc_systemInfo where ip='{_fileName}'";
            var dt = SqlHelper.QuerySql(sql);
            return dt.HasValue() ? dt.Rows[0][0].ToString() : string.Empty;
        }

        #endregion
    }
}
