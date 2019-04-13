using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS_Tools;

namespace SlayerTaskCompletionAlarm
{
    public class SlayerTaskCompletionAlarm : RSToolsPlugin
    {
        private Config localConfig;
        private static string[] matchingTerms = { "tasks in a row", "Return to a Slayer Master"};
        private TextMatcher chatMatcher;

        public string PluginName
        {
            get
            {
                return "Chat Box Slayer Task Completion Alarm";
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Plays a sound when the slayer task completion keywords are detected in the chat box";
            }
        }

        public string PluginPackage
        {
            get
            {
                return "slayercompalarm";
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
            Display.POINT[] ChatScanner = PluginAPI.GetRectangle(Convert.ToInt32(set[0]), Convert.ToInt32(set[1]), Convert.ToInt32(set[2]), Convert.ToInt32(set[3]));

            // Offset correction
            if (ChatScanner[0].X >= localConfig.xOffset)
                ChatScanner[0].X -= localConfig.xOffset;
            if (ChatScanner[0].Y >= localConfig.yOffset)
                ChatScanner[0].Y -= localConfig.yOffset;
            if (ChatScanner[1].X >= localConfig.xOffset)
                ChatScanner[1].X -= localConfig.xOffset;
            if (ChatScanner[1].Y >= localConfig.yOffset)
                ChatScanner[1].Y -= localConfig.yOffset;
            Bitmap chatAreaBitmap = Display.CropBitmap(gameImage, ChatScanner[0], ChatScanner[1]);
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
            //PluginAPI.WriteLine("\n" + txt);

            if (chatMatcher.certainMatch(txt))
            {
                PluginAPI.WriteLine("Chat BMP: " + w + "x" + h);
                PluginAPI.SuccessWriteLine("Text match found!");
                PluginAPI.alert();
                Console.ReadKey();
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
            PluginAPI.WriteLine("Text matcher initialised.");
        }
    }
}
