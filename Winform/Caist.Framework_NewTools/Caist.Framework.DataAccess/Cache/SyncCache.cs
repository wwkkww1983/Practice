using Caist.Framework.ThreadPool;
using Newtonsoft.Json;
using SyncModel;
using SyncUtil;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace Caist.Framework.DataAccess.Cache
{
    public class SyncCache
    {
        /// <summary>
        /// Cache
        /// </summary>
        private static readonly ConcurrentDictionary<string, List<DataBaseModel>> _keyValueDict = new ConcurrentDictionary<string, List<DataBaseModel>>();
        private static readonly ConcurrentDictionary<string, string> _sqlDict = new ConcurrentDictionary<string, string>();
        private static readonly object _locker = new object();
        private static readonly object _locker1 = new object();
        public static List<DataBaseModel> GetConfigDataTable(string key)
        {
            if (!_keyValueDict.Keys.Contains(key))
            {
                lock (_locker)
                {
                    var path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Common.GetConfigValue("ConfigPath"));
                    string txt = FileOperation.ReadText(path);
                    if (txt.HasValue())
                    {
                        var models = JsonConvert.DeserializeObject<List<DataBaseModel>>(txt);
                        _keyValueDict[key] = models;
                    }
                }
            }

            return _keyValueDict[key];
        }

        /// <summary>
        /// 缓存配置的SQL语句
        /// </summary>
        public static string GetConfigSQL(string key)
        {
            if (!_sqlDict.Keys.Contains(key))
            {
                lock (_locker1)
                {
                    var path = Path.Combine(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase, Common.GetConfigValue("PeoplePosition"));
                    string txt = FileOperation.ReadText(path);
                    if (txt.HasValue())
                    {
                        _sqlDict[key] = txt;
                    }
                }
            }

            return _sqlDict[key];
        }
    }
}
