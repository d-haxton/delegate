using System;
using System.Collections.Generic;
using System.Linq;
using Delegate.Socket.interfaces;
using Delegate.Socket.sockets.tcp;
using Delegate.Tera.Common.game;
using Delegate.Tera.Network.Decryption;
using Delegate.Tera.Network.interfaces;
using Delegate.Tera.Network.Messaging;

namespace Delegate.Tera.Network
{
    public class DelegateSniffer : IDelegateSniffer
    {
        private readonly Dictionary<string, Server> _serversByIp;
        private readonly HashSet<TcpConnection> _isNew = new HashSet<TcpConnection>();
        private TcpConnection _serverToClient;
        private TcpConnection _clientToServer;
        private ConnectionDecrypter _decrypter;
        private MessageSplitter _messageSplitter;

        public event Action<Server> NewConnection = delegate { };
        public event Action<Message> MessageReceived = delegate { }; 

        public DelegateSniffer(ISniffer socketWrapper, ITeraServerList serverList)
        {
            socketWrapper.Start();
            _serversByIp = serverList.Servers.ToDictionary(x => x.Ip.Replace("\n","").Trim());
            socketWrapper.NewConnection += HandleNewConnection;
        }

        private void HandleNewConnection(TcpConnection tcpObj)
        {
            if (_serversByIp.ContainsKey(tcpObj.Destination.Address.ToString()) ||
                _serversByIp.ContainsKey(tcpObj.Source.Address.ToString()))
            {
                _isNew.Add(tcpObj);
                tcpObj.DataReceived += HandleTcpDataReceived;
            }
        }

        private void HandleTcpDataReceived(TcpConnection connection, ArraySegment<byte> data)
        {
            if (data.Count == 0)
                return;
            if (_isNew.Contains(connection))
            {
                if (_serversByIp.ContainsKey(connection.Source.Address.ToString()) &&
                    data.Array.Skip(data.Offset).Take(4).SequenceEqual(new byte[] { 1, 0, 0, 0 }))
                {
                    _isNew.Remove(connection);
                    var server = _serversByIp[connection.Source.Address.ToString()];
                    _serverToClient = connection;
                    _clientToServer = null;

                    _decrypter = new ConnectionDecrypter();
                    _decrypter.ClientToServerDecrypted += HandleClientToServerDecrypted;
                    _decrypter.ServerToClientDecrypted += HandleServerToClientDecrypted;

                    _messageSplitter = new MessageSplitter();
                    _messageSplitter.MessageReceived += HandleMessageReceived;
                    NewConnection?.Invoke(server);
                }
                if (_serverToClient != null && _clientToServer == null &&
                    (_serverToClient.Destination.Equals(connection.Source) &&
                     _serverToClient.Source.Equals(connection.Destination)))
                {
                    _isNew.Remove(connection);
                    _clientToServer = connection;
                }
            }

            if (!(connection == _clientToServer || connection == _serverToClient))
                return;
            if (_decrypter == null)
                return;
            var dataArray = data.Array.Skip(data.Offset).Take(data.Count).ToArray();
            if (connection == _clientToServer)
                _decrypter.ClientToServer(dataArray);
            else
                _decrypter.ServerToClient(dataArray);
        }

        private void HandleMessageReceived(Message obj)
        {
            MessageReceived?.Invoke(obj);
        }

        private void HandleServerToClientDecrypted(byte[] obj)
        {
            _messageSplitter.ServerToClient(DateTime.UtcNow, obj);
        }

        private void HandleClientToServerDecrypted(byte[] obj)
        {
            _messageSplitter.ClientToServer(DateTime.UtcNow, obj);
        }
    }
}
