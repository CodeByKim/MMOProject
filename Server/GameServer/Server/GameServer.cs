using System;

using Core.Common;
using Core.Server;

internal class GameServer : BaseServer<GameConnection>
{
    public GameServer() : base()
    {
    }

    public override void Initialize(string configPath)
    {
        base.Initialize(configPath);

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