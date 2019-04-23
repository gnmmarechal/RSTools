using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    public class PluginLoader
    {
        public static List<IRSToolsPluginBase> Plugins { get; set; }

        public void loadPlugins()
        {
            Plugins = new List<IRSToolsPluginBase>();


            if (Directory.Exists(PluginConstants.PluginFolder))
            {
                String[] files = Directory.GetFiles(PluginConstants.PluginFolder);

                foreach (String file in files)
                {
                    if (file.EndsWith(".dll"))
                    {
                        Assembly.LoadFile(Path.GetFullPath(file));
                    }
                }
            }

            Type interfaceType = typeof(IRSToolsPluginBase);
            Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .Where(p => interfaceType.IsAssignableFrom(p) && p.IsClass)
                .ToArray();

            foreach (Type type in types)
                Plugins.Add((IRSToolsPluginBase)Activator.CreateInstance(type));
        }
    }
}
