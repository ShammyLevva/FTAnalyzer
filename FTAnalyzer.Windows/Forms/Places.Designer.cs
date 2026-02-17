using FTAnalyzer.Forms.Controls;
using FTAnalyzer.Utilities;
using System;
using System.Windows.Forms;

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
            try
            {
                if (disposing && (components is not null))
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Places));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            splitContainerFacts = new SplitContainer();
            splitContainerMap = new SplitContainer();
            tvPlaces = new MultiSelectTreeview();
            tbOpacity = new TrackBar();
            mapBox1 = new SharpMap.Forms.MapBox();
            mapZoomToolStrip = new SharpMap.Forms.ToolBar.MapZoomToolStrip(components);
            btnSelect = new ToolStripButton();
            linkLabel1 = new LinkLabel();
            dgFacts = new DataGridView();
            statusStrip = new StatusStrip();
            txtCount = new ToolStripStatusLabel();
            pbPlaces = new ToolStripProgressBar();
            menuStrip = new MenuStrip();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            mnuHideScaleBar = new ToolStripMenuItem();
            resetFormDefaultSizeAndPositionToolStripMenuItem = new ToolStripMenuItem();
            FactIcon = new DataGridViewImageColumn();
            FactsIndividualID = new DataGridViewTextBoxColumn();
            Forenames = new DataGridViewTextBoxColumn();
            Surname = new DataGridViewTextBoxColumn();
            TypeOfFact = new DataGridViewTextBoxColumn();
            FactDate = new DataGridViewTextBoxColumn();
            AgeAtFact = new DataGridViewTextBoxColumn();
            FactLocation = new DataGridViewTextBoxColumn();
            LocationIcon = new DataGridViewImageColumn();
            Latitude = new DataGridViewTextBoxColumn();
            Longitude = new DataGridViewTextBoxColumn();
            GeocodeStatus = new DataGridViewTextBoxColumn();
            FoundLocation = new DataGridViewTextBoxColumn();
            FoundResultType = new DataGridViewTextBoxColumn();
            Comment = new DataGridViewTextBoxColumn();
            SourceList = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)splitContainerFacts).BeginInit();
            splitContainerFacts.Panel1.SuspendLayout();
            splitContainerFacts.Panel2.SuspendLayout();
            splitContainerFacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMap).BeginInit();
            splitContainerMap.Panel1.SuspendLayout();
            splitContainerMap.Panel2.SuspendLayout();
            splitContainerMap.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbOpacity).BeginInit();
            mapZoomToolStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgFacts).BeginInit();
            statusStrip.SuspendLayout();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerFacts
            // 
            splitContainerFacts.Dock = DockStyle.Fill;
            splitContainerFacts.FixedPanel = FixedPanel.Panel2;
            splitContainerFacts.Location = new Point(0, 24);
            splitContainerFacts.Margin = new Padding(4);
            splitContainerFacts.Name = "splitContainerFacts";
            splitContainerFacts.Orientation = Orientation.Horizontal;
            // 
            // splitContainerFacts.Panel1
            // 
            splitContainerFacts.Panel1.Controls.Add(splitContainerMap);
            // 
            // splitContainerFacts.Panel2
            // 
            splitContainerFacts.Panel2.Controls.Add(dgFacts);
            splitContainerFacts.Panel2.Controls.Add(statusStrip);
            splitContainerFacts.Size = new Size(1265, 637);
            splitContainerFacts.SplitterDistance = 452;
            splitContainerFacts.TabIndex = 18;
            splitContainerFacts.SplitterMoved += SplitContainerFacts_SplitterMoved;
            // 
            // splitContainerMap
            // 
            splitContainerMap.Dock = DockStyle.Fill;
            splitContainerMap.FixedPanel = FixedPanel.Panel1;
            splitContainerMap.Location = new Point(0, 0);
            splitContainerMap.Margin = new Padding(4);
            splitContainerMap.Name = "splitContainerMap";
            // 
            // splitContainerMap.Panel1
            // 
            splitContainerMap.Panel1.Controls.Add(tvPlaces);
            // 
            // splitContainerMap.Panel2
            // 
            splitContainerMap.Panel2.Controls.Add(tbOpacity);
            splitContainerMap.Panel2.Controls.Add(mapBox1);
            splitContainerMap.Panel2.Controls.Add(mapZoomToolStrip);
            splitContainerMap.Panel2.Controls.Add(linkLabel1);
            splitContainerMap.Size = new Size(1265, 452);
            splitContainerMap.SplitterDistance = 127;
            splitContainerMap.TabIndex = 2;
            splitContainerMap.SplitterMoved += SplitContainerMap_SplitterMoved;
            // 
            // tvPlaces
            // 
            tvPlaces.Dock = DockStyle.Fill;
            tvPlaces.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            tvPlaces.HideSelection = false;
            tvPlaces.Location = new Point(0, 0);
            tvPlaces.Margin = new Padding(4);
            tvPlaces.Name = "tvPlaces";
            tvPlaces.SelectedNodes = (List<TreeNode>)resources.GetObject("tvPlaces.SelectedNodes");
            tvPlaces.Size = new Size(127, 452);
            tvPlaces.TabIndex = 0;
            tvPlaces.AfterSelect += TvPlaces_AfterSelect;
            tvPlaces.NodeMouseDoubleClick += TvPlaces_NodeMouseDoubleClick;
            // 
            // tbOpacity
            // 
            tbOpacity.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            tbOpacity.LargeChange = 20;
            tbOpacity.Location = new Point(0, 396);
            tbOpacity.Margin = new Padding(4);
            tbOpacity.Maximum = 100;
            tbOpacity.Name = "tbOpacity";
            tbOpacity.Size = new Size(291, 45);
            tbOpacity.SmallChange = 5;
            tbOpacity.TabIndex = 17;
            tbOpacity.TickFrequency = 10;
            tbOpacity.TickStyle = TickStyle.TopLeft;
            tbOpacity.Value = 100;
            tbOpacity.Scroll += TbOpacity_Scroll;
            // 
            // mapBox1
            // 
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.None;
            mapBox1.CustomTool = null;
            mapBox1.Dock = DockStyle.Fill;
            mapBox1.FineZoomFactor = 10D;
            mapBox1.Location = new Point(0, 35);
            mapBox1.MapQueryMode = SharpMap.Forms.MapBox.MapQueryType.LayerByIndex;
            mapBox1.Margin = new Padding(4);
            mapBox1.Name = "mapBox1";
            mapBox1.QueryGrowFactor = 5F;
            mapBox1.QueryLayerIndex = 0;
            mapBox1.SelectionBackColor = Color.FromArgb(210, 244, 244, 244);
            mapBox1.SelectionForeColor = Color.FromArgb(244, 244, 244);
            mapBox1.ShowProgressUpdate = true;
            mapBox1.Size = new Size(1134, 417);
            mapBox1.TabIndex = 2;
            mapBox1.Text = "mapBox1";
            mapBox1.WheelZoomMagnitude = -2D;
            mapBox1.MapZoomChanged += MapBox1_MapZoomChanged;
            mapBox1.MapQueried += MapBox1_MapQueried;
            mapBox1.MapCenterChanged += MapBox1_MapCenterChanged;
            mapBox1.ActiveToolChanged += MapBox1_ActiveToolChanged;
            mapBox1.MouseDoubleClick += MapBox1_MouseDoubleClick;
            // 
            // mapZoomToolStrip
            // 
            mapZoomToolStrip.Enabled = false;
            mapZoomToolStrip.ImageScalingSize = new Size(28, 28);
            mapZoomToolStrip.Items.AddRange(new ToolStripItem[] { btnSelect });
            mapZoomToolStrip.Location = new Point(0, 0);
            mapZoomToolStrip.MapControl = mapBox1;
            mapZoomToolStrip.Name = "mapZoomToolStrip";
            mapZoomToolStrip.Padding = new Padding(0, 0, 3, 0);
            mapZoomToolStrip.Size = new Size(1134, 35);
            mapZoomToolStrip.TabIndex = 1;
            mapZoomToolStrip.Text = "mapZoomToolStrip1";
            // 
            // btnSelect
            // 
            btnSelect.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnSelect.Image = (Image)resources.GetObject("btnSelect.Image");
            btnSelect.ImageTransparentColor = Color.Magenta;
            btnSelect.Name = "btnSelect";
            btnSelect.Size = new Size(32, 32);
            btnSelect.Text = "Location Selection ";
            btnSelect.Click += BtnSelect_Click;
            // 
            // linkLabel1
            // 
            linkLabel1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            linkLabel1.AutoSize = true;
            linkLabel1.Location = new Point(983, 29);
            linkLabel1.Margin = new Padding(4, 0, 4, 0);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(138, 15);
            linkLabel1.TabIndex = 16;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "© Google - Terms of Use";
            linkLabel1.TextAlign = ContentAlignment.TopRight;
            linkLabel1.LinkClicked += LinkLabel1_LinkClicked;
            // 
            // dgFacts
            // 
            dgFacts.AllowUserToAddRows = false;
            dgFacts.AllowUserToDeleteRows = false;
            dgFacts.AllowUserToOrderColumns = true;
            dgFacts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgFacts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgFacts.Columns.AddRange(new DataGridViewColumn[] { FactIcon, FactsIndividualID, Forenames, Surname, TypeOfFact, FactDate, AgeAtFact, FactLocation, LocationIcon, Latitude, Longitude, GeocodeStatus, FoundLocation, FoundResultType, Comment, SourceList });
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgFacts.DefaultCellStyle = dataGridViewCellStyle1;
            dgFacts.Dock = DockStyle.Fill;
            dgFacts.Location = new Point(0, 0);
            dgFacts.Margin = new Padding(4);
            dgFacts.Name = "dgFacts";
            dgFacts.ReadOnly = true;
            dgFacts.RowHeadersWidth = 16;
            dgFacts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgFacts.ShowEditingIcon = false;
            dgFacts.Size = new Size(1265, 159);
            dgFacts.TabIndex = 3;
            dgFacts.CellDoubleClick += DgFacts_CellDoubleClick;
            dgFacts.CellToolTipTextNeeded += DgFacts_CellToolTipTextNeeded;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(28, 28);
            statusStrip.Items.AddRange(new ToolStripItem[] { txtCount, pbPlaces });
            statusStrip.Location = new Point(0, 159);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 17, 0);
            statusStrip.Size = new Size(1265, 22);
            statusStrip.TabIndex = 4;
            statusStrip.Text = "statusStrip1";
            // 
            // txtCount
            // 
            txtCount.Name = "txtCount";
            txtCount.Size = new Size(0, 17);
            // 
            // pbPlaces
            // 
            pbPlaces.Name = "pbPlaces";
            pbPlaces.Size = new Size(116, 19);
            pbPlaces.Visible = false;
            // 
            // menuStrip
            // 
            menuStrip.ImageScalingSize = new Size(28, 28);
            menuStrip.Items.AddRange(new ToolStripItem[] { optionsToolStripMenuItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(7, 2, 0, 2);
            menuStrip.Size = new Size(1265, 24);
            menuStrip.TabIndex = 19;
            menuStrip.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuHideScaleBar, resetFormDefaultSizeAndPositionToolStripMenuItem });
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(61, 20);
            optionsToolStripMenuItem.Text = "Options";
            // 
            // mnuHideScaleBar
            // 
            mnuHideScaleBar.CheckOnClick = true;
            mnuHideScaleBar.Name = "mnuHideScaleBar";
            mnuHideScaleBar.Size = new Size(262, 22);
            mnuHideScaleBar.Text = "Hide Scale Bar";
            mnuHideScaleBar.Click += MnuHideScaleBar_Click;
            // 
            // resetFormDefaultSizeAndPositionToolStripMenuItem
            // 
            resetFormDefaultSizeAndPositionToolStripMenuItem.Name = "resetFormDefaultSizeAndPositionToolStripMenuItem";
            resetFormDefaultSizeAndPositionToolStripMenuItem.Size = new Size(262, 22);
            resetFormDefaultSizeAndPositionToolStripMenuItem.Text = "Reset form default size and position";
            resetFormDefaultSizeAndPositionToolStripMenuItem.Click += ResetFormDefaultSizeAndPositionToolStripMenuItem_Click;
            // 
            // FactIcon
            // 
            FactIcon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FactIcon.DataPropertyName = "Icon";
            FactIcon.HeaderText = "";
            FactIcon.MinimumWidth = 20;
            FactIcon.Name = "FactIcon";
            FactIcon.ReadOnly = true;
            FactIcon.Resizable = DataGridViewTriState.False;
            FactIcon.Width = 20;
            // 
            // FactsIndividualID
            // 
            FactsIndividualID.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FactsIndividualID.DataPropertyName = "IndividualID";
            FactsIndividualID.HeaderText = "Ind. ID";
            FactsIndividualID.MinimumWidth = 9;
            FactsIndividualID.Name = "FactsIndividualID";
            FactsIndividualID.ReadOnly = true;
            FactsIndividualID.Width = 50;
            // 
            // Forenames
            // 
            Forenames.DataPropertyName = "Forenames";
            Forenames.HeaderText = "Forenames";
            Forenames.MinimumWidth = 100;
            Forenames.Name = "Forenames";
            Forenames.ReadOnly = true;
            // 
            // Surname
            // 
            Surname.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Surname.DataPropertyName = "Surname";
            Surname.HeaderText = "Surname";
            Surname.MinimumWidth = 75;
            Surname.Name = "Surname";
            Surname.ReadOnly = true;
            Surname.Width = 75;
            // 
            // TypeOfFact
            // 
            TypeOfFact.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            TypeOfFact.DataPropertyName = "TypeOfFact";
            TypeOfFact.HeaderText = "Fact Type";
            TypeOfFact.MinimumWidth = 80;
            TypeOfFact.Name = "TypeOfFact";
            TypeOfFact.ReadOnly = true;
            TypeOfFact.Width = 80;
            // 
            // FactDate
            // 
            FactDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FactDate.DataPropertyName = "FactDate";
            FactDate.HeaderText = "Fact Date";
            FactDate.MinimumWidth = 150;
            FactDate.Name = "FactDate";
            FactDate.ReadOnly = true;
            FactDate.Width = 150;
            // 
            // AgeAtFact
            // 
            AgeAtFact.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            AgeAtFact.DataPropertyName = "AgeAtFact";
            AgeAtFact.HeaderText = "Age";
            AgeAtFact.MinimumWidth = 50;
            AgeAtFact.Name = "AgeAtFact";
            AgeAtFact.ReadOnly = true;
            AgeAtFact.Width = 50;
            // 
            // FactLocation
            // 
            FactLocation.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FactLocation.DataPropertyName = "Location";
            FactLocation.HeaderText = "Location";
            FactLocation.MinimumWidth = 150;
            FactLocation.Name = "FactLocation";
            FactLocation.ReadOnly = true;
            FactLocation.Width = 150;
            // 
            // LocationIcon
            // 
            LocationIcon.DataPropertyName = "LocationIcon";
            LocationIcon.HeaderText = "";
            LocationIcon.MinimumWidth = 20;
            LocationIcon.Name = "LocationIcon";
            LocationIcon.ReadOnly = true;
            LocationIcon.Resizable = DataGridViewTriState.False;
            LocationIcon.SortMode = DataGridViewColumnSortMode.Automatic;
            LocationIcon.Width = 20;
            // 
            // Latitude
            // 
            Latitude.DataPropertyName = "Latitude";
            Latitude.HeaderText = "Latitude";
            Latitude.MinimumWidth = 9;
            Latitude.Name = "Latitude";
            Latitude.ReadOnly = true;
            Latitude.Width = 75;
            // 
            // Longitude
            // 
            Longitude.DataPropertyName = "Longitude";
            Longitude.HeaderText = "Longitude";
            Longitude.MinimumWidth = 9;
            Longitude.Name = "Longitude";
            Longitude.ReadOnly = true;
            Longitude.Width = 86;
            // 
            // GeocodeStatus
            // 
            GeocodeStatus.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            GeocodeStatus.DataPropertyName = "GeocodeStatus";
            GeocodeStatus.HeaderText = "Geocode Status";
            GeocodeStatus.MinimumWidth = 9;
            GeocodeStatus.Name = "GeocodeStatus";
            GeocodeStatus.ReadOnly = true;
            GeocodeStatus.Width = 175;
            // 
            // FoundLocation
            // 
            FoundLocation.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FoundLocation.DataPropertyName = "FoundLocation";
            FoundLocation.HeaderText = "FoundLocation";
            FoundLocation.MinimumWidth = 120;
            FoundLocation.Name = "FoundLocation";
            FoundLocation.ReadOnly = true;
            FoundLocation.Width = 120;
            // 
            // FoundResultType
            // 
            FoundResultType.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FoundResultType.DataPropertyName = "FoundResultType";
            FoundResultType.HeaderText = "Found Result Type";
            FoundResultType.MinimumWidth = 9;
            FoundResultType.Name = "FoundResultType";
            FoundResultType.ReadOnly = true;
            FoundResultType.Width = 175;
            // 
            // Comment
            // 
            Comment.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Comment.DataPropertyName = "Comment";
            Comment.HeaderText = "Comment";
            Comment.MinimumWidth = 120;
            Comment.Name = "Comment";
            Comment.ReadOnly = true;
            Comment.Width = 120;
            // 
            // SourceList
            // 
            SourceList.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            SourceList.DataPropertyName = "SourceList";
            SourceList.HeaderText = "Sources";
            SourceList.MinimumWidth = 200;
            SourceList.Name = "SourceList";
            SourceList.ReadOnly = true;
            SourceList.Width = 350;
            // 
            // Places
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1265, 661);
            Controls.Add(splitContainerFacts);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip;
            Margin = new Padding(4);
            Name = "Places";
            Text = "Places";
            FormClosed += Places_FormClosed;
            Load += Places_Load;
            Move += Places_Move;
            Resize += Places_Resize;
            splitContainerFacts.Panel1.ResumeLayout(false);
            splitContainerFacts.Panel2.ResumeLayout(false);
            splitContainerFacts.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerFacts).EndInit();
            splitContainerFacts.ResumeLayout(false);
            splitContainerMap.Panel1.ResumeLayout(false);
            splitContainerMap.Panel2.ResumeLayout(false);
            splitContainerMap.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerMap).EndInit();
            splitContainerMap.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)tbOpacity).EndInit();
            mapZoomToolStrip.ResumeLayout(false);
            mapZoomToolStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgFacts).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();

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
        private System.Windows.Forms.ToolStripProgressBar pbPlaces;
        private System.Windows.Forms.ToolStripMenuItem mnuHideScaleBar;
        private System.Windows.Forms.ToolStripButton btnSelect;
        private MultiSelectTreeview tvPlaces;
        private System.Windows.Forms.ToolStripMenuItem resetFormDefaultSizeAndPositionToolStripMenuItem;
        private TrackBar tbOpacity;
        private DataGridViewImageColumn FactIcon;
        private DataGridViewTextBoxColumn FactsIndividualID;
        private DataGridViewTextBoxColumn Forenames;
        private DataGridViewTextBoxColumn Surname;
        private DataGridViewTextBoxColumn TypeOfFact;
        private DataGridViewTextBoxColumn FactDate;
        private DataGridViewTextBoxColumn AgeAtFact;
        private DataGridViewTextBoxColumn FactLocation;
        private DataGridViewImageColumn LocationIcon;
        private DataGridViewTextBoxColumn Latitude;
        private DataGridViewTextBoxColumn Longitude;
        private DataGridViewTextBoxColumn GeocodeStatus;
        private DataGridViewTextBoxColumn FoundLocation;
        private DataGridViewTextBoxColumn FoundResultType;
        private DataGridViewTextBoxColumn Comment;
        private DataGridViewTextBoxColumn SourceList;
    }
}