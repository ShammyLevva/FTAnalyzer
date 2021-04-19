using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FTAnalyzer.Utilities;
using FTAnalyzer.Filters;

namespace FTAnalyzer.Forms
{
    public partial class People : Form
    {
        enum ReportType { People, MissingChildrenStatus, MismatchedChildrenStatus }

        bool selectRow;
        readonly Font boldFont;
        readonly Font normalFont;
        Dictionary<IDisplayIndividual, IDisplayFamily> families;
        readonly FamilyTree ft = FamilyTree.Instance;
        readonly ReportFormHelper indReportFormHelper;
        readonly ReportFormHelper famReportFormHelper;
        ReportType reportType = ReportType.People;

        public People()
        {
            try
            {
                InitializeComponent();
                Top += NativeMethods.TopTaskbarOffset;
                indReportFormHelper = new ReportFormHelper(this, Text, dgIndividuals, ResetTable, "People");
                famReportFormHelper = new ReportFormHelper(this, Text, dgFamilies, ResetTable, "People");
                ExtensionMethods.DoubleBuffered(dgIndividuals, true);
                ExtensionMethods.DoubleBuffered(dgFamilies, true);
                boldFont = new Font(dgFamilies.DefaultCellStyle.Font, FontStyle.Bold);
                normalFont = new Font(dgFamilies.DefaultCellStyle.Font, FontStyle.Regular);
                SetSaveButtonsStatus(false);
            }
            catch (Exception) { }
        }

        void SetSaveButtonsStatus(bool value)
        {
            mnuSaveColumnLayout.Visible = value;
            mnuResetColumns.Visible = value;
            tssSaveButtons.Visible = value;
        }

        void UpdateStatusCount()
        {
            if (reportType == ReportType.MissingChildrenStatus || reportType == ReportType.MismatchedChildrenStatus)
                txtCount.Text = $"{dgFamilies.RowCount} Problems detected. {Properties.Messages.Hints_IndividualFamily} Shift Double click to see colour census report for family.";
            else
            {
                if (splitContainer.Panel2Collapsed)
                    txtCount.Text = $"Count: {dgIndividuals.RowCount} Individuals.  {Properties.Messages.Hints_Individual}";
                else
                    txtCount.Text = $"Count: {dgIndividuals.RowCount} Individuals and {dgFamilies.RowCount} Families. {Properties.Messages.Hints_IndividualFamily}";
            }
        }

        public void SetLocation(FactLocation loc, int level)
        {
            Text = $"Individuals & Families with connection to {loc}";
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
            Text = "Individuals whose occupation was " + (job.Length == 0 ? "not entered" : job);
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            foreach (Individual i in workers)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();
            dgIndividuals.Dock = DockStyle.Fill;
            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }
        public void SetCustomFacts(string factType, SortableBindingList<Individual> individuals)
        {
            Text = "Individuals whose have the custom fact of " + (string.IsNullOrEmpty(factType) ? "not entered" : factType);
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            foreach (Individual i in individuals)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();
            dgIndividuals.Dock = DockStyle.Fill;
            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }

