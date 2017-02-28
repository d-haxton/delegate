using System;
using System.Collections.Generic;
using Delegate.Socket.events;
using Delegate.Socket.interfaces;
using Delegate.Socket.sockets.tcp;

namespace Delegate.Socket.sockets
{
    public class SocketSniffer : ISniffer
    {
        private readonly ISocketWrapper _socket;
        private readonly Dictionary<ConnectionId, TcpConnection> _connections = new Dictionary<ConnectionId, TcpConnection>();
        public event Action<TcpConnection> NewConnection = delegate { }; 

        public SocketSniffer(ISocketWrapper socket)
        {
            _socket = socket;
            _socket.ReadEvent += OnReadEvent;
        }

        private void OnReadEvent(object sender, ReadEventArgs e)
        {
            //Console.WriteLine($"Bytes read: {e.Count}");
            ParsePacket(e.BytesReceived);
        }

        private void ParsePacket(byte[] packet)
        {
            var ipPacket = new IPv4Packet(packet);
            if (ipPacket.Protocol != IpProtocol.Tcp)
                return;

            var tcpPacket = new TcpPacket(ipPacket.Payload);
            var isConnectionPacket = (tcpPacket.Flags & TcpFlags.Syn) != 0;
            var connectionId = new ConnectionId(ipPacket.SourceIp, tcpPacket.SourcePort, ipPacket.DestinationIp, tcpPacket.DestinationPort);

            //Console.WriteLine(connectionId.Source.ToString());

            if (isConnectionPacket)
            {
                HandleConnectionPacket(tcpPacket, connectionId);
            }
            else
            {
                HandleNonConnectionPacket(tcpPacket, connectionId);
            }
        }

        private void HandleConnectionPacket(TcpPacket tcpPacket, ConnectionId connectionId)
        {
            var connection = new TcpConnection(connectionId, tcpPacket.SequenceNumber);

            NewConnection?.Invoke(connection);

            if (connection.HasSubscribers)
            {
                _connections[connectionId] = connection;
            }
        }

        private void HandleNonConnectionPacket(TcpPacket tcpPacket, ConnectionId connectionId)
        {
            TcpConnection connection;
            if (_connections.TryGetValue(connectionId, out connection))
            {
                connection.HandleTcpReceived(tcpPacket.SequenceNumber, tcpPacket.Payload);
            }
        }

        public void Start()
        {
            if (_socket.IsCreated)
            {
                _socket.BeginRead();
            }
            else
            {
                _socket.Create();
                _socket.BeginRead();
            }
        }

        public void Stop()
        {
            _socket.StopRead();
        }
    }
}
