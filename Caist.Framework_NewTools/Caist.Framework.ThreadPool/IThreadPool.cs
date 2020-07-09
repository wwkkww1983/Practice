using System;

namespace Caist.Framework.ThreadPool
{
    public delegate object WorkItemCallback(object state);

    public interface IThreadPool
    {
        string Name { get; }
        StartInfo StartInfo { get; set; }
        int QueueCount { get; }
        int ThreadCount { get; }
        int MaxThreadCount { get; }

        IWorkItem QueueUserWorkItem(WorkItemCallback callback, Object state, String name = "");
        bool WaitAll();
        bool WaitAll(int millisecondsTimeout);
        bool WaitAll(TimeSpan timeout);
        void Close();
    }
}
