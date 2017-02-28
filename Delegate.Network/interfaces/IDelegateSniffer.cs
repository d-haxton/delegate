using System;
using Delegate.Tera.Common.game;

namespace Delegate.Tera.Network.interfaces
{
    public interface IDelegateSniffer
    {
        event Action<Server> NewConnection;
        event Action<Message> MessageReceived;
    }
}
