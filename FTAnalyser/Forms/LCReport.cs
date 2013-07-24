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

namespace FTAnalyzer.Forms
{
    public partial class LCReport : Form
    {

        private PrintingDataGridViewProvider printProvider;
        private Dictionary<int, DataGridViewCellStyle> styles;

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
            dgReportSheet.Sort(dgReportSheet.Columns[2], ListSortDirection.Ascending);
            ResizeColumns();
            tsRecords.Text = CountText(reportList);
        }

        private class CensusCount
        {
            public int year { get; set; }
            public int count { get; set; }

            public CensusCount(int year, int count) { this.year = year; this.count = count; }

            public override bool Equals(Object that)
            {
                if (that == null || !(that is CensusCount))
                    return false;
                CensusCount c = (CensusCount)that;
                // two CensusCounts are equal if same year and count
                return c.year == this.year && c.count == this.count;
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }

        private string CountText(SortableBindingList<IDisplayLCReport> reportList)
        {

            StringBuilder output = new StringBuilder("Count : " + reportList.Count + " records listed.");
/*
 *          Dictionary<CensusCount, int> totals = new Dictionary<CensusCount, int>();
            for (int census = 1841; census <= 1911; census += 10)
                for (int i = 0; i <= 4; i++)
                    totals[new CensusCount(census, i)] = 0;

            foreach (IDisplayLCReport r in reportList)
            {
                totals[new CensusCount(1841, r.C1841)]++;
                totals[new CensusCount(1851, r.C1851)]++;
                totals[new CensusCount(1861, r.C1861)]++;
                totals[new CensusCount(1871, r.C1871)]++;
                totals[new CensusCount(1881, r.C1881)]++;
                totals[new CensusCount(1891, r.C1891)]++;
                totals[new CensusCount(1901, r.C1901)]++;
                totals[new CensusCount(1911, r.C1911)]++;
            }
*/
            return output.ToString();
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgReportSheet.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }

        private void dgReportSheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex <= 4)
            {
                return;
            }
            DataGridViewCellStyle style = dgReportSheet.DefaultCellStyle;
            DataGridViewCell cell = dgReportSheet.Rows[e.RowIndex].Cells[e.ColumnIndex];
            int value = (int)cell.Value;
            styles.TryGetValue(value, out style);
            if (style != null)
            {
                e.CellStyle.BackColor = style.BackColor;
                e.CellStyle.ForeColor = style.ForeColor;
                switch(value)
                {
                    case 0 :
                        cell.ToolTipText = "Not alive at time of census.";
                        break;
                    case 1 : 
                        cell.ToolTipText = "No census information entered.";
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
    }
}
