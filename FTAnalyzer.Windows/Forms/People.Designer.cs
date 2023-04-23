using System;

namespace FTAnalyzer.Forms
{
    partial class People
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
                indReportFormHelper.Dispose();
                famReportFormHelper.Dispose();
                boldFont.Dispose();
                normalFont.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(People));
            statusStrip1 = new StatusStrip();
            txtCount = new ToolStripStatusLabel();
            splitContainer = new SplitContainer();
            dgIndividuals = new Controls.VirtualDGVIndividuals();
            ctxViewNotes = new ContextMenuStrip(components);
            viewNotesToolStripMenuItem = new ToolStripMenuItem();
            dgChildrenStatus = new Controls.VirtualDGVChildrenStatus();
            dgFamilies = new Controls.VirtualDGVFamily();
            toolStrip1 = new ToolStrip();
            mnuSaveColumnLayout = new ToolStripButton();
            mnuResetColumns = new ToolStripButton();
            tssSaveButtons = new ToolStripSeparator();
            printToolStripButton = new ToolStripButton();
            printPreviewToolStripButton = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            mnuExportToExcel = new ToolStripButton();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgIndividuals).BeginInit();
            ctxViewNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgChildrenStatus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgFamilies).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(28, 28);
            statusStrip1.Items.AddRange(new ToolStripItem[] { txtCount });
            statusStrip1.Location = new Point(0, 1323);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Padding = new Padding(2, 0, 28, 0);
            statusStrip1.Size = new Size(2076, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // txtCount
            // 
            txtCount.Name = "txtCount";
            txtCount.Size = new Size(0, 13);
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Margin = new Padding(7, 8, 7, 8);
            splitContainer.Name = "splitContainer";
            splitContainer.Orientation = Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(dgIndividuals);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(dgChildrenStatus);
            splitContainer.Panel2.Controls.Add(dgFamilies);
            splitContainer.Size = new Size(2076, 1323);
            splitContainer.SplitterDistance = 673;
            splitContainer.SplitterWidth = 9;
            splitContainer.TabIndex = 7;
            // 
            // dgIndividuals
            // 
            dgIndividuals.AllowUserToAddRows = false;
            dgIndividuals.AllowUserToDeleteRows = false;
            dgIndividuals.AllowUserToOrderColumns = true;
            dgIndividuals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgIndividuals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgIndividuals.ContextMenuStrip = ctxViewNotes;
            dgIndividuals.Dock = DockStyle.Fill;
            dgIndividuals.FilterAndSortEnabled = true;
            dgIndividuals.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgIndividuals.Location = new Point(0, 0);
            dgIndividuals.Margin = new Padding(7, 8, 7, 8);
            dgIndividuals.MultiSelect = false;
            dgIndividuals.Name = "dgIndividuals";
            dgIndividuals.ReadOnly = true;
            dgIndividuals.RightToLeft = RightToLeft.No;
            dgIndividuals.RowHeadersVisible = false;
            dgIndividuals.RowHeadersWidth = 50;
            dgIndividuals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgIndividuals.Size = new Size(2076, 673);
            dgIndividuals.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgIndividuals.TabIndex = 2;
            dgIndividuals.VirtualMode = true;
            dgIndividuals.CellDoubleClick += DgIndividuals_CellDoubleClick;
            dgIndividuals.MouseDown += DgIndividuals_MouseDown;
            // 
            // ctxViewNotes
            // 
            ctxViewNotes.ImageScalingSize = new Size(28, 28);
            ctxViewNotes.Items.AddRange(new ToolStripItem[] { viewNotesToolStripMenuItem });
            ctxViewNotes.Name = "contextMenuStrip1";
            ctxViewNotes.Size = new Size(192, 40);
            ctxViewNotes.Opened += ContextMenuStrip1_Opened;
            // 
            // viewNotesToolStripMenuItem
            // 
            viewNotesToolStripMenuItem.Name = "viewNotesToolStripMenuItem";
            viewNotesToolStripMenuItem.Size = new Size(191, 36);
            viewNotesToolStripMenuItem.Text = "View Notes";
            viewNotesToolStripMenuItem.Click += ViewNotesToolStripMenuItem_Click;
            // 
            // dgChildrenStatus
            // 
            dgChildrenStatus.AllowUserToAddRows = false;
            dgChildrenStatus.AllowUserToDeleteRows = false;
            dgChildrenStatus.AllowUserToOrderColumns = true;
            dgChildrenStatus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgChildrenStatus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgChildrenStatus.Dock = DockStyle.Fill;
            dgChildrenStatus.FilterAndSortEnabled = true;
            dgChildrenStatus.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgChildrenStatus.Location = new Point(0, 0);
            dgChildrenStatus.Margin = new Padding(7, 8, 7, 8);
            dgChildrenStatus.MultiSelect = false;
            dgChildrenStatus.Name = "dgChildrenStatus";
            dgChildrenStatus.ReadOnly = true;
            dgChildrenStatus.RightToLeft = RightToLeft.No;
            dgChildrenStatus.RowHeadersVisible = false;
            dgChildrenStatus.RowHeadersWidth = 50;
            dgChildrenStatus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgChildrenStatus.Size = new Size(2076, 641);
            dgChildrenStatus.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgChildrenStatus.TabIndex = 6;
            dgChildrenStatus.VirtualMode = true;
            dgChildrenStatus.CellDoubleClick += DgFamilies_CellDoubleClick;
            dgChildrenStatus.CellFormatting += DgChildrenStatus_CellFormatting;
            // 
            // dgFamilies
            // 
            dgFamilies.AllowUserToAddRows = false;
            dgFamilies.AllowUserToDeleteRows = false;
            dgFamilies.AllowUserToOrderColumns = true;
            dgFamilies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgFamilies.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgFamilies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgFamilies.Dock = DockStyle.Fill;
            dgFamilies.FilterAndSortEnabled = true;
            dgFamilies.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgFamilies.Location = new Point(0, 0);
            dgFamilies.Margin = new Padding(7, 8, 7, 8);
            dgFamilies.MultiSelect = false;
            dgFamilies.Name = "dgFamilies";
            dgFamilies.ReadOnly = true;
            dgFamilies.RightToLeft = RightToLeft.No;
            dgFamilies.RowHeadersVisible = false;
            dgFamilies.RowHeadersWidth = 50;
            dgFamilies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgFamilies.Size = new Size(2076, 641);
            dgFamilies.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgFamilies.TabIndex = 5;
            dgFamilies.VirtualMode = true;
            dgFamilies.CellDoubleClick += DgFamilies_CellDoubleClick;
            dgFamilies.CellFormatting += DgFamilies_CellFormatting;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(28, 28);
            toolStrip1.Items.AddRange(new ToolStripItem[] { mnuSaveColumnLayout, mnuResetColumns, tssSaveButtons, printToolStripButton, printPreviewToolStripButton, toolStripSeparator, mnuExportToExcel });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 4, 0);
            toolStrip1.Size = new Size(2076, 38);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveColumnLayout
            // 
            mnuSaveColumnLayout.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuSaveColumnLayout.Image = (Image)resources.GetObject("mnuSaveColumnLayout.Image");
            mnuSaveColumnLayout.ImageTransparentColor = Color.Magenta;
            mnuSaveColumnLayout.Name = "mnuSaveColumnLayout";
            mnuSaveColumnLayout.Size = new Size(40, 32);
            mnuSaveColumnLayout.Text = "Save Column Sort Order, layout, width etc";
            mnuSaveColumnLayout.Visible = false;
            mnuSaveColumnLayout.Click += MnuSaveColumnLayout_Click;
            // 
            // mnuResetColumns
            // 
            mnuResetColumns.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuResetColumns.Image = (Image)resources.GetObject("mnuResetColumns.Image");
            mnuResetColumns.ImageTransparentColor = Color.Magenta;
            mnuResetColumns.Name = "mnuResetColumns";
            mnuResetColumns.Size = new Size(40, 32);
            mnuResetColumns.Text = "Reset Column Sort Order to Default";
            mnuResetColumns.Visible = false;
            mnuResetColumns.Click += MnuResetColumns_Click;
            // 
            // tssSaveButtons
            // 
            tssSaveButtons.Name = "tssSaveButtons";
            tssSaveButtons.Size = new Size(6, 38);
            tssSaveButtons.Visible = false;
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printToolStripButton.Image = (Image)resources.GetObject("printToolStripButton.Image");
            printToolStripButton.ImageTransparentColor = Color.Magenta;
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Size = new Size(40, 32);
            printToolStripButton.Text = "&Print";
            printToolStripButton.Click += PrintToolStripButton_Click;
            // 
            // printPreviewToolStripButton
            // 
            printPreviewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printPreviewToolStripButton.Image = (Image)resources.GetObject("printPreviewToolStripButton.Image");
            printPreviewToolStripButton.ImageTransparentColor = Color.Magenta;
            printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            printPreviewToolStripButton.Size = new Size(40, 32);
            printPreviewToolStripButton.Text = "Print Preview...";
            printPreviewToolStripButton.Click += PrintPreviewToolStripButton_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 38);
            // 
            // mnuExportToExcel
            // 
            mnuExportToExcel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuExportToExcel.Image = (Image)resources.GetObject("mnuExportToExcel.Image");
            mnuExportToExcel.ImageTransparentColor = Color.Magenta;
            mnuExportToExcel.Name = "mnuExportToExcel";
            mnuExportToExcel.Size = new Size(40, 32);
            mnuExportToExcel.Text = "Export to Excel";
            mnuExportToExcel.Click += MnuExportToExcel_Click;
            // 
            // People
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(2076, 1345);
            Controls.Add(toolStrip1);
            Controls.Add(splitContainer);
            Controls.Add(statusStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(7, 8, 7, 8);
            Name = "People";
            Text = "Individuals & Families";
            FormClosed += People_FormClosed;
            Load += People_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgIndividuals).EndInit();
            ctxViewNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgChildrenStatus).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgFamilies).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtCount;
        private System.Windows.Forms.SplitContainer splitContainer;
        private FTAnalyzer.Forms.Controls.VirtualDGVIndividuals dgIndividuals;
        private FTAnalyzer.Forms.Controls.VirtualDGVFamily dgFamilies;
        private FTAnalyzer.Forms.Controls.VirtualDGVChildrenStatus dgChildrenStatus;
        private System.Windows.Forms.ContextMenuStrip ctxViewNotes;
        private System.Windows.Forms.ToolStripMenuItem viewNotesToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton mnuSaveColumnLayout;
        private System.Windows.Forms.ToolStripButton mnuResetColumns;
        private System.Windows.Forms.ToolStripSeparator tssSaveButtons;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
    }
}