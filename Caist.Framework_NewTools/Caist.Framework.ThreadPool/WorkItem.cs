﻿using System;
using System.Threading;

namespace Caist.Framework.ThreadPool
{
    internal class WorkItem : IWorkItem
    {
        private ManualResetEvent _event;
        private IWorkResult _result;
        private bool _isCancel;

        internal WorkItem()
        {
            _event = new ManualResetEvent(false);
            _isCancel = false;
        }

        public String Name { get; set; }
        public WorkItemCallback Callback { get; set; }
        public Object State { get; set; }
        public WaitHandle WaitHandle { get { return _event; } }
        public bool IsCancel { get { return _isCancel; } }

        public IWorkResult Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                _event.Set();
            }
        }

        public IWorkResult GetResult()
        {
            return this.GetResult(-1);
        }

        public IWorkResult GetResult(int millisecondsTimeout)
        {
            _event.WaitOne(millisecondsTimeout);
            return _result;
        }

        public IWorkResult GetResult(TimeSpan timeout)
        {
            return this.GetResult((int)timeout.TotalMilliseconds);
        }

        public void Cancel()
        {
            _isCancel = true;
        }
    }
}
