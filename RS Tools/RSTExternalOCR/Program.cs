using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RS_Tools;
using Tesseract;

namespace RSTExternalOCR
{
    class Program
    {
        static readonly string ip = "127.0.0.1";
        static readonly int port = 49153;

        static void Main(string[] args)
        {
            Display.eng = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);

            bool running = true;
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(ip);
            TcpListener listener = new TcpListener(localAdd, port);
            Console.WriteLine("Listening...");
            listener.Start();
            while (running)
            {
                //---incoming client connected---
                TcpClient client = listener.AcceptTcpClient();

                //---get the incoming data through a network stream---
                NetworkStream nwStream = client.GetStream();
                byte[] buffer = new byte[sizeof(Int32)];
                int bytesRead = nwStream.Read(buffer, 0, sizeof(Int32));
                Console.WriteLine("[{0}]", string.Join(", ", buffer));
                // Receive data size
                int imageSize = BitConverter.ToInt32(buffer, 0);
                Console.WriteLine("IMAGE SIZE: " + imageSize);

                buffer = new byte[imageSize];
                bytesRead = nwStream.Read(buffer, 0, imageSize);
                

                //---convert the data received into a string---
                Bitmap original = null;
                //Console.WriteLine("[{0}]", string.Join(", ", buffer));
                var ms = new MemoryStream(buffer);
                ms.Seek(0, SeekOrigin.Begin);
                original = new Bitmap(ms);
                //original.Save("tets.bmp");
                //original = (Bitmap)Image.FromStream(ms);

                Bitmap copy = new Bitmap(original.Width, original.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                using (Graphics graphics = Graphics.FromImage(copy))
                {
                    graphics.DrawImage(original, new Point(0, 0));
                }
                ms.Close();
                //copy.Save("ye.bmp");
                Bitmap bigSc = Display.ResizeImage(copy, copy.Width * 5, copy.Height * 5);
                Bitmap bigSc2 = Display.AdjustContrast(bigSc, 40);


                //string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //Console.WriteLine("Received : " + dataReceived);

                //---write back the text to the client---
                //Console.WriteLine("Sending back : " + dataReceived);
                string reply = Display.GetText(bigSc2);
                Console.WriteLine(reply);
                buffer = Encoding.ASCII.GetBytes(reply);
                nwStream.Write(buffer, 0, buffer.Length);
                client.Close();
                copy.Dispose();
                original.Dispose();
                bigSc.Dispose();
                bigSc2.Dispose();
            }
            listener.Stop();
            Console.ReadLine();


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
