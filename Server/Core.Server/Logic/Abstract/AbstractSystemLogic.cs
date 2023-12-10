using System;

namespace Core.Server
{
    internal abstract class AbstractSystemLogic<TConnection> : AbstractLogic<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        public AbstractSystemLogic(AbstractServer<TConnection> server) : base(server)
        {
        }

        public abstract void OnNewConnection(TConnection conn);
    }
}
