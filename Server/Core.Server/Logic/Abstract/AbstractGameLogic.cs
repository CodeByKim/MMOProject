using System;

namespace Core.Server
{
    internal abstract class AbstractGameLogic<TConnection> : AbstractLogic<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        public AbstractGameLogic(AbstractServer<TConnection> server) : base(server)
        {
        }
    }
}
