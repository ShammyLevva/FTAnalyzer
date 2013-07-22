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
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgReportSheet.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }

        private void dgReportSheet_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex == -1 || e.ColumnIndex <= 7)
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
                //e.CellStyle.ForeColor = style.ForeColor;
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
