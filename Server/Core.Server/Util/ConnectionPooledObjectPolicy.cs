using System;

using Microsoft.Extensions.ObjectPool;

namespace Core.Server
{
    public class ConnectionPooledObjectPolicy<TConnection> : IPooledObjectPolicy<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        public TConnection Create()
        {
            var connection = new TConnection();
            return connection;
        }

        public bool Return(TConnection conn)
        {
            conn.Release();
            return true;
        }
    }
}
