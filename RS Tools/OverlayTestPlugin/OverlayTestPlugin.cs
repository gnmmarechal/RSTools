using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RS_Tools;

namespace OverlayTestPlugin
{
    public class OverlayTestPlugin : MiniRSToolsPlugin
    {
        private Config localConfig;
        private int i = 0;

        public string PluginName
        {
            get
            {
                return "Overlay Test Plugin";
            }
            
        }

        public string PluginPackage
        {
            get
            {
                return "overlaytest";
            }
        }

        public int PluginVersion
        {
            get
            {
                return 1;
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Made for testing purposes";
            }
        }

        public void Run()
        {
            
            Control a = PluginAPIOverlay.NewLabel(100, 200, "Overlay Test : " + i++, "test", new Font("Arial", 16), Color.Transparent, Color.White);

            RSTools.AddOverlayControl(a);
        }

        public void Setup(Config cfg)
        {
            localConfig = cfg;
            PluginAPI.WriteLine("Configuration file loaded.");
        }
    }
}
