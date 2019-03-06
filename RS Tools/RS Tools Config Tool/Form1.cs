using RS_Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RS_Tools_Config_Tool
{
    public partial class mainForm : Form
    {
        static bool isLPressed;
        public mainForm()
        {
            InitializeComponent();

            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(mainForm_KeyDown);
            this.KeyUp += new KeyEventHandler(mainForm_KeyUp);
        }

        private void mainForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.L:
                    isLPressed = true;
                    break;

            }
        }

        private void mainForm_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.L:
                    isLPressed = false;
                    break;

            }
        }

        private void coordinateLockTimer_Tick(object sender, EventArgs e)
        {
            if (isLPressed)
            {
                RS_Tools.Display.POINT p;
                RS_Tools.Display.GetCursorPos(out p);
                currentXValTextBox.Text =  p.X.ToString();
                currentYValTextBox.Text = p.Y.ToString();
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            xValueTextBox.Text = "";
            yValueTextBox.Text = "";
            chatScannerX2TextBox.Text = "";
            chatScannerY2TextBox.Text = "";
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Display.POINT c1, c2;
            c1.X = Convert.ToInt32(xValueTextBox.Text);
            c1.Y = Convert.ToInt32(yValueTextBox.Text);
            c2.X = Convert.ToInt32(chatScannerX2TextBox.Text);
            c2.Y = Convert.ToInt32(chatScannerY2TextBox.Text);

            Display.POINT[] chatBox = { c1, c2};
            Config cfg = new Config(chatBox, chatBox, chatBox);
            System.IO.File.WriteAllText("config.cfg", cfg.ToString());
        }
    }
}
