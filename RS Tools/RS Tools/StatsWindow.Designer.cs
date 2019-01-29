namespace RS_Tools
{
    partial class StatsWindow
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
            this.label1 = new System.Windows.Forms.Label();
            this.warnTimer = new System.Windows.Forms.Timer(this.components);
            this.healthValue = new System.Windows.Forms.Label();
            this.healthWarningCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Health:";
            // 
            // warnTimer
            // 
            this.warnTimer.Enabled = true;
            this.warnTimer.Tick += new System.EventHandler(this.warnTimer_Tick);
            // 
            // healthValue
            // 
            this.healthValue.AutoSize = true;
            this.healthValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.healthValue.ForeColor = System.Drawing.SystemColors.MenuHighlight;
            this.healthValue.Location = new System.Drawing.Point(86, 13);
            this.healthValue.Name = "healthValue";
            this.healthValue.Size = new System.Drawing.Size(15, 20);
            this.healthValue.TabIndex = 1;
            this.healthValue.Text = "-";
            // 
            // healthWarningCheckbox
            // 
            this.healthWarningCheckbox.AutoSize = true;
            this.healthWarningCheckbox.Location = new System.Drawing.Point(13, 310);
            this.healthWarningCheckbox.Name = "healthWarningCheckbox";
            this.healthWarningCheckbox.Size = new System.Drawing.Size(118, 17);
            this.healthWarningCheckbox.TabIndex = 2;
            this.healthWarningCheckbox.Text = "Warn on low health";
            this.healthWarningCheckbox.UseVisualStyleBackColor = true;
            // 
            // StatsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(524, 339);
            this.Controls.Add(this.healthWarningCheckbox);
            this.Controls.Add(this.healthValue);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StatsWindow";
            this.Text = "StatsWindow";
            this.Load += new System.EventHandler(this.StatsWindow_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer warnTimer;
        public System.Windows.Forms.Label healthValue;
        public System.Windows.Forms.CheckBox healthWarningCheckbox;
    }
}