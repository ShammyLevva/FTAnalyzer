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
        public Individual Individual { get; private set; }
        public Family Family { get; private set; }
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
            ExtensionMethods.DoubleBuffered(dgFacts, true);
            reportFormHelper = new ReportFormHelper(this, this.Text, dgFacts, this.ResetTable, "Facts");
            italicFont = new Font(dgFacts.DefaultCellStyle.Font, FontStyle.Italic);
            dgFacts.Columns["IndividualID"].Visible = true;
            dgFacts.Columns["CensusReference"].Visible = false;
        }

        public Facts(Individual individual)
            : this()
        {
            this.Individual = individual;
            AddIndividualsFacts(individual, null);
            this.Text = "Facts Report for " + individual.IndividualID + ": " + individual.Name;
            SetupFacts();
            dgFacts.Columns["IndividualID"].Visible = false; // all same individual so hide ID
        }

        public Facts(Family family)
            : this()
        {
            this.Family = family;
            foreach (DisplayFact f in family.AllDisplayFacts)
                facts.Add(f);
            this.Text = "Facts Report for " + family.FamilyRef;
            SetupFacts();
        }

        public Facts(IEnumerable<Individual> individuals, List<string> factTypes)
            : this()
        {
            this.allFacts = true;
            int distinctIndividuals = 0;
            foreach (Individual ind in individuals)
            {
                int before = facts.Count;
                AddIndividualsFacts(ind, factTypes);
                int after = facts.Count;
                if(before != after)
                    distinctIndividuals++;
            }
            this.Text = "Facts Report for all " + distinctIndividuals + " individuals. Facts count: " + facts.Count;
            SetupFacts();
        }

        public Facts(CensusReference.ReferenceStatus status)
            : this()
        {
            this.allFacts = true;
            foreach (Individual ind in ft.AllIndividuals)
                foreach (Fact f in ind.AllFacts)
                    if (f.IsCensusFact && f.CensusReference != null && f.CensusReference.Status == status)
                        facts.Add(new DisplayFact(ind, f));
            if (status == FTAnalyzer.CensusReference.ReferenceStatus.GOOD)
                this.Text = "Census Reference Report. Facts count: " + facts.Count;
            else if (status == FTAnalyzer.CensusReference.ReferenceStatus.INCOMPLETE)
                this.Text = "Incomplete Census Reference Report. Facts count: " + facts.Count;
            else if (status == FTAnalyzer.CensusReference.ReferenceStatus.UNRECOGNISED)
                this.Text = "Unrecognised Census Reference Report. Facts count: " + facts.Count;
            else if (status == FTAnalyzer.CensusReference.ReferenceStatus.BLANK)
                this.Text = "Blank Census Reference Report. Facts count: " + facts.Count;
            SetupFacts();
            dgFacts.Columns["CensusReference"].Visible = true;
        }

        public Facts(FactSource source)
            : this()
        {
            this.allFacts = true;
            this.facts = ft.GetSourceDisplayFacts(source);
            this.Text = "Facts Report for source: " + source.ToString() + ". Facts count: " + facts.Count;
            SetupFacts();
            dgFacts.Columns["CensusReference"].Visible = true;
        }

        private void AddIndividualsFacts(Individual individual, List<string> factTypes)
        {
            foreach (Fact f in individual.AllFacts)
                if (factTypes == null || factTypes.Contains(f.FactTypeDescription))
                    facts.Add(new DisplayFact(individual, f));
            foreach (Fact f in individual.ErrorFacts)
            {
                // only add ignored and errors as allowed have are in AllFacts
                if (f.FactErrorLevel != Fact.FactError.WARNINGALLOW)
                    if (factTypes == null || factTypes.Contains(f.FactTypeDescription))
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
            reportFormHelper.PrintReport("Facts Report");
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
            MessageBox.Show("Form Settings Saved", "Facts");
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

        private void dgFacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DisplayFact f = dgFacts.Rows[e.RowIndex].DataBoundItem as DisplayFact;
                Sources sourceForm = new Sources(f);
                sourceForm.Show();
            }
        }
    }
}
