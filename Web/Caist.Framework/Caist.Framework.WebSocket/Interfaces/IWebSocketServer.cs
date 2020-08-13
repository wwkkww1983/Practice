using System;

namespace Caist.Framework.WebSocket.Interfaces
{
    public interface IWebSocketServer : IDisposable
    {
        void Start(Action<IWebSocketConnection> config);
    }
}
