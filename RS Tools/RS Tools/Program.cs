using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Media;
using System.Threading;
using Tesseract;
using System.Text.RegularExpressions;

namespace RS_Tools
{
    class Program
    {

        // Config values
        static bool extraWindows = false;
        static int minHealthValue = 2000;


        [STAThread]
        static void Main(string[] args)
        {

            Console.WriteLine("RS Tools by gnmmarechal");
            Display.POINT c1, c2;
            Display.POINT healthC1, healthC2;

            c1.X = 1917;
            c1.Y = 791;
            healthC1.X = 1999;
            healthC1.Y = 685;

            c2.X = 2449;
            c2.Y = 1045;
            healthC2.X = 2077;
            healthC2.Y = 701;

            /*Console.WriteLine("Chat Scanner Coordinates:");
            Console.WriteLine("C1>");
            Console.ReadKey();
            Display.GetCursorPos(out c1);
            Console.WriteLine("C1: {0}, {1}", c1.X, c1.Y);

            Console.WriteLine("C2>");
            Console.ReadKey();
            Display.GetCursorPos(out c2);
            Console.WriteLine("C2: {0}, {1}", c2.X, c2.Y);



            Console.WriteLine("Health Scanner Coordinates:");
            Console.WriteLine("HC1>");
            Console.ReadKey();
            Display.GetCursorPos(out healthC1);
            Console.WriteLine("HC1: {0}, {1}", healthC1.X, healthC1.Y);

            Console.WriteLine("HC2>");
            Console.ReadKey();
            Display.GetCursorPos(out healthC2);
            Console.WriteLine("HC2: {0}, {1}", healthC2.X, healthC2.Y);*/


            Console.WriteLine("Starting screen capture and analysis...");


            int loopCount = 0;

            string[] targetChatTerms = { "tasks in a row", "shines", "loot", "Return to a Slayer Master", "receive", "eceive", "You receive", "ring of fortune", "ring of wealth", "beam"};
            TextMatcher matcher = new TextMatcher(targetChatTerms);
            Display d1 = new Display();
            
            var f = new PictureForm();
            var t = new Thread(() => {
                
                f.ShowDialog();
            });
            t.SetApartmentState(ApartmentState.STA);

            var f2 = new StatsWindow();
            var t2 = new Thread(() => {

                f2.ShowDialog();
            });
            t2.SetApartmentState(ApartmentState.STA);

            if (!extraWindows)
            {
                Console.WriteLine("Disabling extra windows...");
            }
            else
            {
                Console.WriteLine("Starting extra windows...");
                t.Start();
                t2.Start();
            }

            while (true)
            {
                // Chat scanner 
                Bitmap screenshot = Display.GetAreaBitmap(c1, c2);
                Bitmap bigSc = Display.ResizeImage(screenshot, screenshot.Width * 5, screenshot.Height * 5);
                Bitmap bigSc2 = Display.AdjustContrast(bigSc, 40);
                Console.WriteLine("Chat BMP {0}: " + screenshot.Width + "x" + screenshot.Height, loopCount++);
                if (f.Visible)
                    f.Invoke(new Action(() => { f.picture.Image = bigSc2; }));
                string txt = "";
                try
                {
                    txt = Display.GetText(bigSc2);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

                Console.WriteLine(txt);
                if (matcher.certainMatch(txt))
                {
                    alert();
                    Console.ReadLine();
                }

                // Health scanner
                Bitmap healthSc = Display.GetAreaBitmap(healthC1, healthC2);
                Bitmap healthScPostA = Display.ResizeImage(healthSc, healthSc.Width * 3, healthSc.Height * 3);
                Bitmap healthScPostB = Display.AdjustContrast(healthScPostA, 40);
                int[] health = { -1, -1 };

                try
                {
                    health = parseHealth(healthScPostB);
                } catch (Exception e)
                {
                    Console.WriteLine("Invalid health values. Skipping.");
                }

                Console.WriteLine("Health Scanner Values: {0}/{1}", health[0], health[1]);
                if (f2.Visible)
                    f2.Invoke(new Action(() => { f2.healthValue.Text = Convert.ToString(health[0]); }));

                if (f2.Visible)
                {
                    if (f2.healthWarningCheckbox.Checked && health[0] < minHealthValue && health[0] != -1)
                    {
                        healthAlert();
                    }
                }
                else if (health[0] < minHealthValue && health[0] != -1)
                {
                    healthWarning();
                }



                // Dispose of objects
                screenshot.Dispose();
                bigSc.Dispose();
                bigSc2.Dispose();
                healthSc.Dispose();
                healthScPostA.Dispose();
                healthScPostB.Dispose();
                

            }


            Console.ReadLine();
        }

        private static int[] parseHealth(Bitmap healthScPostB)
        {
            int[] healthVals = new int[2];
            string textVals = RemoveWhitespace(Display.GetText(healthScPostB));
            string[] t = textVals.Split('/');
            Console.WriteLine("H String: " + textVals);
            healthVals[0] = Convert.ToInt32(t[0]);
            healthVals[1] = Convert.ToInt32(t[1]);
            return healthVals;
        }

        static void sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }

        public static string RemoveWhitespace(string input)
        {
            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        static void checkImage(Bitmap screenshot)
        {
            Application.EnableVisualStyles();
            PictureForm f = new PictureForm();
            f.picture.Image = screenshot;
            Application.Run(f);
        }

        static void alert()
        {
            ConsoleColor cur = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("MATCH FOUND!");
            for (int i = 0; i < 10; i++)
            {
                SystemSounds.Beep.Play();
                sleep(600);
                SystemSounds.Beep.Play();
                sleep(600);
                SystemSounds.Beep.Play();
            }
            Console.ForegroundColor = cur;
        }

        static void healthAlert()
        {
            
            for (int i = 0; i < 3; i++)
            {
                SystemSounds.Beep.Play();
                sleep(600);
                SystemSounds.Beep.Play();
                sleep(600);
                SystemSounds.Beep.Play();
            }
            MessageBox.Show("Warning! Low health!");
        }

        static void healthWarning()
        {
            ConsoleColor cur = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("WARNING: LOW HEALTH!");
            for (int i = 0; i < 2; i++)
            {
                SystemSounds.Beep.Play();
                sleep(600);
                SystemSounds.Beep.Play();
                sleep(600);
                SystemSounds.Beep.Play();
            }
            Console.ForegroundColor = cur;
        }
    }
}
