using System;

namespace FTAnalyzer.Forms
{
    partial class ColourCensus
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

                boldFont.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColourCensus));
            this.dgReportSheet = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewFacts = new System.Windows.Forms.ToolStripMenuItem();
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
            this.cbCensusSearchProvider = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cbRegion = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbFilter = new System.Windows.Forms.ToolStripComboBox();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.IndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Forenames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelationToRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1841 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1851 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1861 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1871 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1881 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1891 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1901 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1911 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1921 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1939 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1790 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1800 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1810 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1820 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1830 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1840 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1850 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1860 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1870 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1880 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1890 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1900 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1910 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1920 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1930 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1940 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.US1950 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1851 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1861 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1871 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1881 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1891 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1901 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1906 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1911 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1916 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Can1921 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ire1901 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ire1911 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ahnentafel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgReportSheet)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgReportSheet
            // 
            this.dgReportSheet.AllowUserToAddRows = false;
            this.dgReportSheet.AllowUserToDeleteRows = false;
            this.dgReportSheet.AllowUserToOrderColumns = true;
            this.dgReportSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgReportSheet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgReportSheet.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgReportSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReportSheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IndividualID,
            this.Forenames,
            this.Surname,
            this.Relation,
            this.RelationToRoot,
            this.C1841,
            this.C1851,
            this.C1861,
            this.C1871,
            this.C1881,
            this.C1891,
            this.C1901,
            this.C1911,
            this.C1921,
            this.C1939,
            this.US1790,
            this.US1800,
            this.US1810,
            this.US1820,
            this.US1830,
            this.US1840,
            this.US1850,
            this.US1860,
            this.US1870,
            this.US1880,
            this.US1890,
            this.US1900,
            this.US1910,
            this.US1920,
            this.US1930,
            this.US1940,
            this.US1950,
            this.Can1851,
            this.Can1861,
            this.Can1871,
            this.Can1881,
            this.Can1891,
            this.Can1901,
            this.Can1906,
            this.Can1911,
            this.Can1916,
            this.Can1921,
            this.Ire1901,
            this.Ire1911,
            this.BirthDate,
            this.BirthLocation,
            this.DeathDate,
            this.DeathLocation,
            this.BestLocation,
            this.Ahnentafel});
            this.dgReportSheet.ContextMenuStrip = this.contextMenuStrip;
            this.dgReportSheet.Location = new System.Drawing.Point(0, 52);
            this.dgReportSheet.Margin = new System.Windows.Forms.Padding(6);
            this.dgReportSheet.MultiSelect = false;
            this.dgReportSheet.Name = "dgReportSheet";
            this.dgReportSheet.ReadOnly = true;
            this.dgReportSheet.RowHeadersWidth = 20;
            this.dgReportSheet.Size = new System.Drawing.Size(1903, 978);
            this.dgReportSheet.TabIndex = 1;
            this.dgReportSheet.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.DgReportSheet_CellContextMenuStripNeeded);
            this.dgReportSheet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgReportSheet_CellDoubleClick);
            this.dgReportSheet.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgReportSheet_CellFormatting);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
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
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
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
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
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
            this.cbCensusSearchProvider,
            this.toolStripLabel3,
            this.cbRegion,
            this.toolStripLabel2,
            this.cbFilter});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1903, 42);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveCensusColumnLayout
            // 
            this.mnuSaveCensusColumnLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSaveCensusColumnLayout.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveCensusColumnLayout.Image")));
            this.mnuSaveCensusColumnLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSaveCensusColumnLayout.Name = "mnuSaveCensusColumnLayout";
            this.mnuSaveCensusColumnLayout.Size = new System.Drawing.Size(40, 36);
            this.mnuSaveCensusColumnLayout.Text = "Save Census Column Sort Order";
            this.mnuSaveCensusColumnLayout.Click += new System.EventHandler(this.MnuSaveCensusColumnLayout_Click);
            // 
            // mnuResetCensusColumns
            // 
            this.mnuResetCensusColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuResetCensusColumns.Image = ((System.Drawing.Image)(resources.GetObject("mnuResetCensusColumns.Image")));
            this.mnuResetCensusColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuResetCensusColumns.Name = "mnuResetCensusColumns";
            this.mnuResetCensusColumns.Size = new System.Drawing.Size(40, 36);
            this.mnuResetCensusColumns.Text = "Reset Census Column Sort Order to Default";
            this.mnuResetCensusColumns.Click += new System.EventHandler(this.MnuResetCensusColumns_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 42);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(40, 36);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.Click += new System.EventHandler(this.PrintToolStripButton_Click);
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(40, 36);
            this.printPreviewToolStripButton.Text = "Print Preview...";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.PrintPreviewToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 42);
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportToExcel.Image")));
            this.mnuExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(40, 36);
            this.mnuExportToExcel.Text = "Export to Excel";
            this.mnuExportToExcel.Click += new System.EventHandler(this.MnuExportToExcel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 42);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(206, 36);
            this.toolStripLabel1.Text = "Census search using:";
            // 
            // cbCensusSearchProvider
            // 
            this.cbCensusSearchProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCensusSearchProvider.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbCensusSearchProvider.Items.AddRange(new object[] {
            "Ancestry",
            "Find My Past",
            "FreeCen",
            "FamilySearch",
            "Scotlands People"});
            this.cbCensusSearchProvider.Name = "cbCensusSearchProvider";
            this.cbCensusSearchProvider.Size = new System.Drawing.Size(219, 42);
            this.cbCensusSearchProvider.SelectedIndexChanged += new System.EventHandler(this.CbCensusSearchProvider_SelectedIndexChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(82, 36);
            this.toolStripLabel3.Text = "Region:";
            // 
            // cbRegion
            // 
            this.cbRegion.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbRegion.Items.AddRange(new object[] {
            ".com",
            ".co.uk",
            ".ca",
            ".com.au"});
            this.cbRegion.Name = "cbRegion";
            this.cbRegion.Size = new System.Drawing.Size(219, 42);
            this.cbRegion.Text = ".co.uk";
            this.cbRegion.SelectedIndexChanged += new System.EventHandler(this.CbRegion_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(69, 36);
            this.toolStripLabel2.Text = "Filter :";
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.DropDownWidth = 170;
            this.cbFilter.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cbFilter.Items.AddRange(new object[] {
            "All Individuals",
            "None Found (All Red)",
            "All Found (All Green)",
            "Lost Cousins Missing (Yellow)",
            "Lost Cousins Present (Orange)",
            "Outside UK (Dark Grey)",
            "Some Missing (Some Red)",
            "Some Found (Some Green)",
            "Known Missing (Mid Green)"});
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(170, 42);
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.CbFilter_SelectedIndexChanged);
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
            this.Forenames.Width = 200;
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
            this.RelationToRoot.Width = 145;
            // 
            // C1841
            // 
            this.C1841.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1841.DataPropertyName = "C1841";
            this.C1841.HeaderText = "1841 Census";
            this.C1841.MinimumWidth = 60;
            this.C1841.Name = "C1841";
            this.C1841.ReadOnly = true;
            this.C1841.Width = 60;
            // 
            // C1851
            // 
            this.C1851.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1851.DataPropertyName = "C1851";
            this.C1851.HeaderText = "1851 Census";
            this.C1851.MinimumWidth = 60;
            this.C1851.Name = "C1851";
            this.C1851.ReadOnly = true;
            this.C1851.Width = 60;
            // 
            // C1861
            // 
            this.C1861.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1861.DataPropertyName = "C1861";
            this.C1861.HeaderText = "1861 Census";
            this.C1861.MinimumWidth = 60;
            this.C1861.Name = "C1861";
            this.C1861.ReadOnly = true;
            this.C1861.Width = 60;
            // 
            // C1871
            // 
            this.C1871.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1871.DataPropertyName = "C1871";
            this.C1871.HeaderText = "1871 Census";
            this.C1871.MinimumWidth = 60;
            this.C1871.Name = "C1871";
            this.C1871.ReadOnly = true;
            this.C1871.Width = 60;
            // 
            // C1881
            // 
            this.C1881.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1881.DataPropertyName = "C1881";
            this.C1881.HeaderText = "1881 Census";
            this.C1881.MinimumWidth = 60;
            this.C1881.Name = "C1881";
            this.C1881.ReadOnly = true;
            this.C1881.Width = 60;
            // 
            // C1891
            // 
            this.C1891.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1891.DataPropertyName = "C1891";
            this.C1891.HeaderText = "1891 Census";
            this.C1891.MinimumWidth = 60;
            this.C1891.Name = "C1891";
            this.C1891.ReadOnly = true;
            this.C1891.Width = 60;
            // 
            // C1901
            // 
            this.C1901.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1901.DataPropertyName = "C1901";
            this.C1901.HeaderText = "1901 Census";
            this.C1901.MinimumWidth = 60;
            this.C1901.Name = "C1901";
            this.C1901.ReadOnly = true;
            this.C1901.Width = 60;
            // 
            // C1911
            // 
            this.C1911.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1911.DataPropertyName = "C1911";
            this.C1911.HeaderText = "1911 Census";
            this.C1911.MinimumWidth = 60;
            this.C1911.Name = "C1911";
            this.C1911.ReadOnly = true;
            this.C1911.Width = 60;
            // 
            // C1921
            // 
            this.C1921.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1921.DataPropertyName = "C1921";
            this.C1921.HeaderText = "1921 Census";
            this.C1921.MinimumWidth = 60;
            this.C1921.Name = "C1921";
            this.C1921.ReadOnly = true;
            this.C1921.Width = 60;
            // 
            // C1939
            // 
            this.C1939.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1939.DataPropertyName = "C1939";
            this.C1939.HeaderText = "1939 National Register";
            this.C1939.MinimumWidth = 60;
            this.C1939.Name = "C1939";
            this.C1939.ReadOnly = true;
            this.C1939.Width = 60;
            // 
            // US1790
            // 
            this.US1790.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1790.DataPropertyName = "US1790";
            this.US1790.HeaderText = "1790 US Census";
            this.US1790.MinimumWidth = 60;
            this.US1790.Name = "US1790";
            this.US1790.ReadOnly = true;
            this.US1790.Width = 60;
            // 
            // US1800
            // 
            this.US1800.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1800.DataPropertyName = "US1800";
            this.US1800.HeaderText = "1800 US Census";
            this.US1800.MinimumWidth = 60;
            this.US1800.Name = "US1800";
            this.US1800.ReadOnly = true;
            this.US1800.Width = 60;
            // 
            // US1810
            // 
            this.US1810.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1810.DataPropertyName = "US1810";
            this.US1810.HeaderText = "1810 US Census";
            this.US1810.MinimumWidth = 60;
            this.US1810.Name = "US1810";
            this.US1810.ReadOnly = true;
            this.US1810.Width = 60;
            // 
            // US1820
            // 
            this.US1820.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1820.DataPropertyName = "US1820";
            this.US1820.HeaderText = "1820 US Census";
            this.US1820.MinimumWidth = 60;
            this.US1820.Name = "US1820";
            this.US1820.ReadOnly = true;
            this.US1820.Width = 60;
            // 
            // US1830
            // 
            this.US1830.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1830.DataPropertyName = "US1830";
            this.US1830.HeaderText = "1830 US Census";
            this.US1830.MinimumWidth = 60;
            this.US1830.Name = "US1830";
            this.US1830.ReadOnly = true;
            this.US1830.Width = 60;
            // 
            // US1840
            // 
            this.US1840.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1840.DataPropertyName = "US1840";
            this.US1840.HeaderText = "1840 US Census";
            this.US1840.MinimumWidth = 60;
            this.US1840.Name = "US1840";
            this.US1840.ReadOnly = true;
            this.US1840.Width = 60;
            // 
            // US1850
            // 
            this.US1850.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1850.DataPropertyName = "US1850";
            this.US1850.HeaderText = "1850 US Census";
            this.US1850.MinimumWidth = 60;
            this.US1850.Name = "US1850";
            this.US1850.ReadOnly = true;
            this.US1850.Width = 60;
            // 
            // US1860
            // 
            this.US1860.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1860.DataPropertyName = "US1860";
            this.US1860.HeaderText = "1860 US Census";
            this.US1860.MinimumWidth = 60;
            this.US1860.Name = "US1860";
            this.US1860.ReadOnly = true;
            this.US1860.Width = 60;
            // 
            // US1870
            // 
            this.US1870.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1870.DataPropertyName = "US1870";
            this.US1870.HeaderText = "1870 US Census";
            this.US1870.MinimumWidth = 60;
            this.US1870.Name = "US1870";
            this.US1870.ReadOnly = true;
            this.US1870.Width = 60;
            // 
            // US1880
            // 
            this.US1880.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1880.DataPropertyName = "US1880";
            this.US1880.HeaderText = "1880 US Census";
            this.US1880.MinimumWidth = 60;
            this.US1880.Name = "US1880";
            this.US1880.ReadOnly = true;
            this.US1880.Width = 60;
            // 
            // US1890
            // 
            this.US1890.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1890.DataPropertyName = "US1890";
            this.US1890.HeaderText = "1890 US Census";
            this.US1890.MinimumWidth = 60;
            this.US1890.Name = "US1890";
            this.US1890.ReadOnly = true;
            this.US1890.Width = 60;
            // 
            // US1900
            // 
            this.US1900.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1900.DataPropertyName = "US1900";
            this.US1900.HeaderText = "1900 US Census";
            this.US1900.MinimumWidth = 60;
            this.US1900.Name = "US1900";
            this.US1900.ReadOnly = true;
            this.US1900.Width = 60;
            // 
            // US1910
            // 
            this.US1910.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1910.DataPropertyName = "US1910";
            this.US1910.HeaderText = "1910 US Census";
            this.US1910.MinimumWidth = 60;
            this.US1910.Name = "US1910";
            this.US1910.ReadOnly = true;
            this.US1910.Width = 60;
            // 
            // US1920
            // 
            this.US1920.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1920.DataPropertyName = "US1920";
            this.US1920.HeaderText = "1920 US Census";
            this.US1920.MinimumWidth = 60;
            this.US1920.Name = "US1920";
            this.US1920.ReadOnly = true;
            this.US1920.Width = 60;
            // 
            // US1930
            // 
            this.US1930.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1930.DataPropertyName = "US1930";
            this.US1930.HeaderText = "1930 US Census";
            this.US1930.MinimumWidth = 60;
            this.US1930.Name = "US1930";
            this.US1930.ReadOnly = true;
            this.US1930.Width = 60;
            // 
            // US1940
            // 
            this.US1940.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1940.DataPropertyName = "US1940";
            this.US1940.HeaderText = "1940 US Census";
            this.US1940.MinimumWidth = 60;
            this.US1940.Name = "US1940";
            this.US1940.ReadOnly = true;
            this.US1940.Width = 60;
            // 
            // US1950
            // 
            this.US1950.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.US1950.DataPropertyName = "US1950";
            this.US1950.HeaderText = "1950 US Census";
            this.US1950.MinimumWidth = 60;
            this.US1950.Name = "US1950";
            this.US1950.ReadOnly = true;
            this.US1950.Width = 60;
            // 
            // Can1851
            // 
            this.Can1851.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1851.DataPropertyName = "Can1851";
            this.Can1851.HeaderText = "1851/2 Canada Census";
            this.Can1851.MinimumWidth = 60;
            this.Can1851.Name = "Can1851";
            this.Can1851.ReadOnly = true;
            this.Can1851.Width = 60;
            // 
            // Can1861
            // 
            this.Can1861.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1861.DataPropertyName = "Can1861";
            this.Can1861.HeaderText = "1861 Canada Census";
            this.Can1861.MinimumWidth = 60;
            this.Can1861.Name = "Can1861";
            this.Can1861.ReadOnly = true;
            this.Can1861.Width = 60;
            // 
            // Can1871
            // 
            this.Can1871.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1871.DataPropertyName = "Can1871";
            this.Can1871.HeaderText = "1871 Canada Census";
            this.Can1871.MinimumWidth = 60;
            this.Can1871.Name = "Can1871";
            this.Can1871.ReadOnly = true;
            this.Can1871.Width = 60;
            // 
            // Can1881
            // 
            this.Can1881.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1881.DataPropertyName = "Can1881";
            this.Can1881.HeaderText = "1881 Canada Census";
            this.Can1881.MinimumWidth = 60;
            this.Can1881.Name = "Can1881";
            this.Can1881.ReadOnly = true;
            this.Can1881.Width = 60;
            // 
            // Can1891
            // 
            this.Can1891.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1891.DataPropertyName = "Can1891";
            this.Can1891.HeaderText = "1891 Canada Census";
            this.Can1891.MinimumWidth = 60;
            this.Can1891.Name = "Can1891";
            this.Can1891.ReadOnly = true;
            this.Can1891.Width = 60;
            // 
            // Can1901
            // 
            this.Can1901.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1901.DataPropertyName = "Can1901";
            this.Can1901.HeaderText = "1901 Canada Census";
            this.Can1901.MinimumWidth = 60;
            this.Can1901.Name = "Can1901";
            this.Can1901.ReadOnly = true;
            this.Can1901.Width = 60;
            // 
            // Can1906
            // 
            this.Can1906.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1906.DataPropertyName = "Can1906";
            this.Can1906.HeaderText = "1906 Canada Census";
            this.Can1906.MinimumWidth = 60;
            this.Can1906.Name = "Can1906";
            this.Can1906.ReadOnly = true;
            this.Can1906.Width = 60;
            // 
            // Can1911
            // 
            this.Can1911.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1911.DataPropertyName = "Can1911";
            this.Can1911.HeaderText = "1911 Canada Census";
            this.Can1911.MinimumWidth = 60;
            this.Can1911.Name = "Can1911";
            this.Can1911.ReadOnly = true;
            this.Can1911.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Can1911.Width = 60;
            // 
            // Can1916
            // 
            this.Can1916.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1916.DataPropertyName = "Can1916";
            this.Can1916.HeaderText = "1916 Canada Census";
            this.Can1916.MinimumWidth = 60;
            this.Can1916.Name = "Can1916";
            this.Can1916.ReadOnly = true;
            this.Can1916.Width = 60;
            // 
            // Can1921
            // 
            this.Can1921.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Can1921.DataPropertyName = "Can1921";
            this.Can1921.HeaderText = "1921 Canada Census";
            this.Can1921.MinimumWidth = 60;
            this.Can1921.Name = "Can1921";
            this.Can1921.ReadOnly = true;
            this.Can1921.Width = 60;
            // 
            // Ire1901
            // 
            this.Ire1901.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Ire1901.DataPropertyName = "Ire1901";
            this.Ire1901.HeaderText = "1901 Irish Census";
            this.Ire1901.MinimumWidth = 60;
            this.Ire1901.Name = "Ire1901";
            this.Ire1901.ReadOnly = true;
            this.Ire1901.Width = 60;
            // 
            // Ire1911
            // 
            this.Ire1911.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Ire1911.DataPropertyName = "Ire1911";
            this.Ire1911.HeaderText = "1911 Irish Census";
            this.Ire1911.MinimumWidth = 60;
            this.Ire1911.Name = "Ire1911";
            this.Ire1911.ReadOnly = true;
            this.Ire1911.Width = 60;
            // 
            // BirthDate
            // 
            this.BirthDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.BirthDate.DataPropertyName = "BirthDate";
            this.BirthDate.HeaderText = "Birth Date";
            this.BirthDate.MinimumWidth = 50;
            this.BirthDate.Name = "BirthDate";
            this.BirthDate.ReadOnly = true;
            this.BirthDate.Width = 150;
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
            // DeathDate
            // 
            this.DeathDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DeathDate.DataPropertyName = "DeathDate";
            this.DeathDate.HeaderText = "Death Date";
            this.DeathDate.MinimumWidth = 50;
            this.DeathDate.Name = "DeathDate";
            this.DeathDate.ReadOnly = true;
            this.DeathDate.Width = 150;
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
            this.Ahnentafel.Width = 147;
            // 
            // ColourCensus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1903, 1076);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgReportSheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ColourCensus";
            this.Text = "Colour Census Report Result";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ColourCensus_FormClosed);
            this.Load += new System.EventHandler(this.ColourCensus_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgReportSheet)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgReportSheet;
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
        private System.Windows.Forms.ToolStripComboBox cbCensusSearchProvider;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbFilter;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuSaveCensusColumnLayout;
        private System.Windows.Forms.ToolStripButton mnuResetCensusColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuViewFacts;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cbRegion;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Forenames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relation;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelationToRoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1841;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1851;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1861;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1871;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1881;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1891;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1901;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1911;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1921;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1939;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1790;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1800;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1810;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1820;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1830;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1840;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1850;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1860;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1870;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1880;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1890;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1900;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1910;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1920;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1930;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1940;
        private System.Windows.Forms.DataGridViewTextBoxColumn US1950;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1851;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1861;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1871;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1881;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1891;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1901;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1906;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1911;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1916;
        private System.Windows.Forms.DataGridViewTextBoxColumn Can1921;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ire1901;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ire1911;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeathDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeathLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ahnentafel;
    }
}