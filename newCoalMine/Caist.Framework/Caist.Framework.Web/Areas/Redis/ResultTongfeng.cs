using Caist.Framework.Web.Areas.EnumCode;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Caist.Framework.Web.Areas.Redis
{
    public class ResultTongfeng
    {
        /// <summary>
        /// 订阅
        /// </summary>
        public static string Subscription(Tongfeng mainfan)
        {
            string val = string.Empty;
            try
            {
                var keyvlaue = GetDescriptionByName(mainfan);
                using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379"))
                {
                    ISubscriber sub = redis.GetSubscriber();
                    sub.Subscribe(keyvlaue.ToString(), (channel, message) =>
                    {
                        Thread.Sleep(1000);
                        val = message.ToString();
                        //输出收到的消息
                    });
                }
                if (val == null || val == "")
                    return "0";
            }
            catch (Exception)
            {
                return "0";
            }
            return val;
        }
        private static T ConvertObj<T>(RedisValue value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return default(T);
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
        }

        private static List<T> ConvetList<T>(RedisValue[] values)
        {
            List<T> result = new List<T>();
            foreach (var item in values)
            {
                var model = ConvertObj<T>(item);
                if (model != null)
                    result.Add(model);
            }
            return result;
        }

        /// <summary>
        /// get enum description by name
        /// </summary>
        /// <typeparam name="T">enum type</typeparam>
        /// <param name="enumItemName">the enum name</param>
        /// <returns></returns>
        /// 
        private static object GetDescriptionByName(Tongfeng enumItemName)
        {
            FieldInfo fi = enumItemName.GetType().GetField(enumItemName.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return enumItemName.ToString();
            }
        }
    }
}

