using System;
using System.Collections.Generic;
using System.Text;
using JL.Done.Core.Entitys;

namespace JL.Done.Core.Dao
{
    public interface ISnapshot<T>
    {
        void Update(T newEntity);
    }
}
