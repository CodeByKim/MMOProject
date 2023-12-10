using System;
using System.Collections.Generic;

using Core.Common;

namespace Core.Server
{
    public class ServerConnector<TConnection> : Connector<TConnection>
        where TConnection : ServerConnector<TConnection>
    {
        protected override void OnDisconnected(AbstractConnection conn, DisconnectReason reason)
        {
        }

        protected override AbstractPacketResolver<TConnection> OnGetPacketResolver()
        {
            return null;
        }
    }
}
