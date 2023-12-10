using System;
using System.Collections.Generic;

using Google.Protobuf;

namespace Core.Common
{
    public abstract class AbstractPacketHandler<TConnection> where TConnection : AbstractConnection
    {
        public abstract void OnHandle(TConnection conn, IMessage packet);
    }
}
