using System;

namespace FTAnalyzer.Forms
{
    partial class MissingData
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MissingData));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.dsBirth = new FTAnalyzer.Forms.Controls.DateSliders();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 435);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(688, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // dsBirth
            // 
            this.dsBirth.GroupBoxText = "Birth Date";
            this.dsBirth.Location = new System.Drawing.Point(12, 12);
            this.dsBirth.Name = "dsBirth";
            this.dsBirth.Size = new System.Drawing.Size(334, 163);
            this.dsBirth.TabIndex = 2;
            // 
            // MissingData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(688, 457);
            this.Controls.Add(this.dsBirth);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MissingData";
            this.Text = "Missing Data Configuration";
            this.Load += new System.EventHandler(this.MissingData_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip1;
        private FTAnalyzer.Forms.Controls.DateSliders dsBirth;
    }
}