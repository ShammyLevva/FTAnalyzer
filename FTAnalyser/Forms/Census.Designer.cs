namespace FTAnalyzer.Forms
{
    partial class Census
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
            this.dgCensus = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsRecords = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dgCensus)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgCensus
            // 
            this.dgCensus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCensus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCensus.Location = new System.Drawing.Point(0, 0);
            this.dgCensus.Name = "dgCensus";
            this.dgCensus.Size = new System.Drawing.Size(1038, 558);
            this.dgCensus.TabIndex = 1;
            this.dgCensus.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgCensus_CellFormatting);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecords});
            this.statusStrip.Location = new System.Drawing.Point(0, 561);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1038, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            this.tsRecords.Name = "tsRecords";
            this.tsRecords.Size = new System.Drawing.Size(118, 17);
            this.tsRecords.Text = "toolStripStatusLabel1";
            // 
            // Census
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 583);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgCensus);
            this.Name = "Census";
            this.Text = "Census Records to search for";
            ((System.ComponentModel.ISupportInitialize)(this.dgCensus)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCensus;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
    }
}