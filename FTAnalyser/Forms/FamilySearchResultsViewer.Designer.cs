namespace FTAnalyzer.Forms
{
    partial class FamilySearchResultsViewer
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
            this.components = new System.ComponentModel.Container();
            this.lbResults = new System.Windows.Forms.ListBox();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.tooltips = new System.Windows.Forms.ToolTip(this.components);
            this.upFamilySearchResultsFDayilter = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labDays = new System.Windows.Forms.Label();
            this.labFileCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.upFamilySearchResultsFDayilter)).BeginInit();
            this.SuspendLayout();
            // 
            // lbResults
            // 
            this.lbResults.FormattingEnabled = true;
            this.lbResults.Location = new System.Drawing.Point(12, 477);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(770, 95);
            this.lbResults.TabIndex = 0;
            this.lbResults.SelectedIndexChanged += new System.EventHandler(this.lbResults_SelectedIndexChanged);
            this.lbResults.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbResults_MouseMove);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(12, 12);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(1003, 459);
            this.webBrowser.TabIndex = 1;
            // 
            // upFamilySearchResultsFDayilter
            // 
            this.upFamilySearchResultsFDayilter.Location = new System.Drawing.Point(940, 477);
            this.upFamilySearchResultsFDayilter.Maximum = new decimal(new int[] {
            365,
            0,
            0,
            0});
            this.upFamilySearchResultsFDayilter.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.upFamilySearchResultsFDayilter.Name = "upFamilySearchResultsFDayilter";
            this.upFamilySearchResultsFDayilter.Size = new System.Drawing.Size(40, 20);
            this.upFamilySearchResultsFDayilter.TabIndex = 2;
            this.upFamilySearchResultsFDayilter.Value = new decimal(new int[] {
            7,
            0,
            0,
            0});
            this.upFamilySearchResultsFDayilter.ValueChanged += new System.EventHandler(this.upFamilySearchResultsFDayilter_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(788, 479);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(146, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Only show files created in last";
            // 
            // labDays
            // 
            this.labDays.AutoSize = true;
            this.labDays.Location = new System.Drawing.Point(986, 479);
            this.labDays.Name = "labDays";
            this.labDays.Size = new System.Drawing.Size(29, 13);
            this.labDays.TabIndex = 4;
            this.labDays.Text = "days";
            // 
            // labFileCount
            // 
            this.labFileCount.AutoSize = true;
            this.labFileCount.Location = new System.Drawing.Point(788, 501);
            this.labFileCount.Name = "labFileCount";
            this.labFileCount.Size = new System.Drawing.Size(35, 13);
            this.labFileCount.TabIndex = 5;
            this.labFileCount.Text = "label2";
            // 
            // FamilySearchResultsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 580);
            this.Controls.Add(this.labFileCount);
            this.Controls.Add(this.labDays);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.upFamilySearchResultsFDayilter);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.lbResults);
            this.Name = "FamilySearchResultsViewer";
            this.Text = "FamilySearchResultsViewer";
            ((System.ComponentModel.ISupportInitialize)(this.upFamilySearchResultsFDayilter)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lbResults;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolTip tooltips;
        private System.Windows.Forms.NumericUpDown upFamilySearchResultsFDayilter;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labDays;
        private System.Windows.Forms.Label labFileCount;
    }
}