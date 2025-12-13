using FTAnalyzer.Forms.Controls;
using FTAnalyzer.Exports;
using FTAnalyzer.Filters;
using FTAnalyzer.Forms;
using FTAnalyzer.Properties;
using FTAnalyzer.UserControls;
using FTAnalyzer.Utilities;
using Printing.DataGridViewPrint.Tools;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Text;
using System.Xml;
using HtmlAgilityPack;
using System.Diagnostics;
using FTAnalyzer.Windows;
using ICSharpCode.SharpZipLib.Zip;

namespace FTAnalyzer
{
    public partial class MainForm : Form
    {
        public static readonly string VERSION = "10.0.0.0-beta 12";
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(MainForm));

        Cursor storedCursor = Cursors.Default;
        readonly FamilyTree ft = FamilyTree.Instance;
        bool stopProcessing;
        string filename;
        readonly PrivateFontCollection fonts = new();
        Font handwritingFont;
        Font boldFont;
        Font normalFont;
        bool loading;
        bool WWI;
        VirtualReportFormHelper<IDisplayDuplicateIndividual> rfhDuplicates;

        public MainForm()
        {
            try
            {
                loading = true;
                InitializeComponent();
                FamilyTree.Instance.Version = $"v{VERSION}";
                _ = NativeMethods.GetTaskBarPos(); // Sets taskbar offset
                displayOptionsOnLoadToolStripMenuItem.Checked = GeneralSettings.Default.ReportOptions;
                treetopsRelation.MarriedToDB = false;
                ShowMenus(false);
                string logMessage = $"Started FTAnalyzer version {VERSION}";
                log.Info(logMessage);
                int pos = VERSION.IndexOf('-');
                string ver = pos > 0 ? VERSION[..VERSION.IndexOf('-')] : VERSION;
                DatabaseHelper.Instance.CheckDatabaseVersion(new Version(ver));
                CheckSystemVersion();
                if (!Application.ExecutablePath.Contains("WindowsApps"))
                    CheckWebVersion(); // check for web version if not windows store app
                SetSavePath();
                BuildRecentList();
            }
            catch (Exception e)
            {
                UIHelpers.ShowMessage($"FTAnalyzer encountered a problem whilst starting up error was : {e.Message}");
            }
        }


        void MainForm_Load(object sender, EventArgs e)
        {
            SetupFonts();
            RegisterEventHandlers();
            Text = $"Family Tree Analyzer v{VERSION}";
            SetHeightWidth();
            rfhDuplicates = new(this, "Duplicates", dgDuplicates, ResetDuplicatesTable, "Duplicates", false);
            ft.LoadStandardisedNames(Application.StartupPath);
            tsCountLabel.Text = string.Empty;
            tsHintsLabel.Text = "Welcome to Family Tree Analyzer, if you have any questions please raise them on the User group - see help menu for details";
            loading = false;
        }

        static void CheckSystemVersion()
        {
            OperatingSystem os = Environment.OSVersion;
            if (os.Version.Major == 6 && os.Version.Minor < 2)
                UIHelpers.ShowMessage("Please note Microsoft has ended Windows 7 support as such it is no longer advisable to be connected to the internet using it. Any security flaws that are unpatched may be being actively exploited by hackers. You should upgrade as soon as possible.\n\nPlease be aware that FTAnalyzer may be unstable on an outdated unsupported Operating System.");
        }

        static async Task CheckWebVersion()
        {
            try
            {
                Settings.Default.StartTime = DateTime.Now;
                Settings.Default.Save();
                HtmlAgilityPack.HtmlDocument doc;
                using (HttpClient client = new())
                {
                    doc = new HtmlAgilityPack.HtmlDocument();
                    string webData = await client.GetStringAsync("https://github.com/ShammyLevva/FTAnalyzer");
                    doc.LoadHtml(webData);
                }
                HtmlNode? versionNode = doc.DocumentNode.SelectSingleNode("//div[@class='d-flex']/span");
                if (versionNode is not null)
                {
                    string webVersion = versionNode.InnerText.ToUpper().Replace("VERSION", "").Trim();
                    string thisVersion = VERSION;
                    if (VERSION.Contains("-beta"))
                        thisVersion = VERSION[..VERSION.IndexOf('-')];
                    Version web = new(webVersion);
                    Version local = new(thisVersion);
                    if (web > local)
                    {
                        string text = $"Version installed: {VERSION}, Web version available: {webVersion}\nDo you want to go to website to download the latest version?\nSelect Cancel to visit release website for older machines.";
                        DialogResult download = UIHelpers.ShowMessage(text, "FTAnalyzer", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (download == DialogResult.Yes)
                            SpecialMethods.VisitWebsite("https://www.microsoft.com/en-gb/p/ftanalyzer/9pmjl9hvpl7x?cid=clickonceappupgrade");
                        if (download == DialogResult.Cancel)
                            SpecialMethods.VisitWebsite("https://github.com/ShammyLevva/FTAnalyzer/releases");
                    }
                    await Analytics.CheckProgramUsageAsync();
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                UIHelpers.ShowMessage("Unable to check website for new version please check https://github.com/ShammyLevva/FTAnalyzer/releases to see if you are running the latest version.", "FTAnalyzer");
            }
        }


        void SetupFonts()
        {
            try
            {
                SpecialMethods.SetFonts(this);
                byte[] fontData = Resources.KUNSTLER;
                IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
                System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
                uint dummy = 0;
                fonts.AddMemoryFont(fontPtr, Resources.KUNSTLER.Length);
                NativeMethods.AddFontMemResourceEx(fontPtr, (uint)Resources.KUNSTLER.Length, IntPtr.Zero, ref dummy);
                System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);
                switch (FontSettings.Default.FontNumber)
                {
                    case 1:
                        handwritingFont = new(fonts.Families[0], 46.0F, FontStyle.Bold);
                        boldFont = new(dgCountries.DefaultCellStyle.Font.FontFamily, 8.25F, FontStyle.Bold);
                        normalFont = new(dgCountries.DefaultCellStyle.Font.FontFamily, 8.25F, FontStyle.Regular);
                        FontSettings.Default.FontHeight = 22;
                        break;
                    case 2:
                        handwritingFont = new(fonts.Families[0], 60.0F, FontStyle.Bold);
                        boldFont = new(dgCountries.DefaultCellStyle.Font.FontFamily, 10F, FontStyle.Bold);
                        normalFont = new(dgCountries.DefaultCellStyle.Font.FontFamily, 10F, FontStyle.Regular);
                        FontSettings.Default.FontHeight = 27;
                        break;
                    case 3:
                        handwritingFont = new(fonts.Families[0], 68.0F, FontStyle.Bold);
                        boldFont = new(dgCountries.DefaultCellStyle.Font.FontFamily, 12F, FontStyle.Bold);
                        normalFont = new(dgCountries.DefaultCellStyle.Font.FontFamily, 12F, FontStyle.Regular);
                        FontSettings.Default.FontHeight = 32;
                        break;
                    case 4:
                        handwritingFont = new(fonts.Families[0], 76.0F, FontStyle.Bold);
                        boldFont = new(dgCountries.DefaultCellStyle.Font.FontFamily, 14F, FontStyle.Bold);
                        normalFont = new(dgCountries.DefaultCellStyle.Font.FontFamily, 14F, FontStyle.Regular);
                        FontSettings.Default.FontHeight = 37;
                        break;
                }
                SetInitialScreenControls();
                UpdateDataErrorsDisplay();
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Exception {e.Message}");
            } // for font sizing exception
        }

        void SetInitialScreenControls()
        {
            int progressBarLeft = labRelationships.Right + 15;
            pbSources.Left = progressBarLeft;
            pbIndividuals.Left = progressBarLeft;
            pbFamilies.Left = progressBarLeft;
            pbRelationships.Left = progressBarLeft;
            pbSources.Refresh();
            pbIndividuals.Refresh();
            pbFamilies.Refresh();
            pbRelationships.Refresh();
            LbProgramName.Left = pbRelationships.Right + 15;
            LbProgramName.Font = handwritingFont;
            LbProgramName.Refresh();
            pictureBox1.Left = LbProgramName.Right;
            pictureBox1.Refresh();
            Width = Math.Min(pictureBox1.Right + 100, Screen.GetWorkingArea(new Point(0, 0)).Width);
            splitGedcom.SplitterDistance = Math.Max(pbRelationships.Bottom + 18, 110);
            splitGedcom.Refresh();
            menuStrip1.Font = normalFont;
            rtbOutput.Font = normalFont;
            rtbToday.Font = normalFont;
            rtbLostCousins.Font = normalFont;
            treeViewLocations.Font = normalFont;
            SetStatusBar();
            CheckMaxWindowSizes(new Point(0, 0));
            Refresh();
        }

        private void SetStatusBar()
        {
            tsCountLabel.Font = normalFont;
            tsHintsLabel.Font = normalFont;
            tsStatusLabel.Font = normalFont;
            statusStrip.Height = FontSettings.Default.FontHeight;
            tsCountLabel.Height = FontSettings.Default.FontHeight;
            tsHintsLabel.Height = FontSettings.Default.FontHeight;
            tsStatusLabel.Height = FontSettings.Default.FontHeight;
        }


        void RegisterEventHandlers()
        {
            Options.ReloadRequired += new EventHandler(Options_ReloadData);
            GeneralSettingsUI.MinParentalAgeChanged += new EventHandler(Options_MinimumParentalAgeChanged);
            GeneralSettingsUI.AliasInNameChanged += new EventHandler(Options_AliasInNameChanged);
            FontSettingsUI.GlobalFontChanged += new EventHandler(Options_GlobalFontChanged);
        }


        void SetHeightWidth()
        {
            MainForm mainForm = this;
            // load height & width from registry - note need to use temp variables as setting them causes form
            // to resize thus setting the values for both
            int Width = (int)Application.UserAppDataRegistry.GetValue("Mainform size - width", mainForm.Width);
            int Height = (int)Application.UserAppDataRegistry.GetValue("Mainform size - height", mainForm.Height);
            int Top = (int)Application.UserAppDataRegistry.GetValue("Mainform position - top", mainForm.Top);
            int Left = (int)Application.UserAppDataRegistry.GetValue("Mainform position - left", mainForm.Left);
            string maxState = (WindowState == FormWindowState.Maximized).ToString();
            string maximised = Application.UserAppDataRegistry.GetValue("Mainform maximised", maxState).ToString() ?? maxState;
            Point leftTop = ReportFormHelper.CheckIsOnScreen(Top, Left);
            if (leftTop.X < 0) leftTop.X = 0;
            if (leftTop.Y < 0) leftTop.Y = 0;
            mainForm.Width = Width;
            mainForm.Height = Height;
            mainForm.Top = leftTop.Y;
            mainForm.Left = leftTop.X;
            CheckMaxWindowSizes(leftTop);
            if (maximised == "True")
                WindowState = FormWindowState.Maximized;
        }

        #region Load File

        async Task LoadFileAsync(string filename)
        {
            try
            {
                HourGlass(this, true);
                this.filename = filename;
                CloseGEDCOM(false);
                if (!stopProcessing)
                {
                    if (await LoadTreeAsync(filename).ConfigureAwait(true))
                    {
                        SetDataErrorsCheckedDefaults(ckbDataErrors);
                        SetupFactsCheckboxes();
                        AddFileToRecentList(filename);
                        Text = $"Family Tree Analyzer v{VERSION}. Analysing: {filename}";
                        Application.UseWaitCursor = false;
                        mnuCloseGEDCOM.Enabled = true;
                        EnableLoadMenus();
                        ShowMenus(true);
                        UIHelpers.ShowMessage($"Gedcom File {filename} Loaded", "FTAnalyzer");
                    }
                    else
                        CleanUp(true);
                }
            }
            catch (IOException ex)
            {
                UIHelpers.ShowMessage($"Error: Could not read file from disk. Original error: {ex.Message}", "FTAnalyzer");
            }
            catch (Exception ex2)
            {
                string message = ex2.Message + "\n" + (ex2.InnerException is not null ? ex2.InnerException.Message : string.Empty);
                UIHelpers.ShowMessage($"Error: Problem processing your file. Please try again.\n" +
                    $"If this problem persists please report this at https://www.ftanalyzer.com/issues. Error was: {message}\n{ex2.InnerException}", "FTAnalyzer");
                CleanUp(true);
            }
            finally
            {
                HourGlass(this, false);
            }
        }

        async Task<bool> LoadTreeAsync(string filename)
        {
            Progress<string> outputText = new(rtbOutput.AppendText);
            XmlDocument? doc;
            Stopwatch timer = new();
            timer.Start();
            using (FileStream stream = new(filename, FileMode.Open, FileAccess.Read))
            {
                doc = await Task.Run(() => ft.LoadTreeHeader(filename, stream, outputText)).ConfigureAwait(true);
            }
            if (doc is null)
            {
                timer.Stop();
                return false;
            }
            timer.Stop();
            WriteTime("File Loaded", outputText, timer);
            timer.Start();
            ft.DocumentLoaded = true;
            var sourceProgress = new Progress<int>(value => { pbSources.Value = value; });
            var individualProgress = new Progress<int>(value => { pbIndividuals.Value = value; });
            var familyProgress = new Progress<int>(value => { pbFamilies.Value = value; });
            var RelationshipProgress = new Progress<int>(value => { pbRelationships.Value = value; });
            await Task.Run(() => ft.LoadTreeSources(doc, sourceProgress, outputText)).ConfigureAwait(true);
            await Task.Run(() => ft.LoadTreeIndividuals(doc, individualProgress, outputText)).ConfigureAwait(true);
            await Task.Run(() => ft.LoadTreeFamilies(doc, familyProgress, outputText)).ConfigureAwait(true);
            await Task.Run(() => ft.LoadTreeRelationships(doc, RelationshipProgress, outputText)).ConfigureAwait(true);
            await Task.Run(() => FamilyTree.CleanUpXML()).ConfigureAwait(true);
            doc = null;
            ft.DocumentLoaded = false;
            timer.Stop();
            WriteTime("\nFile Loaded and Analysed", outputText, timer);
            WriteMemory(outputText);
            return true;
        }

        static void WriteTime(string prefixText, IProgress<string> outputText, Stopwatch timer)
        {
            TimeSpan ts = timer.Elapsed;
            // Format and display the TimeSpan value.
            string elapsedTime = string.Format("{0:00}h {1:00}m {2:00}.{3:00}s", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            outputText.Report($"{prefixText} in {elapsedTime}\n\n");
        }

        static void WriteMemory(IProgress<string> outputText)
        {
            long memoryBefore = GC.GetTotalMemory(false);
            long memoryAfter = GC.GetTotalMemory(true);
            string sizeBefore = SpecialMethods.SizeSuffix(memoryBefore, 2);
            string sizeAfter = SpecialMethods.SizeSuffix(memoryAfter, 2);
            outputText.Report($"File used {sizeBefore} during loading, reduced to {sizeAfter} after processing.");
        }

        void EnableLoadMenus()
        {
            openToolStripMenuItem.Enabled = true;
            databaseToolStripMenuItem.Enabled = true;
            mnuRestore.Enabled = false;
            mnuLoadLocationsCSV.Enabled = false;
        }

        void CloseGEDCOM(bool keepOutput)
        {
            DisposeIndividualForms();
            ShowMenus(false);
            tabSelector.SelectTab(tabDisplayProgress);
            if (!keepOutput)
                rtbOutput.Text = string.Empty;
            tsCountLabel.Text = string.Empty;
            tsHintsLabel.Text = string.Empty;
            tsStatusLabel.Text = string.Empty;
            statusStrip.Refresh();
            rtbLCoutput.Text = string.Empty;
            rtbLCUpdateData.Text = string.Empty;
            rtbCheckAncestors.Text = string.Empty;
            rtbToday.Text = string.Empty;
            pbSources.Value = 0;
            pbIndividuals.Value = 0;
            pbFamilies.Value = 0;
            pbRelationships.Value = 0;
            SetupGridControls();
            cmbReferrals.Items.Clear();
            cmbReferrals.Text = string.Empty;
            ClearColourFamilyCombo();
            Statistics.Instance.Clear();
            btnReferrals.Enabled = false;
            openToolStripMenuItem.Enabled = false;
            databaseToolStripMenuItem.Enabled = false;
            mnuRecent.Enabled = false;
            tabMainListsSelector.SelectedTab = tabIndividuals; // force back to first tab
            tabErrorFixSelector.SelectedTab = tabDataErrors; //force tab back to data errors tab
            tabCtrlLocations.SelectedTab = tabTreeView; // otherwise totals etc look wrong
            treeViewLocations.Nodes.Clear();
            Text = "Family Tree Analyzer v" + VERSION;
        }

        void SetupGridControls()
        {
            // set datasources for locations in reverse order to avoid null pointer cell formatting race condition
            dgPlaces.DataSource = null;
            dgAddresses.DataSource = null;
            dgSubRegions.DataSource = null;
            dgRegions.DataSource = null;
            dgCountries.DataSource = null;
            dgIndividuals.DataSource = null;
            dgFamilies.DataSource = null;
            dgTreeTops.DataSource = null;
            dgWorldWars.DataSource = null;
            dgLooseBirths.DataSource = null;
            dgLooseDeaths.DataSource = null;
            dgLooseInfo.DataSource = null;
            dgDataErrors.DataSource = null;
            dgOccupations.DataSource = null;
            dgSurnames.DataSource = null;
            dgDuplicates.DataSource = null;
            dgSources.DataSource = null;
            dgCustomFacts.DataSource = null;
            dgCheckAncestors.DataSource = null;
            ExtensionMethods.DoubleBuffered(dgPlaces, true);
            ExtensionMethods.DoubleBuffered(dgAddresses, true);
            ExtensionMethods.DoubleBuffered(dgSubRegions, true);
            ExtensionMethods.DoubleBuffered(dgRegions, true);
            ExtensionMethods.DoubleBuffered(dgCountries, true);
            ExtensionMethods.DoubleBuffered(dgIndividuals, true);
            ExtensionMethods.DoubleBuffered(dgFamilies, true);
            ExtensionMethods.DoubleBuffered(dgTreeTops, true);
            ExtensionMethods.DoubleBuffered(dgWorldWars, true);
            ExtensionMethods.DoubleBuffered(dgLooseBirths, true);
            ExtensionMethods.DoubleBuffered(dgLooseDeaths, true);
            ExtensionMethods.DoubleBuffered(dgLooseInfo, true);
            ExtensionMethods.DoubleBuffered(dgDataErrors, true);
            ExtensionMethods.DoubleBuffered(dgOccupations, true);
            ExtensionMethods.DoubleBuffered(dgSurnames, true);
            ExtensionMethods.DoubleBuffered(dgDuplicates, true);
            ExtensionMethods.DoubleBuffered(dgSources, true);
            ExtensionMethods.DoubleBuffered(dgCustomFacts, true);
            ExtensionMethods.DoubleBuffered(dgCheckAncestors, true);
        }

        static void SetSavePath()
        {
            try
            {
                GeneralSettings.Default.SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Family Tree Analyzer");
                if (!Directory.Exists(GeneralSettings.Default.SavePath))
                    Directory.CreateDirectory(GeneralSettings.Default.SavePath);
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage($"Found a problem starting up.\nPlease report this at https://www.ftanalyzer.com/issues\nThe error was :{ex.Message}", "FTAnalyzer");
            }
        }


        async void OpenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Settings.Default.LoadLocation))
                openGedcom.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                openGedcom.InitialDirectory = Settings.Default.LoadLocation;
            openGedcom.FileName = "*.ged";
            openGedcom.Filter = "GED files (*.ged)|*.ged|All files (*.*)|*.*";
            openGedcom.FilterIndex = 1;
            openGedcom.RestoreDirectory = true;

