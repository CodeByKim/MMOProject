using System;
using System.Collections.Generic;

using Core.Client;
using Core.Common;

public class DummyConnector : Connector<DummyConnector>
{
    public DummyConnector()
    {
    }

    protected override void OnDisconnected(BaseConnection conn, DisconnectReason reason)
    {
        //Logger.Info($"OnDisconnected: {conn.ID}, Reason: {reason}");
    }

    protected override AbstractPacketResolver<DummyConnector> OnGetPacketResolver()
    {
        return new GamePacketResolver();
    }
}
