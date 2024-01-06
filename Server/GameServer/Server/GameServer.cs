using System;

using Core.Common;
using Core.Server;

internal class GameServer : AbstractServer<GameConnection>
{
    public GameServer(string configPath) : base(configPath)
    {
    }

    /*
     * 이렇게 객체를 리턴하는 메소드를 만들지 말고
     * dll에서 찾아오는 방식을 해도 되지 않을까?
     */
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