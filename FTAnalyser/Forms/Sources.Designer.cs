namespace FTAnalyzer.Forms
{
    partial class Sources
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
            reportFormHelper.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Sources));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuSaveColumnLayout = new System.Windows.Forms.ToolStripButton();
            this.mnuResetColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.dgSources = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.SourceID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceTitle = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceMedium = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Publication = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Author = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceText = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSources)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveColumnLayout,
            this.mnuResetColumns,
            this.toolStripSeparator2,
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator,
            this.mnuExportToExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(857, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveColumnLayout
            // 
            this.mnuSaveColumnLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSaveColumnLayout.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveColumnLayout.Image")));
            this.mnuSaveColumnLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSaveColumnLayout.Name = "mnuSaveColumnLayout";
            this.mnuSaveColumnLayout.Size = new System.Drawing.Size(23, 22);
            this.mnuSaveColumnLayout.Text = "Save Column Sort Order";
            this.mnuSaveColumnLayout.Click += new System.EventHandler(this.mnuSaveColumnLayout_Click);
            // 
            // mnuResetColumns
            // 
            this.mnuResetColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuResetColumns.Image = ((System.Drawing.Image)(resources.GetObject("mnuResetColumns.Image")));
            this.mnuResetColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuResetColumns.Name = "mnuResetColumns";
            this.mnuResetColumns.Size = new System.Drawing.Size(23, 22);
            this.mnuResetColumns.Text = "Reset Column Sort Order to Default";
            this.mnuResetColumns.Click += new System.EventHandler(this.mnuResetColumns_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
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
            // dgSources
            // 
            this.dgSources.AllowUserToAddRows = false;
            this.dgSources.AllowUserToDeleteRows = false;
            this.dgSources.AllowUserToOrderColumns = true;
            this.dgSources.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSources.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SourceID,
            this.SourceTitle,
            this.SourceMedium,
            this.Publication,
            this.Author,
            this.SourceText,
            this.FactCount});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgSources.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgSources.Location = new System.Drawing.Point(0, 25);
            this.dgSources.Name = "dgSources";
            this.dgSources.ReadOnly = true;
            this.dgSources.RowHeadersWidth = 20;
            this.dgSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSources.ShowEditingIcon = false;
            this.dgSources.Size = new System.Drawing.Size(857, 337);
            this.dgSources.TabIndex = 2;
            this.dgSources.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSources_CellDoubleClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecords});
            this.statusStrip.Location = new System.Drawing.Point(0, 365);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(857, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            this.tsRecords.Name = "tsRecords";
            this.tsRecords.Size = new System.Drawing.Size(118, 17);
            this.tsRecords.Text = "toolStripStatusLabel1";
            // 
            // SourceID
            // 
            this.SourceID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SourceID.DataPropertyName = "SourceID";
            this.SourceID.HeaderText = "ID";
            this.SourceID.MinimumWidth = 50;
            this.SourceID.Name = "SourceID";
            this.SourceID.ReadOnly = true;
            this.SourceID.Width = 50;
            // 
            // SourceTitle
            // 
            this.SourceTitle.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SourceTitle.DataPropertyName = "SourceTitle";
            this.SourceTitle.HeaderText = "Title";
            this.SourceTitle.MinimumWidth = 100;
            this.SourceTitle.Name = "SourceTitle";
            this.SourceTitle.ReadOnly = true;
            // 
            // SourceMedium
            // 
            this.SourceMedium.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SourceMedium.DataPropertyName = "SourceMedium";
            this.SourceMedium.HeaderText = "Medium";
            this.SourceMedium.MinimumWidth = 50;
            this.SourceMedium.Name = "SourceMedium";
            this.SourceMedium.ReadOnly = true;
            this.SourceMedium.Width = 69;
            // 
            // Publication
            // 
            this.Publication.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Publication.DataPropertyName = "Publication";
            this.Publication.HeaderText = "Publication";
            this.Publication.MinimumWidth = 50;
            this.Publication.Name = "Publication";
            this.Publication.ReadOnly = true;
            this.Publication.Width = 84;
            // 
            // Author
            // 
            this.Author.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.Author.DataPropertyName = "Author";
            this.Author.HeaderText = "Author";
            this.Author.MinimumWidth = 50;
            this.Author.Name = "Author";
            this.Author.ReadOnly = true;
            this.Author.Width = 63;
            // 
            // SourceText
            // 
            this.SourceText.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.SourceText.DataPropertyName = "SourceText";
            this.SourceText.HeaderText = "Text";
            this.SourceText.MinimumWidth = 200;
            this.SourceText.Name = "SourceText";
            this.SourceText.ReadOnly = true;
            this.SourceText.Width = 200;
            // 
            // FactCount
            // 
            this.FactCount.DataPropertyName = "FactCount";
            this.FactCount.HeaderText = "Facts";
            this.FactCount.Name = "FactCount";
            this.FactCount.ReadOnly = true;
            // 
            // Sources
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 387);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgSources);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Sources";
            this.Text = "Sources";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Sources_FormClosed);
            this.TextChanged += new System.EventHandler(this.Sources_TextChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSources)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgSources;
        private System.Windows.Forms.ToolStripButton mnuSaveColumnLayout;
        private System.Windows.Forms.ToolStripButton mnuResetColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceID;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceTitle;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceMedium;
        private System.Windows.Forms.DataGridViewTextBoxColumn Publication;
        private System.Windows.Forms.DataGridViewTextBoxColumn Author;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceText;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactCount;
    }
}