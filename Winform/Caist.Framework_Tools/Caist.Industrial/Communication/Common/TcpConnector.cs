﻿using System;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using Serilog;

namespace Communication.Common
{
    /// <summary>
    ///     Socket收到的数据
    /// </summary>
    public class SocketMessageEventArgs : EventArgs
    {
        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="message">需要返回的信息</param>
        public SocketMessageEventArgs(byte[] message)
        {
            Message = message;
        }

        /// <summary>
        ///     返回的信息
        /// </summary>
        public byte[] Message { get; }
    }

    /// <summary>
    ///     Socket收发类
    ///     作者：本类来源于CSDN，并由罗圣（Chris L.）根据实际需要修改
    /// </summary>
    public class TcpConnector : BaseConnector, IDisposable
    {
        private readonly string _host;
        private readonly int _port;

        /// <summary>
        ///     1MB 的接收缓冲区
        /// </summary>
        private readonly byte[] _receiveBuffer = new byte[1024];

        private int _errorCount;
        private int _receiveCount;

        private int _sendCount;

        private TcpClient _socketClient;

        private int _timeoutTime;

        private bool m_disposed;

        /// <summary>
        ///     构造器
        /// </summary>
        /// <param name="ipaddress">Ip地址</param>
        /// <param name="port">端口</param>
        /// <param name="timeoutTime">超时时间</param>
        public TcpConnector(string ipaddress, int port, int timeoutTime)
        {
            _host = ipaddress;
            _port = port;
            TimeoutTime = timeoutTime;
        }

        /// <summary>
        ///     连接关键字
        /// </summary>
        public override string ConnectionToken => _host;

        /// <summary>
        ///     超时时间
        /// </summary>
        public int TimeoutTime
        {
            get  =>
            _timeoutTime;
            set
            {
                _timeoutTime = value;
                if (_socketClient != null)
                    _socketClient.ReceiveTimeout = _timeoutTime;
            }
        }

        /// <summary>
        ///     是否已经连接
        /// </summary>
        public override bool IsConnected => _socketClient?.Client != null && _socketClient.Connected;

