using System;

using Core.Server.Connection;
using Core.Server.Server;

namespace Core.Server.Logic
{
    internal abstract class AbstractSystemLogic<TConnection> : AbstractLogic<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        public AbstractSystemLogic(BaseServer<TConnection> server) : base(server)
        {
        }

        public abstract void OnNewConnection(TConnection conn);
    }
}
