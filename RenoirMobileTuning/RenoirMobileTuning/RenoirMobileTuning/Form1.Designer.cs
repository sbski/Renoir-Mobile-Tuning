using OpenLibSys;

namespace RenoirMobileTuning
{
    partial class rmtForm
    {
        private readonly Ols ols;

        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainTC = new System.Windows.Forms.TabControl();
            this.mainTab = new System.Windows.Forms.TabPage();
            this.settingsTabPage = new System.Windows.Forms.TabPage();
            this.settingsStatusStrip = new System.Windows.Forms.StatusStrip();
            this.mainTC.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTC
            // 
            this.mainTC.Controls.Add(this.mainTab);
            this.mainTC.Controls.Add(this.settingsTabPage);
            this.mainTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTC.Location = new System.Drawing.Point(0, 0);
            this.mainTC.Name = "mainTC";
            this.mainTC.SelectedIndex = 0;
            this.mainTC.Size = new System.Drawing.Size(800, 450);
            this.mainTC.TabIndex = 0;
            // 
            // mainTab
            // 
            this.mainTab.Location = new System.Drawing.Point(4, 24);
            this.mainTab.Name = "mainTab";
            this.mainTab.Padding = new System.Windows.Forms.Padding(3);
            this.mainTab.Size = new System.Drawing.Size(792, 422);
            this.mainTab.TabIndex = 0;
            this.mainTab.Text = "tabPage1";
            this.mainTab.UseVisualStyleBackColor = true;
            // 
            // settingsTabPage
            // 
            this.settingsTabPage.Location = new System.Drawing.Point(4, 24);
            this.settingsTabPage.Name = "settingsTabPage";
            this.settingsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTabPage.Size = new System.Drawing.Size(792, 422);
            this.settingsTabPage.TabIndex = 1;
            this.settingsTabPage.Text = "tabPage2";
            this.settingsTabPage.UseVisualStyleBackColor = true;
            // 
            // settingsStatusStrip
            // 
            this.settingsStatusStrip.Location = new System.Drawing.Point(0, 428);
            this.settingsStatusStrip.MinimumSize = new System.Drawing.Size(20, 0);
            this.settingsStatusStrip.Name = "settingsStatusStrip";
            this.settingsStatusStrip.Size = new System.Drawing.Size(800, 22);
            this.settingsStatusStrip.TabIndex = 1;
            this.settingsStatusStrip.Text = "statusStrip1";
            // 
            // rmtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.settingsStatusStrip);
            this.Controls.Add(this.mainTC);
            this.Name = "rmtForm";
            this.Text = "Form1";
            this.mainTC.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl mainTC;
        private System.Windows.Forms.TabPage mainTab;
        private System.Windows.Forms.TabPage settingsTabPage;
        private System.Windows.Forms.StatusStrip settingsStatusStrip;
    }
}

