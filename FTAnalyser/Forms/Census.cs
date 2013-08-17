using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Printing.DataGridViewPrint.Tools;

namespace FTAnalyzer.Forms
{
    public partial class Census : Form
    {
        private Dictionary<int, DataGridViewCellStyle> rowStyles;
        private int numFamilies;
        private FactDate censusDate;
        private FactLocation location;
        private FactLocation location2;
        private FactLocation censusLocation;

        private PrintingDataGridViewProvider printProvider;

        public Census(string censusCountry)
        {
            InitializeComponent();

            printDocument.DefaultPageSettings.Margins =
               new System.Drawing.Printing.Margins(40, 40, 40, 40);

            printProvider = PrintingDataGridViewProvider.Create(
                printDocument, dgCensus, true, true, true,
                new TitlePrintBlock(this.Text), null, null);

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

        public void setupCensus(RegistrationsProcessor rp, FactDate date, bool censusDone, bool includeResidence, bool lostCousinCheck, int maxAge)
        {
            FamilyTree ft = FamilyTree.Instance;
            censusDate = date;
            List<Registration> regs = ft.getAllCensusRegistrations(date, censusDone, includeResidence, lostCousinCheck);
            List<Registration> census = rp.processRegistrations(regs);
            List<IDisplayCensus> ds = new List<IDisplayCensus>();
            int pos = 0; // position of DisplayCensus object in original list.
            foreach (CensusRegistration r in census)
            {
                foreach (Individual i in r.Members)
                {
                    if (i.getAge(date).MinAge <= maxAge)
                        if (location == null)
                        {  // no location check TODO check if known location vs censusCountry (United Kingdom, Ireland, United States, Canada)
                            if(!r.FilterCountry.isKnownCountry)
                                ds.Add(new DisplayCensus(pos++, r, i)); // if we don't recognise the country and we aren't checking then ignore it
                            else  if(censusLocation.Country == FactLocation.UNITED_KINGDOM && (r.FilterCountry.isUnitedKingdom))
                                ds.Add(new DisplayCensus(pos++, r, i));
                            else if (censusLocation.Country == FactLocation.IRELAND || censusLocation.Country == FactLocation.UNITED_STATES || censusLocation.Country == FactLocation.CANADA)
                            {
                                if(r.FilterCountry.Equals(censusLocation))
                                    ds.Add(new DisplayCensus(pos++, r, i));
                            }
                        }
                        else if(r.FilterCountry.Equals(location) || (location2 != null && r.FilterCountry.Equals(location2)))
                        {
                            ds.Add(new DisplayCensus(pos++, r, i));
                        }
                }
            }
            dgCensus.DataSource = ds;
            StyleRows();
            ResizeColumns();
            tsRecords.Text = ds.Count + " Records / " + numFamilies + " Families.";
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgCensus.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }

        private void StyleRows()
        {
            string currentFamilyGed = "";
            bool highlighted = true;
            numFamilies = 0;
            rowStyles = new Dictionary<int, DataGridViewCellStyle>();

            Font boldFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Bold);
            Font regularFont = new Font(dgCensus.DefaultCellStyle.Font, FontStyle.Regular);

            foreach (DataGridViewRow r in dgCensus.Rows)
            {
                DisplayCensus cr = (DisplayCensus)r.DataBoundItem;
                if (cr.FamilyGed != currentFamilyGed)
                {
                    currentFamilyGed = cr.FamilyGed;
                    highlighted = !highlighted;
                    numFamilies++;
                }
                DataGridViewCellStyle style = new DataGridViewCellStyle();
                style.BackColor = highlighted ? Color.LightGray : Color.White;
                style.ForeColor = cr.RelationType == Individual.DIRECT ? Color.Red : Color.Black;
                style.Font = cr.isAlive ? boldFont : regularFont;
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
                return string.Empty;
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
            switch (e.ColumnIndex)
            {
                case 0: // Family GED
                    comp = new IDisplayCensusComparerWrapper(new CensusFamilyGedComparer());
                    break;
                case 1: // By location (original sort order)
                    comp = new IDisplayCensusComparerWrapper(new DefaultCensusComparer());
                    break;
                case 2: // Census Name
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

        private class IDisplayCensusComparerWrapper : Comparer<IDisplayCensus> {

            private Comparer<DisplayCensus> comparer;

            public IDisplayCensusComparerWrapper(Comparer<DisplayCensus> comp) {
                this.comparer = comp;
            }

            public override int Compare(IDisplayCensus x, IDisplayCensus y) {
                return comparer.Compare((DisplayCensus) x, (DisplayCensus) y);
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
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
            DisplayCensus ds = dgCensus.CurrentRow == null ? null : (DisplayCensus)dgCensus.CurrentRow.DataBoundItem;
            FactLocation loc = ds == null ? null : ds.RegistrationLocation;
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
            DisplayCensus ds = dgCensus.CurrentRow == null ? null : (DisplayCensus)dgCensus.CurrentRow.DataBoundItem;
            FactLocation loc = ds == null ? null : ds.RegistrationLocation;
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
            DisplayCensus ds = dgCensus.CurrentRow == null ? null : (DisplayCensus)dgCensus.CurrentRow.DataBoundItem;
            FamilyTree ft = FamilyTree.Instance;
            ft.SearchCensus(censusLocation.Country, censusDate.StartDate.Year, ds.Individual, cbCensusSearchProvider.SelectedIndex);
        }

        private void cbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Search Provider", cbCensusSearchProvider.SelectedItem.ToString());
            dgCensus.Focus();
        }

        public FactDate CensusDate
        {
            get { return this.censusDate; }
        }
    }
}
