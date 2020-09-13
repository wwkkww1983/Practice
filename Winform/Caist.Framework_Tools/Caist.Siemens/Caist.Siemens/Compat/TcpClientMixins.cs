﻿namespace Caist.Siemens
{
    public static class TcpClientMixins
    {
        #if NETSTANDARD1_3
        public static void Close(this TcpClient tcpClient)
        {
            tcpClient.Dispose();
        }

        public static void Connect(this TcpClient tcpClient, string host, int port)
        {
            tcpClient.ConnectAsync(host, port).GetAwaiter().GetResult();
        }
        #endif
    }
}
