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
        public bool overlayIgnoreTopWindow = false;

        private readonly int defaultLogSize = 5;
        public int overlayLogSize = 5;

        private readonly int defaultLogTime = 10000;
        public int overlayLogTime = 10000;

        public String gameWindowName = null;

        public bool useNetworkOCR = false;
        public string networkOCRIP = "";
        public int networkOCRPort = 0;

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
                if (reLine.Length > 0)
                    reLine.Substring(reLine.Length - 1);
                if (splitLine[0].Equals("UseNetworkOCR"))
                {
                    try
                    {
                        networkOCRIP = splitLine[1];
                        networkOCRPort = Convert.ToInt32(splitLine[2]);
                        PluginAPI.WriteLine("Network OCR Enabled: " + networkOCRIP + ":" + networkOCRPort);
                        useNetworkOCR = true;
                    }
                    catch(Exception)
                    {
                        PluginAPI.WriteLine("Error reading Network OCR settings. Using integrated OCR.");
                        useNetworkOCR = false;
                    }
                }
                else if (splitLine[0].Equals("OverlayLogMaxTime"))
                {
                    try
                    {
                        overlayLogTime = Convert.ToInt32(splitLine[1]);
                        PluginAPI.WriteLine("Overlay Max Log Line Time (ms): " + overlayLogTime);
                    }
                    catch (Exception)
                    {
                        PluginAPI.WriteLine("Error reading overlay max log time. Using default size.");
                        overlayLogTime = defaultLogTime;
                    }
                }
                else if (splitLine[0].Equals("OverlayLogMaxSize"))
                {
                    try
                    {
                        overlayLogSize = Convert.ToInt32(splitLine[1]);
                        PluginAPI.WriteLine("Overlay Max Log Height: " + overlayLogSize);
                    }
                    catch (Exception)
                    {
                        PluginAPI.WriteLine("Error reading overlay max log size. Using default size.");
                        overlayLogSize = defaultLogSize;
                    }
                }
                else if (splitLine[0].Equals("OverlayIgnoreTopWindow"))
                {
                    overlayIgnoreTopWindow = true;
                    PluginAPI.WriteLine("Ignoring top window for overlay!");
                }
                else if (splitLine[0].Equals("GameWindow"))
                {
                    gameWindowName = string.Join("", splitLine.Skip(1).ToArray());
                    PluginAPI.WriteLine("Game Window Name: " + gameWindowName);

                    try
                    {
                        Win32.RECT rect;
                        IntPtr handle = Win32.FindWindow(null, gameWindowName);
                        Win32.GetWindowRect(handle, out rect);
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
