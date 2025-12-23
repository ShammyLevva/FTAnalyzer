using FTAnalyzer.Properties;
using Printing.DataGridViewPrint.Tools;
using System.ComponentModel;
using System.Data;
using System.Drawing.Printing;
using System.Xml;

namespace FTAnalyzer.Utilities
{
    public class ReportFormHelper : IDisposable
    {
        readonly PrintingDataGridViewProvider printProvider;
        readonly PrintDocument printDocument;
        readonly PrintDialog printDialog;
        readonly PrintPreviewDialog printPreviewDialog;
        internal readonly Form parent;
        readonly Tuple<int, int> defaultLocation;
        readonly Tuple<int, int> defaultSize;
        readonly Action _resetTable;
        readonly string _registry;
        readonly bool _saveForm;

        public DataGridView ReportGrid { get; private set; }

        public string PrintTitle { get; set; }

        public ReportFormHelper(Form parent, string title, DataGridView report, Action resetTable, string registry, bool saveForm = true)
        {
            this.parent = parent;
            defaultLocation = new Tuple<int, int>(parent.Top, parent.Left);
            defaultSize = new Tuple<int, int>(parent.Height, parent.Width);
            PrintTitle = title;
            ReportGrid = report;
            _resetTable = resetTable;
            _registry = registry;
            _saveForm = saveForm;

            printDocument = new PrintDocument();
            printDocument.DefaultPageSettings.Landscape = true;
            printDocument.DefaultPageSettings.Margins = new Margins(15, 15, 15, 15);

            printProvider = PrintingDataGridViewProvider.Create(printDocument, ReportGrid, true, true, true, new TitlePrintBlock(PrintTitle), null, null);

            printDialog = new PrintDialog
            {
                AllowSelection = true,
                AllowSomePages = true,
                Document = printDocument,
                UseEXDialog = true
            };

            printPreviewDialog = new PrintPreviewDialog
            {
                AutoScrollMargin = new Size(0, 0),
                AutoScrollMinSize = new Size(0, 0),
                ClientSize = new Size(400, 300),
                Document = printDocument
            };
        }

        public void PrintReport(string reportname)
        {
            if (ReportGrid.DataSource is null || ReportGrid.RowCount == 0)
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
            if (ReportGrid.DataSource is null || ReportGrid.RowCount == 0)
                return;
            printProvider.Drawer.TitlePrintBlock = new TitlePrintBlock(PrintTitle);
            printPreviewDialog.ShowDialog(parent);
        }

        public virtual void DoExportToExcel<T>(DataGridViewColumnCollection shown = null)
        {
            if (ReportGrid.DataSource is null || ReportGrid.RowCount == 0)
                return;
            parent.Cursor = Cursors.WaitCursor;
            SortableBindingList<T> gridDatasource = ReportGrid.DataSource as SortableBindingList<T> ?? [];
            if (gridDatasource.Count != 0)
            {
                using DataTable dt = ListtoDataTableConvertor.ToDataTable(gridDatasource.ToList(), shown);
                ExportToExcel.Export(dt);
            }
            parent.Cursor = Cursors.Default;
            MessageBox.Show($"Excel Export of {gridDatasource.Count} rows completed");
        }

        public void DoExportToExcel(List<IExportReferrals> list)
        {
            if (list is null || list.Count == 0)
                return;
            parent.Cursor = Cursors.WaitCursor;
            using (DataTable dt = ListtoDataTableConvertor.ToDataTable(list))
                ExportToExcel.Export(dt);
            parent.Cursor = Cursors.Default;
        }

        public void SaveColumnLayout(string filename)
        {
            using DataTable dt = new("table");
            var query = from DataGridViewColumn col in ReportGrid.Columns
                        orderby col.DisplayIndex
                        select col;

            foreach (DataGridViewColumn col in query)
            {
                DataColumn dc = new(col.Name);
                dc.ExtendedProperties["Width"] = col.Width;
                if (col == ReportGrid.SortedColumn)
                    dc.ExtendedProperties["Sort"] = ReportGrid.SortOrder;
                dt.Columns.Add(dc);
            }
            string path = Path.Combine(GeneralSettings.Default.SavePath, filename);
            dt.WriteXmlSchema(path);
            SaveFormLayout();
        }

