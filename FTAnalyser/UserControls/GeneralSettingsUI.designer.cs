namespace FTAnalyzer.UserControls
{
	partial class GeneralSettingsUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeneralSettingsUI));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.chkUseBaptisms = new System.Windows.Forms.CheckBox();
            this.chkAllowEmptyLocations = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.upDownAge = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.chkUseBurials = new System.Windows.Forms.CheckBox();
            this.chkMultipleFactForms = new System.Windows.Forms.CheckBox();
            this.chkUseAlias = new System.Windows.Forms.CheckBox();
            this.chkReverseLocations = new System.Windows.Forms.CheckBox();
            this.chkShowWorldEvents = new System.Windows.Forms.CheckBox();
            this.chkIgnoreFactTypeWarnings = new System.Windows.Forms.CheckBox();
            this.chkTreatFemaleAsUnknown = new System.Windows.Forms.CheckBox();
            this.chkMultiAncestor = new System.Windows.Forms.CheckBox();
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
            this.chkAllowEmptyLocations.CheckedChanged += new System.EventHandler(this.ChkAllowEmptyLocations_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
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
            // chkUseAlias
            // 
            resources.ApplyResources(this.chkUseAlias, "chkUseAlias");
            this.chkUseAlias.Name = "chkUseAlias";
            this.chkUseAlias.UseVisualStyleBackColor = true;
            // 
            // chkReverseLocations
            // 
            resources.ApplyResources(this.chkReverseLocations, "chkReverseLocations");
            this.chkReverseLocations.Name = "chkReverseLocations";
            this.chkReverseLocations.UseVisualStyleBackColor = true;
            this.chkReverseLocations.CheckedChanged += new System.EventHandler(this.ChkReverseLocations_CheckedChanged);
            // 
            // chkShowWorldEvents
            // 
            resources.ApplyResources(this.chkShowWorldEvents, "chkShowWorldEvents");
            this.chkShowWorldEvents.Checked = true;
            this.chkShowWorldEvents.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowWorldEvents.Name = "chkShowWorldEvents";
            this.chkShowWorldEvents.UseVisualStyleBackColor = true;
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
            this.chkTreatFemaleAsUnknown.CheckedChanged += new System.EventHandler(this.ChkTreatFemaleAsUnknown_CheckedChanged);
            // 
            // chkMultiAncestor
            // 
            resources.ApplyResources(this.chkMultiAncestor, "chkMultiAncestor");
            this.chkMultiAncestor.Name = "chkMultiAncestor";
            this.chkMultiAncestor.UseVisualStyleBackColor = true;
            // 
            // GeneralSettingsUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkMultiAncestor);
            this.Controls.Add(this.chkTreatFemaleAsUnknown);
            this.Controls.Add(this.chkIgnoreFactTypeWarnings);
            this.Controls.Add(this.chkShowWorldEvents);
            this.Controls.Add(this.chkReverseLocations);
            this.Controls.Add(this.chkUseAlias);
            this.Controls.Add(this.chkMultipleFactForms);
            this.Controls.Add(this.chkUseBurials);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.upDownAge);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkAllowEmptyLocations);
            this.Controls.Add(this.chkUseBaptisms);
            this.Name = "GeneralSettingsUI";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.upDownAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.CheckBox chkUseBaptisms;
        private System.Windows.Forms.CheckBox chkAllowEmptyLocations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown upDownAge;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkUseBurials;
        private System.Windows.Forms.CheckBox chkMultipleFactForms;
        private System.Windows.Forms.CheckBox chkUseAlias;
        private System.Windows.Forms.CheckBox chkReverseLocations;
        private System.Windows.Forms.CheckBox chkShowWorldEvents;
        private System.Windows.Forms.CheckBox chkIgnoreFactTypeWarnings;
        private System.Windows.Forms.CheckBox chkTreatFemaleAsUnknown;
        private System.Windows.Forms.CheckBox chkMultiAncestor;
    }
}
