using System;

using Google.Protobuf;

namespace Core.Common
{
    internal static class PacketUtil
    {
        internal static byte[] CreateBuffer(in PacketHeader header, IMessage packet)
        {
            var buffer = new byte[PacketHeader.HeaderSize + header.PayloadSize];
            HeaderToBuffer(header, buffer);
            PacketToBuffer(packet, header.PayloadSize, buffer);

            return buffer;
        }

        private static void HeaderToBuffer(in PacketHeader header, byte[] buffer)
        {
            var packetId = BitConverter.GetBytes(header.PacketId);
            var payload = BitConverter.GetBytes(header.PayloadSize);

            Array.Copy(packetId, 0, buffer, 0, sizeof(short));
            Array.Copy(payload, 0, buffer, sizeof(short), sizeof(short));
        }

        private static void PacketToBuffer(IMessage packet, short payload, byte[] buffer)
        {
            Array.Copy(packet.ToByteArray(), 0, buffer, PacketHeader.HeaderSize, payload);
        }
    }
}
