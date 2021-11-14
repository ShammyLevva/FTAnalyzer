using System;

namespace FTAnalyzer.Forms
{
    partial class Facts
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
 
                italicFont.Dispose();
                linkFont.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Facts));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.mnuSaveColumnLayout = new System.Windows.Forms.ToolStripButton();
            this.mnuResetColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowHideFacts = new System.Windows.Forms.ToolStripButton();
            this.dgFacts = new System.Windows.Forms.DataGridView();
            this.FactIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.IgnoreFact = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.IndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Forenames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TypeOfFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SurnameAtDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Relation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RelationToRoot = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DateofBirth = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FactLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Comment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CensusRefYear = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CensusReference = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AgeAtFact = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Preferred = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.SourcesCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SourceList = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFacts)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveColumnLayout,
            this.mnuResetColumns,
            this.toolStripSeparator2,
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator,
            this.mnuExportToExcel,
            this.sep1,
            this.btnShowHideFacts});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.toolStrip1.Size = new System.Drawing.Size(1738, 38);
            this.toolStrip1.TabIndex = 0;
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
            this.mnuResetColumns.Click += new System.EventHandler(this.MnuResetColumns_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 38);
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
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(6, 38);
            // 
            // btnShowHideFacts
            // 
            this.btnShowHideFacts.Checked = true;
            this.btnShowHideFacts.CheckOnClick = true;
            this.btnShowHideFacts.CheckState = System.Windows.Forms.CheckState.Checked;
            this.btnShowHideFacts.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnShowHideFacts.Image = ((System.Drawing.Image)(resources.GetObject("btnShowHideFacts.Image")));
            this.btnShowHideFacts.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowHideFacts.Name = "btnShowHideFacts";
            this.btnShowHideFacts.Size = new System.Drawing.Size(40, 32);
            this.btnShowHideFacts.Text = "Show Hide";
            this.btnShowHideFacts.ToolTipText = "Show/Hide Ignored Facts";
            this.btnShowHideFacts.Click += new System.EventHandler(this.BtnShowHideFacts_Click);
            // 
            // dgFacts
            // 
            this.dgFacts.AllowUserToAddRows = false;
            this.dgFacts.AllowUserToDeleteRows = false;
            this.dgFacts.AllowUserToOrderColumns = true;
            this.dgFacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgFacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFacts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.FactIcon,
            this.IgnoreFact,
            this.IndividualID,
            this.Forenames,
            this.Surname,
            this.TypeOfFact,
            this.FactDate,
            this.SurnameAtDate,
            this.Relation,
            this.RelationToRoot,
            this.DateofBirth,
            this.FactLocation,
            this.Comment,
            this.CensusRefYear,
            this.CensusReference,
            this.AgeAtFact,
            this.Preferred,
            this.SourcesCount,
            this.SourceList});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgFacts.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgFacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFacts.Location = new System.Drawing.Point(0, 38);
            this.dgFacts.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.dgFacts.Name = "dgFacts";
            this.dgFacts.ReadOnly = true;
            this.dgFacts.RowHeadersWidth = 20;
            this.dgFacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFacts.ShowEditingIcon = false;
            this.dgFacts.Size = new System.Drawing.Size(1738, 637);
            this.dgFacts.TabIndex = 2;
            this.dgFacts.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgFacts_CellContentClick);
            this.dgFacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgFacts_CellDoubleClick);
            this.dgFacts.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgFacts_CellFormatting);
            this.dgFacts.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.DgFacts_CellToolTipTextNeeded);
            // 
            // FactIcon
            // 
            this.FactIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells;
            this.FactIcon.DataPropertyName = "Icon";
            this.FactIcon.HeaderText = "";
            this.FactIcon.MinimumWidth = 30;
            this.FactIcon.Name = "FactIcon";
            this.FactIcon.ReadOnly = true;
            this.FactIcon.Width = 30;
            // 
            // IgnoreFact
            // 
            this.IgnoreFact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IgnoreFact.DataPropertyName = "IgnoreFact";
            this.IgnoreFact.FalseValue = "False";
            this.IgnoreFact.HeaderText = "Ignore";
            this.IgnoreFact.MinimumWidth = 9;
            this.IgnoreFact.Name = "IgnoreFact";
            this.IgnoreFact.ReadOnly = true;
            this.IgnoreFact.TrueValue = "True";
            this.IgnoreFact.Width = 40;
            // 
            // IndividualID
            // 
            this.IndividualID.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.IndividualID.DataPropertyName = "IndividualID";
            this.IndividualID.HeaderText = "Ind ID";
            this.IndividualID.MinimumWidth = 9;
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
            this.Forenames.Width = 175;
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
            // TypeOfFact
            // 
            this.TypeOfFact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.TypeOfFact.DataPropertyName = "TypeOfFact";
            this.TypeOfFact.HeaderText = "Fact Type";
            this.TypeOfFact.MinimumWidth = 85;
            this.TypeOfFact.Name = "TypeOfFact";
            this.TypeOfFact.ReadOnly = true;
            this.TypeOfFact.Width = 85;
            // 
            // FactDate
            // 
            this.FactDate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactDate.DataPropertyName = "FactDate";
            this.FactDate.HeaderText = "Fact Date";
            this.FactDate.MinimumWidth = 50;
            this.FactDate.Name = "FactDate";
            this.FactDate.ReadOnly = true;
            this.FactDate.Width = 150;
            // 
            // SurnameAtDate
            // 
            this.SurnameAtDate.DataPropertyName = "SurnameAtDate";
            this.SurnameAtDate.HeaderText = "Surname at Date";
            this.SurnameAtDate.MinimumWidth = 75;
            this.SurnameAtDate.Name = "SurnameAtDate";
            this.SurnameAtDate.ReadOnly = true;
            this.SurnameAtDate.Width = 147;
            // 
            // Relation
            // 
            this.Relation.DataPropertyName = "Relation";
            this.Relation.HeaderText = "Relation";
            this.Relation.MinimumWidth = 105;
            this.Relation.Name = "Relation";
            this.Relation.ReadOnly = true;
            this.Relation.Width = 123;
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
            // DateofBirth
            // 
            this.DateofBirth.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.DateofBirth.DataPropertyName = "DateofBirth";
            this.DateofBirth.HeaderText = "Date of Birth";
            this.DateofBirth.MinimumWidth = 50;
            this.DateofBirth.Name = "DateofBirth";
            this.DateofBirth.ReadOnly = true;
            this.DateofBirth.Width = 150;
            // 
            // FactLocation
            // 
            this.FactLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FactLocation.DataPropertyName = "Location";
            this.FactLocation.HeaderText = "Location";
            this.FactLocation.MinimumWidth = 120;
            this.FactLocation.Name = "FactLocation";
            this.FactLocation.ReadOnly = true;
            this.FactLocation.Width = 250;
            // 
            // Comment
            // 
            this.Comment.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Comment.DataPropertyName = "Comment";
            this.Comment.HeaderText = "Comment";
            this.Comment.MinimumWidth = 120;
            this.Comment.Name = "Comment";
            this.Comment.ReadOnly = true;
            this.Comment.Width = 250;
            // 
            // CensusRefYear
            // 
            this.CensusRefYear.DataPropertyName = "CensusRefYear";
            this.CensusRefYear.HeaderText = "Census Ref Year";
            this.CensusRefYear.MinimumWidth = 9;
            this.CensusRefYear.Name = "CensusRefYear";
            this.CensusRefYear.ReadOnly = true;
            this.CensusRefYear.Width = 148;
            // 
            // CensusReference
            // 
            this.CensusReference.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.CensusReference.DataPropertyName = "CensusReference";
            this.CensusReference.HeaderText = "Census Reference";
            this.CensusReference.MinimumWidth = 25;
            this.CensusReference.Name = "CensusReference";
            this.CensusReference.ReadOnly = true;
            this.CensusReference.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.CensusReference.Width = 250;
            // 
            // AgeAtFact
            // 
            this.AgeAtFact.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.AgeAtFact.DataPropertyName = "AgeAtFact";
            this.AgeAtFact.HeaderText = "Age";
            this.AgeAtFact.MinimumWidth = 55;
            this.AgeAtFact.Name = "AgeAtFact";
            this.AgeAtFact.ReadOnly = true;
            this.AgeAtFact.Width = 55;
            // 
            // Preferred
            // 
            this.Preferred.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Preferred.DataPropertyName = "Preferred";
            this.Preferred.HeaderText = "Preferred";
            this.Preferred.MinimumWidth = 9;
            this.Preferred.Name = "Preferred";
            this.Preferred.ReadOnly = true;
            this.Preferred.Width = 55;
            // 
            // SourcesCount
            // 
            this.SourcesCount.DataPropertyName = "SourcesCount";
            this.SourcesCount.HeaderText = "Num Sources";
            this.SourcesCount.MinimumWidth = 9;
            this.SourcesCount.Name = "SourcesCount";
            this.SourcesCount.ReadOnly = true;
            this.SourcesCount.Width = 158;
            // 
            // SourceList
            // 
            this.SourceList.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.SourceList.DataPropertyName = "SourceList";
            this.SourceList.HeaderText = "Sources";
            this.SourceList.MinimumWidth = 50;
            this.SourceList.Name = "SourceList";
            this.SourceList.ReadOnly = true;
            this.SourceList.Width = 300;
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecords});
            this.statusStrip.Location = new System.Drawing.Point(0, 675);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(2, 0, 26, 0);
            this.statusStrip.Size = new System.Drawing.Size(1738, 39);
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            this.tsRecords.Name = "tsRecords";
            this.tsRecords.Size = new System.Drawing.Size(206, 30);
            this.tsRecords.Text = "toolStripStatusLabel1";
            // 
            // Facts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1738, 714);
            this.Controls.Add(this.dgFacts);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "Facts";
            this.Text = "Facts";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Facts_FormClosed);
            this.Load += new System.EventHandler(this.Facts_Load);
            this.TextChanged += new System.EventHandler(this.Facts_TextChanged);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFacts)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.DataGridView dgFacts;
        private System.Windows.Forms.ToolStripButton mnuSaveColumnLayout;
        private System.Windows.Forms.ToolStripButton mnuResetColumns;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripButton btnShowHideFacts;
        private System.Windows.Forms.DataGridViewImageColumn FactIcon;
        private System.Windows.Forms.DataGridViewCheckBoxColumn IgnoreFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn IndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Forenames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewTextBoxColumn TypeOfFact;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn SurnameAtDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Relation;
        private System.Windows.Forms.DataGridViewTextBoxColumn RelationToRoot;
        private System.Windows.Forms.DataGridViewTextBoxColumn DateofBirth;
        private System.Windows.Forms.DataGridViewTextBoxColumn FactLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Comment;
        private System.Windows.Forms.DataGridViewTextBoxColumn CensusRefYear;
        private System.Windows.Forms.DataGridViewTextBoxColumn CensusReference;
        private System.Windows.Forms.DataGridViewTextBoxColumn AgeAtFact;
        private System.Windows.Forms.DataGridViewCheckBoxColumn Preferred;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourcesCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn SourceList;
    }
}