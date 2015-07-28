using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Utilities;
using Printing.DataGridViewPrint.Tools;
using System.IO;

namespace FTAnalyzer.Forms
{
    public partial class Sources : Form
    {
        private FamilyTree ft = FamilyTree.Instance;
        private SortableBindingList<IDisplaySource> sources;
        private ReportFormHelper reportFormHelper;

        public Sources(DisplayFact fact)
        {
            InitializeComponent();
            this.sources = new SortableBindingList<IDisplaySource>();
            dgSources.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgSources, true);
            reportFormHelper = new ReportFormHelper(this, this.Text, dgSources, this.ResetTable, "Sources");
            AddSources(fact);
        }

        private void AddSources(DisplayFact fact)
        {
            foreach (FactSource s in fact.Sources)
                if(!sources.Contains(s))
                    sources.Add(s);
            dgSources.DataSource = sources;
            reportFormHelper.LoadColumnLayout("SourcesColumns.xml");
            tsRecords.Text = sources.Count + " Records";
        }
        
        private void ResetTable()
        {
            dgSources.Sort(dgSources.Columns["SourceTitle"], ListSortDirection.Ascending);
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport("Sources Report");
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        private void Sources_TextChanged(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = this.Text;
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplaySource>();
        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("SourcesColumns.xml");
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("SourcesColumns.xml");
            MessageBox.Show("Form Settings Saved", "Sources");
        }

        private void Sources_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void dgSources_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                FactSource source = (FactSource)dgSources.CurrentRow.DataBoundItem;
                Facts factForm = new Facts(source);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }   
    }
}
