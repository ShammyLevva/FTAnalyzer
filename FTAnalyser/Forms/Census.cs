using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Filters;
using System.IO;
using FTAnalyzer.Utilities;
using System.Collections;
using FTAnalyzer.UserControls;
using System.Web;

namespace FTAnalyzer.Forms
{
    public partial class Census : Form
    {
        //private int numFamilies;
        public CensusDate CensusDate { get; private set; }
        public int RecordCount { get; private set; }
        private string censusCountry;
        private bool CensusDone;
        private ReportFormHelper reportFormHelper;
        private FamilyTree ft;

        public bool LostCousins { get; private set; }

        public Census(CensusDate censusDate, bool censusDone)
        {
            InitializeComponent();
            dgCensus.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgCensus, true);
            ft = FamilyTree.Instance;
            reportFormHelper = new ReportFormHelper(this, "Census Report", dgCensus, this.ResetTable, "Census");

            this.LostCousins = false;
            this.CensusDate = censusDate;
            this.censusCountry = CensusDate.Country;
            this.RecordCount = 0;
            this.CensusDone = censusDone;
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
                defaultProvider = "FamilySearch";
            cbCensusSearchProvider.Text = defaultProvider;
            GeneralSettings.CompactCensusRefChanged += new EventHandler(RefreshCensusReferences);
        }

        public void SetupCensus(Predicate<CensusIndividual> filter)
        {
            IEnumerable<CensusFamily> censusFamilies = ft.GetAllCensusFamilies(CensusDate, CensusDone, true);
            List<CensusIndividual> individuals = censusFamilies.SelectMany(f => f.Members).Where(filter).ToList();
            //RemoveDuplicateIndividuals(individuals);
            RecordCount = individuals.Count;
            SetupDataGridView(CensusDone, individuals);
        }

        private void RemoveDuplicateIndividuals(List<CensusIndividual> individuals)
        {  // detect all possible duplicates and remove any where the individual is the only person in the census family
            List<CensusIndividual> toRemove = new List<CensusIndividual>();
            IEnumerable<CensusIndividual> duplicates = individuals.Distinct(new CensusIndividualIDComparer());
            foreach(CensusIndividual c in duplicates)
            {
                IEnumerable<CensusIndividual> entries = individuals.Where(x => x.IndividualID == c.IndividualID);
                bool remove = false;
                List<CensusIndividual> candidates = new List<CensusIndividual>();
                foreach (CensusIndividual ci in entries)
                {
                    if (ci.FamilyMembersCount == 1)
                        candidates.Add(ci);
                    else
                        remove = true;
                }
                if (remove)
                    toRemove.AddRange(candidates);
            }
            // we now have all people to remove that appear solo and in a family but what about people that appear solo twice or more
            // we need to remove the toRemove from list and the multiple solo people.
            // Another foreach loop???
        }

        public void SetupLCCensus(Predicate<CensusIndividual> relationFilter, bool showEnteredLostCousins)
        {
            this.LostCousins = true;
            Predicate<CensusIndividual> predicate;
            if (showEnteredLostCousins)
                predicate = x => x.IsLostCousinsEntered(CensusDate, false);
            else
                predicate = x => x.MissingLostCousins(CensusDate, false);
            IEnumerable<CensusFamily> censusFamilies = ft.GetAllCensusFamilies(CensusDate, true, false);
            Predicate<CensusIndividual> filter = FilterUtils.AndFilter<CensusIndividual>(relationFilter, predicate);
            List<CensusIndividual> individuals = censusFamilies.SelectMany(f => f.Members).Where(filter).ToList();
            RecordCount = individuals.Count;
            SetupDataGridView(true, individuals);
        }

        private void SetupDataGridView(bool censusDone, List<CensusIndividual> individuals)
        {
            dgCensus.DataSource = new SortableBindingList<IDisplayCensus>(individuals);
            if (!censusDone)
                dgCensus.Columns["CensusReference"].Visible = false;
            reportFormHelper.LoadColumnLayout("CensusColumns.xml");
            int numIndividuals = (from x in individuals select x.IndividualID).Distinct().Count();
            int numFamilies = (from x in individuals select x.FamilyID).Distinct().Count();

            tsRecords.Text = individuals.Count + " Rows containing " + numIndividuals + " Individuals and " +
                             numFamilies + " Families. " + CensusProviderText();
        }

        private void ResetTable()
        {
            dgCensus.Sort(dgCensus.Columns["Position"], ListSortDirection.Ascending);
            dgCensus.Sort(dgCensus.Columns["FamilyID"], ListSortDirection.Ascending);
            dgCensus.AutoResizeColumns();
            StyleRows();
        }

