using Caist.Framework.Cache.Factory;
using Caist.Framework.Util;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Cache
{
    public class PeopleCache
    {
        private readonly string cacheKey = typeof(PeopleCache).Name;

        public async Task<string> GetSQL()
        {
            var sql = CacheFactory.Cache().GetCache<string>(cacheKey);
            if (sql == null)
            {
                using (StreamReader sr = new StreamReader(GlobalContext.SystemConfig.PeoplePositionSqlPath, Encoding.UTF8))
                {
                    sql = sr.ReadToEnd();
                }
                CacheFactory.Cache().AddCache(cacheKey, sql);
                return sql;
            }
            else
            {
                return sql;
            }
        }

        public void Remove()
        {
            CacheFactory.Cache().RemoveCache(cacheKey);
        }

    }
}
