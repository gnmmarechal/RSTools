using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    public interface RSToolsPlugin
    {
        string PluginName { get; }
        string PluginPackage { get; }
        int PluginVersion { get;  }
        string PluginDescription { get; }
        void Setup(String data);
        void Setup(Config cfg);
        void Run(in Bitmap gameImage, String settings);
        void Run(in Bitmap gameImage);
        void Run(in Bitmap gameImage, String settings, ref String communication);
    }
}
