using System;

using Core.Common;
using Core.Server;

internal class GameServer : AbstractServer<GameConnection>
{
    public GameServer()
    {
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