using System;

using Core.Common;

namespace Core.Client
{
    public class ClientConnector : Connector<ClientConnector>
    {
        protected override void OnDisconnected(AbstractConnection conn, DisconnectReason reason)
        {
        }
    }
}
