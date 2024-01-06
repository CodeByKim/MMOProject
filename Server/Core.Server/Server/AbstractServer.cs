using System;
using System.Net.Sockets;

using Core.Common;
using Microsoft.Extensions.ObjectPool;

namespace Core.Server
{
    public abstract class AbstractServer<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        private Acceptor<TConnection> _acceptor;
        private DefaultObjectPool<TConnection> _connectionPool;

        private List<AbstractSystemLogic<TConnection>> _systemLogics;
        private List<AbstractGameLogic<TConnection>> _gameLogics;

        public AbstractPacketResolver<TConnection> PacketResolver { get; internal set; }

        public AbstractServer()
        {
            _acceptor = new Acceptor<TConnection>(this);
            _connectionPool = new DefaultObjectPool<TConnection>(new ConnectionPooledObjectPolicy<TConnection>(this),
                                                                 ServerConfig.Instance.ConnectionPoolCount);
            _systemLogics = new List<AbstractSystemLogic<TConnection>>();
            _gameLogics = new List<AbstractGameLogic<TConnection>>();

            _systemLogics.Add(new RoomControlLogic<TConnection>(this));
        }

        public void Run()
        {
            _acceptor.Run();

            foreach (var logic in _systemLogics)
                logic.Run();

            foreach (var logic in _gameLogics)
                logic.Run();
        }

        internal void AcceptNewClient(Socket socket)
        {
            var conn = AllocConnection();
            conn.Run(socket);

            foreach (var logic in _systemLogics)
                logic.OnNewConnection(conn);

            OnNewConnection(conn);
        }

        internal void Disconnect(TConnection conn, DisconnectReason reason)
        {
            OnDisconnected(conn, reason);

            _connectionPool.Return(conn);
        }

        private TConnection AllocConnection()
        {
            var conn = _connectionPool.Get();
            conn.OnTakeFromPool(this);

            return conn;
        }

        protected abstract void OnNewConnection(TConnection conn);

        protected abstract void OnDisconnected(TConnection conn, DisconnectReason reason);
    }
}
