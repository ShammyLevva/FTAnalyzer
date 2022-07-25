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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(People));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgIndividuals = new FTAnalyzer.Forms.Controls.VirtualDGVIndividuals();
            this.ctxViewNotes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.viewNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dgChildrenStatus = new FTAnalyzer.Forms.Controls.VirtualDGVChildrenStatus();
            this.dgFamilies = new FTAnalyzer.Forms.Controls.VirtualDGVFamily();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuSaveColumnLayout = new System.Windows.Forms.ToolStripButton();
            this.mnuResetColumns = new System.Windows.Forms.ToolStripButton();
            this.tssSaveButtons = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.ctxViewNotes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgChildrenStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 1054);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(2, 0, 26, 0);
            this.statusStrip1.Size = new System.Drawing.Size(1903, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtCount
            // 
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(0, 13);
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 38);
            this.splitContainer.Margin = new System.Windows.Forms.Padding(6);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgIndividuals);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgChildrenStatus);
            this.splitContainer.Panel2.Controls.Add(this.dgFamilies);
            this.splitContainer.Size = new System.Drawing.Size(1903, 1016);
            this.splitContainer.SplitterDistance = 517;
            this.splitContainer.SplitterWidth = 7;
            this.splitContainer.TabIndex = 7;
            // 
            // dgIndividuals
            // 
            this.dgIndividuals.AllowUserToAddRows = false;
            this.dgIndividuals.AllowUserToDeleteRows = false;
            this.dgIndividuals.AllowUserToOrderColumns = true;
            this.dgIndividuals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.ContextMenuStrip = this.ctxViewNotes;
            this.dgIndividuals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgIndividuals.Location = new System.Drawing.Point(0, 0);
            this.dgIndividuals.Margin = new System.Windows.Forms.Padding(6);
            this.dgIndividuals.MultiSelect = false;
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.ReadOnly = true;
            this.dgIndividuals.RowHeadersVisible = false;
            this.dgIndividuals.RowHeadersWidth = 50;
            this.dgIndividuals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgIndividuals.Size = new System.Drawing.Size(1903, 517);
            this.dgIndividuals.TabIndex = 2;
            this.dgIndividuals.VirtualMode = true;
            this.dgIndividuals.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgIndividuals_CellDoubleClick);
            this.dgIndividuals.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgIndividuals_MouseDown);
            // 
            // ctxViewNotes
            // 
            this.ctxViewNotes.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.ctxViewNotes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewNotesToolStripMenuItem});
            this.ctxViewNotes.Name = "contextMenuStrip1";
            this.ctxViewNotes.Size = new System.Drawing.Size(192, 40);
            this.ctxViewNotes.Opened += new System.EventHandler(this.ContextMenuStrip1_Opened);
            // 
            // viewNotesToolStripMenuItem
            // 
            this.viewNotesToolStripMenuItem.Name = "viewNotesToolStripMenuItem";
            this.viewNotesToolStripMenuItem.Size = new System.Drawing.Size(191, 36);
            this.viewNotesToolStripMenuItem.Text = "View Notes";
            this.viewNotesToolStripMenuItem.Click += new System.EventHandler(this.ViewNotesToolStripMenuItem_Click);
            // 
            // dgChildrenStatus
            // 
            this.dgChildrenStatus.AllowUserToAddRows = false;
            this.dgChildrenStatus.AllowUserToDeleteRows = false;
            this.dgChildrenStatus.AllowUserToOrderColumns = true;
            this.dgChildrenStatus.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgChildrenStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgChildrenStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgChildrenStatus.Location = new System.Drawing.Point(0, 0);
            this.dgChildrenStatus.Margin = new System.Windows.Forms.Padding(6);
            this.dgChildrenStatus.MultiSelect = false;
            this.dgChildrenStatus.Name = "dgChildrenStatus";
            this.dgChildrenStatus.ReadOnly = true;
            this.dgChildrenStatus.RowHeadersVisible = false;
            this.dgChildrenStatus.RowHeadersWidth = 50;
            this.dgChildrenStatus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgChildrenStatus.Size = new System.Drawing.Size(1903, 492);
            this.dgChildrenStatus.TabIndex = 6;
            this.dgChildrenStatus.VirtualMode = true;
            this.dgChildrenStatus.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgFamilies_CellDoubleClick);
            this.dgChildrenStatus.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgChildrenStatus_CellFormatting);
            // 
            // dgFamilies
            // 
            this.dgFamilies.AllowUserToAddRows = false;
            this.dgFamilies.AllowUserToDeleteRows = false;
            this.dgFamilies.AllowUserToOrderColumns = true;
            this.dgFamilies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgFamilies.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.dgFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFamilies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFamilies.Location = new System.Drawing.Point(0, 0);
            this.dgFamilies.Margin = new System.Windows.Forms.Padding(6);
            this.dgFamilies.MultiSelect = false;
            this.dgFamilies.Name = "dgFamilies";
            this.dgFamilies.ReadOnly = true;
            this.dgFamilies.RowHeadersVisible = false;
            this.dgFamilies.RowHeadersWidth = 50;
            this.dgFamilies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFamilies.Size = new System.Drawing.Size(1903, 492);
            this.dgFamilies.TabIndex = 5;
            this.dgFamilies.VirtualMode = true;
            this.dgFamilies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgFamilies_CellDoubleClick);
            this.dgFamilies.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgFamilies_CellFormatting);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveColumnLayout,
            this.mnuResetColumns,
            this.tssSaveButtons,
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator,
            this.mnuExportToExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1903, 38);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveColumnLayout
            // 
            this.mnuSaveColumnLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSaveColumnLayout.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveColumnLayout.Image")));
            this.mnuSaveColumnLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSaveColumnLayout.Name = "mnuSaveColumnLayout";
            this.mnuSaveColumnLayout.Size = new System.Drawing.Size(40, 32);
            this.mnuSaveColumnLayout.Text = "Save Column Sort Order, layout, width etc";
            this.mnuSaveColumnLayout.Visible = false;
            this.mnuSaveColumnLayout.Click += new System.EventHandler(this.MnuSaveColumnLayout_Click);
            // 
            // mnuResetColumns
            // 
            this.mnuResetColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuResetColumns.Image = ((System.Drawing.Image)(resources.GetObject("mnuResetColumns.Image")));
            this.mnuResetColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuResetColumns.Name = "mnuResetColumns";
            this.mnuResetColumns.Size = new System.Drawing.Size(40, 32);
            this.mnuResetColumns.Text = "Reset Column Sort Order to Default";
            this.mnuResetColumns.Visible = false;
            this.mnuResetColumns.Click += new System.EventHandler(this.MnuResetColumns_Click);
            // 
            // tssSaveButtons
            // 
            this.tssSaveButtons.Name = "tssSaveButtons";
            this.tssSaveButtons.Size = new System.Drawing.Size(6, 38);
            this.tssSaveButtons.Visible = false;
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
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 38);
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
            // People
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1903, 1076);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "People";
            this.Text = "Individuals & Families";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.People_FormClosed);
            this.Load += new System.EventHandler(this.People_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            this.ctxViewNotes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgChildrenStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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