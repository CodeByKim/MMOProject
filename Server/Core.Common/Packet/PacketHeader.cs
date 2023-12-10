using System;
using System.Collections.Generic;

using Google.Protobuf;

namespace Core.Common
{
    public struct PacketHeader
    {
        public static readonly short HeaderSize = 4;

        public short PacketId { get; private set; }
        public short PayloadSize { get; private set; }

        public PacketHeader(IMessage packet, short packetId)
        {
            PacketId = packetId;
            PayloadSize = (short)packet.CalculateSize();
        }

        internal void CopyTo(RingBuffer buffer)
        {
            var data = buffer.Peek(HeaderSize);

            PacketId = BitConverter.ToInt16(data.Array, data.Offset);
            PayloadSize = BitConverter.ToInt16(data.Array, data.Offset + sizeof(short));
        }
    }
}
