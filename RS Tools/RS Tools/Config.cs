using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RS_Tools
{
    public class Config
    {
        public int[] gameResolution = { 1920, 1080 };
        public int xOffset = 0;
        public int yOffset = 0;

        private Dictionary<String, String> pluginSettings;

        public Config(String fileName)
        {
            List<String> fileContents = new List<String>( File.ReadAllLines(fileName));
            pluginSettings = new Dictionary<string, string>();


            foreach (String line in fileContents)
            {
                String[] splitLine = line.Split(' ');
                String reLine = "";
                for (int i = 1; i < splitLine.Length; i++)
                {
                    reLine += splitLine[i] + " ";
                }
                reLine.Substring(reLine.Length - 1);
                if (splitLine[0].Equals("Resolution"))
                {
                    PluginAPI.WriteLine("Window Resolution: " + splitLine[1] + "x" + splitLine[2]);
                    gameResolution[0] = Convert.ToInt32(splitLine[1]);
                    gameResolution[1] = Convert.ToInt32(splitLine[2]);
                }
                else if (splitLine[0].Equals("WindowOffsets"))
                {
                    PluginAPI.WriteLine("Window Offsets: X:" + splitLine[1] + " Y:" + splitLine[2]);
                    xOffset = Convert.ToInt32(splitLine[1]);
                    yOffset = Convert.ToInt32(splitLine[2]);
                }
                else if (line.StartsWith("//") || line.StartsWith("#"))
                {
                    // Comment line, do nothing.
                }
                else
                {
                    pluginSettings.Add(splitLine[0], reLine);
                    PluginAPI.WriteLine("Found config: " + splitLine[0] + " - " + reLine);
                }


            }

        }

        public String GetSettings()
        {
            String pluginName = new StackFrame(1).GetMethod().DeclaringType.Name;
            String str = pluginSettings[pluginName];

            return str;
        }

    }
}
