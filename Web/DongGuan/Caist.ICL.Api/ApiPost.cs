using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.ICL.Api
{
    /// <summary>
    /// API输入参数集合
    /// ljl@2018
    /// </summary>
    public class ApiPost : Dictionary<string, object>
    {

        public string GetString(string name)
        {
            return this[name].ToString();
        }

        public List<string> GetList(string name)
        {
            var data = this[name];
            if (data is JArray)
            {
                return (data as JArray).Select(w => w.ToString()).ToList();
            }
            return new List<string>();
        }


        public int GetInt(string name)
        {
            return Convert.ToInt32(this[name]);
        }

    }
}
