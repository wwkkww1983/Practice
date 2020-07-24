using System;
using System.Collections.Generic;
using System.Text;
using Caist.ICL.Core.Entitys;

namespace Caist.ICL.Core.Dao
{
    public interface ISnapshot<T>
    {
        void Update(T newEntity);
    }
}
