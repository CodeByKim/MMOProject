using System.Net;
using System.Net.Sockets;

namespace Core.Server
{
    public class Acceptor<TConnection>
        where TConnection : ClientConnection<TConnection>, new()
    {
        private Socket _socket;
        private IPEndPoint _endPoint;
        private AbstractServer<TConnection> _server;

        public Acceptor(AbstractServer<TConnection> server)
        {
            _server = server;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _endPoint = new IPEndPoint(IPAddress.Any, ServerConfig.Instance.PortNumber);
        }

        public async void Run()
        {
            _socket.Bind(_endPoint);

            _socket.Listen(ServerConfig.Instance.Backlog);

            while (true)
            {
                var clientSocket = await _socket.AcceptAsync();

                _server.AcceptNewClient(clientSocket);
            }
        }
    }
}
