using System;

namespace FTAnalyzer.Forms.Controls
{
    partial class CensusDateSelector
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.cbCensusDate = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Census Date";
            // 
            // cbCensusDate
            // 
            this.cbCensusDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCensusDate.FormattingEnabled = true;
            this.cbCensusDate.Items.AddRange(new object[] {
            "1841",
            "1851",
            "1861",
            "1871",
            "1881",
            "1891",
            "1901",
            "1911"});
            this.cbCensusDate.Location = new System.Drawing.Point(77, 3);
            this.cbCensusDate.Name = "cbCensusDate";
            this.cbCensusDate.Size = new System.Drawing.Size(49, 21);
            this.cbCensusDate.TabIndex = 3;
            this.cbCensusDate.SelectedIndexChanged += new System.EventHandler(this.CbCensusDate_SelectedIndexChanged);
            // 
            // CensusDateSelector
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbCensusDate);
            this.Name = "CensusDateSelector";
            this.Size = new System.Drawing.Size(129, 27);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCensusDate;
    }
}
