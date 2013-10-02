using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using FTAnalyzer.Filters;
using FTAnalyzer.Forms;
using FTAnalyzer.Utilities;
using FTAnalyzer.UserControls;
using Printing.DataGridViewPrint.Tools;
using System.Drawing;

namespace FTAnalyzer
{
    public partial class MainForm : Form
    {
        private string VERSION = "2.3.0.0-beta-test2";
        //private bool _checkForUpdatesEnabled = false;
        //private bool _showNoUpdateMessage = false;
        //private System.Threading.Timer _timerCheckForUpdates;

        private Cursor storedCursor = Cursors.Default;
        private FamilyTree ft = FamilyTree.Instance;
        private FactDate censusDate = CensusDate.UKCENSUS1881;
        private bool stopProcessing = false;
        private string filename;

        private DataGridViewCellStyle knownCountryStyle = null;

        public MainForm()
        {
            InitializeComponent();
            displayOptionsOnLoadToolStripMenuItem.Checked = Properties.GeneralSettings.Default.ReportOptions;
            ft.XmlErrorBox = rtbOutput;
            VERSION = PublishVersion();
            treetopsRelation.MarriedToDB = false;
            ShowMenus(false);
            SetSavePath();
            int pos =VERSION.IndexOf('-');
            string ver = pos > 0 ? VERSION.Substring(0, VERSION.IndexOf('-')) : VERSION;
            ft.CheckDatabaseVersion(new Version(ver));
        }

        private string PublishVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                return string.Format("{0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);
            }
            else
                return VERSION;
        }

