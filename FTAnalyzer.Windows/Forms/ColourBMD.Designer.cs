using System;

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
            try
            {
                if (disposing && (components is not null))
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
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ColourBMD));
            dgBMDReportSheet = new DataGridView();
            IndividualID = new DataGridViewTextBoxColumn();
            Forenames = new DataGridViewTextBoxColumn();
            Surname = new DataGridViewTextBoxColumn();
            Relation = new DataGridViewTextBoxColumn();
            RelationToRoot = new DataGridViewTextBoxColumn();
            Birth = new DataGridViewTextBoxColumn();
            BaptChri = new DataGridViewTextBoxColumn();
            Marriage1 = new DataGridViewTextBoxColumn();
            Marriage2 = new DataGridViewTextBoxColumn();
            Marriage3 = new DataGridViewTextBoxColumn();
            Death = new DataGridViewTextBoxColumn();
            CremBuri = new DataGridViewTextBoxColumn();
            BirthDate = new DataGridViewTextBoxColumn();
            DeathDate = new DataGridViewTextBoxColumn();
            FirstMarriage = new DataGridViewTextBoxColumn();
            SecondMarriage = new DataGridViewTextBoxColumn();
            ThirdMarriage = new DataGridViewTextBoxColumn();
            BirthLocation = new DataGridViewTextBoxColumn();
            DeathLocation = new DataGridViewTextBoxColumn();
            Ahnentafel = new DataGridViewTextBoxColumn();
            statusStrip = new StatusStrip();
            tsRecords = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            mnuSaveCensusColumnLayout = new ToolStripButton();
            mnuResetCensusColumns = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            printToolStripButton = new ToolStripButton();
            printPreviewToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuExportToExcel = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            cbBMDSearchProvider = new ToolStripComboBox();
            toolStripLabel3 = new ToolStripLabel();
            cbRegion = new ToolStripComboBox();
            toolStripLabel2 = new ToolStripLabel();
            cbFilter = new ToolStripComboBox();
            tsApplyToLabel = new ToolStripLabel();
            cbApplyTo = new ToolStripComboBox();
            printDocument = new System.Drawing.Printing.PrintDocument();
            printDialog = new PrintDialog();
            printPreviewDialog = new PrintPreviewDialog();
            contextMenuStrip = new ContextMenuStrip(components);
            mnuViewFacts = new ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)dgBMDReportSheet).BeginInit();
            statusStrip.SuspendLayout();
            toolStrip1.SuspendLayout();
            contextMenuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // dgBMDReportSheet
            // 
            dgBMDReportSheet.AccessibleDescription = "";
            dgBMDReportSheet.AllowUserToAddRows = false;
            dgBMDReportSheet.AllowUserToDeleteRows = false;
            dgBMDReportSheet.AllowUserToOrderColumns = true;
            dgBMDReportSheet.AllowUserToResizeRows = false;
            dgBMDReportSheet.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Control;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.142858F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgBMDReportSheet.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dgBMDReportSheet.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgBMDReportSheet.Columns.AddRange(new DataGridViewColumn[] { IndividualID, Forenames, Surname, Relation, RelationToRoot, Birth, BaptChri, Marriage1, Marriage2, Marriage3, Death, CremBuri, BirthDate, DeathDate, FirstMarriage, SecondMarriage, ThirdMarriage, BirthLocation, DeathLocation, Ahnentafel });
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.142858F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dgBMDReportSheet.DefaultCellStyle = dataGridViewCellStyle2;
            dgBMDReportSheet.Dock = DockStyle.Fill;
            dgBMDReportSheet.Location = new Point(0, 35);
            dgBMDReportSheet.Name = "dgBMDReportSheet";
            dgBMDReportSheet.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Control;
            dataGridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.142858F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dgBMDReportSheet.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            dgBMDReportSheet.RowHeadersWidth = 20;
            dgBMDReportSheet.Size = new Size(1036, 440);
            dgBMDReportSheet.TabIndex = 1;
            dgBMDReportSheet.CellContextMenuStripNeeded += DgBMDReportSheet_CellContextMenuStripNeeded;
            dgBMDReportSheet.CellDoubleClick += DgReportSheet_CellDoubleClick;
            dgBMDReportSheet.CellFormatting += DgReportSheet_CellFormatting;
            dgBMDReportSheet.SelectionChanged += DgBMDReportSheet_SelectionChanged;
            // 
            // IndividualID
            // 
            IndividualID.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            IndividualID.DataPropertyName = "IndividualID";
            IndividualID.HeaderText = "Ind. ID";
            IndividualID.MinimumWidth = 50;
            IndividualID.Name = "IndividualID";
            IndividualID.ReadOnly = true;
            IndividualID.Width = 50;
            // 
            // Forenames
            // 
            Forenames.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Forenames.DataPropertyName = "Forenames";
            Forenames.HeaderText = "Forenames";
            Forenames.MinimumWidth = 100;
            Forenames.Name = "Forenames";
            Forenames.ReadOnly = true;
            // 
            // Surname
            // 
            Surname.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Surname.DataPropertyName = "Surname";
            Surname.HeaderText = "Surname";
            Surname.MinimumWidth = 75;
            Surname.Name = "Surname";
            Surname.ReadOnly = true;
            Surname.Width = 75;
            // 
            // Relation
            // 
            Relation.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Relation.DataPropertyName = "Relation";
            Relation.HeaderText = "Relation";
            Relation.MinimumWidth = 105;
            Relation.Name = "Relation";
            Relation.ReadOnly = true;
            Relation.Width = 105;
            // 
            // RelationToRoot
            // 
            RelationToRoot.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            RelationToRoot.DataPropertyName = "RelationToRoot";
            RelationToRoot.HeaderText = "Relation To Root";
            RelationToRoot.MinimumWidth = 100;
            RelationToRoot.Name = "RelationToRoot";
            RelationToRoot.ReadOnly = true;
            // 
            // Birth
            // 
            Birth.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Birth.DataPropertyName = "Birth";
            Birth.HeaderText = "Birth";
            Birth.MinimumWidth = 60;
            Birth.Name = "Birth";
            Birth.ReadOnly = true;
            Birth.Resizable = DataGridViewTriState.False;
            Birth.Width = 60;
            // 
            // BaptChri
            // 
            BaptChri.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            BaptChri.DataPropertyName = "BaptChri";
            BaptChri.HeaderText = "Baptism Christening";
            BaptChri.MinimumWidth = 62;
            BaptChri.Name = "BaptChri";
            BaptChri.ReadOnly = true;
            BaptChri.Resizable = DataGridViewTriState.False;
            BaptChri.Width = 114;
            // 
            // Marriage1
            // 
            Marriage1.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Marriage1.DataPropertyName = "Marriage1";
            Marriage1.HeaderText = "Marriage No. 1";
            Marriage1.MinimumWidth = 60;
            Marriage1.Name = "Marriage1";
            Marriage1.ReadOnly = true;
            Marriage1.Resizable = DataGridViewTriState.False;
            Marriage1.Width = 86;
            // 
            // Marriage2
            // 
            Marriage2.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Marriage2.DataPropertyName = "Marriage2";
            Marriage2.HeaderText = "Marriage No. 2";
            Marriage2.MinimumWidth = 60;
            Marriage2.Name = "Marriage2";
            Marriage2.ReadOnly = true;
            Marriage2.Resizable = DataGridViewTriState.False;
            Marriage2.Width = 86;
            // 
            // Marriage3
            // 
            Marriage3.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Marriage3.DataPropertyName = "Marriage3";
            Marriage3.HeaderText = "Marriage No. 3";
            Marriage3.MinimumWidth = 60;
            Marriage3.Name = "Marriage3";
            Marriage3.ReadOnly = true;
            Marriage3.Resizable = DataGridViewTriState.False;
            Marriage3.Width = 86;
            // 
            // Death
            // 
            Death.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Death.DataPropertyName = "Death";
            Death.HeaderText = "Death";
            Death.MinimumWidth = 60;
            Death.Name = "Death";
            Death.ReadOnly = true;
            Death.Resizable = DataGridViewTriState.False;
            Death.Width = 61;
            // 
            // CremBuri
            // 
            CremBuri.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            CremBuri.DataPropertyName = "CremBuri";
            CremBuri.HeaderText = "Burial Cremation";
            CremBuri.MinimumWidth = 60;
            CremBuri.Name = "CremBuri";
            CremBuri.ReadOnly = true;
            CremBuri.Resizable = DataGridViewTriState.False;
            CremBuri.Width = 99;
            // 
            // BirthDate
            // 
            BirthDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            BirthDate.DataPropertyName = "BirthDate";
            BirthDate.HeaderText = "Birth Date";
            BirthDate.MinimumWidth = 50;
            BirthDate.Name = "BirthDate";
            BirthDate.ReadOnly = true;
            BirthDate.Width = 73;
            // 
            // DeathDate
            // 
            DeathDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            DeathDate.DataPropertyName = "DeathDate";
            DeathDate.HeaderText = "Death Date";
            DeathDate.MinimumWidth = 50;
            DeathDate.Name = "DeathDate";
            DeathDate.ReadOnly = true;
            DeathDate.Width = 80;
            // 
            // FirstMarriage
            // 
            FirstMarriage.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            FirstMarriage.DataPropertyName = "FirstMarriage";
            FirstMarriage.HeaderText = "First Marriage";
            FirstMarriage.MinimumWidth = 100;
            FirstMarriage.Name = "FirstMarriage";
            FirstMarriage.ReadOnly = true;
            // 
            // SecondMarriage
            // 
            SecondMarriage.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            SecondMarriage.DataPropertyName = "SecondMarriage";
            SecondMarriage.HeaderText = "Second Marriage";
            SecondMarriage.MinimumWidth = 100;
            SecondMarriage.Name = "SecondMarriage";
            SecondMarriage.ReadOnly = true;
            SecondMarriage.Width = 104;
            // 
            // ThirdMarriage
            // 
            ThirdMarriage.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            ThirdMarriage.DataPropertyName = "ThirdMarriage";
            ThirdMarriage.HeaderText = "Third Marriage";
            ThirdMarriage.MinimumWidth = 100;
            ThirdMarriage.Name = "ThirdMarriage";
            ThirdMarriage.ReadOnly = true;
            // 
            // BirthLocation
            // 
            BirthLocation.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            BirthLocation.DataPropertyName = "BirthLocation";
            BirthLocation.HeaderText = "Birth Location";
            BirthLocation.MinimumWidth = 120;
            BirthLocation.Name = "BirthLocation";
            BirthLocation.ReadOnly = true;
            BirthLocation.Width = 120;
            // 
            // DeathLocation
            // 
            DeathLocation.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            DeathLocation.DataPropertyName = "DeathLocation";
            DeathLocation.HeaderText = "Death Location";
            DeathLocation.MinimumWidth = 120;
            DeathLocation.Name = "DeathLocation";
            DeathLocation.ReadOnly = true;
            DeathLocation.Width = 120;
            // 
            // Ahnentafel
            // 
            Ahnentafel.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            Ahnentafel.DataPropertyName = "Ahnentafel";
            Ahnentafel.HeaderText = "Ahnentafel";
            Ahnentafel.MinimumWidth = 20;
            Ahnentafel.Name = "Ahnentafel";
            Ahnentafel.ReadOnly = true;
            Ahnentafel.Width = 83;
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(28, 28);
            statusStrip.Items.AddRange(new ToolStripItem[] { tsRecords });
            statusStrip.Location = new Point(0, 475);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1036, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            tsRecords.Name = "tsRecords";
            tsRecords.Size = new Size(118, 17);
            tsRecords.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(28, 28);
            toolStrip1.Items.AddRange(new ToolStripItem[] { mnuSaveCensusColumnLayout, mnuResetCensusColumns, toolStripSeparator3, printToolStripButton, printPreviewToolStripButton, toolStripSeparator1, mnuExportToExcel, toolStripSeparator2, toolStripLabel1, cbBMDSearchProvider, toolStripLabel3, cbRegion, toolStripLabel2, cbFilter, tsApplyToLabel, cbApplyTo });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 2, 0);
            toolStrip1.Size = new Size(1036, 35);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveCensusColumnLayout
            // 
            mnuSaveCensusColumnLayout.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuSaveCensusColumnLayout.Image = (Image)resources.GetObject("mnuSaveCensusColumnLayout.Image");
            mnuSaveCensusColumnLayout.ImageTransparentColor = Color.Magenta;
            mnuSaveCensusColumnLayout.Name = "mnuSaveCensusColumnLayout";
            mnuSaveCensusColumnLayout.Size = new Size(32, 32);
            mnuSaveCensusColumnLayout.Text = "Save Census Column Sort Order";
            mnuSaveCensusColumnLayout.Click += MnuSaveCensusColumnLayout_Click;
            // 
            // mnuResetCensusColumns
            // 
            mnuResetCensusColumns.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuResetCensusColumns.Image = (Image)resources.GetObject("mnuResetCensusColumns.Image");
            mnuResetCensusColumns.ImageTransparentColor = Color.Magenta;
            mnuResetCensusColumns.Name = "mnuResetCensusColumns";
            mnuResetCensusColumns.Size = new Size(32, 32);
            mnuResetCensusColumns.Text = "Reset Census Column Sort Order to Default";
            mnuResetCensusColumns.Click += MnuResetCensusColumns_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 35);
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printToolStripButton.Image = (Image)resources.GetObject("printToolStripButton.Image");
            printToolStripButton.ImageTransparentColor = Color.Magenta;
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Size = new Size(32, 32);
            printToolStripButton.Text = "&Print";
            printToolStripButton.Click += PrintToolStripButton_Click;
            // 
            // printPreviewToolStripButton
            // 
            printPreviewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printPreviewToolStripButton.Image = (Image)resources.GetObject("printPreviewToolStripButton.Image");
            printPreviewToolStripButton.ImageTransparentColor = Color.Magenta;
            printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            printPreviewToolStripButton.Size = new Size(32, 32);
            printPreviewToolStripButton.Text = "Print Preview...";
            printPreviewToolStripButton.Click += PrintPreviewToolStripButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 35);
            // 
            // mnuExportToExcel
            // 
            mnuExportToExcel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuExportToExcel.Image = (Image)resources.GetObject("mnuExportToExcel.Image");
            mnuExportToExcel.ImageTransparentColor = Color.Magenta;
            mnuExportToExcel.Name = "mnuExportToExcel";
            mnuExportToExcel.Size = new Size(32, 32);
            mnuExportToExcel.Text = "Export to Excel";
            mnuExportToExcel.Click += MnuExportToExcel_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 35);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(106, 32);
            toolStripLabel1.Text = "BMD Search using:";
            // 
            // cbBMDSearchProvider
            // 
            cbBMDSearchProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            cbBMDSearchProvider.Items.AddRange(new object[] { "Ancestry", "Find My Past", "FreeBMD", "FamilySearch", "Scotlands People" });
            cbBMDSearchProvider.Name = "cbBMDSearchProvider";
            cbBMDSearchProvider.Size = new Size(141, 35);
            cbBMDSearchProvider.SelectedIndexChanged += CbCensusSearchProvider_SelectedIndexChanged;
            // 
            // toolStripLabel3
            // 
            toolStripLabel3.Name = "toolStripLabel3";
            toolStripLabel3.Size = new Size(47, 32);
            toolStripLabel3.Text = "Region:";
            // 
            // cbRegion
            // 
            cbRegion.AutoCompleteCustomSource.AddRange(new string[] { ".com", ".co.uk", ".ca", ".com.au" });
            cbRegion.Items.AddRange(new object[] { ".com", ".co.uk", ".ca", ".com.au" });
            cbRegion.Name = "cbRegion";
            cbRegion.Size = new Size(141, 35);
            cbRegion.Text = ".co.uk";
            cbRegion.SelectedIndexChanged += CbRegion_SelectedIndexChanged;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(39, 32);
            toolStripLabel2.Text = "Filter :";
            // 
            // cbFilter
            // 
            cbFilter.DropDownStyle = ComboBoxStyle.DropDownList;
            cbFilter.DropDownWidth = 330;
            cbFilter.Items.AddRange(new object[] { "All Individuals", "Date Missing (Red)", "Date Found (Green)", "Open Ended Date Range (Orange Red)", "Very Wide Date Range (Light Red)", "Wide Date Range (Orange)", "Narrow Date Range (Yellow)", "Just Year Date (Yellow Green)", "Approx Date Range (Light Green)", "Of Marrying Age no partner (Pink)", "No Partner shared fact/children (Coral)", "Partner but no marriage (Red Brown)" });
            cbFilter.Name = "cbFilter";
            cbFilter.Size = new Size(150, 35);
            cbFilter.SelectedIndexChanged += CbFilter_SelectedIndexChanged;
            // 
            // tsApplyToLabel
            // 
            tsApplyToLabel.AccessibleDescription = "";
            tsApplyToLabel.Name = "tsApplyToLabel";
            tsApplyToLabel.Size = new Size(57, 32);
            tsApplyToLabel.Text = "Apply To:";
            // 
            // cbApplyTo
            // 
            cbApplyTo.DropDownStyle = ComboBoxStyle.DropDownList;
            cbApplyTo.DropDownWidth = 121;
            cbApplyTo.Items.AddRange(new object[] { "All BMD Records", "Births Only", "Marriages Only", "Deaths Only", "Births & Deaths", "Births & Marriages", "Marriages & Deaths" });
            cbApplyTo.Name = "cbApplyTo";
            cbApplyTo.Size = new Size(75, 35);
            cbApplyTo.SelectedIndexChanged += CbApplyTo_SelectedIndexChanged;
            // 
            // printDialog
            // 
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;
            printDialog.Document = printDocument;
            printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            printPreviewDialog.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog.ClientSize = new Size(400, 300);
            printPreviewDialog.Document = printDocument;
            printPreviewDialog.Enabled = true;
            printPreviewDialog.Icon = (Icon)resources.GetObject("printPreviewDialog.Icon");
            printPreviewDialog.Name = "printPreviewDialog";
            printPreviewDialog.Visible = false;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(28, 28);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { mnuViewFacts });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(190, 26);
            // 
            // mnuViewFacts
            // 
            mnuViewFacts.Name = "mnuViewFacts";
            mnuViewFacts.Size = new Size(189, 22);
            mnuViewFacts.Text = "View Individuals Facts";
            mnuViewFacts.Click += MnuViewFacts_Click;
            // 
            // ColourBMD
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1036, 497);
            Controls.Add(dgBMDReportSheet);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "ColourBMD";
            Text = "Colour BMD Report Result";
            FormClosed += ColourBMD_FormClosed;
            Load += ColourBMD_Load;
            ((System.ComponentModel.ISupportInitialize)dgBMDReportSheet).EndInit();
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            contextMenuStrip.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
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
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cbRegion;
        private System.Windows.Forms.ToolStripLabel tsApplyToLabel;
        private System.Windows.Forms.ToolStripComboBox cbApplyTo;
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
        private System.Windows.Forms.DataGridViewTextBoxColumn Ahnentafel;
    }
}