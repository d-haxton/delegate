using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Delegate.Socket.events;
using Delegate.Socket.interfaces;

namespace Delegate.Socket.sockets
{
    public class SocketWrapper : ISocketWrapper
    {
        private readonly ISocketSettings _settings;
        private readonly List<System.Net.Sockets.Socket> _sockets = new List<System.Net.Sockets.Socket>();
        private readonly byte[] _buffer = new byte[65525];

        public SocketWrapper(ISocketSettings settings)
        {
            _settings = settings;
        }

        public void Create()
        {
            foreach (var endpoint in _settings.IpEndpoint)
            {
                try
                {
                    var socket = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.IP);

                    socket.Bind(endpoint);
                    socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, 1);
                    var inBytes = new byte[] { 1, 0, 0, 0 };
                    socket.IOControl(IOControlCode.ReceiveAll, inBytes, null);

                    //todo catch exceptions -> iscreated = false if unable to handle exception
                    IsCreated = true;
                    _sockets.Add(socket);
                }
                catch 
                {
                    // do not add it to the list
                }

            }
        }

        public void BeginRead()
        {
            foreach (var socket in _sockets)
            {
                socket.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, RecieveSocket, socket);
            }
        }

        private void RecieveSocket(IAsyncResult ar)
        {
            var socket = (System.Net.Sockets.Socket)ar.AsyncState;
            var localEp = socket.LocalEndPoint;
            var count = socket.EndReceive(ar);

            var readEventArgs = new ReadEventArgs(_buffer, count, localEp);
            ReadEvent?.Invoke(this, readEventArgs);
            BeginRead();
        }

        public void StopRead()
        {
            foreach (var socket in _sockets)
            {
                socket.Close();
            }
            IsCreated = false;
        }

        public bool IsCreated { get; set; }

        public event EventHandler<ReadEventArgs> ReadEvent = delegate { };
    }
}