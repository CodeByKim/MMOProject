using System;
using System.Net.Sockets;

using Core.Common;
using Google.Protobuf;

namespace Core.Common
{
    public abstract class Connector<TConnection> : AbstractConnection
        where TConnection : AbstractConnection
    {
        //private AbstractPacketResolver<TConnection> _packetResolver;

        public Connector() : base()
        {
        }

        public void Initialize(string configPath)
        {
            //ClientConfig.Instance.Load(configPath);

            //_packetResolver = OnGetPacketResolver();

            //var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Initialize(socket, ClientConfig.Instance.ReceiveBufferSize);
        }

        public async Task ConnectAsync(string ip, int portNumber)
        {
            await _socket.ConnectAsync(ip, portNumber);

            ReceiveAsync();
        }

        protected override void OnDispatchPacket(PacketHeader header, ArraySegment<byte> payload)
        {
            //var conn = this as TConnection;
            //var packetId = header.PacketId;
            //var packet = _packetResolver.OnResolvePacket(conn, packetId);
            //if (packet is null)
            //{
            //    //Logger.Error($"Not Found Packet Handler, PacketId: {packetId}");
            //    return;
            //}

            //packet.MergeFrom(payload);
            //_packetResolver.Execute(conn, packetId, packet);
        }

        protected abstract AbstractPacketResolver<TConnection> OnGetPacketResolver();
    }
}
