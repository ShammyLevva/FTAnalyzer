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
    public partial class Facts : Form
    {
        private Individual individual;
        private Family family;
        private FamilyTree ft = FamilyTree.Instance;
        private SortableBindingList<IDisplayFact> facts;
        private Font italicFont;
        private bool allFacts;
        private ReportFormHelper reportFormHelper;

        private Facts()
        {
            InitializeComponent();
            this.facts = new SortableBindingList<IDisplayFact>();
            this.allFacts = false;
            dgFacts.AutoGenerateColumns = false;
            reportFormHelper = new ReportFormHelper(this, this.Text, dgFacts, this.ResetTable, "Facts");
            italicFont = new Font(dgFacts.DefaultCellStyle.Font, FontStyle.Italic);
        }

        public Facts(Individual individual)
            : this()
        {
            this.individual = individual;
            AddIndividualsFacts(individual);
            this.Text = "Facts Report for " + individual.IndividualID + ": " + individual.Name;
            SetupFacts();
            dgFacts.Columns["IndividualID"].Visible = false;
        }

        public Facts(Family family)
            : this()
        {
            this.family = family;
            foreach (DisplayFact f in family.AllDisplayFacts)
                facts.Add(f);
            this.Text = "Facts Report for " + family.FamilyRef;
            SetupFacts();
            dgFacts.Columns["IndividualID"].Visible = true;
        }

        public Facts(IEnumerable<Individual> individuals)
            : this()
        {
            this.allFacts = true;
            foreach (Individual ind in individuals)
                AddIndividualsFacts(ind);
            this.Text = "Facts Report for all " + individuals.Count() + " individuals. Facts count: " + facts.Count;
            SetupFacts();
            dgFacts.Columns["IndividualID"].Visible = true;
        }

        private void AddIndividualsFacts(Individual individual)
        {
            foreach (Fact f in individual.AllFacts)
                facts.Add(new DisplayFact(individual, f));
            foreach (Fact f in individual.ErrorFacts)
            {
                // only add ignored and errors as allowed have are in AllFacts
                if (f.FactErrorLevel != Fact.FactError.WARNINGALLOW)
                    facts.Add(new DisplayFact(individual, f));
            }
        }

        private void SetupFacts()
        {
            dgFacts.DataSource = facts;
            reportFormHelper.LoadColumnLayout("FactsColumns.xml");
            tsRecords.Text = facts.Count + " Records";
        }

        private void SetBackColour()
        {
            bool backColourGrey = false;
            DisplayFact previous = null;
            foreach (DisplayFact fact in facts)
            {
                if (previous != null && previous.IndividualID != fact.IndividualID)
                    backColourGrey = !backColourGrey;
                fact.BackColour = backColourGrey ? Color.LightGray : Color.White;
                previous = fact;
            }
        }

        private void ResetTable()
        {
            if (allFacts)
            {
                dgFacts.Sort(dgFacts.Columns["FactDate"], ListSortDirection.Ascending);
                dgFacts.Sort(dgFacts.Columns["Forenames"], ListSortDirection.Ascending);
                dgFacts.Sort(dgFacts.Columns["Surname"], ListSortDirection.Ascending);
            }
            else
                dgFacts.Sort(dgFacts.Columns["FactDate"], ListSortDirection.Ascending);
            SetBackColour();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport();
        }

        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        private void Facts_TextChanged(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = this.Text;
        }

        private void mnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayFact>();
        }

        private void mnuResetColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("FactsColumns.xml");
        }

        private void mnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("FactsColumns.xml");
            MessageBox.Show("Column Settings Saved", "Facts");
        }

        private void dgFacts_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DisplayFact f = dgFacts.Rows[e.RowIndex].DataBoundItem as DisplayFact;
                e.ToolTipText = f.Fact.FactErrorMessage;
            }
        }

        private void dgFacts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                DisplayFact f = dgFacts.Rows[e.RowIndex].DataBoundItem as DisplayFact;
                DataGridViewCell cell = dgFacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
                if (f.Fact.FactErrorLevel != Fact.FactError.GOOD)
                {
                    cell.InheritedStyle.Font = italicFont;
                    cell.ToolTipText = "Fact is inaccurate but is being used due to Tolerate slightly inaccurate census dates option.";
                    if (f.Fact.FactErrorLevel != Fact.FactError.WARNINGALLOW)
                    {
                        cell.InheritedStyle.ForeColor = Color.Red; // if ignoring facts then set as red
                        cell.ToolTipText = "Fact is an error and isn't being used";
                    }
                }
                cell.Style.BackColor = f.BackColour;
            }
        }

        private void Facts_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
        
        private void Facts_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
