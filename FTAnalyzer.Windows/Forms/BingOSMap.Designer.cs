using System;

namespace FTAnalyzer.Forms
{
    partial class BingOSMap
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
            try
            {
                if (disposing && (components is not null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
            }
            catch (Exception) { }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BingOSMap));
            webBrowser = new WebBrowser();
            labMapLevel = new Label();
            labTOU = new Label();
            SuspendLayout();
            // 
            // webBrowser
            // 
            webBrowser.Dock = DockStyle.Fill;
            webBrowser.Location = new Point(0, 0);
            webBrowser.Margin = new Padding(4, 3, 4, 3);
            webBrowser.MinimumSize = new Size(23, 23);
            webBrowser.Name = "webBrowser";
            webBrowser.ScrollBarsEnabled = false;
            webBrowser.Size = new Size(1034, 584);
            webBrowser.TabIndex = 0;
            webBrowser.DocumentCompleted += WebBrowser_DocumentCompleted;
            // 
            // labMapLevel
            // 
            labMapLevel.AutoSize = true;
            labMapLevel.Dock = DockStyle.Bottom;
            labMapLevel.Location = new Point(0, 569);
            labMapLevel.Margin = new Padding(4, 0, 4, 0);
            labMapLevel.Name = "labMapLevel";
            labMapLevel.Size = new Size(32, 15);
            labMapLevel.TabIndex = 1;
            labMapLevel.Text = "label";
            // 
            // labTOU
            // 
            labTOU.AutoSize = true;
            labTOU.Dock = DockStyle.Right;
            labTOU.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Underline, GraphicsUnit.Point, 0);
            labTOU.ForeColor = SystemColors.Highlight;
            labTOU.Location = new Point(775, 0);
            labTOU.Margin = new Padding(4, 0, 4, 0);
            labTOU.Name = "labTOU";
            labTOU.Size = new Size(259, 13);
            labTOU.TabIndex = 2;
            labTOU.Text = "https://www.microsoft.com/Maps/product/terms.html";
            labTOU.Click += LabTOU_Click;
            // 
            // BingOSMap
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1034, 584);
            Controls.Add(labTOU);
            Controls.Add(labMapLevel);
            Controls.Add(webBrowser);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "BingOSMap";
            Text = "Bing OS Map";
            FormClosed += BingOSMap_FormClosed;
            Load += BingOSMap_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label labMapLevel;
        private System.Windows.Forms.Label labTOU;
    }
}