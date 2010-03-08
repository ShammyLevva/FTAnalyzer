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
            this.censusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1841Census = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1851Census = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1861Census = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1871Census = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1881Census = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1891Census = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1901Census = new System.Windows.Forms.ToolStripMenuItem();
            this.mnu1911Census = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabDisplayProgress = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.pbFamilies = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.pbIndividuals = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.pbSources = new System.Windows.Forms.ProgressBar();
            this.tabIndividuals = new System.Windows.Forms.TabPage();
            this.dgIndividuals = new System.Windows.Forms.DataGridView();
            this.tabTestFactDate = new System.Windows.Forms.TabPage();
            this.txtEndDate = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtStartDate = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtTestDate = new System.Windows.Forms.TextBox();
            this.btnTestDates = new System.Windows.Forms.Button();
            this.tabLooseDeaths = new System.Windows.Forms.TabPage();
            this.dgLooseDeaths = new System.Windows.Forms.DataGridView();
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabLocations = new System.Windows.Forms.TabPage();
            this.tabCtrlLocations = new System.Windows.Forms.TabControl();
            this.tabCountries = new System.Windows.Forms.TabPage();
            this.tabRegions = new System.Windows.Forms.TabPage();
            this.tabParishes = new System.Windows.Forms.TabPage();
            this.tabAddresses = new System.Windows.Forms.TabPage();
            this.dgCountries = new System.Windows.Forms.DataGridView();
            this.dgRegions = new System.Windows.Forms.DataGridView();
            this.dgParishes = new System.Windows.Forms.DataGridView();
            this.dgAddresses = new System.Windows.Forms.DataGridView();
            this.menuStrip1.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabDisplayProgress.SuspendLayout();
            this.tabIndividuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.tabTestFactDate.SuspendLayout();
            this.tabLooseDeaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).BeginInit();
            this.statusStrip.SuspendLayout();
            this.tabLocations.SuspendLayout();
            this.tabCtrlLocations.SuspendLayout();
            this.tabCountries.SuspendLayout();
            this.tabRegions.SuspendLayout();
            this.tabParishes.SuspendLayout();
            this.tabAddresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCountries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgParishes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAddresses)).BeginInit();
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
            this.censusToolStripMenuItem});
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
            // censusToolStripMenuItem
            // 
            this.censusToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnu1841Census,
            this.mnu1851Census,
            this.mnu1861Census,
            this.mnu1871Census,
            this.mnu1881Census,
            this.mnu1891Census,
            this.mnu1901Census,
            this.mnu1911Census});
            this.censusToolStripMenuItem.Name = "censusToolStripMenuItem";
            this.censusToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
            this.censusToolStripMenuItem.Text = "Census";
            // 
            // mnu1841Census
            // 
            this.mnu1841Census.Name = "mnu1841Census";
            this.mnu1841Census.Size = new System.Drawing.Size(109, 22);
            this.mnu1841Census.Text = "1841";
            // 
            // mnu1851Census
            // 
            this.mnu1851Census.Name = "mnu1851Census";
            this.mnu1851Census.Size = new System.Drawing.Size(109, 22);
            this.mnu1851Census.Text = "1851";
            // 
            // mnu1861Census
            // 
            this.mnu1861Census.Name = "mnu1861Census";
            this.mnu1861Census.Size = new System.Drawing.Size(109, 22);
            this.mnu1861Census.Text = "1861";
            // 
            // mnu1871Census
            // 
            this.mnu1871Census.Name = "mnu1871Census";
            this.mnu1871Census.Size = new System.Drawing.Size(109, 22);
            this.mnu1871Census.Text = "1871";
            // 
            // mnu1881Census
            // 
            this.mnu1881Census.Name = "mnu1881Census";
            this.mnu1881Census.Size = new System.Drawing.Size(109, 22);
            this.mnu1881Census.Text = "1881";
            // 
            // mnu1891Census
            // 
            this.mnu1891Census.Name = "mnu1891Census";
            this.mnu1891Census.Size = new System.Drawing.Size(109, 22);
            this.mnu1891Census.Text = "1891";
            // 
            // mnu1901Census
            // 
            this.mnu1901Census.Name = "mnu1901Census";
            this.mnu1901Census.Size = new System.Drawing.Size(109, 22);
            this.mnu1901Census.Text = "1901";
            // 
            // mnu1911Census
            // 
            this.mnu1911Census.Name = "mnu1911Census";
            this.mnu1911Census.Size = new System.Drawing.Size(109, 22);
            this.mnu1911Census.Text = "1911";
            this.mnu1911Census.Click += new System.EventHandler(this.mnu1911Census_Click);
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabDisplayProgress);
            this.tabControl.Controls.Add(this.tabIndividuals);
            this.tabControl.Controls.Add(this.tabTestFactDate);
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
            // tabTestFactDate
            // 
            this.tabTestFactDate.Controls.Add(this.txtEndDate);
            this.tabTestFactDate.Controls.Add(this.label3);
            this.tabTestFactDate.Controls.Add(this.txtStartDate);
            this.tabTestFactDate.Controls.Add(this.label2);
            this.tabTestFactDate.Controls.Add(this.label1);
            this.tabTestFactDate.Controls.Add(this.txtTestDate);
            this.tabTestFactDate.Controls.Add(this.btnTestDates);
            this.tabTestFactDate.Location = new System.Drawing.Point(4, 22);
            this.tabTestFactDate.Name = "tabTestFactDate";
            this.tabTestFactDate.Padding = new System.Windows.Forms.Padding(3);
            this.tabTestFactDate.Size = new System.Drawing.Size(1001, 420);
            this.tabTestFactDate.TabIndex = 0;
            this.tabTestFactDate.Text = "Test Fact Date";
            this.tabTestFactDate.UseVisualStyleBackColor = true;
            // 
            // txtEndDate
            // 
            this.txtEndDate.Enabled = false;
            this.txtEndDate.Location = new System.Drawing.Point(293, 53);
            this.txtEndDate.Name = "txtEndDate";
            this.txtEndDate.Size = new System.Drawing.Size(81, 20);
            this.txtEndDate.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(205, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "End Date";
            // 
            // txtStartDate
            // 
            this.txtStartDate.Enabled = false;
            this.txtStartDate.Location = new System.Drawing.Point(106, 53);
            this.txtStartDate.Name = "txtStartDate";
            this.txtStartDate.Size = new System.Drawing.Size(81, 20);
            this.txtStartDate.TabIndex = 13;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Start Date";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Enter Test Date";
            // 
            // txtTestDate
            // 
            this.txtTestDate.Location = new System.Drawing.Point(106, 19);
            this.txtTestDate.Name = "txtTestDate";
            this.txtTestDate.Size = new System.Drawing.Size(174, 20);
            this.txtTestDate.TabIndex = 10;
            // 
            // btnTestDates
            // 
            this.btnTestDates.Location = new System.Drawing.Point(289, 14);
            this.btnTestDates.Name = "btnTestDates";
            this.btnTestDates.Size = new System.Drawing.Size(85, 28);
            this.btnTestDates.TabIndex = 9;
            this.btnTestDates.Text = "Test FactDate";
            this.btnTestDates.UseVisualStyleBackColor = true;
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
            this.tabTestFactDate.ResumeLayout(false);
            this.tabTestFactDate.PerformLayout();
            this.tabLooseDeaths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).EndInit();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.tabLocations.ResumeLayout(false);
            this.tabCtrlLocations.ResumeLayout(false);
            this.tabCountries.ResumeLayout(false);
            this.tabRegions.ResumeLayout(false);
            this.tabParishes.ResumeLayout(false);
            this.tabAddresses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCountries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgParishes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgAddresses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabTestFactDate;
        private System.Windows.Forms.TextBox txtEndDate;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtStartDate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtTestDate;
        private System.Windows.Forms.Button btnTestDates;
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
        private System.Windows.Forms.ToolStripMenuItem censusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnu1841Census;
        private System.Windows.Forms.ToolStripMenuItem mnu1851Census;
        private System.Windows.Forms.ToolStripMenuItem mnu1861Census;
        private System.Windows.Forms.ToolStripMenuItem mnu1871Census;
        private System.Windows.Forms.ToolStripMenuItem mnu1881Census;
        private System.Windows.Forms.ToolStripMenuItem mnu1891Census;
        private System.Windows.Forms.ToolStripMenuItem mnu1901Census;
        private System.Windows.Forms.ToolStripMenuItem mnu1911Census;
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
    }
}

