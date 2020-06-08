using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caist.Framework.Cache
{
    interface IBusinessCache<T>
    {
        Task<List<T>> GetList();

        void Update(long id);

        void Remove(long id);
    }
}
