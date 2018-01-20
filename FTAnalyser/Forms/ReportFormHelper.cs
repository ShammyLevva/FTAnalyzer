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
    public class ReportFormHelper : IDisposable
    {
        private PrintingDataGridViewProvider printProvider;
        private PrintDocument printDocument;
        private PrintDialog printDialog;
        private PrintPreviewDialog printPreviewDialog;
        private Action resetTable;
        private Form parent;
        private Tuple<int, int> defaultLocation;
        private Tuple<int, int> defaultSize;
        private string registry;
        private bool saveForm;

        public DataGridView ReportGrid { get; private set; }
        public String PrintTitle { get; set; }

        public ReportFormHelper(Form parent, string title, DataGridView report, Action resetTable, string registry, bool saveForm = true)
        {
            this.parent = parent;
            this.defaultLocation = new Tuple<int, int>(parent.Top, parent.Left);
            this.defaultSize = new Tuple<int, int>(parent.Height, parent.Width);
            this.PrintTitle = title;
            this.ReportGrid = report;
            this.resetTable = resetTable;
            this.registry = registry;
            this.saveForm = saveForm;

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

        public void PrintReport(string reportname)
        {
            if (ReportGrid.DataSource == null || ReportGrid.RowCount == 0)
                return;
            if (printDialog.ShowDialog(parent) == DialogResult.OK)
            {
                printProvider.Drawer.TitlePrintBlock = new TitlePrintBlock(PrintTitle);
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.DocumentName = reportname;
                printDocument.Print();
            }
        }

        public void PrintPreviewReport()
        {
            if (ReportGrid.DataSource == null || ReportGrid.RowCount == 0)
                return;
            printProvider.Drawer.TitlePrintBlock = new TitlePrintBlock(PrintTitle);
            printPreviewDialog.ShowDialog(parent);
        }

        public void DoExportToExcel<T>(DataGridViewColumnCollection shown = null)
        {
            if (ReportGrid.DataSource == null || ReportGrid.RowCount == 0)
                return;
            parent.Cursor = Cursors.WaitCursor;
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            SortableBindingList<T> gridDatasource = ReportGrid.DataSource as SortableBindingList<T>;
            DataTable dt = convertor.ToDataTable(gridDatasource.ToList(), shown);
            ExportToExcel.Export(dt);
            parent.Cursor = Cursors.Default;
        }

        public void DoExportToExcel(List<IExportReferrals> list)
        {
            if (list == null || list.Count == 0)
                return;
            parent.Cursor = Cursors.WaitCursor;
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            DataTable dt = convertor.ToDataTable(list);
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
            SaveFormLayout();
        }

        public static Point CheckIsOnScreen(int top, int left)
        {
            Point toCheck = new Point(left, top);
            foreach (Screen s in Screen.AllScreens)
                if (s.Bounds.Contains(toCheck))
                    return toCheck; // its inside bounds so return the point checked
            return new Point(50, 50);
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
                        if (col.ColumnName == "GoogleLocation")
                            col.ColumnName = "FoundLocation";
                        if (col.ColumnName == "GoogleResultType")
                            col.ColumnName = "FoundResultType";
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
            LoadFormLayout();
        }

        public void ResetColumnLayout(string filename)
        {
            resetTable();
            for (int i = 0; i < ReportGrid.Columns.Count; i++)
            {
                ReportGrid.Columns[i].DisplayIndex = i;
               // ReportGrid.Columns[i].Width = ReportGrid.Columns[i].MinimumWidth;
            }
            SaveColumnLayout(filename);
            ResetFormLayout();
        }

        public void LoadFormLayout()
        {
            if (saveForm)
            {
                parent.WindowState = FormWindowState.Normal;
                parent.StartPosition = FormStartPosition.Manual;
                int top = (int)Application.UserAppDataRegistry.GetValue(registry + " position - top", defaultLocation.Item1);
                int left = (int)Application.UserAppDataRegistry.GetValue(registry + " position - left", defaultLocation.Item2);
                Point topLeft = CheckIsOnScreen(top, left);
                parent.Top = topLeft.Y;
                parent.Left = topLeft.X;
                parent.Height = (int)Application.UserAppDataRegistry.GetValue(registry + " size - height", defaultSize.Item1);
                parent.Width = (int)Application.UserAppDataRegistry.GetValue(registry + " size - width", defaultSize.Item2);
            }
        }

        public void SaveFormLayout()
        {
            if (saveForm && parent.WindowState == FormWindowState.Normal)
            {  //only save window size if not maximised or minimised
                Application.UserAppDataRegistry.SetValue(registry + " position - top", parent.Top);
                Application.UserAppDataRegistry.SetValue(registry + " position - left", parent.Left);
                Application.UserAppDataRegistry.SetValue(registry + " size - height", parent.Height);
                Application.UserAppDataRegistry.SetValue(registry + " size - width", parent.Width);
            }
        }

        private void ResetFormLayout()
        {
            if (saveForm)
            {
                parent.Top = defaultLocation.Item1;
                parent.Left = defaultLocation.Item2;
                parent.Height = defaultSize.Item1;
                parent.Width = defaultSize.Item2;
                SaveFormLayout();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                printDocument.Dispose();
                printDialog.Dispose();
                printPreviewDialog.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
