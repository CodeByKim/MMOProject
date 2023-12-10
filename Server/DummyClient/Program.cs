using System;

using Newtonsoft.Json;
using Protocol;

internal class ClientConfig
{
    public string Ip { get; set; } = "127.0.0.1";

    public int PortNumber { get; set; } = 10000;

    public int ReceiveBufferSize { get; set; } = 1024 * 4;
}

internal class Program
{
    static async Task Main(string[] args)
    {
        var configText = File.ReadAllText("ClientConfig.json");
        var config = JsonConvert.DeserializeObject<ClientConfig>(configText);

        var connector = new DummyConnector(config.ReceiveBufferSize);
        await connector.ConnectAsync(config.Ip, config.PortNumber);

        Console.WriteLine("Success Connect");

        while (true)
        {
            PktEcho pkt = new PktEcho();
            pkt.Message = "Echo Test";

            connector.Send((short)PacketId.PktEcho, pkt);

            Thread.Sleep(100);
        }
    }
}