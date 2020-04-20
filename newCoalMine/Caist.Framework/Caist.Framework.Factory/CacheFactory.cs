using Caist.Framework.Cache.Interface;
using Caist.Framework.MemoryCache;

namespace Caist.Framework.Cache.Factory
{
    public class CacheFactory
    {
        public static ICache Cache()
        {
            return new MemoryCacheImp();
        }
    }
}
