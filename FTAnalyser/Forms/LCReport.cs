using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Printing.DataGridViewPrint.Tools;
using FTAnalyzer.Utilities;
using System.Web;
using System.Diagnostics;

namespace FTAnalyzer.Forms
{
    public partial class LCReport : Form
    {

        private PrintingDataGridViewProvider printProvider;
        private Dictionary<int, DataGridViewCellStyle> styles;
        private int c1841ColumnIndex;
        private int c1911ColumnIndex;
        private SortableBindingList<IDisplayLCReport> reportList;

        public LCReport(SortableBindingList<IDisplayLCReport> reportList)
        {
            InitializeComponent();
            this.reportList = reportList;
            styles = new Dictionary<int, DataGridViewCellStyle>();
            DataGridViewCellStyle notAlive = new DataGridViewCellStyle();
            notAlive.BackColor = notAlive.ForeColor = Color.DarkGray;
            styles.Add(0, notAlive);
            DataGridViewCellStyle missingCensus = new DataGridViewCellStyle();
            missingCensus.BackColor = missingCensus.ForeColor = Color.Red;
            styles.Add(1, missingCensus);
            DataGridViewCellStyle censusMissingLC = new DataGridViewCellStyle();
            censusMissingLC.BackColor = censusMissingLC.ForeColor = Color.Yellow;
            styles.Add(2, censusMissingLC);
            DataGridViewCellStyle notCensusEnterednotLCYear = new DataGridViewCellStyle();
            notCensusEnterednotLCYear.BackColor = notCensusEnterednotLCYear.ForeColor = Color.Green;
            styles.Add(3, notCensusEnterednotLCYear);
            DataGridViewCellStyle allEntered = new DataGridViewCellStyle();
            allEntered.BackColor = allEntered.ForeColor = Color.Green;
            styles.Add(4, allEntered);
            DataGridViewCellStyle lcNoCensus = new DataGridViewCellStyle();
            lcNoCensus.BackColor = lcNoCensus.ForeColor = Color.DarkOrange;
            styles.Add(5, lcNoCensus);

            printDocument.DefaultPageSettings.Margins =
               new System.Drawing.Printing.Margins(15,15,15,15);

            printProvider = PrintingDataGridViewProvider.Create(
                printDocument, dgReportSheet, true, true, true,
                new TitlePrintBlock("Lost Cousins Census Report"), null, null);

            printDocument.DefaultPageSettings.Landscape = true;

            dgReportSheet.DataSource = reportList;
            // Sort by birth date, then forenames, then surname to get the final order required.
            dgReportSheet.Sort(dgReportSheet.Columns["BirthDate"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Forenames"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Surname"], ListSortDirection.Ascending);
            c1841ColumnIndex = dgReportSheet.Columns["C1841"].Index;
            c1911ColumnIndex = dgReportSheet.Columns["C1911"].Index;
            ResizeColumns();
            tsRecords.Text = "Count : " + reportList.Count + " records listed.";
            string defaultProvider = (string)Application.UserAppDataRegistry.GetValue("Default Search Provider");
            if (defaultProvider == null)
            {
                defaultProvider = "Ancestry";
            }
            cbCensusSearchProvider.Text = defaultProvider;
            cbFilter.Text = "All Individuals";
        }

        private string CountText(SortableBindingList<IDisplayLCReport> reportList)
        {

            StringBuilder output = new StringBuilder();

            //Dictionary<int, int> totals = new Dictionary<int, int>();
            //for (int census = 1841; census <= 1911; census += 10)
            //    for (int i = 0; i <= 4; i++)
            //        totals[census * 10 + i] = 0;

            //foreach (IDisplayLCReport r in reportList)
            //{
            //    totals[18410 + r.C1841]++;
            //    totals[18510 + r.C1851]++;
            //    totals[18610 + r.C1861]++;
            //    totals[18710 + r.C1871]++;
            //    totals[18810 + r.C1881]++;
            //    totals[18910 + r.C1891]++;
            //    totals[19010 + r.C1901]++;
            //    totals[19110 + r.C1911]++;
            //}

            //for (int census = 1841; census <= 1911; census += 10)
            //{
            //    output.Append(census);
            //    output.Append(":");
            //    output.Append(totals[census * 10 + 1]);
            //    output.Append("/");
            //    output.Append(totals[census * 10 + 2]);
            //    output.Append("/");
            //    output.Append(totals[census * 10 + 3] + totals[census * 10 + 4]);
            //    output.Append(" ");
            //}

            return output.ToString();
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgReportSheet.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            for (int i = c1841ColumnIndex; i <= c1911ColumnIndex; i++)
                dgReportSheet.Columns[i].Width = 50;
        }