        private void SetSavePath()
        {
            try
            {
                Properties.GeneralSettings.Default.SavePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Family Tree Analyzer");
                if (!Directory.Exists(Properties.GeneralSettings.Default.SavePath))
                    Directory.CreateDirectory(Properties.GeneralSettings.Default.SavePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Found a problem starting up.\nPlease report this at http://ftanalyzer.codeplex.com\nThe error was :" + ex.Message);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.LoadLocation))
                openGedcom.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            else
                openGedcom.InitialDirectory = Properties.Settings.Default.LoadLocation;
            openGedcom.FileName = "*.ged";
            openGedcom.Filter = "GED files (*.ged)|*.ged|All files (*.*)|*.*";
            openGedcom.FilterIndex = 1;
            openGedcom.RestoreDirectory = true;

            if (openGedcom.ShowDialog() == DialogResult.OK)
            {
                LoadFile(openGedcom.FileName);
                Properties.Settings.Default.LoadLocation = Path.GetFullPath(openGedcom.FileName);
                Properties.Settings.Default.Save();
            }
        }

        private void LoadFile(string filename)
        {
            try
            {
                HourGlass(true);
                this.filename = filename;
                DisposeIndividualForms();
                ShowMenus(false);
                tabSelector.SelectTab(tabDisplayProgress);
                rtbOutput.Text = "";
                pbSources.Value = pbIndividuals.Value = pbFamilies.Value = 0;
                dgCountries.DataSource = null;
                dgRegions.DataSource = null;
                dgSubRegions.DataSource = null;
                dgAddresses.DataSource = null;
                dgPlaces.DataSource = null;
                dgIndividuals.DataSource = null;
                dgFamilies.DataSource = null;
                dgTreeTops.DataSource = null;
                dgWarDead.DataSource = null;
                dgLooseDeaths.DataSource = null;
                dgDataErrors.DataSource = null;
                dgOccupations.DataSource = null;
                treeViewLocations.Nodes.Clear();
                Application.DoEvents();
                if (!stopProcessing)
                {
                    //document.Save("GedcomOutput.xml");
                    if (ft.LoadTree(filename, pbSources, pbIndividuals, pbFamilies))
                    {
                        ft.SetDataErrorsCheckedDefaults(ckbDataErrors);
                        Application.UseWaitCursor = false;
                        ShowMenus(true);
                        HourGlass(false);
                        MessageBox.Show("Gedcom File " + filename + " Loaded");
                    }
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

        private void ShowMenus(bool enabled)
        {
            mnuPrint.Enabled = enabled;
            mnuReload.Enabled = enabled;
            mnuFactsToExcel.Enabled = enabled;
            mnuIndividualsToExcel.Enabled = enabled;
            mnuFamiliesToExcel.Enabled = enabled;
            mnuChildAgeProfiles.Enabled = enabled;
            mnuOlderParents.Enabled = enabled;
            mnuShowTimeline.Enabled = enabled;
            mnuGeocodeLocations.Enabled = enabled;
        }

        private void DisposeIndividualForms()
        {
            List<Form> toDispose = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (!object.ReferenceEquals(f, this))
                    toDispose.Add(f);
            }
            foreach (Form f in toDispose)
                f.Dispose();
        }

        private void DisposeDuplicateForms(object form)
        {
            List<Form> toDispose = new List<Form>();
            foreach (Form f in Application.OpenForms)
            {
                if (!object.ReferenceEquals(f, form) && f.GetType() == form.GetType())
                    if (form is Census)
                    {
                        Census newForm = form as Census;
                        Census oldForm = f as Census;
                        if (oldForm.CensusDate.Equals(newForm.CensusDate) && oldForm.LostCousins == newForm.LostCousins)
                            toDispose.Add(f);
                    }
                    else
                        toDispose.Add(f);
            }
            foreach (Form f in toDispose)
                f.Dispose();
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
            mnuPrint.Enabled = false;
            if (ft.Loading)
            {
                tabSelector.SelectedTab = tabDisplayProgress;
                tsCountLabel.Text = "";
                tsHintsLabel.Text = "";
            }
            else
            {
                if (!ft.DataLoaded)
                {   // do not process anything if no GEDCOM yet loaded
                    if (tabSelector.SelectedTab != tabDisplayProgress)
                    {
                        tabSelector.SelectedTab = tabDisplayProgress;
                        tsCountLabel.Text = "";
                        tsHintsLabel.Text = "";
                        MessageBox.Show(Properties.ErrorMessages.FTA_0002, "Error : FTA_0002");
                    }
                    return;
                }
                HourGlass(true);
                if (tabSelector.SelectedTab == tabDisplayProgress)
                {
                    tsCountLabel.Text = "";
                    tsHintsLabel.Text = "";
                    mnuPrint.Enabled = true;
                }
                else if (tabSelector.SelectedTab == tabIndividuals)
                {
                    SortableBindingList<IDisplayIndividual> list = ft.AllDisplayIndividuals;
                    dgIndividuals.DataSource = list;
                    dgIndividuals.Sort(dgIndividuals.Columns["Ind_ID"], ListSortDirection.Ascending);
                    dgIndividuals.Focus();
                    mnuPrint.Enabled = true;
                    tsCountLabel.Text = Properties.Messages.Count + list.Count;
                    tsHintsLabel.Text = Properties.Messages.Hints_Individual;
                }
                else if (tabSelector.SelectedTab == tabFamilies)
                {
                    SortableBindingList<IDisplayFamily> list = ft.AllDisplayFamilies;
                    dgFamilies.DataSource = list;
                    dgFamilies.Sort(dgFamilies.Columns["FamilyID"], ListSortDirection.Ascending);
                    dgFamilies.Focus();
                    mnuPrint.Enabled = true;
                    tsCountLabel.Text = Properties.Messages.Count + list.Count;
                    tsHintsLabel.Text = Properties.Messages.Hints_Family;
                }
                else if (tabSelector.SelectedTab == tabOccupations)
                {
                    SortableBindingList<IDisplayOccupation> list = ft.AllDisplayOccupations;
                    dgOccupations.DataSource = list;
                    dgOccupations.Sort(dgOccupations.Columns["Occupation"], ListSortDirection.Ascending);
                    dgOccupations.Focus();
                    mnuPrint.Enabled = true;
                    tsCountLabel.Text = Properties.Messages.Count + list.Count;
                    tsHintsLabel.Text = Properties.Messages.Hints_Occupation;
                }
                else if (tabSelector.SelectedTab == tabCensus)
                {
                    cenDate.RevertToDefaultDate();
                    tsCountLabel.Text = "";
                    tsHintsLabel.Text = "";
                    btnShowResults.Enabled = ft.IndividualCount > 0;
                    cenDate.AddAllCensusItems();
                }
                else if (tabSelector.SelectedTab == tabTreetops)
                {
                    tsCountLabel.Text = "";
                    tsHintsLabel.Text = "";
                    dgTreeTops.DataSource = null;
                    if (ckbTTIgnoreLocations.Checked)
                        treetopsCountry.Enabled = false;
                    else
                        treetopsCountry.Enabled = true;
                }
                else if (tabSelector.SelectedTab == tabWarDead)
                {
                    tsCountLabel.Text = "";
                    tsHintsLabel.Text = "";
                    dgWarDead.DataSource = null;
                    if (ckbWDIgnoreLocations.Checked)
                        wardeadCountry.Enabled = false;
                    else
                        wardeadCountry.Enabled = true;
                }
                else if (tabSelector.SelectedTab == tabLostCousins)
                {
                    tsCountLabel.Text = "";
                    tsHintsLabel.Text = "";
                    btnLC1881EW.Enabled = btnLC1881Scot.Enabled = btnLC1841EW.Enabled =
                        btnLC1881Canada.Enabled = btnLC1880USA.Enabled = btnLC1911Ireland.Enabled =
                        btnLC1911EW.Enabled = ft.IndividualCount > 0;
                }
                else if (tabSelector.SelectedTab == tabDataErrors)
                {
                    SortableBindingList<DataError> errors = ft.DataErrors(ckbDataErrors);
                    dgDataErrors.DataSource = errors;
                    dgDataErrors.Focus();
                    mnuPrint.Enabled = true;
                    tsCountLabel.Text = Properties.Messages.Count + errors.Count;
                    tsHintsLabel.Text = Properties.Messages.Hints_Individual;
                }
                else if (tabSelector.SelectedTab == tabLooseDeaths)
                {
                    SortableBindingList<IDisplayLooseDeath> looseDeathList = ft.LooseDeaths;
                    dgLooseDeaths.DataSource = looseDeathList;
                    dgLooseDeaths.Focus();
                    mnuPrint.Enabled = true;
                    tsCountLabel.Text = Properties.Messages.Count + looseDeathList.Count;
                    tsHintsLabel.Text = Properties.Messages.Hints_Individual;
                }
                else if (tabSelector.SelectedTab == tabLocations)
                {
                    HourGlass(true);
                    tabCtrlLocations.SelectedIndex = 0;
                    tsCountLabel.Text = "";
                    tsHintsLabel.Text = Properties.Messages.Hints_Location;
                    treeViewLocations.Nodes.Clear();
                    Application.DoEvents();
                    treeViewLocations.Nodes.AddRange(ft.GetAllLocationsTreeNodes(treeViewLocations.Font));
                    mnuPrint.Enabled = false;
                    dgCountries.DataSource = ft.AllDisplayCountries;
                    dgRegions.DataSource = ft.AllDisplayRegions;
                    dgSubRegions.DataSource = ft.AllDisplaySubRegions;
                    dgAddresses.DataSource = ft.AllDisplayAddresses;
                    dgPlaces.DataSource = ft.AllDisplayPlaces;
                    HourGlass(false);
                }
                HourGlass(false);
            }
        }

        private void dgCountries_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgCountries.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.SetLocation(loc, FactLocation.COUNTRY);
            DisposeDuplicateForms(frmInd);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = dgRegions.CurrentRow == null ? FactLocation.UNKNOWN_LOCATION : (FactLocation)dgRegions.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.SetLocation(loc, FactLocation.REGION);
            DisposeDuplicateForms(frmInd);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgSubRegions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgSubRegions.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.SetLocation(loc, FactLocation.PARISH);
            DisposeDuplicateForms(frmInd);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgAddresses_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgAddresses.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.SetLocation(loc, FactLocation.ADDRESS);
            DisposeDuplicateForms(frmInd);
            frmInd.Show();
            HourGlass(false);
        }

        private void dgPlaces_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            HourGlass(true);
            FactLocation loc = (FactLocation)dgPlaces.CurrentRow.DataBoundItem;
            Forms.People frmInd = new Forms.People();
            frmInd.SetLocation(loc, FactLocation.PLACE);
            DisposeDuplicateForms(frmInd);
            frmInd.Show();
            HourGlass(false);
        }

        private void btnShowResults_Click(object sender, EventArgs e)
        {
            Census census;
            string country;
            Predicate<CensusIndividual> filter = CreateCensusIndividualFilter();
            IComparer<CensusIndividual> censusComparator;
            //            if (ckbNoLocations.Checked)
            //            {
            census = new Census(false, cenDate.CensusCountry);
            country = string.Empty;
            censusComparator = new DefaultCensusComparer();
            //}
            //else
            //{
            //    census = new Census(cenDate.CensusCountry, censusCountry.GetLocation);
            //    country = " " + cenDate.Country;
            //    censusComparator = new CensusLocationComparer(FactLocation.PARISH);
            //}
            census.SetupCensus(filter, censusComparator, censusDate, false, false);
            census.Text = "People missing a " + censusDate.StartDate.Year.ToString() + country + " Census Record that you can search for";
            DisposeDuplicateForms(census);
            census.Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is Family Tree Analyzer version " + VERSION);
        }

        #region Filters
        private Predicate<CensusIndividual> CreateCensusIndividualFilter()
        {
            Predicate<CensusIndividual> relationFilter = relTypesCensus.BuildFilter<CensusIndividual>(x => x.RelationType);
            Predicate<CensusIndividual> locationFilter;

            //            if (ckbNoLocations.Checked)
            //            {
            locationFilter = (x => x.IsValidLocation(cenDate.CensusCountry));
            //}
            //else
            //{
            //    locationFilter = censusCountry.BuildFilter<CensusIndividual>(
            //        cenDate.SelectedDate, (d, x) => x.BestLocation(d));
            //}

            Predicate<CensusIndividual> filter = FilterUtils.AndFilter<CensusIndividual>(locationFilter, relationFilter,
                    FilterUtils.DateFilter<CensusIndividual>(x => x.CensusDate, cenDate.SelectedDate));

            if (txtSurname.Text.Length > 0)
            {
                Predicate<CensusIndividual> surnameFilter = FilterUtils.StringFilter<CensusIndividual>(x => x.Surname, txtSurname.Text);
                filter = FilterUtils.AndFilter<CensusIndividual>(filter, surnameFilter);
            }

            filter = FilterUtils.AndFilter<CensusIndividual>(x => x.Age.MinAge < (int)udAgeFilter.Value, filter);
            return filter;
        }

        private Predicate<Individual> createTreeTopsIndividualFilter()
        {
            Predicate<Individual> locationFilter = treetopsCountry.BuildFilter<Individual>(FactDate.UNKNOWN_DATE, (d, x) => x.BestLocation(d));
            Predicate<Individual> relationFilter = treetopsRelation.BuildFilter<Individual>(x => x.RelationType);
            Predicate<Individual> filter = FilterUtils.AndFilter<Individual>(locationFilter, relationFilter);

            if (ckbTTIgnoreLocations.Checked)
                filter = relationFilter;
            else
                filter = FilterUtils.AndFilter<Individual>(locationFilter, relationFilter);

            if (txtTreetopsSurname.Text.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, txtTreetopsSurname.Text);
                filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
            }

            return filter;
        }

        private Predicate<Individual> CreateWardeadIndividualFilter(FactDate birthRange, FactDate deathRange)
        {
            Predicate<Individual> filter;
            Predicate<Individual> locationFilter = wardeadCountry.BuildFilter<Individual>(FactDate.UNKNOWN_DATE, (d, x) => x.BestLocation(d));
            Predicate<Individual> relationFilter = wardeadRelation.BuildFilter<Individual>(x => x.RelationType);
            Predicate<Individual> birthFilter = FilterUtils.DateFilter<Individual>(x => x.BirthDate, birthRange);
            Predicate<Individual> deathFilter = FilterUtils.DateFilter<Individual>(x => x.DeathDate, deathRange);

            if (ckbWDIgnoreLocations.Checked)
                filter = FilterUtils.AndFilter<Individual>(
                        FilterUtils.AndFilter<Individual>(birthFilter, deathFilter), relationFilter);
            else
                filter = FilterUtils.AndFilter<Individual>(
                        FilterUtils.AndFilter<Individual>(birthFilter, deathFilter),
                        FilterUtils.AndFilter<Individual>(locationFilter, relationFilter));

            if (txtWarDeadSurname.Text.Length > 0)
            {
                Predicate<Individual> surnameFilter = FilterUtils.StringFilter<Individual>(x => x.Surname, txtWarDeadSurname.Text);
                filter = FilterUtils.AndFilter<Individual>(filter, surnameFilter);
            }

            return filter;
        }
        #endregion

        #region Lost Cousins
        private void LostCousinsCensus(string location, Predicate<CensusIndividual> filter, FactDate censusDate, string reportTitle)
        {
            Func<CensusIndividual, int> relationType = x => x.RelationType;
            Func<CensusIndividual, FactDate> registrationDate = x => x.CensusDate;
            HourGlass(true);
            Predicate<CensusIndividual> relation =
                FilterUtils.OrFilter<CensusIndividual>(
                    FilterUtils.OrFilter<CensusIndividual>(
                        FilterUtils.IntFilter<CensusIndividual>(relationType, Individual.BLOOD),
                        FilterUtils.IntFilter<CensusIndividual>(relationType, Individual.DIRECT)),
                    FilterUtils.IntFilter<CensusIndividual>(relationType, Individual.MARRIEDTODB));
            IComparer<CensusIndividual> comparer;
            filter = FilterUtils.TrueFilter<CensusIndividual>();
            Census census = new Census(true, location);
            comparer = new DefaultCensusComparer();

            if (ckbRestrictions.Checked)
                filter = FilterUtils.AndFilter<CensusIndividual>(
                    FilterUtils.DateFilter<CensusIndividual>(registrationDate, censusDate),
                    filter, relation);
            else
                filter = FilterUtils.AndFilter<CensusIndividual>(FilterUtils.DateFilter<CensusIndividual>(registrationDate, censusDate), filter);

            census.SetupCensus(filter, comparer, censusDate, true, ckbShowLCEntered.Checked);
            if (ckbShowLCEntered.Checked)
                census.Text = reportTitle + " already entered into Lost Cousins website";
            else
                census.Text = reportTitle + " to enter into Lost Cousins website";
            HourGlass(false);
            DisposeDuplicateForms(census);
            census.Show();
        }

        private void btnLC1881EW_Click(object sender, EventArgs e)
        {
            Func<CensusIndividual, string> country = x => x.BestLocation(CensusDate.UKCENSUS1881).Country;
            Predicate<CensusIndividual> filter = FilterUtils.OrFilter<CensusIndividual>(
                FilterUtils.StringFilter<CensusIndividual>(country, Countries.ENGLAND),
                FilterUtils.StringFilter<CensusIndividual>(country, Countries.WALES));
            string reportTitle = "1881 England & Wales Census Records on file";
            LostCousinsCensus(Countries.ENG_WALES, filter, CensusDate.UKCENSUS1881, reportTitle);
        }

        private void btnLC1881Scot_Click(object sender, EventArgs e)
        {
            Func<CensusIndividual, string> country = x => x.BestLocation(CensusDate.UKCENSUS1881).Country;
            Predicate<CensusIndividual> filter = FilterUtils.StringFilter<CensusIndividual>(country, Countries.SCOTLAND);
            string reportTitle = "1881 Scotland Census Records on file";
            LostCousinsCensus(Countries.SCOTLAND, filter, CensusDate.UKCENSUS1881, reportTitle);
        }

        private void btnLC1881Canada_Click(object sender, EventArgs e)
        {
            Func<CensusIndividual, string> country = x => x.BestLocation(CensusDate.UKCENSUS1881).Country;
            Predicate<CensusIndividual> filter = FilterUtils.StringFilter<CensusIndividual>(country, Countries.CANADA);
            string reportTitle = "1881 Canada Census Records on file";
            LostCousinsCensus(Countries.CANADA, filter, CensusDate.CANADACENSUS1881, reportTitle);
        }

        private void btnLC1841EW_Click(object sender, EventArgs e)
        {
            Func<CensusIndividual, string> country = x => x.BestLocation(CensusDate.UKCENSUS1841).Country;
            Predicate<CensusIndividual> filter = FilterUtils.OrFilter<CensusIndividual>(
                FilterUtils.StringFilter<CensusIndividual>(country, Countries.ENGLAND),
                FilterUtils.StringFilter<CensusIndividual>(country, Countries.WALES));
            string reportTitle = "1841 England & Wales Census Records on file";
            LostCousinsCensus(Countries.ENG_WALES, filter, CensusDate.UKCENSUS1841, reportTitle);
        }


        private void btnLC1911EW_Click(object sender, EventArgs e)
        {
            Func<CensusIndividual, string> country = x => x.BestLocation(CensusDate.UKCENSUS1911).Country;
            Predicate<CensusIndividual> filter = FilterUtils.OrFilter<CensusIndividual>(
                FilterUtils.StringFilter<CensusIndividual>(country, Countries.ENGLAND),
                FilterUtils.StringFilter<CensusIndividual>(country, Countries.WALES));
            string reportTitle = "1911 England & Wales Census Records on file";
            LostCousinsCensus(Countries.ENG_WALES, filter, CensusDate.UKCENSUS1911, reportTitle);
        }

        private void btnLC1880USA_Click(object sender, EventArgs e)
        {
            Func<CensusIndividual, string> country = x => x.BestLocation(CensusDate.USCENSUS1880).Country;
            Predicate<CensusIndividual> filter = FilterUtils.StringFilter<CensusIndividual>(country, Countries.UNITED_STATES);
            string reportTitle = "1880 US Census Records on file";
            LostCousinsCensus(Countries.UNITED_STATES, filter, CensusDate.USCENSUS1880, reportTitle);
        }

        private void btnLC1911Ireland_Click(object sender, EventArgs e)
        {
            Func<CensusIndividual, string> country = x => x.BestLocation(CensusDate.IRELANDCENSUS1911).Country;
            Predicate<CensusIndividual> filter = FilterUtils.StringFilter<CensusIndividual>(country, Countries.IRELAND);
            string reportTitle = "1911 Ireland Census Records on file";
            LostCousinsCensus(Countries.IRELAND, filter, CensusDate.IRELANDCENSUS1911, reportTitle);
        }

        private void labLostCousinsWeb_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Process.Start(null, "http://www.lostcousins.com/?ref=LC585149");
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

        //private void _timerCheckForUpdates_Callback(object data)
        //{
        //    if (_checkForUpdatesEnabled)
        //    {
        //        Version currentVersion = new Version(VERSION);
        //        string strLatestVersion = new Utilities.WebRequestWrapper().GetLatestVersionString();
        //        if (!string.IsNullOrEmpty(strLatestVersion))
        //        {
        //            Version latestVersion = new Version(strLatestVersion);
        //            if (currentVersion < latestVersion)
        //            {
        //                _checkForUpdatesEnabled = false;
        //                DialogResult result = MessageBox.Show(string.Format("A new version of FTAnalyzer has been released, version {0}!\nWould you like to go to the FTAnalyzer site to download the new version?",
        //                    strLatestVersion), "New Version Released!", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
        //                if (result == DialogResult.Yes)
        //                    Process.Start("http://FTAnalyzer.codeplex.com/");
        //            }
        //            else if (_showNoUpdateMessage)
        //            {
        //                MessageBox.Show("You are running the latest version of FTAnalyzer");
        //            }
        //        }
        //        string strBetaVersion = new Utilities.WebRequestWrapper().GetBetaVersionString();
        //        if (!string.IsNullOrEmpty(strBetaVersion))
        //        {
        //            Version betaVersion = new Version(strBetaVersion);
        //            if (currentVersion < betaVersion)
        //            {
        //                _checkForUpdatesEnabled = false;
        //                DialogResult result = MessageBox.Show(string.Format("A new TEST version of FTAnalyzer has been released, version {0}!\nWould you like to go to the FTAnalyzer site to download the new version?\nPlease note this version is possibly unstable and should only be used by testers.",
        //                    strBetaVersion), "New TEST Version Released!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        //                if (result == DialogResult.Yes)
        //                    Process.Start("http://FTAnalyzer.codeplex.com/");
        //            }
        //        }
        //    }
        //}

        private void MainForm_Load(object sender, EventArgs e)
        {
            //_timerCheckForUpdates = new System.Threading.Timer(new System.Threading.TimerCallback(_timerCheckForUpdates_Callback));
            //_timerCheckForUpdates.Change(3000, 1000 * 60 * 60 * 8); //Check for updates 3 sec after the form loads, and then again every 8 hours
            //GeneralSettings.UseBaptismDatesChanged += new EventHandler(Options_BaptismChanged);
            GeneralSettings.AllowEmptyLocationsChanged += new EventHandler(Options_AllowEmptyLocationsChanged);
            GeneralSettings.UseResidenceAsCensusChanged += new EventHandler(Options_UseResidenceAsCensusChanged);
            //GeneralSettings.StrictResidenceDatesChanged += new EventHandler(Options_StrictResidenceDatesChanged);
            GeneralSettings.TolerateInaccurateCensusChanged += new EventHandler(Options_TolerateInaccurateCensusChanged);
            this.Text = "Family Tree Analyzer v" + VERSION;
        }

        #region ToolStrip Clicks
        //private void checkForUpdatesToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    _checkForUpdatesEnabled = true;
        //    _showNoUpdateMessage = true;
        //    _timerCheckForUpdates_Callback(null);
        //    _showNoUpdateMessage = false;
        //}

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserControls.Options options = new UserControls.Options();
            options.ShowDialog(this);
            options.Dispose();
        }

