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
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.dgFamilies = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).BeginInit();
            this.SuspendLayout();
            // 
            // dgIndividuals
            // 
            this.dgIndividuals.AllowUserToAddRows = false;
            this.dgIndividuals.AllowUserToDeleteRows = false;
            this.dgIndividuals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgIndividuals.Location = new System.Drawing.Point(0, 0);
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.Size = new System.Drawing.Size(1038, 283);
            this.dgIndividuals.TabIndex = 1;
            this.dgIndividuals.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgIndividuals_CellDoubleClick);
            this.dgIndividuals.SelectionChanged += new System.EventHandler(this.dgIndividuals_SelectionChanged);
            // 
            // dgFamilies
            // 
            this.dgFamilies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFamilies.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgFamilies.Location = new System.Drawing.Point(0, 300);
            this.dgFamilies.Name = "dgFamilies";
            this.dgFamilies.Size = new System.Drawing.Size(1038, 283);
            this.dgFamilies.TabIndex = 2;
            this.dgFamilies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFamilies_CellDoubleClick);
            // 
            // People
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 583);
            this.Controls.Add(this.dgFamilies);
            this.Controls.Add(this.dgIndividuals);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "People";
            this.Text = "Individuals & Families";
            this.Resize += new System.EventHandler(this.People_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgIndividuals;
        private System.Windows.Forms.DataGridView dgFamilies;
    }
}