﻿using System;

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
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.cbRegion = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbFilter = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.cbApplyTo = new System.Windows.Forms.ToolStripComboBox();
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
            this.Ahnentafel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgBMDReportSheet)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgBMDReportSheet
            // 
            this.dgBMDReportSheet.AccessibleDescription = "";
            this.dgBMDReportSheet.AllowUserToAddRows = false;
            this.dgBMDReportSheet.AllowUserToDeleteRows = false;
            this.dgBMDReportSheet.AllowUserToOrderColumns = true;
            this.dgBMDReportSheet.AllowUserToResizeRows = false;
            this.dgBMDReportSheet.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgBMDReportSheet.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
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
            this.Ahnentafel});
            this.dgBMDReportSheet.Location = new System.Drawing.Point(0, 52);
            this.dgBMDReportSheet.Margin = new System.Windows.Forms.Padding(6);
            this.dgBMDReportSheet.MultiSelect = false;
            this.dgBMDReportSheet.Name = "dgBMDReportSheet";
            this.dgBMDReportSheet.ReadOnly = true;
            this.dgBMDReportSheet.RowHeadersWidth = 20;
            this.dgBMDReportSheet.Size = new System.Drawing.Size(1903, 978);
            this.dgBMDReportSheet.TabIndex = 1;
            this.dgBMDReportSheet.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.DgBMDReportSheet_CellContextMenuStripNeeded);
            this.dgBMDReportSheet.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgReportSheet_CellDoubleClick);
            this.dgBMDReportSheet.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgReportSheet_CellFormatting);
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
            this.toolStripSeparator3,
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator1,
            this.mnuExportToExcel,
            this.toolStripSeparator2,
            this.toolStripLabel1,
            this.cbBMDSearchProvider,
            this.toolStripLabel3,
            this.cbRegion,
            this.toolStripLabel2,
            this.cbFilter,
            this.toolStripLabel4,
            this.cbApplyTo});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1903, 38);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveCensusColumnLayout
            // 
            this.mnuSaveCensusColumnLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSaveCensusColumnLayout.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveCensusColumnLayout.Image")));
            this.mnuSaveCensusColumnLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSaveCensusColumnLayout.Name = "mnuSaveCensusColumnLayout";
            this.mnuSaveCensusColumnLayout.Size = new System.Drawing.Size(40, 32);
            this.mnuSaveCensusColumnLayout.Text = "Save Census Column Sort Order";
            this.mnuSaveCensusColumnLayout.Click += new System.EventHandler(this.MnuSaveCensusColumnLayout_Click);
            // 
            // mnuResetCensusColumns
            // 
            this.mnuResetCensusColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuResetCensusColumns.Image = ((System.Drawing.Image)(resources.GetObject("mnuResetCensusColumns.Image")));
            this.mnuResetCensusColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuResetCensusColumns.Name = "mnuResetCensusColumns";
            this.mnuResetCensusColumns.Size = new System.Drawing.Size(40, 32);
            this.mnuResetCensusColumns.Text = "Reset Census Column Sort Order to Default";
            this.mnuResetCensusColumns.Click += new System.EventHandler(this.MnuResetCensusColumns_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(40, 32);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.Click += new System.EventHandler(this.PrintToolStripButton_Click);
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(40, 32);
            this.printPreviewToolStripButton.Text = "Print Preview...";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.PrintPreviewToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportToExcel.Image")));
            this.mnuExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(40, 32);
            this.mnuExportToExcel.Text = "Export to Excel";
            this.mnuExportToExcel.Click += new System.EventHandler(this.MnuExportToExcel_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(188, 32);
            this.toolStripLabel1.Text = "BMD Search using:";
            // 
            // cbBMDSearchProvider
            // 
            this.cbBMDSearchProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBMDSearchProvider.Items.AddRange(new object[] {
            "Ancestry",
            "Find My Past",
            "FreeBMD",
            "FamilySearch",
            "Scotlands People"});
            this.cbBMDSearchProvider.Name = "cbBMDSearchProvider";
            this.cbBMDSearchProvider.Size = new System.Drawing.Size(219, 38);
            this.cbBMDSearchProvider.SelectedIndexChanged += new System.EventHandler(this.CbCensusSearchProvider_SelectedIndexChanged);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(82, 32);
            this.toolStripLabel3.Text = "Region:";
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
            this.cbRegion.Size = new System.Drawing.Size(219, 38);
            this.cbRegion.Text = ".co.uk";
            this.cbRegion.SelectedIndexChanged += new System.EventHandler(this.CbRegion_SelectedIndexChanged);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(69, 32);
            this.toolStripLabel2.Text = "Filter :";
            // 
            // cbFilter
            // 
            this.cbFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilter.DropDownWidth = 220;
            this.cbFilter.Items.AddRange(new object[] {
            "All Individuals",
            "Date Missing (Red)",
            "Date Found (Green)",
            "Open Ended Date Range (Orange Red)",
            "Very Wide Date Range (Light Red)",
            "Wide Date Range (Orange)",
            "Narrow Date Range (Yellow)",
            "Just Year Date (Yellow Green)",
            "Approx Date Range (Light Green)",
            "Of Marrying Age no partner (Pink)",
            "No Partner shared fact/children (Coral)",
            "Partner but no marriage (Red Brown)"});
            this.cbFilter.Name = "cbFilter";
            this.cbFilter.Size = new System.Drawing.Size(235, 38);
            this.cbFilter.SelectedIndexChanged += new System.EventHandler(this.CbFilter_SelectedIndexChanged);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.AccessibleDescription = "";
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(98, 32);
            this.toolStripLabel4.Text = "Apply To:";
            // 
            // cbApplyTo
            // 
            this.cbApplyTo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbApplyTo.DropDownWidth = 121;
            this.cbApplyTo.Items.AddRange(new object[] {
            "All BMD Records",
            "Births Only",
            "Marriages Only",
            "Deaths Only",
            "Births & Deaths",
            "Births & Marriages",
            "Marriages & Deaths"});
            this.cbApplyTo.Name = "cbApplyTo";
            this.cbApplyTo.Size = new System.Drawing.Size(121, 38);
            this.cbApplyTo.SelectedIndexChanged += new System.EventHandler(this.CbApplyTo_SelectedIndexChanged);
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
            // IndividualID
            // 
            this.IndividualID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.IndividualID.DataPropertyName = "IndividualID";
            this.IndividualID.HeaderText = "Ind. ID";
            this.IndividualID.MinimumWidth = 50;
            this.IndividualID.Name = "IndividualID";
            this.IndividualID.ReadOnly = true;
            this.IndividualID.Width = 109;
            // 
            // Forenames
            // 
            this.Forenames.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Forenames.DataPropertyName = "Forenames";
            this.Forenames.HeaderText = "Forenames";
            this.Forenames.MinimumWidth = 100;
            this.Forenames.Name = "Forenames";
            this.Forenames.ReadOnly = true;
            this.Forenames.Width = 152;
            // 
            // Surname
            // 
            this.Surname.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Surname.DataPropertyName = "Surname";
            this.Surname.HeaderText = "Surname";
            this.Surname.MinimumWidth = 75;
            this.Surname.Name = "Surname";
            this.Surname.ReadOnly = true;
            this.Surname.Width = 133;
            // 
            // Relation
            // 
            this.Relation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Relation.DataPropertyName = "Relation";
            this.Relation.HeaderText = "Relation";
            this.Relation.MinimumWidth = 105;
            this.Relation.Name = "Relation";
            this.Relation.ReadOnly = true;
            this.Relation.Width = 123;
            // 
            // RelationToRoot
            // 
            this.RelationToRoot.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.RelationToRoot.DataPropertyName = "RelationToRoot";
            this.RelationToRoot.HeaderText = "Relation To Root";
            this.RelationToRoot.MinimumWidth = 100;
            this.RelationToRoot.Name = "RelationToRoot";
            this.RelationToRoot.ReadOnly = true;
            this.RelationToRoot.Width = 145;
            // 
            // Birth
            // 
            this.Birth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Birth.DataPropertyName = "Birth";
            this.Birth.HeaderText = "Birth";
            this.Birth.MinimumWidth = 60;
            this.Birth.Name = "Birth";
            this.Birth.ReadOnly = true;
            this.Birth.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Birth.Width = 92;
            // 
            // BaptChri
            // 
            this.BaptChri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.BaptChri.DataPropertyName = "BaptChri";
            this.BaptChri.HeaderText = "Baptism Christening";
            this.BaptChri.MinimumWidth = 62;
            this.BaptChri.Name = "BaptChri";
            this.BaptChri.ReadOnly = true;
            this.BaptChri.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.BaptChri.Width = 208;
            // 
            // Marriage1
            // 
            this.Marriage1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Marriage1.DataPropertyName = "Marriage1";
            this.Marriage1.HeaderText = "Marriage No. 1";
            this.Marriage1.MinimumWidth = 60;
            this.Marriage1.Name = "Marriage1";
            this.Marriage1.ReadOnly = true;
            this.Marriage1.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Marriage1.Width = 152;
            // 
            // Marriage2
            // 
            this.Marriage2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Marriage2.DataPropertyName = "Marriage2";
            this.Marriage2.HeaderText = "Marriage No. 2";
            this.Marriage2.MinimumWidth = 60;
            this.Marriage2.Name = "Marriage2";
            this.Marriage2.ReadOnly = true;
            this.Marriage2.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Marriage2.Width = 152;
            // 
            // Marriage3
            // 
            this.Marriage3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Marriage3.DataPropertyName = "Marriage3";
            this.Marriage3.HeaderText = "Marriage No. 3";
            this.Marriage3.MinimumWidth = 60;
            this.Marriage3.Name = "Marriage3";
            this.Marriage3.ReadOnly = true;
            this.Marriage3.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Marriage3.Width = 152;
            // 
            // Death
            // 
            this.Death.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Death.DataPropertyName = "Death";
            this.Death.HeaderText = "Death";
            this.Death.MinimumWidth = 60;
            this.Death.Name = "Death";
            this.Death.ReadOnly = true;
            this.Death.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Death.Width = 105;
            // 
            // CremBuri
            // 
            this.CremBuri.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.CremBuri.DataPropertyName = "CremBuri";
            this.CremBuri.HeaderText = "Burial Cremation";
            this.CremBuri.MinimumWidth = 60;
            this.CremBuri.Name = "CremBuri";
            this.CremBuri.ReadOnly = true;
            this.CremBuri.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CremBuri.Width = 181;
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
            // FirstMarriage
            // 
            this.FirstMarriage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FirstMarriage.DataPropertyName = "FirstMarriage";
            this.FirstMarriage.HeaderText = "First Marriage";
            this.FirstMarriage.MinimumWidth = 100;
            this.FirstMarriage.Name = "FirstMarriage";
            this.FirstMarriage.ReadOnly = true;
            this.FirstMarriage.Width = 158;
            // 
            // SecondMarriage
            // 
            this.SecondMarriage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SecondMarriage.DataPropertyName = "SecondMarriage";
            this.SecondMarriage.HeaderText = "Second Marriage";
            this.SecondMarriage.MinimumWidth = 100;
            this.SecondMarriage.Name = "SecondMarriage";
            this.SecondMarriage.ReadOnly = true;
            this.SecondMarriage.Width = 186;
            // 
            // ThirdMarriage
            // 
            this.ThirdMarriage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.ThirdMarriage.DataPropertyName = "ThirdMarriage";
            this.ThirdMarriage.HeaderText = "Third Marriage";
            this.ThirdMarriage.MinimumWidth = 100;
            this.ThirdMarriage.Name = "ThirdMarriage";
            this.ThirdMarriage.ReadOnly = true;
            this.ThirdMarriage.Width = 166;
            // 
            // BirthLocation
            // 
            this.BirthLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.BirthLocation.DataPropertyName = "BirthLocation";
            this.BirthLocation.HeaderText = "Birth Location";
            this.BirthLocation.MinimumWidth = 120;
            this.BirthLocation.Name = "BirthLocation";
            this.BirthLocation.ReadOnly = true;
            this.BirthLocation.Width = 157;
            // 
            // DeathLocation
            // 
            this.DeathLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.DeathLocation.DataPropertyName = "DeathLocation";
            this.DeathLocation.HeaderText = "Death Location";
            this.DeathLocation.MinimumWidth = 120;
            this.DeathLocation.Name = "DeathLocation";
            this.DeathLocation.ReadOnly = true;
            this.DeathLocation.Width = 169;
            // 
            // Ahnentafel
            // 
            this.Ahnentafel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Ahnentafel.DataPropertyName = "Ahnentafel";
            this.Ahnentafel.HeaderText = "Ahnentafel";
            this.Ahnentafel.MinimumWidth = 20;
            this.Ahnentafel.Name = "Ahnentafel";
            this.Ahnentafel.ReadOnly = true;
            this.Ahnentafel.Width = 147;
            // 
            // ColourBMD
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1903, 1076);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgBMDReportSheet);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "ColourBMD";
            this.Text = "Colour BMD Report Result";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ColourBMD_FormClosed);
            this.Load += new System.EventHandler(this.ColourBMD_Load);
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
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripComboBox cbRegion;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
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