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
using FTAnalyzer.Utilities;
using FTAnalyzer.UserControls;
using Printing.DataGridViewPrint.Tools;
using System.Drawing;
using FTAnalyzer.Forms;
using Ionic.Zip;
using System.Collections.Specialized;
using Controls;

namespace FTAnalyzer
{
    public partial class MainForm : Form
    {
        private string VERSION = "3.1.1.0-beta-test2";

        private Cursor storedCursor = Cursors.Default;
        private FamilyTree ft = FamilyTree.Instance;
        private bool stopProcessing = false;
        private string filename;
        private Font boldFont;

        public MainForm()
        {
            InitializeComponent();
            displayOptionsOnLoadToolStripMenuItem.Checked = Properties.GeneralSettings.Default.ReportOptions;
            ft.XmlErrorBox = rtbOutput;
            VERSION = PublishVersion();
            treetopsRelation.MarriedToDB = false;
            ShowMenus(false);
            SetSavePath();
            int pos = VERSION.IndexOf('-');
            string ver = pos > 0 ? VERSION.Substring(0, VERSION.IndexOf('-')) : VERSION;
            DatabaseHelper.Instance.CheckDatabaseVersion(new Version(ver));
            BuildRecentList();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //GeneralSettings.UseBaptismDatesChanged += new EventHandler(Options_BaptismChanged);
            GeneralSettings.AllowEmptyLocationsChanged += new EventHandler(Options_AllowEmptyLocationsChanged);
            GeneralSettings.UseResidenceAsCensusChanged += new EventHandler(Options_UseResidenceAsCensusChanged);
            //GeneralSettings.StrictResidenceDatesChanged += new EventHandler(Options_StrictResidenceDatesChanged);
            GeneralSettings.TolerateInaccurateCensusChanged += new EventHandler(Options_TolerateInaccurateCensusChanged);
            GeneralSettings.MinParentalAgeChanged += new EventHandler(Options_MinimumParentalAgeChanged);
            this.Text = "Family Tree Analyzer v" + VERSION;
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
                rtbOutput.Text = string.Empty;
                tsCountLabel.Text = string.Empty;
                tsHintsLabel.Text = string.Empty;
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
                dgLooseBirths.DataSource = null;
                dgLooseDeaths.DataSource = null;
                dgDataErrors.DataSource = null;
                dgOccupations.DataSource = null;
                tabCtrlLooseBDs.SelectedTab = tabLooseBirths; // force back to first tab
                tabCtrlLocations.SelectedTab = tabTreeView; // otherwise totals etc look wrong
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
                        AddFileToRecentList(filename);
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
            mnuLocationsGeocodeReport.Enabled = enabled;
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

        public static void DisposeDuplicateForms(object form)
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
            {
                GC.SuppressFinalize(f);
                f.Dispose();
            }
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
                    btnShowCensusMissing.Enabled = ft.IndividualCount > 0;
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
                    UpdateLostCousinsReport();
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
                else if (tabSelector.SelectedTab == tabLooseBirthDeaths)
                {
                    UpdateLooseBirthDeaths();
                }
                else if (tabSelector.SelectedTab == tabLocations)
                {
                    HourGlass(true);
                    tabCtrlLocations.SelectedIndex = 0;
                    tsCountLabel.Text = "";
                    tsHintsLabel.Text = Properties.Messages.Hints_Location;
                    treeViewLocations.Nodes.Clear();
                    boldFont = new Font(dgCountries.DefaultCellStyle.Font, FontStyle.Bold);
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

        private void UpdateLooseBirthDeaths()
        {
            SortableBindingList<IDisplayLooseBirth> looseBirthList = ft.LooseBirths;
            SortableBindingList<IDisplayLooseDeath> looseDeathList = ft.LooseDeaths;
            dgLooseDeaths.DataSource = looseDeathList;
            dgLooseDeaths.Sort(dgLooseDeaths.Columns["Forenames"], ListSortDirection.Ascending);
            dgLooseDeaths.Sort(dgLooseDeaths.Columns["Surname"], ListSortDirection.Ascending);
            dgLooseBirths.DataSource = looseBirthList;
            dgLooseBirths.Sort(dgLooseBirths.Columns["Forenames"], ListSortDirection.Ascending);
            dgLooseBirths.Sort(dgLooseBirths.Columns["Surname"], ListSortDirection.Ascending);
            dgLooseBirths.Focus();
            mnuPrint.Enabled = true;
            tsCountLabel.Text = Properties.Messages.Count + looseBirthList.Count;
            tsHintsLabel.Text = Properties.Messages.Hints_Loose_Births + Properties.Messages.Hints_Individual;
        }

        private void tabCtrlLooseBDs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabCtrlLooseBDs.SelectedTab == tabLooseBirths)
            {
                dgLooseBirths.Focus();
                tsCountLabel.Text = Properties.Messages.Count + dgLooseBirths.RowCount;
                tsHintsLabel.Text = Properties.Messages.Hints_Loose_Births + Properties.Messages.Hints_Individual;
            }
            else if (tabCtrlLooseBDs.SelectedTab == tabLooseDeaths)
            {
                dgLooseDeaths.Focus();
                tsCountLabel.Text = Properties.Messages.Count + dgLooseDeaths.RowCount;
                tsHintsLabel.Text = Properties.Messages.Hints_Loose_Deaths + Properties.Messages.Hints_Individual;
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
            frmInd.SetLocation(loc, FactLocation.SUBREGION);
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

        private void btnShowCensus_Click(object sender, EventArgs e)
        {
            bool censusDone = sender == btnShowCensusEntered;
            Predicate<CensusIndividual> filter = CreateCensusIndividualFilter(censusDone);
            Census census = new Census(cenDate.SelectedDate);
            census.SetupCensus(filter, censusDone);
            if (censusDone)
                census.Text = "People entered with a " + cenDate.SelectedDate.StartDate.Year.ToString() + " " + cenDate.CensusCountry + " Census Record";
            else
                census.Text = "People missing a " + cenDate.SelectedDate.StartDate.Year.ToString() + " " + cenDate.CensusCountry + " Census Record that you can search for";
            DisposeDuplicateForms(census);
            census.Show();
        }

        private void btnMissingCensusLocation_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            People people = new People();
            people.SetupMissingCensusLocation();
            DisposeDuplicateForms(people);
            people.Show();
            HourGlass(false);
        }

        private void btnDuplicateCensus_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            People people = new People();
            people.SetupDuplicateCensus();
            DisposeDuplicateForms(people);
            people.Show();
            HourGlass(false);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is Family Tree Analyzer version " + VERSION);
        }

