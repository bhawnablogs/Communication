using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace client
{


    class Client
    {
        static void Main(string[] args)
        {

            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
             IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("<IPAddress>"), 8888);//here IP Address is of server with which user wants to connect
            string data = "";

            clientSocket.Connect(serverEndPoint);
            Console.WriteLine("Connection to Server Successfull.......");

            while (data != "X")
            {
                Console.WriteLine("Enter some data to send to server or Press 'X' Enter to close");

                data = Console.ReadLine();

                byte[] bytes = Encoding.UTF8.GetBytes(data);

                clientSocket.Send(bytes);

                int receiveBufferSize = clientSocket.ReceiveBufferSize;
                byte[] buffer = new byte[receiveBufferSize];


                int receivedBytes = clientSocket.Receive(buffer);
                byte[] receivedData = new byte[receivedBytes];

                for (int i = 0; i < receivedBytes; i++)
                {
                    receivedData[i] = buffer[i];
                }

                String received = Encoding.UTF8.GetString(receivedData);

                Console.WriteLine("Response from Server : {0}", received);

            }

        }
    }
}
