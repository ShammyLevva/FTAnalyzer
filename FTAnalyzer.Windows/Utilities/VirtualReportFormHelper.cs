using FTAnalyzer.Forms.Controls;
using System.Data;

namespace FTAnalyzer.Utilities
{
    internal class VirtualReportFormHelper<T> : ReportFormHelper
    {
        public new VirtualDataGridView<T> ReportGrid { get; private set; }
        public VirtualReportFormHelper(Form parent, string title, VirtualDataGridView<T> report, Action resetTable, string registry, bool saveForm = true)
            : base(parent, title, report, resetTable, registry, saveForm)
        {
            ReportGrid = report;
        }

        public override void DoExportToExcel<S>(DataGridViewColumnCollection shown = null)
        {
            if (ReportGrid.DataSource is null || ReportGrid.RowCount == 0)
                return;
            parent.Cursor = Cursors.WaitCursor;
            SortableBindingList<S> source = ReportGrid.DataSource as SortableBindingList<S> ?? [];
            if (source.Count != 0)
            {
                using DataTable dt = ListtoDataTableConvertor.ToDataTable(source.ToList(), shown);
                ExportToExcel.Export(dt);
            }
            parent.Cursor = Cursors.Default;
            MessageBox.Show($"Excel Export of {source.Count} rows completed");
        }
    }
}
