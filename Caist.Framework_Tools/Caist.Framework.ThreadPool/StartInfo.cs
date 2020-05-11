using System;

namespace Caist.Framework.ThreadPool
{
    /// <summary>
    /// we should adjust these parameters by using scenario, such as long term or short term
    /// </summary>
    public class StartInfo
    {
        /// <summary>
        /// 默认值：min=2
        /// </summary>
        public Int32 MinWorkerThreads { get; set; }
        /// <summary>
        /// default: max = 10
        /// </summary>
        public Int32 MaxWorkerThreads { get; set; }
        /// <summary>
        /// default: drop = never, MaxQueue has no effect when drop = never
        /// </summary>
        public DropEnum DropEnum { get; set; }
        /// <summary>
        /// 默认值：timeout=60s，基于秒
        /// </summary>
        public Int32 Timeout { get; set; }
        /// <summary>
        /// default: interval = 1s, based on second
        /// </summary>
        public Int32 AdjustInterval { get; set; }
        /// <summary>
        /// default: queue = 1000
        /// </summary>
        public Int32 MaxQueueCount { get; set; }

        public StartInfo()
        {
            MinWorkerThreads = 2;
            MaxWorkerThreads = 10;
            DropEnum = DropEnum.Never;
            Timeout = 60;
            AdjustInterval = 1;
            MaxQueueCount = 1000;
        }
    }

    public enum DropEnum
    {
        /// <summary>
        /// unlimited queue length, never drop
        /// </summary>
        Never = 0,
        /// <summary>
        /// when queue is full, drop the new coming
        /// </summary>
        DropNewest = 1,
        /// <summary>
        /// when queue is full, dequeue the head and drop it
        /// </summary>
        DropOldest = 2
    }
}
