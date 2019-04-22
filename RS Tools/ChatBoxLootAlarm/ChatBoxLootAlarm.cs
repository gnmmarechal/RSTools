using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS_Tools;

namespace ChatBoxLootAlarm
{
    public class ChatBoxLootAlarm : RSToolsPlugin
    {
        private Config localConfig;
        private static readonly string[] matchingTerms = {"shines", "loot", "receive", "eceive", "You receive", "ring of fortune", "ring of wealth", "beam" };
        private TextMatcher chatMatcher;
        private Display.POINT[] ChatScanner;
        private long lastWarningTime = 0L;
        private int warningInterval = 1000;

        public string PluginName
        {
            get
            {
                return "Chat Box Loot Alarm";
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Plays a sound when the loot keywords are detected in the chat box";
            }
        }

        public string PluginPackage
        {
            get
            {
                return "cboxlootalarm";
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

            Bitmap chatAreaBitmap = Display.CropBitmap(gameImage, ChatScanner[0], ChatScanner[1]);

            Bitmap bigSc = Display.ResizeImage(chatAreaBitmap, chatAreaBitmap.Width * 5, chatAreaBitmap.Height * 5);
            Bitmap bigSc2 = Display.AdjustContrast(bigSc, 40);

            String txt = "";
            try
            {
                txt = Display.GetText(bigSc2);
            }
            catch (Exception e)
            {
                PluginAPI.WriteLine("EXCEPTION: " + e.Message);
            }

            if (chatMatcher.certainMatch(txt) && DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() > lastWarningTime + warningInterval)
            {
                PluginAPI.SuccessWriteLine("Text match found!");
                RSTools.OverlayStandardLog("Task completion text match found!");
                PluginAPI.alert();
                lastWarningTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            bigSc2.Dispose();
            bigSc.Dispose();
            chatAreaBitmap.Dispose();
        }

        public void Setup(Config cfg)
        {
            localConfig = cfg;
            PluginAPI.WriteLine("Configuration file loaded.");
            chatMatcher = new TextMatcher(matchingTerms);
            PluginAPI.WriteLine("Parsing settings...");
            // Parse Settings
            String settings = localConfig.GetSettings();

            String[] set = settings.Split(' ');
            ChatScanner = PluginAPI.GetRectangle(Convert.ToInt32(set[0]), Convert.ToInt32(set[1]), Convert.ToInt32(set[2]), Convert.ToInt32(set[3]));

            PluginAPI.WriteLine("Text matcher initialised.");
        }
    }
}
