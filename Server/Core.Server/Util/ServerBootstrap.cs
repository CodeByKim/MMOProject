using System;
using System.Collections.Generic;

using Core.Common;

namespace Core.Server
{
    public class ServerBootstrap<TServer, TConnection>
        where TServer : AbstractServer<TConnection>, new()
        where TConnection : ClientConnection<TConnection>, new()
    {
        private AbstractPacketResolver<TConnection> _packetResolver;
        private TServer _server;

        public ServerBootstrap<TServer, TConnection> LoadConfig(string path)
        {
            ServerConfig.Instance.Load(path);
            return this;
        }

        public ServerBootstrap<TServer, TConnection> SetPacketResolver(AbstractPacketResolver<TConnection> packetResolver)
        {
            _packetResolver = packetResolver;
            return this;
        }

        public void Run()
        {
            _server = new TServer();
            _server.PacketResolver = _packetResolver;

            _server.Run();
        }
    }
}
