using Newtonsoft.Json;
using System.Data;
using System.IO;
using System.Net.Http;

namespace HikvisionDemo
{
    public static class CommonExtends
    {
        public static bool HasValue(this string value)
        {
            return value != null && !string.IsNullOrEmpty(value);
        }
        public static bool HasData(this DataTable dt)
        {
            return dt != null && dt.Rows.Count > 0;
        }
        public static string ToJson(this PostParam pp)
        {
            return pp != null ? JsonConvert.SerializeObject(pp) : string.Empty;
        }
        //public static string ToJson(this HttpResponseMessage hrm)
        //{
        //    string res = string.Empty;
        //    Stream stream = hrm.Content.ReadAsStringAsync
        //    return pp != null ? JsonConvert.SerializeObject(pp) : string.Empty;
        //}
    }
}
