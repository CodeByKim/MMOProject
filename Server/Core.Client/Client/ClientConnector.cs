using System;

using Core.Common;

namespace Core.Client
{
    public abstract class ClientConnector<TConnection> : Connector<TConnection>
        where TConnection : ClientConnector<TConnection>
    {
        public void Initialize(string configPath)
        {
            ClientConfig.Instance.Load(configPath);
        }
    }
}
