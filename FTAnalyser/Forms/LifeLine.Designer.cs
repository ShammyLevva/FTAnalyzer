using FTAnalyzer.Forms.Controls;
using System;

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
            try
            {
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
                labelLayer.Dispose();
                linesLayer.Dispose();
                points.Dispose();
                selections.Dispose();
                lifelines.Dispose();
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LifeLine));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.tbOpacity = new System.Windows.Forms.TrackBar();
            this.mapBox1 = new SharpMap.Forms.MapBox();
            this.mapZoomToolStrip = new SharpMap.Forms.ToolBar.MapZoomToolStrip(this.components);
            this.btnSelect = new System.Windows.Forms.ToolStripButton();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
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
            this.FoundLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FoundResultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.txtCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hideLabelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHideScaleBar = new System.Windows.Forms.ToolStripMenuItem();
            this.resetFormToDefaultSizeAndPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapTooltip = new System.Windows.Forms.ToolTip(this.components);
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
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).BeginInit();
            this.mapZoomToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFacts)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerFacts
            // 
            this.splitContainerFacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerFacts.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainerFacts.Location = new System.Drawing.Point(0, 24);
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
            this.splitContainerFacts.Size = new System.Drawing.Size(1123, 620);
            this.splitContainerFacts.SplitterDistance = 450;
            this.splitContainerFacts.TabIndex = 18;
            this.splitContainerFacts.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainerFacts_SplitterMoved);
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
            this.splitContainerMap.Panel1.Controls.Add(this.dgIndividuals);
            // 
            // splitContainerMap.Panel2
            // 
            this.splitContainerMap.Panel2.Controls.Add(this.tbOpacity);
            this.splitContainerMap.Panel2.Controls.Add(this.mapBox1);
            this.splitContainerMap.Panel2.Controls.Add(this.mapZoomToolStrip);
            this.splitContainerMap.Panel2.Controls.Add(this.linkLabel1);
            this.splitContainerMap.Size = new System.Drawing.Size(1123, 450);
            this.splitContainerMap.SplitterDistance = 330;
            this.splitContainerMap.TabIndex = 2;
            this.splitContainerMap.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.SplitContainerMap_SplitterMoved);
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
            this.dgIndividuals.Size = new System.Drawing.Size(330, 450);
            this.dgIndividuals.TabIndex = 0;
            this.dgIndividuals.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.DgIndividuals_CellToolTipTextNeeded);
            this.dgIndividuals.SelectionChanged += new System.EventHandler(this.DgIndividuals_SelectionChanged);
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
            this.ctxmnuSelectOthers.Size = new System.Drawing.Size(244, 92);
            // 
            // addAllFamilyMembersToolStripMenuItem
            // 
            this.addAllFamilyMembersToolStripMenuItem.Name = "addAllFamilyMembersToolStripMenuItem";
            this.addAllFamilyMembersToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.addAllFamilyMembersToolStripMenuItem.Text = "Select all Family Members";
            this.addAllFamilyMembersToolStripMenuItem.Click += new System.EventHandler(this.AddAllFamilyMembersToolStripMenuItem_Click);
            // 
            // selectAllAncestorsToolStripMenuItem
            // 
            this.selectAllAncestorsToolStripMenuItem.Name = "selectAllAncestorsToolStripMenuItem";
            this.selectAllAncestorsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.selectAllAncestorsToolStripMenuItem.Text = "Select all Ancestors";
            this.selectAllAncestorsToolStripMenuItem.Click += new System.EventHandler(this.SelectAllAncestorsToolStripMenuItem_Click);
            // 
            // selectAllDescendantsToolStripMenuItem
            // 
            this.selectAllDescendantsToolStripMenuItem.Name = "selectAllDescendantsToolStripMenuItem";
            this.selectAllDescendantsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.selectAllDescendantsToolStripMenuItem.Text = "Select all Descendants";
            this.selectAllDescendantsToolStripMenuItem.Click += new System.EventHandler(this.SelectAllDescendantsToolStripMenuItem_Click);
            // 
            // selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem
            // 
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem.Name = "selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem";
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem.Size = new System.Drawing.Size(243, 22);
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem.Text = "Select all Relations (all of above)";
            this.selectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem.Click += new System.EventHandler(this.SelectAllRelationsfamilyAncestorsDescendantsToolStripMenuItem_Click);
            // 
            // tbOpacity
            // 
            this.tbOpacity.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tbOpacity.LargeChange = 20;
            this.tbOpacity.Location = new System.Drawing.Point(3, 402);
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
            // mapBox1
            // 
            this.mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            this.mapBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.mapBox1.CustomTool = null;
            this.mapBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mapBox1.FineZoomFactor = 10D;
            this.mapBox1.Location = new System.Drawing.Point(0, 25);
            this.mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            this.mapBox1.Name = "mapBox1";
            this.mapBox1.QueryGrowFactor = 5F;
            this.mapBox1.QueryLayerIndex = 2;
            this.mapBox1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(244)))), ((int)(((byte)(244)))));
            this.mapBox1.ShowProgressUpdate = true;
            this.mapBox1.Size = new System.Drawing.Size(789, 425);
            this.mapBox1.TabIndex = 2;
            this.mapBox1.Text = "mapBox1";
            this.mapBox1.WheelZoomMagnitude = -2D;
            this.mapBox1.MouseMove += new SharpMap.Forms.MapBox.MouseEventHandler(this.MapBox1_MouseMove);
            this.mapBox1.MapQueried += new SharpMap.Forms.MapBox.MapQueryHandler(this.MapBox1_MapQueried);
            this.mapBox1.ActiveToolChanged += new SharpMap.Forms.MapBox.ActiveToolChangedHandler(this.MapBox1_ActiveToolChanged);
            // 
            // mapZoomToolStrip
            // 
            this.mapZoomToolStrip.Enabled = false;
            this.mapZoomToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSelect});
            this.mapZoomToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mapZoomToolStrip.MapControl = this.mapBox1;
            this.mapZoomToolStrip.Name = "mapZoomToolStrip";
            this.mapZoomToolStrip.Size = new System.Drawing.Size(789, 25);
            this.mapZoomToolStrip.TabIndex = 1;
            this.mapZoomToolStrip.Text = "mapZoomToolStrip1";
            // 
            // btnSelect
            // 
            this.btnSelect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnSelect.Image = ((System.Drawing.Image)(resources.GetObject("btnSelect.Image")));
            this.btnSelect.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(23, 22);
            this.btnSelect.Text = "Point Selection ";
            this.btnSelect.Click += new System.EventHandler(this.BtnSelect_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(661, 25);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(125, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "© Google - Terms of Use";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel1_LinkClicked);
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
            this.FactName,
            this.TypeOfFact,
            this.FactDate,
            this.AgeAtFact,
            this.FactLocation,
            this.LocationIcon,
            this.GeocodeStatus,
            this.FoundLocation,
            this.FoundResultType,
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
            this.dgFacts.Size = new System.Drawing.Size(1123, 144);
            this.dgFacts.TabIndex = 3;
            this.dgFacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgFacts_CellDoubleClick);
            this.dgFacts.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.DgFacts_CellToolTipTextNeeded);
            this.dgFacts.SelectionChanged += new System.EventHandler(this.DgFacts_SelectionChanged);
            // 
            // FactIcon
            // 
            this.FactIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactIcon.DataPropertyName = "Icon";
            this.FactIcon.HeaderText = "";
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
            this.FactLocation.MinimumWidth = 200;
            this.FactLocation.Name = "FactLocation";
            this.FactLocation.ReadOnly = true;
            this.FactLocation.Width = 250;
            // 
            // LocationIcon
            // 
            this.LocationIcon.DataPropertyName = "LocationIcon";
            this.LocationIcon.HeaderText = "";
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
            // FoundLocation
            // 
            this.FoundLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FoundLocation.DataPropertyName = "FoundLocation";
            this.FoundLocation.HeaderText = "FoundLocation";
            this.FoundLocation.MinimumWidth = 120;
            this.FoundLocation.Name = "FoundLocation";
            this.FoundLocation.ReadOnly = true;
            this.FoundLocation.Width = 120;
            // 
            // FoundResultType
            // 
            this.FoundResultType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FoundResultType.DataPropertyName = "FoundResultType";
            this.FoundResultType.HeaderText = "Found Result Type";
            this.FoundResultType.Name = "FoundResultType";
            this.FoundResultType.ReadOnly = true;
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
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtCount});
            this.statusStrip.Location = new System.Drawing.Point(0, 144);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1123, 22);
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
            this.menuStrip.Size = new System.Drawing.Size(1123, 24);
            this.menuStrip.TabIndex = 19;
            this.menuStrip.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hideLabelsToolStripMenuItem,
            this.mnuHideScaleBar,
            this.resetFormToDefaultSizeAndPositionToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // hideLabelsToolStripMenuItem
            // 
            this.hideLabelsToolStripMenuItem.CheckOnClick = true;
            this.hideLabelsToolStripMenuItem.Name = "hideLabelsToolStripMenuItem";
            this.hideLabelsToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.hideLabelsToolStripMenuItem.Text = "Hide Labels";
            this.hideLabelsToolStripMenuItem.Click += new System.EventHandler(this.HideLabelsToolStripMenuItem_Click);
            // 
            // mnuHideScaleBar
            // 
            this.mnuHideScaleBar.CheckOnClick = true;
            this.mnuHideScaleBar.Name = "mnuHideScaleBar";
            this.mnuHideScaleBar.Size = new System.Drawing.Size(276, 22);
            this.mnuHideScaleBar.Text = "Hide Scale Bar";
            this.mnuHideScaleBar.Click += new System.EventHandler(this.MnuHideScaleBar_Click);
            // 
            // resetFormToDefaultSizeAndPositionToolStripMenuItem
            // 
            this.resetFormToDefaultSizeAndPositionToolStripMenuItem.Name = "resetFormToDefaultSizeAndPositionToolStripMenuItem";
            this.resetFormToDefaultSizeAndPositionToolStripMenuItem.Size = new System.Drawing.Size(276, 22);
            this.resetFormToDefaultSizeAndPositionToolStripMenuItem.Text = "Reset form to default size and position";
            this.resetFormToDefaultSizeAndPositionToolStripMenuItem.Click += new System.EventHandler(this.ResetFormToDefaultSizeAndPositionToolStripMenuItem_Click);
            // 
            // mapTooltip
            // 
            this.mapTooltip.AutoPopDelay = 5000;
            this.mapTooltip.InitialDelay = 500;
            this.mapTooltip.ReshowDelay = 100;
            // 
            // LifeLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1123, 644);
            this.Controls.Add(this.splitContainerFacts);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "LifeLine";
            this.Text = "Lifeline";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LifeLine_FormClosed);
            this.Load += new System.EventHandler(this.LifeLine_Load);
            this.Move += new System.EventHandler(this.LifeLine_Move);
            this.Resize += new System.EventHandler(this.LifeLine_Resize);
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
            ((System.ComponentModel.ISupportInitialize)(this.tbOpacity)).EndInit();
            this.mapZoomToolStrip.ResumeLayout(false);
            this.mapZoomToolStrip.PerformLayout();
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
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hideLabelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuHideScaleBar;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private System.Windows.Forms.ToolTip mapTooltip;
        private System.Windows.Forms.DataGridViewImageColumn FactIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactsIndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactName;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeOfFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeAtFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactLocation;
        private System.Windows.Forms.DataGridViewImageColumn LocationIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodeStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoundLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoundResultType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceList;
        private System.Windows.Forms.ToolStripMenuItem resetFormToDefaultSizeAndPositionToolStripMenuItem;
        private System.Windows.Forms.TrackBar tbOpacity;
    }
}