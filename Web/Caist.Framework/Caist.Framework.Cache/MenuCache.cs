using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Caist.Framework.Cache.Factory;
using Caist.Framework.Entity.SystemManage;
using Caist.Framework.Service.SystemManage;

namespace Caist.Framework.Cache
{
    public class MenuCache
    {
        private string cacheKey = typeof(MenuCache).Name;

        private MenuService menuService = new MenuService();

        public async Task<List<MenuEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<List<MenuEntity>>(cacheKey);
            if (cacheList == null)
            {
                var data = await menuService.GetList(null);
                var list = data.ToList();
                CacheFactory.Cache().AddCache(cacheKey, list);
                return list;
            }
            else
            {
                return cacheList;
            }
        }

        public void Remove()
        {
            CacheFactory.Cache().RemoveCache(cacheKey);
        }

    }
}
