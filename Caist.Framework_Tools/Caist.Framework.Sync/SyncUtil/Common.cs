using SyncModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncUtil
{
    public class Common
    {
        public static string GetConfigValue(string key)
        {
            string str = string.Empty;
            if (!string.IsNullOrWhiteSpace(key))
            {
                str = ConfigurationManager.AppSettings[key];
            }
            return str;
        }
    }

}
