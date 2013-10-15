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
using System.ComponentModel;

namespace FTAnalyzer
{
    class ReportFormHelper
    {
        private PrintingDataGridViewProvider printProvider;
        private PrintDocument printDocument;
        private PrintDialog printDialog;
        private PrintPreviewDialog printPreviewDialog;
        private Action resetTable;

        public DataGridView ReportGrid { get; private set; }
        public String PrintTitle { get; set; }

        public ReportFormHelper(string title, DataGridView report, Action resetTable)
        {
            this.PrintTitle = title;
            this.ReportGrid = report;
            this.resetTable = resetTable;

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

        public void DoExportToExcel<T>(Form parent)
        {
            parent.Cursor = Cursors.WaitCursor;
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            DataTable dt = convertor.ToDataTable((ReportGrid.DataSource as SortableBindingList<T>).ToList());
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
                DataColumn dc = new DataColumn(col.Name);
                dc.ExtendedProperties["Width"] = col.Width;
                if (col == ReportGrid.SortedColumn)
                    dc.ExtendedProperties["Sort"] = ReportGrid.SortOrder;
                dt.Columns.Add(dc);
            }
            string path = Path.Combine(Properties.GeneralSettings.Default.SavePath, filename);
            dt.WriteXmlSchema(path);
        }

        public void LoadColumnLayout(string filename)
        {
            try
            {
                this.resetTable();
                DataTable dt = new DataTable();
                string path = Path.Combine(Properties.GeneralSettings.Default.SavePath, filename);
                dt.ReadXmlSchema(path);
                if (dt.Columns.Count == ReportGrid.Columns.Count)
                {   // only load column layout and sort order if save file has same number of columns as current form
                    // this allows for upgrades that add extra columns
                    int i = 0;
                    foreach (DataColumn col in dt.Columns)
                    {
                        ReportGrid.Columns[col.ColumnName].DisplayIndex = i;
                        if (col.ExtendedProperties.Contains("Width"))
                        {
                            int width = 0;
                            if (int.TryParse((string)col.ExtendedProperties["Width"], out width))
                                ReportGrid.Columns[col.ColumnName].Width = width;
                        }
                        if (col.ExtendedProperties.Contains("Sort"))
                        {
                            ListSortDirection direction = "Ascending".Equals(col.ExtendedProperties["Sort"]) ?
                                    ListSortDirection.Ascending :
                                    ListSortDirection.Descending;
                            ReportGrid.Sort(ReportGrid.Columns[col.ColumnName], direction);
                        }
                        i++;
                    }
                }
                else
                    ResetColumnLayout(filename);
            }
            catch (Exception)
            {
                ResetColumnLayout(filename);
            }
        }

        public void ResetColumnLayout(string filename)
        {
            resetTable();
            for (int i = 0; i < ReportGrid.Columns.Count; i++)
                ReportGrid.Columns[i].DisplayIndex = i;
            SaveColumnLayout(filename);
        }

    }
}
