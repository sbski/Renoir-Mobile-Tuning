namespace renoir_tuning_utility
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel16 = new System.Windows.Forms.Panel();
            this.upDownMaxCurrentLimit = new System.Windows.Forms.NumericUpDown();
            this.panel17 = new System.Windows.Forms.Panel();
            this.checkMaxCurrentLimit = new System.Windows.Forms.CheckBox();
            this.panel14 = new System.Windows.Forms.Panel();
            this.upDownCurrentLimit = new System.Windows.Forms.NumericUpDown();
            this.panel15 = new System.Windows.Forms.Panel();
            this.checkCurrentLimit = new System.Windows.Forms.CheckBox();
            this.panel10 = new System.Windows.Forms.Panel();
            this.upDownTctlTemp = new System.Windows.Forms.NumericUpDown();
            this.panel11 = new System.Windows.Forms.Panel();
            this.checkTctlTemp = new System.Windows.Forms.CheckBox();
            this.panel12 = new System.Windows.Forms.Panel();
            this.upDownStapmTime = new System.Windows.Forms.NumericUpDown();
            this.panel13 = new System.Windows.Forms.Panel();
            this.checkStapmTime = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.upDownSlowTime = new System.Windows.Forms.NumericUpDown();
            this.panel6 = new System.Windows.Forms.Panel();
            this.checkSlowTime = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.upDownStapmLimit = new System.Windows.Forms.NumericUpDown();
            this.panel8 = new System.Windows.Forms.Panel();
            this.checkStapmLimit = new System.Windows.Forms.CheckBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.upDownSlowLimit = new System.Windows.Forms.NumericUpDown();
            this.panel4 = new System.Windows.Forms.Panel();
            this.checkSlowLimit = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.upDownFastLimit = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.checkFastLimit = new System.Windows.Forms.CheckBox();
            this.ApplySettings = new System.Windows.Forms.Button();
            this.notifyIconRMT = new System.Windows.Forms.NotifyIcon(this.components);
            this.panel9.SuspendLayout();
            this.panel16.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownMaxCurrentLimit)).BeginInit();
            this.panel17.SuspendLayout();
            this.panel14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownCurrentLimit)).BeginInit();
            this.panel15.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownTctlTemp)).BeginInit();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownStapmTime)).BeginInit();
            this.panel13.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSlowTime)).BeginInit();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownStapmLimit)).BeginInit();
            this.panel8.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownSlowLimit)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.upDownFastLimit)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel9
            // 
            resources.ApplyResources(this.panel9, "panel9");
            this.panel9.BackColor = System.Drawing.Color.Transparent;
            this.panel9.Controls.Add(this.panel16);
            this.panel9.Controls.Add(this.panel14);
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Controls.Add(this.panel12);
            this.panel9.Controls.Add(this.panel5);
            this.panel9.Controls.Add(this.panel7);
            this.panel9.Controls.Add(this.panel3);
            this.panel9.Controls.Add(this.panel1);
            this.panel9.Controls.Add(this.ApplySettings);
            this.panel9.Name = "panel9";
            // 
            // panel16
            // 
            resources.ApplyResources(this.panel16, "panel16");
            this.panel16.Controls.Add(this.upDownMaxCurrentLimit);
            this.panel16.Controls.Add(this.panel17);
            this.panel16.Name = "panel16";
            // 
            // upDownMaxCurrentLimit
            // 
            resources.ApplyResources(this.upDownMaxCurrentLimit, "upDownMaxCurrentLimit");
            this.upDownMaxCurrentLimit.Maximum = new decimal(new int[] {
            150,
            0,
            0,
            0});
            this.upDownMaxCurrentLimit.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.upDownMaxCurrentLimit.Name = "upDownMaxCurrentLimit";
            this.upDownMaxCurrentLimit.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            this.upDownMaxCurrentLimit.ValueChanged += new System.EventHandler(this.upDownMaxCurrentLimit_ValueChanged);
            // 
            // panel17
            // 
            this.panel17.Controls.Add(this.checkMaxCurrentLimit);
            resources.ApplyResources(this.panel17, "panel17");
            this.panel17.Name = "panel17";
            // 
            // checkMaxCurrentLimit
            // 
            resources.ApplyResources(this.checkMaxCurrentLimit, "checkMaxCurrentLimit");
            this.checkMaxCurrentLimit.Name = "checkMaxCurrentLimit";
            this.checkMaxCurrentLimit.UseVisualStyleBackColor = true;
            this.checkMaxCurrentLimit.CheckedChanged += new System.EventHandler(this.checkMaxCurrentLimit_CheckedChanged);
            // 
            // panel14
            // 
            resources.ApplyResources(this.panel14, "panel14");
            this.panel14.Controls.Add(this.upDownCurrentLimit);
            this.panel14.Controls.Add(this.panel15);
            this.panel14.Name = "panel14";
            // 
            // upDownCurrentLimit
            // 
            resources.ApplyResources(this.upDownCurrentLimit, "upDownCurrentLimit");
            this.upDownCurrentLimit.Maximum = new decimal(new int[] {
            105,
            0,
            0,
            0});
            this.upDownCurrentLimit.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.upDownCurrentLimit.Name = "upDownCurrentLimit";
            this.upDownCurrentLimit.Value = new decimal(new int[] {
            51,
            0,
            0,
            0});
            this.upDownCurrentLimit.ValueChanged += new System.EventHandler(this.upDownCurrentLimit_ValueChanged);
            // 
            // panel15
            // 
            this.panel15.Controls.Add(this.checkCurrentLimit);
            resources.ApplyResources(this.panel15, "panel15");
            this.panel15.Name = "panel15";
            // 
            // checkCurrentLimit
            // 
            resources.ApplyResources(this.checkCurrentLimit, "checkCurrentLimit");
            this.checkCurrentLimit.Name = "checkCurrentLimit";
            this.checkCurrentLimit.UseVisualStyleBackColor = true;
            this.checkCurrentLimit.CheckedChanged += new System.EventHandler(this.checkCurrentLimit_CheckedChanged);
            // 
            // panel10
            // 
            resources.ApplyResources(this.panel10, "panel10");
            this.panel10.Controls.Add(this.upDownTctlTemp);
            this.panel10.Controls.Add(this.panel11);
            this.panel10.Name = "panel10";
            // 
            // upDownTctlTemp
            // 
            resources.ApplyResources(this.upDownTctlTemp, "upDownTctlTemp");
            this.upDownTctlTemp.Maximum = new decimal(new int[] {
            97,
            0,
            0,
            0});
            this.upDownTctlTemp.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.upDownTctlTemp.Name = "upDownTctlTemp";
            this.upDownTctlTemp.Value = new decimal(new int[] {
            96,
            0,
            0,
            0});
            // 
            // panel11
            // 
            this.panel11.Controls.Add(this.checkTctlTemp);
            resources.ApplyResources(this.panel11, "panel11");
            this.panel11.Name = "panel11";
            // 
            // checkTctlTemp
            // 
            resources.ApplyResources(this.checkTctlTemp, "checkTctlTemp");
            this.checkTctlTemp.Name = "checkTctlTemp";
            this.checkTctlTemp.UseVisualStyleBackColor = true;
            this.checkTctlTemp.CheckedChanged += new System.EventHandler(this.checkTctlTemp_CheckedChanged);
            // 
            // panel12
            // 
            resources.ApplyResources(this.panel12, "panel12");
            this.panel12.Controls.Add(this.upDownStapmTime);
            this.panel12.Controls.Add(this.panel13);
            this.panel12.Name = "panel12";
            // 
            // upDownStapmTime
            // 
            resources.ApplyResources(this.upDownStapmTime, "upDownStapmTime");
            this.upDownStapmTime.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.upDownStapmTime.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.upDownStapmTime.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.upDownStapmTime.Name = "upDownStapmTime";
            this.upDownStapmTime.Value = new decimal(new int[] {
            300,
            0,
            0,
            0});
            this.upDownStapmTime.ValueChanged += new System.EventHandler(this.upDownStapmTime_ValueChanged);
            // 
            // panel13
            // 
            this.panel13.Controls.Add(this.checkStapmTime);
            resources.ApplyResources(this.panel13, "panel13");
            this.panel13.Name = "panel13";
            // 
            // checkStapmTime
            // 
            resources.ApplyResources(this.checkStapmTime, "checkStapmTime");
            this.checkStapmTime.Name = "checkStapmTime";
            this.checkStapmTime.UseVisualStyleBackColor = true;
            this.checkStapmTime.CheckedChanged += new System.EventHandler(this.checkStapmTime_CheckedChanged);
            // 
            // panel5
            // 
            resources.ApplyResources(this.panel5, "panel5");
            this.panel5.Controls.Add(this.upDownSlowTime);
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Name = "panel5";
            // 
            // upDownSlowTime
            // 
            resources.ApplyResources(this.upDownSlowTime, "upDownSlowTime");
            this.upDownSlowTime.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.upDownSlowTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upDownSlowTime.Name = "upDownSlowTime";
            this.upDownSlowTime.Value = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.upDownSlowTime.ValueChanged += new System.EventHandler(this.upDownSlowTime_ValueChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.checkSlowTime);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // checkSlowTime
            // 
            resources.ApplyResources(this.checkSlowTime, "checkSlowTime");
            this.checkSlowTime.Name = "checkSlowTime";
            this.checkSlowTime.UseVisualStyleBackColor = true;
            this.checkSlowTime.CheckedChanged += new System.EventHandler(this.checkSlowTime_CheckedChanged);
            // 
            // panel7
            // 
            resources.ApplyResources(this.panel7, "panel7");
            this.panel7.Controls.Add(this.upDownStapmLimit);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Name = "panel7";
            // 
            // upDownStapmLimit
            // 
            resources.ApplyResources(this.upDownStapmLimit, "upDownStapmLimit");
            this.upDownStapmLimit.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.upDownStapmLimit.Name = "upDownStapmLimit";
            this.upDownStapmLimit.Value = new decimal(new int[] {
            35,
            0,
            0,
            0});
            this.upDownStapmLimit.ValueChanged += new System.EventHandler(this.upDownStapmLimit_ValueChanged);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.checkStapmLimit);
            resources.ApplyResources(this.panel8, "panel8");
            this.panel8.Name = "panel8";
            // 
            // checkStapmLimit
            // 
            resources.ApplyResources(this.checkStapmLimit, "checkStapmLimit");
            this.checkStapmLimit.Name = "checkStapmLimit";
            this.checkStapmLimit.UseVisualStyleBackColor = true;
            this.checkStapmLimit.CheckedChanged += new System.EventHandler(this.checkStapmLimit_CheckedChanged);
            // 
            // panel3
            // 
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Controls.Add(this.upDownSlowLimit);
            this.panel3.Controls.Add(this.panel4);
            this.panel3.Name = "panel3";
            // 
            // upDownSlowLimit
            // 
            resources.ApplyResources(this.upDownSlowLimit, "upDownSlowLimit");
            this.upDownSlowLimit.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.upDownSlowLimit.Name = "upDownSlowLimit";
            this.upDownSlowLimit.Value = new decimal(new int[] {
            54,
            0,
            0,
            0});
            this.upDownSlowLimit.ValueChanged += new System.EventHandler(this.upDownSlowLimit_ValueChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.checkSlowLimit);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // checkSlowLimit
            // 
            resources.ApplyResources(this.checkSlowLimit, "checkSlowLimit");
            this.checkSlowLimit.Name = "checkSlowLimit";
            this.checkSlowLimit.UseVisualStyleBackColor = true;
            this.checkSlowLimit.CheckedChanged += new System.EventHandler(this.checkSlowLimit_CheckedChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.upDownFastLimit);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Name = "panel1";
            // 
            // upDownFastLimit
            // 
            resources.ApplyResources(this.upDownFastLimit, "upDownFastLimit");
            this.upDownFastLimit.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.upDownFastLimit.Name = "upDownFastLimit";
            this.upDownFastLimit.Value = new decimal(new int[] {
            65,
            0,
            0,
            0});
            this.upDownFastLimit.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.checkFastLimit);
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.Name = "panel2";
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // checkFastLimit
            // 
            resources.ApplyResources(this.checkFastLimit, "checkFastLimit");
            this.checkFastLimit.Name = "checkFastLimit";
            this.checkFastLimit.UseVisualStyleBackColor = true;
            this.checkFastLimit.CheckedChanged += new System.EventHandler(this.checkFastLimit_CheckedChanged);
            // 
            // ApplySettings
            // 
            resources.ApplyResources(this.ApplySettings, "ApplySettings");
            this.ApplySettings.Name = "ApplySettings";
            this.ApplySettings.UseVisualStyleBackColor = true;
            this.ApplySettings.Click += new System.EventHandler(this.ApplySettings_Click);
            // 
            // notifyIconRMT
            // 
            resources.ApplyResources(this.notifyIconRMT, "notifyIconRMT");
            this.notifyIconRMT.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIconRMT_MouseDoubleClick);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CausesValidation = false;
            this.Controls.Add(this.panel9);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.Name = "Form1";
            this.Opacity = 0.95D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.panel16.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownMaxCurrentLimit)).EndInit();
            this.panel17.ResumeLayout(false);
            this.panel14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownCurrentLimit)).EndInit();
            this.panel15.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownTctlTemp)).EndInit();
            this.panel11.ResumeLayout(false);
            this.panel12.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownStapmTime)).EndInit();
            this.panel13.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownSlowTime)).EndInit();
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownStapmLimit)).EndInit();
            this.panel8.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownSlowLimit)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.upDownFastLimit)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button ApplySettings;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.NumericUpDown upDownFastLimit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown upDownSlowLimit;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.CheckBox checkSlowLimit;
        private System.Windows.Forms.CheckBox checkFastLimit;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.NumericUpDown upDownCurrentLimit;
        private System.Windows.Forms.Panel panel15;
        private System.Windows.Forms.CheckBox checkCurrentLimit;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.NumericUpDown upDownTctlTemp;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.CheckBox checkTctlTemp;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.NumericUpDown upDownStapmTime;
        private System.Windows.Forms.Panel panel13;
        private System.Windows.Forms.CheckBox checkStapmTime;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.NumericUpDown upDownSlowTime;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.CheckBox checkSlowTime;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.NumericUpDown upDownStapmLimit;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.CheckBox checkStapmLimit;
        private System.Windows.Forms.Panel panel16;
        private System.Windows.Forms.NumericUpDown upDownMaxCurrentLimit;
        private System.Windows.Forms.Panel panel17;
        private System.Windows.Forms.CheckBox checkMaxCurrentLimit;
        internal System.Windows.Forms.NotifyIcon notifyIconRMT;
    }
}

