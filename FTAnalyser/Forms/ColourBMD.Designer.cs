namespace FTAnalyzer.Forms
{
    partial class ColourBMD
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
            boldFont.Dispose();
            reportFormHelper.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColourBMD));
            this.dgBMDReportSheet = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuSaveCensusColumnLayout = new System.Windows.Forms.ToolStripButton();
            this.mnuResetCensusColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbBMDSearchProvider = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbFilter = new System.Windows.Forms.ToolStripComboBox();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewFacts = new System.Windows.Forms.ToolStripMenuItem();
            this.IndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Forenames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelationToRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Birth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BaptChri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marriage1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marriage2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marriage3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Death = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CremBuri = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstMarriage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SecondMarriage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThirdMarriage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ahnentafel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgBMDReportSheet)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgBMDReportSheet
            // 
            this.dgBMDReportSheet.AllowUserToAddRows = false;
            this.dgBMDReportSheet.AllowUserToDeleteRows = false;
            this.dgBMDReportSheet.AllowUserToOrderColumns = true;
            this.dgBMDReportSheet.AllowUserToResizeRows = false;
            this.dgBMDReportSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgBMDReportSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgBMDReportSheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndividualID,
            this.Forenames,
            this.Surname,
            this.Relation,
            this.RelationToRoot,
            this.Birth,
            this.BaptChri,
            this.Marriage1,
            this.Marriage2,
            this.Marriage3,
            this.Death,
            this.CremBuri,
            this.BirthDate,
            this.DeathDate,
            this.FirstMarriage,
            this.SecondMarriage,
            this.ThirdMarriage,
            this.BirthLocation,
            this.DeathLocation,
            this.BestLocation,
            this.Ahnentafel});
            this.dgBMDReportSheet.Location = new System.Drawing.Point(0, 28);
            this.dgBMDReportSheet.MultiSelect = false;
            this.dgBMDReportSheet.Name = "dgBMDReportSheet";
            this.dgBMDReportSheet.ReadOnly = true;
            this.dgBMDReportSheet.RowHeadersWidth = 20;
            this.dgBMDReportSheet.Size = new System.Drawing.Size(1038, 530);
            this.dgBMDReportSheet.TabIndex = 1;
            this.dgBMDReportSheet.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgBMDReportSheet_CellContextMenuStripNeeded);
            this.dgBMDReportSheet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgReportSheet_CellDoubleClick);
            this.dgBMDReportSheet.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgReportSheet_CellFormatting);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecords});
            this.statusStrip.Location = new System.Drawing.Point(0, 561);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1038, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            this.tsRecords.Name = "tsRecords";
            this.tsRecords.Size = new System.Drawing.Size(118, 17);
            this.tsRecords.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveCensusColumnLayout,
            this.mnuResetCensusColumns,
            this.toolStripSeparator3,
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator1,
            this.mnuExportToExcel,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.cbBMDSearchProvider,
            this.toolStripLabel2,
            this.cbFilter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1038, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveCensusColumnLayout
            // 
            this.mnuSaveCensusColumnLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSaveCensusColumnLayout.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveCensusColumnLayout.Image")));
            this.mnuSaveCensusColumnLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSaveCensusColumnLayout.Name = "mnuSaveCensusColumnLayout";
            this.mnuSaveCensusColumnLayout.Size = new System.Drawing.Size(23, 22);
            this.mnuSaveCensusColumnLayout.Text = "Save Census Column Sort Order";
            this.mnuSaveCensusColumnLayout.Click += new System.EventHandler(this.mnuSaveCensusColumnLayout_Click);
            // 
            // mnuResetCensusColumns
            // 
            this.mnuResetCensusColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuResetCensusColumns.Image = ((System.Drawing.Image)(resources.GetObject("mnuResetCensusColumns.Image")));
            this.mnuResetCensusColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuResetCensusColumns.Name = "mnuResetCensusColumns";
            this.mnuResetCensusColumns.Size = new System.Drawing.Size(23, 22);
            this.mnuResetCensusColumns.Text = "Reset Census Column Sort Order to Default";
            this.mnuResetCensusColumns.Click += new System.EventHandler(this.mnuResetCensusColumns_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printPreviewToolStripButton.Text = "Print Preview...";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.printPreviewToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportToExcel.Image")));
            this.mnuExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(23, 22);
            this.mnuExportToExcel.Text = "Export to Excel";
            this.mnuExportToExcel.Click += new System.EventHandler(this.mnuExportToExcel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(106, 22);
            this.toolStripLabel1.Text = "BMD Search using:";
            // 
            // cbBMDSearchProvider
            // 
            this.cbBMDSearchProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBMDSearchProvider.Items.AddRange(new object[] {
            "Ancestry",
            "Find My Past",
            "FreeCen",
            "FamilySearch"});
            this.cbBMDSearchProvider.Name = "cbBMDSearchProvider";
            this.cbBMDSearchProvider.Size = new System.Drawing.Size(121, 25);
            this.cbBMDSearchProvider.SelectedIndexChanged += new System.EventHandler(this.cbCensusSearchProvider_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(39, 22);
            this.toolStripLabel2.Text = "Filter :";
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.DropDownWidth = 220;
            this.cbFilter.Items.AddRange(new object[] {
            "All Individuals",
            "None Found (All Red)",
            "All Found (All Green)",
            "All Wide Date Range (Orange)",
            "All Approx Date Range (Light Green)",
            "All Narrow Date Range (Yellow)",
            "Some Missing (Some Red)",
            "Some Found (Some Green)",
            "Some Wide Date Range (Orange)",
            "Some Narrow Date Range (Yellow)",
            "Some Approx Date Range (Light Green)",
            "Of Marrying Age no partner (Peach)",
            "No Partner shared fact/children (Light Blue)",
            "Partner but no marriage (Dark Blue)"});
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(235, 25);
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.cbFilter_SelectedIndexChanged);
            // 
            // printDialog
            // 
            this.printDialog.AllowSelection = true;
            this.printDialog.AllowSomePages = true;
            this.printDialog.Document = this.printDocument;
            this.printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewFacts});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(190, 26);
            // 
            // mnuViewFacts
            // 
            this.mnuViewFacts.Name = "mnuViewFacts";
            this.mnuViewFacts.Size = new System.Drawing.Size(189, 22);
            this.mnuViewFacts.Text = "View Individuals Facts";
            this.mnuViewFacts.Click += new System.EventHandler(this.mnuViewFacts_Click);
            // 
            // IndividualID
            // 
            this.IndividualID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IndividualID.DataPropertyName = "IndividualID";
            this.IndividualID.HeaderText = "Ind. ID";
            this.IndividualID.MinimumWidth = 50;
            this.IndividualID.Name = "IndividualID";
            this.IndividualID.ReadOnly = true;
            this.IndividualID.Width = 50;
            // 
            // Forenames
            // 
            this.Forenames.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
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
            // Relation
            // 
            this.Relation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Relation.DataPropertyName = "Relation";
            this.Relation.HeaderText = "Relation";
            this.Relation.MinimumWidth = 105;
            this.Relation.Name = "Relation";
            this.Relation.ReadOnly = true;
            this.Relation.Width = 105;
            // 
            // RelationToRoot
            // 
            this.RelationToRoot.DataPropertyName = "RelationToRoot";
            this.RelationToRoot.HeaderText = "Relation To Root";
            this.RelationToRoot.MinimumWidth = 100;
            this.RelationToRoot.Name = "RelationToRoot";
            this.RelationToRoot.ReadOnly = true;
            this.RelationToRoot.Width = 150;
            // 
            // Birth
            // 
            this.Birth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Birth.DataPropertyName = "Birth";
            this.Birth.HeaderText = "Birth";
            this.Birth.MinimumWidth = 60;
            this.Birth.Name = "Birth";
            this.Birth.ReadOnly = true;
            this.Birth.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Birth.Width = 60;
            // 
            // BaptChri
            // 
            this.BaptChri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BaptChri.DataPropertyName = "BaptChri";
            this.BaptChri.HeaderText = "Baptism Christening";
            this.BaptChri.MinimumWidth = 62;
            this.BaptChri.Name = "BaptChri";
            this.BaptChri.ReadOnly = true;
            this.BaptChri.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BaptChri.Width = 62;
            // 
            // Marriage1
            // 
            this.Marriage1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Marriage1.DataPropertyName = "Marriage1";
            this.Marriage1.HeaderText = "Marriage No. 1";
            this.Marriage1.MinimumWidth = 60;
            this.Marriage1.Name = "Marriage1";
            this.Marriage1.ReadOnly = true;
            this.Marriage1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Marriage1.Width = 60;
            // 
            // Marriage2
            // 
            this.Marriage2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Marriage2.DataPropertyName = "Marriage2";
            this.Marriage2.HeaderText = "Marriage No. 2";
            this.Marriage2.MinimumWidth = 60;
            this.Marriage2.Name = "Marriage2";
            this.Marriage2.ReadOnly = true;
            this.Marriage2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Marriage2.Width = 60;
            // 
            // Marriage3
            // 
            this.Marriage3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Marriage3.DataPropertyName = "Marriage3";
            this.Marriage3.HeaderText = "Marriage No. 3";
            this.Marriage3.MinimumWidth = 60;
            this.Marriage3.Name = "Marriage3";
            this.Marriage3.ReadOnly = true;
            this.Marriage3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Marriage3.Width = 60;
            // 
            // Death
            // 
            this.Death.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Death.DataPropertyName = "Death";
            this.Death.HeaderText = "Death";
            this.Death.MinimumWidth = 60;
            this.Death.Name = "Death";
            this.Death.ReadOnly = true;
            this.Death.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Death.Width = 60;
            // 
            // CremBuri
            // 
            this.CremBuri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CremBuri.DataPropertyName = "CremBuri";
            this.CremBuri.HeaderText = "Burial Cremation";
            this.CremBuri.MinimumWidth = 60;
            this.CremBuri.Name = "CremBuri";
            this.CremBuri.ReadOnly = true;
            this.CremBuri.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CremBuri.Width = 60;
            // 
            // BirthDate
            // 
            this.BirthDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BirthDate.DataPropertyName = "BirthDate";
            this.BirthDate.HeaderText = "Birth Date";
            this.BirthDate.MinimumWidth = 150;
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.ReadOnly = true;
            this.BirthDate.Width = 150;
            // 
            // DeathDate
            // 
            this.DeathDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DeathDate.DataPropertyName = "DeathDate";
            this.DeathDate.HeaderText = "Death Date";
            this.DeathDate.MinimumWidth = 150;
            this.DeathDate.Name = "DeathDate";
            this.DeathDate.ReadOnly = true;
            this.DeathDate.Width = 150;
            // 
            // FirstMarriage
            // 
            this.FirstMarriage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FirstMarriage.DataPropertyName = "FirstMarriage";
            this.FirstMarriage.HeaderText = "First Marriage";
            this.FirstMarriage.MinimumWidth = 100;
            this.FirstMarriage.Name = "FirstMarriage";
            this.FirstMarriage.ReadOnly = true;
            // 
            // SecondMarriage
            // 
            this.SecondMarriage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SecondMarriage.DataPropertyName = "SecondMarriage";
            this.SecondMarriage.HeaderText = "Second Marriage";
            this.SecondMarriage.MinimumWidth = 100;
            this.SecondMarriage.Name = "SecondMarriage";
            this.SecondMarriage.ReadOnly = true;
            // 
            // ThirdMarriage
            // 
            this.ThirdMarriage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.ThirdMarriage.DataPropertyName = "ThirdMarriage";
            this.ThirdMarriage.HeaderText = "Third Marriage";
            this.ThirdMarriage.MinimumWidth = 100;
            this.ThirdMarriage.Name = "ThirdMarriage";
            this.ThirdMarriage.ReadOnly = true;
            // 
            // BirthLocation
            // 
            this.BirthLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BirthLocation.DataPropertyName = "BirthLocation";
            this.BirthLocation.HeaderText = "Birth Location";
            this.BirthLocation.MinimumWidth = 120;
            this.BirthLocation.Name = "BirthLocation";
            this.BirthLocation.ReadOnly = true;
            this.BirthLocation.Width = 120;
            // 
            // DeathLocation
            // 
            this.DeathLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DeathLocation.DataPropertyName = "DeathLocation";
            this.DeathLocation.HeaderText = "Death Location";
            this.DeathLocation.MinimumWidth = 120;
            this.DeathLocation.Name = "DeathLocation";
            this.DeathLocation.ReadOnly = true;
            this.DeathLocation.Width = 120;
            // 
            // BestLocation
            // 
            this.BestLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BestLocation.DataPropertyName = "BestLocation";
            this.BestLocation.HeaderText = "Best Location";
            this.BestLocation.MinimumWidth = 120;
            this.BestLocation.Name = "BestLocation";
            this.BestLocation.ReadOnly = true;
            this.BestLocation.Width = 120;
            // 
            // Ahnentafel
            // 
            this.Ahnentafel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Ahnentafel.DataPropertyName = "Ahnentafel";
            this.Ahnentafel.HeaderText = "Ahnentafel";
            this.Ahnentafel.MinimumWidth = 20;
            this.Ahnentafel.Name = "Ahnentafel";
            this.Ahnentafel.ReadOnly = true;
            this.Ahnentafel.Width = 83;
            // 
            // ColourBMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 583);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgBMDReportSheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColourBMD";
            this.Text = "Colour BMD Report Result";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ColourBMD_FormClosed);
            ((System.ComponentModel.ISupportInitialize)(this.dgBMDReportSheet)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgBMDReportSheet;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbBMDSearchProvider;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbFilter;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuSaveCensusColumnLayout;
        private System.Windows.Forms.ToolStripButton mnuResetCensusColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuViewFacts;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Forenames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relation;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelationToRoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn Birth;
        private System.Windows.Forms.DataGridViewTextBoxColumn BaptChri;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marriage1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marriage2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marriage3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Death;
        private System.Windows.Forms.DataGridViewTextBoxColumn CremBuri;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeathDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstMarriage;
        private System.Windows.Forms.DataGridViewTextBoxColumn SecondMarriage;
        private System.Windows.Forms.DataGridViewTextBoxColumn ThirdMarriage;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeathLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ahnentafel;
    }
}