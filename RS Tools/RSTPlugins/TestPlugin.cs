using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RS_Tools;
namespace RSTPlugins
{
    public class TestPlugin : RSToolsPlugin
    {
        public string PluginName
        {
            get
            {
                return "Test Plugin";
            }
        }

        public string PluginDescription
        {
            get
            {
                return "Test plugin for RS Tools";
            }
        }

        public string PluginPackage => throw new NotImplementedException();

        public int PluginVersion => throw new NotImplementedException();

        public void Run(System.Drawing.Bitmap gameImage, string settings)
        {
            throw new NotImplementedException();
        }

        public void Run(System.Drawing.Bitmap gameImage)
        {
            throw new NotImplementedException();
        }

        public void Run(System.Drawing.Bitmap gameImage, string settings, ref string communication)
        {
            throw new NotImplementedException();
        }

        public void Setup(string data)
        {
            PluginAPI.WriteLine("Hello from a plugin");
        }

        public void Setup(Config cfg)
        {
            throw new NotImplementedException();
        }
    }
}