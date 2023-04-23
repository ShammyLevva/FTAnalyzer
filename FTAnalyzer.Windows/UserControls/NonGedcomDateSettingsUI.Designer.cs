using System;

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
            this.components = new System.ComponentModel.Container();
            this.chkUseNonGedcomDates = new System.Windows.Forms.CheckBox();
            this.gbDateFormat = new System.Windows.Forms.GroupBox();
            this.rbyyyymmdd = new System.Windows.Forms.RadioButton();
            this.rbyyyyddmm = new System.Windows.Forms.RadioButton();
            this.rbmmddyyyy = new System.Windows.Forms.RadioButton();
            this.rbddmmyyyy = new System.Windows.Forms.RadioButton();
            this.gbSeparator = new System.Windows.Forms.GroupBox();
            this.rbSpace = new System.Windows.Forms.RadioButton();
            this.rbDot = new System.Windows.Forms.RadioButton();
            this.rbDash = new System.Windows.Forms.RadioButton();
            this.rbSlash = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbDateFormat.SuspendLayout();
            this.gbSeparator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkUseNonGedcomDates
            // 
            this.chkUseNonGedcomDates.AutoSize = true;
            this.chkUseNonGedcomDates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkUseNonGedcomDates.Location = new System.Drawing.Point(12, 15);
            this.chkUseNonGedcomDates.Name = "chkUseNonGedcomDates";
            this.chkUseNonGedcomDates.Size = new System.Drawing.Size(185, 17);
            this.chkUseNonGedcomDates.TabIndex = 1;
            this.chkUseNonGedcomDates.Text = "Allow Non GEDCOM date formats";
            this.chkUseNonGedcomDates.UseVisualStyleBackColor = true;
            this.chkUseNonGedcomDates.CheckedChanged += new System.EventHandler(this.ChkUseNonGedcomDates_CheckedChanged);
            // 
            // gbDateFormat
            // 
            this.gbDateFormat.Controls.Add(this.rbyyyymmdd);
            this.gbDateFormat.Controls.Add(this.rbyyyyddmm);
            this.gbDateFormat.Controls.Add(this.rbmmddyyyy);
            this.gbDateFormat.Controls.Add(this.rbddmmyyyy);
            this.gbDateFormat.Enabled = false;
            this.gbDateFormat.Location = new System.Drawing.Point(12, 130);
            this.gbDateFormat.Name = "gbDateFormat";
            this.gbDateFormat.Size = new System.Drawing.Size(200, 118);
            this.gbDateFormat.TabIndex = 6;
            this.gbDateFormat.TabStop = false;
            this.gbDateFormat.Text = "Allowed Date Format";
            // 
            // rbyyyymmdd
            // 
            this.rbyyyymmdd.AutoSize = true;
            this.rbyyyymmdd.Location = new System.Drawing.Point(6, 65);
            this.rbyyyymmdd.Name = "rbyyyymmdd";
            this.rbyyyymmdd.Size = new System.Drawing.Size(83, 17);
            this.rbyyyymmdd.TabIndex = 8;
            this.rbyyyymmdd.Text = "yyyy/mm/dd";
            this.rbyyyymmdd.UseVisualStyleBackColor = true;
            // 
            // rbyyyyddmm
            // 
            this.rbyyyyddmm.AutoSize = true;
            this.rbyyyyddmm.Location = new System.Drawing.Point(6, 88);
            this.rbyyyyddmm.Name = "rbyyyyddmm";
            this.rbyyyyddmm.Size = new System.Drawing.Size(83, 17);
            this.rbyyyyddmm.TabIndex = 9;
            this.rbyyyyddmm.Text = "yyyy/dd/mm";
            this.rbyyyyddmm.UseVisualStyleBackColor = true;
            // 
            // rbmmddyyyy
            // 
            this.rbmmddyyyy.AutoSize = true;
            this.rbmmddyyyy.Location = new System.Drawing.Point(6, 42);
            this.rbmmddyyyy.Name = "rbmmddyyyy";
            this.rbmmddyyyy.Size = new System.Drawing.Size(83, 17);
            this.rbmmddyyyy.TabIndex = 7;
            this.rbmmddyyyy.Text = "mm/dd/yyyy";
            this.rbmmddyyyy.UseVisualStyleBackColor = true;
            // 
            // rbddmmyyyy
            // 
            this.rbddmmyyyy.AutoSize = true;
            this.rbddmmyyyy.Checked = true;
            this.rbddmmyyyy.Location = new System.Drawing.Point(6, 19);
            this.rbddmmyyyy.Name = "rbddmmyyyy";
            this.rbddmmyyyy.Size = new System.Drawing.Size(83, 17);
            this.rbddmmyyyy.TabIndex = 6;
            this.rbddmmyyyy.TabStop = true;
            this.rbddmmyyyy.Text = "dd/mm/yyyy";
            this.rbddmmyyyy.UseVisualStyleBackColor = true;
            // 
            // gbSeparator
            // 
            this.gbSeparator.Controls.Add(this.rbSpace);
            this.gbSeparator.Controls.Add(this.rbDot);
            this.gbSeparator.Controls.Add(this.rbDash);
            this.gbSeparator.Controls.Add(this.rbSlash);
            this.gbSeparator.Enabled = false;
            this.gbSeparator.Location = new System.Drawing.Point(12, 53);
            this.gbSeparator.Name = "gbSeparator";
            this.gbSeparator.Size = new System.Drawing.Size(200, 71);
            this.gbSeparator.TabIndex = 7;
            this.gbSeparator.TabStop = false;
            this.gbSeparator.Text = "Date Separator";
            // 
            // rbSpace
            // 
            this.rbSpace.AutoSize = true;
            this.rbSpace.Location = new System.Drawing.Point(93, 42);
            this.rbSpace.Name = "rbSpace";
            this.rbSpace.Size = new System.Drawing.Size(62, 17);
            this.rbSpace.TabIndex = 5;
            this.rbSpace.Text = "  Space";
            this.rbSpace.UseVisualStyleBackColor = true;
            this.rbSpace.CheckedChanged += new System.EventHandler(this.RbSpace_CheckedChanged);
            // 
            // rbDot
            // 
            this.rbDot.AutoSize = true;
            this.rbDot.Location = new System.Drawing.Point(93, 19);
            this.rbDot.Name = "rbDot";
            this.rbDot.Size = new System.Drawing.Size(48, 17);
            this.rbDot.TabIndex = 3;
            this.rbDot.Text = ". Dot";
            this.rbDot.UseVisualStyleBackColor = true;
            this.rbDot.CheckedChanged += new System.EventHandler(this.RbDot_CheckedChanged);
            // 
            // rbDash
            // 
            this.rbDash.AutoSize = true;
            this.rbDash.Location = new System.Drawing.Point(6, 42);
            this.rbDash.Name = "rbDash";
            this.rbDash.Size = new System.Drawing.Size(56, 17);
            this.rbDash.TabIndex = 4;
            this.rbDash.Text = "- Dash";
            this.rbDash.UseVisualStyleBackColor = true;
            this.rbDash.CheckedChanged += new System.EventHandler(this.RbDash_CheckedChanged);
            // 
            // rbSlash
            // 
            this.rbSlash.AutoSize = true;
            this.rbSlash.Checked = true;
            this.rbSlash.Location = new System.Drawing.Point(6, 19);
            this.rbSlash.Name = "rbSlash";
            this.rbSlash.Size = new System.Drawing.Size(59, 17);
            this.rbSlash.TabIndex = 2;
            this.rbSlash.TabStop = true;
            this.rbSlash.Tag = "";
            this.rbSlash.Text = "/ Slash";
            this.rbSlash.UseVisualStyleBackColor = true;
            this.rbSlash.CheckedChanged += new System.EventHandler(this.RbSlash_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(9, 396);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "(*) Changing these options requires a GEDCOM reload";
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // NonGedcomDateSettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gbSeparator);
            this.Controls.Add(this.gbDateFormat);
            this.Controls.Add(this.chkUseNonGedcomDates);
            this.Name = "NonGedcomDateSettingsUI";
            this.Size = new System.Drawing.Size(325, 421);
            this.Leave += new System.EventHandler(this.NonGedcomDateSettingsUI_Leave);
            this.gbDateFormat.ResumeLayout(false);
            this.gbDateFormat.PerformLayout();
            this.gbSeparator.ResumeLayout(false);
            this.gbSeparator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkUseNonGedcomDates;
        private System.Windows.Forms.GroupBox gbDateFormat;
        private System.Windows.Forms.RadioButton rbyyyymmdd;
        private System.Windows.Forms.RadioButton rbyyyyddmm;
        private System.Windows.Forms.RadioButton rbmmddyyyy;
        private System.Windows.Forms.RadioButton rbddmmyyyy;
        private System.Windows.Forms.GroupBox gbSeparator;
        private System.Windows.Forms.RadioButton rbDash;
        private System.Windows.Forms.RadioButton rbSlash;
        private System.Windows.Forms.RadioButton rbSpace;
        private System.Windows.Forms.RadioButton rbDot;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
