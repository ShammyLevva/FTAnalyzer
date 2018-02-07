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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;

namespace FTAnalyzer.Forms
{
    public partial class Facts : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Individual Individual { get; private set; }
        public Family Family { get; private set; }
        private FamilyTree ft = FamilyTree.Instance;
        private SortableBindingList<IDisplayFact> facts;
        private Font italicFont;
        private Font linkFont;
        private bool allFacts;
        private ReportFormHelper reportFormHelper;
        private bool CensusRefReport;
        private List<string> IgnoreList;

        private Facts()
        {
            InitializeComponent();
            this.facts = new SortableBindingList<IDisplayFact>();
            this.facts.SortFinished += new EventHandler(Grid_SortFinished);
            this.allFacts = false;
            this.CensusRefReport = false;
            dgFacts.AutoGenerateColumns = false;
            ExtensionMethods.DoubleBuffered(dgFacts, true);
            reportFormHelper = new ReportFormHelper(this, this.Text, dgFacts, this.ResetTable, "Facts");
            italicFont = new Font(dgFacts.DefaultCellStyle.Font, FontStyle.Italic);
            linkFont = new Font(dgFacts.DefaultCellStyle.Font, FontStyle.Underline);
            dgFacts.Columns["IndividualID"].Visible = true;
            dgFacts.Columns["CensusReference"].Visible = true; // originally false - trying true v5.0.0.3
            dgFacts.Columns["IgnoreFact"].Visible = false;
            dgFacts.ReadOnly = true;
            sep1.Visible = false;
            btnShowHideFacts.Visible = false;
        }

        private void Grid_SortFinished(object sender, EventArgs e)
        {
            SetBackColour();
        }

