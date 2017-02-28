using System;
using Delegate.Socket.sockets.tcp;

namespace Delegate.Socket.interfaces
{
    public interface ISniffer
    {
        void Start();
        void Stop();
        event Action<TcpConnection> NewConnection;
    }
}
