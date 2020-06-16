using Newtonsoft.Json;
using SyncModel;
using SyncUtil;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Caist.Framework.DataAccess.Cache
{
    public class SyncCache
    {
        /// <summary>
        /// Cache
        /// </summary>
        private static readonly ConcurrentDictionary<string, List<DataBaseModel>> keyValueDict = new ConcurrentDictionary<string, List<DataBaseModel>>();
        private static readonly object _locker = new object();
        public static List<DataBaseModel> GetConfigDataTable(string key)
        {
            if (!keyValueDict.Keys.Contains(key))
            {
                lock (_locker)
                {
                    string txt = FileOperation.ReadText(Common.GetConfigValue("ConfigPath"));
                    if (txt.HasValue())
                    {
                        var models = JsonConvert.DeserializeObject<List<DataBaseModel>>(txt);
                        keyValueDict[key] = models;
                    }
                }
            }

            return keyValueDict[key];
        }
    }
}
