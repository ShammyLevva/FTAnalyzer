namespace DgvFilterPopup {
    partial class DgvNumRangeColumnFilter {
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
            this.comboBoxOperator = new System.Windows.Forms.ComboBox();
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.textBoxValue2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.Location = new System.Drawing.Point(3, 3);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(55, 21);
            this.comboBoxOperator.TabIndex = 0;
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(64, 3);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(71, 20);
            this.textBoxValue.TabIndex = 1;
            // 
            // textBoxValue2
            // 
            this.textBoxValue2.Location = new System.Drawing.Point(64, 29);
            this.textBoxValue2.Name = "textBoxValue2";
            this.textBoxValue2.Size = new System.Drawing.Size(71, 20);
            this.textBoxValue2.TabIndex = 2;
            // 
            // DgvNumRangeColumnFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.textBoxValue2);
            this.Controls.Add(this.textBoxValue);
            this.Controls.Add(this.comboBoxOperator);
            this.Name = "DgvNumRangeColumnFilter";
            this.Size = new System.Drawing.Size(139, 54);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxOperator;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.TextBox textBoxValue2;
    }
}
