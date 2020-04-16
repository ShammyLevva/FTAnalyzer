using System;

namespace FTAnalyzer.UserControls
{
	partial class MappingSettingsUI
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MappingSettingsUI));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.txtMapPath = new System.Windows.Forms.TextBox();
            this.btnBrowseFolder = new System.Windows.Forms.Button();
            this.ckbUseParishBoundaries = new System.Windows.Forms.CheckBox();
            this.ckbHideScaleBar = new System.Windows.Forms.CheckBox();
            this.chkIncludePartialGeocoded = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtGoogleAPI = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtMapPath
            // 
            resources.ApplyResources(this.txtMapPath, "txtMapPath");
            this.txtMapPath.Name = "txtMapPath";
            // 
            // btnBrowseFolder
            // 
            resources.ApplyResources(this.btnBrowseFolder, "btnBrowseFolder");
            this.btnBrowseFolder.Name = "btnBrowseFolder";
            this.btnBrowseFolder.UseVisualStyleBackColor = true;
            this.btnBrowseFolder.Click += new System.EventHandler(this.BtnBrowseFolder_Click);
            // 
            // ckbUseParishBoundaries
            // 
            resources.ApplyResources(this.ckbUseParishBoundaries, "ckbUseParishBoundaries");
            this.ckbUseParishBoundaries.Checked = true;
            this.ckbUseParishBoundaries.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbUseParishBoundaries.Name = "ckbUseParishBoundaries";
            this.ckbUseParishBoundaries.UseVisualStyleBackColor = true;
            // 
            // ckbHideScaleBar
            // 
            resources.ApplyResources(this.ckbHideScaleBar, "ckbHideScaleBar");
            this.ckbHideScaleBar.Checked = true;
            this.ckbHideScaleBar.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbHideScaleBar.Name = "ckbHideScaleBar";
            this.ckbHideScaleBar.UseVisualStyleBackColor = true;
            // 
            // chkIncludePartialGeocoded
            // 
            resources.ApplyResources(this.chkIncludePartialGeocoded, "chkIncludePartialGeocoded");
            this.chkIncludePartialGeocoded.Checked = true;
            this.chkIncludePartialGeocoded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIncludePartialGeocoded.Name = "chkIncludePartialGeocoded";
            this.chkIncludePartialGeocoded.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtGoogleAPI
            // 
            resources.ApplyResources(this.txtGoogleAPI, "txtGoogleAPI");
            this.txtGoogleAPI.Name = "txtGoogleAPI";
            // 
            // MappingSettingsUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtGoogleAPI);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkIncludePartialGeocoded);
            this.Controls.Add(this.ckbHideScaleBar);
            this.Controls.Add(this.ckbUseParishBoundaries);
            this.Controls.Add(this.btnBrowseFolder);
            this.Controls.Add(this.txtMapPath);
            this.Controls.Add(this.label1);
            this.Name = "MappingSettingsUI";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.Button btnBrowseFolder;
        private System.Windows.Forms.TextBox txtMapPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox ckbUseParishBoundaries;
        private System.Windows.Forms.CheckBox ckbHideScaleBar;
        private System.Windows.Forms.CheckBox chkIncludePartialGeocoded;
        private System.Windows.Forms.TextBox txtGoogleAPI;
        private System.Windows.Forms.Label label2;
    }
}
