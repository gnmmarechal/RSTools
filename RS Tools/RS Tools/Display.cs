using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Tesseract;
using System.Drawing.Drawing2D;
using EyeOpen.Imaging;
using System.Diagnostics;
using static RS_Tools.Win32;
using System.Net.Sockets;

namespace RS_Tools
{
    public class Display
    {

        public static TesseractEngine eng = null;
        internal static bool useNetworkOCR = false;
        internal static string networkOCRIP;
        internal static int networkOCRPort;

        public Display()
        {
            eng = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            eng.SetVariable("debug_file", "nul");
        }

        public static double getBitmapSimilarity(Bitmap a, Bitmap b)
        {
            ComparableImage a1 = new ComparableImage(a), b1 = new ComparableImage(b);

            return a1.CalculateSimilarity(b1);
        }

        public static Bitmap GetScreenBitmap()
        {
            Bitmap bmp = new Bitmap(SystemInformation.VirtualScreen.Width,
                               SystemInformation.VirtualScreen.Height,
                               PixelFormat.Format32bppArgb);
            Graphics gr = Graphics.FromImage(bmp);
            gr.CopyFromScreen(0, 0, 0, 0, bmp.Size);

            return bmp;
        }

        /*public static Bitmap cropBitmap(Bitmap source, POINT StartPoint, POINT EndPoint)
        {
            Bitmap CroppedImage = source; 
            int x = Math.Min(StartPoint.X, EndPoint.X);
            int y = Math.Min(StartPoint.Y, EndPoint.Y);
            int width = Math.Abs(StartPoint.X - EndPoint.X);
            int height = Math.Abs(StartPoint.Y - EndPoint.Y);
            Rectangle source_rect = new Rectangle(x, y, width, height);
            Rectangle dest_rect = new Rectangle(0, 0, width, height);

            // Copy that part of the image to a new bitmap.
            Bitmap DisplayImage = new Bitmap(width, height);
            Graphics DisplayGraphics = Graphics.FromImage(DisplayImage);
            DisplayGraphics.DrawImage(CroppedImage, dest_rect, source_rect, GraphicsUnit.Pixel);

            return CroppedImage;
        }*/
        public static Bitmap GetWholeDisplayBitmap()
        {
            POINT Zero, Limit;
            Limit.X = SystemInformation.VirtualScreen.Width;
            Limit.Y = SystemInformation.VirtualScreen.Height;
            Zero.X = 0;
            Zero.Y = 0;
            return GetAreaBitmap(Zero, Limit);
        }
        public static Bitmap GetAreaBitmap(POINT c1, POINT c2)
        {
            //Bitmap bmp = GetScreenBitmap();
            int X = c2.X - c1.X;
            int Y = c2.Y - c1.Y;
            Bitmap bmp = new Bitmap(X, Y, PixelFormat.Format32bppArgb);
            Graphics gr = Graphics.FromImage(bmp);
            try
            {
                gr.CopyFromScreen(c1.X, c1.Y, 0, 0, bmp.Size);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return bmp;
        }
        public static Bitmap ConvertToAForgeFormat(Image image)
        {
            return ConvertToFormat(image, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }
        public static Bitmap ConvertToFormat(Image image, PixelFormat format)
        {
            Bitmap copy = new Bitmap(image.Width, image.Height, format);
            using (Graphics gr = Graphics.FromImage(copy))
            {
                gr.DrawImage(image, new Rectangle(0, 0, copy.Width, copy.Height));
            }
            return copy;
        }

        public static Bitmap CropBitmap(Bitmap bitmap, POINT cropStart, POINT cropEnd)
        {
            int X1 = Math.Min(cropStart.X, cropEnd.X);
            int Y1 = Math.Min(cropStart.Y, cropEnd.Y);
            int X2 = Math.Abs(cropStart.X - cropEnd.X);
            int Y2 = Math.Abs(cropStart.Y - cropEnd.Y);
            return CropBitmap(bitmap, X1, Y1, X2, Y2);
        }
        public static Bitmap CropBitmap(Bitmap bitmap, int cropX, int cropY, int cropWidth, int cropHeight)
        {
            Rectangle rect = new Rectangle(cropX, cropY, cropWidth, cropHeight);

            Bitmap cropped = bitmap.Clone(rect, bitmap.PixelFormat);
            //bitmap.Dispose();
            return cropped;
        }
        // Get colour at pixel
        public static Color GetColorAt(int x, int y)
        {
            IntPtr desk = GetDesktopWindow();
            IntPtr dc = GetWindowDC(desk);
            int a = (int)GetPixel(dc, x, y);
            ReleaseDC(desk, dc);
            return Color.FromArgb(255, (a >> 0) & 0xff, (a >> 8) & 0xff, (a >> 16) & 0xff);
        }
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.None;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            //image.Dispose();
            return destImage;
        }

        public static Bitmap ResizeImageSmoothing(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }
            //image.Dispose();
            return destImage;
        }

