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
        public PluginAPIOverlay()
        {
            InitializeComponent();
        }

        private void PluginAPIOverlay_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.BlanchedAlmond;
            this.TransparencyKey = Color.BlanchedAlmond;
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;

            int initialStyle = Display.GetWindowLong(this.Handle, -20);
            Display.SetWindowLong(this.Handle, -20, initialStyle | 0x80000 | 0x20);
        }
    }
}
