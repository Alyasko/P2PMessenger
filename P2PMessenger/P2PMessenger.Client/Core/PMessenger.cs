using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace P2PMessenger.Client.Core
{
    class PMessenger : IPMessenger
    {
        private Random r = new Random();

        private IPEndPoint _localIpEndPoint;
        private Socket _connectionSocket;

        public PMessenger()
        {
            _localIpEndPoint = new IPEndPoint(new IPAddress(new byte[] {0, 0, 0, 0}), r.Next(15000, 30000));
        }

        public void Connect(string client)
        {


            if (_connectionSocket != null)
                return;

            _connectionSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            var parts = client.Split(':');
            var port = Convert.ToInt32(parts[1]);

            Console.WriteLine("If you are Olya press 1");
            var id = Convert.ToInt32(Console.ReadLine());

            if (id == 1)
            {
                parts[0] = "178.151.143.78";
            }
            else
            {
                parts[0] = "159.224.68.188";
            }

            _connectionSocket.Connect(parts[0], port);

            Task.Run(() =>
            {
                ListenRemoteSocket(_connectionSocket);
            });
        }

        private void ListenRemoteSocket(Socket socket)
        {
            
            while (true)
            {
                var buffer = new byte[256];
                _connectionSocket.Receive(buffer);

                Console.WriteLine("Remote: " + Encoding.Default.GetString(buffer));
            }
        }

        public void Send(string message)
        {
            _connectionSocket.Send(Encoding.Default.GetBytes(message));
        }

        public string GetMyIp()
        {
            return _localIpEndPoint.ToString();
        }

        public bool IsConnected => _connectionSocket != null;

        public string Read()
        {
            throw new NotImplementedException();
        }

        public string DownloadHistory()
        {
            throw new NotImplementedException();
        }

        public void StartServer()
        {
            var socket = new Socket(SocketType.Stream, ProtocolType.Tcp);

            socket.Bind(_localIpEndPoint);

            socket.Listen(10);

            Task.Run(() =>
            {
                while (true)
                {
                    _connectionSocket = socket.Accept();

                    Task.Run(() =>
                    {
                        Console.WriteLine($"Socket accepted: {_connectionSocket.RemoteEndPoint}");
                        Console.WriteLine("Press any key to start messaging");

                        ListenRemoteSocket(_connectionSocket);
                    });
                }
            });
        }
    }
}
