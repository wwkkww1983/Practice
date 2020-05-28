using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;

namespace Caist.Framework.ThreadPool
{
    internal static class Util
    {
        internal static string Format(this Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format("message:{0}", ex.Message));
            sb.AppendLine(string.Format("StackTrace:{0}", ex.StackTrace));
            sb.AppendLine(string.Format("Source:{0}", ex.Source));
            if (null != ex.InnerException)
            {
                sb.AppendLine("Inner Exception:");
                sb.AppendLine(Format(ex.InnerException));
            }
            return sb.ToString();
        }
    }

    public static class CommonExtends
    {
        public static string ToJson<T>(this T t)
        {
            return JsonConvert.SerializeObject(t);
        }
        public static bool HasValue<T>(this List<T> t)
        {
            return t != null && t.Count > 0;
        }
        public static bool HasValue(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }
        /// <summary>
        /// 根据字符串获取配置文件值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="appSetting">默认为获取appsetting的值</param>
        /// <returns></returns>
        public static string GetConfigrationStr(this string key, bool appSetting = true)
        {
            string str = string.Empty;
            if (key.HasValue())
            {
                str = appSetting ? ConfigurationManager.AppSettings[key] : ConfigurationManager.ConnectionStrings[key].ConnectionString;
            }
            return str;
        }
    }
}
