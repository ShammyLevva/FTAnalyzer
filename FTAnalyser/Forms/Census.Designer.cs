using System;

namespace FTAnalyzer.Forms
{
    partial class Census
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
                reportFormHelper.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Census));
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuSaveCensusColumnLayout = new System.Windows.Forms.ToolStripButton();
            this.mnuResetCensusColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnMapLocation = new System.Windows.Forms.ToolStripButton();
            this.tsBtnMapOSLocation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbCensusSearchProvider = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbRegion = new System.Windows.Forms.ToolStripComboBox();
            this.btnHelp = new System.Windows.Forms.ToolStripButton();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewFacts = new System.Windows.Forms.ToolStripMenuItem();
            this.dgCensus = new System.Windows.Forms.DataGridView();
            this.FamilyID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Position = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CensusLoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CensusName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Age = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Occupation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CensusStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelationToRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CensusReference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CensusYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ahnentafel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCensus)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecords});
            this.statusStrip.Location = new System.Drawing.Point(0, 1037);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 26, 0);
            this.statusStrip.Size = new System.Drawing.Size(1903, 39);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            this.tsRecords.Name = "tsRecords";
            this.tsRecords.Size = new System.Drawing.Size(206, 30);
            this.tsRecords.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveCensusColumnLayout,
            this.mnuResetCensusColumns,
            this.toolStripSeparator2,
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator,
            this.mnuExportToExcel,
            this.toolStripSeparator3,
            this.tsBtnMapLocation,
            this.tsBtnMapOSLocation,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cbCensusSearchProvider,
            this.toolStripLabel2,
            this.cbRegion,
            this.btnHelp});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1903, 40);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveCensusColumnLayout
            // 
            this.mnuSaveCensusColumnLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSaveCensusColumnLayout.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveCensusColumnLayout.Image")));
            this.mnuSaveCensusColumnLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSaveCensusColumnLayout.Name = "mnuSaveCensusColumnLayout";
            this.mnuSaveCensusColumnLayout.Size = new System.Drawing.Size(40, 34);
            this.mnuSaveCensusColumnLayout.Text = "Save Census Column Sort Order";
            this.mnuSaveCensusColumnLayout.Click += new System.EventHandler(this.MnuSaveCensusColumnLayout_Click);
            // 
            // mnuResetCensusColumns
            // 
            this.mnuResetCensusColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuResetCensusColumns.Image = ((System.Drawing.Image)(resources.GetObject("mnuResetCensusColumns.Image")));
            this.mnuResetCensusColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuResetCensusColumns.Name = "mnuResetCensusColumns";
            this.mnuResetCensusColumns.Size = new System.Drawing.Size(40, 34);
            this.mnuResetCensusColumns.Text = "Reset Census Column Sort Order to Default";
            this.mnuResetCensusColumns.Click += new System.EventHandler(this.MnuResetCensusColumns_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 40);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(40, 34);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.Click += new System.EventHandler(this.PrintToolStripButton_Click);
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(40, 34);
            this.printPreviewToolStripButton.Text = "Print Preview...";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.PrintPreviewToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 40);
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportToExcel.Image")));
            this.mnuExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(40, 34);
            this.mnuExportToExcel.Text = "Export to Excel";
            this.mnuExportToExcel.Click += new System.EventHandler(this.MnuExportToExcel_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 40);
            // 
            // tsBtnMapLocation
            // 
            this.tsBtnMapLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnMapLocation.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnMapLocation.Image")));
            this.tsBtnMapLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnMapLocation.Name = "tsBtnMapLocation";
            this.tsBtnMapLocation.Size = new System.Drawing.Size(302, 34);
            this.tsBtnMapLocation.Text = "Show Location on Google Map";
            this.tsBtnMapLocation.Click += new System.EventHandler(this.TsBtnMapLocation_Click);
            // 
            // tsBtnMapOSLocation
            // 
            this.tsBtnMapOSLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnMapOSLocation.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnMapOSLocation.Image")));
            this.tsBtnMapOSLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnMapOSLocation.Name = "tsBtnMapOSLocation";
            this.tsBtnMapOSLocation.Size = new System.Drawing.Size(263, 34);
            this.tsBtnMapOSLocation.Text = "Show Location on OS Map";
            this.tsBtnMapOSLocation.Click += new System.EventHandler(this.TsBtnMapOSLocation_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 40);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(206, 34);
            this.toolStripLabel1.Text = "Census search using:";
            // 
            // cbCensusSearchProvider
            // 
            this.cbCensusSearchProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCensusSearchProvider.Items.AddRange(new object[] {
            "Ancestry",
            "Find My Past",
            "FreeCen",
            "FamilySearch",
            "Scotlands People"});
            this.cbCensusSearchProvider.Name = "cbCensusSearchProvider";
            this.cbCensusSearchProvider.Size = new System.Drawing.Size(121, 40);
            this.cbCensusSearchProvider.SelectedIndexChanged += new System.EventHandler(this.CbCensusSearchProvider_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(82, 34);
            this.toolStripLabel2.Text = "Region:";
            // 
            // cbRegion
            // 
            this.cbRegion.AutoCompleteCustomSource.AddRange(new string[] {
            ".com",
            ".co.uk",
            ".ca",
            ".com.au"});
            this.cbRegion.Items.AddRange(new object[] {
            ".com",
            ".co.uk",
            ".ca",
            ".com.au"});
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(121, 40);
            this.cbRegion.Text = ".co.uk";
            this.cbRegion.SelectedIndexChanged += new System.EventHandler(this.CbRegion_SelectedIndexChanged);
            // 
            // btnHelp
            // 
            this.btnHelp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnHelp.Image = ((System.Drawing.Image)(resources.GetObject("btnHelp.Image")));
            this.btnHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(40, 34);
            this.btnHelp.Text = "toolStripButton1";
            this.btnHelp.Click += new System.EventHandler(this.BtnHelp_Click);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewFacts});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(288, 40);
            // 
            // mnuViewFacts
            // 
            this.mnuViewFacts.Name = "mnuViewFacts";
            this.mnuViewFacts.Size = new System.Drawing.Size(287, 36);
            this.mnuViewFacts.Text = "View Individuals Facts";
            this.mnuViewFacts.Click += new System.EventHandler(this.MnuViewFacts_Click);
            // 
            // dgCensus
            // 
            this.dgCensus.AllowUserToAddRows = false;
            this.dgCensus.AllowUserToDeleteRows = false;
            this.dgCensus.AllowUserToOrderColumns = true;
            this.dgCensus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCensus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgCensus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCensus.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FamilyID,
            this.Position,
            this.IndividualID,
            this.CensusLoc,
            this.CensusName,
            this.Age,
            this.Occupation,
            this.BirthDate,
            this.BirthLocation,
            this.DeathDate,
            this.DeathLocation,
            this.CensusStatus,
            this.Relation,
            this.RelationToRoot,
            this.CensusReference,
            this.CensusYear,
            this.Ahnentafel});
            this.dgCensus.Location = new System.Drawing.Point(0, 52);
            this.dgCensus.Margin = new System.Windows.Forms.Padding(6);
            this.dgCensus.Name = "dgCensus";
            this.dgCensus.ReadOnly = true;
            this.dgCensus.RowHeadersWidth = 23;
            this.dgCensus.ShowEditingIcon = false;
            this.dgCensus.ShowRowErrors = false;
            this.dgCensus.Size = new System.Drawing.Size(1903, 978);
            this.dgCensus.TabIndex = 1;
            this.dgCensus.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.DgCensus_CellContextMenuStripNeeded);
            this.dgCensus.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgCensus_CellDoubleClick);
            this.dgCensus.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgCensus_CellFormatting);
            this.dgCensus.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.DgCensus_ColumnHeaderMouseClick);
            this.dgCensus.ColumnSortModeChanged += new System.Windows.Forms.DataGridViewColumnEventHandler(this.DgCensus_ColumnSortModeChanged);
            // 
            // FamilyID
            // 
            this.FamilyID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FamilyID.DataPropertyName = "FamilyID";
            this.FamilyID.HeaderText = "Family ID";
            this.FamilyID.MinimumWidth = 9;
            this.FamilyID.Name = "FamilyID";
            this.FamilyID.ReadOnly = true;
            this.FamilyID.Width = 134;
            // 
            // Position
            // 
            this.Position.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Position.DataPropertyName = "Position";
            this.Position.HeaderText = "Position";
            this.Position.MinimumWidth = 9;
            this.Position.Name = "Position";
            this.Position.ReadOnly = true;
            this.Position.Visible = false;
            this.Position.Width = 122;
            // 
            // IndividualID
            // 
            this.IndividualID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.IndividualID.DataPropertyName = "IndividualID";
            this.IndividualID.HeaderText = "Ind. ID";
            this.IndividualID.MinimumWidth = 9;
            this.IndividualID.Name = "IndividualID";
            this.IndividualID.ReadOnly = true;
            this.IndividualID.Width = 109;
            // 
            // CensusLoc
            // 
            this.CensusLoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CensusLoc.DataPropertyName = "CensusLocation";
            this.CensusLoc.HeaderText = "Likely Census Location";
            this.CensusLoc.MinimumWidth = 9;
            this.CensusLoc.Name = "CensusLoc";
            this.CensusLoc.ReadOnly = true;
            this.CensusLoc.Width = 233;
            // 
            // CensusName
            // 
            this.CensusName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CensusName.DataPropertyName = "CensusName";
            this.CensusName.HeaderText = "Census Name";
            this.CensusName.MinimumWidth = 9;
            this.CensusName.Name = "CensusName";
            this.CensusName.ReadOnly = true;
            this.CensusName.Width = 164;
            // 
            // Age
            // 
            this.Age.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Age.DataPropertyName = "Age";
            this.Age.HeaderText = "Age";
            this.Age.MinimumWidth = 9;
            this.Age.Name = "Age";
            this.Age.ReadOnly = true;
            this.Age.Width = 89;
            // 
            // Occupation
            // 
            this.Occupation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Occupation.DataPropertyName = "Occupation";
            this.Occupation.HeaderText = "Occupation";
            this.Occupation.MinimumWidth = 9;
            this.Occupation.Name = "Occupation";
            this.Occupation.ReadOnly = true;
            this.Occupation.Width = 153;
            // 
            // BirthDate
            // 
            this.BirthDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.BirthDate.DataPropertyName = "BirthDate";
            this.BirthDate.HeaderText = "Birth Date";
            this.BirthDate.MinimumWidth = 50;
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.ReadOnly = true;
            this.BirthDate.Width = 128;
            // 
            // BirthLocation
            // 
            this.BirthLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.BirthLocation.DataPropertyName = "BirthLocation";
            this.BirthLocation.HeaderText = "Birth Location";
            this.BirthLocation.MinimumWidth = 9;
            this.BirthLocation.Name = "BirthLocation";
            this.BirthLocation.ReadOnly = true;
            this.BirthLocation.Width = 157;
            // 
            // DeathDate
            // 
            this.DeathDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DeathDate.DataPropertyName = "DeathDate";
            this.DeathDate.HeaderText = "Death Date";
            this.DeathDate.MinimumWidth = 50;
            this.DeathDate.Name = "DeathDate";
            this.DeathDate.ReadOnly = true;
            this.DeathDate.Width = 139;
            // 
            // DeathLocation
            // 
            this.DeathLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DeathLocation.DataPropertyName = "DeathLocation";
            this.DeathLocation.HeaderText = "Death Location";
            this.DeathLocation.MinimumWidth = 9;
            this.DeathLocation.Name = "DeathLocation";
            this.DeathLocation.ReadOnly = true;
            this.DeathLocation.Width = 169;
            // 
            // CensusStatus
            // 
            this.CensusStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CensusStatus.DataPropertyName = "CensusStatus";
            this.CensusStatus.HeaderText = "Husband / Wife / Child";
            this.CensusStatus.MinimumWidth = 9;
            this.CensusStatus.Name = "CensusStatus";
            this.CensusStatus.ReadOnly = true;
            this.CensusStatus.Width = 187;
            // 
            // Relation
            // 
            this.Relation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Relation.DataPropertyName = "Relation";
            this.Relation.HeaderText = "Relation";
            this.Relation.MinimumWidth = 9;
            this.Relation.Name = "Relation";
            this.Relation.ReadOnly = true;
            this.Relation.Width = 123;
            // 
            // RelationToRoot
            // 
            this.RelationToRoot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.RelationToRoot.DataPropertyName = "RelationToRoot";
            this.RelationToRoot.HeaderText = "Relation To Root";
            this.RelationToRoot.MinimumWidth = 50;
            this.RelationToRoot.Name = "RelationToRoot";
            this.RelationToRoot.ReadOnly = true;
            this.RelationToRoot.Width = 145;
            // 
            // CensusReference
            // 
            this.CensusReference.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CensusReference.DataPropertyName = "CensusRef";
            this.CensusReference.HeaderText = "Census Reference";
            this.CensusReference.MinimumWidth = 50;
            this.CensusReference.Name = "CensusReference";
            this.CensusReference.ReadOnly = true;
            this.CensusReference.Width = 197;
            // 
            // CensusYear
            // 
            this.CensusYear.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CensusYear.DataPropertyName = "Census";
            this.CensusYear.HeaderText = "Census";
            this.CensusYear.MinimumWidth = 9;
            this.CensusYear.Name = "CensusYear";
            this.CensusYear.ReadOnly = true;
            this.CensusYear.Width = 121;
            // 
            // Ahnentafel
            // 
            this.Ahnentafel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Ahnentafel.DataPropertyName = "Ahnentafel";
            this.Ahnentafel.HeaderText = "Ahnentafel";
            this.Ahnentafel.MinimumWidth = 9;
            this.Ahnentafel.Name = "Ahnentafel";
            this.Ahnentafel.ReadOnly = true;
            this.Ahnentafel.Width = 147;
            // 
            // Census
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1903, 1076);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgCensus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "Census";
            this.Text = "Census Records to search for";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Census_FormClosed);
            this.Load += new System.EventHandler(this.Census_Load);
            this.TextChanged += new System.EventHandler(this.Census_TextChanged);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCensus)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripButton tsBtnMapLocation;
        private System.Windows.Forms.ToolStripButton tsBtnMapOSLocation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbCensusSearchProvider;
        private System.Windows.Forms.ToolStripButton mnuSaveCensusColumnLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuResetCensusColumns;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuViewFacts;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbRegion;
        private System.Windows.Forms.DataGridView dgCensus;
        private System.Windows.Forms.DataGridViewTextBoxColumn FamilyID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Position;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CensusLoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn CensusName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Age;
        private System.Windows.Forms.DataGridViewTextBoxColumn Occupation;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeathDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeathLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn CensusStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relation;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelationToRoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn CensusReference;
        private System.Windows.Forms.DataGridViewTextBoxColumn CensusYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ahnentafel;
    }
}