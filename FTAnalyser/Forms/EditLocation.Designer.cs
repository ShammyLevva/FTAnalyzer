namespace FTAnalyzer.Forms
{
    partial class EditLocation
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditLocation));
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.mapZoomToolStrip = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
            this.mnuMapStyle = new System.Windows.Forms.ToolStripDropDownButton();
            this.mnuGoogleMap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGoogleSatellite = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOpenStreetMap = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBingMapAerial = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBingMapRoads = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBingMapHybrid = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBingMapOS = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnReload = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.mapZoomToolStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // mapBox1
            // 
            this.mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
            this.mapBox1.AllowDrop = true;
            this.mapBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.mapBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBox1.FineZoomFactor = 10D;
            this.mapBox1.Location = new System.Drawing.Point(0, 25);
            this.mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.PanOnClick = false;
            this.mapBox1.PreviewMode = SharpMap.Forms.MapBox.PreviewModes.Fast;
            this.mapBox1.QueryGrowFactor = 5F;
            this.mapBox1.QueryLayerIndex = 0;
            this.mapBox1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.ShowProgressUpdate = true;
            this.mapBox1.Size = new System.Drawing.Size(754, 513);
            this.mapBox1.TabIndex = 9;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            this.mapBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.mapBox1_MouseClick);
            // 
            // mapZoomToolStrip
            // 
            this.mapZoomToolStrip.Enabled = false;
            this.mapZoomToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuMapStyle,
            this.btnSave,
            this.btnReload});
            this.mapZoomToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mapZoomToolStrip.MapControl = this.mapBox1;
            this.mapZoomToolStrip.Name = "mapZoomToolStrip";
            this.mapZoomToolStrip.Size = new System.Drawing.Size(754, 25);
            this.mapZoomToolStrip.TabIndex = 10;
            this.mapZoomToolStrip.Text = "MapZoomToolStrip";
            // 
            // mnuMapStyle
            // 
            this.mnuMapStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.mnuMapStyle.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGoogleMap,
            this.mnuGoogleSatellite,
            this.mnuOpenStreetMap,
            this.mnuBingMapAerial,
            this.mnuBingMapRoads,
            this.mnuBingMapHybrid,
            this.mnuBingMapOS});
            this.mnuMapStyle.Image = ((System.Drawing.Image)(resources.GetObject("mnuMapStyle.Image")));
            this.mnuMapStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuMapStyle.Name = "mnuMapStyle";
            this.mnuMapStyle.Size = new System.Drawing.Size(71, 22);
            this.mnuMapStyle.Text = "Map style";
            // 
            // mnuGoogleMap
            // 
            this.mnuGoogleMap.Checked = true;
            this.mnuGoogleMap.CheckOnClick = true;
            this.mnuGoogleMap.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuGoogleMap.Name = "mnuGoogleMap";
            this.mnuGoogleMap.Size = new System.Drawing.Size(164, 22);
            this.mnuGoogleMap.Text = "Google Map";
            this.mnuGoogleMap.Click += new System.EventHandler(this.mnuMapStyle_Click);
            // 
            // mnuGoogleSatellite
            // 
            this.mnuGoogleSatellite.CheckOnClick = true;
            this.mnuGoogleSatellite.Name = "mnuGoogleSatellite";
            this.mnuGoogleSatellite.Size = new System.Drawing.Size(164, 22);
            this.mnuGoogleSatellite.Text = "Google Satellite";
            this.mnuGoogleSatellite.Visible = false;
            this.mnuGoogleSatellite.Click += new System.EventHandler(this.mnuMapStyle_Click);
            // 
            // mnuOpenStreetMap
            // 
            this.mnuOpenStreetMap.CheckOnClick = true;
            this.mnuOpenStreetMap.Name = "mnuOpenStreetMap";
            this.mnuOpenStreetMap.Size = new System.Drawing.Size(164, 22);
            this.mnuOpenStreetMap.Text = "Open Street Map";
            this.mnuOpenStreetMap.Click += new System.EventHandler(this.mnuMapStyle_Click);
            // 
            // mnuBingMapAerial
            // 
            this.mnuBingMapAerial.CheckOnClick = true;
            this.mnuBingMapAerial.Name = "mnuBingMapAerial";
            this.mnuBingMapAerial.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapAerial.Text = "Aerial Bing Map";
            this.mnuBingMapAerial.Click += new System.EventHandler(this.mnuMapStyle_Click);
            // 
            // mnuBingMapRoads
            // 
            this.mnuBingMapRoads.CheckOnClick = true;
            this.mnuBingMapRoads.Name = "mnuBingMapRoads";
            this.mnuBingMapRoads.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapRoads.Text = "Roads Bing Map";
            this.mnuBingMapRoads.Click += new System.EventHandler(this.mnuMapStyle_Click);
            // 
            // mnuBingMapHybrid
            // 
            this.mnuBingMapHybrid.CheckOnClick = true;
            this.mnuBingMapHybrid.Name = "mnuBingMapHybrid";
            this.mnuBingMapHybrid.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapHybrid.Text = "Hybrid Bing Map";
            this.mnuBingMapHybrid.Click += new System.EventHandler(this.mnuMapStyle_Click);
            // 
            // mnuBingMapOS
            // 
            this.mnuBingMapOS.Name = "mnuBingMapOS";
            this.mnuBingMapOS.Size = new System.Drawing.Size(164, 22);
            this.mnuBingMapOS.Text = "OS Bing Map";
            this.mnuBingMapOS.Visible = false;
            this.mnuBingMapOS.Click += new System.EventHandler(this.mnuMapStyle_Click);
            // 
            // btnSave
            // 
            this.btnSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSave.Image = global::FTAnalyzer.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(23, 22);
            this.btnSave.Text = "toolStripButton1";
            this.btnSave.ToolTipText = "Save location to database";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnReload
            // 
            this.btnReload.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnReload.Image = global::FTAnalyzer.Properties.Resources.Restart;
            this.btnReload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(23, 22);
            this.btnReload.Text = "toolStripButton2";
            this.btnReload.ToolTipText = "Reset Point to previous position";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(754, 22);
            this.statusStrip1.TabIndex = 11;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(633, 17);
            this.toolStripStatusLabel1.Text = "Left click to select pointer, move to the correct place (using zoom/pan) then rig" +
                "ht click to place pointer in new location";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(684, 25);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(70, 13);
            this.linkLabel1.TabIndex = 17;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Terms of Use";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // EditLocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(754, 538);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.mapBox1);
            this.Controls.Add(this.mapZoomToolStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditLocation";
            this.Text = "EditLocation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.EditLocation_FormClosing);
            this.mapZoomToolStrip.ResumeLayout(false);
            this.mapZoomToolStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private SharpMap.Forms.MapBox mapBox1;
        private SharpMap.Forms.ToolBar.MapZoomToolStrip mapZoomToolStrip;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripButton btnSave;
        private System.Windows.Forms.ToolStripButton btnReload;
        private System.Windows.Forms.ToolStripDropDownButton mnuMapStyle;
        private System.Windows.Forms.ToolStripMenuItem mnuGoogleMap;
        private System.Windows.Forms.ToolStripMenuItem mnuGoogleSatellite;
        private System.Windows.Forms.ToolStripMenuItem mnuOpenStreetMap;
        private System.Windows.Forms.ToolStripMenuItem mnuBingMapAerial;
        private System.Windows.Forms.ToolStripMenuItem mnuBingMapRoads;
        private System.Windows.Forms.ToolStripMenuItem mnuBingMapHybrid;
        private System.Windows.Forms.ToolStripMenuItem mnuBingMapOS;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}