        public static Bitmap AdjustContrast(Bitmap Image, float Value)
        {
            Value = (100.0f + Value) / 100.0f;
            Value *= Value;
            Bitmap NewBitmap = (Bitmap)Image.Clone();
            BitmapData data = NewBitmap.LockBits(
                new Rectangle(0, 0, NewBitmap.Width, NewBitmap.Height),
                ImageLockMode.ReadWrite,
                NewBitmap.PixelFormat);
            int Height = NewBitmap.Height;
            int Width = NewBitmap.Width;

            unsafe
            {
                for (int y = 0; y < Height; ++y)
                {
                    byte* row = (byte*)data.Scan0 + (y * data.Stride);
                    int columnOffset = 0;
                    for (int x = 0; x < Width; ++x)
                    {
                        byte B = row[columnOffset];
                        byte G = row[columnOffset + 1];
                        byte R = row[columnOffset + 2];

                        float Red = R / 255.0f;
                        float Green = G / 255.0f;
                        float Blue = B / 255.0f;
                        Red = (((Red - 0.5f) * Value) + 0.5f) * 255.0f;
                        Green = (((Green - 0.5f) * Value) + 0.5f) * 255.0f;
                        Blue = (((Blue - 0.5f) * Value) + 0.5f) * 255.0f;

                        int iR = (int)Red;
                        iR = iR > 255 ? 255 : iR;
                        iR = iR < 0 ? 0 : iR;
                        int iG = (int)Green;
                        iG = iG > 255 ? 255 : iG;
                        iG = iG < 0 ? 0 : iG;
                        int iB = (int)Blue;
                        iB = iB > 255 ? 255 : iB;
                        iB = iB < 0 ? 0 : iB;

                        row[columnOffset] = (byte)iB;
                        row[columnOffset + 1] = (byte)iG;
                        row[columnOffset + 2] = (byte)iR;

                        columnOffset += 4;
                    }
                }
            }

            NewBitmap.UnlockBits(data);
            //Image.Dispose();
            return NewBitmap;
        }

        public static string GetText(Bitmap imgsource)
        {
            try
            {
                if (useNetworkOCR)
                    return Networking.NetworkOCR(imgsource, networkOCRIP, networkOCRPort);
            }
            catch (SocketException)
            {
                PluginAPI.WriteLine("Remote OCR server error. Defaulting to the local engine.");
                if (eng == null)
                {
                    PluginAPI.WriteLine("Local OCR engine is null!");
                    throw new Exception("OCR engine is null!");
                }
            }

            var ocrtext = string.Empty;
            using (var img = PixConverter.ToPix(imgsource))
            {
                lock (RSTools._lockObj) // You can only process one image at a time
                {


                    using (var page = eng.Process(img)) // Program.engine
                    {
                        ocrtext = page.GetText();
                    }

                }
            }

            //imgsource.Dispose();
            return ocrtext;
        }

        public static string[] GetTextLines(Bitmap imgsource)
        {
            return GetText(imgsource).Split('\n');
        }

        public static string GetActiveWindowTitle()
        {
            return GetWindowTitle(GetForegroundWindow());
        }

        public static string GetWindowTitle(IntPtr handle)
        {
            const int nChars = 256;
            StringBuilder Buff = new StringBuilder(nChars);

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }
    }
}
