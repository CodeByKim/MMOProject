using System;

using Microsoft.Extensions.ObjectPool;

namespace Core.Server
{
    public class ConnectionPooledObjectPolicy<TConnection> : IPooledObjectPolicy<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        private AbstractServer<TConnection> _server;

        public ConnectionPooledObjectPolicy(AbstractServer<TConnection> server)
        {
            _server = server;
        }

        public TConnection Create()
        {
            var connection = new TConnection();
            connection.Initialize(_server);

            return connection;
        }

        public bool Return(TConnection conn)
        {
            conn.OnReturnedToPool();
            return true;
        }
    }
}
