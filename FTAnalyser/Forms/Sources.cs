using FTAnalyzer.Utilities;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class Sources : Form
    {
        readonly SortableBindingList<IDisplaySource> sources;
        readonly ReportFormHelper reportFormHelper;

        public Sources(DisplayFact fact)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            sources = new SortableBindingList<IDisplaySource>();
            dgSources.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgSources, true);
            reportFormHelper = new ReportFormHelper(this, this.Text, dgSources, this.ResetTable, "Sources");
            AddSources(fact);
        }

        void AddSources(DisplayFact fact)
        {
            foreach (FactSource s in fact.Sources)
                if(!sources.Contains(s))
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
            MessageBox.Show("Form Settings Saved", "Sources");
        }

        void Sources_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void DgSources_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                FactSource source = (FactSource)dgSources.CurrentRow.DataBoundItem;
                Facts factForm = new Facts(source);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        void Sources_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
