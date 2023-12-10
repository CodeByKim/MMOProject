using System.Net;
using System.Net.Sockets;

namespace Core.Server
{
    internal class Acceptor
    {
        public Action<Socket> OnNewClientHandler { get; set; }

        private Socket _socket;
        private IPEndPoint _endPoint;

        public Acceptor()
        {
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

                OnNewClientHandler(clientSocket);
            }
        }
    }
}
