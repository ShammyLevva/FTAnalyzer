using System;

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
            try
            {
                if (disposing && (components is not null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
                normalFont?.Dispose();
                boldFont?.Dispose();
                handwritingFont?.Dispose();
                fonts?.Dispose();
                rfhDuplicates?.Dispose();
                cts?.Dispose();
                storedCursor?.Dispose();
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            openGedcom = new OpenFileDialog();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            mnuReload = new ToolStripMenuItem();
            mnuPrint = new ToolStripMenuItem();
            toolStripSeparator6 = new ToolStripSeparator();
            mnuRecent = new ToolStripMenuItem();
            mnuRecent1 = new ToolStripMenuItem();
            mnuRecent2 = new ToolStripMenuItem();
            mnuRecent3 = new ToolStripMenuItem();
            mnuRecent4 = new ToolStripMenuItem();
            mnuRecent5 = new ToolStripMenuItem();
            toolStripSeparator7 = new ToolStripSeparator();
            clearRecentFileListToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            databaseToolStripMenuItem = new ToolStripMenuItem();
            backupToolStripMenuItem = new ToolStripMenuItem();
            mnuRestore = new ToolStripMenuItem();
            toolStripSeparator11 = new ToolStripSeparator();
            mnuLoadLocationsCSV = new ToolStripMenuItem();
            mnuLoadLocationsTNG = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            mnuCloseGEDCOM = new ToolStripMenuItem();
            exitToolStripMenuItem = new ToolStripMenuItem();
            mnuReports = new ToolStripMenuItem();
            mnuChildAgeProfiles = new ToolStripMenuItem();
            mnuOlderParents = new ToolStripMenuItem();
            mnuPossibleCensusFacts = new ToolStripMenuItem();
            mnuCousinsCountReport = new ToolStripMenuItem();
            mnuHowManyGreats = new ToolStripMenuItem();
            mnuBirthdayEffect = new ToolStripMenuItem();
            mnuPossiblyMissingChildReport = new ToolStripMenuItem();
            MnuAgedOver99Report = new ToolStripMenuItem();
            MnuSingleParentsReport = new ToolStripMenuItem();
            mnuExport = new ToolStripMenuItem();
            mnuIndividualsToExcel = new ToolStripMenuItem();
            mnuFamiliesToExcel = new ToolStripMenuItem();
            mnuFactsToExcel = new ToolStripMenuItem();
            MnuExportLocations = new ToolStripMenuItem();
            mnuSourcesToExcel = new ToolStripMenuItem();
            MnuCustomFactsToExcel = new ToolStripMenuItem();
            toolStripSeparator12 = new ToolStripSeparator();
            mnuDataErrorsToExcel = new ToolStripMenuItem();
            mnuSurnamesToExcel = new ToolStripMenuItem();
            toolStripSeparator8 = new ToolStripSeparator();
            mnuLooseBirthsToExcel = new ToolStripMenuItem();
            mnuLooseDeathsToExcel = new ToolStripMenuItem();
            toolStripSeparator9 = new ToolStripSeparator();
            mnuTreetopsToExcel = new ToolStripMenuItem();
            mnuWorldWarsToExcel = new ToolStripMenuItem();
            toolStripSeparator13 = new ToolStripSeparator();
            mnuDNA_GEDCOM = new ToolStripMenuItem();
            toolStripSeparator15 = new ToolStripSeparator();
            mnuJSON = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            optionsToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            displayOptionsOnLoadToolStripMenuItem = new ToolStripMenuItem();
            resetToDefaultFormSizeToolStripMenuItem = new ToolStripMenuItem();
            mnuMaps = new ToolStripMenuItem();
            mnuShowTimeline = new ToolStripMenuItem();
            mnuLifelines = new ToolStripMenuItem();
            mnuPlaces = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            mnuLocationsGeocodeReport = new ToolStripMenuItem();
            toolStripSeparator10 = new ToolStripSeparator();
            mnuGeocodeLocations = new ToolStripMenuItem();
            mnuOSGeocoder = new ToolStripMenuItem();
            mnuLookupBlankFoundLocations = new ToolStripMenuItem();
            toolStripSeparator16 = new ToolStripSeparator();
            mnuGoogleMyMaps = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            viewOnlineManualToolStripMenuItem = new ToolStripMenuItem();
            onlineGuidesToUsingFTAnalyzerToolStripMenuItem = new ToolStripMenuItem();
            reportAnIssueToolStripMenuItem = new ToolStripMenuItem();
            facebookSupportGroupToolStripMenuItem = new ToolStripMenuItem();
            facebookUserGroupToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            getGoogleAPIKeyToolStripMenuItem = new ToolStripMenuItem();
            googleAPISetupGuideToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator14 = new ToolStripSeparator();
            privacyPolicyToolStripMenuItem = new ToolStripMenuItem();
            whatsNewToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            mnuSetRoot = new ContextMenuStrip(components);
            setAsRootToolStripMenuItem = new ToolStripMenuItem();
            viewNotesToolStripMenuItem = new ToolStripMenuItem();
            tsCount = new ToolStripStatusLabel();
            statusStrip = new StatusStrip();
            tsCountLabel = new ToolStripStatusLabel();
            tsHintsLabel = new ToolStripStatusLabel();
            tspbTabProgress = new ToolStripProgressBar();
            tsStatusLabel = new ToolStripStatusLabel();
            toolTips = new ToolTip(components);
            cmbColourFamily = new ComboBox();
            btnRandomSurnameColour = new Button();
            tbDuplicateScore = new TrackBar();
            chkLCRootPersonConfirm = new CheckBox();
            dgCheckAncestors = new DataGridView();
            labErrorTabAggrMatch = new Label();
            labErrorTabLooseMatch = new Label();
            chkIgnoreUnnamedTwins = new CheckBox();
            radioOnlyPreferred = new RadioButton();
            radioOnlyAlternate = new RadioButton();
            dgDataErrors = new Forms.Controls.VirtualDGVDataErrors();
            dgCountries = new Forms.Controls.VirtualDGVLocations();
            dgRegions = new Forms.Controls.VirtualDGVLocations();
            btnShowExclusions = new Button();
            ckbFactExclude = new CheckedListBox();
            btnUpdateLostCousinsWebsite = new Button();
            printPreviewDialog = new PrintPreviewDialog();
            printDialog = new PrintDialog();
            printDocument = new System.Drawing.Printing.PrintDocument();
            tabWorldWars = new TabPage();
            ckbMilitaryOnly = new CheckBox();
            ckbWDIgnoreLocations = new CheckBox();
            btnWWII = new Button();
            btnWWI = new Button();
            label9 = new Label();
            txtWorldWarsSurname = new TextBox();
            dgWorldWars = new Forms.Controls.VirtualDGVIndividuals();
            wardeadRelation = new Forms.Controls.RelationTypes();
            wardeadCountry = new Forms.Controls.CensusCountry();
            ctxViewNotes = new ContextMenuStrip(components);
            mnuViewNotes = new ToolStripMenuItem();
            tabTreetops = new TabPage();
            ckbTTIncludeOnlyOneParent = new CheckBox();
            ckbTTIgnoreLocations = new CheckBox();
            btnTreeTops = new Button();
            label8 = new Label();
            txtTreetopsSurname = new TextBox();
            dgTreeTops = new Forms.Controls.VirtualDGVIndividuals();
            treetopsRelation = new Forms.Controls.RelationTypes();
            treetopsCountry = new Forms.Controls.CensusCountry();
            tabColourReports = new TabPage();
            groupBox7 = new GroupBox();
            btnAdvancedMissingData = new Button();
            btnStandardMissingData = new Button();
            labResearchTabFamilyFilter = new Label();
            groupBox3 = new GroupBox();
            ckbIgnoreNoDeathDate = new CheckBox();
            ckbIgnoreNoBirthDate = new CheckBox();
            btnIrishColourCensus = new Button();
            btnCanadianColourCensus = new Button();
            btnUKColourCensus = new Button();
            btnUSColourCensus = new Button();
            btnColourBMD = new Button();
            labResearchTabSurname = new Label();
            txtColouredSurname = new TextBox();
            relTypesColoured = new Forms.Controls.RelationTypes();
            tabLostCousins = new TabPage();
            LCSubTabs = new TabControl();
            LCReportsTab = new TabPage();
            Referrals = new GroupBox();
            ckbReferralInCommon = new CheckBox();
            btnReferrals = new Button();
            cmbReferrals = new ComboBox();
            labLCSelectInd = new Label();
            btnLCnoCensus = new Button();
            btnLCDuplicates = new Button();
            btnLCMissingCountry = new Button();
            btnLC1940USA = new Button();
            rtbLostCousins = new RichTextBox();
            linkLabel2 = new LinkLabel();
            btnLC1911EW = new Button();
            LabLostCousinsWeb = new LinkLabel();
            ckbShowLCEntered = new CheckBox();
            btnLC1841EW = new Button();
            btnLC1911Ireland = new Button();
            btnLC1880USA = new Button();
            btnLC1881EW = new Button();
            btnLC1881Canada = new Button();
            btnLC1881Scot = new Button();
            relTypesLC = new Forms.Controls.RelationTypes();
            LCUpdatesTab = new TabPage();
            rtbLCoutput = new Utilities.ScrollingRichTextBox();
            btnViewInvalidRefs = new Button();
            btnLCPotentialUploads = new Button();
            label21 = new Label();
            rtbLCUpdateData = new RichTextBox();
            groupBox8 = new GroupBox();
            btnLCLogin = new Button();
            label20 = new Label();
            labLCUpdatesEmail = new Label();
            txtLCEmail = new TextBox();
            txtLCPassword = new MaskedTextBox();
            LCVerifyTab = new TabPage();
            rtbCheckAncestors = new Utilities.ScrollingRichTextBox();
            btnCheckMyAncestors = new Button();
            lblCheckAncestors = new Label();
            tabCensus = new TabPage();
            groupBox2 = new GroupBox();
            btnAliveOnDate = new Button();
            txtAliveDates = new TextBox();
            label22 = new Label();
            chkAnyCensusYear = new CheckBox();
            groupBox10 = new GroupBox();
            btnShowCensusMissing = new Button();
            btnShowCensusEntered = new Button();
            btnRandomSurnameEntered = new Button();
            btnRandomSurnameMissing = new Button();
            groupBox4 = new GroupBox();
            btnInconsistentLocations = new Button();
            btnUnrecognisedCensusRef = new Button();
            btnIncompleteCensusRef = new Button();
            btnMissingCensusRefs = new Button();
            btnCensusRefs = new Button();
            chkExcludeUnknownBirths = new CheckBox();
            labCensusTabSurname = new Label();
            txtCensusSurname = new TextBox();
            labCensusExcludeOverAge = new Label();
            udAgeFilter = new NumericUpDown();
            cenDate = new Forms.Controls.CensusDateSelector();
            relTypesCensus = new Forms.Controls.RelationTypes();
            groupBox9 = new GroupBox();
            groupBox11 = new GroupBox();
            BtnAutoCreatedCensusFacts = new Button();
            BtnProblemCensusFacts = new Button();
            groupBox1 = new GroupBox();
            btnDuplicateCensus = new Button();
            btnMissingCensusLocation = new Button();
            groupBox5 = new GroupBox();
            btnMismatchedChildrenStatus = new Button();
            btnNoChildrenStatus = new Button();
            groupBox6 = new GroupBox();
            btnReportUnrecognised = new Button();
            tabLocations = new TabPage();
            btnOldOSMap = new Button();
            btnModernOSMap = new Button();
            btnShowMap = new Button();
            tabCtrlLocations = new TabControl();
            tabTreeView = new TabPage();
            treeViewLocations = new TreeView();
            imageList = new ImageList(components);
            tabCountries = new TabPage();
            tabRegions = new TabPage();
            tabSubRegions = new TabPage();
            dgSubRegions = new Forms.Controls.VirtualDGVLocations();
            tabAddresses = new TabPage();
            dgAddresses = new Forms.Controls.VirtualDGVLocations();
            tabPlaces = new TabPage();
            dgPlaces = new Forms.Controls.VirtualDGVLocations();
            tabDisplayProgress = new TabPage();
            splitGedcom = new SplitContainer();
            panel2 = new Panel();
            LbProgramName = new Label();
            pictureBox1 = new PictureBox();
            labRelationships = new Label();
            pbRelationships = new ProgressBar();
            labFamilies = new Label();
            pbFamilies = new ProgressBar();
            labIndividuals = new Label();
            pbIndividuals = new ProgressBar();
            labSources = new Label();
            pbSources = new ProgressBar();
            rtbOutput = new Utilities.ScrollingRichTextBox();
            tabSelector = new TabControl();
            tabMainLists = new TabPage();
            tabMainListsSelector = new TabControl();
            tabIndividuals = new TabPage();
            dgIndividuals = new Forms.Controls.VirtualDGVIndividuals();
            tabFamilies = new TabPage();
            dgFamilies = new Forms.Controls.VirtualDGVFamily();
            tabSources = new TabPage();
            dgSources = new Forms.Controls.VirtualDGVSources();
            tabOccupations = new TabPage();
            dgOccupations = new Forms.Controls.VirtualDGVOccupations();
            tabCustomFacts = new TabPage();
            dgCustomFacts = new Forms.Controls.VirtualDGVCustomFacts();
            tabErrorsFixes = new TabPage();
            tabErrorFixSelector = new TabControl();
            tabDataErrors = new TabPage();
            gbDataErrorTypes = new GroupBox();
            ckbDataErrors = new CheckedListBox();
            btnSelectAll = new Button();
            btnClearAll = new Button();
            tabDuplicates = new TabPage();
            labDuplicateSlider = new Label();
            labCompletion = new Label();
            ckbHideIgnoredDuplicates = new CheckBox();
            labErrorTabCandidateDupTitle = new Label();
            labCalcDuplicates = new Label();
            pbDuplicates = new ProgressBar();
            btnCancelDuplicates = new Button();
            dgDuplicates = new Forms.Controls.VirtualDGVDuplicates();
            tabLooseBirths = new TabPage();
            dgLooseBirths = new Forms.Controls.VirtualDGVLooseBirths();
            tabLooseDeaths = new TabPage();
            dgLooseDeaths = new Forms.Controls.VirtualDGVLooseDeaths();
            tabLooseInfo = new TabPage();
            dgLooseInfo = new Forms.Controls.VirtualDGVLooseInfo();
            tabSurnames = new TabPage();
            chkSurnamesIgnoreCase = new CheckBox();
            btnShowSurnames = new Button();
            dgSurnames = new Forms.Controls.VirtualDGVSurnames();
            Surname = new DataGridViewTextBoxColumn();
            URI = new DataGridViewLinkColumn();
            Individuals = new DataGridViewTextBoxColumn();
            Families = new DataGridViewTextBoxColumn();
            Marriages = new DataGridViewTextBoxColumn();
            reltypesSurnames = new Forms.Controls.RelationTypes();
            tabFacts = new TabPage();
            btnDuplicateFacts = new Button();
            panel1 = new Panel();
            radioAllFacts = new RadioButton();
            btnDeselectExcludeAllFactTypes = new Button();
            lblExclude = new Label();
            btnExcludeAllFactTypes = new Button();
            btnDeselectAllFactTypes = new Button();
            btnShowFacts = new Button();
            btnSelectAllFactTypes = new Button();
            label3 = new Label();
            ckbFactSelect = new CheckedListBox();
            txtFactsSurname = new TextBox();
            relTypesFacts = new Forms.Controls.RelationTypes();
            tabToday = new TabPage();
            rtbToday = new Utilities.ScrollingRichTextBox();
            labTodayYearStep = new Label();
            nudToday = new NumericUpDown();
            btnUpdateTodaysEvents = new Button();
            labTodayLoadWorldEvents = new Label();
            pbToday = new ProgressBar();
            rbTodayMonth = new RadioButton();
            rbTodaySingle = new RadioButton();
            labTodaySelectDate = new Label();
            dpToday = new DateTimePicker();
            NonDuplicate = new DataGridViewCheckBoxColumn();
            Score = new DataGridViewTextBoxColumn();
            DuplicateIndividualID = new DataGridViewTextBoxColumn();
            DuplicateName = new DataGridViewTextBoxColumn();
            DuplicateForenames = new DataGridViewTextBoxColumn();
            DuplicateSurname = new DataGridViewTextBoxColumn();
            DuplicateBirthDate = new DataGridViewTextBoxColumn();
            DuplicateBirthLocation = new DataGridViewTextBoxColumn();
            MatchIndividualID = new DataGridViewTextBoxColumn();
            MatchName = new DataGridViewTextBoxColumn();
            MatchBirthDate = new DataGridViewTextBoxColumn();
            MatchBirthLocation = new DataGridViewTextBoxColumn();
            saveDatabase = new SaveFileDialog();
            restoreDatabase = new OpenFileDialog();
            imageList1 = new ImageList(components);
            menuStrip1.SuspendLayout();
            mnuSetRoot.SuspendLayout();
            statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)tbDuplicateScore).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgCheckAncestors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgDataErrors).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgCountries).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgRegions).BeginInit();
            tabWorldWars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgWorldWars).BeginInit();
            ctxViewNotes.SuspendLayout();
            tabTreetops.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgTreeTops).BeginInit();
            tabColourReports.SuspendLayout();
            groupBox7.SuspendLayout();
            groupBox3.SuspendLayout();
            tabLostCousins.SuspendLayout();
            LCSubTabs.SuspendLayout();
            LCReportsTab.SuspendLayout();
            Referrals.SuspendLayout();
            LCUpdatesTab.SuspendLayout();
            groupBox8.SuspendLayout();
            LCVerifyTab.SuspendLayout();
            tabCensus.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox10.SuspendLayout();
            groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)udAgeFilter).BeginInit();
            groupBox9.SuspendLayout();
            groupBox11.SuspendLayout();
            groupBox1.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            tabLocations.SuspendLayout();
            tabCtrlLocations.SuspendLayout();
            tabTreeView.SuspendLayout();
            tabCountries.SuspendLayout();
            tabRegions.SuspendLayout();
            tabSubRegions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgSubRegions).BeginInit();
            tabAddresses.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgAddresses).BeginInit();
            tabPlaces.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgPlaces).BeginInit();
            tabDisplayProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitGedcom).BeginInit();
            splitGedcom.Panel1.SuspendLayout();
            splitGedcom.Panel2.SuspendLayout();
            splitGedcom.SuspendLayout();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            tabSelector.SuspendLayout();
            tabMainLists.SuspendLayout();
            tabMainListsSelector.SuspendLayout();
            tabIndividuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgIndividuals).BeginInit();
            tabFamilies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgFamilies).BeginInit();
            tabSources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgSources).BeginInit();
            tabOccupations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgOccupations).BeginInit();
            tabCustomFacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgCustomFacts).BeginInit();
            tabErrorsFixes.SuspendLayout();
            tabErrorFixSelector.SuspendLayout();
            tabDataErrors.SuspendLayout();
            gbDataErrorTypes.SuspendLayout();
            tabDuplicates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgDuplicates).BeginInit();
            tabLooseBirths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgLooseBirths).BeginInit();
            tabLooseDeaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgLooseDeaths).BeginInit();
            tabLooseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgLooseInfo).BeginInit();
            tabSurnames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgSurnames).BeginInit();
            tabFacts.SuspendLayout();
            panel1.SuspendLayout();
            tabToday.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudToday).BeginInit();
            SuspendLayout();
            // 
            // openGedcom
            // 
            openGedcom.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(32, 32);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, mnuReports, mnuExport, toolsToolStripMenuItem, mnuMaps, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new Padding(4, 1, 0, 1);
            menuStrip1.Size = new Size(1246, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, mnuReload, mnuPrint, toolStripSeparator6, mnuRecent, toolStripSeparator3, databaseToolStripMenuItem, toolStripSeparator5, mnuCloseGEDCOM, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 22);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(184, 22);
            openToolStripMenuItem.Text = "Open GEDCOM file...";
            openToolStripMenuItem.Click += OpenToolStripMenuItem_Click;
            // 
            // mnuReload
            // 
            mnuReload.Enabled = false;
            mnuReload.Name = "mnuReload";
            mnuReload.Size = new Size(184, 22);
            mnuReload.Text = "Reload";
            mnuReload.Click += ReloadToolStripMenuItem_Click;
            // 
            // mnuPrint
            // 
            mnuPrint.Enabled = false;
            mnuPrint.Name = "mnuPrint";
            mnuPrint.Size = new Size(184, 22);
            mnuPrint.Text = "Print";
            mnuPrint.Click += MnuPrint_Click;
            // 
            // toolStripSeparator6
            // 
            toolStripSeparator6.Name = "toolStripSeparator6";
            toolStripSeparator6.Size = new Size(181, 6);
            // 
            // mnuRecent
            // 
            mnuRecent.DropDownItems.AddRange(new ToolStripItem[] { mnuRecent1, mnuRecent2, mnuRecent3, mnuRecent4, mnuRecent5, toolStripSeparator7, clearRecentFileListToolStripMenuItem });
            mnuRecent.Name = "mnuRecent";
            mnuRecent.Size = new Size(184, 22);
            mnuRecent.Text = "Recent Files";
            mnuRecent.DropDownOpening += MnuRecent_DropDownOpening;
            // 
            // mnuRecent1
            // 
            mnuRecent1.Name = "mnuRecent1";
            mnuRecent1.Size = new Size(182, 22);
            mnuRecent1.Text = "1.";
            mnuRecent1.Click += OpenRecentFile_Click;
            // 
            // mnuRecent2
            // 
            mnuRecent2.Name = "mnuRecent2";
            mnuRecent2.Size = new Size(182, 22);
            mnuRecent2.Text = "2.";
            mnuRecent2.Click += OpenRecentFile_Click;
            // 
            // mnuRecent3
            // 
            mnuRecent3.Name = "mnuRecent3";
            mnuRecent3.Size = new Size(182, 22);
            mnuRecent3.Text = "3.";
            mnuRecent3.Click += OpenRecentFile_Click;
            // 
            // mnuRecent4
            // 
            mnuRecent4.Name = "mnuRecent4";
            mnuRecent4.Size = new Size(182, 22);
            mnuRecent4.Text = "4.";
            mnuRecent4.Click += OpenRecentFile_Click;
            // 
            // mnuRecent5
            // 
            mnuRecent5.Name = "mnuRecent5";
            mnuRecent5.Size = new Size(182, 22);
            mnuRecent5.Text = "5.";
            mnuRecent5.Click += OpenRecentFile_Click;
            // 
            // toolStripSeparator7
            // 
            toolStripSeparator7.Name = "toolStripSeparator7";
            toolStripSeparator7.Size = new Size(179, 6);
            // 
            // clearRecentFileListToolStripMenuItem
            // 
            clearRecentFileListToolStripMenuItem.Image = (Image)resources.GetObject("clearRecentFileListToolStripMenuItem.Image");
            clearRecentFileListToolStripMenuItem.Name = "clearRecentFileListToolStripMenuItem";
            clearRecentFileListToolStripMenuItem.Size = new Size(182, 22);
            clearRecentFileListToolStripMenuItem.Text = "Clear Recent File List";
            clearRecentFileListToolStripMenuItem.Click += ClearRecentFileListToolStripMenuItem_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(181, 6);
            // 
            // databaseToolStripMenuItem
            // 
            databaseToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { backupToolStripMenuItem, mnuRestore, toolStripSeparator11, mnuLoadLocationsCSV, mnuLoadLocationsTNG });
            databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            databaseToolStripMenuItem.Size = new Size(184, 22);
            databaseToolStripMenuItem.Text = "Geocode Database";
            // 
            // backupToolStripMenuItem
            // 
            backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            backupToolStripMenuItem.Size = new Size(237, 22);
            backupToolStripMenuItem.Text = "Backup";
            backupToolStripMenuItem.Click += BackupToolStripMenuItem_Click;
            // 
            // mnuRestore
            // 
            mnuRestore.Name = "mnuRestore";
            mnuRestore.Size = new Size(237, 22);
            mnuRestore.Text = "Restore";
            mnuRestore.ToolTipText = "Restore is only available prior to loading GEDCOM";
            mnuRestore.Click += RestoreToolStripMenuItem_Click;
            // 
            // toolStripSeparator11
            // 
            toolStripSeparator11.Name = "toolStripSeparator11";
            toolStripSeparator11.Size = new Size(234, 6);
            // 
            // mnuLoadLocationsCSV
            // 
            mnuLoadLocationsCSV.Name = "mnuLoadLocationsCSV";
            mnuLoadLocationsCSV.Size = new Size(237, 22);
            mnuLoadLocationsCSV.Text = "Load Geocoded Locations CSV";
            mnuLoadLocationsCSV.Click += MnuLoadLocationsCSV_Click;
            // 
            // mnuLoadLocationsTNG
            // 
            mnuLoadLocationsTNG.Name = "mnuLoadLocationsTNG";
            mnuLoadLocationsTNG.Size = new Size(237, 22);
            mnuLoadLocationsTNG.Text = "Load Geocoded Locations TNG";
            mnuLoadLocationsTNG.Click += MnuLoadLocationsTNG_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(181, 6);
            // 
            // mnuCloseGEDCOM
            // 
            mnuCloseGEDCOM.Name = "mnuCloseGEDCOM";
            mnuCloseGEDCOM.Size = new Size(184, 22);
            mnuCloseGEDCOM.Text = "Close GEDCOM file";
            mnuCloseGEDCOM.Click += MnuCloseGEDCOM_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(184, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += ExitToolStripMenuItem_Click;
            // 
            // mnuReports
            // 
            mnuReports.DropDownItems.AddRange(new ToolStripItem[] { mnuChildAgeProfiles, mnuOlderParents, mnuPossibleCensusFacts, mnuCousinsCountReport, mnuHowManyGreats, mnuBirthdayEffect, mnuPossiblyMissingChildReport, MnuAgedOver99Report, MnuSingleParentsReport });
            mnuReports.Name = "mnuReports";
            mnuReports.Size = new Size(59, 22);
            mnuReports.Text = "Reports";
            // 
            // mnuChildAgeProfiles
            // 
            mnuChildAgeProfiles.Name = "mnuChildAgeProfiles";
            mnuChildAgeProfiles.Size = new Size(230, 22);
            mnuChildAgeProfiles.Text = "Parent Age Report";
            mnuChildAgeProfiles.Click += ChildAgeProfilesToolStripMenuItem_Click;
            // 
            // mnuOlderParents
            // 
            mnuOlderParents.Name = "mnuOlderParents";
            mnuOlderParents.Size = new Size(230, 22);
            mnuOlderParents.Text = "Older Parents";
            mnuOlderParents.Click += OlderParentsToolStripMenuItem_Click;
            // 
            // mnuPossibleCensusFacts
            // 
            mnuPossibleCensusFacts.Name = "mnuPossibleCensusFacts";
            mnuPossibleCensusFacts.Size = new Size(230, 22);
            mnuPossibleCensusFacts.Text = "Possible Census Facts";
            mnuPossibleCensusFacts.ToolTipText = "This report aims to find census facts that have been incorrectly recorded as notes";
            mnuPossibleCensusFacts.Click += PossibleCensusFactsToolStripMenuItem_Click;
            // 
            // mnuCousinsCountReport
            // 
            mnuCousinsCountReport.Name = "mnuCousinsCountReport";
            mnuCousinsCountReport.Size = new Size(230, 22);
            mnuCousinsCountReport.Text = "Cousins Count Report";
            mnuCousinsCountReport.Click += CousinsCountReportToolStripMenuItem_Click;
            // 
            // mnuHowManyGreats
            // 
            mnuHowManyGreats.Name = "mnuHowManyGreats";
            mnuHowManyGreats.Size = new Size(230, 22);
            mnuHowManyGreats.Text = "How Many Directs Report";
            mnuHowManyGreats.Click += HowManyDirectsReportToolStripMenuItem_Click;
            // 
            // mnuBirthdayEffect
            // 
            mnuBirthdayEffect.Name = "mnuBirthdayEffect";
            mnuBirthdayEffect.Size = new Size(230, 22);
            mnuBirthdayEffect.Text = "Birthday Effect Report";
            mnuBirthdayEffect.Click += BirthdayEffectReportToolStripMenuItem_Click;
            // 
            // mnuPossiblyMissingChildReport
            // 
            mnuPossiblyMissingChildReport.Name = "mnuPossiblyMissingChildReport";
            mnuPossiblyMissingChildReport.Size = new Size(230, 22);
            mnuPossiblyMissingChildReport.Text = "Possibly Missing Child Report";
            mnuPossiblyMissingChildReport.Click += PossiblyMissingChildReportToolStripMenuItem_Click;
            // 
            // MnuAgedOver99Report
            // 
            MnuAgedOver99Report.Name = "MnuAgedOver99Report";
            MnuAgedOver99Report.Size = new Size(230, 22);
            MnuAgedOver99Report.Text = "Aged over 99 Report";
            MnuAgedOver99Report.Click += MnuAgedOver99Report_Click;
            // 
            // MnuSingleParentsReport
            // 
            MnuSingleParentsReport.Name = "MnuSingleParentsReport";
            MnuSingleParentsReport.Size = new Size(230, 22);
            MnuSingleParentsReport.Text = "Single Parents Report";
            MnuSingleParentsReport.Click += MnuSingleParentsReport_Click;
            // 
            // mnuExport
            // 
            mnuExport.DropDownItems.AddRange(new ToolStripItem[] { mnuIndividualsToExcel, mnuFamiliesToExcel, mnuFactsToExcel, MnuExportLocations, mnuSourcesToExcel, MnuCustomFactsToExcel, toolStripSeparator12, mnuDataErrorsToExcel, mnuSurnamesToExcel, toolStripSeparator8, mnuLooseBirthsToExcel, mnuLooseDeathsToExcel, toolStripSeparator9, mnuTreetopsToExcel, mnuWorldWarsToExcel, toolStripSeparator13, mnuDNA_GEDCOM, toolStripSeparator15, mnuJSON });
            mnuExport.Name = "mnuExport";
            mnuExport.Size = new Size(53, 22);
            mnuExport.Text = "Export";
            // 
            // mnuIndividualsToExcel
            // 
            mnuIndividualsToExcel.Name = "mnuIndividualsToExcel";
            mnuIndividualsToExcel.Size = new Size(222, 22);
            mnuIndividualsToExcel.Text = "Individuals to Excel";
            mnuIndividualsToExcel.Click += IndividualsToExcelToolStripMenuItem_Click;
            // 
            // mnuFamiliesToExcel
            // 
            mnuFamiliesToExcel.Name = "mnuFamiliesToExcel";
            mnuFamiliesToExcel.Size = new Size(222, 22);
            mnuFamiliesToExcel.Text = "Families to Excel";
            mnuFamiliesToExcel.Click += FamiliesToExcelToolStripMenuItem_Click;
            // 
            // mnuFactsToExcel
            // 
            mnuFactsToExcel.Name = "mnuFactsToExcel";
            mnuFactsToExcel.Size = new Size(222, 22);
            mnuFactsToExcel.Text = "Facts to Excel";
            mnuFactsToExcel.Click += FactsToExcelToolStripMenuItem_Click;
            // 
            // MnuExportLocations
            // 
            MnuExportLocations.Name = "MnuExportLocations";
            MnuExportLocations.Size = new Size(222, 22);
            MnuExportLocations.Text = "Locations to Excel";
            MnuExportLocations.Click += MnuExportLocations_Click;
            // 
            // mnuSourcesToExcel
            // 
            mnuSourcesToExcel.Name = "mnuSourcesToExcel";
            mnuSourcesToExcel.Size = new Size(222, 22);
            mnuSourcesToExcel.Text = "Sources to Excel";
            mnuSourcesToExcel.Click += MnuSourcesToExcel_Click;
            // 
            // MnuCustomFactsToExcel
            // 
            MnuCustomFactsToExcel.Name = "MnuCustomFactsToExcel";
            MnuCustomFactsToExcel.Size = new Size(222, 22);
            MnuCustomFactsToExcel.Text = "Custom Facts to Excel";
            MnuCustomFactsToExcel.Click += MnuCustomFactsToExcel_Click;
            // 
            // toolStripSeparator12
            // 
            toolStripSeparator12.Name = "toolStripSeparator12";
            toolStripSeparator12.Size = new Size(219, 6);
            // 
            // mnuDataErrorsToExcel
            // 
            mnuDataErrorsToExcel.Name = "mnuDataErrorsToExcel";
            mnuDataErrorsToExcel.Size = new Size(222, 22);
            mnuDataErrorsToExcel.Text = "Data Errors to Excel";
            mnuDataErrorsToExcel.Click += MnuDataErrorsToExcel_Click;
            // 
            // mnuSurnamesToExcel
            // 
            mnuSurnamesToExcel.Name = "mnuSurnamesToExcel";
            mnuSurnamesToExcel.Size = new Size(222, 22);
            mnuSurnamesToExcel.Text = "Surnames to Excel";
            mnuSurnamesToExcel.Click += MnuSurnamesToExcel_Click;
            // 
            // toolStripSeparator8
            // 
            toolStripSeparator8.Name = "toolStripSeparator8";
            toolStripSeparator8.Size = new Size(219, 6);
            // 
            // mnuLooseBirthsToExcel
            // 
            mnuLooseBirthsToExcel.Name = "mnuLooseBirthsToExcel";
            mnuLooseBirthsToExcel.Size = new Size(222, 22);
            mnuLooseBirthsToExcel.Text = "Loose Births to Excel";
            mnuLooseBirthsToExcel.Click += LooseBirthsToExcelToolStripMenuItem_Click;
            // 
            // mnuLooseDeathsToExcel
            // 
            mnuLooseDeathsToExcel.Name = "mnuLooseDeathsToExcel";
            mnuLooseDeathsToExcel.Size = new Size(222, 22);
            mnuLooseDeathsToExcel.Text = "Loose Deaths to Excel";
            mnuLooseDeathsToExcel.Click += LooseDeathsToExcelToolStripMenuItem_Click;
            // 
            // toolStripSeparator9
            // 
            toolStripSeparator9.Name = "toolStripSeparator9";
            toolStripSeparator9.Size = new Size(219, 6);
            // 
            // mnuTreetopsToExcel
            // 
            mnuTreetopsToExcel.Name = "mnuTreetopsToExcel";
            mnuTreetopsToExcel.Size = new Size(222, 22);
            mnuTreetopsToExcel.Text = "Current Treetops to Excel";
            mnuTreetopsToExcel.Click += MnuTreetopsToExcel_Click;
            // 
            // mnuWorldWarsToExcel
            // 
            mnuWorldWarsToExcel.Name = "mnuWorldWarsToExcel";
            mnuWorldWarsToExcel.Size = new Size(222, 22);
            mnuWorldWarsToExcel.Text = "Current World Wars to Excel";
            mnuWorldWarsToExcel.Click += MnuWorldWarsToExcel_Click;
            // 
            // toolStripSeparator13
            // 
            toolStripSeparator13.Name = "toolStripSeparator13";
            toolStripSeparator13.Size = new Size(219, 6);
            // 
            // mnuDNA_GEDCOM
            // 
            mnuDNA_GEDCOM.Name = "mnuDNA_GEDCOM";
            mnuDNA_GEDCOM.Size = new Size(222, 22);
            mnuDNA_GEDCOM.Text = "Minimalist DNA GEDCOM";
            mnuDNA_GEDCOM.Click += MnuDNA_GEDCOM_Click;
            // 
            // toolStripSeparator15
            // 
            toolStripSeparator15.Name = "toolStripSeparator15";
            toolStripSeparator15.Size = new Size(219, 6);
            toolStripSeparator15.Visible = false;
            // 
            // mnuJSON
            // 
            mnuJSON.Name = "mnuJSON";
            mnuJSON.Size = new Size(222, 22);
            mnuJSON.Text = "JSON for Visualisations";
            mnuJSON.Visible = false;
            mnuJSON.Click += MnuJSON_Click;
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { optionsToolStripMenuItem, toolStripSeparator2, displayOptionsOnLoadToolStripMenuItem, resetToDefaultFormSizeToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 22);
            toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            optionsToolStripMenuItem.Size = new Size(208, 22);
            optionsToolStripMenuItem.Text = "Options";
            optionsToolStripMenuItem.Click += OptionsToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(205, 6);
            // 
            // displayOptionsOnLoadToolStripMenuItem
            // 
            displayOptionsOnLoadToolStripMenuItem.CheckOnClick = true;
            displayOptionsOnLoadToolStripMenuItem.Name = "displayOptionsOnLoadToolStripMenuItem";
            displayOptionsOnLoadToolStripMenuItem.Size = new Size(208, 22);
            displayOptionsOnLoadToolStripMenuItem.Text = "Display Options on Load";
            displayOptionsOnLoadToolStripMenuItem.Click += DisplayOptionsOnLoadToolStripMenuItem_Click;
            // 
            // resetToDefaultFormSizeToolStripMenuItem
            // 
            resetToDefaultFormSizeToolStripMenuItem.Name = "resetToDefaultFormSizeToolStripMenuItem";
            resetToDefaultFormSizeToolStripMenuItem.Size = new Size(208, 22);
            resetToDefaultFormSizeToolStripMenuItem.Text = "Reset to Default form size";
            resetToDefaultFormSizeToolStripMenuItem.Click += ResetToDefaultFormSizeToolStripMenuItem_Click;
            // 
            // mnuMaps
            // 
            mnuMaps.DropDownItems.AddRange(new ToolStripItem[] { mnuShowTimeline, mnuLifelines, mnuPlaces, toolStripSeparator4, mnuLocationsGeocodeReport, toolStripSeparator10, mnuGeocodeLocations, mnuOSGeocoder, mnuLookupBlankFoundLocations, toolStripSeparator16, mnuGoogleMyMaps });
            mnuMaps.Name = "mnuMaps";
            mnuMaps.Size = new Size(48, 22);
            mnuMaps.Text = "Maps";
            // 
            // mnuShowTimeline
            // 
            mnuShowTimeline.Name = "mnuShowTimeline";
            mnuShowTimeline.Size = new Size(284, 22);
            mnuShowTimeline.Text = "Show Timeline";
            mnuShowTimeline.Click += MnuShowTimeline_Click;
            // 
            // mnuLifelines
            // 
            mnuLifelines.Name = "mnuLifelines";
            mnuLifelines.Size = new Size(284, 22);
            mnuLifelines.Text = "Show Lifelines";
            mnuLifelines.Click += MnuLifelines_Click;
            // 
            // mnuPlaces
            // 
            mnuPlaces.Name = "mnuPlaces";
            mnuPlaces.Size = new Size(284, 22);
            mnuPlaces.Text = "Show Places";
            mnuPlaces.Click += MnuPlaces_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(281, 6);
            // 
            // mnuLocationsGeocodeReport
            // 
            mnuLocationsGeocodeReport.Name = "mnuLocationsGeocodeReport";
            mnuLocationsGeocodeReport.Size = new Size(284, 22);
            mnuLocationsGeocodeReport.Text = "Display Geocoded Locations";
            mnuLocationsGeocodeReport.Click += LocationsGeocodeReportToolStripMenuItem_Click;
            // 
            // toolStripSeparator10
            // 
            toolStripSeparator10.Name = "toolStripSeparator10";
            toolStripSeparator10.Size = new Size(281, 6);
            // 
            // mnuGeocodeLocations
            // 
            mnuGeocodeLocations.Name = "mnuGeocodeLocations";
            mnuGeocodeLocations.Size = new Size(284, 22);
            mnuGeocodeLocations.Text = "Run Google Geocoder to Find Locations";
            mnuGeocodeLocations.Click += MnuGeocodeLocations_Click;
            // 
            // mnuOSGeocoder
            // 
            mnuOSGeocoder.Name = "mnuOSGeocoder";
            mnuOSGeocoder.Size = new Size(284, 22);
            mnuOSGeocoder.Text = "Run OS Geocoder to Find Locations";
            mnuOSGeocoder.Click += MnuOSGeocoder_Click;
            // 
            // mnuLookupBlankFoundLocations
            // 
            mnuLookupBlankFoundLocations.Name = "mnuLookupBlankFoundLocations";
            mnuLookupBlankFoundLocations.Size = new Size(284, 22);
            mnuLookupBlankFoundLocations.Text = "Lookup Blank Google Locations";
            mnuLookupBlankFoundLocations.Click += MnuLookupBlankFoundLocations_Click;
            // 
            // toolStripSeparator16
            // 
            toolStripSeparator16.Name = "toolStripSeparator16";
            toolStripSeparator16.Size = new Size(281, 6);
            // 
            // mnuGoogleMyMaps
            // 
            mnuGoogleMyMaps.Name = "mnuGoogleMyMaps";
            mnuGoogleMyMaps.Size = new Size(284, 22);
            mnuGoogleMyMaps.Text = "Export to Google MyMaps";
            mnuGoogleMyMaps.Click += MnuGoogleMyMaps_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { viewOnlineManualToolStripMenuItem, onlineGuidesToUsingFTAnalyzerToolStripMenuItem, reportAnIssueToolStripMenuItem, facebookSupportGroupToolStripMenuItem, facebookUserGroupToolStripMenuItem, toolStripSeparator1, getGoogleAPIKeyToolStripMenuItem, googleAPISetupGuideToolStripMenuItem, toolStripSeparator14, privacyPolicyToolStripMenuItem, whatsNewToolStripMenuItem, aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 22);
            helpToolStripMenuItem.Text = "Help";
            // 
            // viewOnlineManualToolStripMenuItem
            // 
            viewOnlineManualToolStripMenuItem.Name = "viewOnlineManualToolStripMenuItem";
            viewOnlineManualToolStripMenuItem.Size = new Size(254, 22);
            viewOnlineManualToolStripMenuItem.Text = "View Online Manual";
            viewOnlineManualToolStripMenuItem.Click += ViewOnlineManualToolStripMenuItem_Click;
            // 
            // onlineGuidesToUsingFTAnalyzerToolStripMenuItem
            // 
            onlineGuidesToUsingFTAnalyzerToolStripMenuItem.Name = "onlineGuidesToUsingFTAnalyzerToolStripMenuItem";
            onlineGuidesToUsingFTAnalyzerToolStripMenuItem.Size = new Size(254, 22);
            onlineGuidesToUsingFTAnalyzerToolStripMenuItem.Text = "Online Guides to Using FTAnalyzer";
            onlineGuidesToUsingFTAnalyzerToolStripMenuItem.Click += OnlineGuidesToUsingFTAnalyzerToolStripMenuItem_Click;
            // 
            // reportAnIssueToolStripMenuItem
            // 
            reportAnIssueToolStripMenuItem.Name = "reportAnIssueToolStripMenuItem";
            reportAnIssueToolStripMenuItem.Size = new Size(254, 22);
            reportAnIssueToolStripMenuItem.Text = "Report an Issue";
            reportAnIssueToolStripMenuItem.Click += ReportAnIssueToolStripMenuItem_Click;
            // 
            // facebookSupportGroupToolStripMenuItem
            // 
            facebookSupportGroupToolStripMenuItem.Image = Properties.Resources.flogo_rgb_hex_brc_site_250;
            facebookSupportGroupToolStripMenuItem.Name = "facebookSupportGroupToolStripMenuItem";
            facebookSupportGroupToolStripMenuItem.Size = new Size(254, 22);
            facebookSupportGroupToolStripMenuItem.Text = "Facebook Support Page";
            facebookSupportGroupToolStripMenuItem.Click += FacebookSupportGroupToolStripMenuItem_Click;
            // 
            // facebookUserGroupToolStripMenuItem
            // 
            facebookUserGroupToolStripMenuItem.Image = Properties.Resources.flogo_rgb_hex_brc_site_250;
            facebookUserGroupToolStripMenuItem.Name = "facebookUserGroupToolStripMenuItem";
            facebookUserGroupToolStripMenuItem.Size = new Size(254, 22);
            facebookUserGroupToolStripMenuItem.Text = "Facebook User Group";
            facebookUserGroupToolStripMenuItem.Click += FacebookUserGroupToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(251, 6);
            // 
            // getGoogleAPIKeyToolStripMenuItem
            // 
            getGoogleAPIKeyToolStripMenuItem.Image = Properties.Resources.GoogleMapsAPI;
            getGoogleAPIKeyToolStripMenuItem.Name = "getGoogleAPIKeyToolStripMenuItem";
            getGoogleAPIKeyToolStripMenuItem.Size = new Size(254, 22);
            getGoogleAPIKeyToolStripMenuItem.Text = "Get Google API Key";
            getGoogleAPIKeyToolStripMenuItem.Click += GetGoogleAPIKeyToolStripMenuItem_Click;
            // 
            // googleAPISetupGuideToolStripMenuItem
            // 
            googleAPISetupGuideToolStripMenuItem.Image = Properties.Resources.GoogleMapsAPI;
            googleAPISetupGuideToolStripMenuItem.Name = "googleAPISetupGuideToolStripMenuItem";
            googleAPISetupGuideToolStripMenuItem.Size = new Size(254, 22);
            googleAPISetupGuideToolStripMenuItem.Text = "Google API Setup Guide";
            googleAPISetupGuideToolStripMenuItem.Click += GoogleAPISetupGuideToolStripMenuItem_Click;
            // 
            // toolStripSeparator14
            // 
            toolStripSeparator14.Name = "toolStripSeparator14";
            toolStripSeparator14.Size = new Size(251, 6);
            // 
            // privacyPolicyToolStripMenuItem
            // 
            privacyPolicyToolStripMenuItem.Name = "privacyPolicyToolStripMenuItem";
            privacyPolicyToolStripMenuItem.Size = new Size(254, 22);
            privacyPolicyToolStripMenuItem.Text = "Privacy Policy";
            privacyPolicyToolStripMenuItem.Click += PrivacyPolicyToolStripMenuItem_Click;
            // 
            // whatsNewToolStripMenuItem
            // 
            whatsNewToolStripMenuItem.Name = "whatsNewToolStripMenuItem";
            whatsNewToolStripMenuItem.Size = new Size(254, 22);
            whatsNewToolStripMenuItem.Text = "What's New";
            whatsNewToolStripMenuItem.Click += WhatsNewToolStripMenuItem_Click;
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(254, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += AboutToolStripMenuItem_Click;
            // 
            // mnuSetRoot
            // 
            mnuSetRoot.ImageScalingSize = new Size(32, 32);
            mnuSetRoot.Items.AddRange(new ToolStripItem[] { setAsRootToolStripMenuItem, viewNotesToolStripMenuItem });
            mnuSetRoot.Name = "mnuSetRoot";
            mnuSetRoot.Size = new Size(174, 48);
            mnuSetRoot.Opened += MnuSetRoot_Opened;
            // 
            // setAsRootToolStripMenuItem
            // 
            setAsRootToolStripMenuItem.Name = "setAsRootToolStripMenuItem";
            setAsRootToolStripMenuItem.Size = new Size(173, 22);
            setAsRootToolStripMenuItem.Text = "Set As Root Person";
            setAsRootToolStripMenuItem.Click += SetAsRootToolStripMenuItem_Click;
            // 
            // viewNotesToolStripMenuItem
            // 
            viewNotesToolStripMenuItem.Name = "viewNotesToolStripMenuItem";
            viewNotesToolStripMenuItem.Size = new Size(173, 22);
            viewNotesToolStripMenuItem.Text = "View Notes";
            viewNotesToolStripMenuItem.Click += ViewNotesToolStripMenuItem_Click;
            // 
            // tsCount
            // 
            tsCount.Name = "tsCount";
            tsCount.Size = new Size(52, 17);
            tsCount.Text = "Count: 0";
            // 
            // statusStrip
            // 
            statusStrip.ImageScalingSize = new Size(32, 32);
            statusStrip.Items.AddRange(new ToolStripItem[] { tsCountLabel, tsHintsLabel, tspbTabProgress, tsStatusLabel });
            statusStrip.Location = new Point(0, 475);
            statusStrip.Name = "statusStrip";
            statusStrip.Padding = new Padding(1, 0, 16, 0);
            statusStrip.Size = new Size(1246, 22);
            statusStrip.TabIndex = 10;
            statusStrip.Text = "statusStrip1";
            // 
            // tsCountLabel
            // 
            tsCountLabel.Name = "tsCountLabel";
            tsCountLabel.Size = new Size(0, 17);
            // 
            // tsHintsLabel
            // 
            tsHintsLabel.Name = "tsHintsLabel";
            tsHintsLabel.Size = new Size(0, 17);
            // 
            // tspbTabProgress
            // 
            tspbTabProgress.Name = "tspbTabProgress";
            tspbTabProgress.Size = new Size(233, 16);
            tspbTabProgress.Visible = false;
            // 
            // tsStatusLabel
            // 
            tsStatusLabel.Name = "tsStatusLabel";
            tsStatusLabel.Size = new Size(0, 17);
            // 
            // cmbColourFamily
            // 
            cmbColourFamily.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            cmbColourFamily.FormattingEnabled = true;
            cmbColourFamily.Location = new Point(474, 56);
            cmbColourFamily.Margin = new Padding(4);
            cmbColourFamily.Name = "cmbColourFamily";
            cmbColourFamily.Size = new Size(664, 23);
            cmbColourFamily.TabIndex = 60;
            toolTips.SetToolTip(cmbColourFamily, "Select a family to limit the reports to just that family");
            cmbColourFamily.Click += CmbColourFamily_Click;
            // 
            // btnRandomSurnameColour
            // 
            btnRandomSurnameColour.Location = new Point(726, 15);
            btnRandomSurnameColour.Margin = new Padding(4);
            btnRandomSurnameColour.Name = "btnRandomSurnameColour";
            btnRandomSurnameColour.Size = new Size(342, 29);
            btnRandomSurnameColour.TabIndex = 62;
            btnRandomSurnameColour.Text = "Select Random Surname from Direct Ancestor's Surnames";
            toolTips.SetToolTip(btnRandomSurnameColour, "Once selected click the appropriate report button to view the report. eg: UK Colour Census Report.");
            btnRandomSurnameColour.UseVisualStyleBackColor = true;
            btnRandomSurnameColour.Click += BtnRandomSurnameColour_Click;
            // 
            // tbDuplicateScore
            // 
            tbDuplicateScore.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tbDuplicateScore.BackColor = SystemColors.ControlLightLight;
            tbDuplicateScore.Location = new Point(522, 4);
            tbDuplicateScore.Margin = new Padding(4);
            tbDuplicateScore.Minimum = 1;
            tbDuplicateScore.Name = "tbDuplicateScore";
            tbDuplicateScore.Size = new Size(700, 45);
            tbDuplicateScore.TabIndex = 22;
            tbDuplicateScore.TickFrequency = 5;
            toolTips.SetToolTip(tbDuplicateScore, "Adjust Slider to right to limit results to more likely matches");
            tbDuplicateScore.Value = 1;
            tbDuplicateScore.Scroll += TbDuplicateScore_Scroll;
            // 
            // chkLCRootPersonConfirm
            // 
            chkLCRootPersonConfirm.Font = new Font("Microsoft Sans Serif", 8.25F);
            chkLCRootPersonConfirm.Location = new Point(35, 170);
            chkLCRootPersonConfirm.Margin = new Padding(4);
            chkLCRootPersonConfirm.Name = "chkLCRootPersonConfirm";
            chkLCRootPersonConfirm.RightToLeft = RightToLeft.Yes;
            chkLCRootPersonConfirm.Size = new Size(475, 28);
            chkLCRootPersonConfirm.TabIndex = 4;
            chkLCRootPersonConfirm.Text = "rootperson";
            chkLCRootPersonConfirm.TextAlign = ContentAlignment.MiddleRight;
            toolTips.SetToolTip(chkLCRootPersonConfirm, "The Lost Cousins Data includes a relationship field please make sure the root person relates to the root person on the Lost Cousins website.");
            chkLCRootPersonConfirm.UseVisualStyleBackColor = true;
            chkLCRootPersonConfirm.CheckedChanged += ChkLCRootPersonConfirm_CheckedChanged;
            // 
            // dgCheckAncestors
            // 
            dgCheckAncestors.AllowUserToAddRows = false;
            dgCheckAncestors.AllowUserToDeleteRows = false;
            dgCheckAncestors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgCheckAncestors.ColumnHeadersHeight = 40;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = SystemColors.Window;
            dataGridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle5.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
            dgCheckAncestors.DefaultCellStyle = dataGridViewCellStyle5;
            dgCheckAncestors.Location = new Point(4, 98);
            dgCheckAncestors.Margin = new Padding(4);
            dgCheckAncestors.Name = "dgCheckAncestors";
            dgCheckAncestors.ReadOnly = true;
            dgCheckAncestors.RowHeadersWidth = 82;
            dgCheckAncestors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgCheckAncestors.ShowCellToolTips = false;
            dgCheckAncestors.ShowEditingIcon = false;
            dgCheckAncestors.Size = new Size(1217, 287);
            dgCheckAncestors.TabIndex = 7;
            toolTips.SetToolTip(dgCheckAncestors, "Double click to see list of facts for that individual");
            // 
            // labErrorTabAggrMatch
            // 
            labErrorTabAggrMatch.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labErrorTabAggrMatch.AutoSize = true;
            labErrorTabAggrMatch.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            labErrorTabAggrMatch.Location = new Point(1098, 28);
            labErrorTabAggrMatch.Margin = new Padding(4, 0, 4, 0);
            labErrorTabAggrMatch.Name = "labErrorTabAggrMatch";
            labErrorTabAggrMatch.Size = new Size(108, 13);
            labErrorTabAggrMatch.TabIndex = 24;
            labErrorTabAggrMatch.Text = "Aggressive Match";
            toolTips.SetToolTip(labErrorTabAggrMatch, "Will produce duplicates in list when the two individuals are a very close match to each other - only those with highest duplicate match score");
            // 
            // labErrorTabLooseMatch
            // 
            labErrorTabLooseMatch.AutoSize = true;
            labErrorTabLooseMatch.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            labErrorTabLooseMatch.Location = new Point(518, 28);
            labErrorTabLooseMatch.Margin = new Padding(4, 0, 4, 0);
            labErrorTabLooseMatch.Name = "labErrorTabLooseMatch";
            labErrorTabLooseMatch.Size = new Size(80, 13);
            labErrorTabLooseMatch.TabIndex = 23;
            labErrorTabLooseMatch.Text = "Loose Match";
            toolTips.SetToolTip(labErrorTabLooseMatch, "Will produce duplicates in list when the two individuals decent but vague match to each other - Lowest duplicate match score");
            // 
            // chkIgnoreUnnamedTwins
            // 
            chkIgnoreUnnamedTwins.AutoSize = true;
            chkIgnoreUnnamedTwins.Checked = true;
            chkIgnoreUnnamedTwins.CheckState = CheckState.Checked;
            chkIgnoreUnnamedTwins.Location = new Point(287, 74);
            chkIgnoreUnnamedTwins.Margin = new Padding(4);
            chkIgnoreUnnamedTwins.Name = "chkIgnoreUnnamedTwins";
            chkIgnoreUnnamedTwins.Size = new Size(92, 19);
            chkIgnoreUnnamedTwins.TabIndex = 29;
            chkIgnoreUnnamedTwins.Text = "Ignore Twins";
            toolTips.SetToolTip(chkIgnoreUnnamedTwins, "Ignores duplicates where forename is unknown");
            chkIgnoreUnnamedTwins.UseVisualStyleBackColor = true;
            chkIgnoreUnnamedTwins.Visible = false;
            // 
            // radioOnlyPreferred
            // 
            radioOnlyPreferred.AutoSize = true;
            radioOnlyPreferred.Location = new Point(119, 2);
            radioOnlyPreferred.Margin = new Padding(2);
            radioOnlyPreferred.Name = "radioOnlyPreferred";
            radioOnlyPreferred.Size = new Size(131, 19);
            radioOnlyPreferred.TabIndex = 39;
            radioOnlyPreferred.Text = "Show only Preferred";
            toolTips.SetToolTip(radioOnlyPreferred, "Select this option to only show Preferred facts when you click one of the display buttons");
            radioOnlyPreferred.UseVisualStyleBackColor = true;
            radioOnlyPreferred.CheckedChanged += RadioFacts_CheckedChanged;
            // 
            // radioOnlyAlternate
            // 
            radioOnlyAlternate.AutoSize = true;
            radioOnlyAlternate.Location = new Point(266, 4);
            radioOnlyAlternate.Margin = new Padding(2);
            radioOnlyAlternate.Name = "radioOnlyAlternate";
            radioOnlyAlternate.Size = new Size(140, 19);
            radioOnlyAlternate.TabIndex = 40;
            radioOnlyAlternate.Text = "Show only Alternative";
            toolTips.SetToolTip(radioOnlyAlternate, "Select this option to only show Alternative facts when you click one of the display buttons");
            radioOnlyAlternate.UseVisualStyleBackColor = true;
            radioOnlyAlternate.CheckedChanged += RadioFacts_CheckedChanged;
            // 
            // dgDataErrors
            // 
            dgDataErrors.AllowUserToAddRows = false;
            dgDataErrors.AllowUserToDeleteRows = false;
            dgDataErrors.AllowUserToOrderColumns = true;
            dgDataErrors.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgDataErrors.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgDataErrors.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgDataErrors.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = SystemColors.Window;
            dataGridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 8.25F);
            dataGridViewCellStyle6.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgDataErrors.DefaultCellStyle = dataGridViewCellStyle6;
            dgDataErrors.FilterAndSortEnabled = true;
            dgDataErrors.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgDataErrors.Location = new Point(9, 195);
            dgDataErrors.Margin = new Padding(7);
            dgDataErrors.MultiSelect = false;
            dgDataErrors.Name = "dgDataErrors";
            dgDataErrors.ReadOnly = true;
            dgDataErrors.RightToLeft = RightToLeft.No;
            dgDataErrors.RowHeadersVisible = false;
            dgDataErrors.RowHeadersWidth = 50;
            dgDataErrors.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgDataErrors.ShowCellToolTips = false;
            dgDataErrors.ShowEditingIcon = false;
            dgDataErrors.Size = new Size(1221, 189);
            dgDataErrors.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgDataErrors.TabIndex = 7;
            toolTips.SetToolTip(dgDataErrors, "Double click to see list of facts for that individual");
            dgDataErrors.VirtualMode = true;
            dgDataErrors.CellDoubleClick += DgDataErrors_CellDoubleClick;
            // 
            // dgCountries
            // 
            dgCountries.AllowUserToAddRows = false;
            dgCountries.AllowUserToDeleteRows = false;
            dgCountries.AllowUserToOrderColumns = true;
            dgCountries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgCountries.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgCountries.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgCountries.Dock = DockStyle.Fill;
            dgCountries.FilterAndSortEnabled = true;
            dgCountries.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgCountries.Location = new Point(4, 4);
            dgCountries.Margin = new Padding(7);
            dgCountries.MultiSelect = false;
            dgCountries.Name = "dgCountries";
            dgCountries.ReadOnly = true;
            dgCountries.RightToLeft = RightToLeft.No;
            dgCountries.RowHeadersVisible = false;
            dgCountries.RowHeadersWidth = 50;
            dgCountries.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgCountries.Size = new Size(1218, 372);
            dgCountries.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgCountries.TabIndex = 1;
            toolTips.SetToolTip(dgCountries, "Double click on Country name to see list of individuals with that Country.");
            dgCountries.VirtualMode = true;
            // 
            // dgRegions
            // 
            dgRegions.AllowUserToAddRows = false;
            dgRegions.AllowUserToDeleteRows = false;
            dgRegions.AllowUserToOrderColumns = true;
            dgRegions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgRegions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgRegions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgRegions.Dock = DockStyle.Fill;
            dgRegions.FilterAndSortEnabled = true;
            dgRegions.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgRegions.Location = new Point(4, 4);
            dgRegions.Margin = new Padding(7);
            dgRegions.MultiSelect = false;
            dgRegions.Name = "dgRegions";
            dgRegions.ReadOnly = true;
            dgRegions.RightToLeft = RightToLeft.No;
            dgRegions.RowHeadersVisible = false;
            dgRegions.RowHeadersWidth = 50;
            dgRegions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgRegions.Size = new Size(1218, 372);
            dgRegions.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgRegions.TabIndex = 2;
            toolTips.SetToolTip(dgRegions, "Double click on Region name to see list of individuals with that Region.");
            dgRegions.VirtualMode = true;
            // 
            // btnShowExclusions
            // 
            btnShowExclusions.Location = new Point(383, 246);
            btnShowExclusions.Margin = new Padding(4);
            btnShowExclusions.Name = "btnShowExclusions";
            btnShowExclusions.Size = new Size(33, 59);
            btnShowExclusions.TabIndex = 41;
            btnShowExclusions.Text = "=>";
            toolTips.SetToolTip(btnShowExclusions, "Show Exclusions");
            btnShowExclusions.UseVisualStyleBackColor = true;
            btnShowExclusions.Click += BtnShowExclusions_Click;
            // 
            // ckbFactExclude
            // 
            ckbFactExclude.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ckbFactExclude.FormattingEnabled = true;
            ckbFactExclude.Location = new Point(424, 158);
            ckbFactExclude.Margin = new Padding(4);
            ckbFactExclude.Name = "ckbFactExclude";
            ckbFactExclude.ScrollAlwaysVisible = true;
            ckbFactExclude.SelectionMode = SelectionMode.None;
            ckbFactExclude.Size = new Size(365, 220);
            ckbFactExclude.TabIndex = 38;
            toolTips.SetToolTip(ckbFactExclude, "Any fact types selected in this box excludes people who have this fact type from report");
            ckbFactExclude.Visible = false;
            ckbFactExclude.MouseClick += CkbFactExclude_MouseClick;
            // 
            // btnUpdateLostCousinsWebsite
            // 
            btnUpdateLostCousinsWebsite.Enabled = false;
            btnUpdateLostCousinsWebsite.Location = new Point(349, 201);
            btnUpdateLostCousinsWebsite.Margin = new Padding(4);
            btnUpdateLostCousinsWebsite.Name = "btnUpdateLostCousinsWebsite";
            btnUpdateLostCousinsWebsite.Size = new Size(161, 26);
            btnUpdateLostCousinsWebsite.TabIndex = 5;
            btnUpdateLostCousinsWebsite.Text = "Update Lost Cousins site";
            btnUpdateLostCousinsWebsite.UseVisualStyleBackColor = true;
            btnUpdateLostCousinsWebsite.Visible = false;
            btnUpdateLostCousinsWebsite.Click += BtnUpdateLostCousinsWebsite_Click;
            // 
            // printPreviewDialog
            // 
            printPreviewDialog.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog.ClientSize = new Size(400, 300);
            printPreviewDialog.Enabled = true;
            printPreviewDialog.Icon = (Icon)resources.GetObject("printPreviewDialog.Icon");
            printPreviewDialog.Name = "printPreviewDialog";
            printPreviewDialog.Visible = false;
            // 
            // printDialog
            // 
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;
            printDialog.UseEXDialog = true;
            // 
            // tabWorldWars
            // 
            tabWorldWars.Controls.Add(ckbMilitaryOnly);
            tabWorldWars.Controls.Add(ckbWDIgnoreLocations);
            tabWorldWars.Controls.Add(btnWWII);
            tabWorldWars.Controls.Add(btnWWI);
            tabWorldWars.Controls.Add(label9);
            tabWorldWars.Controls.Add(txtWorldWarsSurname);
            tabWorldWars.Controls.Add(dgWorldWars);
            tabWorldWars.Controls.Add(wardeadRelation);
            tabWorldWars.Controls.Add(wardeadCountry);
            tabWorldWars.Location = new Point(4, 24);
            tabWorldWars.Margin = new Padding(4);
            tabWorldWars.Name = "tabWorldWars";
            tabWorldWars.Size = new Size(1238, 412);
            tabWorldWars.TabIndex = 8;
            tabWorldWars.Text = "World Wars";
            tabWorldWars.ToolTipText = "Find men of fighting age during WWI & WWII";
            tabWorldWars.UseVisualStyleBackColor = true;
            // 
            // ckbMilitaryOnly
            // 
            ckbMilitaryOnly.AutoSize = true;
            ckbMilitaryOnly.Location = new Point(326, 121);
            ckbMilitaryOnly.Margin = new Padding(4);
            ckbMilitaryOnly.Name = "ckbMilitaryOnly";
            ckbMilitaryOnly.Size = new Size(291, 19);
            ckbMilitaryOnly.TabIndex = 33;
            ckbMilitaryOnly.Text = "Limit Results to only those men with Military Facts";
            ckbMilitaryOnly.UseVisualStyleBackColor = true;
            // 
            // ckbWDIgnoreLocations
            // 
            ckbWDIgnoreLocations.AutoSize = true;
            ckbWDIgnoreLocations.Checked = true;
            ckbWDIgnoreLocations.CheckState = CheckState.Checked;
            ckbWDIgnoreLocations.Location = new Point(9, 121);
            ckbWDIgnoreLocations.Margin = new Padding(4);
            ckbWDIgnoreLocations.Name = "ckbWDIgnoreLocations";
            ckbWDIgnoreLocations.Size = new Size(279, 19);
            ckbWDIgnoreLocations.TabIndex = 32;
            ckbWDIgnoreLocations.Text = "Include Unknown Countries in World Wars Filter";
            ckbWDIgnoreLocations.UseVisualStyleBackColor = true;
            ckbWDIgnoreLocations.CheckedChanged += CkbWDIgnoreLocations_CheckedChanged;
            // 
            // btnWWII
            // 
            btnWWII.Location = new Point(884, 67);
            btnWWII.Margin = new Padding(4);
            btnWWII.Name = "btnWWII";
            btnWWII.Size = new Size(111, 29);
            btnWWII.TabIndex = 31;
            btnWWII.Text = "World War II";
            btnWWII.UseVisualStyleBackColor = true;
            btnWWII.Click += BtnWWII_Click;
            // 
            // btnWWI
            // 
            btnWWI.Location = new Point(758, 67);
            btnWWI.Margin = new Padding(4);
            btnWWI.Name = "btnWWI";
            btnWWI.Size = new Size(111, 29);
            btnWWI.TabIndex = 30;
            btnWWI.Text = "World War I";
            btnWWI.UseVisualStyleBackColor = true;
            btnWWI.Click += BtnWWI_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(694, 29);
            label9.Margin = new Padding(4, 0, 4, 0);
            label9.Name = "label9";
            label9.Size = new Size(54, 15);
            label9.TabIndex = 28;
            label9.Text = "Surname";
            // 
            // txtWorldWarsSurname
            // 
            txtWorldWarsSurname.Location = new Point(758, 25);
            txtWorldWarsSurname.Margin = new Padding(4);
            txtWorldWarsSurname.Name = "txtWorldWarsSurname";
            txtWorldWarsSurname.Size = new Size(234, 23);
            txtWorldWarsSurname.TabIndex = 27;
            // 
            // dgWorldWars
            // 
            dgWorldWars.AllowUserToAddRows = false;
            dgWorldWars.AllowUserToDeleteRows = false;
            dgWorldWars.AllowUserToOrderColumns = true;
            dgWorldWars.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgWorldWars.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgWorldWars.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgWorldWars.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgWorldWars.FilterAndSortEnabled = true;
            dgWorldWars.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgWorldWars.Location = new Point(0, 151);
            dgWorldWars.Margin = new Padding(7);
            dgWorldWars.MultiSelect = false;
            dgWorldWars.Name = "dgWorldWars";
            dgWorldWars.ReadOnly = true;
            dgWorldWars.RightToLeft = RightToLeft.No;
            dgWorldWars.RowHeadersVisible = false;
            dgWorldWars.RowHeadersWidth = 50;
            dgWorldWars.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgWorldWars.Size = new Size(1238, 261);
            dgWorldWars.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgWorldWars.TabIndex = 29;
            dgWorldWars.VirtualMode = true;
            dgWorldWars.CellDoubleClick += DgWorldWars_CellDoubleClick;
            dgWorldWars.MouseDown += DgWorldWars_MouseDown;
            // 
            // wardeadRelation
            // 
            wardeadRelation.Location = new Point(315, 4);
            wardeadRelation.Margin = new Padding(7);
            wardeadRelation.MarriedToDB = true;
            wardeadRelation.Name = "wardeadRelation";
            wardeadRelation.Size = new Size(376, 115);
            wardeadRelation.TabIndex = 26;
            // 
            // wardeadCountry
            // 
            wardeadCountry.Location = new Point(9, 29);
            wardeadCountry.Margin = new Padding(7);
            wardeadCountry.Name = "wardeadCountry";
            wardeadCountry.Size = new Size(299, 85);
            wardeadCountry.TabIndex = 25;
            wardeadCountry.Title = "Default Country";
            wardeadCountry.UKEnabled = true;
            // 
            // ctxViewNotes
            // 
            ctxViewNotes.ImageScalingSize = new Size(32, 32);
            ctxViewNotes.Items.AddRange(new ToolStripItem[] { mnuViewNotes });
            ctxViewNotes.Name = "contextMenuStrip1";
            ctxViewNotes.Size = new Size(134, 26);
            ctxViewNotes.Opening += CtxViewNotes_Opening;
            // 
            // mnuViewNotes
            // 
            mnuViewNotes.Name = "mnuViewNotes";
            mnuViewNotes.Size = new Size(133, 22);
            mnuViewNotes.Text = "View Notes";
            mnuViewNotes.Click += MnuViewNotes_Click;
            // 
            // tabTreetops
            // 
            tabTreetops.Controls.Add(ckbTTIncludeOnlyOneParent);
            tabTreetops.Controls.Add(ckbTTIgnoreLocations);
            tabTreetops.Controls.Add(btnTreeTops);
            tabTreetops.Controls.Add(label8);
            tabTreetops.Controls.Add(txtTreetopsSurname);
            tabTreetops.Controls.Add(dgTreeTops);
            tabTreetops.Controls.Add(treetopsRelation);
            tabTreetops.Controls.Add(treetopsCountry);
            tabTreetops.Location = new Point(4, 24);
            tabTreetops.Margin = new Padding(4);
            tabTreetops.Name = "tabTreetops";
            tabTreetops.Size = new Size(1238, 412);
            tabTreetops.TabIndex = 7;
            tabTreetops.Text = "Treetops";
            tabTreetops.UseVisualStyleBackColor = true;
            // 
            // ckbTTIncludeOnlyOneParent
            // 
            ckbTTIncludeOnlyOneParent.AutoSize = true;
            ckbTTIncludeOnlyOneParent.Checked = true;
            ckbTTIncludeOnlyOneParent.CheckState = CheckState.Checked;
            ckbTTIncludeOnlyOneParent.Location = new Point(326, 121);
            ckbTTIncludeOnlyOneParent.Margin = new Padding(4);
            ckbTTIncludeOnlyOneParent.Name = "ckbTTIncludeOnlyOneParent";
            ckbTTIncludeOnlyOneParent.Size = new Size(302, 19);
            ckbTTIncludeOnlyOneParent.TabIndex = 29;
            ckbTTIncludeOnlyOneParent.Text = "Include Individuals that have only one parent known";
            ckbTTIncludeOnlyOneParent.UseVisualStyleBackColor = true;
            // 
            // ckbTTIgnoreLocations
            // 
            ckbTTIgnoreLocations.AutoSize = true;
            ckbTTIgnoreLocations.Checked = true;
            ckbTTIgnoreLocations.CheckState = CheckState.Checked;
            ckbTTIgnoreLocations.Location = new Point(9, 121);
            ckbTTIgnoreLocations.Margin = new Padding(4);
            ckbTTIgnoreLocations.Name = "ckbTTIgnoreLocations";
            ckbTTIgnoreLocations.Size = new Size(262, 19);
            ckbTTIgnoreLocations.TabIndex = 27;
            ckbTTIgnoreLocations.Text = "Include Unknown Countries in Treetops Filter";
            ckbTTIgnoreLocations.UseVisualStyleBackColor = true;
            ckbTTIgnoreLocations.CheckedChanged += CkbTTIgnoreLocations_CheckedChanged;
            // 
            // btnTreeTops
            // 
            btnTreeTops.Location = new Point(758, 67);
            btnTreeTops.Margin = new Padding(4);
            btnTreeTops.Name = "btnTreeTops";
            btnTreeTops.Size = new Size(235, 29);
            btnTreeTops.TabIndex = 25;
            btnTreeTops.Text = "Show People at top of tree";
            btnTreeTops.UseVisualStyleBackColor = true;
            btnTreeTops.Click += BtnTreeTops_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(694, 29);
            label8.Margin = new Padding(4, 0, 4, 0);
            label8.Name = "label8";
            label8.Size = new Size(54, 15);
            label8.TabIndex = 24;
            label8.Text = "Surname";
            // 
            // txtTreetopsSurname
            // 
            txtTreetopsSurname.Location = new Point(758, 25);
            txtTreetopsSurname.Margin = new Padding(4);
            txtTreetopsSurname.Name = "txtTreetopsSurname";
            txtTreetopsSurname.Size = new Size(234, 23);
            txtTreetopsSurname.TabIndex = 23;
            // 
            // dgTreeTops
            // 
            dgTreeTops.AllowUserToAddRows = false;
            dgTreeTops.AllowUserToDeleteRows = false;
            dgTreeTops.AllowUserToOrderColumns = true;
            dgTreeTops.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgTreeTops.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgTreeTops.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgTreeTops.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgTreeTops.FilterAndSortEnabled = true;
            dgTreeTops.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgTreeTops.Location = new Point(0, 151);
            dgTreeTops.Margin = new Padding(7);
            dgTreeTops.MultiSelect = false;
            dgTreeTops.Name = "dgTreeTops";
            dgTreeTops.ReadOnly = true;
            dgTreeTops.RightToLeft = RightToLeft.No;
            dgTreeTops.RowHeadersVisible = false;
            dgTreeTops.RowHeadersWidth = 50;
            dgTreeTops.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgTreeTops.Size = new Size(1238, 261);
            dgTreeTops.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgTreeTops.TabIndex = 28;
            dgTreeTops.VirtualMode = true;
            dgTreeTops.CellDoubleClick += DgTreeTops_CellDoubleClick;
            dgTreeTops.MouseDown += DgTreeTops_MouseDown;
            // 
            // treetopsRelation
            // 
            treetopsRelation.Location = new Point(315, 4);
            treetopsRelation.Margin = new Padding(7);
            treetopsRelation.MarriedToDB = true;
            treetopsRelation.Name = "treetopsRelation";
            treetopsRelation.Size = new Size(376, 111);
            treetopsRelation.TabIndex = 12;
            // 
            // treetopsCountry
            // 
            treetopsCountry.Location = new Point(9, 29);
            treetopsCountry.Margin = new Padding(7);
            treetopsCountry.Name = "treetopsCountry";
            treetopsCountry.Size = new Size(299, 85);
            treetopsCountry.TabIndex = 11;
            treetopsCountry.Title = "Default Country";
            treetopsCountry.UKEnabled = true;
            // 
            // tabColourReports
            // 
            tabColourReports.Controls.Add(groupBox7);
            tabColourReports.Controls.Add(btnRandomSurnameColour);
            tabColourReports.Controls.Add(labResearchTabFamilyFilter);
            tabColourReports.Controls.Add(cmbColourFamily);
            tabColourReports.Controls.Add(groupBox3);
            tabColourReports.Controls.Add(btnColourBMD);
            tabColourReports.Controls.Add(labResearchTabSurname);
            tabColourReports.Controls.Add(txtColouredSurname);
            tabColourReports.Controls.Add(relTypesColoured);
            tabColourReports.Location = new Point(4, 24);
            tabColourReports.Margin = new Padding(4);
            tabColourReports.Name = "tabColourReports";
            tabColourReports.Size = new Size(1238, 412);
            tabColourReports.TabIndex = 12;
            tabColourReports.Text = "Research Suggestions";
            tabColourReports.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            groupBox7.Controls.Add(btnAdvancedMissingData);
            groupBox7.Controls.Add(btnStandardMissingData);
            groupBox7.Location = new Point(494, 134);
            groupBox7.Margin = new Padding(4);
            groupBox7.Name = "groupBox7";
            groupBox7.Padding = new Padding(4);
            groupBox7.Size = new Size(477, 126);
            groupBox7.TabIndex = 63;
            groupBox7.TabStop = false;
            groupBox7.Text = "Missing Data Reports";
            groupBox7.Visible = false;
            // 
            // btnAdvancedMissingData
            // 
            btnAdvancedMissingData.Location = new Point(242, 22);
            btnAdvancedMissingData.Margin = new Padding(4);
            btnAdvancedMissingData.Name = "btnAdvancedMissingData";
            btnAdvancedMissingData.Size = new Size(228, 26);
            btnAdvancedMissingData.TabIndex = 40;
            btnAdvancedMissingData.Text = "Advanced Missing Data Report";
            btnAdvancedMissingData.UseVisualStyleBackColor = true;
            btnAdvancedMissingData.Click += BtnAdvancedMissingData_Click;
            // 
            // btnStandardMissingData
            // 
            btnStandardMissingData.Location = new Point(7, 22);
            btnStandardMissingData.Margin = new Padding(4);
            btnStandardMissingData.Name = "btnStandardMissingData";
            btnStandardMissingData.Size = new Size(228, 26);
            btnStandardMissingData.TabIndex = 39;
            btnStandardMissingData.Text = "Standard Missing Data Report";
            btnStandardMissingData.UseVisualStyleBackColor = true;
            btnStandardMissingData.Click += BtnStandardMissingData_Click;
            // 
            // labResearchTabFamilyFilter
            // 
            labResearchTabFamilyFilter.AutoSize = true;
            labResearchTabFamilyFilter.Location = new Point(396, 60);
            labResearchTabFamilyFilter.Margin = new Padding(4, 0, 4, 0);
            labResearchTabFamilyFilter.Name = "labResearchTabFamilyFilter";
            labResearchTabFamilyFilter.Size = new Size(71, 15);
            labResearchTabFamilyFilter.TabIndex = 61;
            labResearchTabFamilyFilter.Text = "Family Filter";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(ckbIgnoreNoDeathDate);
            groupBox3.Controls.Add(ckbIgnoreNoBirthDate);
            groupBox3.Controls.Add(btnIrishColourCensus);
            groupBox3.Controls.Add(btnCanadianColourCensus);
            groupBox3.Controls.Add(btnUKColourCensus);
            groupBox3.Controls.Add(btnUSColourCensus);
            groupBox3.Location = new Point(9, 134);
            groupBox3.Margin = new Padding(4);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4);
            groupBox3.Size = new Size(477, 126);
            groupBox3.TabIndex = 36;
            groupBox3.TabStop = false;
            groupBox3.Text = "Census Suggestions Reports";
            // 
            // ckbIgnoreNoDeathDate
            // 
            ckbIgnoreNoDeathDate.AutoSize = true;
            ckbIgnoreNoDeathDate.Location = new Point(243, 89);
            ckbIgnoreNoDeathDate.Margin = new Padding(4);
            ckbIgnoreNoDeathDate.Name = "ckbIgnoreNoDeathDate";
            ckbIgnoreNoDeathDate.Size = new Size(222, 19);
            ckbIgnoreNoDeathDate.TabIndex = 67;
            ckbIgnoreNoDeathDate.Text = "Ignore Individuals with no death date";
            ckbIgnoreNoDeathDate.UseVisualStyleBackColor = true;
            // 
            // ckbIgnoreNoBirthDate
            // 
            ckbIgnoreNoBirthDate.AutoSize = true;
            ckbIgnoreNoBirthDate.Location = new Point(7, 89);
            ckbIgnoreNoBirthDate.Margin = new Padding(4);
            ckbIgnoreNoBirthDate.Name = "ckbIgnoreNoBirthDate";
            ckbIgnoreNoBirthDate.Size = new Size(217, 19);
            ckbIgnoreNoBirthDate.TabIndex = 66;
            ckbIgnoreNoBirthDate.Text = "Ignore Individuals with no birth date";
            ckbIgnoreNoBirthDate.UseVisualStyleBackColor = true;
            // 
            // btnIrishColourCensus
            // 
            btnIrishColourCensus.Location = new Point(242, 22);
            btnIrishColourCensus.Margin = new Padding(4);
            btnIrishColourCensus.Name = "btnIrishColourCensus";
            btnIrishColourCensus.Size = new Size(228, 26);
            btnIrishColourCensus.TabIndex = 39;
            btnIrishColourCensus.Text = "View Irish Colour Census Report";
            btnIrishColourCensus.UseVisualStyleBackColor = true;
            btnIrishColourCensus.Click += BtnIrishColourCensus_Click;
            // 
            // btnCanadianColourCensus
            // 
            btnCanadianColourCensus.Location = new Point(242, 55);
            btnCanadianColourCensus.Margin = new Padding(4);
            btnCanadianColourCensus.Name = "btnCanadianColourCensus";
            btnCanadianColourCensus.Size = new Size(228, 26);
            btnCanadianColourCensus.TabIndex = 41;
            btnCanadianColourCensus.Text = "View Canadian Colour Census Report";
            btnCanadianColourCensus.UseVisualStyleBackColor = true;
            btnCanadianColourCensus.Click += BtnCanadianColourCensus_Click;
            // 
            // btnUKColourCensus
            // 
            btnUKColourCensus.Location = new Point(7, 22);
            btnUKColourCensus.Margin = new Padding(4);
            btnUKColourCensus.Name = "btnUKColourCensus";
            btnUKColourCensus.Size = new Size(228, 26);
            btnUKColourCensus.TabIndex = 38;
            btnUKColourCensus.Text = "View UK Colour Census Report";
            btnUKColourCensus.UseVisualStyleBackColor = true;
            btnUKColourCensus.Click += BtnUKColourCensus_Click;
            // 
            // btnUSColourCensus
            // 
            btnUSColourCensus.Location = new Point(7, 55);
            btnUSColourCensus.Margin = new Padding(4);
            btnUSColourCensus.Name = "btnUSColourCensus";
            btnUSColourCensus.Size = new Size(228, 26);
            btnUSColourCensus.TabIndex = 40;
            btnUSColourCensus.Text = "View US Colour Census Report";
            btnUSColourCensus.UseVisualStyleBackColor = true;
            btnUSColourCensus.Click += BtnUSColourCensus_Click;
            // 
            // btnColourBMD
            // 
            btnColourBMD.Location = new Point(68, 266);
            btnColourBMD.Margin = new Padding(4);
            btnColourBMD.Name = "btnColourBMD";
            btnColourBMD.Size = new Size(358, 26);
            btnColourBMD.TabIndex = 42;
            btnColourBMD.Text = "View Colour Birth/Marriage/Death Report";
            btnColourBMD.UseVisualStyleBackColor = true;
            btnColourBMD.Click += BtnColourBMD_Click;
            // 
            // labResearchTabSurname
            // 
            labResearchTabSurname.AutoSize = true;
            labResearchTabSurname.Location = new Point(396, 22);
            labResearchTabSurname.Margin = new Padding(4, 0, 4, 0);
            labResearchTabSurname.Name = "labResearchTabSurname";
            labResearchTabSurname.Size = new Size(54, 15);
            labResearchTabSurname.TabIndex = 59;
            labResearchTabSurname.Text = "Surname";
            // 
            // txtColouredSurname
            // 
            txtColouredSurname.Location = new Point(474, 19);
            txtColouredSurname.Margin = new Padding(4);
            txtColouredSurname.Name = "txtColouredSurname";
            txtColouredSurname.Size = new Size(234, 23);
            txtColouredSurname.TabIndex = 30;
            txtColouredSurname.TextChanged += TxtColouredSurname_TextChanged;
            // 
            // relTypesColoured
            // 
            relTypesColoured.Location = new Point(9, 9);
            relTypesColoured.Margin = new Padding(7);
            relTypesColoured.MarriedToDB = true;
            relTypesColoured.Name = "relTypesColoured";
            relTypesColoured.Size = new Size(379, 118);
            relTypesColoured.TabIndex = 26;
            relTypesColoured.RelationTypesChanged += RelTypesColoured_RelationTypesChanged;
            // 
            // tabLostCousins
            // 
            tabLostCousins.Controls.Add(LCSubTabs);
            tabLostCousins.Location = new Point(4, 24);
            tabLostCousins.Margin = new Padding(4);
            tabLostCousins.Name = "tabLostCousins";
            tabLostCousins.Padding = new Padding(4);
            tabLostCousins.Size = new Size(1238, 412);
            tabLostCousins.TabIndex = 5;
            tabLostCousins.Text = "Lost Cousins";
            tabLostCousins.UseVisualStyleBackColor = true;
            // 
            // LCSubTabs
            // 
            LCSubTabs.Controls.Add(LCReportsTab);
            LCSubTabs.Controls.Add(LCUpdatesTab);
            LCSubTabs.Controls.Add(LCVerifyTab);
            LCSubTabs.Dock = DockStyle.Fill;
            LCSubTabs.Location = new Point(4, 4);
            LCSubTabs.Margin = new Padding(4);
            LCSubTabs.Name = "LCSubTabs";
            LCSubTabs.SelectedIndex = 0;
            LCSubTabs.Size = new Size(1230, 404);
            LCSubTabs.TabIndex = 0;
            // 
            // LCReportsTab
            // 
            LCReportsTab.Controls.Add(Referrals);
            LCReportsTab.Controls.Add(btnLCnoCensus);
            LCReportsTab.Controls.Add(btnLCDuplicates);
            LCReportsTab.Controls.Add(btnLCMissingCountry);
            LCReportsTab.Controls.Add(btnLC1940USA);
            LCReportsTab.Controls.Add(rtbLostCousins);
            LCReportsTab.Controls.Add(linkLabel2);
            LCReportsTab.Controls.Add(btnLC1911EW);
            LCReportsTab.Controls.Add(LabLostCousinsWeb);
            LCReportsTab.Controls.Add(ckbShowLCEntered);
            LCReportsTab.Controls.Add(btnLC1841EW);
            LCReportsTab.Controls.Add(btnLC1911Ireland);
            LCReportsTab.Controls.Add(btnLC1880USA);
            LCReportsTab.Controls.Add(btnLC1881EW);
            LCReportsTab.Controls.Add(btnLC1881Canada);
            LCReportsTab.Controls.Add(btnLC1881Scot);
            LCReportsTab.Controls.Add(relTypesLC);
            LCReportsTab.Location = new Point(4, 24);
            LCReportsTab.Margin = new Padding(4);
            LCReportsTab.Name = "LCReportsTab";
            LCReportsTab.Padding = new Padding(4);
            LCReportsTab.Size = new Size(1222, 376);
            LCReportsTab.TabIndex = 0;
            LCReportsTab.Text = "Reports";
            LCReportsTab.UseVisualStyleBackColor = true;
            // 
            // Referrals
            // 
            Referrals.Controls.Add(ckbReferralInCommon);
            Referrals.Controls.Add(btnReferrals);
            Referrals.Controls.Add(cmbReferrals);
            Referrals.Controls.Add(labLCSelectInd);
            Referrals.Location = new Point(7, 336);
            Referrals.Margin = new Padding(4);
            Referrals.Name = "Referrals";
            Referrals.Padding = new Padding(4);
            Referrals.Size = new Size(581, 96);
            Referrals.TabIndex = 40;
            Referrals.TabStop = false;
            Referrals.Text = "Referrals";
            // 
            // ckbReferralInCommon
            // 
            ckbReferralInCommon.AutoSize = true;
            ckbReferralInCommon.Location = new Point(13, 56);
            ckbReferralInCommon.Margin = new Padding(4);
            ckbReferralInCommon.Name = "ckbReferralInCommon";
            ckbReferralInCommon.Size = new Size(170, 19);
            ckbReferralInCommon.TabIndex = 3;
            ckbReferralInCommon.Text = "Limit to Common Relatives";
            ckbReferralInCommon.UseVisualStyleBackColor = true;
            // 
            // btnReferrals
            // 
            btnReferrals.Location = new Point(317, 52);
            btnReferrals.Margin = new Padding(4);
            btnReferrals.Name = "btnReferrals";
            btnReferrals.Size = new Size(257, 26);
            btnReferrals.TabIndex = 2;
            btnReferrals.Text = "Generate Referral Report for this Individual";
            btnReferrals.UseVisualStyleBackColor = true;
            btnReferrals.Click += BtnReferrals_Click;
            // 
            // cmbReferrals
            // 
            cmbReferrals.FormattingEnabled = true;
            cmbReferrals.Location = new Point(113, 21);
            cmbReferrals.Margin = new Padding(4);
            cmbReferrals.Name = "cmbReferrals";
            cmbReferrals.Size = new Size(460, 23);
            cmbReferrals.TabIndex = 1;
            cmbReferrals.Click += CmbReferrals_Click;
            // 
            // labLCSelectInd
            // 
            labLCSelectInd.AutoSize = true;
            labLCSelectInd.Location = new Point(7, 24);
            labLCSelectInd.Margin = new Padding(4, 0, 4, 0);
            labLCSelectInd.Name = "labLCSelectInd";
            labLCSelectInd.Size = new Size(93, 15);
            labLCSelectInd.TabIndex = 0;
            labLCSelectInd.Text = "Select Individual";
            // 
            // btnLCnoCensus
            // 
            btnLCnoCensus.Location = new Point(399, 284);
            btnLCnoCensus.Margin = new Padding(4);
            btnLCnoCensus.Name = "btnLCnoCensus";
            btnLCnoCensus.Size = new Size(189, 31);
            btnLCnoCensus.TabIndex = 39;
            btnLCnoCensus.Text = "Lost Cousins w/bad Census";
            btnLCnoCensus.UseVisualStyleBackColor = true;
            btnLCnoCensus.Click += BtnLCnoCensus_Click;
            // 
            // btnLCDuplicates
            // 
            btnLCDuplicates.Location = new Point(203, 284);
            btnLCDuplicates.Margin = new Padding(4);
            btnLCDuplicates.Name = "btnLCDuplicates";
            btnLCDuplicates.Size = new Size(189, 31);
            btnLCDuplicates.TabIndex = 38;
            btnLCDuplicates.Text = "Lost Cousins Duplicate Facts";
            btnLCDuplicates.UseVisualStyleBackColor = true;
            btnLCDuplicates.Click += BtnLCDuplicates_Click;
            // 
            // btnLCMissingCountry
            // 
            btnLCMissingCountry.Location = new Point(7, 284);
            btnLCMissingCountry.Margin = new Padding(4);
            btnLCMissingCountry.Name = "btnLCMissingCountry";
            btnLCMissingCountry.Size = new Size(189, 31);
            btnLCMissingCountry.TabIndex = 37;
            btnLCMissingCountry.Text = "Lost Cousins with no Country";
            btnLCMissingCountry.UseVisualStyleBackColor = true;
            btnLCMissingCountry.Click += BtnLCMissingCountry_Click;
            // 
            // btnLC1940USA
            // 
            btnLC1940USA.Location = new Point(399, 209);
            btnLC1940USA.Margin = new Padding(4);
            btnLC1940USA.Name = "btnLC1940USA";
            btnLC1940USA.Size = new Size(189, 31);
            btnLC1940USA.TabIndex = 35;
            btnLC1940USA.Text = "1940 US Census";
            btnLC1940USA.UseVisualStyleBackColor = true;
            btnLC1940USA.Click += BtnLC1940USA_Click;
            // 
            // rtbLostCousins
            // 
            rtbLostCousins.BackColor = Color.White;
            rtbLostCousins.BorderStyle = BorderStyle.None;
            rtbLostCousins.Font = new Font("Microsoft Sans Serif", 9F);
            rtbLostCousins.Location = new Point(625, 7);
            rtbLostCousins.Margin = new Padding(4);
            rtbLostCousins.Name = "rtbLostCousins";
            rtbLostCousins.ReadOnly = true;
            rtbLostCousins.Size = new Size(610, 396);
            rtbLostCousins.TabIndex = 34;
            rtbLostCousins.TabStop = false;
            rtbLostCousins.Text = "";
            // 
            // linkLabel2
            // 
            linkLabel2.AutoSize = true;
            linkLabel2.Font = new Font("Microsoft Sans Serif", 9.75F);
            linkLabel2.Location = new Point(847, 410);
            linkLabel2.Margin = new Padding(4, 0, 4, 0);
            linkLabel2.Name = "linkLabel2";
            linkLabel2.Size = new Size(173, 16);
            linkLabel2.TabIndex = 33;
            linkLabel2.TabStop = true;
            linkLabel2.Text = "Visit the Lost Cousins Forum";
            linkLabel2.LinkClicked += LinkLabel2_LinkClicked;
            // 
            // btnLC1911EW
            // 
            btnLC1911EW.Location = new Point(7, 209);
            btnLC1911EW.Margin = new Padding(4);
            btnLC1911EW.Name = "btnLC1911EW";
            btnLC1911EW.Size = new Size(189, 31);
            btnLC1911EW.TabIndex = 32;
            btnLC1911EW.Text = "1911 England && Wales Census";
            btnLC1911EW.UseVisualStyleBackColor = true;
            btnLC1911EW.Click += BtnLC1911EW_Click;
            // 
            // LabLostCousinsWeb
            // 
            LabLostCousinsWeb.AutoSize = true;
            LabLostCousinsWeb.Font = new Font("Microsoft Sans Serif", 9.75F);
            LabLostCousinsWeb.Location = new Point(623, 410);
            LabLostCousinsWeb.Margin = new Padding(4, 0, 4, 0);
            LabLostCousinsWeb.Name = "LabLostCousinsWeb";
            LabLostCousinsWeb.Size = new Size(185, 16);
            LabLostCousinsWeb.TabIndex = 31;
            LabLostCousinsWeb.TabStop = true;
            LabLostCousinsWeb.Text = "Visit the Lost Cousins Website";
            LabLostCousinsWeb.LinkClicked += LabLostCousinsWeb_Click;
            LabLostCousinsWeb.Click += LabLostCousinsWeb_Click;
            // 
            // ckbShowLCEntered
            // 
            ckbShowLCEntered.AutoSize = true;
            ckbShowLCEntered.Location = new Point(7, 247);
            ckbShowLCEntered.Margin = new Padding(4);
            ckbShowLCEntered.Name = "ckbShowLCEntered";
            ckbShowLCEntered.Size = new Size(460, 19);
            ckbShowLCEntered.TabIndex = 30;
            ckbShowLCEntered.Text = "Show already entered to Lost Cousins (unticked = show those to yet to be entered)";
            ckbShowLCEntered.UseVisualStyleBackColor = true;
            // 
            // btnLC1841EW
            // 
            btnLC1841EW.Location = new Point(7, 171);
            btnLC1841EW.Margin = new Padding(4);
            btnLC1841EW.Name = "btnLC1841EW";
            btnLC1841EW.Size = new Size(189, 31);
            btnLC1841EW.TabIndex = 29;
            btnLC1841EW.Text = "1841 England && Wales Census";
            btnLC1841EW.UseVisualStyleBackColor = true;
            btnLC1841EW.Click += BtnLC1841EW_Click;
            // 
            // btnLC1911Ireland
            // 
            btnLC1911Ireland.Location = new Point(203, 171);
            btnLC1911Ireland.Margin = new Padding(4);
            btnLC1911Ireland.Name = "btnLC1911Ireland";
            btnLC1911Ireland.Size = new Size(189, 31);
            btnLC1911Ireland.TabIndex = 28;
            btnLC1911Ireland.Text = "1911 Ireland Census";
            btnLC1911Ireland.UseVisualStyleBackColor = true;
            btnLC1911Ireland.Click += BtnLC1911Ireland_Click;
            // 
            // btnLC1880USA
            // 
            btnLC1880USA.Location = new Point(399, 171);
            btnLC1880USA.Margin = new Padding(4);
            btnLC1880USA.Name = "btnLC1880USA";
            btnLC1880USA.Size = new Size(189, 31);
            btnLC1880USA.TabIndex = 27;
            btnLC1880USA.Text = "1880 US Census";
            btnLC1880USA.UseVisualStyleBackColor = true;
            btnLC1880USA.Click += BtnLC1880USA_Click;
            // 
            // btnLC1881EW
            // 
            btnLC1881EW.Location = new Point(7, 133);
            btnLC1881EW.Margin = new Padding(4);
            btnLC1881EW.Name = "btnLC1881EW";
            btnLC1881EW.Size = new Size(189, 31);
            btnLC1881EW.TabIndex = 26;
            btnLC1881EW.Text = "1881 England && Wales Census";
            btnLC1881EW.UseVisualStyleBackColor = true;
            btnLC1881EW.Click += BtnLC1881EW_Click;
            // 
            // btnLC1881Canada
            // 
            btnLC1881Canada.Location = new Point(203, 209);
            btnLC1881Canada.Margin = new Padding(4);
            btnLC1881Canada.Name = "btnLC1881Canada";
            btnLC1881Canada.Size = new Size(189, 31);
            btnLC1881Canada.TabIndex = 25;
            btnLC1881Canada.Text = "1881 Canada Census";
            btnLC1881Canada.UseVisualStyleBackColor = true;
            btnLC1881Canada.Click += BtnLC1881Canada_Click;
            // 
            // btnLC1881Scot
            // 
            btnLC1881Scot.Location = new Point(203, 133);
            btnLC1881Scot.Margin = new Padding(4);
            btnLC1881Scot.Name = "btnLC1881Scot";
            btnLC1881Scot.Size = new Size(189, 31);
            btnLC1881Scot.TabIndex = 24;
            btnLC1881Scot.Text = "1881 Scotland Census";
            btnLC1881Scot.UseVisualStyleBackColor = true;
            btnLC1881Scot.Click += BtnLC1881Scot_Click;
            // 
            // relTypesLC
            // 
            relTypesLC.Location = new Point(7, 7);
            relTypesLC.Margin = new Padding(7);
            relTypesLC.MarriedToDB = true;
            relTypesLC.Name = "relTypesLC";
            relTypesLC.Size = new Size(379, 119);
            relTypesLC.TabIndex = 36;
            relTypesLC.RelationTypesChanged += RelTypesLC_RelationTypesChanged;
            // 
            // LCUpdatesTab
            // 
            LCUpdatesTab.Controls.Add(rtbLCoutput);
            LCUpdatesTab.Controls.Add(btnViewInvalidRefs);
            LCUpdatesTab.Controls.Add(btnLCPotentialUploads);
            LCUpdatesTab.Controls.Add(chkLCRootPersonConfirm);
            LCUpdatesTab.Controls.Add(label21);
            LCUpdatesTab.Controls.Add(rtbLCUpdateData);
            LCUpdatesTab.Controls.Add(groupBox8);
            LCUpdatesTab.Controls.Add(btnUpdateLostCousinsWebsite);
            LCUpdatesTab.Location = new Point(4, 24);
            LCUpdatesTab.Margin = new Padding(4);
            LCUpdatesTab.Name = "LCUpdatesTab";
            LCUpdatesTab.Padding = new Padding(4);
            LCUpdatesTab.Size = new Size(1222, 376);
            LCUpdatesTab.TabIndex = 1;
            LCUpdatesTab.Text = "Updates";
            LCUpdatesTab.UseVisualStyleBackColor = true;
            // 
            // rtbLCoutput
            // 
            rtbLCoutput.BackColor = SystemColors.Window;
            rtbLCoutput.Font = new Font("Microsoft Sans Serif", 9F);
            rtbLCoutput.Location = new Point(4, 234);
            rtbLCoutput.Margin = new Padding(4);
            rtbLCoutput.Name = "rtbLCoutput";
            rtbLCoutput.ReadOnly = true;
            rtbLCoutput.Size = new Size(1222, 258);
            rtbLCoutput.TabIndex = 41;
            rtbLCoutput.TabStop = false;
            rtbLCoutput.Text = resources.GetString("rtbLCoutput.Text");
            rtbLCoutput.TextChanged += RtbLCoutput_TextChanged;
            // 
            // btnViewInvalidRefs
            // 
            btnViewInvalidRefs.Location = new Point(177, 201);
            btnViewInvalidRefs.Margin = new Padding(4);
            btnViewInvalidRefs.Name = "btnViewInvalidRefs";
            btnViewInvalidRefs.Size = new Size(161, 26);
            btnViewInvalidRefs.TabIndex = 40;
            btnViewInvalidRefs.Text = "View Invalid Census Refs";
            btnViewInvalidRefs.UseVisualStyleBackColor = true;
            btnViewInvalidRefs.Click += BtnViewInvalidRefs_Click;
            // 
            // btnLCPotentialUploads
            // 
            btnLCPotentialUploads.Location = new Point(7, 201);
            btnLCPotentialUploads.Margin = new Padding(4);
            btnLCPotentialUploads.Name = "btnLCPotentialUploads";
            btnLCPotentialUploads.Size = new Size(161, 26);
            btnLCPotentialUploads.TabIndex = 39;
            btnLCPotentialUploads.Text = "View Potential Uploads";
            btnLCPotentialUploads.UseVisualStyleBackColor = true;
            btnLCPotentialUploads.Click += BtnLCPotentialUploads_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Microsoft Sans Serif", 9.75F);
            label21.Location = new Point(546, 14);
            label21.Margin = new Padding(4, 0, 4, 0);
            label21.Name = "label21";
            label21.Size = new Size(382, 16);
            label21.TabIndex = 37;
            label21.Text = "Census Records with valid Reference to upload to Lost Cousins";
            // 
            // rtbLCUpdateData
            // 
            rtbLCUpdateData.BackColor = SystemColors.Window;
            rtbLCUpdateData.BorderStyle = BorderStyle.None;
            rtbLCUpdateData.Font = new Font("Microsoft Sans Serif", 9F);
            rtbLCUpdateData.ForeColor = Color.Red;
            rtbLCUpdateData.Location = new Point(550, 36);
            rtbLCUpdateData.Margin = new Padding(4);
            rtbLCUpdateData.Name = "rtbLCUpdateData";
            rtbLCUpdateData.ReadOnly = true;
            rtbLCUpdateData.Size = new Size(456, 195);
            rtbLCUpdateData.TabIndex = 36;
            rtbLCUpdateData.TabStop = false;
            rtbLCUpdateData.Text = "Please Login to see data to update";
            // 
            // groupBox8
            // 
            groupBox8.Controls.Add(btnLCLogin);
            groupBox8.Controls.Add(label20);
            groupBox8.Controls.Add(labLCUpdatesEmail);
            groupBox8.Controls.Add(txtLCEmail);
            groupBox8.Controls.Add(txtLCPassword);
            groupBox8.Font = new Font("Microsoft Sans Serif", 9.75F);
            groupBox8.Location = new Point(35, 25);
            groupBox8.Margin = new Padding(4);
            groupBox8.Name = "groupBox8";
            groupBox8.Padding = new Padding(4);
            groupBox8.Size = new Size(475, 136);
            groupBox8.TabIndex = 1;
            groupBox8.TabStop = false;
            groupBox8.Text = "Lost Cousins Login Details";
            // 
            // btnLCLogin
            // 
            btnLCLogin.BackColor = Color.Red;
            btnLCLogin.Location = new Point(362, 79);
            btnLCLogin.Margin = new Padding(4);
            btnLCLogin.Name = "btnLCLogin";
            btnLCLogin.Size = new Size(88, 29);
            btnLCLogin.TabIndex = 3;
            btnLCLogin.Text = "Login";
            btnLCLogin.UseVisualStyleBackColor = false;
            btnLCLogin.Click += BtnLCLogin_Click;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(40, 83);
            label20.Margin = new Padding(4, 0, 4, 0);
            label20.Name = "label20";
            label20.Size = new Size(67, 16);
            label20.TabIndex = 3;
            label20.Text = "Password";
            label20.TextAlign = ContentAlignment.TopRight;
            // 
            // labLCUpdatesEmail
            // 
            labLCUpdatesEmail.AutoSize = true;
            labLCUpdatesEmail.Font = new Font("Microsoft Sans Serif", 9.75F);
            labLCUpdatesEmail.Location = new Point(7, 36);
            labLCUpdatesEmail.Margin = new Padding(4, 0, 4, 0);
            labLCUpdatesEmail.Name = "labLCUpdatesEmail";
            labLCUpdatesEmail.Size = new Size(95, 16);
            labLCUpdatesEmail.TabIndex = 2;
            labLCUpdatesEmail.Text = "Email Address";
            labLCUpdatesEmail.TextAlign = ContentAlignment.TopRight;
            // 
            // txtLCEmail
            // 
            txtLCEmail.Location = new Point(126, 32);
            txtLCEmail.Margin = new Padding(4);
            txtLCEmail.Name = "txtLCEmail";
            txtLCEmail.Size = new Size(323, 22);
            txtLCEmail.TabIndex = 1;
            txtLCEmail.TextChanged += TxtLCEmail_TextChanged;
            // 
            // txtLCPassword
            // 
            txtLCPassword.Location = new Point(126, 80);
            txtLCPassword.Margin = new Padding(4);
            txtLCPassword.Name = "txtLCPassword";
            txtLCPassword.PasswordChar = '*';
            txtLCPassword.Size = new Size(228, 22);
            txtLCPassword.TabIndex = 2;
            txtLCPassword.TextChanged += TxtLCPassword_TextChanged;
            // 
            // LCVerifyTab
            // 
            LCVerifyTab.Controls.Add(rtbCheckAncestors);
            LCVerifyTab.Controls.Add(dgCheckAncestors);
            LCVerifyTab.Controls.Add(btnCheckMyAncestors);
            LCVerifyTab.Controls.Add(lblCheckAncestors);
            LCVerifyTab.Location = new Point(4, 24);
            LCVerifyTab.Margin = new Padding(4);
            LCVerifyTab.Name = "LCVerifyTab";
            LCVerifyTab.Size = new Size(1222, 376);
            LCVerifyTab.TabIndex = 2;
            LCVerifyTab.Text = "Verification";
            LCVerifyTab.UseVisualStyleBackColor = true;
            // 
            // rtbCheckAncestors
            // 
            rtbCheckAncestors.BackColor = SystemColors.Window;
            rtbCheckAncestors.BorderStyle = BorderStyle.None;
            rtbCheckAncestors.Font = new Font("Microsoft Sans Serif", 9F);
            rtbCheckAncestors.ForeColor = Color.Red;
            rtbCheckAncestors.Location = new Point(390, 14);
            rtbCheckAncestors.Margin = new Padding(4);
            rtbCheckAncestors.Name = "rtbCheckAncestors";
            rtbCheckAncestors.ReadOnly = true;
            rtbCheckAncestors.Size = new Size(835, 76);
            rtbCheckAncestors.TabIndex = 37;
            rtbCheckAncestors.TabStop = false;
            rtbCheckAncestors.Text = "Please Login to see data to update";
            rtbCheckAncestors.TextChanged += RtbCheckAncestors_TextChanged;
            // 
            // btnCheckMyAncestors
            // 
            btnCheckMyAncestors.BackColor = Color.Red;
            btnCheckMyAncestors.Location = new Point(18, 46);
            btnCheckMyAncestors.Margin = new Padding(4);
            btnCheckMyAncestors.Name = "btnCheckMyAncestors";
            btnCheckMyAncestors.Size = new Size(288, 26);
            btnCheckMyAncestors.TabIndex = 1;
            btnCheckMyAncestors.Text = "Load My Ancestors Page to check for Errors";
            btnCheckMyAncestors.UseVisualStyleBackColor = false;
            btnCheckMyAncestors.Click += BtnCheckMyAncestors_Click;
            // 
            // lblCheckAncestors
            // 
            lblCheckAncestors.AutoSize = true;
            lblCheckAncestors.Font = new Font("Microsoft Sans Serif", 9.75F);
            lblCheckAncestors.Location = new Point(14, 14);
            lblCheckAncestors.Margin = new Padding(4, 0, 4, 0);
            lblCheckAncestors.Name = "lblCheckAncestors";
            lblCheckAncestors.Size = new Size(315, 16);
            lblCheckAncestors.TabIndex = 0;
            lblCheckAncestors.Text = "Not Currently Logged in Use Updates Page to Login";
            // 
            // tabCensus
            // 
            tabCensus.Controls.Add(groupBox2);
            tabCensus.Controls.Add(groupBox9);
            tabCensus.Location = new Point(4, 24);
            tabCensus.Margin = new Padding(4);
            tabCensus.Name = "tabCensus";
            tabCensus.Padding = new Padding(4);
            tabCensus.Size = new Size(1238, 412);
            tabCensus.TabIndex = 0;
            tabCensus.Text = "Census";
            tabCensus.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(btnAliveOnDate);
            groupBox2.Controls.Add(txtAliveDates);
            groupBox2.Controls.Add(label22);
            groupBox2.Controls.Add(chkAnyCensusYear);
            groupBox2.Controls.Add(groupBox10);
            groupBox2.Controls.Add(groupBox4);
            groupBox2.Controls.Add(chkExcludeUnknownBirths);
            groupBox2.Controls.Add(labCensusTabSurname);
            groupBox2.Controls.Add(txtCensusSurname);
            groupBox2.Controls.Add(labCensusExcludeOverAge);
            groupBox2.Controls.Add(udAgeFilter);
            groupBox2.Controls.Add(cenDate);
            groupBox2.Controls.Add(relTypesCensus);
            groupBox2.Location = new Point(9, 7);
            groupBox2.Margin = new Padding(4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4);
            groupBox2.Size = new Size(1124, 335);
            groupBox2.TabIndex = 23;
            groupBox2.TabStop = false;
            groupBox2.Text = "Census Search Reports";
            // 
            // btnAliveOnDate
            // 
            btnAliveOnDate.Location = new Point(750, 143);
            btnAliveOnDate.Margin = new Padding(4);
            btnAliveOnDate.Name = "btnAliveOnDate";
            btnAliveOnDate.Size = new Size(350, 29);
            btnAliveOnDate.TabIndex = 41;
            btnAliveOnDate.Text = "Show Individuals possibly alive on above date(s)";
            btnAliveOnDate.UseVisualStyleBackColor = true;
            btnAliveOnDate.Click += BtnAliveOnDate_Click;
            // 
            // txtAliveDates
            // 
            txtAliveDates.Location = new Point(830, 97);
            txtAliveDates.Margin = new Padding(4);
            txtAliveDates.Name = "txtAliveDates";
            txtAliveDates.Size = new Size(270, 23);
            txtAliveDates.TabIndex = 40;
            txtAliveDates.Text = "Enter valid GEDCOM date/date range";
            txtAliveDates.Enter += TxtAliveDates_Enter;
            txtAliveDates.Validating += TxtAliveDates_Validating;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(747, 100);
            label22.Margin = new Padding(4, 0, 4, 0);
            label22.Name = "label22";
            label22.Size = new Size(71, 15);
            label22.TabIndex = 39;
            label22.Text = "Alive Dates: ";
            // 
            // chkAnyCensusYear
            // 
            chkAnyCensusYear.AutoSize = true;
            chkAnyCensusYear.Checked = true;
            chkAnyCensusYear.CheckState = CheckState.Checked;
            chkAnyCensusYear.Location = new Point(396, 149);
            chkAnyCensusYear.Margin = new Padding(4);
            chkAnyCensusYear.Name = "chkAnyCensusYear";
            chkAnyCensusYear.RightToLeft = RightToLeft.Yes;
            chkAnyCensusYear.Size = new Size(314, 19);
            chkAnyCensusYear.TabIndex = 36;
            chkAnyCensusYear.Text = "Include ALL census years for Census Reference reports ";
            chkAnyCensusYear.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            groupBox10.Controls.Add(btnShowCensusMissing);
            groupBox10.Controls.Add(btnShowCensusEntered);
            groupBox10.Controls.Add(btnRandomSurnameEntered);
            groupBox10.Controls.Add(btnRandomSurnameMissing);
            groupBox10.Location = new Point(11, 181);
            groupBox10.Margin = new Padding(4);
            groupBox10.Name = "groupBox10";
            groupBox10.Padding = new Padding(4);
            groupBox10.Size = new Size(1106, 73);
            groupBox10.TabIndex = 35;
            groupBox10.TabStop = false;
            groupBox10.Text = "Census Record Reports";
            // 
            // btnShowCensusMissing
            // 
            btnShowCensusMissing.Location = new Point(7, 25);
            btnShowCensusMissing.Margin = new Padding(4);
            btnShowCensusMissing.Name = "btnShowCensusMissing";
            btnShowCensusMissing.Size = new Size(175, 29);
            btnShowCensusMissing.TabIndex = 39;
            btnShowCensusMissing.Text = "Show Not Found on Census";
            btnShowCensusMissing.UseVisualStyleBackColor = true;
            btnShowCensusMissing.Click += BtnShowCensus_Click;
            // 
            // btnShowCensusEntered
            // 
            btnShowCensusEntered.Location = new Point(189, 25);
            btnShowCensusEntered.Margin = new Padding(4);
            btnShowCensusEntered.Name = "btnShowCensusEntered";
            btnShowCensusEntered.Size = new Size(180, 29);
            btnShowCensusEntered.TabIndex = 38;
            btnShowCensusEntered.Text = "Show Found on Census";
            btnShowCensusEntered.UseVisualStyleBackColor = true;
            btnShowCensusEntered.Click += BtnShowCensus_Click;
            // 
            // btnRandomSurnameEntered
            // 
            btnRandomSurnameEntered.Location = new Point(376, 25);
            btnRandomSurnameEntered.Margin = new Padding(4);
            btnRandomSurnameEntered.Name = "btnRandomSurnameEntered";
            btnRandomSurnameEntered.Size = new Size(350, 29);
            btnRandomSurnameEntered.TabIndex = 37;
            btnRandomSurnameEntered.Text = "Show Found Random Surname from Direct Ancestors";
            btnRandomSurnameEntered.UseVisualStyleBackColor = true;
            btnRandomSurnameEntered.Click += BtnRandomSurname_Click;
            // 
            // btnRandomSurnameMissing
            // 
            btnRandomSurnameMissing.Location = new Point(740, 25);
            btnRandomSurnameMissing.Margin = new Padding(4);
            btnRandomSurnameMissing.Name = "btnRandomSurnameMissing";
            btnRandomSurnameMissing.Size = new Size(351, 29);
            btnRandomSurnameMissing.TabIndex = 36;
            btnRandomSurnameMissing.Text = "Show Not Found Random Surname from Direct Ancestors";
            btnRandomSurnameMissing.UseVisualStyleBackColor = true;
            btnRandomSurnameMissing.Click += BtnRandomSurname_Click;
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(btnInconsistentLocations);
            groupBox4.Controls.Add(btnUnrecognisedCensusRef);
            groupBox4.Controls.Add(btnIncompleteCensusRef);
            groupBox4.Controls.Add(btnMissingCensusRefs);
            groupBox4.Controls.Add(btnCensusRefs);
            groupBox4.Location = new Point(11, 261);
            groupBox4.Margin = new Padding(4);
            groupBox4.Name = "groupBox4";
            groupBox4.Padding = new Padding(4);
            groupBox4.Size = new Size(1106, 61);
            groupBox4.TabIndex = 34;
            groupBox4.TabStop = false;
            groupBox4.Text = "Census Reference Reports";
            // 
            // btnInconsistentLocations
            // 
            btnInconsistentLocations.Location = new Point(740, 22);
            btnInconsistentLocations.Margin = new Padding(4);
            btnInconsistentLocations.Name = "btnInconsistentLocations";
            btnInconsistentLocations.Size = new Size(351, 29);
            btnInconsistentLocations.TabIndex = 29;
            btnInconsistentLocations.Text = "Inconsistent census locations for families with same census ref";
            btnInconsistentLocations.UseVisualStyleBackColor = true;
            btnInconsistentLocations.Click += BtnInconsistentLocations_Click;
            // 
            // btnUnrecognisedCensusRef
            // 
            btnUnrecognisedCensusRef.Location = new Point(376, 22);
            btnUnrecognisedCensusRef.Margin = new Padding(4);
            btnUnrecognisedCensusRef.Name = "btnUnrecognisedCensusRef";
            btnUnrecognisedCensusRef.Size = new Size(168, 29);
            btnUnrecognisedCensusRef.TabIndex = 8;
            btnUnrecognisedCensusRef.Text = "Unrecognised Census Refs";
            btnUnrecognisedCensusRef.UseVisualStyleBackColor = true;
            btnUnrecognisedCensusRef.Click += BtnUnrecognisedCensusRef_Click;
            // 
            // btnIncompleteCensusRef
            // 
            btnIncompleteCensusRef.Location = new Point(189, 22);
            btnIncompleteCensusRef.Margin = new Padding(4);
            btnIncompleteCensusRef.Name = "btnIncompleteCensusRef";
            btnIncompleteCensusRef.Size = new Size(180, 29);
            btnIncompleteCensusRef.TabIndex = 7;
            btnIncompleteCensusRef.Text = "Incomplete Census Refs";
            btnIncompleteCensusRef.UseVisualStyleBackColor = true;
            btnIncompleteCensusRef.Click += BtnIncompleteCensusRef_Click;
            // 
            // btnMissingCensusRefs
            // 
            btnMissingCensusRefs.Location = new Point(551, 22);
            btnMissingCensusRefs.Margin = new Padding(4);
            btnMissingCensusRefs.Name = "btnMissingCensusRefs";
            btnMissingCensusRefs.Size = new Size(175, 29);
            btnMissingCensusRefs.TabIndex = 6;
            btnMissingCensusRefs.Text = "Missing Census Refs";
            btnMissingCensusRefs.UseVisualStyleBackColor = true;
            btnMissingCensusRefs.Click += BtnMissingCensusRefs_Click;
            // 
            // btnCensusRefs
            // 
            btnCensusRefs.Location = new Point(7, 22);
            btnCensusRefs.Margin = new Padding(4);
            btnCensusRefs.Name = "btnCensusRefs";
            btnCensusRefs.Size = new Size(175, 29);
            btnCensusRefs.TabIndex = 5;
            btnCensusRefs.Text = "Good Census Refs";
            btnCensusRefs.UseVisualStyleBackColor = true;
            btnCensusRefs.Click += BtnCensusRefs_Click;
            // 
            // chkExcludeUnknownBirths
            // 
            chkExcludeUnknownBirths.AutoSize = true;
            chkExcludeUnknownBirths.Location = new Point(397, 73);
            chkExcludeUnknownBirths.Margin = new Padding(4);
            chkExcludeUnknownBirths.Name = "chkExcludeUnknownBirths";
            chkExcludeUnknownBirths.RightToLeft = RightToLeft.Yes;
            chkExcludeUnknownBirths.Size = new Size(265, 19);
            chkExcludeUnknownBirths.TabIndex = 31;
            chkExcludeUnknownBirths.Text = "Exclude Individuals with unknown birth dates";
            chkExcludeUnknownBirths.UseVisualStyleBackColor = true;
            // 
            // labCensusTabSurname
            // 
            labCensusTabSurname.AutoSize = true;
            labCensusTabSurname.Location = new Point(747, 45);
            labCensusTabSurname.Margin = new Padding(4, 0, 4, 0);
            labCensusTabSurname.Name = "labCensusTabSurname";
            labCensusTabSurname.Size = new Size(54, 15);
            labCensusTabSurname.TabIndex = 30;
            labCensusTabSurname.Text = "Surname";
            // 
            // txtCensusSurname
            // 
            txtCensusSurname.Location = new Point(830, 41);
            txtCensusSurname.Margin = new Padding(4);
            txtCensusSurname.Name = "txtCensusSurname";
            txtCensusSurname.Size = new Size(270, 23);
            txtCensusSurname.TabIndex = 29;
            // 
            // labCensusExcludeOverAge
            // 
            labCensusExcludeOverAge.AutoSize = true;
            labCensusExcludeOverAge.Location = new Point(397, 45);
            labCensusExcludeOverAge.Margin = new Padding(4, 0, 4, 0);
            labCensusExcludeOverAge.Name = "labCensusExcludeOverAge";
            labCensusExcludeOverAge.Size = new Size(193, 15);
            labCensusExcludeOverAge.TabIndex = 26;
            labCensusExcludeOverAge.Text = "Exclude individuals over the age of ";
            // 
            // udAgeFilter
            // 
            udAgeFilter.Location = new Point(631, 44);
            udAgeFilter.Margin = new Padding(4);
            udAgeFilter.Maximum = new decimal(new int[] { 110, 0, 0, 0 });
            udAgeFilter.Minimum = new decimal(new int[] { 60, 0, 0, 0 });
            udAgeFilter.Name = "udAgeFilter";
            udAgeFilter.Size = new Size(50, 23);
            udAgeFilter.TabIndex = 25;
            udAgeFilter.Value = new decimal(new int[] { 90, 0, 0, 0 });
            // 
            // cenDate
            // 
            cenDate.AutoSize = true;
            cenDate.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            cenDate.Country = "Scotland";
            cenDate.Location = new Point(15, 140);
            cenDate.Margin = new Padding(6);
            cenDate.Name = "cenDate";
            cenDate.Size = new Size(150, 31);
            cenDate.TabIndex = 28;
            // 
            // relTypesCensus
            // 
            relTypesCensus.Location = new Point(11, 22);
            relTypesCensus.Margin = new Padding(7);
            relTypesCensus.MarriedToDB = true;
            relTypesCensus.Name = "relTypesCensus";
            relTypesCensus.Size = new Size(379, 114);
            relTypesCensus.TabIndex = 27;
            // 
            // groupBox9
            // 
            groupBox9.Controls.Add(groupBox11);
            groupBox9.Controls.Add(groupBox1);
            groupBox9.Controls.Add(groupBox5);
            groupBox9.Controls.Add(groupBox6);
            groupBox9.Location = new Point(9, 369);
            groupBox9.Margin = new Padding(4);
            groupBox9.Name = "groupBox9";
            groupBox9.Padding = new Padding(4);
            groupBox9.Size = new Size(1124, 92);
            groupBox9.TabIndex = 32;
            groupBox9.TabStop = false;
            groupBox9.Text = "Census Reports that don't use filters above";
            // 
            // groupBox11
            // 
            groupBox11.Controls.Add(BtnAutoCreatedCensusFacts);
            groupBox11.Controls.Add(BtnProblemCensusFacts);
            groupBox11.Location = new Point(743, 22);
            groupBox11.Margin = new Padding(4);
            groupBox11.Name = "groupBox11";
            groupBox11.Padding = new Padding(4);
            groupBox11.Size = new Size(373, 68);
            groupBox11.TabIndex = 33;
            groupBox11.TabStop = false;
            groupBox11.Text = "Census Facts";
            // 
            // BtnAutoCreatedCensusFacts
            // 
            BtnAutoCreatedCensusFacts.Location = new Point(191, 24);
            BtnAutoCreatedCensusFacts.Margin = new Padding(4);
            BtnAutoCreatedCensusFacts.Name = "BtnAutoCreatedCensusFacts";
            BtnAutoCreatedCensusFacts.Size = new Size(175, 26);
            BtnAutoCreatedCensusFacts.TabIndex = 39;
            BtnAutoCreatedCensusFacts.Text = "Auto Created Census Facts";
            BtnAutoCreatedCensusFacts.UseVisualStyleBackColor = true;
            BtnAutoCreatedCensusFacts.Click += BtnCensusAutoCreatedFacts_Click;
            // 
            // BtnProblemCensusFacts
            // 
            BtnProblemCensusFacts.Location = new Point(7, 24);
            BtnProblemCensusFacts.Margin = new Padding(4);
            BtnProblemCensusFacts.Name = "BtnProblemCensusFacts";
            BtnProblemCensusFacts.Size = new Size(175, 26);
            BtnProblemCensusFacts.TabIndex = 38;
            BtnProblemCensusFacts.Text = "Problem Census Facts";
            BtnProblemCensusFacts.UseVisualStyleBackColor = true;
            BtnProblemCensusFacts.Click += BtnCensusProblemFacts_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(btnDuplicateCensus);
            groupBox1.Controls.Add(btnMissingCensusLocation);
            groupBox1.Location = new Point(9, 239);
            groupBox1.Margin = new Padding(4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4);
            groupBox1.Size = new Size(379, 101);
            groupBox1.TabIndex = 22;
            groupBox1.TabStop = false;
            groupBox1.Text = "Potential Census Fact Problems";
            // 
            // btnDuplicateCensus
            // 
            btnDuplicateCensus.Location = new Point(193, 22);
            btnDuplicateCensus.Margin = new Padding(4);
            btnDuplicateCensus.Name = "btnDuplicateCensus";
            btnDuplicateCensus.Size = new Size(175, 29);
            btnDuplicateCensus.TabIndex = 6;
            btnDuplicateCensus.Text = "Duplicate Census Facts";
            btnDuplicateCensus.UseVisualStyleBackColor = true;
            btnDuplicateCensus.Click += BtnDuplicateCensus_Click;
            // 
            // btnMissingCensusLocation
            // 
            btnMissingCensusLocation.Location = new Point(11, 22);
            btnMissingCensusLocation.Margin = new Padding(4);
            btnMissingCensusLocation.Name = "btnMissingCensusLocation";
            btnMissingCensusLocation.Size = new Size(175, 29);
            btnMissingCensusLocation.TabIndex = 5;
            btnMissingCensusLocation.Text = "Missing Census Locations";
            btnMissingCensusLocation.UseVisualStyleBackColor = true;
            btnMissingCensusLocation.Click += BtnMissingCensusLocation_Click;
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(btnMismatchedChildrenStatus);
            groupBox5.Controls.Add(btnNoChildrenStatus);
            groupBox5.Location = new Point(7, 22);
            groupBox5.Margin = new Padding(4);
            groupBox5.Name = "groupBox5";
            groupBox5.Padding = new Padding(4);
            groupBox5.Size = new Size(382, 68);
            groupBox5.TabIndex = 32;
            groupBox5.TabStop = false;
            groupBox5.Text = "1911 UK Census";
            // 
            // btnMismatchedChildrenStatus
            // 
            btnMismatchedChildrenStatus.Location = new Point(193, 22);
            btnMismatchedChildrenStatus.Margin = new Padding(4);
            btnMismatchedChildrenStatus.Name = "btnMismatchedChildrenStatus";
            btnMismatchedChildrenStatus.Size = new Size(180, 29);
            btnMismatchedChildrenStatus.TabIndex = 7;
            btnMismatchedChildrenStatus.Text = "Mismatched Children Status";
            btnMismatchedChildrenStatus.UseVisualStyleBackColor = true;
            btnMismatchedChildrenStatus.Click += BtnMismatchedChildrenStatus_Click;
            // 
            // btnNoChildrenStatus
            // 
            btnNoChildrenStatus.Location = new Point(11, 22);
            btnNoChildrenStatus.Margin = new Padding(4);
            btnNoChildrenStatus.Name = "btnNoChildrenStatus";
            btnNoChildrenStatus.Size = new Size(175, 29);
            btnNoChildrenStatus.TabIndex = 6;
            btnNoChildrenStatus.Text = "Missing Children Status";
            btnNoChildrenStatus.UseVisualStyleBackColor = true;
            btnNoChildrenStatus.Click += BtnNoChildrenStatus_Click;
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(btnReportUnrecognised);
            groupBox6.Location = new Point(424, 22);
            groupBox6.Margin = new Padding(4);
            groupBox6.Name = "groupBox6";
            groupBox6.Padding = new Padding(4);
            groupBox6.Size = new Size(285, 68);
            groupBox6.TabIndex = 31;
            groupBox6.TabStop = false;
            groupBox6.Text = "Export Missing/Unrecognised data to File";
            // 
            // btnReportUnrecognised
            // 
            btnReportUnrecognised.Anchor = AnchorStyles.Left;
            btnReportUnrecognised.Location = new Point(7, 22);
            btnReportUnrecognised.Margin = new Padding(4);
            btnReportUnrecognised.Name = "btnReportUnrecognised";
            btnReportUnrecognised.Size = new Size(261, 29);
            btnReportUnrecognised.TabIndex = 30;
            btnReportUnrecognised.Text = "Export Unrecognised/Missing Census Refs";
            btnReportUnrecognised.UseVisualStyleBackColor = true;
            btnReportUnrecognised.Click += BtnReportUnrecognised_Click;
            // 
            // tabLocations
            // 
            tabLocations.Controls.Add(btnOldOSMap);
            tabLocations.Controls.Add(btnModernOSMap);
            tabLocations.Controls.Add(btnShowMap);
            tabLocations.Controls.Add(tabCtrlLocations);
            tabLocations.Location = new Point(4, 24);
            tabLocations.Margin = new Padding(4);
            tabLocations.Name = "tabLocations";
            tabLocations.Padding = new Padding(4);
            tabLocations.Size = new Size(1238, 412);
            tabLocations.TabIndex = 4;
            tabLocations.Text = "Locations";
            tabLocations.UseVisualStyleBackColor = true;
            // 
            // btnOldOSMap
            // 
            btnOldOSMap.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnOldOSMap.Location = new Point(992, 2);
            btnOldOSMap.Margin = new Padding(4);
            btnOldOSMap.Name = "btnOldOSMap";
            btnOldOSMap.Size = new Size(121, 23);
            btnOldOSMap.TabIndex = 3;
            btnOldOSMap.Text = "Show Old OS Map";
            btnOldOSMap.UseVisualStyleBackColor = true;
            btnOldOSMap.Click += BtnOSMap_Click;
            // 
            // btnModernOSMap
            // 
            btnModernOSMap.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnModernOSMap.Location = new Point(846, 2);
            btnModernOSMap.Margin = new Padding(4);
            btnModernOSMap.Name = "btnModernOSMap";
            btnModernOSMap.Size = new Size(146, 23);
            btnModernOSMap.TabIndex = 5;
            btnModernOSMap.Text = "Show Modern OS Map";
            btnModernOSMap.UseVisualStyleBackColor = true;
            btnModernOSMap.Visible = false;
            btnModernOSMap.Click += BtnOSMap_Click;
            // 
            // btnShowMap
            // 
            btnShowMap.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnShowMap.Location = new Point(1110, 2);
            btnShowMap.Margin = new Padding(4);
            btnShowMap.Name = "btnShowMap";
            btnShowMap.Size = new Size(121, 23);
            btnShowMap.TabIndex = 2;
            btnShowMap.Text = "Show Google Map";
            btnShowMap.UseVisualStyleBackColor = true;
            btnShowMap.Click += BtnShowMap_Click;
            // 
            // tabCtrlLocations
            // 
            tabCtrlLocations.Controls.Add(tabTreeView);
            tabCtrlLocations.Controls.Add(tabCountries);
            tabCtrlLocations.Controls.Add(tabRegions);
            tabCtrlLocations.Controls.Add(tabSubRegions);
            tabCtrlLocations.Controls.Add(tabAddresses);
            tabCtrlLocations.Controls.Add(tabPlaces);
            tabCtrlLocations.Dock = DockStyle.Fill;
            tabCtrlLocations.Location = new Point(4, 4);
            tabCtrlLocations.Margin = new Padding(4);
            tabCtrlLocations.Name = "tabCtrlLocations";
            tabCtrlLocations.SelectedIndex = 0;
            tabCtrlLocations.Size = new Size(1230, 404);
            tabCtrlLocations.TabIndex = 0;
            tabCtrlLocations.SelectedIndexChanged += TabCtrlLocations_SelectedIndexChanged;
            tabCtrlLocations.Selecting += TabCtrlLocations_Selecting;
            // 
            // tabTreeView
            // 
            tabTreeView.Controls.Add(treeViewLocations);
            tabTreeView.Location = new Point(4, 24);
            tabTreeView.Margin = new Padding(4);
            tabTreeView.Name = "tabTreeView";
            tabTreeView.Padding = new Padding(4);
            tabTreeView.Size = new Size(1222, 376);
            tabTreeView.TabIndex = 5;
            tabTreeView.Text = "Tree View";
            tabTreeView.UseVisualStyleBackColor = true;
            // 
            // treeViewLocations
            // 
            treeViewLocations.CausesValidation = false;
            treeViewLocations.Dock = DockStyle.Fill;
            treeViewLocations.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            treeViewLocations.ImageIndex = 0;
            treeViewLocations.ImageList = imageList;
            treeViewLocations.Location = new Point(4, 4);
            treeViewLocations.Margin = new Padding(4);
            treeViewLocations.Name = "treeViewLocations";
            treeViewLocations.SelectedImageIndex = 0;
            treeViewLocations.ShowNodeToolTips = true;
            treeViewLocations.Size = new Size(1214, 368);
            treeViewLocations.TabIndex = 0;
            treeViewLocations.BeforeCollapse += TreeViewLocations_BeforeCollapse;
            treeViewLocations.BeforeExpand += TreeViewLocations_BeforeExpand;
            treeViewLocations.AfterSelect += TreeViewLocations_AfterSelect;
            treeViewLocations.NodeMouseClick += TreeViewLocations_NodeMouseClick;
            treeViewLocations.NodeMouseDoubleClick += TreeViewLocations_NodeMouseDoubleClick;
            treeViewLocations.MouseDown += TreeViewLocations_MouseDown;
            // 
            // imageList
            // 
            imageList.ColorDepth = ColorDepth.Depth8Bit;
            imageList.ImageStream = (ImageListStreamer)resources.GetObject("imageList.ImageStream");
            imageList.TransparentColor = Color.Transparent;
            imageList.Images.SetKeyName(0, "QuestionMark.png");
            imageList.Images.SetKeyName(1, "GoogleMatch.png");
            imageList.Images.SetKeyName(2, "GooglePartial.png");
            imageList.Images.SetKeyName(3, "Complete_OK.png");
            imageList.Images.SetKeyName(4, "CriticalError.png");
            imageList.Images.SetKeyName(5, "Flagged.png");
            imageList.Images.SetKeyName(6, "OutOfBounds.png");
            imageList.Images.SetKeyName(7, "Warning.png");
            // 
            // tabCountries
            // 
            tabCountries.Controls.Add(dgCountries);
            tabCountries.Location = new Point(4, 24);
            tabCountries.Margin = new Padding(4);
            tabCountries.Name = "tabCountries";
            tabCountries.Padding = new Padding(4);
            tabCountries.Size = new Size(1222, 376);
            tabCountries.TabIndex = 0;
            tabCountries.Text = "Countries";
            tabCountries.ToolTipText = "Double click on Country name to see list of individuals with that Country.";
            tabCountries.UseVisualStyleBackColor = true;
            // 
            // tabRegions
            // 
            tabRegions.Controls.Add(dgRegions);
            tabRegions.Location = new Point(4, 24);
            tabRegions.Margin = new Padding(4);
            tabRegions.Name = "tabRegions";
            tabRegions.Padding = new Padding(4);
            tabRegions.Size = new Size(1222, 376);
            tabRegions.TabIndex = 1;
            tabRegions.Text = "Regions";
            tabRegions.ToolTipText = "Double click on Region name to see list of individuals with that Region.";
            tabRegions.UseVisualStyleBackColor = true;
            // 
            // tabSubRegions
            // 
            tabSubRegions.Controls.Add(dgSubRegions);
            tabSubRegions.Location = new Point(4, 24);
            tabSubRegions.Margin = new Padding(4);
            tabSubRegions.Name = "tabSubRegions";
            tabSubRegions.Padding = new Padding(4);
            tabSubRegions.Size = new Size(1222, 376);
            tabSubRegions.TabIndex = 2;
            tabSubRegions.Text = "SubRegions";
            tabSubRegions.ToolTipText = "Double click on 'Parish' name to see list of individuals with that parish/area.";
            tabSubRegions.UseVisualStyleBackColor = true;
            // 
            // dgSubRegions
            // 
            dgSubRegions.AllowUserToAddRows = false;
            dgSubRegions.AllowUserToDeleteRows = false;
            dgSubRegions.AllowUserToOrderColumns = true;
            dgSubRegions.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgSubRegions.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgSubRegions.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSubRegions.Dock = DockStyle.Fill;
            dgSubRegions.FilterAndSortEnabled = true;
            dgSubRegions.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgSubRegions.Location = new Point(4, 4);
            dgSubRegions.Margin = new Padding(7);
            dgSubRegions.MultiSelect = false;
            dgSubRegions.Name = "dgSubRegions";
            dgSubRegions.ReadOnly = true;
            dgSubRegions.RightToLeft = RightToLeft.No;
            dgSubRegions.RowHeadersVisible = false;
            dgSubRegions.RowHeadersWidth = 50;
            dgSubRegions.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgSubRegions.Size = new Size(1218, 372);
            dgSubRegions.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgSubRegions.TabIndex = 1;
            dgSubRegions.VirtualMode = true;
            dgSubRegions.CellDoubleClick += DgSubRegions_CellDoubleClick;
            dgSubRegions.CellFormatting += DgSubRegions_CellFormatting;
            // 
            // tabAddresses
            // 
            tabAddresses.Controls.Add(dgAddresses);
            tabAddresses.Location = new Point(4, 24);
            tabAddresses.Margin = new Padding(4);
            tabAddresses.Name = "tabAddresses";
            tabAddresses.Padding = new Padding(4);
            tabAddresses.Size = new Size(1222, 376);
            tabAddresses.TabIndex = 3;
            tabAddresses.Text = "Addresses";
            tabAddresses.ToolTipText = "Double click on Address name to see list of individuals with that Address.";
            tabAddresses.UseVisualStyleBackColor = true;
            // 
            // dgAddresses
            // 
            dgAddresses.AllowUserToAddRows = false;
            dgAddresses.AllowUserToDeleteRows = false;
            dgAddresses.AllowUserToOrderColumns = true;
            dgAddresses.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgAddresses.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgAddresses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgAddresses.Dock = DockStyle.Fill;
            dgAddresses.FilterAndSortEnabled = true;
            dgAddresses.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgAddresses.Location = new Point(4, 4);
            dgAddresses.Margin = new Padding(7);
            dgAddresses.MultiSelect = false;
            dgAddresses.Name = "dgAddresses";
            dgAddresses.ReadOnly = true;
            dgAddresses.RightToLeft = RightToLeft.No;
            dgAddresses.RowHeadersVisible = false;
            dgAddresses.RowHeadersWidth = 50;
            dgAddresses.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgAddresses.Size = new Size(1218, 372);
            dgAddresses.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgAddresses.TabIndex = 1;
            dgAddresses.VirtualMode = true;
            dgAddresses.CellDoubleClick += DgAddresses_CellDoubleClick;
            dgAddresses.CellFormatting += DgAddresses_CellFormatting;
            // 
            // tabPlaces
            // 
            tabPlaces.Controls.Add(dgPlaces);
            tabPlaces.Location = new Point(4, 24);
            tabPlaces.Margin = new Padding(4);
            tabPlaces.Name = "tabPlaces";
            tabPlaces.Padding = new Padding(4);
            tabPlaces.Size = new Size(1222, 376);
            tabPlaces.TabIndex = 4;
            tabPlaces.Text = "Places";
            tabPlaces.ToolTipText = "Double click on Address name to see list of individuals with that Place";
            tabPlaces.UseVisualStyleBackColor = true;
            // 
            // dgPlaces
            // 
            dgPlaces.AllowUserToAddRows = false;
            dgPlaces.AllowUserToDeleteRows = false;
            dgPlaces.AllowUserToOrderColumns = true;
            dgPlaces.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgPlaces.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgPlaces.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgPlaces.Dock = DockStyle.Fill;
            dgPlaces.FilterAndSortEnabled = true;
            dgPlaces.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgPlaces.Location = new Point(4, 4);
            dgPlaces.Margin = new Padding(7);
            dgPlaces.MultiSelect = false;
            dgPlaces.Name = "dgPlaces";
            dgPlaces.ReadOnly = true;
            dgPlaces.RightToLeft = RightToLeft.No;
            dgPlaces.RowHeadersVisible = false;
            dgPlaces.RowHeadersWidth = 50;
            dgPlaces.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgPlaces.Size = new Size(1218, 372);
            dgPlaces.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgPlaces.TabIndex = 2;
            dgPlaces.VirtualMode = true;
            dgPlaces.CellDoubleClick += DgPlaces_CellDoubleClick;
            dgPlaces.CellFormatting += DgPlaces_CellFormatting;
            // 
            // tabDisplayProgress
            // 
            tabDisplayProgress.Controls.Add(splitGedcom);
            tabDisplayProgress.Location = new Point(4, 24);
            tabDisplayProgress.Margin = new Padding(4);
            tabDisplayProgress.Name = "tabDisplayProgress";
            tabDisplayProgress.Padding = new Padding(4);
            tabDisplayProgress.Size = new Size(1238, 412);
            tabDisplayProgress.TabIndex = 1;
            tabDisplayProgress.Text = "Gedcom Stats";
            tabDisplayProgress.UseVisualStyleBackColor = true;
            // 
            // splitGedcom
            // 
            splitGedcom.Dock = DockStyle.Fill;
            splitGedcom.FixedPanel = FixedPanel.Panel1;
            splitGedcom.Location = new Point(4, 4);
            splitGedcom.Margin = new Padding(4);
            splitGedcom.Name = "splitGedcom";
            splitGedcom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGedcom.Panel1
            // 
            splitGedcom.Panel1.Controls.Add(panel2);
            splitGedcom.Panel1MinSize = 110;
            // 
            // splitGedcom.Panel2
            // 
            splitGedcom.Panel2.Controls.Add(rtbOutput);
            splitGedcom.Size = new Size(1230, 404);
            splitGedcom.SplitterDistance = 110;
            splitGedcom.SplitterWidth = 5;
            splitGedcom.TabIndex = 15;
            // 
            // panel2
            // 
            panel2.Controls.Add(LbProgramName);
            panel2.Controls.Add(pictureBox1);
            panel2.Controls.Add(labRelationships);
            panel2.Controls.Add(pbRelationships);
            panel2.Controls.Add(labFamilies);
            panel2.Controls.Add(pbFamilies);
            panel2.Controls.Add(labIndividuals);
            panel2.Controls.Add(pbIndividuals);
            panel2.Controls.Add(labSources);
            panel2.Controls.Add(pbSources);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 0);
            panel2.Margin = new Padding(4);
            panel2.Name = "panel2";
            panel2.Size = new Size(1230, 110);
            panel2.TabIndex = 14;
            // 
            // LbProgramName
            // 
            LbProgramName.AutoSize = true;
            LbProgramName.Font = new Font("Kunstler Script", 52F, FontStyle.Bold);
            LbProgramName.Location = new Point(485, 22);
            LbProgramName.Margin = new Padding(4, 0, 4, 0);
            LbProgramName.Name = "LbProgramName";
            LbProgramName.Size = new Size(521, 76);
            LbProgramName.TabIndex = 17;
            LbProgramName.Text = "Family Tree Analyzer";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources._256;
            pictureBox1.Location = new Point(1100, 4);
            pictureBox1.Margin = new Padding(4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(117, 106);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 19;
            pictureBox1.TabStop = false;
            // 
            // labRelationships
            // 
            labRelationships.AutoSize = true;
            labRelationships.Location = new Point(4, 91);
            labRelationships.Margin = new Padding(4, 0, 4, 0);
            labRelationships.Name = "labRelationships";
            labRelationships.Size = new Size(144, 15);
            labRelationships.TabIndex = 16;
            labRelationships.Text = "Relationships && Locations";
            // 
            // pbRelationships
            // 
            pbRelationships.Location = new Point(156, 88);
            pbRelationships.Margin = new Padding(4);
            pbRelationships.Name = "pbRelationships";
            pbRelationships.Size = new Size(321, 19);
            pbRelationships.TabIndex = 15;
            // 
            // labFamilies
            // 
            labFamilies.AutoSize = true;
            labFamilies.Location = new Point(4, 66);
            labFamilies.Margin = new Padding(4, 0, 4, 0);
            labFamilies.Name = "labFamilies";
            labFamilies.Size = new Size(96, 15);
            labFamilies.TabIndex = 14;
            labFamilies.Text = "Loading Families";
            // 
            // pbFamilies
            // 
            pbFamilies.Location = new Point(156, 62);
            pbFamilies.Margin = new Padding(4);
            pbFamilies.Name = "pbFamilies";
            pbFamilies.Size = new Size(321, 19);
            pbFamilies.TabIndex = 13;
            // 
            // labIndividuals
            // 
            labIndividuals.AutoSize = true;
            labIndividuals.Location = new Point(4, 40);
            labIndividuals.Margin = new Padding(4, 0, 4, 0);
            labIndividuals.Name = "labIndividuals";
            labIndividuals.Size = new Size(110, 15);
            labIndividuals.TabIndex = 12;
            labIndividuals.Text = "Loading Individuals";
            // 
            // pbIndividuals
            // 
            pbIndividuals.Location = new Point(156, 37);
            pbIndividuals.Margin = new Padding(4);
            pbIndividuals.Name = "pbIndividuals";
            pbIndividuals.Size = new Size(321, 19);
            pbIndividuals.TabIndex = 11;
            // 
            // labSources
            // 
            labSources.AutoSize = true;
            labSources.Location = new Point(4, 15);
            labSources.Margin = new Padding(4, 0, 4, 0);
            labSources.Name = "labSources";
            labSources.Size = new Size(94, 15);
            labSources.TabIndex = 10;
            labSources.Text = "Loading Sources";
            // 
            // pbSources
            // 
            pbSources.Location = new Point(156, 11);
            pbSources.Margin = new Padding(4);
            pbSources.Name = "pbSources";
            pbSources.Size = new Size(321, 19);
            pbSources.TabIndex = 9;
            // 
            // rtbOutput
            // 
            rtbOutput.Dock = DockStyle.Fill;
            rtbOutput.Font = new Font("Microsoft Sans Serif", 9F);
            rtbOutput.Location = new Point(0, 0);
            rtbOutput.Margin = new Padding(4);
            rtbOutput.Name = "rtbOutput";
            rtbOutput.ReadOnly = true;
            rtbOutput.Size = new Size(1230, 289);
            rtbOutput.TabIndex = 14;
            rtbOutput.Text = "";
            rtbOutput.TextChanged += RtbOutput_TextChanged;
            // 
            // tabSelector
            // 
            tabSelector.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabSelector.Controls.Add(tabDisplayProgress);
            tabSelector.Controls.Add(tabMainLists);
            tabSelector.Controls.Add(tabErrorsFixes);
            tabSelector.Controls.Add(tabSurnames);
            tabSelector.Controls.Add(tabLocations);
            tabSelector.Controls.Add(tabFacts);
            tabSelector.Controls.Add(tabCensus);
            tabSelector.Controls.Add(tabLostCousins);
            tabSelector.Controls.Add(tabColourReports);
            tabSelector.Controls.Add(tabTreetops);
            tabSelector.Controls.Add(tabWorldWars);
            tabSelector.Controls.Add(tabToday);
            tabSelector.Location = new Point(0, 31);
            tabSelector.Margin = new Padding(4);
            tabSelector.Name = "tabSelector";
            tabSelector.SelectedIndex = 0;
            tabSelector.Size = new Size(1246, 440);
            tabSelector.TabIndex = 9;
            tabSelector.SelectedIndexChanged += TabControl_SelectedIndexChanged;
            // 
            // tabMainLists
            // 
            tabMainLists.Controls.Add(tabMainListsSelector);
            tabMainLists.Location = new Point(4, 24);
            tabMainLists.Margin = new Padding(4);
            tabMainLists.Name = "tabMainLists";
            tabMainLists.Padding = new Padding(4);
            tabMainLists.Size = new Size(1238, 412);
            tabMainLists.TabIndex = 18;
            tabMainLists.Text = "Main Lists";
            tabMainLists.UseVisualStyleBackColor = true;
            // 
            // tabMainListsSelector
            // 
            tabMainListsSelector.Controls.Add(tabIndividuals);
            tabMainListsSelector.Controls.Add(tabFamilies);
            tabMainListsSelector.Controls.Add(tabSources);
            tabMainListsSelector.Controls.Add(tabOccupations);
            tabMainListsSelector.Controls.Add(tabCustomFacts);
            tabMainListsSelector.Dock = DockStyle.Fill;
            tabMainListsSelector.Location = new Point(4, 4);
            tabMainListsSelector.Margin = new Padding(4);
            tabMainListsSelector.Name = "tabMainListsSelector";
            tabMainListsSelector.SelectedIndex = 0;
            tabMainListsSelector.Size = new Size(1230, 404);
            tabMainListsSelector.TabIndex = 0;
            tabMainListsSelector.SelectedIndexChanged += TabMainListSelector_SelectedIndexChanged;
            // 
            // tabIndividuals
            // 
            tabIndividuals.Controls.Add(dgIndividuals);
            tabIndividuals.Location = new Point(4, 24);
            tabIndividuals.Margin = new Padding(4);
            tabIndividuals.Name = "tabIndividuals";
            tabIndividuals.Padding = new Padding(4);
            tabIndividuals.Size = new Size(1222, 376);
            tabIndividuals.TabIndex = 0;
            tabIndividuals.Text = "Individuals";
            tabIndividuals.UseVisualStyleBackColor = true;
            // 
            // dgIndividuals
            // 
            dgIndividuals.AllowUserToAddRows = false;
            dgIndividuals.AllowUserToDeleteRows = false;
            dgIndividuals.AllowUserToOrderColumns = true;
            dgIndividuals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgIndividuals.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgIndividuals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgIndividuals.Dock = DockStyle.Fill;
            dgIndividuals.FilterAndSortEnabled = true;
            dgIndividuals.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgIndividuals.Location = new Point(4, 4);
            dgIndividuals.Margin = new Padding(7);
            dgIndividuals.MultiSelect = false;
            dgIndividuals.Name = "dgIndividuals";
            dgIndividuals.ReadOnly = true;
            dgIndividuals.RightToLeft = RightToLeft.No;
            dgIndividuals.RowHeadersVisible = false;
            dgIndividuals.RowHeadersWidth = 50;
            dgIndividuals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgIndividuals.Size = new Size(1218, 372);
            dgIndividuals.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgIndividuals.TabIndex = 1;
            dgIndividuals.VirtualMode = true;
            dgIndividuals.CellDoubleClick += DgIndividuals_CellDoubleClick;
            dgIndividuals.MouseDown += DgIndividuals_MouseDown;
            // 
            // tabFamilies
            // 
            tabFamilies.Controls.Add(dgFamilies);
            tabFamilies.Location = new Point(4, 24);
            tabFamilies.Margin = new Padding(4);
            tabFamilies.Name = "tabFamilies";
            tabFamilies.Padding = new Padding(4);
            tabFamilies.Size = new Size(1222, 376);
            tabFamilies.TabIndex = 1;
            tabFamilies.Text = "Families";
            tabFamilies.UseVisualStyleBackColor = true;
            // 
            // dgFamilies
            // 
            dgFamilies.AllowUserToAddRows = false;
            dgFamilies.AllowUserToDeleteRows = false;
            dgFamilies.AllowUserToOrderColumns = true;
            dgFamilies.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgFamilies.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgFamilies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgFamilies.Dock = DockStyle.Fill;
            dgFamilies.FilterAndSortEnabled = true;
            dgFamilies.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgFamilies.Location = new Point(4, 4);
            dgFamilies.Margin = new Padding(7);
            dgFamilies.MultiSelect = false;
            dgFamilies.Name = "dgFamilies";
            dgFamilies.ReadOnly = true;
            dgFamilies.RightToLeft = RightToLeft.No;
            dgFamilies.RowHeadersVisible = false;
            dgFamilies.RowHeadersWidth = 50;
            dgFamilies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgFamilies.Size = new Size(1218, 372);
            dgFamilies.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgFamilies.TabIndex = 2;
            dgFamilies.VirtualMode = true;
            dgFamilies.CellDoubleClick += DgFamilies_CellDoubleClick;
            // 
            // tabSources
            // 
            tabSources.Controls.Add(dgSources);
            tabSources.Location = new Point(4, 24);
            tabSources.Margin = new Padding(4);
            tabSources.Name = "tabSources";
            tabSources.Size = new Size(1222, 376);
            tabSources.TabIndex = 2;
            tabSources.Text = "Sources";
            tabSources.UseVisualStyleBackColor = true;
            // 
            // dgSources
            // 
            dgSources.AllowUserToAddRows = false;
            dgSources.AllowUserToDeleteRows = false;
            dgSources.AllowUserToOrderColumns = true;
            dgSources.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgSources.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgSources.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSources.Dock = DockStyle.Fill;
            dgSources.FilterAndSortEnabled = true;
            dgSources.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgSources.Location = new Point(0, 0);
            dgSources.Margin = new Padding(7);
            dgSources.MultiSelect = false;
            dgSources.Name = "dgSources";
            dgSources.ReadOnly = true;
            dgSources.RightToLeft = RightToLeft.No;
            dgSources.RowHeadersVisible = false;
            dgSources.RowHeadersWidth = 50;
            dgSources.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgSources.Size = new Size(1222, 376);
            dgSources.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgSources.TabIndex = 2;
            dgSources.VirtualMode = true;
            dgSources.CellDoubleClick += DgSources_CellDoubleClick;
            // 
            // tabOccupations
            // 
            tabOccupations.Controls.Add(dgOccupations);
            tabOccupations.Location = new Point(4, 24);
            tabOccupations.Margin = new Padding(4);
            tabOccupations.Name = "tabOccupations";
            tabOccupations.Size = new Size(1222, 376);
            tabOccupations.TabIndex = 3;
            tabOccupations.Text = "Occupations";
            tabOccupations.UseVisualStyleBackColor = true;
            // 
            // dgOccupations
            // 
            dgOccupations.AllowUserToAddRows = false;
            dgOccupations.AllowUserToDeleteRows = false;
            dgOccupations.AllowUserToOrderColumns = true;
            dgOccupations.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgOccupations.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgOccupations.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgOccupations.Dock = DockStyle.Fill;
            dgOccupations.FilterAndSortEnabled = true;
            dgOccupations.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgOccupations.Location = new Point(0, 0);
            dgOccupations.Margin = new Padding(7);
            dgOccupations.MultiSelect = false;
            dgOccupations.Name = "dgOccupations";
            dgOccupations.ReadOnly = true;
            dgOccupations.RightToLeft = RightToLeft.No;
            dgOccupations.RowHeadersVisible = false;
            dgOccupations.RowHeadersWidth = 50;
            dgOccupations.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgOccupations.Size = new Size(1222, 376);
            dgOccupations.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgOccupations.TabIndex = 3;
            dgOccupations.VirtualMode = true;
            dgOccupations.CellDoubleClick += DgOccupations_CellDoubleClick;
            // 
            // tabCustomFacts
            // 
            tabCustomFacts.Controls.Add(dgCustomFacts);
            tabCustomFacts.Location = new Point(4, 24);
            tabCustomFacts.Margin = new Padding(4);
            tabCustomFacts.Name = "tabCustomFacts";
            tabCustomFacts.Size = new Size(1222, 376);
            tabCustomFacts.TabIndex = 4;
            tabCustomFacts.Text = "Custom Facts";
            tabCustomFacts.UseVisualStyleBackColor = true;
            // 
            // dgCustomFacts
            // 
            dgCustomFacts.AllowUserToAddRows = false;
            dgCustomFacts.AllowUserToDeleteRows = false;
            dgCustomFacts.AllowUserToOrderColumns = true;
            dgCustomFacts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgCustomFacts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgCustomFacts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgCustomFacts.Dock = DockStyle.Fill;
            dgCustomFacts.FilterAndSortEnabled = true;
            dgCustomFacts.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgCustomFacts.Location = new Point(0, 0);
            dgCustomFacts.Margin = new Padding(7);
            dgCustomFacts.MultiSelect = false;
            dgCustomFacts.Name = "dgCustomFacts";
            dgCustomFacts.ReadOnly = true;
            dgCustomFacts.RightToLeft = RightToLeft.No;
            dgCustomFacts.RowHeadersVisible = false;
            dgCustomFacts.RowHeadersWidth = 50;
            dgCustomFacts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgCustomFacts.Size = new Size(1222, 376);
            dgCustomFacts.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgCustomFacts.TabIndex = 4;
            dgCustomFacts.VirtualMode = true;
            dgCustomFacts.CellDoubleClick += DgCustomFacts_CellDoubleClick;
            dgCustomFacts.CellValueChanged += DgCustomFacts_CellValueChanged;
            // 
            // tabErrorsFixes
            // 
            tabErrorsFixes.Controls.Add(tabErrorFixSelector);
            tabErrorsFixes.Location = new Point(4, 24);
            tabErrorsFixes.Margin = new Padding(4);
            tabErrorsFixes.Name = "tabErrorsFixes";
            tabErrorsFixes.Size = new Size(1238, 412);
            tabErrorsFixes.TabIndex = 19;
            tabErrorsFixes.Text = "Errors/Fixes";
            tabErrorsFixes.UseVisualStyleBackColor = true;
            // 
            // tabErrorFixSelector
            // 
            tabErrorFixSelector.Controls.Add(tabDataErrors);
            tabErrorFixSelector.Controls.Add(tabDuplicates);
            tabErrorFixSelector.Controls.Add(tabLooseBirths);
            tabErrorFixSelector.Controls.Add(tabLooseDeaths);
            tabErrorFixSelector.Controls.Add(tabLooseInfo);
            tabErrorFixSelector.Dock = DockStyle.Fill;
            tabErrorFixSelector.Location = new Point(0, 0);
            tabErrorFixSelector.Margin = new Padding(4);
            tabErrorFixSelector.Name = "tabErrorFixSelector";
            tabErrorFixSelector.SelectedIndex = 0;
            tabErrorFixSelector.ShowToolTips = true;
            tabErrorFixSelector.Size = new Size(1238, 412);
            tabErrorFixSelector.TabIndex = 0;
            tabErrorFixSelector.SelectedIndexChanged += TabErrorFixSelector_SelectedIndexChanged;
            // 
            // tabDataErrors
            // 
            tabDataErrors.Controls.Add(dgDataErrors);
            tabDataErrors.Controls.Add(gbDataErrorTypes);
            tabDataErrors.Location = new Point(4, 24);
            tabDataErrors.Margin = new Padding(4);
            tabDataErrors.Name = "tabDataErrors";
            tabDataErrors.Padding = new Padding(4);
            tabDataErrors.Size = new Size(1230, 384);
            tabDataErrors.TabIndex = 0;
            tabDataErrors.Text = "Data Errors";
            tabDataErrors.UseVisualStyleBackColor = true;
            // 
            // gbDataErrorTypes
            // 
            gbDataErrorTypes.Controls.Add(ckbDataErrors);
            gbDataErrorTypes.Controls.Add(btnSelectAll);
            gbDataErrorTypes.Controls.Add(btnClearAll);
            gbDataErrorTypes.Dock = DockStyle.Top;
            gbDataErrorTypes.Location = new Point(4, 4);
            gbDataErrorTypes.Margin = new Padding(4);
            gbDataErrorTypes.Name = "gbDataErrorTypes";
            gbDataErrorTypes.Padding = new Padding(4);
            gbDataErrorTypes.Size = new Size(1222, 185);
            gbDataErrorTypes.TabIndex = 1;
            gbDataErrorTypes.TabStop = false;
            gbDataErrorTypes.Text = "Types of Data Error to display";
            // 
            // ckbDataErrors
            // 
            ckbDataErrors.CheckOnClick = true;
            ckbDataErrors.ColumnWidth = 300;
            ckbDataErrors.Dock = DockStyle.Top;
            ckbDataErrors.FormattingEnabled = true;
            ckbDataErrors.Location = new Point(4, 20);
            ckbDataErrors.Margin = new Padding(4);
            ckbDataErrors.MultiColumn = true;
            ckbDataErrors.Name = "ckbDataErrors";
            ckbDataErrors.ScrollAlwaysVisible = true;
            ckbDataErrors.Size = new Size(1214, 112);
            ckbDataErrors.TabIndex = 8;
            ckbDataErrors.SelectedIndexChanged += CkbDataErrors_SelectedIndexChanged;
            // 
            // btnSelectAll
            // 
            btnSelectAll.Location = new Point(9, 155);
            btnSelectAll.Margin = new Padding(4);
            btnSelectAll.Name = "btnSelectAll";
            btnSelectAll.Size = new Size(88, 26);
            btnSelectAll.TabIndex = 7;
            btnSelectAll.Text = "Select All";
            btnSelectAll.UseVisualStyleBackColor = true;
            btnSelectAll.Click += BtnSelectAll_Click;
            // 
            // btnClearAll
            // 
            btnClearAll.Location = new Point(104, 155);
            btnClearAll.Margin = new Padding(4);
            btnClearAll.Name = "btnClearAll";
            btnClearAll.Size = new Size(88, 26);
            btnClearAll.TabIndex = 6;
            btnClearAll.Text = "Clear All";
            btnClearAll.UseVisualStyleBackColor = true;
            btnClearAll.Click += BtnClearAll_Click;
            // 
            // tabDuplicates
            // 
            tabDuplicates.Controls.Add(labDuplicateSlider);
            tabDuplicates.Controls.Add(labCompletion);
            tabDuplicates.Controls.Add(chkIgnoreUnnamedTwins);
            tabDuplicates.Controls.Add(ckbHideIgnoredDuplicates);
            tabDuplicates.Controls.Add(labErrorTabCandidateDupTitle);
            tabDuplicates.Controls.Add(labErrorTabAggrMatch);
            tabDuplicates.Controls.Add(labErrorTabLooseMatch);
            tabDuplicates.Controls.Add(tbDuplicateScore);
            tabDuplicates.Controls.Add(labCalcDuplicates);
            tabDuplicates.Controls.Add(pbDuplicates);
            tabDuplicates.Controls.Add(btnCancelDuplicates);
            tabDuplicates.Controls.Add(dgDuplicates);
            tabDuplicates.Location = new Point(4, 24);
            tabDuplicates.Margin = new Padding(4);
            tabDuplicates.Name = "tabDuplicates";
            tabDuplicates.Padding = new Padding(4);
            tabDuplicates.Size = new Size(1230, 384);
            tabDuplicates.TabIndex = 1;
            tabDuplicates.Text = "Duplicates?";
            tabDuplicates.UseVisualStyleBackColor = true;
            // 
            // labDuplicateSlider
            // 
            labDuplicateSlider.Anchor = AnchorStyles.Top;
            labDuplicateSlider.AutoSize = true;
            labDuplicateSlider.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold);
            labDuplicateSlider.Location = new Point(807, 28);
            labDuplicateSlider.Margin = new Padding(4, 0, 4, 0);
            labDuplicateSlider.Name = "labDuplicateSlider";
            labDuplicateSlider.Size = new Size(104, 13);
            labDuplicateSlider.TabIndex = 32;
            labDuplicateSlider.Text = "Match Quality : 1";
            // 
            // labCompletion
            // 
            labCompletion.AutoSize = true;
            labCompletion.Location = new Point(142, 40);
            labCompletion.Margin = new Padding(4, 0, 4, 0);
            labCompletion.Name = "labCompletion";
            labCompletion.Size = new Size(0, 15);
            labCompletion.TabIndex = 30;
            // 
            // ckbHideIgnoredDuplicates
            // 
            ckbHideIgnoredDuplicates.AutoSize = true;
            ckbHideIgnoredDuplicates.Checked = true;
            ckbHideIgnoredDuplicates.CheckState = CheckState.Checked;
            ckbHideIgnoredDuplicates.Location = new Point(14, 74);
            ckbHideIgnoredDuplicates.Margin = new Padding(4);
            ckbHideIgnoredDuplicates.Name = "ckbHideIgnoredDuplicates";
            ckbHideIgnoredDuplicates.Size = new Size(249, 19);
            ckbHideIgnoredDuplicates.TabIndex = 28;
            ckbHideIgnoredDuplicates.Text = "Hide Possible Duplicates marked as Ignore";
            ckbHideIgnoredDuplicates.UseVisualStyleBackColor = true;
            ckbHideIgnoredDuplicates.CheckedChanged += CkbHideIgnoredDuplicates_CheckedChanged;
            // 
            // labErrorTabCandidateDupTitle
            // 
            labErrorTabCandidateDupTitle.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            labErrorTabCandidateDupTitle.AutoSize = true;
            labErrorTabCandidateDupTitle.Font = new Font("Microsoft Sans Serif", 11.25F, FontStyle.Bold);
            labErrorTabCandidateDupTitle.Location = new Point(480, 73);
            labErrorTabCandidateDupTitle.Margin = new Padding(4, 0, 4, 0);
            labErrorTabCandidateDupTitle.Name = "labErrorTabCandidateDupTitle";
            labErrorTabCandidateDupTitle.Size = new Size(199, 18);
            labErrorTabCandidateDupTitle.TabIndex = 26;
            labErrorTabCandidateDupTitle.Text = "Candidate Duplicates List";
            // 
            // labCalcDuplicates
            // 
            labCalcDuplicates.AutoSize = true;
            labCalcDuplicates.Location = new Point(8, 11);
            labCalcDuplicates.Margin = new Padding(4, 0, 4, 0);
            labCalcDuplicates.Name = "labCalcDuplicates";
            labCalcDuplicates.Size = new Size(125, 15);
            labCalcDuplicates.TabIndex = 21;
            labCalcDuplicates.Text = "Calculating Duplicates";
            // 
            // pbDuplicates
            // 
            pbDuplicates.Location = new Point(146, 7);
            pbDuplicates.Margin = new Padding(4);
            pbDuplicates.Name = "pbDuplicates";
            pbDuplicates.Size = new Size(330, 26);
            pbDuplicates.TabIndex = 20;
            // 
            // btnCancelDuplicates
            // 
            btnCancelDuplicates.Image = (Image)resources.GetObject("btnCancelDuplicates.Image");
            btnCancelDuplicates.Location = new Point(483, 7);
            btnCancelDuplicates.Margin = new Padding(4);
            btnCancelDuplicates.Name = "btnCancelDuplicates";
            btnCancelDuplicates.Size = new Size(27, 26);
            btnCancelDuplicates.TabIndex = 27;
            btnCancelDuplicates.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnCancelDuplicates.UseVisualStyleBackColor = true;
            btnCancelDuplicates.Visible = false;
            btnCancelDuplicates.Click += BtnCancelDuplicates_Click;
            // 
            // dgDuplicates
            // 
            dgDuplicates.AllowUserToAddRows = false;
            dgDuplicates.AllowUserToDeleteRows = false;
            dgDuplicates.AllowUserToOrderColumns = true;
            dgDuplicates.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgDuplicates.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgDuplicates.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgDuplicates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgDuplicates.FilterAndSortEnabled = true;
            dgDuplicates.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgDuplicates.Location = new Point(-1, 100);
            dgDuplicates.Margin = new Padding(7);
            dgDuplicates.MultiSelect = false;
            dgDuplicates.Name = "dgDuplicates";
            dgDuplicates.ReadOnly = true;
            dgDuplicates.RightToLeft = RightToLeft.No;
            dgDuplicates.RowHeadersVisible = false;
            dgDuplicates.RowHeadersWidth = 50;
            dgDuplicates.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgDuplicates.Size = new Size(1231, 284);
            dgDuplicates.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgDuplicates.TabIndex = 19;
            dgDuplicates.VirtualMode = true;
            dgDuplicates.CellContentClick += DgDuplicates_CellContentClick;
            dgDuplicates.CellDoubleClick += DgDuplicates_CellDoubleClick;
            // 
            // tabLooseBirths
            // 
            tabLooseBirths.Controls.Add(dgLooseBirths);
            tabLooseBirths.Location = new Point(4, 24);
            tabLooseBirths.Margin = new Padding(4);
            tabLooseBirths.Name = "tabLooseBirths";
            tabLooseBirths.Size = new Size(1230, 384);
            tabLooseBirths.TabIndex = 2;
            tabLooseBirths.Text = "Loose Births";
            tabLooseBirths.UseVisualStyleBackColor = true;
            // 
            // dgLooseBirths
            // 
            dgLooseBirths.AllowUserToAddRows = false;
            dgLooseBirths.AllowUserToDeleteRows = false;
            dgLooseBirths.AllowUserToOrderColumns = true;
            dgLooseBirths.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgLooseBirths.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgLooseBirths.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgLooseBirths.Dock = DockStyle.Fill;
            dgLooseBirths.FilterAndSortEnabled = true;
            dgLooseBirths.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgLooseBirths.Location = new Point(0, 0);
            dgLooseBirths.Margin = new Padding(7);
            dgLooseBirths.MultiSelect = false;
            dgLooseBirths.Name = "dgLooseBirths";
            dgLooseBirths.ReadOnly = true;
            dgLooseBirths.RightToLeft = RightToLeft.No;
            dgLooseBirths.RowHeadersVisible = false;
            dgLooseBirths.RowHeadersWidth = 50;
            dgLooseBirths.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgLooseBirths.Size = new Size(1230, 384);
            dgLooseBirths.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgLooseBirths.TabIndex = 3;
            dgLooseBirths.VirtualMode = true;
            dgLooseBirths.CellDoubleClick += DgLooseBirths_CellDoubleClick;
            // 
            // tabLooseDeaths
            // 
            tabLooseDeaths.Controls.Add(dgLooseDeaths);
            tabLooseDeaths.Location = new Point(4, 24);
            tabLooseDeaths.Margin = new Padding(4);
            tabLooseDeaths.Name = "tabLooseDeaths";
            tabLooseDeaths.Size = new Size(1230, 384);
            tabLooseDeaths.TabIndex = 3;
            tabLooseDeaths.Text = "Loose Deaths";
            tabLooseDeaths.UseVisualStyleBackColor = true;
            // 
            // dgLooseDeaths
            // 
            dgLooseDeaths.AllowUserToAddRows = false;
            dgLooseDeaths.AllowUserToDeleteRows = false;
            dgLooseDeaths.AllowUserToOrderColumns = true;
            dgLooseDeaths.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgLooseDeaths.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgLooseDeaths.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgLooseDeaths.Dock = DockStyle.Fill;
            dgLooseDeaths.FilterAndSortEnabled = true;
            dgLooseDeaths.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgLooseDeaths.Location = new Point(0, 0);
            dgLooseDeaths.Margin = new Padding(7);
            dgLooseDeaths.MultiSelect = false;
            dgLooseDeaths.Name = "dgLooseDeaths";
            dgLooseDeaths.ReadOnly = true;
            dgLooseDeaths.RightToLeft = RightToLeft.No;
            dgLooseDeaths.RowHeadersVisible = false;
            dgLooseDeaths.RowHeadersWidth = 50;
            dgLooseDeaths.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgLooseDeaths.Size = new Size(1230, 384);
            dgLooseDeaths.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgLooseDeaths.TabIndex = 2;
            dgLooseDeaths.VirtualMode = true;
            dgLooseDeaths.CellDoubleClick += DgLooseDeaths_CellDoubleClick;
            // 
            // tabLooseInfo
            // 
            tabLooseInfo.Controls.Add(dgLooseInfo);
            tabLooseInfo.Location = new Point(4, 24);
            tabLooseInfo.Margin = new Padding(4);
            tabLooseInfo.Name = "tabLooseInfo";
            tabLooseInfo.Size = new Size(1230, 384);
            tabLooseInfo.TabIndex = 4;
            tabLooseInfo.Text = "All Loose Info";
            tabLooseInfo.UseVisualStyleBackColor = true;
            // 
            // dgLooseInfo
            // 
            dgLooseInfo.AllowUserToAddRows = false;
            dgLooseInfo.AllowUserToDeleteRows = false;
            dgLooseInfo.AllowUserToOrderColumns = true;
            dgLooseInfo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgLooseInfo.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgLooseInfo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgLooseInfo.Dock = DockStyle.Fill;
            dgLooseInfo.FilterAndSortEnabled = true;
            dgLooseInfo.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgLooseInfo.Location = new Point(0, 0);
            dgLooseInfo.Margin = new Padding(7);
            dgLooseInfo.MultiSelect = false;
            dgLooseInfo.Name = "dgLooseInfo";
            dgLooseInfo.ReadOnly = true;
            dgLooseInfo.RightToLeft = RightToLeft.No;
            dgLooseInfo.RowHeadersVisible = false;
            dgLooseInfo.RowHeadersWidth = 50;
            dgLooseInfo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgLooseInfo.Size = new Size(1230, 384);
            dgLooseInfo.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgLooseInfo.TabIndex = 4;
            dgLooseInfo.VirtualMode = true;
            dgLooseInfo.CellDoubleClick += DgLooseInfo_CellDoubleClick;
            // 
            // tabSurnames
            // 
            tabSurnames.Controls.Add(chkSurnamesIgnoreCase);
            tabSurnames.Controls.Add(btnShowSurnames);
            tabSurnames.Controls.Add(dgSurnames);
            tabSurnames.Controls.Add(reltypesSurnames);
            tabSurnames.Location = new Point(4, 24);
            tabSurnames.Margin = new Padding(4);
            tabSurnames.Name = "tabSurnames";
            tabSurnames.Padding = new Padding(4);
            tabSurnames.Size = new Size(1238, 412);
            tabSurnames.TabIndex = 14;
            tabSurnames.Text = "Surnames";
            tabSurnames.UseVisualStyleBackColor = true;
            // 
            // chkSurnamesIgnoreCase
            // 
            chkSurnamesIgnoreCase.AutoSize = true;
            chkSurnamesIgnoreCase.Checked = true;
            chkSurnamesIgnoreCase.CheckState = CheckState.Checked;
            chkSurnamesIgnoreCase.Location = new Point(580, 90);
            chkSurnamesIgnoreCase.Margin = new Padding(4);
            chkSurnamesIgnoreCase.Name = "chkSurnamesIgnoreCase";
            chkSurnamesIgnoreCase.Size = new Size(88, 19);
            chkSurnamesIgnoreCase.TabIndex = 24;
            chkSurnamesIgnoreCase.Text = "Ignore Case";
            chkSurnamesIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // btnShowSurnames
            // 
            btnShowSurnames.Location = new Point(393, 85);
            btnShowSurnames.Margin = new Padding(4);
            btnShowSurnames.Name = "btnShowSurnames";
            btnShowSurnames.Size = new Size(180, 26);
            btnShowSurnames.TabIndex = 23;
            btnShowSurnames.Text = "Show Surnames";
            btnShowSurnames.UseVisualStyleBackColor = true;
            btnShowSurnames.Click += BtnShowSurnames_Click;
            // 
            // dgSurnames
            // 
            dgSurnames.AllowUserToAddRows = false;
            dgSurnames.AllowUserToDeleteRows = false;
            dgSurnames.AllowUserToOrderColumns = true;
            dgSurnames.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgSurnames.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgSurnames.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgSurnames.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgSurnames.Columns.AddRange(new DataGridViewColumn[] { Surname, URI, Individuals, Families, Marriages });
            dgSurnames.FilterAndSortEnabled = true;
            dgSurnames.FilterStringChangedInvokeBeforeDatasourceUpdate = true;
            dgSurnames.Location = new Point(4, 119);
            dgSurnames.Margin = new Padding(7);
            dgSurnames.MultiSelect = false;
            dgSurnames.Name = "dgSurnames";
            dgSurnames.ReadOnly = true;
            dgSurnames.RightToLeft = RightToLeft.No;
            dgSurnames.RowHeadersVisible = false;
            dgSurnames.RowHeadersWidth = 50;
            dgSurnames.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgSurnames.Size = new Size(1234, 293);
            dgSurnames.SortStringChangedInvokeBeforeDatasourceUpdate = true;
            dgSurnames.TabIndex = 1;
            dgSurnames.VirtualMode = true;
            dgSurnames.CellContentClick += DgSurnames_CellContentClick;
            dgSurnames.CellDoubleClick += DgSurnames_CellDoubleClick;
            // 
            // Surname
            // 
            Surname.DataPropertyName = "Surname";
            Surname.HeaderText = "Surname";
            Surname.MinimumWidth = 22;
            Surname.Name = "Surname";
            Surname.ReadOnly = true;
            Surname.SortMode = DataGridViewColumnSortMode.Programmatic;
            Surname.Width = 79;
            // 
            // URI
            // 
            URI.DataPropertyName = "URI";
            URI.HeaderText = "Link";
            URI.MinimumWidth = 22;
            URI.Name = "URI";
            URI.ReadOnly = true;
            URI.Resizable = DataGridViewTriState.True;
            URI.SortMode = DataGridViewColumnSortMode.Programmatic;
            URI.Visible = false;
            URI.Width = 200;
            // 
            // Individuals
            // 
            Individuals.DataPropertyName = "Individuals";
            Individuals.HeaderText = "Individuals";
            Individuals.MinimumWidth = 22;
            Individuals.Name = "Individuals";
            Individuals.ReadOnly = true;
            Individuals.SortMode = DataGridViewColumnSortMode.Programmatic;
            Individuals.Width = 89;
            // 
            // Families
            // 
            Families.DataPropertyName = "Families";
            Families.HeaderText = "Families";
            Families.MinimumWidth = 22;
            Families.Name = "Families";
            Families.ReadOnly = true;
            Families.SortMode = DataGridViewColumnSortMode.Programmatic;
            Families.Width = 75;
            // 
            // Marriages
            // 
            Marriages.DataPropertyName = "Marriages";
            Marriages.HeaderText = "Marriages";
            Marriages.MinimumWidth = 22;
            Marriages.Name = "Marriages";
            Marriages.ReadOnly = true;
            Marriages.SortMode = DataGridViewColumnSortMode.Programmatic;
            Marriages.Width = 84;
            // 
            // reltypesSurnames
            // 
            reltypesSurnames.Location = new Point(7, 4);
            reltypesSurnames.Margin = new Padding(7);
            reltypesSurnames.MarriedToDB = true;
            reltypesSurnames.Name = "reltypesSurnames";
            reltypesSurnames.Size = new Size(379, 114);
            reltypesSurnames.TabIndex = 22;
            // 
            // tabFacts
            // 
            tabFacts.Controls.Add(btnDuplicateFacts);
            tabFacts.Controls.Add(btnShowExclusions);
            tabFacts.Controls.Add(panel1);
            tabFacts.Controls.Add(btnDeselectExcludeAllFactTypes);
            tabFacts.Controls.Add(lblExclude);
            tabFacts.Controls.Add(btnExcludeAllFactTypes);
            tabFacts.Controls.Add(btnDeselectAllFactTypes);
            tabFacts.Controls.Add(ckbFactExclude);
            tabFacts.Controls.Add(btnShowFacts);
            tabFacts.Controls.Add(btnSelectAllFactTypes);
            tabFacts.Controls.Add(label3);
            tabFacts.Controls.Add(ckbFactSelect);
            tabFacts.Controls.Add(txtFactsSurname);
            tabFacts.Controls.Add(relTypesFacts);
            tabFacts.Location = new Point(4, 24);
            tabFacts.Margin = new Padding(4);
            tabFacts.Name = "tabFacts";
            tabFacts.Size = new Size(1238, 412);
            tabFacts.TabIndex = 13;
            tabFacts.Text = "Facts";
            tabFacts.UseVisualStyleBackColor = true;
            // 
            // btnDuplicateFacts
            // 
            btnDuplicateFacts.Location = new Point(799, 49);
            btnDuplicateFacts.Margin = new Padding(4);
            btnDuplicateFacts.Name = "btnDuplicateFacts";
            btnDuplicateFacts.Size = new Size(189, 44);
            btnDuplicateFacts.TabIndex = 42;
            btnDuplicateFacts.Text = "Show Duplicate Facts of Selected Fact Type";
            btnDuplicateFacts.UseVisualStyleBackColor = true;
            btnDuplicateFacts.Click += BtnDuplicateFacts_Click;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(radioOnlyAlternate);
            panel1.Controls.Add(radioOnlyPreferred);
            panel1.Controls.Add(radioAllFacts);
            panel1.Location = new Point(197, 385);
            panel1.Margin = new Padding(2);
            panel1.Name = "panel1";
            panel1.Size = new Size(412, 25);
            panel1.TabIndex = 38;
            // 
            // radioAllFacts
            // 
            radioAllFacts.AutoSize = true;
            radioAllFacts.Checked = true;
            radioAllFacts.Location = new Point(0, 2);
            radioAllFacts.Margin = new Padding(2);
            radioAllFacts.Name = "radioAllFacts";
            radioAllFacts.Size = new Size(101, 19);
            radioAllFacts.TabIndex = 38;
            radioAllFacts.TabStop = true;
            radioAllFacts.Text = "Show All Facts";
            radioAllFacts.UseVisualStyleBackColor = true;
            radioAllFacts.CheckedChanged += RadioFacts_CheckedChanged;
            // 
            // btnDeselectExcludeAllFactTypes
            // 
            btnDeselectExcludeAllFactTypes.Location = new Point(631, 126);
            btnDeselectExcludeAllFactTypes.Margin = new Padding(4);
            btnDeselectExcludeAllFactTypes.Name = "btnDeselectExcludeAllFactTypes";
            btnDeselectExcludeAllFactTypes.Size = new Size(159, 26);
            btnDeselectExcludeAllFactTypes.TabIndex = 40;
            btnDeselectExcludeAllFactTypes.Text = "De-select all Fact Types";
            btnDeselectExcludeAllFactTypes.UseVisualStyleBackColor = true;
            btnDeselectExcludeAllFactTypes.Visible = false;
            btnDeselectExcludeAllFactTypes.Click += BtnDeselectExcludeAllFactTypes_Click;
            // 
            // lblExclude
            // 
            lblExclude.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblExclude.Location = new Point(365, 431);
            lblExclude.Margin = new Padding(4, 0, 4, 0);
            lblExclude.Name = "lblExclude";
            lblExclude.Size = new Size(342, 15);
            lblExclude.TabIndex = 32;
            lblExclude.Text = "Select Facts to Exclude from Report";
            lblExclude.TextAlign = ContentAlignment.TopCenter;
            lblExclude.Visible = false;
            // 
            // btnExcludeAllFactTypes
            // 
            btnExcludeAllFactTypes.Location = new Point(424, 125);
            btnExcludeAllFactTypes.Margin = new Padding(4);
            btnExcludeAllFactTypes.Name = "btnExcludeAllFactTypes";
            btnExcludeAllFactTypes.Size = new Size(159, 26);
            btnExcludeAllFactTypes.TabIndex = 39;
            btnExcludeAllFactTypes.Text = "Select all Fact Types";
            btnExcludeAllFactTypes.UseVisualStyleBackColor = true;
            btnExcludeAllFactTypes.Visible = false;
            btnExcludeAllFactTypes.Click += BtnExcludeAllFactTypes_Click;
            // 
            // btnDeselectAllFactTypes
            // 
            btnDeselectAllFactTypes.Location = new Point(216, 126);
            btnDeselectAllFactTypes.Margin = new Padding(4);
            btnDeselectAllFactTypes.Name = "btnDeselectAllFactTypes";
            btnDeselectAllFactTypes.Size = new Size(159, 26);
            btnDeselectAllFactTypes.TabIndex = 27;
            btnDeselectAllFactTypes.Text = "De-select all Fact Types";
            btnDeselectAllFactTypes.UseVisualStyleBackColor = true;
            btnDeselectAllFactTypes.Click += BtnDeselectAllFactTypes_Click;
            // 
            // btnShowFacts
            // 
            btnShowFacts.Location = new Point(424, 49);
            btnShowFacts.Margin = new Padding(4);
            btnShowFacts.Name = "btnShowFacts";
            btnShowFacts.Size = new Size(365, 44);
            btnShowFacts.TabIndex = 37;
            btnShowFacts.Text = "Show Facts for Individuals with Selected Fact Types";
            btnShowFacts.UseVisualStyleBackColor = true;
            btnShowFacts.Click += BtnShowFacts_Click;
            // 
            // btnSelectAllFactTypes
            // 
            btnSelectAllFactTypes.Location = new Point(9, 125);
            btnSelectAllFactTypes.Margin = new Padding(4);
            btnSelectAllFactTypes.Name = "btnSelectAllFactTypes";
            btnSelectAllFactTypes.Size = new Size(159, 26);
            btnSelectAllFactTypes.TabIndex = 26;
            btnSelectAllFactTypes.Text = "Select all Fact Types";
            btnSelectAllFactTypes.UseVisualStyleBackColor = true;
            btnSelectAllFactTypes.Click += BtnSelectAllFactTypes_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(428, 22);
            label3.Margin = new Padding(4, 0, 4, 0);
            label3.Name = "label3";
            label3.Size = new Size(54, 15);
            label3.TabIndex = 36;
            label3.Text = "Surname";
            // 
            // ckbFactSelect
            // 
            ckbFactSelect.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            ckbFactSelect.FormattingEnabled = true;
            ckbFactSelect.Location = new Point(9, 158);
            ckbFactSelect.Margin = new Padding(4);
            ckbFactSelect.Name = "ckbFactSelect";
            ckbFactSelect.ScrollAlwaysVisible = true;
            ckbFactSelect.SelectionMode = SelectionMode.None;
            ckbFactSelect.Size = new Size(366, 220);
            ckbFactSelect.TabIndex = 25;
            ckbFactSelect.MouseClick += CkbFactSelect_MouseClick;
            // 
            // txtFactsSurname
            // 
            txtFactsSurname.Location = new Point(491, 19);
            txtFactsSurname.Margin = new Padding(4);
            txtFactsSurname.Name = "txtFactsSurname";
            txtFactsSurname.Size = new Size(298, 23);
            txtFactsSurname.TabIndex = 35;
            // 
            // relTypesFacts
            // 
            relTypesFacts.Location = new Point(9, 4);
            relTypesFacts.Margin = new Padding(7);
            relTypesFacts.MarriedToDB = true;
            relTypesFacts.Name = "relTypesFacts";
            relTypesFacts.Size = new Size(379, 115);
            relTypesFacts.TabIndex = 21;
            relTypesFacts.RelationTypesChanged += RelTypesFacts_RelationTypesChanged;
            // 
            // tabToday
            // 
            tabToday.Controls.Add(rtbToday);
            tabToday.Controls.Add(labTodayYearStep);
            tabToday.Controls.Add(nudToday);
            tabToday.Controls.Add(btnUpdateTodaysEvents);
            tabToday.Controls.Add(labTodayLoadWorldEvents);
            tabToday.Controls.Add(pbToday);
            tabToday.Controls.Add(rbTodayMonth);
            tabToday.Controls.Add(rbTodaySingle);
            tabToday.Controls.Add(labTodaySelectDate);
            tabToday.Controls.Add(dpToday);
            tabToday.Location = new Point(4, 24);
            tabToday.Margin = new Padding(4);
            tabToday.Name = "tabToday";
            tabToday.Padding = new Padding(4);
            tabToday.Size = new Size(1238, 412);
            tabToday.TabIndex = 17;
            tabToday.Text = "On This Day";
            tabToday.UseVisualStyleBackColor = true;
            // 
            // rtbToday
            // 
            rtbToday.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            rtbToday.Font = new Font("Microsoft Sans Serif", 9F);
            rtbToday.Location = new Point(4, 55);
            rtbToday.Margin = new Padding(4);
            rtbToday.Name = "rtbToday";
            rtbToday.ReadOnly = true;
            rtbToday.Size = new Size(1230, 361);
            rtbToday.TabIndex = 17;
            rtbToday.Text = "";
            // 
            // labTodayYearStep
            // 
            labTodayYearStep.AutoSize = true;
            labTodayYearStep.Location = new Point(645, 26);
            labTodayYearStep.Margin = new Padding(4, 0, 4, 0);
            labTodayYearStep.Name = "labTodayYearStep";
            labTodayYearStep.Size = new Size(61, 15);
            labTodayYearStep.TabIndex = 16;
            labTodayYearStep.Text = "Year Step :";
            // 
            // nudToday
            // 
            nudToday.Location = new Point(714, 24);
            nudToday.Margin = new Padding(4);
            nudToday.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudToday.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudToday.Name = "nudToday";
            nudToday.Size = new Size(49, 23);
            nudToday.TabIndex = 15;
            nudToday.Value = new decimal(new int[] { 5, 0, 0, 0 });
            nudToday.ValueChanged += NudToday_ValueChanged;
            // 
            // btnUpdateTodaysEvents
            // 
            btnUpdateTodaysEvents.Location = new Point(298, 21);
            btnUpdateTodaysEvents.Margin = new Padding(4);
            btnUpdateTodaysEvents.Name = "btnUpdateTodaysEvents";
            btnUpdateTodaysEvents.Size = new Size(134, 26);
            btnUpdateTodaysEvents.TabIndex = 14;
            btnUpdateTodaysEvents.Text = "Update list of Events";
            btnUpdateTodaysEvents.UseVisualStyleBackColor = true;
            btnUpdateTodaysEvents.Click += BtnUpdateTodaysEvents_Click;
            // 
            // labTodayLoadWorldEvents
            // 
            labTodayLoadWorldEvents.AutoSize = true;
            labTodayLoadWorldEvents.Location = new Point(771, 27);
            labTodayLoadWorldEvents.Margin = new Padding(4, 0, 4, 0);
            labTodayLoadWorldEvents.Name = "labTodayLoadWorldEvents";
            labTodayLoadWorldEvents.Size = new Size(122, 15);
            labTodayLoadWorldEvents.TabIndex = 13;
            labTodayLoadWorldEvents.Text = "Loading World Events";
            // 
            // pbToday
            // 
            pbToday.Location = new Point(901, 24);
            pbToday.Margin = new Padding(4);
            pbToday.Name = "pbToday";
            pbToday.Size = new Size(198, 23);
            pbToday.TabIndex = 12;
            // 
            // rbTodayMonth
            // 
            rbTodayMonth.AutoSize = true;
            rbTodayMonth.Location = new Point(534, 24);
            rbTodayMonth.Margin = new Padding(4);
            rbTodayMonth.Name = "rbTodayMonth";
            rbTodayMonth.Size = new Size(98, 19);
            rbTodayMonth.TabIndex = 11;
            rbTodayMonth.Text = "Whole Month";
            rbTodayMonth.UseVisualStyleBackColor = true;
            rbTodayMonth.CheckedChanged += RbTodayMonth_CheckedChanged;
            // 
            // rbTodaySingle
            // 
            rbTodaySingle.AutoSize = true;
            rbTodaySingle.Checked = true;
            rbTodaySingle.Location = new Point(439, 24);
            rbTodaySingle.Margin = new Padding(4);
            rbTodaySingle.Name = "rbTodaySingle";
            rbTodaySingle.Size = new Size(80, 19);
            rbTodaySingle.TabIndex = 10;
            rbTodaySingle.TabStop = true;
            rbTodaySingle.Text = "Single Day";
            rbTodaySingle.UseVisualStyleBackColor = true;
            rbTodaySingle.CheckedChanged += RbTodaySingle_CheckedChanged;
            // 
            // labTodaySelectDate
            // 
            labTodaySelectDate.AutoSize = true;
            labTodaySelectDate.Location = new Point(4, 26);
            labTodaySelectDate.Margin = new Padding(4, 0, 4, 0);
            labTodaySelectDate.Name = "labTodaySelectDate";
            labTodaySelectDate.Size = new Size(71, 15);
            labTodaySelectDate.TabIndex = 9;
            labTodaySelectDate.Text = "Select Date :";
            // 
            // dpToday
            // 
            dpToday.Location = new Point(91, 24);
            dpToday.Margin = new Padding(4);
            dpToday.Name = "dpToday";
            dpToday.Size = new Size(199, 23);
            dpToday.TabIndex = 8;
            // 
            // NonDuplicate
            // 
            NonDuplicate.DataPropertyName = "IgnoreNonDuplicate";
            NonDuplicate.FalseValue = "False";
            NonDuplicate.HeaderText = "Ignore";
            NonDuplicate.MinimumWidth = 40;
            NonDuplicate.Name = "NonDuplicate";
            NonDuplicate.ReadOnly = true;
            NonDuplicate.TrueValue = "True";
            NonDuplicate.Width = 40;
            // 
            // Score
            // 
            Score.DataPropertyName = "Score";
            Score.HeaderText = "Score";
            Score.MinimumWidth = 10;
            Score.Name = "Score";
            Score.ReadOnly = true;
            Score.Width = 200;
            // 
            // DuplicateIndividualID
            // 
            DuplicateIndividualID.DataPropertyName = "IndividualID";
            DuplicateIndividualID.HeaderText = "ID";
            DuplicateIndividualID.MinimumWidth = 10;
            DuplicateIndividualID.Name = "DuplicateIndividualID";
            DuplicateIndividualID.ReadOnly = true;
            DuplicateIndividualID.Width = 200;
            // 
            // DuplicateName
            // 
            DuplicateName.DataPropertyName = "Name";
            DuplicateName.HeaderText = "Name";
            DuplicateName.MinimumWidth = 50;
            DuplicateName.Name = "DuplicateName";
            DuplicateName.ReadOnly = true;
            DuplicateName.Width = 150;
            // 
            // DuplicateForenames
            // 
            DuplicateForenames.DataPropertyName = "Forenames";
            DuplicateForenames.HeaderText = "Forenames";
            DuplicateForenames.MinimumWidth = 10;
            DuplicateForenames.Name = "DuplicateForenames";
            DuplicateForenames.ReadOnly = true;
            DuplicateForenames.Visible = false;
            DuplicateForenames.Width = 200;
            // 
            // DuplicateSurname
            // 
            DuplicateSurname.DataPropertyName = "Surname";
            DuplicateSurname.HeaderText = "Surname";
            DuplicateSurname.MinimumWidth = 10;
            DuplicateSurname.Name = "DuplicateSurname";
            DuplicateSurname.ReadOnly = true;
            DuplicateSurname.Visible = false;
            DuplicateSurname.Width = 200;
            // 
            // DuplicateBirthDate
            // 
            DuplicateBirthDate.DataPropertyName = "BirthDate";
            DuplicateBirthDate.HeaderText = "Birthdate";
            DuplicateBirthDate.MinimumWidth = 50;
            DuplicateBirthDate.Name = "DuplicateBirthDate";
            DuplicateBirthDate.ReadOnly = true;
            DuplicateBirthDate.Width = 150;
            // 
            // DuplicateBirthLocation
            // 
            DuplicateBirthLocation.DataPropertyName = "BirthLocation";
            DuplicateBirthLocation.HeaderText = "Birth Location";
            DuplicateBirthLocation.MinimumWidth = 100;
            DuplicateBirthLocation.Name = "DuplicateBirthLocation";
            DuplicateBirthLocation.ReadOnly = true;
            DuplicateBirthLocation.Width = 175;
            // 
            // MatchIndividualID
            // 
            MatchIndividualID.DataPropertyName = "MatchIndividualID";
            MatchIndividualID.HeaderText = "Match ID";
            MatchIndividualID.MinimumWidth = 10;
            MatchIndividualID.Name = "MatchIndividualID";
            MatchIndividualID.ReadOnly = true;
            MatchIndividualID.Width = 50;
            // 
            // MatchName
            // 
            MatchName.DataPropertyName = "MatchName";
            MatchName.HeaderText = "Match Name";
            MatchName.MinimumWidth = 50;
            MatchName.Name = "MatchName";
            MatchName.ReadOnly = true;
            MatchName.Width = 150;
            // 
            // MatchBirthDate
            // 
            MatchBirthDate.DataPropertyName = "MatchBirthDate";
            MatchBirthDate.HeaderText = "Match Birthdate";
            MatchBirthDate.MinimumWidth = 50;
            MatchBirthDate.Name = "MatchBirthDate";
            MatchBirthDate.ReadOnly = true;
            MatchBirthDate.Width = 150;
            // 
            // MatchBirthLocation
            // 
            MatchBirthLocation.DataPropertyName = "MatchBirthLocation";
            MatchBirthLocation.HeaderText = "Match Birth Location";
            MatchBirthLocation.MinimumWidth = 100;
            MatchBirthLocation.Name = "MatchBirthLocation";
            MatchBirthLocation.ReadOnly = true;
            MatchBirthLocation.Width = 175;
            // 
            // saveDatabase
            // 
            saveDatabase.DefaultExt = "zip";
            saveDatabase.Filter = "Zip Files | *.zip";
            // 
            // restoreDatabase
            // 
            restoreDatabase.FileName = "*.zip";
            restoreDatabase.Filter = "Gecode Databases | *.s3db | Zip Files | *.zip";
            // 
            // imageList1
            // 
            imageList1.ColorDepth = ColorDepth.Depth8Bit;
            imageList1.ImageSize = new Size(16, 16);
            imageList1.TransparentColor = Color.Transparent;
            // 
            // MainForm
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1246, 497);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip1);
            Controls.Add(tabSelector);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Margin = new Padding(4);
            MinimumSize = new Size(659, 446);
            Name = "MainForm";
            Text = "Family Tree Analyzer";
            FormClosing += MainForm_FormClosing;
            Load += MainForm_Load;
            DragDrop += MainForm_DragDrop;
            DragEnter += MainForm_DragEnter;
            Move += MainForm_Move;
            Resize += MainForm_Resize;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            mnuSetRoot.ResumeLayout(false);
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)tbDuplicateScore).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgCheckAncestors).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgDataErrors).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgCountries).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgRegions).EndInit();
            tabWorldWars.ResumeLayout(false);
            tabWorldWars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgWorldWars).EndInit();
            ctxViewNotes.ResumeLayout(false);
            tabTreetops.ResumeLayout(false);
            tabTreetops.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgTreeTops).EndInit();
            tabColourReports.ResumeLayout(false);
            tabColourReports.PerformLayout();
            groupBox7.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            tabLostCousins.ResumeLayout(false);
            LCSubTabs.ResumeLayout(false);
            LCReportsTab.ResumeLayout(false);
            LCReportsTab.PerformLayout();
            Referrals.ResumeLayout(false);
            Referrals.PerformLayout();
            LCUpdatesTab.ResumeLayout(false);
            LCUpdatesTab.PerformLayout();
            groupBox8.ResumeLayout(false);
            groupBox8.PerformLayout();
            LCVerifyTab.ResumeLayout(false);
            LCVerifyTab.PerformLayout();
            tabCensus.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox10.ResumeLayout(false);
            groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)udAgeFilter).EndInit();
            groupBox9.ResumeLayout(false);
            groupBox11.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            groupBox5.ResumeLayout(false);
            groupBox6.ResumeLayout(false);
            tabLocations.ResumeLayout(false);
            tabCtrlLocations.ResumeLayout(false);
            tabTreeView.ResumeLayout(false);
            tabCountries.ResumeLayout(false);
            tabRegions.ResumeLayout(false);
            tabSubRegions.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgSubRegions).EndInit();
            tabAddresses.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgAddresses).EndInit();
            tabPlaces.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgPlaces).EndInit();
            tabDisplayProgress.ResumeLayout(false);
            splitGedcom.Panel1.ResumeLayout(false);
            splitGedcom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitGedcom).EndInit();
            splitGedcom.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            tabSelector.ResumeLayout(false);
            tabMainLists.ResumeLayout(false);
            tabMainListsSelector.ResumeLayout(false);
            tabIndividuals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgIndividuals).EndInit();
            tabFamilies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgFamilies).EndInit();
            tabSources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgSources).EndInit();
            tabOccupations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgOccupations).EndInit();
            tabCustomFacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgCustomFacts).EndInit();
            tabErrorsFixes.ResumeLayout(false);
            tabErrorFixSelector.ResumeLayout(false);
            tabDataErrors.ResumeLayout(false);
            gbDataErrorTypes.ResumeLayout(false);
            tabDuplicates.ResumeLayout(false);
            tabDuplicates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgDuplicates).EndInit();
            tabLooseBirths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgLooseBirths).EndInit();
            tabLooseDeaths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgLooseDeaths).EndInit();
            tabLooseInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgLooseInfo).EndInit();
            tabSurnames.ResumeLayout(false);
            tabSurnames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgSurnames).EndInit();
            tabFacts.ResumeLayout(false);
            tabFacts.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            tabToday.ResumeLayout(false);
            tabToday.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudToday).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
        private FTAnalyzer.Forms.Controls.VirtualDGVIndividuals dgWorldWars;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtWorldWarsSurname;
        private FTAnalyzer.Forms.Controls.RelationTypes wardeadRelation;
        private FTAnalyzer.Forms.Controls.CensusCountry wardeadCountry;
        private System.Windows.Forms.TabPage tabTreetops;
        private FTAnalyzer.Forms.Controls.VirtualDGVIndividuals dgTreeTops;
        private System.Windows.Forms.CheckBox ckbTTIgnoreLocations;
        private System.Windows.Forms.Button btnTreeTops;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtTreetopsSurname;
        private FTAnalyzer.Forms.Controls.RelationTypes treetopsRelation;
        private FTAnalyzer.Forms.Controls.CensusCountry treetopsCountry;
        private System.Windows.Forms.TabPage tabColourReports;
        private System.Windows.Forms.Label labResearchTabSurname;
        private System.Windows.Forms.TextBox txtColouredSurname;
        private FTAnalyzer.Forms.Controls.RelationTypes relTypesColoured;
        private System.Windows.Forms.TabPage tabLostCousins;
        private System.Windows.Forms.TabPage tabCensus;
        private System.Windows.Forms.TabPage tabLocations;
        private System.Windows.Forms.TabControl tabCtrlLocations;
        private System.Windows.Forms.TabPage tabTreeView;
        private System.Windows.Forms.TreeView treeViewLocations;
        private System.Windows.Forms.TabPage tabCountries;
        private System.Windows.Forms.TabPage tabRegions;
        private System.Windows.Forms.TabPage tabSubRegions;
        private FTAnalyzer.Forms.Controls.VirtualDGVLocations dgSubRegions;
        private System.Windows.Forms.TabPage tabAddresses;
        private FTAnalyzer.Forms.Controls.VirtualDGVLocations dgAddresses;
        private System.Windows.Forms.TabPage tabPlaces;
        private FTAnalyzer.Forms.Controls.VirtualDGVLocations dgPlaces;
        private System.Windows.Forms.TabPage tabDisplayProgress;
        private System.Windows.Forms.TabControl tabSelector;
        private System.Windows.Forms.ToolStripMenuItem mnuMaps;
        private System.Windows.Forms.Button btnOldOSMap;
        private System.Windows.Forms.ToolStripMenuItem mnuShowTimeline;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.ToolStripMenuItem whatsNewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem mnuLocationsGeocodeReport;
        private System.Windows.Forms.ToolStripMenuItem mnuGeocodeLocations;
        private System.Windows.Forms.ToolStripMenuItem databaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem backupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRestore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.SaveFileDialog saveDatabase;
        private System.Windows.Forms.OpenFileDialog restoreDatabase;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem clearRecentFileListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent1;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent2;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent3;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent4;
        private System.Windows.Forms.ToolStripMenuItem mnuRecent5;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolStripMenuItem mnuLifelines;
        private System.Windows.Forms.ToolStripMenuItem resetToDefaultFormSizeToolStripMenuItem;
        private System.Windows.Forms.TabPage tabFacts;
        private FTAnalyzer.Forms.Controls.RelationTypes relTypesFacts;
        private System.Windows.Forms.ToolStripMenuItem mnuPlaces;
        private System.Windows.Forms.TabPage tabSurnames;
        private FTAnalyzer.Forms.Controls.VirtualDGVSurnames dgSurnames;
        private System.Windows.Forms.DataGridViewTextBoxColumn Surname;
        private System.Windows.Forms.DataGridViewLinkColumn URI;
        private System.Windows.Forms.DataGridViewTextBoxColumn Individuals;
        private System.Windows.Forms.DataGridViewTextBoxColumn Families;
        private System.Windows.Forms.DataGridViewTextBoxColumn Marriages;
        private System.Windows.Forms.CheckedListBox ckbFactSelect;
        private System.Windows.Forms.Button btnDeselectAllFactTypes;
        private System.Windows.Forms.Button btnSelectAllFactTypes;
        private System.Windows.Forms.ToolStripMenuItem mnuPossibleCensusFacts;
        private System.Windows.Forms.ToolStripMenuItem viewNotesToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip ctxViewNotes;
        private System.Windows.Forms.ToolStripMenuItem mnuViewNotes;
        private System.Windows.Forms.ToolStripMenuItem mnuLooseBirthsToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuLooseDeathsToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuSourcesToExcel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem mnuTreetopsToExcel;
        private System.Windows.Forms.ToolStripMenuItem mnuWorldWarsToExcel;
        private System.Windows.Forms.CheckBox chkExcludeUnknownBirths;
        private System.Windows.Forms.Label labCensusTabSurname;
        private System.Windows.Forms.TextBox txtCensusSurname;
        private System.Windows.Forms.Label labCensusExcludeOverAge;
        private System.Windows.Forms.NumericUpDown udAgeFilter;
        private FTAnalyzer.Forms.Controls.CensusDateSelector cenDate;
        private FTAnalyzer.Forms.Controls.RelationTypes relTypesCensus;
        private System.Windows.Forms.Label labResearchTabFamilyFilter;
        private System.Windows.Forms.ComboBox cmbColourFamily;
        private System.Windows.Forms.ToolStripMenuItem onlineGuidesToUsingFTAnalyzerToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar tspbTabProgress;
        private System.Windows.Forms.ToolStripMenuItem mnuOSGeocoder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mnuLookupBlankFoundLocations;
        private System.Windows.Forms.CheckBox ckbMilitaryOnly;
        private System.Windows.Forms.Button btnRandomSurnameColour;
        private System.Windows.Forms.Label lblExclude;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseGEDCOM;
        private System.Windows.Forms.TabPage tabToday;
        private System.Windows.Forms.Label labTodaySelectDate;
        private System.Windows.Forms.DateTimePicker dpToday;
        private System.Windows.Forms.RadioButton rbTodayMonth;
        private System.Windows.Forms.RadioButton rbTodaySingle;
        private System.Windows.Forms.Label labTodayLoadWorldEvents;
        private System.Windows.Forms.ProgressBar pbToday;
        private System.Windows.Forms.Button btnUpdateTodaysEvents;
        private System.Windows.Forms.Label labTodayYearStep;
        private System.Windows.Forms.NumericUpDown nudToday;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadLocationsCSV;
        private System.Windows.Forms.ToolStripMenuItem mnuLoadLocationsTNG;
        private System.Windows.Forms.ToolStripStatusLabel tsStatusLabel;
        private System.Windows.Forms.Button btnShowSurnames;
        private FTAnalyzer.Forms.Controls.RelationTypes reltypesSurnames;
        private System.Windows.Forms.ToolStripMenuItem mnuCousinsCountReport;
        private System.Windows.Forms.TabPage tabMainLists;
        private System.Windows.Forms.TabControl tabMainListsSelector;
        private System.Windows.Forms.TabPage tabIndividuals;
        private FTAnalyzer.Forms.Controls.VirtualDGVIndividuals dgIndividuals;
        private System.Windows.Forms.TabPage tabFamilies;
        private FTAnalyzer.Forms.Controls.VirtualDGVFamily dgFamilies;
        private System.Windows.Forms.TabPage tabSources;
        private System.Windows.Forms.TabPage tabOccupations;
        private FTAnalyzer.Forms.Controls.VirtualDGVOccupations dgOccupations;
        private FTAnalyzer.Forms.Controls.VirtualDGVSources dgSources;
        private System.Windows.Forms.TabPage tabErrorsFixes;
        private System.Windows.Forms.TabControl tabErrorFixSelector;
        private System.Windows.Forms.TabPage tabDataErrors;
        private System.Windows.Forms.GroupBox gbDataErrorTypes;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.TabPage tabDuplicates;
        private System.Windows.Forms.CheckBox ckbHideIgnoredDuplicates;
        private System.Windows.Forms.Button btnCancelDuplicates;
        private System.Windows.Forms.Label labErrorTabCandidateDupTitle;
        private System.Windows.Forms.Label labErrorTabAggrMatch;
        private System.Windows.Forms.Label labErrorTabLooseMatch;
        private System.Windows.Forms.TrackBar tbDuplicateScore;
        private System.Windows.Forms.Label labCalcDuplicates;
        private System.Windows.Forms.ProgressBar pbDuplicates;
        private FTAnalyzer.Forms.Controls.VirtualDGVDuplicates dgDuplicates;
        private System.Windows.Forms.TabPage tabLooseBirths;
        private FTAnalyzer.Forms.Controls.VirtualDGVLooseBirths dgLooseBirths;
        private System.Windows.Forms.TabPage tabLooseDeaths;
        private FTAnalyzer.Forms.Controls.VirtualDGVLooseDeaths dgLooseDeaths;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Button btnAdvancedMissingData;
        private System.Windows.Forms.Button btnStandardMissingData;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnIrishColourCensus;
        private System.Windows.Forms.Button btnCanadianColourCensus;
        private System.Windows.Forms.Button btnUKColourCensus;
        private System.Windows.Forms.Button btnUSColourCensus;
        private System.Windows.Forms.Button btnColourBMD;
        private System.Windows.Forms.CheckBox ckbIgnoreNoDeathDate;
        private System.Windows.Forms.CheckBox ckbIgnoreNoBirthDate;
        private System.Windows.Forms.CheckBox ckbTTIncludeOnlyOneParent;
        private System.Windows.Forms.ToolStripMenuItem privacyPolicyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mnuHowManyGreats;
        private System.Windows.Forms.ToolStripMenuItem facebookSupportGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem facebookUserGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripMenuItem mnuDataErrorsToExcel;
        private System.Windows.Forms.Button btnModernOSMap;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem mnuDNA_GEDCOM;
        private System.Windows.Forms.ToolStripMenuItem MnuExportLocations;
        private System.Windows.Forms.ToolStripMenuItem getGoogleAPIKeyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripMenuItem googleAPISetupGuideToolStripMenuItem;
        private System.Windows.Forms.TabControl LCSubTabs;
        private System.Windows.Forms.TabPage LCReportsTab;
        private System.Windows.Forms.GroupBox Referrals;
        private System.Windows.Forms.CheckBox ckbReferralInCommon;
        private System.Windows.Forms.Button btnReferrals;
        private System.Windows.Forms.ComboBox cmbReferrals;
        private System.Windows.Forms.Label labLCSelectInd;
        private System.Windows.Forms.Button btnLCnoCensus;
        private System.Windows.Forms.Button btnLCDuplicates;
        private System.Windows.Forms.Button btnLCMissingCountry;
        private System.Windows.Forms.Button btnLC1940USA;
        private System.Windows.Forms.RichTextBox rtbLostCousins;
        private System.Windows.Forms.LinkLabel linkLabel2;
        private System.Windows.Forms.Button btnLC1911EW;
        private System.Windows.Forms.LinkLabel LabLostCousinsWeb;
        private System.Windows.Forms.CheckBox ckbShowLCEntered;
        private System.Windows.Forms.Button btnLC1841EW;
        private System.Windows.Forms.Button btnLC1911Ireland;
        private System.Windows.Forms.Button btnLC1880USA;
        private System.Windows.Forms.Button btnLC1881EW;
        private System.Windows.Forms.Button btnLC1881Canada;
        private System.Windows.Forms.Button btnLC1881Scot;
        private FTAnalyzer.Forms.Controls.RelationTypes relTypesLC;
        private System.Windows.Forms.TabPage LCUpdatesTab;
        private System.Windows.Forms.Button btnUpdateLostCousinsWebsite;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.Button btnLCLogin;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label labLCUpdatesEmail;
        private System.Windows.Forms.TextBox txtLCEmail;
        private System.Windows.Forms.MaskedTextBox txtLCPassword;
        private System.Windows.Forms.CheckBox chkLCRootPersonConfirm;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RichTextBox rtbLCUpdateData;
        private System.Windows.Forms.CheckBox chkSurnamesIgnoreCase;
        private System.Windows.Forms.Button btnLCPotentialUploads;
        private System.Windows.Forms.Button btnViewInvalidRefs;
        private System.Windows.Forms.TabPage tabLooseInfo;
        private FTAnalyzer.Forms.Controls.VirtualDGVLooseInfo dgLooseInfo;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDuplicateCensus;
        private System.Windows.Forms.Button btnMissingCensusLocation;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button btnMismatchedChildrenStatus;
        private System.Windows.Forms.Button btnNoChildrenStatus;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button btnReportUnrecognised;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnInconsistentLocations;
        private System.Windows.Forms.Button btnUnrecognisedCensusRef;
        private System.Windows.Forms.Button btnIncompleteCensusRef;
        private System.Windows.Forms.Button btnMissingCensusRefs;
        private System.Windows.Forms.Button btnCensusRefs;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.Button btnRandomSurnameEntered;
        private System.Windows.Forms.Button btnRandomSurnameMissing;
        private System.Windows.Forms.CheckBox chkAnyCensusYear;
        private System.Windows.Forms.TabPage LCVerifyTab;
        private System.Windows.Forms.DataGridView dgCheckAncestors;
        private System.Windows.Forms.Button btnCheckMyAncestors;
        private System.Windows.Forms.Label lblCheckAncestors;
        private Utilities.ScrollingRichTextBox rtbCheckAncestors;
        private System.Windows.Forms.Button btnShowCensusMissing;
        private System.Windows.Forms.Button btnShowCensusEntered;
        private System.Windows.Forms.ToolStripMenuItem mnuBirthdayEffect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripMenuItem mnuJSON;
        private System.Windows.Forms.ToolStripMenuItem mnuPossiblyMissingChildReport;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.Button BtnProblemCensusFacts;
        private System.Windows.Forms.Button BtnAutoCreatedCensusFacts;
        private System.Windows.Forms.ToolStripMenuItem MnuAgedOver99Report;
        private System.Windows.Forms.ToolStripMenuItem MnuSingleParentsReport;
        private System.Windows.Forms.TabPage tabCustomFacts;
        private FTAnalyzer.Forms.Controls.VirtualDGVCustomFacts dgCustomFacts;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox txtAliveDates;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Button btnAliveOnDate;
        private System.Windows.Forms.ToolStripMenuItem mnuSurnamesToExcel;
        private System.Windows.Forms.CheckBox chkIgnoreUnnamedTwins;
        private System.Windows.Forms.Label labCompletion;
        private System.Windows.Forms.DataGridViewCheckBoxColumn NonDuplicate;
        private System.Windows.Forms.DataGridViewTextBoxColumn Score;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateIndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateName;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateForenames;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateSurname;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn DuplicateBirthLocation;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchIndividualID;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchName;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchBirthDate;
        private System.Windows.Forms.DataGridViewTextBoxColumn MatchBirthLocation;
        private System.Windows.Forms.Label labDuplicateSlider;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioOnlyAlternate;
        private System.Windows.Forms.RadioButton radioOnlyPreferred;
        private System.Windows.Forms.RadioButton radioAllFacts;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripMenuItem mnuGoogleMyMaps;
        private Forms.Controls.VirtualDGVDataErrors dgDataErrors;
        private Forms.Controls.VirtualDGVLocations dgCountries;
        private Forms.Controls.VirtualDGVLocations dgRegions;
        private System.Windows.Forms.SplitContainer splitGedcom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LbProgramName;
        private System.Windows.Forms.Label labRelationships;
        private System.Windows.Forms.ProgressBar pbRelationships;
        private System.Windows.Forms.Label labFamilies;
        private System.Windows.Forms.ProgressBar pbFamilies;
        private System.Windows.Forms.Label labIndividuals;
        private System.Windows.Forms.ProgressBar pbIndividuals;
        private System.Windows.Forms.Label labSources;
        private System.Windows.Forms.ProgressBar pbSources;
        private Utilities.ScrollingRichTextBox rtbOutput;
        private System.Windows.Forms.Button btnShowMap;
        private Utilities.ScrollingRichTextBox rtbToday;
        private System.Windows.Forms.ToolStripMenuItem MnuCustomFactsToExcel;
        private global::System.Windows.Forms.CheckedListBox ckbDataErrors;
        private global::FTAnalyzer.Utilities.ScrollingRichTextBox rtbLCoutput;
        private Button btnDuplicateFacts;
        private Button btnShowExclusions;
        private Button btnDeselectExcludeAllFactTypes;
        private Button btnExcludeAllFactTypes;
        private CheckedListBox ckbFactExclude;
        private Button btnShowFacts;
        private Label label3;
        private TextBox txtFactsSurname;
    }
}

