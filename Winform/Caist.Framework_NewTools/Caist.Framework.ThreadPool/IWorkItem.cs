using System;
using System.Threading;

namespace Caist.Framework.ThreadPool
{
    public interface IWorkItem
    {
        WorkItemCallback Callback { get; }
        string Name { get; }
        object State { get; }
        WaitHandle WaitHandle { get; }
        IWorkResult Result { set; }
        bool IsCancel { get; }

        IWorkResult GetResult();
        IWorkResult GetResult(int millisecondsTimeout);
        IWorkResult GetResult(TimeSpan timeout);
        void Cancel();
    }
}
