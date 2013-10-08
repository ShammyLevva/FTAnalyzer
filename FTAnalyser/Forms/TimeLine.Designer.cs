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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLine));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtLocations = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbGeocoding = new System.Windows.Forms.ToolStripProgressBar();
            this.txtGoogleWait = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.geocodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geocodeLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directAncestorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bloodRelativesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marriedToDirectOrBloodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatedByMarriageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unknownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRetryPartial = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKeepZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.tbYears = new System.Windows.Forms.TrackBar();
            this.labMin = new System.Windows.Forms.Label();
            this.labMax = new System.Windows.Forms.Label();
            this.labValue = new System.Windows.Forms.Label();
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.mapZoomToolStrip = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.menuMap = new System.Windows.Forms.ToolStripMenuItem();
            this.menuSatellite = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbYears)).BeginInit();
            this.mapZoomToolStrip.SuspendLayout();
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
            this.txtGoogleWait.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.txtGoogleWait.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geocodeToolStripMenuItem,
            this.relationsToolStripMenuItem,
            this.mnuOptions});
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
            // relationsToolStripMenuItem
            // 
            this.relationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directAncestorsToolStripMenuItem,
            this.bloodRelativesToolStripMenuItem,
            this.marriedToDirectOrBloodToolStripMenuItem,
            this.relatedByMarriageToolStripMenuItem,
            this.unknownToolStripMenuItem});
            this.relationsToolStripMenuItem.Name = "relationsToolStripMenuItem";
            this.relationsToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.relationsToolStripMenuItem.Text = "Relations";
            // 
            // directAncestorsToolStripMenuItem
            // 
            this.directAncestorsToolStripMenuItem.Checked = true;
            this.directAncestorsToolStripMenuItem.CheckOnClick = true;
            this.directAncestorsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.directAncestorsToolStripMenuItem.Name = "directAncestorsToolStripMenuItem";
            this.directAncestorsToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.directAncestorsToolStripMenuItem.Text = "Direct Ancestors";
            this.directAncestorsToolStripMenuItem.CheckedChanged += new System.EventHandler(this.Relations_CheckedChanged);
            // 
            // bloodRelativesToolStripMenuItem
            // 
            this.bloodRelativesToolStripMenuItem.Checked = true;
            this.bloodRelativesToolStripMenuItem.CheckOnClick = true;
            this.bloodRelativesToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.bloodRelativesToolStripMenuItem.Name = "bloodRelativesToolStripMenuItem";
            this.bloodRelativesToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.bloodRelativesToolStripMenuItem.Text = "Blood Relatives";
            this.bloodRelativesToolStripMenuItem.CheckedChanged += new System.EventHandler(this.Relations_CheckedChanged);
            // 
            // marriedToDirectOrBloodToolStripMenuItem
            // 
            this.marriedToDirectOrBloodToolStripMenuItem.Checked = true;
            this.marriedToDirectOrBloodToolStripMenuItem.CheckOnClick = true;
            this.marriedToDirectOrBloodToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.marriedToDirectOrBloodToolStripMenuItem.Name = "marriedToDirectOrBloodToolStripMenuItem";
            this.marriedToDirectOrBloodToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.marriedToDirectOrBloodToolStripMenuItem.Text = "Married to Direct or Blood";
            this.marriedToDirectOrBloodToolStripMenuItem.CheckedChanged += new System.EventHandler(this.Relations_CheckedChanged);
            // 
            // relatedByMarriageToolStripMenuItem
            // 
            this.relatedByMarriageToolStripMenuItem.CheckOnClick = true;
            this.relatedByMarriageToolStripMenuItem.Name = "relatedByMarriageToolStripMenuItem";
            this.relatedByMarriageToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.relatedByMarriageToolStripMenuItem.Text = "Related by Marriage";
            this.relatedByMarriageToolStripMenuItem.CheckedChanged += new System.EventHandler(this.Relations_CheckedChanged);
            // 
            // unknownToolStripMenuItem
            // 
            this.unknownToolStripMenuItem.CheckOnClick = true;
            this.unknownToolStripMenuItem.Name = "unknownToolStripMenuItem";
            this.unknownToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.unknownToolStripMenuItem.Text = "Unknown";
            this.unknownToolStripMenuItem.CheckedChanged += new System.EventHandler(this.Relations_CheckedChanged);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRetryPartial,
            this.mnuKeepZoom});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // mnuRetryPartial
            // 
            this.mnuRetryPartial.CheckOnClick = true;
            this.mnuRetryPartial.Name = "mnuRetryPartial";
            this.mnuRetryPartial.Size = new System.Drawing.Size(231, 22);
            this.mnuRetryPartial.Text = "Retry Partially Geocoded";
            // 
            // mnuKeepZoom
            // 
            this.mnuKeepZoom.CheckOnClick = true;
            this.mnuKeepZoom.Name = "mnuKeepZoom";
            this.mnuKeepZoom.Size = new System.Drawing.Size(231, 22);
            this.mnuKeepZoom.Text = "Keep Zoom on changing Year";
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
            this.mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            this.mapBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mapBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBox1.FineZoomFactor = 10D;
            this.mapBox1.Location = new System.Drawing.Point(0, 94);
            this.mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.PanOnClick = false;
            this.mapBox1.PreviewMode = SharpMap.Forms.MapBox.PreviewModes.Fast;
            this.mapBox1.QueryGrowFactor = 5F;
            this.mapBox1.QueryLayerIndex = 0;
            this.mapBox1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.ShowProgressUpdate = true;
            this.mapBox1.Size = new System.Drawing.Size(920, 468);
            this.mapBox1.TabIndex = 7;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            this.mapBox1.MapZoomChanged += new SharpMap.Forms.MapBox.MapZoomHandler(this.mapBox1_MapZoomChanged);
            this.mapBox1.MapQueried += new SharpMap.Forms.MapBox.MapQueryHandler(this.mapBox1_MapQueried);
            this.mapBox1.MapCenterChanged += new SharpMap.Forms.MapBox.MapCenterChangedHandler(this.mapBox1_MapCenterChanged);
            this.mapBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapBox1_MouseDoubleClick);
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
            this.mapZoomToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripDropDownButton1});
            this.mapZoomToolStrip.Location = new System.Drawing.Point(0, 69);
            this.mapZoomToolStrip.MapControl = this.mapBox1;
            this.mapZoomToolStrip.Name = "mapZoomToolStrip";
            this.mapZoomToolStrip.Size = new System.Drawing.Size(920, 25);
            this.mapZoomToolStrip.TabIndex = 8;
            this.mapZoomToolStrip.Text = "MapZoomToolStrip";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.ToolTipText = "Location Selection ";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuMap,
            this.menuSatellite});
            this.toolStripDropDownButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButton1.Image")));
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(71, 22);
            this.toolStripDropDownButton1.Text = "Map style";
            this.toolStripDropDownButton1.Visible = false;
            // 
            // menuMap
            // 
            this.menuMap.CheckOnClick = true;
            this.menuMap.Name = "menuMap";
            this.menuMap.Size = new System.Drawing.Size(115, 22);
            this.menuMap.Text = "Map";
            this.menuMap.Click += new System.EventHandler(this.googleMapToolStripMenuItem_Click);
            // 
            // menuSatellite
            // 
            this.menuSatellite.Checked = true;
            this.menuSatellite.CheckOnClick = true;
            this.menuSatellite.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menuSatellite.Name = "menuSatellite";
            this.menuSatellite.Size = new System.Drawing.Size(115, 22);
            this.menuSatellite.Text = "Satellite";
            this.menuSatellite.Click += new System.EventHandler(this.googleMapToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(328, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Currently Viewing:";
            // 
            // TimeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(920, 584);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.mapBox1);
            this.Controls.Add(this.mapZoomToolStrip);
            this.Controls.Add(this.labValue);
            this.Controls.Add(this.labMax);
            this.Controls.Add(this.labMin);
            this.Controls.Add(this.tbYears);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TimeLine";
            this.Text = "Timeline of Individuals";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TimeLine_FormClosing);
            this.Load += new System.EventHandler(this.TimeLine_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbYears)).EndInit();
            this.mapZoomToolStrip.ResumeLayout(false);
            this.mapZoomToolStrip.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem relationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directAncestorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bloodRelativesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marriedToDirectOrBloodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relatedByMarriageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unknownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRetryPartial;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuKeepZoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripMenuItem menuMap;
        private System.Windows.Forms.ToolStripMenuItem menuSatellite;
    }
}