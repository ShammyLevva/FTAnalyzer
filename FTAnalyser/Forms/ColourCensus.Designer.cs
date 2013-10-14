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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColourCensus));
            this.dgReportSheet = new System.Windows.Forms.DataGridView();
            this.Ind_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Forenames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1841 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1851 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1861 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1871 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1881 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1891 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1901 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.C1911 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BirthLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DeathLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BestLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ahnentafel = new System.Windows.Forms.DataGridViewTextBoxColumn();
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
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbFilter = new System.Windows.Forms.ToolStripComboBox();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgReportSheet)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgReportSheet
            // 
            this.dgReportSheet.AllowUserToAddRows = false;
            this.dgReportSheet.AllowUserToDeleteRows = false;
            this.dgReportSheet.AllowUserToOrderColumns = true;
            this.dgReportSheet.AllowUserToResizeRows = false;
            this.dgReportSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgReportSheet.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgReportSheet.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Ind_ID,
            this.Forenames,
            this.Surname,
            this.Relation,
            this.C1841,
            this.C1851,
            this.C1861,
            this.C1871,
            this.C1881,
            this.C1891,
            this.C1901,
            this.C1911,
            this.BirthDate,
            this.BirthLocation,
            this.DeathDate,
            this.DeathLocation,
            this.BestLocation,
            this.Ahnentafel});
            this.dgReportSheet.Location = new System.Drawing.Point(0, 28);
            this.dgReportSheet.MultiSelect = false;
            this.dgReportSheet.Name = "dgReportSheet";
            this.dgReportSheet.ReadOnly = true;
            this.dgReportSheet.Size = new System.Drawing.Size(1038, 530);
            this.dgReportSheet.TabIndex = 1;
            this.dgReportSheet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgReportSheet_CellDoubleClick);
            this.dgReportSheet.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgReportSheet_CellFormatting);
            // 
            // Ind_ID
            // 
            this.Ind_ID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Ind_ID.DataPropertyName = "Ind_ID";
            this.Ind_ID.HeaderText = "Ind. ID";
            this.Ind_ID.MinimumWidth = 50;
            this.Ind_ID.Name = "Ind_ID";
            this.Ind_ID.ReadOnly = true;
            this.Ind_ID.Width = 50;
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
            // C1841
            // 
            this.C1841.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.C1841.DataPropertyName = "C1841";
            this.C1841.HeaderText = "1841 Census";
            this.C1841.MinimumWidth = 60;
            this.C1841.Name = "C1841";
            this.C1841.ReadOnly = true;
            this.C1841.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.C1851.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.C1861.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.C1871.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.C1881.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.C1891.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.C1901.Resizable = System.Windows.Forms.DataGridViewTriState.False;
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
            this.C1911.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.C1911.Width = 60;
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
            this.DeathDate.MinimumWidth = 150;
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
            this.Ahnentafel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Ahnentafel.DataPropertyName = "Ahnentafel";
            this.Ahnentafel.HeaderText = "Ahnentafel";
            this.Ahnentafel.MinimumWidth = 50;
            this.Ahnentafel.Name = "Ahnentafel";
            this.Ahnentafel.ReadOnly = true;
            this.Ahnentafel.Width = 50;
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
            this.cbCensusSearchProvider,
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
            this.toolStripLabel1.Size = new System.Drawing.Size(117, 22);
            this.toolStripLabel1.Text = "Census search using:";
            // 
            // cbCensusSearchProvider
            // 
            this.cbCensusSearchProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCensusSearchProvider.Items.AddRange(new object[] {
            "Ancestry",
            "Find My Past",
            "FreeCen",
            "FamilySearch"});
            this.cbCensusSearchProvider.Name = "cbCensusSearchProvider";
            this.cbCensusSearchProvider.Size = new System.Drawing.Size(121, 25);
            this.cbCensusSearchProvider.SelectedIndexChanged += new System.EventHandler(this.cbCensusSearchProvider_SelectedIndexChanged);
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
            this.cbFilter.DropDownWidth = 170;
            this.cbFilter.Items.AddRange(new object[] {
            "All Individuals",
            "None Found (All Red)",
            "All Found (All Green)",
            "Lost Cousins Missing (Yellow)",
            "Lost Cousins Present (Orange)",
            "Some Missing (Some Red)",
            "Some Found (Some Green)"});
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(185, 25);
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
            // ColourCensus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 583);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgReportSheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ColourCensus";
            this.Text = "Colour Census Report Result";
            ((System.ComponentModel.ISupportInitialize)(this.dgReportSheet)).EndInit();
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Ind_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Forenames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relation;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1841;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1851;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1861;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1871;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1881;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1891;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1901;
        private System.Windows.Forms.DataGridViewTextBoxColumn C1911;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn BirthLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeathDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DeathLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn BestLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ahnentafel;
    }
}