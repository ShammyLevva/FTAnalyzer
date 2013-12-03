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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(People));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.txtCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.dgFamilies = new System.Windows.Forms.DataGridView();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).BeginInit();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtCount});
            this.statusStrip1.Location = new System.Drawing.Point(0, 561);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1038, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // txtCount
            // 
            this.txtCount.Name = "txtCount";
            this.txtCount.Size = new System.Drawing.Size(118, 17);
            this.txtCount.Text = "toolStripStatusLabel1";
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(0, 0);
            this.splitContainer.Name = "splitContainer";
            this.splitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgIndividuals);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgFamilies);
            this.splitContainer.Size = new System.Drawing.Size(1038, 561);
            this.splitContainer.SplitterDistance = 286;
            this.splitContainer.TabIndex = 5;
            // 
            // dgIndividuals
            // 
            this.dgIndividuals.AllowUserToAddRows = false;
            this.dgIndividuals.AllowUserToDeleteRows = false;
            this.dgIndividuals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgIndividuals.Location = new System.Drawing.Point(0, 0);
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.Size = new System.Drawing.Size(1038, 286);
            this.dgIndividuals.TabIndex = 2;
            this.dgIndividuals.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgIndividuals_CellDoubleClick_1);
            // 
            // dgFamilies
            // 
            this.dgFamilies.AllowUserToAddRows = false;
            this.dgFamilies.AllowUserToDeleteRows = false;
            this.dgFamilies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFamilies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFamilies.Location = new System.Drawing.Point(0, 0);
            this.dgFamilies.Name = "dgFamilies";
            this.dgFamilies.Size = new System.Drawing.Size(1038, 271);
            this.dgFamilies.TabIndex = 5;
            this.dgFamilies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFamilies_CellDoubleClick_1);
            // 
            // People
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 583);
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "People";
            this.Text = "Individuals & Families";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.People_FormClosed);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel txtCount;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgIndividuals;
        private System.Windows.Forms.DataGridView dgFamilies;
    }
}