        public void SetSurnameStats(IDisplaySurnames stat, bool ignoreCase)
        {
            Text = $"Individuals & Families whose surame is {stat.Surname}";
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            bool indSurnames(Individual x) => x.Surname.Equals(stat.Surname);
            foreach (Individual i in ft.AllIndividuals.Filter(indSurnames))
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();
            dgIndividuals.Dock = DockStyle.Fill;

            bool famSurnames(Family x) => x.ContainsSurname(stat.Surname, ignoreCase);
            SortableBindingList<IDisplayFamily> dsFam = new SortableBindingList<IDisplayFamily>();
            foreach (Family f in ft.AllFamilies.Filter(famSurnames))
                dsFam.Add(f);
            dgFamilies.DataSource = dsFam;
            SortFamilies();
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void SetupLCDuplicates(Predicate<Individual> relationFilter)
        {
            bool lcFacts(Individual i) => i.DuplicateLCFacts > 0;
            Predicate<Individual> filter = FilterUtils.AndFilter<Individual>(relationFilter, lcFacts);
            List<Individual> individuals = ft.AllIndividuals.Filter(filter).ToList();
            SetIndividuals(individuals, "Lost Cousins with Duplicate Facts");
        }

        public void SetupLCnoCensus(Predicate<Individual> relationFilter)
        {
            List<Individual> listtoCheck = ft.AllIndividuals.Filter(relationFilter).ToList();
            List<Individual> individuals = new List<Individual>();
            Predicate<Individual> lcFacts = new Predicate<Individual>(i => i.HasLostCousinsFactWithNoCensusFact);
            IEnumerable<Individual> censusMissing = listtoCheck.Filter(lcFacts);
            individuals.AddRange(censusMissing);
            individuals = individuals.Distinct<Individual>().ToList();
            SetIndividuals(individuals, "Lost Cousins facts with no corresponding census entry");
        }

        public void SetupLCNoCountry(Predicate<Individual> relationFilter)
        {
            bool lcFacts(Individual x) => x.LostCousinsFacts > 0;
            Predicate<Individual> filter = FilterUtils.AndFilter(relationFilter, lcFacts);
            IEnumerable<Individual> listToCheck = ft.AllIndividuals.Filter(filter).ToList();

            bool missing(Individual x) => !x.IsLostCousinsEntered(CensusDate.EWCENSUS1841, false)
                                       && !x.IsLostCousinsEntered(CensusDate.EWCENSUS1881, false)
                                       && !x.IsLostCousinsEntered(CensusDate.SCOTCENSUS1881, false)
                                       && !x.IsLostCousinsEntered(CensusDate.CANADACENSUS1881, false)
                                       && !x.IsLostCousinsEntered(CensusDate.EWCENSUS1911, false)
                                       && !x.IsLostCousinsEntered(CensusDate.IRELANDCENSUS1911, false)
                                       && !x.IsLostCousinsEntered(CensusDate.USCENSUS1880, false)
                                       && !x.IsLostCousinsEntered(CensusDate.USCENSUS1940, false);
            List<Individual> individuals = listToCheck.Filter(missing).ToList<Individual>();
            SetIndividuals(individuals, "Lost Cousins facts with no facts found to identify Country");
        }

        public void SetupPossiblyMissingChildrenReport()
        {
            Text = "Families who might be missing a child between marriage date and first child born.";
            dgFamilies.DataSource = ft.PossiblyMissingChildFamilies;
            SortFamilies();
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void SetupAgedOver99Report()
        {
            Text = "Invididuals Aged over 99 in file.";
            dgIndividuals.DataSource = ft.AgedOver99;
            SortIndividuals();
            splitContainer.Panel1Collapsed = false;
            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }

        public void SetupAliveAtDate(FactDate aliveAtDate, Predicate<Individual> filter)
        {
            Text = $"Individuals alive {aliveAtDate}";
            var result = new SortableBindingList<IDisplayIndividual>();
            IEnumerable<Individual> list = ft.AllIndividuals.Filter(filter);
            foreach(Individual i in list)
                result.Add(i);
            dgIndividuals.DataSource = result;
            SortIndividuals();
            splitContainer.Panel1Collapsed = false;
            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }

        public void SingleParents()
        {
            Text = "Families with only one or no parent";
            dgFamilies.DataSource = ft.SingleFamilies;
            SortFamilies();
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void ListRelationToRoot(string relationtoRoot)
        {
            bool filter(Individual x) => x.RelationToRoot.Equals(relationtoRoot);
            List<Individual> individuals = ft.AllIndividuals.Filter(filter).ToList();
            SetIndividuals(individuals, "Individuals who are a " + relationtoRoot + " of root person");
        }

        void SortIndividuals()
        {
            if (dgIndividuals.Columns.Count > 0)
            {
                dgIndividuals.Sort(dgIndividuals.Columns[1], ListSortDirection.Ascending);
                dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
            }
        }

        void SortFamilies() => dgFamilies.Sort(dgFamilies.Columns[0], ListSortDirection.Ascending);

        public void SetIndividuals(List<Individual> individuals, string reportTitle)
        {
            Text = reportTitle;
            dgIndividuals.DataSource = new SortableBindingList<IDisplayIndividual>(individuals);
            dgIndividuals.Dock = DockStyle.Fill;

            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }

        public bool OlderParents(int minAge)
        {
            Text = "Parents aged " + minAge + "+ at time of child's birth";
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
                MessageBox.Show("You have no parents older than " + minAge + " at time of children's birth.", "FTAnalyzer");
                return false;
            }
        }

        void DgIndividuals_SelectionChanged(object sender, EventArgs e)
        {
            if (selectRow && dgIndividuals.CurrentRow != null)
            {
                IDisplayIndividual ind = (IDisplayIndividual)dgIndividuals.CurrentRow.DataBoundItem;
                families.TryGetValue(ind, out IDisplayFamily f);
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

        void DgIndividuals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
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

        void DgFamilies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string famID = (string)dgFamilies.CurrentRow.Cells["FamilyID"].Value;
                Family fam = ft.GetFamily(famID);
                if (fam != null)
                {
                    if ((reportType == ReportType.MismatchedChildrenStatus || reportType == ReportType.MissingChildrenStatus) && ModifierKeys.Equals(Keys.Shift))
                    {
                        List<IDisplayColourCensus> list = fam.Members.ToList<IDisplayColourCensus>();
                        ColourCensus rs = new ColourCensus(Countries.UNITED_KINGDOM, list);
                        MainForm.DisposeDuplicateForms(rs);
                        rs.Show();
                        rs.Focus();
                    }
                    else
                    {
                        Facts factForm = new Facts(fam);
                        MainForm.DisposeDuplicateForms(factForm);
                        factForm.Show();
                    }
                }
            }
        }

        public void SetupMissingCensusLocation()
        {
            List<Individual> individuals = new List<Individual>();
            foreach (CensusDate censusDate in CensusDate.SUPPORTED_CENSUS)
            {
                Predicate<Individual> censusFacts = new Predicate<Individual>(x => x.IsCensusDone(censusDate) && !x.HasCensusLocation(censusDate));
                IEnumerable<Individual> censusMissing = ft.AllIndividuals.Filter(censusFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct().ToList();
            SetIndividuals(individuals, "Individuals with census records with no census location");
        }

        public void SetupDuplicateCensus()
        {
            List<Individual> individuals = new List<Individual>();
            foreach (CensusDate censusDate in CensusDate.SUPPORTED_CENSUS)
            {
                Predicate<Individual> censusFacts = new Predicate<Individual>(i => i.CensusDateFactCount(censusDate) > 1);
                IEnumerable<Individual> censusMissing = ft.AllIndividuals.Filter(censusFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct().ToList();
            SetIndividuals(individuals, "Individuals that may have more than one census/residence record for a census year");
        }

        public void SetupChildrenStatusReport()
        {
            SortableBindingList<IDisplayChildrenStatus> results = new SortableBindingList<IDisplayChildrenStatus>();
            IEnumerable<CensusFamily> toSearch = ft.GetAllCensusFamilies(CensusDate.UKCENSUS1911, true, true);
            foreach (CensusFamily fam in toSearch)
            {
                if (fam.On1911Census && fam.HasGoodChildrenStatus && !fam.FamilyType.Equals(Family.SOLOINDIVIDUAL) && !fam.FamilyType.Equals(Family.PRE_MARRIAGE) &&
                    (fam.ExpectedTotal != fam.ChildrenTotal || fam.ExpectedAlive != fam.ChildrenAlive || fam.ExpectedDead != fam.ChildrenDead))
                    results.Add(fam);
            }
            reportType = ReportType.MismatchedChildrenStatus;
            dgFamilies.DataSource = results;
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            famReportFormHelper.LoadColumnLayout("ChildrenStatusFamColumns.xml");
            SetSaveButtonsStatus(true);
            Text = "1911 Census Families where the children status recorded doesn't match the children in tree";
            UpdateStatusCount();
        }

        void People_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void ContextMenuStrip1_Opened(object sender, EventArgs e)
        {
            string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
            Individual ind = ft.GetIndividual(indID);
            if (ind != null)
                viewNotesToolStripMenuItem.Enabled = ind.HasNotes;
        }

        void ViewNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
            Individual ind = ft.GetIndividual(indID);
            if (ind != null)
            {
                Notes notes = new Notes(ind);
                notes.Show();
            }
        }

        void ShowViewNotesMenu(DataGridView dg, MouseEventArgs e)
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

        void ResetTable()
        {
            if (!splitContainer.Panel1Collapsed)
                SortIndividuals();
            if (!splitContainer.Panel2Collapsed)
                SortFamilies();
        }

        void MnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            if (reportType == ReportType.MismatchedChildrenStatus || reportType == ReportType.MissingChildrenStatus)
            {
                if (!splitContainer.Panel1Collapsed)
                    indReportFormHelper.SaveColumnLayout("ChildrenStatusIndColumns.xml");
                if (!splitContainer.Panel2Collapsed)
                    famReportFormHelper.SaveColumnLayout("ChildrenStatusFamColumns.xml");
                MessageBox.Show("Form Settings Saved", "People");
            }
        }

        void MnuResetColumns_Click(object sender, EventArgs e)
        {
            if (reportType == ReportType.MismatchedChildrenStatus || reportType == ReportType.MissingChildrenStatus)
            {
                if (!splitContainer.Panel1Collapsed)
                    indReportFormHelper.ResetColumnLayout("ChildrenStatusIndColumns.xml");
                if (!splitContainer.Panel2Collapsed)
                    famReportFormHelper.ResetColumnLayout("ChildrenStatusFamColumns.xml");
            }
        }

        void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            indReportFormHelper.PrintReport(Text);
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.PrintReport(Text + " - Families");
        }

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            indReportFormHelper.PrintPreviewReport();
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.PrintPreviewReport();
        }

        void MnuExportToExcel_Click(object sender, EventArgs e)
        {
            indReportFormHelper.DoExportToExcel<IDisplayIndividual>();
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.DoExportToExcel<IDisplayFamily>();
        }

        void DgIndividuals_MouseDown(object sender, MouseEventArgs e) => ShowViewNotesMenu(dgIndividuals, e);

        public void SetupNoChildrenStatus()
        {
            SortableBindingList<IDisplayFamily> results = new SortableBindingList<IDisplayFamily>();
            IEnumerable<CensusFamily> toSearch = ft.GetAllCensusFamilies(CensusDate.UKCENSUS1911, true, true);
            foreach (Family fam in toSearch)
            {
                if (fam.On1911Census && !fam.HasAnyChildrenStatus && fam.BothParentsAlive(CensusDate.UKCENSUS1911) && !fam.FamilyType.Equals(Family.PRE_MARRIAGE))
                    results.Add(fam);
            }
            reportType = ReportType.MissingChildrenStatus;
            dgFamilies.DataSource = results;
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            famReportFormHelper.LoadColumnLayout("ChildrenStatusFamColumns.xml");
            SetSaveButtonsStatus(true);
            Text = "Families with a 1911 census record but no Children Status record showing Children Alive/Dead";
            UpdateStatusCount();
        }

        void DgFamilies_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (reportType == ReportType.MissingChildrenStatus)
                e.CellStyle.BackColor = Color.BlanchedAlmond;
            else if (reportType == ReportType.MismatchedChildrenStatus)
            {
                e.CellStyle.BackColor = Color.BlanchedAlmond;
                e.CellStyle.Font = normalFont;
                DataGridViewCellCollection cells = dgFamilies.Rows[e.RowIndex].Cells;
                cells[e.ColumnIndex].ToolTipText = string.Empty;
                switch (e.ColumnIndex)
                {
                    case 6: // Totals
                    case 9:
                        if (!cells["ChildrenTotal"].Value.Equals(cells["ExpectedTotal"].Value))
                        {
                            e.CellStyle.BackColor = Color.Peru;
                            e.CellStyle.Font = boldFont;
                            cells[e.ColumnIndex].ToolTipText = "Number of children known to this family doesn't match total from Children Status.";
                        }
                        else
                            cells[e.ColumnIndex].ToolTipText = "Totals match";
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case 7: // Alive
                    case 10:
                        if (!cells["ChildrenAlive"].Value.Equals(cells["ExpectedAlive"].Value))
                        {
                            e.CellStyle.BackColor = Color.SandyBrown;
                            e.CellStyle.Font = boldFont;
                            cells[e.ColumnIndex].ToolTipText = "Number of children alive at time of census doesn't match number alive from Children Status.";
                        }
                        else
                            cells[e.ColumnIndex].ToolTipText = "Numbers alive match";
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                    case 8: // Dead
                    case 11:
                        if (!cells["ChildrenDead"].Value.Equals(cells["ExpectedDead"].Value))
                        {
                            e.CellStyle.BackColor = Color.Tan;
                            e.CellStyle.Font = boldFont;
                            cells[e.ColumnIndex].ToolTipText = "Number of children dead by time of census doesn't match number dead from Children Status.";
                        }
                        else
                            cells[e.ColumnIndex].ToolTipText = "Numbers dead match";
                        e.CellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        break;
                }
            }
        }

        void People_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
