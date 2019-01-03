namespace FTAnalyzer.UserControls
{
	partial class FileHandlingUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileHandlingUI));
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.chkRetryFailedLines = new System.Windows.Forms.CheckBox();
            this.chkConvertDiacritics = new System.Windows.Forms.CheckBox();
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
            // chkRetryFailedLines
            // 
            resources.ApplyResources(this.chkRetryFailedLines, "chkRetryFailedLines");
            this.chkRetryFailedLines.Name = "chkRetryFailedLines";
            this.chkRetryFailedLines.UseVisualStyleBackColor = true;
            this.chkRetryFailedLines.CheckedChanged += new System.EventHandler(this.ChkRetryFailedLines_CheckedChanged);
            // 
            // chkConvertDiacritics
            // 
            resources.ApplyResources(this.chkConvertDiacritics, "chkConvertDiacritics");
            this.chkConvertDiacritics.Checked = true;
            this.chkConvertDiacritics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConvertDiacritics.Name = "chkConvertDiacritics";
            this.chkConvertDiacritics.UseVisualStyleBackColor = true;
            this.chkConvertDiacritics.CheckedChanged += new System.EventHandler(this.ChkConvertdiacritics_CheckedChanged);
            // 
            // FileHandlingUI
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkConvertDiacritics);
            this.Controls.Add(this.chkRetryFailedLines);
            this.Controls.Add(this.label1);
            this.Name = "FileHandlingUI";
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkRetryFailedLines;
        private System.Windows.Forms.CheckBox chkConvertDiacritics;
    }
}
