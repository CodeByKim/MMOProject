using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var configPath = "ServerConfig.json";

        var server = new GameServer();
        server.Initialize(configPath);
        server.Run();

        Console.ReadLine();
    }
}