using System.Collections.Generic;
using System.Net;
using Delegate.Socket.interfaces;

namespace Delegate.Socket.sockets
{
    public class SocketSettings : ISocketSettings
    {
        public SocketSettings(INetworkInterfaces networkInterfaces)
        {
            IpEndpoint = new List<IPEndPoint>();
            foreach (var unparsedIp in networkInterfaces.Networks)
            {
                var endpoint = new IPEndPoint(IPAddress.Parse(unparsedIp), 0);
                IpEndpoint.Add(endpoint);
            }
        }

        public List<IPEndPoint> IpEndpoint { get; }
    }
}