        #region Filters
        private Predicate<CensusIndividual> CreateCensusIndividualFilter(bool censusDone)
        {
            Predicate<CensusIndividual> relationFilter = relTypesCensus.BuildFilter<CensusIndividual>(x => x.RelationType);
            Predicate<CensusIndividual> dateFilter = censusDone ?
                new Predicate<CensusIndividual>(x => x.IsCensusDone(cenDate.SelectedDate)) :
                new Predicate<CensusIndividual>(x => !x.IsCensusDone(cenDate.SelectedDate));

            Predicate<CensusIndividual> filter = FilterUtils.AndFilter<CensusIndividual>(relationFilter, dateFilter);
            if (txtSurname.Text.Length > 0)
            {
                Predicate<CensusIndividual> surnameFilter = FilterUtils.StringFilter<CensusIndividual>(x => x.Surname, txtSurname.Text);
                filter = FilterUtils.AndFilter<CensusIndividual>(filter, surnameFilter);
            }

            filter = FilterUtils.AndFilter<CensusIndividual>(x => x.Age.MinAge < (int)udAgeFilter.Value, filter);
            return filter;
        }

        private Predicate<Individual> CreateTreeTopsIndividualFilter()
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

        private void UpdateLostCousinsReport()
        {
            HourGlass(true);
            rtbLostCousins.Clear();
            Application.DoEvents();
            rtbLostCousins.AppendText("Lost Cousins facts recorded:\n\n");
            rtbLostCousins.SelectionStart = 0;
            rtbLostCousins.SelectionLength = rtbLostCousins.TextLength;
            rtbLostCousins.SelectionFont = new Font(rtbLostCousins.Font, FontStyle.Bold);
            rtbLostCousins.SelectionLength = 0;

            Predicate<Individual> relationFilter = relTypesLC.BuildFilter<Individual>(x => x.RelationType);
            IEnumerable<Individual> listToCheck = ft.AllIndividuals.Where(relationFilter).ToList();

            int countEW1841, countEW1881, countSco1881, countCan1881, countEW1911, countIre1911, countUS1880, countUS1940;
            int missingEW1841, missingEW1881, missingSco1881, missingCan1881, missingEW1911, missingIre1911, missingUS1880, missingUS1940;
            countEW1841 = countEW1881 = countSco1881 = countCan1881 = countEW1911 = countIre1911 = countUS1880 = countUS1940 = 0;
            missingEW1841 = missingEW1881 = missingSco1881 = missingCan1881 = missingEW1911 = missingIre1911 = missingUS1880 = missingUS1940 = 0;
            //int index = 0;
            foreach (Individual ind in listToCheck)
            {
                countEW1841 += (ind.IsLostCousinsEntered(CensusDate.EWCENSUS1841, false) ? 1 : 0);
                countEW1881 += (ind.IsLostCousinsEntered(CensusDate.EWCENSUS1881, false) ? 1 : 0);
                countSco1881 += (ind.IsLostCousinsEntered(CensusDate.SCOTCENSUS1881, false) ? 1 : 0);
                countCan1881 += (ind.IsLostCousinsEntered(CensusDate.CANADACENSUS1881, false) ? 1 : 0);
                countEW1911 += (ind.IsLostCousinsEntered(CensusDate.EWCENSUS1911, false) ? 1 : 0);
                countIre1911 += (ind.IsLostCousinsEntered(CensusDate.IRELANDCENSUS1911, false) ? 1 : 0);
                countUS1880 += (ind.IsLostCousinsEntered(CensusDate.USCENSUS1880, false) ? 1 : 0);
                countUS1940 += (ind.IsLostCousinsEntered(CensusDate.USCENSUS1940, false) ? 1 : 0);

                missingEW1841 += (ind.MissingLostCousins(CensusDate.EWCENSUS1841, false) ? 1 : 0);
                missingEW1881 += (ind.MissingLostCousins(CensusDate.EWCENSUS1881, false) ? 1 : 0);
                missingSco1881 += (ind.MissingLostCousins(CensusDate.SCOTCENSUS1881, false) ? 1 : 0);
                missingCan1881 += (ind.MissingLostCousins(CensusDate.CANADACENSUS1881, false) ? 1 : 0);
                missingEW1911 += (ind.MissingLostCousins(CensusDate.EWCENSUS1911, false) ? 1 : 0);
                missingIre1911 += (ind.MissingLostCousins(CensusDate.IRELANDCENSUS1911, false) ? 1 : 0);
                missingUS1880 += (ind.MissingLostCousins(CensusDate.USCENSUS1880, false) ? 1 : 0);
                missingUS1940 += (ind.MissingLostCousins(CensusDate.USCENSUS1940, false) ? 1 : 0);

                //if (ind.MissingLostCousins(CensusDate.EWCENSUS1841, false))
                //    Console.WriteLine(index++ + ": " + ind.ToString());
            }

            int moreThanOneLCfact = listToCheck.Sum(i => i.DuplicateLCFacts);
            int LCtotal = listToCheck.Sum(i => i.LostCousinsFacts);
            int total = countEW1841 + countEW1881 + countSco1881 + countCan1881 + countEW1911 + countIre1911 + countUS1880 + countUS1940 + moreThanOneLCfact;
            int missingtotal = missingEW1841 + missingEW1881 + missingSco1881 + missingCan1881 + missingEW1911 + missingIre1911 + missingUS1880 + missingUS1940;

            rtbLostCousins.AppendText("1881 England & Wales Census: " + countEW1881 + " Found, " + missingEW1881 + " Missing\n");
            rtbLostCousins.AppendText("1841 England & Wales Census: " + countEW1841 + " Found, " + missingEW1841 + " Missing\n");
            rtbLostCousins.AppendText("1911 England & Wales Census: " + countEW1911 + " Found, " + missingEW1911 + " Missing\n");
            rtbLostCousins.AppendText("_____________________________________________\n");
            rtbLostCousins.AppendText("1881 Scotland Census: " + countSco1881 + " Found, " + missingSco1881 + " Missing\n");
            rtbLostCousins.AppendText("_____________________________________________\n");
            rtbLostCousins.AppendText("1911 Ireland Census: " + countIre1911 + " Found, " + missingIre1911 + " Missing\n");
            rtbLostCousins.AppendText("_____________________________________________\n");
            rtbLostCousins.AppendText("1881 Canada Census: " + countCan1881 + " Found, " + missingCan1881 + " Missing\n");
            rtbLostCousins.AppendText("_____________________________________________\n");
            rtbLostCousins.AppendText("1880 US Census: " + countUS1880 + " Found, " + missingUS1880 + " Missing\n");
            rtbLostCousins.AppendText("1940 US Census: " + countUS1940 + " Found, " + missingUS1940 + " Missing\n");
            rtbLostCousins.AppendText("_____________________________________________\n");
            if(moreThanOneLCfact > 0)
                rtbLostCousins.AppendText("Duplicate LostCousins facts: " + moreThanOneLCfact + "\n");
            if (LCtotal > total)
                rtbLostCousins.AppendText("LostCousins fact with no country: " + (LCtotal - total) + "\n");
            if(moreThanOneLCfact > 0 || LCtotal > total)
                rtbLostCousins.AppendText("_____________________________________________\n");
            rtbLostCousins.AppendText("Totals: " + LCtotal + " Found, " + missingtotal + " Missing");

            if (missingtotal > 0)
            {
                int startpos = rtbLostCousins.TextLength;
                rtbLostCousins.AppendText("\n\nYou have " + missingtotal + " Census facts with no LostCousins fact");
                rtbLostCousins.AppendText("\nClick the Lost Cousins website link to add them today.");
                int endpos = rtbLostCousins.TextLength;
                rtbLostCousins.Select(startpos, endpos);
                rtbLostCousins.SelectionColor = Color.Red;
            }
            HourGlass(false);
        }

