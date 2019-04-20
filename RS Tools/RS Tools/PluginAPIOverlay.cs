using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS_Tools
{
    public partial class PluginAPIOverlay : Form
    {

        public static Color transparent = Color.BlanchedAlmond;

        public PluginAPIOverlay()
        {
            InitializeComponent();
        }

        private void PluginAPIOverlay_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            this.BackColor = transparent;
            this.TransparencyKey = transparent;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;

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
                Name = name,
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
            return NewLabel(x, y, text, "label_" + (new Random()).Next(999999), font, backColour, foreColour);
        }
    }
}
