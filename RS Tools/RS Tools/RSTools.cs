﻿using System;
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
using System.IO;

namespace RS_Tools
{
    class RSTools
    {


        // Config values
        static bool isRunning = true;
        private object messageQueue;
        public static readonly object _lockObj = new object();
        public static readonly long _bootTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();

        //[STAThread]
        static void Main(string[] args)
        {
            Display.eng = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            Display.eng.SetVariable("debug_file", "nul");
            Console.WriteLine("RS Tools by gnmmarechal");
            Console.WriteLine("\nTesseract Version: " + Display.eng.Version);

            Config cfg = new Config("config.cfg");

            if (args.Length == 1)
            {
                cfg = new Config(args[0]);
            }
            cfg.SetBootTime(_bootTime);
            Console.WriteLine("\n===Hit ENTER to load all plugins===");
            Console.ReadKey();


            Console.WriteLine("Starting screen capture and loading all plugins...");


            int loopCount = 0;

            Display d1 = new Display();

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

            List<String> disabledPluginList = new List<String>();

            PluginAPI.WriteLine("Starting plugin execution loop.");
            while (isRunning)
            {

                // New System 

                //Console.WriteLine("Game Area BMP {0}: " + Convert.ToString(gameAreaScreenshot.Width) + "x" + Convert.ToString(gameAreaScreenshot.Height), Convert.ToString(loopCount));


                // Run Plugins
                List<Thread> threadList = new List<Thread>();
                bool runWorker = true;
                foreach (RSToolsPlugin plugin in PluginLoader.Plugins)
                {
                    if (!disabledPluginList.Contains(plugin.PluginPackage))
                    {
                        Thread t = new Thread(() =>
                        {
                            while (runWorker)
                            {
                                Bitmap completeScreenshot = Display.GetWholeDisplayBitmap();
                                Bitmap gameAreaScreenshot = Display.CropBitmap(completeScreenshot, cfg.xOffset, cfg.yOffset, cfg.gameResolution[0], cfg.gameResolution[1]);
                                completeScreenshot.Dispose();
                                plugin.Run((Bitmap)gameAreaScreenshot);
                                gameAreaScreenshot.Dispose();
                                GC.Collect();
                            }

                        });

                        t.Start();
                        threadList.Add(t);
                    }

                }

                // Read user commands

                string input;
                do
                {
                    input = Console.ReadLine();
                    switch(input.Split(' ')[0])
                    {
                        case "disable":

                            break;
                    }
                } while (!String.IsNullOrWhiteSpace(input));
                runWorker = false;


                foreach (Thread t in threadList) // This might be unnecessary right now.
                {
                    t.Join();
                }

                
                loopCount++;
            }


            Console.ReadLine();
            //engine.Dispose();
            Display.eng.Dispose();

        }



        static void sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }



        static void checkImage(Bitmap screenshot)
        {
            Application.EnableVisualStyles();
            PictureForm f = new PictureForm();
            f.picture.Image = screenshot;
            //screenshot.Dispose();
            Application.Run(f);
        }

    }
}