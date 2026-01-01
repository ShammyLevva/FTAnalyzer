using FTAnalyzer.Filters;
using FTAnalyzer.Graphics;
using FTAnalyzer.Properties;
using FTAnalyzer.Shared.Utilities;
using FTAnalyzer.UserControls;
using FTAnalyzer.Utilities;
using Microsoft.Win32;
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

        readonly string DEFAULT_PROVIDER = "FamilySearch";
        readonly string DEFAULT_REGION = ".co.uk";

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
            string defaultProvider = RegistrySettings.GetRegistryValue("Default Search Provider", DEFAULT_PROVIDER).ToString() ?? DEFAULT_PROVIDER;
            defaultProvider ??= DEFAULT_PROVIDER;
            string defaultRegion = RegistrySettings.GetRegistryValue("Default Region", DEFAULT_REGION).ToString() ?? DEFAULT_REGION;
            defaultRegion ??= DEFAULT_REGION;
            cbCensusSearchProvider.Text = defaultProvider;
            cbRegion.Text = defaultRegion;
            CensusSettingsUI.CompactCensusRefChanged += new EventHandler(RefreshCensusReferences);
            Top += NativeMethods.TopTaskbarOffset;
        }

        public void SetupCensus(Predicate<CensusIndividual> filter)
        {
            IEnumerable<CensusFamily> censusFamilies = ft.GetAllCensusFamilies(CensusDate, CensusDone, true);
            List<CensusIndividual> individuals = [.. censusFamilies.SelectMany(f => f.Members).Filter(filter)];
            individuals = FilterDuplicateIndividuals(individuals);
            RecordCount = individuals.Count;
            SetupDataGridView(CensusDone, individuals);
        }

        static List<CensusIndividual> FilterDuplicateIndividuals(List<CensusIndividual> individuals)
        {
            List<CensusIndividual> result = [.. individuals.Filter(i => i.FamilyMembersCount > 1)];
            HashSet<string> ids = [.. result.Select(i => i.IndividualID)];
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
            List<CensusIndividual> individuals = [.. censusFamilies.SelectMany(f => f.Members).Filter(filter)];
            individuals = FilterDuplicateIndividuals(individuals);
            List<Individual> listToCheck = [.. ft.AllIndividuals.Filter(individualFilter)];
            //CompareLists(individuals, listToCheck);
            RecordCount = individuals.Count;
            SetupDataGridView(true, individuals);
        }

        public void SetupLCupdateList(List<CensusIndividual>? listItems)
        {
            if (listItems is null) return;
            LostCousins = true;
            RecordCount = listItems.Count;
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
            DataGridViewColumn? pos = dgCensus.Columns["Position"];
            DataGridViewColumn? famID = dgCensus.Columns["FamilyID"];
            if (pos is not null && famID is not null)
            {
                dgCensus.Sort(pos, ListSortDirection.Ascending);
                dgCensus.Sort(famID, ListSortDirection.Ascending);
                dgCensus.AutoResizeColumns();
                StyleRows();
            }
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
                    CensusIndividual? cr = (CensusIndividual?)row.DataBoundItem;
                    if (cr is null) return;
                    if (row.Cells[sortColumn].Value.ToString() != currentRowText)
                    {
                        currentRowText = row.Cells[sortColumn].Value.ToString() ?? string.Empty;
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
                CensusIndividual? ind = (CensusIndividual?)dgCensus.Rows[e.RowIndex].DataBoundItem;
                if (ind is not null || ind.CellStyle is not null)
                {
                    e.CellStyle = ind.CellStyle;
                    cell.ToolTipText = GetTooltipText(ind.CellStyle);
                }
            }
        }

        void PrintToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintReport("Census Report");

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintPreviewReport();

        void Census_TextChanged(object sender, EventArgs e) => reportFormHelper.PrintTitle = Text;

        void TsBtnMapLocation_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CensusIndividual? ds = dgCensus.CurrentRow is null || dgCensus.CurrentRow.DataBoundItem is null ? null
                : (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
            FactLocation? loc = ds?.CensusLocation;
            if (loc is not null)
            {   // Do geo coding stuff
                GoogleMap.ShowLocation(loc, loc.Level);
            }
            Cursor = Cursors.Default;
        }

        void TsBtnMapOSLocation_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CensusIndividual? ds = (CensusIndividual?)dgCensus.CurrentRow.DataBoundItem;
            FactLocation? loc = ds?.CensusLocation;
            if (loc is not null)
            {   // Do geo coding stuff
                BingOSMap frmBingMap = new();
                if (frmBingMap.SetLocation(loc, loc.Level))
                    frmBingMap.Show();
                else
                    UIHelpers.ShowMessage($"Unable to find location : {loc}", "FTAnalyzer");
            }
            Cursor = Cursors.Default;
        }

        void DgCensus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgCensus.CurrentRow is not null && !CensusDate.VALUATIONROLLS.Contains(CensusDate))
            {
                CensusIndividual? ds = (CensusIndividual?)dgCensus.CurrentRow.DataBoundItem;
                if (ds is null) return;
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
                        UIHelpers.ShowMessage(ex.Message);
                    }
                }
            }
        }

        void CbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            string defaultProvider = cbCensusSearchProvider.SelectedItem.ToString() ?? DEFAULT_PROVIDER;
            RegistrySettings.SetRegistryValue("Default Search Provider", defaultProvider, RegistryValueKind.String);
            dgCensus.Refresh(); // force update of tooltips
            dgCensus.Focus();
        }

        void CbRegion_SelectedIndexChanged(object sender, EventArgs e)
        {
            RegistrySettings.SetRegistryValue("Default Region", cbRegion.SelectedItem.ToString() ?? DEFAULT_REGION, RegistryValueKind.String);
            Settings.Default.defaultURLRegion = cbRegion.SelectedItem.ToString();
            Settings.Default.Save();
            dgCensus.Refresh(); // force update of tooltips
            dgCensus.Focus();
        }

        void MnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("CensusColumns.xml");
            UIHelpers.ShowMessage("Form Settings Saved", "Census");
        }

        void MnuResetCensusColumns_Click(object sender, EventArgs e) => reportFormHelper.ResetColumnLayout("CensusColumns.xml");

        void MnuExportToExcel_Click(object sender, EventArgs e) => reportFormHelper.DoExportToExcel<IDisplayCensus>();

        void DgCensus_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e) => StyleRows();

        void DgCensus_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e) => StyleRows();

        void MnuViewFacts_Click(object sender, EventArgs e)
        {
            if (dgCensus.CurrentRow is not null)
            {
                CensusIndividual? ds = (CensusIndividual?)dgCensus.CurrentRow.DataBoundItem;
                if (ds != null)
                {
                    Facts factForm = new(ds);
                    MainForm.DisposeDuplicateForms(factForm);
                    factForm.Show();
                }
            }
        }

        void DgCensus_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgCensus.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        void RefreshCensusReferences(object? sender, EventArgs e) => dgCensus.Refresh();

        void Census_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void BtnHelp_Click(object sender, EventArgs e) => SpecialMethods.VisitWebsite("https://www.ftanalyzer.com/The%20Census%20Tab");

        void Census_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
