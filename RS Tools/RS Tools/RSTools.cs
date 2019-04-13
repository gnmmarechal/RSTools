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
    class RSTools
    {


        // Config values
        static bool isRunning = true;
        public static readonly object _lockObj = new object();

        //[STAThread]
        static void Main(string[] args)
        {
            Display.eng = new TesseractEngine(@"./tessdata", "eng", EngineMode.Default);
            Console.WriteLine("RS Tools by gnmmarechal");

            Config cfg = new Config("config.cfg");

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

            PluginAPI.WriteLine("Starting plugin execution loop.");
            while (isRunning)
            {

                // New System 

                //Console.WriteLine("Game Area BMP {0}: " + Convert.ToString(gameAreaScreenshot.Width) + "x" + Convert.ToString(gameAreaScreenshot.Height), Convert.ToString(loopCount));


                // Run Plugins
                List<Thread> threadList = new List<Thread>();
                foreach (RSToolsPlugin plugin in PluginLoader.Plugins)
                {
                    /* To-do:
                     * Add multithread support.
                     * Each plugin should be its own thread.
                     * Ex.
                     * Thread t = new Thread( () => (plugin.Run(blabla)));
                     * t.Start()
                     * 
                     * then t.Join() to wait for it to finish before moving on to the next loop.
                     */
                    Thread t = new Thread(() =>
                   {
                       Bitmap completeScreenshot = Display.GetWholeDisplayBitmap();
                       Bitmap gameAreaScreenshot = Display.CropBitmap(completeScreenshot, cfg.xOffset, cfg.yOffset, cfg.gameResolution[0], cfg.gameResolution[1]);
                       completeScreenshot.Dispose();
                         //PluginAPI.WriteLine("Screenshot taken");
                         plugin.Run((Bitmap)gameAreaScreenshot);
                       gameAreaScreenshot.Dispose();
                   });

                    t.Start();
                    threadList.Add(t);
                }



                foreach (Thread t in threadList)
                {
                    t.Join();
                }

                GC.Collect();
                loopCount++;
                //Console.Clear();
                //sleep(100);
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
