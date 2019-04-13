﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS_Tools;

namespace LobbyPause
{
    public class LobbyPause : RSToolsPlugin
    {
        private Config localConfig;

        public string PluginName
        {
            get
            {
                return "Lobby Pause";
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Pauses RS Tools when the \"Play Now\" button is detected";
            }
        }

        public string PluginPackage
        {
            get
            {
                return "lobbypause";
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
            Display.POINT[] PlayButtonArea = PluginAPI.GetRectangle(Convert.ToInt32(set[0]), Convert.ToInt32(set[1]), Convert.ToInt32(set[2]), Convert.ToInt32(set[3]));
            String filePath = set[4];
            // Offset correction
            if (PlayButtonArea[0].X >= localConfig.xOffset)
                PlayButtonArea[0].X -= localConfig.xOffset;
            if (PlayButtonArea[0].Y >= localConfig.yOffset)
                PlayButtonArea[0].Y -= localConfig.yOffset;
            if (PlayButtonArea[1].X >= localConfig.xOffset)
                PlayButtonArea[1].X -= localConfig.xOffset;
            if (PlayButtonArea[1].Y >= localConfig.yOffset)
                PlayButtonArea[1].Y -= localConfig.yOffset;
            Bitmap playBitmap = Display.CropBitmap(gameImage, PlayButtonArea[0], PlayButtonArea[1]);
            Bitmap ogPlayBitmap = new Bitmap(filePath);
            double similarity = Display.getBitmapSimilarity(playBitmap, ogPlayBitmap);
            if (similarity > 0.8)
            {
                PluginAPI.WriteLine("Lobby detected. RS Tools paused.");
                PluginAPI.alert();
                Console.ReadKey();
            }
            ogPlayBitmap.Dispose();
            playBitmap.Dispose();
        }

        public void Setup(Config cfg)
        {
            localConfig = cfg;
            PluginAPI.WriteLine("Configuration file loaded.");
        }

    }
}
