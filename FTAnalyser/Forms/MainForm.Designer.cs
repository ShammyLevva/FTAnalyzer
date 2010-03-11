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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.btnShowResults = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.ckbUnknown = new System.Windows.Forms.CheckBox();
            this.ckbMarriageDB = new System.Windows.Forms.CheckBox();
            this.ckbMarriage = new System.Windows.Forms.CheckBox();
            this.ckbBlood = new System.Windows.Forms.CheckBox();
            this.ckbDirects = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbCensusDate = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbUSA = new System.Windows.Forms.RadioButton();
            this.rbCanada = new System.Windows.Forms.RadioButton();
            this.rbGB = new System.Windows.Forms.RadioButton();
            this.rbWales = new System.Windows.Forms.RadioButton();
            this.rbEngland = new System.Windows.Forms.RadioButton();
            this.rbScotland = new System.Windows.Forms.RadioButton();
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
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabLostCousins = new System.Windows.Forms.TabPage();
            this.btnLC1881Scot = new System.Windows.Forms.Button();
            this.btnLC1881Canada = new System.Windows.Forms.Button();
            this.btnLC1881EW = new System.Windows.Forms.Button();
            this.btnLC1841EW = new System.Windows.Forms.Button();
            this.btnLC1911Ireland = new System.Windows.Forms.Button();
            this.btnLC1880USA = new System.Windows.Forms.Button();
            this.ckbRestrictions = new System.Windows.Forms.CheckBox();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDisplayProgress.SuspendLayout();
            this.tabIndividuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.tabCensus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.statusStrip.SuspendLayout();
            this.tabLostCousins.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1009, 24);
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
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
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
            this.tabControl.Location = new System.Drawing.Point(0, 27);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1009, 446);
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
            this.tabDisplayProgress.Size = new System.Drawing.Size(1001, 420);
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
            this.rtbOutput.Size = new System.Drawing.Size(979, 325);
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
            this.tabIndividuals.Size = new System.Drawing.Size(1001, 420);
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
            this.dgIndividuals.Size = new System.Drawing.Size(995, 414);
            this.dgIndividuals.TabIndex = 0;
            this.dgIndividuals.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgIndividuals_ColumnHeaderMouseClick);
            // 
            // tabCensus
            // 
            this.tabCensus.Controls.Add(this.btnShowResults);
            this.tabCensus.Controls.Add(this.groupBox2);
            this.tabCensus.Controls.Add(this.label1);
            this.tabCensus.Controls.Add(this.cbCensusDate);
            this.tabCensus.Controls.Add(this.groupBox1);
            this.tabCensus.Location = new System.Drawing.Point(4, 22);
            this.tabCensus.Name = "tabCensus";
            this.tabCensus.Padding = new System.Windows.Forms.Padding(3);
            this.tabCensus.Size = new System.Drawing.Size(1001, 420);
            this.tabCensus.TabIndex = 0;
            this.tabCensus.Text = "Census";
            this.tabCensus.UseVisualStyleBackColor = true;
            // 
            // btnShowResults
            // 
            this.btnShowResults.Location = new System.Drawing.Point(257, 93);
            this.btnShowResults.Name = "btnShowResults";
            this.btnShowResults.Size = new System.Drawing.Size(82, 25);
            this.btnShowResults.TabIndex = 4;
            this.btnShowResults.Text = "Show Results";
            this.btnShowResults.UseVisualStyleBackColor = true;
            this.btnShowResults.Click += new System.EventHandler(this.btnShowResults_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.ckbUnknown);
            this.groupBox2.Controls.Add(this.ckbMarriageDB);
            this.groupBox2.Controls.Add(this.ckbMarriage);
            this.groupBox2.Controls.Add(this.ckbBlood);
            this.groupBox2.Controls.Add(this.ckbDirects);
            this.groupBox2.Location = new System.Drawing.Point(345, 15);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(317, 72);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Relationship Types";
            // 
            // ckbUnknown
            // 
            this.ckbUnknown.AutoSize = true;
            this.ckbUnknown.Location = new System.Drawing.Point(243, 20);
            this.ckbUnknown.Name = "ckbUnknown";
            this.ckbUnknown.Size = new System.Drawing.Size(72, 17);
            this.ckbUnknown.TabIndex = 4;
            this.ckbUnknown.Text = "Unknown";
            this.ckbUnknown.UseVisualStyleBackColor = true;
            // 
            // ckbMarriageDB
            // 
            this.ckbMarriageDB.AutoSize = true;
            this.ckbMarriageDB.Checked = true;
            this.ckbMarriageDB.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbMarriageDB.Location = new System.Drawing.Point(116, 42);
            this.ckbMarriageDB.Name = "ckbMarriageDB";
            this.ckbMarriageDB.Size = new System.Drawing.Size(146, 17);
            this.ckbMarriageDB.TabIndex = 3;
            this.ckbMarriageDB.Text = "Married to Blood or Direct";
            this.ckbMarriageDB.UseVisualStyleBackColor = true;
            // 
            // ckbMarriage
            // 
            this.ckbMarriage.AutoSize = true;
            this.ckbMarriage.Location = new System.Drawing.Point(116, 19);
            this.ckbMarriage.Name = "ckbMarriage";
            this.ckbMarriage.Size = new System.Drawing.Size(121, 17);
            this.ckbMarriage.TabIndex = 2;
            this.ckbMarriage.Text = "Related by Marriage";
            this.ckbMarriage.UseVisualStyleBackColor = true;
            // 
            // ckbBlood
            // 
            this.ckbBlood.AutoSize = true;
            this.ckbBlood.Checked = true;
            this.ckbBlood.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbBlood.Location = new System.Drawing.Point(6, 43);
            this.ckbBlood.Name = "ckbBlood";
            this.ckbBlood.Size = new System.Drawing.Size(100, 17);
            this.ckbBlood.TabIndex = 1;
            this.ckbBlood.Text = "Blood Relations";
            this.ckbBlood.UseVisualStyleBackColor = true;
            // 
            // ckbDirects
            // 
            this.ckbDirects.AutoSize = true;
            this.ckbDirects.Checked = true;
            this.ckbDirects.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbDirects.Location = new System.Drawing.Point(6, 20);
            this.ckbDirects.Name = "ckbDirects";
            this.ckbDirects.Size = new System.Drawing.Size(104, 17);
            this.ckbDirects.TabIndex = 0;
            this.ckbDirects.Text = "Direct Ancestors";
            this.ckbDirects.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Census Date";
            // 
            // cbCensusDate
            // 
            this.cbCensusDate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbCensusDate.FormattingEnabled = true;
            this.cbCensusDate.Items.AddRange(new object[] {
            "1841",
            "1851",
            "1861",
            "1871",
            "1881",
            "1891",
            "1901",
            "1911"});
            this.cbCensusDate.Location = new System.Drawing.Point(91, 93);
            this.cbCensusDate.Name = "cbCensusDate";
            this.cbCensusDate.Size = new System.Drawing.Size(49, 21);
            this.cbCensusDate.TabIndex = 1;
            this.cbCensusDate.SelectedValueChanged += new System.EventHandler(this.cbCensusDate_SelectedValueChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbUSA);
            this.groupBox1.Controls.Add(this.rbCanada);
            this.groupBox1.Controls.Add(this.rbGB);
            this.groupBox1.Controls.Add(this.rbWales);
            this.groupBox1.Controls.Add(this.rbEngland);
            this.groupBox1.Controls.Add(this.rbScotland);
            this.groupBox1.Location = new System.Drawing.Point(13, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(326, 72);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Census Country";
            // 
            // rbUSA
            // 
            this.rbUSA.AutoSize = true;
            this.rbUSA.Location = new System.Drawing.Point(79, 42);
            this.rbUSA.Name = "rbUSA";
            this.rbUSA.Size = new System.Drawing.Size(89, 17);
            this.rbUSA.TabIndex = 5;
            this.rbUSA.Text = "United States";
            this.rbUSA.UseVisualStyleBackColor = true;
            // 
            // rbCanada
            // 
            this.rbCanada.AutoSize = true;
            this.rbCanada.Location = new System.Drawing.Point(6, 42);
            this.rbCanada.Name = "rbCanada";
            this.rbCanada.Size = new System.Drawing.Size(62, 17);
            this.rbCanada.TabIndex = 4;
            this.rbCanada.Text = "Canada";
            this.rbCanada.UseVisualStyleBackColor = true;
            // 
            // rbGB
            // 
            this.rbGB.AutoSize = true;
            this.rbGB.Location = new System.Drawing.Point(210, 19);
            this.rbGB.Name = "rbGB";
            this.rbGB.Size = new System.Drawing.Size(83, 17);
            this.rbGB.TabIndex = 3;
            this.rbGB.Text = "Great Britain";
            this.rbGB.UseVisualStyleBackColor = true;
            // 
            // rbWales
            // 
            this.rbWales.AutoSize = true;
            this.rbWales.Location = new System.Drawing.Point(149, 19);
            this.rbWales.Name = "rbWales";
            this.rbWales.Size = new System.Drawing.Size(55, 17);
            this.rbWales.TabIndex = 2;
            this.rbWales.Text = "Wales";
            this.rbWales.UseVisualStyleBackColor = true;
            // 
            // rbEngland
            // 
            this.rbEngland.AutoSize = true;
            this.rbEngland.Location = new System.Drawing.Point(79, 19);
            this.rbEngland.Name = "rbEngland";
            this.rbEngland.Size = new System.Drawing.Size(64, 17);
            this.rbEngland.TabIndex = 1;
            this.rbEngland.Text = "England";
            this.rbEngland.UseVisualStyleBackColor = true;
            // 
            // rbScotland
            // 
            this.rbScotland.AutoSize = true;
            this.rbScotland.Checked = true;
            this.rbScotland.Location = new System.Drawing.Point(6, 19);
            this.rbScotland.Name = "rbScotland";
            this.rbScotland.Size = new System.Drawing.Size(67, 17);
            this.rbScotland.TabIndex = 0;
            this.rbScotland.TabStop = true;
            this.rbScotland.Text = "Scotland";
            this.rbScotland.UseVisualStyleBackColor = true;
            // 
            // tabLooseDeaths
            // 
            this.tabLooseDeaths.Controls.Add(this.dgLooseDeaths);
            this.tabLooseDeaths.Location = new System.Drawing.Point(4, 22);
            this.tabLooseDeaths.Name = "tabLooseDeaths";
            this.tabLooseDeaths.Padding = new System.Windows.Forms.Padding(3);
            this.tabLooseDeaths.Size = new System.Drawing.Size(1001, 420);
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
            this.dgLooseDeaths.Size = new System.Drawing.Size(1001, 417);
            this.dgLooseDeaths.TabIndex = 0;
            // 
            // tabLocations
            // 
            this.tabLocations.Controls.Add(this.tabCtrlLocations);
            this.tabLocations.Location = new System.Drawing.Point(4, 22);
            this.tabLocations.Name = "tabLocations";
            this.tabLocations.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocations.Size = new System.Drawing.Size(1001, 420);
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
            this.tabCtrlLocations.Size = new System.Drawing.Size(998, 419);
            this.tabCtrlLocations.TabIndex = 0;
            // 
            // tabCountries
            // 
            this.tabCountries.Controls.Add(this.dgCountries);
            this.tabCountries.Location = new System.Drawing.Point(4, 22);
            this.tabCountries.Name = "tabCountries";
            this.tabCountries.Padding = new System.Windows.Forms.Padding(3);
            this.tabCountries.Size = new System.Drawing.Size(990, 393);
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
            this.dgCountries.Size = new System.Drawing.Size(987, 387);
            this.dgCountries.TabIndex = 0;
            this.dgCountries.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgCountries_CellContentDoubleClick);
            // 
            // tabRegions
            // 
            this.tabRegions.Controls.Add(this.dgRegions);
            this.tabRegions.Location = new System.Drawing.Point(4, 22);
            this.tabRegions.Name = "tabRegions";
            this.tabRegions.Padding = new System.Windows.Forms.Padding(3);
            this.tabRegions.Size = new System.Drawing.Size(990, 393);
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
            this.dgRegions.Size = new System.Drawing.Size(987, 387);
            this.dgRegions.TabIndex = 1;
            this.dgRegions.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgRegions_CellContentDoubleClick);
            // 
            // tabParishes
            // 
            this.tabParishes.Controls.Add(this.dgParishes);
            this.tabParishes.Location = new System.Drawing.Point(4, 22);
            this.tabParishes.Name = "tabParishes";
            this.tabParishes.Size = new System.Drawing.Size(990, 393);
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
            this.dgParishes.Size = new System.Drawing.Size(987, 387);
            this.dgParishes.TabIndex = 1;
            this.dgParishes.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgParishes_CellContentDoubleClick);
            // 
            // tabAddresses
            // 
            this.tabAddresses.Controls.Add(this.dgAddresses);
            this.tabAddresses.Location = new System.Drawing.Point(4, 22);
            this.tabAddresses.Name = "tabAddresses";
            this.tabAddresses.Size = new System.Drawing.Size(990, 393);
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
            this.dgAddresses.Size = new System.Drawing.Size(987, 387);
            this.dgAddresses.TabIndex = 1;
            this.dgAddresses.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgAddresses_CellContentDoubleClick);
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
            this.statusStrip.Location = new System.Drawing.Point(0, 476);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1009, 22);
            this.statusStrip.TabIndex = 10;
            this.statusStrip.Text = "statusStrip1";
            // 
            // tsCountLabel
            // 
            this.tsCountLabel.Name = "tsCountLabel";
            this.tsCountLabel.Size = new System.Drawing.Size(52, 17);
            this.tsCountLabel.Text = "Count : 0";
            // 
            // tabLostCousins
            // 
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
            this.tabLostCousins.Size = new System.Drawing.Size(1001, 420);
            this.tabLostCousins.TabIndex = 5;
            this.tabLostCousins.Text = "Lost Cousins";
            this.tabLostCousins.UseVisualStyleBackColor = true;
            // 
            // btnLC1881Scot
            // 
            this.btnLC1881Scot.Location = new System.Drawing.Point(190, 45);
            this.btnLC1881Scot.Name = "btnLC1881Scot";
            this.btnLC1881Scot.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881Scot.TabIndex = 0;
            this.btnLC1881Scot.Text = "1881 Scotland Census";
            this.btnLC1881Scot.UseVisualStyleBackColor = true;
            this.btnLC1881Scot.Click += new System.EventHandler(this.btnLC1881Scot_Click);
            // 
            // btnLC1881Canada
            // 
            this.btnLC1881Canada.Location = new System.Drawing.Point(358, 45);
            this.btnLC1881Canada.Name = "btnLC1881Canada";
            this.btnLC1881Canada.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881Canada.TabIndex = 4;
            this.btnLC1881Canada.Text = "1881 Canada Census";
            this.btnLC1881Canada.UseVisualStyleBackColor = true;
            this.btnLC1881Canada.Click += new System.EventHandler(this.btnLC1881Canada_Click);
            // 
            // btnLC1881EW
            // 
            this.btnLC1881EW.Location = new System.Drawing.Point(22, 45);
            this.btnLC1881EW.Name = "btnLC1881EW";
            this.btnLC1881EW.Size = new System.Drawing.Size(162, 36);
            this.btnLC1881EW.TabIndex = 5;
            this.btnLC1881EW.Text = "1881 England && Wales Census";
            this.btnLC1881EW.UseVisualStyleBackColor = true;
            this.btnLC1881EW.Click += new System.EventHandler(this.btnLC1881EW_Click);
            // 
            // btnLC1841EW
            // 
            this.btnLC1841EW.Location = new System.Drawing.Point(22, 87);
            this.btnLC1841EW.Name = "btnLC1841EW";
            this.btnLC1841EW.Size = new System.Drawing.Size(162, 36);
            this.btnLC1841EW.TabIndex = 8;
            this.btnLC1841EW.Text = "1841 England && Wales Census";
            this.btnLC1841EW.UseVisualStyleBackColor = true;
            this.btnLC1841EW.Click += new System.EventHandler(this.btnLC1841EW_Click);
            // 
            // btnLC1911Ireland
            // 
            this.btnLC1911Ireland.Location = new System.Drawing.Point(358, 87);
            this.btnLC1911Ireland.Name = "btnLC1911Ireland";
            this.btnLC1911Ireland.Size = new System.Drawing.Size(162, 36);
            this.btnLC1911Ireland.TabIndex = 7;
            this.btnLC1911Ireland.Text = "1911 Ireland Census";
            this.btnLC1911Ireland.UseVisualStyleBackColor = true;
            this.btnLC1911Ireland.Click += new System.EventHandler(this.btnLC1911Ireland_Click);
            // 
            // btnLC1880USA
            // 
            this.btnLC1880USA.Location = new System.Drawing.Point(190, 87);
            this.btnLC1880USA.Name = "btnLC1880USA";
            this.btnLC1880USA.Size = new System.Drawing.Size(162, 36);
            this.btnLC1880USA.TabIndex = 6;
            this.btnLC1880USA.Text = "1880 US Census";
            this.btnLC1880USA.UseVisualStyleBackColor = true;
            this.btnLC1880USA.Click += new System.EventHandler(this.btnLC1880USA_Click);
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
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1009, 498);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Family Tree Analyzer";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabDisplayProgress.ResumeLayout(false);
            this.tabDisplayProgress.PerformLayout();
            this.tabIndividuals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            this.tabCensus.ResumeLayout(false);
            this.tabCensus.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabLostCousins.ResumeLayout(false);
            this.tabLostCousins.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
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
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbScotland;
        private System.Windows.Forms.RadioButton rbEngland;
        private System.Windows.Forms.RadioButton rbWales;
        private System.Windows.Forms.RadioButton rbGB;
        private System.Windows.Forms.RadioButton rbUSA;
        private System.Windows.Forms.RadioButton rbCanada;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbCensusDate;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox ckbBlood;
        private System.Windows.Forms.CheckBox ckbDirects;
        private System.Windows.Forms.CheckBox ckbMarriageDB;
        private System.Windows.Forms.CheckBox ckbMarriage;
        private System.Windows.Forms.CheckBox ckbUnknown;
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
    }
}

