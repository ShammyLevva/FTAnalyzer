using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Utilities;
using FTAnalyzer.Filters;

namespace FTAnalyzer.Forms
{
    public partial class People : Form
    {
        private enum ReportType { People, MissingChildrenStatus, MismatchedChildrenStatus } 
        
        private bool selectRow = false;
        private Dictionary<IDisplayIndividual, IDisplayFamily> families;
        private FamilyTree ft = FamilyTree.Instance;
        private ReportFormHelper indReportFormHelper;
        private ReportFormHelper famReportFormHelper;
        private ReportType reportType = ReportType.People;

        public People()
        {
            InitializeComponent();
            indReportFormHelper = new ReportFormHelper(this, this.Text, dgIndividuals, this.ResetTable, "People");
            famReportFormHelper = new ReportFormHelper(this, this.Text, dgFamilies, this.ResetTable, "People");
            ExtensionMethods.DoubleBuffered(dgIndividuals, true);
            ExtensionMethods.DoubleBuffered(dgFamilies, true);
        }

        private void UpdateStatusCount()
        {
            if (splitContainer.Panel2Collapsed)
                txtCount.Text = "Count: " + dgIndividuals.RowCount + " Individuals.  " + Properties.Messages.Hints_Individual;
            else
            {
                txtCount.Text = "Count: " + dgIndividuals.RowCount + " Individuals and " + dgFamilies.RowCount + " Families. " + Properties.Messages.Hints_IndividualFamily;
            }
        }

