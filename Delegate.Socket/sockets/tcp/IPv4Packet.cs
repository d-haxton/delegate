﻿using System;

namespace Delegate.Socket.sockets.tcp
{
    public enum IpProtocol : byte
    {
        Tcp = 6,
        Udp = 17
    }
    internal static class ParserHelpers
    {
        public static uint GetUInt32BigEndian(byte[] array, int offset)
        {
            return ((uint)array[offset + 0] << 24) |
                   ((uint)array[offset + 1] << 16) |
                   ((uint)array[offset + 2] << 8) |
                   ((uint)array[offset + 3]);
        }

        public static ushort GetUInt16BigEndian(byte[] array, int offset)
        {
            return (ushort)(
                               (array[offset + 0] << 8) |
                               (array[offset + 1]));
        }
    }
    public class IPv4Packet
    {
        public readonly ArraySegment<byte> Packet;
        public byte VersionAndHeaderLength => Packet.Array[Packet.Offset + 0];
        public byte DscpAndEcn => Packet.Array[Packet.Offset + 1];
        public ushort TotalLength => ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 2);
        public ushort Identification => ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 4);
        public byte Flags => (byte)(Packet.Array[Packet.Offset + 6] >> 13);
        public ushort FragmentOffset => (ushort)(ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 6) & 0x1FFF);
        public byte TimeToLive => Packet.Array[Packet.Offset + 8];
        public IpProtocol Protocol => (IpProtocol)Packet.Array[Packet.Offset + 9];
        public ushort HeaderChecksum => ParserHelpers.GetUInt16BigEndian(Packet.Array, Packet.Offset + 10);
        public uint SourceIp => ParserHelpers.GetUInt32BigEndian(Packet.Array, Packet.Offset + 12);
        public uint DestinationIp => ParserHelpers.GetUInt32BigEndian(Packet.Array, Packet.Offset + 16);

        public int HeaderLength => (VersionAndHeaderLength & 0x0F) * 4;


        public ArraySegment<byte> Payload
        {
            get
            {
                var headerLength = HeaderLength;
                return new ArraySegment<byte>(Packet.Array, Packet.Offset + headerLength, TotalLength - headerLength);
            }
        }

        public IPv4Packet(byte[] packet)
        {
            var arraySeg = new ArraySegment<byte>(packet);
            Packet = arraySeg;
        }
    }
}
