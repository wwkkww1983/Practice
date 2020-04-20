using Caist.Framework.Cache.Factory;
using Caist.Framework.Entity.ApplicationManage;
using Caist.Framework.Service.ApplicationManage;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caist.Framework.Cache
{
    public class ViewFunctionCache
    {
        private string cacheKey = typeof(ViewFunctionCache).Name;

        private ViewFunctionService viewFunctionService = new ViewFunctionService();

        public async Task<List<ViewFunctionEntity>> GetList()
        {
            var cacheList = CacheFactory.Cache().GetCache<List<ViewFunctionEntity>>(cacheKey);
            if (cacheList == null)
            {
                var data = await viewFunctionService.GetList(null);
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
