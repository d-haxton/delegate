using System;
using Delegate.Socket.events;

namespace Delegate.Socket.interfaces
{
    public interface ISocketWrapper
    {
        void BeginRead();
        void StopRead();
        void Create();
        bool IsCreated { get; }
        event EventHandler<ReadEventArgs> ReadEvent;
    }
}
