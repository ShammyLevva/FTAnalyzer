using System;

namespace FTAnalyzer.Forms
{
    partial class Census
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
                reportFormHelper.Dispose();
            }
            catch (Exception) { }
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Census));
            statusStrip = new StatusStrip();
            tsRecords = new ToolStripStatusLabel();
            toolStrip1 = new ToolStrip();
            mnuSaveCensusColumnLayout = new ToolStripButton();
            mnuResetCensusColumns = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            printToolStripButton = new ToolStripButton();
            printPreviewToolStripButton = new ToolStripButton();
            toolStripSeparator = new ToolStripSeparator();
            mnuExportToExcel = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            tsBtnMapLocation = new ToolStripButton();
            tsBtnMapOSLocation = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            toolStripLabel1 = new ToolStripLabel();
            cbCensusSearchProvider = new ToolStripComboBox();
            toolStripLabel2 = new ToolStripLabel();
            cbRegion = new ToolStripComboBox();
            btnHelp = new ToolStripButton();
            contextMenuStrip = new ContextMenuStrip(components);
            mnuViewFacts = new ToolStripMenuItem();
            panelCensus = new Panel();
            dgCensus = new FTAnalyzer.Forms.Controls.VirtualDgvCensus();
            statusStrip.SuspendLayout();
            toolStrip1.SuspendLayout();
            contextMenuStrip.SuspendLayout();
            panelCensus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgCensus).BeginInit();
            SuspendLayout();
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(28, 28);
            statusStrip.Items.AddRange(new ToolStripItem[] { tsRecords });
            statusStrip.Location = new Point(0, 641);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 17, 0);
            statusStrip.Size = new Size(1211, 22);
            statusStrip.TabIndex = 2;
            statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            tsRecords.Name = "tsRecords";
            tsRecords.Size = new Size(118, 17);
            tsRecords.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(28, 28);
            toolStrip1.Items.AddRange(new ToolStripItem[] { mnuSaveCensusColumnLayout, mnuResetCensusColumns, toolStripSeparator2, printToolStripButton, printPreviewToolStripButton, toolStripSeparator, mnuExportToExcel, toolStripSeparator3, tsBtnMapLocation, tsBtnMapOSLocation, toolStripSeparator1, toolStripLabel1, cbCensusSearchProvider, toolStripLabel2, cbRegion, btnHelp });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(0, 0, 3, 0);
            toolStrip1.Size = new Size(1211, 35);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // mnuSaveCensusColumnLayout
            // 
            mnuSaveCensusColumnLayout.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuSaveCensusColumnLayout.Image = (Image)resources.GetObject("mnuSaveCensusColumnLayout.Image");
            mnuSaveCensusColumnLayout.ImageTransparentColor = Color.Magenta;
            mnuSaveCensusColumnLayout.Name = "mnuSaveCensusColumnLayout";
            mnuSaveCensusColumnLayout.Size = new Size(32, 32);
            mnuSaveCensusColumnLayout.Text = "Save Census Column Sort Order";
            mnuSaveCensusColumnLayout.Click += MnuSaveCensusColumnLayout_Click;
            // 
            // mnuResetCensusColumns
            // 
            mnuResetCensusColumns.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuResetCensusColumns.Image = (Image)resources.GetObject("mnuResetCensusColumns.Image");
            mnuResetCensusColumns.ImageTransparentColor = Color.Magenta;
            mnuResetCensusColumns.Name = "mnuResetCensusColumns";
            mnuResetCensusColumns.Size = new Size(32, 32);
            mnuResetCensusColumns.Text = "Reset Census Column Sort Order to Default";
            mnuResetCensusColumns.Click += MnuResetCensusColumns_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(6, 35);
            // 
            // printToolStripButton
            // 
            printToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printToolStripButton.Image = (Image)resources.GetObject("printToolStripButton.Image");
            printToolStripButton.ImageTransparentColor = Color.Magenta;
            printToolStripButton.Name = "printToolStripButton";
            printToolStripButton.Size = new Size(32, 32);
            printToolStripButton.Text = "&Print";
            printToolStripButton.Click += PrintToolStripButton_Click;
            // 
            // printPreviewToolStripButton
            // 
            printPreviewToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            printPreviewToolStripButton.Image = (Image)resources.GetObject("printPreviewToolStripButton.Image");
            printPreviewToolStripButton.ImageTransparentColor = Color.Magenta;
            printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            printPreviewToolStripButton.Size = new Size(32, 32);
            printPreviewToolStripButton.Text = "Print Preview...";
            printPreviewToolStripButton.Click += PrintPreviewToolStripButton_Click;
            // 
            // toolStripSeparator
            // 
            toolStripSeparator.Name = "toolStripSeparator";
            toolStripSeparator.Size = new Size(6, 35);
            // 
            // mnuExportToExcel
            // 
            mnuExportToExcel.DisplayStyle = ToolStripItemDisplayStyle.Image;
            mnuExportToExcel.Image = (Image)resources.GetObject("mnuExportToExcel.Image");
            mnuExportToExcel.ImageTransparentColor = Color.Magenta;
            mnuExportToExcel.Name = "mnuExportToExcel";
            mnuExportToExcel.Size = new Size(32, 32);
            mnuExportToExcel.Text = "Export to Excel";
            mnuExportToExcel.Click += MnuExportToExcel_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(6, 35);
            // 
            // tsBtnMapLocation
            // 
            tsBtnMapLocation.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsBtnMapLocation.Image = (Image)resources.GetObject("tsBtnMapLocation.Image");
            tsBtnMapLocation.ImageTransparentColor = Color.Magenta;
            tsBtnMapLocation.Name = "tsBtnMapLocation";
            tsBtnMapLocation.Size = new Size(174, 32);
            tsBtnMapLocation.Text = "Show Location on Google Map";
            tsBtnMapLocation.Click += TsBtnMapLocation_Click;
            // 
            // tsBtnMapOSLocation
            // 
            tsBtnMapOSLocation.DisplayStyle = ToolStripItemDisplayStyle.Text;
            tsBtnMapOSLocation.Image = (Image)resources.GetObject("tsBtnMapOSLocation.Image");
            tsBtnMapOSLocation.ImageTransparentColor = Color.Magenta;
            tsBtnMapOSLocation.Name = "tsBtnMapOSLocation";
            tsBtnMapOSLocation.Size = new Size(151, 32);
            tsBtnMapOSLocation.Text = "Show Location on OS Map";
            tsBtnMapOSLocation.Click += TsBtnMapOSLocation_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 35);
            // 
            // toolStripLabel1
            // 
            toolStripLabel1.Name = "toolStripLabel1";
            toolStripLabel1.Size = new Size(117, 32);
            toolStripLabel1.Text = "Census search using:";
            // 
            // cbCensusSearchProvider
            // 
            cbCensusSearchProvider.DropDownStyle = ComboBoxStyle.DropDownList;
            cbCensusSearchProvider.DropDownWidth = 200;
            cbCensusSearchProvider.Items.AddRange(new object[] { "Ancestry", "Find My Past", "FreeCen", "FamilySearch", "Scotlands People" });
            cbCensusSearchProvider.Name = "cbCensusSearchProvider";
            cbCensusSearchProvider.Size = new Size(129, 35);
            cbCensusSearchProvider.SelectedIndexChanged += CbCensusSearchProvider_SelectedIndexChanged;
            // 
            // toolStripLabel2
            // 
            toolStripLabel2.Name = "toolStripLabel2";
            toolStripLabel2.Size = new Size(47, 32);
            toolStripLabel2.Text = "Region:";
            // 
            // cbRegion
            // 
            cbRegion.AutoCompleteCustomSource.AddRange(new string[] { ".com", ".co.uk", ".ca", ".com.au" });
            cbRegion.Items.AddRange(new object[] { ".com", ".co.uk", ".ca", ".com.au" });
            cbRegion.Name = "cbRegion";
            cbRegion.Size = new Size(78, 35);
            cbRegion.Text = ".co.uk";
            cbRegion.SelectedIndexChanged += CbRegion_SelectedIndexChanged;
            // 
            // btnHelp
            // 
            btnHelp.DisplayStyle = ToolStripItemDisplayStyle.Image;
            btnHelp.Image = (Image)resources.GetObject("btnHelp.Image");
            btnHelp.ImageTransparentColor = Color.Magenta;
            btnHelp.Name = "btnHelp";
            btnHelp.Size = new Size(32, 32);
            btnHelp.Text = "toolStripButton1";
            btnHelp.Click += BtnHelp_Click;
            // 
            // contextMenuStrip
            // 
            contextMenuStrip.ImageScalingSize = new Size(28, 28);
            contextMenuStrip.Items.AddRange(new ToolStripItem[] { mnuViewFacts });
            contextMenuStrip.Name = "contextMenuStrip";
            contextMenuStrip.Size = new Size(190, 26);
            // 
            // mnuViewFacts
            // 
            mnuViewFacts.Name = "mnuViewFacts";
            mnuViewFacts.Size = new Size(189, 22);
            mnuViewFacts.Text = "View Individuals Facts";
            mnuViewFacts.Click += MnuViewFacts_Click;
            // 
            // panelCensus
            // 
            panelCensus.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelCensus.Controls.Add(dgCensus);
            panelCensus.Location = new Point(0, 35);
            panelCensus.Margin = new Padding(0);
            panelCensus.Name = "panelCensus";
            panelCensus.Size = new Size(1211, 606);
            panelCensus.TabIndex = 1;
            // 
            // dgCensus
            // 
            dgCensus.AllowUserToAddRows = false;
            dgCensus.AllowUserToDeleteRows = false;
            dgCensus.AllowUserToOrderColumns = true;
            dgCensus.Dock = DockStyle.Fill;
            dgCensus.Location = new Point(0, 0);
            dgCensus.Margin = new Padding(0);
            dgCensus.Name = "dgCensus";
            dgCensus.Size = new Size(1211, 606);
            dgCensus.TabIndex = 0;
            dgCensus.CellContextMenuStripNeeded += DgCensus_CellContextMenuStripNeeded;
            dgCensus.CellDoubleClick += DgCensus_CellDoubleClick;
            dgCensus.CellFormatting += DgCensus_CellFormatting;
            dgCensus.Sorted += DgCensus_Sorted;
            // 
            // Census
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1211, 663);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip);
            Controls.Add(panelCensus);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "Census";
            Text = "Census Records to search for";
            FormClosed += Census_FormClosed;
            Load += Census_Load;
            TextChanged += Census_TextChanged;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            contextMenuStrip.ResumeLayout(false);
            panelCensus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgCensus).EndInit();
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripButton tsBtnMapLocation;
        private System.Windows.Forms.ToolStripButton tsBtnMapOSLocation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbCensusSearchProvider;
        private System.Windows.Forms.ToolStripButton mnuSaveCensusColumnLayout;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton mnuResetCensusColumns;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuViewFacts;
        private System.Windows.Forms.ToolStripButton btnHelp;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox cbRegion;
        private System.Windows.Forms.Panel panelCensus;
        private FTAnalyzer.Forms.Controls.VirtualDgvCensus dgCensus;
    }
}