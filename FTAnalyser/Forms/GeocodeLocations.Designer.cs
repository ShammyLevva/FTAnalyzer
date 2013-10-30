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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeocodeLocations));
            this.dgLocations = new System.Windows.Forms.DataGridView();
            this.LocationIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.GeocodedLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeocodeStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GoogleResultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuVerified = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIncorrect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNotSearched = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditLocation = new System.Windows.Forms.ToolStripMenuItem();
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
            this.geocodeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.geocodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeocodeLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReverseGeocode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRetryPartial = new System.Windows.Forms.ToolStripMenuItem();
            this.updateChangesWithoutAskingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeocodeStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStatusSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGoogleResultType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectClear = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseGeocodeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            ((System.ComponentModel.ISupportInitialize)(this.dgLocations)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
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
            this.LocationIcon,
            this.GeocodedLocation,
            this.Latitude,
            this.Longitude,
            this.GeocodeStatus,
            this.GoogleLocation,
            this.GoogleResultType});
            this.dgLocations.ContextMenuStrip = this.contextMenuStrip;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgLocations.DefaultCellStyle = dataGridViewCellStyle6;
            this.dgLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLocations.Location = new System.Drawing.Point(0, 49);
            this.dgLocations.MultiSelect = false;
            this.dgLocations.Name = "dgLocations";
            this.dgLocations.ReadOnly = true;
            this.dgLocations.RowHeadersVisible = false;
            this.dgLocations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLocations.ShowEditingIcon = false;
            this.dgLocations.Size = new System.Drawing.Size(894, 300);
            this.dgLocations.TabIndex = 5;
            this.dgLocations.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.dgLocations_CellContextMenuStripNeeded);
            this.dgLocations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLocations_CellDoubleClick);
            this.dgLocations.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.dgLocations_CellToolTipTextNeeded_1);
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
            dataGridViewCellStyle4.Format = "N7";
            this.Latitude.DefaultCellStyle = dataGridViewCellStyle4;
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
            dataGridViewCellStyle5.Format = "N7";
            this.Longitude.DefaultCellStyle = dataGridViewCellStyle5;
            this.Longitude.HeaderText = "Longitude";
            this.Longitude.MinimumWidth = 75;
            this.Longitude.Name = "Longitude";
            this.Longitude.ReadOnly = true;
            this.Longitude.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.Longitude.Width = 75;
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
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVerified,
            this.mnuIncorrect,
            this.mnuNotSearched,
            this.toolStripSeparator1,
            this.mnuEditLocation});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(240, 98);
            // 
            // mnuVerified
            // 
            this.mnuVerified.Image = ((System.Drawing.Image)(resources.GetObject("mnuVerified.Image")));
            this.mnuVerified.Name = "mnuVerified";
            this.mnuVerified.Size = new System.Drawing.Size(239, 22);
            this.mnuVerified.Text = "Mark Location as Verified";
            this.mnuVerified.Click += new System.EventHandler(this.mnuVerified_Click);
            // 
            // mnuIncorrect
            // 
            this.mnuIncorrect.Image = ((System.Drawing.Image)(resources.GetObject("mnuIncorrect.Image")));
            this.mnuIncorrect.Name = "mnuIncorrect";
            this.mnuIncorrect.Size = new System.Drawing.Size(239, 22);
            this.mnuIncorrect.Text = "Mark Location as Incorrect";
            this.mnuIncorrect.Click += new System.EventHandler(this.mnuIncorrect_Click);
            // 
            // mnuNotSearched
            // 
            this.mnuNotSearched.Image = ((System.Drawing.Image)(resources.GetObject("mnuNotSearched.Image")));
            this.mnuNotSearched.Name = "mnuNotSearched";
            this.mnuNotSearched.Size = new System.Drawing.Size(239, 22);
            this.mnuNotSearched.Text = "Reset Location to Not Searched";
            this.mnuNotSearched.Click += new System.EventHandler(this.mnuNotSearched_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
            // mnuEditLocation
            // 
            this.mnuEditLocation.Name = "mnuEditLocation";
            this.mnuEditLocation.Size = new System.Drawing.Size(239, 22);
            this.mnuEditLocation.Text = "Edit Location";
            this.mnuEditLocation.Click += new System.EventHandler(this.mnuEditLocation_Click);
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
            // geocodeBackgroundWorker
            // 
            this.geocodeBackgroundWorker.WorkerReportsProgress = true;
            this.geocodeBackgroundWorker.WorkerSupportsCancellation = true;
            this.geocodeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.geocodingBackgroundWorker_DoWork);
            this.geocodeBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.geocodingBackgroundWorker_ProgressChanged);
            this.geocodeBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.geocodingBackgroundWorker_RunWorkerCompleted);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geocodeToolStripMenuItem,
            this.mnuOptions,
            this.filtersToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(894, 24);
            this.menuStrip1.TabIndex = 7;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // geocodeToolStripMenuItem
            // 
            this.geocodeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGeocodeLocations,
            this.mnuReverseGeocode});
            this.geocodeToolStripMenuItem.Name = "geocodeToolStripMenuItem";
            this.geocodeToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.geocodeToolStripMenuItem.Text = "Geocoder";
            // 
            // mnuGeocodeLocations
            // 
            this.mnuGeocodeLocations.Name = "mnuGeocodeLocations";
            this.mnuGeocodeLocations.Size = new System.Drawing.Size(281, 22);
            this.mnuGeocodeLocations.Text = "Run Geocoder to find Locations";
            this.mnuGeocodeLocations.ToolTipText = "Looks up map co-ordinates for locations in your file";
            this.mnuGeocodeLocations.Click += new System.EventHandler(this.mnuGeocodeLocations_Click);
            // 
            // mnuReverseGeocode
            // 
            this.mnuReverseGeocode.Name = "mnuReverseGeocode";
            this.mnuReverseGeocode.Size = new System.Drawing.Size(281, 22);
            this.mnuReverseGeocode.Text = "Lookup Locations for User entered data";
            this.mnuReverseGeocode.Visible = false;
            this.mnuReverseGeocode.Click += new System.EventHandler(this.mnuReverseGeocde_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRetryPartial,
            this.updateChangesWithoutAskingToolStripMenuItem});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // mnuRetryPartial
            // 
            this.mnuRetryPartial.CheckOnClick = true;
            this.mnuRetryPartial.Name = "mnuRetryPartial";
            this.mnuRetryPartial.Size = new System.Drawing.Size(306, 22);
            this.mnuRetryPartial.Text = "Retry Partially Geocoded on new Geocoding";
            // 
            // updateChangesWithoutAskingToolStripMenuItem
            // 
            this.updateChangesWithoutAskingToolStripMenuItem.CheckOnClick = true;
            this.updateChangesWithoutAskingToolStripMenuItem.Name = "updateChangesWithoutAskingToolStripMenuItem";
            this.updateChangesWithoutAskingToolStripMenuItem.Size = new System.Drawing.Size(306, 22);
            this.updateChangesWithoutAskingToolStripMenuItem.Text = "Update Changes without asking";
            this.updateChangesWithoutAskingToolStripMenuItem.ToolTipText = "No longer asks if you want to save changes to locations";
            this.updateChangesWithoutAskingToolStripMenuItem.Click += new System.EventHandler(this.updateChangesWithoutAskingToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGeocodeStatus,
            this.mnuGoogleResultType});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // mnuGeocodeStatus
            // 
            this.mnuGeocodeStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStatusSelectAll});
            this.mnuGeocodeStatus.Name = "mnuGeocodeStatus";
            this.mnuGeocodeStatus.Size = new System.Drawing.Size(176, 22);
            this.mnuGeocodeStatus.Text = "Geocode Status";
            // 
            // mnuStatusSelectAll
            // 
            this.mnuStatusSelectAll.Name = "mnuStatusSelectAll";
            this.mnuStatusSelectAll.Size = new System.Drawing.Size(122, 22);
            this.mnuStatusSelectAll.Text = "Select All";
            this.mnuStatusSelectAll.Click += new System.EventHandler(this.mnuStatusSelectAll_Click);
            // 
            // mnuGoogleResultType
            // 
            this.mnuGoogleResultType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelectClear});
            this.mnuGoogleResultType.Name = "mnuGoogleResultType";
            this.mnuGoogleResultType.Size = new System.Drawing.Size(176, 22);
            this.mnuGoogleResultType.Text = "Google Result Type";
            // 
            // mnuSelectClear
            // 
            this.mnuSelectClear.Name = "mnuSelectClear";
            this.mnuSelectClear.Size = new System.Drawing.Size(122, 22);
            this.mnuSelectClear.Text = "Select All";
            this.mnuSelectClear.Click += new System.EventHandler(this.mnuSelectClear_Click);
            // 
            // reverseGeocodeBackgroundWorker
            // 
            this.reverseGeocodeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.reverseGeocodeBackgroundWorker_DoWork);
            this.reverseGeocodeBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.geocodingBackgroundWorker_ProgressChanged);
            this.reverseGeocodeBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.geocodingBackgroundWorker_RunWorkerCompleted);
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
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "GeocodeLocations";
            this.Text = "Locations Geocoding  Status Report";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GeocodeLocations_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.dgLocations)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
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
        private System.ComponentModel.BackgroundWorker geocodeBackgroundWorker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem geocodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuGeocodeLocations;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuRetryPartial;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuGeocodeStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuGoogleResultType;
        private System.Windows.Forms.ToolStripMenuItem updateChangesWithoutAskingToolStripMenuItem;
        private System.Windows.Forms.DataGridViewImageColumn LocationIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodedLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodeStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn GoogleResultType;
        private System.Windows.Forms.ToolStripMenuItem mnuSelectClear;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem mnuVerified;
        private System.Windows.Forms.ToolStripMenuItem mnuIncorrect;
        private System.Windows.Forms.ToolStripMenuItem mnuNotSearched;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mnuEditLocation;
        private System.Windows.Forms.ToolStripMenuItem mnuStatusSelectAll;
        private System.Windows.Forms.ToolStripMenuItem mnuReverseGeocode;
        private System.ComponentModel.BackgroundWorker reverseGeocodeBackgroundWorker;
    }
}