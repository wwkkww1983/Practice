﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Caist.Framework.WebSocket
{
    public class QueuedStream : Stream
    {
        readonly Stream _stream;
        readonly Queue<WriteData> _queue = new Queue<WriteData>();
        int _pendingWrite;
        bool _disposed;

        public QueuedStream(Stream stream)
        {
            _stream = stream;
        }

        public override bool CanRead
        {
            get { return _stream.CanRead; }
        }

        public override bool CanSeek
        {
            get { return _stream.CanSeek; }
        }

        public override bool CanWrite
        {
            get { return _stream.CanWrite; }
        }

        public override long Length
        {
            get { return _stream.Length; }
        }

        public override long Position
        {
            get { return _stream.Position; }
            set { _stream.Position = value; }
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return _stream.Read(buffer, offset, count);
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _stream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _stream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("QueuedStream尚不支持同步写入操作.");
        }

        public override IAsyncResult BeginRead(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            return _stream.BeginRead(buffer, offset, count, callback, state);
        }

        public override IAsyncResult BeginWrite(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
        {
            lock (_queue)
            {
                var data = new WriteData(buffer, offset, count, callback, state);
                if (_pendingWrite > 0)
                {
                    _queue.Enqueue(data);
                    return data.AsyncResult;
                }
                return BeginWriteInternal(buffer, offset, count, callback, state, data);
            }
        }

        public override int EndRead(IAsyncResult asyncResult)
        {
            return _stream.EndRead(asyncResult);
        }

        public override void EndWrite(IAsyncResult asyncResult)
        {
            if (asyncResult is QueuedWriteResult)
            {
                var queuedResult = asyncResult as QueuedWriteResult;
                if (queuedResult.Exception != null) throw queuedResult.Exception;
                var ar = queuedResult.ActualResult;
                if (ar == null)
                {
                    throw new NotSupportedException(
                        "QueuedStream不支持同步写入操作。在调用EndWrite之前，请等待调用回调.");
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public override void Flush()
        {
            _stream.Flush();
        }

        public override void Close()
        {
            _stream.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _stream.Dispose();
                }
                _disposed = true;
            }
            base.Dispose(disposing);
        }

        IAsyncResult BeginWriteInternal(byte[] buffer, int offset, int count, AsyncCallback callback, object state, WriteData queued)
        {
            _pendingWrite++;
            var result = _stream.BeginWrite(buffer, offset, count, ar =>
            {
                queued.AsyncResult.ActualResult = ar;
                try
                {
                    _stream.EndWrite(ar);
                }
                catch (Exception exc)
                {
                    queued.AsyncResult.Exception = exc;
                }
                lock (_queue)
                {
                    _pendingWrite--;
                    while (_queue.Count > 0)
                    {
                        var data = _queue.Dequeue();
                        try
                        {
                            data.AsyncResult.ActualResult = BeginWriteInternal(data.Buffer, data.Offset, data.Count, data.Callback, data.State, data);
                            break;
                        }
                        catch (Exception exc)
                        {
                            _pendingWrite--;
                            data.AsyncResult.Exception = exc;
                            data.Callback(data.AsyncResult);
                        }
                    }
                    callback(queued.AsyncResult);
                }
            }, state);
            queued.AsyncResult.ActualResult = result;
            return queued.AsyncResult;
        }

        #region Nested type: WriteData

        class WriteData
        {
            public readonly byte[] Buffer;
            public readonly int Offset;
            public readonly int Count;
            public readonly AsyncCallback Callback;
            public readonly object State;
            public readonly QueuedWriteResult AsyncResult;

            public WriteData(byte[] buffer, int offset, int count, AsyncCallback callback, object state)
            {
                Buffer = buffer;
                Offset = offset;
                Count = count;
                Callback = callback;
                State = state;
                AsyncResult = new QueuedWriteResult(state);
            }
        }

        #endregion

        #region Nested type: QueuedWriteResult

        class QueuedWriteResult : IAsyncResult
        {
            readonly object _state;

            public QueuedWriteResult(object state)
            {
                _state = state;
            }

            public Exception Exception { get; set; }

            public IAsyncResult ActualResult { get; set; }

            public object AsyncState
            {
                get { return _state; }
            }

            public WaitHandle AsyncWaitHandle
            {
                get { throw new NotSupportedException("排队写入操作不支持等待句柄."); }
            }

            public bool CompletedSynchronously
            {
                get { return false; }
            }

            public bool IsCompleted
            {
                get { return ActualResult != null && ActualResult.IsCompleted; }
            }
        }

        #endregion
    }
}
