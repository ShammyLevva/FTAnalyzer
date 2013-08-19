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
            this.openGedcom = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.BirthRegistrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deathRegistrationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marriageRegistrationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.childAgeProfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.olderParentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.individualsToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.familiesToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.factsToExcelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.geocodeLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOnlineManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSelector = new System.Windows.Forms.TabControl();
            this.tabDisplayProgress = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
            this.rtbOutput = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pbFamilies = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.pbIndividuals = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.pbSources = new System.Windows.Forms.ProgressBar();
            this.tabIndividuals = new System.Windows.Forms.TabPage();
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.tabFamilies = new System.Windows.Forms.TabPage();
            this.dgFamilies = new System.Windows.Forms.DataGridView();
            this.tabLocations = new System.Windows.Forms.TabPage();
            this.btnBingOSMap = new System.Windows.Forms.Button();
            this.btnShowMap = new System.Windows.Forms.Button();
            this.tabCtrlLocations = new System.Windows.Forms.TabControl();
            this.tabCountries = new System.Windows.Forms.TabPage();
            this.dgCountries = new System.Windows.Forms.DataGridView();
            this.tabRegions = new System.Windows.Forms.TabPage();
            this.dgRegions = new System.Windows.Forms.DataGridView();
            this.tabParishes = new System.Windows.Forms.TabPage();
            this.dgParishes = new System.Windows.Forms.DataGridView();
            this.tabAddresses = new System.Windows.Forms.TabPage();
            this.dgAddresses = new System.Windows.Forms.DataGridView();
            this.tabPlaces = new System.Windows.Forms.TabPage();
            this.dgPlaces = new System.Windows.Forms.DataGridView();
            this.tabOccupations = new System.Windows.Forms.TabPage();
            this.dgOccupations = new System.Windows.Forms.DataGridView();
            this.tabDataErrors = new System.Windows.Forms.TabPage();
            this.dgDataErrors = new System.Windows.Forms.DataGridView();
            this.gbDataErrorTypes = new System.Windows.Forms.GroupBox();
            this.ckbDataErrors = new System.Windows.Forms.CheckedListBox();
            this.tabLooseDeaths = new System.Windows.Forms.TabPage();
            this.dgLooseDeaths = new System.Windows.Forms.DataGridView();
            this.tabCensus = new System.Windows.Forms.TabPage();
            this.btnLCReport2 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.ckbNoLocations = new System.Windows.Forms.CheckBox();
            this.ckbCensusResidence = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.udAgeFilter = new System.Windows.Forms.NumericUpDown();
            this.btnShowResults = new System.Windows.Forms.Button();
            this.cenDate = new Controls.CensusDateSelector();
            this.censusCountry = new Controls.CensusCountry();
            this.relationTypes = new Controls.RelationTypes();
            this.tabLostCousins = new System.Windows.Forms.TabPage();
            this.btnLCReport = new System.Windows.Forms.Button();
            this.ckbLCIgnoreCountry = new System.Windows.Forms.CheckBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.btnLC1911EW = new System.Windows.Forms.Button();
            this.ckbLCResidence = new System.Windows.Forms.CheckBox();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.ckbHideRecorded = new System.Windows.Forms.CheckBox();
            this.ckbRestrictions = new System.Windows.Forms.CheckBox();
            this.btnLC1841EW = new System.Windows.Forms.Button();
            this.btnLC1911Ireland = new System.Windows.Forms.Button();
            this.btnLC1880USA = new System.Windows.Forms.Button();
            this.btnLC1881EW = new System.Windows.Forms.Button();
            this.btnLC1881Canada = new System.Windows.Forms.Button();
            this.btnLC1881Scot = new System.Windows.Forms.Button();
            this.tabFamilySearch = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtFamilySearchSurname = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbFamilySearchRegion = new System.Windows.Forms.RadioButton();
            this.rbFamilySearchCountry = new System.Windows.Forms.RadioButton();
            this.btnCancelFamilySearch = new System.Windows.Forms.Button();
            this.btnViewResults = new System.Windows.Forms.Button();
            this.pbFamilySearch = new System.Windows.Forms.ProgressBar();
            this.btnFamilySearchMarriageSearch = new System.Windows.Forms.Button();
            this.btnFamilySearchChildrenSearch = new System.Windows.Forms.Button();
            this.btnFamilySearchFolderBrowse = new System.Windows.Forms.Button();
            this.txtFamilySearchfolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbFamilySearchResults = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.FamilySearchrelationTypes = new Controls.RelationTypes();
            this.FamilySearchDefaultCountry = new Controls.CensusCountry();
            this.tabTreetops = new System.Windows.Forms.TabPage();
            this.dgTreeTops = new System.Windows.Forms.DataGridView();
            this.ckbTTIgnoreLocations = new System.Windows.Forms.CheckBox();
            this.btnTreeTops = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTreetopsSurname = new System.Windows.Forms.TextBox();
            this.treetopsRelation = new Controls.RelationTypes();
            this.treetopsCountry = new Controls.CensusCountry();
            this.tabWarDead = new System.Windows.Forms.TabPage();
            this.ckbWDIgnoreLocations = new System.Windows.Forms.CheckBox();
            this.btnWWII = new System.Windows.Forms.Button();
            this.btnWWI = new System.Windows.Forms.Button();
            this.dgWarDead = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWarDeadSurname = new System.Windows.Forms.TextBox();
            this.wardeadRelation = new Controls.RelationTypes();
            this.wardeadCountry = new Controls.CensusCountry();
            this.mnuSetRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.menuStrip1.SuspendLayout();
            this.tabSelector.SuspendLayout();
            this.tabDisplayProgress.SuspendLayout();
            this.tabIndividuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.tabFamilies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).BeginInit();
            this.tabLocations.SuspendLayout();
            this.tabCtrlLocations.SuspendLayout();
            this.tabCountries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCountries)).BeginInit();
            this.tabRegions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegions)).BeginInit();
            this.tabParishes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgParishes)).BeginInit();
            this.tabAddresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAddresses)).BeginInit();
            this.tabPlaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPlaces)).BeginInit();
            this.tabOccupations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOccupations)).BeginInit();
            this.tabDataErrors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDataErrors)).BeginInit();
            this.gbDataErrorTypes.SuspendLayout();
            this.tabLooseDeaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).BeginInit();
            this.tabCensus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).BeginInit();
            this.tabLostCousins.SuspendLayout();
            this.tabFamilySearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabTreetops.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTreeTops)).BeginInit();
            this.tabWarDead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWarDead)).BeginInit();
            this.mnuSetRoot.SuspendLayout();
            this.statusStrip.SuspendLayout();
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
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(939, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.mnuPrint,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Enabled = false;
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(103, 22);
            this.mnuPrint.Text = "Print";
            this.mnuPrint.Click += new System.EventHandler(this.mnuPrint_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(100, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // mnuReports
            // 
            this.mnuReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BirthRegistrationToolStripMenuItem,
            this.deathRegistrationsToolStripMenuItem,
            this.marriageRegistrationsToolStripMenuItem,
            this.toolStripSeparator2,
            this.childAgeProfilesToolStripMenuItem,
            this.olderParentsToolStripMenuItem});
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Size = new System.Drawing.Size(59, 20);
            this.mnuReports.Text = "Reports";
            this.mnuReports.Visible = false;
            // 
            // BirthRegistrationToolStripMenuItem
            // 
            this.BirthRegistrationToolStripMenuItem.Name = "BirthRegistrationToolStripMenuItem";
            this.BirthRegistrationToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            // 
            // deathRegistrationsToolStripMenuItem
            // 
            this.deathRegistrationsToolStripMenuItem.Name = "deathRegistrationsToolStripMenuItem";
            this.deathRegistrationsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            // 
            // marriageRegistrationsToolStripMenuItem
            // 
            this.marriageRegistrationsToolStripMenuItem.Name = "marriageRegistrationsToolStripMenuItem";
            this.marriageRegistrationsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(165, 6);
            this.toolStripSeparator2.Visible = false;
            // 
            // childAgeProfilesToolStripMenuItem
            // 
            this.childAgeProfilesToolStripMenuItem.Name = "childAgeProfilesToolStripMenuItem";
            this.childAgeProfilesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.childAgeProfilesToolStripMenuItem.Text = "Child Age Profiles";
            this.childAgeProfilesToolStripMenuItem.Click += new System.EventHandler(this.childAgeProfilesToolStripMenuItem_Click);
            // 
            // olderParentsToolStripMenuItem
            // 
            this.olderParentsToolStripMenuItem.Name = "olderParentsToolStripMenuItem";
            this.olderParentsToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.olderParentsToolStripMenuItem.Text = "Older Parents";
            this.olderParentsToolStripMenuItem.Click += new System.EventHandler(this.olderParentsToolStripMenuItem_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.individualsToExcelToolStripMenuItem,
            this.familiesToExcelToolStripMenuItem,
            this.factsToExcelToolStripMenuItem});
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(52, 20);
            this.mnuExport.Text = "Export";
            this.mnuExport.Visible = false;
            // 
            // individualsToExcelToolStripMenuItem
            // 
            this.individualsToExcelToolStripMenuItem.Name = "individualsToExcelToolStripMenuItem";
            this.individualsToExcelToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.individualsToExcelToolStripMenuItem.Text = "Individuals to Excel";
            this.individualsToExcelToolStripMenuItem.Click += new System.EventHandler(this.individualsToExcelToolStripMenuItem_Click);
            // 
            // familiesToExcelToolStripMenuItem
            // 
            this.familiesToExcelToolStripMenuItem.Name = "familiesToExcelToolStripMenuItem";
            this.familiesToExcelToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.familiesToExcelToolStripMenuItem.Text = "Families to Excel";
            this.familiesToExcelToolStripMenuItem.Click += new System.EventHandler(this.familiesToExcelToolStripMenuItem_Click);
            // 
            // factsToExcelToolStripMenuItem
            // 
            this.factsToExcelToolStripMenuItem.Name = "factsToExcelToolStripMenuItem";
            this.factsToExcelToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.factsToExcelToolStripMenuItem.Text = "Facts to Excel";
            this.factsToExcelToolStripMenuItem.Click += new System.EventHandler(this.factsToExcelToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.geocodeLocationsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // geocodeLocationsToolStripMenuItem
            // 
            this.geocodeLocationsToolStripMenuItem.Name = "geocodeLocationsToolStripMenuItem";
            this.geocodeLocationsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.geocodeLocationsToolStripMenuItem.Text = "Geocode Locations";
            this.geocodeLocationsToolStripMenuItem.Visible = false;
            this.geocodeLocationsToolStripMenuItem.Click += new System.EventHandler(this.geocodeLocationsToolStripMenuItem_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewOnlineManualToolStripMenuItem,
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator1,
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
            // checkForUpdatesToolStripMenuItem
            // 
            //this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            //this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            //this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            //this.checkForUpdatesToolStripMenuItem.Visible = false;
            //this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabSelector
            // 
            this.tabSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSelector.Controls.Add(this.tabDisplayProgress);
            this.tabSelector.Controls.Add(this.tabIndividuals);
            this.tabSelector.Controls.Add(this.tabFamilies);
            this.tabSelector.Controls.Add(this.tabLocations);
            this.tabSelector.Controls.Add(this.tabOccupations);
            this.tabSelector.Controls.Add(this.tabDataErrors);
            this.tabSelector.Controls.Add(this.tabLooseDeaths);
            this.tabSelector.Controls.Add(this.tabCensus);
            this.tabSelector.Controls.Add(this.tabLostCousins);
            this.tabSelector.Controls.Add(this.tabFamilySearch);
            this.tabSelector.Controls.Add(this.tabTreetops);
            this.tabSelector.Controls.Add(this.tabWarDead);
            this.tabSelector.Location = new System.Drawing.Point(0, 27);
            this.tabSelector.Name = "tabSelector";
            this.tabSelector.SelectedIndex = 0;
            this.tabSelector.Size = new System.Drawing.Size(939, 428);
            this.tabSelector.TabIndex = 9;
            this.tabSelector.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabDisplayProgress
            // 
            this.tabDisplayProgress.Controls.Add(this.webBrowser);
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
            this.tabDisplayProgress.Size = new System.Drawing.Size(931, 402);
            this.tabDisplayProgress.TabIndex = 1;
            this.tabDisplayProgress.Text = "Load Gedcom";
            this.tabDisplayProgress.UseVisualStyleBackColor = true;
            // 
            // webBrowser
            // 
            this.webBrowser.Location = new System.Drawing.Point(867, 3);
            this.webBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser.Name = "webBrowser";
            this.webBrowser.Size = new System.Drawing.Size(61, 54);
            this.webBrowser.TabIndex = 7;
            this.webBrowser.Visible = false;
            // 
            // rtbOutput
            // 
            this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutput.Location = new System.Drawing.Point(3, 90);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(925, 303);
            this.rtbOutput.TabIndex = 6;
            this.rtbOutput.Text = "";
            this.rtbOutput.TextChanged += new System.EventHandler(this.rtbOutput_TextChanged);
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
            this.pbFamilies.Location = new System.Drawing.Point(112, 60);
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
            this.pbIndividuals.Location = new System.Drawing.Point(112, 38);
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
            this.pbSources.Location = new System.Drawing.Point(112, 16);
            this.pbSources.Name = "pbSources";
            this.pbSources.Size = new System.Drawing.Size(316, 16);
            this.pbSources.TabIndex = 0;
            // 
            // tabIndividuals
            // 
            this.tabIndividuals.Controls.Add(this.dgIndividuals);
            this.tabIndividuals.Location = new System.Drawing.Point(4, 22);
            this.tabIndividuals.Name = "tabIndividuals";
            this.tabIndividuals.Padding = new System.Windows.Forms.Padding(3);
            this.tabIndividuals.Size = new System.Drawing.Size(931, 402);
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
            this.dgIndividuals.Size = new System.Drawing.Size(925, 396);
            this.dgIndividuals.TabIndex = 0;
            this.dgIndividuals.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dgIndividuals_MouseDown);
            // 
            // tabFamilies
            // 
            this.tabFamilies.Controls.Add(this.dgFamilies);
            this.tabFamilies.Location = new System.Drawing.Point(4, 22);
            this.tabFamilies.Name = "tabFamilies";
            this.tabFamilies.Size = new System.Drawing.Size(931, 402);
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
            this.dgFamilies.Size = new System.Drawing.Size(931, 402);
            this.dgFamilies.TabIndex = 1;
            // 
            // tabLocations
            // 
            this.tabLocations.Controls.Add(this.btnBingOSMap);
            this.tabLocations.Controls.Add(this.btnShowMap);
            this.tabLocations.Controls.Add(this.tabCtrlLocations);
            this.tabLocations.Location = new System.Drawing.Point(4, 22);
            this.tabLocations.Name = "tabLocations";
            this.tabLocations.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocations.Size = new System.Drawing.Size(931, 402);
            this.tabLocations.TabIndex = 4;
            this.tabLocations.Text = "Locations";
            this.tabLocations.UseVisualStyleBackColor = true;
            // 
            // btnBingOSMap
            // 
            this.btnBingOSMap.Location = new System.Drawing.Point(721, 1);
            this.btnBingOSMap.Name = "btnBingOSMap";
            this.btnBingOSMap.Size = new System.Drawing.Size(98, 22);
            this.btnBingOSMap.TabIndex = 3;
            this.btnBingOSMap.Text = "Show OS Map";
            this.btnBingOSMap.UseVisualStyleBackColor = true;
            this.btnBingOSMap.Click += new System.EventHandler(this.btnBingOSMap_Click);
            // 
            // btnShowMap
            // 
            this.btnShowMap.Location = new System.Drawing.Point(825, 1);
            this.btnShowMap.Name = "btnShowMap";
            this.btnShowMap.Size = new System.Drawing.Size(98, 22);
            this.btnShowMap.TabIndex = 2;
            this.btnShowMap.Text = "Show Map";
            this.btnShowMap.UseVisualStyleBackColor = true;
            this.btnShowMap.Click += new System.EventHandler(this.btnShowMap_Click);
            // 
            // tabCtrlLocations
            // 
            this.tabCtrlLocations.Controls.Add(this.tabCountries);
            this.tabCtrlLocations.Controls.Add(this.tabRegions);
            this.tabCtrlLocations.Controls.Add(this.tabParishes);
            this.tabCtrlLocations.Controls.Add(this.tabAddresses);
            this.tabCtrlLocations.Controls.Add(this.tabPlaces);
            this.tabCtrlLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrlLocations.Location = new System.Drawing.Point(3, 3);
            this.tabCtrlLocations.Name = "tabCtrlLocations";
            this.tabCtrlLocations.SelectedIndex = 0;
            this.tabCtrlLocations.Size = new System.Drawing.Size(925, 396);
            this.tabCtrlLocations.TabIndex = 0;
            this.tabCtrlLocations.SelectedIndexChanged += new System.EventHandler(this.tabCtrlLocations_SelectedIndexChanged);
            // 
            // tabCountries
            // 
            this.tabCountries.Controls.Add(this.dgCountries);
            this.tabCountries.Location = new System.Drawing.Point(4, 22);
            this.tabCountries.Name = "tabCountries";
            this.tabCountries.Padding = new System.Windows.Forms.Padding(3);
            this.tabCountries.Size = new System.Drawing.Size(917, 370);
            this.tabCountries.TabIndex = 0;
            this.tabCountries.Text = "Countries";
            this.tabCountries.ToolTipText = "Double click on Country name to see list of individuals with that Country.";
            this.tabCountries.UseVisualStyleBackColor = true;
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
            this.dgCountries.Size = new System.Drawing.Size(911, 364);
            this.dgCountries.TabIndex = 0;
            this.toolTips.SetToolTip(this.dgCountries, "Double click on Country name to see list of individuals with that Country.");
            this.dgCountries.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCountries_CellDoubleClick);
            this.dgCountries.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgCountries_CellFormatting);
            // 
            // tabRegions
            // 
            this.tabRegions.Controls.Add(this.dgRegions);
            this.tabRegions.Location = new System.Drawing.Point(4, 22);
            this.tabRegions.Name = "tabRegions";
            this.tabRegions.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegions.Size = new System.Drawing.Size(917, 370);
            this.tabRegions.TabIndex = 1;
            this.tabRegions.Text = "Regions";
            this.tabRegions.ToolTipText = "Double click on Region name to see list of individuals with that Region.";
            this.tabRegions.UseVisualStyleBackColor = true;
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
            this.dgRegions.Size = new System.Drawing.Size(911, 364);
            this.dgRegions.TabIndex = 1;
            this.toolTips.SetToolTip(this.dgRegions, "Double click on Region name to see list of individuals with that Region.");
            this.dgRegions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRegions_CellDoubleClick);
            // 
            // tabParishes
            // 
            this.tabParishes.Controls.Add(this.dgParishes);
            this.tabParishes.Location = new System.Drawing.Point(4, 22);
            this.tabParishes.Name = "tabParishes";
            this.tabParishes.Padding = new System.Windows.Forms.Padding(3);
            this.tabParishes.Size = new System.Drawing.Size(917, 370);
            this.tabParishes.TabIndex = 2;
            this.tabParishes.Text = "Parishes";
            this.tabParishes.ToolTipText = "Double click on \'Parish\' name to see list of individuals with that parish/area.";
            this.tabParishes.UseVisualStyleBackColor = true;
            // 
            // dgParishes
            // 
            this.dgParishes.AllowUserToAddRows = false;
            this.dgParishes.AllowUserToDeleteRows = false;
            this.dgParishes.AllowUserToResizeRows = false;
            this.dgParishes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgParishes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgParishes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgParishes.Location = new System.Drawing.Point(3, 3);
            this.dgParishes.MultiSelect = false;
            this.dgParishes.Name = "dgParishes";
            this.dgParishes.RowHeadersVisible = false;
            this.dgParishes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgParishes.Size = new System.Drawing.Size(911, 364);
            this.dgParishes.TabIndex = 1;
            this.dgParishes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgParishes_CellDoubleClick);
            // 
            // tabAddresses
            // 
            this.tabAddresses.Controls.Add(this.dgAddresses);
            this.tabAddresses.Location = new System.Drawing.Point(4, 22);
            this.tabAddresses.Name = "tabAddresses";
            this.tabAddresses.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddresses.Size = new System.Drawing.Size(917, 370);
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
            this.dgAddresses.Size = new System.Drawing.Size(911, 364);
            this.dgAddresses.TabIndex = 1;
            this.dgAddresses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAddresses_CellDoubleClick);
            // 
            // tabPlaces
            // 
            this.tabPlaces.Controls.Add(this.dgPlaces);
            this.tabPlaces.Location = new System.Drawing.Point(4, 22);
            this.tabPlaces.Name = "tabPlaces";
            this.tabPlaces.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlaces.Size = new System.Drawing.Size(917, 370);
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
            this.dgPlaces.Size = new System.Drawing.Size(911, 364);
            this.dgPlaces.TabIndex = 2;
            this.dgPlaces.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPlaces_CellDoubleClick);
            // 
            // tabOccupations
            // 
            this.tabOccupations.Controls.Add(this.dgOccupations);
            this.tabOccupations.Location = new System.Drawing.Point(4, 22);
            this.tabOccupations.Name = "tabOccupations";
            this.tabOccupations.Size = new System.Drawing.Size(931, 402);
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
            this.dgOccupations.Size = new System.Drawing.Size(931, 402);
            this.dgOccupations.TabIndex = 2;
            this.dgOccupations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgOccupations_CellDoubleClick);
            // 
            // tabDataErrors
            // 
            this.tabDataErrors.Controls.Add(this.dgDataErrors);
            this.tabDataErrors.Controls.Add(this.gbDataErrorTypes);
            this.tabDataErrors.Location = new System.Drawing.Point(4, 22);
            this.tabDataErrors.Name = "tabDataErrors";
            this.tabDataErrors.Size = new System.Drawing.Size(931, 402);
            this.tabDataErrors.TabIndex = 11;
            this.tabDataErrors.Text = "Data Errors";
            this.tabDataErrors.UseVisualStyleBackColor = true;
            // 
            // dgDataErrors
            // 
            this.dgDataErrors.AllowUserToAddRows = false;
            this.dgDataErrors.AllowUserToDeleteRows = false;
            this.dgDataErrors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgDataErrors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDataErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgDataErrors.Location = new System.Drawing.Point(0, 108);
            this.dgDataErrors.MultiSelect = false;
            this.dgDataErrors.Name = "dgDataErrors";
            this.dgDataErrors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDataErrors.Size = new System.Drawing.Size(931, 294);
            this.dgDataErrors.TabIndex = 3;
            // 
            // gbDataErrorTypes
            // 
            this.gbDataErrorTypes.Controls.Add(this.ckbDataErrors);
            this.gbDataErrorTypes.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDataErrorTypes.Location = new System.Drawing.Point(0, 0);
            this.gbDataErrorTypes.Name = "gbDataErrorTypes";
            this.gbDataErrorTypes.Size = new System.Drawing.Size(931, 108);
            this.gbDataErrorTypes.TabIndex = 0;
            this.gbDataErrorTypes.TabStop = false;
            this.gbDataErrorTypes.Text = "Types of Data Error to display";
            // 
            // ckbDataErrors
            // 
            this.ckbDataErrors.CheckOnClick = true;
            this.ckbDataErrors.ColumnWidth = 225;
            this.ckbDataErrors.FormattingEnabled = true;
            this.ckbDataErrors.Location = new System.Drawing.Point(8, 19);
            this.ckbDataErrors.MultiColumn = true;
            this.ckbDataErrors.Name = "ckbDataErrors";
            this.ckbDataErrors.Size = new System.Drawing.Size(700, 79);
            this.ckbDataErrors.TabIndex = 0;
            this.ckbDataErrors.SelectedIndexChanged += new System.EventHandler(this.ckbDataErrors_SelectedIndexChanged);
            // 
            // tabLooseDeaths
            // 
            this.tabLooseDeaths.Controls.Add(this.dgLooseDeaths);
            this.tabLooseDeaths.Location = new System.Drawing.Point(4, 22);
            this.tabLooseDeaths.Name = "tabLooseDeaths";
            this.tabLooseDeaths.Padding = new System.Windows.Forms.Padding(3);
            this.tabLooseDeaths.Size = new System.Drawing.Size(931, 402);
            this.tabLooseDeaths.TabIndex = 3;
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
            this.dgLooseDeaths.Size = new System.Drawing.Size(925, 396);
            this.dgLooseDeaths.TabIndex = 0;
            // 
            // tabCensus
            // 
            this.tabCensus.Controls.Add(this.btnLCReport2);
            this.tabCensus.Controls.Add(this.label1);
            this.tabCensus.Controls.Add(this.txtSurname);
            this.tabCensus.Controls.Add(this.ckbNoLocations);
            this.tabCensus.Controls.Add(this.ckbCensusResidence);
            this.tabCensus.Controls.Add(this.label2);
            this.tabCensus.Controls.Add(this.udAgeFilter);
            this.tabCensus.Controls.Add(this.btnShowResults);
            this.tabCensus.Controls.Add(this.cenDate);
            this.tabCensus.Controls.Add(this.censusCountry);
            this.tabCensus.Controls.Add(this.relationTypes);
            this.tabCensus.Location = new System.Drawing.Point(4, 22);
            this.tabCensus.Name = "tabCensus";
            this.tabCensus.Padding = new System.Windows.Forms.Padding(3);
            this.tabCensus.Size = new System.Drawing.Size(931, 402);
            this.tabCensus.TabIndex = 0;
            this.tabCensus.Text = "Census";
            this.tabCensus.UseVisualStyleBackColor = true;
            // 
            // btnLCReport2
            // 
            this.btnLCReport2.Location = new System.Drawing.Point(606, 82);
            this.btnLCReport2.Name = "btnLCReport2";
            this.btnLCReport2.Size = new System.Drawing.Size(185, 23);
            this.btnLCReport2.TabIndex = 21;
            this.btnLCReport2.Text = "View Lost Cousins census Report";
            this.btnLCReport2.UseVisualStyleBackColor = true;
            this.btnLCReport2.Click += new System.EventHandler(this.btnLCReport2_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(603, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Surname";
            // 
            // txtSurname
            // 
            this.txtSurname.Location = new System.Drawing.Point(658, 14);
            this.txtSurname.Name = "txtSurname";
            this.txtSurname.Size = new System.Drawing.Size(201, 20);
            this.txtSurname.TabIndex = 19;
            // 
            // ckbNoLocations
            // 
            this.ckbNoLocations.AutoSize = true;
            this.ckbNoLocations.Checked = true;
            this.ckbNoLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbNoLocations.Location = new System.Drawing.Point(272, 114);
            this.ckbNoLocations.Name = "ckbNoLocations";
            this.ckbNoLocations.Size = new System.Drawing.Size(171, 17);
            this.ckbNoLocations.TabIndex = 18;
            this.ckbNoLocations.Text = "Ignore locations in census filter";
            this.ckbNoLocations.UseVisualStyleBackColor = true;
            this.ckbNoLocations.CheckedChanged += new System.EventHandler(this.ckbNoLocations_CheckedChanged);
            // 
            // ckbCensusResidence
            // 
            this.ckbCensusResidence.AutoSize = true;
            this.ckbCensusResidence.Checked = true;
            this.ckbCensusResidence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbCensusResidence.Location = new System.Drawing.Point(11, 111);
            this.ckbCensusResidence.Name = "ckbCensusResidence";
            this.ckbCensusResidence.Size = new System.Drawing.Size(213, 17);
            this.ckbCensusResidence.TabIndex = 14;
            this.ckbCensusResidence.Text = "Treat \'Residence\' facts as Census facts";
            this.ckbCensusResidence.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Exclude individuals over the age of ";
            // 
            // udAgeFilter
            // 
            this.udAgeFilter.Location = new System.Drawing.Point(189, 85);
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
            // btnShowResults
            // 
            this.btnShowResults.Location = new System.Drawing.Point(6, 134);
            this.btnShowResults.Name = "btnShowResults";
            this.btnShowResults.Size = new System.Drawing.Size(82, 25);
            this.btnShowResults.TabIndex = 4;
            this.btnShowResults.Text = "Show Results";
            this.btnShowResults.UseVisualStyleBackColor = true;
            this.btnShowResults.Click += new System.EventHandler(this.btnShowResults_Click);
            // 
            // cenDate
            // 
            this.cenDate.AutoSize = true;
            this.cenDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cenDate.Country = "";
            this.cenDate.Location = new System.Drawing.Point(272, 81);
            this.cenDate.Name = "cenDate";
            this.cenDate.Size = new System.Drawing.Size(186, 27);
            this.cenDate.TabIndex = 17;
            this.cenDate.CensusChanged += new System.EventHandler(this.cenDate_CensusChanged);
            // 
            // censusCountry
            // 
            this.censusCountry.Location = new System.Drawing.Point(6, 6);
            this.censusCountry.Name = "censusCountry";
            this.censusCountry.Size = new System.Drawing.Size(260, 78);
            this.censusCountry.TabIndex = 16;
            this.censusCountry.Title = "Census Country";
            this.censusCountry.UKEnabled = false;
            this.censusCountry.CountryChanged += new System.EventHandler(this.censusCountry_CountryChanged);
            // 
            // relationTypes
            // 
            this.relationTypes.Location = new System.Drawing.Point(272, 6);
            this.relationTypes.Name = "relationTypes";
            this.relationTypes.Size = new System.Drawing.Size(325, 78);
            this.relationTypes.TabIndex = 15;
            // 
            // tabLostCousins
            // 
            this.tabLostCousins.Controls.Add(this.btnLCReport);
            this.tabLostCousins.Controls.Add(this.ckbLCIgnoreCountry);
            this.tabLostCousins.Controls.Add(this.linkLabel2);
            this.tabLostCousins.Controls.Add(this.btnLC1911EW);
            this.tabLostCousins.Controls.Add(this.ckbLCResidence);
            this.tabLostCousins.Controls.Add(this.linkLabel1);
            this.tabLostCousins.Controls.Add(this.ckbHideRecorded);
            this.tabLostCousins.Controls.Add(this.ckbRestrictions);
            this.tabLostCousins.Controls.Add(this.btnLC1841EW);
            this.tabLostCousins.Controls.Add(this.btnLC1911Ireland);
            this.tabLostCousins.Controls.Add(this.btnLC1880USA);
            this.tabLostCousins.Controls.Add(this.btnLC1881EW);
            this.tabLostCousins.Controls.Add(this.btnLC1881Canada);
            this.tabLostCousins.Controls.Add(this.btnLC1881Scot);
            this.tabLostCousins.Location = new System.Drawing.Point(4, 22);
            this.tabLostCousins.Name = "tabLostCousins";
            this.tabLostCousins.Padding = new System.Windows.Forms.Padding(3);
            this.tabLostCousins.Size = new System.Drawing.Size(931, 402);
            this.tabLostCousins.TabIndex = 5;
            this.tabLostCousins.Text = "Lost Cousins";
            this.tabLostCousins.UseVisualStyleBackColor = true;
            // 
            // btnLCReport
            // 
            this.btnLCReport.Location = new System.Drawing.Point(22, 273);
            this.btnLCReport.Name = "btnLCReport";
            this.btnLCReport.Size = new System.Drawing.Size(185, 23);
            this.btnLCReport.TabIndex = 17;
            this.btnLCReport.Text = "View Lost Cousins census Report";
            this.btnLCReport.UseVisualStyleBackColor = true;
            this.btnLCReport.Click += new System.EventHandler(this.btnLCReport_Click);
            // 
            // ckbLCIgnoreCountry
            // 
            this.ckbLCIgnoreCountry.AutoSize = true;
            this.ckbLCIgnoreCountry.Location = new System.Drawing.Point(22, 66);
            this.ckbLCIgnoreCountry.Name = "ckbLCIgnoreCountry";
            this.ckbLCIgnoreCountry.Size = new System.Drawing.Size(150, 17);
            this.ckbLCIgnoreCountry.TabIndex = 16;
            this.ckbLCIgnoreCountry.Text = "Ignore Country in Location";
            this.toolTips.SetToolTip(this.ckbLCIgnoreCountry, "Tick this if you don\'t usually don\'t have country names in your locations");
            this.ckbLCIgnoreCountry.UseVisualStyleBackColor = true;
            this.ckbLCIgnoreCountry.CheckedChanged += new System.EventHandler(this.ckbLCIgnoreCountry_CheckedChanged);
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(211, 244);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(174, 16);
            this.linkLabel2.TabIndex = 15;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Visit the Lost Cousins Forum";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
            // 
            // btnLC1911EW
            // 
            this.btnLC1911EW.Location = new System.Drawing.Point(22, 196);
            this.btnLC1911EW.Name = "btnLC1911EW";
            this.btnLC1911EW.Size = new System.Drawing.Size(162, 36);
            this.btnLC1911EW.TabIndex = 14;
            this.btnLC1911EW.Text = "1911 England && Wales Census";
            this.btnLC1911EW.UseVisualStyleBackColor = true;
            this.btnLC1911EW.Click += new System.EventHandler(this.btnLC1911EW_Click);
            // 
            // ckbLCResidence
            // 
            this.ckbLCResidence.AutoSize = true;
            this.ckbLCResidence.Checked = true;
            this.ckbLCResidence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbLCResidence.Location = new System.Drawing.Point(22, 89);
            this.ckbLCResidence.Name = "ckbLCResidence";
            this.ckbLCResidence.Size = new System.Drawing.Size(213, 17);
            this.ckbLCResidence.TabIndex = 13;
            this.ckbLCResidence.Text = "Treat \'Residence\' facts as Census facts";
            this.ckbLCResidence.UseVisualStyleBackColor = true;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(19, 244);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(186, 16);
            this.linkLabel1.TabIndex = 12;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Visit the Lost Cousins Website";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // ckbHideRecorded
            // 
            this.ckbHideRecorded.AutoSize = true;
            this.ckbHideRecorded.Checked = true;
            this.ckbHideRecorded.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbHideRecorded.Location = new System.Drawing.Point(22, 45);
            this.ckbHideRecorded.Name = "ckbHideRecorded";
            this.ckbHideRecorded.Size = new System.Drawing.Size(311, 17);
            this.ckbHideRecorded.TabIndex = 10;
            this.ckbHideRecorded.Text = "Hide results for individuals tagged as entered in Lost Cousins";
            this.ckbHideRecorded.UseVisualStyleBackColor = true;
            // 
            // ckbRestrictions
            // 
            this.ckbRestrictions.AutoSize = true;
            this.ckbRestrictions.Checked = true;
            this.ckbRestrictions.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbRestrictions.Location = new System.Drawing.Point(22, 22);
            this.ckbRestrictions.Name = "ckbRestrictions";
            this.ckbRestrictions.Size = new System.Drawing.Size(521, 17);
            this.ckbRestrictions.TabIndex = 9;
            this.ckbRestrictions.Text = "Restrict results to only those direct ancestors, blood relations and those marrie" +
                "d to direct or blood relations";
            this.ckbRestrictions.UseVisualStyleBackColor = true;
            // 
            // btnLC1841EW
            // 
            this.btnLC1841EW.Location = new System.Drawing.Point(22, 112);
            this.btnLC1841EW.Name = "btnLC1841EW";
            this.btnLC1841EW.Size = new System.Drawing.Size(162, 36);
            this.btnLC1841EW.TabIndex = 8;
            this.btnLC1841EW.Text = "1841 England && Wales Census";
            this.btnLC1841EW.UseVisualStyleBackColor = true;
            this.btnLC1841EW.Click += new System.EventHandler(this.btnLC1841EW_Click);
            // 
            // btnLC1911Ireland
            // 
            this.btnLC1911Ireland.Location = new System.Drawing.Point(190, 196);
            this.btnLC1911Ireland.Name = "btnLC1911Ireland";
            this.btnLC1911Ireland.Size = new System.Drawing.Size(162, 36);
            this.btnLC1911Ireland.TabIndex = 7;
            this.btnLC1911Ireland.Text = "1911 Ireland Census";
            this.btnLC1911Ireland.UseVisualStyleBackColor = true;
            this.btnLC1911Ireland.Click += new System.EventHandler(this.btnLC1911Ireland_Click);
            // 
            // btnLC1880USA
            // 
            this.btnLC1880USA.Location = new System.Drawing.Point(190, 112);
            this.btnLC1880USA.Name = "btnLC1880USA";
            this.btnLC1880USA.Size = new System.Drawing.Size(162, 36);
            this.btnLC1880USA.TabIndex = 6;
            this.btnLC1880USA.Text = "1880 US Census";
            this.btnLC1880USA.UseVisualStyleBackColor = true;
            this.btnLC1880USA.Click += new System.EventHandler(this.btnLC1880USA_Click);
            // 
            // btnLC1881EW
            // 
            this.btnLC1881EW.Location = new System.Drawing.Point(22, 154);
            this.btnLC1881EW.Name = "btnLC1881EW";
            this.btnLC1881EW.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881EW.TabIndex = 5;
            this.btnLC1881EW.Text = "1881 England && Wales Census";
            this.btnLC1881EW.UseVisualStyleBackColor = true;
            this.btnLC1881EW.Click += new System.EventHandler(this.btnLC1881EW_Click);
            // 
            // btnLC1881Canada
            // 
            this.btnLC1881Canada.Location = new System.Drawing.Point(358, 154);
            this.btnLC1881Canada.Name = "btnLC1881Canada";
            this.btnLC1881Canada.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881Canada.TabIndex = 4;
            this.btnLC1881Canada.Text = "1881 Canada Census";
            this.btnLC1881Canada.UseVisualStyleBackColor = true;
            this.btnLC1881Canada.Click += new System.EventHandler(this.btnLC1881Canada_Click);
            // 
            // btnLC1881Scot
            // 
            this.btnLC1881Scot.Location = new System.Drawing.Point(190, 154);
            this.btnLC1881Scot.Name = "btnLC1881Scot";
            this.btnLC1881Scot.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881Scot.TabIndex = 0;
            this.btnLC1881Scot.Text = "1881 Scotland Census";
            this.btnLC1881Scot.UseVisualStyleBackColor = true;
            this.btnLC1881Scot.Click += new System.EventHandler(this.btnLC1881Scot_Click);
            // 
            // tabFamilySearch
            // 
            this.tabFamilySearch.Controls.Add(this.label7);
            this.tabFamilySearch.Controls.Add(this.txtFamilySearchSurname);
            this.tabFamilySearch.Controls.Add(this.btnOpenFolder);
            this.tabFamilySearch.Controls.Add(this.groupBox1);
            this.tabFamilySearch.Controls.Add(this.btnCancelFamilySearch);
            this.tabFamilySearch.Controls.Add(this.btnViewResults);
            this.tabFamilySearch.Controls.Add(this.pbFamilySearch);
            this.tabFamilySearch.Controls.Add(this.btnFamilySearchMarriageSearch);
            this.tabFamilySearch.Controls.Add(this.btnFamilySearchChildrenSearch);
            this.tabFamilySearch.Controls.Add(this.btnFamilySearchFolderBrowse);
            this.tabFamilySearch.Controls.Add(this.txtFamilySearchfolder);
            this.tabFamilySearch.Controls.Add(this.label3);
            this.tabFamilySearch.Controls.Add(this.rtbFamilySearchResults);
            this.tabFamilySearch.Controls.Add(this.FamilySearchrelationTypes);
            this.tabFamilySearch.Controls.Add(this.FamilySearchDefaultCountry);
            this.tabFamilySearch.Location = new System.Drawing.Point(4, 22);
            this.tabFamilySearch.Name = "tabFamilySearch";
            this.tabFamilySearch.Size = new System.Drawing.Size(931, 402);
            this.tabFamilySearch.TabIndex = 6;
            this.tabFamilySearch.Text = "FamilySearch";
            this.tabFamilySearch.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(506, 125);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Surname";
            // 
            // txtFamilySearchSurname
            // 
            this.txtFamilySearchSurname.Location = new System.Drawing.Point(561, 122);
            this.txtFamilySearchSurname.Name = "txtFamilySearchSurname";
            this.txtFamilySearchSurname.Size = new System.Drawing.Size(201, 20);
            this.txtFamilySearchSurname.TabIndex = 21;
            // 
            // btnOpenFolder
            // 
            this.btnOpenFolder.Location = new System.Drawing.Point(598, 11);
            this.btnOpenFolder.Name = "btnOpenFolder";
            this.btnOpenFolder.Size = new System.Drawing.Size(77, 21);
            this.btnOpenFolder.TabIndex = 13;
            this.btnOpenFolder.Text = "Open Folder";
            this.btnOpenFolder.UseVisualStyleBackColor = true;
            this.btnOpenFolder.Click += new System.EventHandler(this.btnOpenFolder_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbFamilySearchRegion);
            this.groupBox1.Controls.Add(this.rbFamilySearchCountry);
            this.groupBox1.Location = new System.Drawing.Point(598, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 74);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search By";
            // 
            // rbFamilySearchRegion
            // 
            this.rbFamilySearchRegion.AutoSize = true;
            this.rbFamilySearchRegion.Location = new System.Drawing.Point(7, 44);
            this.rbFamilySearchRegion.Name = "rbFamilySearchRegion";
            this.rbFamilySearchRegion.Size = new System.Drawing.Size(147, 17);
            this.rbFamilySearchRegion.TabIndex = 1;
            this.rbFamilySearchRegion.TabStop = true;
            this.rbFamilySearchRegion.Text = "County / State / Province";
            this.rbFamilySearchRegion.UseVisualStyleBackColor = true;
            // 
            // rbFamilySearchCountry
            // 
            this.rbFamilySearchCountry.AutoSize = true;
            this.rbFamilySearchCountry.Checked = true;
            this.rbFamilySearchCountry.Location = new System.Drawing.Point(7, 20);
            this.rbFamilySearchCountry.Name = "rbFamilySearchCountry";
            this.rbFamilySearchCountry.Size = new System.Drawing.Size(61, 17);
            this.rbFamilySearchCountry.TabIndex = 0;
            this.rbFamilySearchCountry.TabStop = true;
            this.rbFamilySearchCountry.Text = "Country";
            this.rbFamilySearchCountry.UseVisualStyleBackColor = true;
            // 
            // btnCancelFamilySearch
            // 
            this.btnCancelFamilySearch.Location = new System.Drawing.Point(266, 118);
            this.btnCancelFamilySearch.Name = "btnCancelFamilySearch";
            this.btnCancelFamilySearch.Size = new System.Drawing.Size(123, 27);
            this.btnCancelFamilySearch.TabIndex = 9;
            this.btnCancelFamilySearch.Text = "Cancel Search";
            this.btnCancelFamilySearch.UseVisualStyleBackColor = true;
            this.btnCancelFamilySearch.Click += new System.EventHandler(this.btnCancelFamilySearch_Click);
            // 
            // btnViewResults
            // 
            this.btnViewResults.Location = new System.Drawing.Point(0, 0);
            this.btnViewResults.Name = "btnViewResults";
            this.btnViewResults.Size = new System.Drawing.Size(75, 23);
            this.btnViewResults.TabIndex = 23;
            // 
            // pbFamilySearch
            // 
            this.pbFamilySearch.Location = new System.Drawing.Point(0, 0);
            this.pbFamilySearch.Name = "pbFamilySearch";
            this.pbFamilySearch.Size = new System.Drawing.Size(100, 23);
            this.pbFamilySearch.TabIndex = 24;
            // 
            // btnFamilySearchMarriageSearch
            // 
            this.btnFamilySearchMarriageSearch.Location = new System.Drawing.Point(0, 0);
            this.btnFamilySearchMarriageSearch.Name = "btnFamilySearchMarriageSearch";
            this.btnFamilySearchMarriageSearch.Size = new System.Drawing.Size(75, 23);
            this.btnFamilySearchMarriageSearch.TabIndex = 25;
            // 
            // btnFamilySearchChildrenSearch
            // 
            this.btnFamilySearchChildrenSearch.Location = new System.Drawing.Point(0, 0);
            this.btnFamilySearchChildrenSearch.Name = "btnFamilySearchChildrenSearch";
            this.btnFamilySearchChildrenSearch.Size = new System.Drawing.Size(75, 23);
            this.btnFamilySearchChildrenSearch.TabIndex = 26;
            // 
            // btnFamilySearchFolderBrowse
            // 
            this.btnFamilySearchFolderBrowse.Location = new System.Drawing.Point(0, 0);
            this.btnFamilySearchFolderBrowse.Name = "btnFamilySearchFolderBrowse";
            this.btnFamilySearchFolderBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnFamilySearchFolderBrowse.TabIndex = 27;
            // 
            // txtFamilySearchfolder
            // 
            this.txtFamilySearchfolder.Location = new System.Drawing.Point(0, 0);
            this.txtFamilySearchfolder.Name = "txtFamilySearchfolder";
            this.txtFamilySearchfolder.Size = new System.Drawing.Size(100, 20);
            this.txtFamilySearchfolder.TabIndex = 28;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Folder to store results in : ";
            // 
            // rtbFamilySearchResults
            // 
            this.rtbFamilySearchResults.Location = new System.Drawing.Point(0, 177);
            this.rtbFamilySearchResults.Name = "rtbFamilySearchResults";
            this.rtbFamilySearchResults.Size = new System.Drawing.Size(931, 229);
            this.rtbFamilySearchResults.TabIndex = 12;
            this.rtbFamilySearchResults.Text = "";
            this.rtbFamilySearchResults.TextChanged += new System.EventHandler(this.rtbFamilySearchResults_TextChanged);
            // 
            // FamilySearchrelationTypes
            // 
            this.FamilySearchrelationTypes.Location = new System.Drawing.Point(270, 38);
            this.FamilySearchrelationTypes.Name = "FamilySearchrelationTypes";
            this.FamilySearchrelationTypes.Size = new System.Drawing.Size(322, 74);
            this.FamilySearchrelationTypes.TabIndex = 10;
            // 
            // FamilySearchDefaultCountry
            // 
            this.FamilySearchDefaultCountry.Location = new System.Drawing.Point(8, 38);
            this.FamilySearchDefaultCountry.Name = "FamilySearchDefaultCountry";
            this.FamilySearchDefaultCountry.Size = new System.Drawing.Size(256, 74);
            this.FamilySearchDefaultCountry.TabIndex = 7;
            this.FamilySearchDefaultCountry.Title = "Default Country";
            this.FamilySearchDefaultCountry.UKEnabled = false;
            this.FamilySearchDefaultCountry.CountryChanged += new System.EventHandler(this.FamilySearchDefaultCountry_CountryChanged);
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
            this.tabTreetops.Size = new System.Drawing.Size(931, 402);
            this.tabTreetops.TabIndex = 7;
            this.tabTreetops.Text = "Treetops";
            this.tabTreetops.UseVisualStyleBackColor = true;
            // 
            // dgTreeTops
            // 
            this.dgTreeTops.AllowUserToAddRows = false;
            this.dgTreeTops.AllowUserToDeleteRows = false;
            this.dgTreeTops.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgTreeTops.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTreeTops.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgTreeTops.Location = new System.Drawing.Point(0, 110);
            this.dgTreeTops.Name = "dgTreeTops";
            this.dgTreeTops.ReadOnly = true;
            this.dgTreeTops.Size = new System.Drawing.Size(931, 292);
            this.dgTreeTops.TabIndex = 28;
            // 
            // ckbTTIgnoreLocations
            // 
            this.ckbTTIgnoreLocations.AutoSize = true;
            this.ckbTTIgnoreLocations.Checked = true;
            this.ckbTTIgnoreLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTTIgnoreLocations.Location = new System.Drawing.Point(8, 87);
            this.ckbTTIgnoreLocations.Name = "ckbTTIgnoreLocations";
            this.ckbTTIgnoreLocations.Size = new System.Drawing.Size(175, 17);
            this.ckbTTIgnoreLocations.TabIndex = 27;
            this.ckbTTIgnoreLocations.Text = "Ignore locations in treetops filter";
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
            // treetopsRelation
            // 
            this.treetopsRelation.Location = new System.Drawing.Point(270, 12);
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
            // tabWarDead
            // 
            this.tabWarDead.Controls.Add(this.ckbWDIgnoreLocations);
            this.tabWarDead.Controls.Add(this.btnWWII);
            this.tabWarDead.Controls.Add(this.btnWWI);
            this.tabWarDead.Controls.Add(this.dgWarDead);
            this.tabWarDead.Controls.Add(this.label9);
            this.tabWarDead.Controls.Add(this.txtWarDeadSurname);
            this.tabWarDead.Controls.Add(this.wardeadRelation);
            this.tabWarDead.Controls.Add(this.wardeadCountry);
            this.tabWarDead.Location = new System.Drawing.Point(4, 22);
            this.tabWarDead.Name = "tabWarDead";
            this.tabWarDead.Size = new System.Drawing.Size(931, 402);
            this.tabWarDead.TabIndex = 8;
            this.tabWarDead.Text = "War Dead";
            this.tabWarDead.UseVisualStyleBackColor = true;
            // 
            // ckbWDIgnoreLocations
            // 
            this.ckbWDIgnoreLocations.AutoSize = true;
            this.ckbWDIgnoreLocations.Checked = true;
            this.ckbWDIgnoreLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbWDIgnoreLocations.Location = new System.Drawing.Point(8, 87);
            this.ckbWDIgnoreLocations.Name = "ckbWDIgnoreLocations";
            this.ckbWDIgnoreLocations.Size = new System.Drawing.Size(181, 17);
            this.ckbWDIgnoreLocations.TabIndex = 32;
            this.ckbWDIgnoreLocations.Text = "Ignore locations in war dead filter";
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
            // dgWarDead
            // 
            this.dgWarDead.AllowUserToAddRows = false;
            this.dgWarDead.AllowUserToDeleteRows = false;
            this.dgWarDead.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgWarDead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWarDead.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgWarDead.Location = new System.Drawing.Point(0, 110);
            this.dgWarDead.Name = "dgWarDead";
            this.dgWarDead.ReadOnly = true;
            this.dgWarDead.Size = new System.Drawing.Size(931, 292);
            this.dgWarDead.TabIndex = 29;
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
            // txtWarDeadSurname
            // 
            this.txtWarDeadSurname.Location = new System.Drawing.Point(650, 22);
            this.txtWarDeadSurname.Name = "txtWarDeadSurname";
            this.txtWarDeadSurname.Size = new System.Drawing.Size(201, 20);
            this.txtWarDeadSurname.TabIndex = 27;
            // 
            // wardeadRelation
            // 
            this.wardeadRelation.Location = new System.Drawing.Point(270, 12);
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
            this.tsCount.Text = "Count : 0";
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCountLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 458);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(939, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsCountLabel
            // 
            this.tsCountLabel.Name = "tsCountLabel";
            this.tsCountLabel.Size = new System.Drawing.Size(0, 17);
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
            this.printDialog.UseEXDialog = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 480);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Family Tree Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabSelector.ResumeLayout(false);
            this.tabDisplayProgress.ResumeLayout(false);
            this.tabDisplayProgress.PerformLayout();
            this.tabIndividuals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            this.tabFamilies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).EndInit();
            this.tabLocations.ResumeLayout(false);
            this.tabCtrlLocations.ResumeLayout(false);
            this.tabCountries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCountries)).EndInit();
            this.tabRegions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgRegions)).EndInit();
            this.tabParishes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgParishes)).EndInit();
            this.tabAddresses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgAddresses)).EndInit();
            this.tabPlaces.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgPlaces)).EndInit();
            this.tabOccupations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOccupations)).EndInit();
            this.tabDataErrors.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgDataErrors)).EndInit();
            this.gbDataErrorTypes.ResumeLayout(false);
            this.tabLooseDeaths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).EndInit();
            this.tabCensus.ResumeLayout(false);
            this.tabCensus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).EndInit();
            this.tabLostCousins.ResumeLayout(false);
            this.tabLostCousins.PerformLayout();
            this.tabFamilySearch.ResumeLayout(false);
            this.tabFamilySearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabTreetops.ResumeLayout(false);
            this.tabTreetops.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTreeTops)).EndInit();
            this.tabWarDead.ResumeLayout(false);
            this.tabWarDead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWarDead)).EndInit();
            this.mnuSetRoot.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openGedcom;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TabControl tabSelector;
        private System.Windows.Forms.TabPage tabCensus;
        private System.Windows.Forms.TabPage tabDisplayProgress;
        private System.Windows.Forms.ProgressBar pbSources;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar pbFamilies;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar pbIndividuals;
        private System.Windows.Forms.TabPage tabIndividuals;
        private System.Windows.Forms.DataGridView dgIndividuals;
        private System.Windows.Forms.TabPage tabLooseDeaths;
        private System.Windows.Forms.DataGridView dgLooseDeaths;
        private System.Windows.Forms.ToolStripStatusLabel tsCount;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel tsCountLabel;
        private System.Windows.Forms.TabPage tabLocations;
        private System.Windows.Forms.TabControl tabCtrlLocations;
        private System.Windows.Forms.TabPage tabCountries;
        private System.Windows.Forms.TabPage tabRegions;
        private System.Windows.Forms.TabPage tabParishes;
        private System.Windows.Forms.TabPage tabAddresses;
        private System.Windows.Forms.DataGridView dgCountries;
        private System.Windows.Forms.DataGridView dgRegions;
        private System.Windows.Forms.DataGridView dgParishes;
        private System.Windows.Forms.DataGridView dgAddresses;
        private System.Windows.Forms.Button btnShowResults;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.TabPage tabLostCousins;
        private System.Windows.Forms.Button btnLC1881Scot;
        private System.Windows.Forms.Button btnLC1841EW;
        private System.Windows.Forms.Button btnLC1911Ireland;
        private System.Windows.Forms.Button btnLC1880USA;
        private System.Windows.Forms.Button btnLC1881EW;
        private System.Windows.Forms.Button btnLC1881Canada;
        private System.Windows.Forms.CheckBox ckbRestrictions;
        private System.Windows.Forms.ToolStripMenuItem checkForUpdatesToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.CheckBox ckbHideRecorded;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udAgeFilter;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TabPage tabFamilySearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnFamilySearchFolderBrowse;
        private System.Windows.Forms.TextBox txtFamilySearchfolder;
        private System.Windows.Forms.Button btnFamilySearchMarriageSearch;
        private System.Windows.Forms.Button btnFamilySearchChildrenSearch;
        private System.Windows.Forms.ProgressBar pbFamilySearch;
        private System.Windows.Forms.CheckBox ckbLCResidence;
        private System.Windows.Forms.CheckBox ckbCensusResidence;
        private Controls.RelationTypes relationTypes;
        private Controls.CensusCountry censusCountry;
        private Controls.CensusDateSelector cenDate;
        private Controls.CensusCountry FamilySearchDefaultCountry;
        private System.Windows.Forms.Button btnViewResults;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Button btnCancelFamilySearch;
        private Controls.RelationTypes FamilySearchrelationTypes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbFamilySearchRegion;
        private System.Windows.Forms.RadioButton rbFamilySearchCountry;
        private global::FTAnalyzer.Utilities.ScrollingRichTextBox rtbOutput;
        private global::FTAnalyzer.Utilities.ScrollingRichTextBox rtbFamilySearchResults;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem mnuReports;
        private System.Windows.Forms.ToolStripMenuItem BirthRegistrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deathRegistrationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marriageRegistrationsToolStripMenuItem;
        private System.Windows.Forms.Button btnLC1911EW;
        private System.Windows.Forms.CheckBox ckbNoLocations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtFamilySearchSurname;
        private System.Windows.Forms.TabPage tabTreetops;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTreetopsSurname;
        private Controls.RelationTypes treetopsRelation;
        private Controls.CensusCountry treetopsCountry;
        private System.Windows.Forms.Button btnTreeTops;
        private System.Windows.Forms.TabPage tabWarDead;
        private System.Windows.Forms.DataGridView dgWarDead;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWarDeadSurname;
        private Controls.RelationTypes wardeadRelation;
        private Controls.CensusCountry wardeadCountry;
        private System.Windows.Forms.Button btnWWII;
        private System.Windows.Forms.Button btnWWI;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.WebBrowser webBrowser;
        private System.Windows.Forms.TabPage tabFamilies;
        private System.Windows.Forms.DataGridView dgFamilies;
        private System.Windows.Forms.ToolStripMenuItem mnuPrint;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.CheckBox ckbLCIgnoreCountry;
        private System.Windows.Forms.TabPage tabOccupations;
        private System.Windows.Forms.DataGridView dgOccupations;
        private System.Windows.Forms.Button btnShowMap;
        private System.Windows.Forms.ContextMenuStrip mnuSetRoot;
        private System.Windows.Forms.ToolStripMenuItem setAsRootToolStripMenuItem;
        private System.Windows.Forms.Button btnBingOSMap;
        private System.Windows.Forms.ToolStripMenuItem geocodeLocationsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabDataErrors;
        private System.Windows.Forms.GroupBox gbDataErrorTypes;
        private System.Windows.Forms.CheckedListBox ckbDataErrors;
        private System.Windows.Forms.DataGridView dgDataErrors;
        private System.Windows.Forms.Button btnLCReport;
        private System.Windows.Forms.Button btnLCReport2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem childAgeProfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewOnlineManualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem olderParentsToolStripMenuItem;
        private System.Windows.Forms.TabPage tabPlaces;
        private System.Windows.Forms.DataGridView dgPlaces;
        private System.Windows.Forms.CheckBox ckbTTIgnoreLocations;
        private System.Windows.Forms.CheckBox ckbWDIgnoreLocations;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripMenuItem individualsToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem familiesToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem factsToExcelToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgTreeTops;
    }
}