        private void StyleRows()
        {
            string currentRowText = "";
            bool highlighted = true;

            Font boldFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Bold);
            Font regularFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Regular);
            int sortColumn = dgCensus.SortedColumn.Index;
            foreach (DataGridViewRow row in dgCensus.Rows)
            {
                CensusIndividual cr = (CensusIndividual)row.DataBoundItem;
                if (row.Cells[sortColumn].Value.ToString() != currentRowText)
                {
                    currentRowText = row.Cells[sortColumn].Value.ToString();
                    highlighted = !highlighted;
                }
                DataGridViewCellStyle style = new DataGridViewCellStyle(dgCensus.DefaultCellStyle);
                style.BackColor = highlighted ? Color.LightGray : Color.White;
                style.ForeColor = cr.RelationType == Individual.DIRECT ? Color.Red : Color.Black;
                style.Font = (cr.IsCensusDone(CensusDate) || (cr.IsAlive(CensusDate) && !cr.DeathDate.StartsBefore(CensusDate))) ? boldFont : regularFont;
                cr.CellStyle = style;
            }
        }

        private string GetTooltipText(DataGridViewCellStyle style)
        {
            string result;
            if (style.Font.Bold && style.ForeColor == Color.Red)
                result = "This direct ancestor is known to be alive on this census.";
            else if (style.Font.Bold)
                result = "This individual is known to be alive on this census.\n";
            else if (style.ForeColor == Color.Red)
                result = "This is a direct ancestor that may be alive on this census.";
            else
                result = "This individual may be alive on this census.";
            return result + "\n" + CensusProviderText();
        }

        private string CensusProviderText()
        {
            if (CensusDate.VALUATIONROLLS.Contains(CensusDate))
                return string.Empty;
            return "Double click to search " + cbCensusSearchProvider.Text + " for this person's census record. Shift Double click to display thier facts.";
        }

        private void dgCensus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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

        private class IDisplayCensusComparerWrapper : Comparer<IDisplayCensus>
        {
            private Comparer<CensusIndividual> comparer;

            public IDisplayCensusComparerWrapper(Comparer<CensusIndividual> comp)
            {
                this.comparer = comp;
            }

            public override int Compare(IDisplayCensus x, IDisplayCensus y)
            {
                return comparer.Compare((CensusIndividual)x, (CensusIndividual)y);
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport("Census Report");
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        private void Census_TextChanged(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = this.Text;
        }

        private void tsBtnMapLocation_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CensusIndividual ds = dgCensus.CurrentRow == null ? null : (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
            FactLocation loc = ds == null ? null : ds.CensusLocation;
            if (loc != null)
            {   // Do geo coding stuff
                GoogleMap frmGoogleMap = new GoogleMap();
                if (frmGoogleMap.SetLocation(loc, loc.Level))
                    frmGoogleMap.Show();
                else
                    MessageBox.Show("Unable to find location : " + loc.ToString(), "FT Analyzer");
            }
            this.Cursor = Cursors.Default;
        }

        private void tsBtnMapOSLocation_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CensusIndividual ds = dgCensus.CurrentRow == null ? null : (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
            FactLocation loc = ds == null ? null : ds.CensusLocation;
            if (loc != null)
            {   // Do geo coding stuff
                BingOSMap frmBingMap = new BingOSMap();
                if (frmBingMap.SetLocation(loc, loc.Level))
                    frmBingMap.Show();
                else
                    MessageBox.Show("Unable to find location : " + loc.ToString(), "FT Analyzer");
            }
            this.Cursor = Cursors.Default;
        }

        private void dgCensus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgCensus.CurrentRow != null && !CensusDate.VALUATIONROLLS.Contains(CensusDate))
            {
                CensusIndividual ds = (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
                FamilyTree ft = FamilyTree.Instance;
                if (Control.ModifierKeys.Equals(Keys.Shift))
                {
                    Facts factForm = new Facts(ds);
                    MainForm.DisposeDuplicateForms(factForm);
                    factForm.Show();
                }
                else
                    ft.SearchCensus(censusCountry, CensusDate.StartDate.Year, ds, cbCensusSearchProvider.SelectedIndex);
            }
        }

        private void cbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Search Provider", cbCensusSearchProvider.SelectedItem.ToString());
            dgCensus.Refresh(); // force update of tooltips
            dgCensus.Focus();
        }

        private void mnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("CensusColumns.xml");
            MessageBox.Show("Form Settings Saved", "Census");
        }

        private void mnuResetCensusColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("CensusColumns.xml");
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayCensus>();
        }

        private void dgCensus_ColumnSortModeChanged(object sender, DataGridViewColumnEventArgs e)
        {
            StyleRows();
        }

        private void dgCensus_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            StyleRows();
        }

        private void mnuViewFacts_Click(object sender, EventArgs e)
        {
            if (dgCensus.CurrentRow != null)
            {
                CensusIndividual ds = (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
                Facts factForm = new Facts(ds);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        private void dgCensus_CellContextMenuStripNeeded(object sender, DataGridViewCellContextMenuStripNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                dgCensus.Rows[e.RowIndex].Cells[e.ColumnIndex].Selected = true;
        }

        private void RefreshCensusReferences(object sender, EventArgs e)
        {
            dgCensus.Refresh();
        }

        private void Census_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void btnHelp_Click(object sender, EventArgs e)
        {
            HttpUtility.VisitWebsite("https://ftanalyzer.codeplex.com/wikipage?title=The%20Census%20Tab&referringTitle=Documentation");
        }
    }
}
