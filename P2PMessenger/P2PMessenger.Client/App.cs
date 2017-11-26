using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using P2PMessenger.Client.API;
using P2PMessenger.Client.Core;
using RestEase;

namespace P2PMessenger.Client
{
    class App
    {
        public const string ServerIp = "";

        private IMessengerApi _messengerApi;
        private IPMessenger _messenger;

        public App()
        {
            _messenger = new PMessenger();
            _messengerApi = RestClient.For<IMessengerApi>("http://localhost:54545/api");
        }

        public void Run()
        {
            Console.WriteLine("P2P Messenger");

            var myIp = _messenger.GetMyIp();

            Console.WriteLine($"My ip: {myIp}");

            StartServer();

            _messengerApi.RegisterAsync(myIp).Wait();
            ConnectedUsers = _messengerApi.GetConnectedUsersAsync().Result.ToList();

            ConnectToClient();
            StartMessaging();
        }

        private void StartServer()
        {
            _messenger.StartServer();
        }

        private void ConnectToClient()
        {
            Console.WriteLine("Connected clients:");

            for (int i = 0; i < ConnectedUsers.Count(); i++)
            {
                Console.WriteLine($"{i}: {ConnectedUsers[i]}");
            }

            Console.WriteLine("Please select client:");

            var idS = Console.ReadLine();

            if (!_messenger.IsConnected)
            {
                var id = Convert.ToInt32(idS);

                _messenger.Connect(ConnectedUsers[id]);
            }
        }

        private void StartMessaging()
        {
            while (true)
            {
                Console.Write("> ");
                var message = Console.ReadLine();

                _messenger.Send(message);
            }
        }

        public List<string> ConnectedUsers { get; set; }
    }
}
