using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace FTAnalyzer
{
    public partial class MainForm : Form
    {
        private string VERSION = "1.3.7.0";
        private bool _checkForUpdatesEnabled = true;
        private System.Threading.Timer _timerCheckForUpdates;

        private Cursor storedCursor = Cursors.Default;
        private FamilyTree ft = FamilyTree.Instance;
        private FactDate censusDate = CensusDate.UKCENSUS1881;
        private bool stopProcessing = false;

        public MainForm()
        {
            InitializeComponent();
            showLocationsToolStripMenuItem.Visible = false;
            ft.XmlErrorBox = rtbOutput;
            //tabSelector.TabPages.RemoveByKey("tabIGISearch");
            //toolTips.SetToolTip(tabCountries, "Double click on Country name to see list of individuals with that Country.");
            //toolTips.SetToolTip(dgCountries, "Double click on Country name to see list of individuals with that Country.");
            //toolTips.SetToolTip(tabRegions, "Double click on Region name to see list of individuals with that Region.");
            //toolTips.SetToolTip(tabParishes, "Double click on 'Parish' name to see list of individuals with that parish/area.");
            //toolTips.SetToolTip(tabAddresses, "Double click on Address name to see list of individuals with that Address.");
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openGedcom.InitialDirectory = Application.StartupPath + "../..";
            openGedcom.FileName = "*.ged";
            openGedcom.Filter = "GED files (*.ged)|*.ged|All files (*.*)|*.*";
            openGedcom.FilterIndex = 1;
            openGedcom.RestoreDirectory = true;

            if (openGedcom.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    HourGlass(true);
                    closeIndividualForms();
                    tabSelector.SelectTab(tabDisplayProgress);
                    rtbOutput.Text = "";
                    rtbIGIResults.Text = "";
                    pbSources.Value = pbIndividuals.Value = pbFamilies.Value = 0;
                    Application.DoEvents();
                    if (!stopProcessing)
                    {
                        XmlDocument document = GedcomToXml.Load(openGedcom.FileName);
                        document.Save("GedcomOutput.xml");
                        ft.LoadTree(document, pbSources, pbIndividuals, pbFamilies);
                        HourGlass(false);
                        MessageBox.Show("Gedcom File Loaded");
                    }
                }
                catch (IOException ex)
                {
                    MessageBox.Show("Error: Could not read file from disk. Original error: " + ex.Message);
                }
                catch (Exception ex2)
                {
                    MessageBox.Show("Error: Problem processing your file.\n" +
                        "Please report this at http://ftanalyzer.codeplex.com. Error was: " + ex2.Message);
                }
            }
        }

        private void closeIndividualForms()
        {
            List<Form> toClose = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (f is Forms.People || f is Forms.Census)
                    toClose.Add(f);
            }
            foreach (Form f in toClose)
                f.Close();
        }

        private void HourGlass(bool on)
        {
            if (on)
            {
                storedCursor = this.Cursor;
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = storedCursor;
            }
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ft.Loading)
            {
                tabSelector.SelectedTab = tabDisplayProgress;
            }
            else
            {
                if (tabSelector.SelectedTab == tabDisplayProgress)
                {
                    tsCountLabel.Text = "";
                }
                else if (tabSelector.SelectedTab == tabIndividuals)
                {
                    List<IDisplayIndividual> list = ft.AllDisplayIndividuals;
                    dgIndividuals.DataSource = list;
                    tsCountLabel.Text = "Count : " + list.Count;
                }
                else if (tabSelector.SelectedTab == tabCensus)
                {
                    cenDate.RevertToDefaultDate();
                    tsCountLabel.Text = "";
                    btnShowResults.Enabled = ft.IndividualCount > 0;
                }
                else if (tabSelector.SelectedTab == tabLostCousins)
                {
                    tsCountLabel.Text = "";
                    btnLC1881EW.Enabled = btnLC1881Scot.Enabled = btnLC1841EW.Enabled =
                        btnLC1881Canada.Enabled = btnLC1880USA.Enabled = btnLC1911Ireland.Enabled =
                        btnLC1911EW.Enabled = ft.IndividualCount > 0;
                }
                else if (tabSelector.SelectedTab == tabLooseDeaths)
                {
                    HourGlass(true);
                    List<IDisplayLooseDeath> looseDeathList = ft.GetLooseDeaths();
                    dgLooseDeaths.DataSource = looseDeathList;
                    tsCountLabel.Text = "Count : " + looseDeathList.Count;
                    HourGlass(false);
                }
                else if (tabSelector.SelectedTab == tabLocations)
                {
                    HourGlass(true);
                    tsCountLabel.Text = "";
                    List<IDisplayLocation> countries = ft.AllCountries;
                    List<IDisplayLocation> regions = ft.AllRegions;
                    List<IDisplayLocation> parishes = ft.AllParishes;
                    List<IDisplayLocation> addresses = ft.AllAddresses;
                    countries.Sort(new FactLocationComparer(FactLocation.COUNTRY));
                    regions.Sort(new FactLocationComparer(FactLocation.REGION));
                    parishes.Sort(new FactLocationComparer(FactLocation.PARISH));
                    addresses.Sort(new FactLocationComparer(FactLocation.ADDRESS));
                    dgCountries.DataSource = countries;
                    dgRegions.DataSource = regions;
                    dgParishes.DataSource = parishes;
                    dgAddresses.DataSource = addresses;
                    HourGlass(false);
                }
                else if (tabSelector.SelectedTab == tabIGISearch)
                {
                    btnCancelIGISearch.Visible = false;
                    btnViewResults.Visible = true;
                    tsCountLabel.Text = "";
                    btnIGIChildrenSearch.Enabled = btnIGIMarriageSearch.Enabled = ft.IndividualCount > 0;
                    try
                    {
                        txtIGIfolder.Text = (string)Application.UserAppDataRegistry.GetValue("IGI Search Path");
                    }
                    catch (Exception)
                    {
                        txtIGIfolder.Text = string.Empty;
                    }
                }
            }
        }

        private void dgIndividuals_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Comparer<IDisplayIndividual> comparer;
            switch (e.ColumnIndex)
            {
                case 0: // ID
                    comparer = new DefaultIndividualComparer();
                    break;
                case 1: // Forename
                    comparer = new IndividualNameComparer();
                    break;
                default:
                    comparer = new DefaultIndividualComparer();
                    break;
            }

            List<IDisplayIndividual> list = ft.AllDisplayIndividuals;
            list.Sort(comparer);
            dgIndividuals.DataSource = list;
            tsCountLabel.Text = "Count : " + list.Count;
        }

        private void dgCountries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgCountries.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.setLocation(loc, FactLocation.COUNTRY);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = dgRegions.CurrentRow == null ? new FactLocation() : (FactLocation)dgRegions.CurrentRow.DataBoundItem;
            if (Control.ModifierKeys == Keys.Shift)
            {
                // Do geo coding stuff
                Forms.GoogleMap frmMap = new Forms.GoogleMap();
                frmMap.setLocation(loc, FactLocation.REGION);
                frmMap.Show();
            } else {
                Forms.People frmInd = new Forms.People();
                frmInd.setLocation(loc, FactLocation.REGION);
                frmInd.Show();
            }
            HourGlass(false);
        }

        private void dgParishes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgParishes.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.setLocation(loc, FactLocation.PARISH);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgAddresses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgAddresses.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.setLocation(loc, FactLocation.ADDRESS);
            frmInd.Show();
            HourGlass(false);
        }

        private void btnShowResults_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = createCensusRegistrationFilter();
            MultiComparator<Registration> censusComparator = new MultiComparator<Registration>();
            if (!ckbNoLocations.Checked) // only compare on location if no locations isn't checked
                censusComparator.addComparator(new LocationComparator(FactLocation.PARISH));
            censusComparator.addComparator(new DateComparator());
            RegistrationsProcessor censusRP = new RegistrationsProcessor(filter, censusComparator);

            Forms.Census census = new Forms.Census();
            census.setupCensus(censusRP, censusDate, false, ckbCensusResidence.Checked, (int)udAgeFilter.Value);
            census.Text = censusDate.StartDate.Year.ToString() + " Census Records to search for";
            census.Show();
        }
 
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is Family Tree Analyzer version " + VERSION);
        }

        #region Filters
        private Filter<Registration> createCensusRegistrationFilter()
        {
            Filter<Registration> filter;
            Filter<Registration> locationFilter = censusCountry.BuildFilter<Registration>();
            Filter<Registration> relationFilter = relationTypes.BuildFilter<Registration>();

            if (ckbNoLocations.Checked)
                filter = new AndFilter<Registration>(relationFilter, new DateFilter<Registration>(cenDate.SelectedDate));
            else
                filter = new AndFilter<Registration>(locationFilter, relationFilter, new DateFilter<Registration>(cenDate.SelectedDate));

            if (txtSurname.Text.Length > 0)
                filter = new AndFilter<Registration>(filter, new SurnameFilter<Registration>(txtSurname.Text.ToUpper()));

            return filter;
        }

        private Filter<Individual> createTreeTopsIndividualFilter()
        {
            Filter<Individual> locationFilter = treetopsCountry.BuildFilter<Individual>();
            Filter<Individual> relationFilter = treetopsRelation.BuildFilter<Individual>();
            Filter<Individual> filter = new AndFilter<Individual>(locationFilter, relationFilter);

            if (txtTreetopsSurname.Text.Length > 0)
                filter = new AndFilter<Individual>(filter, new SurnameFilter<Individual>(txtTreetopsSurname.Text.ToUpper()));

            return filter;
        }

        private Filter<Individual> createWardeadIndividualFilter(FactDate birthRange, FactDate deathRange)
        {
            Filter<Individual> locationFilter = wardeadCountry.BuildFilter<Individual>();
            Filter<Individual> relationFilter = wardeadRelation.BuildFilter<Individual>();
            Filter<Individual> birthFilter = new IndividualBirthFilter(birthRange);
            Filter<Individual> deathFilter = new IndividualDeathFilter(deathRange);
            Filter<Individual> filter = new AndFilter<Individual>(
                                            new AndFilter<Individual>(birthFilter,deathFilter),
                                            new AndFilter<Individual>(locationFilter, relationFilter));

            if (tabSelector.Text.Length > 0)
                filter = new AndFilter<Individual>(filter, new SurnameFilter<Individual>(tabSelector.Text.ToUpper()));

            return filter;
        }
        #endregion 

        #region Lost Cousins
        private void LostCousinsCensus(Filter<Registration> filter, FactDate censusDate, string reportTitle)
        {
            HourGlass(true);
            Filter<Registration> relation =
                new OrFilter<Registration>(
                    new OrFilter<Registration>(new RelationFilter<Registration>(Individual.BLOOD), new RelationFilter<Registration>(Individual.DIRECT)),
                    new RelationFilter<Registration>(Individual.MARRIEDTODB));
            Filter<Registration> noLCFact = new NotFilter<Registration>(new FactFilter<Registration>(Fact.LOSTCOUSINS, censusDate));
            if (ckbRestrictions.Checked)
                filter = new AndFilter<Registration>(new DateFilter<Registration>(censusDate), filter, relation);
            else
                filter = new AndFilter<Registration>(new DateFilter<Registration>(censusDate), filter);
            if (ckbHideRecorded.Checked)
                filter = new AndFilter<Registration>(filter, noLCFact);
            MultiComparator<Registration> censusComparator = new MultiComparator<Registration>();
            censusComparator.addComparator(new LocationComparator(FactLocation.PARISH));
            censusComparator.addComparator(new DateComparator());
            RegistrationsProcessor censusRP = new RegistrationsProcessor(filter, censusComparator);

            Forms.Census census = new Forms.Census();
            census.setupCensus(censusRP, censusDate, true, ckbLCResidence.Checked, 110);
            census.Text = reportTitle;
            HourGlass(false);
            census.Show();
        }

        private void btnLC1881EW_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.ENGLAND, LocationFilter<Registration>.WALES);
            string reportTitle = "1881 England & Wales Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(filter, CensusDate.UKCENSUS1881, reportTitle);
        }

        private void btnLC1881Scot_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = LocationFilter<Registration>.SCOTLAND;
            string reportTitle = "1881 Scotland Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(filter, CensusDate.UKCENSUS1881, reportTitle);
        }

        private void btnLC1881Canada_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = LocationFilter<Registration>.CANADA;
            string reportTitle = "1881 Canada Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(filter, CensusDate.CANADACENSUS1881, reportTitle);
        }

        private void btnLC1841EW_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.ENGLAND, LocationFilter<Registration>.WALES);
            string reportTitle = "1841 England & Wales Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(filter, CensusDate.UKCENSUS1841, reportTitle);
        }


        private void btnLC1911EW_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.ENGLAND, LocationFilter<Registration>.WALES);
            string reportTitle = "1911 England & Wales Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(filter, CensusDate.UKCENSUS1911, reportTitle);
        }

        private void btnLC1880USA_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.USA, LocationFilter<Registration>.US);
            string reportTitle = "1880 US Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(filter, CensusDate.USCENSUS1880, reportTitle);
        }

        private void btnLC1911Ireland_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.EIRE, LocationFilter<Registration>.IRELAND);
            string reportTitle = "1911 Ireland Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(filter, new FactDate("1911"), reportTitle);
        }

        private void labLostCousinsWeb_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Help.ShowHelp(null, "http://www.lostcousins.com/?ref=LC585149");
            HourGlass(false);
        }

        private void labLostCousinsWeb_MouseEnter(object sender, EventArgs e)
        {
            storedCursor = this.Cursor;
            this.Cursor = Cursors.Hand;
        }

        private void labLostCousinsWeb_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = storedCursor;
        }
        #endregion

        private void _timerCheckForUpdates_Callback(object data)
        {
            if (_checkForUpdatesEnabled)
            {
                Version currentVersion = new Version(VERSION);
                string strLatestVersion = new Utilities.WebRequestWrapper().GetLatestVersionString();
                if (!string.IsNullOrEmpty(strLatestVersion))
                {
                    Version latestVersion = new Version(strLatestVersion);
                    if (currentVersion < latestVersion)
                    {
                        _checkForUpdatesEnabled = false;
                        DialogResult result = MessageBox.Show(string.Format("A new version of FTAnalyzer has been released, version {0}!\nWould you like to go to the FTAnalyzer site to download the new version?",
                            strLatestVersion), "New Version Released!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (result == DialogResult.Yes)
                            Help.ShowHelp(null, "http://FTAnalyzer.codeplex.com/");
                    }
                }
                string strBetaVersion = new Utilities.WebRequestWrapper().GetBetaVersionString();
                if (!string.IsNullOrEmpty(strBetaVersion))
                {
                    Version betaVersion = new Version(strBetaVersion);
                    if (currentVersion < betaVersion)
                    {
                        _checkForUpdatesEnabled = false;
                        DialogResult result = MessageBox.Show(string.Format("A new TEST version of FTAnalyzer has been released, version {0}!\nWould you like to go to the FTAnalyzer site to download the new version?\nPlease note this version is possibly unstable and should only be used by testers.",
                            strBetaVersion), "New TEST Version Released!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                            Help.ShowHelp(null, "http://FTAnalyzer.codeplex.com/");
                    }
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            _timerCheckForUpdates = new System.Threading.Timer(new System.Threading.TimerCallback(_timerCheckForUpdates_Callback));
            _timerCheckForUpdates.Change(3000, 1000 * 60 * 60 * 8); //Check for updates 3 sec after the form loads, and then again every 8 hours
            this.Text = "Family Tree Analyzer v" + VERSION;
        }

        #region ToolStrip Clicks
        private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _checkForUpdatesEnabled = true;
            _timerCheckForUpdates_Callback(null);
        }

        private void showLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Forms.Locations locationsForm = new Forms.Locations();
            List<FactLocation> locations = ft.AllLocations;
            locations.Sort();
            locationsForm.BuildLocationTree(locations);
            locationsForm.Show();
        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserControls.Options options = new UserControls.Options();
            options.ShowDialog(this);
            options.Dispose();
        }

        private void BirthRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MultiComparator<Registration> birthComparator = new MultiComparator<Registration>();
            birthComparator.addComparator(new LocationComparator(FactLocation.PARISH));
            birthComparator.addComparator(new DateComparator());

            Filter<Registration> partialEnglishData =
                new AndFilter<Registration>(new IncompleteDataFilter<Registration>(FactLocation.PARISH), LocationFilter<Registration>.ENGLAND);
            Filter<Registration> directOrBlood = new OrFilter<Registration>(
                    new RelationFilter<Registration>(Individual.DIRECT),
                    new RelationFilter<Registration>(Individual.BLOOD));

            RegistrationsProcessor onlineBirthsRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(directOrBlood, partialEnglishData), birthComparator);

            List<Registration> regs = ft.getAllBirthRegistrations();
            List<Registration> result = onlineBirthsRP.processRegistrations(regs);

            RegistrationReport report = new RegistrationReport();
            report.SetupBirthRegistration(result);
            report.Show();
        }

        private void deathRegistrationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MultiComparator<Registration> deathComparator = new MultiComparator<Registration>();
            deathComparator.addComparator(new LocationComparator(FactLocation.PARISH));
            deathComparator.addComparator(new DateComparator());

            Filter<Registration> partialEnglishData =
                new AndFilter<Registration>(new IncompleteDataFilter<Registration>(FactLocation.PARISH), LocationFilter<Registration>.ENGLAND);
            Filter<Registration> directOrBlood = new OrFilter<Registration>(
                    new RelationFilter<Registration>(Individual.DIRECT),
                    new RelationFilter<Registration>(Individual.BLOOD));

            RegistrationsProcessor onlineDeathsRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(directOrBlood, partialEnglishData), deathComparator);

            List<Registration> regs = ft.getAllDeathRegistrations();
            List<Registration> result = onlineDeathsRP.processRegistrations(regs);

            RegistrationReport report = new RegistrationReport();
            report.SetupDeathRegistration(result);
            report.Show();
        }

        private void marriageRegistrationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MultiComparator<Registration> marriageComparator = new MultiComparator<Registration>();
            marriageComparator.addComparator(new LocationComparator(FactLocation.PARISH));
            marriageComparator.addComparator(new DateComparator());

            Filter<Registration> partialEnglishData =
                new AndFilter<Registration>(new IncompleteDataFilter<Registration>(FactLocation.PARISH), LocationFilter<Registration>.ENGLAND);
            Filter<Registration> directOrBlood = new OrFilter<Registration>(
                    new RelationFilter<Registration>(Individual.DIRECT),
                    new RelationFilter<Registration>(Individual.BLOOD));

            RegistrationsProcessor onlineMarriagesRP = new RegistrationsProcessor(
                    new AndFilter<Registration>(directOrBlood, partialEnglishData), marriageComparator);

            List<Registration> regs = ft.getAllMarriageRegistrations();
            List<Registration> result = onlineMarriagesRP.processRegistrations(regs);

            RegistrationReport report = new RegistrationReport();
            report.SetupMarriageRegistration(result);
            report.Show();
        }

        #endregion 

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lostcousins.com/?ref=LC585149");
        }

        private void btnIGIFolderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.ShowNewFolderButton = true;
            browse.Description = "Please select a folder where the results of the IGI search will be placed";
            browse.RootFolder = Environment.SpecialFolder.Desktop;
            if (txtIGIfolder.Text != string.Empty)
                browse.SelectedPath = txtIGIfolder.Text;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Application.UserAppDataRegistry.SetValue("IGI Search Path", browse.SelectedPath);
                txtIGIfolder.Text = browse.SelectedPath;
            }
        }

        private void btnIGIMarriageSearch_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            btnCancelIGISearch.Visible = true;
            btnViewResults.Visible = false;
            btnIGIChildrenSearch.Enabled = false;
            btnIGIMarriageSearch.Enabled = false;
            rtbIGIResults.Text = "IGI Marriage Search started.\n";
            int level = rbIGISearchCountry.Checked ? FactLocation.COUNTRY : FactLocation.REGION;
            IGISearchForm form = new IGINewSearchForm(rtbIGIResults, IGIDefaultCountry.Country, level, IGIrelationTypes.Status, txtIGISurname.Text, webBrowser);
            List<Family> families = ft.AllFamilies;
            int counter = 0;
            pbIGISearch.Visible = true;
            pbIGISearch.Maximum = families.Count;
            pbIGISearch.Value = 0;
            stopProcessing = false;
            foreach (Family f in families)
            {
                form.SearchIGI(f, txtIGIfolder.Text, IGISearchForm.MARRIAGESEARCH);
                pbIGISearch.Value = counter++;
                Application.DoEvents();
                if (stopProcessing)
                    break;
            }
            pbIGISearch.Visible = false;
            btnCancelIGISearch.Visible = false;
            btnViewResults.Visible = true;
            btnIGIChildrenSearch.Enabled = true;
            btnIGIMarriageSearch.Enabled = true;
            rtbIGIResults.AppendText("\nIGI Marriage Search finished.\n");
            HourGlass(false);
        }

        private void btnIGIChildrenSearch_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            btnCancelIGISearch.Visible = true;
            btnViewResults.Visible = false;
            btnIGIChildrenSearch.Enabled = false;
            btnIGIMarriageSearch.Enabled = false;
            rtbIGIResults.Text = "IGI Children Search started.\n";
            int level = rbIGISearchCountry.Checked ? FactLocation.COUNTRY : FactLocation.REGION;
            IGISearchForm form = new IGIOldSearchForm(rtbIGIResults, IGIDefaultCountry.Country, level, IGIrelationTypes.Status, txtIGISurname.Text);
            List<Family> families = ft.AllFamilies;
            int counter = 0;
            pbIGISearch.Visible = true;
            pbIGISearch.Maximum = families.Count;
            pbIGISearch.Value = 0;
            stopProcessing = false;
            foreach (Family f in families)
            {
                pbIGISearch.Value = counter++;
                form.SearchIGI(f, txtIGIfolder.Text, IGISearchForm.CHILDRENSEARCH);
                Application.DoEvents();
                if (stopProcessing)
                    break;
            }
            pbIGISearch.Visible = false;
            btnCancelIGISearch.Visible = false;
            btnViewResults.Visible = true;
            btnIGIChildrenSearch.Enabled = true;
            btnIGIMarriageSearch.Enabled = true;
            rtbIGIResults.AppendText("\nIGI Children Search finished.\n");
            HourGlass(false);
        }

        private void censusCountry_CountryChanged(object sender, EventArgs e)
        {
            cenDate.Country = censusCountry.Country;
        }

        private void cenDate_CensusChanged(object sender, EventArgs e)
        {
            censusDate = cenDate.SelectedDate;
        }

        private void btnViewResults_Click(object sender, EventArgs e)
        {
            Forms.IGISearchResultsViewer frmResults = new Forms.IGISearchResultsViewer(txtIGIfolder.Text);
            if (frmResults.ResultsPresent)
                frmResults.Show();
            else
                MessageBox.Show("Sorry there are no results files in the selected folder.");
        }

        private void btnCancelIGISearch_Click(object sender, EventArgs e)
        {
            stopProcessing = true;
        }

        private void IGIDefaultCountry_CountryChanged(object sender, EventArgs e)
        {
            if (IGIDefaultCountry.Country == FactLocation.SCOTLAND)
                rbIGISearchCountry.Checked = true;
            else
                rbIGISearchRegion.Checked = true;
        }

        private void rtbIGIResults_TextChanged(object sender, EventArgs e)
        {
            rtbIGIResults.ScrollToBottom();
        }

        private void rtbOutput_TextChanged(object sender, EventArgs e)
        {
            rtbOutput.ScrollToBottom();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopProcessing = true;
        }

        private void btnOpenFolder_Click(object sender, EventArgs e)
        {
            string windir = Environment.GetEnvironmentVariable("WINDIR");
            System.Diagnostics.Process prc = new System.Diagnostics.Process();
            prc.StartInfo.FileName = windir + @"\explorer.exe";
            prc.StartInfo.Arguments = txtIGIfolder.Text;
            prc.Start();
        }

        private void btnTreeTops_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Filter<Individual> filter = createTreeTopsIndividualFilter();
            List<IDisplayTreeTops> treeTopsList = ft.GetTreeTops(filter);
            treeTopsList.Sort(new BirthDateComparer());
            dgTreeTops.DataSource = treeTopsList;
            foreach (DataGridViewColumn c in dgTreeTops.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = "Count : " + treeTopsList.Count;
            HourGlass(false);
        }

        private void btnWWI_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Filter<Individual> filter = createWardeadIndividualFilter(new FactDate("BET 1869 AND 1904"), new FactDate("BET 1914 AND 1918"));
            List<IDisplayTreeTops> warDeadList = ft.GetWarDead(filter);
            warDeadList.Sort(new BirthDateComparer(BirthDateComparer.ASCENDING));
            dgWarDead.DataSource = warDeadList;
            foreach (DataGridViewColumn c in dgWarDead.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = "Count : " + warDeadList.Count;
            HourGlass(false);
        }

        private void btnWWII_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Filter<Individual> filter = createWardeadIndividualFilter(new FactDate("BET 1894 AND 1931"), new FactDate("BET 1939 AND 1945"));
            List<IDisplayTreeTops> warDeadList = ft.GetWarDead(filter);
            warDeadList.Sort(new BirthDateComparer(BirthDateComparer.ASCENDING));
            dgWarDead.DataSource = warDeadList;
            foreach (DataGridViewColumn c in dgWarDead.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = "Count : " + warDeadList.Count;
            HourGlass(false);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forums.lc");
        }
    }
}
