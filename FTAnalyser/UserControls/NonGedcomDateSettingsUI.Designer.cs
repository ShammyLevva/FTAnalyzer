namespace FTAnalyzer.UserControls
{
    partial class NonGedcomDateSettingsUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.chkUseNonGedcomDates = new System.Windows.Forms.CheckBox();
            this.gbDateFormat = new System.Windows.Forms.GroupBox();
            this.rbddmmyyyy = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.rbyyyyddmm = new System.Windows.Forms.RadioButton();
            this.rbyyyymmdd = new System.Windows.Forms.RadioButton();
            this.gbSeparator = new System.Windows.Forms.GroupBox();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.gbDateFormat.SuspendLayout();
            this.gbSeparator.SuspendLayout();
            this.SuspendLayout();
            // 
            // chkUseNonGedcomDates
            // 
            this.chkUseNonGedcomDates.AutoSize = true;
            this.chkUseNonGedcomDates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkUseNonGedcomDates.Location = new System.Drawing.Point(12, 15);
            this.chkUseNonGedcomDates.Name = "chkUseNonGedcomDates";
            this.chkUseNonGedcomDates.Size = new System.Drawing.Size(185, 17);
            this.chkUseNonGedcomDates.TabIndex = 4;
            this.chkUseNonGedcomDates.Text = "Allow Non GEDCOM date formats";
            this.chkUseNonGedcomDates.UseVisualStyleBackColor = true;
            // 
            // gbDateFormat
            // 
            this.gbDateFormat.Controls.Add(this.rbyyyymmdd);
            this.gbDateFormat.Controls.Add(this.rbyyyyddmm);
            this.gbDateFormat.Controls.Add(this.radioButton3);
            this.gbDateFormat.Controls.Add(this.rbddmmyyyy);
            this.gbDateFormat.Enabled = false;
            this.gbDateFormat.Location = new System.Drawing.Point(12, 115);
            this.gbDateFormat.Name = "gbDateFormat";
            this.gbDateFormat.Size = new System.Drawing.Size(200, 118);
            this.gbDateFormat.TabIndex = 6;
            this.gbDateFormat.TabStop = false;
            this.gbDateFormat.Text = "Allowed Date Format";
            // 
            // rbddmmyyyy
            // 
            this.rbddmmyyyy.AutoSize = true;
            this.rbddmmyyyy.Checked = true;
            this.rbddmmyyyy.Location = new System.Drawing.Point(6, 19);
            this.rbddmmyyyy.Name = "rbddmmyyyy";
            this.rbddmmyyyy.Size = new System.Drawing.Size(83, 17);
            this.rbddmmyyyy.TabIndex = 7;
            this.rbddmmyyyy.TabStop = true;
            this.rbddmmyyyy.Text = "dd/mm/yyyy";
            this.rbddmmyyyy.UseVisualStyleBackColor = true;
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(6, 42);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(83, 17);
            this.radioButton3.TabIndex = 9;
            this.radioButton3.Text = "mm/dd/yyyy";
            this.radioButton3.UseVisualStyleBackColor = true;
            // 
            // rbyyyyddmm
            // 
            this.rbyyyyddmm.AutoSize = true;
            this.rbyyyyddmm.Location = new System.Drawing.Point(6, 88);
            this.rbyyyyddmm.Name = "rbyyyyddmm";
            this.rbyyyyddmm.Size = new System.Drawing.Size(83, 17);
            this.rbyyyyddmm.TabIndex = 10;
            this.rbyyyyddmm.Text = "yyyy/dd/mm";
            this.rbyyyyddmm.UseVisualStyleBackColor = true;
            // 
            // rbyyyymmdd
            // 
            this.rbyyyymmdd.AutoSize = true;
            this.rbyyyymmdd.Location = new System.Drawing.Point(6, 65);
            this.rbyyyymmdd.Name = "rbyyyymmdd";
            this.rbyyyymmdd.Size = new System.Drawing.Size(83, 17);
            this.rbyyyymmdd.TabIndex = 11;
            this.rbyyyymmdd.Text = "yyyy/mm/dd";
            this.rbyyyymmdd.UseVisualStyleBackColor = true;
            // 
            // gbSeparator
            // 
            this.gbSeparator.Controls.Add(this.radioButton5);
            this.gbSeparator.Controls.Add(this.radioButton4);
            this.gbSeparator.Controls.Add(this.radioButton2);
            this.gbSeparator.Controls.Add(this.radioButton1);
            this.gbSeparator.Enabled = false;
            this.gbSeparator.Location = new System.Drawing.Point(12, 38);
            this.gbSeparator.Name = "gbSeparator";
            this.gbSeparator.Size = new System.Drawing.Size(200, 71);
            this.gbSeparator.TabIndex = 7;
            this.gbSeparator.TabStop = false;
            this.gbSeparator.Text = "Date Separator";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 19);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(59, 17);
            this.radioButton1.TabIndex = 7;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "/ Slash";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(6, 42);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(56, 17);
            this.radioButton2.TabIndex = 8;
            this.radioButton2.Text = "- Dash";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Location = new System.Drawing.Point(93, 19);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(48, 17);
            this.radioButton4.TabIndex = 9;
            this.radioButton4.Text = ". Dot";
            this.radioButton4.UseVisualStyleBackColor = true;
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Location = new System.Drawing.Point(93, 42);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(62, 17);
            this.radioButton5.TabIndex = 10;
            this.radioButton5.Text = "  Space";
            this.radioButton5.UseVisualStyleBackColor = true;
            // 
            // NonGedcomDateSettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSeparator);
            this.Controls.Add(this.gbDateFormat);
            this.Controls.Add(this.chkUseNonGedcomDates);
            this.Name = "NonGedcomDateSettingsUI";
            this.Size = new System.Drawing.Size(312, 421);
            this.gbDateFormat.ResumeLayout(false);
            this.gbDateFormat.PerformLayout();
            this.gbSeparator.ResumeLayout(false);
            this.gbSeparator.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkUseNonGedcomDates;
        private System.Windows.Forms.GroupBox gbDateFormat;
        private System.Windows.Forms.RadioButton rbyyyymmdd;
        private System.Windows.Forms.RadioButton rbyyyyddmm;
        private System.Windows.Forms.RadioButton radioButton3;
        private System.Windows.Forms.RadioButton rbddmmyyyy;
        private System.Windows.Forms.GroupBox gbSeparator;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton5;
        private System.Windows.Forms.RadioButton radioButton4;
    }
}
