namespace FTAnalyzer.Forms
{
    partial class IGISearchResultsViewer
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
            this.SuspendLayout();
            // 
            // lbResults
            // 
            this.lbResults.FormattingEnabled = true;
            this.lbResults.Location = new System.Drawing.Point(12, 5);
            this.lbResults.Name = "lbResults";
            this.lbResults.Size = new System.Drawing.Size(256, 563);
            this.lbResults.TabIndex = 0;
            this.lbResults.SelectedIndexChanged += new System.EventHandler(this.lbResults_SelectedIndexChanged);
            this.lbResults.MouseMove += new System.Windows.Forms.MouseEventHandler(this.lbResults_MouseMove);
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(274, 5);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(741, 560);
            this.webBrowser.TabIndex = 1;
            // 
            // IGISearchResultsViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1027, 580);
            this.Controls.Add(this.webBrowser);
            this.Controls.Add(this.lbResults);
            this.Name = "IGISearchResultsViewer";
            this.Text = "IGISearchResultsViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbResults;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolTip tooltips;
    }
}