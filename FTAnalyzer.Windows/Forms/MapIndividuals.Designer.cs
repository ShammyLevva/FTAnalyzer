using System;

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
            try
            {
                if (disposing && (components is not null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
                italicFont.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapIndividuals));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            toolStrip1 = new ToolStrip();
            mnuSaveColumnLayout = new ToolStripButton();
            mnuResetColumns = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            printToolStripButton = new ToolStripButton();
            printPreviewToolStripButton = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            mnuExportToExcel = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuEditLocation = new ToolStripButton();
            dgIndividuals = new DataGridView();
            FactIcon = new DataGridViewImageColumn();
            FactLocation = new DataGridViewTextBoxColumn();
            IndividualID = new DataGridViewTextBoxColumn();
            FactName = new DataGridViewTextBoxColumn();
            Relaton = new DataGridViewTextBoxColumn();
            RelationToRoot = new DataGridViewTextBoxColumn();
            TypeOfFact = new DataGridViewTextBoxColumn();
            FactDate = new DataGridViewTextBoxColumn();
            FoundLocation = new DataGridViewTextBoxColumn();
            AgeAtFact = new DataGridViewTextBoxColumn();
            Ahnentafel = new DataGridViewTextBoxColumn();
            SortDistance = new DataGridViewTextBoxColumn();
            contextMenuStrip = new ContextMenuStrip(components);
            editLocationToolStripMenuItem = new ToolStripMenuItem();
            statusStrip = new StatusStrip();
            tsRecords = new ToolStripStatusLabel();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgIndividuals).BeginInit();
            contextMenuStrip.SuspendLayout();
            statusStrip.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { mnuSaveColumnLayout, mnuResetColumns, toolStripSeparator2, printToolStripButton, printPreviewToolStripButton, toolStripSeparator, mnuExportToExcel, toolStripSeparator1, mnuEditLocation });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(993, 25);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveColumnLayout
            // 
            mnuSaveColumnLayout.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuSaveColumnLayout.Image = (Image)resources.GetObject("mnuSaveColumnLayout.Image");
            mnuSaveColumnLayout.ImageTransparentColor = Color.Magenta;
            mnuSaveColumnLayout.Name = "mnuSaveColumnLayout";
            mnuSaveColumnLayout.Size = new Size(23, 22);
            mnuSaveColumnLayout.Text = "Save Column Sort Order, layout, width etc";
            mnuSaveColumnLayout.Click += MnuSaveColumnLayout_Click;
            // 
            // mnuResetColumns
            // 
            mnuResetColumns.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuResetColumns.Image = (Image)resources.GetObject("mnuResetColumns.Image");
            mnuResetColumns.ImageTransparentColor = Color.Magenta;
            mnuResetColumns.Name = "mnuResetColumns";
            mnuResetColumns.Size = new Size(23, 22);
            mnuResetColumns.Text = "Reset Column Sort Order to Default";
            mnuResetColumns.Click += MnuResetColumns_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 25);
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printToolStripButton.Image = (Image)resources.GetObject("printToolStripButton.Image");
            printToolStripButton.ImageTransparentColor = Color.Magenta;
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Size = new Size(23, 22);
            printToolStripButton.Text = "&Print";
            printToolStripButton.Click += PrintToolStripButton_Click;
            // 
            // printPreviewToolStripButton
            // 
            printPreviewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printPreviewToolStripButton.Image = (Image)resources.GetObject("printPreviewToolStripButton.Image");
            printPreviewToolStripButton.ImageTransparentColor = Color.Magenta;
            printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            printPreviewToolStripButton.Size = new Size(23, 22);
            printPreviewToolStripButton.Text = "Print Preview...";
            printPreviewToolStripButton.Click += PrintPreviewToolStripButton_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 25);
            // 
            // mnuExportToExcel
            // 
            mnuExportToExcel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuExportToExcel.Image = (Image)resources.GetObject("mnuExportToExcel.Image");
            mnuExportToExcel.ImageTransparentColor = Color.Magenta;
            mnuExportToExcel.Name = "mnuExportToExcel";
            mnuExportToExcel.Size = new Size(23, 22);
            mnuExportToExcel.Text = "Export to Excel";
            mnuExportToExcel.Click += MnuExportToExcel_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 25);
            // 
            // mnuEditLocation
            // 
            mnuEditLocation.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuEditLocation.Image = (Image)resources.GetObject("mnuEditLocation.Image");
            mnuEditLocation.ImageTransparentColor = Color.Magenta;
            mnuEditLocation.Name = "mnuEditLocation";
            mnuEditLocation.Size = new Size(23, 22);
            mnuEditLocation.Text = "Edit Location";
            mnuEditLocation.ToolTipText = "Edit Selected Location";
            mnuEditLocation.Click += MnuEditLocation_Click;
            // 
            // dgIndividuals
            // 
            dgIndividuals.AllowUserToAddRows = false;
            dgIndividuals.AllowUserToDeleteRows = false;
            dgIndividuals.AllowUserToOrderColumns = true;
            dgIndividuals.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgIndividuals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgIndividuals.Columns.AddRange(new DataGridViewColumn[] { FactIcon, FactLocation, IndividualID, FactName, Relaton, RelationToRoot, TypeOfFact, FactDate, FoundLocation, AgeAtFact, Ahnentafel, SortDistance });
            dgIndividuals.ContextMenuStrip = contextMenuStrip;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = SystemColors.Window;
            dataGridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dgIndividuals.DefaultCellStyle = dataGridViewCellStyle1;
            dgIndividuals.Location = new Point(0, 25);
            dgIndividuals.Name = "dgIndividuals";
            dgIndividuals.ReadOnly = true;
            dgIndividuals.RowHeadersVisible = false;
            dgIndividuals.RowHeadersWidth = 21;
            dgIndividuals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgIndividuals.ShowEditingIcon = false;
            dgIndividuals.Size = new Size(993, 337);
            dgIndividuals.TabIndex = 2;
            dgIndividuals.CellContextMenuStripNeeded += DgIndividuals_CellContextMenuStripNeeded;
            dgIndividuals.CellDoubleClick += DgIndividuals_CellDoubleClick;
            dgIndividuals.CellToolTipTextNeeded += DgIndividuals_CellToolTipTextNeeded;
            // 
            // FactIcon
            // 
            FactIcon.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FactIcon.DataPropertyName = "Icon";
            FactIcon.HeaderText = "";
            FactIcon.MinimumWidth = 20;
            FactIcon.Name = "FactIcon";
            FactIcon.ReadOnly = true;
            FactIcon.Resizable = DataGridViewTriState.False;
            FactIcon.Width = 20;
            // 
            // FactLocation
            // 
            FactLocation.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FactLocation.DataPropertyName = "Location";
            FactLocation.HeaderText = "Location";
            FactLocation.MinimumWidth = 300;
            FactLocation.Name = "FactLocation";
            FactLocation.ReadOnly = true;
            FactLocation.Width = 300;
            // 
            // IndividualID
            // 
            IndividualID.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            IndividualID.DataPropertyName = "IndividualID";
            IndividualID.HeaderText = "Ind. ID";
            IndividualID.MinimumWidth = 50;
            IndividualID.Name = "IndividualID";
            IndividualID.ReadOnly = true;
            IndividualID.Width = 50;
            // 
            // FactName
            // 
            FactName.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FactName.DataPropertyName = "Name";
            FactName.HeaderText = "Name";
            FactName.MinimumWidth = 150;
            FactName.Name = "FactName";
            FactName.ReadOnly = true;
            FactName.Width = 150;
            // 
            // Relaton
            // 
            Relaton.DataPropertyName = "Relation";
            Relaton.HeaderText = "Relaton";
            Relaton.MinimumWidth = 105;
            Relaton.Name = "Relaton";
            Relaton.ReadOnly = true;
            Relaton.Width = 105;
            // 
            // RelationToRoot
            // 
            RelationToRoot.DataPropertyName = "RelationToRoot";
            RelationToRoot.HeaderText = "Relation To Root";
            RelationToRoot.MinimumWidth = 100;
            RelationToRoot.Name = "RelationToRoot";
            RelationToRoot.ReadOnly = true;
            RelationToRoot.Width = 150;
            // 
            // TypeOfFact
            // 
            TypeOfFact.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            TypeOfFact.DataPropertyName = "TypeOfFact";
            TypeOfFact.HeaderText = "Fact Type";
            TypeOfFact.MinimumWidth = 80;
            TypeOfFact.Name = "TypeOfFact";
            TypeOfFact.ReadOnly = true;
            TypeOfFact.Width = 80;
            // 
            // FactDate
            // 
            FactDate.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            FactDate.DataPropertyName = "FactDate";
            FactDate.HeaderText = "Closest Fact Date";
            FactDate.MinimumWidth = 150;
            FactDate.Name = "FactDate";
            FactDate.ReadOnly = true;
            FactDate.Width = 150;
            // 
            // FoundLocation
            // 
            FoundLocation.DataPropertyName = "FoundLocation";
            FoundLocation.HeaderText = "Found Location";
            FoundLocation.MinimumWidth = 175;
            FoundLocation.Name = "FoundLocation";
            FoundLocation.ReadOnly = true;
            FoundLocation.Width = 175;
            // 
            // AgeAtFact
            // 
            AgeAtFact.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            AgeAtFact.DataPropertyName = "AgeAtFact";
            AgeAtFact.HeaderText = "Age";
            AgeAtFact.MinimumWidth = 60;
            AgeAtFact.Name = "AgeAtFact";
            AgeAtFact.ReadOnly = true;
            AgeAtFact.Width = 60;
            // 
            // Ahnentafel
            // 
            Ahnentafel.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            Ahnentafel.HeaderText = "Ahnentafel";
            Ahnentafel.MinimumWidth = 60;
            Ahnentafel.Name = "Ahnentafel";
            Ahnentafel.ReadOnly = true;
            Ahnentafel.Width = 60;
            // 
            // SortDistance
            // 
            SortDistance.DataPropertyName = "SortDistance";
            SortDistance.HeaderText = "Geometry";
            SortDistance.Name = "SortDistance";
            SortDistance.ReadOnly = true;
            SortDistance.Visible = false;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { editLocationToolStripMenuItem });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(144, 26);
            // 
            // editLocationToolStripMenuItem
            // 
            editLocationToolStripMenuItem.Name = "editLocationToolStripMenuItem";
            editLocationToolStripMenuItem.Size = new Size(143, 22);
            editLocationToolStripMenuItem.Text = "Edit Location";
            editLocationToolStripMenuItem.Click += EditLocationToolStripMenuItem_Click;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new ToolStripItem[] { tsRecords });
            statusStrip.Location = new Point(0, 365);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new Size(993, 22);
            statusStrip.TabIndex = 3;
            statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            tsRecords.Name = "tsRecords";
            tsRecords.Size = new Size(0, 17);
            // 
            // MapIndividuals
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(993, 387);
            Controls.Add(statusStrip);
            Controls.Add(dgIndividuals);
            Controls.Add(toolStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "MapIndividuals";
            Text = "Individuals";
            FormClosed += MapIndividuals_FormClosed;
            Load += MapIndividuals_Load;
            TextChanged += Facts_TextChanged;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgIndividuals).EndInit();
            contextMenuStrip.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private DataGridView dgIndividuals;
        private ToolStripButton mnuSaveColumnLayout;
        private ToolStripButton mnuResetColumns;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripButton printToolStripButton;
        private ToolStripButton printPreviewToolStripButton;
        private ToolStripSeparator toolStripSeparator;
        private ToolStripButton mnuExportToExcel;
        private StatusStrip statusStrip;
        private ToolStripStatusLabel tsRecords;
        private ToolStripButton mnuEditLocation;
        private ToolStripSeparator toolStripSeparator1;
        private ContextMenuStrip contextMenuStrip;
        private ToolStripMenuItem editLocationToolStripMenuItem;
        private DataGridViewImageColumn FactIcon;
        private DataGridViewTextBoxColumn FactLocation;
        private DataGridViewTextBoxColumn IndividualID;
        private DataGridViewTextBoxColumn FactName;
        private DataGridViewTextBoxColumn Relaton;
        private DataGridViewTextBoxColumn RelationToRoot;
        private DataGridViewTextBoxColumn TypeOfFact;
        private DataGridViewTextBoxColumn FactDate;
        private DataGridViewTextBoxColumn FoundLocation;
        private DataGridViewTextBoxColumn AgeAtFact;
        private DataGridViewTextBoxColumn Ahnentafel;
        private DataGridViewTextBoxColumn SortDistance;
    }
}