        /// <summary>
        ///     实现IDisposable接口
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            //.NET Framework 类库
            // GC..::.SuppressFinalize 方法
            //请求系统不要调用指定对象的终结器。
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///     虚方法，可供子类重写
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!m_disposed)
            {
                if (disposing)
                {
                    // Release managed resources
                }
                // Release unmanaged resources
                if (_socketClient != null)
                {
                    CloseClientSocket();
#if NET40 || NET45 || NET451 || NET452
                    _socketClient.Close();
#else
                    _socketClient.Dispose();
#endif
                    Log.Debug("Tcp client {ConnectionToken} Disposed", ConnectionToken);
                }
                m_disposed = true;
            }
        }

        /// <summary>
        ///     析构函数
        ///     当客户端没有显示调用Dispose()时由GC完成资源回收功能
        /// </summary>
        ~TcpConnector()
        {
            Dispose(false);
        }

        /// <summary>
        ///     连接
        /// </summary>
        /// <returns>是否连接成功</returns>
        public override bool Connect()
        {
            return AsyncHelper.RunSync(ConnectAsync);
        }

        /// <summary>
        ///     连接
        /// </summary>
        /// <returns>是否连接成功</returns>
        public override async Task<bool> ConnectAsync()
        {
            if (_socketClient != null)
                Disconnect();
            try
            {
                _socketClient = new TcpClient
                {
                    SendTimeout = TimeoutTime,
                    ReceiveTimeout = TimeoutTime
                };

                try
                {
                    var cts = new CancellationTokenSource();
                    cts.CancelAfter(TimeoutTime);
                    await _socketClient.ConnectAsync(_host, _port).WithCancellation(cts.Token);
                }
                catch (Exception e)
                {
                    Log.Error(e, "Tcp client {ConnectionToken} connect error", ConnectionToken);
                }
                if (_socketClient.Connected)
                {
                    Log.Information("Tcp client {ConnectionToken} connected", ConnectionToken);
                    return true;
                }
                Log.Error("Tcp client {ConnectionToken} connect failed.", ConnectionToken);
                return false;
            }
            catch (Exception err)
            {
                Log.Error(err, "Tcp client {ConnectionToken} connect exception", ConnectionToken);
                return false;
            }
        }

        /// <summary>
        ///     断开
        /// </summary>
        /// <returns>是否断开成功</returns>
        public override bool Disconnect()
        {
            if (_socketClient == null)
                return true;

            try
            {
                Dispose();
                Log.Information("Tcp client {ConnectionToken} disconnected successfully", ConnectionToken);
                return true;
            }
            catch (Exception err)
            {
                Log.Error(err, "Tcp client {ConnectionToken} disconnected exception", ConnectionToken);
                return false;
            }
            finally
            {
                _socketClient = null;
            }
        }

        /// <summary>
        ///     发送数据，不需要返回任何值
        /// </summary>
        /// <param name="message">发送的信息</param>
        /// <returns>是否发送成功</returns>
        public override bool SendMsgWithoutReturn(byte[] message)
        {
            return AsyncHelper.RunSync(() => SendMsgWithoutReturnAsync(message));
        }

        /// <summary>
        ///     发送数据，不需要返回任何值
        /// </summary>
        /// <param name="message">发送的信息</param>
        /// <returns>是否发送成功</returns>
        public override async Task<bool> SendMsgWithoutReturnAsync(byte[] message)
        {
            var datagram = message;

            try
            {
                if (!IsConnected)
                    await ConnectAsync();

                var stream = _socketClient.GetStream();

                Log.Verbose("Tcp client {ConnectionToken} send text len = {Length}", ConnectionToken, datagram.Length);
                Log.Verbose($"Tcp client {ConnectionToken} send text = {String.Concat(datagram.Select(p => " " + p))}");
                await stream.WriteAsync(datagram, 0, datagram.Length);

                RefreshSendCount();

                return true;
            }
            catch (Exception err)
            {
                Log.Error(err, "Tcp client {ConnectionToken} send exception", ConnectionToken);
                CloseClientSocket();
                return false;
            }
        }

        /// <summary>
        ///     发送数据，需要返回
        /// </summary>
        /// <param name="message">发送的数据</param>
        /// <returns>是否发送成功</returns>
        public override byte[] SendMsg(byte[] message)
        {
            return AsyncHelper.RunSync(() => SendMsgAsync(message));
        }

        /// <summary>
        ///     发送数据，需要返回
        /// </summary>
        /// <param name="message">发送的数据</param>
        /// <returns>是否发送成功</returns>
        public override async Task<byte[]> SendMsgAsync(byte[] message)
        {
            var datagram = message;

            try
            {
                if (!IsConnected)
                    await ConnectAsync();

                var stream = _socketClient.GetStream();

                RefreshSendCount();

                Log.Verbose("Tcp client {ConnectionToken} send text len = {Length}", ConnectionToken, datagram.Length);
                Log.Verbose($"Tcp client {ConnectionToken} send: {String.Concat(datagram.Select(p => " " + p))}");
                await stream.WriteAsync(datagram, 0, datagram.Length);

                var receiveBytes = await ReceiveAsync(stream);
                Log.Verbose("Tcp client {ConnectionToken} receive text len = {Length}", ConnectionToken,
                    receiveBytes == null ? 0 : receiveBytes.Length);
                Log.Verbose($"Tcp client {ConnectionToken} receive: {String.Concat(receiveBytes.Select(p => " " + p))}");

                RefreshReceiveCount();

                return receiveBytes;
            }
            catch (Exception err)
            {
                Log.Error(err, "Tcp client {ConnectionToken} send exception", ConnectionToken);
                CloseClientSocket();
                return null;
            }
        }

        /// <summary>
        ///     接收返回消息
        /// </summary>
        /// <param name="stream">Network Stream</param>
        /// <returns>返回的消息</returns>
        protected async Task<byte[]> ReceiveAsync(NetworkStream stream)
        {
            try
            {
                var len = await stream.ReadAsync(_receiveBuffer, 0, _receiveBuffer.Length);
                stream.Flush();
                // 异步接收回答
                if (len > 0)
                    return CheckReplyDatagram(len);
                return null;
            }
            catch (Exception err)
            {
                Log.Error(err, "Tcp client {ConnectionToken} receive exception", ConnectionToken);
                CloseClientSocket();
                return null;
            }
        }

        /// <summary>
        ///     接收消息，并转换成字符串
        /// </summary>
        /// <param name="len"></param>
        private byte[] CheckReplyDatagram(int len)
        {
            var replyMessage = new byte[len];
            Array.Copy(_receiveBuffer, replyMessage, len);

            if (len <= 0)
                RefreshErrorCount();

            return replyMessage;
        }

        private void RefreshSendCount()
        {
            _sendCount++;
            Log.Verbose("Tcp client {ConnectionToken} send count: {SendCount}", ConnectionToken, _sendCount);
        }

        private void RefreshReceiveCount()
        {
            _receiveCount++;
            Log.Verbose("Tcp client {ConnectionToken} receive count: {SendCount}", ConnectionToken, _receiveCount);
        }

        private void RefreshErrorCount()
        {
            _errorCount++;
            Log.Verbose("Tcp client {ConnectionToken} error count: {ErrorCount}", ConnectionToken, _errorCount);
        }

        private void CloseClientSocket()
        {
            try
            {
                var stream = _socketClient.GetStream();
                stream.Dispose();
                _socketClient.Client.Shutdown(SocketShutdown.Both);
                _socketClient.Client.Dispose();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Tcp client {ConnectionToken} client close exception", ConnectionToken);
            }
        }
    }
}