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

        public LCReport(SortableBindingList<IDisplayLCReport> reportList)
        {
            InitializeComponent();
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

            printDocument.DefaultPageSettings.Margins =
               new System.Drawing.Printing.Margins(40, 40, 40, 40);

            printProvider = PrintingDataGridViewProvider.Create(
                printDocument, dgReportSheet, true, true, true,
                new TitlePrintBlock(this.Text), null, null);

            printDocument.DefaultPageSettings.Landscape = true;

            dgReportSheet.DataSource = reportList;
            // Sort by birth date, then forenames, then surname to get the final order required.
            dgReportSheet.Sort(dgReportSheet.Columns["BirthDate"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Forenames"], ListSortDirection.Ascending);
            dgReportSheet.Sort(dgReportSheet.Columns["Surname"], ListSortDirection.Ascending);
            c1841ColumnIndex = dgReportSheet.Columns["C1841"].Index;
            c1911ColumnIndex = dgReportSheet.Columns["C1911"].Index;
            ResizeColumns();
            tsRecords.Text = CountText(reportList);

            cbCensusSearchProvider.SelectedIndex = 0;
        }

        private string CountText(SortableBindingList<IDisplayLCReport> reportList)
        {

            StringBuilder output = new StringBuilder("Count : " + reportList.Count + " records listed.");

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
                    }
                }
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

        private void dgReportSheet_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
            int value = (int)cell.Value;
            if (value == 1 || value == 2)
            {
                IDisplayLCReport person = (IDisplayLCReport)dgReportSheet.Rows[e.RowIndex].DataBoundItem;
                int censusYear = (1841 + (e.ColumnIndex - c1841ColumnIndex) * 10);
                FamilyTree ft = FamilyTree.Instance;
                ft.SearchCensus(censusYear, ft.getIndividual(person.IndividualID), cbCensusSearchProvider.SelectedIndex);
            }

        }
    }
}
