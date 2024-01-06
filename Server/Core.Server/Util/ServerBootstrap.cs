using System;
using System.Collections.Generic;

using Core.Common;
using Microsoft.Extensions.ObjectPool;

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
            if (_packetResolver is null)
            {
                Logger.Error("PacketResolver가 설정되지 않음");
                return;
            }

            _server = new TServer();

            _server.Initialize(_packetResolver);
            _server.Run();
        }
    }
}
