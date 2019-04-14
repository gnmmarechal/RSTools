﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS_Tools;

namespace EmptyInventoryAlert
{
    public class EmptyInventoryAlert : RSToolsPlugin
    {
        private Config localConfig;

        public string PluginName
        {
            get
            {
                return "Empty Inventory Alarm";
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Plays a sound when the inventory is empty";
            }
        }

        public string PluginPackage
        {
            get
            {
                return "emptyinvalarm";
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
            Display.POINT[] InventoryScanner = PluginAPI.GetRectangle(Convert.ToInt32(set[0]), Convert.ToInt32(set[1]), Convert.ToInt32(set[2]), Convert.ToInt32(set[3]));
            String filePath = set[4];
            // Offset correction
            if (InventoryScanner[0].X >= localConfig.xOffset)
                InventoryScanner[0].X -= localConfig.xOffset;
            if (InventoryScanner[0].Y >= localConfig.yOffset)
                InventoryScanner[0].Y -= localConfig.yOffset;
            if (InventoryScanner[1].X >= localConfig.xOffset)
                InventoryScanner[1].X -= localConfig.xOffset;
            if (InventoryScanner[1].Y >= localConfig.yOffset)
                InventoryScanner[1].Y -= localConfig.yOffset;

            if  (DateTimeOffset.UtcNow.ToUnixTimeMilliseconds() > Properties.Settings.Default.LastWarning + 5000)
            {
                
                Bitmap InventoryBitmap = Display.CropBitmap(gameImage, InventoryScanner[0], InventoryScanner[1]);
                Bitmap ogInventoryBitmap = new Bitmap(filePath);
                double similarity = Display.getBitmapSimilarity(InventoryBitmap, ogInventoryBitmap);
                if (similarity == 1.0)
                {
                    PluginAPI.WriteLine("Your inventory is empty!");
                    PluginAPI.alert();

                    Properties.Settings.Default.LastWarning = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                    Properties.Settings.Default.Save();
                }

                InventoryBitmap.Dispose();
                ogInventoryBitmap.Dispose();
            }
        }

        public void Setup(Config cfg)
        {
            localConfig = cfg;
            PluginAPI.WriteLine("Configuration file loaded.");
        }
    }
}