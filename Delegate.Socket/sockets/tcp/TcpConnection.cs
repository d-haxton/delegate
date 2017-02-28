using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Delegate.Socket.sockets.tcp
{
    public class TcpConnection
    {
        public readonly IPEndPoint Source;
        public readonly IPEndPoint Destination;
        private readonly SortedDictionary<long, byte[]> _bufferedPackets = new SortedDictionary<long, byte[]>();
        public long BytesReceived { get; private set; }
        public uint InitialSequenceNumber { get; }

        public event Action<TcpConnection, ArraySegment<byte>> DataReceived;

        public bool HasSubscribers => DataReceived != null;
        internal string BufferedPacketDescription { get { return string.Join(", ", _bufferedPackets.OrderBy(x => x.Key).Select(x => x.Key + "+" + x.Value.Length)); } }
        public uint CurrentSequenceNumber => unchecked((uint)(InitialSequenceNumber + 1 + BytesReceived));

        public long SequenceNumberToBytesReceived(uint sequenceNumber)
        {
            var offsetToCurrent = unchecked((int)(sequenceNumber - CurrentSequenceNumber));
            return BytesReceived + offsetToCurrent;
        }

        internal void OnDataReceived(ArraySegment<byte> data)
        {
            DataReceived?.Invoke(this, data);
        }

        internal void HandleTcpReceived(uint sequenceNumber, ArraySegment<byte> data)
        {
            var dataPosition = SequenceNumberToBytesReceived(sequenceNumber);
            if (dataPosition == BytesReceived)
            {
                OnDataReceived(data);
                BytesReceived += data.Count;
            }
            else
            {
                var dataArray = new byte[data.Count];
                Array.Copy(data.Array, data.Offset, dataArray, 0, data.Count);
                lock (_bufferedPackets)
                {
                    if (!_bufferedPackets.ContainsKey(dataPosition) ||
                        _bufferedPackets[dataPosition].Length < dataArray.Length)
                    {
                        _bufferedPackets[dataPosition] = dataArray;
                    }
                }

            }

            long firstBufferedPosition;
            while (_bufferedPackets.Any() && ((firstBufferedPosition = _bufferedPackets.Keys.First()) <= BytesReceived))
            {
                var dataArray = _bufferedPackets[firstBufferedPosition];
                _bufferedPackets.Remove(firstBufferedPosition);

                var alreadyReceivedBytes = BytesReceived - firstBufferedPosition;

                if (alreadyReceivedBytes < dataArray.Length)
                {
                    var count = dataArray.Length - alreadyReceivedBytes;
                    OnDataReceived(new ArraySegment<byte>(dataArray, (int)alreadyReceivedBytes, (int)count));
                    BytesReceived += count;
                }
            }
        }

        internal TcpConnection(ConnectionId connectionId, uint sequenceNumber)
        {
            Source = connectionId.Source.ToIpEndpoint();
            Destination = connectionId.Destination.ToIpEndpoint();
            InitialSequenceNumber = sequenceNumber;
        }

        public override string ToString()
        {
            return $"{Source} -> {Destination}";
        }
    }
}
