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
                if (disposing && (components is not null))
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
            label1 = new Label();
            cbCensusDate = new ComboBox();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(4, 7);
            label1.Margin = new Padding(4, 0, 4, 0);
            label1.Name = "label1";
            label1.Size = new Size(72, 15);
            label1.TabIndex = 4;
            label1.Text = "Census Date";
            // 
            // cbCensusDate
            // 
            cbCensusDate.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCensusDate.FormattingEnabled = true;
            cbCensusDate.Location = new Point(90, 4);
            cbCensusDate.Margin = new Padding(4, 4, 4, 4);
            cbCensusDate.Name = "cbCensusDate";
            cbCensusDate.Size = new Size(56, 23);
            cbCensusDate.TabIndex = 3;
            cbCensusDate.SelectedIndexChanged += CbCensusDate_SelectedIndexChanged;
            // 
            // CensusDateSelector
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            Controls.Add(label1);
            Controls.Add(cbCensusDate);
            Margin = new Padding(4, 4, 4, 4);
            Name = "CensusDateSelector";
            Size = new Size(150, 31);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCensusDate;
    }
}
