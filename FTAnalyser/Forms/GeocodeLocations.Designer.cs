namespace FTAnalyzer.Forms
{
    partial class GeocodeLocations
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeocodeLocations));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgLocations = new System.Windows.Forms.DataGridView();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.txtLocations = new System.Windows.Forms.ToolStripStatusLabel();
            this.pbGeocoding = new System.Windows.Forms.ToolStripProgressBar();
            this.txtGoogleWait = new System.Windows.Forms.ToolStripStatusLabel();
            this.mnuSaveColumnLayout = new System.Windows.Forms.ToolStripButton();
            this.mnuExportToExcel = new System.Windows.Forms.ToolStripButton();
            this.mnuResetColumns = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.printToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.printPreviewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.geocodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeocodeLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRetryPartial = new System.Windows.Forms.ToolStripMenuItem();
            this.GeocodedLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LocationIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.GeocodeStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleResultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgLocations)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgLocations
            // 
            this.dgLocations.AllowUserToAddRows = false;
            this.dgLocations.AllowUserToDeleteRows = false;
            this.dgLocations.AllowUserToOrderColumns = true;
            this.dgLocations.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgLocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLocations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.GeocodedLocation,
            this.Latitude,
            this.Longitude,
            this.LocationIcon,
            this.GeocodeStatus,
            this.GoogleLocation,
            this.GoogleResultType});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLocations.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLocations.Location = new System.Drawing.Point(0, 49);
            this.dgLocations.Name = "dgLocations";
            this.dgLocations.ReadOnly = true;
            this.dgLocations.RowHeadersVisible = false;
            this.dgLocations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLocations.ShowEditingIcon = false;
            this.dgLocations.Size = new System.Drawing.Size(894, 300);
            this.dgLocations.TabIndex = 5;
            this.dgLocations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLocations_CellDoubleClick);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.txtLocations,
            this.pbGeocoding,
            this.txtGoogleWait});
            this.statusStrip.Location = new System.Drawing.Point(0, 349);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(894, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // txtLocations
            // 
            this.txtLocations.Name = "txtLocations";
            this.txtLocations.Size = new System.Drawing.Size(0, 17);
            // 
            // pbGeocoding
            // 
            this.pbGeocoding.Name = "pbGeocoding";
            this.pbGeocoding.Size = new System.Drawing.Size(100, 16);
            this.pbGeocoding.Visible = false;
            // 
            // txtGoogleWait
            // 
            this.txtGoogleWait.Name = "txtGoogleWait";
            this.txtGoogleWait.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.txtGoogleWait.Size = new System.Drawing.Size(0, 17);
            // 
            // mnuSaveColumnLayout
            // 
            this.mnuSaveColumnLayout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuSaveColumnLayout.Image = ((System.Drawing.Image)(resources.GetObject("mnuSaveColumnLayout.Image")));
            this.mnuSaveColumnLayout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuSaveColumnLayout.Name = "mnuSaveColumnLayout";
            this.mnuSaveColumnLayout.Size = new System.Drawing.Size(23, 22);
            this.mnuSaveColumnLayout.Text = "Save Column Sort Order";
            this.mnuSaveColumnLayout.Click += new System.EventHandler(this.mnuSaveColumnLayout_Click);
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportToExcel.Image")));
            this.mnuExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(23, 22);
            this.mnuExportToExcel.Text = "Export to Excel";
            this.mnuExportToExcel.Click += new System.EventHandler(this.mnuExportToExcel_Click);
            // 
            // mnuResetColumns
            // 
            this.mnuResetColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuResetColumns.Image = ((System.Drawing.Image)(resources.GetObject("mnuResetColumns.Image")));
            this.mnuResetColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuResetColumns.Name = "mnuResetColumns";
            this.mnuResetColumns.Size = new System.Drawing.Size(23, 22);
            this.mnuResetColumns.Text = "Reset Column Sort Order to Default";
            this.mnuResetColumns.Click += new System.EventHandler(this.mnuResetColumns_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSaveColumnLayout,
            this.mnuResetColumns,
            this.toolStripSeparator2,
            this.printToolStripButton,
            this.printPreviewToolStripButton,
            this.toolStripSeparator,
            this.mnuExportToExcel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(894, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
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
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printPreviewToolStripButton.Text = "Print Preview...";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.printPreviewToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker_ProgressChanged);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geocodeToolStripMenuItem,
            this.mnuOptions});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(894, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // geocodeToolStripMenuItem
            // 
            this.geocodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGeocodeLocations});
            this.geocodeToolStripMenuItem.Name = "geocodeToolStripMenuItem";
            this.geocodeToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.geocodeToolStripMenuItem.Text = "Process";
            // 
            // mnuGeocodeLocations
            // 
            this.mnuGeocodeLocations.Name = "mnuGeocodeLocations";
            this.mnuGeocodeLocations.Size = new System.Drawing.Size(175, 22);
            this.mnuGeocodeLocations.Text = "Geocode Locations";
            this.mnuGeocodeLocations.ToolTipText = "Looks up map co-ordinates for locations in your file";
            this.mnuGeocodeLocations.Click += new System.EventHandler(this.mnuGeocodeLocations_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRetryPartial});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // mnuRetryPartial
            // 
            this.mnuRetryPartial.CheckOnClick = true;
            this.mnuRetryPartial.Name = "mnuRetryPartial";
            this.mnuRetryPartial.Size = new System.Drawing.Size(203, 22);
            this.mnuRetryPartial.Text = "Retry Partially Geocoded";
            // 
            // GeocodedLocation
            // 
            this.GeocodedLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GeocodedLocation.DataPropertyName = "SortableLocation";
            this.GeocodedLocation.HeaderText = "Location";
            this.GeocodedLocation.MinimumWidth = 450;
            this.GeocodedLocation.Name = "GeocodedLocation";
            this.GeocodedLocation.ReadOnly = true;
            this.GeocodedLocation.Width = 450;
            // 
            // Latitude
            // 
            this.Latitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Latitude.DataPropertyName = "Latitude";
            dataGridViewCellStyle1.Format = "N7";
            this.Latitude.DefaultCellStyle = dataGridViewCellStyle1;
            this.Latitude.HeaderText = "Latitude";
            this.Latitude.MinimumWidth = 75;
            this.Latitude.Name = "Latitude";
            this.Latitude.ReadOnly = true;
            this.Latitude.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Latitude.Width = 75;
            // 
            // Longitude
            // 
            this.Longitude.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.Longitude.DataPropertyName = "Longitude";
            dataGridViewCellStyle2.Format = "N7";
            this.Longitude.DefaultCellStyle = dataGridViewCellStyle2;
            this.Longitude.HeaderText = "Longitude";
            this.Longitude.MinimumWidth = 75;
            this.Longitude.Name = "Longitude";
            this.Longitude.ReadOnly = true;
            this.Longitude.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Longitude.Width = 75;
            // 
            // LocationIcon
            // 
            this.LocationIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LocationIcon.DataPropertyName = "Icon";
            this.LocationIcon.HeaderText = global::FTAnalyzer.Properties.Resources.FTA_0002;
            this.LocationIcon.MinimumWidth = 20;
            this.LocationIcon.Name = "LocationIcon";
            this.LocationIcon.ReadOnly = true;
            this.LocationIcon.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.LocationIcon.Width = 20;
            // 
            // GeocodeStatus
            // 
            this.GeocodeStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GeocodeStatus.DataPropertyName = "Geocoded";
            this.GeocodeStatus.HeaderText = "Geocode Status";
            this.GeocodeStatus.MinimumWidth = 110;
            this.GeocodeStatus.Name = "GeocodeStatus";
            this.GeocodeStatus.ReadOnly = true;
            this.GeocodeStatus.Width = 110;
            // 
            // GoogleLocation
            // 
            this.GoogleLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GoogleLocation.DataPropertyName = "GoogleLocation";
            this.GoogleLocation.HeaderText = "Google Location";
            this.GoogleLocation.MinimumWidth = 400;
            this.GoogleLocation.Name = "GoogleLocation";
            this.GoogleLocation.ReadOnly = true;
            this.GoogleLocation.Width = 400;
            // 
            // GoogleResultType
            // 
            this.GoogleResultType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GoogleResultType.DataPropertyName = "GoogleResultType";
            this.GoogleResultType.HeaderText = "Google Result Type";
            this.GoogleResultType.MinimumWidth = 300;
            this.GoogleResultType.Name = "GoogleResultType";
            this.GoogleResultType.ReadOnly = true;
            this.GoogleResultType.Width = 300;
            // 
            // GeocodeLocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 371);
            this.Controls.Add(this.dgLocations);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.statusStrip);
            this.Name = "GeocodeLocations";
            this.Text = "Geocode Locations";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeocodeLocations_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgLocations)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgLocations;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel txtLocations;
        private System.Windows.Forms.ToolStripButton mnuSaveColumnLayout;
        private System.Windows.Forms.ToolStripButton mnuExportToExcel;
        private System.Windows.Forms.ToolStripButton mnuResetColumns;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton printToolStripButton;
        private System.Windows.Forms.ToolStripButton printPreviewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripProgressBar pbGeocoding;
        private System.Windows.Forms.ToolStripStatusLabel txtGoogleWait;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem geocodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuGeocodeLocations;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuRetryPartial;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodedLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
        private System.Windows.Forms.DataGridViewImageColumn LocationIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodeStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleResultType;
    }
}