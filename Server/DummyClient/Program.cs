using System;

using Protocol;

internal class Program
{
    static async Task Main(string[] args)
    {
        var ip = "127.0.0.1";
        var portNumber = 8888;

        var connector = new DummyConnector();
        connector.Initialize("ClientConfig.json");

        await connector.ConnectAsync(ip, portNumber);
        Console.WriteLine("Success Connect");


        while (true)
        {
            PktEcho pkt = new PktEcho();
            pkt.Message = "Echo Test";

            connector.Send((short)PacketId.PktEcho, pkt);

            Thread.Sleep(100);
        }

        Console.ReadLine();
    }
}