        private void LostCousinsCensus(CensusDate censusDate, string reportTitle)
        {
            HourGlass(true);
            Census census = new Census(censusDate);
            Predicate<CensusIndividual> relationFilter = relTypesLC.BuildFilter<CensusIndividual>(x => x.RelationType);
            census.SetupLCCensus(relationFilter, ckbShowLCEntered.Checked);
            if (ckbShowLCEntered.Checked)
                census.Text = reportTitle + " already entered into Lost Cousins website (includes entries with no country)";
            else
                census.Text = reportTitle + " to enter into Lost Cousins website";

            DisposeDuplicateForms(census);
            census.Show();
            HourGlass(false);
        }

        private void btnLCMissingCountry_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Predicate<Individual> relationFilter = relTypesLC.BuildFilter<Individual>(x => x.RelationType);
            People people = new People();
            people.SetupLCNoCountry(relationFilter);
            DisposeDuplicateForms(people); 
            people.Show();
            HourGlass(false);
        }

        private void relTypesLC_RelationTypesChanged(object sender, EventArgs e)
        {
            UpdateLostCousinsReport();
        }

        private void btnLCDuplicates_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            Predicate<Individual> relationFilter = relTypesLC.BuildFilter<Individual>(x => x.RelationType);
            People people = new People();
            people.SetupLCDuplicates(relationFilter);
            DisposeDuplicateForms(people);
            people.Show();
            HourGlass(false);
        }

        private void btnLCnoCensus_Click(object sender, EventArgs e)
        {
            HourGlass(true); 
            Predicate<Individual> relationFilter = relTypesLC.BuildFilter<Individual>(x => x.RelationType);
            People people = new People();
            people.SetupLCnoCensus(relationFilter);
            DisposeDuplicateForms(people);
            people.Show();
            HourGlass(false);
        }

        private void btnLC1881EW_Click(object sender, EventArgs e)
        {
            string reportTitle = "1881 England & Wales Census Records on file";
            LostCousinsCensus(CensusDate.EWCENSUS1881, reportTitle);
        }

        private void btnLC1881Scot_Click(object sender, EventArgs e)
        {
            string reportTitle = "1881 Scotland Census Records on file";
            LostCousinsCensus(CensusDate.SCOTCENSUS1881, reportTitle);
        }

        private void btnLC1881Canada_Click(object sender, EventArgs e)
        {
            string reportTitle = "1881 Canada Census Records on file";
            LostCousinsCensus(CensusDate.CANADACENSUS1881, reportTitle);
        }

        private void btnLC1841EW_Click(object sender, EventArgs e)
        {
            string reportTitle = "1841 England & Wales Census Records on file";
            LostCousinsCensus(CensusDate.EWCENSUS1841, reportTitle);
        }


        private void btnLC1911EW_Click(object sender, EventArgs e)
        {
            string reportTitle = "1911 England & Wales Census Records on file";
            LostCousinsCensus(CensusDate.EWCENSUS1911, reportTitle);
        }

        private void btnLC1880USA_Click(object sender, EventArgs e)
        {
            string reportTitle = "1880 US Census Records on file";
            LostCousinsCensus(CensusDate.USCENSUS1880, reportTitle);
        }

        private void btnLC1911Ireland_Click(object sender, EventArgs e)
        {
            string reportTitle = "1911 Ireland Census Records on file";
            LostCousinsCensus(CensusDate.IRELANDCENSUS1911, reportTitle);
        }

        private void btnLC1940USA_Click(object sender, EventArgs e)
        {
            string reportTitle = "1940 US Census Records on file";
            LostCousinsCensus(CensusDate.USCENSUS1940, reportTitle);
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

        #region ToolStrip Clicks

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UserControls.Options options = new UserControls.Options();
            options.ShowDialog(this);
            options.Dispose();
        }

        #endregion

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.lostcousins.com/?ref=LC585149");
        }

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
            Predicate<Individual> filter = CreateTreeTopsIndividualFilter();
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
            else if (tabSelector.SelectedTab == tabLooseBirthDeaths)
            {
                if (tabCtrlLooseBDs.SelectedTab == tabLooseBirths)
                    PrintDataGrid(true, dgLooseBirths, "List of Loose Births");
                else if (tabCtrlLooseBDs.SelectedTab == tabLooseDeaths)
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
            int locType = GetMapLocationType(out loc);
            if (loc != null)
            {   // Do geo coding stuff
                GoogleMap frmGoogleMap = new GoogleMap();
                if (frmGoogleMap.SetLocation(loc, locType))
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
            int locType = GetMapLocationType(out loc);
            if (loc != null)
            {   // Do geo coding stuff
                BingOSMap frmBingMap = new BingOSMap();
                if (frmBingMap.SetLocation(loc, locType))
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

        private int GetMapLocationType(out FactLocation loc)
        {
            // get the tab
            int locType = FactLocation.UNKNOWN;
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
                    break;
                case "Regions":
                    loc = dgRegions.CurrentRow == null ? null : (FactLocation)dgRegions.CurrentRow.DataBoundItem;
                    break;
                case "SubRegions":
                    loc = dgSubRegions.CurrentRow == null ? null : (FactLocation)dgSubRegions.CurrentRow.DataBoundItem;
                    break;
                case "Addresses":
                    loc = dgAddresses.CurrentRow == null ? null : (FactLocation)dgAddresses.CurrentRow.DataBoundItem;
                    break;
                case "Places":
                    loc = dgPlaces.CurrentRow == null ? null : (FactLocation)dgPlaces.CurrentRow.DataBoundItem;
                    break;
            }
            if (loc == null)
            {
                if (tabCtrlLocations.SelectedTab.Text == "Tree View")
                    MessageBox.Show("Location selected isn't valid to show on the map.");
                else
                    MessageBox.Show("Nothing selected. Please select a location to show on the map.");
                return locType;
            }
            if (locType == FactLocation.UNKNOWN)
                return loc.Level;
            else
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
            int index = 0;
            foreach (DataErrorGroup dataError in ckbDataErrors.Items)
            {
                bool itemChecked = ckbDataErrors.GetItemChecked(index++);
                Application.UserAppDataRegistry.SetValue(dataError.ToString(), itemChecked);
            }
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

        private void tabCtrlLocations_Selecting(object sender, TabControlCancelEventArgs e)
        {
            HourGlass(true); // turn on when tab selected so all the formatting gets hourglass
        }

        private void tabCtrlLocations_SelectedIndexChanged(object sender, EventArgs e)
        {
            HourGlass(true);
            Application.DoEvents();
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
            DataGridViewCell cell = grid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            if (e.ColumnIndex == 0)
            {
                string country = (string)cell.Value;
                if (Countries.IsKnownCountry(country))
                    e.CellStyle.Font = boldFont;
            }
            else
            {
                FactLocation loc = grid.Rows[e.RowIndex].DataBoundItem as FactLocation;
                cell.ToolTipText = "Geocoding Status : " + loc.Geocoded;
            }
        }

        private void dgCountries_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == dgCountries.Columns["Icon"].Index)
            {
                FormatCellLocations(dgCountries, e);
            }
        }

        private void dgRegions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == dgCountries.Columns["Icon"].Index)
            {
                FormatCellLocations(dgRegions, e);
            }
        }

        private void dgSubRegions_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == dgCountries.Columns["Icon"].Index)
            {
                FormatCellLocations(dgSubRegions, e);
            }
        }

        private void dgAddresses_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == dgCountries.Columns["Icon"].Index)
            {
                FormatCellLocations(dgAddresses, e);
            }
        }

        private void dgPlaces_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 0 || e.ColumnIndex == dgCountries.Columns["Icon"].Index)
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
            QueryReloadData();
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
            QueryReloadData();
        }

        private void Options_MinimumParentalAgeChanged(object sender, EventArgs e)
        {
            ft.ResetLooseFacts();
            if (tabSelector.SelectedTab == tabLooseBirthDeaths)
                UpdateLooseBirthDeaths();
        }

        private void QueryReloadData()
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

        private void dgLooseBirths_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                ShowFacts((string)dgLooseBirths.CurrentRow.Cells["Ind_ID"].Value);
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
                GeocodeLocations geo = null;
                foreach (Form f in Application.OpenForms)
                {
                    if (f.GetType() == typeof(GeocodeLocations))
                    {
                        geo = f as GeocodeLocations;
                        break;
                    }
                }
                if (geo == null)
                    geo = new GeocodeLocations();
                geo.Show();
                geo.Focus();
                geo.StartGeoCoding();
                HourGlass(false);
            }
        }

        private void locationsGeocodeReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            HourGlass(true);
            GeocodeLocations geo = new GeocodeLocations();
            geo.Show();
            DisposeDuplicateForms(geo);
            HourGlass(false);
        }

        private void treeViewLocations_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (treeViewLocations.SelectedNode != e.Node && e.Button.Equals(MouseButtons.Right))
                treeViewLocations.SelectedNode = e.Node;
        }

        private void treeViewLocations_AfterSelect(object sender, TreeViewEventArgs e)
        {
            treeViewLocations.SelectedImageIndex = e.Node.ImageIndex;
        }

        private void ckbRestrictions_CheckedChanged(object sender, EventArgs e)
        {
            UpdateLostCousinsReport();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ft.Geocoding)
                MessageBox.Show("You need to stop Geocoding before you can export the database");
            else
            {
                string directory = Application.UserAppDataRegistry.GetValue("Geocode Backup Directory", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).ToString();
                saveDatabase.FileName = "FTAnalyzer-Geocodes-" + DateTime.Now.ToString("yyyy-MM-dd") + ".zip";
                saveDatabase.InitialDirectory = directory;
                DialogResult result = saveDatabase.ShowDialog();
                if (result == DialogResult.OK)
                {
                    DatabaseHelper dbh = DatabaseHelper.Instance;
                    dbh.StartBackupDatabase();
                    if (File.Exists(saveDatabase.FileName))
                        File.Delete(saveDatabase.FileName);
                    ZipFile zip = new ZipFile(saveDatabase.FileName);
                    zip.AddFile(dbh.Filename, string.Empty);
                    zip.Comment = "FT Analyzer zip file created by v" + PublishVersion() + " on " + DateTime.Now.ToString("dd MMM yyyy HH:mm");
                    zip.Save();
                    dbh.EndBackupDatabase();
                    Application.UserAppDataRegistry.SetValue("Geocode Backup Directory", Path.GetDirectoryName(saveDatabase.FileName));
                    MessageBox.Show("Database exported to " + saveDatabase.FileName);
                }
            }
        }

        private void clearRecentFileListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearRecentList();
            BuildRecentList();
        }

        private static void ClearRecentList()
        {
            Properties.Settings.Default.RecentFiles.Clear();
            for (int i = 0; i < 5; i++)
            {
                Properties.Settings.Default.RecentFiles.Add(string.Empty);
            }
            Properties.Settings.Default.Save();
        }

        private void BuildRecentList()
        {
            if (Properties.Settings.Default.RecentFiles == null || Properties.Settings.Default.RecentFiles.Count != 5)
            {
                ClearRecentList();
            }

            bool added = false;
            for (int i = 0; i < 5; i++)
            {
                string name = Properties.Settings.Default.RecentFiles[i];
                if (name != null && name.Length > 0)
                {
                    added = true;
                    mnuRecent.DropDownItems[i].Visible = true;
                    mnuRecent.DropDownItems[i].Text = (i + 1) + ". " + name;
                    mnuRecent.DropDownItems[i].Tag = name;
                }
                else
                {
                    mnuRecent.DropDownItems[i].Visible = false;
                }
            }

            toolStripSeparator7.Visible = added;
            clearRecentFileListToolStripMenuItem.Visible = added;
        }

        private void AddFileToRecentList(string filename)
        {
            string[] recent = new string[5];

            if (Properties.Settings.Default.RecentFiles != null)
            {
                int j = 1;
                for (int i = 0; i < Properties.Settings.Default.RecentFiles.Count; i++)
                {
                    if (Properties.Settings.Default.RecentFiles[i] != filename)
                    {
                        recent[j++] = Properties.Settings.Default.RecentFiles[i];
                        if (j == 5) break;
                    }
                }
            }

            recent[0] = filename;
            Properties.Settings.Default.RecentFiles = new StringCollection();
            Properties.Settings.Default.RecentFiles.AddRange(recent);
            Properties.Settings.Default.Save();

            BuildRecentList();
        }

        private void OpenRecentFile_Click(object sender, EventArgs e)
        {
            string filename = (string)(sender as ToolStripMenuItem).Tag;
            LoadFile(filename);
        }

        private void dgIndividuals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string indID = (string)dgIndividuals.CurrentRow.Cells["Ind_ID"].Value;
            Individual ind = ft.GetIndividual(indID);
            Facts factForm = new Facts(ind);
            DisposeDuplicateForms(factForm);
            factForm.Show();
        }

        private void buildLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (KeyValuePair<string, string> shift in FactLocation.COUNTRY_SHIFTS)
            {
                FactLocation.GetLocation(shift.Key + ", " + shift.Value);
            }
            ft.LoadGeoLocationsFromDataBase();
            GeocodeLocations gl = new GeocodeLocations();
            gl.Show();
        }
    }
}
