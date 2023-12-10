using System;

using Core.Common;

namespace Core.Client
{
    public class ClientConfig : Config<ClientConfig>
    {
        public int PortNumber { get; set; } = 10000;

        public int ReceiveBufferSize { get; set; } = 1024 * 4;
    }
}