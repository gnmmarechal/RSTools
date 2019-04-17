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
        private Display.POINT[] ExpScanner, LastInvScanner;
        private int minXp = 99999999;

        private String[] fileLocation = new string[6];

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
                return "Plays a sound when the Divination experience received in the Empty Throne falls below a certain level and when the current inventory is finished";
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
                return 2;
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

            // Detect if inventory is done.
            Bitmap lastInvArea = Display.CropBitmap(gameImage, LastInvScanner[0], LastInvScanner[1]);
            Bitmap crystal_lastInvArea = new Bitmap(fileLocation[0]);
            Bitmap empty_lastInvArea = new Bitmap(fileLocation[1]);
            List<Bitmap> processedCrystals_lastInvArea = new List<Bitmap>();

            for (int i = 2; i < fileLocation.Length; i++)
            {
                processedCrystals_lastInvArea.Add(new Bitmap(fileLocation[i]));
            }

            double similarity = Display.getBitmapSimilarity(lastInvArea, empty_lastInvArea);

            if (similarity != 1)
            {
                similarity = Display.getBitmapSimilarity(lastInvArea, crystal_lastInvArea);
                double secondSimilarity = 0;

                foreach (Bitmap selCrystal in processedCrystals_lastInvArea)
                {
                    secondSimilarity = Math.Max(secondSimilarity, Display.getBitmapSimilarity(lastInvArea, selCrystal));
                }

                if (similarity != 1 && secondSimilarity == 1)
                {
                    PluginAPI.WriteLine("Inventory finished!");
                    PluginAPI.alert();
                }
            }

            crystal_lastInvArea.Dispose();
            empty_lastInvArea.Dispose();
            foreach (Bitmap b in processedCrystals_lastInvArea)
            {
                b.Dispose();
            }
            lastInvArea.Dispose();
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
            LastInvScanner = PluginAPI.GetRectangle(Convert.ToInt32(set[5]), Convert.ToInt32(set[6]), Convert.ToInt32(set[7]), Convert.ToInt32(set[8]));
            fileLocation[0] = set[9];
            fileLocation[1] = set[10];

            fileLocation[2] = set[11];
            fileLocation[3] = set[12];
            fileLocation[4] = set[13];
            fileLocation[5] = set[14];

        }
    }
}