            if (openGedcom.ShowDialog() == DialogResult.OK)
            {
                await LoadFileAsync(openGedcom.FileName).ConfigureAwait(true);
                Settings.Default.LoadLocation = Path.GetFullPath(openGedcom.FileName);
                Settings.Default.Save();
                await Analytics.TrackAction(Analytics.MainFormAction, Analytics.LoadGEDCOMEvent).ConfigureAwait(true);
            }
        }

        void MnuCloseGEDCOM_Click(object sender, EventArgs e)
        {
            if (!loading)
                CleanUp(false);
        }

        void CleanUp(bool retainText)
        {
            CloseGEDCOM(retainText);
            ft.ResetData();
            EnableLoadMenus();
            mnuRestore.Enabled = true;
            mnuLoadLocationsCSV.Enabled = true;
            mnuCloseGEDCOM.Enabled = false;
            mnuDuplicatesToExcel.Enabled = false;
            BuildRecentList();
        }
        #endregion

        void ShowMenus(bool enabled)
        {
            mnuPrint.Enabled = enabled;
            mnuReload.Enabled = enabled;
            mnuCloseGEDCOM.Enabled = enabled;
            mnuFactsToExcel.Enabled = enabled;
            mnuIndividualsToExcel.Enabled = enabled;
            mnuFamiliesToExcel.Enabled = enabled;
            MnuExportLocations.Enabled = enabled;
            mnuSourcesToExcel.Enabled = enabled;
            mnuDataErrorsToExcel.Enabled = enabled;
            mnuSurnamesToExcel.Enabled = enabled;
            mnuLooseBirthsToExcel.Enabled = enabled;
            mnuLooseDeathsToExcel.Enabled = enabled;
            mnuChildAgeProfiles.Enabled = enabled;
            mnuOlderParents.Enabled = enabled;
            mnuBirthdayEffect.Enabled = enabled;
            mnuPossibleCensusFacts.Enabled = enabled;
            mnuPossiblyMissingChildReport.Enabled = enabled;
            mnuShowTimeline.Enabled = enabled;
            mnuGeocodeLocations.Enabled = enabled;
            mnuOSGeocoder.Enabled = enabled;
            mnuLocationsGeocodeReport.Enabled = enabled;
            mnuLifelines.Enabled = enabled;
            mnuPlaces.Enabled = enabled;
            mnuCousinsCountReport.Enabled = enabled;
            mnuHowManyGreats.Enabled = enabled;
            MnuAgedOver99Report.Enabled = enabled;
            mnuLookupBlankFoundLocations.Enabled = enabled;
            MnuSingleParentsReport.Enabled = enabled;
            mnuTreetopsToExcel.Enabled = enabled && dgTreeTops.RowCount > 0;
            mnuWorldWarsToExcel.Enabled = enabled && dgWorldWars.RowCount > 0;
            mnuDNA_GEDCOM.Enabled = enabled;
            mnuJSON.Enabled = enabled;
            mnuGoogleMyMaps.Enabled = enabled;
            MnuCustomFactsToExcel.Enabled = enabled;
        }

        static void HourGlass(Form form, bool on) => form.UseWaitCursor = on;

        void DgCountries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                HourGlass(this, true);
                var loc = (FactLocation)dgCountries.CurrentRowDataBoundItem;
                var frmInd = new People();
                frmInd.SetLocation(loc, FactLocation.COUNTRY);
                DisposeDuplicateForms(frmInd);
                frmInd.Show();
                HourGlass(this, false);
            }
        }

        void DgRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                HourGlass(this, true);
                var loc = dgRegions.CurrentRow is null ? FactLocation.BLANK_LOCATION : (FactLocation)dgRegions.CurrentRowDataBoundItem;
                var frmInd = new People();
                frmInd.SetLocation(loc, FactLocation.REGION);
                DisposeDuplicateForms(frmInd);
                frmInd.Show();
                HourGlass(this, false);
            }
        }

        void DgSubRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                HourGlass(this, true);
                var loc = (FactLocation)dgSubRegions.CurrentRowDataBoundItem;
                var frmInd = new People();
                frmInd.SetLocation(loc, FactLocation.SUBREGION);
                DisposeDuplicateForms(frmInd);
                frmInd.Show();
                HourGlass(this, false);
            }
        }

        void DgAddresses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                HourGlass(this, true);
                var loc = (FactLocation)dgAddresses.CurrentRowDataBoundItem;
                var frmInd = new People();
                frmInd.SetLocation(loc, FactLocation.ADDRESS);
                DisposeDuplicateForms(frmInd);
                frmInd.Show();
                HourGlass(this, false);
            }
        }

        void DgPlaces_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                HourGlass(this, true);
                var loc = (FactLocation)dgPlaces.CurrentRowDataBoundItem;
                var frmInd = new People();
                frmInd.SetLocation(loc, FactLocation.PLACE);
                DisposeDuplicateForms(frmInd);
                frmInd.Show();
                HourGlass(this, false);
            }
        }

        void RtbOutput_TextChanged(object sender, EventArgs e) => rtbOutput.ScrollToBottom();
        void RtbLCoutput_TextChanged(object sender, EventArgs e) => rtbLCoutput.ScrollToBottom();
        void RtbCheckAncestors_TextChanged(object sender, EventArgs e) => rtbCheckAncestors.ScrollToBottom();

        bool shutdown;

        async void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!shutdown)
            {
                shutdown = true;
                e.Cancel = true;
                await Analytics.EndProgramAsync().ConfigureAwait(true);
                Close();
            }
            DatabaseHelper.Instance.Dispose();
            stopProcessing = true;
        }

        void BtnTreeTops_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> filter = CreateTreeTopsIndividualFilter();
            List<IDisplayIndividual> treeTopsList = [.. ft.GetTreeTops(filter)];
            treeTopsList.Sort(new BirthDateComparer());
            dgTreeTops.DataSource = [.. treeTopsList];
            dgTreeTops.Focus();
            foreach (DataGridViewColumn c in dgTreeTops.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = Messages.Count + treeTopsList.Count;
            tsHintsLabel.Text = Messages.Hints_Individual;
            mnuPrint.Enabled = true;
            dgTreeTops.VirtualGridFiltered += VirtualGridFiltered;
            ShowMenus(true);
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.TreetopsEvent);
        }

        Predicate<Individual> warDeadFilter;

        void BtnWWI_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            WWI = true;
            warDeadFilter = CreateWardeadIndividualFilter(new FactDate("BET 1869 AND 1904"), new FactDate("FROM 28 JUL 1914"));
            List<IDisplayIndividual> warDeadList = [.. ft.GetWorldWars(warDeadFilter)];
            warDeadList.Sort(new BirthDateComparer(BirthDateComparer.ASCENDING));
            dgWorldWars.DataSource = [.. warDeadList];
            dgWorldWars.Focus();
            foreach (DataGridViewColumn c in dgWorldWars.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = Messages.Count + warDeadList.Count;
            tsHintsLabel.Text = $"{Messages.Hints_Individual}  {Messages.Hints_LivesOfFirstWorldWar}";
            dgWorldWars.VirtualGridFiltered += VirtualGridFiltered;
            mnuPrint.Enabled = true;
            ShowMenus(true);
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.WWIReportEvent);
        }

        void BtnWWII_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            WWI = false;
            warDeadFilter = CreateWardeadIndividualFilter(new FactDate("BET 1894 AND 1931"), new FactDate("FROM 1 SEP 1939"));
            List<IDisplayIndividual> warDeadList = [.. ft.GetWorldWars(warDeadFilter)];
            warDeadList.Sort(new BirthDateComparer(BirthDateComparer.ASCENDING));
            dgWorldWars.DataSource = [.. warDeadList];
            dgWorldWars.Focus();
            foreach (DataGridViewColumn c in dgWorldWars.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = Messages.Count + warDeadList.Count;
            tsHintsLabel.Text = Messages.Hints_Individual;
            dgWorldWars.VirtualGridFiltered += VirtualGridFiltered;
            mnuPrint.Enabled = true;
            ShowMenus(true);
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.WWIIReportEvent);
        }

        void LinkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => SpecialMethods.VisitWebsite("https://forums.lc");

        void DgOccupations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                HourGlass(this, true);
                var occ = (DisplayOccupation)dgOccupations.CurrentRowDataBoundItem;
                var frmInd = new People();
                frmInd.SetWorkers(occ.Occupation, ft.AllWorkers(occ.Occupation));
                DisposeDuplicateForms(frmInd);
                frmInd.Show();
                HourGlass(this, false);
            }
        }

        void DgCustomFacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                HourGlass(this, true);
                var customFacts = (DisplayCustomFact)dgCustomFacts.CurrentRowDataBoundItem;
                var frmCustomFacts = new People();
                frmCustomFacts.SetCustomFacts(customFacts.CustomFactName, ft.AllCustomFactIndividuals(customFacts.CustomFactName), ft.AllCustomFactFamilies(customFacts.CustomFactName));
                DisposeDuplicateForms(frmCustomFacts);
                frmCustomFacts.Show();
                HourGlass(this, false);
            }
        }

        void DgCustomFacts_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            var customFact = (DisplayCustomFact)dgCustomFacts.CurrentRowDataBoundItem;
            DatabaseHelper.IgnoreCustomFact(customFact.CustomFactName, customFact.Ignore);
        }

        void SetAsRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            var ind = (Individual)dgIndividuals.CurrentRowDataBoundItem;
            if (ind is not null)
            {
                var outputText = new Progress<string>(value => { rtbOutput.AppendText(value); });
                ft.UpdateRootIndividual(ind.IndividualID, null, outputText);
                dgIndividuals.Refresh();
                UIHelpers.ShowMessage($"Root person set as {ind.Name}\n\n{ft.PrintRelationCount()}", "FTAnalyzer");
            }
            HourGlass(this, false);
        }

        void MnuSetRoot_Opened(object sender, EventArgs e)
        {
            var ind = (Individual)dgIndividuals.CurrentRowDataBoundItem;
            if (ind is not null)
                viewNotesToolStripMenuItem.Enabled = ind.HasNotes;
        }

        void ViewNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Individual ind = (Individual)dgIndividuals.CurrentRowDataBoundItem;
            if (ind is not null)
            {
                Notes notes = new(ind);
                notes.Show();
            }
            HourGlass(this, false);
        }

        void BtnShowMap_Click(object sender, EventArgs e)
        {
            float zoom = GetMapZoomLevel(out FactLocation loc);
            if (loc is not null && loc.IsGeoCoded(false))
            {
                string URL = $"https://www.google.com/maps/@{loc.Latitude},{loc.Longitude},{zoom}z";
                SpecialMethods.VisitWebsite(URL);
            }
            else
                UIHelpers.ShowMessage($"{loc} is not yet geocoded so can't be displayed.");
        }

        void BtnOSMap_Click(object sender, EventArgs e)
        {
            bool oldOSMap = (sender as Button).Name == "btnOldOSMap";
            {
                float zoom = GetMapZoomLevel(out FactLocation loc);
                if (loc is not null && loc.IsGeoCoded(false))
                {
                    if (loc.IsWithinUKBounds)
                    {
                        if (oldOSMap)
                        {
                            string URL = $"https://maps.nls.uk/geo/explore/#zoom={zoom}&lat={loc.Latitude}&lon={loc.Longitude}&layers=1&b=1";
                            SpecialMethods.VisitWebsite(URL);
                        }
                    }
                    else
                        UIHelpers.ShowMessage($"{loc} is outwith the UK so cannot be shown on a UK OS Map.");
                }
                else
                    UIHelpers.ShowMessage($"{loc} is not yet geocoded so can't be displayed.");
            }
        }

        float GetMapZoomLevel(out FactLocation loc)
        {
            // get the tab
            loc = null;
            try
            {
                switch (tabCtrlLocations.SelectedTab.Text)
                {
                    case "Tree View":
                        TreeNode node = treeViewLocations.SelectedNode;
                        if (node is not null)
                            loc = node.Text == "<blank>" ? null : ((FactLocation)node.Tag).GetLocation(node.Level);
                        break;
                    case "Countries":
                        loc = dgCountries.CurrentRow is null ? null : (FactLocation)dgCountries.CurrentRowDataBoundItem;
                        break;
                    case "Regions":
                        loc = dgRegions.CurrentRow is null ? null : (FactLocation)dgRegions.CurrentRowDataBoundItem;
                        break;
                    case "SubRegions":
                        loc = dgSubRegions.CurrentRow is null ? null : (FactLocation)dgSubRegions.CurrentRowDataBoundItem;
                        break;
                    case "Addresses":
                        loc = dgAddresses.CurrentRow is null ? null : (FactLocation)dgAddresses.CurrentRowDataBoundItem;
                        break;
                    case "Places":
                        loc = dgPlaces.CurrentRow is null ? null : (FactLocation)dgPlaces.CurrentRowDataBoundItem;
                        break;
                }
                if (loc is null)
                {
                    if (tabCtrlLocations.SelectedTab.Text == "Tree View")
                        UIHelpers.ShowMessage("Location selected isn't valid to show on the map.", "FTAnalyzer");
                    else
                        UIHelpers.ShowMessage("Nothing selected. Please select a location to show on the map.", "FTAnalyzer");
                    return 0f;
                }
                return loc.ZoomLevel;
            }
            catch (NullReferenceException)
            {
                return 0f;
            }
        }

        #region DataErrors
        void CkbDataErrors_SelectedIndexChanged(object sender, EventArgs e) => UpdateDataErrorsDisplay();


        void UpdateDataErrorsDisplay()
        {
            HourGlass(this, true);
            SortableBindingList<IDisplayDataError> errors = DataErrors(ckbDataErrors);
            dgDataErrors.DataSource = errors;
            tsCountLabel.Text = Messages.Count + errors.Count;
            tsHintsLabel.Text = Messages.Hints_Individual;
            dgDataErrors.VirtualGridFiltered += VirtualGridFiltered;
            int index = 0;
            int maxwidth = 0;
            try
            {
                foreach (DataErrorGroup dataError in ckbDataErrors.Items)
                {
                    if (dataError.ToString().Length > maxwidth)
                        maxwidth = dataError.ToString().Length;
                    bool itemChecked = ckbDataErrors.GetItemChecked(index++);
                    Application.UserAppDataRegistry.SetValue(dataError.ToString(), itemChecked);
                }
            }
            catch (IOException)
            {
                UIHelpers.ShowMessage("Unable to save DataError preferences. Please check App has rights to save user preferences to registry.");
            }
            var scaling = GraphicsUtilities.GetCurrentScaling();
            ckbDataErrors.ColumnWidth = (int)(maxwidth * FontSettings.Default.FontWidth * scaling);
            HourGlass(this, false);
        }

        public void SetDataErrorsCheckedDefaults(CheckedListBox list)
        {
            list.Items.Clear();
            foreach (DataErrorGroup dataError in ft.DataErrorTypes)
            {
                int index = list.Items.Add(dataError);
                bool itemChecked = Application.UserAppDataRegistry.GetValue(dataError.ToString(), "True").Equals("True");
                list.SetItemChecked(index, itemChecked);
            }
        }

        void BtnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbDataErrors.Items.Count; i++)
            {
                ckbDataErrors.SetItemChecked(i, true);
            }
            UpdateDataErrorsDisplay();
        }

        void BtnClearAll_Click(object sender, EventArgs e)
        {
            foreach (int indexChecked in ckbDataErrors.CheckedIndices)
            {
                ckbDataErrors.SetItemChecked(indexChecked, false);
            }
            UpdateDataErrorsDisplay();
        }

        void SetupDataErrors()
        {
            dgDataErrors.DataSource = DataErrors(ckbDataErrors);
            dgDataErrors.Focus();
            mnuPrint.Enabled = true;
            UpdateDataErrorsDisplay();
            dgDataErrors.BringToFront();
        }

        public static SortableBindingList<IDisplayDataError> DataErrors(CheckedListBox list)
        {
            List<IDisplayDataError> errors = [];
            foreach (int indexChecked in list.CheckedIndices)
            {
                DataErrorGroup item = (DataErrorGroup)list.Items[indexChecked];
                errors.AddRange(item.Errors);
            }
            return [.. errors];
        }
        #endregion

        void ChildAgeProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIHelpers.ShowMessage("Sorry this report is currently Unavailable.");
            //Statistics s = Statistics.Instance;
            //Chart chart = new Chart();
            //int[,,] stats = s.ChildrenBirthProfiles();
            //chart.BuildChildBirthProfile(stats);
            //DisposeDuplicateForms(chart);
            //chart.Show();
            //Analytics.TrackAction(Analytics.MainFormAction, Analytics.BirthProfileEvent);
            //UIHelpers.ShowMessage(s.BuildOutput(stats), "Birth Profile Information");
        }

        void ViewOnlineManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.OnlineManualEvent);
            SpecialMethods.VisitWebsite("https://www.ftanalyzer.com");
        }

        void OnlineGuidesToUsingFTAnalyzerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.OnlineGuideEvent);
            SpecialMethods.VisitWebsite("https://www.ftanalyzer.com/guides");
        }

        void PrivacyPolicyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.PrivacyEvent);
            SpecialMethods.VisitWebsite("https://www.ftanalyzer.com/privacy");
        }

        void OlderParentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            People frmInd = new();
            string inputAge = "50";
            DialogResult result = DialogResult.Cancel;
            int age = 0;
            do
            {
                try
                {
                    result = InputBox.Show("Enter age between 13 and 90", "Please select minimum age to report on", ref inputAge);
                    age = int.Parse(inputAge);
                }
                catch (Exception)
                {
                    if (result != DialogResult.Cancel)
                        UIHelpers.ShowMessage("Invalid Age entered", "FTAnalyzer");
                }
                if (age < 13 || age > 90)
                    UIHelpers.ShowMessage("Please enter an age between 13 and 90", "FTAnalyzer");
            } while ((result != DialogResult.Cancel) && (age < 13 || age > 90));
            if (result == DialogResult.OK)
            {
                if (frmInd.OlderParents(age))
                {
                    DisposeDuplicateForms(frmInd);
                    frmInd.Show();
                    Analytics.TrackAction(Analytics.MainFormAction, Analytics.OlderParentsEvent);
                }
            }
            HourGlass(this, false);
        }

        void CkbTTIgnoreLocations_CheckedChanged(object sender, EventArgs e) => treetopsCountry.Enabled = !ckbTTIgnoreLocations.Checked;

        void CkbWDIgnoreLocations_CheckedChanged(object sender, EventArgs e) => wardeadCountry.Enabled = !ckbWDIgnoreLocations.Checked;

        void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
            Application.Exit();
        }

        void TabCtrlLocations_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                HourGlass(this, true); // turn on when tab selected so all the formatting gets hourglass
            }
            catch (Exception) // attempt to fix font issue
            { }
        }

        void TabCtrlLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                HourGlass(this, true);
                Application.DoEvents();
                TabPage current = tabCtrlLocations.SelectedTab ?? tabCtrlLocations.TabPages[0];
                Control control = current.Controls[0];
                control.Focus();
                if (control is VirtualDGVLocations dg)
                {
                    tsCountLabel.Text = $"{Messages.Count}{dg.RowCount} {dg.Name[2..]}";
                    mnuPrint.Enabled = true;
                    dg.VirtualGridFiltered += VirtualGridFiltered;
                }
                else
                {
                    tsCountLabel.Text = string.Empty;
                    mnuPrint.Enabled = false;
                }
                tsHintsLabel.Text = Messages.Hints_Location;
                HourGlass(this, false);
            }
            catch (Exception) // attempt to fix font issue
            { }
        }

        #region CellFormatting
        void FormatCellLocations(VirtualDGVLocations grid, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (e.ColumnIndex == 0)
                {
                    string country = (string)cell.Value;
                    if (Countries.IsKnownCountry(country))
                        e.CellStyle.Font = boldFont;
                    else
                        e.CellStyle.Font = normalFont;
                }
                else if (e.ColumnIndex == 1)
                {
                    string region = (string)cell.Value;
                    if (region.Length > 0 && Regions.IsKnownRegion(region))
                        e.CellStyle.Font = boldFont;
                    else
                        e.CellStyle.Font = normalFont;
                }
                else
                {
                    FactLocation loc = (FactLocation)grid.DataBoundItem(e.RowIndex);
                    cell.ToolTipText = $"Geocoding Status : {loc.Geocoded}";
                }
            }
            catch (Exception) { }
        }

        void DgCountries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == dgCountries?.Columns[nameof(IDisplayLocation.Icon)].Index)
                FormatCellLocations(dgCountries, e);
        }

        void DgRegions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex <= 1 || e.ColumnIndex == dgCountries?.Columns[nameof(IDisplayLocation.Icon)].Index)
                FormatCellLocations(dgRegions, e);
        }

        void DgSubRegions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex <= 1 || e.ColumnIndex == dgCountries?.Columns[nameof(IDisplayLocation.Icon)].Index)
                FormatCellLocations(dgSubRegions, e);
        }

        void DgAddresses_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex <= 1 || e.ColumnIndex == dgCountries?.Columns[nameof(IDisplayLocation.Icon)].Index)
                FormatCellLocations(dgAddresses, e);
        }

        void DgPlaces_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex <= 1 || e.ColumnIndex == dgCountries?.Columns[nameof(IDisplayLocation.Icon)].Index)
                FormatCellLocations(dgPlaces, e);
        }
        #endregion

        #region EventHandlers
        void Options_BaptismChanged(object? sender, EventArgs e)
        {
            // do anything that needs doing when option changes
        }

        async void Options_ReloadData(object? sender, EventArgs e) => await QueryReloadData().ConfigureAwait(true);

        void Options_MinimumParentalAgeChanged(object? sender, EventArgs e)
        {
            ft.ResetLooseFacts();
            if (tabSelector.SelectedTab == tabErrorsFixes && tabErrorFixSelector.SelectedTab.Equals(tabLooseBirths))
                SetupLooseBirths();
            if (tabSelector.SelectedTab == tabErrorsFixes && tabErrorFixSelector.SelectedTab.Equals(tabLooseDeaths))
                SetupLooseDeaths();
        }

        void Options_AliasInNameChanged(object? sender, EventArgs e) => ft.SetFullNames();

        void Options_GlobalFontChanged(object? sender, EventArgs e)
        {
            HourGlass(this, true);
            SetupFonts();
            HourGlass(this, false);
        }
        #endregion

        #region Reload Data
        async Task QueryReloadData()
        {
            if (GeneralSettings.Default.ReloadRequired && ft.DataLoaded)
            {
                DialogResult dr = UIHelpers.ShowMessage("This option requires the data to be refreshed.\n\nDo you want to reload now?\n\nClicking no will keep the data with the old option.", "Reload GEDCOM File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                GeneralSettings.Default.ReloadRequired = false;
                GeneralSettings.Default.Save();
                if (dr == DialogResult.Yes)
                {
                    await LoadFileAsync(filename).ConfigureAwait(true);
                }
            }
        }

        async void ReloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralSettings.Default.ReloadRequired = false;
            GeneralSettings.Default.Save();
            await LoadFileAsync(filename).ConfigureAwait(true);
        }
        #endregion

        bool preventExpand;

        void TreeViewLocations_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            HourGlass(this, true);
            var location = e.Node.Tag as FactLocation;
            if (location is not null)
            {
                if (ft.CountPeopleAtLocation(location, e.Node.Level) == 0)
                    UIHelpers.ShowMessage($"You have no one in your file at {location}.");
                else
                {
                    var frmInd = new People();
                    frmInd.SetLocation(location, e.Node.Level);
                    DisposeDuplicateForms(frmInd);
                    frmInd.Show();
                }
            }
            HourGlass(this, false);
        }

        void TreeViewLocations_BeforeCollapse(object sender, TreeViewCancelEventArgs e) => e.Cancel = preventExpand && e.Action == TreeViewAction.Collapse;

        void TreeViewLocations_BeforeExpand(object sender, TreeViewCancelEventArgs e) => e.Cancel = preventExpand && e.Action == TreeViewAction.Expand;

        void TreeViewLocations_MouseDown(object sender, MouseEventArgs e) => preventExpand = e.Clicks > 1;

        void DisplayOptionsOnLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GeneralSettings.Default.ReportOptions = displayOptionsOnLoadToolStripMenuItem.Checked;
            GeneralSettings.Default.Save();
        }

        void ReportAnIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.ReportIssueEvent);
            SpecialMethods.VisitWebsite("https://github.com/ShammyLevva/FTAnalyzer/issues");
        }

        void WhatsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.WhatsNewEvent);
            SpecialMethods.VisitWebsite("https://www.ftanalyzer.com/Whats%20New%20in%20this%20Release");
        }

        void MnuShowTimeline_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            TimeLine tl = new(new Progress<string>(value => { rtbOutput.AppendText(value); }));
            DisposeDuplicateForms(tl);
            tl.Show();
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MapsAction, Analytics.ShowTimelinesEvent);
        }

        enum GecodingType { Google = 1, OS = 2, Reverse = 3 }

        void MnuGeocodeLocations_Click(object sender, EventArgs e)
        {
            StartGeocoding(GecodingType.Google);
            Analytics.TrackAction(Analytics.GeocodingAction, Analytics.GoogleGeocodingEvent);
        }

        void MnuOSGeocoder_Click(object sender, EventArgs e)
        {
            StartGeocoding(GecodingType.OS);
            Analytics.TrackAction(Analytics.GeocodingAction, Analytics.OSGeocodingEvent);
        }

        void MnuLookupBlankFoundLocations_Click(object sender, EventArgs e)
        {
            StartGeocoding(GecodingType.Reverse);
            Analytics.TrackAction(Analytics.GeocodingAction, Analytics.ReverseGeocodingEvent);
        }

        void StartGeocoding(GecodingType type)
        {
            if (!ft.Geocoding) // don't geocode if another geocode session in progress
            {
                try
                {
                    HourGlass(this, true);
                    GeocodeLocations? geo = null;
                    foreach (Form f in Application.OpenForms)
                    {
                        if (f is GeocodeLocations locations)
                        {
                            geo = locations;
                            break;
                        }
                    }
                    geo ??= new GeocodeLocations(new Progress<string>(value => { rtbOutput.AppendText(value); }));
                    geo.Show();
                    geo.Focus();
                    Application.DoEvents();
                    switch (type)
                    {
                        case GecodingType.Google:
                            geo.StartGoogleGeoCoding(false);
                            break;
                        case GecodingType.OS:
                            geo.StartOSGeoCoding();
                            break;
                        case GecodingType.Reverse:
                            geo.StartReverseGeoCoding();
                            break;
                    }
                    HourGlass(this, false);
                }
                catch (Exception) { }
            }
        }

        void LocationsGeocodeReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            GeocodeLocations geo = new(new Progress<string>(value => { rtbOutput.AppendText(value); }));
            DisposeDuplicateForms(geo);
            geo.Show();
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MapsAction, Analytics.GeocodesEvent);
        }

        void TreeViewLocations_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (treeViewLocations.SelectedNode != e.Node && e.Button.Equals(MouseButtons.Right))
                    treeViewLocations.SelectedNode = e.Node;
            }
            catch (Exception) { }
        }

        void TreeViewLocations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                treeViewLocations.SelectedImageIndex = e.Node.ImageIndex;
            }
            catch (Exception) { }
        }

        void MnuLifelines_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            LifeLine l = new(new Progress<string>(value => { rtbOutput.AppendText(value); }));
            DisposeDuplicateForms(l);
            l.Show();
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MapsAction, Analytics.LifelinesEvent);
        }

        void MnuPlaces_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Places p = new(new Progress<string>(value => { rtbOutput.AppendText(value); }));
            DisposeDuplicateForms(p);
            p.Show();
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MapsAction, Analytics.ShowPlacesEvent);
        }

        void DgSurnames_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                HourGlass(this, true);
                IDisplaySurnames stat = dgSurnames.CurrentRowDataBoundItem;
                People frmInd = new();
                Predicate<Individual> indFilter = reltypesSurnames.BuildFilter<Individual>(x => x.RelationType);
                Predicate<Family> famFilter = reltypesSurnames.BuildFamilyFilter<Family>(x => x.RelationTypes);
                frmInd.SetSurnameStats(stat, indFilter, famFilter, chkSurnamesIgnoreCase.Checked);
                DisposeDuplicateForms(frmInd);
                frmInd.Show();
                HourGlass(this, false);
                Analytics.TrackAction(Analytics.MainFormAction, Analytics.ViewAllSurnameEvent);
            }
        }

        void DgSurnames_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                DataGridViewCell cell = dgSurnames.Rows[e.RowIndex].Cells[nameof(IDisplaySurnames.Surname)];
                if (cell.Value.ToString() is not null)
                {
                    HourGlass(this, true);
                    Statistics.DisplayGOONSpage(cell.Value.ToString());
                    Analytics.TrackAction(Analytics.MainFormAction, Analytics.GOONSEvent);
                    HourGlass(this, false);
                }
            }
        }

        void PossibleCensusFactsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            var predicate = new Predicate<Individual>(x => x.Notes.Contains("census", StringComparison.CurrentCultureIgnoreCase));
            var censusNotes = ft.AllIndividuals.Filter(predicate).ToList();
            var people = new People();
            people.SetIndividuals(censusNotes, "List of Possible Census records incorrectly recorded as notes");
            DisposeDuplicateForms(people);
            people.Show();
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.PossibleCensusEvent);
        }

        #region Tab Control
        void TabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                mnuPrint.Enabled = false;
                tsCountLabel.Text = string.Empty;
                tsHintsLabel.Text = string.Empty;
                tspbTabProgress.Visible = false;
                if (ft.Loading)
                {
                    tabSelector.SelectedTab = tabDisplayProgress;
                }
                else
                {
                    if (!ft.DataLoaded)
                    {   // do not process anything if no GEDCOM yet loaded
                        if (tabSelector.SelectedTab != tabDisplayProgress)
                        {
                            tabSelector.SelectedTab = tabDisplayProgress;
                            mnuRestore.Enabled = true;
                            mnuLoadLocationsCSV.Enabled = true;
                            UIHelpers.ShowMessage(ErrorMessages.FTA_0002, "FTAnalyzer Error : FTA_0002");
                        }
                        return;
                    }
                    HourGlass(this, true);
                    if (tabSelector.SelectedTab == tabDisplayProgress)
                    {
                        mnuPrint.Enabled = true;
                    }
                    if (tabSelector.SelectedTab == tabMainLists)
                    {
                        if (dgIndividuals.DataSource is null)
                            SetupIndividualsTab(); // select individuals tab if first time opening main lists tab
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.MainListsEvent);
                    }
                    if (tabSelector.SelectedTab == tabErrorsFixes)
                    {
                        if (dgDataErrors.DataSource is null)
                            SetupDataErrors(); // select data errors tab if first time opening errors fixes tab
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.ErrorsFixesEvent);
                    }
                    else if (tabSelector.SelectedTab == tabFacts)
                    {
                        // already cleared text just set preferred facts button
                        radioAllFacts.Checked = true;
                        radioOnlyAlternate.Enabled = GeneralSettings.Default.IncludeAlternateFacts;
                        panelFactsSurname.Left = relTypesFacts.Right + relTypesFacts.Margin.Right + panelFactsSurname.Margin.Left;
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.FactsTabEvent);
                    }
                    else if (tabSelector.SelectedTab == tabSurnames)
                    {
                        // show empty form click button to load
                        btnShowSurnames.Left = reltypesSurnames.Width + btnShowSurnames.Margin.Left;
                        chkSurnamesIgnoreCase.Left = btnShowSurnames.Left + btnShowSurnames.Width + btnShowSurnames.Margin.Right;
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.SurnamesTabEvent);
                    }
                    else if (tabSelector.SelectedTab == tabCensus)
                    {
                        cenDate.RevertToDefaultDate();
                        btnShowCensusMissing.Enabled = ft.IndividualCount > 0;
                        cenDate.AddAllCensusItems();
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.CensusTabEvent);
                    }
                    else if (tabSelector.SelectedTab == tabResearchSuggestions)
                    {
                        gbFilters.Left = relTypesResearchSuggest.Right + relTypesResearchSuggest.Margin.Right + gbFilters.Margin.Left;
                    }
                    else if (tabSelector.SelectedTab == tabTreetops)
                    {
                        dgTreeTops.DataSource = null;
                        treetopsCountry.Enabled = !ckbTTIgnoreLocations.Checked;
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.TreetopsTabEvent);
                    }
                    else if (tabSelector.SelectedTab == tabWorldWars)
                    {
                        dgWorldWars.DataSource = null;
                        wardeadCountry.Enabled = !ckbWDIgnoreLocations.Checked;
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.WorldWarsTabEvent);
                    }
                    else if (tabSelector.SelectedTab == tabLostCousins)
                    {
                        HourGlass(this, true);
                        btnLC1881EW.Enabled = btnLC1881Scot.Enabled = btnLC1841EW.Enabled =
                            btnLC1881Canada.Enabled = btnLC1880USA.Enabled = btnLC1911Ireland.Enabled =
                            btnLC1911EW.Enabled = ft.IndividualCount > 0;
                        LCSubTabs.TabPages.Remove(LCVerifyTab); // hide verification tab as it does nothing
                        UpdateLCReports();
                        txtLCEmail.Text = Application.UserAppDataRegistry.GetValue("LostCousinsEmail", string.Empty).ToString();
                        chkLCRootPersonConfirm.Text = $"Confirm {ft.RootPerson} as root Person";
                        tabLostCousins.Refresh();
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.LostCousinsTabEvent);
                        HourGlass(this, false);
                    }
                    else if (tabSelector.SelectedTab == tabToday)
                    {
                        bool todaysMonth = Application.UserAppDataRegistry.GetValue("Todays Events Month", "False").Equals("True");
                        int todaysStep = int.Parse(Application.UserAppDataRegistry.GetValue("Todays Events Step", "5").ToString() ?? "5");
                        rbTodayMonth.Checked = todaysMonth;
                        nudToday.Value = todaysStep;
                        Analytics.TrackAction(Analytics.MainFormAction, Analytics.TodayTabEvent);
                    }
                    else if (tabSelector.SelectedTab == tabLocations)
                    {
                        HourGlass(this, true);
                        LoadLocations();
                    }
                    HourGlass(this, false);
                }
            }
            catch (Exception) { }
        }

        void LoadLocations()
        {
            tabCtrlLocations.SelectedIndex = 0;
            tsCountLabel.Text = string.Empty;
            tsHintsLabel.Text = Messages.Hints_Location;
            tspbTabProgress.Visible = true;
            treeViewLocations.Nodes.Clear();
            TreeNode[] nodes = TreeViewHandler.Instance.GetAllLocationsTreeNodes(true, tspbTabProgress);
            try
            {
                treeViewLocations.Nodes.AddRange(nodes);
            }
            catch (ArgumentException fEx)
            {
                Debug.WriteLine(fEx.Message); // typically font loading error
            }
            mnuPrint.Enabled = false;
            dgCountries.DataSource = ft.AllDisplayCountries;
            dgRegions.DataSource = ft.AllDisplayRegions;
            dgSubRegions.DataSource = ft.AllDisplaySubRegions;
            dgAddresses.DataSource = ft.AllDisplayAddresses;
            dgPlaces.DataSource = ft.AllDisplayPlaces;
            tspbTabProgress.Visible = false;
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.LocationTabViewed);
        }

        void TabMainListSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabMainListsSelector.SelectedTab == tabIndividuals)
            {
                SetupIndividualsTab();
                Analytics.TrackAction(Analytics.MainListsAction, Analytics.IndividualsTabEvent);
            }
            else if (tabMainListsSelector.SelectedTab == tabFamilies)
            {
                SetupFamiliesTab();
                Analytics.TrackAction(Analytics.MainListsAction, Analytics.FamilyTabEvent);
            }
            else if (tabMainListsSelector.SelectedTab == tabSources)
            {
                SortableBindingList<IDisplaySource> list = ft.AllDisplaySources;
                dgSources.DataSource = list;
                dgSources.Sort(dgSources.Columns[nameof(IDisplaySource.SourceID)], ListSortDirection.Ascending);
                dgSources.Focus();
                mnuPrint.Enabled = true;
                tsCountLabel.Text = Messages.Count + list.Count.ToString("N0");
                tsHintsLabel.Text = Messages.Hints_Sources;
                dgSources.VirtualGridFiltered += VirtualGridFiltered;
                Analytics.TrackAction(Analytics.MainListsAction, Analytics.SourcesTabEvent);
            }
            else if (tabMainListsSelector.SelectedTab == tabOccupations)
            {
                SortableBindingList<IDisplayOccupation> list = ft.AllDisplayOccupations;
                dgOccupations.DataSource = list;
                dgOccupations.Sort(dgOccupations.Columns[nameof(IDisplayOccupation.Occupation)], ListSortDirection.Ascending);
                dgOccupations.Focus();
                mnuPrint.Enabled = true;
                tsCountLabel.Text = Messages.Count + list.Count.ToString("N0");
                tsHintsLabel.Text = Messages.Hints_Occupation;
                dgOccupations.VirtualGridFiltered += VirtualGridFiltered;
                Analytics.TrackAction(Analytics.MainListsAction, Analytics.OccupationsTabEvent);
            }
            else if (tabMainListsSelector.SelectedTab == tabCustomFacts)
            {
                SortableBindingList<IDisplayCustomFact> list = ft.AllCustomFacts;
                dgCustomFacts.DataSource = list;
                dgCustomFacts.Sort(dgCustomFacts.Columns[nameof(IDisplayCustomFact.CustomFactName)], ListSortDirection.Ascending);
                dgCustomFacts.Focus();
                dgCustomFacts.Columns[nameof(IDisplayCustomFact.Ignore)].ReadOnly = false;
                dgCustomFacts.Columns[nameof(IDisplayCustomFact.Ignore)].ToolTipText = "Tick box to ignore warnings for this custom fact type.";
                mnuPrint.Enabled = true;
                tsCountLabel.Text = Messages.Count + list.Count.ToString("N0");
                tsHintsLabel.Text = Messages.Hints_CustomFacts;
                dgCustomFacts.VirtualGridFiltered += VirtualGridFiltered;
                Analytics.TrackAction(Analytics.MainListsAction, Analytics.CustomFactTabEvent);
            }
        }

        private void SetupFamiliesTab()
        {
            SortableBindingList<IDisplayFamily> list = ft.AllDisplayFamilies;
            dgFamilies.DataSource = list;
            dgFamilies.Sort(dgFamilies.Columns[nameof(IDisplayFamily.FamilyID)], ListSortDirection.Ascending);
            dgFamilies.Focus();
            mnuPrint.Enabled = true;
            tsCountLabel.Text = Messages.Count + list.Count.ToString("N0");
            tsHintsLabel.Text = Messages.Hints_Family;
            dgFamilies.VirtualGridFiltered += VirtualGridFiltered;
        }

        void SetupIndividualsTab()
        {
            SortableBindingList<IDisplayIndividual> list = ft.AllDisplayIndividuals;
            dgIndividuals.DataSource = list;
            dgIndividuals.Sort(dgIndividuals.Columns[nameof(IDisplayIndividual.IndividualID)], ListSortDirection.Ascending);
            dgIndividuals.AllowUserToResizeColumns = true;
            dgIndividuals.Focus();
            mnuPrint.Enabled = true;
            tsCountLabel.Text = Messages.Count + list.Count.ToString("N0");
            tsHintsLabel.Text = Messages.Hints_Individual;
            dgIndividuals.VirtualGridFiltered += VirtualGridFiltered;
            ScrollBarDebug.LogScreenData(this, dgIndividuals, "SetupIndividualsTab");
        }

        void VirtualGridFiltered(object? sender, CountEventArgs e) => tsCountLabel.Text = e.FilterText;

        async void TabErrorFixSelector_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabErrorFixSelector.SelectedTab == tabDataErrors)
                SetupDataErrors();
            else if (tabErrorFixSelector.SelectedTab == tabDuplicates)
            {
                rfhDuplicates.LoadColumnLayout("DuplicatesColumns.xml");
                ckbHideIgnoredDuplicates.Checked = GeneralSettings.Default.HideIgnoredDuplicates;
                await SetPossibleDuplicates().ConfigureAwait(true);
                dgDuplicates.Focus();
                mnuPrint.Enabled = true;
                await Analytics.TrackAction(Analytics.ErrorsFixesAction, Analytics.DuplicatesTabEvent).ConfigureAwait(true);
            }
            if (tabErrorFixSelector.SelectedTab == tabLooseBirths)
            {
                if (dgLooseBirths.DataSource is null)
                    SetupLooseBirths();
                await Analytics.TrackAction(Analytics.ErrorsFixesAction, Analytics.LooseBirthsEvent).ConfigureAwait(true);
            }
            else if (tabErrorFixSelector.SelectedTab == tabLooseDeaths)
            {
                if (dgLooseDeaths.DataSource is null)
                    SetupLooseDeaths();
                await Analytics.TrackAction(Analytics.ErrorsFixesAction, Analytics.LooseDeathsEvent).ConfigureAwait(true);
            }
            else if (tabErrorFixSelector.SelectedTab == tabLooseInfo)
            {
                if (dgLooseInfo.DataSource is null)
                    SetupLooseInfo();
                await Analytics.TrackAction(Analytics.ErrorsFixesAction, Analytics.LooseInfoEvent).ConfigureAwait(true);
            }
        }

        #endregion

        #region Filters
        Predicate<ExportFact> CreateFactsFilter()
        {
            var filter = relTypesFacts.BuildFilter<ExportFact>(x => x.RelationType);
            if (txtFactsSurname.Text.Length > 0)
            {
                var surnameFilter = FilterUtils.StringFilter<ExportFact>(x => x.Surname, txtFactsSurname.Text.Trim());
                filter = FilterUtils.AndFilter(filter, surnameFilter);
            }
            return filter;
        }

        Predicate<Individual> CreateAliveatDateFilter(FactDate aliveDate, string surname)
        {
            var relationFilter = relTypesCensus.BuildFilter<Individual>(x => x.RelationType);
            var dateFilter = new Predicate<Individual>(x => x.IsPossiblyAlive(aliveDate));
            Predicate<Individual> filter = FilterUtils.AndFilter(relationFilter, dateFilter);
            if (surname.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, surname);
                filter = FilterUtils.AndFilter(filter, surnameFilter);
            }
            if (chkExcludeUnknownBirths.Checked)
                filter = FilterUtils.AndFilter(x => x.BirthDate.IsKnown, filter);
            return filter;
        }

        Predicate<CensusIndividual> CreateCensusIndividualFilter(CensusDate censusDate, bool censusDone, string surname)
        {
            Predicate<CensusIndividual> relationFilter = relTypesCensus.BuildFilter<CensusIndividual>(x => x.RelationType);
            Predicate<CensusIndividual> dateFilter = censusDone ?
                new(x => x.IsCensusDone(censusDate) && !x.OutOfCountry(censusDate)) :
                new(x => !x.IsCensusDone(censusDate) && !x.OutOfCountry(censusDate));
            Predicate<CensusIndividual> filter = FilterUtils.AndFilter(relationFilter, dateFilter);
            if (!censusDone && GeneralSettings.Default.HidePeopleWithMissingTag)
            {  //if we are reporting missing from census and we are hiding people who have a missing tag then only select those who are not tagged missing
                Predicate<CensusIndividual> missingTag = new(x => !x.IsTaggedMissingCensus(censusDate));
                filter = FilterUtils.AndFilter(filter, missingTag);
            }
            if (surname.Length > 0)
            {
                Predicate<CensusIndividual> surnameFilter = FilterUtils.StringFilter<CensusIndividual>(x => x.Surname, surname);
                filter = FilterUtils.AndFilter(filter, surnameFilter);
            }
            if (chkExcludeUnknownBirths.Checked)
                filter = FilterUtils.AndFilter(x => x.BirthDate.IsKnown, filter);
            filter = FilterUtils.AndFilter(x => x.Age.MinAge < (int)udAgeFilter.Value, filter);
            return filter;
        }
        Predicate<Individual> CreateIndividualCensusFilter(CensusDate cemsusDate, bool censusDone, string surname, bool anyCensus)
        {
            Predicate<Individual> filter;
            var relationFilter = relTypesCensus.BuildFilter<Individual>(x => x.RelationType);
            if (anyCensus) // only filter on date if not selecting any date
            {
                filter = relationFilter;
            }
            else
            {
                Predicate<Individual> dateFilter = censusDone ?
                    new(x => x.IsCensusDone(cemsusDate) && !x.OutOfCountry(cemsusDate)) :
                    new(x => !x.IsCensusDone(cemsusDate) && !x.OutOfCountry(cemsusDate));
                filter = FilterUtils.AndFilter(relationFilter, dateFilter);
            }
            if (!censusDone && GeneralSettings.Default.HidePeopleWithMissingTag)
            {  //if we are reporting missing from census and we are hiding people who have a missing tag then only select those who are not tagged missing
                Predicate<Individual> missingTag = new(x => !x.IsTaggedMissingCensus(cemsusDate));
                filter = FilterUtils.AndFilter(filter, missingTag);
            }
            if (surname.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, surname);
                filter = FilterUtils.AndFilter(filter, surnameFilter);
            }
            if (chkExcludeUnknownBirths.Checked)
                filter = FilterUtils.AndFilter(x => x.BirthDate.IsKnown, filter);
            filter = FilterUtils.AndFilter(x => x.GetMinAge(cemsusDate) < (int)udAgeFilter.Value, filter);
            return filter;
        }

        Predicate<Individual> CreateTreeTopsIndividualFilter()
        {
            Predicate<Individual> treetopFilter = ckbTTIncludeOnlyOneParent.Checked ?
                new(ind => ind.HasOnlyOneParent || !ind.HasParents) : new(ind => !ind.HasParents);
            Predicate<Individual> locationFilter = treetopsCountry.BuildFilter<Individual>(FactDate.UNKNOWN_DATE, (d, x) => x.BestLocation(d));
            Predicate<Individual> relationFilter = treetopsRelation.BuildFilter<Individual>(x => x.RelationType);
            Predicate<Individual> filter = FilterUtils.AndFilter(locationFilter, relationFilter);
            filter = ckbTTIgnoreLocations.Checked ? relationFilter : FilterUtils.AndFilter(locationFilter, relationFilter);

            if (txtTreetopsSurname.Text.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, txtTreetopsSurname.Text);
                filter = FilterUtils.AndFilter(filter, surnameFilter);
            }
            filter = FilterUtils.AndFilter(filter, treetopFilter);
            return filter;
        }

        Predicate<Individual> CreateWardeadIndividualFilter(FactDate birthRange, FactDate deathRange)
        {
            Predicate<Individual> filter;
            Predicate<Individual> locationFilter = wardeadCountry.BuildFilter<Individual>(FactDate.UNKNOWN_DATE, (d, x) => x.BestLocation(d));
            Predicate<Individual> relationFilter = wardeadRelation.BuildFilter<Individual>(x => x.RelationType);
            Predicate<Individual> birthFilter = FilterUtils.DateFilter<Individual>(x => x.BirthDate, birthRange);
            Predicate<Individual> deathFilter = FilterUtils.DateFilter<Individual>(x => x.DeathDate, deathRange);

            if (ckbWDIgnoreLocations.Checked)
                filter = FilterUtils.AndFilter(FilterUtils.AndFilter(birthFilter, deathFilter), relationFilter);
            else
                filter = FilterUtils.AndFilter(FilterUtils.AndFilter(birthFilter, deathFilter), FilterUtils.AndFilter(locationFilter, relationFilter));

            if (txtWorldWarsSurname.Text.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, txtWorldWarsSurname.Text);
                filter = FilterUtils.AndFilter(filter, surnameFilter);
            }
            if (ckbMilitaryOnly.Checked)
                filter = FilterUtils.AndFilter(filter, x => x.HasMilitaryFacts);

            return filter;
        }
        #endregion

        #region Lost Cousins
        void CkbRestrictions_CheckedChanged(object sender, EventArgs e) => UpdateLCReports();

        void LostCousinsCensus(CensusDate censusDate, string reportTitle)
        {
            HourGlass(this, true);
            Census census = new(censusDate, true);
            Predicate<CensusIndividual> relationFilter = relTypesLC.BuildFilter<CensusIndividual>(x => x.RelationType);
            Predicate<Individual> individualRelationFilter = relTypesLC.BuildFilter<Individual>(x => x.RelationType);
            census.SetupLCCensus(relationFilter, ckbShowLCEntered.Checked, individualRelationFilter);
            if (ckbShowLCEntered.Checked)
                census.Text = $"{reportTitle} already entered into Lost Cousins website (includes entries with no country)";
            else
                census.Text = $"{reportTitle} to enter into Lost Cousins website";
            DisposeDuplicateForms(census);
            census.Show();
            Task.Run(() => Analytics.TrackActionAsync(Analytics.LostCousinsAction, Analytics.LCReportYearEvent, censusDate.BestYear.ToString()));
            HourGlass(this, false);
        }

        async void BtnLCLogin_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            try
            {
                Application.UserAppDataRegistry.SetValue("LostCousinsEmail", txtLCEmail.Text);
            }
            catch (IOException)
            {
                UIHelpers.ShowMessage("Error unable to save Lost Cousins email address preference. Please check App has rights to save user preferences to registry.");
            }
            bool websiteAvailable = await Program.LCClient.LostCousinsLoginAsync(txtLCEmail.Text, txtLCPassword.Text);
            btnLCLogin.BackColor = websiteAvailable ? Color.LightGreen : Color.Red;
            btnLCLogin.Enabled = !websiteAvailable;
            btnUpdateLostCousinsWebsite.Visible = websiteAvailable;
            btnCheckMyAncestors.BackColor = websiteAvailable ? Color.LightGreen : Color.Red;
            lblCheckAncestors.Text = websiteAvailable ? "Logged into Lost Cousins" : "Not Currently Logged in Use Updates Page to Login";
            HourGlass(this, false);
            if (websiteAvailable)
                UIHelpers.ShowMessage("Lost Cousins login succeeded.");
            else
                UIHelpers.ShowMessage("Unable to login to Lost Cousins website. Check email/password and try again.");
        }

        List<CensusIndividual> LCUpdates;
        List<CensusIndividual> LCInvalidReferences;

        void BtnLCPotentialUploads_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Census census = new(CensusDate.ANYCENSUS, true);
            census.SetupLCupdateList(LCUpdates);
            census.Text = $"Potential Records to upload to Lost Cousins Website";
            DisposeDuplicateForms(census);
            Analytics.TrackAction(Analytics.LostCousinsAction, Analytics.PreviewLostCousins);
            census.Show();
            HourGlass(this, false);
        }

        void BtnViewInvalidRefs_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Census census = new(CensusDate.ANYCENSUS, true);
            census.SetupLCupdateList(LCInvalidReferences);
            census.Text = $"Incompatible Census References in Records to upload to Lost Cousins Website";
            DisposeDuplicateForms(census);
            Analytics.TrackAction(Analytics.LostCousinsAction, Analytics.PreviewLostCousins);
            census.Show();
            HourGlass(this, false);
        }

        async void BtnUpdateLostCousinsWebsite_Click(object sender, EventArgs e)
        {
            btnUpdateLostCousinsWebsite.Enabled = false;
            if (LCUpdates?.Count > 0)
            {
                rtbLCoutput.Text = string.Empty;
                int response = UIHelpers.ShowYesNo($"You have {LCUpdates.Count} possible records to add to Lost Cousins. Proceed?", "Lost Cousins");
                if (response == UIHelpers.Yes)
                {
                    rtbLCoutput.Text = "Started Processing Lost Cousins entries.\n\n";
                    Progress<string> outputText = new(value => { rtbLCoutput.AppendText(value); });
                    int count = await Task.Run(() => ExportToLostCousins.ProcessListAsync(LCUpdates, outputText)).ConfigureAwait(true);
                    string resultText = $"{DateTime.Now.ToUniversalTime():yyyy-MM-dd HH:mm}: uploaded {count} records";
                    await Analytics.TrackActionAsync(Analytics.LostCousinsAction, Analytics.UpdateLostCousins, resultText).ConfigureAwait(true);
                    SpecialMethods.VisitWebsite("https://www.lostcousins.com/pages/members/ancestors/");
                    UpdateLCReports();
                }
            }
            else
                UIHelpers.ShowMessage("You have no records to add to Lost Cousins at this time. Use the Research Suggestions to find more people on the census, or enter/update missing or incomplete census references.");
            btnUpdateLostCousinsWebsite.Enabled = true;
        }

        void UpdateLCReports()
        {
            HourGlass(this, true);
            UpdateLostCousinsReport();
            UpdateLCOutput();
            HourGlass(this, false);
        }

        void UpdateLCOutput()
        {
            rtbLCUpdateData.ForeColor = Color.Black;
            Predicate<CensusIndividual> relationFilter = relTypesLC.BuildFilter<CensusIndividual>(x => x.RelationType, true);
            LCUpdates = [];
            LCInvalidReferences = [];
            rtbLCUpdateData.Text = ft.LCOutput(LCUpdates, LCInvalidReferences, relationFilter);
        }

        void BtnCheckMyAncestors_Click(object sender, EventArgs e)
        {
            if (btnCheckMyAncestors.BackColor == Color.LightGreen)
            {
                Progress<string> outputText = new(rtbCheckAncestors.AppendText);
                dgCheckAncestors.DataSource = ExportToLostCousins.VerifyAncestorsAsync(outputText);
                dgCheckAncestors.Refresh();

            }
        }

        void BtnLCMissingCountry_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> relationFilter = relTypesLC.BuildFilter<Individual>(x => x.RelationType);
            People people = new();
            people.SetupLCNoCountry(relationFilter);
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.LostCousinsAction, Analytics.NoLCCountryEvent);
            HourGlass(this, false);
        }

        void RelTypesLC_RelationTypesChanged(object sender, EventArgs e) => UpdateLCReports();

        void TxtLCEmail_TextChanged(object sender, EventArgs e) => ClearLogin();

        void TxtLCPassword_TextChanged(object sender, EventArgs e) => ClearLogin();

        void ClearLogin()
        {
            if (btnUpdateLostCousinsWebsite.Visible) // if we can login clear cookies to reset session
                Program.LCClient.EmptyCookieJar();
            btnLCLogin.BackColor = Color.Red;
            btnLCLogin.Enabled = true;
            btnUpdateLostCousinsWebsite.Visible = false;
        }

        void UpdateLostCousinsReport() => rtbLostCousins.Text = ft.UpdateLostCousinsReport(relTypesLC.BuildFilter<Individual>(x => x.RelationType));

        void BtnLCDuplicates_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> relationFilter = relTypesLC.BuildFilter<Individual>(x => x.RelationType);
            People people = new();
            people.SetupLCDuplicates(relationFilter);
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.LostCousinsAction, Analytics.LCDuplicatesEvent);
            HourGlass(this, false);
        }

        void BtnLCnoCensus_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> relationFilter = relTypesLC.BuildFilter<Individual>(x => x.RelationType);
            People people = new();
            people.SetupLCnoCensus(relationFilter);
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.LostCousinsAction, Analytics.NoLCCensusEvent);
            HourGlass(this, false);
        }

        void ChkLCRootPersonConfirm_CheckedChanged(object sender, EventArgs e)
        {
            btnUpdateLostCousinsWebsite.Enabled = chkLCRootPersonConfirm.Checked;
            btnUpdateLostCousinsWebsite.BackColor = chkLCRootPersonConfirm.Checked ? Color.LightGreen : Color.LightGray;
        }

        void BtnLC1881EW_Click(object sender, EventArgs e) => LostCousinsCensus(CensusDate.EWCENSUS1881, "1881 England & Wales Census Records on file");

        void BtnLC1881Scot_Click(object sender, EventArgs e) => LostCousinsCensus(CensusDate.SCOTCENSUS1881, "1881 Scotland Census Records on file");

        void BtnLC1881Canada_Click(object sender, EventArgs e) => LostCousinsCensus(CensusDate.CANADACENSUS1881, "1881 Canada Census Records on file");

        void BtnLC1841EW_Click(object sender, EventArgs e) => LostCousinsCensus(CensusDate.EWCENSUS1841, "1841 England & Wales Census Records on file");

        void BtnLC1911EW_Click(object sender, EventArgs e) => LostCousinsCensus(CensusDate.EWCENSUS1911, "1911 England & Wales Census Records on file");

        void BtnLC1880USA_Click(object sender, EventArgs e) => LostCousinsCensus(CensusDate.USCENSUS1880, "1880 US Census Records on file");

        void BtnLC1911Ireland_Click(object sender, EventArgs e) => LostCousinsCensus(CensusDate.IRELANDCENSUS1911, "1911 Ireland Census Records on file");

        void BtnLC1940USA_Click(object sender, EventArgs e) => LostCousinsCensus(CensusDate.USCENSUS1940, "1940 US Census Records on file");

        void LabLostCousinsWeb_Click(object sender, EventArgs e)
        {
            SpecialMethods.VisitWebsite("https://www.lostcousins.com/?ref=LC585149");
            Analytics.TrackAction(Analytics.LostCousinsAction, Analytics.LCWebLinkEvent);
        }

        void LabLostCousinsWeb_MouseEnter(object sender, EventArgs e)
        {
            storedCursor = Cursor;
            Cursor = Cursors.Hand;
        }

        void LabLostCousinsWeb_MouseLeave(object sender, EventArgs e) => Cursor = storedCursor;
        #endregion

        #region ToolStrip Clicks
        void AboutToolStripMenuItem_Click(object sender, EventArgs e) => UIHelpers.ShowMessage($"This is Family Tree Analyzer version {VERSION}", "FTAnalyzer");

        void OptionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Options options = new();
                options.ShowDialog(this);
                options.Dispose();
                Analytics.TrackAction(Analytics.MainFormAction, Analytics.OptionsEvent);
            }
            catch (Exception) { }
        }

        #endregion

        #region Print Routines
        void MnuPrint_Click(object sender, EventArgs e)
        {
            try
            {
                printDocument = new PrintDocument();
                printDocument.DefaultPageSettings.Margins = new Margins(50, 50, 50, 25);
                printDocument.DefaultPageSettings.Landscape = true;
                printDialog.PrinterSettings.DefaultPageSettings.Margins = new Margins(50, 50, 50, 25);
                printDialog.PrinterSettings.DefaultPageSettings.Landscape = true;

                if (tabSelector.SelectedTab == tabDisplayProgress && ft.DataLoaded)
                {
                    if (printDialog.ShowDialog(this) == DialogResult.OK)
                    {
                        using Utilities.Printing p = new(rtbOutput);
                        printDocument.PrintPage += new PrintPageEventHandler(p.PrintPage);
                        printDocument.PrinterSettings = printDialog.PrinterSettings;
                        printDocument.DocumentName = "GEDCOM Load Results";
                        printDocument.Print();
                    }
                }
                if (tabSelector.SelectedTab == tabMainLists)
                {
                    if (tabMainListsSelector.SelectedTab == tabIndividuals)
                        PrintDataGrid(Orientation.Landscape, dgIndividuals, "List of Individuals");
                    else if (tabMainListsSelector.SelectedTab == tabFamilies)
                        PrintDataGrid(Orientation.Landscape, dgFamilies, "List of Families");
                    else if (tabMainListsSelector.SelectedTab == tabSources)
                        PrintDataGrid(Orientation.Landscape, dgSources, "List of Sources");
                    else if (tabMainListsSelector.SelectedTab == tabOccupations)
                        PrintDataGrid(Orientation.Portrait, dgOccupations, "List of Occupations");
                }
                else if (tabSelector.SelectedTab == tabErrorsFixes)
                {
                    if (tabErrorFixSelector.SelectedTab == tabDuplicates)
                        PrintDataGrid(Orientation.Landscape, dgDuplicates, "ist of Potential Duplicates");
                    else if (tabErrorFixSelector.SelectedTab == tabDataErrors)
                        PrintDataGrid(Orientation.Landscape, dgDataErrors, "List of Data Errors");
                    else if (tabErrorFixSelector.SelectedTab == tabLooseBirths)
                        PrintDataGrid(Orientation.Landscape, dgLooseBirths, "List of Loose Births");
                    else if (tabErrorFixSelector.SelectedTab == tabLooseDeaths)
                        PrintDataGrid(Orientation.Landscape, dgLooseDeaths, "List of Loose Deaths");
                    else if (tabErrorFixSelector.SelectedTab == tabLooseInfo)
                        PrintDataGrid(Orientation.Landscape, dgLooseInfo, "List of Loose Births/Deaths");
                }
                else if (tabSelector.SelectedTab == tabLocations)
                {
                    if (tabCtrlLocations.SelectedTab == tabCountries)
                        PrintDataGrid(Orientation.Portrait, dgCountries, "List of Countries");
                    if (tabCtrlLocations.SelectedTab == tabRegions)
                        PrintDataGrid(Orientation.Portrait, dgRegions, "List of Regions");
                    if (tabCtrlLocations.SelectedTab == tabSubRegions)
                        PrintDataGrid(Orientation.Portrait, dgSubRegions, "List of Sub Regions");
                    if (tabCtrlLocations.SelectedTab == tabAddresses)
                        PrintDataGrid(Orientation.Portrait, dgAddresses, "List of Addresses");
                    if (tabCtrlLocations.SelectedTab == tabPlaces)
                        PrintDataGrid(Orientation.Portrait, dgPlaces, "List of Places");
                }
                else if (tabSelector.SelectedTab == tabTreetops)
                {
                    PrintDataGrid(Orientation.Landscape, dgTreeTops, "List of People at Top of Tree");
                }
                else if (tabSelector.SelectedTab == tabWorldWars)
                {
                    PrintDataGrid(Orientation.Landscape, dgWorldWars, "List of Individuals who may have served in the World Wars");
                }
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage($"Error Printing : {ex.Message}");
            }
        }

        enum Orientation { Landscape, Portrait }

        void PrintDataGrid(Orientation orientation, DataGridView dg, string title)
        {
            PrintingDataGridViewProvider.Create(printDocument, dg, true, true, true, new TitlePrintBlock(title), null, null);
            printDialog.PrinterSettings.DefaultPageSettings.Landscape = (orientation == Orientation.Landscape);
            printDialog.PrinterSettings.DefaultPageSettings.Margins.Left = 50;
            printDialog.PrinterSettings.DefaultPageSettings.Margins.Right = 50;
            printDialog.PrinterSettings.DefaultPageSettings.Margins.Top = 50;
            printDialog.PrinterSettings.DefaultPageSettings.Margins.Bottom = 50;
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDocument.DocumentName = title;
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.Print();
            }
        }
        #endregion

        #region Dispose Routines
        void DisposeIndividualForms()
        {
            try
            {
                List<Form> toDispose = [];
                foreach (Form f in Application.OpenForms)
                {
                    if (!ReferenceEquals(f, this))
                        toDispose.Add(f);
                }
                foreach (Form f in toDispose)
                    f.Dispose();
            }
            catch (Exception) { }
        }

        public static void DisposeDuplicateForms(object form)
        {
            try
            {
                List<Form> toDispose = [];
                foreach (Form f in Application.OpenForms)
                {
                    if (!ReferenceEquals(f, form) && f.GetType() == form.GetType())
                        if (form is Census newCensusForm)
                        {
                            Census oldForm = (Census)f;
                            if (oldForm.CensusDate.Equals(newCensusForm.CensusDate) && oldForm.LostCousins == newCensusForm.LostCousins)
                                toDispose.Add(f);
                        }
                        else if (form is Facts newFactsForm)
                        {
                            Facts oldForm = (Facts)f;
                            if (oldForm.Individual is not null && oldForm.Individual.Equals(newFactsForm.Individual))
                                toDispose.Add(f);
                            if (oldForm.Family is not null && oldForm.Family.Equals(newFactsForm.Family))
                                toDispose.Add(f);
                        }
                        else
                            toDispose.Add(f);
                }
                foreach (Form f in toDispose)
                {
                    GC.SuppressFinalize(f);
                    if (f.Visible)
                        f.Close(); // call close method to force tidy up of forms & dispose
                    else
                        f.Dispose();
                }
            }
            catch (Exception) { }
        }
        #endregion

        #region Backup/Restore Database
        void BackupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ft.Geocoding)
                UIHelpers.ShowMessage("You need to stop Geocoding before you can export the database", "FTAnalyzer");
            else
            {
                DatabaseHelper.Instance.BackupDatabase(saveDatabase, $"FTAnalyzer zip file created by v{VERSION}");
                Analytics.TrackAction(Analytics.MainFormAction, Analytics.DBBackupEvent);
            }
        }

        void RestoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ft.Geocoding)
                UIHelpers.ShowMessage("You need to stop Geocoding before you can import the database", "FTAnalyzer");
            else
            {
                string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directory = Application.UserAppDataRegistry.GetValue("Geocode Backup Directory", myDocuments).ToString() ?? myDocuments;
                restoreDatabase.FileName = "*.zip";
                restoreDatabase.InitialDirectory = directory;
                DialogResult result = restoreDatabase.ShowDialog();
                if (result == DialogResult.OK && File.Exists(restoreDatabase.FileName))
                {
                    HourGlass(this, true);
                    bool failed = false;
                    using (ZipFile zip = new(restoreDatabase.FileName))
                    {
                        if (zip.Count == 1 && zip.FindEntry("Geocodes.s3db", true) > 0)
                        {
                            DatabaseHelper dbh = DatabaseHelper.Instance;
                            if (DatabaseHelper.StartBackupRestoreDatabase())
                            {
                                File.Copy(dbh.DatabaseFile, dbh.CurrentFilename, true); // copy exisiting file to safety
                                ZipHelper.ExtractZipFile(restoreDatabase.FileName, null, dbh.DatabasePath);
                                if (dbh.RestoreDatabase(new Progress<string>(rtbOutput.AppendText)))
                                    if (dbh.RestoreDatabase(new Progress<string>(rtbOutput.AppendText)))
                                        UIHelpers.ShowMessage("Database restored from " + restoreDatabase.FileName, "FTAnalyzer Database Restore Complete");
                                    else
                                    {
                                        File.Copy(dbh.CurrentFilename, dbh.DatabaseFile, true);
                                        dbh.RestoreDatabase(new Progress<string>(value => { rtbOutput.AppendText(value); })); // restore original database
                                        failed = true;
                                    }
                            }
                            else
                                UIHelpers.ShowMessage("Database file could not be extracted", "FTAnalyzer Database Restore Error");
                        }
                        else
                        {
                            failed = true;
                        }
                        if (failed)
                            UIHelpers.ShowMessage($"{restoreDatabase.FileName} doesn't appear to be an FTAnalyzer database", "FTAnalyzer Database Restore Error");
                        else
                            Analytics.TrackAction(Analytics.MainFormAction, Analytics.DBRestoreEvent);
                    }
                    HourGlass(this, false);
                }
            }
        }
        #endregion

        #region Recent File List
        void ClearRecentFileListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearRecentList();
            BuildRecentList();
        }

        static void ClearRecentList()
        {
            Settings.Default.RecentFiles.Clear();
            for (int i = 0; i < 5; i++)
            {
                Settings.Default.RecentFiles.Add(string.Empty);
            }
            Settings.Default.Save();
        }

        void BuildRecentList()
        {
            if (Settings.Default.RecentFiles is null || Settings.Default.RecentFiles.Count != 5)
                ClearRecentList();
            bool added = false;
            int count = 0;
            for (int i = 0; i < 5; i++)
            {
                string? name = Settings.Default.RecentFiles[i];
                if (name is not null && name.Length > 0 && File.Exists(name))
                {
                    added = true;
                    mnuRecent.DropDownItems[i].Visible = true;
                    mnuRecent.DropDownItems[i].Text = ++count + ". " + name;
                    mnuRecent.DropDownItems[i].Tag = name;
                }
                else
                    mnuRecent.DropDownItems[i].Visible = false;
            }
            toolStripSeparator7.Visible = added;
            clearRecentFileListToolStripMenuItem.Visible = added;
            mnuRecent.Enabled = added;
        }

        void AddFileToRecentList(string filename)
        {
            string[] recent = new string[5];

            if (Settings.Default.RecentFiles is not null)
            {
                int j = 1;
                for (int i = 0; i < Settings.Default.RecentFiles.Count; i++)
                {
                    if (Settings.Default.RecentFiles[i] != filename && File.Exists(Settings.Default.RecentFiles[i]))
                    {
                        recent[j++] = Settings.Default.RecentFiles[i];
                        if (j == 5) break;
                    }
                }
            }

            recent[0] = filename;
            Settings.Default.RecentFiles = [.. recent];
            Settings.Default.Save();

            BuildRecentList();
        }

        async void OpenRecentFile_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem? item = sender as ToolStripMenuItem;
            if (item?.Tag is not null)
            {
                string filename = (string)item.Tag ?? string.Empty;
                await LoadFileAsync(filename).ConfigureAwait(true);
            }
        }

        void MnuRecent_DropDownOpening(object sender, EventArgs e) => BuildRecentList();
        #endregion

        void DgFamilies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string famID = (string)dgFamilies.CurrentRow.Cells[nameof(IDisplayFamily.FamilyID)].Value;
                ShowFamilyFacts(famID);
            }
        }

        void DgDataErrors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowIndividualsFacts((string)dgDataErrors.CurrentRow.Cells[nameof(IDisplayDataError.Reference)].Value);
        }

        void DgLooseDeaths_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowIndividualsFacts((string)dgLooseDeaths.CurrentRow.Cells[nameof(IDisplayLooseDeath.IndividualID)].Value);
        }

        void DgLooseBirths_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowIndividualsFacts((string)dgLooseBirths.CurrentRow.Cells[nameof(IDisplayLooseBirth.IndividualID)].Value);
        }

        void DgLooseInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowIndividualsFacts((string)dgLooseInfo.CurrentRow.Cells[nameof(IDisplayLooseInfo.IndividualID)].Value);
        }

        void DgTreeTops_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowIndividualsFacts((string)dgTreeTops.CurrentRow.Cells[nameof(IDisplayIndividual.IndividualID)].Value);
        }

        void DgWorldWars_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string indID = (string)dgWorldWars.CurrentRow.Cells[nameof(IDisplayIndividual.IndividualID)].Value;
                if (WWI && ModifierKeys.Equals(Keys.Shift))
                    LivesOfFirstWorldWar(indID);
                else
                    ShowIndividualsFacts(indID);
            }
        }

        void LivesOfFirstWorldWar(string indID)
        {
            Individual? ind = ft.GetIndividual(indID);
            if (ind is null) return;
            string searchtext = ind.Forename + "+" + ind.Surname;
            if (ind.ServiceNumber.Length > 0)
                searchtext += "+" + ind.ServiceNumber;
            SpecialMethods.VisitWebsite("https://www.livesofthefirstworldwar.org/search#FreeSearch=" + searchtext + "&PageIndex=1&PageSize=20");
        }

        void DgIndividuals_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = dgIndividuals.HitTest(e.Location.X, e.Location.Y);
            if (e.Button == MouseButtons.Right)
            {
                var ht = dgIndividuals.HitTest(e.X, e.Y);
                if (ht.Type != DataGridViewHitTestType.ColumnHeader)
                {
                    if (hti.RowIndex >= 0 && hti.ColumnIndex >= 0)
                    {
                        dgIndividuals.CurrentCell = dgIndividuals.Rows[hti.RowIndex].Cells[hti.ColumnIndex];
                        // Can leave these here - doesn't hurt
                        dgIndividuals.Rows[hti.RowIndex].Selected = true;
                        dgIndividuals.Focus();
                        mnuSetRoot.Show(MousePosition);
                    }
                }
            }
            if (hti.RowIndex >= 0 && hti.ColumnIndex >= 0)
            {
                if (dgIndividuals.Rows[hti.RowIndex].Cells[hti.ColumnIndex].GetType() == typeof(DataGridViewLinkCell))
                {
                    string familySearchID = dgIndividuals.Rows[hti.RowIndex].Cells[hti.ColumnIndex].Value.ToString() ?? string.Empty;
                    if (!string.IsNullOrEmpty(familySearchID))
                    {
                        string url = $"https://www.familysearch.org/tree/person/details/{familySearchID}";
                        SpecialMethods.VisitWebsite(url);
                    }
                }
                else if (e.Clicks == 2)
                {
                    string indID = (string)dgIndividuals.CurrentRow.Cells[nameof(IDisplayIndividual.IndividualID)].Value;
                    ShowIndividualsFacts(indID);
                }
            }
        }

        void DgIndividuals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string indID = (string)dgIndividuals.CurrentRow.Cells[nameof(IDisplayIndividual.IndividualID)].Value;
                ShowIndividualsFacts(indID);
            }
        }

        void DgSources_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                FactSource source = (FactSource)dgSources.CurrentRowDataBoundItem;
                Facts factForm = new(source);
                DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        void DgDuplicates_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (pbDuplicates.Visible || e.RowIndex < 0 || e.ColumnIndex < 0)
                return; // do nothing if progress bar still visible
            string indA_ID = (string)dgDuplicates.CurrentRow.Cells[nameof(IDisplayDuplicateIndividual.IndividualID)].Value;
            string indB_ID = (string)dgDuplicates.CurrentRow.Cells[nameof(IDisplayDuplicateIndividual.MatchIndividualID)].Value;
            if (GeneralSettings.Default.MultipleFactForms)
            {
                ShowIndividualsFacts(indA_ID);
                ShowIndividualsFacts(indB_ID, true);
            }
            else
            {
                Individual? a = ft.GetIndividual(indA_ID);
                Individual? b = ft.GetIndividual(indB_ID);
                if (a is null)
                    UIHelpers.ShowMessage($"Couldn't find details for Individual with ID: {indA_ID}");
                else if (b is null)
                    UIHelpers.ShowMessage($"Couldn't find details for Individual with ID: {indB_ID}");
                else
                {
                    List<Individual> dupInd = [a, b];
                    Facts f = new(dupInd, null, null, Facts.AlternateFacts.AllFacts);
                    DisposeDuplicateForms(f);
                    f.Show();
                }
            }
        }

        #region Facts Tab

        void SetupFactsCheckboxes()
        {
            Predicate<ExportFact> filter = CreateFactsFilter();
            SetFactTypeList(ckbFactSelect, ckbFactExclude, filter);
            SetShowFactsButton();
        }

        void RelTypesFacts_RelationTypesChanged(object sender, EventArgs e) => SetupFactsCheckboxes();

        void TxtFactsSurname_TextChanged(object sender, EventArgs e) => SetupFactsCheckboxes();

        public static void ShowIndividualsFacts(string indID, bool offset = false)
        {
            Individual? ind = FamilyTree.Instance.GetIndividual(indID);
            if (ind is not null)
            {
                Facts factForm = new(ind);
                DisposeDuplicateForms(factForm);
                factForm.Show();
                if (offset)
                {
                    factForm.Left += 200;
                    factForm.Top += 100;
                }
            }
        }

        public static void ShowFamilyFacts(string famID, bool offset = false)
        {
            Family? fam = FamilyTree.Instance.GetFamily(famID);
            if (fam is not null)
            {
                Facts factForm = new(fam);
                DisposeDuplicateForms(factForm);
                factForm.Show();
                if (offset)
                {
                    factForm.Left += 200;
                    factForm.Top += 100;
                }
            }
        }

        void BtnShowFacts_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> filter = relTypesFacts.BuildFilter<Individual>(x => x.RelationType);
            if (txtFactsSurname.Text.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, txtFactsSurname.Text);
                filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
            }
            Facts facts;
            if (radioOnlyPreferred.Checked)
                facts = new Facts(ft.AllIndividuals.Filter(filter), BuildFactTypeList(ckbFactSelect, true), BuildFactTypeList(ckbFactExclude, true), Facts.AlternateFacts.PreferredOnly);
            else if (radioOnlyAlternate.Checked)
                facts = new Facts(ft.AllIndividuals.Filter(filter), BuildFactTypeList(ckbFactSelect, true), BuildFactTypeList(ckbFactExclude, true), Facts.AlternateFacts.AlternateOnly);
            else
                facts = new Facts(ft.AllIndividuals.Filter(filter), BuildFactTypeList(ckbFactSelect, true), BuildFactTypeList(ckbFactExclude, true), Facts.AlternateFacts.AllFacts);
            facts.Show();
            HourGlass(this, false);
        }

        List<string> BuildFactTypeList(CheckedListBox list, bool includeCreated)
        {
            List<string> result = [];
            if (list == ckbFactExclude && ckbFactExclude.Visible == false)
                return result; // if we aren't looking to exclude facts don't pass anything to list of exclusions
            int index = 0;
            foreach (string factType in list.Items)
            {
                if (list.GetItemChecked(index++))
                {
                    if (includeCreated)
                        result.Add(factType);
                    else
                        if (factType != Fact.GetFactTypeDescription(Fact.PARENT) && factType != Fact.GetFactTypeDescription(Fact.CHILDREN))
                        result.Add(factType);
                }
            }
            return result;
        }

        void BtnSelectAllFactTypes_Click(object sender, EventArgs e) => SetFactTypes(ckbFactSelect, true, "Fact: ");

        void BtnDeselectAllFactTypes_Click(object sender, EventArgs e) => SetFactTypes(ckbFactSelect, false, "Fact: ");


        void SetFactTypes(CheckedListBox list, bool selected, string registryPrefix)
        {
            for (int index = 0; index < list.Items.Count; index++)
            {
                string factType = list.Items[index].ToString() ?? string.Empty;
                list.SetItemChecked(index, selected);
                try
                {
                    Application.UserAppDataRegistry.SetValue(registryPrefix + factType, selected);
                }
                catch (IOException)
                {
                    UIHelpers.ShowMessage("Unable to save fact selection preferences. Please check App has permission to save user preferences to registry.");
                }
            }
            SetShowFactsButton();
        }


        void CkbFactSelect_MouseClick(object sender, MouseEventArgs e)
        {
            int index = ckbFactSelect.IndexFromPoint(e.Location);
            if (index >= 0)
            {
                string factType = ckbFactSelect.Items[index].ToString() ?? string.Empty;
                bool selected = ckbFactSelect.GetItemChecked(index);
                ckbFactSelect.SetItemChecked(index, !selected);
                try
                {
                    Application.UserAppDataRegistry.SetValue($"Fact: {factType}", !selected);
                }
                catch (IOException)
                {
                    UIHelpers.ShowMessage("Unable to save fact selection preferences. Please check App has permission to save user preferences to registry.");
                }
                SetShowFactsButton();
            }
        }

        void SetShowFactsButton()
        {
            string alternate = string.Empty;
            if (radioOnlyAlternate.Checked)
                alternate = "Alternate ";
            else if (radioOnlyPreferred.Checked)
                alternate = "Preferred ";
            if (ckbFactSelect.CheckedItems.Count == 0 && ckbFactExclude.CheckedItems.Count > 0)
                btnShowFacts.Text = $"Show all {alternate}Facts for Individuals who are missing the selected excluded Fact Types";
            else
                btnShowFacts.Text = $"Show only the selected {alternate}Facts for Individuals" + (ckbFactExclude.Visible ? " who don't have any of the excluded Fact Types" : string.Empty);
            btnShowFacts.Enabled = ckbFactSelect.CheckedItems.Count > 0 || (ckbFactExclude.Visible && ckbFactExclude.CheckedItems.Count > 0);
        }

        void BtnExcludeAllFactTypes_Click(object sender, EventArgs e) => SetFactTypes(ckbFactExclude, true, "Exclude Fact: ");

        void BtnDeselectExcludeAllFactTypes_Click(object sender, EventArgs e) => SetFactTypes(ckbFactExclude, false, "Exclude Fact: ");

        void BtnShowExclusions_Click(object sender, EventArgs e)
        {
            bool visible = !ckbFactExclude.Visible;
            ckbFactExclude.Visible = visible;
            btnExcludeAllFactTypes.Visible = visible;
            btnDeselectExcludeAllFactTypes.Visible = visible;
            lblExclude.Visible = visible;
            SetShowFactsButton();
        }


        void CkbFactExclude_MouseClick(object sender, MouseEventArgs e)
        {
            int index = ckbFactExclude.IndexFromPoint(e.Location);
            string factType = ckbFactExclude.Items[index].ToString() ?? string.Empty;
            bool selected = ckbFactExclude.GetItemChecked(index);
            ckbFactExclude.SetItemChecked(index, !selected);
            try
            {
                Application.UserAppDataRegistry.SetValue($"Exclude Fact: {factType}", !selected);
            }
            catch (IOException)
            {
                UIHelpers.ShowMessage("Unable to save fact exclusion preferences. Please check App has permission to save user preferences to registry.");
            }
            SetShowFactsButton();
        }

        void BtnDuplicateFacts_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> filter = relTypesFacts.BuildFilter<Individual>(x => x.RelationType);
            if (txtFactsSurname.Text.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, txtFactsSurname.Text);
                filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
            }
            Facts facts = new(ft.AllIndividuals.Filter(filter), BuildFactTypeList(ckbFactSelect, false));
            facts.Show();
            HourGlass(this, false);
        }
        #endregion

        #region Form Drag Drop

        async void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            bool fileLoaded = false;
            string[]? files = e.Data.GetData(DataFormats.FileDrop) as string[];
            if (files is not null)
            {
                foreach (string filename in files)
                {
                    if (Path.GetExtension(filename.ToLower()) == ".ged")
                    {
                        fileLoaded = true;
                        await LoadFileAsync(filename).ConfigureAwait(true);
                        break;
                    }
                }
                if (!fileLoaded)
                    if (files.Length > 1)
                        UIHelpers.ShowMessage("Unable to load File. None of the files dragged and dropped were *.ged files", "FTAnalyzer");
                    else
                        UIHelpers.ShowMessage("Unable to load File. The file dragged and dropped wasn't a *.ged file", "FTAnalyzer");
            }
        }

        void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        #endregion

        #region Manage Form Position

        void ResetToDefaultFormSizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadDefaultPosition();
            SavePosition();
        }

        void LoadDefaultPosition()
        {
            loading = true;
            Height = 561;
            Width = 1114;
            Top = 50 + NativeMethods.TopTaskbarOffset;
            Left = 50;
            loading = false;
        }


        void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                rtbToday.Top = dpToday.Top + 30;
                splitGedcom.Height = 100;
                SavePosition();
            }
            catch (Exception) { }
        }

        void MainForm_Move(object sender, EventArgs e) => SavePosition();


        void SavePosition()
        {
            if (!loading && WindowState != FormWindowState.Minimized)
            {  //only save window size if not minimised
                try
                {
                    CheckMaxWindowSizes(new Point(0, 0));
                    Application.UserAppDataRegistry.SetValue("Mainform size - width", Width);
                    Application.UserAppDataRegistry.SetValue("Mainform size - height", Height);
                    Application.UserAppDataRegistry.SetValue("Mainform position - top", Top);
                    Application.UserAppDataRegistry.SetValue("Mainform position - left", Left);
                    string maxState = (WindowState == FormWindowState.Maximized).ToString();
                    Application.UserAppDataRegistry.SetValue("Mainform maximised", maxState);
                }
                catch (IOException)
                {
                    UIHelpers.ShowMessage("Unable to save window permissions please check App has rights to save user preferences to registry");
                }
            }
        }

        void CheckMaxWindowSizes(Point topleft)
        {
            int boundaryWidth = rtbOutput.Margin.Left + tabSelector.Margin.Left + tabSelector.Margin.Right;
            int boundaryHeight = panel2.Height - statusStrip.Height - menuStrip1.Height - tabSelector.Location.Y + tabSelector.Margin.Top + tabSelector.Margin.Bottom;
            Rectangle workarea = Screen.GetWorkingArea(topleft);
            log.Debug($"CheckMaxWindowSizes: boundaryWidth:{boundaryWidth}, boundaryHeight: {boundaryHeight}");
            log.Debug($"tabselector- X: {tabSelector.Location.X} Y: {tabSelector.Location.Y}");
            if (Width > workarea.Width)
                Width = workarea.Width;
            if (Height > workarea.Height)
                Height = workarea.Height;
            if (tabSelector.Left + tabSelector.Width + boundaryWidth > Size.Width)
                tabSelector.Width = Size.Width - tabSelector.Left - boundaryWidth;
            if (tabSelector.Top + tabSelector.Height + boundaryHeight > Size.Height)
                tabSelector.Height = Size.Height - tabSelector.Top - boundaryHeight;
        }
        #endregion

        #region Duplicates Tab
        CancellationTokenSource cts;
        SortableBindingList<IDisplayDuplicateIndividual> duplicateData;

        async Task SetPossibleDuplicates()
        {
            SetDuplicateControlsVisibility(true);
            rfhDuplicates.SaveColumnLayout("DuplicatesColumns.xml");
            var progress = new Progress<int>(value =>
            {
                if (value < 0)
                    value = 0;
                if (value > pbDuplicates.Maximum)
                    value = pbDuplicates.Maximum;
                pbDuplicates.Value = value;
            });
            var progressText = new Progress<string>(value => labCompletion.Text = value);
            var maxScore = new Progress<int>(value =>
            {
                tbDuplicateScore.TickFrequency = value / 20;
                tbDuplicateScore.SetRange(1, value);
            });
            cts = new CancellationTokenSource();
            int score = tbDuplicateScore.Value;
            labDuplicateSlider.Text = $"Match Quality : {tbDuplicateScore.Value}  ";
            bool ignoreUnknownTwins = chkIgnoreUnnamedTwins.Checked;
            tsCountLabel.Text = "Calculating Duplicates this may take some considerable time";
            tsHintsLabel.Text = string.Empty;
            duplicateData = await Task.Run(() => ft.GenerateDuplicatesList(score, ignoreUnknownTwins, progress, progressText, maxScore, cts.Token)).ConfigureAwait(true);
            cts = null;
            if (duplicateData is not null)
            {
                dgDuplicates.DataSource = duplicateData;
                rfhDuplicates.LoadColumnLayout("DuplicatesColumns.xml");
                tsCountLabel.Text = $"Possible Duplicate Count : {dgDuplicates.RowCount:N0}.  {Messages.Hints_Duplicates}";
                dgDuplicates.VirtualGridFiltered += VirtualGridFiltered;
                dgDuplicates.UseWaitCursor = false;
            }
            SetDuplicateControlsVisibility(false);
            HourGlass(this, false);
        }

        void SetDuplicateControlsVisibility(bool visible)
        {
            btnCancelDuplicates.Visible = visible;
            labCalcDuplicates.Visible = visible;
            pbDuplicates.Visible = visible;
            labCompletion.Visible = visible;
            mnuDuplicatesToExcel.Enabled = !visible; // hide whilst setting up make visible when done
        }

        void ResetDuplicatesTable()
        {
            if (dgDuplicates.RowCount > 0)
            {
                dgDuplicates.Sort(dgDuplicates.Columns[nameof(IDisplayDuplicateIndividual.Forenames)], ListSortDirection.Ascending);
                dgDuplicates.Sort(dgDuplicates.Columns[nameof(IDisplayDuplicateIndividual.Surname)], ListSortDirection.Ascending);
                dgDuplicates.Sort(dgDuplicates.Columns[nameof(IDisplayDuplicateIndividual.Score)], ListSortDirection.Descending);
            }
        }

        async void TbDuplicateScore_Scroll(object sender, EventArgs e)
        {
            // do nothing if progress bar still visible
            if (!pbDuplicates.Visible)
                await SetPossibleDuplicates().ConfigureAwait(true);
        }

        void BtnCancelDuplicates_Click(object sender, EventArgs e)
        {
            if (cts is not null)
            {
                cts.Cancel();
                UIHelpers.ShowMessage("Possible Duplicate Search Cancelled", "FTAnalyzer");
            }
        }

        void DgDuplicates_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0 && !pbDuplicates.Visible) // don't do anything if progressbar still loading duplicates
            {
                DisplayDuplicateIndividual? dupInd = dgDuplicates.DataBoundItem(e.RowIndex) as DisplayDuplicateIndividual;
                if (dupInd is not null)
                {
                    NonDuplicate nonDup = new(dupInd);
                    dupInd.IgnoreNonDuplicate = !dupInd.IgnoreNonDuplicate; // flip state of checkbox
                    if (dupInd.IgnoreNonDuplicate)
                    {  //ignoring this record so add it to the list if its not already present
                        if (!ft.NonDuplicates.ContainsDuplicate(nonDup))
                            ft.NonDuplicates.Add(nonDup);
                    }
                    else
                        ft.NonDuplicates.Remove(nonDup); // no longer ignoring so remove from list
                    ft.SerializeNonDuplicates();
                }
            }
        }

        async void CkbHideIgnoredDuplicates_CheckedChanged(object sender, EventArgs e)
        {
            if (pbDuplicates.Visible)
                return; // do nothing if progress bar still visible
            GeneralSettings.Default.HideIgnoredDuplicates = ckbHideIgnoredDuplicates.Checked;
            GeneralSettings.Default.Save();
            await SetPossibleDuplicates().ConfigureAwait(true);
        }
        #endregion

        #region Census Tab
        void BtnShowCensus_Click(object sender, EventArgs e)
        {
            bool censusDone = sender == btnShowCensusEntered;
            ShowCensus(cenDate.SelectedDate, censusDone, txtCensusSurname.Text, false);
            Analytics.TrackAction(Analytics.CensusTabAction, censusDone ? Analytics.ShowCensusEvent : Analytics.MissingCensusEvent);
        }

        void ShowCensus(CensusDate censusDate, bool censusDone, string surname, bool random)
        {
            Predicate<CensusIndividual> filter;
            Census census = new(censusDate, censusDone);
            if (random)
                census.Text = $"People with surname {surname}";
            else
                census.Text = "People";
            if (censusDone)
                census.Text += $" entered with a {censusDate} record";
            else
                census.Text += $" missing a {censusDate} record that you can search for";

            if (random)
            {
                int tries = 0;
                while (random && census.RecordCount == 0 && tries < 5)
                {
                    surname = GetRandomSurname();
                    filter = CreateCensusIndividualFilter(censusDate, censusDone, surname);
                    census.SetupCensus(filter);
                    tries++;
                }
            }
            else
            {
                filter = CreateCensusIndividualFilter(censusDate, censusDone, surname);
                census.SetupCensus(filter);
            }
            DisposeDuplicateForms(census);
            census.Show();
        }

        void BtnRandomSurname_Click(object sender, EventArgs e)
        {
            string surname = GetRandomSurname();
            bool censusDone = sender == btnRandomSurnameEntered;
            ShowCensus(cenDate.SelectedDate, censusDone, surname, true);
        }

        string GetRandomSurname()
        {
            IEnumerable<Individual> directs = ft.AllIndividuals.Filter(x => x.RelationType == Individual.DIRECT || x.RelationType == Individual.DESCENDANT);
            List<string> surnames = [.. directs.Select(x => x.Surname).Distinct()];
            Random rnd = new();
            string surname;
            do
            {
                int selection = rnd.Next(surnames.Count);
                surname = surnames[selection];
            } while (surname == "UNKNOWN" && surnames.Count > 10);
            return surname;
        }

        void BtnMissingCensusLocation_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            People people = new();
            people.SetupMissingCensusLocation();
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.CensusTabAction, Analytics.MissingCensusLocationEvent);
            HourGlass(this, false);
        }

        void BtnDuplicateCensus_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            People people = new();
            people.SetupDuplicateCensus();
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.CensusTabAction, Analytics.DuplicateCensusEvent);
            HourGlass(this, false);
        }

        void BtnNoChildrenStatus_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            People people = new();
            people.SetupNoChildrenStatus();
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.CensusTabAction, Analytics.NoChildrenStatusEvent);
            HourGlass(this, false);
        }

        void BtnMismatchedChildrenStatus_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            People people = new();
            people.SetupChildrenStatusReport();
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.CensusTabAction, Analytics.MisMatchedEvent);
            HourGlass(this, false);
        }

        void ShowCensusRefFacts(CensusReference.ReferenceStatus status, Predicate<Individual> filter)
        {
            HourGlass(this, true);
            CensusDate date = chkAnyCensusYear.Checked ? CensusDate.ANYCENSUS : cenDate.SelectedDate;
            Facts facts = new(status, filter, date);
            facts.Show();
            HourGlass(this, false);
        }

        void BtnCensusRefs_Click(object sender, EventArgs e) =>
            ShowCensusRefFacts(CensusReference.ReferenceStatus.GOOD, CreateIndividualCensusFilter(cenDate.SelectedDate, true, txtCensusSurname.Text, chkAnyCensusYear.Checked));

        void BtnMissingCensusRefs_Click(object sender, EventArgs e) =>
            ShowCensusRefFacts(CensusReference.ReferenceStatus.BLANK, CreateIndividualCensusFilter(cenDate.SelectedDate, true, txtCensusSurname.Text, chkAnyCensusYear.Checked));

        void BtnIncompleteCensusRef_Click(object sender, EventArgs e) =>
            ShowCensusRefFacts(CensusReference.ReferenceStatus.INCOMPLETE, CreateIndividualCensusFilter(cenDate.SelectedDate, true, txtCensusSurname.Text, chkAnyCensusYear.Checked));

        void BtnUnrecognisedCensusRef_Click(object sender, EventArgs e) =>
            ShowCensusRefFacts(CensusReference.ReferenceStatus.UNRECOGNISED, CreateIndividualCensusFilter(cenDate.SelectedDate, true, txtCensusSurname.Text, chkAnyCensusYear.Checked));


        void BtnReportUnrecognised_Click(object sender, EventArgs e)
        {
            IEnumerable<string> unrecognisedResults = ft.UnrecognisedCensusReferences();
            IEnumerable<string> missingResults = ft.MissingCensusReferences();
            IEnumerable<string> notesResults = ft.UnrecognisedCensusReferencesNotes();

            if (unrecognisedResults.Any() || missingResults.Any() || notesResults.Any())
                SaveUnrecognisedDataFile(unrecognisedResults, missingResults, notesResults, $"Unrecognised & Missing Census References for {Path.GetFileNameWithoutExtension(filename)}.txt",
                    "\n\nPlease check the file and remove any private notes information before posting");
            else
                UIHelpers.ShowMessage("No unrecognised census references found.", "FTAnalyzer");
        }


        static void SaveUnrecognisedDataFile(IEnumerable<string> unrecognisedResults, IEnumerable<string> missingResults, IEnumerable<string> notesResults,
                                      string unrecognisedFilename, string privateWarning)
        {
            try
            {
                string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using SaveFileDialog saveFileDialog = new();
                string initialDir = Application.UserAppDataRegistry.GetValue("Report Unrecognised Census References Path", myDocuments).ToString() ?? string.Empty;
                saveFileDialog.InitialDirectory = initialDir ?? myDocuments;
                saveFileDialog.FileName = unrecognisedFilename;
                saveFileDialog.Filter = "Report File (*.txt)|*.txt";
                saveFileDialog.FilterIndex = 1;

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.GetDirectoryName(saveFileDialog.FileName) ?? string.Empty;
                    Application.UserAppDataRegistry.SetValue("Report Unrecognised Census References Path", path);
                    FamilyTree.WriteUnrecognisedReferencesFile(unrecognisedResults, missingResults, notesResults, saveFileDialog.FileName);
                    Analytics.TrackAction(Analytics.ReportsAction, Analytics.UnrecognisedCensusEvent);
                    UIHelpers.ShowMessage("File written to " + saveFileDialog.FileName + "\n\nPlease create an issue at https://www.ftanalyzer.com/issues in issues section and upload your file, if you feel you have standard census references that should be recognised." + privateWarning, "FTAnalyzer");
                }
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage(ex.Message, "FTAnalyzer");
            }
        }

        async void BtnInconsistentLocations_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            List<DisplayFact> results = [];
            tspbTabProgress.Maximum = 100;
            tspbTabProgress.Visible = true;
            Progress<int> progress = new(value => { tspbTabProgress.Value = value; });
            Predicate<Individual> filter = CreateIndividualCensusFilter(cenDate.SelectedDate, true, txtCensusSurname.Text, chkAnyCensusYear.Checked);
            await Task.Run(() => results = ProcessInconsistentLocations(filter, progress));
            tspbTabProgress.Visible = false;
            Facts factForm = new(results);
            DisposeDuplicateForms(factForm);
            factForm.Show();
            factForm.ShowHideFactRows();
            HourGlass(this, false);
        }

        List<DisplayFact> ProcessInconsistentLocations(Predicate<Individual> filter, IProgress<int> progressBar)
        {
            List<DisplayFact> results = [];
            int records = 0;
            int currentProgress = 0;
            progressBar.Report(records);
            List<DisplayFact> censusRefs = [];
            foreach (Individual ind in ft.AllIndividuals.Filter(filter))
                foreach (Fact f in ind.AllFacts)
                    if (f.IsCensusFact && f.CensusReference is not null && f.CensusReference.Reference.Length > 0)
                        censusRefs.Add(new DisplayFact(ind, f));
            IEnumerable<string> distinctRefs = censusRefs.Select(x => x.FactDate.StartDate.Year + x.CensusReference.ToString()).Distinct();
            int maxRecords = distinctRefs.Count() + 1;
            foreach (string censusref in distinctRefs)
            {
                IEnumerable<DisplayFact> result = censusRefs.Filter(x => censusref == x.FactDate.StartDate.Year + x.CensusReference.ToString());
                int count = result.Select(x => x.Location).Distinct().Count();
                if (count > 1)
                    results.AddRange(result);
                int progress = Math.Min(100 * records++ / maxRecords, 100);
                Debug.Print($"Record: {records} progress: {progress}");
                if (progress != currentProgress)
                {
                    currentProgress = progress;
                    progressBar.Report(progress);
                }
            }
            return results;
        }

        void BtnCensusProblemFacts_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> filter = new(x => x.ErrorFacts.Count > 0);
            Facts facts = new(filter, true);
            facts.Show();
            HourGlass(this, false);
        }

        void BtnCensusAutoCreatedFacts_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> filter = new(x => x.FactCount(Fact.CENSUS_FTA) > 0);
            Facts facts = new(filter, false);
            facts.Show();
            HourGlass(this, false);
        }
        #endregion

        #region Colour Reports Tab
        void BtnColourBMD_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> relTypeFilter = relTypesResearchSuggest.BuildFilter<Individual>(x => x.RelationType);
            ComboBoxFamily? cbFamily = cmbColourFamily.SelectedItem as ComboBoxFamily;
            List<IDisplayColourBMD> list = ft.ColourBMD(relTypeFilter, txtColouredSurname.Text, cbFamily);
            ColourBMD rs = new(list);
            DisposeDuplicateForms(rs);
            rs.Show();
            rs.Focus();
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.ColourBMDEvent);
            HourGlass(this, false);
        }

        async void DisplayColourCensus(string country)
        {
            HourGlass(this, true);
            Predicate<Individual> relTypeFilter = relTypesResearchSuggest.BuildFilter<Individual>(x => x.RelationType);
            ComboBoxFamily? cbFamily = cmbColourFamily.SelectedItem as ComboBoxFamily;
            List<IDisplayColourCensus> list =
                    ft.ColourCensus(country, relTypeFilter, txtColouredSurname.Text, cbFamily, ckbIgnoreNoBirthDate.Checked, ckbIgnoreNoDeathDate.Checked);
            ColourCensus rs = new(country, list);
            DisposeDuplicateForms(rs);
            rs.Show();
            rs.Focus();
            await Analytics.TrackActionAsync(Analytics.MainFormAction, Analytics.ColourCensusEvent, country).ConfigureAwait(true);
            HourGlass(this, false);
        }

        void BtnUKColourCensus_Click(object sender, EventArgs e) => DisplayColourCensus(Countries.UNITED_KINGDOM);

        void BtnIrishColourCensus_Click(object sender, EventArgs e) => DisplayColourCensus(Countries.IRELAND);

        void BtnUSColourCensus_Click(object sender, EventArgs e) => DisplayColourCensus(Countries.UNITED_STATES);

        void BtnCanadianColourCensus_Click(object sender, EventArgs e) => DisplayColourCensus(Countries.CANADA);

        void BtnStandardMissingData_Click(object sender, EventArgs e) => UIHelpers.ShowMessage("Not Implemented Yet", "FTAnalyzer");

        void BtnAdvancedMissingData_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> relTypeFilter = relTypesResearchSuggest.BuildFilter<Individual>(x => x.RelationType);
            ComboBoxFamily? cbFamily = cmbColourFamily.SelectedItem as ComboBoxFamily;
            List<IDisplayMissingData> list = ft.MissingData(relTypeFilter, txtColouredSurname.Text, cbFamily);
            MissingData rs = new(list);
            DisposeDuplicateForms(rs);
            rs.Show();
            rs.Focus();
            HourGlass(this, false);
        }

        void CmbColourFamily_Click(object sender, EventArgs e) => UpdateColourFamilyComboBox(null);

        void RelTypesColoured_RelationTypesChanged(object sender, EventArgs e) => RefreshColourFamilyComboBox();

        void TxtColouredSurname_TextChanged(object sender, EventArgs e) => RefreshColourFamilyComboBox();

        void RefreshColourFamilyComboBox()
        {
            ComboBoxFamily? f = null;
            if (cmbColourFamily.Text != "All Families")
                f = cmbColourFamily.SelectedItem as ComboBoxFamily; // store the previous value to set it again after
            ClearColourFamilyCombo();
            bool stillThere = UpdateColourFamilyComboBox(f);
            if (f is not null && stillThere)  // the previously selected value is still present so select it
                cmbColourFamily.SelectedItem = f;
        }

        void ClearColourFamilyCombo()
        {
            cmbColourFamily.Items.Clear();
            cmbColourFamily.Text = "All Families";
        }

        bool UpdateColourFamilyComboBox(ComboBoxFamily? f)
        {
            bool stillThere = false;
            if (cmbColourFamily.Items.Count == 0)
            {
                HourGlass(this, true);
                IEnumerable<Family> candidates = ft.AllFamilies;
                Predicate<Family> relationFilter = relTypesResearchSuggest.BuildFamilyFilter<Family>(x => x.RelationTypes);
                if (txtColouredSurname.Text.Length > 0)
                    candidates = candidates.Filter(x => x.ContainsSurname(txtColouredSurname.Text, true));
                List<Family> list = [.. candidates.Filter(relationFilter)];
                list.Sort(new DefaultFamilyComparer());
                foreach (Family family in list)
                {
                    ComboBoxFamily cbf = new(family);
                    cmbColourFamily.Items.Add(cbf);
                    if (cbf.Equals(f))
                        stillThere = true;
                }
                btnReferrals.Enabled = true;
                HourGlass(this, false);
            }
            return stillThere;
        }

        void BtnRandomSurnameColour_Click(object sender, EventArgs e) => txtColouredSurname.Text = GetRandomSurname();
        #endregion

        #region Loose Birth/Death Tabs
        void SetupLooseBirths()
        {
            try
            {
                SortableBindingList<IDisplayLooseBirth> looseBirthList = ft.LooseBirths();
                dgLooseBirths.DataSource = looseBirthList;
                dgLooseBirths.Sort(dgLooseBirths.Columns[nameof(IDisplayLooseBirth.Forenames)], ListSortDirection.Ascending);
                dgLooseBirths.Sort(dgLooseBirths.Columns[nameof(IDisplayLooseBirth.Surname)], ListSortDirection.Ascending);
                dgLooseBirths.Focus();
                mnuPrint.Enabled = true;
                tsCountLabel.Text = Messages.Count + looseBirthList.Count;
                tsHintsLabel.Text = Messages.Hints_Loose_Births + Messages.Hints_Individual;
                dgLooseBirths.VirtualGridFiltered += VirtualGridFiltered;
            }
            catch (LooseDataException ex)
            {
                UIHelpers.ShowMessage(ex.Message, "FTAnalyzer");
            }
        }

        void SetupLooseDeaths()
        {
            try
            {
                SortableBindingList<IDisplayLooseDeath> looseDeathList = ft.LooseDeaths();
                dgLooseDeaths.DataSource = looseDeathList;
                dgLooseDeaths.Sort(dgLooseDeaths.Columns[nameof(IDisplayLooseDeath.Forenames)], ListSortDirection.Ascending);
                dgLooseDeaths.Sort(dgLooseDeaths.Columns[nameof(IDisplayLooseDeath.Surname)], ListSortDirection.Ascending);
                dgLooseDeaths.Focus();
                mnuPrint.Enabled = true;
                tsCountLabel.Text = Messages.Count + looseDeathList.Count;
                tsHintsLabel.Text = Messages.Hints_Loose_Deaths + Messages.Hints_Individual;
                dgLooseDeaths.VirtualGridFiltered += VirtualGridFiltered;
            }
            catch (LooseDataException ex)
            {
                UIHelpers.ShowMessage(ex.Message, "FTAnalyzer");
            }
        }

        void SetupLooseInfo()
        {
            try
            {
                SortableBindingList<IDisplayLooseInfo> looseInfoList = ft.LooseInfo();
                dgLooseInfo.DataSource = looseInfoList;
                dgLooseInfo.Sort(dgLooseInfo.Columns[nameof(IDisplayLooseInfo.Forenames)], ListSortDirection.Ascending);
                dgLooseInfo.Sort(dgLooseInfo.Columns[nameof(IDisplayLooseInfo.Surname)], ListSortDirection.Ascending);
                dgLooseInfo.Focus();
                mnuPrint.Enabled = true;
                tsCountLabel.Text = Messages.Count + looseInfoList.Count;
                tsHintsLabel.Text = "Double click to view records. " + Messages.Hints_Individual;
                dgLooseInfo.VirtualGridFiltered += VirtualGridFiltered;
            }
            catch (LooseDataException ex)
            {
                UIHelpers.ShowMessage(ex.Message, "FTAnalyzer");
            }
        }

        #endregion

        #region View Notes
        void CtxViewNotes_Opening(object sender, CancelEventArgs e)
        {
            Individual? ind = GetContextIndividual(sender);
            if (ind is not null)
                mnuViewNotes.Enabled = ind.HasNotes;
            else
                e.Cancel = true;
        }

        static Individual? GetContextIndividual(object? sender)
        {
            Individual? ind = null;
            ContextMenuStrip? cms = null;
            if (sender is ContextMenuStrip strip)
                cms = strip;
            if (sender is ToolStripMenuItem tsmi && tsmi.Owner is not null)
                cms = (ContextMenuStrip)tsmi.Owner;
            if (cms is not null && cms.Tag is not null)
                ind = (Individual)cms.Tag;
            return ind;
        }

        void MnuViewNotes_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Individual? ind = GetContextIndividual(sender);
            if (ind is not null)
            {
                Notes notes = new(ind);
                notes.Show();
            }
            HourGlass(this, false);
        }

        void DgTreeTops_MouseDown(object sender, MouseEventArgs e) => ShowViewNotesMenu(dgTreeTops, e);

        void DgWorldWars_MouseDown(object sender, MouseEventArgs e) => ShowViewNotesMenu(dgWorldWars, e);

        void ShowViewNotesMenu(VirtualDataGridView<IDisplayIndividual> dg, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hti = dg.HitTest(e.Location.X, e.Location.Y);
            if (e.Button == MouseButtons.Right)
            {
                var ht = dg.HitTest(e.X, e.Y);
                if (ht.Type != DataGridViewHitTestType.ColumnHeader)
                {
                    if (hti.RowIndex >= 0 && hti.ColumnIndex >= 0)
                    {
                        dg.CurrentCell = dg.Rows[hti.RowIndex].Cells[hti.ColumnIndex];
                        // Can leave these here - doesn't hurt
                        dg.Rows[hti.RowIndex].Selected = true;
                        dg.Focus();
                        ctxViewNotes.Tag = dg.CurrentRowDataBoundItem;
                        ctxViewNotes.Show(MousePosition);
                    }
                }
            }
        }
        #endregion

        #region Referrals
        void CmbReferrals_Click(object sender, EventArgs e)
        {
            if (cmbReferrals.Items.Count == 0)
            {
                HourGlass(this, true);
                List<Individual> list = [.. ft.AllIndividuals];
                list.Sort(new NameComparer<Individual>(true, false));
                foreach (Individual ind in list)
                    cmbReferrals.Items.Add(ind);
                btnReferrals.Enabled = true;
                HourGlass(this, false);
            }
        }

        void BtnReferrals_Click(object sender, EventArgs e)
        {
            if (cmbReferrals.SelectedItem is Individual selected)
            {
                HourGlass(this, true);
                Individual root = ft.RootPerson;
                ft.SetRelations(selected.IndividualID, null);
                LostCousinsReferral lcr = new(selected, ckbReferralInCommon.Checked);
                DisposeDuplicateForms(lcr);
                lcr.Show();
                ft.SetRelations(root.IndividualID, null);
                HourGlass(this, false);
            }
        }
        #endregion

        #region Export To Excel
        void IndividualsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable([.. ft.AllIndividuals]))
                ExportToExcel.Export(dt);
            Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportIndEvent);
            HourGlass(this, false);
        }

        void FamiliesToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable([.. ft.AllFamilies]))
                ExportToExcel.Export(dt);
            Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportFamEvent);
            HourGlass(this, false);
        }

        void FactsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable([.. ft.AllExportFacts]))
                ExportToExcel.Export(dt);
            Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportFactsEvent);
            HourGlass(this, false);
        }

        void LooseBirthsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            try
            {
                List<IDisplayLooseBirth> list = [.. ft.LooseBirths()];
                list.Sort(new LooseBirthComparer());
                using (DataTable dt = ListtoDataTableConvertor.ToDataTable(list))
                    ExportToExcel.Export(dt);
                Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportLooseBirthsEvent);
            }
            catch (LooseDataException ex)
            {
                UIHelpers.ShowMessage(ex.Message, "FTAnalyzer");
            }
            HourGlass(this, false);
        }

        void LooseDeathsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            try
            {
                List<IDisplayLooseDeath> list = [.. ft.LooseDeaths()];
                list.Sort(new LooseDeathComparer());
                using (DataTable dt = ListtoDataTableConvertor.ToDataTable(list))
                    ExportToExcel.Export(dt);
                Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportLooseDeathsEvent);
            }
            catch (LooseDataException ex)
            {
                UIHelpers.ShowMessage(ex.Message, "FTAnalyzer");
            }
            HourGlass(this, false);
        }

        void MnuExportLocations_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable([.. ft.AllDisplayPlaces]))
                ExportToExcel.Export(dt);
            Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportLocationsEvent);
            HourGlass(this, false);
        }

        void MnuSourcesToExcel_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable([.. ft.AllSources]))
                ExportToExcel.Export(dt);
            Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportSourcesEvent);
            HourGlass(this, false);
        }

        void MnuCustomFactsToExcel_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable([.. ft.AllCustomFacts]))
                ExportToExcel.Export(dt);
            Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportCustomFactEvent);
            HourGlass(this, false);
        }

        void MnuDataErrorsToExcel_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable([.. DataErrors(ckbDataErrors)]))
                ExportToExcel.Export(dt);
            Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportDataErrorsEvent);
            HourGlass(this, false);
        }

        void MnuDuplicatesToExcel_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            if (duplicateData is not null)
            {
                using (DataTable dt = ListtoDataTableConvertor.ToDataTable([.. duplicateData]))
                    ExportToExcel.Export(dt);
                Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportDuplicatesEvent);
            }
            HourGlass(this, false);
        }

        void MnuTreetopsToExcel_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            Predicate<Individual> filter = CreateTreeTopsIndividualFilter();
            List<IExportIndividual> treeTopsList = [.. ft.GetExportTreeTops(filter)];
            treeTopsList.Sort(new BirthDateComparer());
            SortableBindingList<IExportIndividual> list = [.. treeTopsList];
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable(list.ToList()))
                ExportToExcel.Export(dt);
            Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportTreeTopsEvent);
            HourGlass(this, false);
        }

        void MnuWorldWarsToExcel_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            if (warDeadFilter is not null)
            {
                ListtoDataTableConvertor convertor = new();
                List<IExportIndividual> warDeadList = [.. ft.GetExportWorldWars(warDeadFilter)];
                warDeadList.Sort(new BirthDateComparer(BirthDateComparer.ASCENDING));
                SortableBindingList<IExportIndividual> list = [.. warDeadList];
                using (DataTable dt = ListtoDataTableConvertor.ToDataTable(list.ToList()))
                    ExportToExcel.Export(dt);
                Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportWorldWarsEvent);
            }
            HourGlass(this, false);
        }

        async void MnuSurnamesToExcel_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            ListtoDataTableConvertor convertor = new();
            SortableBindingList<IDisplaySurnames> stats;
            if (dgSurnames.DataSource is not null)
                stats = dgSurnames.DataSource;
            else
            {
                tspbTabProgress.Visible = true;
                Predicate<Individual> indFilter = reltypesSurnames.BuildFilter<Individual>(x => x.RelationType);
                Predicate<Family> famFilter = reltypesSurnames.BuildFamilyFilter<Family>(x => x.RelationTypes);
                Progress<int> progress = new(value => { tspbTabProgress.Value = value; });
                stats = await Task.Run(() =>
                    new SortableBindingList<IDisplaySurnames>(Statistics.Instance.Surnames(indFilter, famFilter, progress, chkSurnamesIgnoreCase.Checked))).ConfigureAwait(true);
                tspbTabProgress.Visible = false;
            }
            List<IDisplaySurnames> list = [.. stats];
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable(list))
                ExportToExcel.Export(dt);
            await Analytics.TrackAction(Analytics.ExportAction, Analytics.ExportSurnamesEvent);
            HourGlass(this, false);
        }
        #endregion

        #region Today

        async Task ShowTodaysEvents()
        {
            pbToday.Visible = true;
            labTodayLoadWorldEvents.Visible = true;
            rtbToday.ResetText();
            Progress<int> progress = new(value => { pbToday.Value = value; });
            Progress<string> outputText = new(text => { rtbToday.Rtf = text; });
            await Task.Run(() => ft.AddTodaysFacts(dpToday.Value, rbTodayMonth.Checked, (int)nudToday.Value, progress, outputText)).ConfigureAwait(true);
            labTodayLoadWorldEvents.Visible = false;
            pbToday.Visible = false;
            await Analytics.TrackAction(Analytics.MainFormAction, Analytics.TodayClickedEvent).ConfigureAwait(true);
        }


        void RbTodayMonth_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Application.UserAppDataRegistry.SetValue("Todays Events Month", rbTodayMonth.Checked);
            }
            catch (IOException)
            {
                UIHelpers.ShowMessage("Unable to save Today preference. Please check App has rights to save user preferences to registry.");
            }
        }


        void RbTodaySingle_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                Application.UserAppDataRegistry.SetValue("Todays Events Month", !rbTodaySingle.Checked);
            }
            catch (IOException)
            {
                UIHelpers.ShowMessage("Unable to save Today preference. Please check App has rights to save user preferences to registry.");
            }
        }

        async void BtnUpdateTodaysEvents_Click(object sender, EventArgs e) => await ShowTodaysEvents().ConfigureAwait(true);


        void NudToday_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                Application.UserAppDataRegistry.SetValue("Todays Events Step", nudToday.Value);
            }
            catch (IOException)
            {
                UIHelpers.ShowMessage("Unable to save Today preference. Please check App has rights to save user preferences to registry.");
            }
        }
        #endregion


        public void SetFactTypeList(CheckedListBox ckbFactSelect, CheckedListBox ckbFactExclude, Predicate<ExportFact> filter)
        {
            List<string> factTypes = [.. ft.AllExportFacts.Filter(filter).Select(x => x.FactType).Distinct()];
            factTypes.Sort();
            ckbFactSelect.Items.Clear();
            ckbFactExclude.Items.Clear();
            foreach (string factType in factTypes)
            {
                if (!ckbFactSelect.Items.Contains(factType))
                {
                    int index = ckbFactSelect.Items.Add(factType);
                    bool itemChecked = Application.UserAppDataRegistry.GetValue($"Fact: {factType}", "True").Equals("True");
                    ckbFactSelect.SetItemChecked(index, itemChecked);
                }
                if (!ckbFactExclude.Items.Contains(factType))
                {
                    int index = ckbFactExclude.Items.Add(factType);
                    bool itemChecked = Application.UserAppDataRegistry.GetValue($"Exlude Fact: {factType}", "False").Equals("True");
                    ckbFactExclude.SetItemChecked(index, itemChecked);
                }
            }
        }

        void MnuLoadLocationsCSV_Click(object sender, EventArgs e) => LoadLocations(tspbTabProgress, tsStatusLabel, 1);

        void MnuLoadLocationsTNG_Click(object sender, EventArgs e) => LoadLocations(tspbTabProgress, tsStatusLabel, 2);

        #region Load CSV Location Data


        public static void LoadLocationData(ToolStripProgressBar pb, ToolStripStatusLabel label, int defaultIndex)
        {
            string csvFilename = string.Empty;
            pb.Visible = true;
            try
            {
                string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using OpenFileDialog openFileDialog = new();
                string initialDir = Application.UserAppDataRegistry.GetValue("Excel Export Individual Path", myDocuments).ToString() ?? string.Empty;
                openFileDialog.InitialDirectory = initialDir ?? myDocuments;
                openFileDialog.Filter = "Comma Separated Value (*.csv)|*.csv|TNG format (*.tng)|*.tng";
                openFileDialog.FilterIndex = defaultIndex;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    csvFilename = openFileDialog.FileName;
                    label.Text = "Loading " + csvFilename;
                    string path = Path.GetDirectoryName(csvFilename) ?? string.Empty;
                    Application.UserAppDataRegistry.SetValue("Excel Export Individual Path", path);
                    if (csvFilename.EndsWith("TNG", StringComparison.InvariantCultureIgnoreCase))
                        ReadTNGdata(pb, csvFilename);
                    else
                        ReadCSVdata(pb, csvFilename);
                }
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage($"Error loading CSV location data from {csvFilename}\nError was {ex.Message}", "FTAnalyzer");
            }
            pb.Visible = false;
            label.Text = string.Empty;
        }

        public static void ReadTNGdata(ToolStripProgressBar pb, string tngFilename)
        {
            int rowCount = 0;
            int lineCount = File.ReadLines(tngFilename).Count();
            pb.Maximum = lineCount;
            pb.Minimum = 0;
            pb.Value = rowCount;
            using CsvFileReader reader = new(tngFilename, ';');
            CsvRow row = [];
            while (reader.ReadRow(row))
            {
                if (row.Count == 4)
                {
                    FactLocation.GetLocation(row[1], row[3], row[2], FactLocation.Geocode.NOT_SEARCHED, true, true);
                    rowCount++;
                }
                pb.Value++;
                if (pb.Value % 10 == 0)
                    Application.DoEvents();
            }
            UIHelpers.ShowMessage($"Loaded {rowCount} locations from TNG file {tngFilename}", "FTAnalyzer");
        }

        public static void ReadCSVdata(ToolStripProgressBar pb, string csvFilename)
        {
            int rowCount = 0;
            int lineCount = File.ReadLines(csvFilename).Count();
            pb.Maximum = lineCount;
            pb.Minimum = 0;
            pb.Value = rowCount;
            using (CsvFileReader reader = new(csvFilename))
            {
                CsvRow headerRow = [];
                CsvRow row = [];

                reader.ReadRow(headerRow);
                if (headerRow.Count != 3)
                    throw new InvalidLocationCSVFileException("Location file should have 3 values per line.");
                if (!headerRow[0].Trim().ToUpper().Equals("LOCATION"))
                    throw new InvalidLocationCSVFileException("No Location header record. Header should be Location, Latitude, Longitude");
                if (!headerRow[1].Trim().ToUpper().Equals("LATITUDE"))
                    throw new InvalidLocationCSVFileException("No Latitude header record. Header should be Location, Latitude, Longitude");
                if (!headerRow[2].Trim().ToUpper().Equals("LONGITUDE"))
                    throw new InvalidLocationCSVFileException("No Longitude header record. Header should be Location, Latitude, Longitude");
                while (reader.ReadRow(row))
                {
                    if (row.Count == 3)
                    {
                        FactLocation loc = FactLocation.GetLocation(row[0], row[1], row[2], FactLocation.Geocode.NOT_SEARCHED, true, true);
                        rowCount++;
                    }
                    pb.Value++;
                    if (pb.Value % 10 == 0)
                        Application.DoEvents();
                }
            }
            UIHelpers.ShowMessage($"Loaded {rowCount} locations from file {csvFilename}", "FTAnalyzer");
        }
        #endregion


        void LoadLocations(ToolStripProgressBar pb, ToolStripStatusLabel label, int defaultIndex)
        {
            DialogResult result = UIHelpers.ShowMessage("It is recommended you backup your Geocoding database first.\nDo you want to backup now?", "FTAnalyzer",
                                                         MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
                DatabaseHelper.Instance.BackupDatabase(saveDatabase, "FTAnalyzer zip file created by v" + VERSION);
            if (result != DialogResult.Cancel)
                LoadLocationData(pb, label, defaultIndex);
        }

        async void BtnShowSurnames_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            tsCountLabel.Text = string.Empty;
            tsHintsLabel.Text = string.Empty;
            tspbTabProgress.Visible = true;
            Predicate<Individual> indFilter = reltypesSurnames.BuildFilter<Individual>(x => x.RelationType);
            Predicate<Family> famFilter = reltypesSurnames.BuildFamilyFilter<Family>(x => x.RelationTypes);
            var progress = new Progress<int>(value => { tspbTabProgress.Value = value; });
            var list = await Task.Run(() =>
                new SortableBindingList<IDisplaySurnames>(Statistics.Instance.Surnames(indFilter, famFilter, progress, chkSurnamesIgnoreCase.Checked))).ConfigureAwait(true);
            tspbTabProgress.Visible = false;
            dgSurnames.DataSource = list;
            dgSurnames.Sort(dgSurnames.Columns[nameof(IDisplaySurnames.Surname)], ListSortDirection.Ascending);
            dgSurnames.Focus();
            tsCountLabel.Text = $"{Messages.Count}{list.Count} Surnames.";
            tsHintsLabel.Text = Messages.Hints_Surname;
            dgSurnames.VirtualGridFiltered += VirtualGridFiltered;
            HourGlass(this, false);
            await Analytics.TrackAction(Analytics.MainFormAction, Analytics.ShowSurnamesEvent).ConfigureAwait(true);
        }

        void CousinsCountReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            StatisticsForm f = new(StatisticsForm.StatisticType.CousinCount);
            DisposeDuplicateForms(f);
            f.Show();
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.CousinCountEvent);
        }

        void HowManyDirectsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            StatisticsForm f = new(StatisticsForm.StatisticType.HowManyDirects);
            DisposeDuplicateForms(f);
            f.Show();
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.DirectsReportEvent);
        }

        void FacebookSupportGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpecialMethods.VisitWebsite("https://www.facebook.com/ftanalyzer");
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.FacebookSupportEvent);
        }

        void FacebookUserGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpecialMethods.VisitWebsite("https://www.facebook.com/groups/ftanalyzer");
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.FacebookUsersEvent);
        }

        void MnuDNA_GEDCOM_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            DNA_GEDCOM.Export();
            HourGlass(this, false);
        }

        void GetGoogleAPIKeyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SpecialMethods.VisitWebsite("https://developers.google.com/maps/documentation/embed/get-api-key");
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.GoogleAPIKey);
        }

        void GoogleAPISetupGuideToolStripMenuItem_Click(object sender, EventArgs e) => SpecialMethods.VisitWebsite("https://www.ftanalyzer.com/GoogleAPI");

        void BirthdayEffectReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            StatisticsForm f = new(StatisticsForm.StatisticType.BirthdayEffect);
            DisposeDuplicateForms(f);
            f.Show();
            HourGlass(this, false);
            Analytics.TrackAction(Analytics.MainFormAction, Analytics.BirthdayEffectEvent);
        }

        void PossiblyMissingChildReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            People people = new();
            people.SetupPossiblyMissingChildrenReport();
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.ReportsAction, Analytics.PossiblyMissingChildren);
            HourGlass(this, false);
        }

        void MnuAgedOver99Report_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            People people = new();
            people.SetupAgedOver99Report();
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.ReportsAction, Analytics.AgedOver99Report);
            HourGlass(this, false);
        }

        void MnuSingleParentsReport_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            People people = new();
            people.SingleParents();
            DisposeDuplicateForms(people);
            people.Show();
            Analytics.TrackAction(Analytics.ReportsAction, Analytics.AgedOver99Report);
            HourGlass(this, false);
        }


        void MnuJSON_Click(object sender, EventArgs e)
        {
            HourGlass(this, true);
            try
            {
                string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using SaveFileDialog saveFileDialog = new();
                string initialDir = Application.UserAppDataRegistry.GetValue("JSON Export Path", myDocuments).ToString() ?? string.Empty;
                saveFileDialog.InitialDirectory = initialDir ?? myDocuments;
                saveFileDialog.Filter = "JavaScript Object Notation (*.json)|*.json";
                saveFileDialog.FilterIndex = 1;
                DialogResult dr = saveFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    string path = Path.GetDirectoryName(saveFileDialog.FileName) ?? string.Empty;
                    Application.UserAppDataRegistry.SetValue("JSON Export Path", path);
                    using (StreamWriter output = new(new FileStream(saveFileDialog.FileName, FileMode.Create, FileAccess.Write), Encoding.UTF8))
                    {
                        var data = new JsonExport(filename);
                        data.WriteJsonData(output);
                    }
                    UIHelpers.ShowMessage($"File written to {saveFileDialog.FileName}", "FTAnalyzer");
                }
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage(ex.Message, "FTAnalyzer");
            }
            HourGlass(this, false);
        }

        FactDate AliveDate { get; set; }
        void TxtAliveDates_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtAliveDates.Text))
            {
                txtAliveDates.Text = "Enter valid GEDCOM date/date range";
                AliveDate = FactDate.UNKNOWN_DATE;
                return;
            }
            FactDate aliveDate;
            HourGlass(this, true);
            try
            {
                aliveDate = new FactDate(txtAliveDates.Text);
            }
            catch (FactDateException)
            {
                aliveDate = FactDate.UNKNOWN_DATE;
            }
            HourGlass(this, false);
            if (aliveDate == FactDate.UNKNOWN_DATE)
            {
                e.Cancel = true;
                UIHelpers.ShowMessage($"{txtAliveDates.Text} is not a valid GEDCOM date.", "FTAnalyzer");
                return;
            }
            AliveDate = aliveDate;
        }

        void TxtAliveDates_Enter(object sender, EventArgs e)
        {
            if (txtAliveDates.Text.StartsWith("Enter"))
                txtAliveDates.Text = string.Empty;
        }

        void BtnAliveOnDate_Click(object sender, EventArgs e)
        {
            if (AliveDate != FactDate.UNKNOWN_DATE)
            {
                HourGlass(this, true);
                People people = new();
                Predicate<Individual> filter = CreateAliveatDateFilter(AliveDate, txtCensusSurname.Text);
                people.SetupAliveAtDate(AliveDate, filter);
                DisposeDuplicateForms(people);
                people.Show();
                Analytics.TrackAction(Analytics.CensusTabAction, Analytics.AliveAtDate);
                HourGlass(this, false);
            }
        }

        void RadioFacts_CheckedChanged(object sender, EventArgs e) => SetShowFactsButton();


        async void MnuGoogleMyMaps_Click(object sender, EventArgs e)
        {
            try
            {
                string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                using SaveFileDialog saveFileDialog = new();
                string initialDir = Application.UserAppDataRegistry.GetValue("Google MyMaps Path", myDocuments).ToString() ?? string.Empty;
                string initialFile = Application.UserAppDataRegistry.GetValue("Google MyMaps Filename", myDocuments).ToString() ?? string.Empty;
                saveFileDialog.InitialDirectory = initialDir ?? myDocuments;
                saveFileDialog.FileName = initialFile ?? string.Empty;
                saveFileDialog.Filter = "Keyhole Markup Language (*.kml)|*.kml";
                saveFileDialog.FilterIndex = 1;
                DialogResult dr = saveFileDialog.ShowDialog();
                if (dr == DialogResult.OK)
                {
                    if (!saveFileDialog.FileName.EndsWith(".kml"))
                        saveFileDialog.FileName += ".kml";
                    string path = Path.GetDirectoryName(saveFileDialog.FileName) ?? string.Empty;
                    string file = Path.GetFileName(saveFileDialog.FileName) ?? string.Empty;
                    Application.UserAppDataRegistry.SetValue("Google MyMaps Path", path);
                    Application.UserAppDataRegistry.SetValue("Google MyMaps Filename", file);
                    Progress<int> progress = new(value => { tspbTabProgress.Value = value; });
                    tspbTabProgress.Visible = true;
                    tspbTabProgress.Maximum = 100;
                    await Task.Run(() =>
                        GoogleMap.GenerateKML(saveFileDialog.FileName, ft.AllExportableGeocodedLocations(progress)));
                    UIHelpers.ShowMessage($"File written to {saveFileDialog.FileName}", "FTAnalyzer");
                    tspbTabProgress.Visible = false;
                }
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage(ex.Message, "FTAnalyzer");
            }
        }
    }
}