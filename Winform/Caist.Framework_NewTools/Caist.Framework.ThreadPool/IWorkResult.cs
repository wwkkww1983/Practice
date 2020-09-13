using System;

namespace Caist.Framework.ThreadPool
{
    public interface IWorkResult
    {
        object Result { get; set; }
        Exception Exception { get; set; }
    }
}
