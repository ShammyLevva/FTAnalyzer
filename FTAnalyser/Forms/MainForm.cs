using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using Printing.DataGridViewPrint.Tools;
using FTAnalyzer.Utilities;
using FTAnalyzer.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace FTAnalyzer
{
    public partial class MainForm : Form
    {
        private string VERSION = "1.5.7.5";
        private bool _checkForUpdatesEnabled = true;
        private bool _showNoUpdateMessage = false;
        private System.Threading.Timer _timerCheckForUpdates;

        private Cursor storedCursor = Cursors.Default;
        private FamilyTree ft = FamilyTree.Instance;
        private FactDate censusDate = CensusDate.UKCENSUS1881;
        private bool stopProcessing = false;

        public MainForm()
        {
            InitializeComponent();
            ft.XmlErrorBox = rtbOutput;
            tabSelector.TabPages.RemoveByKey("tabFamilySearch");
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
                    rtbFamilySearchResults.Text = "";
                    pbSources.Value = pbIndividuals.Value = pbFamilies.Value = 0;
                    Application.DoEvents();
                    if (!stopProcessing)
                    {
                        XmlDocument document = GedcomToXml.Load(openGedcom.FileName);
                        document.Save("GedcomOutput.xml");
                        ft.LoadTree(document, pbSources, pbIndividuals, pbFamilies);
                        ft.SetDataErrorsCheckedDefaults(ckbDataErrors);
                        Application.UseWaitCursor = false;
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
                finally
                {
                    HourGlass(false);
                }
            }
        }

        private void closeIndividualForms()
        {
            List<Form> toClose = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (f is Forms.People || f is Forms.LCReport)
                    toClose.Add(f);
            }
            foreach (Form f in toClose)
                f.Close();
        }

        private void HourGlass(bool on)
        {
            if (on)
            {
                this.Cursor = Cursors.WaitCursor;
            }
            else
            {
                this.Cursor = Cursors.Default;
            }
            Application.DoEvents();
        }

        private void tabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            printToolStripMenuItem.Enabled = false;
            if (ft.Loading)
            {
                tabSelector.SelectedTab = tabDisplayProgress;
                tsCountLabel.Text = "";
            }
            else
            {
                if (!ft.DataLoaded)
                {   // do not process anything if no GEDCOM yet loaded
                    if (tabSelector.SelectedTab != tabDisplayProgress)
                    {
                        tabSelector.SelectedTab = tabDisplayProgress;
                        tsCountLabel.Text = "";
                        MessageBox.Show("You need to load your GEDCOM file before the program can display results.\nClick File | Open.");
                    }
                    return;
                }
                HourGlass(true);
                if (tabSelector.SelectedTab == tabDisplayProgress)
                {
                    tsCountLabel.Text = "";
                }
                else if (tabSelector.SelectedTab == tabIndividuals)
                {
                    SortableBindingList<IDisplayIndividual> list = ft.AllDisplayIndividuals;
                    dgIndividuals.DataSource = list;
                    dgIndividuals.Sort(dgIndividuals.Columns["IndividualID"], ListSortDirection.Ascending);
                    tsCountLabel.Text = "Count : " + list.Count;
                }
                else if (tabSelector.SelectedTab == tabFamilies)
                {
                    SortableBindingList<IDisplayFamily> list = ft.AllDisplayFamilies;
                    dgFamilies.DataSource = list;
                    dgFamilies.Sort(dgFamilies.Columns["FamilyGed"], ListSortDirection.Ascending);
                    tsCountLabel.Text = "Count : " + list.Count;
                }
                else if (tabSelector.SelectedTab == tabOccupations)
                {
                    SortableBindingList<IDisplayOccupation> list = ft.AllDisplayOccupations;
                    dgOccupations.DataSource = list;
                    dgOccupations.Sort(dgOccupations.Columns["Occupation"], ListSortDirection.Ascending);
                    tsCountLabel.Text = "Count : " + list.Count;
                }
                else if (tabSelector.SelectedTab == tabCensus)
                {
                    cenDate.RevertToDefaultDate();
                    tsCountLabel.Text = "";
                    btnShowResults.Enabled = ft.IndividualCount > 0;
                    if (ckbNoLocations.Checked)
                        censusCountry.Enabled = false;
                    else
                        censusCountry.Enabled = true;
                }
                else if (tabSelector.SelectedTab == tabLostCousins)
                {
                    tsCountLabel.Text = "";
                    btnLC1881EW.Enabled = btnLC1881Scot.Enabled = btnLC1841EW.Enabled =
                        btnLC1881Canada.Enabled = btnLC1880USA.Enabled = btnLC1911Ireland.Enabled =
                        btnLC1911EW.Enabled = ft.IndividualCount > 0;
                }
                else if (tabSelector.SelectedTab == tabDataErrors)
                {
                    List<DataError> errors =  ft.DataErrors(ckbDataErrors);
                    dgDataErrors.DataSource = errors;
                    tsCountLabel.Text = "Count : " + errors.Count;
                }
                else if (tabSelector.SelectedTab == tabLooseDeaths)
                {
                    SortableBindingList<IDisplayLooseDeath> looseDeathList = ft.GetLooseDeaths();
                    dgLooseDeaths.DataSource = looseDeathList;
                    tsCountLabel.Text = "Count : " + looseDeathList.Count;
                }
                else if (tabSelector.SelectedTab == tabLocations)
                {
                    tsCountLabel.Text = "";
                    List<IDisplayLocation> countries = ft.AllCountries;
                    List<IDisplayLocation> regions = ft.AllRegions;
                    List<IDisplayLocation> parishes = ft.AllParishes;
                    List<IDisplayLocation> addresses = ft.AllAddresses;
                    List<IDisplayLocation> places = ft.AllPlaces;
                    countries.Sort(new FactLocationComparer(FactLocation.COUNTRY));
                    regions.Sort(new FactLocationComparer(FactLocation.REGION));
                    parishes.Sort(new FactLocationComparer(FactLocation.PARISH));
                    addresses.Sort(new FactLocationComparer(FactLocation.ADDRESS));
                    places.Sort(new FactLocationComparer(FactLocation.PLACE));
                    dgCountries.DataSource = countries;
                    dgRegions.DataSource = regions;
                    dgParishes.DataSource = parishes;
                    dgAddresses.DataSource = addresses;
                    dgPlaces.DataSource = places;
                }
                else if (tabSelector.SelectedTab == tabFamilySearch)
                {
                    btnCancelFamilySearch.Visible = false;
                    btnViewResults.Visible = true;
                    tsCountLabel.Text = "";
                    btnFamilySearchChildrenSearch.Enabled = btnFamilySearchMarriageSearch.Enabled = ft.IndividualCount > 0;
                    try
                    {
                        txtFamilySearchfolder.Text = (string)Application.UserAppDataRegistry.GetValue("FamilySearch Search Path");
                    }
                    catch (Exception)
                    {
                        txtFamilySearchfolder.Text = string.Empty;
                    }
                }
                HourGlass(false);
            }
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
            Forms.People frmInd = new Forms.People();
            frmInd.setLocation(loc, FactLocation.REGION);
            frmInd.Show();
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

        private void dgPlaces_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgPlaces.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.setLocation(loc, FactLocation.PLACE);
            frmInd.Show();
            HourGlass(false);
        }

        private void btnShowResults_Click(object sender, EventArgs e)
        {
            Census census;
            Filter<Registration> filter = createCensusRegistrationFilter();
            MultiComparator<Registration> censusComparator = new MultiComparator<Registration>();
            if (!ckbNoLocations.Checked) // only compare on location if no locations isn't checked
                censusComparator.addComparator(new LocationComparator(FactLocation.PARISH));
            censusComparator.addComparator(new DateComparator());
            RegistrationsProcessor censusRP = new RegistrationsProcessor(filter, censusComparator);
            
            if (ckbNoLocations.Checked)
                census = new Census();
            else
                census = new Census(censusCountry.GetLocation);

            census.setupCensus(censusRP, censusDate, false, ckbCensusResidence.Checked, false, (int)udAgeFilter.Value);
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
                                            new AndFilter<Individual>(birthFilter, deathFilter),
                                            new AndFilter<Individual>(locationFilter, relationFilter));

            if (tabSelector.Text.Length > 0)
                filter = new AndFilter<Individual>(filter, new SurnameFilter<Individual>(tabSelector.Text.ToUpper()));

            return filter;
        }
        #endregion

        #region Lost Cousins
        private void LostCousinsCensus(string location, Filter<Registration> filter, FactDate censusDate, string reportTitle)
        {
            HourGlass(true);
            Census census;
            Filter<Registration> relation =
                new OrFilter<Registration>(
                    new OrFilter<Registration>(new RelationFilter<Registration>(Individual.BLOOD), new RelationFilter<Registration>(Individual.DIRECT)),
                    new RelationFilter<Registration>(Individual.MARRIEDTODB));
            MultiComparator<Registration> censusComparator = new MultiComparator<Registration>();
            if (ckbLCIgnoreCountry.Checked) // only add the parish location comparator if we are using locations
            {
                filter = new TrueFilter<Registration>(); // if we are ignoring locations then ignore what was passed as a filter
                census = new Census();
            }
            else
            {
                if(location == FactLocation.ENG_WALES)
                    census = new Census(new FactLocation(FactLocation.ENGLAND), new FactLocation(FactLocation.WALES));
                else
                    census = new Census(new FactLocation(location));
                censusComparator.addComparator(new LocationComparator(FactLocation.COUNTRY));
            }
            if (ckbLCIgnoreCountry.Checked)
            if (ckbRestrictions.Checked)
                filter = new AndFilter<Registration>(new DateFilter<Registration>(censusDate), filter, relation);
            else
                filter = new AndFilter<Registration>(new DateFilter<Registration>(censusDate), filter);
            censusComparator.addComparator(new DateComparator());
            RegistrationsProcessor censusRP = new RegistrationsProcessor(filter, censusComparator);

            census.setupCensus(censusRP, censusDate, true, ckbLCResidence.Checked, ckbHideRecorded.Checked, 110);
            census.Text = reportTitle;
            HourGlass(false);
            census.Show();
        }

        private void btnLC1881EW_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.ENGLAND, LocationFilter<Registration>.WALES);
            string reportTitle = "1881 England & Wales Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(FactLocation.ENG_WALES, filter, CensusDate.UKCENSUS1881, reportTitle);
        }

        private void btnLC1881Scot_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = LocationFilter<Registration>.SCOTLAND;
            string reportTitle = "1881 Scotland Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(FactLocation.SCOTLAND, filter, CensusDate.UKCENSUS1881, reportTitle);
        }

        private void btnLC1881Canada_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = LocationFilter<Registration>.CANADA;
            string reportTitle = "1881 Canada Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(FactLocation.CANADA, filter, CensusDate.CANADACENSUS1881, reportTitle);
        }

        private void btnLC1841EW_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.ENGLAND, LocationFilter<Registration>.WALES);
            string reportTitle = "1841 England & Wales Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(FactLocation.ENG_WALES, filter, CensusDate.UKCENSUS1841, reportTitle);
        }


        private void btnLC1911EW_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.ENGLAND, LocationFilter<Registration>.WALES);
            string reportTitle = "1911 England & Wales Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(FactLocation.ENG_WALES, filter, CensusDate.UKCENSUS1911, reportTitle);
        }

        private void btnLC1880USA_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.USA, LocationFilter<Registration>.US);
            string reportTitle = "1880 US Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(FactLocation.UNITED_STATES, filter, CensusDate.USCENSUS1880, reportTitle);
        }

        private void btnLC1911Ireland_Click(object sender, EventArgs e)
        {
            Filter<Registration> filter = new OrFilter<Registration>(LocationFilter<Registration>.EIRE, LocationFilter<Registration>.IRELAND);
            string reportTitle = "1911 Ireland Census Records on file to enter to Lost Cousins";
            LostCousinsCensus(FactLocation.IRELAND, filter, new FactDate("1911"), reportTitle);
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
                            Process.Start("http://FTAnalyzer.codeplex.com/");
                    }
                    else if (_showNoUpdateMessage)
                    {
                        MessageBox.Show("You are running the latest version of FTAnalyzer");
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
                            Process.Start("http://FTAnalyzer.codeplex.com/");
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
            _showNoUpdateMessage = true;
            _timerCheckForUpdates_Callback(null);
            _showNoUpdateMessage = false;
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

        private void btnFamilySearchFolderBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog browse = new FolderBrowserDialog();
            browse.ShowNewFolderButton = true;
            browse.Description = "Please select a folder where the results of the FamilySearch search will be placed";
            browse.RootFolder = Environment.SpecialFolder.Desktop;
            if (txtFamilySearchfolder.Text != string.Empty)
                browse.SelectedPath = txtFamilySearchfolder.Text;
            if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Application.UserAppDataRegistry.SetValue("FamilySearch Search Path", browse.SelectedPath);
                txtFamilySearchfolder.Text = browse.SelectedPath;
            }
        }

        private void btnFamilySearchMarriageSearch_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            btnCancelFamilySearch.Visible = true;
            btnViewResults.Visible = false;
            btnFamilySearchChildrenSearch.Enabled = false;
            btnFamilySearchMarriageSearch.Enabled = false;
            rtbFamilySearchResults.Text = "FamilySearch Marriage Search started.\n";
            int level = rbFamilySearchCountry.Checked ? FactLocation.COUNTRY : FactLocation.REGION;
            FamilySearchForm form = new FamilySearchNewSearchForm(rtbFamilySearchResults, FamilySearchDefaultCountry.Country, level, FamilySearchrelationTypes.Status, txtFamilySearchSurname.Text, webBrowser);
            List<Family> families = ft.AllFamilies;
            int counter = 0;
            pbFamilySearch.Visible = true;
            pbFamilySearch.Maximum = families.Count;
            pbFamilySearch.Value = 0;
            stopProcessing = false;
            foreach (Family f in families)
            {
                form.SearchFamilySearch(f, txtFamilySearchfolder.Text, FamilySearchForm.MARRIAGESEARCH);
                pbFamilySearch.Value = counter++;
                Application.DoEvents();
                if (stopProcessing)
                    break;
            }
            pbFamilySearch.Visible = false;
            btnCancelFamilySearch.Visible = false;
            btnViewResults.Visible = true;
            btnFamilySearchChildrenSearch.Enabled = true;
            btnFamilySearchMarriageSearch.Enabled = true;
            rtbFamilySearchResults.AppendText("\nFamilySearch Marriage Search finished.\n");
            HourGlass(false);
        }

        private void btnFamilySearchChildrenSearch_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            btnCancelFamilySearch.Visible = true;
            btnViewResults.Visible = false;
            btnFamilySearchChildrenSearch.Enabled = false;
            btnFamilySearchMarriageSearch.Enabled = false;
            rtbFamilySearchResults.Text = "FamilySearch Children Search started.\n";
            int level = rbFamilySearchCountry.Checked ? FactLocation.COUNTRY : FactLocation.REGION;
            FamilySearchForm form = new FamilySearchOldSearchForm(rtbFamilySearchResults, FamilySearchDefaultCountry.Country, level, FamilySearchrelationTypes.Status, txtFamilySearchSurname.Text);
            List<Family> families = ft.AllFamilies;
            int counter = 0;
            pbFamilySearch.Visible = true;
            pbFamilySearch.Maximum = families.Count;
            pbFamilySearch.Value = 0;
            stopProcessing = false;
            foreach (Family f in families)
            {
                pbFamilySearch.Value = counter++;
                form.SearchFamilySearch(f, txtFamilySearchfolder.Text, FamilySearchForm.CHILDRENSEARCH);
                Application.DoEvents();
                if (stopProcessing)
                    break;
            }
            pbFamilySearch.Visible = false;
            btnCancelFamilySearch.Visible = false;
            btnViewResults.Visible = true;
            btnFamilySearchChildrenSearch.Enabled = true;
            btnFamilySearchMarriageSearch.Enabled = true;
            rtbFamilySearchResults.AppendText("\nFamilySearch Children Search finished.\n");
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
            Forms.FamilySearchResultsViewer frmResults = new Forms.FamilySearchResultsViewer(txtFamilySearchfolder.Text);
            if (frmResults.ResultsPresent)
                frmResults.Show();
            else
                MessageBox.Show("Sorry there are no results files in the selected folder.");
        }

        private void btnCancelFamilySearch_Click(object sender, EventArgs e)
        {
            stopProcessing = true;
        }

        private void FamilySearchDefaultCountry_CountryChanged(object sender, EventArgs e)
        {
            if (FamilySearchDefaultCountry.Country == FactLocation.SCOTLAND)
                rbFamilySearchCountry.Checked = true;
            else
                rbFamilySearchRegion.Checked = true;
        }

        private void rtbFamilySearchResults_TextChanged(object sender, EventArgs e)
        {
            rtbFamilySearchResults.ScrollToBottom();
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
            prc.StartInfo.Arguments = txtFamilySearchfolder.Text;
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
            printToolStripMenuItem.Enabled = true;
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
            printToolStripMenuItem.Enabled = true;
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
            printToolStripMenuItem.Enabled = true;
            HourGlass(false);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forums.lc");
        }

        private void ckbNoLocations_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbNoLocations.Checked)
                censusCountry.Enabled = false;
            else
                censusCountry.Enabled = true;
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            printDocument.DefaultPageSettings.Margins =
               new System.Drawing.Printing.Margins(40, 40, 40, 40);
            printDocument.DefaultPageSettings.Landscape = true;

            if (tabSelector.SelectedTab == tabTreetops)
            {
                PrintingDataGridViewProvider printProvider = PrintingDataGridViewProvider.Create(
                    printDocument, dgTreeTops, true, true, true,
                    new TitlePrintBlock(this.Text), null, null);
                if (printDialog.ShowDialog(this) == DialogResult.OK)
                {
                    printDocument.PrinterSettings = printDialog.PrinterSettings;
                    printDocument.Print();
                }
            }
            else if (tabSelector.SelectedTab == tabWarDead)
            {
                PrintingDataGridViewProvider printProvider = PrintingDataGridViewProvider.Create(
                    printDocument, dgWarDead, true, true, true,
                    new TitlePrintBlock(this.Text), null, null);
                if (printDialog.ShowDialog(this) == DialogResult.OK)
                {
                    printDocument.PrinterSettings = printDialog.PrinterSettings;
                    printDocument.Print();
                }
            }
        }

        private void ckbLCIgnoreCountry_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbLCIgnoreCountry.Checked)
            {
                btnLC1841EW.Text = "1841 Census";
                btnLC1880USA.Text = "1880 Census";
                btnLC1881EW.Text = "1881 Census";
                btnLC1911EW.Text = "1911 Census";
                btnLC1881Scot.Visible = false;
                btnLC1911Ireland.Visible = false;
                btnLC1881Canada.Visible = false;
            }
            else
            {
                btnLC1880USA.Text = "1880 US Census";
                btnLC1841EW.Text = "1841 England && Wales Census";
                btnLC1881EW.Text = "1881 England && Wales Census";
                btnLC1911EW.Text = "1911 England && Wales Census";
                btnLC1881Scot.Visible = true;
                btnLC1911Ireland.Visible = true;
                btnLC1881Canada.Visible = true;
            }
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            rtbOutput.Top = pbFamilies.Top + 30;
        }

        private void dgOccupations_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            DisplayOccupation occ = (DisplayOccupation)dgOccupations.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.setWorkers(occ.Occupation, ft.AllWorkers(occ.Occupation));
            frmInd.Show();
            HourGlass(false);
        }

        private void setAsRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Individual ind = (Individual)dgIndividuals.CurrentRow.DataBoundItem;
            if (ind != null)
            {
                ft.SetRelations(ind.GedcomID);
                dgIndividuals.Refresh();
                MessageBox.Show("Root person set as " + ind.Name + "\n\n" + ft.PrintRelationCount());
            }
            HourGlass(false);
        }

        private void btnShowMap_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FactLocation loc = null;
            int locType = getMapLocationType(out loc);
            if (loc != null)
            {   // Do geo coding stuff
                GoogleMap frmGoogleMap = new GoogleMap();
                if (frmGoogleMap.setLocation(loc, locType))
                    frmGoogleMap.Show();
                else
                    MessageBox.Show("Unable to find location : " + loc.getLocation(locType));
            }
            this.Cursor = Cursors.Default;
        }

        private void btnBingOSMap_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            FactLocation loc = null;
            int locType = getMapLocationType(out loc);
            if (loc != null)
            {   // Do geo coding stuff
                BingOSMap frmBingMap = new BingOSMap();
                if (frmBingMap.setLocation(loc, locType))
                    frmBingMap.Show();
                else
                    MessageBox.Show("Unable to find location : " + loc.getLocation(locType));
            }
            this.Cursor = Cursors.Default;
        }

        private int getMapLocationType(out FactLocation loc)
        {
            // get the tab
            int locType = FactLocation.COUNTRY;
            loc = null;
            switch (tabCtrlLocations.SelectedTab.Text)
            {
                case "Countries":
                    loc = dgCountries.CurrentRow == null ? null : (FactLocation)dgCountries.CurrentRow.DataBoundItem;
                    locType = FactLocation.COUNTRY;
                    break;
                case "Regions":
                    loc = dgRegions.CurrentRow == null ? null : (FactLocation)dgRegions.CurrentRow.DataBoundItem;
                    locType = FactLocation.REGION;
                    break;
                case "Parishes":
                    loc = dgParishes.CurrentRow == null ? null : (FactLocation)dgParishes.CurrentRow.DataBoundItem;
                    locType = FactLocation.PARISH;
                    break;
                case "Addresses":
                    loc = dgAddresses.CurrentRow == null ? null : (FactLocation)dgAddresses.CurrentRow.DataBoundItem;
                    locType = FactLocation.ADDRESS;
                    break;
            }
            if (loc == null)
                MessageBox.Show("Please select a location to show on the map.");
            return locType;
        }

        private void geocodeLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            try
            {
                SQLiteConnection conn = new SQLiteConnection("Data Source=Geocodes.s3db;Version=3;");
                conn.Open();
                List<FactLocation> locations = ft.AllLocations;
                SQLiteCommand cmd = new SQLiteCommand(conn);
                int count = 0;
                int good = 0;
                int bad = 0;
                foreach (FactLocation loc in locations)
                {
                    string sql = string.Format("select location from geocode where location = \"{0}\"", loc.ToString());
                    cmd.CommandText = sql;
                    SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    if (!reader.Read())
                    {  // location isn't found so add it
                        GoogleMap.GeoResponse res = GoogleMap.CallGeoWSCount(loc.ToString(), 10);
                        if (res.Status == "OK" && res.Results.Length > 0)
                        {
                            int foundLevel = GoogleMap.GetFactLocation(res.Results[0].Types);
                            if (foundLevel >= loc.Level)
                            {
                                sql = string.Format("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel)" +
                                        "values (\"{0}\",{1},{2},{3},date('now'),\"{4}\",{5})", loc.ToString(), loc.Level,
                                        res.Results[0].Geometry.Location.Lat, res.Results[0].Geometry.Location.Lng,
                                        res.Results[0].ReturnAddress, foundLevel);
                                good++;
                            }
                            else
                            {
                                sql = string.Format("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel)" +
                                        "values (\"{0}\",{1},{2},{3},date('now'),\"{4}\",{5})", loc.ToString(), loc.Level, 0, 0, "", foundLevel);
                                bad++;
                            }
                            cmd = new SQLiteCommand(sql, conn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                    count++;
                    Console.WriteLine("Found " + good + " records and failed to find " + bad + " records from " + count + " of " + locations.Count);
                }
                conn.Close();
                MessageBox.Show("Finished Geocoding");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error geocoding : " + ex.Message);
            }
            HourGlass(false);
        }

        private void ckbDataErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            HourGlass(true);
            List<DataError> errors = ft.DataErrors(ckbDataErrors);
            dgDataErrors.DataSource = errors;
            tsCountLabel.Text = "Count : " + errors.Count;
            HourGlass(false);
        }

        private void btnLCReport_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            SortableBindingList<IDisplayLCReport> list = ft.LCReport(ckbRestrictions.Checked);
            LCReport rs = new LCReport(list);
            rs.Show();
            HourGlass(false);
        }

        private void btnLCReport2_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            SortableBindingList<IDisplayLCReport> list = ft.LCReport(ckbRestrictions.Checked);
            LCReport rs = new LCReport(list);
            rs.Show();
            HourGlass(false);
        }

        private void childAgeProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ft.DataLoaded)
            {
                MessageBox.Show("You must load a GEDCOM file before you can see any statistics");
            }
            else
            {
                Statistics s = Statistics.Instance;
                MessageBox.Show(s.ChildrenBirthProfiles());
            }
        }

        private void viewOnlineManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://ftanalyzer.codeplex.com/documentation");
        }

        private void olderParentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!ft.DataLoaded)
            {
                MessageBox.Show("You must load a GEDCOM file before you can see any statistics");
            }
            else
            {
                HourGlass(true);
                Forms.People frmInd = new Forms.People();
                frmInd.OlderParents();
                frmInd.Show();
                HourGlass(false);
            }
        }
    }
}