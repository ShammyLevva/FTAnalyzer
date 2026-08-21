using FTAnalyzer.Exports;
using FTAnalyzer.Utilities;
using System.Data;

namespace FTAnalyzer.Forms
{
    /// <summary>
    /// Desktop presentation for the shared Irish 1911 census helper tool (see
    /// FTAnalyzer.Shared/Exports/IrishCensusHelper.cs) - finds old-style National Archives of
    /// Ireland census citations, looks each one up live, and lets the user export the resolved
    /// list. All the scanning/lookup logic is shared with FTAnalyzer.Web
    /// (Components/Pages/IrishCensusHelper.razor drives the exact same classes); this form is
    /// just the WinForms wiring around it.
    /// </summary>
    public partial class IrishCensusHelperForm : Form
    {
        readonly List<IrishCensusHelperRecord> records;
        CancellationTokenSource? cts;

        public IrishCensusHelperForm()
        {
            InitializeComponent();
            records = IrishCensusHelperScanner.FindOldStyleReferences(FamilyTree.Instance.AllIndividuals);
            lblCount.Text = records.Count == 1
                ? "1 old-style citation found."
                : $"{records.Count:N0} old-style citations found.";
            btnStart.Enabled = records.Count > 0;
            RefreshGrid();
        }

        void RefreshGrid()
        {
            dgResults.Rows.Clear();
            foreach (IrishCensusHelperRecord r in records)
                dgResults.Rows.Add(r.Name, r.OldUrl, r.ResolvedUrl ?? string.Empty, r.Status);
        }

        async void BtnStart_Click(object sender, EventArgs e)
        {
            btnStart.Enabled = false;
            btnCancel.Visible = true;
            btnExport.Enabled = false;
            txtProgress.Clear();
            try
            {
                cts?.Dispose();
                cts = new CancellationTokenSource();
                Progress<string> progress = new(msg => txtProgress.AppendText(msg + Environment.NewLine));
                await IrishCensusHelperProcessor.ResolveAllAsync(records, Program.IrishCensusClient, progress, cts.Token);
            }
            catch (OperationCanceledException)
            {
                txtProgress.AppendText("Cancelled." + Environment.NewLine);
            }
            finally
            {
                RefreshGrid();
                btnStart.Enabled = true;
                btnCancel.Visible = false;
                btnExport.Enabled = records.Any(r => r.ResolvedUrl is not null);
            }
        }

        void BtnCancel_Click(object sender, EventArgs e) => cts?.Cancel();

        void BtnExport_Click(object sender, EventArgs e)
        {
            DataTable dt = new();
            dt.Columns.Add("Name");
            dt.Columns.Add("Old Citation URL");
            dt.Columns.Add("Resolved URL");
            dt.Columns.Add("Status");
            foreach (IrishCensusHelperRecord r in records)
                dt.Rows.Add(r.Name, r.OldUrl, r.ResolvedUrl, r.Status);
            ExportToExcel.Export(dt);
        }

        void IrishCensusHelperForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            cts?.Cancel();
            cts?.Dispose();
        }
    }
}
