﻿using FTAnalyzer.Forms.Controls;
namespace FTAnalyzer.Forms
{
    partial class Places
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Places));
            this.splitContainerFacts = new System.Windows.Forms.SplitContainer();
            this.splitContainerMap = new System.Windows.Forms.SplitContainer();
            this.tvPlaces = new System.Windows.Forms.TreeView();
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.mapZoomToolStrip = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
            this.dgFacts = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.txtCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FactIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.FactsIndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Forenames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeOfFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgeAtFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeocodeStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleResultTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.progressbar = new System.Windows.Forms.ToolStripProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFacts)).BeginInit();
            this.splitContainerFacts.Panel1.SuspendLayout();
            this.splitContainerFacts.Panel2.SuspendLayout();
            this.splitContainerFacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMap)).BeginInit();
            this.splitContainerMap.Panel1.SuspendLayout();
            this.splitContainerMap.Panel2.SuspendLayout();
            this.splitContainerMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFacts)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerFacts
            // 
            this.splitContainerFacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFacts.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerFacts.Location = new System.Drawing.Point(0, 0);
            this.splitContainerFacts.Name = "splitContainerFacts";
            this.splitContainerFacts.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainerFacts.Panel1
            // 
            this.splitContainerFacts.Panel1.Controls.Add(this.splitContainerMap);
            // 
            // splitContainerFacts.Panel2
            // 
            this.splitContainerFacts.Panel2.Controls.Add(this.dgFacts);
            this.splitContainerFacts.Panel2.Controls.Add(this.statusStrip);
            this.splitContainerFacts.Size = new System.Drawing.Size(1113, 590);
            this.splitContainerFacts.SplitterDistance = 459;
            this.splitContainerFacts.TabIndex = 18;
            // 
            // splitContainerMap
            // 
            this.splitContainerMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMap.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainerMap.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMap.Name = "splitContainerMap";
            // 
            // splitContainerMap.Panel1
            // 
            this.splitContainerMap.Panel1.Controls.Add(this.tvPlaces);
            // 
            // splitContainerMap.Panel2
            // 
            this.splitContainerMap.Panel2.Controls.Add(this.mapBox1);
            this.splitContainerMap.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainerMap.Panel2.Controls.Add(this.mapZoomToolStrip);
            this.splitContainerMap.Size = new System.Drawing.Size(1113, 459);
            this.splitContainerMap.SplitterDistance = 330;
            this.splitContainerMap.TabIndex = 2;
            // 
            // tvPlaces
            // 
            this.tvPlaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvPlaces.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tvPlaces.Location = new System.Drawing.Point(0, 0);
            this.tvPlaces.Name = "tvPlaces";
            this.tvPlaces.Size = new System.Drawing.Size(330, 459);
            this.tvPlaces.TabIndex = 0;
            this.tvPlaces.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvPlaces_BeforeCollapse);
            this.tvPlaces.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvPlaces_BeforeExpand);
            this.tvPlaces.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvPlaces_AfterSelect);
            this.tvPlaces.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tvPlaces_NodeMouseDoubleClick);
            this.tvPlaces.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tvPlaces_MouseDown);
            // 
            // mapBox1
            // 
            this.mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.mapBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.mapBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBox1.FineZoomFactor = 10D;
            this.mapBox1.Location = new System.Drawing.Point(0, 25);
            this.mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.QueryGrowFactor = 5F;
            this.mapBox1.QueryLayerIndex = 0;
            this.mapBox1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.ShowProgressUpdate = true;
            this.mapBox1.Size = new System.Drawing.Size(779, 434);
            this.mapBox1.TabIndex = 2;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            this.mapBox1.MapZoomChanged += new SharpMap.Forms.MapBox.MapZoomHandler(this.mapBox1_MapZoomChanged);
            this.mapBox1.MapCenterChanged += new SharpMap.Forms.MapBox.MapCenterChangedHandler(this.mapBox1_MapCenterChanged);
            this.mapBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mapBox1_MouseDoubleClick);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(651, 25);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "© Google - Terms of Use";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // mapZoomToolStrip
            // 
            this.mapZoomToolStrip.Enabled = false;
            this.mapZoomToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mapZoomToolStrip.MapControl = this.mapBox1;
            this.mapZoomToolStrip.Name = "mapZoomToolStrip";
            this.mapZoomToolStrip.Size = new System.Drawing.Size(779, 25);
            this.mapZoomToolStrip.TabIndex = 1;
            this.mapZoomToolStrip.Text = "mapZoomToolStrip1";
            // 
            // dgFacts
            // 
            this.dgFacts.AllowUserToAddRows = false;
            this.dgFacts.AllowUserToDeleteRows = false;
            this.dgFacts.AllowUserToOrderColumns = true;
            this.dgFacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFacts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FactIcon,
            this.FactsIndividualID,
            this.Forenames,
            this.Surname,
            this.TypeOfFact,
            this.FactDate,
            this.AgeAtFact,
            this.FactLocation,
            this.LocationIcon,
            this.Latitude,
            this.Longitude,
            this.GeocodeStatus,
            this.GoogleLocation,
            this.GoogleResultTypes,
            this.Comment,
            this.SourceList});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFacts.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgFacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFacts.Location = new System.Drawing.Point(0, 0);
            this.dgFacts.Name = "dgFacts";
            this.dgFacts.ReadOnly = true;
            this.dgFacts.RowHeadersWidth = 16;
            this.dgFacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFacts.ShowEditingIcon = false;
            this.dgFacts.Size = new System.Drawing.Size(1113, 105);
            this.dgFacts.TabIndex = 3;
            this.dgFacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFacts_CellDoubleClick);
            this.dgFacts.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dgFacts_CellToolTipTextNeeded);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtCount,
            this.progressbar});
            this.statusStrip.Location = new System.Drawing.Point(0, 105);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1113, 22);
            this.statusStrip.TabIndex = 4;
            this.statusStrip.Text = "statusStrip1";
            // 
            // txtCount
            // 
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(0, 17);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(1113, 24);
            this.menuStrip.TabIndex = 19;
            this.menuStrip.Text = "menuStrip1";
            this.menuStrip.Visible = false;
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Visible = false;
            // 
            // FactIcon
            // 
            this.FactIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactIcon.DataPropertyName = "Icon";
            this.FactIcon.HeaderText = global::FTAnalyzer.Properties.Resources.FTA_0002;
            this.FactIcon.MinimumWidth = 20;
            this.FactIcon.Name = "FactIcon";
            this.FactIcon.ReadOnly = true;
            this.FactIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FactIcon.Width = 20;
            // 
            // FactsIndividualID
            // 
            this.FactsIndividualID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactsIndividualID.DataPropertyName = "IndividualID";
            this.FactsIndividualID.HeaderText = "Ind. ID";
            this.FactsIndividualID.Name = "FactsIndividualID";
            this.FactsIndividualID.ReadOnly = true;
            this.FactsIndividualID.Width = 50;
            // 
            // Forenames
            // 
            this.Forenames.DataPropertyName = "Forenames";
            this.Forenames.HeaderText = "Forenames";
            this.Forenames.MinimumWidth = 100;
            this.Forenames.Name = "Forenames";
            this.Forenames.ReadOnly = true;
            // 
            // Surname
            // 
            this.Surname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Surname.DataPropertyName = "Surname";
            this.Surname.HeaderText = "Surname";
            this.Surname.MinimumWidth = 75;
            this.Surname.Name = "Surname";
            this.Surname.ReadOnly = true;
            this.Surname.Width = 75;
            // 
            // TypeOfFact
            // 
            this.TypeOfFact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TypeOfFact.DataPropertyName = "TypeOfFact";
            this.TypeOfFact.HeaderText = "Fact Type";
            this.TypeOfFact.MinimumWidth = 80;
            this.TypeOfFact.Name = "TypeOfFact";
            this.TypeOfFact.ReadOnly = true;
            this.TypeOfFact.Width = 80;
            // 
            // FactDate
            // 
            this.FactDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactDate.DataPropertyName = "FactDate";
            this.FactDate.HeaderText = "Fact Date";
            this.FactDate.MinimumWidth = 150;
            this.FactDate.Name = "FactDate";
            this.FactDate.ReadOnly = true;
            this.FactDate.Width = 150;
            // 
            // AgeAtFact
            // 
            this.AgeAtFact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AgeAtFact.DataPropertyName = "AgeAtFact";
            this.AgeAtFact.HeaderText = "Age";
            this.AgeAtFact.MinimumWidth = 50;
            this.AgeAtFact.Name = "AgeAtFact";
            this.AgeAtFact.ReadOnly = true;
            this.AgeAtFact.Width = 50;
            // 
            // FactLocation
            // 
            this.FactLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactLocation.DataPropertyName = "Location";
            this.FactLocation.HeaderText = "Location";
            this.FactLocation.MinimumWidth = 150;
            this.FactLocation.Name = "FactLocation";
            this.FactLocation.ReadOnly = true;
            this.FactLocation.Width = 150;
            // 
            // LocationIcon
            // 
            this.LocationIcon.DataPropertyName = "LocationIcon";
            this.LocationIcon.HeaderText = global::FTAnalyzer.Properties.Resources.FTA_0002;
            this.LocationIcon.MinimumWidth = 20;
            this.LocationIcon.Name = "LocationIcon";
            this.LocationIcon.ReadOnly = true;
            this.LocationIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LocationIcon.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.LocationIcon.Width = 20;
            // 
            // Latitude
            // 
            this.Latitude.DataPropertyName = "Latitude";
            this.Latitude.HeaderText = "Latitude";
            this.Latitude.Name = "Latitude";
            this.Latitude.ReadOnly = true;
            // 
            // Longitude
            // 
            this.Longitude.DataPropertyName = "Longitude";
            this.Longitude.HeaderText = "Longitude";
            this.Longitude.Name = "Longitude";
            this.Longitude.ReadOnly = true;
            // 
            // GeocodeStatus
            // 
            this.GeocodeStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GeocodeStatus.DataPropertyName = "GeocodeStatus";
            this.GeocodeStatus.HeaderText = "Geocode Status";
            this.GeocodeStatus.Name = "GeocodeStatus";
            this.GeocodeStatus.ReadOnly = true;
            // 
            // GoogleLocation
            // 
            this.GoogleLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GoogleLocation.DataPropertyName = "GoogleLocation";
            this.GoogleLocation.HeaderText = "GoogleLocation";
            this.GoogleLocation.MinimumWidth = 120;
            this.GoogleLocation.Name = "GoogleLocation";
            this.GoogleLocation.ReadOnly = true;
            this.GoogleLocation.Width = 120;
            // 
            // GoogleResultTypes
            // 
            this.GoogleResultTypes.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GoogleResultTypes.DataPropertyName = "GoogleResultTypes";
            this.GoogleResultTypes.HeaderText = "Google Result Types";
            this.GoogleResultTypes.Name = "GoogleResultTypes";
            this.GoogleResultTypes.ReadOnly = true;
            // 
            // Comment
            // 
            this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.MinimumWidth = 120;
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Width = 120;
            // 
            // SourceList
            // 
            this.SourceList.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SourceList.DataPropertyName = "SourceList";
            this.SourceList.HeaderText = "Sources";
            this.SourceList.MinimumWidth = 120;
            this.SourceList.Name = "SourceList";
            this.SourceList.ReadOnly = true;
            this.SourceList.Width = 250;
            // 
            // progressbar
            // 
            this.progressbar.Name = "progressbar";
            this.progressbar.Size = new System.Drawing.Size(100, 16);
            this.progressbar.Visible = false;
            // 
            // Places
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 590);
            this.Controls.Add(this.splitContainerFacts);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "Places";
            this.Text = "Places";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Places_FormClosed);
            this.Load += new System.EventHandler(this.Places_Load);
            this.splitContainerFacts.Panel1.ResumeLayout(false);
            this.splitContainerFacts.Panel2.ResumeLayout(false);
            this.splitContainerFacts.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFacts)).EndInit();
            this.splitContainerFacts.ResumeLayout(false);
            this.splitContainerMap.Panel1.ResumeLayout(false);
            this.splitContainerMap.Panel2.ResumeLayout(false);
            this.splitContainerMap.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMap)).EndInit();
            this.splitContainerMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFacts)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStripMapSelector mnuMapStyle = new ToolStripMapSelector();
        private System.Windows.Forms.SplitContainer splitContainerFacts;
        private System.Windows.Forms.SplitContainer splitContainerMap;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private SharpMap.Forms.MapBox mapBox1;
        private SharpMap.Forms.ToolBar.MapZoomToolStrip mapZoomToolStrip;
        private System.Windows.Forms.DataGridView dgFacts;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel txtCount;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.TreeView tvPlaces;
        private System.Windows.Forms.DataGridViewImageColumn FactIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactsIndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Forenames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeOfFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeAtFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactLocation;
        private System.Windows.Forms.DataGridViewImageColumn LocationIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodeStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleResultTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceList;
        private System.Windows.Forms.ToolStripProgressBar progressbar;
    }
}