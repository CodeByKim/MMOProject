using System;
using System.Collections.Generic;

using Core.Common;

public class DummyConnector : Connector<DummyConnector>
{
    public DummyConnector(int receiveBufferSize) : base(receiveBufferSize)
    {
    }

    protected override void OnDisconnected(AbstractConnection conn, DisconnectReason reason)
    {
        Console.WriteLine($"OnDisconnected: {conn.ID}, Reason: {reason}");
    }

    protected override AbstractPacketResolver<DummyConnector> OnGetPacketResolver()
    {
        return new GamePacketResolver();
    }
}
