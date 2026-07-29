using System;

namespace FTAnalyzer.Forms
{
    partial class Chart
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
            this.chartDisplay = new ScottPlot.WinForms.FormsPlot();
            this.pnlStats = new System.Windows.Forms.Panel();
            this.dgParentAgeStats = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgParentAgeStats)).BeginInit();
            this.pnlStats.SuspendLayout();
            this.SuspendLayout();
            //
            // chartDisplay
            //
            this.chartDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartDisplay.Location = new System.Drawing.Point(0, 0);
            this.chartDisplay.Name = "chartDisplay";
            this.chartDisplay.Size = new System.Drawing.Size(895, 421);
            this.chartDisplay.TabIndex = 0;
            //
            // pnlStats
            //
            this.pnlStats.Controls.Add(this.dgParentAgeStats);
            this.pnlStats.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStats.Location = new System.Drawing.Point(0, 421);
            this.pnlStats.Name = "pnlStats";
            this.pnlStats.Padding = new System.Windows.Forms.Padding(6, 0, 6, 6);
            this.pnlStats.Size = new System.Drawing.Size(895, 200);
            this.pnlStats.TabIndex = 1;
            //
            // dgParentAgeStats
            //
            this.dgParentAgeStats.AllowUserToAddRows = false;
            this.dgParentAgeStats.AllowUserToDeleteRows = false;
            this.dgParentAgeStats.AllowUserToResizeRows = false;
            this.dgParentAgeStats.AutoGenerateColumns = false;
            this.dgParentAgeStats.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgParentAgeStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgParentAgeStats.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgParentAgeStats.Location = new System.Drawing.Point(6, 0);
            this.dgParentAgeStats.Name = "dgParentAgeStats";
            this.dgParentAgeStats.ReadOnly = true;
            this.dgParentAgeStats.RowHeadersVisible = false;
            this.dgParentAgeStats.Size = new System.Drawing.Size(883, 194);
            this.dgParentAgeStats.TabIndex = 0;
            //
            // Chart
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 621);
            this.Controls.Add(this.chartDisplay);
            this.Controls.Add(this.pnlStats);
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "Chart";
            this.Text = "Chart";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Chart_FormClosed);
            this.Load += new System.EventHandler(this.Chart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgParentAgeStats)).EndInit();
            this.pnlStats.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ScottPlot.WinForms.FormsPlot chartDisplay;
        private System.Windows.Forms.Panel pnlStats;
        private System.Windows.Forms.DataGridView dgParentAgeStats;
    }
}
