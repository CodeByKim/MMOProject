using System;
using System.Collections.Generic;

using Google.Protobuf;

namespace Core.Common
{
    public abstract class AbstractPacketResolver<TConnection> where TConnection : AbstractConnection
    {
        private Dictionary<short, AbstractPacketHandler<TConnection>> _packetHandlers;

        public AbstractPacketResolver()
        {
            _packetHandlers = new Dictionary<short, AbstractPacketHandler<TConnection>>();
            OnRegisterPacketHandler(_packetHandlers);
        }

        protected bool ContainHandler(short packetId)
        {
            return _packetHandlers.ContainsKey(packetId);
        }

        public void Execute(TConnection conn, short packetId, IMessage packet)
        {
            var handler = _packetHandlers[packetId];
            handler.OnHandle(conn, packet);
        }

        public abstract IMessage OnResolvePacket(TConnection conn, short packetId);

        protected abstract void OnRegisterPacketHandler(Dictionary<short, AbstractPacketHandler<TConnection>> handlers);
    }
}