        public static Point CheckIsOnScreen(int top, int left)
        {
            Point toCheck = new(left, top + NativeMethods.TopTaskbarOffset);
            foreach (Screen s in Screen.AllScreens)
                if (s.Bounds.Contains(toCheck))
                    return toCheck; // its inside bounds so return the point checked
            return new Point(50, 50 + NativeMethods.TopTaskbarOffset);
        }

        public void LoadColumnLayout(string filename)
        {
            try
            {
                _resetTable();
                using DataTable dt = new();
                string path = Path.Combine(GeneralSettings.Default.SavePath, filename);
                string xml = File.ReadAllText(path);
                StringReader sreader = new(xml);
                using (XmlReader reader = XmlReader.Create(sreader, new XmlReaderSettings() { XmlResolver = null }))
                {
                    dt.ReadXmlSchema(reader);
                }
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
                            if (int.TryParse(col.ExtendedProperties["Width"].ToString(), out int width))
                                ReportGrid.Columns[col.ColumnName].Width = width;
                        }
                        if (col.ExtendedProperties.Contains("Sort"))
                        {
                            ListSortDirection direction = "Ascending".Equals(col.ExtendedProperties["Sort"]) ?
                                    ListSortDirection.Ascending :
                                    ListSortDirection.Descending;
                            DataGridViewColumn sortCol = ReportGrid.Columns[col.ColumnName];
                            if(sortCol is not null)
                                ReportGrid.Sort(sortCol, direction);
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
            _resetTable();
            for (int i = 0; i < ReportGrid.Columns.Count; i++)
            {
                ReportGrid.Columns[i].DisplayIndex = i;
                // ReportGrid.Columns[i].Width = ReportGrid.Columns[i].MinimumWidth;
            }
            SaveColumnLayout(filename);
        }

        public void LoadFormLayout()
        {
            if (_saveForm)
            {
                parent.WindowState = FormWindowState.Normal;
                parent.StartPosition = FormStartPosition.Manual;
                int top = (int)Application.UserAppDataRegistry.GetValue(_registry + " position - top", defaultLocation.Item1);
                int left = (int)Application.UserAppDataRegistry.GetValue(_registry + " position - left", defaultLocation.Item2);
                Point topLeft = CheckIsOnScreen(top, left);
                parent.Top = topLeft.Y;
                parent.Left = topLeft.X;
                parent.Height = (int)Application.UserAppDataRegistry.GetValue(_registry + " size - height", defaultSize.Item1);
                parent.Width = (int)Application.UserAppDataRegistry.GetValue(_registry + " size - width", defaultSize.Item2);
            }
        }

        public void SaveFormLayout()
        {
            if (_saveForm && parent.WindowState == FormWindowState.Normal)
            {  //only save window size if not maximised or minimised
                Application.UserAppDataRegistry.SetValue(_registry + " position - top", parent.Top);
                Application.UserAppDataRegistry.SetValue(_registry + " position - left", parent.Left);
                Application.UserAppDataRegistry.SetValue(_registry + " size - height", parent.Height);
                Application.UserAppDataRegistry.SetValue(_registry + " size - width", parent.Width);
            }
        }

        //void ResetFormLayout()
        //{
        //    if (_saveForm)
        //    {
        //        parent.Top = defaultLocation.Item1;
        //        parent.Left = defaultLocation.Item2;
        //        parent.Height = defaultSize.Item1;
        //        parent.Width = defaultSize.Item2;
        //        SaveFormLayout();
        //    }
        //}

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing)
                {
                    printDocument.Dispose();
                    printDialog.Dispose();
                    printPreviewDialog.Dispose();
                }
            }
            catch (Exception) { }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
