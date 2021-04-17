using FTAnalyzer.Forms.Controls;
using System;

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
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
                clusters.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeLine));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtLocations = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.geocodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geocodeLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.directAncestorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bloodRelativesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.descendantToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marriedToDirectOrBloodToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.relatedByMarriageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unknownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDisableTimeline = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHideScaleBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuKeepZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLimitFactDates = new System.Windows.Forms.ToolStripMenuItem();
            this.cbLimitFactDates = new System.Windows.Forms.ToolStripComboBox();
            this.resetFormToDefaultPostiionAndSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tbYears = new System.Windows.Forms.TrackBar();
            this.labMin = new System.Windows.Forms.Label();
            this.labMax = new System.Windows.Forms.Label();
            this.labValue = new System.Windows.Forms.Label();
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.mapZoomToolStrip = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.txtTimeInterval = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPlay = new System.Windows.Forms.ToolStripButton();
            this.btnStop = new System.Windows.Forms.ToolStripButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.btnBack1 = new System.Windows.Forms.Button();
            this.btnForward1 = new System.Windows.Forms.Button();
            this.btnBack10 = new System.Windows.Forms.Button();
            this.btnForward10 = new System.Windows.Forms.Button();
            this.tbOpacity = new System.Windows.Forms.TrackBar();
            this.linkedByMarriageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbYears)).BeginInit();
            this.mapZoomToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtLocations});
            this.statusStrip1.Location = new System.Drawing.Point(0, 562);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(921, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtLocations
            // 
            this.txtLocations.Name = "txtLocations";
            this.txtLocations.Size = new System.Drawing.Size(58, 17);
            this.txtLocations.Text = "Locations";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geocodeToolStripMenuItem,
            this.relationsToolStripMenuItem,
            this.mnuOptions});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(921, 24);
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
            this.geocodeLocationsToolStripMenuItem.Click += new System.EventHandler(this.GeocodeLocationsToolStripMenuItem_Click);
            // 
            // relationsToolStripMenuItem
            // 
            this.relationsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.directAncestorsToolStripMenuItem,
            this.bloodRelativesToolStripMenuItem,
            this.descendantToolStripMenuItem,
            this.marriedToDirectOrBloodToolStripMenuItem,
            this.relatedByMarriageToolStripMenuItem,
            this.linkedByMarriageToolStripMenuItem,
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
            // descendantToolStripMenuItem
            // 
            this.descendantToolStripMenuItem.Checked = true;
            this.descendantToolStripMenuItem.CheckOnClick = true;
            this.descendantToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.descendantToolStripMenuItem.Name = "descendantToolStripMenuItem";
            this.descendantToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.descendantToolStripMenuItem.Text = "Descendant";
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
            this.mnuDisableTimeline,
            this.mnuHideScaleBar,
            this.mnuKeepZoom,
            this.mnuLimitFactDates,
            this.resetFormToDefaultPostiionAndSizeToolStripMenuItem});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // mnuDisableTimeline
            // 
            this.mnuDisableTimeline.CheckOnClick = true;
            this.mnuDisableTimeline.Name = "mnuDisableTimeline";
            this.mnuDisableTimeline.Size = new System.Drawing.Size(276, 22);
            this.mnuDisableTimeline.Text = "Disable Timeline: Shows All Locations";
            this.mnuDisableTimeline.Click += new System.EventHandler(this.MnuDisableTimeline_Click);
            // 
            // mnuHideScaleBar
            // 
            this.mnuHideScaleBar.CheckOnClick = true;
            this.mnuHideScaleBar.Name = "mnuHideScaleBar";
            this.mnuHideScaleBar.Size = new System.Drawing.Size(276, 22);
            this.mnuHideScaleBar.Text = "Hide Scale Bar";
            this.mnuHideScaleBar.Click += new System.EventHandler(this.MnuHideScaleBar_Click);
            // 
            // mnuKeepZoom
            // 
            this.mnuKeepZoom.CheckOnClick = true;
            this.mnuKeepZoom.Name = "mnuKeepZoom";
            this.mnuKeepZoom.Size = new System.Drawing.Size(276, 22);
            this.mnuKeepZoom.Text = "Keep Zoom on changing Year";
            // 
            // mnuLimitFactDates
            // 
            this.mnuLimitFactDates.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cbLimitFactDates});
            this.mnuLimitFactDates.Name = "mnuLimitFactDates";
            this.mnuLimitFactDates.Size = new System.Drawing.Size(276, 22);
            this.mnuLimitFactDates.Text = "Limit Fact Dates";
            // 
            // cbLimitFactDates
            // 
            this.cbLimitFactDates.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cbLimitFactDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbLimitFactDates.Items.AddRange(new object[] {
            "No Limit",
            "Exact year only",
            "+/- 2 years",
            "+/- 5 years",
            "+/-10 years",
            "+/-20 years",
            "+/-50 years",
            "+/-100 years"});
            this.cbLimitFactDates.Name = "cbLimitFactDates";
            this.cbLimitFactDates.Size = new System.Drawing.Size(121, 23);
            this.cbLimitFactDates.SelectedIndexChanged += new System.EventHandler(this.CbLimitFactDates_SelectedIndexChanged);
            // 
            // resetFormToDefaultPostiionAndSizeToolStripMenuItem
            // 
            this.resetFormToDefaultPostiionAndSizeToolStripMenuItem.Name = "resetFormToDefaultPostiionAndSizeToolStripMenuItem";
            this.resetFormToDefaultPostiionAndSizeToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.resetFormToDefaultPostiionAndSizeToolStripMenuItem.Text = "Reset form to default position and size";
            this.resetFormToDefaultPostiionAndSizeToolStripMenuItem.Click += new System.EventHandler(this.ResetFormToDefaultPostiionAndSizeToolStripMenuItem_Click);
            // 
            // tbYears
            // 
            this.tbYears.Dock = System.Windows.Forms.DockStyle.Top;
            this.tbYears.Location = new System.Drawing.Point(0, 24);
            this.tbYears.Name = "tbYears";
            this.tbYears.Size = new System.Drawing.Size(921, 45);
            this.tbYears.TabIndex = 3;
            this.tbYears.TickFrequency = 5;
            this.tbYears.Scroll += new System.EventHandler(this.TbYears_Scroll);
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
            this.labMax.Location = new System.Drawing.Point(883, 52);
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
            this.labValue.Location = new System.Drawing.Point(439, 50);
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
            this.mapBox1.CustomTool = null;
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
            this.mapBox1.Size = new System.Drawing.Size(921, 468);
            this.mapBox1.TabIndex = 7;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            this.mapBox1.MapZoomChanged += new SharpMap.Forms.MapBox.MapZoomHandler(this.MapBox1_MapZoomChanged);
            this.mapBox1.MapQueried += new SharpMap.Forms.MapBox.MapQueryHandler(this.MapBox1_MapQueried);
            this.mapBox1.MapCenterChanged += new SharpMap.Forms.MapBox.MapCenterChangedHandler(this.MapBox1_MapCenterChanged);
            this.mapBox1.ActiveToolChanged += new SharpMap.Forms.MapBox.ActiveToolChangedHandler(this.MapBox1_ActiveToolChanged);
            this.mapBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MapBox1_MouseDoubleClick);
            // 
            // mapZoomToolStrip
            // 
            this.mapZoomToolStrip.Enabled = false;
            this.mapZoomToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelect,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.txtTimeInterval,
            this.toolStripLabel2,
            this.toolStripSeparator3,
            this.btnPlay,
            this.btnStop});
            this.mapZoomToolStrip.Location = new System.Drawing.Point(0, 69);
            this.mapZoomToolStrip.MapControl = this.mapBox1;
            this.mapZoomToolStrip.Name = "mapZoomToolStrip";
            this.mapZoomToolStrip.Size = new System.Drawing.Size(921, 25);
            this.mapZoomToolStrip.TabIndex = 8;
            this.mapZoomToolStrip.Text = "MapZoomToolStrip";
            // 
            // btnSelect
            // 
            this.btnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(23, 22);
            this.btnSelect.Text = "Location Selection ";
            this.btnSelect.ToolTipText = "Location Selection ";
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(79, 22);
            this.toolStripLabel1.Text = "Time Interval:";
            // 
            // txtTimeInterval
            // 
            this.txtTimeInterval.Name = "txtTimeInterval";
            this.txtTimeInterval.Size = new System.Drawing.Size(35, 25);
            this.txtTimeInterval.Text = "2000";
            this.txtTimeInterval.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TxtTimeInterval_KeyPress);
            this.txtTimeInterval.Validated += new System.EventHandler(this.TxtTimeInterval_Validated);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(23, 22);
            this.toolStripLabel2.Text = "ms";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPlay
            // 
            this.btnPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnPlay.Image = ((System.Drawing.Image)(resources.GetObject("btnPlay.Image")));
            this.btnPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(23, 22);
            this.btnPlay.Text = "toolStripButton2";
            this.btnPlay.ToolTipText = "Play Timeline from current year";
            this.btnPlay.Click += new System.EventHandler(this.BtnPlay_Click);
            // 
            // btnStop
            // 
            this.btnStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
            this.btnStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(23, 22);
            this.btnStop.Text = "toolStripButton3";
            this.btnStop.ToolTipText = "Stop Timeline Playback";
            this.btnStop.Visible = false;
            this.btnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(793, 94);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 13);
            this.linkLabel1.TabIndex = 15;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "© Google - Terms of Use";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
            // 
            // timer
            // 
            this.timer.Interval = 2000;
            this.timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // btnBack1
            // 
            this.btnBack1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBack1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack1.Location = new System.Drawing.Point(410, 47);
            this.btnBack1.Name = "btnBack1";
            this.btnBack1.Size = new System.Drawing.Size(23, 22);
            this.btnBack1.TabIndex = 10;
            this.btnBack1.Text = "<";
            this.btnBack1.UseVisualStyleBackColor = true;
            this.btnBack1.Click += new System.EventHandler(this.BtnBack1_Click);
            // 
            // btnForward1
            // 
            this.btnForward1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnForward1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForward1.Location = new System.Drawing.Point(492, 47);
            this.btnForward1.Name = "btnForward1";
            this.btnForward1.Size = new System.Drawing.Size(23, 22);
            this.btnForward1.TabIndex = 11;
            this.btnForward1.Text = ">";
            this.btnForward1.UseVisualStyleBackColor = true;
            this.btnForward1.Click += new System.EventHandler(this.BtnForward1_Click);
            // 
            // btnBack10
            // 
            this.btnBack10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnBack10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBack10.Location = new System.Drawing.Point(375, 47);
            this.btnBack10.Name = "btnBack10";
            this.btnBack10.Size = new System.Drawing.Size(29, 22);
            this.btnBack10.TabIndex = 12;
            this.btnBack10.Text = "<<";
            this.btnBack10.UseVisualStyleBackColor = true;
            this.btnBack10.Click += new System.EventHandler(this.BtnBack10_Click);
            // 
            // btnForward10
            // 
            this.btnForward10.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnForward10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnForward10.Location = new System.Drawing.Point(521, 47);
            this.btnForward10.Name = "btnForward10";
            this.btnForward10.Size = new System.Drawing.Size(29, 22);
            this.btnForward10.TabIndex = 13;
            this.btnForward10.Text = ">>";
            this.btnForward10.UseVisualStyleBackColor = true;
            this.btnForward10.Click += new System.EventHandler(this.BtnForward10_Click);
            // 
            // tbOpacity
            // 
            this.tbOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbOpacity.LargeChange = 20;
            this.tbOpacity.Location = new System.Drawing.Point(6, 514);
            this.tbOpacity.Maximum = 100;
            this.tbOpacity.Name = "tbOpacity";
            this.tbOpacity.Size = new System.Drawing.Size(250, 45);
            this.tbOpacity.SmallChange = 5;
            this.tbOpacity.TabIndex = 18;
            this.tbOpacity.TickFrequency = 10;
            this.tbOpacity.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.tbOpacity.Value = 100;
            this.tbOpacity.Scroll += new System.EventHandler(this.TbOpacity_Scroll);
            // 
            // linkedByMarriageToolStripMenuItem
            // 
            this.linkedByMarriageToolStripMenuItem.Name = "linkedByMarriageToolStripMenuItem";
            this.linkedByMarriageToolStripMenuItem.Size = new System.Drawing.Size(211, 22);
            this.linkedByMarriageToolStripMenuItem.Text = "Linked through Marriages";
            // 
            // TimeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 584);
            this.Controls.Add(this.tbOpacity);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnForward10);
            this.Controls.Add(this.btnBack10);
            this.Controls.Add(this.btnForward1);
            this.Controls.Add(this.btnBack1);
            this.Controls.Add(this.mapBox1);
            this.Controls.Add(this.mapZoomToolStrip);
            this.Controls.Add(this.labValue);
            this.Controls.Add(this.labMax);
            this.Controls.Add(this.labMin);
            this.Controls.Add(this.tbYears);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "TimeLine";
            this.Text = "Timeline of Individuals";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.TimeLine_FormClosed);
            this.Load += new System.EventHandler(this.TimeLine_Load);
            this.Move += new System.EventHandler(this.TimeLine_Move);
            this.Resize += new System.EventHandler(this.TimeLine_Resize);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbYears)).EndInit();
            this.mapZoomToolStrip.ResumeLayout(false);
            this.mapZoomToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtLocations;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem geocodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem geocodeLocationsToolStripMenuItem;
        private System.Windows.Forms.TrackBar tbYears;
        private System.Windows.Forms.Label labMin;
        private System.Windows.Forms.Label labMax;
        private System.Windows.Forms.Label labValue;
        private SharpMap.Forms.MapBox mapBox1;
        private SharpMap.Forms.ToolBar.MapZoomToolStrip mapZoomToolStrip;
        private System.Windows.Forms.ToolStripMenuItem relationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem directAncestorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bloodRelativesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marriedToDirectOrBloodToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem relatedByMarriageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unknownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuKeepZoom;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.ToolStripButton btnPlay;
        private System.Windows.Forms.ToolStripButton btnStop;
        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox txtTimeInterval;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.Button btnBack1;
        private System.Windows.Forms.Button btnForward1;
        private System.Windows.Forms.Button btnBack10;
        private System.Windows.Forms.Button btnForward10;
        private System.Windows.Forms.ToolStripMenuItem mnuDisableTimeline;
        private System.Windows.Forms.ToolStripMenuItem mnuLimitFactDates;
        private System.Windows.Forms.ToolStripComboBox cbLimitFactDates;
        private System.Windows.Forms.ToolStripMenuItem mnuHideScaleBar;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private ToolStripMapSelector mnuMapStyle = new ToolStripMapSelector();
        private System.Windows.Forms.ToolStripMenuItem resetFormToDefaultPostiionAndSizeToolStripMenuItem;
        private System.Windows.Forms.TrackBar tbOpacity;
        private System.Windows.Forms.ToolStripMenuItem descendantToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem linkedByMarriageToolStripMenuItem;
    }
}