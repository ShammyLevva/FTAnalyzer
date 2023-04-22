using FTAnalyzer.Filters;
using FTAnalyzer.Properties;
using FTAnalyzer.UserControls;
using FTAnalyzer.Utilities;
using Printing.DataGridViewPrint;
using System.ComponentModel;

namespace FTAnalyzer.Forms
{
    public partial class Census : Form
    {
        public CensusDate CensusDate { get; private set; }
        public int RecordCount { get; private set; }
        public bool LostCousins { get; private set; }

        readonly string censusCountry;
        readonly bool CensusDone;
        readonly ReportFormHelper reportFormHelper;
        readonly FamilyTree ft;

        public Census(CensusDate censusDate, bool censusDone)
        {
            InitializeComponent();
            dgCensus.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgCensus, true);
            ft = FamilyTree.Instance;
            reportFormHelper = new ReportFormHelper(this, "Census Report", dgCensus, ResetTable, "Census");

            LostCousins = false;
            CensusDate = censusDate;
            censusCountry = CensusDate.Country;
            RecordCount = 0;
            CensusDone = censusDone;
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            defaultProvider ??= "FamilySearch";
            string defaultRegion = (string)Application.UserAppDataRegistry.GetValue("Default Region");
            defaultRegion ??= ".co.uk";
            cbCensusSearchProvider.Text = defaultProvider;
            cbRegion.Text = defaultRegion;
            CensusSettingsUI.CompactCensusRefChanged += new EventHandler(RefreshCensusReferences);
            Top += NativeMethods.TopTaskbarOffset;
        }

        public void SetupCensus(Predicate<CensusIndividual> filter)
        {
            IEnumerable<CensusFamily> censusFamilies = ft.GetAllCensusFamilies(CensusDate, CensusDone, true);
            List<CensusIndividual> individuals = censusFamilies.SelectMany(f => f.Members).Filter(filter).ToList();
            individuals = FilterDuplicateIndividuals(individuals);
            RecordCount = individuals.Count;
            SetupDataGridView(CensusDone, individuals);
        }

        static List<CensusIndividual> FilterDuplicateIndividuals(List<CensusIndividual> individuals)
        {
            List<CensusIndividual> result = individuals.Filter(i => i.FamilyMembersCount > 1).ToList();
            HashSet<string> ids = new(result.Select(i => i.IndividualID));
            foreach (CensusIndividual i in individuals.Filter(i => i.FamilyMembersCount == 1))
                if (!ids.Contains(i.IndividualID))
                {
                    result.Add(i);
                    ids.Add(i.IndividualID);
                }
            return result;
        }

        public void SetupLCCensus(Predicate<CensusIndividual> relationFilter, bool showEnteredLostCousins, Predicate<Individual> individualRelationFilter)
        {
            LostCousins = true;
            Predicate<CensusIndividual> predicate;
            Predicate<Individual> individualPredicate;
            if (showEnteredLostCousins)
            {
                predicate = x => x.IsLostCousinsEntered(CensusDate, false);
                individualPredicate = x => x.IsLostCousinsEntered(CensusDate, false);
            }
            else
            { 
                predicate = x => x.MissingLostCousins(CensusDate, false);
                individualPredicate = x => x.MissingLostCousins(CensusDate, false);
            }
            Predicate<CensusIndividual> filter = FilterUtils.AndFilter(relationFilter, predicate);
            Predicate<Individual> individualFilter = FilterUtils.AndFilter(individualRelationFilter, individualPredicate);
            IEnumerable<CensusFamily> censusFamilies = ft.GetAllCensusFamilies(CensusDate, true, false);
            List<CensusIndividual> individuals = censusFamilies.SelectMany(f => f.Members).Filter(filter).ToList();
            individuals = FilterDuplicateIndividuals(individuals);
            List<Individual> listToCheck = ft.AllIndividuals.Filter(individualFilter).ToList();
            //CompareLists(individuals, listToCheck);
            RecordCount = individuals.Count;
            SetupDataGridView(true, individuals);
        }