        private void dgReportSheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex < c1841ColumnIndex || e.ColumnIndex > c1911ColumnIndex)
            {
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells["Relation"];
                string relation = (string)cell.Value;
                if (relation == "Direct Ancestor")
                {
                    e.CellStyle.Font = new Font(dgReportSheet.DefaultCellStyle.Font, FontStyle.Bold);
                }
            }
            else
            {
                DataGridViewCellStyle style = dgReportSheet.DefaultCellStyle;
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                int value = (int)cell.Value;
                styles.TryGetValue(value, out style);
                if (style != null)
                {
                    e.CellStyle.BackColor = style.BackColor;
                    e.CellStyle.ForeColor = style.ForeColor;
                    e.CellStyle.SelectionForeColor = e.CellStyle.SelectionBackColor;
                    switch (value)
                    {
                        case 0:
                            cell.ToolTipText = "Not alive at time of census.";
                            break;
                        case 1:
                            cell.ToolTipText = "No census information entered. Double click to search " + cbCensusSearchProvider.SelectedItem + ".";
                            break;
                        case 2:
                            cell.ToolTipText = "Census entered but no Lost Cousins flag set.";
                            break;
                        case 3:
                            cell.ToolTipText = "Census entered and not a Lost Cousins year.";
                            break;
                        case 4:
                            cell.ToolTipText = "Census entered and flagged as entered on Lost Cousins.";
                            break;
                        case 5:
                            cell.ToolTipText = "Lost Cousins flagged but no Census entered.";
                            break;
                    }
                }
            }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.DocumentName = "Lost Cousins Census Report";
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

        private void dgReportSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= c1841ColumnIndex && e.ColumnIndex <= c1911ColumnIndex)
            {
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                int value = (int)cell.Value;
                if (value == 1 || value == 2)
                {
                    IDisplayLCReport person = (IDisplayLCReport)dgReportSheet.Rows[e.RowIndex].DataBoundItem;
                    int censusYear = (1841 + (e.ColumnIndex - c1841ColumnIndex) * 10);
                    FamilyTree ft = FamilyTree.Instance;
                    string censusCountry = person.BestLocation(new FactDate(censusYear.ToString())).CensusCountry;
                    ft.SearchCensus(censusCountry, censusYear, ft.GetIndividual(person.IndividualID), cbCensusSearchProvider.SelectedIndex);
                }
            }
        }

        private void cbCensusSearchProvider_SelectedIndexChanged(object sender, EventArgs e)
        {
            Application.UserAppDataRegistry.SetValue("Default Search Provider", cbCensusSearchProvider.SelectedItem.ToString());
            dgReportSheet.Focus();
        }

        private List<IDisplayLCReport> BuildFilter(int toFind, bool all)
        {
            List<IDisplayLCReport> result = new List<IDisplayLCReport>();
            foreach(IDisplayLCReport row in this.reportList)
            {
                if (all)
                {
                    if ((row.C1841 == toFind || row.C1841 == 0) && (row.C1851 == toFind || row.C1851 == 0) &&
                        (row.C1861 == toFind || row.C1861 == 0) && (row.C1871 == toFind || row.C1871 == 0) &&
                        (row.C1881 == toFind || row.C1881 == 0) && (row.C1891 == toFind || row.C1891 == 0) &&
                        (row.C1901 == toFind || row.C1901 == 0) && (row.C1911 == toFind || row.C1911 == 0) &&
                        !(row.C1841 == 0 && row.C1851 == 0 && row.C1861 == 0 && row.C1871 == 0 && 
                          row.C1881 == 0 && row.C1891 == 0 && row.C1901 == 0 && row.C1911 == 0 && toFind != 0)) // exclude all greys
                        result.Add(row);
                }
                else
                {
                    if (row.C1841 == toFind || row.C1851 == toFind || row.C1861 == toFind || row.C1871 == toFind ||
                       row.C1881 == toFind || row.C1891 == toFind || row.C1901 == toFind || row.C1911 == toFind)
                        result.Add(row);
                }

            }   
            return result;
        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            List<IDisplayLCReport> list;
            switch (cbFilter.SelectedIndex)
            {
                case -1: // nothing selected
                case 0: // All Individuals
                    dgReportSheet.DataSource = this.reportList;
                    break;
                case 1: // Not Alive (All Grey)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayLCReport>(BuildFilter(0, true));
                    break;
                case 2: // None Found (All Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayLCReport>(BuildFilter(1, true));
                    break;
                case 3: // All Found (All Green)
                    list = new List<IDisplayLCReport>();
                    list.AddRange(BuildFilter(3, true));
                    list.AddRange(BuildFilter(4, true));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayLCReport>(list);
                    break;
                case 4: // Lost Cousins Missing (Yellows)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayLCReport>(BuildFilter(2, false));
                    break;
                case 5: // Lost Cousins Present (Orange)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayLCReport>(BuildFilter(5, false));
                    break;
                case 6: // Some Missing (Some Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayLCReport>(BuildFilter(1, false));
                    break;
                case 7:
                    list = new List<IDisplayLCReport>();
                    list.AddRange(BuildFilter(3, false));
                    list.AddRange(BuildFilter(4, false));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayLCReport>(list);
                    break;
            }
            ResizeColumns();
            dgReportSheet.Focus();
            tsRecords.Text = "Count : " + dgReportSheet.RowCount + " records listed.";
            this.Cursor = Cursors.Default;
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            List<IDisplayLCReport> export = (dgReportSheet.DataSource as SortableBindingList<IDisplayLCReport>).ToList<IDisplayLCReport>();
            DataTable dt = convertor.ToDataTable(export);
            ExportToExcel.Export(dt);
            this.Cursor = Cursors.Default;
        }
    }
}
