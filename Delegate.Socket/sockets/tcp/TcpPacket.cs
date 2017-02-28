using System;

namespace Delegate.Socket.sockets.tcp
{
    [Flags]
    public enum TcpFlags : byte
    {
        Cwr = 1 << 7,
        Ecu = 1 << 6,
        Urg = 1 << 5,
        Ack = 1 << 4,
        Psh = 1 << 3,
        Rst = 1 << 2,
        Syn = 1 << 1,
        Fin = 1 << 0
    }
    public class TcpPacket
    {
        public readonly ArraySegment<byte> Packet;
        public ushort SourcePort => ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 0);
        public ushort DestinationPort => ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 2);
        public uint SequenceNumber => ParserHelpers.GetUInt32BigEndian(Packet.Array, Packet.Offset + 4);
        public uint AcknowledgementNumber => ParserHelpers.GetUInt32BigEndian(Packet.Array, Packet.Offset + 8);
        public byte OffsetAndFlags => Packet.Array[Packet.Offset + 12];
        public TcpFlags Flags => (TcpFlags)Packet.Array[Packet.Offset + 13];
        public ushort WindowSize => ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 14);
        public ushort Checksum => ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 16);
        public ushort UrgentPointer => ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 18);

        public int HeaderLength
        {
            get
            {
                var lengthInWords = OffsetAndFlags >> 4;
                if (lengthInWords < 5)
                    throw new FormatException("Incorrect TcpHeader length");
                return lengthInWords * 4;
            }
        }

        public int OptionsLength => HeaderLength - 20;

        public ArraySegment<byte> Options
        {
            get
            {
                var optionsLength = OptionsLength;
                return new ArraySegment<byte>(Packet.Array, 20, optionsLength);
            }
        }

        public ArraySegment<byte> Payload
        {
            get
            {
                var headerLength = HeaderLength;
                return new ArraySegment<byte>(Packet.Array, Packet.Offset + headerLength, Packet.Count - headerLength);
            }
        }

        public TcpPacket(ArraySegment<byte> packet)
        {
            Packet = packet;
        }
    }
}
