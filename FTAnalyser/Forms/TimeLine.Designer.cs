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
            this.components = new System.ComponentModel.Container();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtLocations = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbGeocoding = new System.Windows.Forms.ToolStripProgressBar();
            this.txtGoogleWait = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.geocodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geocodeLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbYears = new System.Windows.Forms.TrackBar();
            this.labMin = new System.Windows.Forms.Label();
            this.labMax = new System.Windows.Forms.Label();
            this.labValue = new System.Windows.Forms.Label();
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.mapZoomToolStrip = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 562);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(920, 22);
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
            this.menuStrip1.Size = new System.Drawing.Size(920, 24);
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
            // tbYears
            // 
            this.tbYears.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbYears.Location = new System.Drawing.Point(0, 24);
            this.tbYears.Name = "tbYears";
            this.tbYears.Size = new System.Drawing.Size(920, 45);
            this.tbYears.TabIndex = 3;
            this.tbYears.TickFrequency = 5;
            this.tbYears.Scroll += new System.EventHandler(this.tbYears_Scroll);
            // 
            // labMin
            // 
            this.labMin.AutoSize = true;
            this.labMin.Location = new System.Drawing.Point(3, 54);
            this.labMin.Name = "labMin";
            this.labMin.Size = new System.Drawing.Size(35, 13);
            this.labMin.TabIndex = 4;
            this.labMin.Text = "label1";
            this.labMin.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labMax
            // 
            this.labMax.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labMax.AutoSize = true;
            this.labMax.Location = new System.Drawing.Point(882, 52);
            this.labMax.Name = "labMax";
            this.labMax.Size = new System.Drawing.Size(35, 13);
            this.labMax.TabIndex = 5;
            this.labMax.Text = "label1";
            this.labMax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // labValue
            // 
            this.labValue.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labValue.AutoSize = true;
            this.labValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labValue.Location = new System.Drawing.Point(439, 52);
            this.labValue.Name = "labValue";
            this.labValue.Size = new System.Drawing.Size(47, 15);
            this.labValue.TabIndex = 6;
            this.labValue.Text = "label1";
            this.labValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mapBox1
            // 
            this.mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.mapBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.mapBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.mapBox1.FineZoomFactor = 10D;
            this.mapBox1.Location = new System.Drawing.Point(0, 75);
            this.mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.QueryGrowFactor = 5F;
            this.mapBox1.QueryLayerIndex = 0;
            this.mapBox1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.ShowProgressUpdate = false;
            this.mapBox1.Size = new System.Drawing.Size(917, 484);
            this.mapBox1.TabIndex = 7;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // mapZoomToolStrip
            // 
            this.mapZoomToolStrip.Enabled = false;
            this.mapZoomToolStrip.Location = new System.Drawing.Point(0, 69);
            this.mapZoomToolStrip.MapControl = this.mapBox1;
            this.mapZoomToolStrip.Name = "mapZoomToolStrip";
            this.mapZoomToolStrip.Size = new System.Drawing.Size(920, 25);
            this.mapZoomToolStrip.TabIndex = 8;
            this.mapZoomToolStrip.Text = "MapZoomToolStrip";
            // 
            // TimeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 584);
            this.Controls.Add(this.mapZoomToolStrip);
            this.Controls.Add(this.mapBox1);
            this.Controls.Add(this.labValue);
            this.Controls.Add(this.labMax);
            this.Controls.Add(this.labMin);
            this.Controls.Add(this.tbYears);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TimeLine";
            this.Text = "TimeLine";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimeLine_FormClosing);
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
        private System.Windows.Forms.TrackBar tbYears;
        private System.Windows.Forms.Label labMin;
        private System.Windows.Forms.Label labMax;
        private System.Windows.Forms.Label labValue;
        private SharpMap.Forms.MapBox mapBox1;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private SharpMap.Forms.ToolBar.MapZoomToolStrip mapZoomToolStrip;
    }
}