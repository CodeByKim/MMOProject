using System;

using Core.Server.Connection;
using Core.Server.Server;

namespace Core.Server.Logic
{
    internal abstract class AbstractLogic<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        protected BaseServer<TConnection> _server;

        public AbstractLogic(BaseServer<TConnection> server)
        {
            _server = server;
        }

        internal void Run()
        {
            OnInitialize();

            ThreadPool.QueueUserWorkItem(
                (param) =>
                {
                    while (true)
                    {
                        OnUpdate();

                        Thread.Sleep(10);
                    }
                });
        }

        public abstract void OnInitialize();

        public abstract void OnUpdate();
    }
}
