using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Printing.DataGridViewPrint.Tools;
using FTAnalyzer.Filters;
using System.IO;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    public partial class Census : Form
    {
        private Dictionary<int, DataGridViewCellStyle> rowStyles;
        private int numFamilies;
        public FactDate CensusDate { get; private set; }
        private FactLocation location;
        private FactLocation location2;
        private FactLocation censusLocation;

        private PrintingDataGridViewProvider printProvider;

        public Census(string censusCountry)
        {
            InitializeComponent();

            printDocument.DefaultPageSettings.Margins =
               new System.Drawing.Printing.Margins(15,15,15,15);

            printProvider = PrintingDataGridViewProvider.Create(
                printDocument, dgCensus, true, true, true,
                new TitlePrintBlock("Missing from Census Report"), null, null);

            printDocument.DefaultPageSettings.Landscape = true;
            this.censusLocation = new FactLocation(censusCountry);
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
            {
                defaultProvider = "Ancestry";
            }
            cbCensusSearchProvider.Text = defaultProvider;
        }

        public Census(string censusCountry, FactLocation location)
            : this(censusCountry)
        {
            this.location = location;
        }

        public Census(string censusCountry, FactLocation location, FactLocation location2)
            : this(censusCountry)
        {
            this.location = location;
            this.location2 = location2;
        }

        public void SetupCensus(Predicate<CensusIndividual> filter, IComparer<CensusIndividual> comparer,
                FactDate date, bool censusDone, bool lostCousinCheck)
        {
            FamilyTree ft = FamilyTree.Instance;
            CensusDate = date;
            IEnumerable<CensusFamily> censusFamilies = ft.GetAllCensusFamilies(date, censusDone, lostCousinCheck);
            List<CensusIndividual> individuals = censusFamilies.SelectMany(f => f.Members).Where(filter).ToList();
            individuals.Sort(comparer);
            dgCensus.DataSource = individuals.ToList<IDisplayCensus>();
            StyleRows();
            LoadCensusColumnLayout();
            ResizeColumns();
            tsRecords.Text = individuals.Count + " Records / " + numFamilies + " Families.";
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgCensus.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }

        private void StyleRows()
        {
            string currentFamilyID = "";
            bool highlighted = true;
            numFamilies = 0;
            rowStyles = new Dictionary<int, DataGridViewCellStyle>();

            Font boldFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Bold);
            Font regularFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Regular);

            foreach (DataGridViewRow r in dgCensus.Rows)
            {
                CensusIndividual cr = (CensusIndividual)r.DataBoundItem;
                if (cr.FamilyID != currentFamilyID)
                {
                    currentFamilyID = cr.FamilyID;
                    highlighted = !highlighted;
                    numFamilies++;
                }
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = highlighted ? Color.LightGray : Color.White;
                style.ForeColor = cr.RelationType == Individual.DIRECT ? Color.Red : Color.Black;
                style.Font = cr.IsAlive ? boldFont : regularFont;
                rowStyles.Add(r.Index, style);
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
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            DataGridViewCellStyle style = dgCensus.DefaultCellStyle;
            DataGridViewCell cell = dgCensus.Rows[e.RowIndex].Cells[e.ColumnIndex];
            rowStyles.TryGetValue(e.RowIndex, out style);
            if (style != null)
            {
                e.CellStyle.BackColor = style.BackColor;
                e.CellStyle.ForeColor = style.ForeColor;
                e.CellStyle.Font = style.Font;
                cell.ToolTipText = GetTooltipText(style);
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
                StyleRows();
                dgCensus.Refresh();
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
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.DocumentName = "Missing from Census Report";
                printDocument.Print();
            }
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printPreviewDialog.ShowDialog(this);
            }
        }

        private void Census_TextChanged(object sender, EventArgs e)
        {
            printProvider.Drawer.TitlePrintBlock = new TitlePrintBlock(this.Text);
        }

        private void tsBtnMapLocation_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            CensusIndividual ds = dgCensus.CurrentRow == null ? null : (CensusIndividual)dgCensus.CurrentRow.DataBoundItem;
            FactLocation loc = ds == null ? null : ds.CensusLocation;
            if (loc != null)
            {   // Do geo coding stuff
                GoogleMap frmGoogleMap = new GoogleMap();
                if (frmGoogleMap.setLocation(loc, loc.Level))
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
                if (frmBingMap.setLocation(loc, loc.Level))
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
                ft.SearchCensus(censusLocation.Country, CensusDate.StartDate.Year, ds, cbCensusSearchProvider.SelectedIndex);
            }
        }

        private void cbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Search Provider", cbCensusSearchProvider.SelectedItem.ToString());
            dgCensus.Focus();
        }
        
        private void mnuSaveCensusColumnLayout_Click(object sender, EventArgs e)
        {
            SaveCensusColumnLayout();
            MessageBox.Show("Column Sort Order Saved", "Census Column Sorting");
        }

        private void SaveCensusColumnLayout()
        {
            DataTable dt = new DataTable("table");
            var query = from DataGridViewColumn col in dgCensus.Columns
                        orderby col.DisplayIndex
                        select col;

            foreach (DataGridViewColumn col in query)
            {
                dt.Columns.Add(col.Name);
            }
            string path = Path.Combine(Properties.GeneralSettings.Default.SavePath, "CensusColumns.xml");
            dt.WriteXmlSchema(path);
        }

        private void LoadCensusColumnLayout()
        {
            try
            {
                DataTable dt = new DataTable();
                string path = Path.Combine(Properties.GeneralSettings.Default.SavePath, "CensusColumns.xml");
                dt.ReadXmlSchema(path);

                int i = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    dgCensus.Columns[col.ColumnName].DisplayIndex = i;
                    i++;
                }
            }
            catch (Exception)
            {
                ResetCensusColumnLayout();
            }
        }

        private void ResetCensusColumnLayout()
        {
            for (int i = 0; i < dgCensus.Columns.Count; i++)
                dgCensus.Columns[i].DisplayIndex = i;
            SaveCensusColumnLayout();
        }

        private void mnuResetCensusColumns_Click(object sender, EventArgs e)
        {
            ResetCensusColumnLayout();
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            DataTable dt = convertor.ToDataTable(dgCensus.DataSource as List<IDisplayCensus>);
            ExportToExcel.Export(dt);
            this.Cursor = Cursors.Default;
        }
    }
}
