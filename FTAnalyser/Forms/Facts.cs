using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    public partial class Facts : Form
    {
        private Individual individual;
        private Family family;
        private FamilyTree ft = FamilyTree.Instance;
        private SortableBindingList<IDisplayFact> facts;            

        public Facts(Individual individual)
        {
            InitializeComponent();
            this.individual = individual;
            this.facts = new SortableBindingList<IDisplayFact>();
            foreach (Fact f in individual.AllFacts)
                facts.Add(new DisplayFact(individual.Name,f));
            this.Text = "All Facts for " + individual.Name;
            SetupFacts();
        }

        public Facts(Family family)
        {
            InitializeComponent();
            this.family = family;
            this.facts = new SortableBindingList<IDisplayFact>();
            foreach (DisplayFact f in family.AllDisplayFacts)
                facts.Add(f);
            this.Text = "All Facts for " + family.FamilyRef;
            SetupFacts();
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

        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {

        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {

        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {

        }
    }
}
