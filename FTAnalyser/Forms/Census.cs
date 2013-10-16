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
        private int numFamilies;
        public FactDate CensusDate { get; private set; }
        private string censusCountry;
        private ReportFormHelper reportFormHelper;
        private IComparer<CensusIndividual> comparer;

        public bool LostCousins { get; private set; }

        public Census(bool lostCousins, string censusCountry)
        {
            InitializeComponent();
            dgCensus.AutoGenerateColumns = false;

            reportFormHelper = new ReportFormHelper("Census Report", dgCensus, this.ResetTable);

            this.LostCousins = lostCousins;

            this.censusCountry = censusCountry;
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
            {
                defaultProvider = "Ancestry";
            }
            cbCensusSearchProvider.Text = defaultProvider;
        }

        public void SetupCensus(Predicate<CensusIndividual> filter, IComparer<CensusIndividual> comparer,
                FactDate censusDate, bool censusDone)
        {
            this.CensusDate = censusDate;
            this.comparer = comparer;
            IEnumerable<CensusFamily> censusFamilies = FamilyTree.Instance.GetAllCensusFamilies(censusDate, censusDone);
            List<CensusIndividual> individuals = censusFamilies.SelectMany(f => f.Members).Where(filter).ToList();
            dgCensus.DataSource = individuals;
            if (!censusDone)
                dgCensus.Columns["CensusReference"].Visible = false;
            reportFormHelper.LoadColumnLayout("CensusColumns.xml");
            tsRecords.Text = individuals.Count + " Records / " + numFamilies + " Families.";
        }

        public void SetupLCCensus(Predicate<CensusIndividual> filter, IComparer<CensusIndividual> comparer,
                                    FactDate date, bool censusDone, bool showEnteredLostCousins)
        {
        }

        private void ResetTable()
        {
            (dgCensus.DataSource as List<CensusIndividual>).Sort(comparer);
            dgCensus.AutoResizeColumns();
            StyleRows();
        }

        private void StyleRows()
        {
            string currentFamilyID = "";
            bool highlighted = true;
            numFamilies = 0;

            Font boldFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Bold);
            Font regularFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Regular);

            foreach (DataGridViewRow row in dgCensus.Rows)
            {
                CensusIndividual cr = (CensusIndividual)row.DataBoundItem;
                if (cr.FamilyID != currentFamilyID)
                {
                    currentFamilyID = cr.FamilyID;
                    highlighted = !highlighted;
                    numFamilies++;
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

        private void dgCensus_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            Comparer<IDisplayCensus> comp;
            DataGridViewColumn column = dgCensus.Columns[e.ColumnIndex];
            switch (column.Name)
            {
                case "FamilyGed": // Family GED
                    comp = new IDisplayCensusComparerWrapper(new CensusFamilyGedComparer());
                    break;
                case "CensusLocation": // By location (original sort order)
                    comp = new IDisplayCensusComparerWrapper(new CensusLocationComparer());
                    break;
                case "CensusName": // Census Name
                    comp = new IDisplayCensusComparerWrapper(new CensusIndividualNameComparer());
                    break;
                default:
                    comp = null;
                    break;
            }

            if (comp != null)
            {
                List<IDisplayCensus> list = (List<IDisplayCensus>)dgCensus.DataSource;
                list.Sort(comp);
                dgCensus.DataSource = list;
                dgCensus.Refresh();
                StyleRows();
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