        //private void BirthRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MultiComparator<Registration> birthComparator = new MultiComparator<Registration>();
        //    birthComparator.addComparator(new LocationComparator(FactLocation.PARISH));
        //    birthComparator.addComparator(new DateComparator());

        //    Func<Registration, bool> partialEnglishData =
        //        FilterUtils.AndFilter<Registration>(
        //            FilterUtils.IncompleteDataFilter<Registration>(
        //                FactLocation.PARISH, x => x.isCertificatePresent(), x => x.FilterDate, (d, x) => x.BestLocation(d)),
        //            FilterUtils.StringFilter<Registration>(x => x.BestLocation(FactDate.UNKNOWN_DATE).Country, Countries.ENGLAND));

        //    Func<Registration, bool> directOrBlood = FilterUtils.OrFilter<Registration>(
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.DIRECT),
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.BLOOD));

        //    RegistrationsProcessor onlineBirthsRP = new RegistrationsProcessor(
        //            FilterUtils.AndFilter<Registration>(directOrBlood, partialEnglishData), birthComparator);

        //    List<Registration> regs = ft.getAllBirthRegistrations();
        //    List<Registration> result = onlineBirthsRP.processRegistrations(regs);

        //    RegistrationReport report = new RegistrationReport();
        //    report.SetupBirthRegistration(result);
        //    DisposeDuplicateForms(report);
        //    report.Show();
        //}

        //private void deathRegistrationsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MultiComparator<Registration> deathComparator = new MultiComparator<Registration>();
        //    deathComparator.addComparator(new LocationComparator(FactLocation.PARISH));
        //    deathComparator.addComparator(new DateComparator());

        //    Func<Registration, bool> partialEnglishData =
        //        FilterUtils.AndFilter<Registration>(
        //            FilterUtils.IncompleteDataFilter<Registration>(
        //                FactLocation.PARISH, x => x.isCertificatePresent(), x => x.FilterDate, (d, x) => x.BestLocation(d)),
        //            FilterUtils.StringFilter<Registration>(x => x.BestLocation(FactDate.UNKNOWN_DATE).Country, Countries.ENGLAND));

        //    Func<Registration, bool> directOrBlood = FilterUtils.OrFilter<Registration>(
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.DIRECT),
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.BLOOD));

        //    RegistrationsProcessor onlineDeathsRP = new RegistrationsProcessor(
        //            FilterUtils.AndFilter<Registration>(directOrBlood, partialEnglishData), deathComparator);

        //    List<Registration> regs = ft.getAllDeathRegistrations();
        //    List<Registration> result = onlineDeathsRP.processRegistrations(regs);

        //    RegistrationReport report = new RegistrationReport();
        //    report.SetupDeathRegistration(result);
        //    DisposeDuplicateForms(report);
        //    report.Show();
        //}

        //private void marriageRegistrationsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    MultiComparator<Registration> marriageComparator = new MultiComparator<Registration>();
        //    marriageComparator.addComparator(new LocationComparator(FactLocation.PARISH));
        //    marriageComparator.addComparator(new DateComparator());

        //    Func<Registration, bool> partialEnglishData =
        //        FilterUtils.AndFilter<Registration>(
        //            FilterUtils.IncompleteDataFilter<Registration>(
        //                FactLocation.PARISH, x => x.isCertificatePresent(), x => x.FilterDate, (d, x) => x.BestLocation(d)),
        //            FilterUtils.StringFilter<Registration>(x => x.BestLocation(FactDate.UNKNOWN_DATE).Country, Countries.ENGLAND));

        //    Func<Registration, bool> directOrBlood = FilterUtils.OrFilter<Registration>(
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.DIRECT),
        //            FilterUtils.IntFilter<Registration>(x => x.RelationType, Individual.BLOOD));

        //    RegistrationsProcessor onlineMarriagesRP = new RegistrationsProcessor(
        //            FilterUtils.AndFilter<Registration>(directOrBlood, partialEnglishData), marriageComparator);

        //    List<Registration> regs = ft.getAllMarriageRegistrations();
        //    List<Registration> result = onlineMarriagesRP.processRegistrations(regs);

        //    RegistrationReport report = new RegistrationReport();
        //    report.SetupMarriageRegistration(result);
        //    DisposeDuplicateForms(report);
        //    report.Show();
        //}

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lostcousins.com/?ref=LC585149");
        }

        //private void btnFamilySearchFolderBrowse_Click(object sender, EventArgs e)
        //{
        //    FolderBrowserDialog browse = new FolderBrowserDialog();
        //    browse.ShowNewFolderButton = true;
        //    browse.Description = "Please select a folder where the results of the FamilySearch search will be placed";
        //    browse.RootFolder = Environment.SpecialFolder.Desktop;
        //    if (txtFamilySearchfolder.Text != string.Empty)
        //        browse.SelectedPath = txtFamilySearchfolder.Text;
        //    if (browse.ShowDialog() == System.Windows.Forms.DialogResult.OK)
        //    {
        //        Application.UserAppDataRegistry.SetValue("FamilySearch Search Path", browse.SelectedPath);
        //        txtFamilySearchfolder.Text = browse.SelectedPath;
        //    }
        //}

        //private void btnFamilySearchMarriageSearch_Click(object sender, EventArgs e)
        //{
        //    HourGlass(true);
        //    btnCancelFamilySearch.Visible = true;
        //    btnViewResults.Visible = false;
        //    btnFamilySearchChildrenSearch.Enabled = false;
        //    btnFamilySearchMarriageSearch.Enabled = false;
        //    rtbFamilySearchResults.Text = "FamilySearch Marriage Search started.\n";
        //    int level = rbFamilySearchCountry.Checked ? FactLocation.COUNTRY : FactLocation.REGION;
        //    FamilySearchForm form = new FamilySearchNewSearchForm(rtbFamilySearchResults, FamilySearchDefaultCountry.Country, level, FamilySearchrelationTypes.Status, txtFamilySearchSurname.Text, webBrowser);
        //    IList<Family> families = ft.AllFamilies.ToList();
        //    int counter = 0;
        //    pbFamilySearch.Visible = true;
        //    pbFamilySearch.Maximum = families.Count;
        //    pbFamilySearch.Value = 0;
        //    stopProcessing = false;
        //    foreach (Family f in ft.AllFamilies)
        //    {
        //        form.SearchFamilySearch(f, txtFamilySearchfolder.Text, FamilySearchForm.MARRIAGESEARCH);
        //        pbFamilySearch.Value = counter++;
        //        Application.DoEvents();
        //        if (stopProcessing)
        //            break;
        //    }
        //    pbFamilySearch.Visible = false;
        //    btnCancelFamilySearch.Visible = false;
        //    btnViewResults.Visible = true;
        //    btnFamilySearchChildrenSearch.Enabled = true;
        //    btnFamilySearchMarriageSearch.Enabled = true;
        //    rtbFamilySearchResults.AppendText("\nFamilySearch Marriage Search finished.\n");
        //    HourGlass(false);
        //}

        //private void btnFamilySearchChildrenSearch_Click(object sender, EventArgs e)
        //{
        //    HourGlass(true);
        //    btnCancelFamilySearch.Visible = true;
        //    btnViewResults.Visible = false;
        //    btnFamilySearchChildrenSearch.Enabled = false;
        //    btnFamilySearchMarriageSearch.Enabled = false;
        //    rtbFamilySearchResults.Text = "FamilySearch Children Search started.\n";
        //    int level = rbFamilySearchCountry.Checked ? FactLocation.COUNTRY : FactLocation.REGION;
        //    FamilySearchForm form = new FamilySearchOldSearchForm(rtbFamilySearchResults, FamilySearchDefaultCountry.Country, level, FamilySearchrelationTypes.Status, txtFamilySearchSurname.Text);
        //    IList<Family> families = ft.AllFamilies.ToList();
        //    int counter = 0;
        //    pbFamilySearch.Visible = true;
        //    pbFamilySearch.Maximum = families.Count;
        //    pbFamilySearch.Value = 0;
        //    stopProcessing = false;
        //    foreach (Family f in families)
        //    {
        //        pbFamilySearch.Value = counter++;
        //        form.SearchFamilySearch(f, txtFamilySearchfolder.Text, FamilySearchForm.CHILDRENSEARCH);
        //        Application.DoEvents();
        //        if (stopProcessing)
        //            break;
        //    }
        //    pbFamilySearch.Visible = false;
        //    btnCancelFamilySearch.Visible = false;
        //    btnViewResults.Visible = true;
        //    btnFamilySearchChildrenSearch.Enabled = true;
        //    btnFamilySearchMarriageSearch.Enabled = true;
        //    rtbFamilySearchResults.AppendText("\nFamilySearch Children Search finished.\n");
        //    HourGlass(false);
        //}

        //private void censusCountry_CountryChanged(object sender, EventArgs e)
        //{
        //    cenDate.Country = censusCountry.Country;
        //}

        private void cenDate_CensusChanged(object sender, EventArgs e)
        {
            censusDate = cenDate.SelectedDate;
        }

        //private void btnViewResults_Click(object sender, EventArgs e)
        //{
        //    FamilySearchResultsViewer frmResults = new FamilySearchResultsViewer(txtFamilySearchfolder.Text);
        //    if (frmResults.ResultsPresent)
        //    {
        //        DisposeDuplicateForms(frmResults);
        //        frmResults.Show();
        //    }
        //    else
        //    {
        //        frmResults.Dispose();
        //        MessageBox.Show("Sorry there are no results files in the selected folder.");
        //    }
        //}

        //private void btnCancelFamilySearch_Click(object sender, EventArgs e)
        //{
        //    stopProcessing = true;
        //}

        //private void FamilySearchDefaultCountry_CountryChanged(object sender, EventArgs e)
        //{
        //    if (FamilySearchDefaultCountry.Country == Countries.SCOTLAND)
        //        rbFamilySearchCountry.Checked = true;
        //    else
        //        rbFamilySearchRegion.Checked = true;
        //}

        //private void rtbFamilySearchResults_TextChanged(object sender, EventArgs e)
        //{
        //    rtbFamilySearchResults.ScrollToBottom();
        //}

        private void rtbOutput_TextChanged(object sender, EventArgs e)
        {
            rtbOutput.ScrollToBottom();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopProcessing = true;
        }

        private void btnTreeTops_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Predicate<Individual> filter = createTreeTopsIndividualFilter();
            List<IDisplayIndividual> treeTopsList = ft.GetTreeTops(filter).ToList();
            treeTopsList.Sort(new BirthDateComparer());
            dgTreeTops.DataSource = new SortableBindingList<IDisplayIndividual>(treeTopsList);
            dgTreeTops.Focus();
            foreach (DataGridViewColumn c in dgTreeTops.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = Properties.Messages.Count + treeTopsList.Count;
            tsHintsLabel.Text = Properties.Messages.Hints_Individual;
            mnuPrint.Enabled = true;
            HourGlass(false);
        }

        private void btnWWI_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Predicate<Individual> filter = CreateWardeadIndividualFilter(new FactDate("BET 1869 AND 1904"), new FactDate("BET 1914 AND 1918"));
            List<IDisplayIndividual> warDeadList = ft.GetWarDead(filter).ToList();
            warDeadList.Sort(new BirthDateComparer(BirthDateComparer.ASCENDING));
            dgWarDead.DataSource = new SortableBindingList<IDisplayIndividual>(warDeadList);
            dgWarDead.Focus();
            foreach (DataGridViewColumn c in dgWarDead.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = Properties.Messages.Count + warDeadList.Count;
            tsHintsLabel.Text = Properties.Messages.Hints_Individual;
            mnuPrint.Enabled = true;
            HourGlass(false);
        }

        private void btnWWII_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Predicate<Individual> filter = CreateWardeadIndividualFilter(new FactDate("BET 1894 AND 1931"), new FactDate("BET 1939 AND 1945"));
            List<IDisplayIndividual> warDeadList = ft.GetWarDead(filter).ToList();
            warDeadList.Sort(new BirthDateComparer(BirthDateComparer.ASCENDING));
            dgWarDead.DataSource = new SortableBindingList<IDisplayIndividual>(warDeadList);
            dgWarDead.Focus();
            foreach (DataGridViewColumn c in dgWarDead.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            tsCountLabel.Text = Properties.Messages.Count + warDeadList.Count;
            tsHintsLabel.Text = Properties.Messages.Hints_Individual;
            mnuPrint.Enabled = true;
            HourGlass(false);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://forums.lc");
        }

        private void mnuPrint_Click(object sender, EventArgs e)
        {
            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Margins =
               new System.Drawing.Printing.Margins(15, 15, 15, 15);
            printDocument.DefaultPageSettings.Landscape = true;
            printDialog.PrinterSettings.DefaultPageSettings.Landscape = true;

            if (tabSelector.SelectedTab == tabDisplayProgress && ft.DataLoaded)
            {
                if (printDialog.ShowDialog(this) == DialogResult.OK)
                {
                    Utilities.Printing p = new Utilities.Printing(rtbOutput);
                    printDocument.PrintPage += new PrintPageEventHandler(p.PrintPage);
                    printDocument.PrinterSettings = printDialog.PrinterSettings;
                    printDocument.DocumentName = "GEDCOM Load Results";
                    printDocument.Print();
                }
            }
            if (tabSelector.SelectedTab == tabIndividuals)
            {
                PrintDataGrid(true, dgIndividuals, "List of Individuals");
            }
            if (tabSelector.SelectedTab == tabFamilies)
            {
                PrintDataGrid(true, dgFamilies, "List of Families");
            }
            if (tabSelector.SelectedTab == tabOccupations)
            {
                PrintDataGrid(false, dgOccupations, "List of Occupations");
            }
            if (tabSelector.SelectedTab == tabLocations)
            {
                if (tabCtrlLocations.SelectedTab == tabCountries)
                    PrintDataGrid(false, dgCountries, "List of Countries");
                if (tabCtrlLocations.SelectedTab == tabRegions)
                    PrintDataGrid(false, dgRegions, "List of Regions");
                if (tabCtrlLocations.SelectedTab == tabSubRegions)
                    PrintDataGrid(false, dgSubRegions, "List of Sub Regions");
                if (tabCtrlLocations.SelectedTab == tabAddresses)
                    PrintDataGrid(false, dgAddresses, "List of Addresses");
                if (tabCtrlLocations.SelectedTab == tabPlaces)
                    PrintDataGrid(false, dgPlaces, "List of Places");
            }
            if (tabSelector.SelectedTab == tabDataErrors)
            {
                PrintDataGrid(false, dgDataErrors, "List of Data Errors");
            }
            else if (tabSelector.SelectedTab == tabLooseDeaths)
            {
                PrintDataGrid(true, dgLooseDeaths, "List of Loose Deaths");
            }
            else if (tabSelector.SelectedTab == tabTreetops)
            {
                PrintDataGrid(true, dgTreeTops, "List of People at Top of Tree");
            }
            else if (tabSelector.SelectedTab == tabWarDead)
            {
                PrintDataGrid(true, dgWarDead, "List of Possible War Dead");
            }
        }

        private void PrintDataGrid(bool landscape, DataGridView dg, string title)
        {
            PrintingDataGridViewProvider printProvider = PrintingDataGridViewProvider.Create(
                printDocument, dg, true, true, true,
                new TitlePrintBlock(title), null, null);
            printDialog.PrinterSettings.DefaultPageSettings.Landscape = landscape;
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDocument.DocumentName = title;
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.Print();
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
            frmInd.SetWorkers(occ.Occupation, ft.AllWorkers(occ.Occupation));
            DisposeDuplicateForms(frmInd);
            frmInd.Show();
            HourGlass(false);
        }

        private void setAsRootToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Individual ind = (Individual)dgIndividuals.CurrentRow.DataBoundItem;
            if (ind != null)
            {
                ft.SetRelations(ind.IndividualID);
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
                {
                    DisposeDuplicateForms(frmGoogleMap);
                    frmGoogleMap.Show();
                }
                else
                {
                    frmGoogleMap.Dispose();
                    MessageBox.Show("Unable to find location : " + loc.GetLocation(locType));
                }
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
                {
                    DisposeDuplicateForms(frmBingMap);
                    frmBingMap.Show();
                }
                else
                {
                    frmBingMap.Dispose();
                    MessageBox.Show("Unable to find location : " + loc.GetLocation(locType));
                }
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
                case "Tree View":
                    TreeNode node = treeViewLocations.SelectedNode;
                    if (node != null)
                    {
                        loc = node.Text == "<blank>" ? null : ((FactLocation)node.Tag).GetLocation(node.Level);
                        locType = node.Level;
                    }
                    break;
                case "Countries":
                    loc = dgCountries.CurrentRow == null ? null : (FactLocation)dgCountries.CurrentRow.DataBoundItem;
                    locType = FactLocation.COUNTRY;
                    break;
                case "Regions":
                    loc = dgRegions.CurrentRow == null ? null : (FactLocation)dgRegions.CurrentRow.DataBoundItem;
                    locType = FactLocation.REGION;
                    break;
                case "Parishes":
                    loc = dgSubRegions.CurrentRow == null ? null : (FactLocation)dgSubRegions.CurrentRow.DataBoundItem;
                    locType = FactLocation.PARISH;
                    break;
                case "Addresses":
                    loc = dgAddresses.CurrentRow == null ? null : (FactLocation)dgAddresses.CurrentRow.DataBoundItem;
                    locType = FactLocation.ADDRESS;
                    break;
            }
            if (loc == null)
            {
                if (tabCtrlLocations.SelectedTab.Text == "Tree View")
                    MessageBox.Show("Location selected isn't valid to show on the map.");
                else
                    MessageBox.Show("Please select a location to show on the map.");
            }
            return locType;
        }

        private void ckbDataErrors_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDataErrorsDisplay();
        }

        private void UpdateDataErrorsDisplay()
        {
            HourGlass(true);
            SortableBindingList<DataError> errors = ft.DataErrors(ckbDataErrors);
            dgDataErrors.DataSource = errors;
            tsCountLabel.Text = Properties.Messages.Count + errors.Count;
            tsHintsLabel.Text = Properties.Messages.Hints_Individual;
            HourGlass(false);
        }

        private void childAgeProfilesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Statistics s = Statistics.Instance;
            MessageBox.Show(s.ChildrenBirthProfiles());
        }

        private void viewOnlineManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://ftanalyzer.codeplex.com/documentation");
        }

        private void olderParentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Forms.People frmInd = new Forms.People();
            string inputAge = "50";
            DialogResult result = DialogResult.Cancel;
            int age = 0;
            do
            {
                try
                {
                    result = InputBox.Show("Enter age between 13 and 90", "Please select minimum age to report on", ref inputAge);
                    age = Int32.Parse(inputAge);
                }
                catch (Exception)
                {
                    if (result != DialogResult.Cancel)
                        MessageBox.Show("Invalid Age entered");
                }
                if (age < 13 || age > 90)
                    MessageBox.Show("Please enter an age between 13 and 90");
            } while ((result != DialogResult.Cancel) && (age < 13 || age > 90));
            if (result == DialogResult.OK)
            {
                if (frmInd.OlderParents(age))
                {
                    DisposeDuplicateForms(frmInd);
                    frmInd.Show();
                }
            }
            HourGlass(false);
        }

        private void ckbTTIgnoreLocations_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbTTIgnoreLocations.Checked)
                treetopsCountry.Enabled = false;
            else
                treetopsCountry.Enabled = true;
        }

        private void ckbWDIgnoreLocations_CheckedChanged(object sender, EventArgs e)
        {
            if (ckbWDIgnoreLocations.Checked)
                wardeadCountry.Enabled = false;
            else
                wardeadCountry.Enabled = true;
        }

        private void individualsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            DataTable dt = convertor.ToDataTable(new List<IExportIndividual>(ft.AllIndividuals));
            ExportToExcel.Export(dt);
            HourGlass(false);
        }

        private void familiesToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            DataTable dt = convertor.ToDataTable(new List<IDisplayFamily>(ft.AllFamilies));
            ExportToExcel.Export(dt);
            HourGlass(false);
        }

        private void factsToExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            DataTable dt = convertor.ToDataTable(new List<ExportFacts>(ft.AllExportFacts));
            ExportToExcel.Export(dt);
            HourGlass(false);
        }

        private void dgIndividuals_MouseDown(object sender, MouseEventArgs e)
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
            if (e.Clicks == 2)
            {
                if (hti.RowIndex >= 0 && hti.ColumnIndex >= 0)
                {
                    string indID = (string)dgIndividuals.CurrentRow.Cells["Ind_ID"].Value;
                    Individual ind = ft.GetIndividual(indID);
                    Facts factForm = new Facts(ind);
                    DisposeDuplicateForms(factForm);
                    factForm.Show();
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            Application.Exit();
        }

        private void tabCtrlLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            HourGlass(true);
            TabPage current = tabCtrlLocations.SelectedTab;
            Control control = current.Controls[0];
            control.Focus();
            if (control is DataGridView)
            {
                DataGridView dg = control as DataGridView;
                tsCountLabel.Text = Properties.Messages.Count + dg.RowCount + " " + dg.Name.Substring(2);
                mnuPrint.Enabled = true;
            }
            else
            {
                tsCountLabel.Text = string.Empty;
                mnuPrint.Enabled = false;
            }
            tsHintsLabel.Text = Properties.Messages.Hints_Location;
            HourGlass(false);
        }

        private void FormatCellLocations(DataGridView grid, DataGridViewCellFormattingEventArgs e)
        {
            DataGridViewCellStyle style = grid.DefaultCellStyle;
            DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            string country = (string)cell.Value;
            if (Countries.IsKnownCountry(country))
            {
                if (knownCountryStyle == null)
                {
                    knownCountryStyle = style.Clone();
                    knownCountryStyle.Font = new Font(style.Font, FontStyle.Bold);
                }
                e.CellStyle = knownCountryStyle;
            }
        }

        private void dgCountries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                FormatCellLocations(dgCountries, e);
            }
        }

        private void dgRegions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                FormatCellLocations(dgRegions, e);
            }
        }

        private void dgSubRegions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                FormatCellLocations(dgSubRegions, e);
            }
        }

        private void dgAddresses_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                FormatCellLocations(dgAddresses, e);
            }
        }

        private void dgPlaces_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                FormatCellLocations(dgPlaces, e);
            }
        }

        private void Options_BaptismChanged(object sender, EventArgs e)
        {
            // do anything that needs doing when option changes
        }

        private void Options_AllowEmptyLocationsChanged(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void Options_UseResidenceAsCensusChanged(object sender, EventArgs e)
        {
            // need to refresh any census reports when option changes
        }

        private void Options_StrictResidenceDatesChanged(object sender, EventArgs e)
        {
            // need to refresh any census reports when option changes
        }

        private void Options_TolerateInaccurateCensusChanged(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void ReloadData()
        {
            if (Properties.GeneralSettings.Default.ReloadRequired && ft.DataLoaded)
            {
                DialogResult dr = MessageBox.Show("This option requires the data to be refreshed.\n\nDo you want to reload now?\n\nClicking no will keep the data with the old option.", "Reload GEDCOM File", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                Properties.GeneralSettings.Default.ReloadRequired = false;
                Properties.GeneralSettings.Default.Save();
                if (dr == DialogResult.Yes)
                {
                    LoadFile(filename);
                }
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReloadRequired = false;
            Properties.GeneralSettings.Default.Save();
            LoadFile(filename);
        }

        private bool preventExpand;

        private void treeViewLocations_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            HourGlass(true);
            FactLocation location = e.Node.Tag as FactLocation;
            if (location != null)
            {
                Forms.People frmInd = new Forms.People();
                frmInd.SetLocation(location, e.Node.Level);
                DisposeDuplicateForms(frmInd);
                frmInd.Show();
            }
            HourGlass(false);
        }

        private void treeViewLocations_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = (preventExpand && e.Action == TreeViewAction.Collapse);
        }

        private void treeViewLocations_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            e.Cancel = (preventExpand && e.Action == TreeViewAction.Expand);
        }

        private void treeViewLocations_MouseDown(object sender, MouseEventArgs e)
        {
            preventExpand = e.Clicks > 1;
        }

        private void mainForm_DragDrop(object sender, DragEventArgs e)
        {
            bool fileLoaded = false;
            string[] files = e.Data.GetData(DataFormats.FileDrop) as string[];
            foreach (string filename in files)
            {
                if (Path.GetExtension(filename.ToLower()) == ".ged")
                {
                    fileLoaded = true;
                    LoadFile(filename);
                    break;
                }
            }
            if (!fileLoaded)
                if (files.Length > 1)
                    MessageBox.Show("Unable to load File. None of the files dragged and dropped were *.ged files");
                else
                    MessageBox.Show("Unable to load File. The file dragged and dropped wasn't a *.ged file");
        }

        private void mainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }

        private void dgFamilies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string famID = (string)dgFamilies.CurrentRow.Cells["FamilyID"].Value;
                Family fam = ft.GetFamily(famID);
                Facts factForm = new Facts(fam);
                DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        private void dgDataErrors_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowFacts((string)dgDataErrors.CurrentRow.Cells["Ind_ID"].Value);
        }

        private void dgLooseDeaths_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowFacts((string)dgLooseDeaths.CurrentRow.Cells["Ind_ID"].Value);
        }

        private void dgTreeTops_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowFacts((string)dgTreeTops.CurrentRow.Cells["Ind_ID"].Value);
        }

        private void dgWarDead_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowFacts((string)dgWarDead.CurrentRow.Cells["Ind_ID"].Value);
        }

        private void ShowFacts(string indID)
        {
            Individual ind = ft.GetIndividual(indID);
            Facts factForm = new Facts(ind);
            DisposeDuplicateForms(factForm);
            factForm.Show();
        }

        private void btnColouredCensus_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            List<IDisplayColourCensus> list = ft.ColourCensus(relTypesColoured, txtColouredSurname.Text);
            ColourCensus rs = new ColourCensus(list);
            DisposeDuplicateForms(rs);
            rs.Show();
            rs.Focus();
            HourGlass(false);
        }

        private void btnColourBMD_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            List<IDisplayColourBMD> list = ft.ColourBMD(relTypesColoured, txtColouredSurname.Text);
            ColourBMD rs = new ColourBMD(list);
            DisposeDuplicateForms(rs);
            rs.Show();
            rs.Focus();
            HourGlass(false);
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < ckbDataErrors.Items.Count; i++)
            {
                ckbDataErrors.SetItemChecked(i, true);
            }
            UpdateDataErrorsDisplay();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            foreach (int indexChecked in ckbDataErrors.CheckedIndices)
            {
                ckbDataErrors.SetItemChecked(indexChecked, false);
            }
            UpdateDataErrorsDisplay();
        }

        private void displayOptionsOnLoadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.GeneralSettings.Default.ReportOptions = displayOptionsOnLoadToolStripMenuItem.Checked;
            Properties.GeneralSettings.Default.Save();
        }

        private void reportAnIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://ftanalyzer.codeplex.com/workitem/list/basic");
        }

        private void whatsNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://ftanalyzer.codeplex.com/wikipage?title=What%27s%20New%20in%20this%20Release%3f");
        }
        
        private void btnLCReport_Click(object sender, EventArgs e)
        {
            tabSelector.SelectedTab = tabSelector.TabPages["tabColourReports"];
        }

        private void mnuShowTimeline_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            TimeLine tl = new TimeLine();
            tl.Show();
            DisposeDuplicateForms(tl);
            HourGlass(false);
        }

        private void mnuGeocodeLocations_Click(object sender, EventArgs e)
        {
            if (!ft.Geocoding) // don't geocode if another geocode session in progress
            {
                HourGlass(true);
                TimeLine tl = new TimeLine();
                tl.Show();
                DisposeDuplicateForms(tl);
                tl.StartGeoCoding();
                HourGlass(false);
            }
        }
    }
}
