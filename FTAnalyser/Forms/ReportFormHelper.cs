using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Printing.DataGridViewPrint.Tools;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Drawing;
using FTAnalyzer.Utilities;
using System.Data;
using System.IO;

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

        public void DoExportToExcel(Form parent)
        {
            parent.Cursor = Cursors.WaitCursor;
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            DataTable dt = convertor.ToDataTable((ReportGrid.DataSource as SortableBindingList<IDisplayFact>).ToList());
            ExportToExcel.Export(dt);
            parent.Cursor = Cursors.Default;
        }

        public void SaveColumnLayout(string filename)
        {
            DataTable dt = new DataTable("table");
            var query = from DataGridViewColumn col in ReportGrid.Columns
                        orderby col.DisplayIndex
                        select col;

            foreach (DataGridViewColumn col in query)
            {
                dt.Columns.Add(col.Name);
            }
            string path = Path.Combine(Properties.GeneralSettings.Default.SavePath, filename);
            dt.WriteXmlSchema(path);
        }

        public void LoadColumnLayout(string filename)
        {
            try
            {
                DataTable dt = new DataTable();
                string path = Path.Combine(Properties.GeneralSettings.Default.SavePath, filename);
                dt.ReadXmlSchema(path);

                int i = 0;
                foreach (DataColumn col in dt.Columns)
                {
                    ReportGrid.Columns[col.ColumnName].DisplayIndex = i;
                    i++;
                }
            }
            catch (Exception)
            {
                ResetColumnLayout(filename);
            }
        }

        public void ResetColumnLayout(string filename)
        {
            for (int i = 0; i < ReportGrid.Columns.Count; i++)
                ReportGrid.Columns[i].DisplayIndex = i;
            SaveColumnLayout(filename);
        }

    }
}
