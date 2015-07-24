﻿namespace FTAnalyzer.UserControls
{
	partial class FileHandlingSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileHandlingSettings));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.chkLoadWithFilters = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRetryFailedLines = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // chkLoadWithFilters
            // 
            resources.ApplyResources(this.chkLoadWithFilters, "chkLoadWithFilters");
            this.chkLoadWithFilters.Name = "chkLoadWithFilters";
            this.chkLoadWithFilters.UseVisualStyleBackColor = true;
            this.chkLoadWithFilters.CheckedChanged += new System.EventHandler(this.chkLoadWithFilters_CheckedChanged);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // chkRetryFailedLines
            // 
            resources.ApplyResources(this.chkRetryFailedLines, "chkRetryFailedLines");
            this.chkRetryFailedLines.Name = "chkRetryFailedLines";
            this.chkRetryFailedLines.UseVisualStyleBackColor = true;
            this.chkRetryFailedLines.CheckedChanged += new System.EventHandler(this.chkRetryFailedLines_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // FileHandlingSettings
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.chkRetryFailedLines);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chkLoadWithFilters);
            this.Name = "FileHandlingSettings";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.CheckBox chkLoadWithFilters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRetryFailedLines;
        private System.Windows.Forms.Label label2;


	}
}
