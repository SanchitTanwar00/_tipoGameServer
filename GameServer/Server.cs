using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
// 5:48 
namespace GameServer
{
    class Server
    {
        public static int MAXPLayers { get; private set; }

        public static int Port { get; private set; }
        public static Dictionary<int, Client> Client = new Dictionary<int, Client>(); 
        private static TcpListener tcpListener;
        public static void Start(int _maxPlayers , int _port)
        {
            MAXPLayers = _maxPlayers;
            Port = _port;

            Console.WriteLine("Starting Server...");
            InitServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);

            Console.WriteLine($"Server started on {Port}.");
             
        }

        private static void TCPConnectCallBack(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallBack), null);
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}..");

            for (int i = 1; i <= MAXPLayers; i++)
            {
                if(Client[i].tcp.socket == null)
                {
                    Client[i].tcp.Connect(_client);
                    return;
                }
            }
            Console.WriteLine($"{_client.Client.RemoteEndPoint} failed to connect : Server full");
        }

        private static void InitServerData()
        {
            for(int i = 1; i <= MAXPLayers; i++)
            {
                Client.Add(i, new Client(i));
            }
        }
    }
}