        public void SetLocation(FactLocation loc, int level)
        {
            this.Text = "Individuals & Families with connection to " + loc.ToString();
            level = Math.Min(loc.Level, level); // if location level isn't as detailed as level on tab use location level
            IEnumerable<Individual> listInd = ft.GetIndividualsAtLocation(loc, level);
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            foreach (Individual i in listInd)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();

            IEnumerable<Family> listFam = ft.GetFamiliesAtLocation(loc, level);
            SortableBindingList<IDisplayFamily> dsFam = new SortableBindingList<IDisplayFamily>();
            foreach (Family f in listFam)
                dsFam.Add(f);
            dgFamilies.DataSource = dsFam;
            SortFamilies();
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void SetWorkers(string job, SortableBindingList<Individual> workers)
        {
            this.Text = "Individuals whose occupation was " + (job.Length == 0 ? "not entered" : job);
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            foreach (Individual i in workers)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();
            dgIndividuals.Dock = DockStyle.Fill;
            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }

        public void SetSurnameStats(SurnameStats stat)
        {
            this.Text = "Individuals & Families whose surame is " + stat.Surname;
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            Predicate<Individual> indSurnames = x => x.Surname.Equals(stat.Surname);
            foreach (Individual i in ft.AllIndividuals.Where(indSurnames))
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();
            dgIndividuals.Dock = DockStyle.Fill;

            Predicate<Family> famSurnames = x => x.ContainsSurname(stat.Surname);
            SortableBindingList<IDisplayFamily> dsFam = new SortableBindingList<IDisplayFamily>();
            foreach (Family f in ft.AllFamilies.Where(famSurnames))
                dsFam.Add(f);
            dgFamilies.DataSource = dsFam;
            SortFamilies();
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void SetupLCDuplicates(Predicate<Individual> relationFilter)
        {
            Predicate<Individual> lcFacts = i => i.DuplicateLCFacts > 0;
            Predicate<Individual> filter = FilterUtils.AndFilter<Individual>(relationFilter, lcFacts);
            List<Individual> individuals = ft.AllIndividuals.Where(filter).ToList();
            SetIndividuals(individuals, "Lost Cousins with Duplicate Facts");
        }

        public void SetupLCnoCensus(Predicate<Individual> relationFilter)
        {
            List<Individual> listtoCheck = ft.AllIndividuals.Where(relationFilter).ToList();
            List<Individual> individuals = new List<Individual>();
            foreach (CensusDate censusDate in CensusDate.LOSTCOUSINS_CENSUS)
            {
                Predicate<Individual> lcFacts = new Predicate<Individual>(i => i.LostCousinsCensusFactCount - i.MissingLostCousinsCount - i.LostCousinsFacts != 0);
                IEnumerable<Individual> censusMissing = listtoCheck.Where(lcFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct<Individual>().ToList();
            SetIndividuals(individuals, "Lost Cousins with no corresponding census entry");
        }

        public void SetupLCNoCountry(Predicate<Individual> relationFilter)
        {
            Predicate<Individual> lcFacts = x => x.LostCousinsFacts > 0;
            Predicate<Individual> filter = FilterUtils.AndFilter<Individual>(relationFilter, lcFacts);
            IEnumerable<Individual> listToCheck = ft.AllIndividuals.Where(filter).ToList();

            Predicate<Individual> missing = x => !x.IsLostCousinsEntered(CensusDate.EWCENSUS1841, false)
                                              && !x.IsLostCousinsEntered(CensusDate.EWCENSUS1881, false)
                                              && !x.IsLostCousinsEntered(CensusDate.SCOTCENSUS1881, false)
                                              && !x.IsLostCousinsEntered(CensusDate.CANADACENSUS1881, false)
                                              && !x.IsLostCousinsEntered(CensusDate.EWCENSUS1911, false)
                                              && !x.IsLostCousinsEntered(CensusDate.IRELANDCENSUS1911, false)
                                              && !x.IsLostCousinsEntered(CensusDate.USCENSUS1880, false)
                                              && !x.IsLostCousinsEntered(CensusDate.USCENSUS1940, false);
            List<Individual> individuals = listToCheck.Where(missing).ToList<Individual>();
            SetIndividuals(individuals, "Lost Cousins with No Country");
        }

        private void SortIndividuals()
        {
            //indReportFormHelper.LoadColumnLayout("PeopleIndColumns.xml");
            dgIndividuals.Sort(dgIndividuals.Columns[1], ListSortDirection.Ascending);
            dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
        }

        private void SortFamilies()
        {
            //famReportFormHelper.LoadColumnLayout("PeopleFamColumns.xml");
            dgFamilies.Sort(dgFamilies.Columns[0], ListSortDirection.Ascending);
        }

        public void SetIndividuals(List<Individual> individuals, string reportTitle)
        {
            this.Text = reportTitle;
            dgIndividuals.DataSource = new SortableBindingList<IDisplayIndividual>(individuals);
            dgIndividuals.Dock = DockStyle.Fill;

            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }

        public bool OlderParents(int minAge)
        {
            this.Text = "Parents aged " + minAge + "+ at time of child's birth";
            selectRow = true;
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            SortableBindingList<IDisplayFamily> dsFam = new SortableBindingList<IDisplayFamily>();
            families = new Dictionary<IDisplayIndividual, IDisplayFamily>();
            foreach (Family f in ft.AllFamilies)
            {
                bool added = false;
                foreach (Individual child in f.Children)
                {
                    if (child.BirthDate.IsKnown)
                    {
                        if (f.Husband != null && f.Husband.BirthDate.IsKnown)
                        {
                            Age age = f.Husband.GetAge(child.BirthDate);
                            if (age.MinAge >= minAge && !dsInd.Contains(f.Husband))
                            {
                                dsInd.Add(f.Husband);
                                families.Add(f.Husband, f);
                                added = true;
                            }
                        }
                        if (f.Wife != null && f.Wife.BirthDate.IsKnown)
                        {
                            Age age = f.Wife.GetAge(child.BirthDate);
                            if (age.MinAge >= minAge && !dsInd.Contains(f.Wife))
                            {
                                dsInd.Add(f.Wife);
                                families.Add(f.Wife, f);
                                added = true;
                            }
                        }
                    }
                }
                if (added && !dsFam.Contains(f))
                {
                    dsFam.Add(f);
                }
            }
            if (dsInd.Count > 0)
            {
                dgIndividuals.DataSource = dsInd;
                SortIndividuals();
                dgIndividuals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgFamilies.DataSource = dsFam;
                SortFamilies();
                dgFamilies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgIndividuals.Rows[0].Selected = true; // force a selection to update both grids
                return true;
            }
            else
            {
                MessageBox.Show("You have no parents older than " + minAge + " at time of children's birth.", "FT Analyzer");
                return false;
            }
        }

        private void dgIndividuals_SelectionChanged(object sender, EventArgs e)
        {
            if (selectRow && dgIndividuals.CurrentRow != null)
            {
                IDisplayFamily f;
                IDisplayIndividual ind = (IDisplayIndividual)dgIndividuals.CurrentRow.DataBoundItem;
                families.TryGetValue(ind, out f);
                if (f != null)
                {
                    foreach (DataGridViewRow r in dgFamilies.Rows)
                    {
                        if (r.Cells[0].Value.ToString() == f.FamilyID)
                        {
                            dgFamilies.CurrentCell = r.Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        private void dgIndividuals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
                Individual ind = ft.GetIndividual(indID);
                Facts factForm = new Facts(ind);
                factForm.Show();
            }
        }

        private void dgFamilies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string famID = (string)dgFamilies.CurrentRow.Cells["FamilyID"].Value;
                Family fam = ft.GetFamily(famID);
                if (fam != null)
                {
                    Facts factForm = new Facts(fam);
                    MainForm.DisposeDuplicateForms(factForm);
                    factForm.Show();
                }
            }
        }

        public void SetupMissingCensusLocation()
        {
            List<Individual> individuals = new List<Individual>();
            foreach (CensusDate censusDate in CensusDate.SUPPORTED_CENSUS)
            {
                Predicate<Individual> censusFacts = new Predicate<Individual>(x => x.IsCensusDone(censusDate) && !x.HasCensusLocation(censusDate));
                IEnumerable<Individual> censusMissing = ft.AllIndividuals.Where(censusFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct<Individual>().ToList();
            SetIndividuals(individuals, "Individuals with census records with no census location");
        }

        public void SetupDuplicateCensus()
        {
            List<Individual> individuals = new List<Individual>();
            foreach (CensusDate censusDate in CensusDate.SUPPORTED_CENSUS)
            {
                Predicate<Individual> censusFacts = new Predicate<Individual>(i => i.CensusDateFactCount(censusDate) > 1);
                IEnumerable<Individual> censusMissing = ft.AllIndividuals.Where(censusFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct<Individual>().ToList();
            SetIndividuals(individuals, "Individuals that may have more than one census/residence record for a census year");
        }

        private void dgIndividuals_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
                Individual ind = ft.GetIndividual(indID);
                Facts factForm = new Facts(ind);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        private void dgFamilies_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string famID = (string)dgFamilies.CurrentRow.Cells["FamilyID"].Value;
                Family fam = ft.GetFamily(famID);
                if (fam != null)
                {
                    Facts factForm = new Facts(fam);
                    MainForm.DisposeDuplicateForms(factForm);
                    factForm.Show();
                }
            }
        }

        private void People_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void contextMenuStrip1_Opened(object sender, EventArgs e)
        {
            string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
            Individual ind = ft.GetIndividual(indID);
            if (ind != null)
                viewNotesToolStripMenuItem.Enabled = ind.HasNotes;
        }

        private void viewNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
            Individual ind = ft.GetIndividual(indID);
            if (ind != null)
            {
                Notes notes = new Notes(ind);
                notes.Show();
            }
        }

        private void ShowViewNotesMenu(DataGridView dg, MouseEventArgs e)
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
                        ctxViewNotes.Tag = dg.CurrentRow.DataBoundItem;
                        ctxViewNotes.Show(MousePosition);
                    }
                }
            }
        }

        private void ResetTable()
        {
            SortIndividuals();
            if (!splitContainer.Panel2Collapsed)
                SortFamilies();
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            indReportFormHelper.SaveColumnLayout("PeopleIndColumns.xml");
            famReportFormHelper.SaveColumnLayout("PeopleFamColumns.xml");
            MessageBox.Show("Form Settings Saved", "People");
        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {
            indReportFormHelper.ResetColumnLayout("PeopleIndColumns.xml");
            famReportFormHelper.ResetColumnLayout("PeopleFamColumns.xml");
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            indReportFormHelper.PrintReport(this.Text);
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.PrintReport(this.Text + " - Families");
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            indReportFormHelper.PrintPreviewReport();
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.PrintPreviewReport();
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            indReportFormHelper.DoExportToExcel<IDisplayIndividual>();
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.DoExportToExcel<IDisplayFamily>();
        }

        private void dgIndividuals_MouseDown(object sender, MouseEventArgs e)
        {
            ShowViewNotesMenu(dgIndividuals, e);
        }

        public void SetupNoChildrenStatus()
        {
            SortableBindingList<IDisplayFamily> results = new SortableBindingList<IDisplayFamily>();
            IEnumerable<CensusFamily> toSearch = ft.GetAllCensusFamilies(CensusDate.UKCENSUS1911, true, true);
            foreach (Family f in toSearch)
            {
                if (f.On1911Census && !f.HasChildrenStatus)
                    results.Add(f);
            }
            dgFamilies.DataSource = results;
            SortFamilies();
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
            reportType = ReportType.MissingChildrenStatus;
            this.Text = "Families with a 1911 census record but no Children Status record showing Children Alive/Dead";
        }

        public void SetupChildrenStatusReport()
        {
            SortableBindingList<IDisplayChildrenStatus> results = new SortableBindingList<IDisplayChildrenStatus>();
            IEnumerable<CensusFamily> toSearch = ft.GetAllCensusFamilies(CensusDate.UKCENSUS1911, true, true);
            foreach (CensusFamily f in toSearch)
            {
                if (f.On1911Census && f.HasChildrenStatus && 
                    (f.ExpectedTotal != f.ChildrenTotal || f.ExpectedAlive != f.ChildrenAlive || f.ExpectedDead != f.ChildrenDead))
                    results.Add(f);
            }
            dgFamilies.DataSource = results;
            SortFamilies();
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
            reportType = ReportType.MismatchedChildrenStatus;
            this.Text = "1911 Census Families where the children status recorded doesn't match the children in tree";
        }

        private void dgFamilies_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (reportType == ReportType.MissingChildrenStatus)
                e.CellStyle.BackColor = Color.Wheat;
            else if (reportType == ReportType.MismatchedChildrenStatus)
            {
                e.CellStyle.BackColor = Color.Wheat;
                DataGridViewCellCollection cells = dgFamilies.Rows[e.RowIndex].Cells;
                switch (e.ColumnIndex)
                {
                    case 5: // Totals
                    case 8:
                        e.CellStyle.BackColor = cells["ChildrenTotal"].Value.Equals(cells["ExpectedTotal"].Value) ? Color.Wheat : Color.OrangeRed;
                        break;
                    case 6: // Alive
                    case 9:
                        e.CellStyle.BackColor = cells["ChildrenAlive"].Value.Equals(cells["ExpectedAlive"].Value) ? Color.Wheat : Color.OrangeRed;
                        break;
                    case 7: // ChildrenTotal
                    case 10:
                        e.CellStyle.BackColor = cells["ChildrenDead"].Value.Equals(cells["ExpectedDead"].Value) ? Color.Wheat : Color.OrangeRed;
                        break;
                }
            }
        }
    }
}
