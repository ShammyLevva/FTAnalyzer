using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Printing.DataGridViewPrint.Tools;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;

namespace FTAnalyzer.Forms
{
    class ReportFormHelper
    {
        private PrintingDataGridViewProvider printProvider;
        private PrintDocument printDocument;
        private PrintDialog printDialog;
        private PrintPreviewDialog printPreviewDialog;

        public DataGridView ReportGrid { get; private set; }
        public String PrintTitle { get; set; }

        public ReportFormHelper(string title, DataGridView report)
        {
            this.PrintTitle = title;
            this.ReportGrid = report;

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.DefaultPageSettings.Margins =
                new System.Drawing.Printing.Margins(15, 15, 15, 15);

            printProvider = PrintingDataGridViewProvider.Create(
                printDocument, ReportGrid, true, true, true,
                new TitlePrintBlock(PrintTitle), null, null);

            printDialog = new PrintDialog();
            printDialog.AllowSelection = true;
            printDialog.AllowSomePages = true;
            printDialog.Document = printDocument;
            printDialog.UseEXDialog = true;

            printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.AutoScrollMargin = new Size(0, 0);
            printPreviewDialog.AutoScrollMinSize = new Size(0, 0);
            printPreviewDialog.ClientSize = new Size(400, 300);
            printPreviewDialog.Document = printDocument;

        }

        public void PrintReport(Form parent)
        {
            if (printDialog.ShowDialog(parent) == DialogResult.OK)
            {
                printProvider.Drawer.TitlePrintBlock = new TitlePrintBlock(PrintTitle);
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.DocumentName = "Missing from Census Report";
                printDocument.Print();
            }
        }

        public void PrintPreviewReport(Form parent)
        {
            printProvider.Drawer.TitlePrintBlock = new TitlePrintBlock(PrintTitle);
            printPreviewDialog.ShowDialog(parent);
        }
    }
}
