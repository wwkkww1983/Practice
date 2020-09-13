using System;

namespace Caist.Framework.WebSocket
{
    public interface IWebSocketServer : IDisposable
    {
        void Start(Action<IWebSocketConnection> config);
    }
}
