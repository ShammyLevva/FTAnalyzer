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
            this.printToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BirthRegistrationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deathRegistrationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.marriageRegistrationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSelector = new System.Windows.Forms.TabControl();
            this.tabDisplayProgress = new System.Windows.Forms.TabPage();
            this.webBrowser = new System.Windows.Forms.WebBrowser();
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
            this.tabCtrlLocations = new System.Windows.Forms.TabControl();
            this.tabCountries = new System.Windows.Forms.TabPage();
            this.dgCountries = new System.Windows.Forms.DataGridView();
            this.tabRegions = new System.Windows.Forms.TabPage();
            this.dgRegions = new System.Windows.Forms.DataGridView();
            this.tabParishes = new System.Windows.Forms.TabPage();
            this.dgParishes = new System.Windows.Forms.DataGridView();
            this.tabAddresses = new System.Windows.Forms.TabPage();
            this.dgAddresses = new System.Windows.Forms.DataGridView();
            this.tabLooseDeaths = new System.Windows.Forms.TabPage();
            this.dgLooseDeaths = new System.Windows.Forms.DataGridView();
            this.tabCensus = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSurname = new System.Windows.Forms.TextBox();
            this.ckbNoLocations = new System.Windows.Forms.CheckBox();
            this.ckbCensusResidence = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.udAgeFilter = new System.Windows.Forms.NumericUpDown();
            this.btnShowResults = new System.Windows.Forms.Button();
            this.tabLostCousins = new System.Windows.Forms.TabPage();
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
            this.tabIGISearch = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.txtIGISurname = new System.Windows.Forms.TextBox();
            this.btnOpenFolder = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbIGISearchRegion = new System.Windows.Forms.RadioButton();
            this.rbIGISearchCountry = new System.Windows.Forms.RadioButton();
            this.btnCancelIGISearch = new System.Windows.Forms.Button();
            this.btnViewResults = new System.Windows.Forms.Button();
            this.pbIGISearch = new System.Windows.Forms.ProgressBar();
            this.btnIGIMarriageSearch = new System.Windows.Forms.Button();
            this.btnIGIChildrenSearch = new System.Windows.Forms.Button();
            this.btnIGIFolderBrowse = new System.Windows.Forms.Button();
            this.txtIGIfolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tabTreetops = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgTreeTops = new System.Windows.Forms.DataGridView();
            this.btnTreeTops = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTreetopsSurname = new System.Windows.Forms.TextBox();
            this.tabWarDead = new System.Windows.Forms.TabPage();
            this.btnWWII = new System.Windows.Forms.Button();
            this.btnWWI = new System.Windows.Forms.Button();
            this.dgWarDead = new System.Windows.Forms.DataGridView();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWarDeadSurname = new System.Windows.Forms.TextBox();
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.rtbOutput = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.cenDate = new Controls.CensusDateSelector();
            this.censusCountry = new Controls.CensusCountry();
            this.relationTypes = new Controls.RelationTypes();
            this.rtbIGIResults = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.IGIrelationTypes = new Controls.RelationTypes();
            this.IGIDefaultCountry = new Controls.CensusCountry();
            this.treetopsRelation = new Controls.RelationTypes();
            this.treetopsCountry = new Controls.CensusCountry();
            this.wardeadRelation = new Controls.RelationTypes();
            this.wardeadCountry = new Controls.CensusCountry();
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
            this.tabLooseDeaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).BeginInit();
            this.tabCensus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).BeginInit();
            this.tabLostCousins.SuspendLayout();
            this.tabIGISearch.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabTreetops.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTreeTops)).BeginInit();
            this.tabWarDead.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWarDead)).BeginInit();
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
            this.reportsToolStripMenuItem,
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
            this.printToolStripMenuItem});
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
            // printToolStripMenuItem
            // 
            this.printToolStripMenuItem.Enabled = false;
            this.printToolStripMenuItem.Name = "printToolStripMenuItem";
            this.printToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
            this.printToolStripMenuItem.Text = "Print";
            this.printToolStripMenuItem.Click += new System.EventHandler(this.printToolStripMenuItem_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BirthRegistrationToolStripMenuItem,
            this.deathRegistrationsToolStripMenuItem,
            this.marriageRegistrationsToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            this.reportsToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.reportsToolStripMenuItem.Text = "Reports";
            // 
            // BirthRegistrationToolStripMenuItem
            // 
            this.BirthRegistrationToolStripMenuItem.Name = "BirthRegistrationToolStripMenuItem";
            this.BirthRegistrationToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.BirthRegistrationToolStripMenuItem.Text = "Birth Registrations";
            this.BirthRegistrationToolStripMenuItem.Click += new System.EventHandler(this.BirthRegistrationToolStripMenuItem_Click);
            // 
            // deathRegistrationsToolStripMenuItem
            // 
            this.deathRegistrationsToolStripMenuItem.Name = "deathRegistrationsToolStripMenuItem";
            this.deathRegistrationsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.deathRegistrationsToolStripMenuItem.Text = "Death Registrations";
            this.deathRegistrationsToolStripMenuItem.Click += new System.EventHandler(this.deathRegistrationsToolStripMenuItem_Click);
            // 
            // marriageRegistrationsToolStripMenuItem
            // 
            this.marriageRegistrationsToolStripMenuItem.Name = "marriageRegistrationsToolStripMenuItem";
            this.marriageRegistrationsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            this.marriageRegistrationsToolStripMenuItem.Text = "Marriage Registrations";
            this.marriageRegistrationsToolStripMenuItem.Click += new System.EventHandler(this.marriageRegistrationsToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLocationsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // showLocationsToolStripMenuItem
            // 
            this.showLocationsToolStripMenuItem.Name = "showLocationsToolStripMenuItem";
            this.showLocationsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.showLocationsToolStripMenuItem.Text = "Show locations";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.optionsToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.checkForUpdatesToolStripMenuItem,
            this.toolStripSeparator1,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(168, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
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
            this.tabSelector.Controls.Add(this.tabLooseDeaths);
            this.tabSelector.Controls.Add(this.tabCensus);
            this.tabSelector.Controls.Add(this.tabLostCousins);
            this.tabSelector.Controls.Add(this.tabIGISearch);
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
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgIndividuals.Location = new System.Drawing.Point(3, 3);
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.Size = new System.Drawing.Size(925, 396);
            this.dgIndividuals.TabIndex = 0;
            this.dgIndividuals.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgIndividuals_ColumnHeaderMouseClick);
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
            this.dgFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFamilies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFamilies.Location = new System.Drawing.Point(0, 0);
            this.dgFamilies.Name = "dgFamilies";
            this.dgFamilies.Size = new System.Drawing.Size(931, 402);
            this.dgFamilies.TabIndex = 1;
            this.dgFamilies.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgFamilies_ColumnHeaderMouseClick);
            // 
            // tabLocations
            // 
            this.tabLocations.Controls.Add(this.tabCtrlLocations);
            this.tabLocations.Location = new System.Drawing.Point(4, 22);
            this.tabLocations.Name = "tabLocations";
            this.tabLocations.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocations.Size = new System.Drawing.Size(931, 402);
            this.tabLocations.TabIndex = 4;
            this.tabLocations.Text = "Locations";
            this.tabLocations.UseVisualStyleBackColor = true;
            // 
            // tabCtrlLocations
            // 
            this.tabCtrlLocations.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrlLocations.Controls.Add(this.tabCountries);
            this.tabCtrlLocations.Controls.Add(this.tabRegions);
            this.tabCtrlLocations.Controls.Add(this.tabParishes);
            this.tabCtrlLocations.Controls.Add(this.tabAddresses);
            this.tabCtrlLocations.Location = new System.Drawing.Point(2, 0);
            this.tabCtrlLocations.Name = "tabCtrlLocations";
            this.tabCtrlLocations.SelectedIndex = 0;
            this.tabCtrlLocations.Size = new System.Drawing.Size(929, 402);
            this.tabCtrlLocations.TabIndex = 0;
            // 
            // tabCountries
            // 
            this.tabCountries.Controls.Add(this.dgCountries);
            this.tabCountries.Location = new System.Drawing.Point(4, 22);
            this.tabCountries.Name = "tabCountries";
            this.tabCountries.Padding = new System.Windows.Forms.Padding(3);
            this.tabCountries.Size = new System.Drawing.Size(921, 376);
            this.tabCountries.TabIndex = 0;
            this.tabCountries.Text = "Countries";
            this.tabCountries.UseVisualStyleBackColor = true;
            // 
            // dgCountries
            // 
            this.dgCountries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCountries.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dgCountries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCountries.Location = new System.Drawing.Point(0, 3);
            this.dgCountries.Name = "dgCountries";
            this.dgCountries.Size = new System.Drawing.Size(918, 371);
            this.dgCountries.TabIndex = 0;
            this.toolTips.SetToolTip(this.dgCountries, "Double click on Country name to see list of individuals with that Country.");
            this.dgCountries.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCountries_CellDoubleClick);
            // 
            // tabRegions
            // 
            this.tabRegions.Controls.Add(this.dgRegions);
            this.tabRegions.Location = new System.Drawing.Point(4, 22);
            this.tabRegions.Name = "tabRegions";
            this.tabRegions.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegions.Size = new System.Drawing.Size(921, 376);
            this.tabRegions.TabIndex = 1;
            this.tabRegions.Text = "Regions";
            this.tabRegions.UseVisualStyleBackColor = true;
            // 
            // dgRegions
            // 
            this.dgRegions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgRegions.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgRegions.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgRegions.Location = new System.Drawing.Point(2, 3);
            this.dgRegions.Name = "dgRegions";
            this.dgRegions.Size = new System.Drawing.Size(918, 370);
            this.dgRegions.TabIndex = 1;
            this.toolTips.SetToolTip(this.dgRegions, "Double click on Region name to see list of individuals with that Region.");
            this.dgRegions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRegions_CellDoubleClick);
            // 
            // tabParishes
            // 
            this.tabParishes.Controls.Add(this.dgParishes);
            this.tabParishes.Location = new System.Drawing.Point(4, 22);
            this.tabParishes.Name = "tabParishes";
            this.tabParishes.Size = new System.Drawing.Size(921, 376);
            this.tabParishes.TabIndex = 2;
            this.tabParishes.Text = "Parishes";
            this.tabParishes.UseVisualStyleBackColor = true;
            // 
            // dgParishes
            // 
            this.dgParishes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgParishes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgParishes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgParishes.Location = new System.Drawing.Point(2, 3);
            this.dgParishes.Name = "dgParishes";
            this.dgParishes.Size = new System.Drawing.Size(918, 370);
            this.dgParishes.TabIndex = 1;
            this.dgParishes.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgParishes_CellDoubleClick);
            // 
            // tabAddresses
            // 
            this.tabAddresses.Controls.Add(this.dgAddresses);
            this.tabAddresses.Location = new System.Drawing.Point(4, 22);
            this.tabAddresses.Name = "tabAddresses";
            this.tabAddresses.Size = new System.Drawing.Size(921, 376);
            this.tabAddresses.TabIndex = 3;
            this.tabAddresses.Text = "Addresses";
            this.tabAddresses.UseVisualStyleBackColor = true;
            // 
            // dgAddresses
            // 
            this.dgAddresses.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgAddresses.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAddresses.Location = new System.Drawing.Point(2, 3);
            this.dgAddresses.Name = "dgAddresses";
            this.dgAddresses.Size = new System.Drawing.Size(918, 370);
            this.dgAddresses.TabIndex = 1;
            this.dgAddresses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAddresses_CellDoubleClick);
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
            this.dgLooseDeaths.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgLooseDeaths.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLooseDeaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLooseDeaths.Location = new System.Drawing.Point(0, 0);
            this.dgLooseDeaths.Name = "dgLooseDeaths";
            this.dgLooseDeaths.Size = new System.Drawing.Size(931, 402);
            this.dgLooseDeaths.TabIndex = 0;
            // 
            // tabCensus
            // 
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
            this.udAgeFilter.Size = new System.Drawing.Size(41, 20);
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
            // tabLostCousins
            // 
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
            this.linkLabel2.Location = new System.Drawing.Point(211, 253);
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
            this.linkLabel1.Location = new System.Drawing.Point(19, 253);
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
            // tabIGISearch
            // 
            this.tabIGISearch.Controls.Add(this.label7);
            this.tabIGISearch.Controls.Add(this.txtIGISurname);
            this.tabIGISearch.Controls.Add(this.btnOpenFolder);
            this.tabIGISearch.Controls.Add(this.groupBox1);
            this.tabIGISearch.Controls.Add(this.btnCancelIGISearch);
            this.tabIGISearch.Controls.Add(this.btnViewResults);
            this.tabIGISearch.Controls.Add(this.pbIGISearch);
            this.tabIGISearch.Controls.Add(this.btnIGIMarriageSearch);
            this.tabIGISearch.Controls.Add(this.btnIGIChildrenSearch);
            this.tabIGISearch.Controls.Add(this.btnIGIFolderBrowse);
            this.tabIGISearch.Controls.Add(this.txtIGIfolder);
            this.tabIGISearch.Controls.Add(this.label3);
            this.tabIGISearch.Controls.Add(this.rtbIGIResults);
            this.tabIGISearch.Controls.Add(this.IGIrelationTypes);
            this.tabIGISearch.Controls.Add(this.IGIDefaultCountry);
            this.tabIGISearch.Location = new System.Drawing.Point(4, 22);
            this.tabIGISearch.Name = "tabIGISearch";
            this.tabIGISearch.Size = new System.Drawing.Size(931, 402);
            this.tabIGISearch.TabIndex = 6;
            this.tabIGISearch.Text = "IGI Search";
            this.tabIGISearch.UseVisualStyleBackColor = true;
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
            // txtIGISurname
            // 
            this.txtIGISurname.Location = new System.Drawing.Point(561, 122);
            this.txtIGISurname.Name = "txtIGISurname";
            this.txtIGISurname.Size = new System.Drawing.Size(201, 20);
            this.txtIGISurname.TabIndex = 21;
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
            this.groupBox1.Controls.Add(this.rbIGISearchRegion);
            this.groupBox1.Controls.Add(this.rbIGISearchCountry);
            this.groupBox1.Location = new System.Drawing.Point(598, 38);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(164, 74);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search By";
            // 
            // rbIGISearchRegion
            // 
            this.rbIGISearchRegion.AutoSize = true;
            this.rbIGISearchRegion.Location = new System.Drawing.Point(7, 44);
            this.rbIGISearchRegion.Name = "rbIGISearchRegion";
            this.rbIGISearchRegion.Size = new System.Drawing.Size(147, 17);
            this.rbIGISearchRegion.TabIndex = 1;
            this.rbIGISearchRegion.TabStop = true;
            this.rbIGISearchRegion.Text = "County / State / Province";
            this.rbIGISearchRegion.UseVisualStyleBackColor = true;
            // 
            // rbIGISearchCountry
            // 
            this.rbIGISearchCountry.AutoSize = true;
            this.rbIGISearchCountry.Checked = true;
            this.rbIGISearchCountry.Location = new System.Drawing.Point(7, 20);
            this.rbIGISearchCountry.Name = "rbIGISearchCountry";
            this.rbIGISearchCountry.Size = new System.Drawing.Size(61, 17);
            this.rbIGISearchCountry.TabIndex = 0;
            this.rbIGISearchCountry.TabStop = true;
            this.rbIGISearchCountry.Text = "Country";
            this.rbIGISearchCountry.UseVisualStyleBackColor = true;
            // 
            // btnCancelIGISearch
            // 
            this.btnCancelIGISearch.Location = new System.Drawing.Point(266, 118);
            this.btnCancelIGISearch.Name = "btnCancelIGISearch";
            this.btnCancelIGISearch.Size = new System.Drawing.Size(123, 27);
            this.btnCancelIGISearch.TabIndex = 9;
            this.btnCancelIGISearch.Text = "Cancel Search";
            this.btnCancelIGISearch.UseVisualStyleBackColor = true;
            this.btnCancelIGISearch.Click += new System.EventHandler(this.btnCancelIGISearch_Click);
            // 
            // btnViewResults
            // 
            this.btnViewResults.Location = new System.Drawing.Point(266, 118);
            this.btnViewResults.Name = "btnViewResults";
            this.btnViewResults.Size = new System.Drawing.Size(123, 27);
            this.btnViewResults.TabIndex = 8;
            this.btnViewResults.Text = "View Results";
            this.btnViewResults.UseVisualStyleBackColor = true;
            this.btnViewResults.Click += new System.EventHandler(this.btnViewResults_Click);
            // 
            // pbIGISearch
            // 
            this.pbIGISearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pbIGISearch.Location = new System.Drawing.Point(8, 151);
            this.pbIGISearch.Name = "pbIGISearch";
            this.pbIGISearch.Size = new System.Drawing.Size(915, 20);
            this.pbIGISearch.TabIndex = 6;
            this.pbIGISearch.Visible = false;
            // 
            // btnIGIMarriageSearch
            // 
            this.btnIGIMarriageSearch.Location = new System.Drawing.Point(8, 118);
            this.btnIGIMarriageSearch.Name = "btnIGIMarriageSearch";
            this.btnIGIMarriageSearch.Size = new System.Drawing.Size(123, 27);
            this.btnIGIMarriageSearch.TabIndex = 5;
            this.btnIGIMarriageSearch.Text = "Start Marriage Search";
            this.btnIGIMarriageSearch.UseVisualStyleBackColor = true;
            this.btnIGIMarriageSearch.Click += new System.EventHandler(this.btnIGIMarriageSearch_Click);
            // 
            // btnIGIChildrenSearch
            // 
            this.btnIGIChildrenSearch.Location = new System.Drawing.Point(137, 118);
            this.btnIGIChildrenSearch.Name = "btnIGIChildrenSearch";
            this.btnIGIChildrenSearch.Size = new System.Drawing.Size(123, 27);
            this.btnIGIChildrenSearch.TabIndex = 4;
            this.btnIGIChildrenSearch.Text = "Start Children Search";
            this.btnIGIChildrenSearch.UseVisualStyleBackColor = true;
            this.btnIGIChildrenSearch.Click += new System.EventHandler(this.btnIGIChildrenSearch_Click);
            // 
            // btnIGIFolderBrowse
            // 
            this.btnIGIFolderBrowse.Location = new System.Drawing.Point(515, 11);
            this.btnIGIFolderBrowse.Name = "btnIGIFolderBrowse";
            this.btnIGIFolderBrowse.Size = new System.Drawing.Size(77, 21);
            this.btnIGIFolderBrowse.TabIndex = 2;
            this.btnIGIFolderBrowse.Text = "Browse ...";
            this.btnIGIFolderBrowse.UseVisualStyleBackColor = true;
            this.btnIGIFolderBrowse.Click += new System.EventHandler(this.btnIGIFolderBrowse_Click);
            // 
            // txtIGIfolder
            // 
            this.txtIGIfolder.Location = new System.Drawing.Point(137, 12);
            this.txtIGIfolder.Name = "txtIGIfolder";
            this.txtIGIfolder.Size = new System.Drawing.Size(372, 20);
            this.txtIGIfolder.TabIndex = 1;
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
            // tabTreetops
            // 
            this.tabTreetops.Controls.Add(this.panel1);
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
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.dgTreeTops);
            this.panel1.Location = new System.Drawing.Point(8, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(920, 307);
            this.panel1.TabIndex = 26;
            // 
            // dgTreeTops
            // 
            this.dgTreeTops.AllowUserToAddRows = false;
            this.dgTreeTops.AllowUserToDeleteRows = false;
            this.dgTreeTops.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgTreeTops.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTreeTops.Location = new System.Drawing.Point(0, 0);
            this.dgTreeTops.Name = "dgTreeTops";
            this.dgTreeTops.ReadOnly = true;
            this.dgTreeTops.Size = new System.Drawing.Size(920, 307);
            this.dgTreeTops.TabIndex = 1;
            // 
            // btnTreeTops
            // 
            this.btnTreeTops.Location = new System.Drawing.Point(598, 59);
            this.btnTreeTops.Name = "btnTreeTops";
            this.btnTreeTops.Size = new System.Drawing.Size(253, 27);
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
            // tabWarDead
            // 
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
            // btnWWII
            // 
            this.btnWWII.Location = new System.Drawing.Point(758, 58);
            this.btnWWII.Name = "btnWWII";
            this.btnWWII.Size = new System.Drawing.Size(95, 23);
            this.btnWWII.TabIndex = 31;
            this.btnWWII.Text = "World War II";
            this.btnWWII.UseVisualStyleBackColor = true;
            this.btnWWII.Click += new System.EventHandler(this.btnWWII_Click);
            // 
            // btnWWI
            // 
            this.btnWWI.Location = new System.Drawing.Point(652, 58);
            this.btnWWI.Name = "btnWWI";
            this.btnWWI.Size = new System.Drawing.Size(95, 23);
            this.btnWWI.TabIndex = 30;
            this.btnWWI.Text = "World War I";
            this.btnWWI.UseVisualStyleBackColor = true;
            this.btnWWI.Click += new System.EventHandler(this.btnWWI_Click);
            // 
            // dgWarDead
            // 
            this.dgWarDead.AllowUserToAddRows = false;
            this.dgWarDead.AllowUserToDeleteRows = false;
            this.dgWarDead.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgWarDead.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgWarDead.Location = new System.Drawing.Point(3, 92);
            this.dgWarDead.Name = "dgWarDead";
            this.dgWarDead.ReadOnly = true;
            this.dgWarDead.Size = new System.Drawing.Size(920, 307);
            this.dgWarDead.TabIndex = 29;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(597, 25);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(49, 13);
            this.label9.TabIndex = 28;
            this.label9.Text = "Surname";
            // 
            // txtWarDeadSurname
            // 
            this.txtWarDeadSurname.Location = new System.Drawing.Point(652, 22);
            this.txtWarDeadSurname.Name = "txtWarDeadSurname";
            this.txtWarDeadSurname.Size = new System.Drawing.Size(201, 20);
            this.txtWarDeadSurname.TabIndex = 27;
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
            this.tsCountLabel.Size = new System.Drawing.Size(55, 17);
            this.tsCountLabel.Text = "Count : 0";
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
            // cenDate
            // 
            this.cenDate.AutoSize = true;
            this.cenDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cenDate.Country = "Scotland";
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
            // rtbIGIResults
            // 
            this.rtbIGIResults.Location = new System.Drawing.Point(0, 177);
            this.rtbIGIResults.Name = "rtbIGIResults";
            this.rtbIGIResults.Size = new System.Drawing.Size(931, 229);
            this.rtbIGIResults.TabIndex = 12;
            this.rtbIGIResults.Text = "";
            this.rtbIGIResults.TextChanged += new System.EventHandler(this.rtbIGIResults_TextChanged);
            // 
            // IGIrelationTypes
            // 
            this.IGIrelationTypes.Location = new System.Drawing.Point(270, 38);
            this.IGIrelationTypes.Name = "IGIrelationTypes";
            this.IGIrelationTypes.Size = new System.Drawing.Size(322, 74);
            this.IGIrelationTypes.TabIndex = 10;
            // 
            // IGIDefaultCountry
            // 
            this.IGIDefaultCountry.Location = new System.Drawing.Point(8, 38);
            this.IGIDefaultCountry.Name = "IGIDefaultCountry";
            this.IGIDefaultCountry.Size = new System.Drawing.Size(256, 74);
            this.IGIDefaultCountry.TabIndex = 7;
            this.IGIDefaultCountry.Title = "Default Country";
            this.IGIDefaultCountry.UKEnabled = false;
            this.IGIDefaultCountry.CountryChanged += new System.EventHandler(this.IGIDefaultCountry_CountryChanged);
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
            // wardeadRelation
            // 
            this.wardeadRelation.Location = new System.Drawing.Point(272, 12);
            this.wardeadRelation.Name = "wardeadRelation";
            this.wardeadRelation.Size = new System.Drawing.Size(322, 74);
            this.wardeadRelation.TabIndex = 26;
            // 
            // wardeadCountry
            // 
            this.wardeadCountry.Location = new System.Drawing.Point(10, 12);
            this.wardeadCountry.Name = "wardeadCountry";
            this.wardeadCountry.Size = new System.Drawing.Size(256, 74);
            this.wardeadCountry.TabIndex = 25;
            this.wardeadCountry.Title = "Default Country";
            this.wardeadCountry.UKEnabled = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(939, 480);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabSelector);
            this.Controls.Add(this.menuStrip1);
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
            this.tabLooseDeaths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).EndInit();
            this.tabCensus.ResumeLayout(false);
            this.tabCensus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).EndInit();
            this.tabLostCousins.ResumeLayout(false);
            this.tabLostCousins.PerformLayout();
            this.tabIGISearch.ResumeLayout(false);
            this.tabIGISearch.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabTreetops.ResumeLayout(false);
            this.tabTreetops.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgTreeTops)).EndInit();
            this.tabWarDead.ResumeLayout(false);
            this.tabWarDead.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWarDead)).EndInit();
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
        private System.Windows.Forms.ToolStripMenuItem showLocationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TabPage tabIGISearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnIGIFolderBrowse;
        private System.Windows.Forms.TextBox txtIGIfolder;
        private System.Windows.Forms.Button btnIGIMarriageSearch;
        private System.Windows.Forms.Button btnIGIChildrenSearch;
        private System.Windows.Forms.ProgressBar pbIGISearch;
        private System.Windows.Forms.CheckBox ckbLCResidence;
        private System.Windows.Forms.CheckBox ckbCensusResidence;
        private Controls.RelationTypes relationTypes;
        private Controls.CensusCountry censusCountry;
        private Controls.CensusDateSelector cenDate;
        private Controls.CensusCountry IGIDefaultCountry;
        private System.Windows.Forms.Button btnViewResults;
        private System.Windows.Forms.ToolTip toolTips;
        private System.Windows.Forms.Button btnCancelIGISearch;
        private Controls.RelationTypes IGIrelationTypes;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbIGISearchRegion;
        private System.Windows.Forms.RadioButton rbIGISearchCountry;
        private global::FTAnalyzer.Utilities.ScrollingRichTextBox rtbOutput;
        private global::FTAnalyzer.Utilities.ScrollingRichTextBox rtbIGIResults;
        private System.Windows.Forms.Button btnOpenFolder;
        private System.Windows.Forms.ToolStripMenuItem reportsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BirthRegistrationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deathRegistrationsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem marriageRegistrationsToolStripMenuItem;
        private System.Windows.Forms.Button btnLC1911EW;
        private System.Windows.Forms.CheckBox ckbNoLocations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSurname;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtIGISurname;
        private System.Windows.Forms.TabPage tabTreetops;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTreetopsSurname;
        private Controls.RelationTypes treetopsRelation;
        private Controls.CensusCountry treetopsCountry;
        private System.Windows.Forms.Button btnTreeTops;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgTreeTops;
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
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog;
        private System.Windows.Forms.PrintDialog printDialog;
        private System.Drawing.Printing.PrintDocument printDocument;
        private System.Windows.Forms.CheckBox ckbLCIgnoreCountry;
    }
}

