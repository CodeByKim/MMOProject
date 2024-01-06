using System;

using Core.Server;

internal class Program
{
    private static void Main(string[] args)
    {
        var bootstrap = new ServerBootstrap<GameServer, GameConnection>();
        bootstrap.LoadConfig("ServerConfig.json")
            .SetPacketResolver(new GamePacketResolver())
            .Run();

        Console.ReadLine();
    }
}