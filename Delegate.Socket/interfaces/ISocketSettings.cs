using System.Collections.Generic;
using System.Net;

namespace Delegate.Socket.interfaces
{
    public interface ISocketSettings
    {
        List<IPEndPoint> IpEndpoint { get; }
    }
}
