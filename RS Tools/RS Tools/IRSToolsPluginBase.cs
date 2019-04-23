using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    public interface IRSToolsPluginBase
    {
        string PluginName { get; }
        string PluginPackage { get; }
        int PluginVersion { get; }
        string PluginDescription { get; }
        void Setup(Config cfg);
    }
}
