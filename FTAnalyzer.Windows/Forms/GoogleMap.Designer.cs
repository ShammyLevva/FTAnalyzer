using System;

namespace FTAnalyzer.Forms
{
    partial class GoogleMap
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
                if (disposing && (components != null))
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GoogleMap));
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.labMapLevel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // webBrowser
            // 
            this.webBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser.Location = new System.Drawing.Point(0, 0);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.ScrollBarsEnabled = false;
            this.webBrowser.Size = new System.Drawing.Size(886, 506);
            this.webBrowser.TabIndex = 0;
            this.webBrowser.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.WebBrowser_DocumentCompleted);
            // 
            // labMapLevel
            // 
            this.labMapLevel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labMapLevel.AutoSize = true;
            this.labMapLevel.Location = new System.Drawing.Point(3, 3);
            this.labMapLevel.Name = "labMapLevel";
            this.labMapLevel.Size = new System.Drawing.Size(35, 13);
            this.labMapLevel.TabIndex = 1;
            this.labMapLevel.Text = "label1";
            // 
            // GoogleMap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 506);
            this.Controls.Add(this.labMapLevel);
            this.Controls.Add(this.webBrowser);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GoogleMap";
            this.Text = "Google Map";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GoogleMap_FormClosed);
            this.Load += new System.EventHandler(this.GoogleMap_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.Label labMapLevel;
    }
}