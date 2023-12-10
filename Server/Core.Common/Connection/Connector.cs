using System;
using System.Net.Sockets;

using Core.Common;
using Google.Protobuf;

namespace Core.Common
{
    public abstract class Connector<TConnection> : AbstractConnection
        where TConnection : AbstractConnection
    {
        private AbstractPacketResolver<TConnection> _packetResolver;

        public Connector(int receiveBufferSize) : base(receiveBufferSize)
        {
            _packetResolver = OnGetPacketResolver();

            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Initialize(socket);
        }

        public async Task ConnectAsync(string ip, int portNumber)
        {
            await _socket.ConnectAsync(ip, portNumber);

            ReceiveAsync();
        }

        protected override IMessage OnResolvePacket(short packetId)
        {
            return _packetResolver.OnResolvePacket(packetId);
        }

        protected override void OnDispatchPacket(short packetId, IMessage packet)
        {
            var conn = this as TConnection;
            _packetResolver.Execute(conn, packetId, packet);
        }

        protected abstract AbstractPacketResolver<TConnection> OnGetPacketResolver();
    }
}
