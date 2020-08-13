using Caist.Framework.Cache.Factory;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Cache
{
    public class MqThemeCache
    {
        private string cacheKey = typeof(MqThemeCache).Name;

        private MqThemeService mqThemeService = new MqThemeService();

        public async Task<List<MqThemeEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<List<MqThemeEntity>>(cacheKey);
            if (cacheList == null)
            {
                var data = await mqThemeService.GetList(null);
                var list = data.ToList();
                CacheFactory.Cache().AddCache(cacheKey, list);
                return list;
            }
            else
            {
                return cacheList;
            }
        }

    }
}
