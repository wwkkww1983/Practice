using System;

namespace Caist.Framework.ThreadPool
{
    internal class WorkResult : IWorkResult
    {
        public object Result
        {
            get;
            set;
        }

        public Exception Exception
        {
            get;
            set;
        }
    }
}
