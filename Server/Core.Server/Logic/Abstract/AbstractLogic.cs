using System;

namespace Core.Server
{
    internal abstract class AbstractLogic<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        protected AbstractServer<TConnection> _server;

        public AbstractLogic(AbstractServer<TConnection> server)
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
