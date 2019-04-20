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
        public PluginAPIOverlay overlayForm = null;
        public int[] gameResolution = { 1920, 1080 };
        public int xOffset = 0;
        public int yOffset = 0;
        private bool bootSetFlag = false;
        private long bootTime = 0L;

        public String gameWindowName = null;

        private Dictionary<String, String> pluginSettings;

        public Config(String fileName)
        {
            List<String> fileContents = new List<String>( File.ReadAllLines(fileName));
            pluginSettings = new Dictionary<string, string>();
            bool foundWindowName = false;

            foreach (String line in fileContents)
            {
                String[] splitLine = line.Split(' ');
                String reLine = "";
                for (int i = 1; i < splitLine.Length; i++)
                {
                    reLine += splitLine[i] + " ";
                }
                reLine.Substring(reLine.Length - 1);
                if (splitLine[0].Equals("GameWindow"))
                {
                    gameWindowName = string.Join("", splitLine.Skip(1).ToArray());
                    PluginAPI.WriteLine("Game Window Name: " + gameWindowName);

                    try
                    {
                        Display.RECT rect;
                        IntPtr handle = Display.FindWindow(null, gameWindowName);
                        Display.GetWindowRect(handle, out rect);
                        gameResolution[0] = rect.right - rect.left;
                        gameResolution[1] = rect.bottom - rect.top;
                        if (gameResolution[0] == 0 || gameResolution[1] == 0)
                            throw new Exception();
                        PluginAPI.WriteLine("Window Resolution: " + gameResolution[0] + "x" + gameResolution[1]);

                        xOffset = rect.left;
                        yOffset = rect.top;
                        PluginAPI.WriteLine("Window Offsets: X:" + xOffset + " Y:" + yOffset);

                        foundWindowName = true;

                    } catch (Exception)
                    {
                        PluginAPI.WriteLine("Couldn't get data from window!");
                    }
                }
                else if (splitLine[0].Equals("Resolution") && !foundWindowName)
                {
                    PluginAPI.WriteLine("Window Resolution: " + splitLine[1] + "x" + splitLine[2]);
                    gameResolution[0] = Convert.ToInt32(splitLine[1]);
                    gameResolution[1] = Convert.ToInt32(splitLine[2]);
                }
                else if (splitLine[0].Equals("WindowOffsets") && !foundWindowName)
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

        public long GetBootTime()
        {
            return bootTime;
        }

        public bool SetBootTime(long time)
        {
            if (!bootSetFlag)
            {
                bootTime = time;
                bootSetFlag = true;
                return true;
            }
            return false;
        }
    }
}
