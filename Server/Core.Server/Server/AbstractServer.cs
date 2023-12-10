﻿using System;
using System.Net.Sockets;

using Core.Common;
using Microsoft.Extensions.ObjectPool;

namespace Core.Server
{
    public abstract class AbstractServer<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        private Acceptor _acceptor;
        private DefaultObjectPool<TConnection> _connectionPool;

        private List<AbstractSystemLogic<TConnection>> _systemLogics;
        private List<AbstractGameLogic<TConnection>> _gameLogics;

        public AbstractServer(string configPath)
        {
            ServerConfig.Instance.Load(configPath);

            _acceptor = new Acceptor();
            _connectionPool = new DefaultObjectPool<TConnection>(new ConnectionPooledObjectPolicy<TConnection>(),
                                                                 ServerConfig.Instance.ConnectionPoolCount);
            _acceptor.OnNewClientHandler = AcceptNewClient;

            _systemLogics = new List<AbstractSystemLogic<TConnection>>();
            _gameLogics = new List<AbstractGameLogic<TConnection>>();

            _systemLogics.Add(new RoomControlLogic<TConnection>(this));

            OnInitialized();
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
            var conn = AllocConnection(socket);

            foreach (var logic in _systemLogics)
                logic.OnNewConnection(conn);

            OnNewConnection(conn);

            conn.ReceiveAsync();
        }

        internal void Disconnect(TConnection conn, DisconnectReason reason)
        {
            OnDisconnected(conn, reason);

            _connectionPool.Return(conn);
        }

        private TConnection AllocConnection(Socket socket)
        {
            var conn = _connectionPool.Get();
            conn.Initialize(socket, this);

            return conn;
        }

        public abstract AbstractPacketResolver<TConnection> OnGetPacketResolver();

        protected abstract void OnInitialized();

        protected abstract void OnNewConnection(TConnection conn);

        protected abstract void OnDisconnected(TConnection conn, DisconnectReason reason);
    }
}
