using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RS_Tools
{
    public class Networking
    {
        public static string NetworkOCR(Bitmap obj, string ip, int port)
        {

            //---create a TCPClient object at the IP and port no.---
            TcpClient client = new TcpClient(ip, port);
            NetworkStream nwStream = client.GetStream();
            byte[] imageBytes = ImageToByte(obj);
            byte[] imageSize = BitConverter.GetBytes(imageBytes.Length);
            byte[] bytesToSend = new byte[imageBytes.Length + imageSize.Length];
            imageSize.CopyTo(bytesToSend, 0);
            imageBytes.CopyTo(bytesToSend, imageSize.Length);
            //Console.WriteLine("MESSAGE SIZE: " + (bytesToSend.Length - imageBytes.Length));
            //Console.WriteLine("IMAGE SIZE:" + imageBytes.Length);
            //Console.WriteLine("[{0}]", string.Join(", ", bytesToSend));

            //---send the text---
            //Console.WriteLine("Sending bmp...");
            nwStream.Write(bytesToSend, 0, bytesToSend.Length);

            //---read back the text---
            byte[] bytesToRead = new byte[client.ReceiveBufferSize];
            int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
            string text = Encoding.ASCII.GetString(bytesToRead, 0, bytesRead);
            Console.WriteLine("Received : " + text);
            //Console.ReadLine();
            client.Close();

            return text;
        }


        public static byte[] ImageToByte(Image img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                return stream.ToArray();
            }
        }
    }
}
