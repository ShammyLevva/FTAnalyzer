namespace FTAnalyzer
{
    partial class MapIndividuals
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
            italicFont.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapIndividuals));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuSaveColumnLayout = new System.Windows.Forms.ToolStripButton();
            this.mnuResetColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditLocation = new System.Windows.Forms.ToolStripButton();
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.FactIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.FactLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relaton = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelationToRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeOfFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgeAtFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ahnentafel = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SortDistance = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
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
            this.mnuExportToExcel,
            this.toolStripSeparator1,
            this.mnuEditLocation});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(993, 25);
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // mnuEditLocation
            // 
            this.mnuEditLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuEditLocation.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditLocation.Image")));
            this.mnuEditLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditLocation.Name = "mnuEditLocation";
            this.mnuEditLocation.Size = new System.Drawing.Size(23, 22);
            this.mnuEditLocation.Text = "Edit Location";
            this.mnuEditLocation.ToolTipText = "Edit Selected Location";
            this.mnuEditLocation.Click += new System.EventHandler(this.mnuEditLocation_Click);
            // 
            // dgIndividuals
            // 
            this.dgIndividuals.AllowUserToAddRows = false;
            this.dgIndividuals.AllowUserToDeleteRows = false;
            this.dgIndividuals.AllowUserToOrderColumns = true;
            this.dgIndividuals.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FactIcon,
            this.FactLocation,
            this.IndividualID,
            this.FactName,
            this.Relaton,
            this.RelationToRoot,
            this.TypeOfFact,
            this.FactDate,
            this.GoogleLocation,
            this.AgeAtFact,
            this.Ahnentafel,
            this.SortDistance});
            this.dgIndividuals.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgIndividuals.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgIndividuals.Location = new System.Drawing.Point(0, 25);
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.ReadOnly = true;
            this.dgIndividuals.RowHeadersVisible = false;
            this.dgIndividuals.RowHeadersWidth = 21;
            this.dgIndividuals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgIndividuals.ShowEditingIcon = false;
            this.dgIndividuals.Size = new System.Drawing.Size(993, 337);
            this.dgIndividuals.TabIndex = 2;
            this.dgIndividuals.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgIndividuals_CellContextMenuStripNeeded);
            this.dgIndividuals.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgIndividuals_CellDoubleClick);
            this.dgIndividuals.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dgIndividuals_CellToolTipTextNeeded);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editLocationToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(144, 26);
            // 
            // editLocationToolStripMenuItem
            // 
            this.editLocationToolStripMenuItem.Name = "editLocationToolStripMenuItem";
            this.editLocationToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.editLocationToolStripMenuItem.Text = "Edit Location";
            this.editLocationToolStripMenuItem.Click += new System.EventHandler(this.editLocationToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecords});
            this.statusStrip.Location = new System.Drawing.Point(0, 365);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(993, 22);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            this.tsRecords.Name = "tsRecords";
            this.tsRecords.Size = new System.Drawing.Size(0, 17);
            // 
            // FactIcon
            // 
            this.FactIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactIcon.DataPropertyName = "Icon";
            this.FactIcon.HeaderText = "";
            this.FactIcon.MinimumWidth = 20;
            this.FactIcon.Name = "FactIcon";
            this.FactIcon.ReadOnly = true;
            this.FactIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.FactIcon.Width = 20;
            // 
            // FactLocation
            // 
            this.FactLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactLocation.DataPropertyName = "Location";
            this.FactLocation.HeaderText = "Location";
            this.FactLocation.MinimumWidth = 300;
            this.FactLocation.Name = "FactLocation";
            this.FactLocation.ReadOnly = true;
            this.FactLocation.Width = 300;
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
            // FactName
            // 
            this.FactName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactName.DataPropertyName = "Name";
            this.FactName.HeaderText = "Name";
            this.FactName.MinimumWidth = 150;
            this.FactName.Name = "FactName";
            this.FactName.ReadOnly = true;
            this.FactName.Width = 150;
            // 
            // Relaton
            // 
            this.Relaton.DataPropertyName = "Relation";
            this.Relaton.HeaderText = "Relaton";
            this.Relaton.MinimumWidth = 105;
            this.Relaton.Name = "Relaton";
            this.Relaton.ReadOnly = true;
            this.Relaton.Width = 105;
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
            // TypeOfFact
            // 
            this.TypeOfFact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TypeOfFact.DataPropertyName = "TypeOfFact";
            this.TypeOfFact.HeaderText = "Fact Type";
            this.TypeOfFact.MinimumWidth = 80;
            this.TypeOfFact.Name = "TypeOfFact";
            this.TypeOfFact.ReadOnly = true;
            this.TypeOfFact.Width = 80;
            // 
            // FactDate
            // 
            this.FactDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactDate.DataPropertyName = "FactDate";
            this.FactDate.HeaderText = "Closest Fact Date";
            this.FactDate.MinimumWidth = 150;
            this.FactDate.Name = "FactDate";
            this.FactDate.ReadOnly = true;
            this.FactDate.Width = 150;
            // 
            // GoogleLocation
            // 
            this.GoogleLocation.DataPropertyName = "GoogleLocation";
            this.GoogleLocation.HeaderText = "Google Location";
            this.GoogleLocation.MinimumWidth = 175;
            this.GoogleLocation.Name = "GoogleLocation";
            this.GoogleLocation.ReadOnly = true;
            this.GoogleLocation.Width = 175;
            // 
            // AgeAtFact
            // 
            this.AgeAtFact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AgeAtFact.DataPropertyName = "AgeAtFact";
            this.AgeAtFact.HeaderText = "Age";
            this.AgeAtFact.MinimumWidth = 60;
            this.AgeAtFact.Name = "AgeAtFact";
            this.AgeAtFact.ReadOnly = true;
            this.AgeAtFact.Width = 60;
            // 
            // Ahnentafel
            // 
            this.Ahnentafel.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Ahnentafel.HeaderText = "Ahnentafel";
            this.Ahnentafel.MinimumWidth = 60;
            this.Ahnentafel.Name = "Ahnentafel";
            this.Ahnentafel.ReadOnly = true;
            this.Ahnentafel.Width = 60;
            // 
            // SortDistance
            // 
            this.SortDistance.DataPropertyName = "SortDistance";
            this.SortDistance.HeaderText = "Geometry";
            this.SortDistance.Name = "SortDistance";
            this.SortDistance.ReadOnly = true;
            this.SortDistance.Visible = false;
            // 
            // MapIndividuals
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 387);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgIndividuals);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MapIndividuals";
            this.Text = "Individuals";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MapIndividuals_FormClosed);
            this.TextChanged += new System.EventHandler(this.Facts_TextChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgIndividuals;
        private System.Windows.Forms.ToolStripButton mnuSaveColumnLayout;
        private System.Windows.Forms.ToolStripButton mnuResetColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
        private System.Windows.Forms.ToolStripButton mnuEditLocation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem editLocationToolStripMenuItem;
        private System.Windows.Forms.DataGridViewImageColumn FactIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relaton;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelationToRoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeOfFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeAtFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ahnentafel;
        private System.Windows.Forms.DataGridViewTextBoxColumn SortDistance;
    }
}