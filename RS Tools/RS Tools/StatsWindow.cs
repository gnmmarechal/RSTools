using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS_Tools
{
    public partial class StatsWindow : Form
    {
        public StatsWindow()
        {
            InitializeComponent();
        }

        private void warnTimer_Tick(object sender, EventArgs e)
        {

        }
        static void sleep(int ms)
        {
            System.Threading.Thread.Sleep(ms);
        }

        private void StatsWindow_Load(object sender, EventArgs e)
        {


        }


    }
}
