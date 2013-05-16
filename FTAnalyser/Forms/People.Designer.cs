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
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.dgFamilies = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).BeginInit();
            this.SuspendLayout();
            // 
            // dgIndividuals
            // 
            this.dgIndividuals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.Location = new System.Drawing.Point(0, 0);
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.Size = new System.Drawing.Size(1038, 283);
            this.dgIndividuals.TabIndex = 1;
            // 
            // dgFamilies
            // 
            this.dgFamilies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFamilies.Location = new System.Drawing.Point(0, 300);
            this.dgFamilies.Name = "dgFamilies";
            this.dgFamilies.Size = new System.Drawing.Size(1038, 283);
            this.dgFamilies.TabIndex = 2;
            // 
            // People
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 583);
            this.Controls.Add(this.dgFamilies);
            this.Controls.Add(this.dgIndividuals);
            this.Name = "People";
            this.Text = "Individuals & Families";
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgIndividuals;
        private System.Windows.Forms.DataGridView dgFamilies;
    }
}