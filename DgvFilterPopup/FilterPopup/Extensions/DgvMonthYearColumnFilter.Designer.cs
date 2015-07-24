namespace DgvFilterPopup {

    /// <summary>
    /// An extended <i>column filter</i> implementation allowing filters on date, using months and years.
    /// </summary>
    partial class DgvMonthYearColumnFilter {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.comboBoxYear = new System.Windows.Forms.ComboBox();
            this.comboBoxMonth = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // comboBoxYear
            // 
            this.comboBoxYear.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxYear.FormattingEnabled = true;
            this.comboBoxYear.Location = new System.Drawing.Point(102, 3);
            this.comboBoxYear.Name = "comboBoxYear";
            this.comboBoxYear.Size = new System.Drawing.Size(78, 21);
            this.comboBoxYear.TabIndex = 3;
            // 
            // comboBoxMonth
            // 
            this.comboBoxMonth.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxMonth.FormattingEnabled = true;
            this.comboBoxMonth.Location = new System.Drawing.Point(3, 3);
            this.comboBoxMonth.MaxDropDownItems = 13;
            this.comboBoxMonth.Name = "comboBoxMonth";
            this.comboBoxMonth.Size = new System.Drawing.Size(93, 21);
            this.comboBoxMonth.TabIndex = 2;
            // 
            // DgvMonthYearColumnFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.comboBoxYear);
            this.Controls.Add(this.comboBoxMonth);
            this.Name = "DgvMonthYearColumnFilter";
            this.Size = new System.Drawing.Size(188, 27);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxYear;
        private System.Windows.Forms.ComboBox comboBoxMonth;
    }
}
