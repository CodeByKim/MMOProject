using System;

internal class Program
{
    private static void Main(string[] args)
    {
        var server = new GameServer("ServerConfig.json");
        server.Run();

        Console.ReadLine();
    }
}