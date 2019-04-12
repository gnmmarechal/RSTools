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
        private static string[] matchingTerms = { "tasks in a row", "shines", "loot", "Return to a Slayer Master", "receive", "eceive", "You receive", "ring of fortune", "ring of wealth", "beam" };
        private TextMatcher chatMatcher;

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
                return "Test plugin for RS Tools";
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
                return 1;
            }
        }

        public void Run(System.Drawing.Bitmap gameImage, string settings)
        {
            throw new NotImplementedException();
        }

        public void Run(System.Drawing.Bitmap gameImage)
        {
            // Offset correction
            if (localConfig.ChatScanner[0].X >= localConfig.xOffset)
                localConfig.ChatScanner[0].X -= localConfig.xOffset;
            if (localConfig.ChatScanner[0].Y >= localConfig.yOffset)
                localConfig.ChatScanner[0].Y -= localConfig.yOffset;
            if (localConfig.ChatScanner[1].X >= localConfig.xOffset)
                localConfig.ChatScanner[1].X -= localConfig.xOffset;
            if (localConfig.ChatScanner[1].Y >= localConfig.yOffset)
                localConfig.ChatScanner[1].Y -= localConfig.yOffset;
            Bitmap chatAreaBitmap = Display.CropBitmap(gameImage, localConfig.ChatScanner[0], localConfig.ChatScanner[1]);
            int w = chatAreaBitmap.Width;
            int h = chatAreaBitmap.Height;

            Bitmap bigSc = Display.ResizeImage(chatAreaBitmap, chatAreaBitmap.Width * 5, chatAreaBitmap.Height * 5);
            Bitmap bigSc2 = Display.AdjustContrast(bigSc, 40);

            //PluginAPI.WriteLine("Chat BMP: " + w + "x" + h);
            String txt = "";
            try
            {
                txt = Display.GetText(bigSc2);
            }
            catch (Exception e)
            {
                PluginAPI.WriteLine("EXCEPTION: " + e.Message);
            }
            PluginAPI.WriteLine("\n" + txt);

            if (chatMatcher.certainMatch(txt))
            {
                PluginAPI.WriteLine("Chat BMP: " + w + "x" + h);
                PluginAPI.SuccessWriteLine("Text match found!");
                PluginAPI.alert();
                Console.ReadLine();
            }

            bigSc2.Dispose();
            bigSc.Dispose();
            chatAreaBitmap.Dispose();
        }

        public void Run(System.Drawing.Bitmap gameImage, string settings, ref string communication)
        {
            throw new NotImplementedException();
        }

        public void Setup(string data)
        {
            PluginAPI.WriteLine("This plugin must be configured with Setup( Config ).");
            throw new NotImplementedException();
        }

        public void Setup(Config cfg)
        {
            localConfig = cfg;
            PluginAPI.WriteLine("Configuration file loaded.");
            chatMatcher = new TextMatcher(matchingTerms);
            PluginAPI.WriteLine("Text matcher initialised.");
        }
    }
}
