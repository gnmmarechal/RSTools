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
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace RS_Tools
{

    public class RSTools
    {


        // Config values
        static bool isRunning = true;
        public static readonly object _lockObj = new object();
        public static readonly object _lockObj2 = new object();
        public static readonly object _lockObj3 = new object();
        public static readonly long _bootTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
        public static bool _overlayIgnoreTopWindow = false;

        private static PluginAPIOverlay overlayForm = null;
        public static Queue<Control> controlAddQueue = new Queue<Control>();
        public static Queue<string> controlRemoveQueue = new Queue<string>();
        public static Queue<string> overlayLogQueue = new Queue<string>();

        private static Bitmap gameScreen = null;
        private static bool runOverlay = true;

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
            _overlayIgnoreTopWindow = cfg.overlayIgnoreTopWindow;


            /*Thread screenshotThread = new Thread(() => 
            {

            });*/

            //overlayThread.SetApartmentState(ApartmentState.STA);

            
            Thread overlayThread = new Thread(() =>
           {
               //Application.SetCompatibleTextRenderingDefault(false);
               overlayForm = new PluginAPIOverlay();
               overlayForm.StartPosition = FormStartPosition.Manual;
               overlayForm.Top = cfg.yOffset;
               overlayForm.Left = cfg.xOffset;
               overlayForm.Size = new Size(cfg.gameResolution[0], cfg.gameResolution[1]);
               overlayForm.gameWindowTitle = cfg.gameWindowName;
               overlayForm.ShowDialog();
           });

            overlayThread.SetApartmentState(ApartmentState.STA);
            overlayThread.Start();


            //Display.CropBitmap(Display.GetWholeDisplayBitmap(), 3703, 966, 3735-3703, 988-966).Save("lastInvSlot.bmp");
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
                foreach (RSToolsPluginBase plugin in PluginLoader.Plugins)
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
            foreach (RSToolsPluginBase plugin in PluginLoader.Plugins)
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
                runOverlay = true;

                foreach (RSToolsPluginBase plugin in PluginLoader.Plugins)
                {
                    if (!disabledPluginList.Contains(plugin.PluginPackage))
                    {
                        Thread t = null;


                        // Here add a check to see if a plugin is both ("hybrid") and check flags to see what to run it as

                        if (plugin is RSToolsPlugin)
                        {
                            t = new Thread(() =>
                            {
                                while (runWorker)
                                {
                                    Bitmap completeScreenshot = Display.GetWholeDisplayBitmap();
                                    Bitmap gameAreaScreenshot = Display.CropBitmap(completeScreenshot, cfg.xOffset, cfg.yOffset, cfg.gameResolution[0], cfg.gameResolution[1]);
                                    completeScreenshot.Dispose();
                                    ((RSToolsPlugin)plugin).Run(gameAreaScreenshot);
                                    gameAreaScreenshot.Dispose();
                                    GC.Collect();
                                }

                            });
                        }
                        else if (plugin is MiniRSToolsPlugin) // Plugins that don't require a game bitmap
                        {
                            t = new Thread(() =>
                            {
                                while (runWorker)
                                {
                                    ((MiniRSToolsPlugin)plugin).Run();
                                    GC.Collect();
                                }
                            });
                        }

                        t.Start();
                        threadList.Add(t);
                    }

                }

                // Read user commands

                string input;
                do
                {
                    input = Console.ReadLine();
                    string arg = string.Join("", input.Split(' ').Skip(1).ToArray());
                    String command = input.Split(' ')[0];
                    command = Regex.Replace(command, @"[^0-9a-zA-Z]+", "");
                    if (command.Equals("disable"))
                    {
                        if (!disabledPluginList.Contains(arg))
                        {
                            PluginAPI.WriteLine("Adding plugin to the blacklist...");
                            disabledPluginList.Add(arg);
                        }
                    }
                    else if (command.Equals("enable"))
                    {
                        if (disabledPluginList.Contains(arg))
                        {
                            PluginAPI.WriteLine("Adding plugin to the blacklist...");
                            disabledPluginList.Remove(arg);
                        }
                    }
                    else if (command.Equals("report"))
                    {
                        PluginAPI.WriteLine("Blacklisted packages:\n");
                        foreach (String plugPackage in disabledPluginList)
                        {
                            PluginAPI.WriteLine(plugPackage);
                        }
                    }
                    else if (command.Equals("quit") || command.Equals("exit"))
                    {
                        PluginAPI.WriteLine("Shutting down RSTools...");
                        isRunning = false;
                    }
                } while (!String.IsNullOrWhiteSpace(input));
                while (Console.KeyAvailable)
                {
                    Console.ReadKey(false);
                }

                PluginAPI.WriteLine("Restarting plugin threads...\n");
                runWorker = false;
                runOverlay = false;

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


        // Manage Overlay Form
        public static string AddOverlayControl(Control c)
        {
            lock (_lockObj2)
            {
                controlAddQueue.Enqueue(c);
                
                //overlayForm.AddControl(tempControl);
                return c.Name;
            }
            

        }

        public static void RemoveOverlayControl(String name)
        {
            lock (_lockObj2)
            {
                controlRemoveQueue.Enqueue(name);
            }
        }

        public static void OverlayStandardLog(String text)
        {
            string callingMethod = new StackFrame(1).GetMethod().DeclaringType.Name;
            lock (_lockObj2)
            {
                overlayLogQueue.Enqueue("[" + callingMethod + "] : " + text);
            }
        }

        public static void ClearLog()
        {
            lock (_lockObj2)
            {
                overlayForm.Invoke(new Action(() =>
                {
                    overlayForm.ClearLog();
                }));
            }
        }


    }
}
