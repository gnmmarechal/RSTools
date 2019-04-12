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
using EyeOpen.Imaging;

namespace RS_Tools
{
    class Program
    {

        // Tesseract Engine
        //public static TesseractEngine engine = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
        

        // Config values
        static bool extraWindows = false;
        static int minHealthValue = 2000;
        static bool isRunning = true;


        //[STAThread]
        static void Main(string[] args)
        {
            Display.eng = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            Console.WriteLine("RS Tools by gnmmarechal");
            Display.POINT c1, c2;
            Display.POINT healthC1, healthC2;

            Config cfg = new Config("config.cfg");
            c1 = cfg.ChatScanner[0];
            c2 = cfg.ChatScanner[1];
            healthC1 = cfg.HealthScanner[0];
            healthC2 = cfg.HealthScanner[1];
            minHealthValue = cfg.minHealth;


            Console.WriteLine("Config Read:\n" +
                "Chat Scanner: (" + c1.X + "," + c1.Y + ") - (" + c2.X + "," + c2.Y + ")\n" +
                "Health Scanner: (" + healthC1.X + "," + healthC1.Y + ") - (" + healthC2.X + "," + healthC2.Y + ")\n" +
                "Minimum Health Value: " + minHealthValue + "\n" +
                "Prayer Scanner: (" + cfg.PrayerScanner[0].X + "," + cfg.PrayerScanner[0].Y + ") - (" + cfg.PrayerScanner[1].X + "," + cfg.PrayerScanner[1].Y + ")");



            Console.WriteLine("\n===Hit ENTER to load all plugins===");
            Console.ReadKey();


            Console.WriteLine("Starting screen capture and loading all plugins...");


            int loopCount = 0;

            Display d1 = new Display();
            
            /*var f = new PictureForm();
            var t = new Thread(() => {
                
                f.ShowDialog();
            });
            //t.SetApartmentState(ApartmentState.STA);

            var f2 = new StatsWindow();
            var t2 = new Thread(() => {

                f2.ShowDialog();
            });
            //t2.SetApartmentState(ApartmentState.STA);

            if (!extraWindows)
            {
                Console.WriteLine("Disabling extra windows...");
            }
            else
            {
                Console.WriteLine("Starting extra windows...");
                t.Start();
                t2.Start();
            }*/



            // Load Plugins
            PluginLoader loader;
            try
            {
                loader = new PluginLoader();
                loader.loadPlugins();
                PluginAPI.WriteLine("Loaded plugins:");
                foreach (RSToolsPlugin plugin in PluginLoader.Plugins)
                {
                    string pluginInfo = plugin.PluginName + " | " + plugin.PluginPackage + " | v" + plugin.PluginVersion;
                    PluginAPI.WriteLine(pluginInfo);
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(string.Format("Plugins couldn't be loaded: {0}", e.Message));
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            PluginAPI.WriteLine("Plugin setup started.");
            foreach (RSToolsPlugin plugin in PluginLoader.Plugins)
            {
                plugin.Setup(cfg);
            }

            PluginAPI.WriteLine("Starting plugin execution loop.");
            while (isRunning)
            {

                // New System
                Bitmap completeScreenshot = Display.GetWholeDisplayBitmap();
                Bitmap gameAreaScreenshot = Display.CropBitmap(completeScreenshot, cfg.xOffset, cfg.yOffset, cfg.gameResolution[0], cfg.gameResolution[1]);
                //Console.WriteLine("Game Area BMP {0}: " + Convert.ToString(gameAreaScreenshot.Width) + "x" + Convert.ToString(gameAreaScreenshot.Height), Convert.ToString(loopCount++));
                completeScreenshot.Dispose();

                // Run Plugins

                foreach (RSToolsPlugin plugin in PluginLoader.Plugins)
                {
                    // Must use Clone() or the bitmap will get corrupted (?)
                    plugin.Run((Bitmap)gameAreaScreenshot.Clone());
                }

                gameAreaScreenshot.Dispose();

                // Health scanner
                /* Bitmap healthSc = Display.GetAreaBitmap(healthC1, healthC2);
                 Bitmap healthScPostA = Display.ResizeImage(healthSc, healthSc.Width * 3, healthSc.Height * 3);
                 Bitmap healthScPostB = Display.AdjustContrast(healthScPostA, 40);
                 int[] health = { -1, -1 };

                 try
                 {
                     health = parseHealth(healthScPostB);
                 } catch (Exception e)
                 {
                     Console.WriteLine("Invalid health values. Skipping.");
                     GC.Collect();
                 }

                 Console.WriteLine("Parsed Health Scanner Values: {0}/{1}", health[0], health[1]);
                 //GC.Collect();
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
                 gameAreaScreenshot.Dispose();
                 //chatAreaBitmap.Dispose();
                 //bigSc.Dispose();
                 //bigSc2.Dispose();
                 healthSc.Dispose();
                 healthScPostA.Dispose();
                 healthScPostB.Dispose();*/
                GC.Collect();
                //Console.Clear();
                //sleep(100);
            }


            Console.ReadLine();
            //engine.Dispose();
            Display.eng.Dispose();

        }

        private static int[] parseHealth(Bitmap healthScPostB)
        {
            int[] healthVals = new int[2];
            string textVals = RemoveWhitespace(Display.GetText(healthScPostB));
            string[] t = textVals.Split('/');
            Console.WriteLine("Health String: " + textVals);
            healthVals[0] = Convert.ToInt32(t[0]);
            healthVals[1] = Convert.ToInt32(t[1]);
            healthScPostB.Dispose();
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
            screenshot.Dispose();
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
