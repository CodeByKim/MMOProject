using System;

using Core.Server.Connection;
using Core.Server.Server;

namespace Core.Server.Logic
{
    internal abstract class AbstractGameLogic<TConnection> : AbstractLogic<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        public AbstractGameLogic(BaseServer<TConnection> server) : base(server)
        {
        }
    }
}
