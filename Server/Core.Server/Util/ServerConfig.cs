using System;

using Core.Common;

namespace Core.Server
{
    public class ServerConfig : Config<ServerConfig>
    {
        public int Backlog { get; set; } = 10;

        public int ConnectionPoolCount { get; set; } = 100;

        public int RoomCount { get; set; } = 1;
    }
}
