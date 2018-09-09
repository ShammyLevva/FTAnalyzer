namespace FTAnalyzer.UserControls
{
    partial class CensusSettingsUI
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
            this.components = new System.ComponentModel.Container();
            this.chkAddCreatedLocations = new System.Windows.Forms.CheckBox();
            this.chkAutoCreateCensus = new System.Windows.Forms.CheckBox();
            this.chkCompactCensusRef = new System.Windows.Forms.CheckBox();
            this.chkFamilyCensus = new System.Windows.Forms.CheckBox();
            this.chkTolerateInaccurateCensus = new System.Windows.Forms.CheckBox();
            this.chkCensusResidence = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkHideMissingTagged = new System.Windows.Forms.CheckBox();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // chkAddCreatedLocations
            // 
            this.chkAddCreatedLocations.AutoSize = true;
            this.chkAddCreatedLocations.Checked = true;
            this.chkAddCreatedLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddCreatedLocations.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkAddCreatedLocations.Location = new System.Drawing.Point(17, 118);
            this.chkAddCreatedLocations.Name = "chkAddCreatedLocations";
            this.chkAddCreatedLocations.Size = new System.Drawing.Size(290, 17);
            this.chkAddCreatedLocations.TabIndex = 36;
            this.chkAddCreatedLocations.Text = "Add Auto Created Census Locations to Locations List (*)";
            this.chkAddCreatedLocations.UseVisualStyleBackColor = true;
            // 
            // chkAutoCreateCensus
            // 
            this.chkAutoCreateCensus.AutoSize = true;
            this.chkAutoCreateCensus.Checked = true;
            this.chkAutoCreateCensus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCreateCensus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkAutoCreateCensus.Location = new System.Drawing.Point(17, 95);
            this.chkAutoCreateCensus.Name = "chkAutoCreateCensus";
            this.chkAutoCreateCensus.Size = new System.Drawing.Size(274, 17);
            this.chkAutoCreateCensus.TabIndex = 35;
            this.chkAutoCreateCensus.Text = "Auto Create Census Events from Notes && Sources (*)";
            this.chkAutoCreateCensus.UseVisualStyleBackColor = true;
            // 
            // chkCompactCensusRef
            // 
            this.chkCompactCensusRef.AutoSize = true;
            this.chkCompactCensusRef.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkCompactCensusRef.Location = new System.Drawing.Point(17, 72);
            this.chkCompactCensusRef.Name = "chkCompactCensusRef";
            this.chkCompactCensusRef.Size = new System.Drawing.Size(186, 17);
            this.chkCompactCensusRef.TabIndex = 34;
            this.chkCompactCensusRef.Text = "Use Compact Census References";
            this.chkCompactCensusRef.UseVisualStyleBackColor = true;
            // 
            // chkFamilyCensus
            // 
            this.chkFamilyCensus.AutoSize = true;
            this.chkFamilyCensus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkFamilyCensus.Location = new System.Drawing.Point(17, 49);
            this.chkFamilyCensus.Name = "chkFamilyCensus";
            this.chkFamilyCensus.Size = new System.Drawing.Size(243, 17);
            this.chkFamilyCensus.TabIndex = 33;
            this.chkFamilyCensus.Text = "Family Census Facts Apply To Only Parents (*)";
            this.chkFamilyCensus.UseVisualStyleBackColor = true;
            // 
            // chkTolerateInaccurateCensus
            // 
            this.chkTolerateInaccurateCensus.AutoSize = true;
            this.chkTolerateInaccurateCensus.Checked = true;
            this.chkTolerateInaccurateCensus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTolerateInaccurateCensus.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkTolerateInaccurateCensus.Location = new System.Drawing.Point(17, 26);
            this.chkTolerateInaccurateCensus.Name = "chkTolerateInaccurateCensus";
            this.chkTolerateInaccurateCensus.Size = new System.Drawing.Size(237, 17);
            this.chkTolerateInaccurateCensus.TabIndex = 32;
            this.chkTolerateInaccurateCensus.Text = "Tolerate Slightly Inaccurate Census Dates (*)";
            this.chkTolerateInaccurateCensus.UseVisualStyleBackColor = true;
            // 
            // chkCensusResidence
            // 
            this.chkCensusResidence.AutoSize = true;
            this.chkCensusResidence.Checked = true;
            this.chkCensusResidence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCensusResidence.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkCensusResidence.Location = new System.Drawing.Point(17, 3);
            this.chkCensusResidence.Name = "chkCensusResidence";
            this.chkCensusResidence.Size = new System.Drawing.Size(229, 17);
            this.chkCensusResidence.TabIndex = 31;
            this.chkCensusResidence.Text = "Treat Residence Facts As Census Facts (*)";
            this.chkCensusResidence.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(14, 408);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 13);
            this.label1.TabIndex = 37;
            this.label1.Text = "(*) This option requires a GEDCOM reload";
            // 
            // chkHideMissingTagged
            // 
            this.chkHideMissingTagged.AutoSize = true;
            this.chkHideMissingTagged.Checked = true;
            this.chkHideMissingTagged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHideMissingTagged.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.chkHideMissingTagged.Location = new System.Drawing.Point(17, 141);
            this.chkHideMissingTagged.Name = "chkHideMissingTagged";
            this.chkHideMissingTagged.Size = new System.Drawing.Size(241, 17);
            this.chkHideMissingTagged.TabIndex = 38;
            this.chkHideMissingTagged.Text = "Hide People Tagged As Missing From Census";
            this.chkHideMissingTagged.UseVisualStyleBackColor = true;
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // CensusSettingsUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkHideMissingTagged);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkAddCreatedLocations);
            this.Controls.Add(this.chkAutoCreateCensus);
            this.Controls.Add(this.chkCompactCensusRef);
            this.Controls.Add(this.chkFamilyCensus);
            this.Controls.Add(this.chkTolerateInaccurateCensus);
            this.Controls.Add(this.chkCensusResidence);
            this.Name = "CensusSettingsUI";
            this.Size = new System.Drawing.Size(325, 421);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkAddCreatedLocations;
        private System.Windows.Forms.CheckBox chkAutoCreateCensus;
        private System.Windows.Forms.CheckBox chkCompactCensusRef;
        private System.Windows.Forms.CheckBox chkFamilyCensus;
        private System.Windows.Forms.CheckBox chkTolerateInaccurateCensus;
        private System.Windows.Forms.CheckBox chkCensusResidence;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkHideMissingTagged;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
