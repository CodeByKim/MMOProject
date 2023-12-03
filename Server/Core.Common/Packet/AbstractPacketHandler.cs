using System;
using System.Collections.Generic;

using Core.Common.Connection;
using Google.Protobuf;

namespace Core.Common.Packet
{
    public abstract class AbstractPacketHandler<TConnection> where TConnection : BaseConnection
    {
        public abstract void OnHandle(TConnection conn, IMessage packet);
    }
}
