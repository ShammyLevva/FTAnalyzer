﻿namespace FTAnalyzer.UserControls
{
	partial class GeneralSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralSettings));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkUseBaptisms = new System.Windows.Forms.CheckBox();
            this.chkAllowEmptyLocations = new System.Windows.Forms.CheckBox();
            this.chkCensusResidence = new System.Windows.Forms.CheckBox();
            this.chkTolerateInaccurateCensus = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkFamilyCensus = new System.Windows.Forms.CheckBox();
            this.upDownAge = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.chkUseBurials = new System.Windows.Forms.CheckBox();
            this.chkMultipleFactForms = new System.Windows.Forms.CheckBox();
            this.chkCompactCensusRef = new System.Windows.Forms.CheckBox();
            this.chkUseAlias = new System.Windows.Forms.CheckBox();
            this.chkHideMissingTagged = new System.Windows.Forms.CheckBox();
            this.chkReverseLocations = new System.Windows.Forms.CheckBox();
            this.chkAutoCreateCensus = new System.Windows.Forms.CheckBox();
            this.chkShowWorldEvents = new System.Windows.Forms.CheckBox();
            this.chkAddCreatedLocations = new System.Windows.Forms.CheckBox();
            this.chkIgnoreFactTypeWarnings = new System.Windows.Forms.CheckBox();
            this.chkTreatFemaleAsUnknown = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAge)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chkUseBaptisms
            // 
            resources.ApplyResources(this.chkUseBaptisms, "chkUseBaptisms");
            this.chkUseBaptisms.Checked = true;
            this.chkUseBaptisms.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseBaptisms.Name = "chkUseBaptisms";
            this.chkUseBaptisms.UseVisualStyleBackColor = true;
            // 
            // chkAllowEmptyLocations
            // 
            resources.ApplyResources(this.chkAllowEmptyLocations, "chkAllowEmptyLocations");
            this.chkAllowEmptyLocations.Name = "chkAllowEmptyLocations";
            this.chkAllowEmptyLocations.UseVisualStyleBackColor = true;
            this.chkAllowEmptyLocations.CheckedChanged += new System.EventHandler(this.chkAllowEmptyLocations_CheckedChanged);
            // 
            // chkCensusResidence
            // 
            resources.ApplyResources(this.chkCensusResidence, "chkCensusResidence");
            this.chkCensusResidence.Checked = true;
            this.chkCensusResidence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkCensusResidence.Name = "chkCensusResidence";
            this.chkCensusResidence.UseVisualStyleBackColor = true;
            this.chkCensusResidence.CheckedChanged += new System.EventHandler(this.chkCensusResidence_CheckedChanged);
            // 
            // chkTolerateInaccurateCensus
            // 
            resources.ApplyResources(this.chkTolerateInaccurateCensus, "chkTolerateInaccurateCensus");
            this.chkTolerateInaccurateCensus.Checked = true;
            this.chkTolerateInaccurateCensus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTolerateInaccurateCensus.Name = "chkTolerateInaccurateCensus";
            this.chkTolerateInaccurateCensus.UseVisualStyleBackColor = true;
            this.chkTolerateInaccurateCensus.CheckedChanged += new System.EventHandler(this.chkTolerateInaccurateCensus_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkFamilyCensus
            // 
            resources.ApplyResources(this.chkFamilyCensus, "chkFamilyCensus");
            this.chkFamilyCensus.Name = "chkFamilyCensus";
            this.chkFamilyCensus.UseVisualStyleBackColor = true;
            this.chkFamilyCensus.CheckedChanged += new System.EventHandler(this.chkFamilyCensus_CheckedChanged);
            // 
            // upDownAge
            // 
            resources.ApplyResources(this.upDownAge, "upDownAge");
            this.upDownAge.Maximum = new decimal(new int[] {
            18,
            0,
            0,
            0});
            this.upDownAge.Minimum = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.upDownAge.Name = "upDownAge";
            this.upDownAge.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // chkUseBurials
            // 
            resources.ApplyResources(this.chkUseBurials, "chkUseBurials");
            this.chkUseBurials.Checked = true;
            this.chkUseBurials.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkUseBurials.Name = "chkUseBurials";
            this.chkUseBurials.UseVisualStyleBackColor = true;
            // 
            // chkMultipleFactForms
            // 
            resources.ApplyResources(this.chkMultipleFactForms, "chkMultipleFactForms");
            this.chkMultipleFactForms.Name = "chkMultipleFactForms";
            this.chkMultipleFactForms.UseVisualStyleBackColor = true;
            // 
            // chkCompactCensusRef
            // 
            resources.ApplyResources(this.chkCompactCensusRef, "chkCompactCensusRef");
            this.chkCompactCensusRef.Name = "chkCompactCensusRef";
            this.chkCompactCensusRef.UseVisualStyleBackColor = true;
            // 
            // chkUseAlias
            // 
            resources.ApplyResources(this.chkUseAlias, "chkUseAlias");
            this.chkUseAlias.Name = "chkUseAlias";
            this.chkUseAlias.UseVisualStyleBackColor = true;
            // 
            // chkHideMissingTagged
            // 
            resources.ApplyResources(this.chkHideMissingTagged, "chkHideMissingTagged");
            this.chkHideMissingTagged.Checked = true;
            this.chkHideMissingTagged.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkHideMissingTagged.Name = "chkHideMissingTagged";
            this.chkHideMissingTagged.UseVisualStyleBackColor = true;
            // 
            // chkReverseLocations
            // 
            resources.ApplyResources(this.chkReverseLocations, "chkReverseLocations");
            this.chkReverseLocations.Name = "chkReverseLocations";
            this.chkReverseLocations.UseVisualStyleBackColor = true;
            this.chkReverseLocations.CheckedChanged += new System.EventHandler(this.chkReverseLocations_CheckedChanged);
            // 
            // chkAutoCreateCensus
            // 
            resources.ApplyResources(this.chkAutoCreateCensus, "chkAutoCreateCensus");
            this.chkAutoCreateCensus.Checked = true;
            this.chkAutoCreateCensus.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAutoCreateCensus.Name = "chkAutoCreateCensus";
            this.chkAutoCreateCensus.UseVisualStyleBackColor = true;
            this.chkAutoCreateCensus.CheckedChanged += new System.EventHandler(this.chkAutoCreateCensus_CheckedChanged);
            // 
            // chkShowWorldEvents
            // 
            resources.ApplyResources(this.chkShowWorldEvents, "chkShowWorldEvents");
            this.chkShowWorldEvents.Checked = true;
            this.chkShowWorldEvents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowWorldEvents.Name = "chkShowWorldEvents";
            this.chkShowWorldEvents.UseVisualStyleBackColor = true;
            // 
            // chkAddCreatedLocations
            // 
            resources.ApplyResources(this.chkAddCreatedLocations, "chkAddCreatedLocations");
            this.chkAddCreatedLocations.Checked = true;
            this.chkAddCreatedLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAddCreatedLocations.Name = "chkAddCreatedLocations";
            this.chkAddCreatedLocations.UseVisualStyleBackColor = true;
            this.chkAddCreatedLocations.CheckedChanged += new System.EventHandler(this.chkAddCreatedLocations_CheckedChanged);
            // 
            // chkIgnoreFactTypeWarnings
            // 
            resources.ApplyResources(this.chkIgnoreFactTypeWarnings, "chkIgnoreFactTypeWarnings");
            this.chkIgnoreFactTypeWarnings.Name = "chkIgnoreFactTypeWarnings";
            this.chkIgnoreFactTypeWarnings.UseVisualStyleBackColor = true;
            // 
            // chkTreatFemaleAsUnknown
            // 
            resources.ApplyResources(this.chkTreatFemaleAsUnknown, "chkTreatFemaleAsUnknown");
            this.chkTreatFemaleAsUnknown.Checked = true;
            this.chkTreatFemaleAsUnknown.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkTreatFemaleAsUnknown.Name = "chkTreatFemaleAsUnknown";
            this.chkTreatFemaleAsUnknown.UseVisualStyleBackColor = true;
            this.chkTreatFemaleAsUnknown.CheckedChanged += new System.EventHandler(this.chkTreatFemaleAsUnknown_CheckedChanged);
            // 
            // GeneralSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkTreatFemaleAsUnknown);
            this.Controls.Add(this.chkIgnoreFactTypeWarnings);
            this.Controls.Add(this.chkAddCreatedLocations);
            this.Controls.Add(this.chkShowWorldEvents);
            this.Controls.Add(this.chkAutoCreateCensus);
            this.Controls.Add(this.chkReverseLocations);
            this.Controls.Add(this.chkHideMissingTagged);
            this.Controls.Add(this.chkUseAlias);
            this.Controls.Add(this.chkCompactCensusRef);
            this.Controls.Add(this.chkMultipleFactForms);
            this.Controls.Add(this.chkUseBurials);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.upDownAge);
            this.Controls.Add(this.chkFamilyCensus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkTolerateInaccurateCensus);
            this.Controls.Add(this.chkCensusResidence);
            this.Controls.Add(this.chkAllowEmptyLocations);
            this.Controls.Add(this.chkUseBaptisms);
            this.Name = "GeneralSettings";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox chkUseBaptisms;
        private System.Windows.Forms.CheckBox chkAllowEmptyLocations;
        private System.Windows.Forms.CheckBox chkCensusResidence;
        private System.Windows.Forms.CheckBox chkTolerateInaccurateCensus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkFamilyCensus;
        private System.Windows.Forms.NumericUpDown upDownAge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkUseBurials;
        private System.Windows.Forms.CheckBox chkMultipleFactForms;
        private System.Windows.Forms.CheckBox chkCompactCensusRef;
        private System.Windows.Forms.CheckBox chkUseAlias;
        private System.Windows.Forms.CheckBox chkHideMissingTagged;
        private System.Windows.Forms.CheckBox chkReverseLocations;
        private System.Windows.Forms.CheckBox chkShowWorldEvents;
        private System.Windows.Forms.CheckBox chkAutoCreateCensus;
        private System.Windows.Forms.CheckBox chkAddCreatedLocations;
        private System.Windows.Forms.CheckBox chkIgnoreFactTypeWarnings;
        private System.Windows.Forms.CheckBox chkTreatFemaleAsUnknown;


	}
}
