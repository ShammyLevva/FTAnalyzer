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

namespace FTAnalyzer.Forms
{
    public partial class Census : Form
    {
        //private int numFamilies;
        public CensusDate CensusDate { get; private set; }
        private string censusCountry;
        private ReportFormHelper reportFormHelper;
        private FamilyTree ft;

        public bool LostCousins { get; private set; }

        public Census(CensusDate censusDate)
        {
            InitializeComponent();
            dgCensus.AutoGenerateColumns = false;
            ft = FamilyTree.Instance;
            reportFormHelper = new ReportFormHelper("Census Report", dgCensus, this.ResetTable);

            this.LostCousins = false;
            this.CensusDate = censusDate;
            this.censusCountry = CensusDate.Country;
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
            {
                defaultProvider = "Ancestry";
            }
            cbCensusSearchProvider.Text = defaultProvider;
        }

        public void SetupCensus(Predicate<CensusIndividual> filter, bool censusDone)
        {
            IEnumerable<CensusFamily> censusFamilies = ft.GetAllCensusFamilies(CensusDate, censusDone);
            List<CensusIndividual> individuals = censusFamilies.SelectMany(f => f.Members).Where(filter).ToList();
            SetupDataGridView(censusDone, individuals);
        }

        public void SetupLCCensus(bool onlyBloodOrDirect, bool showEnteredLostCousins)
        {
            this.LostCousins = true;
            IEnumerable<CensusFamily> censusFamilies = ft.GetAllCensusFamilies(CensusDate, true);
            Func<CensusIndividual, bool> filter = onlyBloodOrDirect ?
                new Func<CensusIndividual, bool>(x => x.IsBloodDirect && x.IsCensusDone(CensusDate)) :
                x => x.IsCensusDone(CensusDate);

            IEnumerable<CensusIndividual> onCensus = censusFamilies.SelectMany(f => f.Members).Where(filter);
            List<CensusIndividual> individuals;
            if (showEnteredLostCousins)
            {
                IEnumerable<CensusFamily> notOnCensusFamilies = ft.GetAllCensusFamilies(CensusDate, false);
                filter = onlyBloodOrDirect ?
                    new Func<CensusIndividual, bool>(x => x.IsBloodDirect && !x.IsCensusDone(CensusDate)) :
                    x => !x.IsCensusDone(CensusDate);
                IEnumerable<CensusIndividual> notOnCensus = notOnCensusFamilies.SelectMany(f => f.Members).Where(filter);
                IEnumerable<CensusIndividual> allEligible = onCensus.Union(notOnCensus);

                Predicate<CensusIndividual> predicate = x => x.IsLostCousinEntered(CensusDate);
                individuals = allEligible.Where(predicate).ToList<CensusIndividual>();
            }
            else
            {
                Predicate<CensusIndividual> predicate = x => !x.IsLostCousinEntered(CensusDate);
                individuals = onCensus.Where(predicate).ToList<CensusIndividual>();
            }

            //// Test code
            //HashSet<string> test = new HashSet<string>(ft.AllIndividuals.Where(
            //    new Predicate<Individual>(x => x.IsBloodDirect && x.IsLostCousinEntered(CensusDate.SCOTCENSUS1881))).Select(x => x.Ind_ID));
            //HashSet<string> test2 = new HashSet<string>(individuals.Select(x => x.Ind_ID));
            //foreach (string id in test)
            //{
            //    if (!test2.Contains(id)) Console.WriteLine(id);
            //}

            SetupDataGridView(true, individuals);
        }

        private void SetupDataGridView(bool censusDone, List<CensusIndividual> individuals)
        {
            dgCensus.DataSource = new SortableBindingList<CensusIndividual>(individuals);
            if (!censusDone)
                dgCensus.Columns["CensusReference"].Visible = false;
            reportFormHelper.LoadColumnLayout("CensusColumns.xml");
            int numIndividuals = (from x in individuals select x.Ind_ID).Distinct().Count();
            int numFamilies = (from x in individuals select x.FamilyID).Distinct().Count();

            tsRecords.Text = individuals.Count + " Rows containing " + numIndividuals + " Individuals and " + numFamilies + " Families.";
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
                style.Font = (cr.IsAlive(CensusDate) && !cr.DeathDate.StartsBefore(CensusDate)) ? boldFont : regularFont;
                cr.CellStyle = style;
            }
        }

        private string GetTooltipText(DataGridViewCellStyle style)
        {
            if (style.Font.Bold && style.ForeColor == Color.Red)
                return "This direct ancestor is known to be alive on this census.";
            else if (style.Font.Bold)
                return "This individual is known to be alive on this census.";
            else if (style.ForeColor == Color.Red)
                return "This is a direct ancestor that may be alive on this census.";
            else
                return "Double click to search " + cbCensusSearchProvider.Text + " for this person's census record";
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
            reportFormHelper.PrintReport(this);
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport(this);
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
                    MessageBox.Show("Unable to find location : " + loc.ToString());
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
                    MessageBox.Show("Unable to find location : " + loc.ToString());
            }
            this.Cursor = Cursors.Default;
        }

        private void dgCensus_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                CensusIndividual ds = dgCensus.CurrentRow == null ? null : (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
                FamilyTree ft = FamilyTree.Instance;
                if (LostCousins)
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
            dgCensus.Focus();
        }

        private void mnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("CensusColumns.xml");
            MessageBox.Show("Column Settings Saved", "Census");
        }

        private void mnuResetCensusColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("CensusColumns.xml");
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayCensus>(this);
        }
    }
}
