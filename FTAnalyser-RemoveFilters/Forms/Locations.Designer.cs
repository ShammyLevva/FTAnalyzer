namespace FTAnalyzer.Forms
{
    partial class Locations
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
            this.tvLocations = new System.Windows.Forms.TreeView();
            this.SuspendLayout();
            // 
            // tvLocations
            // 
            this.tvLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tvLocations.Location = new System.Drawing.Point(-1, -1);
            this.tvLocations.Name = "tvLocations";
            this.tvLocations.Size = new System.Drawing.Size(532, 404);
            this.tvLocations.TabIndex = 0;
            // 
            // Locations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(528, 400);
            this.Controls.Add(this.tvLocations);
            this.Name = "Locations";
            this.Text = "Locations";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView tvLocations;
    }
}