        public void SetupLCupdateList(List<CensusIndividual> listItems)
        {
            if (listItems is null) return;
            LostCousins = true;
            RecordCount = listItems is null ? 0 : listItems.Count;
            SetupDataGridView(true, listItems);
        }

        //void CompareLists(List<CensusIndividual> individuals, List<Individual> listToCheck)
        //{
        //    List<string> ids = individuals.Select(x => x.IndividualID).ToList();
        //    List<Individual> missing = new List<Individual>();
        //    foreach(Individual ind in listToCheck)
        //    {
        //        if (!ids.Contains(ind.IndividualID))
        //            missing.Add(ind);
        //    }
        //}

        void SetupDataGridView(bool censusDone, List<CensusIndividual> individuals)
        {
            dgCensus.DataSource = new SortableBindingList<IDisplayCensus>(individuals);
            dgCensus.RowTemplate.Height = (int)(FontSettings.Default.FontHeight * GraphicsUtilities.GetCurrentScaling()); 
            dgCensus.AllowUserToResizeColumns = true;
            if (!censusDone)
                dgCensus.Columns["CensusReference"].Visible = false;
            reportFormHelper.LoadColumnLayout("CensusColumns.xml");
            int numIndividuals = (from x in individuals select x.IndividualID).Distinct().Count();
            int numFamilies = (from x in individuals select x.FamilyID).Distinct().Count();
            tsRecords.Text = $"{individuals.Count} Rows containing {numIndividuals} Individuals and {numFamilies} Families. {CensusProviderText()}";
        }

        void ResetTable()
        {
            dgCensus.Sort(dgCensus.Columns["Position"], ListSortDirection.Ascending);
            dgCensus.Sort(dgCensus.Columns["FamilyID"], ListSortDirection.Ascending);
            dgCensus.AutoResizeColumns();
            StyleRows();
        }

        void StyleRows()
        {
            try
            {
                string currentRowText = "";
                bool highlighted = true;
                Font boldFont = new(dgCensus.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Bold);
                Font regularFont = new(dgCensus.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Regular);
                int sortColumn = dgCensus.SortedColumn.Index;
                foreach (DataGridViewRow row in dgCensus.Rows)
                {
                    CensusIndividual cr = (CensusIndividual)row.DataBoundItem;
                    if (row.Cells[sortColumn].Value.ToString() != currentRowText)
                    {
                        currentRowText = row.Cells[sortColumn].Value.ToString();
                        highlighted = !highlighted;
                    }
                    DataGridViewCellStyle style = new(dgCensus.DefaultCellStyle)
                    {
                        BackColor = highlighted ? Color.LightGray : Color.White,
                        ForeColor = (cr.RelationType == Individual.DIRECT || cr.RelationType == Individual.DESCENDANT) ? Color.Red : Color.Black,
                        Font = (cr.IsCensusDone(CensusDate) || (cr.IsAlive(CensusDate) && !cr.DeathDate.StartsBefore(CensusDate))) ? boldFont : regularFont
                    };
                    cr.CellStyle = style;
                }
            }
            catch (Exception) { }
        }

        string GetTooltipText(DataGridViewCellStyle style)
        {
            string result;
            if (style.Font.Bold && style.ForeColor == Color.Red)
                result = "Bold Red: This direct ancestor is known to be alive on this census.";
            else if (style.Font.Bold)
                result = "Bold: This individual is known to be alive on this census.\n";
            else if (style.ForeColor == Color.Red)
                result = "Red: This is a direct ancestor that may be alive on this census.";
            else
                result = "This individual may be alive on this census.";
            return $"{result}\n{CensusProviderText()}";
        }

        string CensusProviderText() => CensusDate.VALUATIONROLLS.Contains(CensusDate)
                ? string.Empty
                : $"Double click to search {cbCensusSearchProvider.Text} for this person's census record. Shift Double click to display their facts.";

