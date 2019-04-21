using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS_Tools
{
    public partial class PluginAPIOverlay : Form
    {

        public static Color transparent = Color.Fuchsia;
        private CustLabel logLabel = new CustLabel
        {
            Font = new Font("Arial", 14, FontStyle.Bold),
            AutoSize = true,
            ForeColor = Color.ForestGreen,
            Top = 0,
            Left = 0,
            TextRenderingHint = TextRenderingHint.SingleBitPerPixelGridFit
        };
        private int maxLogHeight = 5;
        public string gameWindowTitle = "";

        public PluginAPIOverlay()
        {
            InitializeComponent();
            //SetStyle(ControlStyles.Opaque, false);
        }

        private void PluginAPIOverlay_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.BackColor = transparent;
            this.TransparencyKey = transparent;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.taskTimer.Interval = 500;
            this.AddControl(logLabel);

            int initialStyle = Display.GetWindowLong(this.Handle, -20);
            Display.SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
        }

        public string AddControl(Control c)
        {
            this.Controls.Add(c);
            this.Refresh();
            return c.Name;
        }

        public static Control NewLabel(int x, int y, String text, String name, Font font, Color backColour, Color foreColour)
        {
            Label a = new Label
            {
                BackColor = backColour,
                ForeColor = foreColour,
                Name = new StackFrame(1).GetMethod().DeclaringType.Name + "_" + name,
                AutoSize = true,
                Visible = true
            };
            a.Show();
            a.Text = text;
            a.Font = font;
            a.Top = y;
            a.Left = x;

            

            return a;
        }

        public static Control NewLabelRandName(int x, int y, String text, Font font, Color backColour, Color foreColour)
        {
            Thread.Sleep(20);
            return NewLabel(x, y, text, "label_" + (new Random()).Next(999999), font, backColour, foreColour);
        }

        private void taskTimer_Tick(object sender, EventArgs e)
        {
            lock (RSTools._lockObj3)
            {
                IntPtr gameWindowHandle = Display.FindWindowByCaption(IntPtr.Zero, gameWindowTitle);
                //StandardLog("RS " + gameWindowHandle);
                IntPtr curHandle = Display.GetForegroundWindow();
                //StandardLog("CURHAN " + curHandle);

                if (curHandle == gameWindowHandle)
                {
                    //StandardLog("YAY");
                    this.TopMost = true;
                }
                else if (!RSTools._overlayIgnoreTopWindow)
                {
                    this.SendToBack();
                }

            }
            lock (RSTools._lockObj2)
            {
                while (RSTools.overlayLogQueue.Count > 0)
                {
                    StandardLog(RSTools.overlayLogQueue.Dequeue());
                }
                while (RSTools.controlAddQueue.Count > 0)
                {
                    Control element = RSTools.controlAddQueue.Dequeue();
                    //PluginAPI.WriteLine("Adding " + element.Name);
                    bool foundControl = false;

                    foreach (Control c in this.Controls)
                    {
                        if (c.Name.Equals(element.Name))
                        {
                            // Only copies some elements, PluginAPI.CopyProperties can copy everything, but it causes blinking.
                            foundControl = true;
                            c.Text = element.Text;
                            c.Top = element.Top;
                            c.Left = element.Left;
                            c.BackColor = element.BackColor;
                            c.ForeColor = element.ForeColor;
                            c.Font = element.Font;
                        }
                    }
                    if (!foundControl)
                    {
                        element.BringToFront();
                        this.AddControl(element);
                    }
                    this.Refresh();
                }

                while (RSTools.controlRemoveQueue.Count > 0)
                {
                    String curName = RSTools.controlRemoveQueue.Dequeue();
                    foreach (Control c in this.Controls)
                    {
                        if (c.Name.Equals(curName))
                        {
                            this.Controls.Remove(c);
                            break;
                        }
                    }
                    this.Refresh();
                }

                
            }
        }

        private void StandardLog(string text)
        {
            logLabel.Text += text;
            int currentLogLines = logLabel.Text.Split('\n').Length;

            if (currentLogLines > maxLogHeight)
            {
                logLabel.Text = RemoveFirstLine(logLabel.Text);
            }

            logLabel.Text += "\n";
            this.Refresh();
        }

        private string RemoveFirstLine(string text)
        {
            return text.Substring(text.IndexOf('\n') + 1);
        }

        internal void ClearLog()
        {
            logLabel.Text = "";
        }
    }
}
