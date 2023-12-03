using System;

using Core.Server.Connection;
using Microsoft.Extensions.ObjectPool;

namespace Core.Server.Util
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
