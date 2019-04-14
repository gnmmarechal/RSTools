using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using RS_Tools;

namespace EmptyThroneRoomDivinationAlarm
{
    public class EmptyThroneRoomDivinationAlarm : RSToolsPlugin
    {
        private Config localConfig;
        private Display.POINT[] ExpScanner;
        private int minXp = 99999999;

        public string PluginName
        {
            get
            {
                return "Empty Throne Room Divination Alarm";
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Plays a sound when the Divination experience received in the Empty Throne falls below a certain level";
            }
        }

        public string PluginPackage
        {
            get
            {
                return "emptythronedivalarm";
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

            Bitmap expAreaBitmap = Display.CropBitmap(gameImage, ExpScanner[0], ExpScanner[1]);

            Bitmap bigSc = Display.ResizeImage(expAreaBitmap, expAreaBitmap.Width * 5, expAreaBitmap.Height * 5);
            Bitmap bigSc2 = Display.AdjustContrast(bigSc, 40);

            String txt = "";

            // REGEX \+([0-9])* xp
            try
            {
                txt = Display.GetText(bigSc2);
            }
            catch (Exception e)
            {
                PluginAPI.WriteLine("EXCEPTION: " + e.Message);
            }
            int xp = 99999999;
            if (Regex.IsMatch(txt, "\\+([0-9])* xp"))
            {
                char[] tempArr = txt.ToCharArray();
                String newString = "";
                for (int i = 0; i < tempArr.Length; i++)
                {
                    if (Char.IsDigit(tempArr[i]))
                    {
                        newString += tempArr[i];
                    }
                    else if (tempArr[i] == ' ')
                    {
                        break;
                    }
                }

                try
                {
                    xp = Convert.ToInt32(newString);
                }
                catch (Exception)
                {
                    PluginAPI.WriteLine("EXCEPTION: String matches regex but xp can't be parsed.");
                }

                Properties.Settings.Default.LastXpStringTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                Properties.Settings.Default.Save();
            }
            else if (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() > Properties.Settings.Default.LastXpStringTime + 60000 && localConfig.GetBootTime() < Properties.Settings.Default.LastXpStringTime)
            {
                PluginAPI.WriteLine("Warning: Last detected XP string over 60 seconds ago.");
            }


            if (xp < minXp)
            {
                PluginAPI.WriteLine("XP gain below set level! XP:" + xp);
                PluginAPI.alert();
            }


            bigSc2.Dispose();
            bigSc.Dispose();
            expAreaBitmap.Dispose();
        }

        public void Setup(Config cfg)
        {
            localConfig = cfg;
            PluginAPI.WriteLine("Configuration file loaded.");

            PluginAPI.WriteLine("Parsing settings...");

            // Parse Settings
            String settings = localConfig.GetSettings();

            String[] set = settings.Split(' ');
            ExpScanner = PluginAPI.GetRectangle(Convert.ToInt32(set[0]), Convert.ToInt32(set[1]), Convert.ToInt32(set[2]), Convert.ToInt32(set[3]));
            minXp = Convert.ToInt32(set[4]);
            // Offset correction
            if (ExpScanner[0].X >= localConfig.xOffset)
                ExpScanner[0].X -= localConfig.xOffset;
            if (ExpScanner[0].Y >= localConfig.yOffset)
                ExpScanner[0].Y -= localConfig.yOffset;
            if (ExpScanner[1].X >= localConfig.xOffset)
                ExpScanner[1].X -= localConfig.xOffset;
            if (ExpScanner[1].Y >= localConfig.yOffset)
                ExpScanner[1].Y -= localConfig.yOffset;
        }
    }
}
