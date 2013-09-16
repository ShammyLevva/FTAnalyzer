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
    public partial class ColourBMD : Form
    {

        private PrintingDataGridViewProvider printProvider;
        private Dictionary<int, DataGridViewCellStyle> styles;
        private int birthColumnIndex;
        private int burialColumnIndex;
        private SortableBindingList<IDisplayColourBMD> reportList;

        public ColourBMD(SortableBindingList<IDisplayColourBMD> reportList)
        {
            InitializeComponent();
            this.reportList = reportList;
            styles = new Dictionary<int, DataGridViewCellStyle>();
            DataGridViewCellStyle notRequired = new DataGridViewCellStyle();
            notRequired.BackColor = notRequired.ForeColor = Color.DarkGray;
            styles.Add(0, notRequired);
            DataGridViewCellStyle missingData = new DataGridViewCellStyle();
            missingData.BackColor = missingData.ForeColor = Color.Red;
            styles.Add(1, missingData);
            DataGridViewCellStyle dateRange = new DataGridViewCellStyle();
            dateRange.BackColor = dateRange.ForeColor = Color.DarkOrange;
            styles.Add(2, dateRange);
            DataGridViewCellStyle approxDate = new DataGridViewCellStyle();
            approxDate.BackColor = approxDate.ForeColor = Color.Yellow;
            styles.Add(3, approxDate);
            DataGridViewCellStyle exactDate = new DataGridViewCellStyle();
            exactDate.BackColor = exactDate.ForeColor = Color.Green;
            styles.Add(4, exactDate);
            
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
            birthColumnIndex = dgReportSheet.Columns["C1841"].Index;
            burialColumnIndex = dgReportSheet.Columns["C1911"].Index;
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

        private string CountText(SortableBindingList<IDisplayColourCensus> reportList)
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
            for (int i = birthColumnIndex; i <= burialColumnIndex; i++)
                dgReportSheet.Columns[i].Width = 50;
        }

        private void dgReportSheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex == -1)
            {
                return;
            }
            if (e.ColumnIndex < birthColumnIndex || e.ColumnIndex > burialColumnIndex)
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
            if (e.ColumnIndex >= birthColumnIndex && e.ColumnIndex <= burialColumnIndex)
            {
                DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
                int value = (int)cell.Value;
                if (value == 1 || value == 2)
                {
                    IDisplayColourCensus person = (IDisplayColourCensus)dgReportSheet.Rows[e.RowIndex].DataBoundItem;
                    int censusYear = (1841 + (e.ColumnIndex - birthColumnIndex) * 10);
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

        private List<IDisplayColourCensus> BuildFilter(int toFind, bool all)
        {
            List<IDisplayColourCensus> result = new List<IDisplayColourCensus>();
            foreach(IDisplayColourCensus row in this.reportList)
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
            List<IDisplayColourCensus> list;
            switch (cbFilter.SelectedIndex)
            {
                case -1: // nothing selected
                case 0: // All Individuals
                    dgReportSheet.DataSource = this.reportList;
                    break;
                case 1: // None Found (All Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(1, true));
                    break;
                case 2: // All Found (All Green)
                    list = new List<IDisplayColourCensus>();
                    list.AddRange(BuildFilter(3, true));
                    list.AddRange(BuildFilter(4, true));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(list);
                    break;
                case 3: // Lost Cousins Missing (Yellows)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(2, false));
                    break;
                case 4: // Lost Cousins Present (Orange)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(5, false));
                    break;
                case 5: // Some Missing (Some Red)
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(BuildFilter(1, false));
                    break;
                case 6:
                    list = new List<IDisplayColourCensus>();
                    list.AddRange(BuildFilter(3, false));
                    list.AddRange(BuildFilter(4, false));
                    dgReportSheet.DataSource = new SortableBindingList<IDisplayColourCensus>(list);
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
            List<IDisplayColourCensus> export = (dgReportSheet.DataSource as SortableBindingList<IDisplayColourCensus>).ToList<IDisplayColourCensus>();
            DataTable dt = convertor.ToDataTable(export);
            ExportToExcel.Export(dt);
            this.Cursor = Cursors.Default;
        }
    }
}
