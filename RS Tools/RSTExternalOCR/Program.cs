using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RSTExternalOCR
{
    class Program
    {
        static string ip = "127.0.0.1";
        static int port = 49153;

        static void Main(string[] args)
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Waiting for image...");
                Image bmp = ReceiveBitmap(ip, port);
                Console.WriteLine("Received image!");
            }
            

        }

        public static Image ReceiveBitmap(string ip, int port)
        {
            TcpClient client = new TcpClient();
            bool connected = false;

            while (!connected)
            {
                try
                {
                    client.Connect(ip, port);
                    connected = true;
                }
                catch (SocketException)
                {
                    Console.WriteLine("Could not connect. Retrying in 5 seconds...");
                    Thread.Sleep(5000);
                }
            }

            Console.WriteLine("Connected!");
            NetworkStream stream = client.GetStream();

            Image image = Image.FromStream(stream);

            stream.Close();
            client.Close();

            return image;
        }
    }


}
