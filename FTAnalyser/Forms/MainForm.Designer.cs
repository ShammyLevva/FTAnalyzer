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
                if (disposing && (components != null))
                {
                    components.Dispose();
                }
                base.Dispose(disposing);
                normalFont.Dispose();
                boldFont.Dispose();
                handwritingFont.Dispose();
                fonts.Dispose();
                rfhDuplicates.Dispose();
                if (cts!=null)
                    cts.Dispose();
                storedCursor.Dispose();
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
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
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLoadLocationsCSV = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLoadLocationsTNG = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuCloseGEDCOM = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReports = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuChildAgeProfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOlderParents = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPossibleCensusFacts = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuCousinsCountReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHowManyGreats = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuBirthdayEffect = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuPossiblyMissingChildReport = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuAgedOver99Report = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuSingleParentsReport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuIndividualsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFamiliesToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFactsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuExportLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSourcesToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDataErrorsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSurnamesToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuLooseBirthsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLooseDeathsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuTreetopsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuWorldWarsToExcel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuDNA_GEDCOM = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuJSON = new System.Windows.Forms.ToolStripMenuItem();
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
            this.mnuLocationsGeocodeReport = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuGeocodeLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuOSGeocoder = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLookupBlankFoundLocations = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewOnlineManualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.onlineGuidesToUsingFTAnalyzerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.reportAnIssueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facebookSupportGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.facebookUserGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.getGoogleAPIKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.googleAPISetupGuideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.privacyPolicyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.whatsNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSetRoot = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setAsRootToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewNotesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.tsCountLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsHintsLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tspbTabProgress = new System.Windows.Forms.ToolStripProgressBar();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTips = new System.Windows.Forms.ToolTip(this.components);
            this.dgRegions = new System.Windows.Forms.DataGridView();
            this.dgCountries = new System.Windows.Forms.DataGridView();
            this.cmbColourFamily = new System.Windows.Forms.ComboBox();
            this.btnRandomSurnameColour = new System.Windows.Forms.Button();
            this.ckbFactExclude = new System.Windows.Forms.CheckedListBox();
            this.btnShowExclusions = new System.Windows.Forms.Button();
            this.dgDataErrors = new FTAnalyzer.Forms.Controls.VirtualDGVDataErrors();
            this.tbDuplicateScore = new System.Windows.Forms.TrackBar();
            this.chkLCRootPersonConfirm = new System.Windows.Forms.CheckBox();
            this.dgCheckAncestors = new System.Windows.Forms.DataGridView();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.chkIgnoreUnnamedTwins = new System.Windows.Forms.CheckBox();
            this.printPreviewDialog = new System.Windows.Forms.PrintPreviewDialog();
            this.printDialog = new System.Windows.Forms.PrintDialog();
            this.printDocument = new System.Drawing.Printing.PrintDocument();
            this.tabWorldWars = new System.Windows.Forms.TabPage();
            this.ckbMilitaryOnly = new System.Windows.Forms.CheckBox();
            this.ckbWDIgnoreLocations = new System.Windows.Forms.CheckBox();
            this.btnWWII = new System.Windows.Forms.Button();
            this.btnWWI = new System.Windows.Forms.Button();
            this.dgWorldWars = new FTAnalyzer.Forms.Controls.VirtualDGVIndividuals();
            this.label9 = new System.Windows.Forms.Label();
            this.txtWorldWarsSurname = new System.Windows.Forms.TextBox();
            this.wardeadRelation = new FTAnalyzer.Forms.Controls.RelationTypes();
            this.wardeadCountry = new FTAnalyzer.Forms.Controls.CensusCountry();
            this.ctxViewNotes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuViewNotes = new System.Windows.Forms.ToolStripMenuItem();
            this.tabTreetops = new System.Windows.Forms.TabPage();
            this.ckbTTIncludeOnlyOneParent = new System.Windows.Forms.CheckBox();
            this.dgTreeTops = new FTAnalyzer.Forms.Controls.VirtualDGVIndividuals();
            this.ckbTTIgnoreLocations = new System.Windows.Forms.CheckBox();
            this.btnTreeTops = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.txtTreetopsSurname = new System.Windows.Forms.TextBox();
            this.treetopsRelation = new FTAnalyzer.Forms.Controls.RelationTypes();
            this.treetopsCountry = new FTAnalyzer.Forms.Controls.CensusCountry();
            this.tabColourReports = new System.Windows.Forms.TabPage();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.btnAdvancedMissingData = new System.Windows.Forms.Button();
            this.btnStandardMissingData = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ckbIgnoreNoDeathDate = new System.Windows.Forms.CheckBox();
            this.ckbIgnoreNoBirthDate = new System.Windows.Forms.CheckBox();
            this.btnIrishColourCensus = new System.Windows.Forms.Button();
            this.btnCanadianColourCensus = new System.Windows.Forms.Button();
            this.btnUKColourCensus = new System.Windows.Forms.Button();
            this.btnUSColourCensus = new System.Windows.Forms.Button();
            this.btnColourBMD = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.txtColouredSurname = new System.Windows.Forms.TextBox();
            this.relTypesColoured = new FTAnalyzer.Forms.Controls.RelationTypes();
            this.tabLostCousins = new System.Windows.Forms.TabPage();
            this.LCSubTabs = new System.Windows.Forms.TabControl();
            this.LCReportsTab = new System.Windows.Forms.TabPage();
            this.Referrals = new System.Windows.Forms.GroupBox();
            this.ckbReferralInCommon = new System.Windows.Forms.CheckBox();
            this.btnReferrals = new System.Windows.Forms.Button();
            this.cmbReferrals = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btnLCnoCensus = new System.Windows.Forms.Button();
            this.btnLCDuplicates = new System.Windows.Forms.Button();
            this.btnLCMissingCountry = new System.Windows.Forms.Button();
            this.btnLC1940USA = new System.Windows.Forms.Button();
            this.rtbLostCousins = new System.Windows.Forms.RichTextBox();
            this.linkLabel2 = new System.Windows.Forms.LinkLabel();
            this.btnLC1911EW = new System.Windows.Forms.Button();
            this.LabLostCousinsWeb = new System.Windows.Forms.LinkLabel();
            this.ckbShowLCEntered = new System.Windows.Forms.CheckBox();
            this.btnLC1841EW = new System.Windows.Forms.Button();
            this.btnLC1911Ireland = new System.Windows.Forms.Button();
            this.btnLC1880USA = new System.Windows.Forms.Button();
            this.btnLC1881EW = new System.Windows.Forms.Button();
            this.btnLC1881Canada = new System.Windows.Forms.Button();
            this.btnLC1881Scot = new System.Windows.Forms.Button();
            this.relTypesLC = new FTAnalyzer.Forms.Controls.RelationTypes();
            this.LCUpdatesTab = new System.Windows.Forms.TabPage();
            this.btnViewInvalidRefs = new System.Windows.Forms.Button();
            this.btnLCPotentialUploads = new System.Windows.Forms.Button();
            this.label21 = new System.Windows.Forms.Label();
            this.rtbLCUpdateData = new System.Windows.Forms.RichTextBox();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.btnLCLogin = new System.Windows.Forms.Button();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.txtLCEmail = new System.Windows.Forms.TextBox();
            this.txtLCPassword = new System.Windows.Forms.MaskedTextBox();
            this.btnUpdateLostCousinsWebsite = new System.Windows.Forms.Button();
            this.rtbLCoutput = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.LCVerifyTab = new System.Windows.Forms.TabPage();
            this.rtbCheckAncestors = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.btnCheckMyAncestors = new System.Windows.Forms.Button();
            this.lblCheckAncestors = new System.Windows.Forms.Label();
            this.tabCensus = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnAliveOnDate = new System.Windows.Forms.Button();
            this.txtAliveDates = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.chkAnyCensusYear = new System.Windows.Forms.CheckBox();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.btnShowCensusMissing = new System.Windows.Forms.Button();
            this.btnShowCensusEntered = new System.Windows.Forms.Button();
            this.btnRandomSurnameEntered = new System.Windows.Forms.Button();
            this.btnRandomSurnameMissing = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnInconsistentLocations = new System.Windows.Forms.Button();
            this.btnUnrecognisedCensusRef = new System.Windows.Forms.Button();
            this.btnIncompleteCensusRef = new System.Windows.Forms.Button();
            this.btnMissingCensusRefs = new System.Windows.Forms.Button();
            this.btnCensusRefs = new System.Windows.Forms.Button();
            this.chkExcludeUnknownBirths = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCensusSurname = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.udAgeFilter = new System.Windows.Forms.NumericUpDown();
            this.cenDate = new FTAnalyzer.Forms.Controls.CensusDateSelector();
            this.relTypesCensus = new FTAnalyzer.Forms.Controls.RelationTypes();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.BtnAutoCreatedCensusFacts = new System.Windows.Forms.Button();
            this.BtnProblemCensusFacts = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDuplicateCensus = new System.Windows.Forms.Button();
            this.btnMissingCensusLocation = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btnMismatchedChildrenStatus = new System.Windows.Forms.Button();
            this.btnNoChildrenStatus = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.btnReportUnrecognised = new System.Windows.Forms.Button();
            this.tabLocations = new System.Windows.Forms.TabPage();
            this.btnModernOSMap = new System.Windows.Forms.Button();
            this.btnOldOSMap = new System.Windows.Forms.Button();
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
            this.tabDisplayProgress = new System.Windows.Forms.TabPage();
            this.splitGedcom = new System.Windows.Forms.SplitContainer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.LbProgramName = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.pbRelationships = new System.Windows.Forms.ProgressBar();
            this.label6 = new System.Windows.Forms.Label();
            this.pbFamilies = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.pbIndividuals = new System.Windows.Forms.ProgressBar();
            this.label4 = new System.Windows.Forms.Label();
            this.pbSources = new System.Windows.Forms.ProgressBar();
            this.rtbOutput = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.tabSelector = new System.Windows.Forms.TabControl();
            this.tabMainLists = new System.Windows.Forms.TabPage();
            this.tabMainListsSelector = new System.Windows.Forms.TabControl();
            this.tabIndividuals = new System.Windows.Forms.TabPage();
            this.dgIndividuals = new FTAnalyzer.Forms.Controls.VirtualDGVIndividuals();
            this.tabFamilies = new System.Windows.Forms.TabPage();
            this.dgFamilies = new FTAnalyzer.Forms.Controls.VirtualDGVFamily();
            this.tabSources = new System.Windows.Forms.TabPage();
            this.dgSources = new FTAnalyzer.Forms.Controls.VirtualDGVSources();
            this.tabOccupations = new System.Windows.Forms.TabPage();
            this.dgOccupations = new FTAnalyzer.Forms.Controls.VirtualDGVOccupations();
            this.tabCustomFacts = new System.Windows.Forms.TabPage();
            this.dgCustomFacts = new FTAnalyzer.Forms.Controls.VirtualDGVCustomFacts();
            this.tabErrorsFixes = new System.Windows.Forms.TabPage();
            this.tabErrorFixSelector = new System.Windows.Forms.TabControl();
            this.tabDataErrors = new System.Windows.Forms.TabPage();
            this.gbDataErrorTypes = new System.Windows.Forms.GroupBox();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnClearAll = new System.Windows.Forms.Button();
            this.ckbDataErrors = new System.Windows.Forms.CheckedListBox();
            this.tabDuplicates = new System.Windows.Forms.TabPage();
            this.labDuplicateSlider = new System.Windows.Forms.Label();
            this.labCompletion = new System.Windows.Forms.Label();
            this.ckbHideIgnoredDuplicates = new System.Windows.Forms.CheckBox();
            this.label16 = new System.Windows.Forms.Label();
            this.labCalcDuplicates = new System.Windows.Forms.Label();
            this.pbDuplicates = new System.Windows.Forms.ProgressBar();
            this.dgDuplicates = new FTAnalyzer.Forms.Controls.VirtualDGVDuplicates();
            this.btnCancelDuplicates = new System.Windows.Forms.Button();
            this.tabLooseBirths = new System.Windows.Forms.TabPage();
            this.dgLooseBirths = new System.Windows.Forms.DataGridView();
            this.tabLooseDeaths = new System.Windows.Forms.TabPage();
            this.dgLooseDeaths = new System.Windows.Forms.DataGridView();
            this.tabLooseInfo = new System.Windows.Forms.TabPage();
            this.dgLooseInfo = new System.Windows.Forms.DataGridView();
            this.tabSurnames = new System.Windows.Forms.TabPage();
            this.chkSurnamesIgnoreCase = new System.Windows.Forms.CheckBox();
            this.btnShowSurnames = new System.Windows.Forms.Button();
            this.dgSurnames = new FTAnalyzer.Forms.Controls.VirtualDGVSurnames();
            this.Surname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.URI = new System.Windows.Forms.DataGridViewLinkColumn();
            this.Individuals = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Families = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Marriages = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.reltypesSurnames = new FTAnalyzer.Forms.Controls.RelationTypes();
            this.tabFacts = new System.Windows.Forms.TabPage();
            this.btnDuplicateFacts = new System.Windows.Forms.Button();
            this.lblExclude = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.btnDeselectExcludeAllFactTypes = new System.Windows.Forms.Button();
            this.btnExcludeAllFactTypes = new System.Windows.Forms.Button();
            this.btnDeselectAllFactTypes = new System.Windows.Forms.Button();
            this.btnSelectAllFactTypes = new System.Windows.Forms.Button();
            this.ckbFactSelect = new System.Windows.Forms.CheckedListBox();
            this.btnShowFacts = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtFactsSurname = new System.Windows.Forms.TextBox();
            this.relTypesFacts = new FTAnalyzer.Forms.Controls.RelationTypes();
            this.tabToday = new System.Windows.Forms.TabPage();
            this.label18 = new System.Windows.Forms.Label();
            this.nudToday = new System.Windows.Forms.NumericUpDown();
            this.btnUpdateTodaysEvents = new System.Windows.Forms.Button();
            this.labToday = new System.Windows.Forms.Label();
            this.pbToday = new System.Windows.Forms.ProgressBar();
            this.rbTodayMonth = new System.Windows.Forms.RadioButton();
            this.rbTodaySingle = new System.Windows.Forms.RadioButton();
            this.label17 = new System.Windows.Forms.Label();
            this.dpToday = new System.Windows.Forms.DateTimePicker();
            this.rtbToday = new FTAnalyzer.Utilities.ScrollingRichTextBox();
            this.NonDuplicate = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.Score = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateIndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateForenames = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateSurname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DuplicateBirthLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchIndividualID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchBirthDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MatchBirthLocation = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveDatabase = new System.Windows.Forms.SaveFileDialog();
            this.restoreDatabase = new System.Windows.Forms.OpenFileDialog();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.menuStrip1.SuspendLayout();
            this.mnuSetRoot.SuspendLayout();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCountries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDataErrors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDuplicateScore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCheckAncestors)).BeginInit();
            this.tabWorldWars.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorldWars)).BeginInit();
            this.ctxViewNotes.SuspendLayout();
            this.tabTreetops.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTreeTops)).BeginInit();
            this.tabColourReports.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tabLostCousins.SuspendLayout();
            this.LCSubTabs.SuspendLayout();
            this.LCReportsTab.SuspendLayout();
            this.Referrals.SuspendLayout();
            this.LCUpdatesTab.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.LCVerifyTab.SuspendLayout();
            this.tabCensus.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
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
            this.tabDisplayProgress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGedcom)).BeginInit();
            this.splitGedcom.Panel1.SuspendLayout();
            this.splitGedcom.Panel2.SuspendLayout();
            this.splitGedcom.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabSelector.SuspendLayout();
            this.tabMainLists.SuspendLayout();
            this.tabMainListsSelector.SuspendLayout();
            this.tabIndividuals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).BeginInit();
            this.tabFamilies.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).BeginInit();
            this.tabSources.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSources)).BeginInit();
            this.tabOccupations.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgOccupations)).BeginInit();
            this.tabCustomFacts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomFacts)).BeginInit();
            this.tabErrorsFixes.SuspendLayout();
            this.tabErrorFixSelector.SuspendLayout();
            this.tabDataErrors.SuspendLayout();
            this.gbDataErrorTypes.SuspendLayout();
            this.tabDuplicates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDuplicates)).BeginInit();
            this.tabLooseBirths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseBirths)).BeginInit();
            this.tabLooseDeaths.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).BeginInit();
            this.tabLooseInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseInfo)).BeginInit();
            this.tabSurnames.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSurnames)).BeginInit();
            this.tabFacts.SuspendLayout();
            this.tabToday.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudToday)).BeginInit();
            this.SuspendLayout();
            // 
            // openGedcom
            // 
            this.openGedcom.FileName = "openFileDialog1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.mnuReports,
            this.mnuExport,
            this.toolsToolStripMenuItem,
            this.mnuMaps,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(3, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1104, 24);
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
            this.mnuCloseGEDCOM,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.openToolStripMenuItem.Text = "Open GEDCOM file...";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItem_Click);
            // 
            // mnuReload
            // 
            this.mnuReload.Enabled = false;
            this.mnuReload.Name = "mnuReload";
            this.mnuReload.Size = new System.Drawing.Size(184, 22);
            this.mnuReload.Text = "Reload";
            this.mnuReload.Click += new System.EventHandler(this.ReloadToolStripMenuItem_Click);
            // 
            // mnuPrint
            // 
            this.mnuPrint.Enabled = false;
            this.mnuPrint.Name = "mnuPrint";
            this.mnuPrint.Size = new System.Drawing.Size(184, 22);
            this.mnuPrint.Text = "Print";
            this.mnuPrint.Click += new System.EventHandler(this.MnuPrint_Click);
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
            this.mnuRecent.DropDownOpening += new System.EventHandler(this.MnuRecent_DropDownOpening);
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
            this.clearRecentFileListToolStripMenuItem.Click += new System.EventHandler(this.ClearRecentFileListToolStripMenuItem_Click);
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
            this.mnuRestore,
            this.toolStripSeparator11,
            this.mnuLoadLocationsCSV,
            this.mnuLoadLocationsTNG});
            this.databaseToolStripMenuItem.Name = "databaseToolStripMenuItem";
            this.databaseToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.databaseToolStripMenuItem.Text = "Geocode Database";
            // 
            // backupToolStripMenuItem
            // 
            this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
            this.backupToolStripMenuItem.Size = new System.Drawing.Size(237, 22);
            this.backupToolStripMenuItem.Text = "Backup";
            this.backupToolStripMenuItem.Click += new System.EventHandler(this.BackupToolStripMenuItem_Click);
            // 
            // mnuRestore
            // 
            this.mnuRestore.Name = "mnuRestore";
            this.mnuRestore.Size = new System.Drawing.Size(237, 22);
            this.mnuRestore.Text = "Restore";
            this.mnuRestore.ToolTipText = "Restore is only available prior to loading GEDCOM";
            this.mnuRestore.Click += new System.EventHandler(this.RestoreToolStripMenuItem_Click);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(234, 6);
            // 
            // mnuLoadLocationsCSV
            // 
            this.mnuLoadLocationsCSV.Name = "mnuLoadLocationsCSV";
            this.mnuLoadLocationsCSV.Size = new System.Drawing.Size(237, 22);
            this.mnuLoadLocationsCSV.Text = "Load Geocoded Locations CSV";
            this.mnuLoadLocationsCSV.Click += new System.EventHandler(this.MnuLoadLocationsCSV_Click);
            // 
            // mnuLoadLocationsTNG
            // 
            this.mnuLoadLocationsTNG.Name = "mnuLoadLocationsTNG";
            this.mnuLoadLocationsTNG.Size = new System.Drawing.Size(237, 22);
            this.mnuLoadLocationsTNG.Text = "Load Geocoded Locations TNG";
            this.mnuLoadLocationsTNG.Click += new System.EventHandler(this.MnuLoadLocationsTNG_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(181, 6);
            // 
            // mnuCloseGEDCOM
            // 
            this.mnuCloseGEDCOM.Name = "mnuCloseGEDCOM";
            this.mnuCloseGEDCOM.Size = new System.Drawing.Size(184, 22);
            this.mnuCloseGEDCOM.Text = "Close GEDCOM file";
            this.mnuCloseGEDCOM.Click += new System.EventHandler(this.MnuCloseGEDCOM_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // mnuReports
            // 
            this.mnuReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuChildAgeProfiles,
            this.mnuOlderParents,
            this.mnuPossibleCensusFacts,
            this.mnuCousinsCountReport,
            this.mnuHowManyGreats,
            this.mnuBirthdayEffect,
            this.mnuPossiblyMissingChildReport,
            this.MnuAgedOver99Report,
            this.MnuSingleParentsReport});
            this.mnuReports.Name = "mnuReports";
            this.mnuReports.Size = new System.Drawing.Size(59, 22);
            this.mnuReports.Text = "Reports";
            // 
            // mnuChildAgeProfiles
            // 
            this.mnuChildAgeProfiles.Name = "mnuChildAgeProfiles";
            this.mnuChildAgeProfiles.Size = new System.Drawing.Size(230, 22);
            this.mnuChildAgeProfiles.Text = "Parent Age Report";
            this.mnuChildAgeProfiles.Click += new System.EventHandler(this.ChildAgeProfilesToolStripMenuItem_Click);
            // 
            // mnuOlderParents
            // 
            this.mnuOlderParents.Name = "mnuOlderParents";
            this.mnuOlderParents.Size = new System.Drawing.Size(230, 22);
            this.mnuOlderParents.Text = "Older Parents";
            this.mnuOlderParents.Click += new System.EventHandler(this.OlderParentsToolStripMenuItem_Click);
            // 
            // mnuPossibleCensusFacts
            // 
            this.mnuPossibleCensusFacts.Name = "mnuPossibleCensusFacts";
            this.mnuPossibleCensusFacts.Size = new System.Drawing.Size(230, 22);
            this.mnuPossibleCensusFacts.Text = "Possible Census Facts";
            this.mnuPossibleCensusFacts.ToolTipText = "This report aims to find census facts that have been incorrectly recorded as note" +
    "s";
            this.mnuPossibleCensusFacts.Click += new System.EventHandler(this.PossibleCensusFactsToolStripMenuItem_Click);
            // 
            // mnuCousinsCountReport
            // 
            this.mnuCousinsCountReport.Name = "mnuCousinsCountReport";
            this.mnuCousinsCountReport.Size = new System.Drawing.Size(230, 22);
            this.mnuCousinsCountReport.Text = "Cousins Count Report";
            this.mnuCousinsCountReport.Click += new System.EventHandler(this.CousinsCountReportToolStripMenuItem_Click);
            // 
            // mnuHowManyGreats
            // 
            this.mnuHowManyGreats.Name = "mnuHowManyGreats";
            this.mnuHowManyGreats.Size = new System.Drawing.Size(230, 22);
            this.mnuHowManyGreats.Text = "How Many Directs Report";
            this.mnuHowManyGreats.Click += new System.EventHandler(this.HowManyDirectsReportToolStripMenuItem_Click);
            // 
            // mnuBirthdayEffect
            // 
            this.mnuBirthdayEffect.Name = "mnuBirthdayEffect";
            this.mnuBirthdayEffect.Size = new System.Drawing.Size(230, 22);
            this.mnuBirthdayEffect.Text = "Birthday Effect Report";
            this.mnuBirthdayEffect.Click += new System.EventHandler(this.BirthdayEffectReportToolStripMenuItem_Click);
            // 
            // mnuPossiblyMissingChildReport
            // 
            this.mnuPossiblyMissingChildReport.Name = "mnuPossiblyMissingChildReport";
            this.mnuPossiblyMissingChildReport.Size = new System.Drawing.Size(230, 22);
            this.mnuPossiblyMissingChildReport.Text = "Possibly Missing Child Report";
            this.mnuPossiblyMissingChildReport.Click += new System.EventHandler(this.PossiblyMissingChildReportToolStripMenuItem_Click);
            // 
            // MnuAgedOver99Report
            // 
            this.MnuAgedOver99Report.Name = "MnuAgedOver99Report";
            this.MnuAgedOver99Report.Size = new System.Drawing.Size(230, 22);
            this.MnuAgedOver99Report.Text = "Aged over 99 Report";
            this.MnuAgedOver99Report.Click += new System.EventHandler(this.MnuAgedOver99Report_Click);
            // 
            // MnuSingleParentsReport
            // 
            this.MnuSingleParentsReport.Name = "MnuSingleParentsReport";
            this.MnuSingleParentsReport.Size = new System.Drawing.Size(230, 22);
            this.MnuSingleParentsReport.Text = "Single Parents Report";
            this.MnuSingleParentsReport.Click += new System.EventHandler(this.MnuSingleParentsReport_Click);
            // 
            // mnuExport
            // 
            this.mnuExport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuIndividualsToExcel,
            this.mnuFamiliesToExcel,
            this.mnuFactsToExcel,
            this.MnuExportLocations,
            this.mnuSourcesToExcel,
            this.toolStripSeparator12,
            this.mnuDataErrorsToExcel,
            this.mnuSurnamesToExcel,
            this.toolStripSeparator8,
            this.mnuLooseBirthsToExcel,
            this.mnuLooseDeathsToExcel,
            this.toolStripSeparator9,
            this.mnuTreetopsToExcel,
            this.mnuWorldWarsToExcel,
            this.toolStripSeparator13,
            this.mnuDNA_GEDCOM,
            this.toolStripSeparator15,
            this.mnuJSON});
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.Size = new System.Drawing.Size(53, 22);
            this.mnuExport.Text = "Export";
            // 
            // mnuIndividualsToExcel
            // 
            this.mnuIndividualsToExcel.Name = "mnuIndividualsToExcel";
            this.mnuIndividualsToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuIndividualsToExcel.Text = "Individuals to Excel";
            this.mnuIndividualsToExcel.Click += new System.EventHandler(this.IndividualsToExcelToolStripMenuItem_Click);
            // 
            // mnuFamiliesToExcel
            // 
            this.mnuFamiliesToExcel.Name = "mnuFamiliesToExcel";
            this.mnuFamiliesToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuFamiliesToExcel.Text = "Families to Excel";
            this.mnuFamiliesToExcel.Click += new System.EventHandler(this.FamiliesToExcelToolStripMenuItem_Click);
            // 
            // mnuFactsToExcel
            // 
            this.mnuFactsToExcel.Name = "mnuFactsToExcel";
            this.mnuFactsToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuFactsToExcel.Text = "Facts to Excel";
            this.mnuFactsToExcel.Click += new System.EventHandler(this.FactsToExcelToolStripMenuItem_Click);
            // 
            // MnuExportLocations
            // 
            this.MnuExportLocations.Name = "MnuExportLocations";
            this.MnuExportLocations.Size = new System.Drawing.Size(222, 22);
            this.MnuExportLocations.Text = "Locations to Excel";
            this.MnuExportLocations.Click += new System.EventHandler(this.MnuExportLocations_Click);
            // 
            // mnuSourcesToExcel
            // 
            this.mnuSourcesToExcel.Name = "mnuSourcesToExcel";
            this.mnuSourcesToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuSourcesToExcel.Text = "Sources to Excel";
            this.mnuSourcesToExcel.Click += new System.EventHandler(this.MnuSourcesToExcel_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuDataErrorsToExcel
            // 
            this.mnuDataErrorsToExcel.Name = "mnuDataErrorsToExcel";
            this.mnuDataErrorsToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuDataErrorsToExcel.Text = "Data Errors to Excel";
            this.mnuDataErrorsToExcel.Click += new System.EventHandler(this.MnuDataErrorsToExcel_Click);
            // 
            // mnuSurnamesToExcel
            // 
            this.mnuSurnamesToExcel.Name = "mnuSurnamesToExcel";
            this.mnuSurnamesToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuSurnamesToExcel.Text = "Surnames to Excel";
            this.mnuSurnamesToExcel.Click += new System.EventHandler(this.MnuSurnamesToExcel_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuLooseBirthsToExcel
            // 
            this.mnuLooseBirthsToExcel.Name = "mnuLooseBirthsToExcel";
            this.mnuLooseBirthsToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuLooseBirthsToExcel.Text = "Loose Births to Excel";
            this.mnuLooseBirthsToExcel.Click += new System.EventHandler(this.LooseBirthsToExcelToolStripMenuItem_Click);
            // 
            // mnuLooseDeathsToExcel
            // 
            this.mnuLooseDeathsToExcel.Name = "mnuLooseDeathsToExcel";
            this.mnuLooseDeathsToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuLooseDeathsToExcel.Text = "Loose Deaths to Excel";
            this.mnuLooseDeathsToExcel.Click += new System.EventHandler(this.LooseDeathsToExcelToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuTreetopsToExcel
            // 
            this.mnuTreetopsToExcel.Name = "mnuTreetopsToExcel";
            this.mnuTreetopsToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuTreetopsToExcel.Text = "Current Treetops to Excel";
            this.mnuTreetopsToExcel.Click += new System.EventHandler(this.MnuTreetopsToExcel_Click);
            // 
            // mnuWorldWarsToExcel
            // 
            this.mnuWorldWarsToExcel.Name = "mnuWorldWarsToExcel";
            this.mnuWorldWarsToExcel.Size = new System.Drawing.Size(222, 22);
            this.mnuWorldWarsToExcel.Text = "Current World Wars to Excel";
            this.mnuWorldWarsToExcel.Click += new System.EventHandler(this.MnuWorldWarsToExcel_Click);
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(219, 6);
            // 
            // mnuDNA_GEDCOM
            // 
            this.mnuDNA_GEDCOM.Name = "mnuDNA_GEDCOM";
            this.mnuDNA_GEDCOM.Size = new System.Drawing.Size(222, 22);
            this.mnuDNA_GEDCOM.Text = "Minimalist DNA GEDCOM";
            this.mnuDNA_GEDCOM.Click += new System.EventHandler(this.MnuDNA_GEDCOM_Click);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(219, 6);
            this.toolStripSeparator15.Visible = false;
            // 
            // mnuJSON
            // 
            this.mnuJSON.Name = "mnuJSON";
            this.mnuJSON.Size = new System.Drawing.Size(222, 22);
            this.mnuJSON.Text = "JSON for Visualisations";
            this.mnuJSON.Visible = false;
            this.mnuJSON.Click += new System.EventHandler(this.MnuJSON_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem,
            this.toolStripSeparator2,
            this.displayOptionsOnLoadToolStripMenuItem,
            this.resetToDefaultFormSizeToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            this.optionsToolStripMenuItem.Click += new System.EventHandler(this.OptionsToolStripMenuItem_Click);
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
            this.displayOptionsOnLoadToolStripMenuItem.Click += new System.EventHandler(this.DisplayOptionsOnLoadToolStripMenuItem_Click);
            // 
            // resetToDefaultFormSizeToolStripMenuItem
            // 
            this.resetToDefaultFormSizeToolStripMenuItem.Name = "resetToDefaultFormSizeToolStripMenuItem";
            this.resetToDefaultFormSizeToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.resetToDefaultFormSizeToolStripMenuItem.Text = "Reset to Default form size";
            this.resetToDefaultFormSizeToolStripMenuItem.Click += new System.EventHandler(this.ResetToDefaultFormSizeToolStripMenuItem_Click);
            // 
            // mnuMaps
            // 
            this.mnuMaps.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuShowTimeline,
            this.mnuLifelines,
            this.mnuPlaces,
            this.toolStripSeparator4,
            this.mnuLocationsGeocodeReport,
            this.toolStripSeparator10,
            this.mnuGeocodeLocations,
            this.mnuOSGeocoder,
            this.mnuLookupBlankFoundLocations});
            this.mnuMaps.Name = "mnuMaps";
            this.mnuMaps.Size = new System.Drawing.Size(48, 22);
            this.mnuMaps.Text = "Maps";
            // 
            // mnuShowTimeline
            // 
            this.mnuShowTimeline.Name = "mnuShowTimeline";
            this.mnuShowTimeline.Size = new System.Drawing.Size(284, 22);
            this.mnuShowTimeline.Text = "Show Timeline";
            this.mnuShowTimeline.Click += new System.EventHandler(this.MnuShowTimeline_Click);
            // 
            // mnuLifelines
            // 
            this.mnuLifelines.Name = "mnuLifelines";
            this.mnuLifelines.Size = new System.Drawing.Size(284, 22);
            this.mnuLifelines.Text = "Show Lifelines";
            this.mnuLifelines.Click += new System.EventHandler(this.MnuLifelines_Click);
            // 
            // mnuPlaces
            // 
            this.mnuPlaces.Name = "mnuPlaces";
            this.mnuPlaces.Size = new System.Drawing.Size(284, 22);
            this.mnuPlaces.Text = "Show Places";
            this.mnuPlaces.Click += new System.EventHandler(this.MnuPlaces_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(281, 6);
            // 
            // mnuLocationsGeocodeReport
            // 
            this.mnuLocationsGeocodeReport.Name = "mnuLocationsGeocodeReport";
            this.mnuLocationsGeocodeReport.Size = new System.Drawing.Size(284, 22);
            this.mnuLocationsGeocodeReport.Text = "Display Geocoded Locations";
            this.mnuLocationsGeocodeReport.Click += new System.EventHandler(this.LocationsGeocodeReportToolStripMenuItem_Click);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(281, 6);
            // 
            // mnuGeocodeLocations
            // 
            this.mnuGeocodeLocations.Name = "mnuGeocodeLocations";
            this.mnuGeocodeLocations.Size = new System.Drawing.Size(284, 22);
            this.mnuGeocodeLocations.Text = "Run Google Geocoder to Find Locations";
            this.mnuGeocodeLocations.Click += new System.EventHandler(this.MnuGeocodeLocations_Click);
            // 
            // mnuOSGeocoder
            // 
            this.mnuOSGeocoder.Name = "mnuOSGeocoder";
            this.mnuOSGeocoder.Size = new System.Drawing.Size(284, 22);
            this.mnuOSGeocoder.Text = "Run OS Geocoder to Find Locations";
            this.mnuOSGeocoder.Click += new System.EventHandler(this.MnuOSGeocoder_Click);
            // 
            // mnuLookupBlankFoundLocations
            // 
            this.mnuLookupBlankFoundLocations.Name = "mnuLookupBlankFoundLocations";
            this.mnuLookupBlankFoundLocations.Size = new System.Drawing.Size(284, 22);
            this.mnuLookupBlankFoundLocations.Text = "Lookup Blank Google Locations";
            this.mnuLookupBlankFoundLocations.Click += new System.EventHandler(this.MnuLookupBlankFoundLocations_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewOnlineManualToolStripMenuItem,
            this.onlineGuidesToUsingFTAnalyzerToolStripMenuItem,
            this.reportAnIssueToolStripMenuItem,
            this.facebookSupportGroupToolStripMenuItem,
            this.facebookUserGroupToolStripMenuItem,
            this.toolStripSeparator1,
            this.getGoogleAPIKeyToolStripMenuItem,
            this.googleAPISetupGuideToolStripMenuItem,
            this.toolStripSeparator14,
            this.privacyPolicyToolStripMenuItem,
            this.whatsNewToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 22);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // viewOnlineManualToolStripMenuItem
            // 
            this.viewOnlineManualToolStripMenuItem.Name = "viewOnlineManualToolStripMenuItem";
            this.viewOnlineManualToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.viewOnlineManualToolStripMenuItem.Text = "View Online Manual";
            this.viewOnlineManualToolStripMenuItem.Click += new System.EventHandler(this.ViewOnlineManualToolStripMenuItem_Click);
            // 
            // onlineGuidesToUsingFTAnalyzerToolStripMenuItem
            // 
            this.onlineGuidesToUsingFTAnalyzerToolStripMenuItem.Name = "onlineGuidesToUsingFTAnalyzerToolStripMenuItem";
            this.onlineGuidesToUsingFTAnalyzerToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.onlineGuidesToUsingFTAnalyzerToolStripMenuItem.Text = "Online Guides to Using FTAnalyzer";
            this.onlineGuidesToUsingFTAnalyzerToolStripMenuItem.Click += new System.EventHandler(this.OnlineGuidesToUsingFTAnalyzerToolStripMenuItem_Click);
            // 
            // reportAnIssueToolStripMenuItem
            // 
            this.reportAnIssueToolStripMenuItem.Name = "reportAnIssueToolStripMenuItem";
            this.reportAnIssueToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.reportAnIssueToolStripMenuItem.Text = "Report an Issue";
            this.reportAnIssueToolStripMenuItem.Click += new System.EventHandler(this.ReportAnIssueToolStripMenuItem_Click);
            // 
            // facebookSupportGroupToolStripMenuItem
            // 
            this.facebookSupportGroupToolStripMenuItem.Image = global::FTAnalyzer.Properties.Resources.flogo_rgb_hex_brc_site_250;
            this.facebookSupportGroupToolStripMenuItem.Name = "facebookSupportGroupToolStripMenuItem";
            this.facebookSupportGroupToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.facebookSupportGroupToolStripMenuItem.Text = "Facebook Support Page";
            this.facebookSupportGroupToolStripMenuItem.Click += new System.EventHandler(this.FacebookSupportGroupToolStripMenuItem_Click);
            // 
            // facebookUserGroupToolStripMenuItem
            // 
            this.facebookUserGroupToolStripMenuItem.Image = global::FTAnalyzer.Properties.Resources.flogo_rgb_hex_brc_site_250;
            this.facebookUserGroupToolStripMenuItem.Name = "facebookUserGroupToolStripMenuItem";
            this.facebookUserGroupToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.facebookUserGroupToolStripMenuItem.Text = "Facebook User Group";
            this.facebookUserGroupToolStripMenuItem.Click += new System.EventHandler(this.FacebookUserGroupToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(251, 6);
            // 
            // getGoogleAPIKeyToolStripMenuItem
            // 
            this.getGoogleAPIKeyToolStripMenuItem.Image = global::FTAnalyzer.Properties.Resources.GoogleMapsAPI;
            this.getGoogleAPIKeyToolStripMenuItem.Name = "getGoogleAPIKeyToolStripMenuItem";
            this.getGoogleAPIKeyToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.getGoogleAPIKeyToolStripMenuItem.Text = "Get Google API Key";
            this.getGoogleAPIKeyToolStripMenuItem.Click += new System.EventHandler(this.GetGoogleAPIKeyToolStripMenuItem_Click);
            // 
            // googleAPISetupGuideToolStripMenuItem
            // 
            this.googleAPISetupGuideToolStripMenuItem.Image = global::FTAnalyzer.Properties.Resources.GoogleMapsAPI;
            this.googleAPISetupGuideToolStripMenuItem.Name = "googleAPISetupGuideToolStripMenuItem";
            this.googleAPISetupGuideToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.googleAPISetupGuideToolStripMenuItem.Text = "Google API Setup Guide";
            this.googleAPISetupGuideToolStripMenuItem.Click += new System.EventHandler(this.GoogleAPISetupGuideToolStripMenuItem_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(251, 6);
            // 
            // privacyPolicyToolStripMenuItem
            // 
            this.privacyPolicyToolStripMenuItem.Name = "privacyPolicyToolStripMenuItem";
            this.privacyPolicyToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.privacyPolicyToolStripMenuItem.Text = "Privacy Policy";
            this.privacyPolicyToolStripMenuItem.Click += new System.EventHandler(this.PrivacyPolicyToolStripMenuItem_Click);
            // 
            // whatsNewToolStripMenuItem
            // 
            this.whatsNewToolStripMenuItem.Name = "whatsNewToolStripMenuItem";
            this.whatsNewToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.whatsNewToolStripMenuItem.Text = "What\'s New";
            this.whatsNewToolStripMenuItem.Click += new System.EventHandler(this.WhatsNewToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(254, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItem_Click);
            // 
            // mnuSetRoot
            // 
            this.mnuSetRoot.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.mnuSetRoot.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setAsRootToolStripMenuItem,
            this.viewNotesToolStripMenuItem});
            this.mnuSetRoot.Name = "mnuSetRoot";
            this.mnuSetRoot.Size = new System.Drawing.Size(174, 48);
            this.mnuSetRoot.Opened += new System.EventHandler(this.MnuSetRoot_Opened);
            // 
            // setAsRootToolStripMenuItem
            // 
            this.setAsRootToolStripMenuItem.Name = "setAsRootToolStripMenuItem";
            this.setAsRootToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.setAsRootToolStripMenuItem.Text = "Set As Root Person";
            this.setAsRootToolStripMenuItem.Click += new System.EventHandler(this.SetAsRootToolStripMenuItem_Click);
            // 
            // viewNotesToolStripMenuItem
            // 
            this.viewNotesToolStripMenuItem.Name = "viewNotesToolStripMenuItem";
            this.viewNotesToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.viewNotesToolStripMenuItem.Text = "View Notes";
            this.viewNotesToolStripMenuItem.Click += new System.EventHandler(this.ViewNotesToolStripMenuItem_Click);
            // 
            // tsCount
            // 
            this.tsCount.Name = "tsCount";
            this.tsCount.Size = new System.Drawing.Size(52, 17);
            this.tsCount.Text = "Count: 0";
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsCountLabel,
            this.tsHintsLabel,
            this.tspbTabProgress,
            this.tsStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 499);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1104, 22);
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
            // tspbTabProgress
            // 
            this.tspbTabProgress.Name = "tspbTabProgress";
            this.tspbTabProgress.Size = new System.Drawing.Size(200, 16);
            this.tspbTabProgress.Visible = false;
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(0, 17);
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
            this.dgRegions.RowHeadersWidth = 82;
            this.dgRegions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgRegions.Size = new System.Drawing.Size(1068, 422);
            this.dgRegions.TabIndex = 1;
            this.toolTips.SetToolTip(this.dgRegions, "Double click on Region name to see list of individuals with that Region.");
            this.dgRegions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgRegions_CellDoubleClick);
            this.dgRegions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgRegions_CellFormatting);
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
            this.dgCountries.RowHeadersWidth = 82;
            this.dgCountries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCountries.Size = new System.Drawing.Size(1068, 422);
            this.dgCountries.TabIndex = 0;
            this.toolTips.SetToolTip(this.dgCountries, "Double click on Country name to see list of individuals with that Country.");
            this.dgCountries.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgCountries_CellDoubleClick);
            this.dgCountries.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgCountries_CellFormatting);
            // 
            // cmbColourFamily
            // 
            this.cmbColourFamily.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbColourFamily.FormattingEnabled = true;
            this.cmbColourFamily.Location = new System.Drawing.Point(406, 49);
            this.cmbColourFamily.Name = "cmbColourFamily";
            this.cmbColourFamily.Size = new System.Drawing.Size(509, 21);
            this.cmbColourFamily.TabIndex = 60;
            this.toolTips.SetToolTip(this.cmbColourFamily, "Select a family to limit the reports to just that family");
            this.cmbColourFamily.Click += new System.EventHandler(this.CmbColourFamily_Click);
            // 
            // btnRandomSurnameColour
            // 
            this.btnRandomSurnameColour.Location = new System.Drawing.Point(622, 13);
            this.btnRandomSurnameColour.Name = "btnRandomSurnameColour";
            this.btnRandomSurnameColour.Size = new System.Drawing.Size(293, 25);
            this.btnRandomSurnameColour.TabIndex = 62;
            this.btnRandomSurnameColour.Text = "Select Random Surname from Direct Ancestor\'s Surnames";
            this.toolTips.SetToolTip(this.btnRandomSurnameColour, "Once selected click the appropriate report button to view the report. eg: UK Colo" +
        "ur Census Report.");
            this.btnRandomSurnameColour.UseVisualStyleBackColor = true;
            this.btnRandomSurnameColour.Click += new System.EventHandler(this.BtnRandomSurnameColour_Click);
            // 
            // ckbFactExclude
            // 
            this.ckbFactExclude.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbFactExclude.FormattingEnabled = true;
            this.ckbFactExclude.Location = new System.Drawing.Point(361, 137);
            this.ckbFactExclude.Name = "ckbFactExclude";
            this.ckbFactExclude.ScrollAlwaysVisible = true;
            this.ckbFactExclude.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ckbFactExclude.Size = new System.Drawing.Size(313, 214);
            this.ckbFactExclude.TabIndex = 28;
            this.toolTips.SetToolTip(this.ckbFactExclude, "Any fact types selected in this box excludes people who have this fact type from " +
        "report");
            this.ckbFactExclude.Visible = false;
            this.ckbFactExclude.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CkbFactExclude_MouseClick);
            // 
            // btnShowExclusions
            // 
            this.btnShowExclusions.Location = new System.Drawing.Point(327, 238);
            this.btnShowExclusions.Name = "btnShowExclusions";
            this.btnShowExclusions.Size = new System.Drawing.Size(28, 51);
            this.btnShowExclusions.TabIndex = 33;
            this.btnShowExclusions.Text = "=>";
            this.toolTips.SetToolTip(this.btnShowExclusions, "Show Exclusions");
            this.btnShowExclusions.UseVisualStyleBackColor = true;
            this.btnShowExclusions.Click += new System.EventHandler(this.BtnShowExclusions_Click);
            // 
            // dgDataErrors
            // 
            this.dgDataErrors.AllowUserToAddRows = false;
            this.dgDataErrors.AllowUserToDeleteRows = false;
            this.dgDataErrors.AllowUserToOrderColumns = true;
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
            this.dgDataErrors.Location = new System.Drawing.Point(6, 166);
            this.dgDataErrors.Name = "dgDataErrors";
            this.dgDataErrors.ReadOnly = true;
            this.dgDataErrors.RowHeadersWidth = 82;
            this.dgDataErrors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDataErrors.ShowCellToolTips = false;
            this.dgDataErrors.ShowEditingIcon = false;
            this.dgDataErrors.Size = new System.Drawing.Size(1070, 274);
            this.dgDataErrors.TabIndex = 6;
            this.toolTips.SetToolTip(this.dgDataErrors, "Double click to see list of facts for that individual");
            this.dgDataErrors.VirtualMode = true;
            this.dgDataErrors.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgDataErrors_CellDoubleClick);
            // 
            // tbDuplicateScore
            // 
            this.tbDuplicateScore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbDuplicateScore.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.tbDuplicateScore.Location = new System.Drawing.Point(447, 3);
            this.tbDuplicateScore.Minimum = 1;
            this.tbDuplicateScore.Name = "tbDuplicateScore";
            this.tbDuplicateScore.Size = new System.Drawing.Size(368, 45);
            this.tbDuplicateScore.TabIndex = 22;
            this.tbDuplicateScore.TickFrequency = 5;
            this.toolTips.SetToolTip(this.tbDuplicateScore, "Adjust Slider to right to limit results to more likely matches");
            this.tbDuplicateScore.Value = 1;
            this.tbDuplicateScore.Scroll += new System.EventHandler(this.TbDuplicateScore_Scroll);
            // 
            // chkLCRootPersonConfirm
            // 
            this.chkLCRootPersonConfirm.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkLCRootPersonConfirm.Location = new System.Drawing.Point(30, 147);
            this.chkLCRootPersonConfirm.Name = "chkLCRootPersonConfirm";
            this.chkLCRootPersonConfirm.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkLCRootPersonConfirm.Size = new System.Drawing.Size(407, 24);
            this.chkLCRootPersonConfirm.TabIndex = 4;
            this.chkLCRootPersonConfirm.Text = "rootperson";
            this.chkLCRootPersonConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTips.SetToolTip(this.chkLCRootPersonConfirm, "The Lost Cousins Data includes a relationship field please make sure the root per" +
        "son relates to the root person on the Lost Cousins website.");
            this.chkLCRootPersonConfirm.UseVisualStyleBackColor = true;
            this.chkLCRootPersonConfirm.CheckedChanged += new System.EventHandler(this.ChkLCRootPersonConfirm_CheckedChanged);
            // 
            // dgCheckAncestors
            // 
            this.dgCheckAncestors.AllowUserToAddRows = false;
            this.dgCheckAncestors.AllowUserToDeleteRows = false;
            this.dgCheckAncestors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgCheckAncestors.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgCheckAncestors.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgCheckAncestors.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgCheckAncestors.Location = new System.Drawing.Point(-1, 84);
            this.dgCheckAncestors.Name = "dgCheckAncestors";
            this.dgCheckAncestors.ReadOnly = true;
            this.dgCheckAncestors.RowHeadersWidth = 82;
            this.dgCheckAncestors.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCheckAncestors.ShowCellToolTips = false;
            this.dgCheckAncestors.ShowEditingIcon = false;
            this.dgCheckAncestors.Size = new System.Drawing.Size(1062, 302);
            this.dgCheckAncestors.TabIndex = 7;
            this.toolTips.SetToolTip(this.dgCheckAncestors, "Double click to see list of facts for that individual");
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(712, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 13);
            this.label13.TabIndex = 24;
            this.label13.Text = "Aggressive Match";
            this.toolTips.SetToolTip(this.label13, "Will produce duplicates in list when the two individuals are a very close match t" +
        "o each other - only those with highest duplicate match score");
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(444, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(69, 13);
            this.label12.TabIndex = 23;
            this.label12.Text = "Loose Match";
            this.toolTips.SetToolTip(this.label12, "Will produce duplicates in list when the two individuals decent but vague match t" +
        "o each other - Lowest duplicate match score");
            // 
            // chkIgnoreUnnamedTwins
            // 
            this.chkIgnoreUnnamedTwins.AutoSize = true;
            this.chkIgnoreUnnamedTwins.Checked = true;
            this.chkIgnoreUnnamedTwins.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreUnnamedTwins.Location = new System.Drawing.Point(246, 64);
            this.chkIgnoreUnnamedTwins.Name = "chkIgnoreUnnamedTwins";
            this.chkIgnoreUnnamedTwins.Size = new System.Drawing.Size(87, 17);
            this.chkIgnoreUnnamedTwins.TabIndex = 29;
            this.chkIgnoreUnnamedTwins.Text = "Ignore Twins";
            this.toolTips.SetToolTip(this.chkIgnoreUnnamedTwins, "Ignores duplicates where forename is unknown");
            this.chkIgnoreUnnamedTwins.UseVisualStyleBackColor = true;
            this.chkIgnoreUnnamedTwins.Visible = false;
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
            this.tabWorldWars.Controls.Add(this.ckbMilitaryOnly);
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
            this.tabWorldWars.Size = new System.Drawing.Size(1088, 460);
            this.tabWorldWars.TabIndex = 8;
            this.tabWorldWars.Text = "World Wars";
            this.tabWorldWars.ToolTipText = "Find men of fighting age during WWI & WWII";
            this.tabWorldWars.UseVisualStyleBackColor = true;
            // 
            // ckbMilitaryOnly
            // 
            this.ckbMilitaryOnly.AutoSize = true;
            this.ckbMilitaryOnly.Location = new System.Drawing.Point(279, 105);
            this.ckbMilitaryOnly.Name = "ckbMilitaryOnly";
            this.ckbMilitaryOnly.Size = new System.Drawing.Size(257, 17);
            this.ckbMilitaryOnly.TabIndex = 33;
            this.ckbMilitaryOnly.Text = "Limit Results to only those men with Military Facts";
            this.ckbMilitaryOnly.UseVisualStyleBackColor = true;
            // 
            // ckbWDIgnoreLocations
            // 
            this.ckbWDIgnoreLocations.AutoSize = true;
            this.ckbWDIgnoreLocations.Checked = true;
            this.ckbWDIgnoreLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbWDIgnoreLocations.Location = new System.Drawing.Point(8, 105);
            this.ckbWDIgnoreLocations.Name = "ckbWDIgnoreLocations";
            this.ckbWDIgnoreLocations.Size = new System.Drawing.Size(252, 17);
            this.ckbWDIgnoreLocations.TabIndex = 32;
            this.ckbWDIgnoreLocations.Text = "Include Unknown Countries in World Wars Filter";
            this.ckbWDIgnoreLocations.UseVisualStyleBackColor = true;
            this.ckbWDIgnoreLocations.CheckedChanged += new System.EventHandler(this.CkbWDIgnoreLocations_CheckedChanged);
            // 
            // btnWWII
            // 
            this.btnWWII.Location = new System.Drawing.Point(758, 58);
            this.btnWWII.Name = "btnWWII";
            this.btnWWII.Size = new System.Drawing.Size(95, 25);
            this.btnWWII.TabIndex = 31;
            this.btnWWII.Text = "World War II";
            this.btnWWII.UseVisualStyleBackColor = true;
            this.btnWWII.Click += new System.EventHandler(this.BtnWWII_Click);
            // 
            // btnWWI
            // 
            this.btnWWI.Location = new System.Drawing.Point(650, 58);
            this.btnWWI.Name = "btnWWI";
            this.btnWWI.Size = new System.Drawing.Size(95, 25);
            this.btnWWI.TabIndex = 30;
            this.btnWWI.Text = "World War I";
            this.btnWWI.UseVisualStyleBackColor = true;
            this.btnWWI.Click += new System.EventHandler(this.BtnWWI_Click);
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
            this.dgWorldWars.Location = new System.Drawing.Point(0, 128);
            this.dgWorldWars.MultiSelect = false;
            this.dgWorldWars.Name = "dgWorldWars";
            this.dgWorldWars.ReadOnly = true;
            this.dgWorldWars.RowHeadersWidth = 82;
            this.dgWorldWars.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgWorldWars.Size = new System.Drawing.Size(1082, 312);
            this.dgWorldWars.TabIndex = 29;
            this.dgWorldWars.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgWorldWars_CellDoubleClick);
            this.dgWorldWars.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgWorldWars_MouseDown);
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
            // wardeadRelation
            // 
            this.wardeadRelation.Location = new System.Drawing.Point(270, 3);
            this.wardeadRelation.Margin = new System.Windows.Forms.Padding(6);
            this.wardeadRelation.MarriedToDB = true;
            this.wardeadRelation.Name = "wardeadRelation";
            this.wardeadRelation.Size = new System.Drawing.Size(322, 100);
            this.wardeadRelation.TabIndex = 26;
            // 
            // wardeadCountry
            // 
            this.wardeadCountry.Location = new System.Drawing.Point(8, 25);
            this.wardeadCountry.Margin = new System.Windows.Forms.Padding(6);
            this.wardeadCountry.Name = "wardeadCountry";
            this.wardeadCountry.Size = new System.Drawing.Size(256, 74);
            this.wardeadCountry.TabIndex = 25;
            this.wardeadCountry.Title = "Default Country";
            this.wardeadCountry.UKEnabled = true;
            // 
            // ctxViewNotes
            // 
            this.ctxViewNotes.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.ctxViewNotes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewNotes});
            this.ctxViewNotes.Name = "contextMenuStrip1";
            this.ctxViewNotes.Size = new System.Drawing.Size(134, 26);
            this.ctxViewNotes.Opening += new System.ComponentModel.CancelEventHandler(this.CtxViewNotes_Opening);
            // 
            // mnuViewNotes
            // 
            this.mnuViewNotes.Name = "mnuViewNotes";
            this.mnuViewNotes.Size = new System.Drawing.Size(133, 22);
            this.mnuViewNotes.Text = "View Notes";
            this.mnuViewNotes.Click += new System.EventHandler(this.MnuViewNotes_Click);
            // 
            // tabTreetops
            // 
            this.tabTreetops.Controls.Add(this.ckbTTIncludeOnlyOneParent);
            this.tabTreetops.Controls.Add(this.dgTreeTops);
            this.tabTreetops.Controls.Add(this.ckbTTIgnoreLocations);
            this.tabTreetops.Controls.Add(this.btnTreeTops);
            this.tabTreetops.Controls.Add(this.label8);
            this.tabTreetops.Controls.Add(this.txtTreetopsSurname);
            this.tabTreetops.Controls.Add(this.treetopsRelation);
            this.tabTreetops.Controls.Add(this.treetopsCountry);
            this.tabTreetops.Location = new System.Drawing.Point(4, 22);
            this.tabTreetops.Name = "tabTreetops";
            this.tabTreetops.Size = new System.Drawing.Size(1088, 460);
            this.tabTreetops.TabIndex = 7;
            this.tabTreetops.Text = "Treetops";
            this.tabTreetops.UseVisualStyleBackColor = true;
            // 
            // ckbTTIncludeOnlyOneParent
            // 
            this.ckbTTIncludeOnlyOneParent.AutoSize = true;
            this.ckbTTIncludeOnlyOneParent.Checked = true;
            this.ckbTTIncludeOnlyOneParent.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTTIncludeOnlyOneParent.Location = new System.Drawing.Point(279, 105);
            this.ckbTTIncludeOnlyOneParent.Name = "ckbTTIncludeOnlyOneParent";
            this.ckbTTIncludeOnlyOneParent.Size = new System.Drawing.Size(273, 17);
            this.ckbTTIncludeOnlyOneParent.TabIndex = 29;
            this.ckbTTIncludeOnlyOneParent.Text = "Include Individuals that have only one parent known";
            this.ckbTTIncludeOnlyOneParent.UseVisualStyleBackColor = true;
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
            this.dgTreeTops.Location = new System.Drawing.Point(0, 128);
            this.dgTreeTops.MultiSelect = false;
            this.dgTreeTops.Name = "dgTreeTops";
            this.dgTreeTops.ReadOnly = true;
            this.dgTreeTops.RowHeadersWidth = 82;
            this.dgTreeTops.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgTreeTops.Size = new System.Drawing.Size(1082, 312);
            this.dgTreeTops.TabIndex = 28;
            this.dgTreeTops.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgTreeTops_CellDoubleClick);
            this.dgTreeTops.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgTreeTops_MouseDown);
            // 
            // ckbTTIgnoreLocations
            // 
            this.ckbTTIgnoreLocations.AutoSize = true;
            this.ckbTTIgnoreLocations.Checked = true;
            this.ckbTTIgnoreLocations.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbTTIgnoreLocations.Location = new System.Drawing.Point(8, 105);
            this.ckbTTIgnoreLocations.Name = "ckbTTIgnoreLocations";
            this.ckbTTIgnoreLocations.Size = new System.Drawing.Size(238, 17);
            this.ckbTTIgnoreLocations.TabIndex = 27;
            this.ckbTTIgnoreLocations.Text = "Include Unknown Countries in Treetops Filter";
            this.ckbTTIgnoreLocations.UseVisualStyleBackColor = true;
            this.ckbTTIgnoreLocations.CheckedChanged += new System.EventHandler(this.CkbTTIgnoreLocations_CheckedChanged);
            // 
            // btnTreeTops
            // 
            this.btnTreeTops.Location = new System.Drawing.Point(650, 58);
            this.btnTreeTops.Name = "btnTreeTops";
            this.btnTreeTops.Size = new System.Drawing.Size(201, 25);
            this.btnTreeTops.TabIndex = 25;
            this.btnTreeTops.Text = "Show People at top of tree";
            this.btnTreeTops.UseVisualStyleBackColor = true;
            this.btnTreeTops.Click += new System.EventHandler(this.BtnTreeTops_Click);
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
            this.treetopsRelation.Location = new System.Drawing.Point(270, 3);
            this.treetopsRelation.Margin = new System.Windows.Forms.Padding(6);
            this.treetopsRelation.MarriedToDB = true;
            this.treetopsRelation.Name = "treetopsRelation";
            this.treetopsRelation.Size = new System.Drawing.Size(322, 96);
            this.treetopsRelation.TabIndex = 12;
            // 
            // treetopsCountry
            // 
            this.treetopsCountry.Location = new System.Drawing.Point(8, 25);
            this.treetopsCountry.Margin = new System.Windows.Forms.Padding(6);
            this.treetopsCountry.Name = "treetopsCountry";
            this.treetopsCountry.Size = new System.Drawing.Size(256, 74);
            this.treetopsCountry.TabIndex = 11;
            this.treetopsCountry.Title = "Default Country";
            this.treetopsCountry.UKEnabled = true;
            // 
            // tabColourReports
            // 
            this.tabColourReports.Controls.Add(this.groupBox7);
            this.tabColourReports.Controls.Add(this.btnRandomSurnameColour);
            this.tabColourReports.Controls.Add(this.label14);
            this.tabColourReports.Controls.Add(this.cmbColourFamily);
            this.tabColourReports.Controls.Add(this.groupBox3);
            this.tabColourReports.Controls.Add(this.btnColourBMD);
            this.tabColourReports.Controls.Add(this.label10);
            this.tabColourReports.Controls.Add(this.txtColouredSurname);
            this.tabColourReports.Controls.Add(this.relTypesColoured);
            this.tabColourReports.Location = new System.Drawing.Point(4, 22);
            this.tabColourReports.Name = "tabColourReports";
            this.tabColourReports.Size = new System.Drawing.Size(1088, 460);
            this.tabColourReports.TabIndex = 12;
            this.tabColourReports.Text = "Research Suggestions";
            this.tabColourReports.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.btnAdvancedMissingData);
            this.groupBox7.Controls.Add(this.btnStandardMissingData);
            this.groupBox7.Location = new System.Drawing.Point(423, 116);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(409, 109);
            this.groupBox7.TabIndex = 63;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Missing Data Reports";
            this.groupBox7.Visible = false;
            // 
            // btnAdvancedMissingData
            // 
            this.btnAdvancedMissingData.Location = new System.Drawing.Point(207, 19);
            this.btnAdvancedMissingData.Name = "btnAdvancedMissingData";
            this.btnAdvancedMissingData.Size = new System.Drawing.Size(195, 23);
            this.btnAdvancedMissingData.TabIndex = 40;
            this.btnAdvancedMissingData.Text = "Advanced Missing Data Report";
            this.btnAdvancedMissingData.UseVisualStyleBackColor = true;
            this.btnAdvancedMissingData.Click += new System.EventHandler(this.BtnAdvancedMissingData_Click);
            // 
            // btnStandardMissingData
            // 
            this.btnStandardMissingData.Location = new System.Drawing.Point(6, 19);
            this.btnStandardMissingData.Name = "btnStandardMissingData";
            this.btnStandardMissingData.Size = new System.Drawing.Size(195, 23);
            this.btnStandardMissingData.TabIndex = 39;
            this.btnStandardMissingData.Text = "Standard Missing Data Report";
            this.btnStandardMissingData.UseVisualStyleBackColor = true;
            this.btnStandardMissingData.Click += new System.EventHandler(this.BtnStandardMissingData_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(339, 52);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(61, 13);
            this.label14.TabIndex = 61;
            this.label14.Text = "Family Filter";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ckbIgnoreNoDeathDate);
            this.groupBox3.Controls.Add(this.ckbIgnoreNoBirthDate);
            this.groupBox3.Controls.Add(this.btnIrishColourCensus);
            this.groupBox3.Controls.Add(this.btnCanadianColourCensus);
            this.groupBox3.Controls.Add(this.btnUKColourCensus);
            this.groupBox3.Controls.Add(this.btnUSColourCensus);
            this.groupBox3.Location = new System.Drawing.Point(8, 116);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(409, 109);
            this.groupBox3.TabIndex = 36;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Census Suggestions Reports";
            // 
            // ckbIgnoreNoDeathDate
            // 
            this.ckbIgnoreNoDeathDate.AutoSize = true;
            this.ckbIgnoreNoDeathDate.Location = new System.Drawing.Point(208, 77);
            this.ckbIgnoreNoDeathDate.Name = "ckbIgnoreNoDeathDate";
            this.ckbIgnoreNoDeathDate.Size = new System.Drawing.Size(200, 17);
            this.ckbIgnoreNoDeathDate.TabIndex = 67;
            this.ckbIgnoreNoDeathDate.Text = "Ignore Individuals with no death date";
            this.ckbIgnoreNoDeathDate.UseVisualStyleBackColor = true;
            // 
            // ckbIgnoreNoBirthDate
            // 
            this.ckbIgnoreNoBirthDate.AutoSize = true;
            this.ckbIgnoreNoBirthDate.Location = new System.Drawing.Point(6, 77);
            this.ckbIgnoreNoBirthDate.Name = "ckbIgnoreNoBirthDate";
            this.ckbIgnoreNoBirthDate.Size = new System.Drawing.Size(193, 17);
            this.ckbIgnoreNoBirthDate.TabIndex = 66;
            this.ckbIgnoreNoBirthDate.Text = "Ignore Individuals with no birth date";
            this.ckbIgnoreNoBirthDate.UseVisualStyleBackColor = true;
            // 
            // btnIrishColourCensus
            // 
            this.btnIrishColourCensus.Location = new System.Drawing.Point(207, 19);
            this.btnIrishColourCensus.Name = "btnIrishColourCensus";
            this.btnIrishColourCensus.Size = new System.Drawing.Size(195, 23);
            this.btnIrishColourCensus.TabIndex = 39;
            this.btnIrishColourCensus.Text = "View Irish Colour Census Report";
            this.btnIrishColourCensus.UseVisualStyleBackColor = true;
            this.btnIrishColourCensus.Click += new System.EventHandler(this.BtnIrishColourCensus_Click);
            // 
            // btnCanadianColourCensus
            // 
            this.btnCanadianColourCensus.Location = new System.Drawing.Point(207, 48);
            this.btnCanadianColourCensus.Name = "btnCanadianColourCensus";
            this.btnCanadianColourCensus.Size = new System.Drawing.Size(195, 23);
            this.btnCanadianColourCensus.TabIndex = 41;
            this.btnCanadianColourCensus.Text = "View Canadian Colour Census Report";
            this.btnCanadianColourCensus.UseVisualStyleBackColor = true;
            this.btnCanadianColourCensus.Click += new System.EventHandler(this.BtnCanadianColourCensus_Click);
            // 
            // btnUKColourCensus
            // 
            this.btnUKColourCensus.Location = new System.Drawing.Point(6, 19);
            this.btnUKColourCensus.Name = "btnUKColourCensus";
            this.btnUKColourCensus.Size = new System.Drawing.Size(195, 23);
            this.btnUKColourCensus.TabIndex = 38;
            this.btnUKColourCensus.Text = "View UK Colour Census Report";
            this.btnUKColourCensus.UseVisualStyleBackColor = true;
            this.btnUKColourCensus.Click += new System.EventHandler(this.BtnUKColourCensus_Click);
            // 
            // btnUSColourCensus
            // 
            this.btnUSColourCensus.Location = new System.Drawing.Point(6, 48);
            this.btnUSColourCensus.Name = "btnUSColourCensus";
            this.btnUSColourCensus.Size = new System.Drawing.Size(195, 23);
            this.btnUSColourCensus.TabIndex = 40;
            this.btnUSColourCensus.Text = "View US Colour Census Report";
            this.btnUSColourCensus.UseVisualStyleBackColor = true;
            this.btnUSColourCensus.Click += new System.EventHandler(this.BtnUSColourCensus_Click);
            // 
            // btnColourBMD
            // 
            this.btnColourBMD.Location = new System.Drawing.Point(58, 231);
            this.btnColourBMD.Name = "btnColourBMD";
            this.btnColourBMD.Size = new System.Drawing.Size(307, 23);
            this.btnColourBMD.TabIndex = 42;
            this.btnColourBMD.Text = "View Colour Birth/Marriage/Death Report";
            this.btnColourBMD.UseVisualStyleBackColor = true;
            this.btnColourBMD.Click += new System.EventHandler(this.BtnColourBMD_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(339, 19);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(49, 13);
            this.label10.TabIndex = 59;
            this.label10.Text = "Surname";
            // 
            // txtColouredSurname
            // 
            this.txtColouredSurname.Location = new System.Drawing.Point(406, 16);
            this.txtColouredSurname.Name = "txtColouredSurname";
            this.txtColouredSurname.Size = new System.Drawing.Size(201, 20);
            this.txtColouredSurname.TabIndex = 30;
            this.txtColouredSurname.TextChanged += new System.EventHandler(this.TxtColouredSurname_TextChanged);
            // 
            // relTypesColoured
            // 
            this.relTypesColoured.Location = new System.Drawing.Point(8, 8);
            this.relTypesColoured.Margin = new System.Windows.Forms.Padding(6);
            this.relTypesColoured.MarriedToDB = true;
            this.relTypesColoured.Name = "relTypesColoured";
            this.relTypesColoured.Size = new System.Drawing.Size(325, 102);
            this.relTypesColoured.TabIndex = 26;
            this.relTypesColoured.RelationTypesChanged += new System.EventHandler(this.RelTypesColoured_RelationTypesChanged);
            // 
            // tabLostCousins
            // 
            this.tabLostCousins.Controls.Add(this.LCSubTabs);
            this.tabLostCousins.Location = new System.Drawing.Point(4, 22);
            this.tabLostCousins.Name = "tabLostCousins";
            this.tabLostCousins.Padding = new System.Windows.Forms.Padding(3);
            this.tabLostCousins.Size = new System.Drawing.Size(1088, 460);
            this.tabLostCousins.TabIndex = 5;
            this.tabLostCousins.Text = "Lost Cousins";
            this.tabLostCousins.UseVisualStyleBackColor = true;
            // 
            // LCSubTabs
            // 
            this.LCSubTabs.Controls.Add(this.LCReportsTab);
            this.LCSubTabs.Controls.Add(this.LCUpdatesTab);
            this.LCSubTabs.Controls.Add(this.LCVerifyTab);
            this.LCSubTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LCSubTabs.Location = new System.Drawing.Point(3, 3);
            this.LCSubTabs.Name = "LCSubTabs";
            this.LCSubTabs.SelectedIndex = 0;
            this.LCSubTabs.Size = new System.Drawing.Size(1082, 454);
            this.LCSubTabs.TabIndex = 0;
            // 
            // LCReportsTab
            // 
            this.LCReportsTab.Controls.Add(this.Referrals);
            this.LCReportsTab.Controls.Add(this.btnLCnoCensus);
            this.LCReportsTab.Controls.Add(this.btnLCDuplicates);
            this.LCReportsTab.Controls.Add(this.btnLCMissingCountry);
            this.LCReportsTab.Controls.Add(this.btnLC1940USA);
            this.LCReportsTab.Controls.Add(this.rtbLostCousins);
            this.LCReportsTab.Controls.Add(this.linkLabel2);
            this.LCReportsTab.Controls.Add(this.btnLC1911EW);
            this.LCReportsTab.Controls.Add(this.LabLostCousinsWeb);
            this.LCReportsTab.Controls.Add(this.ckbShowLCEntered);
            this.LCReportsTab.Controls.Add(this.btnLC1841EW);
            this.LCReportsTab.Controls.Add(this.btnLC1911Ireland);
            this.LCReportsTab.Controls.Add(this.btnLC1880USA);
            this.LCReportsTab.Controls.Add(this.btnLC1881EW);
            this.LCReportsTab.Controls.Add(this.btnLC1881Canada);
            this.LCReportsTab.Controls.Add(this.btnLC1881Scot);
            this.LCReportsTab.Controls.Add(this.relTypesLC);
            this.LCReportsTab.Location = new System.Drawing.Point(4, 22);
            this.LCReportsTab.Name = "LCReportsTab";
            this.LCReportsTab.Padding = new System.Windows.Forms.Padding(3);
            this.LCReportsTab.Size = new System.Drawing.Size(1074, 428);
            this.LCReportsTab.TabIndex = 0;
            this.LCReportsTab.Text = "Reports";
            this.LCReportsTab.UseVisualStyleBackColor = true;
            // 
            // Referrals
            // 
            this.Referrals.Controls.Add(this.ckbReferralInCommon);
            this.Referrals.Controls.Add(this.btnReferrals);
            this.Referrals.Controls.Add(this.cmbReferrals);
            this.Referrals.Controls.Add(this.label11);
            this.Referrals.Location = new System.Drawing.Point(6, 319);
            this.Referrals.Name = "Referrals";
            this.Referrals.Size = new System.Drawing.Size(498, 83);
            this.Referrals.TabIndex = 40;
            this.Referrals.TabStop = false;
            this.Referrals.Text = "Referrals";
            // 
            // ckbReferralInCommon
            // 
            this.ckbReferralInCommon.AutoSize = true;
            this.ckbReferralInCommon.Location = new System.Drawing.Point(11, 49);
            this.ckbReferralInCommon.Name = "ckbReferralInCommon";
            this.ckbReferralInCommon.Size = new System.Drawing.Size(150, 17);
            this.ckbReferralInCommon.TabIndex = 3;
            this.ckbReferralInCommon.Text = "Limit to Common Relatives";
            this.ckbReferralInCommon.UseVisualStyleBackColor = true;
            // 
            // btnReferrals
            // 
            this.btnReferrals.Location = new System.Drawing.Point(272, 45);
            this.btnReferrals.Name = "btnReferrals";
            this.btnReferrals.Size = new System.Drawing.Size(220, 23);
            this.btnReferrals.TabIndex = 2;
            this.btnReferrals.Text = "Generate Referral Report for this Individual";
            this.btnReferrals.UseVisualStyleBackColor = true;
            this.btnReferrals.Click += new System.EventHandler(this.BtnReferrals_Click);
            // 
            // cmbReferrals
            // 
            this.cmbReferrals.FormattingEnabled = true;
            this.cmbReferrals.Location = new System.Drawing.Point(97, 18);
            this.cmbReferrals.Name = "cmbReferrals";
            this.cmbReferrals.Size = new System.Drawing.Size(395, 21);
            this.cmbReferrals.TabIndex = 1;
            this.cmbReferrals.Click += new System.EventHandler(this.CmbReferrals_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 21);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(85, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Select Individual";
            // 
            // btnLCnoCensus
            // 
            this.btnLCnoCensus.Location = new System.Drawing.Point(342, 246);
            this.btnLCnoCensus.Name = "btnLCnoCensus";
            this.btnLCnoCensus.Size = new System.Drawing.Size(162, 27);
            this.btnLCnoCensus.TabIndex = 39;
            this.btnLCnoCensus.Text = "Lost Cousins w/bad Census";
            this.btnLCnoCensus.UseVisualStyleBackColor = true;
            this.btnLCnoCensus.Click += new System.EventHandler(this.BtnLCnoCensus_Click);
            // 
            // btnLCDuplicates
            // 
            this.btnLCDuplicates.Location = new System.Drawing.Point(174, 246);
            this.btnLCDuplicates.Name = "btnLCDuplicates";
            this.btnLCDuplicates.Size = new System.Drawing.Size(162, 27);
            this.btnLCDuplicates.TabIndex = 38;
            this.btnLCDuplicates.Text = "Lost Cousins Duplicate Facts";
            this.btnLCDuplicates.UseVisualStyleBackColor = true;
            this.btnLCDuplicates.Click += new System.EventHandler(this.BtnLCDuplicates_Click);
            // 
            // btnLCMissingCountry
            // 
            this.btnLCMissingCountry.Location = new System.Drawing.Point(6, 246);
            this.btnLCMissingCountry.Name = "btnLCMissingCountry";
            this.btnLCMissingCountry.Size = new System.Drawing.Size(162, 27);
            this.btnLCMissingCountry.TabIndex = 37;
            this.btnLCMissingCountry.Text = "Lost Cousins with no Country";
            this.btnLCMissingCountry.UseVisualStyleBackColor = true;
            this.btnLCMissingCountry.Click += new System.EventHandler(this.BtnLCMissingCountry_Click);
            // 
            // btnLC1940USA
            // 
            this.btnLC1940USA.Location = new System.Drawing.Point(342, 181);
            this.btnLC1940USA.Name = "btnLC1940USA";
            this.btnLC1940USA.Size = new System.Drawing.Size(162, 27);
            this.btnLC1940USA.TabIndex = 35;
            this.btnLC1940USA.Text = "1940 US Census";
            this.btnLC1940USA.UseVisualStyleBackColor = true;
            this.btnLC1940USA.Click += new System.EventHandler(this.BtnLC1940USA_Click);
            // 
            // rtbLostCousins
            // 
            this.rtbLostCousins.BackColor = System.Drawing.SystemColors.Window;
            this.rtbLostCousins.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLostCousins.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLostCousins.Location = new System.Drawing.Point(536, 6);
            this.rtbLostCousins.Name = "rtbLostCousins";
            this.rtbLostCousins.ReadOnly = true;
            this.rtbLostCousins.Size = new System.Drawing.Size(528, 362);
            this.rtbLostCousins.TabIndex = 34;
            this.rtbLostCousins.TabStop = false;
            this.rtbLostCousins.Text = "";
            // 
            // linkLabel2
            // 
            this.linkLabel2.AutoSize = true;
            this.linkLabel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel2.Location = new System.Drawing.Point(726, 371);
            this.linkLabel2.Name = "linkLabel2";
            this.linkLabel2.Size = new System.Drawing.Size(174, 16);
            this.linkLabel2.TabIndex = 33;
            this.linkLabel2.TabStop = true;
            this.linkLabel2.Text = "Visit the Lost Cousins Forum";
            this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LinkLabel2_LinkClicked);
            // 
            // btnLC1911EW
            // 
            this.btnLC1911EW.Location = new System.Drawing.Point(6, 181);
            this.btnLC1911EW.Name = "btnLC1911EW";
            this.btnLC1911EW.Size = new System.Drawing.Size(162, 27);
            this.btnLC1911EW.TabIndex = 32;
            this.btnLC1911EW.Text = "1911 England && Wales Census";
            this.btnLC1911EW.UseVisualStyleBackColor = true;
            this.btnLC1911EW.Click += new System.EventHandler(this.BtnLC1911EW_Click);
            // 
            // LabLostCousinsWeb
            // 
            this.LabLostCousinsWeb.AutoSize = true;
            this.LabLostCousinsWeb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabLostCousinsWeb.Location = new System.Drawing.Point(534, 371);
            this.LabLostCousinsWeb.Name = "LabLostCousinsWeb";
            this.LabLostCousinsWeb.Size = new System.Drawing.Size(186, 16);
            this.LabLostCousinsWeb.TabIndex = 31;
            this.LabLostCousinsWeb.TabStop = true;
            this.LabLostCousinsWeb.Text = "Visit the Lost Cousins Website";
            this.LabLostCousinsWeb.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LabLostCousinsWeb_Click);
            this.LabLostCousinsWeb.Click += new System.EventHandler(this.LabLostCousinsWeb_Click);
            // 
            // ckbShowLCEntered
            // 
            this.ckbShowLCEntered.AutoSize = true;
            this.ckbShowLCEntered.Location = new System.Drawing.Point(6, 214);
            this.ckbShowLCEntered.Name = "ckbShowLCEntered";
            this.ckbShowLCEntered.Size = new System.Drawing.Size(415, 17);
            this.ckbShowLCEntered.TabIndex = 30;
            this.ckbShowLCEntered.Text = "Show already entered to Lost Cousins (unticked = show those to yet to be entered)" +
    "";
            this.ckbShowLCEntered.UseVisualStyleBackColor = true;
            // 
            // btnLC1841EW
            // 
            this.btnLC1841EW.Location = new System.Drawing.Point(6, 148);
            this.btnLC1841EW.Name = "btnLC1841EW";
            this.btnLC1841EW.Size = new System.Drawing.Size(162, 27);
            this.btnLC1841EW.TabIndex = 29;
            this.btnLC1841EW.Text = "1841 England && Wales Census";
            this.btnLC1841EW.UseVisualStyleBackColor = true;
            this.btnLC1841EW.Click += new System.EventHandler(this.BtnLC1841EW_Click);
            // 
            // btnLC1911Ireland
            // 
            this.btnLC1911Ireland.Location = new System.Drawing.Point(174, 148);
            this.btnLC1911Ireland.Name = "btnLC1911Ireland";
            this.btnLC1911Ireland.Size = new System.Drawing.Size(162, 27);
            this.btnLC1911Ireland.TabIndex = 28;
            this.btnLC1911Ireland.Text = "1911 Ireland Census";
            this.btnLC1911Ireland.UseVisualStyleBackColor = true;
            this.btnLC1911Ireland.Click += new System.EventHandler(this.BtnLC1911Ireland_Click);
            // 
            // btnLC1880USA
            // 
            this.btnLC1880USA.Location = new System.Drawing.Point(342, 148);
            this.btnLC1880USA.Name = "btnLC1880USA";
            this.btnLC1880USA.Size = new System.Drawing.Size(162, 27);
            this.btnLC1880USA.TabIndex = 27;
            this.btnLC1880USA.Text = "1880 US Census";
            this.btnLC1880USA.UseVisualStyleBackColor = true;
            this.btnLC1880USA.Click += new System.EventHandler(this.BtnLC1880USA_Click);
            // 
            // btnLC1881EW
            // 
            this.btnLC1881EW.Location = new System.Drawing.Point(6, 115);
            this.btnLC1881EW.Name = "btnLC1881EW";
            this.btnLC1881EW.Size = new System.Drawing.Size(162, 27);
            this.btnLC1881EW.TabIndex = 26;
            this.btnLC1881EW.Text = "1881 England && Wales Census";
            this.btnLC1881EW.UseVisualStyleBackColor = true;
            this.btnLC1881EW.Click += new System.EventHandler(this.BtnLC1881EW_Click);
            // 
            // btnLC1881Canada
            // 
            this.btnLC1881Canada.Location = new System.Drawing.Point(174, 181);
            this.btnLC1881Canada.Name = "btnLC1881Canada";
            this.btnLC1881Canada.Size = new System.Drawing.Size(162, 27);
            this.btnLC1881Canada.TabIndex = 25;
            this.btnLC1881Canada.Text = "1881 Canada Census";
            this.btnLC1881Canada.UseVisualStyleBackColor = true;
            this.btnLC1881Canada.Click += new System.EventHandler(this.BtnLC1881Canada_Click);
            // 
            // btnLC1881Scot
            // 
            this.btnLC1881Scot.Location = new System.Drawing.Point(174, 115);
            this.btnLC1881Scot.Name = "btnLC1881Scot";
            this.btnLC1881Scot.Size = new System.Drawing.Size(162, 27);
            this.btnLC1881Scot.TabIndex = 24;
            this.btnLC1881Scot.Text = "1881 Scotland Census";
            this.btnLC1881Scot.UseVisualStyleBackColor = true;
            this.btnLC1881Scot.Click += new System.EventHandler(this.BtnLC1881Scot_Click);
            // 
            // relTypesLC
            // 
            this.relTypesLC.Location = new System.Drawing.Point(6, 6);
            this.relTypesLC.Margin = new System.Windows.Forms.Padding(6);
            this.relTypesLC.MarriedToDB = true;
            this.relTypesLC.Name = "relTypesLC";
            this.relTypesLC.Size = new System.Drawing.Size(325, 103);
            this.relTypesLC.TabIndex = 36;
            this.relTypesLC.RelationTypesChanged += new System.EventHandler(this.RelTypesLC_RelationTypesChanged);
            // 
            // LCUpdatesTab
            // 
            this.LCUpdatesTab.Controls.Add(this.btnViewInvalidRefs);
            this.LCUpdatesTab.Controls.Add(this.btnLCPotentialUploads);
            this.LCUpdatesTab.Controls.Add(this.chkLCRootPersonConfirm);
            this.LCUpdatesTab.Controls.Add(this.label21);
            this.LCUpdatesTab.Controls.Add(this.rtbLCUpdateData);
            this.LCUpdatesTab.Controls.Add(this.groupBox8);
            this.LCUpdatesTab.Controls.Add(this.btnUpdateLostCousinsWebsite);
            this.LCUpdatesTab.Controls.Add(this.rtbLCoutput);
            this.LCUpdatesTab.Location = new System.Drawing.Point(4, 22);
            this.LCUpdatesTab.Name = "LCUpdatesTab";
            this.LCUpdatesTab.Padding = new System.Windows.Forms.Padding(3);
            this.LCUpdatesTab.Size = new System.Drawing.Size(1074, 428);
            this.LCUpdatesTab.TabIndex = 1;
            this.LCUpdatesTab.Text = "Updates";
            this.LCUpdatesTab.UseVisualStyleBackColor = true;
            // 
            // btnViewInvalidRefs
            // 
            this.btnViewInvalidRefs.Location = new System.Drawing.Point(152, 174);
            this.btnViewInvalidRefs.Name = "btnViewInvalidRefs";
            this.btnViewInvalidRefs.Size = new System.Drawing.Size(138, 23);
            this.btnViewInvalidRefs.TabIndex = 40;
            this.btnViewInvalidRefs.Text = "View Invalid Census Refs";
            this.btnViewInvalidRefs.UseVisualStyleBackColor = true;
            this.btnViewInvalidRefs.Click += new System.EventHandler(this.BtnViewInvalidRefs_Click);
            // 
            // btnLCPotentialUploads
            // 
            this.btnLCPotentialUploads.Location = new System.Drawing.Point(6, 174);
            this.btnLCPotentialUploads.Name = "btnLCPotentialUploads";
            this.btnLCPotentialUploads.Size = new System.Drawing.Size(138, 23);
            this.btnLCPotentialUploads.TabIndex = 39;
            this.btnLCPotentialUploads.Text = "View Potential Uploads";
            this.btnLCPotentialUploads.UseVisualStyleBackColor = true;
            this.btnLCPotentialUploads.Click += new System.EventHandler(this.BtnLCPotentialUploads_Click);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label21.Location = new System.Drawing.Point(468, 12);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(383, 16);
            this.label21.TabIndex = 37;
            this.label21.Text = "Census Records with valid Reference to upload to Lost Cousins";
            // 
            // rtbLCUpdateData
            // 
            this.rtbLCUpdateData.BackColor = System.Drawing.SystemColors.Window;
            this.rtbLCUpdateData.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbLCUpdateData.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLCUpdateData.ForeColor = System.Drawing.Color.Red;
            this.rtbLCUpdateData.Location = new System.Drawing.Point(471, 31);
            this.rtbLCUpdateData.Name = "rtbLCUpdateData";
            this.rtbLCUpdateData.ReadOnly = true;
            this.rtbLCUpdateData.Size = new System.Drawing.Size(391, 169);
            this.rtbLCUpdateData.TabIndex = 36;
            this.rtbLCUpdateData.TabStop = false;
            this.rtbLCUpdateData.Text = "Please Login to see data to update";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.btnLCLogin);
            this.groupBox8.Controls.Add(this.label20);
            this.groupBox8.Controls.Add(this.label19);
            this.groupBox8.Controls.Add(this.txtLCEmail);
            this.groupBox8.Controls.Add(this.txtLCPassword);
            this.groupBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox8.Location = new System.Drawing.Point(30, 22);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(407, 118);
            this.groupBox8.TabIndex = 1;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Lost Cousins Login Details";
            // 
            // btnLCLogin
            // 
            this.btnLCLogin.BackColor = System.Drawing.Color.Red;
            this.btnLCLogin.Location = new System.Drawing.Point(310, 68);
            this.btnLCLogin.Name = "btnLCLogin";
            this.btnLCLogin.Size = new System.Drawing.Size(75, 25);
            this.btnLCLogin.TabIndex = 3;
            this.btnLCLogin.Text = "Login";
            this.btnLCLogin.UseVisualStyleBackColor = false;
            this.btnLCLogin.Click += new System.EventHandler(this.BtnLCLogin_Click);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(34, 72);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(68, 16);
            this.label20.TabIndex = 3;
            this.label20.Text = "Password";
            this.label20.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(6, 31);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(96, 16);
            this.label19.TabIndex = 2;
            this.label19.Text = "Email Address";
            this.label19.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // txtLCEmail
            // 
            this.txtLCEmail.Location = new System.Drawing.Point(108, 28);
            this.txtLCEmail.Name = "txtLCEmail";
            this.txtLCEmail.Size = new System.Drawing.Size(277, 22);
            this.txtLCEmail.TabIndex = 1;
            this.txtLCEmail.TextChanged += new System.EventHandler(this.TxtLCEmail_TextChanged);
            // 
            // txtLCPassword
            // 
            this.txtLCPassword.Location = new System.Drawing.Point(108, 69);
            this.txtLCPassword.Name = "txtLCPassword";
            this.txtLCPassword.PasswordChar = '*';
            this.txtLCPassword.Size = new System.Drawing.Size(196, 22);
            this.txtLCPassword.TabIndex = 2;
            this.txtLCPassword.TextChanged += new System.EventHandler(this.TxtLCPassword_TextChanged);
            // 
            // btnUpdateLostCousinsWebsite
            // 
            this.btnUpdateLostCousinsWebsite.Enabled = false;
            this.btnUpdateLostCousinsWebsite.Location = new System.Drawing.Point(299, 174);
            this.btnUpdateLostCousinsWebsite.Name = "btnUpdateLostCousinsWebsite";
            this.btnUpdateLostCousinsWebsite.Size = new System.Drawing.Size(138, 23);
            this.btnUpdateLostCousinsWebsite.TabIndex = 5;
            this.btnUpdateLostCousinsWebsite.Text = "Update Lost Cousins site";
            this.btnUpdateLostCousinsWebsite.UseVisualStyleBackColor = true;
            this.btnUpdateLostCousinsWebsite.Visible = false;
            this.btnUpdateLostCousinsWebsite.Click += new System.EventHandler(this.BtnUpdateLostCousinsWebsite_Click);
            // 
            // rtbLCoutput
            // 
            this.rtbLCoutput.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbLCoutput.BackColor = System.Drawing.SystemColors.Control;
            this.rtbLCoutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbLCoutput.Location = new System.Drawing.Point(3, 203);
            this.rtbLCoutput.Name = "rtbLCoutput";
            this.rtbLCoutput.ReadOnly = true;
            this.rtbLCoutput.Size = new System.Drawing.Size(1056, 180);
            this.rtbLCoutput.TabIndex = 38;
            this.rtbLCoutput.TabStop = false;
            this.rtbLCoutput.Text = "";
            this.rtbLCoutput.TextChanged += new System.EventHandler(this.RtbLCoutput_TextChanged);
            // 
            // LCVerifyTab
            // 
            this.LCVerifyTab.Controls.Add(this.rtbCheckAncestors);
            this.LCVerifyTab.Controls.Add(this.dgCheckAncestors);
            this.LCVerifyTab.Controls.Add(this.btnCheckMyAncestors);
            this.LCVerifyTab.Controls.Add(this.lblCheckAncestors);
            this.LCVerifyTab.Location = new System.Drawing.Point(4, 22);
            this.LCVerifyTab.Name = "LCVerifyTab";
            this.LCVerifyTab.Size = new System.Drawing.Size(1074, 428);
            this.LCVerifyTab.TabIndex = 2;
            this.LCVerifyTab.Text = "Verification";
            this.LCVerifyTab.UseVisualStyleBackColor = true;
            // 
            // rtbCheckAncestors
            // 
            this.rtbCheckAncestors.BackColor = System.Drawing.SystemColors.Window;
            this.rtbCheckAncestors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbCheckAncestors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbCheckAncestors.ForeColor = System.Drawing.Color.Red;
            this.rtbCheckAncestors.Location = new System.Drawing.Point(334, 12);
            this.rtbCheckAncestors.Name = "rtbCheckAncestors";
            this.rtbCheckAncestors.ReadOnly = true;
            this.rtbCheckAncestors.Size = new System.Drawing.Size(733, 66);
            this.rtbCheckAncestors.TabIndex = 37;
            this.rtbCheckAncestors.TabStop = false;
            this.rtbCheckAncestors.Text = "Please Login to see data to update";
            this.rtbCheckAncestors.TextChanged += new System.EventHandler(this.RtbCheckAncestors_TextChanged);
            // 
            // btnCheckMyAncestors
            // 
            this.btnCheckMyAncestors.BackColor = System.Drawing.Color.Red;
            this.btnCheckMyAncestors.Location = new System.Drawing.Point(15, 40);
            this.btnCheckMyAncestors.Name = "btnCheckMyAncestors";
            this.btnCheckMyAncestors.Size = new System.Drawing.Size(247, 23);
            this.btnCheckMyAncestors.TabIndex = 1;
            this.btnCheckMyAncestors.Text = "Load My Ancestors Page to check for Errors";
            this.btnCheckMyAncestors.UseVisualStyleBackColor = false;
            this.btnCheckMyAncestors.Click += new System.EventHandler(this.BtnCheckMyAncestors_Click);
            // 
            // lblCheckAncestors
            // 
            this.lblCheckAncestors.AutoSize = true;
            this.lblCheckAncestors.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCheckAncestors.Location = new System.Drawing.Point(12, 12);
            this.lblCheckAncestors.Name = "lblCheckAncestors";
            this.lblCheckAncestors.Size = new System.Drawing.Size(316, 16);
            this.lblCheckAncestors.TabIndex = 0;
            this.lblCheckAncestors.Text = "Not Currently Logged in Use Updates Page to Login";
            // 
            // tabCensus
            // 
            this.tabCensus.Controls.Add(this.groupBox2);
            this.tabCensus.Controls.Add(this.groupBox9);
            this.tabCensus.Location = new System.Drawing.Point(4, 22);
            this.tabCensus.Name = "tabCensus";
            this.tabCensus.Padding = new System.Windows.Forms.Padding(3);
            this.tabCensus.Size = new System.Drawing.Size(1088, 460);
            this.tabCensus.TabIndex = 0;
            this.tabCensus.Text = "Census";
            this.tabCensus.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnAliveOnDate);
            this.groupBox2.Controls.Add(this.txtAliveDates);
            this.groupBox2.Controls.Add(this.label22);
            this.groupBox2.Controls.Add(this.chkAnyCensusYear);
            this.groupBox2.Controls.Add(this.groupBox10);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.chkExcludeUnknownBirths);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtCensusSurname);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.udAgeFilter);
            this.groupBox2.Controls.Add(this.cenDate);
            this.groupBox2.Controls.Add(this.relTypesCensus);
            this.groupBox2.Location = new System.Drawing.Point(8, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(963, 290);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Census Search Reports";
            // 
            // btnAliveOnDate
            // 
            this.btnAliveOnDate.Location = new System.Drawing.Point(643, 124);
            this.btnAliveOnDate.Name = "btnAliveOnDate";
            this.btnAliveOnDate.Size = new System.Drawing.Size(300, 25);
            this.btnAliveOnDate.TabIndex = 41;
            this.btnAliveOnDate.Text = "Show Individuals possibly alive on above date(s)";
            this.btnAliveOnDate.UseVisualStyleBackColor = true;
            this.btnAliveOnDate.Click += new System.EventHandler(this.BtnAliveOnDate_Click);
            // 
            // txtAliveDates
            // 
            this.txtAliveDates.Location = new System.Drawing.Point(711, 84);
            this.txtAliveDates.Name = "txtAliveDates";
            this.txtAliveDates.Size = new System.Drawing.Size(232, 20);
            this.txtAliveDates.TabIndex = 40;
            this.txtAliveDates.Text = "Enter valid GEDCOM date/date range";
            this.txtAliveDates.Enter += new System.EventHandler(this.TxtAliveDates_Enter);
            this.txtAliveDates.Validating += new System.ComponentModel.CancelEventHandler(this.TxtAliveDates_Validating);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(640, 87);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(67, 13);
            this.label22.TabIndex = 39;
            this.label22.Text = "Alive Dates: ";
            // 
            // chkAnyCensusYear
            // 
            this.chkAnyCensusYear.AutoSize = true;
            this.chkAnyCensusYear.Location = new System.Drawing.Point(339, 129);
            this.chkAnyCensusYear.Name = "chkAnyCensusYear";
            this.chkAnyCensusYear.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAnyCensusYear.Size = new System.Drawing.Size(292, 17);
            this.chkAnyCensusYear.TabIndex = 36;
            this.chkAnyCensusYear.Text = "Include ALL census years for Census Reference reports ";
            this.chkAnyCensusYear.UseVisualStyleBackColor = true;
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.btnShowCensusMissing);
            this.groupBox10.Controls.Add(this.btnShowCensusEntered);
            this.groupBox10.Controls.Add(this.btnRandomSurnameEntered);
            this.groupBox10.Controls.Add(this.btnRandomSurnameMissing);
            this.groupBox10.Location = new System.Drawing.Point(9, 157);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(948, 63);
            this.groupBox10.TabIndex = 35;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "Census Record Reports";
            // 
            // btnShowCensusMissing
            // 
            this.btnShowCensusMissing.Location = new System.Drawing.Point(6, 22);
            this.btnShowCensusMissing.Name = "btnShowCensusMissing";
            this.btnShowCensusMissing.Size = new System.Drawing.Size(150, 25);
            this.btnShowCensusMissing.TabIndex = 39;
            this.btnShowCensusMissing.Text = "Show Not Found on Census";
            this.btnShowCensusMissing.UseVisualStyleBackColor = true;
            this.btnShowCensusMissing.Click += new System.EventHandler(this.BtnShowCensus_Click);
            // 
            // btnShowCensusEntered
            // 
            this.btnShowCensusEntered.Location = new System.Drawing.Point(162, 22);
            this.btnShowCensusEntered.Name = "btnShowCensusEntered";
            this.btnShowCensusEntered.Size = new System.Drawing.Size(154, 25);
            this.btnShowCensusEntered.TabIndex = 38;
            this.btnShowCensusEntered.Text = "Show Found on Census";
            this.btnShowCensusEntered.UseVisualStyleBackColor = true;
            this.btnShowCensusEntered.Click += new System.EventHandler(this.BtnShowCensus_Click);
            // 
            // btnRandomSurnameEntered
            // 
            this.btnRandomSurnameEntered.Location = new System.Drawing.Point(322, 22);
            this.btnRandomSurnameEntered.Name = "btnRandomSurnameEntered";
            this.btnRandomSurnameEntered.Size = new System.Drawing.Size(300, 25);
            this.btnRandomSurnameEntered.TabIndex = 37;
            this.btnRandomSurnameEntered.Text = "Show Found Random Surname from Direct Ancestors";
            this.btnRandomSurnameEntered.UseVisualStyleBackColor = true;
            this.btnRandomSurnameEntered.Click += new System.EventHandler(this.BtnRandomSurname_Click);
            // 
            // btnRandomSurnameMissing
            // 
            this.btnRandomSurnameMissing.Location = new System.Drawing.Point(634, 22);
            this.btnRandomSurnameMissing.Name = "btnRandomSurnameMissing";
            this.btnRandomSurnameMissing.Size = new System.Drawing.Size(301, 25);
            this.btnRandomSurnameMissing.TabIndex = 36;
            this.btnRandomSurnameMissing.Text = "Show Not Found Random Surname from Direct Ancestors";
            this.btnRandomSurnameMissing.UseVisualStyleBackColor = true;
            this.btnRandomSurnameMissing.Click += new System.EventHandler(this.BtnRandomSurname_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btnInconsistentLocations);
            this.groupBox4.Controls.Add(this.btnUnrecognisedCensusRef);
            this.groupBox4.Controls.Add(this.btnIncompleteCensusRef);
            this.groupBox4.Controls.Add(this.btnMissingCensusRefs);
            this.groupBox4.Controls.Add(this.btnCensusRefs);
            this.groupBox4.Location = new System.Drawing.Point(9, 226);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(948, 53);
            this.groupBox4.TabIndex = 34;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Census Reference Reports";
            // 
            // btnInconsistentLocations
            // 
            this.btnInconsistentLocations.Location = new System.Drawing.Point(634, 19);
            this.btnInconsistentLocations.Name = "btnInconsistentLocations";
            this.btnInconsistentLocations.Size = new System.Drawing.Size(301, 25);
            this.btnInconsistentLocations.TabIndex = 29;
            this.btnInconsistentLocations.Text = "Inconsistent census locations for families with same census ref";
            this.btnInconsistentLocations.UseVisualStyleBackColor = true;
            this.btnInconsistentLocations.Click += new System.EventHandler(this.BtnInconsistentLocations_Click);
            // 
            // btnUnrecognisedCensusRef
            // 
            this.btnUnrecognisedCensusRef.Location = new System.Drawing.Point(322, 19);
            this.btnUnrecognisedCensusRef.Name = "btnUnrecognisedCensusRef";
            this.btnUnrecognisedCensusRef.Size = new System.Drawing.Size(144, 25);
            this.btnUnrecognisedCensusRef.TabIndex = 8;
            this.btnUnrecognisedCensusRef.Text = "Unrecognised Census Refs";
            this.btnUnrecognisedCensusRef.UseVisualStyleBackColor = true;
            this.btnUnrecognisedCensusRef.Click += new System.EventHandler(this.BtnUnrecognisedCensusRef_Click);
            // 
            // btnIncompleteCensusRef
            // 
            this.btnIncompleteCensusRef.Location = new System.Drawing.Point(162, 19);
            this.btnIncompleteCensusRef.Name = "btnIncompleteCensusRef";
            this.btnIncompleteCensusRef.Size = new System.Drawing.Size(154, 25);
            this.btnIncompleteCensusRef.TabIndex = 7;
            this.btnIncompleteCensusRef.Text = "Incomplete Census Refs";
            this.btnIncompleteCensusRef.UseVisualStyleBackColor = true;
            this.btnIncompleteCensusRef.Click += new System.EventHandler(this.BtnIncompleteCensusRef_Click);
            // 
            // btnMissingCensusRefs
            // 
            this.btnMissingCensusRefs.Location = new System.Drawing.Point(472, 19);
            this.btnMissingCensusRefs.Name = "btnMissingCensusRefs";
            this.btnMissingCensusRefs.Size = new System.Drawing.Size(150, 25);
            this.btnMissingCensusRefs.TabIndex = 6;
            this.btnMissingCensusRefs.Text = "Missing Census Refs";
            this.btnMissingCensusRefs.UseVisualStyleBackColor = true;
            this.btnMissingCensusRefs.Click += new System.EventHandler(this.BtnMissingCensusRefs_Click);
            // 
            // btnCensusRefs
            // 
            this.btnCensusRefs.Location = new System.Drawing.Point(6, 19);
            this.btnCensusRefs.Name = "btnCensusRefs";
            this.btnCensusRefs.Size = new System.Drawing.Size(150, 25);
            this.btnCensusRefs.TabIndex = 5;
            this.btnCensusRefs.Text = "Good Census Refs";
            this.btnCensusRefs.UseVisualStyleBackColor = true;
            this.btnCensusRefs.Click += new System.EventHandler(this.BtnCensusRefs_Click);
            // 
            // chkExcludeUnknownBirths
            // 
            this.chkExcludeUnknownBirths.AutoSize = true;
            this.chkExcludeUnknownBirths.Location = new System.Drawing.Point(340, 63);
            this.chkExcludeUnknownBirths.Name = "chkExcludeUnknownBirths";
            this.chkExcludeUnknownBirths.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkExcludeUnknownBirths.Size = new System.Drawing.Size(238, 17);
            this.chkExcludeUnknownBirths.TabIndex = 31;
            this.chkExcludeUnknownBirths.Text = "Exclude Individuals with unknown birth dates";
            this.chkExcludeUnknownBirths.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(640, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 30;
            this.label1.Text = "Surname";
            // 
            // txtCensusSurname
            // 
            this.txtCensusSurname.Location = new System.Drawing.Point(711, 36);
            this.txtCensusSurname.Name = "txtCensusSurname";
            this.txtCensusSurname.Size = new System.Drawing.Size(232, 20);
            this.txtCensusSurname.TabIndex = 29;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(340, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 13);
            this.label2.TabIndex = 26;
            this.label2.Text = "Exclude individuals over the age of ";
            // 
            // udAgeFilter
            // 
            this.udAgeFilter.Location = new System.Drawing.Point(521, 36);
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
            this.udAgeFilter.TabIndex = 25;
            this.udAgeFilter.Value = new decimal(new int[] {
            90,
            0,
            0,
            0});
            // 
            // cenDate
            // 
            this.cenDate.AutoSize = true;
            this.cenDate.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.cenDate.Country = "";
            this.cenDate.Location = new System.Drawing.Point(8, 122);
            this.cenDate.Margin = new System.Windows.Forms.Padding(6);
            this.cenDate.Name = "cenDate";
            this.cenDate.Size = new System.Drawing.Size(234, 27);
            this.cenDate.TabIndex = 28;
            // 
            // relTypesCensus
            // 
            this.relTypesCensus.Location = new System.Drawing.Point(9, 19);
            this.relTypesCensus.Margin = new System.Windows.Forms.Padding(6);
            this.relTypesCensus.MarriedToDB = true;
            this.relTypesCensus.Name = "relTypesCensus";
            this.relTypesCensus.Size = new System.Drawing.Size(325, 99);
            this.relTypesCensus.TabIndex = 27;
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.groupBox11);
            this.groupBox9.Controls.Add(this.groupBox1);
            this.groupBox9.Controls.Add(this.groupBox5);
            this.groupBox9.Controls.Add(this.groupBox6);
            this.groupBox9.Location = new System.Drawing.Point(8, 320);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(963, 93);
            this.groupBox9.TabIndex = 32;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Census Reports that don\'t use filters above";
            // 
            // groupBox11
            // 
            this.groupBox11.Controls.Add(this.BtnAutoCreatedCensusFacts);
            this.groupBox11.Controls.Add(this.BtnProblemCensusFacts);
            this.groupBox11.Location = new System.Drawing.Point(637, 19);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(320, 59);
            this.groupBox11.TabIndex = 33;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "Census Facts";
            // 
            // BtnAutoCreatedCensusFacts
            // 
            this.BtnAutoCreatedCensusFacts.Location = new System.Drawing.Point(164, 21);
            this.BtnAutoCreatedCensusFacts.Name = "BtnAutoCreatedCensusFacts";
            this.BtnAutoCreatedCensusFacts.Size = new System.Drawing.Size(150, 23);
            this.BtnAutoCreatedCensusFacts.TabIndex = 39;
            this.BtnAutoCreatedCensusFacts.Text = "Auto Created Census Facts";
            this.BtnAutoCreatedCensusFacts.UseVisualStyleBackColor = true;
            this.BtnAutoCreatedCensusFacts.Click += new System.EventHandler(this.BtnCensusAutoCreatedFacts_Click);
            // 
            // BtnProblemCensusFacts
            // 
            this.BtnProblemCensusFacts.Location = new System.Drawing.Point(6, 21);
            this.BtnProblemCensusFacts.Name = "BtnProblemCensusFacts";
            this.BtnProblemCensusFacts.Size = new System.Drawing.Size(150, 23);
            this.BtnProblemCensusFacts.TabIndex = 38;
            this.BtnProblemCensusFacts.Text = "Problem Census Facts";
            this.BtnProblemCensusFacts.UseVisualStyleBackColor = true;
            this.BtnProblemCensusFacts.Click += new System.EventHandler(this.BtnCensusProblemFacts_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnDuplicateCensus);
            this.groupBox1.Controls.Add(this.btnMissingCensusLocation);
            this.groupBox1.Location = new System.Drawing.Point(8, 207);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(325, 88);
            this.groupBox1.TabIndex = 22;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Potential Census Fact Problems";
            // 
            // btnDuplicateCensus
            // 
            this.btnDuplicateCensus.Location = new System.Drawing.Point(165, 19);
            this.btnDuplicateCensus.Name = "btnDuplicateCensus";
            this.btnDuplicateCensus.Size = new System.Drawing.Size(150, 25);
            this.btnDuplicateCensus.TabIndex = 6;
            this.btnDuplicateCensus.Text = "Duplicate Census Facts";
            this.btnDuplicateCensus.UseVisualStyleBackColor = true;
            this.btnDuplicateCensus.Click += new System.EventHandler(this.BtnDuplicateCensus_Click);
            // 
            // btnMissingCensusLocation
            // 
            this.btnMissingCensusLocation.Location = new System.Drawing.Point(9, 19);
            this.btnMissingCensusLocation.Name = "btnMissingCensusLocation";
            this.btnMissingCensusLocation.Size = new System.Drawing.Size(150, 25);
            this.btnMissingCensusLocation.TabIndex = 5;
            this.btnMissingCensusLocation.Text = "Missing Census Locations";
            this.btnMissingCensusLocation.UseVisualStyleBackColor = true;
            this.btnMissingCensusLocation.Click += new System.EventHandler(this.BtnMissingCensusLocation_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btnMismatchedChildrenStatus);
            this.groupBox5.Controls.Add(this.btnNoChildrenStatus);
            this.groupBox5.Location = new System.Drawing.Point(6, 19);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(327, 59);
            this.groupBox5.TabIndex = 32;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "1911 UK Census";
            // 
            // btnMismatchedChildrenStatus
            // 
            this.btnMismatchedChildrenStatus.Location = new System.Drawing.Point(165, 19);
            this.btnMismatchedChildrenStatus.Name = "btnMismatchedChildrenStatus";
            this.btnMismatchedChildrenStatus.Size = new System.Drawing.Size(154, 25);
            this.btnMismatchedChildrenStatus.TabIndex = 7;
            this.btnMismatchedChildrenStatus.Text = "Mismatched Children Status";
            this.btnMismatchedChildrenStatus.UseVisualStyleBackColor = true;
            this.btnMismatchedChildrenStatus.Click += new System.EventHandler(this.BtnMismatchedChildrenStatus_Click);
            // 
            // btnNoChildrenStatus
            // 
            this.btnNoChildrenStatus.Location = new System.Drawing.Point(9, 19);
            this.btnNoChildrenStatus.Name = "btnNoChildrenStatus";
            this.btnNoChildrenStatus.Size = new System.Drawing.Size(150, 25);
            this.btnNoChildrenStatus.TabIndex = 6;
            this.btnNoChildrenStatus.Text = "Missing Children Status";
            this.btnNoChildrenStatus.UseVisualStyleBackColor = true;
            this.btnNoChildrenStatus.Click += new System.EventHandler(this.BtnNoChildrenStatus_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.btnReportUnrecognised);
            this.groupBox6.Location = new System.Drawing.Point(363, 19);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(244, 59);
            this.groupBox6.TabIndex = 31;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Export Missing/Unrecognised data to File";
            // 
            // btnReportUnrecognised
            // 
            this.btnReportUnrecognised.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnReportUnrecognised.Location = new System.Drawing.Point(6, 19);
            this.btnReportUnrecognised.Name = "btnReportUnrecognised";
            this.btnReportUnrecognised.Size = new System.Drawing.Size(224, 25);
            this.btnReportUnrecognised.TabIndex = 30;
            this.btnReportUnrecognised.Text = "Export Unrecognised/Missing Census Refs";
            this.btnReportUnrecognised.UseVisualStyleBackColor = true;
            this.btnReportUnrecognised.Click += new System.EventHandler(this.BtnReportUnrecognised_Click);
            // 
            // tabLocations
            // 
            this.tabLocations.Controls.Add(this.btnModernOSMap);
            this.tabLocations.Controls.Add(this.btnOldOSMap);
            this.tabLocations.Controls.Add(this.btnShowMap);
            this.tabLocations.Controls.Add(this.tabCtrlLocations);
            this.tabLocations.Location = new System.Drawing.Point(4, 22);
            this.tabLocations.Name = "tabLocations";
            this.tabLocations.Padding = new System.Windows.Forms.Padding(3);
            this.tabLocations.Size = new System.Drawing.Size(1088, 460);
            this.tabLocations.TabIndex = 4;
            this.tabLocations.Text = "Locations";
            this.tabLocations.UseVisualStyleBackColor = true;
            // 
            // btnModernOSMap
            // 
            this.btnModernOSMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModernOSMap.Location = new System.Drawing.Point(709, 2);
            this.btnModernOSMap.Name = "btnModernOSMap";
            this.btnModernOSMap.Size = new System.Drawing.Size(125, 22);
            this.btnModernOSMap.TabIndex = 5;
            this.btnModernOSMap.Text = "Show Modern OS Map";
            this.btnModernOSMap.UseVisualStyleBackColor = true;
            this.btnModernOSMap.Visible = false;
            this.btnModernOSMap.Click += new System.EventHandler(this.BtnOSMap_Click);
            // 
            // btnOldOSMap
            // 
            this.btnOldOSMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOldOSMap.Location = new System.Drawing.Point(850, 2);
            this.btnOldOSMap.Name = "btnOldOSMap";
            this.btnOldOSMap.Size = new System.Drawing.Size(104, 22);
            this.btnOldOSMap.TabIndex = 3;
            this.btnOldOSMap.Text = "Show Old OS Map";
            this.btnOldOSMap.UseVisualStyleBackColor = true;
            this.btnOldOSMap.Click += new System.EventHandler(this.BtnOSMap_Click);
            // 
            // btnShowMap
            // 
            this.btnShowMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowMap.Location = new System.Drawing.Point(970, 2);
            this.btnShowMap.Name = "btnShowMap";
            this.btnShowMap.Size = new System.Drawing.Size(104, 22);
            this.btnShowMap.TabIndex = 2;
            this.btnShowMap.Text = "Show Google Map";
            this.btnShowMap.UseVisualStyleBackColor = true;
            this.btnShowMap.Click += new System.EventHandler(this.BtnShowMap_Click);
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
            this.tabCtrlLocations.Size = new System.Drawing.Size(1082, 454);
            this.tabCtrlLocations.TabIndex = 0;
            this.tabCtrlLocations.SelectedIndexChanged += new System.EventHandler(this.TabCtrlLocations_SelectedIndexChanged);
            this.tabCtrlLocations.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabCtrlLocations_Selecting);
            // 
            // tabTreeView
            // 
            this.tabTreeView.Controls.Add(this.treeViewLocations);
            this.tabTreeView.Location = new System.Drawing.Point(4, 22);
            this.tabTreeView.Name = "tabTreeView";
            this.tabTreeView.Padding = new System.Windows.Forms.Padding(3);
            this.tabTreeView.Size = new System.Drawing.Size(1074, 428);
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
            this.treeViewLocations.Size = new System.Drawing.Size(1068, 422);
            this.treeViewLocations.TabIndex = 0;
            this.treeViewLocations.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeViewLocations_BeforeCollapse);
            this.treeViewLocations.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TreeViewLocations_BeforeExpand);
            this.treeViewLocations.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeViewLocations_AfterSelect);
            this.treeViewLocations.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewLocations_NodeMouseClick);
            this.treeViewLocations.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.TreeViewLocations_NodeMouseDoubleClick);
            this.treeViewLocations.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TreeViewLocations_MouseDown);
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
            this.tabCountries.Size = new System.Drawing.Size(1074, 428);
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
            this.tabRegions.Size = new System.Drawing.Size(1074, 428);
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
            this.tabSubRegions.Size = new System.Drawing.Size(1074, 428);
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
            this.dgSubRegions.RowHeadersWidth = 82;
            this.dgSubRegions.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSubRegions.Size = new System.Drawing.Size(1068, 422);
            this.dgSubRegions.TabIndex = 1;
            this.dgSubRegions.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSubRegions_CellDoubleClick);
            this.dgSubRegions.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgSubRegions_CellFormatting);
            // 
            // tabAddresses
            // 
            this.tabAddresses.Controls.Add(this.dgAddresses);
            this.tabAddresses.Location = new System.Drawing.Point(4, 22);
            this.tabAddresses.Name = "tabAddresses";
            this.tabAddresses.Padding = new System.Windows.Forms.Padding(3);
            this.tabAddresses.Size = new System.Drawing.Size(1074, 428);
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
            this.dgAddresses.RowHeadersWidth = 82;
            this.dgAddresses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgAddresses.Size = new System.Drawing.Size(1068, 422);
            this.dgAddresses.TabIndex = 1;
            this.dgAddresses.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgAddresses_CellDoubleClick);
            this.dgAddresses.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgAddresses_CellFormatting);
            // 
            // tabPlaces
            // 
            this.tabPlaces.Controls.Add(this.dgPlaces);
            this.tabPlaces.Location = new System.Drawing.Point(4, 22);
            this.tabPlaces.Name = "tabPlaces";
            this.tabPlaces.Padding = new System.Windows.Forms.Padding(3);
            this.tabPlaces.Size = new System.Drawing.Size(1074, 428);
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
            this.dgPlaces.RowHeadersWidth = 82;
            this.dgPlaces.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgPlaces.Size = new System.Drawing.Size(1068, 422);
            this.dgPlaces.TabIndex = 2;
            this.dgPlaces.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgPlaces_CellDoubleClick);
            this.dgPlaces.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgPlaces_CellFormatting);
            // 
            // tabDisplayProgress
            // 
            this.tabDisplayProgress.Controls.Add(this.splitGedcom);
            this.tabDisplayProgress.Location = new System.Drawing.Point(4, 22);
            this.tabDisplayProgress.Name = "tabDisplayProgress";
            this.tabDisplayProgress.Padding = new System.Windows.Forms.Padding(3);
            this.tabDisplayProgress.Size = new System.Drawing.Size(1088, 460);
            this.tabDisplayProgress.TabIndex = 1;
            this.tabDisplayProgress.Text = "Gedcom Stats";
            this.tabDisplayProgress.UseVisualStyleBackColor = true;
            // 
            // splitGedcom
            // 
            this.splitGedcom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGedcom.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitGedcom.Location = new System.Drawing.Point(3, 3);
            this.splitGedcom.Name = "splitGedcom";
            this.splitGedcom.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitGedcom.Panel1
            // 
            this.splitGedcom.Panel1.Controls.Add(this.panel2);
            this.splitGedcom.Panel1MinSize = 110;
            // 
            // splitGedcom.Panel2
            // 
            this.splitGedcom.Panel2.Controls.Add(this.rtbOutput);
            this.splitGedcom.Size = new System.Drawing.Size(1082, 454);
            this.splitGedcom.SplitterDistance = 110;
            this.splitGedcom.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.LbProgramName);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.pbRelationships);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.pbFamilies);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.pbIndividuals);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.pbSources);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1082, 110);
            this.panel2.TabIndex = 13;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(943, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // LbProgramName
            // 
            this.LbProgramName.AutoSize = true;
            this.LbProgramName.Font = new System.Drawing.Font("Kunstler Script", 52F, System.Drawing.FontStyle.Bold);
            this.LbProgramName.Location = new System.Drawing.Point(415, 13);
            this.LbProgramName.Name = "LbProgramName";
            this.LbProgramName.Size = new System.Drawing.Size(522, 76);
            this.LbProgramName.TabIndex = 17;
            this.LbProgramName.Text = "Family Tree Analyzer";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(128, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "Relationships && Locations";
            // 
            // pbRelationships
            // 
            this.pbRelationships.Location = new System.Drawing.Point(134, 76);
            this.pbRelationships.Name = "pbRelationships";
            this.pbRelationships.Size = new System.Drawing.Size(275, 16);
            this.pbRelationships.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(85, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Loading Families";
            // 
            // pbFamilies
            // 
            this.pbFamilies.Location = new System.Drawing.Point(134, 54);
            this.pbFamilies.Name = "pbFamilies";
            this.pbFamilies.Size = new System.Drawing.Size(275, 16);
            this.pbFamilies.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 35);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(98, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Loading Individuals";
            // 
            // pbIndividuals
            // 
            this.pbIndividuals.Location = new System.Drawing.Point(134, 32);
            this.pbIndividuals.Name = "pbIndividuals";
            this.pbIndividuals.Size = new System.Drawing.Size(275, 16);
            this.pbIndividuals.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Loading Sources";
            // 
            // pbSources
            // 
            this.pbSources.Location = new System.Drawing.Point(134, 10);
            this.pbSources.Name = "pbSources";
            this.pbSources.Size = new System.Drawing.Size(275, 16);
            this.pbSources.TabIndex = 9;
            // 
            // rtbOutput
            // 
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbOutput.Location = new System.Drawing.Point(0, 0);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(1082, 340);
            this.rtbOutput.TabIndex = 14;
            this.rtbOutput.Text = "";
            // 
            // tabSelector
            // 
            this.tabSelector.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabSelector.Controls.Add(this.tabDisplayProgress);
            this.tabSelector.Controls.Add(this.tabMainLists);
            this.tabSelector.Controls.Add(this.tabErrorsFixes);
            this.tabSelector.Controls.Add(this.tabSurnames);
            this.tabSelector.Controls.Add(this.tabLocations);
            this.tabSelector.Controls.Add(this.tabFacts);
            this.tabSelector.Controls.Add(this.tabCensus);
            this.tabSelector.Controls.Add(this.tabLostCousins);
            this.tabSelector.Controls.Add(this.tabColourReports);
            this.tabSelector.Controls.Add(this.tabTreetops);
            this.tabSelector.Controls.Add(this.tabWorldWars);
            this.tabSelector.Controls.Add(this.tabToday);
            this.tabSelector.Location = new System.Drawing.Point(0, 27);
            this.tabSelector.Name = "tabSelector";
            this.tabSelector.SelectedIndex = 0;
            this.tabSelector.Size = new System.Drawing.Size(1096, 486);
            this.tabSelector.TabIndex = 9;
            this.tabSelector.SelectedIndexChanged += new System.EventHandler(this.TabControl_SelectedIndexChanged);
            // 
            // tabMainLists
            // 
            this.tabMainLists.Controls.Add(this.tabMainListsSelector);
            this.tabMainLists.Location = new System.Drawing.Point(4, 22);
            this.tabMainLists.Name = "tabMainLists";
            this.tabMainLists.Padding = new System.Windows.Forms.Padding(3);
            this.tabMainLists.Size = new System.Drawing.Size(1088, 460);
            this.tabMainLists.TabIndex = 18;
            this.tabMainLists.Text = "Main Lists";
            this.tabMainLists.UseVisualStyleBackColor = true;
            // 
            // tabMainListsSelector
            // 
            this.tabMainListsSelector.Controls.Add(this.tabIndividuals);
            this.tabMainListsSelector.Controls.Add(this.tabFamilies);
            this.tabMainListsSelector.Controls.Add(this.tabSources);
            this.tabMainListsSelector.Controls.Add(this.tabOccupations);
            this.tabMainListsSelector.Controls.Add(this.tabCustomFacts);
            this.tabMainListsSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMainListsSelector.Location = new System.Drawing.Point(3, 3);
            this.tabMainListsSelector.Name = "tabMainListsSelector";
            this.tabMainListsSelector.SelectedIndex = 0;
            this.tabMainListsSelector.Size = new System.Drawing.Size(1082, 454);
            this.tabMainListsSelector.TabIndex = 0;
            this.tabMainListsSelector.SelectedIndexChanged += new System.EventHandler(this.TabMainListSelector_SelectedIndexChanged);
            // 
            // tabIndividuals
            // 
            this.tabIndividuals.Controls.Add(this.dgIndividuals);
            this.tabIndividuals.Location = new System.Drawing.Point(4, 22);
            this.tabIndividuals.Name = "tabIndividuals";
            this.tabIndividuals.Padding = new System.Windows.Forms.Padding(3);
            this.tabIndividuals.Size = new System.Drawing.Size(1074, 428);
            this.tabIndividuals.TabIndex = 0;
            this.tabIndividuals.Text = "Individuals";
            this.tabIndividuals.UseVisualStyleBackColor = true;
            // 
            // dgIndividuals
            // 
            this.dgIndividuals.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgIndividuals.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIndividuals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgIndividuals.Location = new System.Drawing.Point(3, 3);
            this.dgIndividuals.Name = "dgIndividuals";
            this.dgIndividuals.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgIndividuals.Size = new System.Drawing.Size(1068, 422);
            this.dgIndividuals.TabIndex = 1;
            this.dgIndividuals.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgIndividuals_CellDoubleClick);
            this.dgIndividuals.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgIndividuals_MouseDown);
            // 
            // tabFamilies
            // 
            this.tabFamilies.Controls.Add(this.dgFamilies);
            this.tabFamilies.Location = new System.Drawing.Point(4, 22);
            this.tabFamilies.Name = "tabFamilies";
            this.tabFamilies.Padding = new System.Windows.Forms.Padding(3);
            this.tabFamilies.Size = new System.Drawing.Size(1074, 428);
            this.tabFamilies.TabIndex = 1;
            this.tabFamilies.Text = "Families";
            this.tabFamilies.UseVisualStyleBackColor = true;
            // 
            // dgFamilies
            // 
            this.dgFamilies.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgFamilies.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgFamilies.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgFamilies.Location = new System.Drawing.Point(3, 3);
            this.dgFamilies.Name = "dgFamilies";
            this.dgFamilies.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgFamilies.Size = new System.Drawing.Size(1068, 422);
            this.dgFamilies.TabIndex = 2;
            this.dgFamilies.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgFamilies_CellDoubleClick);
            // 
            // tabSources
            // 
            this.tabSources.Controls.Add(this.dgSources);
            this.tabSources.Location = new System.Drawing.Point(4, 22);
            this.tabSources.Name = "tabSources";
            this.tabSources.Size = new System.Drawing.Size(1074, 428);
            this.tabSources.TabIndex = 2;
            this.tabSources.Text = "Sources";
            this.tabSources.UseVisualStyleBackColor = true;
            // 
            // dgSources
            // 
            this.dgSources.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.dgSources.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSources.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSources.Location = new System.Drawing.Point(0, 0);
            this.dgSources.Name = "dgSources";
            this.dgSources.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSources.Size = new System.Drawing.Size(1074, 428);
            this.dgSources.TabIndex = 2;
            this.dgSources.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSources_CellDoubleClick);
            // 
            // tabOccupations
            // 
            this.tabOccupations.Controls.Add(this.dgOccupations);
            this.tabOccupations.Location = new System.Drawing.Point(4, 22);
            this.tabOccupations.Name = "tabOccupations";
            this.tabOccupations.Size = new System.Drawing.Size(1074, 428);
            this.tabOccupations.TabIndex = 3;
            this.tabOccupations.Text = "Occupations";
            this.tabOccupations.UseVisualStyleBackColor = true;
            // 
            // dgOccupations
            // 
            this.dgOccupations.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgOccupations.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgOccupations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgOccupations.Location = new System.Drawing.Point(0, 0);
            this.dgOccupations.Name = "dgOccupations";
            this.dgOccupations.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgOccupations.Size = new System.Drawing.Size(1074, 428);
            this.dgOccupations.TabIndex = 3;
            this.dgOccupations.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgOccupations_CellDoubleClick);
            // 
            // tabCustomFacts
            // 
            this.tabCustomFacts.Controls.Add(this.dgCustomFacts);
            this.tabCustomFacts.Location = new System.Drawing.Point(4, 22);
            this.tabCustomFacts.Name = "tabCustomFacts";
            this.tabCustomFacts.Size = new System.Drawing.Size(1074, 428);
            this.tabCustomFacts.TabIndex = 4;
            this.tabCustomFacts.Text = "Custom Facts";
            this.tabCustomFacts.UseVisualStyleBackColor = true;
            // 
            // dgCustomFacts
            // 
            this.dgCustomFacts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgCustomFacts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCustomFacts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgCustomFacts.Location = new System.Drawing.Point(0, 0);
            this.dgCustomFacts.Name = "dgCustomFacts";
            this.dgCustomFacts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgCustomFacts.Size = new System.Drawing.Size(1074, 428);
            this.dgCustomFacts.TabIndex = 4;
            this.dgCustomFacts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgCustomFacts_CellDoubleClick);
            this.dgCustomFacts.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgCustomFacts_CellValueChanged);
            // 
            // tabErrorsFixes
            // 
            this.tabErrorsFixes.Controls.Add(this.tabErrorFixSelector);
            this.tabErrorsFixes.Location = new System.Drawing.Point(4, 22);
            this.tabErrorsFixes.Name = "tabErrorsFixes";
            this.tabErrorsFixes.Size = new System.Drawing.Size(1088, 460);
            this.tabErrorsFixes.TabIndex = 19;
            this.tabErrorsFixes.Text = "Errors/Fixes";
            this.tabErrorsFixes.UseVisualStyleBackColor = true;
            // 
            // tabErrorFixSelector
            // 
            this.tabErrorFixSelector.Controls.Add(this.tabDataErrors);
            this.tabErrorFixSelector.Controls.Add(this.tabDuplicates);
            this.tabErrorFixSelector.Controls.Add(this.tabLooseBirths);
            this.tabErrorFixSelector.Controls.Add(this.tabLooseDeaths);
            this.tabErrorFixSelector.Controls.Add(this.tabLooseInfo);
            this.tabErrorFixSelector.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabErrorFixSelector.Location = new System.Drawing.Point(0, 0);
            this.tabErrorFixSelector.Name = "tabErrorFixSelector";
            this.tabErrorFixSelector.SelectedIndex = 0;
            this.tabErrorFixSelector.ShowToolTips = true;
            this.tabErrorFixSelector.Size = new System.Drawing.Size(1088, 460);
            this.tabErrorFixSelector.TabIndex = 0;
            this.tabErrorFixSelector.SelectedIndexChanged += new System.EventHandler(this.TabErrorFixSelector_SelectedIndexChanged);
            // 
            // tabDataErrors
            // 
            this.tabDataErrors.Controls.Add(this.dgDataErrors);
            this.tabDataErrors.Controls.Add(this.gbDataErrorTypes);
            this.tabDataErrors.Location = new System.Drawing.Point(4, 22);
            this.tabDataErrors.Name = "tabDataErrors";
            this.tabDataErrors.Padding = new System.Windows.Forms.Padding(3);
            this.tabDataErrors.Size = new System.Drawing.Size(1080, 434);
            this.tabDataErrors.TabIndex = 0;
            this.tabDataErrors.Text = "Data Errors";
            this.tabDataErrors.UseVisualStyleBackColor = true;
            // 
            // gbDataErrorTypes
            // 
            this.gbDataErrorTypes.Controls.Add(this.btnSelectAll);
            this.gbDataErrorTypes.Controls.Add(this.btnClearAll);
            this.gbDataErrorTypes.Controls.Add(this.ckbDataErrors);
            this.gbDataErrorTypes.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDataErrorTypes.Location = new System.Drawing.Point(3, 3);
            this.gbDataErrorTypes.Name = "gbDataErrorTypes";
            this.gbDataErrorTypes.Size = new System.Drawing.Size(1074, 166);
            this.gbDataErrorTypes.TabIndex = 1;
            this.gbDataErrorTypes.TabStop = false;
            this.gbDataErrorTypes.Text = "Types of Data Error to display";
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.Location = new System.Drawing.Point(8, 134);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(75, 23);
            this.btnSelectAll.TabIndex = 7;
            this.btnSelectAll.Text = "Select All";
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.BtnSelectAll_Click);
            // 
            // btnClearAll
            // 
            this.btnClearAll.Location = new System.Drawing.Point(89, 134);
            this.btnClearAll.Name = "btnClearAll";
            this.btnClearAll.Size = new System.Drawing.Size(75, 23);
            this.btnClearAll.TabIndex = 6;
            this.btnClearAll.Text = "Clear All";
            this.btnClearAll.UseVisualStyleBackColor = true;
            this.btnClearAll.Click += new System.EventHandler(this.BtnClearAll_Click);
            // 
            // ckbDataErrors
            // 
            this.ckbDataErrors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ckbDataErrors.CheckOnClick = true;
            this.ckbDataErrors.ColumnWidth = 300;
            this.ckbDataErrors.FormattingEnabled = true;
            this.ckbDataErrors.Location = new System.Drawing.Point(8, 19);
            this.ckbDataErrors.MultiColumn = true;
            this.ckbDataErrors.Name = "ckbDataErrors";
            this.ckbDataErrors.ScrollAlwaysVisible = true;
            this.ckbDataErrors.Size = new System.Drawing.Size(1054, 94);
            this.ckbDataErrors.TabIndex = 0;
            this.ckbDataErrors.SelectedIndexChanged += new System.EventHandler(this.CkbDataErrors_SelectedIndexChanged);
            // 
            // tabDuplicates
            // 
            this.tabDuplicates.Controls.Add(this.labDuplicateSlider);
            this.tabDuplicates.Controls.Add(this.labCompletion);
            this.tabDuplicates.Controls.Add(this.chkIgnoreUnnamedTwins);
            this.tabDuplicates.Controls.Add(this.ckbHideIgnoredDuplicates);
            this.tabDuplicates.Controls.Add(this.label16);
            this.tabDuplicates.Controls.Add(this.label13);
            this.tabDuplicates.Controls.Add(this.label12);
            this.tabDuplicates.Controls.Add(this.tbDuplicateScore);
            this.tabDuplicates.Controls.Add(this.labCalcDuplicates);
            this.tabDuplicates.Controls.Add(this.pbDuplicates);
            this.tabDuplicates.Controls.Add(this.dgDuplicates);
            this.tabDuplicates.Controls.Add(this.btnCancelDuplicates);
            this.tabDuplicates.Location = new System.Drawing.Point(4, 22);
            this.tabDuplicates.Name = "tabDuplicates";
            this.tabDuplicates.Padding = new System.Windows.Forms.Padding(3);
            this.tabDuplicates.Size = new System.Drawing.Size(1080, 434);
            this.tabDuplicates.TabIndex = 1;
            this.tabDuplicates.Text = "Duplicates?";
            this.tabDuplicates.UseVisualStyleBackColor = true;
            // 
            // labDuplicateSlider
            // 
            this.labDuplicateSlider.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.labDuplicateSlider.AutoSize = true;
            this.labDuplicateSlider.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labDuplicateSlider.Location = new System.Drawing.Point(558, 35);
            this.labDuplicateSlider.Name = "labDuplicateSlider";
            this.labDuplicateSlider.Size = new System.Drawing.Size(104, 13);
            this.labDuplicateSlider.TabIndex = 31;
            this.labDuplicateSlider.Text = "Match Quality : 1";
            // 
            // labCompletion
            // 
            this.labCompletion.AutoSize = true;
            this.labCompletion.Location = new System.Drawing.Point(122, 35);
            this.labCompletion.Name = "labCompletion";
            this.labCompletion.Size = new System.Drawing.Size(0, 13);
            this.labCompletion.TabIndex = 30;
            // 
            // ckbHideIgnoredDuplicates
            // 
            this.ckbHideIgnoredDuplicates.AutoSize = true;
            this.ckbHideIgnoredDuplicates.Checked = true;
            this.ckbHideIgnoredDuplicates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckbHideIgnoredDuplicates.Location = new System.Drawing.Point(12, 64);
            this.ckbHideIgnoredDuplicates.Name = "ckbHideIgnoredDuplicates";
            this.ckbHideIgnoredDuplicates.Size = new System.Drawing.Size(228, 17);
            this.ckbHideIgnoredDuplicates.TabIndex = 28;
            this.ckbHideIgnoredDuplicates.Text = "Hide Possible Duplicates marked as Ignore";
            this.ckbHideIgnoredDuplicates.UseVisualStyleBackColor = true;
            this.ckbHideIgnoredDuplicates.CheckedChanged += new System.EventHandler(this.CkbHideIgnoredDuplicates_CheckedChanged);
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(411, 63);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(199, 18);
            this.label16.TabIndex = 26;
            this.label16.Text = "Candidate Duplicates List";
            // 
            // labCalcDuplicates
            // 
            this.labCalcDuplicates.AutoSize = true;
            this.labCalcDuplicates.Location = new System.Drawing.Point(7, 10);
            this.labCalcDuplicates.Name = "labCalcDuplicates";
            this.labCalcDuplicates.Size = new System.Drawing.Size(112, 13);
            this.labCalcDuplicates.TabIndex = 21;
            this.labCalcDuplicates.Text = "Calculating Duplicates";
            // 
            // pbDuplicates
            // 
            this.pbDuplicates.Location = new System.Drawing.Point(125, 6);
            this.pbDuplicates.Name = "pbDuplicates";
            this.pbDuplicates.Size = new System.Drawing.Size(283, 23);
            this.pbDuplicates.TabIndex = 20;
            // 
            // dgDuplicates
            // 
            this.dgDuplicates.AllowUserToAddRows = false;
            this.dgDuplicates.AllowUserToOrderColumns = true;
            this.dgDuplicates.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgDuplicates.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgDuplicates.Location = new System.Drawing.Point(-1, 87);
            this.dgDuplicates.Name = "dgDuplicates";
            this.dgDuplicates.ReadOnly = true;
            this.dgDuplicates.RowHeadersWidth = 15;
            this.dgDuplicates.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgDuplicates.Size = new System.Drawing.Size(1072, 333);
            this.dgDuplicates.TabIndex = 19;
            this.dgDuplicates.VirtualMode = true;
            this.dgDuplicates.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgDuplicates_CellContentClick);
            this.dgDuplicates.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgDuplicates_CellDoubleClick);
            // 
            // btnCancelDuplicates
            // 
            this.btnCancelDuplicates.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelDuplicates.Image")));
            this.btnCancelDuplicates.Location = new System.Drawing.Point(414, 6);
            this.btnCancelDuplicates.Name = "btnCancelDuplicates";
            this.btnCancelDuplicates.Size = new System.Drawing.Size(23, 23);
            this.btnCancelDuplicates.TabIndex = 27;
            this.btnCancelDuplicates.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnCancelDuplicates.UseVisualStyleBackColor = true;
            this.btnCancelDuplicates.Visible = false;
            this.btnCancelDuplicates.Click += new System.EventHandler(this.BtnCancelDuplicates_Click);
            // 
            // tabLooseBirths
            // 
            this.tabLooseBirths.Controls.Add(this.dgLooseBirths);
            this.tabLooseBirths.Location = new System.Drawing.Point(4, 22);
            this.tabLooseBirths.Name = "tabLooseBirths";
            this.tabLooseBirths.Size = new System.Drawing.Size(1080, 434);
            this.tabLooseBirths.TabIndex = 2;
            this.tabLooseBirths.Text = "Loose Births";
            this.tabLooseBirths.UseVisualStyleBackColor = true;
            // 
            // dgLooseBirths
            // 
            this.dgLooseBirths.AllowUserToAddRows = false;
            this.dgLooseBirths.AllowUserToDeleteRows = false;
            this.dgLooseBirths.AllowUserToOrderColumns = true;
            this.dgLooseBirths.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLooseBirths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLooseBirths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLooseBirths.Location = new System.Drawing.Point(0, 0);
            this.dgLooseBirths.MultiSelect = false;
            this.dgLooseBirths.Name = "dgLooseBirths";
            this.dgLooseBirths.RowHeadersWidth = 82;
            this.dgLooseBirths.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLooseBirths.Size = new System.Drawing.Size(1080, 434);
            this.dgLooseBirths.TabIndex = 3;
            this.dgLooseBirths.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgLooseBirths_CellDoubleClick);
            // 
            // tabLooseDeaths
            // 
            this.tabLooseDeaths.Controls.Add(this.dgLooseDeaths);
            this.tabLooseDeaths.Location = new System.Drawing.Point(4, 22);
            this.tabLooseDeaths.Name = "tabLooseDeaths";
            this.tabLooseDeaths.Size = new System.Drawing.Size(1080, 434);
            this.tabLooseDeaths.TabIndex = 3;
            this.tabLooseDeaths.Text = "Loose Deaths";
            this.tabLooseDeaths.UseVisualStyleBackColor = true;
            // 
            // dgLooseDeaths
            // 
            this.dgLooseDeaths.AllowUserToAddRows = false;
            this.dgLooseDeaths.AllowUserToDeleteRows = false;
            this.dgLooseDeaths.AllowUserToOrderColumns = true;
            this.dgLooseDeaths.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLooseDeaths.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLooseDeaths.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLooseDeaths.Location = new System.Drawing.Point(0, 0);
            this.dgLooseDeaths.MultiSelect = false;
            this.dgLooseDeaths.Name = "dgLooseDeaths";
            this.dgLooseDeaths.RowHeadersWidth = 82;
            this.dgLooseDeaths.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLooseDeaths.Size = new System.Drawing.Size(1080, 434);
            this.dgLooseDeaths.TabIndex = 2;
            this.dgLooseDeaths.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgLooseDeaths_CellDoubleClick);
            // 
            // tabLooseInfo
            // 
            this.tabLooseInfo.Controls.Add(this.dgLooseInfo);
            this.tabLooseInfo.Location = new System.Drawing.Point(4, 22);
            this.tabLooseInfo.Name = "tabLooseInfo";
            this.tabLooseInfo.Size = new System.Drawing.Size(1080, 434);
            this.tabLooseInfo.TabIndex = 4;
            this.tabLooseInfo.Text = "All Loose Info";
            this.tabLooseInfo.UseVisualStyleBackColor = true;
            // 
            // dgLooseInfo
            // 
            this.dgLooseInfo.AllowUserToAddRows = false;
            this.dgLooseInfo.AllowUserToDeleteRows = false;
            this.dgLooseInfo.AllowUserToOrderColumns = true;
            this.dgLooseInfo.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgLooseInfo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgLooseInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgLooseInfo.Location = new System.Drawing.Point(0, 0);
            this.dgLooseInfo.MultiSelect = false;
            this.dgLooseInfo.Name = "dgLooseInfo";
            this.dgLooseInfo.RowHeadersWidth = 82;
            this.dgLooseInfo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgLooseInfo.Size = new System.Drawing.Size(1080, 434);
            this.dgLooseInfo.TabIndex = 4;
            this.dgLooseInfo.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgLooseInfo_CellDoubleClick);
            // 
            // tabSurnames
            // 
            this.tabSurnames.Controls.Add(this.chkSurnamesIgnoreCase);
            this.tabSurnames.Controls.Add(this.btnShowSurnames);
            this.tabSurnames.Controls.Add(this.dgSurnames);
            this.tabSurnames.Controls.Add(this.reltypesSurnames);
            this.tabSurnames.Location = new System.Drawing.Point(4, 22);
            this.tabSurnames.Name = "tabSurnames";
            this.tabSurnames.Padding = new System.Windows.Forms.Padding(3);
            this.tabSurnames.Size = new System.Drawing.Size(1088, 460);
            this.tabSurnames.TabIndex = 14;
            this.tabSurnames.Text = "Surnames";
            this.tabSurnames.UseVisualStyleBackColor = true;
            // 
            // chkSurnamesIgnoreCase
            // 
            this.chkSurnamesIgnoreCase.AutoSize = true;
            this.chkSurnamesIgnoreCase.Checked = true;
            this.chkSurnamesIgnoreCase.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkSurnamesIgnoreCase.Location = new System.Drawing.Point(497, 78);
            this.chkSurnamesIgnoreCase.Name = "chkSurnamesIgnoreCase";
            this.chkSurnamesIgnoreCase.Size = new System.Drawing.Size(83, 17);
            this.chkSurnamesIgnoreCase.TabIndex = 24;
            this.chkSurnamesIgnoreCase.Text = "Ignore Case";
            this.chkSurnamesIgnoreCase.UseVisualStyleBackColor = true;
            // 
            // btnShowSurnames
            // 
            this.btnShowSurnames.Location = new System.Drawing.Point(337, 74);
            this.btnShowSurnames.Name = "btnShowSurnames";
            this.btnShowSurnames.Size = new System.Drawing.Size(154, 23);
            this.btnShowSurnames.TabIndex = 23;
            this.btnShowSurnames.Text = "Show Surnames";
            this.btnShowSurnames.UseVisualStyleBackColor = true;
            this.btnShowSurnames.Click += new System.EventHandler(this.BtnShowSurnames_Click);
            // 
            // dgSurnames
            // 
            this.dgSurnames.AllowUserToAddRows = false;
            this.dgSurnames.AllowUserToDeleteRows = false;
            this.dgSurnames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgSurnames.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSurnames.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Surname,
            this.URI,
            this.Individuals,
            this.Families,
            this.Marriages});
            this.dgSurnames.Location = new System.Drawing.Point(3, 103);
            this.dgSurnames.MultiSelect = false;
            this.dgSurnames.Name = "dgSurnames";
            this.dgSurnames.ReadOnly = true;
            this.dgSurnames.RowHeadersWidth = 20;
            this.dgSurnames.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgSurnames.Size = new System.Drawing.Size(1079, 337);
            this.dgSurnames.TabIndex = 1;
            this.dgSurnames.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSurnames_CellContentClick);
            this.dgSurnames.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgSurnames_CellDoubleClick);
            // 
            // Surname
            // 
            this.Surname.DataPropertyName = "Surname";
            this.Surname.HeaderText = "Surname";
            this.Surname.MinimumWidth = 10;
            this.Surname.Name = "Surname";
            this.Surname.ReadOnly = true;
            this.Surname.Width = 200;
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
            this.Individuals.MinimumWidth = 10;
            this.Individuals.Name = "Individuals";
            this.Individuals.ReadOnly = true;
            this.Individuals.Width = 200;
            // 
            // Families
            // 
            this.Families.DataPropertyName = "Families";
            this.Families.HeaderText = "Families";
            this.Families.MinimumWidth = 10;
            this.Families.Name = "Families";
            this.Families.ReadOnly = true;
            this.Families.Width = 200;
            // 
            // Marriages
            // 
            this.Marriages.DataPropertyName = "Marriages";
            this.Marriages.HeaderText = "Marriages";
            this.Marriages.MinimumWidth = 10;
            this.Marriages.Name = "Marriages";
            this.Marriages.ReadOnly = true;
            this.Marriages.Width = 200;
            // 
            // reltypesSurnames
            // 
            this.reltypesSurnames.Location = new System.Drawing.Point(6, 3);
            this.reltypesSurnames.Margin = new System.Windows.Forms.Padding(6);
            this.reltypesSurnames.MarriedToDB = true;
            this.reltypesSurnames.Name = "reltypesSurnames";
            this.reltypesSurnames.Size = new System.Drawing.Size(325, 99);
            this.reltypesSurnames.TabIndex = 22;
            // 
            // tabFacts
            // 
            this.tabFacts.Controls.Add(this.btnDuplicateFacts);
            this.tabFacts.Controls.Add(this.btnShowExclusions);
            this.tabFacts.Controls.Add(this.lblExclude);
            this.tabFacts.Controls.Add(this.label15);
            this.tabFacts.Controls.Add(this.btnDeselectExcludeAllFactTypes);
            this.tabFacts.Controls.Add(this.btnExcludeAllFactTypes);
            this.tabFacts.Controls.Add(this.ckbFactExclude);
            this.tabFacts.Controls.Add(this.btnDeselectAllFactTypes);
            this.tabFacts.Controls.Add(this.btnSelectAllFactTypes);
            this.tabFacts.Controls.Add(this.ckbFactSelect);
            this.tabFacts.Controls.Add(this.btnShowFacts);
            this.tabFacts.Controls.Add(this.label3);
            this.tabFacts.Controls.Add(this.txtFactsSurname);
            this.tabFacts.Controls.Add(this.relTypesFacts);
            this.tabFacts.Location = new System.Drawing.Point(4, 22);
            this.tabFacts.Name = "tabFacts";
            this.tabFacts.Size = new System.Drawing.Size(1088, 460);
            this.tabFacts.TabIndex = 13;
            this.tabFacts.Text = "Facts";
            this.tabFacts.UseVisualStyleBackColor = true;
            // 
            // btnDuplicateFacts
            // 
            this.btnDuplicateFacts.Location = new System.Drawing.Point(681, 42);
            this.btnDuplicateFacts.Name = "btnDuplicateFacts";
            this.btnDuplicateFacts.Size = new System.Drawing.Size(162, 38);
            this.btnDuplicateFacts.TabIndex = 34;
            this.btnDuplicateFacts.Text = "Show Duplicate Facts of Selected Fact Type";
            this.btnDuplicateFacts.UseVisualStyleBackColor = true;
            this.btnDuplicateFacts.Click += new System.EventHandler(this.BtnDuplicateFacts_Click);
            // 
            // lblExclude
            // 
            this.lblExclude.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblExclude.Location = new System.Drawing.Point(358, 427);
            this.lblExclude.Name = "lblExclude";
            this.lblExclude.Size = new System.Drawing.Size(293, 13);
            this.lblExclude.TabIndex = 32;
            this.lblExclude.Text = "Select Facts to Exclude from Report";
            this.lblExclude.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.lblExclude.Visible = false;
            // 
            // label15
            // 
            this.label15.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label15.Location = new System.Drawing.Point(8, 427);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(293, 13);
            this.label15.TabIndex = 31;
            this.label15.Text = "Select Facts to Include in Report";
            this.label15.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnDeselectExcludeAllFactTypes
            // 
            this.btnDeselectExcludeAllFactTypes.Location = new System.Drawing.Point(538, 109);
            this.btnDeselectExcludeAllFactTypes.Name = "btnDeselectExcludeAllFactTypes";
            this.btnDeselectExcludeAllFactTypes.Size = new System.Drawing.Size(136, 23);
            this.btnDeselectExcludeAllFactTypes.TabIndex = 30;
            this.btnDeselectExcludeAllFactTypes.Text = "De-select all Fact Types";
            this.btnDeselectExcludeAllFactTypes.UseVisualStyleBackColor = true;
            this.btnDeselectExcludeAllFactTypes.Visible = false;
            this.btnDeselectExcludeAllFactTypes.Click += new System.EventHandler(this.BtnDeselectExcludeAllFactTypes_Click);
            // 
            // btnExcludeAllFactTypes
            // 
            this.btnExcludeAllFactTypes.Location = new System.Drawing.Point(361, 108);
            this.btnExcludeAllFactTypes.Name = "btnExcludeAllFactTypes";
            this.btnExcludeAllFactTypes.Size = new System.Drawing.Size(136, 23);
            this.btnExcludeAllFactTypes.TabIndex = 29;
            this.btnExcludeAllFactTypes.Text = "Select all Fact Types";
            this.btnExcludeAllFactTypes.UseVisualStyleBackColor = true;
            this.btnExcludeAllFactTypes.Visible = false;
            this.btnExcludeAllFactTypes.Click += new System.EventHandler(this.BtnExcludeAllFactTypes_Click);
            // 
            // btnDeselectAllFactTypes
            // 
            this.btnDeselectAllFactTypes.Location = new System.Drawing.Point(185, 109);
            this.btnDeselectAllFactTypes.Name = "btnDeselectAllFactTypes";
            this.btnDeselectAllFactTypes.Size = new System.Drawing.Size(136, 23);
            this.btnDeselectAllFactTypes.TabIndex = 27;
            this.btnDeselectAllFactTypes.Text = "De-select all Fact Types";
            this.btnDeselectAllFactTypes.UseVisualStyleBackColor = true;
            this.btnDeselectAllFactTypes.Click += new System.EventHandler(this.BtnDeselectAllFactTypes_Click);
            // 
            // btnSelectAllFactTypes
            // 
            this.btnSelectAllFactTypes.Location = new System.Drawing.Point(8, 108);
            this.btnSelectAllFactTypes.Name = "btnSelectAllFactTypes";
            this.btnSelectAllFactTypes.Size = new System.Drawing.Size(136, 23);
            this.btnSelectAllFactTypes.TabIndex = 26;
            this.btnSelectAllFactTypes.Text = "Select all Fact Types";
            this.btnSelectAllFactTypes.UseVisualStyleBackColor = true;
            this.btnSelectAllFactTypes.Click += new System.EventHandler(this.BtnSelectAllFactTypes_Click);
            // 
            // ckbFactSelect
            // 
            this.ckbFactSelect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.ckbFactSelect.FormattingEnabled = true;
            this.ckbFactSelect.Location = new System.Drawing.Point(8, 137);
            this.ckbFactSelect.Name = "ckbFactSelect";
            this.ckbFactSelect.ScrollAlwaysVisible = true;
            this.ckbFactSelect.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.ckbFactSelect.Size = new System.Drawing.Size(313, 214);
            this.ckbFactSelect.TabIndex = 25;
            this.ckbFactSelect.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CkbFactSelect_MouseClick);
            // 
            // btnShowFacts
            // 
            this.btnShowFacts.Location = new System.Drawing.Point(361, 42);
            this.btnShowFacts.Name = "btnShowFacts";
            this.btnShowFacts.Size = new System.Drawing.Size(313, 38);
            this.btnShowFacts.TabIndex = 24;
            this.btnShowFacts.Text = "Show Facts for Individuals with Selected Fact Types";
            this.btnShowFacts.UseVisualStyleBackColor = true;
            this.btnShowFacts.Click += new System.EventHandler(this.BtnShowFacts_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(363, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Surname";
            // 
            // txtFactsSurname
            // 
            this.txtFactsSurname.Location = new System.Drawing.Point(418, 16);
            this.txtFactsSurname.Name = "txtFactsSurname";
            this.txtFactsSurname.Size = new System.Drawing.Size(256, 20);
            this.txtFactsSurname.TabIndex = 22;
            this.txtFactsSurname.TextChanged += new System.EventHandler(this.TxtFactsSurname_TextChanged);
            // 
            // relTypesFacts
            // 
            this.relTypesFacts.Location = new System.Drawing.Point(8, 3);
            this.relTypesFacts.Margin = new System.Windows.Forms.Padding(6);
            this.relTypesFacts.MarriedToDB = true;
            this.relTypesFacts.Name = "relTypesFacts";
            this.relTypesFacts.Size = new System.Drawing.Size(325, 100);
            this.relTypesFacts.TabIndex = 21;
            this.relTypesFacts.RelationTypesChanged += new System.EventHandler(this.RelTypesFacts_RelationTypesChanged);
            // 
            // tabToday
            // 
            this.tabToday.Controls.Add(this.label18);
            this.tabToday.Controls.Add(this.nudToday);
            this.tabToday.Controls.Add(this.btnUpdateTodaysEvents);
            this.tabToday.Controls.Add(this.labToday);
            this.tabToday.Controls.Add(this.pbToday);
            this.tabToday.Controls.Add(this.rbTodayMonth);
            this.tabToday.Controls.Add(this.rbTodaySingle);
            this.tabToday.Controls.Add(this.label17);
            this.tabToday.Controls.Add(this.dpToday);
            this.tabToday.Controls.Add(this.rtbToday);
            this.tabToday.Location = new System.Drawing.Point(4, 22);
            this.tabToday.Name = "tabToday";
            this.tabToday.Padding = new System.Windows.Forms.Padding(3);
            this.tabToday.Size = new System.Drawing.Size(1088, 460);
            this.tabToday.TabIndex = 17;
            this.tabToday.Text = "On This Day";
            this.tabToday.UseVisualStyleBackColor = true;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(553, 23);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(60, 13);
            this.label18.TabIndex = 16;
            this.label18.Text = "Year Step :";
            // 
            // nudToday
            // 
            this.nudToday.Location = new System.Drawing.Point(619, 21);
            this.nudToday.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudToday.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudToday.Name = "nudToday";
            this.nudToday.Size = new System.Drawing.Size(42, 20);
            this.nudToday.TabIndex = 15;
            this.nudToday.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudToday.ValueChanged += new System.EventHandler(this.NudToday_ValueChanged);
            // 
            // btnUpdateTodaysEvents
            // 
            this.btnUpdateTodaysEvents.Location = new System.Drawing.Point(255, 18);
            this.btnUpdateTodaysEvents.Name = "btnUpdateTodaysEvents";
            this.btnUpdateTodaysEvents.Size = new System.Drawing.Size(115, 23);
            this.btnUpdateTodaysEvents.TabIndex = 14;
            this.btnUpdateTodaysEvents.Text = "Update list of Events";
            this.btnUpdateTodaysEvents.UseVisualStyleBackColor = true;
            this.btnUpdateTodaysEvents.Click += new System.EventHandler(this.BtnUpdateTodaysEvents_Click);
            // 
            // labToday
            // 
            this.labToday.AutoSize = true;
            this.labToday.Location = new System.Drawing.Point(681, 23);
            this.labToday.Name = "labToday";
            this.labToday.Size = new System.Drawing.Size(112, 13);
            this.labToday.TabIndex = 13;
            this.labToday.Text = "Loading World Events";
            // 
            // pbToday
            // 
            this.pbToday.Location = new System.Drawing.Point(799, 21);
            this.pbToday.Name = "pbToday";
            this.pbToday.Size = new System.Drawing.Size(230, 20);
            this.pbToday.TabIndex = 12;
            // 
            // rbTodayMonth
            // 
            this.rbTodayMonth.AutoSize = true;
            this.rbTodayMonth.Location = new System.Drawing.Point(458, 21);
            this.rbTodayMonth.Name = "rbTodayMonth";
            this.rbTodayMonth.Size = new System.Drawing.Size(89, 17);
            this.rbTodayMonth.TabIndex = 11;
            this.rbTodayMonth.Text = "Whole Month";
            this.rbTodayMonth.UseVisualStyleBackColor = true;
            this.rbTodayMonth.CheckedChanged += new System.EventHandler(this.RbTodayMonth_CheckedChanged);
            // 
            // rbTodaySingle
            // 
            this.rbTodaySingle.AutoSize = true;
            this.rbTodaySingle.Checked = true;
            this.rbTodaySingle.Location = new System.Drawing.Point(376, 21);
            this.rbTodaySingle.Name = "rbTodaySingle";
            this.rbTodaySingle.Size = new System.Drawing.Size(76, 17);
            this.rbTodaySingle.TabIndex = 10;
            this.rbTodaySingle.TabStop = true;
            this.rbTodaySingle.Text = "Single Day";
            this.rbTodaySingle.UseVisualStyleBackColor = true;
            this.rbTodaySingle.CheckedChanged += new System.EventHandler(this.RbTodaySingle_CheckedChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(3, 23);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(69, 13);
            this.label17.TabIndex = 9;
            this.label17.Text = "Select Date :";
            // 
            // dpToday
            // 
            this.dpToday.Location = new System.Drawing.Point(78, 21);
            this.dpToday.Name = "dpToday";
            this.dpToday.Size = new System.Drawing.Size(171, 20);
            this.dpToday.TabIndex = 8;
            // 
            // rtbToday
            // 
            this.rtbToday.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbToday.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtbToday.Location = new System.Drawing.Point(0, 59);
            this.rtbToday.Name = "rtbToday";
            this.rtbToday.ReadOnly = true;
            this.rtbToday.Size = new System.Drawing.Size(1079, 388);
            this.rtbToday.TabIndex = 7;
            this.rtbToday.Text = "";
            // 
            // NonDuplicate
            // 
            this.NonDuplicate.DataPropertyName = "IgnoreNonDuplicate";
            this.NonDuplicate.FalseValue = "False";
            this.NonDuplicate.HeaderText = "Ignore";
            this.NonDuplicate.MinimumWidth = 40;
            this.NonDuplicate.Name = "NonDuplicate";
            this.NonDuplicate.ReadOnly = true;
            this.NonDuplicate.TrueValue = "True";
            this.NonDuplicate.Width = 40;
            // 
            // Score
            // 
            this.Score.DataPropertyName = "Score";
            this.Score.HeaderText = "Score";
            this.Score.MinimumWidth = 10;
            this.Score.Name = "Score";
            this.Score.ReadOnly = true;
            this.Score.Width = 200;
            // 
            // DuplicateIndividualID
            // 
            this.DuplicateIndividualID.DataPropertyName = "IndividualID";
            this.DuplicateIndividualID.HeaderText = "ID";
            this.DuplicateIndividualID.MinimumWidth = 10;
            this.DuplicateIndividualID.Name = "DuplicateIndividualID";
            this.DuplicateIndividualID.ReadOnly = true;
            this.DuplicateIndividualID.Width = 200;
            // 
            // DuplicateName
            // 
            this.DuplicateName.DataPropertyName = "Name";
            this.DuplicateName.HeaderText = "Name";
            this.DuplicateName.MinimumWidth = 50;
            this.DuplicateName.Name = "DuplicateName";
            this.DuplicateName.ReadOnly = true;
            this.DuplicateName.Width = 150;
            // 
            // DuplicateForenames
            // 
            this.DuplicateForenames.DataPropertyName = "Forenames";
            this.DuplicateForenames.HeaderText = "Forenames";
            this.DuplicateForenames.MinimumWidth = 10;
            this.DuplicateForenames.Name = "DuplicateForenames";
            this.DuplicateForenames.ReadOnly = true;
            this.DuplicateForenames.Visible = false;
            this.DuplicateForenames.Width = 200;
            // 
            // DuplicateSurname
            // 
            this.DuplicateSurname.DataPropertyName = "Surname";
            this.DuplicateSurname.HeaderText = "Surname";
            this.DuplicateSurname.MinimumWidth = 10;
            this.DuplicateSurname.Name = "DuplicateSurname";
            this.DuplicateSurname.ReadOnly = true;
            this.DuplicateSurname.Visible = false;
            this.DuplicateSurname.Width = 200;
            // 
            // DuplicateBirthDate
            // 
            this.DuplicateBirthDate.DataPropertyName = "BirthDate";
            this.DuplicateBirthDate.HeaderText = "Birthdate";
            this.DuplicateBirthDate.MinimumWidth = 50;
            this.DuplicateBirthDate.Name = "DuplicateBirthDate";
            this.DuplicateBirthDate.ReadOnly = true;
            this.DuplicateBirthDate.Width = 150;
            // 
            // DuplicateBirthLocation
            // 
            this.DuplicateBirthLocation.DataPropertyName = "BirthLocation";
            this.DuplicateBirthLocation.HeaderText = "Birth Location";
            this.DuplicateBirthLocation.MinimumWidth = 100;
            this.DuplicateBirthLocation.Name = "DuplicateBirthLocation";
            this.DuplicateBirthLocation.ReadOnly = true;
            this.DuplicateBirthLocation.Width = 175;
            // 
            // MatchIndividualID
            // 
            this.MatchIndividualID.DataPropertyName = "MatchIndividualID";
            this.MatchIndividualID.HeaderText = "Match ID";
            this.MatchIndividualID.MinimumWidth = 10;
            this.MatchIndividualID.Name = "MatchIndividualID";
            this.MatchIndividualID.ReadOnly = true;
            this.MatchIndividualID.Width = 50;
            // 
            // MatchName
            // 
            this.MatchName.DataPropertyName = "MatchName";
            this.MatchName.HeaderText = "Match Name";
            this.MatchName.MinimumWidth = 50;
            this.MatchName.Name = "MatchName";
            this.MatchName.ReadOnly = true;
            this.MatchName.Width = 150;
            // 
            // MatchBirthDate
            // 
            this.MatchBirthDate.DataPropertyName = "MatchBirthDate";
            this.MatchBirthDate.HeaderText = "Match Birthdate";
            this.MatchBirthDate.MinimumWidth = 50;
            this.MatchBirthDate.Name = "MatchBirthDate";
            this.MatchBirthDate.ReadOnly = true;
            this.MatchBirthDate.Width = 150;
            // 
            // MatchBirthLocation
            // 
            this.MatchBirthLocation.DataPropertyName = "MatchBirthLocation";
            this.MatchBirthLocation.HeaderText = "Match Birth Location";
            this.MatchBirthLocation.MinimumWidth = 100;
            this.MatchBirthLocation.Name = "MatchBirthLocation";
            this.MatchBirthLocation.ReadOnly = true;
            this.MatchBirthLocation.Width = 175;
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
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MainForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 521);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.tabSelector);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(580, 499);
            this.Name = "MainForm";
            this.Text = "Family Tree Analyzer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.MainForm_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.MainForm_DragEnter);
            this.Move += new System.EventHandler(this.MainForm_Move);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.mnuSetRoot.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgRegions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCountries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgDataErrors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tbDuplicateScore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCheckAncestors)).EndInit();
            this.tabWorldWars.ResumeLayout(false);
            this.tabWorldWars.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgWorldWars)).EndInit();
            this.ctxViewNotes.ResumeLayout(false);
            this.tabTreetops.ResumeLayout(false);
            this.tabTreetops.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTreeTops)).EndInit();
            this.tabColourReports.ResumeLayout(false);
            this.tabColourReports.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tabLostCousins.ResumeLayout(false);
            this.LCSubTabs.ResumeLayout(false);
            this.LCReportsTab.ResumeLayout(false);
            this.LCReportsTab.PerformLayout();
            this.Referrals.ResumeLayout(false);
            this.Referrals.PerformLayout();
            this.LCUpdatesTab.ResumeLayout(false);
            this.LCUpdatesTab.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.LCVerifyTab.ResumeLayout(false);
            this.LCVerifyTab.PerformLayout();
            this.tabCensus.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.udAgeFilter)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
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
            this.tabDisplayProgress.ResumeLayout(false);
            this.splitGedcom.Panel1.ResumeLayout(false);
            this.splitGedcom.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGedcom)).EndInit();
            this.splitGedcom.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabSelector.ResumeLayout(false);
            this.tabMainLists.ResumeLayout(false);
            this.tabMainListsSelector.ResumeLayout(false);
            this.tabIndividuals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIndividuals)).EndInit();
            this.tabFamilies.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgFamilies)).EndInit();
            this.tabSources.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgSources)).EndInit();
            this.tabOccupations.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgOccupations)).EndInit();
            this.tabCustomFacts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgCustomFacts)).EndInit();
            this.tabErrorsFixes.ResumeLayout(false);
            this.tabErrorFixSelector.ResumeLayout(false);
            this.tabDataErrors.ResumeLayout(false);
            this.gbDataErrorTypes.ResumeLayout(false);
            this.tabDuplicates.ResumeLayout(false);
            this.tabDuplicates.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgDuplicates)).EndInit();
            this.tabLooseBirths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseBirths)).EndInit();
            this.tabLooseDeaths.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseDeaths)).EndInit();
            this.tabLooseInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgLooseInfo)).EndInit();
            this.tabSurnames.ResumeLayout(false);
            this.tabSurnames.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSurnames)).EndInit();
            this.tabFacts.ResumeLayout(false);
            this.tabFacts.PerformLayout();
            this.tabToday.ResumeLayout(false);
            this.tabToday.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudToday)).EndInit();
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
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtColouredSurname;
        private FTAnalyzer.Forms.Controls.RelationTypes relTypesColoured;
        private System.Windows.Forms.TabPage tabLostCousins;
        private System.Windows.Forms.TabPage tabCensus;
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
        private System.Windows.Forms.Button btnShowFacts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtFactsSurname;
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
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCensusSurname;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown udAgeFilter;
        private FTAnalyzer.Forms.Controls.CensusDateSelector cenDate;
        private FTAnalyzer.Forms.Controls.RelationTypes relTypesCensus;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbColourFamily;
        private System.Windows.Forms.ToolStripMenuItem onlineGuidesToUsingFTAnalyzerToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar tspbTabProgress;
        private System.Windows.Forms.ToolStripMenuItem mnuOSGeocoder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripMenuItem mnuLookupBlankFoundLocations;
        private System.Windows.Forms.CheckBox ckbMilitaryOnly;
        private System.Windows.Forms.Button btnRandomSurnameColour;
        private System.Windows.Forms.CheckedListBox ckbFactExclude;
        private System.Windows.Forms.Button btnShowExclusions;
        private System.Windows.Forms.Label lblExclude;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btnDeselectExcludeAllFactTypes;
        private System.Windows.Forms.Button btnExcludeAllFactTypes;
        private System.Windows.Forms.ToolStripMenuItem mnuCloseGEDCOM;
        private System.Windows.Forms.Button btnDuplicateFacts;
        private System.Windows.Forms.TabPage tabToday;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.DateTimePicker dpToday;
        private Utilities.ScrollingRichTextBox rtbToday;
        private System.Windows.Forms.RadioButton rbTodayMonth;
        private System.Windows.Forms.RadioButton rbTodaySingle;
        private System.Windows.Forms.Label labToday;
        private System.Windows.Forms.ProgressBar pbToday;
        private System.Windows.Forms.Button btnUpdateTodaysEvents;
        private System.Windows.Forms.Label label18;
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
        private FTAnalyzer.Forms.Controls.VirtualDGVDataErrors dgDataErrors;
        private System.Windows.Forms.GroupBox gbDataErrorTypes;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnClearAll;
        private System.Windows.Forms.CheckedListBox ckbDataErrors;
        private System.Windows.Forms.TabPage tabDuplicates;
        private System.Windows.Forms.CheckBox ckbHideIgnoredDuplicates;
        private System.Windows.Forms.Button btnCancelDuplicates;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TrackBar tbDuplicateScore;
        private System.Windows.Forms.Label labCalcDuplicates;
        private System.Windows.Forms.ProgressBar pbDuplicates;
        private FTAnalyzer.Forms.Controls.VirtualDGVDuplicates dgDuplicates;
        private System.Windows.Forms.TabPage tabLooseBirths;
        private System.Windows.Forms.DataGridView dgLooseBirths;
        private System.Windows.Forms.TabPage tabLooseDeaths;
        private System.Windows.Forms.DataGridView dgLooseDeaths;
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
        private System.Windows.Forms.Label label11;
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
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox txtLCEmail;
        private System.Windows.Forms.MaskedTextBox txtLCPassword;
        private System.Windows.Forms.CheckBox chkLCRootPersonConfirm;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.RichTextBox rtbLCUpdateData;
        private Utilities.ScrollingRichTextBox rtbLCoutput;
        private System.Windows.Forms.SplitContainer splitGedcom;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label LbProgramName;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ProgressBar pbRelationships;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ProgressBar pbFamilies;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ProgressBar pbIndividuals;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar pbSources;
        private Utilities.ScrollingRichTextBox rtbOutput;
        private System.Windows.Forms.CheckBox chkSurnamesIgnoreCase;
        private System.Windows.Forms.Button btnLCPotentialUploads;
        private System.Windows.Forms.Button btnViewInvalidRefs;
        private System.Windows.Forms.TabPage tabLooseInfo;
        private System.Windows.Forms.DataGridView dgLooseInfo;
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
        private System.Windows.Forms.Label labDuplicateSlider;
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
    }
}

