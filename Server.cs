using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Net;

namespace Server
{
    class Server
    {
        static void Main(string[] args)
        {
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Parse("0.0.0.0"), 8888);
            IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Any, 8888);
            serverSocket.Bind(serverEndPoint);
            Console.WriteLine("Server Started.....");
            string sendData = "";
            while (sendData != "X")
            {
                serverSocket.Listen(100);
                Socket accepted = serverSocket.Accept();
                int bufferSize = accepted.SendBufferSize;
                byte[] buffer = new byte[bufferSize];

                while (true)
                {
                    int bytesRead = accepted.Receive(buffer);

                    byte[] formatted = new byte[bytesRead];

                    for (int i = 0; i < bytesRead; i++)
                    {
                        formatted[i] = buffer[i];
                    }

                    String receivedData = Encoding.UTF8.GetString(formatted);
                    Console.WriteLine("Received Data " + receivedData);

                    //String response = receivedData.ToUpper();
                    //byte[] resp = Encoding.UTF8.GetBytes(response);
                    //accepted.Send(resp, 0, resp.Length, 0);
                    Console.WriteLine("We recived data from Client; What do you want to send to Server... Or Press 'X' to close");
                    sendData = Console.ReadLine();
                    byte[] resp = Encoding.UTF8.GetBytes(sendData);
                    accepted.Send(resp, 0, resp.Length, 0);
                }
            }
        }
    }
}
