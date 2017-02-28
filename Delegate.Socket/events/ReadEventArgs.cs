using System;
using System.Net;

namespace Delegate.Socket.events
{
    public class ReadEventArgs : EventArgs
    {
        public EndPoint LocalEp { get;  }
        public byte[] BytesReceived { get; }
        public int Count { get; }

        public ReadEventArgs(byte[] bytesReceived, int count, EndPoint localEp)
        {
            BytesReceived = bytesReceived;
            Count = count;
            LocalEp = localEp;
        }
    }
}
