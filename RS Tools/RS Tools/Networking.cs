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
        public static void SendImage(Image obj)
        {
            TcpListener listener = new TcpListener(49153);
            listener.Start();
            TcpClient client = listener.AcceptTcpClient();
            NetworkStream stream = client.GetStream();

            Image image = obj;
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, ImageFormat.Bmp);
                byte[] imageBuffer = ms.GetBuffer();
                stream.Write(imageBuffer, 0, (int)ms.Length);
            }

            stream.Close(500);
            client.Close();
            listener.Stop();
        }
    }
}
