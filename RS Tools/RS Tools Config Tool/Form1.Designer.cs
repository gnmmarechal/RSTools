namespace RS_Tools_Config_Tool
{
    partial class mainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.topMenuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xValueTextBox = new System.Windows.Forms.TextBox();
            this.chatScannerPanel = new System.Windows.Forms.Panel();
            this.chatScannerY2TextBox = new System.Windows.Forms.TextBox();
            this.chatScannerX2TextBox = new System.Windows.Forms.TextBox();
            this.titleLabel = new System.Windows.Forms.Label();
            this.yLabel = new System.Windows.Forms.Label();
            this.xLabel = new System.Windows.Forms.Label();
            this.yValueTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.healthY2TextBox = new System.Windows.Forms.TextBox();
            this.healthX2TextBox = new System.Windows.Forms.TextBox();
            this.healthScannerLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.healthY1TextBox = new System.Windows.Forms.TextBox();
            this.healthX1TextBox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.currentYValTextBox = new System.Windows.Forms.TextBox();
            this.currentXValTextBox = new System.Windows.Forms.TextBox();
            this.coordinateLockTimer = new System.Windows.Forms.Timer(this.components);
            this.topMenuStrip.SuspendLayout();
            this.chatScannerPanel.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // topMenuStrip
            // 
            this.topMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.topMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.topMenuStrip.Name = "topMenuStrip";
            this.topMenuStrip.Size = new System.Drawing.Size(744, 24);
            this.topMenuStrip.TabIndex = 0;
            this.topMenuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem,
            this.openToolStripMenuItem,
            this.saveToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // newToolStripMenuItem
            // 
            this.newToolStripMenuItem.Name = "newToolStripMenuItem";
            this.newToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.newToolStripMenuItem.Text = "New";
            this.newToolStripMenuItem.Click += new System.EventHandler(this.newToolStripMenuItem_Click);
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.saveToolStripMenuItem.Text = "Save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // xValueTextBox
            // 
            this.xValueTextBox.Location = new System.Drawing.Point(40, 23);
            this.xValueTextBox.Name = "xValueTextBox";
            this.xValueTextBox.Size = new System.Drawing.Size(52, 20);
            this.xValueTextBox.TabIndex = 1;
            // 
            // chatScannerPanel
            // 
            this.chatScannerPanel.Controls.Add(this.chatScannerY2TextBox);
            this.chatScannerPanel.Controls.Add(this.chatScannerX2TextBox);
            this.chatScannerPanel.Controls.Add(this.titleLabel);
            this.chatScannerPanel.Controls.Add(this.yLabel);
            this.chatScannerPanel.Controls.Add(this.xLabel);
            this.chatScannerPanel.Controls.Add(this.yValueTextBox);
            this.chatScannerPanel.Controls.Add(this.xValueTextBox);
            this.chatScannerPanel.Location = new System.Drawing.Point(3, 3);
            this.chatScannerPanel.Name = "chatScannerPanel";
            this.chatScannerPanel.Size = new System.Drawing.Size(162, 93);
            this.chatScannerPanel.TabIndex = 2;
            // 
            // chatScannerY2TextBox
            // 
            this.chatScannerY2TextBox.Location = new System.Drawing.Point(98, 59);
            this.chatScannerY2TextBox.Name = "chatScannerY2TextBox";
            this.chatScannerY2TextBox.Size = new System.Drawing.Size(52, 20);
            this.chatScannerY2TextBox.TabIndex = 7;
            // 
            // chatScannerX2TextBox
            // 
            this.chatScannerX2TextBox.Location = new System.Drawing.Point(98, 23);
            this.chatScannerX2TextBox.Name = "chatScannerX2TextBox";
            this.chatScannerX2TextBox.Size = new System.Drawing.Size(52, 20);
            this.chatScannerX2TextBox.TabIndex = 6;
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleLabel.Location = new System.Drawing.Point(13, 4);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(100, 16);
            this.titleLabel.TabIndex = 5;
            this.titleLabel.Text = "Chat Scanner";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = true;
            this.yLabel.Location = new System.Drawing.Point(17, 59);
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(17, 13);
            this.yLabel.TabIndex = 4;
            this.yLabel.Text = "Y:";
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = true;
            this.xLabel.Location = new System.Drawing.Point(17, 26);
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(17, 13);
            this.xLabel.TabIndex = 3;
            this.xLabel.Text = "X:";
            // 
            // yValueTextBox
            // 
            this.yValueTextBox.Location = new System.Drawing.Point(40, 59);
            this.yValueTextBox.Name = "yValueTextBox";
            this.yValueTextBox.Size = new System.Drawing.Size(52, 20);
            this.yValueTextBox.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.chatScannerPanel);
            this.flowLayoutPanel1.Controls.Add(this.panel1);
            this.flowLayoutPanel1.Controls.Add(this.panel2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(744, 462);
            this.flowLayoutPanel1.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.healthY2TextBox);
            this.panel1.Controls.Add(this.healthX2TextBox);
            this.panel1.Controls.Add(this.healthScannerLabel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.healthY1TextBox);
            this.panel1.Controls.Add(this.healthX1TextBox);
            this.panel1.Location = new System.Drawing.Point(171, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 93);
            this.panel1.TabIndex = 8;
            // 
            // healthY2TextBox
            // 
            this.healthY2TextBox.Location = new System.Drawing.Point(98, 59);
            this.healthY2TextBox.Name = "healthY2TextBox";
            this.healthY2TextBox.Size = new System.Drawing.Size(52, 20);
            this.healthY2TextBox.TabIndex = 7;
            // 
            // healthX2TextBox
            // 
            this.healthX2TextBox.Location = new System.Drawing.Point(98, 23);
            this.healthX2TextBox.Name = "healthX2TextBox";
            this.healthX2TextBox.Size = new System.Drawing.Size(52, 20);
            this.healthX2TextBox.TabIndex = 6;
            // 
            // healthScannerLabel
            // 
            this.healthScannerLabel.AutoSize = true;
            this.healthScannerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.healthScannerLabel.Location = new System.Drawing.Point(13, 4);
            this.healthScannerLabel.Name = "healthScannerLabel";
            this.healthScannerLabel.Size = new System.Drawing.Size(114, 16);
            this.healthScannerLabel.TabIndex = 5;
            this.healthScannerLabel.Text = "Health Scanner";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 26);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "X:";
            // 
            // healthY1TextBox
            // 
            this.healthY1TextBox.Location = new System.Drawing.Point(40, 59);
            this.healthY1TextBox.Name = "healthY1TextBox";
            this.healthY1TextBox.Size = new System.Drawing.Size(52, 20);
            this.healthY1TextBox.TabIndex = 2;
            // 
            // healthX1TextBox
            // 
            this.healthX1TextBox.Location = new System.Drawing.Point(40, 23);
            this.healthX1TextBox.Name = "healthX1TextBox";
            this.healthX1TextBox.Size = new System.Drawing.Size(52, 20);
            this.healthX1TextBox.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.currentYValTextBox);
            this.panel2.Controls.Add(this.currentXValTextBox);
            this.panel2.Location = new System.Drawing.Point(339, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(162, 93);
            this.panel2.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(13, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(145, 16);
            this.label4.TabIndex = 5;
            this.label4.Text = "Current Coordinates";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(17, 59);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Y:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(17, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(17, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "X:";
            // 
            // currentYValTextBox
            // 
            this.currentYValTextBox.Location = new System.Drawing.Point(40, 59);
            this.currentYValTextBox.Name = "currentYValTextBox";
            this.currentYValTextBox.Size = new System.Drawing.Size(88, 20);
            this.currentYValTextBox.TabIndex = 2;
            // 
            // currentXValTextBox
            // 
            this.currentXValTextBox.Location = new System.Drawing.Point(40, 23);
            this.currentXValTextBox.Name = "currentXValTextBox";
            this.currentXValTextBox.Size = new System.Drawing.Size(88, 20);
            this.currentXValTextBox.TabIndex = 1;
            // 
            // coordinateLockTimer
            // 
            this.coordinateLockTimer.Enabled = true;
            this.coordinateLockTimer.Tick += new System.EventHandler(this.coordinateLockTimer_Tick);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 486);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.topMenuStrip);
            this.KeyPreview = true;
            this.MainMenuStrip = this.topMenuStrip;
            this.Name = "mainForm";
            this.Text = "RS Tools Config Creation Utility";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.topMenuStrip.ResumeLayout(false);
            this.topMenuStrip.PerformLayout();
            this.chatScannerPanel.ResumeLayout(false);
            this.chatScannerPanel.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip topMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.TextBox xValueTextBox;
        private System.Windows.Forms.Panel chatScannerPanel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label yLabel;
        private System.Windows.Forms.Label xLabel;
        private System.Windows.Forms.TextBox yValueTextBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox currentYValTextBox;
        private System.Windows.Forms.TextBox currentXValTextBox;
        private System.Windows.Forms.Timer coordinateLockTimer;
        private System.Windows.Forms.TextBox chatScannerY2TextBox;
        private System.Windows.Forms.TextBox chatScannerX2TextBox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox healthY2TextBox;
        private System.Windows.Forms.TextBox healthX2TextBox;
        private System.Windows.Forms.Label healthScannerLabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox healthY1TextBox;
        private System.Windows.Forms.TextBox healthX1TextBox;
    }
}

