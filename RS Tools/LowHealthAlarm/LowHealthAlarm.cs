using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS_Tools;

namespace LowHealthAlarm
{
    public class LowHealthAlarm : RSToolsPlugin
    {
        private Config localConfig;

        public string PluginName
        {
            get
            {
                return "Low Health Alarm";
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Plays a sound when the detected health is low";
            }
        }

        public string PluginPackage
        {
            get
            {
                return "lowhpalarm";
            }
        }

        public int PluginVersion
        {
            get
            {
                return 1;
            }
        }

        public void Run(in System.Drawing.Bitmap gameImage)
        {
            // Parse Settings
            String settings = localConfig.GetSettings();
            String[] set = settings.Split(' ');
            Display.POINT[] HealthScanner = PluginAPI.GetRectangle(Convert.ToInt32(set[0]), Convert.ToInt32(set[1]), Convert.ToInt32(set[2]), Convert.ToInt32(set[3]));
            int minHealth = Convert.ToInt32(set[4]);
            // Offset correction
            if (HealthScanner[0].X >= localConfig.xOffset)
                HealthScanner[0].X -= localConfig.xOffset;
            if (HealthScanner[0].Y >= localConfig.yOffset)
                HealthScanner[0].Y -= localConfig.yOffset;
            if (HealthScanner[1].X >= localConfig.xOffset)
                HealthScanner[1].X -= localConfig.xOffset;
            if (HealthScanner[1].Y >= localConfig.yOffset)
                HealthScanner[1].Y -= localConfig.yOffset;
            Bitmap chatAreaBitmap = Display.CropBitmap(gameImage, HealthScanner[0], HealthScanner[1]);
            int w = chatAreaBitmap.Width;
            int h = chatAreaBitmap.Height;

            Bitmap bigSc = Display.ResizeImage(chatAreaBitmap, chatAreaBitmap.Width * 3, chatAreaBitmap.Height * 3);
            Bitmap bigSc2 = Display.AdjustContrast(bigSc, 40);
            int[] health = { -1, -1 };
            try
            {
                health = parseHealth(bigSc2);
            }
            catch (Exception e)
            {
                if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() > Properties.Settings.Default.LastWarning + 30000)
                {
                    PluginAPI.WriteLine("EXCEPTION: " + e.Message);
                    Properties.Settings.Default.LastWarning = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    Properties.Settings.Default.Save();
                }
            }

            if (health[0] < minHealth && health[0] != -1)
            {
                PluginAPI.WarningWriteLine("Low health detected! (" + health[0] + "/" + health[1] + ")");
                PluginAPI.alert();

            }

            bigSc2.Dispose();
            bigSc.Dispose();
            chatAreaBitmap.Dispose();
        }

        public void Setup(Config cfg)
        {
            localConfig = cfg;
            PluginAPI.WriteLine("Configuration file loaded.");
        }

        private static int[] parseHealth(Bitmap healthScPostB)
        {
            int[] healthVals = new int[2];
            string textVals = PluginAPI.RemoveWhitespace(Display.GetText(healthScPostB));
            string[] t = textVals.Split('/');
            //PluginAPI.WriteLine("Health String: " + textVals);
            healthVals[0] = Convert.ToInt32(t[0]);
            healthVals[1] = Convert.ToInt32(t[1]);
            healthScPostB.Dispose();
            return healthVals;
        }
    }
}
