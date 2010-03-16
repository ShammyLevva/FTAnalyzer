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
            this.openGedcom = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showLocationsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkForUpdatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDisplayProgress = new System.Windows.Forms.TabPage();
            this.rtbOutput = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pbFamilies = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.pbIndividuals = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.pbSources = new System.Windows.Forms.ProgressBar();
            this.tabIndividuals = new System.Windows.Forms.TabPage();
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.tabCensus = new System.Windows.Forms.TabPage();
            this.ckbCensusResidence = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.udAgeFilter = new System.Windows.Forms.NumericUpDown();
            this.btnShowResults = new System.Windows.Forms.Button();
            this.tabLostCousins = new System.Windows.Forms.TabPage();
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
            this.tabLooseDeaths = new System.Windows.Forms.TabPage();
            this.dgLooseDeaths = new System.Windows.Forms.DataGridView();
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
            this.tabIGISearch = new System.Windows.Forms.TabPage();
            this.pbIGISearch = new System.Windows.Forms.ProgressBar();
            this.btnIGIMarriageSearch = new System.Windows.Forms.Button();
            this.btnIGIChildrenSearch = new System.Windows.Forms.Button();
            this.rtbIGIResults = new System.Windows.Forms.RichTextBox();
            this.btnIGIFolderBrowse = new System.Windows.Forms.Button();
            this.txtIGIfolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.cenDate = new Controls.CensusDateSelector();
            this.censusCountry = new Controls.CensusCountry();
            this.relationTypes = new Controls.RelationTypes();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDisplayProgress.SuspendLayout();
            this.tabIndividuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.tabCensus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).BeginInit();
            this.tabLostCousins.SuspendLayout();
            this.tabLooseDeaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).BeginInit();
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
            this.tabIGISearch.SuspendLayout();
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
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
            this.openToolStripMenuItem.Text = "Open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showLocationsToolStripMenuItem,
            this.optionsToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // showLocationsToolStripMenuItem
            // 
            this.showLocationsToolStripMenuItem.Name = "showLocationsToolStripMenuItem";
            this.showLocationsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.showLocationsToolStripMenuItem.Text = "Show locations";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
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
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // checkForUpdatesToolStripMenuItem
            // 
            this.checkForUpdatesToolStripMenuItem.Name = "checkForUpdatesToolStripMenuItem";
            this.checkForUpdatesToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.checkForUpdatesToolStripMenuItem.Text = "Check for Updates";
            this.checkForUpdatesToolStripMenuItem.Click += new System.EventHandler(this.checkForUpdatesToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(171, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabDisplayProgress);
            this.tabControl.Controls.Add(this.tabIndividuals);
            this.tabControl.Controls.Add(this.tabCensus);
            this.tabControl.Controls.Add(this.tabLostCousins);
            this.tabControl.Controls.Add(this.tabLooseDeaths);
            this.tabControl.Controls.Add(this.tabLocations);
            this.tabControl.Controls.Add(this.tabIGISearch);
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(960, 420);
            this.tabControl.TabIndex = 9;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabDisplayProgress
            // 
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
            this.tabDisplayProgress.Size = new System.Drawing.Size(952, 394);
            this.tabDisplayProgress.TabIndex = 1;
            this.tabDisplayProgress.Text = "Load Gedcom";
            this.tabDisplayProgress.UseVisualStyleBackColor = true;
            // 
            // rtbOutput
            // 
            this.rtbOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbOutput.Location = new System.Drawing.Point(10, 89);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.Size = new System.Drawing.Size(936, 302);
            this.rtbOutput.TabIndex = 6;
            this.rtbOutput.Text = "";
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
            this.tabIndividuals.Size = new System.Drawing.Size(952, 394);
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
            this.dgIndividuals.Size = new System.Drawing.Size(946, 388);
            this.dgIndividuals.TabIndex = 0;
            this.dgIndividuals.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgIndividuals_ColumnHeaderMouseClick);
            // 
            // tabCensus
            // 
            this.tabCensus.Controls.Add(this.cenDate);
            this.tabCensus.Controls.Add(this.censusCountry);
            this.tabCensus.Controls.Add(this.relationTypes);
            this.tabCensus.Controls.Add(this.ckbCensusResidence);
            this.tabCensus.Controls.Add(this.label2);
            this.tabCensus.Controls.Add(this.udAgeFilter);
            this.tabCensus.Controls.Add(this.btnShowResults);
            this.tabCensus.Location = new System.Drawing.Point(4, 22);
            this.tabCensus.Name = "tabCensus";
            this.tabCensus.Padding = new System.Windows.Forms.Padding(3);
            this.tabCensus.Size = new System.Drawing.Size(952, 394);
            this.tabCensus.TabIndex = 0;
            this.tabCensus.Text = "Census";
            this.tabCensus.UseVisualStyleBackColor = true;
            // 
            // ckbCensusResidence
            // 
            this.ckbCensusResidence.AutoSize = true;
            this.ckbCensusResidence.Location = new System.Drawing.Point(19, 119);
            this.ckbCensusResidence.Name = "ckbCensusResidence";
            this.ckbCensusResidence.Size = new System.Drawing.Size(213, 17);
            this.ckbCensusResidence.TabIndex = 14;
            this.ckbCensusResidence.Text = "Treat \'Residence\' facts as Census facts";
            this.ckbCensusResidence.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Exclude individuals over the age of ";
            // 
            // udAgeFilter
            // 
            this.udAgeFilter.Location = new System.Drawing.Point(197, 93);
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
            this.btnShowResults.Location = new System.Drawing.Point(19, 142);
            this.btnShowResults.Name = "btnShowResults";
            this.btnShowResults.Size = new System.Drawing.Size(82, 25);
            this.btnShowResults.TabIndex = 4;
            this.btnShowResults.Text = "Show Results";
            this.btnShowResults.UseVisualStyleBackColor = true;
            this.btnShowResults.Click += new System.EventHandler(this.btnShowResults_Click);
            // 
            // tabLostCousins
            // 
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
            this.tabLostCousins.Size = new System.Drawing.Size(952, 394);
            this.tabLostCousins.TabIndex = 5;
            this.tabLostCousins.Text = "Lost Cousins";
            this.tabLostCousins.UseVisualStyleBackColor = true;
            // 
            // ckbLCResidence
            // 
            this.ckbLCResidence.AutoSize = true;
            this.ckbLCResidence.Location = new System.Drawing.Point(22, 68);
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
            this.linkLabel1.Location = new System.Drawing.Point(19, 189);
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
            this.btnLC1841EW.Location = new System.Drawing.Point(22, 133);
            this.btnLC1841EW.Name = "btnLC1841EW";
            this.btnLC1841EW.Size = new System.Drawing.Size(162, 36);
            this.btnLC1841EW.TabIndex = 8;
            this.btnLC1841EW.Text = "1841 England && Wales Census";
            this.btnLC1841EW.UseVisualStyleBackColor = true;
            this.btnLC1841EW.Click += new System.EventHandler(this.btnLC1841EW_Click);
            // 
            // btnLC1911Ireland
            // 
            this.btnLC1911Ireland.Location = new System.Drawing.Point(358, 133);
            this.btnLC1911Ireland.Name = "btnLC1911Ireland";
            this.btnLC1911Ireland.Size = new System.Drawing.Size(162, 36);
            this.btnLC1911Ireland.TabIndex = 7;
            this.btnLC1911Ireland.Text = "1911 Ireland Census";
            this.btnLC1911Ireland.UseVisualStyleBackColor = true;
            this.btnLC1911Ireland.Click += new System.EventHandler(this.btnLC1911Ireland_Click);
            // 
            // btnLC1880USA
            // 
            this.btnLC1880USA.Location = new System.Drawing.Point(190, 133);
            this.btnLC1880USA.Name = "btnLC1880USA";
            this.btnLC1880USA.Size = new System.Drawing.Size(162, 36);
            this.btnLC1880USA.TabIndex = 6;
            this.btnLC1880USA.Text = "1880 US Census";
            this.btnLC1880USA.UseVisualStyleBackColor = true;
            this.btnLC1880USA.Click += new System.EventHandler(this.btnLC1880USA_Click);
            // 
            // btnLC1881EW
            // 
            this.btnLC1881EW.Location = new System.Drawing.Point(22, 91);
            this.btnLC1881EW.Name = "btnLC1881EW";
            this.btnLC1881EW.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881EW.TabIndex = 5;
            this.btnLC1881EW.Text = "1881 England && Wales Census";
            this.btnLC1881EW.UseVisualStyleBackColor = true;
            this.btnLC1881EW.Click += new System.EventHandler(this.btnLC1881EW_Click);
            // 
            // btnLC1881Canada
            // 
            this.btnLC1881Canada.Location = new System.Drawing.Point(358, 91);
            this.btnLC1881Canada.Name = "btnLC1881Canada";
            this.btnLC1881Canada.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881Canada.TabIndex = 4;
            this.btnLC1881Canada.Text = "1881 Canada Census";
            this.btnLC1881Canada.UseVisualStyleBackColor = true;
            this.btnLC1881Canada.Click += new System.EventHandler(this.btnLC1881Canada_Click);
            // 
            // btnLC1881Scot
            // 
            this.btnLC1881Scot.Location = new System.Drawing.Point(190, 91);
            this.btnLC1881Scot.Name = "btnLC1881Scot";
            this.btnLC1881Scot.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881Scot.TabIndex = 0;
            this.btnLC1881Scot.Text = "1881 Scotland Census";
            this.btnLC1881Scot.UseVisualStyleBackColor = true;
            this.btnLC1881Scot.Click += new System.EventHandler(this.btnLC1881Scot_Click);
            // 
            // tabLooseDeaths
            // 
            this.tabLooseDeaths.Controls.Add(this.dgLooseDeaths);
            this.tabLooseDeaths.Location = new System.Drawing.Point(4, 22);
            this.tabLooseDeaths.Name = "tabLooseDeaths";
            this.tabLooseDeaths.Padding = new System.Windows.Forms.Padding(3);
            this.tabLooseDeaths.Size = new System.Drawing.Size(952, 394);
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
            this.dgLooseDeaths.Size = new System.Drawing.Size(952, 394);
            this.dgLooseDeaths.TabIndex = 0;
            // 
            // tabLocations
            // 
            this.tabLocations.Controls.Add(this.tabCtrlLocations);
            this.tabLocations.Location = new System.Drawing.Point(4, 22);
            this.tabLocations.Name = "tabLocations";
            this.tabLocations.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocations.Size = new System.Drawing.Size(952, 394);
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
            this.tabCtrlLocations.Size = new System.Drawing.Size(950, 394);
            this.tabCtrlLocations.TabIndex = 0;
            // 
            // tabCountries
            // 
            this.tabCountries.Controls.Add(this.dgCountries);
            this.tabCountries.Location = new System.Drawing.Point(4, 22);
            this.tabCountries.Name = "tabCountries";
            this.tabCountries.Padding = new System.Windows.Forms.Padding(3);
            this.tabCountries.Size = new System.Drawing.Size(942, 368);
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
            this.dgCountries.Size = new System.Drawing.Size(939, 363);
            this.dgCountries.TabIndex = 0;
            this.dgCountries.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCountries_CellContentDoubleClick);
            // 
            // tabRegions
            // 
            this.tabRegions.Controls.Add(this.dgRegions);
            this.tabRegions.Location = new System.Drawing.Point(4, 22);
            this.tabRegions.Name = "tabRegions";
            this.tabRegions.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegions.Size = new System.Drawing.Size(942, 368);
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
            this.dgRegions.Size = new System.Drawing.Size(939, 362);
            this.dgRegions.TabIndex = 1;
            this.dgRegions.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRegions_CellContentDoubleClick);
            // 
            // tabParishes
            // 
            this.tabParishes.Controls.Add(this.dgParishes);
            this.tabParishes.Location = new System.Drawing.Point(4, 22);
            this.tabParishes.Name = "tabParishes";
            this.tabParishes.Size = new System.Drawing.Size(942, 368);
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
            this.dgParishes.Size = new System.Drawing.Size(939, 362);
            this.dgParishes.TabIndex = 1;
            this.dgParishes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgParishes_CellContentDoubleClick);
            // 
            // tabAddresses
            // 
            this.tabAddresses.Controls.Add(this.dgAddresses);
            this.tabAddresses.Location = new System.Drawing.Point(4, 22);
            this.tabAddresses.Name = "tabAddresses";
            this.tabAddresses.Size = new System.Drawing.Size(942, 368);
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
            this.dgAddresses.Size = new System.Drawing.Size(939, 362);
            this.dgAddresses.TabIndex = 1;
            this.dgAddresses.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAddresses_CellContentDoubleClick);
            // 
            // tabIGISearch
            // 
            this.tabIGISearch.Controls.Add(this.pbIGISearch);
            this.tabIGISearch.Controls.Add(this.btnIGIMarriageSearch);
            this.tabIGISearch.Controls.Add(this.btnIGIChildrenSearch);
            this.tabIGISearch.Controls.Add(this.rtbIGIResults);
            this.tabIGISearch.Controls.Add(this.btnIGIFolderBrowse);
            this.tabIGISearch.Controls.Add(this.txtIGIfolder);
            this.tabIGISearch.Controls.Add(this.label3);
            this.tabIGISearch.Location = new System.Drawing.Point(4, 22);
            this.tabIGISearch.Name = "tabIGISearch";
            this.tabIGISearch.Size = new System.Drawing.Size(952, 394);
            this.tabIGISearch.TabIndex = 6;
            this.tabIGISearch.Text = "IGI Search";
            this.tabIGISearch.UseVisualStyleBackColor = true;
            // 
            // pbIGISearch
            // 
            this.pbIGISearch.Location = new System.Drawing.Point(552, 12);
            this.pbIGISearch.Name = "pbIGISearch";
            this.pbIGISearch.Size = new System.Drawing.Size(392, 24);
            this.pbIGISearch.TabIndex = 6;
            // 
            // btnIGIMarriageSearch
            // 
            this.btnIGIMarriageSearch.Location = new System.Drawing.Point(692, 68);
            this.btnIGIMarriageSearch.Name = "btnIGIMarriageSearch";
            this.btnIGIMarriageSearch.Size = new System.Drawing.Size(123, 27);
            this.btnIGIMarriageSearch.TabIndex = 5;
            this.btnIGIMarriageSearch.Text = "Start Marriage Search";
            this.btnIGIMarriageSearch.UseVisualStyleBackColor = true;
            this.btnIGIMarriageSearch.Click += new System.EventHandler(this.btnIGIMarriageSearch_Click);
            // 
            // btnIGIChildrenSearch
            // 
            this.btnIGIChildrenSearch.Location = new System.Drawing.Point(821, 68);
            this.btnIGIChildrenSearch.Name = "btnIGIChildrenSearch";
            this.btnIGIChildrenSearch.Size = new System.Drawing.Size(123, 27);
            this.btnIGIChildrenSearch.TabIndex = 4;
            this.btnIGIChildrenSearch.Text = "Start Children Search";
            this.btnIGIChildrenSearch.UseVisualStyleBackColor = true;
            this.btnIGIChildrenSearch.Click += new System.EventHandler(this.btnIGIChildrenSearch_Click);
            // 
            // rtbIGIResults
            // 
            this.rtbIGIResults.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbIGIResults.Location = new System.Drawing.Point(0, 101);
            this.rtbIGIResults.Name = "rtbIGIResults";
            this.rtbIGIResults.Size = new System.Drawing.Size(949, 293);
            this.rtbIGIResults.TabIndex = 3;
            this.rtbIGIResults.Text = "";
            // 
            // btnIGIFolderBrowse
            // 
            this.btnIGIFolderBrowse.Location = new System.Drawing.Point(457, 11);
            this.btnIGIFolderBrowse.Name = "btnIGIFolderBrowse";
            this.btnIGIFolderBrowse.Size = new System.Drawing.Size(77, 21);
            this.btnIGIFolderBrowse.TabIndex = 2;
            this.btnIGIFolderBrowse.Text = "Browse ...";
            this.btnIGIFolderBrowse.UseVisualStyleBackColor = true;
            this.btnIGIFolderBrowse.Click += new System.EventHandler(this.btnIGIFolderBrowse_Click);
            // 
            // txtIGIfolder
            // 
            this.txtIGIfolder.Location = new System.Drawing.Point(147, 12);
            this.txtIGIfolder.Name = "txtIGIfolder";
            this.txtIGIfolder.Size = new System.Drawing.Size(296, 20);
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
            this.statusStrip.Location = new System.Drawing.Point(0, 450);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(960, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsCountLabel
            // 
            this.tsCountLabel.Name = "tsCountLabel";
            this.tsCountLabel.Size = new System.Drawing.Size(52, 17);
            this.tsCountLabel.Text = "Count : 0";
            // 
            // cenDate
            // 
            this.cenDate.AutoSize = true;
            this.cenDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cenDate.Country = "Scotland";
            this.cenDate.Location = new System.Drawing.Point(345, 86);
            this.cenDate.Name = "cenDate";
            this.cenDate.Size = new System.Drawing.Size(186, 27);
            this.cenDate.TabIndex = 17;
            this.cenDate.CensusChanged += new System.EventHandler(this.cenDate_CensusChanged);
            // 
            // censusCountry
            // 
            this.censusCountry.Location = new System.Drawing.Point(8, 9);
            this.censusCountry.Name = "censusCountry";
            this.censusCountry.Size = new System.Drawing.Size(331, 78);
            this.censusCountry.TabIndex = 16;
            this.censusCountry.CountryChanged += new System.EventHandler(this.censusCountry_CountryChanged);
            // 
            // relationTypes
            // 
            this.relationTypes.Location = new System.Drawing.Point(345, 6);
            this.relationTypes.Name = "relationTypes";
            this.relationTypes.Size = new System.Drawing.Size(325, 78);
            this.relationTypes.TabIndex = 15;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 472);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Family Tree Analyzer";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabDisplayProgress.ResumeLayout(false);
            this.tabDisplayProgress.PerformLayout();
            this.tabIndividuals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            this.tabCensus.ResumeLayout(false);
            this.tabCensus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).EndInit();
            this.tabLostCousins.ResumeLayout(false);
            this.tabLostCousins.PerformLayout();
            this.tabLooseDeaths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).EndInit();
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
            this.tabIGISearch.ResumeLayout(false);
            this.tabIGISearch.PerformLayout();
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
        private System.Windows.Forms.TabControl tabControl;
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
        private System.Windows.Forms.RichTextBox rtbOutput;
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
        private System.Windows.Forms.RichTextBox rtbIGIResults;
        private System.Windows.Forms.Button btnIGIMarriageSearch;
        private System.Windows.Forms.Button btnIGIChildrenSearch;
        private System.Windows.Forms.ProgressBar pbIGISearch;
        private System.Windows.Forms.CheckBox ckbLCResidence;
        private System.Windows.Forms.CheckBox ckbCensusResidence;
        private Controls.RelationTypes relationTypes;
        private Controls.CensusCountry censusCountry;
        private Controls.CensusDateSelector cenDate;
    }
}