        void DgCensus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DataGridViewCell cell = dgCensus.Rows[e.RowIndex].Cells[e.ColumnIndex];
                CensusIndividual ind = dgCensus.Rows[e.RowIndex].DataBoundItem as CensusIndividual;
                if (ind.CellStyle != null)
                {
                    e.CellStyle = ind.CellStyle;
                    cell.ToolTipText = GetTooltipText(ind.CellStyle);
                }
            }
        }

        class IDisplayCensusComparerWrapper : Comparer<IDisplayCensus>
        {
            readonly Comparer<CensusIndividual> comparer;
            public IDisplayCensusComparerWrapper(Comparer<CensusIndividual> comp) => comparer = comp;
            public override int Compare(IDisplayCensus x, IDisplayCensus y) => comparer.Compare((CensusIndividual)x, (CensusIndividual)y);
        }

        void PrintToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintReport("Census Report");

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintPreviewReport();

        void Census_TextChanged(object sender, EventArgs e) => reportFormHelper.PrintTitle = Text;

        void TsBtnMapLocation_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CensusIndividual ds = dgCensus.CurrentRow == null ? null : (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
            FactLocation loc = ds?.CensusLocation;
            if (loc != null)
            {   // Do geo coding stuff
                GoogleMap.ShowLocation(loc, loc.Level);
            }
            Cursor = Cursors.Default;
        }

        void TsBtnMapOSLocation_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CensusIndividual ds = dgCensus.CurrentRow == null ? null : (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
            FactLocation loc = ds?.CensusLocation;
            if (loc != null)
            {   // Do geo coding stuff
                BingOSMap frmBingMap = new();
                if (frmBingMap.SetLocation(loc, loc.Level))
                    frmBingMap.Show();
                else
                    MessageBox.Show($"Unable to find location : {loc}", "FTAnalyzer");
            }
            Cursor = Cursors.Default;
        }

        void DgCensus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgCensus.CurrentRow != null && !CensusDate.VALUATIONROLLS.Contains(CensusDate))
            {
                CensusIndividual ds = (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
                if (ModifierKeys.Equals(Keys.Shift))
                {
                    Facts factForm = new(ds);
                    MainForm.DisposeDuplicateForms(factForm);
                    factForm.Show();
                }
                else
                {
                    try
                    {
                        int year = CensusDate.StartDate.Year;
                        if (CensusDate == CensusDate.ANYCENSUS)
                            year = ds.CensusDate.BestYear;
                        FamilyTree.SearchCensus(censusCountry, year, ds, cbCensusSearchProvider.SelectedIndex, cbRegion.Text);
                    }
                    catch (CensusSearchException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        void CbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Search Provider", cbCensusSearchProvider.SelectedItem.ToString());
            dgCensus.Refresh(); // force update of tooltips
            dgCensus.Focus();
        }

        void CbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Region", cbRegion.SelectedItem.ToString());
            Settings.Default.defaultURLRegion = cbRegion.SelectedItem.ToString();
            Settings.Default.Save();
            dgCensus.Refresh(); // force update of tooltips
            dgCensus.Focus();
        }

        void MnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("CensusColumns.xml");
            MessageBox.Show("Form Settings Saved", "Census");
        }

        void MnuResetCensusColumns_Click(object sender, EventArgs e) => reportFormHelper.ResetColumnLayout("CensusColumns.xml");

        void MnuExportToExcel_Click(object sender, EventArgs e) => reportFormHelper.DoExportToExcel<IDisplayCensus>();

        void DgCensus_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e) => StyleRows();

        void DgCensus_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) => StyleRows();

        void MnuViewFacts_Click(object sender, EventArgs e)
        {
            if (dgCensus.CurrentRow != null)
            {
                CensusIndividual ds = (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
                Facts factForm = new(ds);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        void DgCensus_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgCensus.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        void RefreshCensusReferences(object sender, EventArgs e) => dgCensus.Refresh();

        void Census_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void BtnHelp_Click(object sender, EventArgs e) => SpecialMethods.VisitWebsite("https://www.ftanalyzer.com/The%20Census%20Tab");

        void Census_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
