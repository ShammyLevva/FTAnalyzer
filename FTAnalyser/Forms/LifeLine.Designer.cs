﻿using FTAnalyzer.Forms.Controls;
namespace FTAnalyzer.Forms
{
    partial class LifeLine
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LifeLine));
            this.splitContainerFacts = new System.Windows.Forms.SplitContainer();
            this.splitContainerMap = new System.Windows.Forms.SplitContainer();
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.IndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeoLocationCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ctxmnuSelectOthers = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAllFamilyMembersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllAncestorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllDescendantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.mapZoomToolStrip = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.txtCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.dgFacts = new System.Windows.Forms.DataGridView();
            this.FactIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.FactsIndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeOfFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgeAtFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.GeocodeStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleResultTypes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerFacts)).BeginInit();
            this.splitContainerFacts.Panel1.SuspendLayout();
            this.splitContainerFacts.Panel2.SuspendLayout();
            this.splitContainerFacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerMap)).BeginInit();
            this.splitContainerMap.Panel1.SuspendLayout();
            this.splitContainerMap.Panel2.SuspendLayout();
            this.splitContainerMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.ctxmnuSelectOthers.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFacts)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerFacts
            // 
            this.splitContainerFacts.Dock = System.Windows.Forms.DockStyle.Fill;
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
            this.splitContainerFacts.Panel2.Controls.Add(this.statusStrip);
            this.splitContainerFacts.Panel2.Controls.Add(this.dgFacts);
            this.splitContainerFacts.Size = new System.Drawing.Size(1113, 590);
            this.splitContainerFacts.SplitterDistance = 459;
            this.splitContainerFacts.TabIndex = 18;
            // 
            // splitContainerMap
            // 
            this.splitContainerMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerMap.Location = new System.Drawing.Point(0, 0);
            this.splitContainerMap.Name = "splitContainerMap";
            // 
            // splitContainerMap.Panel1
            // 
            this.splitContainerMap.Panel1.Controls.Add(this.dgIndividuals);
            // 
            // splitContainerMap.Panel2
            // 
            this.splitContainerMap.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainerMap.Panel2.Controls.Add(this.mapBox1);
            this.splitContainerMap.Panel2.Controls.Add(this.mapZoomToolStrip);
            this.splitContainerMap.Size = new System.Drawing.Size(1113, 459);
            this.splitContainerMap.SplitterDistance = 330;
            this.splitContainerMap.TabIndex = 2;
            // 
            // dgIndividuals
            // 
            this.dgIndividuals.AllowUserToAddRows = false;
            this.dgIndividuals.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dgIndividuals.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndividualID,
            this.SortedName,
            this.BirthDate,
            this.GeoLocationCount});
            this.dgIndividuals.ContextMenuStrip = this.ctxmnuSelectOthers;
            this.dgIndividuals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgIndividuals.Location = new System.Drawing.Point(0, 0);
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.ReadOnly = true;
            this.dgIndividuals.RowHeadersWidth = 4;
            this.dgIndividuals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgIndividuals.Size = new System.Drawing.Size(330, 459);
            this.dgIndividuals.TabIndex = 0;
            this.dgIndividuals.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dgIndividuals_CellToolTipTextNeeded);
            this.dgIndividuals.SelectionChanged += new System.EventHandler(this.dgIndividuals_SelectionChanged);
            // 
            // IndividualID
            // 
            this.IndividualID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IndividualID.DataPropertyName = "IndividualID";
            this.IndividualID.HeaderText = "Ind. ID";
            this.IndividualID.MinimumWidth = 40;
            this.IndividualID.Name = "IndividualID";
            this.IndividualID.ReadOnly = true;
            this.IndividualID.Width = 40;
            // 
            // SortedName
            // 
            this.SortedName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SortedName.DataPropertyName = "SortedName";
            this.SortedName.HeaderText = "Name";
            this.SortedName.MinimumWidth = 50;
            this.SortedName.Name = "SortedName";
            this.SortedName.ReadOnly = true;
            this.SortedName.Width = 130;
            // 
            // BirthDate
            // 
            this.BirthDate.DataPropertyName = "BirthDate";
            this.BirthDate.HeaderText = "Date of Birth";
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.ReadOnly = true;
            // 
            // GeoLocationCount
            // 
            this.GeoLocationCount.DataPropertyName = "GeoLocationCount";
            this.GeoLocationCount.HeaderText = "Facts";
            this.GeoLocationCount.MinimumWidth = 37;
            this.GeoLocationCount.Name = "GeoLocationCount";
            this.GeoLocationCount.ReadOnly = true;
            this.GeoLocationCount.Width = 37;
            // 
            // ctxmnuSelectOthers
            // 
            this.ctxmnuSelectOthers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAllFamilyMembersToolStripMenuItem,
            this.selectAllAncestorsToolStripMenuItem,
            this.selectAllDescendantsToolStripMenuItem,
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem});
            this.ctxmnuSelectOthers.Name = "ctxmnuSelectOthers";
            this.ctxmnuSelectOthers.Size = new System.Drawing.Size(244, 114);
            // 
            // addAllFamilyMembersToolStripMenuItem
            // 
            this.addAllFamilyMembersToolStripMenuItem.Name = "addAllFamilyMembersToolStripMenuItem";
            this.addAllFamilyMembersToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.addAllFamilyMembersToolStripMenuItem.Text = "Select all Family Members";
            this.addAllFamilyMembersToolStripMenuItem.Click += new System.EventHandler(this.addAllFamilyMembersToolStripMenuItem_Click);
            // 
            // selectAllAncestorsToolStripMenuItem
            // 
            this.selectAllAncestorsToolStripMenuItem.Name = "selectAllAncestorsToolStripMenuItem";
            this.selectAllAncestorsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.selectAllAncestorsToolStripMenuItem.Text = "Select all Ancestors";
            this.selectAllAncestorsToolStripMenuItem.Click += new System.EventHandler(this.selectAllAncestorsToolStripMenuItem_Click);
            // 
            // selectAllDescendantsToolStripMenuItem
            // 
            this.selectAllDescendantsToolStripMenuItem.Name = "selectAllDescendantsToolStripMenuItem";
            this.selectAllDescendantsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.selectAllDescendantsToolStripMenuItem.Text = "Select all Descendants";
            this.selectAllDescendantsToolStripMenuItem.Click += new System.EventHandler(this.selectAllDescendantsToolStripMenuItem_Click);
            // 
            // selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem
            // 
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem.Name = "selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem";
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem.Text = "Select all Relations (all of above)";
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem.Click += new System.EventHandler(this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem_Click);
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
            this.mapBox1.ShowProgressUpdate = false;
            this.mapBox1.Size = new System.Drawing.Size(779, 434);
            this.mapBox1.TabIndex = 2;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
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
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtCount});
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
            // dgFacts
            // 
            this.dgFacts.AllowUserToAddRows = false;
            this.dgFacts.AllowUserToDeleteRows = false;
            this.dgFacts.AllowUserToOrderColumns = true;
            this.dgFacts.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgFacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFacts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FactIcon,
            this.FactsIndividualID,
            this.FactName,
            this.TypeOfFact,
            this.FactDate,
            this.AgeAtFact,
            this.FactLocation,
            this.LocationIcon,
            this.GeocodeStatus,
            this.GoogleLocation,
            this.GoogleResultTypes,
            this.Comment,
            this.SourceList});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFacts.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgFacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFacts.Location = new System.Drawing.Point(0, 0);
            this.dgFacts.Name = "dgFacts";
            this.dgFacts.ReadOnly = true;
            this.dgFacts.RowHeadersWidth = 16;
            this.dgFacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFacts.ShowEditingIcon = false;
            this.dgFacts.Size = new System.Drawing.Size(1113, 127);
            this.dgFacts.TabIndex = 3;
            this.dgFacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFacts_CellDoubleClick);
            this.dgFacts.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dgFacts_CellToolTipTextNeeded);
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
            // FactName
            // 
            this.FactName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactName.DataPropertyName = "Name";
            this.FactName.HeaderText = "Name";
            this.FactName.MinimumWidth = 150;
            this.FactName.Name = "FactName";
            this.FactName.ReadOnly = true;
            this.FactName.Width = 150;
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
            this.FactLocation.MinimumWidth = 120;
            this.FactLocation.Name = "FactLocation";
            this.FactLocation.ReadOnly = true;
            this.FactLocation.Width = 123;
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
            // LifeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1113, 590);
            this.Controls.Add(this.splitContainerFacts);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "LifeLine";
            this.Text = "Lifeline";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LifeLine_FormClosed);
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
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            this.ctxmnuSelectOthers.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFacts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ToolStripMapSelector mnuMapStyle = new ToolStripMapSelector();
        private System.Windows.Forms.SplitContainer splitContainerFacts;
        private System.Windows.Forms.SplitContainer splitContainerMap;
        private System.Windows.Forms.DataGridView dgIndividuals;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private SharpMap.Forms.MapBox mapBox1;
        private SharpMap.Forms.ToolBar.MapZoomToolStrip mapZoomToolStrip;
        private System.Windows.Forms.DataGridView dgFacts;
        private System.Windows.Forms.ContextMenuStrip ctxmnuSelectOthers;
        private System.Windows.Forms.ToolStripMenuItem addAllFamilyMembersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllAncestorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAllDescendantsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel txtCount;
        private System.Windows.Forms.ToolStripMenuItem selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortedName;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeoLocationCount;
        private System.Windows.Forms.DataGridViewImageColumn FactIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactsIndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeOfFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeAtFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactLocation;
        private System.Windows.Forms.DataGridViewImageColumn LocationIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodeStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleResultTypes;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceList;
    }
}