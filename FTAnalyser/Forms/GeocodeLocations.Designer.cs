using System;

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
            try
            {
                if (disposing && (components != null))
                {   
                    components.Dispose();
                }   
                base.Dispose(disposing);

                italicFont.Dispose();
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GeocodeLocations));
            this.dgLocations = new System.Windows.Forms.DataGridView();
            this.LocationIcon = new System.Windows.Forms.DataGridViewImageColumn();
            this.GeocodedLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Latitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Longitude = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GeocodeStatus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FoundLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FoundResultType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuVerified = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIncorrect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuNotSearched = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCopyLocation = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPasteLocation = new System.Windows.Forms.ToolStripMenuItem();
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
            this.googleGeocodeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.geocodeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGoogleGeocodeLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRetryPartial = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOSGeocodeLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReverseGeocode = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.updateChangesWithoutAskingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetAllPartialMatchesToNotSearchedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filtersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGeocodeStatus = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuStatusSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFoundResultType = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectClear = new System.Windows.Forms.ToolStripMenuItem();
            this.reverseGeocodeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.OSGeocodeBackgroundWorker = new System.ComponentModel.BackgroundWorker();
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
            this.dgLocations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLocations.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.LocationIcon,
            this.GeocodedLocation,
            this.Latitude,
            this.Longitude,
            this.GeocodeStatus,
            this.FoundLocation,
            this.FoundResultType});
            this.dgLocations.ContextMenuStrip = this.contextMenuStrip;
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
            this.dgLocations.MultiSelect = false;
            this.dgLocations.Name = "dgLocations";
            this.dgLocations.ReadOnly = true;
            this.dgLocations.RowHeadersVisible = false;
            this.dgLocations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLocations.ShowEditingIcon = false;
            this.dgLocations.Size = new System.Drawing.Size(894, 300);
            this.dgLocations.TabIndex = 5;
            this.dgLocations.CellContextMenuStripNeeded += new System.Windows.Forms.DataGridViewCellContextMenuStripNeededEventHandler(this.DgLocations_CellContextMenuStripNeeded);
            this.dgLocations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgLocations_CellDoubleClick);
            this.dgLocations.CellToolTipTextNeeded += new System.Windows.Forms.DataGridViewCellToolTipTextNeededEventHandler(this.DgLocations_CellToolTipTextNeeded_1);
            // 
            // LocationIcon
            // 
            this.LocationIcon.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.LocationIcon.DataPropertyName = "Icon";
            this.LocationIcon.HeaderText = "";
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
            this.GeocodedLocation.MinimumWidth = 100;
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
            // GeocodeStatus
            // 
            this.GeocodeStatus.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.GeocodeStatus.DataPropertyName = "Geocoded";
            this.GeocodeStatus.HeaderText = "Geocode Status";
            this.GeocodeStatus.MinimumWidth = 115;
            this.GeocodeStatus.Name = "GeocodeStatus";
            this.GeocodeStatus.ReadOnly = true;
            this.GeocodeStatus.Width = 115;
            // 
            // FoundLocation
            // 
            this.FoundLocation.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FoundLocation.DataPropertyName = "FoundLocation";
            this.FoundLocation.HeaderText = "Found Location";
            this.FoundLocation.MinimumWidth = 400;
            this.FoundLocation.Name = "FoundLocation";
            this.FoundLocation.ReadOnly = true;
            this.FoundLocation.Width = 400;
            // 
            // FoundResultType
            // 
            this.FoundResultType.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.FoundResultType.DataPropertyName = "FoundResultType";
            this.FoundResultType.HeaderText = "Found Result Type";
            this.FoundResultType.MinimumWidth = 300;
            this.FoundResultType.Name = "FoundResultType";
            this.FoundResultType.ReadOnly = true;
            this.FoundResultType.Width = 300;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuVerified,
            this.mnuIncorrect,
            this.mnuNotSearched,
            this.toolStripSeparator1,
            this.mnuEditLocation,
            this.mnuCopyLocation,
            this.mnuPasteLocation});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(240, 142);
            // 
            // mnuVerified
            // 
            this.mnuVerified.Image = ((System.Drawing.Image)(resources.GetObject("mnuVerified.Image")));
            this.mnuVerified.Name = "mnuVerified";
            this.mnuVerified.Size = new System.Drawing.Size(239, 22);
            this.mnuVerified.Text = "Mark Location as Verified";
            this.mnuVerified.Click += new System.EventHandler(this.MnuVerified_Click);
            // 
            // mnuIncorrect
            // 
            this.mnuIncorrect.Image = ((System.Drawing.Image)(resources.GetObject("mnuIncorrect.Image")));
            this.mnuIncorrect.Name = "mnuIncorrect";
            this.mnuIncorrect.Size = new System.Drawing.Size(239, 22);
            this.mnuIncorrect.Text = "Mark Location as Incorrect";
            this.mnuIncorrect.Click += new System.EventHandler(this.MnuIncorrect_Click);
            // 
            // mnuNotSearched
            // 
            this.mnuNotSearched.Image = ((System.Drawing.Image)(resources.GetObject("mnuNotSearched.Image")));
            this.mnuNotSearched.Name = "mnuNotSearched";
            this.mnuNotSearched.Size = new System.Drawing.Size(239, 22);
            this.mnuNotSearched.Text = "Reset Location to Not Searched";
            this.mnuNotSearched.Click += new System.EventHandler(this.MnuNotSearched_Click);
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
            this.mnuEditLocation.Click += new System.EventHandler(this.MnuEditLocation_Click);
            // 
            // mnuCopyLocation
            // 
            this.mnuCopyLocation.Name = "mnuCopyLocation";
            this.mnuCopyLocation.Size = new System.Drawing.Size(239, 22);
            this.mnuCopyLocation.Text = "Copy Location";
            this.mnuCopyLocation.Click += new System.EventHandler(this.MnuCopyLocation_Click);
            // 
            // mnuPasteLocation
            // 
            this.mnuPasteLocation.Enabled = false;
            this.mnuPasteLocation.Name = "mnuPasteLocation";
            this.mnuPasteLocation.Size = new System.Drawing.Size(239, 22);
            this.mnuPasteLocation.Text = "Paste Location";
            this.mnuPasteLocation.Click += new System.EventHandler(this.MnuPasteLocation_Click);
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
            this.mnuSaveColumnLayout.Text = "Save Column Sort Order, layout, width etc";
            this.mnuSaveColumnLayout.Click += new System.EventHandler(this.MnuSaveColumnLayout_Click);
            // 
            // mnuExportToExcel
            // 
            this.mnuExportToExcel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuExportToExcel.Image = ((System.Drawing.Image)(resources.GetObject("mnuExportToExcel.Image")));
            this.mnuExportToExcel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuExportToExcel.Name = "mnuExportToExcel";
            this.mnuExportToExcel.Size = new System.Drawing.Size(23, 22);
            this.mnuExportToExcel.Text = "Export to Excel";
            this.mnuExportToExcel.Click += new System.EventHandler(this.MnuExportToExcel_Click);
            // 
            // mnuResetColumns
            // 
            this.mnuResetColumns.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.mnuResetColumns.Image = ((System.Drawing.Image)(resources.GetObject("mnuResetColumns.Image")));
            this.mnuResetColumns.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuResetColumns.Name = "mnuResetColumns";
            this.mnuResetColumns.Size = new System.Drawing.Size(23, 22);
            this.mnuResetColumns.Text = "Reset Column Sort Order to Default";
            this.mnuResetColumns.Click += new System.EventHandler(this.MnuResetColumns_Click);
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
            this.printToolStripButton.Click += new System.EventHandler(this.PrintToolStripButton_Click);
            // 
            // printPreviewToolStripButton
            // 
            this.printPreviewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.printPreviewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("printPreviewToolStripButton.Image")));
            this.printPreviewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.printPreviewToolStripButton.Name = "printPreviewToolStripButton";
            this.printPreviewToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.printPreviewToolStripButton.Text = "Print Preview...";
            this.printPreviewToolStripButton.Click += new System.EventHandler(this.PrintPreviewToolStripButton_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // googleGeocodeBackgroundWorker
            // 
            this.googleGeocodeBackgroundWorker.WorkerReportsProgress = true;
            this.googleGeocodeBackgroundWorker.WorkerSupportsCancellation = true;
            this.googleGeocodeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.GoogleGeocodingBackgroundWorker_DoWork);
            this.googleGeocodeBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.GoogleGeocodingBackgroundWorker_ProgressChanged);
            this.googleGeocodeBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GoogleGeocodingBackgroundWorker_RunWorkerCompleted);
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
            this.mnuGoogleGeocodeLocations,
            this.mnuRetryPartial,
            this.mnuOSGeocodeLocations,
            this.mnuReverseGeocode});
            this.geocodeToolStripMenuItem.Name = "geocodeToolStripMenuItem";
            this.geocodeToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            this.geocodeToolStripMenuItem.Text = "Geocoder";
            // 
            // mnuGoogleGeocodeLocations
            // 
            this.mnuGoogleGeocodeLocations.Name = "mnuGoogleGeocodeLocations";
            this.mnuGoogleGeocodeLocations.Size = new System.Drawing.Size(282, 22);
            this.mnuGoogleGeocodeLocations.Text = "Run Google Geocoder to find Locations";
            this.mnuGoogleGeocodeLocations.ToolTipText = "Looks up map co-ordinates for locations in your file";
            this.mnuGoogleGeocodeLocations.Click += new System.EventHandler(this.MnuGeocodeLocations_Click);
            // 
            // mnuRetryPartial
            // 
            this.mnuRetryPartial.Name = "mnuRetryPartial";
            this.mnuRetryPartial.Size = new System.Drawing.Size(282, 22);
            this.mnuRetryPartial.Text = "Run Google Geocoder retrying Partials";
            this.mnuRetryPartial.Click += new System.EventHandler(this.MnuRetryPartial_Click);
            // 
            // mnuOSGeocodeLocations
            // 
            this.mnuOSGeocodeLocations.Name = "mnuOSGeocodeLocations";
            this.mnuOSGeocodeLocations.Size = new System.Drawing.Size(282, 22);
            this.mnuOSGeocodeLocations.Text = "Run OS Geocoder to find Locations";
            this.mnuOSGeocodeLocations.Click += new System.EventHandler(this.MnuOSGeocodeLocations_Click);
            // 
            // mnuReverseGeocode
            // 
            this.mnuReverseGeocode.Name = "mnuReverseGeocode";
            this.mnuReverseGeocode.Size = new System.Drawing.Size(282, 22);
            this.mnuReverseGeocode.Text = "Lookup Blank Google Locations";
            this.mnuReverseGeocode.Click += new System.EventHandler(this.MnuReverseGeocde_Click);
            // 
            // mnuOptions
            // 
            this.mnuOptions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.updateChangesWithoutAskingToolStripMenuItem,
            this.resetAllPartialMatchesToNotSearchedToolStripMenuItem});
            this.mnuOptions.Name = "mnuOptions";
            this.mnuOptions.Size = new System.Drawing.Size(61, 20);
            this.mnuOptions.Text = "Options";
            // 
            // updateChangesWithoutAskingToolStripMenuItem
            // 
            this.updateChangesWithoutAskingToolStripMenuItem.CheckOnClick = true;
            this.updateChangesWithoutAskingToolStripMenuItem.Name = "updateChangesWithoutAskingToolStripMenuItem";
            this.updateChangesWithoutAskingToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.updateChangesWithoutAskingToolStripMenuItem.Text = "Update Changes without asking";
            this.updateChangesWithoutAskingToolStripMenuItem.ToolTipText = "No longer asks if you want to save changes to locations";
            this.updateChangesWithoutAskingToolStripMenuItem.Click += new System.EventHandler(this.UpdateChangesWithoutAskingToolStripMenuItem_Click);
            // 
            // resetAllPartialMatchesToNotSearchedToolStripMenuItem
            // 
            this.resetAllPartialMatchesToNotSearchedToolStripMenuItem.Name = "resetAllPartialMatchesToNotSearchedToolStripMenuItem";
            this.resetAllPartialMatchesToNotSearchedToolStripMenuItem.Size = new System.Drawing.Size(286, 22);
            this.resetAllPartialMatchesToNotSearchedToolStripMenuItem.Text = "Reset all partial matches to not searched";
            this.resetAllPartialMatchesToNotSearchedToolStripMenuItem.Click += new System.EventHandler(this.ResetAllPartialMatchesToNotSearchedToolStripMenuItem_Click);
            // 
            // filtersToolStripMenuItem
            // 
            this.filtersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGeocodeStatus,
            this.mnuFoundResultType});
            this.filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            this.filtersToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.filtersToolStripMenuItem.Text = "Filters";
            // 
            // mnuGeocodeStatus
            // 
            this.mnuGeocodeStatus.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuStatusSelectAll});
            this.mnuGeocodeStatus.Name = "mnuGeocodeStatus";
            this.mnuGeocodeStatus.Size = new System.Drawing.Size(171, 22);
            this.mnuGeocodeStatus.Text = "Geocode Status";
            // 
            // mnuStatusSelectAll
            // 
            this.mnuStatusSelectAll.Name = "mnuStatusSelectAll";
            this.mnuStatusSelectAll.Size = new System.Drawing.Size(122, 22);
            this.mnuStatusSelectAll.Text = "Select All";
            this.mnuStatusSelectAll.Click += new System.EventHandler(this.MnuStatusSelectAll_Click);
            // 
            // mnuFoundResultType
            // 
            this.mnuFoundResultType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSelectClear});
            this.mnuFoundResultType.Name = "mnuFoundResultType";
            this.mnuFoundResultType.Size = new System.Drawing.Size(171, 22);
            this.mnuFoundResultType.Text = "Found Result Type";
            // 
            // mnuSelectClear
            // 
            this.mnuSelectClear.Name = "mnuSelectClear";
            this.mnuSelectClear.Size = new System.Drawing.Size(122, 22);
            this.mnuSelectClear.Text = "Select All";
            this.mnuSelectClear.Click += new System.EventHandler(this.MnuSelectClear_Click);
            // 
            // reverseGeocodeBackgroundWorker
            // 
            this.reverseGeocodeBackgroundWorker.WorkerReportsProgress = true;
            this.reverseGeocodeBackgroundWorker.WorkerSupportsCancellation = true;
            this.reverseGeocodeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ReverseGeocodeBackgroundWorker_DoWork);
            this.reverseGeocodeBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.GoogleGeocodingBackgroundWorker_ProgressChanged);
            this.reverseGeocodeBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.GoogleGeocodingBackgroundWorker_RunWorkerCompleted);
            // 
            // OSGeocodeBackgroundWorker
            // 
            this.OSGeocodeBackgroundWorker.WorkerReportsProgress = true;
            this.OSGeocodeBackgroundWorker.WorkerSupportsCancellation = true;
            this.OSGeocodeBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.OSGeocodeBackgroundWorker_DoWork);
            this.OSGeocodeBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.OSGeocodeBackgroundWorker_ProgressChanged);
            this.OSGeocodeBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.OSGeocodeBackgroundWorker_RunWorkerCompleted);
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
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.GeocodeLocations_FormClosed);
            this.Load += new System.EventHandler(this.GeocodeLocations_Load);
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
        private System.ComponentModel.BackgroundWorker googleGeocodeBackgroundWorker;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem geocodeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuGoogleGeocodeLocations;
        private System.Windows.Forms.ToolStripMenuItem mnuOptions;
        private System.Windows.Forms.ToolStripMenuItem mnuRetryPartial;
        private System.Windows.Forms.ToolStripMenuItem filtersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuGeocodeStatus;
        private System.Windows.Forms.ToolStripMenuItem mnuFoundResultType;
        private System.Windows.Forms.ToolStripMenuItem updateChangesWithoutAskingToolStripMenuItem;
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
        private System.Windows.Forms.ToolStripMenuItem mnuCopyLocation;
        private System.Windows.Forms.ToolStripMenuItem mnuPasteLocation;
        private System.Windows.Forms.ToolStripMenuItem resetAllPartialMatchesToNotSearchedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOSGeocodeLocations;
        private System.ComponentModel.BackgroundWorker OSGeocodeBackgroundWorker;
        private System.Windows.Forms.DataGridViewImageColumn LocationIcon;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodedLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn Latitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn Longitude;
        private System.Windows.Forms.DataGridViewTextBoxColumn GeocodeStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoundLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn FoundResultType;
    }
}