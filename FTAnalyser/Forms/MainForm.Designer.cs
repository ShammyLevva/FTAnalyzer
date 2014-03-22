namespace FTAnalyzer
{
    partial class MainForm
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
            normalFont.Dispose();
            boldFont.Dispose();
            rfhDuplicates.Dispose();
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.openGedcom = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReload = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuRecent = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecent1 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecent2 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecent3 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecent4 = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRecent5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.clearRecentFileListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.databaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuRestore = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buildLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChildAgeProfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOlderParents = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIndividualsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFamiliesToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFactsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.displayOptionsOnLoadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToDefaultFormSizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuMaps = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowTimeline = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLifelines = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPlaces = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGeocodeLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLocationsGeocodeReport = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOnlineManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportAnIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.whatsNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsHintsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.dgDataErrors = new System.Windows.Forms.DataGridView();
            this.dgRegions = new System.Windows.Forms.DataGridView();
            this.dgCountries = new System.Windows.Forms.DataGridView();
            this.tbDuplicateScore = new System.Windows.Forms.TrackBar();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.tabWorldWars = new System.Windows.Forms.TabPage();
            this.ckbWDIgnoreLocations = new System.Windows.Forms.CheckBox();
            this.btnWWII = new System.Windows.Forms.Button();
            this.btnWWI = new System.Windows.Forms.Button();
            this.dgWorldWars = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWorldWarsSurname = new System.Windows.Forms.TextBox();
            this.tabTreetops = new System.Windows.Forms.TabPage();
            this.dgTreeTops = new System.Windows.Forms.DataGridView();
            this.ckbTTIgnoreLocations = new System.Windows.Forms.CheckBox();
            this.btnTreeTops = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTreetopsSurname = new System.Windows.Forms.TextBox();
            this.tabColourReports = new System.Windows.Forms.TabPage();
            this.btnColourBMD = new System.Windows.Forms.Button();
            this.btnColourCensus = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtColouredSurname = new System.Windows.Forms.TextBox();
            this.tabLostCousins = new System.Windows.Forms.TabPage();
            this.btnLCnoCensus = new System.Windows.Forms.Button();
            this.btnLCDuplicates = new System.Windows.Forms.Button();
            this.btnLCMissingCountry = new System.Windows.Forms.Button();
            this.btnLC1940USA = new System.Windows.Forms.Button();
            this.rtbLostCousins = new System.Windows.Forms.RichTextBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.btnLC1911EW = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ckbShowLCEntered = new System.Windows.Forms.CheckBox();
            this.btnLC1841EW = new System.Windows.Forms.Button();
            this.btnLC1911Ireland = new System.Windows.Forms.Button();
            this.btnLC1880USA = new System.Windows.Forms.Button();
            this.btnLC1881EW = new System.Windows.Forms.Button();
            this.btnLC1881Canada = new System.Windows.Forms.Button();
            this.btnLC1881Scot = new System.Windows.Forms.Button();
            this.tabCensus = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnShowCensusEntered = new System.Windows.Forms.Button();
            this.btnShowCensusMissing = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDuplicateCensus = new System.Windows.Forms.Button();
            this.btnMissingCensusLocation = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.udAgeFilter = new System.Windows.Forms.NumericUpDown();
            this.tabLooseBirthDeaths = new System.Windows.Forms.TabPage();
            this.tabCtrlLooseBDs = new System.Windows.Forms.TabControl();
            this.tabLooseBirths = new System.Windows.Forms.TabPage();
            this.dgLooseBirths = new System.Windows.Forms.DataGridView();
            this.tabLooseDeaths = new System.Windows.Forms.TabPage();
            this.dgLooseDeaths = new System.Windows.Forms.DataGridView();
            this.tabDataErrors = new System.Windows.Forms.TabPage();
            this.gbDataErrorTypes = new System.Windows.Forms.GroupBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.ckbDataErrors = new System.Windows.Forms.CheckedListBox();
            this.tabOccupations = new System.Windows.Forms.TabPage();
            this.dgOccupations = new System.Windows.Forms.DataGridView();
            this.tabLocations = new System.Windows.Forms.TabPage();
            this.btnBingOSMap = new System.Windows.Forms.Button();
            this.btnShowMap = new System.Windows.Forms.Button();
            this.tabCtrlLocations = new System.Windows.Forms.TabControl();
            this.tabTreeView = new System.Windows.Forms.TabPage();
            this.treeViewLocations = new System.Windows.Forms.TreeView();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.tabCountries = new System.Windows.Forms.TabPage();
            this.tabRegions = new System.Windows.Forms.TabPage();
            this.tabSubRegions = new System.Windows.Forms.TabPage();
            this.dgSubRegions = new System.Windows.Forms.DataGridView();
            this.tabAddresses = new System.Windows.Forms.TabPage();
            this.dgAddresses = new System.Windows.Forms.DataGridView();
            this.tabPlaces = new System.Windows.Forms.TabPage();
            this.dgPlaces = new System.Windows.Forms.DataGridView();
            this.tabFamilies = new System.Windows.Forms.TabPage();
            this.dgFamilies = new System.Windows.Forms.DataGridView();
            this.tabIndividuals = new System.Windows.Forms.TabPage();
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.tabDisplayProgress = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.pbRelationships = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.pbFamilies = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.pbIndividuals = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.pbSources = new System.Windows.Forms.ProgressBar();
            this.tabSelector = new System.Windows.Forms.TabControl();
            this.tabSurnames = new System.Windows.Forms.TabPage();
            this.dgSurnames = new System.Windows.Forms.DataGridView();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URI = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Individuals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Families = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marriages = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabFacts = new System.Windows.Forms.TabPage();
            this.btnDeselectAllFactTypes = new System.Windows.Forms.Button();
            this.btnSelectAllFactTypes = new System.Windows.Forms.Button();
            this.ckbFactSelect = new System.Windows.Forms.CheckedListBox();
            this.btnShowFacts = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFactsSurname = new System.Windows.Forms.TextBox();
            this.tabDuplicates = new System.Windows.Forms.TabPage();
            this.dgDuplicates = new System.Windows.Forms.DataGridView();
            this.btnCancelDuplicates = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.labDuplicateSlider = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.labCalcDuplicates = new System.Windows.Forms.Label();
            this.pbDuplicates = new System.Windows.Forms.ProgressBar();
            this.saveDatabase = new System.Windows.Forms.SaveFileDialog();
            this.restoreDatabase = new System.Windows.Forms.OpenFileDialog();
            this.rtbOutput = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.relTypesFacts = new Controls.RelationTypes();
            this.cenDate = new Controls.CensusDateSelector();
            this.relTypesCensus = new Controls.RelationTypes();
            this.relTypesLC = new Controls.RelationTypes();
            this.relTypesColoured = new Controls.RelationTypes();
            this.treetopsRelation = new Controls.RelationTypes();
            this.treetopsCountry = new Controls.CensusCountry();
            this.wardeadRelation = new Controls.RelationTypes();
            this.wardeadCountry = new Controls.CensusCountry();
            this.NonDuplicate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateIndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicatesName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateForenames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateSurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateBirthLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchIndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchBirthLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1.SuspendLayout();
            this.mnuSetRoot.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDataErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCountries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDuplicateScore)).BeginInit();
            this.tabWorldWars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorldWars)).BeginInit();
            this.tabTreetops.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTreeTops)).BeginInit();
            this.tabColourReports.SuspendLayout();
            this.tabLostCousins.SuspendLayout();
            this.tabCensus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).BeginInit();
            this.tabLooseBirthDeaths.SuspendLayout();
            this.tabCtrlLooseBDs.SuspendLayout();
            this.tabLooseBirths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseBirths)).BeginInit();
            this.tabLooseDeaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).BeginInit();
            this.tabDataErrors.SuspendLayout();
            this.gbDataErrorTypes.SuspendLayout();
            this.tabOccupations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOccupations)).BeginInit();
            this.tabLocations.SuspendLayout();
            this.tabCtrlLocations.SuspendLayout();
            this.tabTreeView.SuspendLayout();
            this.tabCountries.SuspendLayout();
            this.tabRegions.SuspendLayout();
            this.tabSubRegions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSubRegions)).BeginInit();
            this.tabAddresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAddresses)).BeginInit();
            this.tabPlaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPlaces)).BeginInit();
            this.tabFamilies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).BeginInit();
            this.tabIndividuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.tabDisplayProgress.SuspendLayout();
            this.tabSelector.SuspendLayout();
            this.tabSurnames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSurnames)).BeginInit();
            this.tabFacts.SuspendLayout();
            this.tabDuplicates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDuplicates)).BeginInit();
            this.SuspendLayout();
            // 
            // openGedcom
            // 
            this.openGedcom.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuReports,
            this.mnuExport,
            this.toolsToolStripMenuItem,
            this.mnuMaps,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1043, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.mnuReload,
            this.mnuPrint,
            this.toolStripSeparator6,
            this.mnuRecent,
            this.toolStripSeparator3,
            this.databaseToolStripMenuItem,
            this.toolStripSeparator5,
            this.exitToolStripMenuItem,
            this.buildLocationsToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openToolStripMenuItem.Text = "Open GEDCOM file...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // mnuReload
            // 
            this.mnuReload.Enabled = false;
            this.mnuReload.Name = "mnuReload";
            this.mnuReload.Size = new System.Drawing.Size(184, 22);
            this.mnuReload.Text = "Reload";
            this.mnuReload.Click += new System.EventHandler(this.reloadToolStripMenuItem_Click);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Enabled = false;
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(184, 22);
            this.mnuPrint.Text = "Print";
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuRecent
            // 
            this.mnuRecent.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuRecent1,
            this.mnuRecent2,
            this.mnuRecent3,
            this.mnuRecent4,
            this.mnuRecent5,
            this.toolStripSeparator7,
            this.clearRecentFileListToolStripMenuItem});
            this.mnuRecent.Name = "mnuRecent";
            this.mnuRecent.Size = new System.Drawing.Size(184, 22);
            this.mnuRecent.Text = "Recent Files";
            // 
            // mnuRecent1
            // 
            this.mnuRecent1.Name = "mnuRecent1";
            this.mnuRecent1.Size = new System.Drawing.Size(182, 22);
            this.mnuRecent1.Text = "1.";
            this.mnuRecent1.Click += new System.EventHandler(this.OpenRecentFile_Click);
            // 
            // mnuRecent2
            // 
            this.mnuRecent2.Name = "mnuRecent2";
            this.mnuRecent2.Size = new System.Drawing.Size(182, 22);
            this.mnuRecent2.Text = "2.";
            this.mnuRecent2.Click += new System.EventHandler(this.OpenRecentFile_Click);
            // 
            // mnuRecent3
            // 
            this.mnuRecent3.Name = "mnuRecent3";
            this.mnuRecent3.Size = new System.Drawing.Size(182, 22);
            this.mnuRecent3.Text = "3.";
            this.mnuRecent3.Click += new System.EventHandler(this.OpenRecentFile_Click);
            // 
            // mnuRecent4
            // 
            this.mnuRecent4.Name = "mnuRecent4";
            this.mnuRecent4.Size = new System.Drawing.Size(182, 22);
            this.mnuRecent4.Text = "4.";
            this.mnuRecent4.Click += new System.EventHandler(this.OpenRecentFile_Click);
            // 
            // mnuRecent5
            // 
            this.mnuRecent5.Name = "mnuRecent5";
            this.mnuRecent5.Size = new System.Drawing.Size(182, 22);
            this.mnuRecent5.Text = "5.";
            this.mnuRecent5.Click += new System.EventHandler(this.OpenRecentFile_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(179, 6);
            // 
            // clearRecentFileListToolStripMenuItem
            // 
            this.clearRecentFileListToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("clearRecentFileListToolStripMenuItem.Image")));
            this.clearRecentFileListToolStripMenuItem.Name = "clearRecentFileListToolStripMenuItem";
            this.clearRecentFileListToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.clearRecentFileListToolStripMenuItem.Text = "Clear Recent File List";
            this.clearRecentFileListToolStripMenuItem.Click += new System.EventHandler(this.clearRecentFileListToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(181, 6);
            // 
            // databaseToolStripMenuItem
            // 
            this.databaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.backupToolStripMenuItem,
            this.mnuRestore});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.databaseToolStripMenuItem.Text = "Geocode Database";
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.backupToolStripMenuItem_Click);
            // 
            // mnuRestore
            // 
            this.mnuRestore.Name = "mnuRestore";
            this.mnuRestore.Size = new System.Drawing.Size(113, 22);
            this.mnuRestore.Text = "Restore";
            this.mnuRestore.ToolTipText = "Restore is only available prior to loading GEDCOM";
            this.mnuRestore.Click += new System.EventHandler(this.restoreToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(181, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // buildLocationsToolStripMenuItem
            // 
            this.buildLocationsToolStripMenuItem.Name = "buildLocationsToolStripMenuItem";
            this.buildLocationsToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.buildLocationsToolStripMenuItem.Text = "Build Locations";
            this.buildLocationsToolStripMenuItem.Visible = false;
            this.buildLocationsToolStripMenuItem.Click += new System.EventHandler(this.buildLocationsToolStripMenuItem_Click);
            // 
            // mnuReports
            // 
            this.mnuReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChildAgeProfiles,
            this.mnuOlderParents});
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Size = new System.Drawing.Size(59, 20);
            this.mnuReports.Text = "Reports";
            // 
            // mnuChildAgeProfiles
            // 
            this.mnuChildAgeProfiles.Name = "mnuChildAgeProfiles";
            this.mnuChildAgeProfiles.Size = new System.Drawing.Size(170, 22);
            this.mnuChildAgeProfiles.Text = "Parent Age Report";
            this.mnuChildAgeProfiles.Click += new System.EventHandler(this.childAgeProfilesToolStripMenuItem_Click);
            // 
            // mnuOlderParents
            // 
            this.mnuOlderParents.Name = "mnuOlderParents";
            this.mnuOlderParents.Size = new System.Drawing.Size(170, 22);
            this.mnuOlderParents.Text = "Older Parents";
            this.mnuOlderParents.Click += new System.EventHandler(this.olderParentsToolStripMenuItem_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuIndividualsToExcel,
            this.mnuFamiliesToExcel,
            this.mnuFactsToExcel});
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(52, 20);
            this.mnuExport.Text = "Export";
            // 
            // mnuIndividualsToExcel
            // 
            this.mnuIndividualsToExcel.Name = "mnuIndividualsToExcel";
            this.mnuIndividualsToExcel.Size = new System.Drawing.Size(174, 22);
            this.mnuIndividualsToExcel.Text = "Individuals to Excel";
            this.mnuIndividualsToExcel.Click += new System.EventHandler(this.individualsToExcelToolStripMenuItem_Click);
            // 
            // mnuFamiliesToExcel
            // 
            this.mnuFamiliesToExcel.Name = "mnuFamiliesToExcel";
            this.mnuFamiliesToExcel.Size = new System.Drawing.Size(174, 22);
            this.mnuFamiliesToExcel.Text = "Families to Excel";
            this.mnuFamiliesToExcel.Click += new System.EventHandler(this.familiesToExcelToolStripMenuItem_Click);
            // 
            // mnuFactsToExcel
            // 
            this.mnuFactsToExcel.Name = "mnuFactsToExcel";
            this.mnuFactsToExcel.Size = new System.Drawing.Size(174, 22);
            this.mnuFactsToExcel.Text = "Facts to Excel";
            this.mnuFactsToExcel.Click += new System.EventHandler(this.factsToExcelToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.toolStripSeparator2,
            this.displayOptionsOnLoadToolStripMenuItem,
            this.resetToDefaultFormSizeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(205, 6);
            // 
            // displayOptionsOnLoadToolStripMenuItem
            // 
            this.displayOptionsOnLoadToolStripMenuItem.CheckOnClick = true;
            this.displayOptionsOnLoadToolStripMenuItem.Name = "displayOptionsOnLoadToolStripMenuItem";
            this.displayOptionsOnLoadToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.displayOptionsOnLoadToolStripMenuItem.Text = "Display Options on Load";
            this.displayOptionsOnLoadToolStripMenuItem.Click += new System.EventHandler(this.displayOptionsOnLoadToolStripMenuItem_Click);
            // 
            // resetToDefaultFormSizeToolStripMenuItem
            // 
            this.resetToDefaultFormSizeToolStripMenuItem.Name = "resetToDefaultFormSizeToolStripMenuItem";
            this.resetToDefaultFormSizeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.resetToDefaultFormSizeToolStripMenuItem.Text = "Reset to Default form size";
            this.resetToDefaultFormSizeToolStripMenuItem.Click += new System.EventHandler(this.resetToDefaultFormSizeToolStripMenuItem_Click);
            // 
            // mnuMaps
            // 
            this.mnuMaps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowTimeline,
            this.mnuLifelines,
            this.mnuPlaces,
            this.toolStripSeparator4,
            this.mnuGeocodeLocations,
            this.mnuLocationsGeocodeReport});
            this.mnuMaps.Name = "mnuMaps";
            this.mnuMaps.Size = new System.Drawing.Size(48, 20);
            this.mnuMaps.Text = "Maps";
            // 
            // mnuShowTimeline
            // 
            this.mnuShowTimeline.Name = "mnuShowTimeline";
            this.mnuShowTimeline.Size = new System.Drawing.Size(243, 22);
            this.mnuShowTimeline.Text = "Show Timeline";
            this.mnuShowTimeline.Click += new System.EventHandler(this.mnuShowTimeline_Click);
            // 
            // mnuLifelines
            // 
            this.mnuLifelines.Name = "mnuLifelines";
            this.mnuLifelines.Size = new System.Drawing.Size(243, 22);
            this.mnuLifelines.Text = "Show Lifelines";
            this.mnuLifelines.Click += new System.EventHandler(this.mnuLifelines_Click);
            // 
            // mnuPlaces
            // 
            this.mnuPlaces.Name = "mnuPlaces";
            this.mnuPlaces.Size = new System.Drawing.Size(243, 22);
            this.mnuPlaces.Text = "Show Places";
            this.mnuPlaces.Click += new System.EventHandler(this.mnuPlaces_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(240, 6);
            // 
            // mnuGeocodeLocations
            // 
            this.mnuGeocodeLocations.Name = "mnuGeocodeLocations";
            this.mnuGeocodeLocations.Size = new System.Drawing.Size(243, 22);
            this.mnuGeocodeLocations.Text = "Run Geocoder to Find Locations";
            this.mnuGeocodeLocations.Click += new System.EventHandler(this.mnuGeocodeLocations_Click);
            // 
            // mnuLocationsGeocodeReport
            // 
            this.mnuLocationsGeocodeReport.Name = "mnuLocationsGeocodeReport";
            this.mnuLocationsGeocodeReport.Size = new System.Drawing.Size(243, 22);
            this.mnuLocationsGeocodeReport.Text = "Display Geocoded Locations";
            this.mnuLocationsGeocodeReport.Click += new System.EventHandler(this.locationsGeocodeReportToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewOnlineManualToolStripMenuItem,
            this.reportAnIssueToolStripMenuItem,
            this.toolStripSeparator1,
            this.whatsNewToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewOnlineManualToolStripMenuItem
            // 
            this.viewOnlineManualToolStripMenuItem.Name = "viewOnlineManualToolStripMenuItem";
            this.viewOnlineManualToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.viewOnlineManualToolStripMenuItem.Text = "View Online Manual";
            this.viewOnlineManualToolStripMenuItem.Click += new System.EventHandler(this.viewOnlineManualToolStripMenuItem_Click);
            // 
            // reportAnIssueToolStripMenuItem
            // 
            this.reportAnIssueToolStripMenuItem.Name = "reportAnIssueToolStripMenuItem";
            this.reportAnIssueToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.reportAnIssueToolStripMenuItem.Text = "Report an Issue";
            this.reportAnIssueToolStripMenuItem.Click += new System.EventHandler(this.reportAnIssueToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // whatsNewToolStripMenuItem
            // 
            this.whatsNewToolStripMenuItem.Name = "whatsNewToolStripMenuItem";
            this.whatsNewToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.whatsNewToolStripMenuItem.Text = "What\'s New";
            this.whatsNewToolStripMenuItem.Click += new System.EventHandler(this.whatsNewToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mnuSetRoot
            // 
            this.mnuSetRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsRootToolStripMenuItem});
            this.mnuSetRoot.Name = "mnuSetRoot";
            this.mnuSetRoot.Size = new System.Drawing.Size(174, 26);
            // 
            // setAsRootToolStripMenuItem
            // 
            this.setAsRootToolStripMenuItem.Name = "setAsRootToolStripMenuItem";
            this.setAsRootToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.setAsRootToolStripMenuItem.Text = "Set As Root Person";
            this.setAsRootToolStripMenuItem.Click += new System.EventHandler(this.setAsRootToolStripMenuItem_Click);
            // 
            // tsCount
            // 
            this.tsCount.Name = "tsCount";
            this.tsCount.Size = new System.Drawing.Size(52, 17);
            this.tsCount.Text = "Count: 0";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCountLabel,
            this.tsHintsLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 501);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1043, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsCountLabel
            // 
            this.tsCountLabel.Name = "tsCountLabel";
            this.tsCountLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // tsHintsLabel
            // 
            this.tsHintsLabel.Name = "tsHintsLabel";
            this.tsHintsLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // dgDataErrors
            // 
            this.dgDataErrors.AllowUserToAddRows = false;
            this.dgDataErrors.AllowUserToDeleteRows = false;
            this.dgDataErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDataErrors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgDataErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgDataErrors.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgDataErrors.Location = new System.Drawing.Point(0, 154);
            this.dgDataErrors.Name = "dgDataErrors";
            this.dgDataErrors.ReadOnly = true;
            this.dgDataErrors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDataErrors.ShowCellToolTips = false;
            this.dgDataErrors.ShowEditingIcon = false;
            this.dgDataErrors.Size = new System.Drawing.Size(1035, 291);
            this.dgDataErrors.TabIndex = 3;
            this.toolTips.SetToolTip(this.dgDataErrors, "Double click to see list of facts for that individual");
            this.dgDataErrors.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDataErrors_CellDoubleClick);
            // 
            // dgRegions
            // 
            this.dgRegions.AllowUserToAddRows = false;
            this.dgRegions.AllowUserToDeleteRows = false;
            this.dgRegions.AllowUserToResizeRows = false;
            this.dgRegions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgRegions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgRegions.Location = new System.Drawing.Point(3, 3);
            this.dgRegions.MultiSelect = false;
            this.dgRegions.Name = "dgRegions";
            this.dgRegions.RowHeadersVisible = false;
            this.dgRegions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRegions.Size = new System.Drawing.Size(1015, 407);
            this.dgRegions.TabIndex = 1;
            this.toolTips.SetToolTip(this.dgRegions, "Double click on Region name to see list of individuals with that Region.");
            this.dgRegions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRegions_CellDoubleClick);
            this.dgRegions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgRegions_CellFormatting);
            // 
            // dgCountries
            // 
            this.dgCountries.AllowUserToAddRows = false;
            this.dgCountries.AllowUserToDeleteRows = false;
            this.dgCountries.AllowUserToResizeRows = false;
            this.dgCountries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgCountries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCountries.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCountries.Location = new System.Drawing.Point(3, 3);
            this.dgCountries.MultiSelect = false;
            this.dgCountries.Name = "dgCountries";
            this.dgCountries.RowHeadersVisible = false;
            this.dgCountries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCountries.Size = new System.Drawing.Size(1015, 407);
            this.dgCountries.TabIndex = 0;
            this.toolTips.SetToolTip(this.dgCountries, "Double click on Country name to see list of individuals with that Country.");
            this.dgCountries.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCountries_CellDoubleClick);
            this.dgCountries.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgCountries_CellFormatting);
            // 
            // tbDuplicateScore
            // 
            this.tbDuplicateScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDuplicateScore.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbDuplicateScore.Location = new System.Drawing.Point(443, 3);
            this.tbDuplicateScore.Minimum = 1;
            this.tbDuplicateScore.Name = "tbDuplicateScore";
            this.tbDuplicateScore.Size = new System.Drawing.Size(589, 45);
            this.tbDuplicateScore.TabIndex = 11;
            this.tbDuplicateScore.TickFrequency = 5;
            this.toolTips.SetToolTip(this.tbDuplicateScore, "Adjust Slider to right to limit results to more likely matches");
            this.tbDuplicateScore.Value = 1;
            this.tbDuplicateScore.Scroll += new System.EventHandler(this.tbDuplicateScore_Scroll);
            // 
            // printPreviewDialog
            // 
            this.printPreviewDialog.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog.Enabled = true;
            this.printPreviewDialog.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog.Icon")));
            this.printPreviewDialog.Name = "printPreviewDialog";
            this.printPreviewDialog.Visible = false;
            // 
            // printDialog
            // 
            this.printDialog.AllowSelection = true;
            this.printDialog.AllowSomePages = true;
            this.printDialog.UseEXDialog = true;
            // 
            // tabWorldWars
            // 
            this.tabWorldWars.Controls.Add(this.ckbWDIgnoreLocations);
            this.tabWorldWars.Controls.Add(this.btnWWII);
            this.tabWorldWars.Controls.Add(this.btnWWI);
            this.tabWorldWars.Controls.Add(this.dgWorldWars);
            this.tabWorldWars.Controls.Add(this.label9);
            this.tabWorldWars.Controls.Add(this.txtWorldWarsSurname);
            this.tabWorldWars.Controls.Add(this.wardeadRelation);
            this.tabWorldWars.Controls.Add(this.wardeadCountry);
            this.tabWorldWars.Location = new System.Drawing.Point(4, 22);
            this.tabWorldWars.Name = "tabWorldWars";
            this.tabWorldWars.Size = new System.Drawing.Size(1035, 445);
            this.tabWorldWars.TabIndex = 8;
            this.tabWorldWars.Text = "World Wars";
            this.tabWorldWars.ToolTipText = "Find men of fighting age during WWI & WWII";
            this.tabWorldWars.UseVisualStyleBackColor = true;
            // 
            // ckbWDIgnoreLocations
            // 
            this.ckbWDIgnoreLocations.AutoSize = true;
            this.ckbWDIgnoreLocations.Checked = true;
            this.ckbWDIgnoreLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbWDIgnoreLocations.Location = new System.Drawing.Point(8, 87);
            this.ckbWDIgnoreLocations.Name = "ckbWDIgnoreLocations";
            this.ckbWDIgnoreLocations.Size = new System.Drawing.Size(252, 17);
            this.ckbWDIgnoreLocations.TabIndex = 32;
            this.ckbWDIgnoreLocations.Text = "Include Unknown Countries in World Wars Filter";
            this.ckbWDIgnoreLocations.UseVisualStyleBackColor = true;
            this.ckbWDIgnoreLocations.CheckedChanged += new System.EventHandler(this.ckbWDIgnoreLocations_CheckedChanged);
            // 
            // btnWWII
            // 
            this.btnWWII.Location = new System.Drawing.Point(758, 58);
            this.btnWWII.Name = "btnWWII";
            this.btnWWII.Size = new System.Drawing.Size(95, 25);
            this.btnWWII.TabIndex = 31;
            this.btnWWII.Text = "World War II";
            this.btnWWII.UseVisualStyleBackColor = true;
            this.btnWWII.Click += new System.EventHandler(this.btnWWII_Click);
            // 
            // btnWWI
            // 
            this.btnWWI.Location = new System.Drawing.Point(650, 58);
            this.btnWWI.Name = "btnWWI";
            this.btnWWI.Size = new System.Drawing.Size(95, 25);
            this.btnWWI.TabIndex = 30;
            this.btnWWI.Text = "World War I";
            this.btnWWI.UseVisualStyleBackColor = true;
            this.btnWWI.Click += new System.EventHandler(this.btnWWI_Click);
            // 
            // dgWorldWars
            // 
            this.dgWorldWars.AllowUserToAddRows = false;
            this.dgWorldWars.AllowUserToDeleteRows = false;
            this.dgWorldWars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgWorldWars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgWorldWars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWorldWars.Location = new System.Drawing.Point(0, 110);
            this.dgWorldWars.Name = "dgWorldWars";
            this.dgWorldWars.ReadOnly = true;
            this.dgWorldWars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWorldWars.Size = new System.Drawing.Size(1035, 332);
            this.dgWorldWars.TabIndex = 29;
            this.dgWorldWars.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgWorldWars_CellDoubleClick);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(595, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Surname";
            // 
            // txtWorldWarsSurname
            // 
            this.txtWorldWarsSurname.Location = new System.Drawing.Point(650, 22);
            this.txtWorldWarsSurname.Name = "txtWorldWarsSurname";
            this.txtWorldWarsSurname.Size = new System.Drawing.Size(201, 20);
            this.txtWorldWarsSurname.TabIndex = 27;
            // 
            // tabTreetops
            // 
            this.tabTreetops.Controls.Add(this.dgTreeTops);
            this.tabTreetops.Controls.Add(this.ckbTTIgnoreLocations);
            this.tabTreetops.Controls.Add(this.btnTreeTops);
            this.tabTreetops.Controls.Add(this.label8);
            this.tabTreetops.Controls.Add(this.txtTreetopsSurname);
            this.tabTreetops.Controls.Add(this.treetopsRelation);
            this.tabTreetops.Controls.Add(this.treetopsCountry);
            this.tabTreetops.Location = new System.Drawing.Point(4, 22);
            this.tabTreetops.Name = "tabTreetops";
            this.tabTreetops.Size = new System.Drawing.Size(1035, 445);
            this.tabTreetops.TabIndex = 7;
            this.tabTreetops.Text = "Treetops";
            this.tabTreetops.UseVisualStyleBackColor = true;
            // 
            // dgTreeTops
            // 
            this.dgTreeTops.AllowUserToAddRows = false;
            this.dgTreeTops.AllowUserToDeleteRows = false;
            this.dgTreeTops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTreeTops.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgTreeTops.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTreeTops.Location = new System.Drawing.Point(0, 110);
            this.dgTreeTops.Name = "dgTreeTops";
            this.dgTreeTops.ReadOnly = true;
            this.dgTreeTops.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTreeTops.Size = new System.Drawing.Size(1035, 332);
            this.dgTreeTops.TabIndex = 28;
            this.dgTreeTops.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgTreeTops_CellDoubleClick);
            // 
            // ckbTTIgnoreLocations
            // 
            this.ckbTTIgnoreLocations.AutoSize = true;
            this.ckbTTIgnoreLocations.Checked = true;
            this.ckbTTIgnoreLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTTIgnoreLocations.Location = new System.Drawing.Point(8, 87);
            this.ckbTTIgnoreLocations.Name = "ckbTTIgnoreLocations";
            this.ckbTTIgnoreLocations.Size = new System.Drawing.Size(238, 17);
            this.ckbTTIgnoreLocations.TabIndex = 27;
            this.ckbTTIgnoreLocations.Text = "Include Unknown Countries in Treetops Filter";
            this.ckbTTIgnoreLocations.UseVisualStyleBackColor = true;
            this.ckbTTIgnoreLocations.CheckedChanged += new System.EventHandler(this.ckbTTIgnoreLocations_CheckedChanged);
            // 
            // btnTreeTops
            // 
            this.btnTreeTops.Location = new System.Drawing.Point(650, 58);
            this.btnTreeTops.Name = "btnTreeTops";
            this.btnTreeTops.Size = new System.Drawing.Size(201, 25);
            this.btnTreeTops.TabIndex = 25;
            this.btnTreeTops.Text = "Show People at top of tree";
            this.btnTreeTops.UseVisualStyleBackColor = true;
            this.btnTreeTops.Click += new System.EventHandler(this.btnTreeTops_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(595, 25);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 24;
            this.label8.Text = "Surname";
            // 
            // txtTreetopsSurname
            // 
            this.txtTreetopsSurname.Location = new System.Drawing.Point(650, 22);
            this.txtTreetopsSurname.Name = "txtTreetopsSurname";
            this.txtTreetopsSurname.Size = new System.Drawing.Size(201, 20);
            this.txtTreetopsSurname.TabIndex = 23;
            // 
            // tabColourReports
            // 
            this.tabColourReports.Controls.Add(this.btnColourBMD);
            this.tabColourReports.Controls.Add(this.btnColourCensus);
            this.tabColourReports.Controls.Add(this.label10);
            this.tabColourReports.Controls.Add(this.txtColouredSurname);
            this.tabColourReports.Controls.Add(this.relTypesColoured);
            this.tabColourReports.Location = new System.Drawing.Point(4, 22);
            this.tabColourReports.Name = "tabColourReports";
            this.tabColourReports.Size = new System.Drawing.Size(1035, 445);
            this.tabColourReports.TabIndex = 12;
            this.tabColourReports.Text = "Search Summaries";
            this.tabColourReports.UseVisualStyleBackColor = true;
            // 
            // btnColourBMD
            // 
            this.btnColourBMD.Location = new System.Drawing.Point(8, 92);
            this.btnColourBMD.Name = "btnColourBMD";
            this.btnColourBMD.Size = new System.Drawing.Size(155, 23);
            this.btnColourBMD.TabIndex = 33;
            this.btnColourBMD.Text = "View Colour BMD Report";
            this.btnColourBMD.UseVisualStyleBackColor = true;
            this.btnColourBMD.Click += new System.EventHandler(this.btnColourBMD_Click);
            // 
            // btnColourCensus
            // 
            this.btnColourCensus.Location = new System.Drawing.Point(169, 92);
            this.btnColourCensus.Name = "btnColourCensus";
            this.btnColourCensus.Size = new System.Drawing.Size(155, 23);
            this.btnColourCensus.TabIndex = 32;
            this.btnColourCensus.Text = "View Colour Census Report";
            this.btnColourCensus.UseVisualStyleBackColor = true;
            this.btnColourCensus.Click += new System.EventHandler(this.btnColouredCensus_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(339, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 31;
            this.label10.Text = "Surname";
            // 
            // txtColouredSurname
            // 
            this.txtColouredSurname.Location = new System.Drawing.Point(394, 16);
            this.txtColouredSurname.Name = "txtColouredSurname";
            this.txtColouredSurname.Size = new System.Drawing.Size(201, 20);
            this.txtColouredSurname.TabIndex = 30;
            // 
            // tabLostCousins
            // 
            this.tabLostCousins.Controls.Add(this.btnLCnoCensus);
            this.tabLostCousins.Controls.Add(this.btnLCDuplicates);
            this.tabLostCousins.Controls.Add(this.btnLCMissingCountry);
            this.tabLostCousins.Controls.Add(this.btnLC1940USA);
            this.tabLostCousins.Controls.Add(this.rtbLostCousins);
            this.tabLostCousins.Controls.Add(this.linkLabel2);
            this.tabLostCousins.Controls.Add(this.btnLC1911EW);
            this.tabLostCousins.Controls.Add(this.linkLabel1);
            this.tabLostCousins.Controls.Add(this.ckbShowLCEntered);
            this.tabLostCousins.Controls.Add(this.btnLC1841EW);
            this.tabLostCousins.Controls.Add(this.btnLC1911Ireland);
            this.tabLostCousins.Controls.Add(this.btnLC1880USA);
            this.tabLostCousins.Controls.Add(this.btnLC1881EW);
            this.tabLostCousins.Controls.Add(this.btnLC1881Canada);
            this.tabLostCousins.Controls.Add(this.btnLC1881Scot);
            this.tabLostCousins.Controls.Add(this.relTypesLC);
            this.tabLostCousins.Location = new System.Drawing.Point(4, 22);
            this.tabLostCousins.Name = "tabLostCousins";
            this.tabLostCousins.Padding = new System.Windows.Forms.Padding(3);
            this.tabLostCousins.Size = new System.Drawing.Size(1035, 445);
            this.tabLostCousins.TabIndex = 5;
            this.tabLostCousins.Text = "Lost Cousins";
            this.tabLostCousins.UseVisualStyleBackColor = true;
            // 
            // btnLCnoCensus
            // 
            this.btnLCnoCensus.Location = new System.Drawing.Point(344, 233);
            this.btnLCnoCensus.Name = "btnLCnoCensus";
            this.btnLCnoCensus.Size = new System.Drawing.Size(162, 27);
            this.btnLCnoCensus.TabIndex = 22;
            this.btnLCnoCensus.Text = "Lost Cousins but no Census";
            this.btnLCnoCensus.UseVisualStyleBackColor = true;
            this.btnLCnoCensus.Click += new System.EventHandler(this.btnLCnoCensus_Click);
            // 
            // btnLCDuplicates
            // 
            this.btnLCDuplicates.Location = new System.Drawing.Point(176, 233);
            this.btnLCDuplicates.Name = "btnLCDuplicates";
            this.btnLCDuplicates.Size = new System.Drawing.Size(162, 27);
            this.btnLCDuplicates.TabIndex = 21;
            this.btnLCDuplicates.Text = "Lost Cousins Duplicate Facts";
            this.btnLCDuplicates.UseVisualStyleBackColor = true;
            this.btnLCDuplicates.Click += new System.EventHandler(this.btnLCDuplicates_Click);
            // 
            // btnLCMissingCountry
            // 
            this.btnLCMissingCountry.Location = new System.Drawing.Point(8, 233);
            this.btnLCMissingCountry.Name = "btnLCMissingCountry";
            this.btnLCMissingCountry.Size = new System.Drawing.Size(162, 27);
            this.btnLCMissingCountry.TabIndex = 20;
            this.btnLCMissingCountry.Text = "Lost Cousins with no Country";
            this.btnLCMissingCountry.UseVisualStyleBackColor = true;
            this.btnLCMissingCountry.Click += new System.EventHandler(this.btnLCMissingCountry_Click);
            // 
            // btnLC1940USA
            // 
            this.btnLC1940USA.Location = new System.Drawing.Point(344, 158);
            this.btnLC1940USA.Name = "btnLC1940USA";
            this.btnLC1940USA.Size = new System.Drawing.Size(162, 27);
            this.btnLC1940USA.TabIndex = 18;
            this.btnLC1940USA.Text = "1940 US Census";
            this.btnLC1940USA.UseVisualStyleBackColor = true;
            this.btnLC1940USA.Click += new System.EventHandler(this.btnLC1940USA_Click);
            // 
            // rtbLostCousins
            // 
            this.rtbLostCousins.BackColor = System.Drawing.SystemColors.Window;
            this.rtbLostCousins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLostCousins.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbLostCousins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLostCousins.Location = new System.Drawing.Point(535, 8);
            this.rtbLostCousins.Name = "rtbLostCousins";
            this.rtbLostCousins.ReadOnly = true;
            this.rtbLostCousins.Size = new System.Drawing.Size(388, 362);
            this.rtbLostCousins.TabIndex = 17;
            this.rtbLostCousins.TabStop = false;
            this.rtbLostCousins.Text = "";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(727, 383);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(174, 16);
            this.linkLabel2.TabIndex = 15;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Visit the Lost Cousins Forum";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // btnLC1911EW
            // 
            this.btnLC1911EW.Location = new System.Drawing.Point(8, 158);
            this.btnLC1911EW.Name = "btnLC1911EW";
            this.btnLC1911EW.Size = new System.Drawing.Size(162, 27);
            this.btnLC1911EW.TabIndex = 14;
            this.btnLC1911EW.Text = "1911 England && Wales Census";
            this.btnLC1911EW.UseVisualStyleBackColor = true;
            this.btnLC1911EW.Click += new System.EventHandler(this.btnLC1911EW_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(535, 383);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(186, 16);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Visit the Lost Cousins Website";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ckbShowLCEntered
            // 
            this.ckbShowLCEntered.AutoSize = true;
            this.ckbShowLCEntered.Location = new System.Drawing.Point(8, 191);
            this.ckbShowLCEntered.Name = "ckbShowLCEntered";
            this.ckbShowLCEntered.Size = new System.Drawing.Size(415, 17);
            this.ckbShowLCEntered.TabIndex = 10;
            this.ckbShowLCEntered.Text = "Show already entered to Lost Cousins (unticked = show those to yet to be entered)" +
    "";
            this.ckbShowLCEntered.UseVisualStyleBackColor = true;
            // 
            // btnLC1841EW
            // 
            this.btnLC1841EW.Location = new System.Drawing.Point(8, 125);
            this.btnLC1841EW.Name = "btnLC1841EW";
            this.btnLC1841EW.Size = new System.Drawing.Size(162, 27);
            this.btnLC1841EW.TabIndex = 8;
            this.btnLC1841EW.Text = "1841 England && Wales Census";
            this.btnLC1841EW.UseVisualStyleBackColor = true;
            this.btnLC1841EW.Click += new System.EventHandler(this.btnLC1841EW_Click);
            // 
            // btnLC1911Ireland
            // 
            this.btnLC1911Ireland.Location = new System.Drawing.Point(176, 125);
            this.btnLC1911Ireland.Name = "btnLC1911Ireland";
            this.btnLC1911Ireland.Size = new System.Drawing.Size(162, 27);
            this.btnLC1911Ireland.TabIndex = 7;
            this.btnLC1911Ireland.Text = "1911 Ireland Census";
            this.btnLC1911Ireland.UseVisualStyleBackColor = true;
            this.btnLC1911Ireland.Click += new System.EventHandler(this.btnLC1911Ireland_Click);
            // 
            // btnLC1880USA
            // 
            this.btnLC1880USA.Location = new System.Drawing.Point(344, 125);
            this.btnLC1880USA.Name = "btnLC1880USA";
            this.btnLC1880USA.Size = new System.Drawing.Size(162, 27);
            this.btnLC1880USA.TabIndex = 6;
            this.btnLC1880USA.Text = "1880 US Census";
            this.btnLC1880USA.UseVisualStyleBackColor = true;
            this.btnLC1880USA.Click += new System.EventHandler(this.btnLC1880USA_Click);
            // 
            // btnLC1881EW
            // 
            this.btnLC1881EW.Location = new System.Drawing.Point(8, 92);
            this.btnLC1881EW.Name = "btnLC1881EW";
            this.btnLC1881EW.Size = new System.Drawing.Size(162, 27);
            this.btnLC1881EW.TabIndex = 5;
            this.btnLC1881EW.Text = "1881 England && Wales Census";
            this.btnLC1881EW.UseVisualStyleBackColor = true;
            this.btnLC1881EW.Click += new System.EventHandler(this.btnLC1881EW_Click);
            // 
            // btnLC1881Canada
            // 
            this.btnLC1881Canada.Location = new System.Drawing.Point(176, 158);
            this.btnLC1881Canada.Name = "btnLC1881Canada";
            this.btnLC1881Canada.Size = new System.Drawing.Size(162, 27);
            this.btnLC1881Canada.TabIndex = 4;
            this.btnLC1881Canada.Text = "1881 Canada Census";
            this.btnLC1881Canada.UseVisualStyleBackColor = true;
            this.btnLC1881Canada.Click += new System.EventHandler(this.btnLC1881Canada_Click);
            // 
            // btnLC1881Scot
            // 
            this.btnLC1881Scot.Location = new System.Drawing.Point(176, 92);
            this.btnLC1881Scot.Name = "btnLC1881Scot";
            this.btnLC1881Scot.Size = new System.Drawing.Size(162, 27);
            this.btnLC1881Scot.TabIndex = 0;
            this.btnLC1881Scot.Text = "1881 Scotland Census";
            this.btnLC1881Scot.UseVisualStyleBackColor = true;
            this.btnLC1881Scot.Click += new System.EventHandler(this.btnLC1881Scot_Click);
            // 
            // tabCensus
            // 
            this.tabCensus.Controls.Add(this.groupBox2);
            this.tabCensus.Controls.Add(this.groupBox1);
            this.tabCensus.Controls.Add(this.label1);
            this.tabCensus.Controls.Add(this.txtSurname);
            this.tabCensus.Controls.Add(this.label2);
            this.tabCensus.Controls.Add(this.udAgeFilter);
            this.tabCensus.Controls.Add(this.cenDate);
            this.tabCensus.Controls.Add(this.relTypesCensus);
            this.tabCensus.Location = new System.Drawing.Point(4, 22);
            this.tabCensus.Name = "tabCensus";
            this.tabCensus.Padding = new System.Windows.Forms.Padding(3);
            this.tabCensus.Size = new System.Drawing.Size(1035, 445);
            this.tabCensus.TabIndex = 0;
            this.tabCensus.Text = "Census";
            this.tabCensus.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnShowCensusEntered);
            this.groupBox2.Controls.Add(this.btnShowCensusMissing);
            this.groupBox2.Location = new System.Drawing.Point(8, 139);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(325, 60);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Census Search Reports";
            // 
            // btnShowCensusEntered
            // 
            this.btnShowCensusEntered.Location = new System.Drawing.Point(162, 19);
            this.btnShowCensusEntered.Name = "btnShowCensusEntered";
            this.btnShowCensusEntered.Size = new System.Drawing.Size(150, 25);
            this.btnShowCensusEntered.TabIndex = 22;
            this.btnShowCensusEntered.Text = "Show Entered on Census";
            this.btnShowCensusEntered.UseVisualStyleBackColor = true;
            this.btnShowCensusEntered.Click += new System.EventHandler(this.btnShowCensus_Click);
            // 
            // btnShowCensusMissing
            // 
            this.btnShowCensusMissing.Location = new System.Drawing.Point(6, 19);
            this.btnShowCensusMissing.Name = "btnShowCensusMissing";
            this.btnShowCensusMissing.Size = new System.Drawing.Size(150, 25);
            this.btnShowCensusMissing.TabIndex = 5;
            this.btnShowCensusMissing.Text = "Show Missing from Census";
            this.btnShowCensusMissing.UseVisualStyleBackColor = true;
            this.btnShowCensusMissing.Click += new System.EventHandler(this.btnShowCensus_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDuplicateCensus);
            this.groupBox1.Controls.Add(this.btnMissingCensusLocation);
            this.groupBox1.Location = new System.Drawing.Point(342, 139);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 61);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Potential Census Fact Problems";
            // 
            // btnDuplicateCensus
            // 
            this.btnDuplicateCensus.Location = new System.Drawing.Point(162, 19);
            this.btnDuplicateCensus.Name = "btnDuplicateCensus";
            this.btnDuplicateCensus.Size = new System.Drawing.Size(150, 25);
            this.btnDuplicateCensus.TabIndex = 6;
            this.btnDuplicateCensus.Text = "Duplicate Census Facts";
            this.btnDuplicateCensus.UseVisualStyleBackColor = true;
            this.btnDuplicateCensus.Click += new System.EventHandler(this.btnDuplicateCensus_Click);
            // 
            // btnMissingCensusLocation
            // 
            this.btnMissingCensusLocation.Location = new System.Drawing.Point(6, 19);
            this.btnMissingCensusLocation.Name = "btnMissingCensusLocation";
            this.btnMissingCensusLocation.Size = new System.Drawing.Size(150, 25);
            this.btnMissingCensusLocation.TabIndex = 5;
            this.btnMissingCensusLocation.Text = "Missing Census Locations";
            this.btnMissingCensusLocation.UseVisualStyleBackColor = true;
            this.btnMissingCensusLocation.Click += new System.EventHandler(this.btnMissingCensusLocation_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(339, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Surname";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(394, 16);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(201, 20);
            this.txtSurname.TabIndex = 19;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(339, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Exclude individuals over the age of ";
            // 
            // udAgeFilter
            // 
            this.udAgeFilter.Location = new System.Drawing.Point(520, 50);
            this.udAgeFilter.Maximum = new decimal(new int[] {
            110,
            0,
            0,
            0});
            this.udAgeFilter.Minimum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.udAgeFilter.Name = "udAgeFilter";
            this.udAgeFilter.Size = new System.Drawing.Size(43, 20);
            this.udAgeFilter.TabIndex = 5;
            this.udAgeFilter.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // tabLooseBirthDeaths
            // 
            this.tabLooseBirthDeaths.Controls.Add(this.tabCtrlLooseBDs);
            this.tabLooseBirthDeaths.Location = new System.Drawing.Point(4, 22);
            this.tabLooseBirthDeaths.Name = "tabLooseBirthDeaths";
            this.tabLooseBirthDeaths.Padding = new System.Windows.Forms.Padding(3);
            this.tabLooseBirthDeaths.Size = new System.Drawing.Size(1035, 445);
            this.tabLooseBirthDeaths.TabIndex = 3;
            this.tabLooseBirthDeaths.Text = "Births/Deaths";
            this.tabLooseBirthDeaths.UseVisualStyleBackColor = true;
            // 
            // tabCtrlLooseBDs
            // 
            this.tabCtrlLooseBDs.Controls.Add(this.tabLooseBirths);
            this.tabCtrlLooseBDs.Controls.Add(this.tabLooseDeaths);
            this.tabCtrlLooseBDs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlLooseBDs.Location = new System.Drawing.Point(3, 3);
            this.tabCtrlLooseBDs.Name = "tabCtrlLooseBDs";
            this.tabCtrlLooseBDs.SelectedIndex = 0;
            this.tabCtrlLooseBDs.Size = new System.Drawing.Size(1029, 439);
            this.tabCtrlLooseBDs.TabIndex = 1;
            this.tabCtrlLooseBDs.SelectedIndexChanged += new System.EventHandler(this.tabCtrlLooseBDs_SelectedIndexChanged);
            // 
            // tabLooseBirths
            // 
            this.tabLooseBirths.Controls.Add(this.dgLooseBirths);
            this.tabLooseBirths.Location = new System.Drawing.Point(4, 22);
            this.tabLooseBirths.Name = "tabLooseBirths";
            this.tabLooseBirths.Padding = new System.Windows.Forms.Padding(3);
            this.tabLooseBirths.Size = new System.Drawing.Size(1021, 413);
            this.tabLooseBirths.TabIndex = 1;
            this.tabLooseBirths.Text = "Loose Births";
            this.tabLooseBirths.UseVisualStyleBackColor = true;
            // 
            // dgLooseBirths
            // 
            this.dgLooseBirths.AllowUserToAddRows = false;
            this.dgLooseBirths.AllowUserToDeleteRows = false;
            this.dgLooseBirths.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLooseBirths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLooseBirths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLooseBirths.Location = new System.Drawing.Point(3, 3);
            this.dgLooseBirths.MultiSelect = false;
            this.dgLooseBirths.Name = "dgLooseBirths";
            this.dgLooseBirths.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLooseBirths.Size = new System.Drawing.Size(1015, 407);
            this.dgLooseBirths.TabIndex = 2;
            this.dgLooseBirths.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLooseBirths_CellDoubleClick);
            // 
            // tabLooseDeaths
            // 
            this.tabLooseDeaths.Controls.Add(this.dgLooseDeaths);
            this.tabLooseDeaths.Location = new System.Drawing.Point(4, 22);
            this.tabLooseDeaths.Name = "tabLooseDeaths";
            this.tabLooseDeaths.Padding = new System.Windows.Forms.Padding(3);
            this.tabLooseDeaths.Size = new System.Drawing.Size(1021, 413);
            this.tabLooseDeaths.TabIndex = 0;
            this.tabLooseDeaths.Text = "Loose Deaths";
            this.tabLooseDeaths.UseVisualStyleBackColor = true;
            // 
            // dgLooseDeaths
            // 
            this.dgLooseDeaths.AllowUserToAddRows = false;
            this.dgLooseDeaths.AllowUserToDeleteRows = false;
            this.dgLooseDeaths.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLooseDeaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLooseDeaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLooseDeaths.Location = new System.Drawing.Point(3, 3);
            this.dgLooseDeaths.MultiSelect = false;
            this.dgLooseDeaths.Name = "dgLooseDeaths";
            this.dgLooseDeaths.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLooseDeaths.Size = new System.Drawing.Size(1015, 407);
            this.dgLooseDeaths.TabIndex = 1;
            this.dgLooseDeaths.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgLooseDeaths_CellDoubleClick);
            // 
            // tabDataErrors
            // 
            this.tabDataErrors.Controls.Add(this.dgDataErrors);
            this.tabDataErrors.Controls.Add(this.gbDataErrorTypes);
            this.tabDataErrors.Location = new System.Drawing.Point(4, 22);
            this.tabDataErrors.Name = "tabDataErrors";
            this.tabDataErrors.Size = new System.Drawing.Size(1035, 445);
            this.tabDataErrors.TabIndex = 11;
            this.tabDataErrors.Text = "Data Errors";
            this.tabDataErrors.UseVisualStyleBackColor = true;
            // 
            // gbDataErrorTypes
            // 
            this.gbDataErrorTypes.Controls.Add(this.btnSelectAll);
            this.gbDataErrorTypes.Controls.Add(this.btnClearAll);
            this.gbDataErrorTypes.Controls.Add(this.ckbDataErrors);
            this.gbDataErrorTypes.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDataErrorTypes.Location = new System.Drawing.Point(0, 0);
            this.gbDataErrorTypes.Name = "gbDataErrorTypes";
            this.gbDataErrorTypes.Size = new System.Drawing.Size(1035, 154);
            this.gbDataErrorTypes.TabIndex = 0;
            this.gbDataErrorTypes.TabStop = false;
            this.gbDataErrorTypes.Text = "Types of Data Error to display";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(8, 119);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 7;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.btnSelectAll_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(89, 119);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 6;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.btnClearAll_Click);
            // 
            // ckbDataErrors
            // 
            this.ckbDataErrors.CheckOnClick = true;
            this.ckbDataErrors.ColumnWidth = 225;
            this.ckbDataErrors.FormattingEnabled = true;
            this.ckbDataErrors.Location = new System.Drawing.Point(8, 19);
            this.ckbDataErrors.MultiColumn = true;
            this.ckbDataErrors.Name = "ckbDataErrors";
            this.ckbDataErrors.Size = new System.Drawing.Size(915, 94);
            this.ckbDataErrors.TabIndex = 0;
            this.ckbDataErrors.SelectedIndexChanged += new System.EventHandler(this.ckbDataErrors_SelectedIndexChanged);
            // 
            // tabOccupations
            // 
            this.tabOccupations.Controls.Add(this.dgOccupations);
            this.tabOccupations.Location = new System.Drawing.Point(4, 22);
            this.tabOccupations.Name = "tabOccupations";
            this.tabOccupations.Size = new System.Drawing.Size(1035, 445);
            this.tabOccupations.TabIndex = 10;
            this.tabOccupations.Text = "Occupations";
            this.tabOccupations.UseVisualStyleBackColor = true;
            // 
            // dgOccupations
            // 
            this.dgOccupations.AllowUserToAddRows = false;
            this.dgOccupations.AllowUserToDeleteRows = false;
            this.dgOccupations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgOccupations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOccupations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOccupations.Location = new System.Drawing.Point(0, 0);
            this.dgOccupations.MultiSelect = false;
            this.dgOccupations.Name = "dgOccupations";
            this.dgOccupations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOccupations.Size = new System.Drawing.Size(1035, 445);
            this.dgOccupations.TabIndex = 2;
            this.dgOccupations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgOccupations_CellDoubleClick);
            // 
            // tabLocations
            // 
            this.tabLocations.Controls.Add(this.btnBingOSMap);
            this.tabLocations.Controls.Add(this.btnShowMap);
            this.tabLocations.Controls.Add(this.tabCtrlLocations);
            this.tabLocations.Location = new System.Drawing.Point(4, 22);
            this.tabLocations.Name = "tabLocations";
            this.tabLocations.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocations.Size = new System.Drawing.Size(1035, 445);
            this.tabLocations.TabIndex = 4;
            this.tabLocations.Text = "Locations";
            this.tabLocations.UseVisualStyleBackColor = true;
            // 
            // btnBingOSMap
            // 
            this.btnBingOSMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBingOSMap.Location = new System.Drawing.Point(853, 1);
            this.btnBingOSMap.Name = "btnBingOSMap";
            this.btnBingOSMap.Size = new System.Drawing.Size(84, 22);
            this.btnBingOSMap.TabIndex = 3;
            this.btnBingOSMap.Text = "Show OS Map";
            this.btnBingOSMap.UseVisualStyleBackColor = true;
            this.btnBingOSMap.Click += new System.EventHandler(this.btnBingOSMap_Click);
            // 
            // btnShowMap
            // 
            this.btnShowMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowMap.Location = new System.Drawing.Point(943, 1);
            this.btnShowMap.Name = "btnShowMap";
            this.btnShowMap.Size = new System.Drawing.Size(84, 22);
            this.btnShowMap.TabIndex = 2;
            this.btnShowMap.Text = "Show Map";
            this.btnShowMap.UseVisualStyleBackColor = true;
            this.btnShowMap.Click += new System.EventHandler(this.btnShowMap_Click);
            // 
            // tabCtrlLocations
            // 
            this.tabCtrlLocations.Controls.Add(this.tabTreeView);
            this.tabCtrlLocations.Controls.Add(this.tabCountries);
            this.tabCtrlLocations.Controls.Add(this.tabRegions);
            this.tabCtrlLocations.Controls.Add(this.tabSubRegions);
            this.tabCtrlLocations.Controls.Add(this.tabAddresses);
            this.tabCtrlLocations.Controls.Add(this.tabPlaces);
            this.tabCtrlLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlLocations.Location = new System.Drawing.Point(3, 3);
            this.tabCtrlLocations.Name = "tabCtrlLocations";
            this.tabCtrlLocations.SelectedIndex = 0;
            this.tabCtrlLocations.Size = new System.Drawing.Size(1029, 439);
            this.tabCtrlLocations.TabIndex = 0;
            this.tabCtrlLocations.SelectedIndexChanged += new System.EventHandler(this.tabCtrlLocations_SelectedIndexChanged);
            this.tabCtrlLocations.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.tabCtrlLocations_Selecting);
            // 
            // tabTreeView
            // 
            this.tabTreeView.Controls.Add(this.treeViewLocations);
            this.tabTreeView.Location = new System.Drawing.Point(4, 22);
            this.tabTreeView.Name = "tabTreeView";
            this.tabTreeView.Padding = new System.Windows.Forms.Padding(3);
            this.tabTreeView.Size = new System.Drawing.Size(1021, 413);
            this.tabTreeView.TabIndex = 5;
            this.tabTreeView.Text = "Tree View";
            this.tabTreeView.UseVisualStyleBackColor = true;
            // 
            // treeViewLocations
            // 
            this.treeViewLocations.CausesValidation = false;
            this.treeViewLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeViewLocations.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeViewLocations.ImageIndex = 0;
            this.treeViewLocations.ImageList = this.imageList;
            this.treeViewLocations.Location = new System.Drawing.Point(3, 3);
            this.treeViewLocations.Name = "treeViewLocations";
            this.treeViewLocations.SelectedImageIndex = 0;
            this.treeViewLocations.ShowNodeToolTips = true;
            this.treeViewLocations.Size = new System.Drawing.Size(1015, 407);
            this.treeViewLocations.TabIndex = 0;
            this.treeViewLocations.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewLocations_BeforeCollapse);
            this.treeViewLocations.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.treeViewLocations_BeforeExpand);
            this.treeViewLocations.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.treeViewLocations_AfterSelect);
            this.treeViewLocations.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewLocations_NodeMouseClick);
            this.treeViewLocations.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeViewLocations_NodeMouseDoubleClick);
            this.treeViewLocations.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeViewLocations_MouseDown);
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "QuestionMark.png");
            this.imageList.Images.SetKeyName(1, "GoogleMatch.png");
            this.imageList.Images.SetKeyName(2, "GooglePartial.png");
            this.imageList.Images.SetKeyName(3, "Complete_OK.png");
            this.imageList.Images.SetKeyName(4, "CriticalError.png");
            this.imageList.Images.SetKeyName(5, "Flagged.png");
            this.imageList.Images.SetKeyName(6, "OutOfBounds.png");
            this.imageList.Images.SetKeyName(7, "Warning.png");
            // 
            // tabCountries
            // 
            this.tabCountries.Controls.Add(this.dgCountries);
            this.tabCountries.Location = new System.Drawing.Point(4, 22);
            this.tabCountries.Name = "tabCountries";
            this.tabCountries.Padding = new System.Windows.Forms.Padding(3);
            this.tabCountries.Size = new System.Drawing.Size(1021, 413);
            this.tabCountries.TabIndex = 0;
            this.tabCountries.Text = "Countries";
            this.tabCountries.ToolTipText = "Double click on Country name to see list of individuals with that Country.";
            this.tabCountries.UseVisualStyleBackColor = true;
            // 
            // tabRegions
            // 
            this.tabRegions.Controls.Add(this.dgRegions);
            this.tabRegions.Location = new System.Drawing.Point(4, 22);
            this.tabRegions.Name = "tabRegions";
            this.tabRegions.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegions.Size = new System.Drawing.Size(1021, 413);
            this.tabRegions.TabIndex = 1;
            this.tabRegions.Text = "Regions";
            this.tabRegions.ToolTipText = "Double click on Region name to see list of individuals with that Region.";
            this.tabRegions.UseVisualStyleBackColor = true;
            // 
            // tabSubRegions
            // 
            this.tabSubRegions.Controls.Add(this.dgSubRegions);
            this.tabSubRegions.Location = new System.Drawing.Point(4, 22);
            this.tabSubRegions.Name = "tabSubRegions";
            this.tabSubRegions.Padding = new System.Windows.Forms.Padding(3);
            this.tabSubRegions.Size = new System.Drawing.Size(1021, 413);
            this.tabSubRegions.TabIndex = 2;
            this.tabSubRegions.Text = "SubRegions";
            this.tabSubRegions.ToolTipText = "Double click on \'Parish\' name to see list of individuals with that parish/area.";
            this.tabSubRegions.UseVisualStyleBackColor = true;
            // 
            // dgSubRegions
            // 
            this.dgSubRegions.AllowUserToAddRows = false;
            this.dgSubRegions.AllowUserToDeleteRows = false;
            this.dgSubRegions.AllowUserToResizeRows = false;
            this.dgSubRegions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgSubRegions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSubRegions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSubRegions.Location = new System.Drawing.Point(3, 3);
            this.dgSubRegions.MultiSelect = false;
            this.dgSubRegions.Name = "dgSubRegions";
            this.dgSubRegions.RowHeadersVisible = false;
            this.dgSubRegions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSubRegions.Size = new System.Drawing.Size(1015, 407);
            this.dgSubRegions.TabIndex = 1;
            this.dgSubRegions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSubRegions_CellDoubleClick);
            this.dgSubRegions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgSubRegions_CellFormatting);
            // 
            // tabAddresses
            // 
            this.tabAddresses.Controls.Add(this.dgAddresses);
            this.tabAddresses.Location = new System.Drawing.Point(4, 22);
            this.tabAddresses.Name = "tabAddresses";
            this.tabAddresses.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddresses.Size = new System.Drawing.Size(1021, 413);
            this.tabAddresses.TabIndex = 3;
            this.tabAddresses.Text = "Addresses";
            this.tabAddresses.ToolTipText = "Double click on Address name to see list of individuals with that Address.";
            this.tabAddresses.UseVisualStyleBackColor = true;
            // 
            // dgAddresses
            // 
            this.dgAddresses.AllowUserToAddRows = false;
            this.dgAddresses.AllowUserToDeleteRows = false;
            this.dgAddresses.AllowUserToResizeRows = false;
            this.dgAddresses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAddresses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgAddresses.Location = new System.Drawing.Point(3, 3);
            this.dgAddresses.MultiSelect = false;
            this.dgAddresses.Name = "dgAddresses";
            this.dgAddresses.RowHeadersVisible = false;
            this.dgAddresses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAddresses.Size = new System.Drawing.Size(1015, 407);
            this.dgAddresses.TabIndex = 1;
            this.dgAddresses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAddresses_CellDoubleClick);
            this.dgAddresses.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgAddresses_CellFormatting);
            // 
            // tabPlaces
            // 
            this.tabPlaces.Controls.Add(this.dgPlaces);
            this.tabPlaces.Location = new System.Drawing.Point(4, 22);
            this.tabPlaces.Name = "tabPlaces";
            this.tabPlaces.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlaces.Size = new System.Drawing.Size(1021, 413);
            this.tabPlaces.TabIndex = 4;
            this.tabPlaces.Text = "Places";
            this.tabPlaces.ToolTipText = "Double click on Address name to see list of individuals with that Place";
            this.tabPlaces.UseVisualStyleBackColor = true;
            // 
            // dgPlaces
            // 
            this.dgPlaces.AllowUserToAddRows = false;
            this.dgPlaces.AllowUserToDeleteRows = false;
            this.dgPlaces.AllowUserToResizeRows = false;
            this.dgPlaces.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgPlaces.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPlaces.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPlaces.Location = new System.Drawing.Point(3, 3);
            this.dgPlaces.MultiSelect = false;
            this.dgPlaces.Name = "dgPlaces";
            this.dgPlaces.RowHeadersVisible = false;
            this.dgPlaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPlaces.Size = new System.Drawing.Size(1015, 407);
            this.dgPlaces.TabIndex = 2;
            this.dgPlaces.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPlaces_CellDoubleClick);
            this.dgPlaces.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgPlaces_CellFormatting);
            // 
            // tabFamilies
            // 
            this.tabFamilies.Controls.Add(this.dgFamilies);
            this.tabFamilies.Location = new System.Drawing.Point(4, 22);
            this.tabFamilies.Name = "tabFamilies";
            this.tabFamilies.Size = new System.Drawing.Size(1035, 445);
            this.tabFamilies.TabIndex = 9;
            this.tabFamilies.Text = "Families";
            this.tabFamilies.UseVisualStyleBackColor = true;
            // 
            // dgFamilies
            // 
            this.dgFamilies.AllowUserToAddRows = false;
            this.dgFamilies.AllowUserToDeleteRows = false;
            this.dgFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFamilies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFamilies.Location = new System.Drawing.Point(0, 0);
            this.dgFamilies.MultiSelect = false;
            this.dgFamilies.Name = "dgFamilies";
            this.dgFamilies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFamilies.Size = new System.Drawing.Size(1035, 445);
            this.dgFamilies.TabIndex = 1;
            this.dgFamilies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgFamilies_CellDoubleClick);
            // 
            // tabIndividuals
            // 
            this.tabIndividuals.Controls.Add(this.dgIndividuals);
            this.tabIndividuals.Location = new System.Drawing.Point(4, 22);
            this.tabIndividuals.Name = "tabIndividuals";
            this.tabIndividuals.Padding = new System.Windows.Forms.Padding(3);
            this.tabIndividuals.Size = new System.Drawing.Size(1035, 445);
            this.tabIndividuals.TabIndex = 2;
            this.tabIndividuals.Text = "Individuals";
            this.tabIndividuals.UseVisualStyleBackColor = true;
            // 
            // dgIndividuals
            // 
            this.dgIndividuals.AllowUserToAddRows = false;
            this.dgIndividuals.AllowUserToDeleteRows = false;
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgIndividuals.Location = new System.Drawing.Point(3, 3);
            this.dgIndividuals.MultiSelect = false;
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.ReadOnly = true;
            this.dgIndividuals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgIndividuals.Size = new System.Drawing.Size(1029, 439);
            this.dgIndividuals.TabIndex = 0;
            this.dgIndividuals.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgIndividuals_CellDoubleClick);
            this.dgIndividuals.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgIndividuals_MouseDown);
            // 
            // tabDisplayProgress
            // 
            this.tabDisplayProgress.Controls.Add(this.label7);
            this.tabDisplayProgress.Controls.Add(this.pbRelationships);
            this.tabDisplayProgress.Controls.Add(this.rtbOutput);
            this.tabDisplayProgress.Controls.Add(this.label6);
            this.tabDisplayProgress.Controls.Add(this.pbFamilies);
            this.tabDisplayProgress.Controls.Add(this.label5);
            this.tabDisplayProgress.Controls.Add(this.pbIndividuals);
            this.tabDisplayProgress.Controls.Add(this.label4);
            this.tabDisplayProgress.Controls.Add(this.pbSources);
            this.tabDisplayProgress.Location = new System.Drawing.Point(4, 22);
            this.tabDisplayProgress.Name = "tabDisplayProgress";
            this.tabDisplayProgress.Padding = new System.Windows.Forms.Padding(3);
            this.tabDisplayProgress.Size = new System.Drawing.Size(1035, 445);
            this.tabDisplayProgress.TabIndex = 1;
            this.tabDisplayProgress.Text = "Gedcom Stats";
            this.tabDisplayProgress.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 85);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(125, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Calculating Relationships";
            // 
            // pbRelationships
            // 
            this.pbRelationships.Location = new System.Drawing.Point(139, 82);
            this.pbRelationships.Name = "pbRelationships";
            this.pbRelationships.Size = new System.Drawing.Size(316, 16);
            this.pbRelationships.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Loading Families";
            // 
            // pbFamilies
            // 
            this.pbFamilies.Location = new System.Drawing.Point(139, 60);
            this.pbFamilies.Name = "pbFamilies";
            this.pbFamilies.Size = new System.Drawing.Size(316, 16);
            this.pbFamilies.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 41);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Loading Individuals";
            // 
            // pbIndividuals
            // 
            this.pbIndividuals.Location = new System.Drawing.Point(139, 38);
            this.pbIndividuals.Name = "pbIndividuals";
            this.pbIndividuals.Size = new System.Drawing.Size(316, 16);
            this.pbIndividuals.TabIndex = 2;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Loading Sources";
            // 
            // pbSources
            // 
            this.pbSources.Location = new System.Drawing.Point(139, 16);
            this.pbSources.Name = "pbSources";
            this.pbSources.Size = new System.Drawing.Size(316, 16);
            this.pbSources.TabIndex = 0;
            // 
            // tabSelector
            // 
            this.tabSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSelector.Controls.Add(this.tabDisplayProgress);
            this.tabSelector.Controls.Add(this.tabIndividuals);
            this.tabSelector.Controls.Add(this.tabFamilies);
            this.tabSelector.Controls.Add(this.tabSurnames);
            this.tabSelector.Controls.Add(this.tabLocations);
            this.tabSelector.Controls.Add(this.tabOccupations);
            this.tabSelector.Controls.Add(this.tabFacts);
            this.tabSelector.Controls.Add(this.tabDataErrors);
            this.tabSelector.Controls.Add(this.tabDuplicates);
            this.tabSelector.Controls.Add(this.tabLooseBirthDeaths);
            this.tabSelector.Controls.Add(this.tabCensus);
            this.tabSelector.Controls.Add(this.tabLostCousins);
            this.tabSelector.Controls.Add(this.tabColourReports);
            this.tabSelector.Controls.Add(this.tabTreetops);
            this.tabSelector.Controls.Add(this.tabWorldWars);
            this.tabSelector.Location = new System.Drawing.Point(0, 27);
            this.tabSelector.Name = "tabSelector";
            this.tabSelector.SelectedIndex = 0;
            this.tabSelector.Size = new System.Drawing.Size(1043, 471);
            this.tabSelector.TabIndex = 9;
            this.tabSelector.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabSurnames
            // 
            this.tabSurnames.Controls.Add(this.dgSurnames);
            this.tabSurnames.Location = new System.Drawing.Point(4, 22);
            this.tabSurnames.Name = "tabSurnames";
            this.tabSurnames.Padding = new System.Windows.Forms.Padding(3);
            this.tabSurnames.Size = new System.Drawing.Size(1035, 445);
            this.tabSurnames.TabIndex = 14;
            this.tabSurnames.Text = "Surnames";
            this.tabSurnames.UseVisualStyleBackColor = true;
            // 
            // dgSurnames
            // 
            this.dgSurnames.AllowUserToAddRows = false;
            this.dgSurnames.AllowUserToDeleteRows = false;
            this.dgSurnames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSurnames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Surname,
            this.URI,
            this.Individuals,
            this.Families,
            this.Marriages});
            this.dgSurnames.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSurnames.Location = new System.Drawing.Point(3, 3);
            this.dgSurnames.MultiSelect = false;
            this.dgSurnames.Name = "dgSurnames";
            this.dgSurnames.ReadOnly = true;
            this.dgSurnames.RowHeadersWidth = 20;
            this.dgSurnames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSurnames.Size = new System.Drawing.Size(1029, 439);
            this.dgSurnames.TabIndex = 1;
            this.dgSurnames.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSurnames_CellContentClick);
            this.dgSurnames.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSurnames_CellDoubleClick);
            this.dgSurnames.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dgSurnames_DataBindingComplete);
            // 
            // Surname
            // 
            this.Surname.DataPropertyName = "Surname";
            this.Surname.HeaderText = "Surname";
            this.Surname.Name = "Surname";
            this.Surname.ReadOnly = true;
            // 
            // URI
            // 
            this.URI.DataPropertyName = "URI";
            this.URI.HeaderText = "Link";
            this.URI.MinimumWidth = 100;
            this.URI.Name = "URI";
            this.URI.ReadOnly = true;
            this.URI.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.URI.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.URI.Visible = false;
            this.URI.Width = 200;
            // 
            // Individuals
            // 
            this.Individuals.DataPropertyName = "Individuals";
            this.Individuals.HeaderText = "Individuals";
            this.Individuals.Name = "Individuals";
            this.Individuals.ReadOnly = true;
            // 
            // Families
            // 
            this.Families.DataPropertyName = "Families";
            this.Families.HeaderText = "Families";
            this.Families.Name = "Families";
            this.Families.ReadOnly = true;
            // 
            // Marriages
            // 
            this.Marriages.DataPropertyName = "Marriages";
            this.Marriages.HeaderText = "Marriages";
            this.Marriages.Name = "Marriages";
            this.Marriages.ReadOnly = true;
            // 
            // tabFacts
            // 
            this.tabFacts.Controls.Add(this.btnDeselectAllFactTypes);
            this.tabFacts.Controls.Add(this.btnSelectAllFactTypes);
            this.tabFacts.Controls.Add(this.ckbFactSelect);
            this.tabFacts.Controls.Add(this.btnShowFacts);
            this.tabFacts.Controls.Add(this.label3);
            this.tabFacts.Controls.Add(this.txtFactsSurname);
            this.tabFacts.Controls.Add(this.relTypesFacts);
            this.tabFacts.Location = new System.Drawing.Point(4, 22);
            this.tabFacts.Name = "tabFacts";
            this.tabFacts.Size = new System.Drawing.Size(1035, 445);
            this.tabFacts.TabIndex = 13;
            this.tabFacts.Text = "Facts";
            this.tabFacts.UseVisualStyleBackColor = true;
            // 
            // btnDeselectAllFactTypes
            // 
            this.btnDeselectAllFactTypes.Location = new System.Drawing.Point(185, 93);
            this.btnDeselectAllFactTypes.Name = "btnDeselectAllFactTypes";
            this.btnDeselectAllFactTypes.Size = new System.Drawing.Size(136, 23);
            this.btnDeselectAllFactTypes.TabIndex = 27;
            this.btnDeselectAllFactTypes.Text = "De-select all Fact Types";
            this.btnDeselectAllFactTypes.UseVisualStyleBackColor = true;
            this.btnDeselectAllFactTypes.Click += new System.EventHandler(this.btnDeselectAllFactTypes_Click);
            // 
            // btnSelectAllFactTypes
            // 
            this.btnSelectAllFactTypes.Location = new System.Drawing.Point(8, 92);
            this.btnSelectAllFactTypes.Name = "btnSelectAllFactTypes";
            this.btnSelectAllFactTypes.Size = new System.Drawing.Size(136, 23);
            this.btnSelectAllFactTypes.TabIndex = 26;
            this.btnSelectAllFactTypes.Text = "Select all Fact Types";
            this.btnSelectAllFactTypes.UseVisualStyleBackColor = true;
            this.btnSelectAllFactTypes.Click += new System.EventHandler(this.btnSelectAllFactTypes_Click);
            // 
            // ckbFactSelect
            // 
            this.ckbFactSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbFactSelect.FormattingEnabled = true;
            this.ckbFactSelect.Location = new System.Drawing.Point(8, 122);
            this.ckbFactSelect.Name = "ckbFactSelect";
            this.ckbFactSelect.ScrollAlwaysVisible = true;
            this.ckbFactSelect.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ckbFactSelect.Size = new System.Drawing.Size(313, 304);
            this.ckbFactSelect.TabIndex = 25;
            this.ckbFactSelect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.ckbFactSelect_MouseClick);
            // 
            // btnShowFacts
            // 
            this.btnShowFacts.Location = new System.Drawing.Point(333, 51);
            this.btnShowFacts.Name = "btnShowFacts";
            this.btnShowFacts.Size = new System.Drawing.Size(262, 23);
            this.btnShowFacts.TabIndex = 24;
            this.btnShowFacts.Text = "Show Facts for Individuals with Selected Fact Types";
            this.btnShowFacts.UseVisualStyleBackColor = true;
            this.btnShowFacts.Click += new System.EventHandler(this.btnShowFacts_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(339, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Surname";
            // 
            // txtFactsSurname
            // 
            this.txtFactsSurname.Location = new System.Drawing.Point(394, 16);
            this.txtFactsSurname.Name = "txtFactsSurname";
            this.txtFactsSurname.Size = new System.Drawing.Size(201, 20);
            this.txtFactsSurname.TabIndex = 22;
            // 
            // tabDuplicates
            // 
            this.tabDuplicates.Controls.Add(this.dgDuplicates);
            this.tabDuplicates.Controls.Add(this.btnCancelDuplicates);
            this.tabDuplicates.Controls.Add(this.label16);
            this.tabDuplicates.Controls.Add(this.labDuplicateSlider);
            this.tabDuplicates.Controls.Add(this.label13);
            this.tabDuplicates.Controls.Add(this.label12);
            this.tabDuplicates.Controls.Add(this.tbDuplicateScore);
            this.tabDuplicates.Controls.Add(this.labCalcDuplicates);
            this.tabDuplicates.Controls.Add(this.pbDuplicates);
            this.tabDuplicates.Location = new System.Drawing.Point(4, 22);
            this.tabDuplicates.Name = "tabDuplicates";
            this.tabDuplicates.Padding = new System.Windows.Forms.Padding(3);
            this.tabDuplicates.Size = new System.Drawing.Size(1035, 445);
            this.tabDuplicates.TabIndex = 15;
            this.tabDuplicates.Text = "Duplicates?";
            this.tabDuplicates.UseVisualStyleBackColor = true;
            // 
            // dgDuplicates
            // 
            this.dgDuplicates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDuplicates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDuplicates.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.NonDuplicate,
            this.Score,
            this.DuplicateIndividualID,
            this.DuplicatesName,
            this.DuplicateForenames,
            this.DuplicateSurname,
            this.DuplicateBirthDate,
            this.DuplicateBirthLocation,
            this.MatchIndividualID,
            this.MatchName,
            this.MatchBirthDate,
            this.MatchBirthLocation});
            this.dgDuplicates.Location = new System.Drawing.Point(0, 67);
            this.dgDuplicates.Name = "dgDuplicates";
            this.dgDuplicates.RowHeadersWidth = 15;
            this.dgDuplicates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDuplicates.Size = new System.Drawing.Size(1032, 375);
            this.dgDuplicates.TabIndex = 18;
            this.dgDuplicates.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDuplicates_CellContentClick);
            this.dgDuplicates.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgDuplicates_CellDoubleClick);
            // 
            // btnCancelDuplicates
            // 
            this.btnCancelDuplicates.Image = global::FTAnalyzer.Properties.Resources.CriticalError;
            this.btnCancelDuplicates.Location = new System.Drawing.Point(411, 6);
            this.btnCancelDuplicates.Name = "btnCancelDuplicates";
            this.btnCancelDuplicates.Size = new System.Drawing.Size(26, 26);
            this.btnCancelDuplicates.TabIndex = 17;
            this.btnCancelDuplicates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelDuplicates.UseVisualStyleBackColor = true;
            this.btnCancelDuplicates.Visible = false;
            this.btnCancelDuplicates.Click += new System.EventHandler(this.btnCancelDuplicates_Click);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(427, 51);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(152, 13);
            this.label16.TabIndex = 16;
            this.label16.Text = "Candidate Duplicates List";
            // 
            // labDuplicateSlider
            // 
            this.labDuplicateSlider.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labDuplicateSlider.AutoSize = true;
            this.labDuplicateSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDuplicateSlider.Location = new System.Drawing.Point(671, 27);
            this.labDuplicateSlider.Name = "labDuplicateSlider";
            this.labDuplicateSlider.Size = new System.Drawing.Size(168, 13);
            this.labDuplicateSlider.TabIndex = 14;
            this.labDuplicateSlider.Text = "Duplicates Match Quality : 1";
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(940, 27);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "Aggressive Match";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(440, 27);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 12;
            this.label12.Text = "Loose Match";
            // 
            // labCalcDuplicates
            // 
            this.labCalcDuplicates.AutoSize = true;
            this.labCalcDuplicates.Location = new System.Drawing.Point(3, 13);
            this.labCalcDuplicates.Name = "labCalcDuplicates";
            this.labCalcDuplicates.Size = new System.Drawing.Size(112, 13);
            this.labCalcDuplicates.TabIndex = 10;
            this.labCalcDuplicates.Text = "Calculating Duplicates";
            // 
            // pbDuplicates
            // 
            this.pbDuplicates.Location = new System.Drawing.Point(121, 9);
            this.pbDuplicates.Name = "pbDuplicates";
            this.pbDuplicates.Size = new System.Drawing.Size(283, 23);
            this.pbDuplicates.TabIndex = 9;
            // 
            // saveDatabase
            // 
            this.saveDatabase.DefaultExt = "zip";
            this.saveDatabase.Filter = "Zip Files | *.zip";
            // 
            // restoreDatabase
            // 
            this.restoreDatabase.FileName = "*.zip";
            this.restoreDatabase.Filter = "Gecode Databases | *.s3db | Zip Files | *.zip";
            // 
            // rtbOutput
            // 
            this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbOutput.Location = new System.Drawing.Point(3, 112);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(1029, 327);
            this.rtbOutput.TabIndex = 6;
            this.rtbOutput.Text = "";
            this.rtbOutput.TextChanged += new System.EventHandler(this.rtbOutput_TextChanged);
            // 
            // relTypesFacts
            // 
            this.relTypesFacts.Location = new System.Drawing.Point(8, 8);
            this.relTypesFacts.MarriedToDB = true;
            this.relTypesFacts.Name = "relTypesFacts";
            this.relTypesFacts.Size = new System.Drawing.Size(325, 78);
            this.relTypesFacts.TabIndex = 21;
            // 
            // cenDate
            // 
            this.cenDate.AutoSize = true;
            this.cenDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cenDate.Country = "";
            this.cenDate.Location = new System.Drawing.Point(14, 92);
            this.cenDate.Name = "cenDate";
            this.cenDate.Size = new System.Drawing.Size(186, 27);
            this.cenDate.TabIndex = 17;
            // 
            // relTypesCensus
            // 
            this.relTypesCensus.Location = new System.Drawing.Point(8, 8);
            this.relTypesCensus.MarriedToDB = true;
            this.relTypesCensus.Name = "relTypesCensus";
            this.relTypesCensus.Size = new System.Drawing.Size(325, 78);
            this.relTypesCensus.TabIndex = 15;
            // 
            // relTypesLC
            // 
            this.relTypesLC.Location = new System.Drawing.Point(8, 8);
            this.relTypesLC.MarriedToDB = true;
            this.relTypesLC.Name = "relTypesLC";
            this.relTypesLC.Size = new System.Drawing.Size(325, 78);
            this.relTypesLC.TabIndex = 19;
            this.relTypesLC.RelationTypesChanged += new System.EventHandler(this.relTypesLC_RelationTypesChanged);
            // 
            // relTypesColoured
            // 
            this.relTypesColoured.Location = new System.Drawing.Point(8, 8);
            this.relTypesColoured.MarriedToDB = true;
            this.relTypesColoured.Name = "relTypesColoured";
            this.relTypesColoured.Size = new System.Drawing.Size(325, 78);
            this.relTypesColoured.TabIndex = 26;
            // 
            // treetopsRelation
            // 
            this.treetopsRelation.Location = new System.Drawing.Point(270, 12);
            this.treetopsRelation.MarriedToDB = true;
            this.treetopsRelation.Name = "treetopsRelation";
            this.treetopsRelation.Size = new System.Drawing.Size(322, 74);
            this.treetopsRelation.TabIndex = 12;
            // 
            // treetopsCountry
            // 
            this.treetopsCountry.Location = new System.Drawing.Point(8, 12);
            this.treetopsCountry.Name = "treetopsCountry";
            this.treetopsCountry.Size = new System.Drawing.Size(256, 74);
            this.treetopsCountry.TabIndex = 11;
            this.treetopsCountry.Title = "Default Country";
            this.treetopsCountry.UKEnabled = true;
            // 
            // wardeadRelation
            // 
            this.wardeadRelation.Location = new System.Drawing.Point(270, 12);
            this.wardeadRelation.MarriedToDB = true;
            this.wardeadRelation.Name = "wardeadRelation";
            this.wardeadRelation.Size = new System.Drawing.Size(322, 74);
            this.wardeadRelation.TabIndex = 26;
            // 
            // wardeadCountry
            // 
            this.wardeadCountry.Location = new System.Drawing.Point(8, 12);
            this.wardeadCountry.Name = "wardeadCountry";
            this.wardeadCountry.Size = new System.Drawing.Size(256, 74);
            this.wardeadCountry.TabIndex = 25;
            this.wardeadCountry.Title = "Default Country";
            this.wardeadCountry.UKEnabled = true;
            // 
            // NonDuplicate
            // 
            this.NonDuplicate.DataPropertyName = "NonDuplicate";
            this.NonDuplicate.FalseValue = "False";
            this.NonDuplicate.HeaderText = "Ignore";
            this.NonDuplicate.Name = "NonDuplicate";
            this.NonDuplicate.TrueValue = "True";
            this.NonDuplicate.Width = 40;
            // 
            // Score
            // 
            this.Score.DataPropertyName = "Score";
            this.Score.HeaderText = "Score";
            this.Score.Name = "Score";
            this.Score.Width = 40;
            // 
            // DuplicateIndividualID
            // 
            this.DuplicateIndividualID.DataPropertyName = "IndividualID";
            this.DuplicateIndividualID.HeaderText = "ID";
            this.DuplicateIndividualID.Name = "DuplicateIndividualID";
            this.DuplicateIndividualID.Width = 50;
            // 
            // DuplicatesName
            // 
            this.DuplicatesName.DataPropertyName = "Name";
            this.DuplicatesName.HeaderText = "Name";
            this.DuplicatesName.MinimumWidth = 50;
            this.DuplicatesName.Name = "DuplicatesName";
            this.DuplicatesName.Width = 150;
            // 
            // DuplicateForenames
            // 
            this.DuplicateForenames.DataPropertyName = "Forenames";
            this.DuplicateForenames.HeaderText = "Forenames";
            this.DuplicateForenames.Name = "DuplicateForenames";
            this.DuplicateForenames.Visible = false;
            // 
            // DuplicateSurname
            // 
            this.DuplicateSurname.DataPropertyName = "Surname";
            this.DuplicateSurname.HeaderText = "Surname";
            this.DuplicateSurname.Name = "DuplicateSurname";
            this.DuplicateSurname.Visible = false;
            // 
            // DuplicateBirthDate
            // 
            this.DuplicateBirthDate.DataPropertyName = "BirthDate";
            this.DuplicateBirthDate.HeaderText = "Birthdate";
            this.DuplicateBirthDate.MinimumWidth = 50;
            this.DuplicateBirthDate.Name = "DuplicateBirthDate";
            this.DuplicateBirthDate.Width = 150;
            // 
            // DuplicateBirthLocation
            // 
            this.DuplicateBirthLocation.DataPropertyName = "BirthLocation";
            this.DuplicateBirthLocation.HeaderText = "Birth Location";
            this.DuplicateBirthLocation.MinimumWidth = 100;
            this.DuplicateBirthLocation.Name = "DuplicateBirthLocation";
            this.DuplicateBirthLocation.Width = 175;
            // 
            // MatchIndividualID
            // 
            this.MatchIndividualID.DataPropertyName = "MatchIndividualID";
            this.MatchIndividualID.HeaderText = "Match ID";
            this.MatchIndividualID.Name = "MatchIndividualID";
            this.MatchIndividualID.Width = 50;
            // 
            // MatchName
            // 
            this.MatchName.DataPropertyName = "MatchName";
            this.MatchName.HeaderText = "Match Name";
            this.MatchName.MinimumWidth = 50;
            this.MatchName.Name = "MatchName";
            this.MatchName.Width = 150;
            // 
            // MatchBirthDate
            // 
            this.MatchBirthDate.DataPropertyName = "MatchBirthDate";
            this.MatchBirthDate.HeaderText = "Match Birthdate";
            this.MatchBirthDate.MinimumWidth = 50;
            this.MatchBirthDate.Name = "MatchBirthDate";
            this.MatchBirthDate.Width = 150;
            // 
            // MatchBirthLocation
            // 
            this.MatchBirthLocation.DataPropertyName = "MatchBirthLocation";
            this.MatchBirthLocation.HeaderText = "Match Birth Location";
            this.MatchBirthLocation.MinimumWidth = 100;
            this.MatchBirthLocation.Name = "MatchBirthLocation";
            this.MatchBirthLocation.Width = 175;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1043, 523);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(600, 400);
            this.Name = "MainForm";
            this.Text = "Family Tree Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainForm_DragEnter);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mnuSetRoot.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDataErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCountries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDuplicateScore)).EndInit();
            this.tabWorldWars.ResumeLayout(false);
            this.tabWorldWars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorldWars)).EndInit();
            this.tabTreetops.ResumeLayout(false);
            this.tabTreetops.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTreeTops)).EndInit();
            this.tabColourReports.ResumeLayout(false);
            this.tabColourReports.PerformLayout();
            this.tabLostCousins.ResumeLayout(false);
            this.tabLostCousins.PerformLayout();
            this.tabCensus.ResumeLayout(false);
            this.tabCensus.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).EndInit();
            this.tabLooseBirthDeaths.ResumeLayout(false);
            this.tabCtrlLooseBDs.ResumeLayout(false);
            this.tabLooseBirths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseBirths)).EndInit();
            this.tabLooseDeaths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).EndInit();
            this.tabDataErrors.ResumeLayout(false);
            this.gbDataErrorTypes.ResumeLayout(false);
            this.tabOccupations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOccupations)).EndInit();
            this.tabLocations.ResumeLayout(false);
            this.tabCtrlLocations.ResumeLayout(false);
            this.tabTreeView.ResumeLayout(false);
            this.tabCountries.ResumeLayout(false);
            this.tabRegions.ResumeLayout(false);
            this.tabSubRegions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSubRegions)).EndInit();
            this.tabAddresses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAddresses)).EndInit();
            this.tabPlaces.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPlaces)).EndInit();
            this.tabFamilies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).EndInit();
            this.tabIndividuals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            this.tabDisplayProgress.ResumeLayout(false);
            this.tabDisplayProgress.PerformLayout();
            this.tabSelector.ResumeLayout(false);
            this.tabSurnames.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSurnames)).EndInit();
            this.tabFacts.ResumeLayout(false);
            this.tabFacts.PerformLayout();
            this.tabDuplicates.ResumeLayout(false);
            this.tabDuplicates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDuplicates)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openGedcom;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tsCount;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsCountLabel;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.ToolStripMenuItem mnuReports;
        //private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.ContextMenuStrip mnuSetRoot;
        private System.Windows.Forms.ToolStripMenuItem setAsRootToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuChildAgeProfiles;
        private System.Windows.Forms.ToolStripMenuItem viewOnlineManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuOlderParents;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripMenuItem mnuIndividualsToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuFamiliesToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuFactsToExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem displayOptionsOnLoadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem reportAnIssueToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuReload;
        private System.Windows.Forms.ToolStripStatusLabel tsHintsLabel;
        private System.Windows.Forms.TabPage tabWorldWars;
        private System.Windows.Forms.CheckBox ckbWDIgnoreLocations;
        private System.Windows.Forms.Button btnWWII;
        private System.Windows.Forms.Button btnWWI;
        private System.Windows.Forms.DataGridView dgWorldWars;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWorldWarsSurname;
        private Controls.RelationTypes wardeadRelation;
        private Controls.CensusCountry wardeadCountry;
        private System.Windows.Forms.TabPage tabTreetops;
        private System.Windows.Forms.DataGridView dgTreeTops;
        private System.Windows.Forms.CheckBox ckbTTIgnoreLocations;
        private System.Windows.Forms.Button btnTreeTops;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTreetopsSurname;
        private Controls.RelationTypes treetopsRelation;
        private Controls.CensusCountry treetopsCountry;
        private System.Windows.Forms.TabPage tabColourReports;
        private System.Windows.Forms.Button btnColourBMD;
        private System.Windows.Forms.Button btnColourCensus;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtColouredSurname;
        private Controls.RelationTypes relTypesColoured;
        private System.Windows.Forms.TabPage tabLostCousins;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button btnLC1911EW;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.CheckBox ckbShowLCEntered;
        private System.Windows.Forms.Button btnLC1841EW;
        private System.Windows.Forms.Button btnLC1911Ireland;
        private System.Windows.Forms.Button btnLC1880USA;
        private System.Windows.Forms.Button btnLC1881EW;
        private System.Windows.Forms.Button btnLC1881Canada;
        private System.Windows.Forms.Button btnLC1881Scot;
        private System.Windows.Forms.TabPage tabCensus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udAgeFilter;
        private Controls.CensusDateSelector cenDate;
        private Controls.RelationTypes relTypesCensus;
        private System.Windows.Forms.TabPage tabLooseBirthDeaths;
        private System.Windows.Forms.TabPage tabDataErrors;
        private System.Windows.Forms.DataGridView dgDataErrors;
        private System.Windows.Forms.GroupBox gbDataErrorTypes;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.CheckedListBox ckbDataErrors;
        private System.Windows.Forms.TabPage tabOccupations;
        private System.Windows.Forms.DataGridView dgOccupations;
        private System.Windows.Forms.TabPage tabLocations;
        private System.Windows.Forms.Button btnShowMap;
        private System.Windows.Forms.TabControl tabCtrlLocations;
        private System.Windows.Forms.TabPage tabTreeView;
        private System.Windows.Forms.TreeView treeViewLocations;
        private System.Windows.Forms.TabPage tabCountries;
        private System.Windows.Forms.DataGridView dgCountries;
        private System.Windows.Forms.TabPage tabRegions;
        private System.Windows.Forms.DataGridView dgRegions;
        private System.Windows.Forms.TabPage tabSubRegions;
        private System.Windows.Forms.DataGridView dgSubRegions;
        private System.Windows.Forms.TabPage tabAddresses;
        private System.Windows.Forms.DataGridView dgAddresses;
        private System.Windows.Forms.TabPage tabPlaces;
        private System.Windows.Forms.DataGridView dgPlaces;
        private System.Windows.Forms.TabPage tabFamilies;
        private System.Windows.Forms.DataGridView dgFamilies;
        private System.Windows.Forms.TabPage tabIndividuals;
        private System.Windows.Forms.DataGridView dgIndividuals;
        private System.Windows.Forms.TabPage tabDisplayProgress;
        private Utilities.ScrollingRichTextBox rtbOutput;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar pbFamilies;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar pbIndividuals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar pbSources;
        private System.Windows.Forms.TabControl tabSelector;
        private System.Windows.Forms.ToolStripMenuItem mnuMaps;
        private System.Windows.Forms.Button btnBingOSMap;
        private System.Windows.Forms.ToolStripMenuItem mnuShowTimeline;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem whatsNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuLocationsGeocodeReport;
        private System.Windows.Forms.RichTextBox rtbLostCousins;
        private System.Windows.Forms.Button btnLC1940USA;
        private System.Windows.Forms.ToolStripMenuItem mnuGeocodeLocations;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRestore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.SaveFileDialog saveDatabase;
        private System.Windows.Forms.OpenFileDialog restoreDatabase;
        private System.Windows.Forms.TabControl tabCtrlLooseBDs;
        private System.Windows.Forms.TabPage tabLooseDeaths;
        private System.Windows.Forms.DataGridView dgLooseDeaths;
        private System.Windows.Forms.TabPage tabLooseBirths;
        private System.Windows.Forms.DataGridView dgLooseBirths;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem clearRecentFileListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent1;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent2;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent3;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent4;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent5;
        private Controls.RelationTypes relTypesLC;
        private System.Windows.Forms.Button btnLCMissingCountry;
        private System.Windows.Forms.Button btnLCDuplicates;
        private System.Windows.Forms.Button btnLCnoCensus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDuplicateCensus;
        private System.Windows.Forms.Button btnMissingCensusLocation;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnShowCensusEntered;
        private System.Windows.Forms.Button btnShowCensusMissing;
        private System.Windows.Forms.ToolStripMenuItem buildLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuLifelines;
        private System.Windows.Forms.ToolStripMenuItem resetToDefaultFormSizeToolStripMenuItem;
        private System.Windows.Forms.TabPage tabFacts;
        private System.Windows.Forms.Button btnShowFacts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFactsSurname;
        private Controls.RelationTypes relTypesFacts;
        private System.Windows.Forms.ToolStripMenuItem mnuPlaces;
        private System.Windows.Forms.TabPage tabSurnames;
        private System.Windows.Forms.DataGridView dgSurnames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewLinkColumn URI;
        private System.Windows.Forms.DataGridViewTextBoxColumn Individuals;
        private System.Windows.Forms.DataGridViewTextBoxColumn Families;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marriages;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar pbRelationships;
        private System.Windows.Forms.TabPage tabDuplicates;
        private System.Windows.Forms.Label labCalcDuplicates;
        private System.Windows.Forms.ProgressBar pbDuplicates;
        private System.Windows.Forms.Label labDuplicateSlider;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar tbDuplicateScore;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Button btnCancelDuplicates;
        private System.Windows.Forms.CheckedListBox ckbFactSelect;
        private System.Windows.Forms.Button btnDeselectAllFactTypes;
        private System.Windows.Forms.Button btnSelectAllFactTypes;
        private System.Windows.Forms.DataGridView dgDuplicates;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NonDuplicate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateIndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicatesName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateForenames;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateSurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateBirthLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchIndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchBirthLocation;
    }
}

