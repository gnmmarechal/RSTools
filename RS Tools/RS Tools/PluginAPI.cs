using System;
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

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

    }
}
