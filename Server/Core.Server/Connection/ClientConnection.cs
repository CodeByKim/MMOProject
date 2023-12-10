﻿using System;
using System.Collections.Concurrent;
using System.Net.Sockets;

using Core.Common;
using Google.Protobuf;

namespace Core.Server
{
    public abstract class ClientConnection<TConnection> : AbstractConnection
        where TConnection : ClientConnection<TConnection>, new()
    {
        private AbstractServer<TConnection> _server;
        private AbstractPacketResolver<TConnection> _packetResolver;

        private ConcurrentQueue<Tuple<short, IMessage>> _packetQueue;

        public ClientConnection() : base(ServerConfig.Instance.ReceiveBufferSize)
        {
        }

        public void Initialize(Socket socket, AbstractServer<TConnection> server)
        {
            Initialize(socket);

            _server = server;

            _packetResolver = _server.OnGetPacketResolver();

            _packetQueue = new ConcurrentQueue<Tuple<short, IMessage>>();
        }

        internal void ConsumePacket()
        {
            var packetCount = _packetQueue.Count;

            for (var i = 0; i < packetCount; i++)
            {
                Tuple<short, IMessage> packetBundle;
                if (!_packetQueue.TryDequeue(out packetBundle))
                    return;

                _packetResolver.Execute(this as TConnection, packetBundle.Item1, packetBundle.Item2);
            }
        }

        protected override IMessage OnResolvePacket(short packetId)
        {
            return _packetResolver.OnResolvePacket(packetId);
        }

        protected override void OnDispatchPacket(short packetId, IMessage packet)
        {
            var packetBundle = new Tuple<short, IMessage>(packetId, packet);
            _packetQueue.Enqueue(packetBundle);
        }

        protected override void OnDisconnected(AbstractConnection conn, DisconnectReason reason)
        {
            _server.Disconnect(conn as TConnection, reason);
        }
    }
}
