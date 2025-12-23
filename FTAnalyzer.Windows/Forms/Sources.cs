using FTAnalyzer.Utilities;
using System.ComponentModel;

namespace FTAnalyzer.Forms
{
    public partial class SourcesForm : Form
    {
        readonly SortableBindingList<IDisplaySource> sources;
        readonly ReportFormHelper reportFormHelper;

        public SourcesForm(DisplayFact fact)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            sources = [];
            dgSources.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgSources, true);
            reportFormHelper = new ReportFormHelper(this, Text, dgSources, ResetTable, "Sources");
            if (fact is not null) // checks for not null 
                AddSources(fact);
        }

        void AddSources(DisplayFact fact)
        {
            foreach (FactSource s in fact.Sources)
                if (!sources.Contains(s))
                    sources.Add(s);
            dgSources.DataSource = sources;
            reportFormHelper.LoadColumnLayout("SourcesColumns.xml");
            tsRecords.Text = sources.Count + " Records";
        }

        void ResetTable() => dgSources.Sort(dgSources.Columns["SourceTitle"], ListSortDirection.Ascending);

        void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport("Sources Report");
        }

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintPreviewReport();

        void Sources_TextChanged(object sender, EventArgs e) => reportFormHelper.PrintTitle = this.Text;

        void MnuExportToExcel_Click(object sender, EventArgs e) => reportFormHelper.DoExportToExcel<IDisplaySource>();

        void MnuResetColumns_Click(object sender, EventArgs e) => reportFormHelper.ResetColumnLayout("SourcesColumns.xml");

        void MnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("SourcesColumns.xml");
            UIHelpers.ShowMessage("Form Settings Saved", "Sources");
        }

        void Sources_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void DgSources_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                FactSource? source = (FactSource?)dgSources.CurrentRow.DataBoundItem;
                if (source is null) return;
                Facts factForm = new(source);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        void Sources_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
