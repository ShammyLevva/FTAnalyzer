﻿namespace FTAnalyzer.Forms
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
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Census));
            this.dgCensus = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsRecords = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.tsBtnMapLocation = new System.Windows.Forms.ToolStripButton();
            this.tsBtnMapOSLocation = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.cbCensusSearchProvider = new System.Windows.Forms.ToolStripComboBox();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgCensus)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgCensus
            // 
            this.dgCensus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCensus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCensus.Location = new System.Drawing.Point(0, 28);
            this.dgCensus.Name = "dgCensus";
            this.dgCensus.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCensus.Size = new System.Drawing.Size(1038, 530);
            this.dgCensus.TabIndex = 1;
            this.dgCensus.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCensus_CellDoubleClick);
            this.dgCensus.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgCensus_CellFormatting);
            this.dgCensus.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgCensus_ColumnHeaderMouseClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRecords});
            this.statusStrip.Location = new System.Drawing.Point(0, 561);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1038, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsRecords
            // 
            this.tsRecords.Name = "tsRecords";
            this.tsRecords.Size = new System.Drawing.Size(118, 17);
            this.tsRecords.Text = "toolStripStatusLabel1";
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator,
            this.tsBtnMapLocation,
            this.tsBtnMapOSLocation,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.cbCensusSearchProvider});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1038, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // printToolStripButton
            // 
            this.printToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printToolStripButton.Image")));
            this.printToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printToolStripButton.Name = "printToolStripButton";
            this.printToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printToolStripButton.Text = "&Print";
            this.printToolStripButton.Click += new System.EventHandler(this.printToolStripButton_Click);
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(89, 22);
            this.printPreviewToolStripButton.Text = "Print Preview...";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.printPreviewToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // tsBtnMapLocation
            // 
            this.tsBtnMapLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnMapLocation.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnMapLocation.Image")));
            this.tsBtnMapLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnMapLocation.Name = "tsBtnMapLocation";
            this.tsBtnMapLocation.Size = new System.Drawing.Size(133, 22);
            this.tsBtnMapLocation.Text = "Show Location on Map";
            this.tsBtnMapLocation.Click += new System.EventHandler(this.tsBtnMapLocation_Click);
            // 
            // tsBtnMapOSLocation
            // 
            this.tsBtnMapOSLocation.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsBtnMapOSLocation.Image = ((System.Drawing.Image)(resources.GetObject("tsBtnMapOSLocation.Image")));
            this.tsBtnMapOSLocation.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsBtnMapOSLocation.Name = "tsBtnMapOSLocation";
            this.tsBtnMapOSLocation.Size = new System.Drawing.Size(151, 22);
            this.tsBtnMapOSLocation.Text = "Show Location on OS Map";
            this.tsBtnMapOSLocation.Click += new System.EventHandler(this.tsBtnMapOSLocation_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(117, 22);
            this.toolStripLabel1.Text = "Census search using:";
            // 
            // cbCensusSearchProvider
            // 
            this.cbCensusSearchProvider.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCensusSearchProvider.Items.AddRange(new object[] {
            "Ancestry",
            "Find My Past",
            "FreeCen",
            "FamilySearch"});
            this.cbCensusSearchProvider.Name = "cbCensusSearchProvider";
            this.cbCensusSearchProvider.Size = new System.Drawing.Size(121, 25);
            this.cbCensusSearchProvider.SelectedIndexChanged += new System.EventHandler(this.cbCensusSearchProvider_SelectedIndexChanged);
            // 
            // printDialog
            // 
            this.printDialog.Document = this.printDocument;
            this.printDialog.UseEXDialog = true;
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Document = this.printDocument;
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // Census
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1038, 583);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.dgCensus);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Census";
            this.Text = "Census Records to search for";
            this.TextChanged += new System.EventHandler(this.Census_TextChanged);
            ((System.ComponentModel.ISupportInitialize)(this.dgCensus)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgCensus;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsRecords;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripButton tsBtnMapLocation;
        private System.Windows.Forms.ToolStripButton tsBtnMapOSLocation;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox cbCensusSearchProvider;
    }
}