        public Facts(Individual individual)
            : this()
        {
            this.Individual = individual;
            AddIndividualsFacts(individual);
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

        public Facts(IEnumerable<Individual> individuals, List<string> factTypes, List<string> excludedTypes)
            : this()
        {
            this.allFacts = true;
            int distinctIndividuals = 0;
            foreach (Individual ind in individuals)
            {
                int before = facts.Count;
                if (factTypes == null)
                    AddIndividualsFacts(ind);
                else
                    AddIndividualsFacts(ind, factTypes, excludedTypes);
                int after = facts.Count;
                if (before != after)
                    distinctIndividuals++;
            }
            string text = distinctIndividuals + " individuals.";
            this.Text = "Facts Report for all " + text + " Facts count: " + facts.Count;
            SetupFacts(text);
        }

        public Facts(IEnumerable<Individual> individuals, List<string> duplicateTypes)
            : this()
        {
            this.allFacts = true;
            int distinctIndividuals = 0;
            foreach (Individual ind in individuals)
            {
                int before = facts.Count;
                AddDuplicateFacts(ind, duplicateTypes);
                int after = facts.Count;
                if (before != after)
                    distinctIndividuals++;
            }
            string text = distinctIndividuals + " individuals.";
            this.Text = "Duplicates Facts Report for all " + text + " Facts count: " + facts.Count;
            SetupFacts(text);
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
            //dgFacts.Columns["CensusReference"].Visible = true;
        }

        public Facts(FactSource source)
            : this()
        {
            this.allFacts = true;
            this.facts = ft.GetSourceDisplayFacts(source);
            this.Text = "Facts Report for source: " + source.ToString() + ". Facts count: " + facts.Count;
            SetupFacts();
            //dgFacts.Columns["CensusReference"].Visible = true;
        }

        public Facts(List<DisplayFact> results)
            : this()
        {
            DeserializeIgnoreList();
            foreach (DisplayFact fact in results)
            {
                fact.IgnoreFact = IgnoreList.Contains(fact.FactHash);
                facts.Add(fact);
            }
            CensusRefReport = true;
            this.Text = "Families with the same census ref but different locations.";
            SetupFacts();
            //dgFacts.Columns["CensusReference"].Visible = true;
            dgFacts.Columns["IgnoreFact"].Visible = true;
            dgFacts.Sort(dgFacts.Columns["DateofBirth"], ListSortDirection.Ascending);
            dgFacts.Sort(dgFacts.Columns["CensusReference"], ListSortDirection.Ascending);
            dgFacts.ReadOnly = false;
            sep1.Visible = true;
            btnShowHideFacts.Visible = true;
        }

        #region IgnoreList
        public void SerializeIgnoreList()
        {
            try
            {
                IFormatter formatter = new BinaryFormatter();
                string file = Path.Combine(Properties.GeneralSettings.Default.SavePath, "IgnoreList.xml");
                using (Stream stream = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    formatter.Serialize(stream, IgnoreList);
                }
            }
            catch (Exception e)
            {
                log.Error("Error " + e.Message + " writing IgnoreList.xml");
            }
        }

        public void DeserializeIgnoreList()
        {
            log.Debug("FamilyTree.DeserializeIgnoreList");
            IgnoreList = new List<string>();
            try
            {
                IFormatter formatter = new BinaryFormatter();
                string file = Path.Combine(Properties.GeneralSettings.Default.SavePath, "IgnoreList.xml");
                if (File.Exists(file))
                {
                    using (Stream stream = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        IgnoreList = (List<string>)formatter.Deserialize(stream);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error("Error " + e.Message + " reading IgnoreList.xml");
            }
        }

        private void DgFacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 1)
            {
                DisplayFact ignoreFact = (DisplayFact)dgFacts.Rows[e.RowIndex].DataBoundItem;
                ignoreFact.IgnoreFact = !(bool)dgFacts.Rows[e.RowIndex].Cells["IgnoreFact"].Value; // value will be value before click so invert it 
                if (ignoreFact.IgnoreFact)
                {  //ignoring this record so add it to the list if its not already present
                    if (!IgnoreList.Contains(ignoreFact.FactHash))
                        IgnoreList.Add(ignoreFact.FactHash);
                }
                else
                    IgnoreList.Remove(ignoreFact.FactHash); // no longer ignoring so remove from list
                SerializeIgnoreList();
            }
            if (e.RowIndex >=0 && e.ColumnIndex == dgFacts.Columns["CensusReference"].Index)
            {
                DisplayFact df = (DisplayFact)dgFacts.Rows[e.RowIndex].DataBoundItem;
                if(df.CensusReference.URL.Length > 0)
                    HttpUtility.VisitWebsite(df.CensusReference.URL);
            }
        }

        private void BtnShowHideFacts_Click(object sender, EventArgs e)
        {
            ShowHideFactRows();
        }

        public void ShowHideFactRows()
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgFacts.DataSource];
            cm.SuspendBinding();
            foreach (DataGridViewRow row in dgFacts.Rows)
            {
                if (btnShowHideFacts.Checked)
                {
                    DisplayFact fact = row.DataBoundItem as DisplayFact;
                    row.Visible = !fact.IgnoreFact;
                }
                else
                    row.Visible = true;
            }
            cm.ResumeBinding();
            dgFacts.Refresh();
        }

        #endregion

        private void AddIndividualsFacts(Individual individual)
        {
            IEnumerable<Fact> list = individual.AllFacts.Union(individual.ErrorFacts.Where(f => f.FactErrorLevel != Fact.FactError.WARNINGALLOW));
            foreach (Fact f in list)
                facts.Add(new DisplayFact(individual, f));
        }

        private void AddIndividualsFacts(Individual individual, List<string> factTypes, List<string> excludedTypes)
        {
            IEnumerable<Fact> list = individual.AllFacts.Union(individual.ErrorFacts.Where(f => f.FactErrorLevel != Fact.FactError.WARNINGALLOW));
            if (factTypes.Count == 0 && excludedTypes != null && !list.Any(x => excludedTypes.Contains(x.FactTypeDescription)))
                facts.Add(new DisplayFact(individual, new Fact(individual.IndividualID, Fact.REPORT, individual.BirthDate, individual.BirthLocation)));
            else
            {
                foreach (Fact f in list)
                    if (factTypes.Contains(f.FactTypeDescription) && !list.Any(x => excludedTypes.Contains(x.FactTypeDescription)))
                        facts.Add(new DisplayFact(individual, f));
            }
        }

        private void AddDuplicateFacts(Individual individual, List<string> factTypes)
        {
            IEnumerable<Fact> list = individual.AllFacts.Union(individual.ErrorFacts.Where(f => f.FactErrorLevel != Fact.FactError.WARNINGALLOW));
            foreach (string factType in factTypes)
            {
                if (list.Count(x => x.FactTypeDescription.Equals(factType)) > 1)
                    foreach (Fact f in list.Where(x => x.FactTypeDescription.Equals(factType)))
                        facts.Add(new DisplayFact(individual, f));
            }
        }

        private void SetupFacts(string extraText = "")
        {
            dgFacts.DataSource = facts;
            reportFormHelper.LoadColumnLayout("FactsColumns.xml");
            tsRecords.Text = facts.Count + " Records " + extraText;
            SetBackColour();
        }

        private void SetBackColour()
        {
            bool backColourGrey = false;
            DisplayFact previous = null;
            foreach (DisplayFact fact in facts)
            {
                if (previous != null)
                    if ((CensusRefReport && previous.CensusReference != fact.CensusReference) || (!CensusRefReport && previous.IndividualID != fact.IndividualID))
                        backColourGrey = !backColourGrey;
#if !__MAC__
                fact.BackColour = backColourGrey ? Color.LightGray : Color.White;
#endif
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

        private void PrintToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintReport("Facts Report");
        }

        private void PrintPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            reportFormHelper.PrintPreviewReport();
        }

        private void Facts_TextChanged(object sender, EventArgs e)
        {
            reportFormHelper.PrintTitle = this.Text;
        }

        private void MnuExportToExcel_Click(object sender, EventArgs e)
        {
            reportFormHelper.DoExportToExcel<IDisplayFact>();
        }

        private void MnuResetColumns_Click(object sender, EventArgs e)
        {
            reportFormHelper.ResetColumnLayout("FactsColumns.xml");
        }

        private void MnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("FactsColumns.xml");
            MessageBox.Show("Form Settings Saved", "Facts");
        }

        private void DgFacts_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DisplayFact f = dgFacts.Rows[e.RowIndex].DataBoundItem as DisplayFact;
                e.ToolTipText = f.Fact.FactErrorMessage;
            }
        }

        private void DgFacts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
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
                if(e.ColumnIndex == dgFacts.Columns["CensusReference"].Index && f.CensusReference != null && f.CensusReference.URL.Length > 0)
                {
                    cell.Style.ForeColor = Color.Blue;
                    cell.Style.Font = linkFont;
                    cell.ToolTipText = "Click link to view census records on Find My Past.";
                }
            }
        }

        private void Facts_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }

        private void DgFacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DisplayFact f = dgFacts.Rows[e.RowIndex].DataBoundItem as DisplayFact;
                if (f.Fact.FactType == Fact.REPORT)
                {
                    Facts person = new Facts(f.Ind);
                    person.Show();
                }
                else
                {
                    Sources sourceForm = new Sources(f);
                    sourceForm.Show();
                }
            }
        }
    }
}
