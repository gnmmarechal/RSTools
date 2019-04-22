using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RS_Tools
{
    public static class PluginAPI
    {
        private static object lockObj = new object();

        private static Bitmap screenshotBitmap = null;
        private static long lastScreenshotTime = 0;
        private static double maxScrPerSecond = 2;

        private static Bitmap gameScreenBitmap = null;
        private static long lastGameScrTime = 0;


        public static void WriteLine(string message)
        {
            try
            {
                ConsoleColor cur = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[" + new StackFrame(1).GetMethod().DeclaringType.Name + "] : ");
                Console.ForegroundColor = cur;
                Console.WriteLine(message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void SuccessWriteLine(string message)
        {
            try
            {
                ConsoleColor cur = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[" + new StackFrame(1).GetMethod().DeclaringType.Name + "] : ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(message);
                Console.ForegroundColor = cur;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void WarningWriteLine(string message)
        {
            try
            {
                ConsoleColor cur = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("[" + new StackFrame(1).GetMethod().DeclaringType.Name + "] : ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(message);
                Console.ForegroundColor = cur;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static void alert()
        {
            playSound("Assets\\alert.wav");
        }
        public static void playSound(String soundPath)
        {
            SoundPlayer player = new SoundPlayer();
            player.SoundLocation = soundPath;
            player.Play();
        }

        //

        public static Win32.POINT[] GetRectangle(int x1, int y1, int x2, int y2)
        {
            Win32.POINT p1 = GetPoint(x1, y1);
            Win32.POINT p2 = GetPoint(x2, y2);
            Win32.POINT[] pArray = { p1, p2 };

            return pArray;
        }
        public static Win32.POINT GetPoint(int x, int y)
        {
            Win32.POINT p;
            p.X = x;
            p.Y = y;
            return p;
        }

        public static Win32.POINT CorrectOffsets(Win32.POINT ogPoint, int xOffset, int yOffset)
        {
            Win32.POINT ogPoint2;
            ogPoint2.X = ogPoint.X + xOffset;
            ogPoint2.Y = ogPoint.Y + yOffset;
            return ogPoint2;
        }

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        private static Bitmap RequestScreenshot()
        {
            long curTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            if (curTime > lastScreenshotTime + 1000/maxScrPerSecond)
            {
                screenshotBitmap = Display.GetWholeDisplayBitmap();
                lastScreenshotTime = curTime;
            }

            return screenshotBitmap;
        }

        public static Bitmap RequestGameScreenshot(int xOffset, int yOffset, int windowResX, int windowResY)
        {
            lock (lockObj)
            {
                long curTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();


                if (curTime > lastGameScrTime + 1000 / maxScrPerSecond)
                {
                    gameScreenBitmap = Display.CropBitmap(RequestScreenshot(), xOffset, yOffset, windowResX, windowResY);
                    lastGameScrTime = curTime;
                }

                return gameScreenBitmap;
            }

        }

        public static void RandWait(int minDelay, int maxDelay)
        {
            Random r = new Random();
            Thread.Sleep(r.Next(minDelay, maxDelay));
        }

        public static void HumanWait(int minDelay)
        {
            Random e = new Random();
            int maxDelay = e.Next(minDelay, minDelay + 5000);
            RandWait(minDelay, maxDelay);
        }

        public static void CopyPropertiesTo<T, TU>(this T source, TU dest)
        {
            var sourceProps = typeof(T).GetProperties().Where(x => x.CanRead).ToList();
            var destProps = typeof(TU).GetProperties()
                    .Where(x => x.CanWrite)
                    .ToList();

            foreach (var sourceProp in sourceProps)
            {
                if (destProps.Any(x => x.Name == sourceProp.Name))
                {
                    var p = destProps.First(x => x.Name == sourceProp.Name);
                    if (p.CanWrite)
                    { // check if the property can be set or no.
                        p.SetValue(dest, sourceProp.GetValue(source, null), null);
                    }
                }

            }

        }
    }
}
