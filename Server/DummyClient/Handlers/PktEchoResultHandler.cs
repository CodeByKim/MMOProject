using System;

using Core.Common.Packet;
using Google.Protobuf;
using Protocol;

public class PktEchoResultHandler : AbstractPacketHandler<DummyConnector>
{
    public override void OnHandle(DummyConnector conn, IMessage packet)
    {
        var pkt = packet as PktEchoResult;

        //Logger.Info($"From: {conn.ID}, message: {pkt.Message}");
    }
}