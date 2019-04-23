using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Network_OCR_Test
{
    class Program
    {
        static readonly string ip = "127.0.0.1";
        static readonly int port = 49153;
        static void Main(string[] args)
        {

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(ip, port);
            NetworkStream nwStream = client.GetStream();
            Bitmap testBmp = new Bitmap("playNow.bmp");
            byte[] imageBytes = ImageToByte(testBmp);
            byte[] imageSize = BitConverter.GetBytes(imageBytes.Length);
            byte[] bytesToSend = new byte[imageBytes.Length + imageSize.Length];
            imageSize.CopyTo(bytesToSend, 0);
            imageBytes.CopyTo(bytesToSend, imageSize.Length);
            //File.WriteAllBytes("sentbytes.txt", bytesToSend);
            Console.WriteLine("MESSAGE SIZE: " + (bytesToSend.Length - imageBytes.Length));
            Console.WriteLine("IMAGE SIZE:" + imageBytes.Length);
            //Console.WriteLine("[{0}]", string.Join(", ", bytesToSend));

            //---send the text---
            Console.WriteLine("Sending bmp...");
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
            Console.ReadLine();
            client.Close();
        }

        public static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                return stream.ToArray();
            }
        }
    }
}
