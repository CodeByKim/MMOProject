using System;

using Core.Common.Connection;
using Core.Common.Packet;
using Core.Server.Server;
using Core.Server.Util;

internal class GameServer : BaseServer<GameConnection>
{
    public GameServer(string configPath) : base(configPath)
    {
    }

    public override void Initialize()
    {
        base.Initialize();

        Logger.Info("initialize server...");
    }

    public override AbstractPacketResolver<GameConnection> OnGetPacketResolver()
    {
        return new GamePacketResolver();
    }

    protected override void OnNewConnection(GameConnection conn)
    {
        Logger.Info($"OnNewConnection: {conn.ID}");
    }

    protected override void OnDisconnected(GameConnection conn, DisconnectReason reason)
    {
        Logger.Info($"OnDisconnected: {conn.ID}, Reason: {reason}");
    }
}