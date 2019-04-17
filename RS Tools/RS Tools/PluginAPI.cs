﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    public static class PluginAPI
    {
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

        public static Display.POINT[] GetRectangle(int x1, int y1, int x2, int y2)
        {
            Display.POINT p1 = GetPoint(x1, y1);
            Display.POINT p2 = GetPoint(x2, y2);
            Display.POINT[] pArray = { p1, p2 };

            return pArray;
        }
        public static Display.POINT GetPoint(int x, int y)
        {
            Display.POINT p;
            p.X = x;
            p.Y = y;
            return p;
        }

        public static Display.POINT CorrectOffsets(Display.POINT ogPoint, int xOffset, int yOffset)
        {
            Display.POINT ogPoint2;
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

    }
}
