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

namespace FTAnalyzer.Forms
{
    public partial class Facts : Form
    {
        private Individual individual;
        private Family family;
        private FamilyTree ft = FamilyTree.Instance;
        private SortableBindingList<IDisplayFact> facts;

        private PrintingDataGridViewProvider printProvider;

        public Facts(Individual individual)
        {
            InitializeComponent();
            SetupPrinting();
            this.individual = individual;
            this.facts = new SortableBindingList<IDisplayFact>();
            foreach (Fact f in individual.AllFacts)
                facts.Add(new DisplayFact(individual.Name, f));
            this.Text = "All Facts for " + individual.Name;
            SetupFacts();
        }

        public Facts(Family family)
        {
            InitializeComponent();
            SetupPrinting();
            this.family = family;
            this.facts = new SortableBindingList<IDisplayFact>();
            foreach (DisplayFact f in family.AllDisplayFacts)
                facts.Add(f);
            this.Text = "All Facts for " + family.FamilyRef;
            SetupFacts();
        }

        private void SetupPrinting()
        {
            printDocument.DefaultPageSettings.Margins =
                new System.Drawing.Printing.Margins(40, 40, 40, 40);

            printProvider = PrintingDataGridViewProvider.Create(
                printDocument, dgFacts, true, true, true,
                new TitlePrintBlock(this.Text), null, null);

            printDocument.DefaultPageSettings.Landscape = true;
        }

        private void SetupFacts()
        {
            dgFacts.DataSource = facts;
            dgFacts.Sort(dgFacts.Columns["FactDate"], ListSortDirection.Ascending);
            //LoadColumnLayout();
            ResizeColumns();
            tsRecords.Text = facts.Count + " Records";
        }

        private void ResizeColumns()
        {
            foreach (DataGridViewColumn c in dgFacts.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printDocument.PrinterSettings = printDialog.PrinterSettings;
                printDocument.Print();
            }
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            if (printDialog.ShowDialog(this) == DialogResult.OK)
            {
                printPreviewDialog.ShowDialog(this);
            }
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            ListtoDataTableConvertor convertor = new ListtoDataTableConvertor();
            DataTable dt = convertor.ToDataTable((dgFacts.DataSource as SortableBindingList<IDisplayFact>).ToList());
            ExportToExcel.Export(dt);
            this.Cursor = Cursors.Default;
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {

        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {

        }

        private void Facts_TextChanged(object sender, EventArgs e)
        {
            printProvider.Drawer.TitlePrintBlock = new TitlePrintBlock(this.Text);
        }
    }
}
