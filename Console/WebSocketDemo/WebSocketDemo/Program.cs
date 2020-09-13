using Fleck;
using System;

namespace WebSocketDemo
{
    class Program
    {
        static IWebSocketConnection _socket;
        static void Main(string[] args)
        {
            var server = new WebSocketServer("ws://127.0.0.1:8181");
            server.Start(socket =>
            {
                socket.OnOpen = () => Console.WriteLine("webscoket 服务已经打开!");
                socket.OnClose = () => Console.WriteLine("webscoke 服务已经关闭!");
                socket.OnMessage = message => testFunction(message);
                _socket = socket;
            });
            while (true)
            {
                var str = Console.ReadLine();
                if (!String.IsNullOrWhiteSpace(str))
                {
                    _socket.Send(str);
                }
            }
        }

        private static void testFunction(string message)
        {
            if (!string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("【客户端消息】:" + message);
            }
        }
    }
}
