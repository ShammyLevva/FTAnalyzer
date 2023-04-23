using System.ComponentModel;
using FTAnalyzer.Utilities;
using FTAnalyzer.Filters;
using FTAnalyzer.Properties;

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
                boldFont = new(dgFamilies.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Bold);
                normalFont = new(dgFamilies.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Regular);
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
                txtCount.Text = $"{dgFamilies.RowCount} Problems detected. {Messages.Hints_IndividualFamily} Shift Double click to see colour census report for family.";
            else
            {
                if (splitContainer.Panel2Collapsed)
                    txtCount.Text = $"Count: {dgIndividuals.RowCount} Individuals.  {Messages.Hints_Individual}";
                else
                    txtCount.Text = $"Count: {dgIndividuals.RowCount} Individuals and {dgFamilies.RowCount} Families. {Messages.Hints_IndividualFamily}";
            }
        }

        public void SetLocation(FactLocation loc, int level)
        {
            Text = $"Individuals & Families with connection to {loc}";
            level = Math.Min(loc.Level, level); // if location level isn't as detailed as level on tab use location level
            IEnumerable<Individual> listInd = ft.GetIndividualsAtLocation(loc, level);
            SortableBindingList<IDisplayIndividual> dsInd = new();
            foreach (Individual i in listInd)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();

            IEnumerable<Family> listFam = ft.GetFamiliesAtLocation(loc, level);
            SortableBindingList<IDisplayFamily> dsFam = new();
            foreach (Family f in listFam)
                dsFam.Add(f);
            dgFamilies.DataSource = dsFam;
            dgFamilies.Visible = true;
            dgChildrenStatus.Visible = false;
            SortFamilies();
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void SetWorkers(string job, SortableBindingList<Individual> workers)
        {
            Text = "Individuals whose occupation was " + (job.Length == 0 ? "not entered" : job);
            SortableBindingList<IDisplayIndividual> dsInd = new();
            foreach (Individual i in workers)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();
            dgIndividuals.Dock = DockStyle.Fill;
            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }
        public void SetCustomFacts(string factType, SortableBindingList<Individual> individuals, SortableBindingList<Family> families)
        {
            Text = "Individuals/Families whose have the custom fact of " + (string.IsNullOrEmpty(factType) ? "not entered" : factType);
            if (individuals.Count > 0)
            {
                SortableBindingList<IDisplayIndividual> dsInd = new();
                foreach (Individual i in individuals)
                    dsInd.Add(i);
                dgIndividuals.DataSource = dsInd;
                SortIndividuals();
                dgIndividuals.Dock = DockStyle.Fill;
                splitContainer.Panel1Collapsed = false;
            }
            else
            {
                dgIndividuals.Visible = false;
                splitContainer.Panel1Collapsed = true;
            }
            if (families.Count > 0)
            {
                SortableBindingList<IDisplayFamily> dsFam = new();
                foreach (Family f in families)
                    dsFam.Add(f);
                splitContainer.Panel2Collapsed = false;
                dgFamilies.Visible = true;
                dgChildrenStatus.Visible = false;
                dgFamilies.DataSource = dsFam;
                SortFamilies();
                dgFamilies.Dock = DockStyle.Fill;
            }
            else
            {
                splitContainer.Panel2Collapsed = true;
                dgFamilies.Visible = false;
                dgChildrenStatus.Visible = false;
            }

            UpdateStatusCount();
        }

        public void SetSurnameStats(IDisplaySurnames stat, Predicate<Individual> indFilter, Predicate<Family> famFilter, bool ignoreCase)
        {
            Text = $"Individuals & Families whose surame is {stat.Surname}";
            SortableBindingList<IDisplayIndividual> dsInd = new();
            bool indSurnames(Individual x) => x.Surname.Equals(stat.Surname);
            Predicate<Individual> filter = FilterUtils.AndFilter(indFilter, indSurnames);
            foreach (Individual i in ft.AllIndividuals.Filter(filter))
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            SortIndividuals();
            dgIndividuals.Dock = DockStyle.Fill;

            bool famSurnames(Family x) => x.ContainsSurname(stat.Surname, ignoreCase);
            Predicate<Family> filter2 = FilterUtils.AndFilter(famFilter, famSurnames);
            SortableBindingList<IDisplayFamily> dsFam = new();
            foreach (Family f in ft.AllFamilies.Filter(filter2))
                dsFam.Add(f);
            dgFamilies.DataSource = dsFam;
            dgFamilies.Visible = true;
            dgChildrenStatus.Visible = false;
            SortFamilies();
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void SetupLCDuplicates(Predicate<Individual> relationFilter)
        {
            static bool lcFacts(Individual i) => i.DuplicateLCFacts > 0;
            Predicate<Individual> filter = FilterUtils.AndFilter(relationFilter, lcFacts);
            List<Individual> individuals = ft.AllIndividuals.Filter(filter).ToList();
            SetIndividuals(individuals, "Lost Cousins with Duplicate Facts");
        }

        public void SetupLCnoCensus(Predicate<Individual> relationFilter)
        {
            List<Individual> listtoCheck = ft.AllIndividuals.Filter(relationFilter).ToList();
            List<Individual> individuals = new();
            Predicate<Individual> lcFacts = new(i => i.HasLostCousinsFactWithNoCensusFact);
            IEnumerable<Individual> censusMissing = listtoCheck.Filter(lcFacts);
            individuals.AddRange(censusMissing);
            individuals = individuals.Distinct().ToList();
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
            dgFamilies.Visible = true;
            dgChildrenStatus.Visible = false;
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
            foreach (Individual i in list)
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
            dgFamilies.Visible = true;
            dgChildrenStatus.Visible = false;
            SortFamilies();
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void ListRelationToRoot(string relationtoRoot)
        {
            bool filter(Individual x) => x.RelationToRoot.Equals(relationtoRoot);
            List<Individual> individuals = ft.AllIndividuals.Filter(filter).ToList();
            SetIndividuals(individuals, $"Individuals who are a {relationtoRoot} of root person");
        }

        void SortIndividuals()
        {
            if (dgIndividuals.Columns.Count > 0)
            {
                dgIndividuals.Sort(dgIndividuals.Columns[1], ListSortDirection.Ascending);
                dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
            }
        }

        void SortFamilies()
        {
            if (dgFamilies.RowCount > 1)
                dgFamilies.Sort(dgFamilies.Columns[0], ListSortDirection.Ascending);
            if (dgChildrenStatus.RowCount > 1)
                dgChildrenStatus.Sort(dgChildrenStatus.Columns[0], ListSortDirection.Ascending);
        }

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
            SortableBindingList<IDisplayIndividual> dsInd = new();
            SortableBindingList<IDisplayFamily> dsFam = new();
            families = new Dictionary<IDisplayIndividual, IDisplayFamily>();
            foreach (Family f in ft.AllFamilies)
            {
                bool added = false;
                foreach (Individual child in f.Children)
                {
                    if (child.BirthDate.IsKnown)
                    {
                        if (f.Husband is not null && f.Husband.BirthDate.IsKnown)
                        {
                            Age age = f.Husband.GetAge(child.BirthDate);
                            if (age.MinAge >= minAge && !dsInd.Contains(f.Husband))
                            {
                                dsInd.Add(f.Husband);
                                families.Add(f.Husband, f);
                                added = true;
                            }
                        }
                        if (f.Wife is not null && f.Wife.BirthDate.IsKnown)
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
                dgFamilies.Visible = true;
                dgChildrenStatus.Visible = false;
                SortFamilies();
                dgFamilies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgIndividuals.Rows[0].Selected = true; // force a selection to update both grids
                return true;
            }
            else
            {
                MessageBox.Show($"You have no parents older than {minAge} at time of children's birth.", "FTAnalyzer");
                return false;
            }
        }

        void DgIndividuals_SelectionChanged(object sender, EventArgs e)
        {
            if (selectRow && dgIndividuals.CurrentRow is not null)
            {
                IDisplayIndividual ind = (IDisplayIndividual)dgIndividuals.CurrentRow.DataBoundItem;
                families.TryGetValue(ind, out IDisplayFamily? f);
                if (f is not null)
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
                string indID = (string)dgIndividuals.CurrentRow.Cells[nameof(IDisplayIndividual.IndividualID)].Value;
                MainForm.ShowIndividualsFacts(indID);
            }
        }

        void DgFamilies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string famID = sender == dgFamilies ?
                    (string)dgFamilies.CurrentRow.Cells[nameof(IDisplayFamily.FamilyID)].Value :
                    (string)dgChildrenStatus.CurrentRow.Cells[nameof(IDisplayFamily.FamilyID)].Value;
                Family? fam = ft.GetFamily(famID);
                if (fam is not null)
                {
                    if ((reportType == ReportType.MismatchedChildrenStatus || reportType == ReportType.MissingChildrenStatus) && ModifierKeys.Equals(Keys.Shift))
                    {
                        List<IDisplayColourCensus> list = fam.Members.ToList<IDisplayColourCensus>();
                        ColourCensus rs = new(Countries.UNITED_KINGDOM, list);
                        MainForm.DisposeDuplicateForms(rs);
                        rs.Show();
                        rs.Focus();
                    }
                    else
                    {
                        MainForm.ShowFamilyFacts(fam.FamilyID);
                    }
                }
            }
        }

        public void SetupMissingCensusLocation()
        {
            List<Individual> individuals = new();
            foreach (CensusDate censusDate in CensusDate.SUPPORTED_CENSUS)
            {
                Predicate<Individual> censusFacts = new(x => x.IsCensusDone(censusDate) && !x.HasCensusLocation(censusDate));
                IEnumerable<Individual> censusMissing = ft.AllIndividuals.Filter(censusFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct().ToList();
            SetIndividuals(individuals, "Individuals with census records with no census location");
        }

        public void SetupDuplicateCensus()
        {
            List<Individual> individuals = new();
            foreach (CensusDate censusDate in CensusDate.SUPPORTED_CENSUS)
            {
                Predicate<Individual> censusFacts = new(i => i.CensusDateFactCount(censusDate) > 1);
                IEnumerable<Individual> censusMissing = ft.AllIndividuals.Filter(censusFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct().ToList();
            SetIndividuals(individuals, "Individuals that may have more than one census/residence record for a census year");
        }

        public void SetupChildrenStatusReport()
        {
            SortableBindingList<IDisplayChildrenStatus> results = new();
            IEnumerable<CensusFamily> toSearch = ft.GetAllCensusFamilies(CensusDate.UKCENSUS1911, true, true);
            foreach (CensusFamily fam in toSearch)
            {
                if (fam.On1911Census && fam.HasGoodChildrenStatus && !fam.FamilyType.Equals(Family.SOLOINDIVIDUAL) && !fam.FamilyType.Equals(Family.PRE_MARRIAGE) &&
                    (fam.ExpectedTotal != fam.ChildrenTotal || fam.ExpectedAlive != fam.ChildrenAlive || fam.ExpectedDead != fam.ChildrenDead))
                    results.Add(fam);
            }
            reportType = ReportType.MismatchedChildrenStatus;
            dgChildrenStatus.DataSource = results;
            dgChildrenStatus.Visible = true;
            dgFamilies.Visible = false;
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            famReportFormHelper.LoadColumnLayout("ChildrenStatusFamColumns.xml");
            SetSaveButtonsStatus(true);
            Text = "1911 Census Families where the children status recorded doesn't match the children in tree";
            UpdateStatusCount();
        }

        void ContextMenuStrip1_Opened(object sender, EventArgs e)
        {
            string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
            Individual? ind = ft.GetIndividual(indID);
            if (ind is not null)
                viewNotesToolStripMenuItem.Enabled = ind.HasNotes;
        }

        void ViewNotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
            Individual? ind = ft.GetIndividual(indID);
            if (ind is not null)
            {
                Notes notes = new(ind);
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
            if (!splitContainer.Panel1Collapsed)
                indReportFormHelper.PrintReport(Text);
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.PrintReport(Text + " - Families");
        }

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            if (!splitContainer.Panel1Collapsed)
                indReportFormHelper.PrintPreviewReport();
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.PrintPreviewReport();
        }

        void MnuExportToExcel_Click(object sender, EventArgs e)
        {
            if (!splitContainer.Panel1Collapsed)
                indReportFormHelper.DoExportToExcel<IDisplayIndividual>();
            if (!splitContainer.Panel2Collapsed)
                famReportFormHelper.DoExportToExcel<IDisplayFamily>();
        }

        void DgIndividuals_MouseDown(object sender, MouseEventArgs e) => ShowViewNotesMenu(dgIndividuals, e);

        public void SetupNoChildrenStatus()
        {
            SortableBindingList<IDisplayFamily> results = new();
            IEnumerable<CensusFamily> toSearch = ft.GetAllCensusFamilies(CensusDate.UKCENSUS1911, true, true);
            foreach (Family fam in toSearch)
            {
                if (fam.On1911Census && !fam.HasAnyChildrenStatus && fam.BothParentsAlive(CensusDate.UKCENSUS1911) && !fam.FamilyType.Equals(Family.PRE_MARRIAGE))
                    results.Add(fam);
            }
            reportType = ReportType.MissingChildrenStatus;
            dgFamilies.DataSource = results;
            dgFamilies.Visible = true;
            dgChildrenStatus.Visible = false;
            splitContainer.Panel1Collapsed = true;
            splitContainer.Panel2Collapsed = false;
            famReportFormHelper.LoadColumnLayout("ChildrenStatusFamColumns.xml");
            SetSaveButtonsStatus(true);
            Text = "Families with a 1911 census record but no Children Status record showing Children Alive/Dead";
            UpdateStatusCount();
        }

        void DgFamilies_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e) => e.CellStyle.BackColor = Color.BlanchedAlmond;

        void DgChildrenStatus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            e.CellStyle.BackColor = Color.BlanchedAlmond;
            e.CellStyle.Font = normalFont;
            DataGridViewCellCollection cells = dgChildrenStatus.Rows[e.RowIndex].Cells;
            cells[e.ColumnIndex].ToolTipText = string.Empty;
            switch (e.ColumnIndex)
            {
                case 6: // Totals
                case 9:
                    if (!cells[nameof(IDisplayChildrenStatus.ChildrenTotal)].Value.Equals(cells[nameof(IDisplayChildrenStatus.ExpectedTotal)].Value))
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
                    if (!cells[nameof(IDisplayChildrenStatus.ChildrenAlive)].Value.Equals(cells[nameof(IDisplayChildrenStatus.ExpectedAlive)].Value))
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
                    if (!cells[nameof(IDisplayChildrenStatus.ChildrenDead)].Value.Equals(cells[nameof(IDisplayChildrenStatus.ExpectedDead)].Value))
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
        void People_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void People_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
