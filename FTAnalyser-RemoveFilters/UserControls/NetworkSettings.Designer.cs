namespace FTAnalyzer.UserControls
{
	partial class NetworkSettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NetworkSettings));
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.MaxRequests = new System.Windows.Forms.NumericUpDown();
			this.extendedToolTipLabel2 = new UserControls.ExtendedToolTipLabel();
			this.UserAgentDropDown = new System.Windows.Forms.ComboBox();
            this.extendedToolTipLabel1 = new UserControls.ExtendedToolTipLabel();
			this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MaxRequests)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.MaxRequests);
			this.groupBox1.Controls.Add(this.extendedToolTipLabel2);
			this.groupBox1.Controls.Add(this.UserAgentDropDown);
			this.groupBox1.Controls.Add(this.extendedToolTipLabel1);
			resources.ApplyResources(this.groupBox1, "groupBox1");
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.TabStop = false;
			// 
			// MaxRequests
			// 
			resources.ApplyResources(this.MaxRequests, "MaxRequests");
			this.MaxRequests.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
			this.MaxRequests.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
			this.MaxRequests.Name = "MaxRequests";
			this.MaxRequests.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
			// 
			// extendedToolTipLabel2
			// 
			resources.ApplyResources(this.extendedToolTipLabel2, "extendedToolTipLabel2");
			this.extendedToolTipLabel2.Name = "extendedToolTipLabel2";

			// 
			// UserAgentDropDown
			// 
			this.UserAgentDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.UserAgentDropDown.FormattingEnabled = true;
			this.UserAgentDropDown.Items.AddRange(new object[] {
            resources.GetString("UserAgentDropDown.Items"),
            resources.GetString("UserAgentDropDown.Items1"),
            resources.GetString("UserAgentDropDown.Items2")});
			resources.ApplyResources(this.UserAgentDropDown, "UserAgentDropDown");
			this.UserAgentDropDown.Name = "UserAgentDropDown";
			// 
			// extendedToolTipLabel1
			// 
			resources.ApplyResources(this.extendedToolTipLabel1, "extendedToolTipLabel1");
			this.extendedToolTipLabel1.Name = "extendedToolTipLabel1";
			this.extendedToolTipLabel1.ToolTipText = "Sets the maximum number of requests that Rawr will send at one time.";
			// 
			// errorProvider1
			// 
			this.errorProvider1.ContainerControl = this;
			// 
			// NetworkSettings
			// 
			resources.ApplyResources(this, "$this");
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Name = "NetworkSettings";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.MaxRequests)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

        private UserControls.ExtendedToolTipLabel extendedToolTipLabel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ComboBox UserAgentDropDown;
		private System.Windows.Forms.ErrorProvider errorProvider1;
        private UserControls.ExtendedToolTipLabel extendedToolTipLabel2;
		private System.Windows.Forms.NumericUpDown MaxRequests;


	}
}
