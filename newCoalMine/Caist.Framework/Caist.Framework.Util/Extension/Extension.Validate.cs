using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Caist.Framework.Util.Extension
{
    public static partial class Extensions
    {
        public static bool IsEmpty(this object value)
        {
            if (value != null && !string.IsNullOrEmpty(value.ParseToString()))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool HasValue(this object value)
        {
            return value != null && !string.IsNullOrEmpty(value.ParseToString());
        }

        public static bool IsNullOrZero(this object value)
        {
            if (value == null || value.ParseToString().Trim() == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException("request");

            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }

        public static string RemoveNullValue<T>(this T v)
        {
            string str = string.Empty;
            if (v.HasValue())
            {
                str = JsonConvert.SerializeObject(v, new JsonSerializerSettings()
                {
                    NullValueHandling = NullValueHandling.Ignore
                });
            }
            return str;
        }
    }
}
