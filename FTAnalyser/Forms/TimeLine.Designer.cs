namespace FTAnalyzer.Forms
{
    partial class TimeLine
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
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtLocations = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbGeocoding = new System.Windows.Forms.ToolStripProgressBar();
            this.txtGoogleWait = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.geocodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geocodeLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.tbYears = new System.Windows.Forms.TrackBar();
            this.labMin = new System.Windows.Forms.Label();
            this.labMax = new System.Windows.Forms.Label();
            this.labValue = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbYears)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtLocations,
            this.pbGeocoding,
            this.txtGoogleWait});
            this.statusStrip1.Location = new System.Drawing.Point(0, 335);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(852, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtLocations
            // 
            this.txtLocations.Name = "txtLocations";
            this.txtLocations.Size = new System.Drawing.Size(58, 17);
            this.txtLocations.Text = "Locations";
            // 
            // pbGeocoding
            // 
            this.pbGeocoding.Name = "pbGeocoding";
            this.pbGeocoding.Size = new System.Drawing.Size(100, 16);
            this.pbGeocoding.Visible = false;
            // 
            // txtGoogleWait
            // 
            this.txtGoogleWait.Name = "txtGoogleWait";
            this.txtGoogleWait.Size = new System.Drawing.Size(118, 17);
            this.txtGoogleWait.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geocodeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(852, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // geocodeToolStripMenuItem
            // 
            this.geocodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geocodeLocationsToolStripMenuItem});
            this.geocodeToolStripMenuItem.Name = "geocodeToolStripMenuItem";
            this.geocodeToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.geocodeToolStripMenuItem.Text = "Process";
            // 
            // geocodeLocationsToolStripMenuItem
            // 
            this.geocodeLocationsToolStripMenuItem.Name = "geocodeLocationsToolStripMenuItem";
            this.geocodeLocationsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.geocodeLocationsToolStripMenuItem.Text = "Geocode Locations";
            this.geocodeLocationsToolStripMenuItem.ToolTipText = "Looks up map co-ordinates for locations in your file";
            this.geocodeLocationsToolStripMenuItem.Click += new System.EventHandler(this.geocodeLocationsToolStripMenuItem_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Location = new System.Drawing.Point(0, 78);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(852, 398);
            this.webBrowser1.TabIndex = 2;
            // 
            // tbYears
            // 
            this.tbYears.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbYears.Location = new System.Drawing.Point(0, 24);
            this.tbYears.Name = "tbYears";
            this.tbYears.Size = new System.Drawing.Size(852, 45);
            this.tbYears.TabIndex = 3;
            this.tbYears.TickFrequency = 5;
            this.tbYears.Scroll += new System.EventHandler(this.tbYears_Scroll);
            // 
            // labMin
            // 
            this.labMin.AutoSize = true;
            this.labMin.Location = new System.Drawing.Point(3, 56);
            this.labMin.Name = "labMin";
            this.labMin.Size = new System.Drawing.Size(35, 13);
            this.labMin.TabIndex = 4;
            this.labMin.Text = "label1";
            this.labMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labMax
            // 
            this.labMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.labMax.AutoSize = true;
            this.labMax.Location = new System.Drawing.Point(814, 56);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(35, 13);
            this.labMax.TabIndex = 5;
            this.labMax.Text = "label1";
            this.labMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labValue
            // 
            this.labValue.AutoSize = true;
            this.labValue.Location = new System.Drawing.Point(412, 56);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(35, 13);
            this.labValue.TabIndex = 6;
            this.labValue.Text = "label1";
            this.labValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TimeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 357);
            this.Controls.Add(this.labValue);
            this.Controls.Add(this.labMax);
            this.Controls.Add(this.labMin);
            this.Controls.Add(this.tbYears);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TimeLine";
            this.Text = "Timeline";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbYears)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtLocations;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem geocodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geocodeLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar pbGeocoding;
        private System.Windows.Forms.ToolStripStatusLabel txtGoogleWait;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.TrackBar tbYears;
        private System.Windows.Forms.Label labMin;
        private System.Windows.Forms.Label labMax;
        private System.Windows.Forms.Label labValue;
    }
}