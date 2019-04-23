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
using Tesseract;

namespace RSTExternalOCR
{
    class Program
    {
        static TesseractEngine localEng = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
        static readonly string ip = "127.0.0.1";
        static readonly int port = 49153;
        static readonly object _lockObject = new object();
        private static object _lockObj = new object();

        static void Main(string[] args)
        {
            
            //Display.eng.SetVariable("classify_enable_learning", "false");

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

                // Start client handler thread
                new Thread(() =>
                {
                    ClientHandler(client);
                }).Start();

                //client.Close();
            }
            listener.Stop();
            Console.ReadLine();


        }

        public static string GetText(Bitmap imgsource)
        {

            lock (_lockObj)
            {
                var ocrtext = string.Empty;
                using (var img = PixConverter.ToPix(imgsource))
                {


                    using (var page = localEng.Process(img))
                    {
                        ocrtext = page.GetText();
                    }

                }

                return ocrtext;
            }

        }

        static void ClientHandler(TcpClient client)
        {
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

            Bitmap original = null;
            //Console.WriteLine("[{0}]", string.Join(", ", buffer));
            var ms = new MemoryStream(buffer);
            ms.Seek(0, SeekOrigin.Begin);
            original = new Bitmap(ms);

            Bitmap copy = new Bitmap(original.Width, original.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
            using (Graphics graphics = Graphics.FromImage(copy))
            {
                graphics.DrawImage(original, new Point(0, 0));
            }
            ms.Close();

            //Bitmap bigSc = Display.ResizeImage(copy, copy.Width * 5, copy.Height * 5);


            //string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            //Console.WriteLine("Received : " + dataReceived);

            //---write back the text to the client---
            //Console.WriteLine("Sending back : " + dataReceived);
            string reply = "";
            reply = GetText(copy);
            
            Console.WriteLine(reply);
            buffer = Encoding.ASCII.GetBytes(reply);
            nwStream.Write(buffer, 0, buffer.Length);
            copy.Dispose();
            original.Dispose();
            //client.Close();
        }
    }


}
