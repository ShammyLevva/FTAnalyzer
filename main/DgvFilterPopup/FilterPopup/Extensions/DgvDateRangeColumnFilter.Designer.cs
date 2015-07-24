namespace DgvFilterPopup {
    partial class DgvDateRangeColumnFilter {
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
            this.dateTimePickerValue = new System.Windows.Forms.DateTimePicker();
            this.dateTimePickerValue2 = new System.Windows.Forms.DateTimePicker();
            this.SuspendLayout();
            // 
            // comboBoxOperator
            // 
            this.comboBoxOperator.FormattingEnabled = true;
            this.comboBoxOperator.Location = new System.Drawing.Point(3, 3);
            this.comboBoxOperator.Name = "comboBoxOperator";
            this.comboBoxOperator.Size = new System.Drawing.Size(49, 21);
            this.comboBoxOperator.TabIndex = 0;
            // 
            // dateTimePickerValue
            // 
            this.dateTimePickerValue.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerValue.Location = new System.Drawing.Point(58, 3);
            this.dateTimePickerValue.Name = "dateTimePickerValue";
            this.dateTimePickerValue.Size = new System.Drawing.Size(127, 20);
            this.dateTimePickerValue.TabIndex = 1;
            // 
            // dateTimePickerValueUntil
            // 
            this.dateTimePickerValue2.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePickerValue2.Location = new System.Drawing.Point(58, 29);
            this.dateTimePickerValue2.Name = "dateTimePickerValueUntil";
            this.dateTimePickerValue2.Size = new System.Drawing.Size(127, 20);
            this.dateTimePickerValue2.TabIndex = 2;
            // 
            // DgvDateRangeColumnFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.dateTimePickerValue2);
            this.Controls.Add(this.dateTimePickerValue);
            this.Controls.Add(this.comboBoxOperator);
            this.Name = "DgvDateRangeColumnFilter";
            this.Size = new System.Drawing.Size(191, 54);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxOperator;
        private System.Windows.Forms.DateTimePicker dateTimePickerValue;
        private System.Windows.Forms.DateTimePicker dateTimePickerValue2;
    }
}
