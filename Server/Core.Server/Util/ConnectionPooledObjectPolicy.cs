using System;

using Microsoft.Extensions.ObjectPool;

namespace Core.Server
{
    public class ConnectionPooledObjectPolicy<TConnection> : IPooledObjectPolicy<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        private AbstractServer<TConnection> m_server;

        public ConnectionPooledObjectPolicy(AbstractServer<TConnection> server)
        {
            m_server = server;
        }

        public TConnection Create()
        {
            var connection = new TConnection();
            return connection;
        }

        public bool Return(TConnection conn)
        {
            conn.OnReturnedToPool();
            return true;
        }
    }
}
