﻿using FTAnalyzer.Filters;
using FTAnalyzer.Utilities;
using FTAnalyzer.Properties;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;
using System.Diagnostics;

namespace FTAnalyzer.Forms
{
    public partial class Facts : Form
    {
        public Individual Individual { get; private set; }
        public Family Family { get; private set; }

        readonly FamilyTree ft = FamilyTree.Instance;
        readonly SortableBindingList<IDisplayFact> facts;
        readonly Font italicFont;
        readonly Font linkFont;
        readonly bool allFacts;
        readonly ReportFormHelper reportFormHelper;
        readonly bool CensusRefReport;
        List<string> IgnoreList;

        Facts()
        {
            try
            {
                InitializeComponent();
                Top += NativeMethods.TopTaskbarOffset;
                facts = new SortableBindingList<IDisplayFact>();
                facts.SortFinished += new EventHandler(Grid_SortFinished);
                allFacts = false;
                CensusRefReport = false;
                dgFacts.AutoGenerateColumns = false;
                ExtensionMethods.DoubleBuffered(dgFacts, true);
                reportFormHelper = new ReportFormHelper(this, Text, dgFacts, ResetTable, "Facts");
                italicFont = new(dgFacts.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Italic);
                linkFont = new(dgFacts.DefaultCellStyle.Font.FontFamily, FontSettings.Default.FontSize, FontStyle.Underline);
                dgFacts.Columns["IndividualID"].Visible = true;
                dgFacts.Columns["CensusReference"].Visible = true;
                dgFacts.Columns["IgnoreFact"].Visible = false;
                dgFacts.ReadOnly = true;
                dgFacts.RowTemplate.Height = (int)(FontSettings.Default.FontHeight * GraphicsUtilities.GetCurrentScaling());
                sep1.Visible = false;
                btnShowHideFacts.Visible = false;
            }
            catch (Exception) { }
        }

        void Grid_SortFinished(object? sender, EventArgs e) => SetBackColour();

        public Facts(Individual individual)
            : this()
        {
            Individual = individual;
            AddIndividualsFacts(individual);
            Text = $"Facts Report for {individual.IndividualID}: {individual.Name}";
            SetupFacts();
            dgFacts.Columns["IndividualID"].Visible = false; // all same individual so hide ID
            Analytics.TrackAction(Analytics.FactsFormAction, Analytics.FactsIndividualsEvent);
        }

        public Facts(Family family)
            : this()
        {
            Family = family;
            foreach (DisplayFact f in family.AllDisplayFacts)
                facts.Add(f);
            Text = $"Facts Report for {family.FamilyRef}";
            SetupFacts();
            Analytics.TrackAction(Analytics.FactsFormAction, Analytics.FactsFamiliesEvent);
        }

        public enum AlternateFacts { AllFacts, PreferredOnly, AlternateOnly }

        public Facts(IEnumerable<Individual> individuals, List<string> factTypes, List<string> excludedTypes, AlternateFacts alternateFacts)
            : this()
        {
            allFacts = true;
            int distinctIndividuals = 0;
            foreach (Individual ind in individuals)
            {
                int before = facts.Count;
                if (factTypes is null)
                    AddIndividualsFacts(ind);
                else
                    AddIndividualsFacts(ind, factTypes, excludedTypes, alternateFacts);
                int after = facts.Count;
                if (before != after)
                    distinctIndividuals++;
            }
            string text = $"{distinctIndividuals} individuals.";
            Text = $"Facts Report for all {text} Facts count: {facts.Count}";
            SetupFacts(text);
            Analytics.TrackAction(Analytics.FactsFormAction, Analytics.FactsGroupIndividualsEvent);
        }

        public Facts(IEnumerable<Individual> individuals, List<string> duplicateTypes)
            : this()
        {
            allFacts = true;
            int distinctIndividuals = 0;
            foreach (Individual ind in individuals)
            {
                int before = facts.Count;
                AddDuplicateFacts(ind, duplicateTypes);
                int after = facts.Count;
                if (before != after)
                    distinctIndividuals++;
            }
            string text = $"{distinctIndividuals} individuals.";
            Text = $"Duplicates Facts Report for all{text} Facts count: {facts.Count}";
            SetupFacts(text);
            Analytics.TrackAction(Analytics.FactsFormAction, Analytics.FactsDuplicatesEvent);
        }

        public Facts(CensusReference.ReferenceStatus status, Predicate<Individual> filter, CensusDate censusDate)
            : this()
        {
            allFacts = true;
            IEnumerable<Individual> listToCheck = ft.AllIndividuals.Filter(filter);
            foreach (Individual ind in listToCheck)
            {
                foreach (Fact f in ind.AllFacts)
                    if (f.IsCensusFact && f.FactDate.Overlaps(censusDate) && f.CensusReference is not null && f.CensusReference.Status == status)
                        facts.Add(new DisplayFact(ind, f));
            }
            if (status == FTAnalyzer.CensusReference.ReferenceStatus.GOOD)
                Text = $"Census Reference Report. Facts count: {facts.Count}";
            else if (status == FTAnalyzer.CensusReference.ReferenceStatus.INCOMPLETE)
                Text = $"Incomplete Census Reference Report. Facts count: {facts.Count}";
            else if (status == FTAnalyzer.CensusReference.ReferenceStatus.UNRECOGNISED)
                Text = $"Unrecognised Census Reference Report. Facts count: {facts.Count}";
            else if (status == FTAnalyzer.CensusReference.ReferenceStatus.BLANK)
                Text = $"Blank Census Reference Report. Facts count: {facts.Count}";
            SetupFacts();
            //dgFacts.Columns["CensusReference"].Visible = true;
            Analytics.TrackAction(Analytics.FactsFormAction, Analytics.FactsCensusRefEvent);
        }

        public Facts(Predicate<Individual> filter, bool errors)
            : this()
        {
            allFacts = true;
            IEnumerable<Individual> listToCheck = ft.AllIndividuals.Filter(filter);
            foreach (Individual ind in listToCheck)
            {
                IList<Fact> factsToCheck = errors ? ind.ErrorFacts : ind.Facts;
                foreach (Fact f in factsToCheck)
                {
                    if (errors)
                    {
                        if (f.IsCensusFact)
                            facts.Add(new DisplayFact(ind, f));
                    }
                    else
                    {
                        if (f.FactType == Fact.CENSUS_FTA)
                            facts.Add(new DisplayFact(ind, f));
                    }
                }
            }
            SetupFacts();
            //Analytics.TrackAction();
        }

        public Facts(FactSource source)
            : this()
        {
            allFacts = true;
            facts = FamilyTree.GetSourceDisplayFacts(source);
            Text = $"Facts Report for source: {source}. Facts count: {facts.Count}";
            SetupFacts();
            //dgFacts.Columns["CensusReference"].Visible = true;
            Analytics.TrackAction(Analytics.FactsFormAction, Analytics.FactsSourceEvent);
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
            Text = "Families with the same census ref but different locations.";
            SetupFacts();
            //dgFacts.Columns["CensusReference"].Visible = true;
            dgFacts.Columns["IgnoreFact"].Visible = true;
            dgFacts.Sort(dgFacts.Columns["DateofBirth"], ListSortDirection.Ascending);
            dgFacts.Sort(dgFacts.Columns["CensusReference"], ListSortDirection.Ascending);
            dgFacts.ReadOnly = false;
            sep1.Visible = true;
            btnShowHideFacts.Visible = true;
            Analytics.TrackAction(Analytics.FactsFormAction, Analytics.FactsCensusRefIssueEvent);
        }

        #region IgnoreList
        public void SerializeIgnoreList()
        {
            try
            {
                string jsonFile = Path.Combine(GeneralSettings.Default.SavePath, "IgnoreList.json");
                string ignoreList = JsonSerializer.Serialize(IgnoreList);
                File.WriteAllText(jsonFile, ignoreList);
            }
            catch (Exception)
            {
                //log.Error("Error " + e.Message + " writing IgnoreList.xml");
            }
        }

        public void DeserializeIgnoreList()
        {
            IgnoreList = new List<string>();
            try
            {
                string xmlfile = Path.Combine(GeneralSettings.Default.SavePath, "IgnoreList.xml");
                string jsonFile = Path.Combine(GeneralSettings.Default.SavePath, "IgnoreList.json");
                //if (File.Exists(xmlfile))
                //    ConvertIgnoreListXMLToJson(xmlfile);
                if (File.Exists(jsonFile))
                    IgnoreList = JsonSerializer.Deserialize<List<string>>(File.ReadAllText(jsonFile));
                else
                    IgnoreList = new List<string>();
            }
            catch (Exception ex)
            {
                Debug.Print($"Error {ex.Message} reading IgnoreList file");
                IgnoreList = new List<string>();
            }
        }

        //void ConvertIgnoreListXMLToJson(string xmlFile)
        //{
        //    IFormatter formatter = new BinaryFormatter();
        //    using Stream stream = new FileStream(xmlFile, FileMode.Open, FileAccess.Read, FileShare.Read);
        //    IgnoreList = (List<string>)formatter.Deserialize(stream);
        //    string jsonFile = Path.Combine(GeneralSettings.Default.SavePath, "IgnoreList.json");
        //    string nonDuplicates = JsonSerializer.Serialize(IgnoreList);
        //    File.WriteAllText(jsonFile, nonDuplicates);
        //    File.Delete(xmlFile);
        //}


        void DgFacts_CellContentClick(object sender, DataGridViewCellEventArgs e)
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
            if (e.RowIndex >= 0 && e.ColumnIndex == dgFacts.Columns["CensusReference"].Index)
            {
                DisplayFact df = (DisplayFact)dgFacts.Rows[e.RowIndex].DataBoundItem;
                if (df.CensusReference.URL.Length > 0)
                    SpecialMethods.VisitWebsite(df.CensusReference.URL);
            }
        }

        void BtnShowHideFacts_Click(object sender, EventArgs e) => ShowHideFactRows();

        public void ShowHideFactRows()
        {
            CurrencyManager cm = (CurrencyManager)BindingContext[dgFacts.DataSource];
            cm.SuspendBinding();
            foreach (DataGridViewRow row in dgFacts.Rows)
            {
                if (btnShowHideFacts.Checked)
                {
                    DisplayFact fact = (DisplayFact)row.DataBoundItem;
                    row.Visible = !fact.IgnoreFact;
                }
                else
                    row.Visible = true;
            }
            cm.ResumeBinding();
            dgFacts.Refresh();
        }

        #endregion

        void AddIndividualsFacts(Individual individual)
        {
            if (individual is not null)
            {
                IEnumerable<Fact> list = individual.AllFacts.Union(individual.ErrorFacts.Where(f => f.FactErrorLevel != Fact.FactError.WARNINGALLOW));
                foreach (Fact f in list)
                    facts.Add(new DisplayFact(individual, f));
            }
        }

        void AddIndividualsFacts(Individual individual, List<string> factTypes, List<string> excludedTypes, AlternateFacts alternateFacts)
        {
            if (individual is not null)
            {
                IEnumerable<Fact> list = individual.AllFacts.Union(individual.ErrorFacts.Where(f => f.FactErrorLevel != Fact.FactError.WARNINGALLOW));
                if (alternateFacts == AlternateFacts.PreferredOnly)
                    list = list.Where(x => x.Preferred);
                else if (alternateFacts == AlternateFacts.AlternateOnly)
                    list = list.Where(x => !x.Preferred);
                if (factTypes.Count == 0 && excludedTypes is not null && !list.Any(x => excludedTypes.Contains(x.FactTypeDescription)))
                    facts.Add(new DisplayFact(individual, new Fact(individual.IndividualID, Fact.REPORT, individual.BirthDate, individual.BirthLocation)));
                else
                {
                    foreach (Fact f in list)
                        if (factTypes.Contains(f.FactTypeDescription) && !list.Any(x => excludedTypes.Contains(x.FactTypeDescription)))
                            facts.Add(new DisplayFact(individual, f));
                }
            }
        }

        void AddDuplicateFacts(Individual individual, List<string> factTypes)
        {
            if (individual is not null)
            {
                IEnumerable<Fact> list = individual.AllFacts.Union(individual.ErrorFacts.Where(f => f.FactErrorLevel != Fact.FactError.WARNINGALLOW));
                foreach (string factType in factTypes)
                {
                    if (list.Count(x => x.FactTypeDescription.Equals(factType)) > 1)
                        foreach (Fact f in list.Where(x => x.FactTypeDescription.Equals(factType)))
                            facts.Add(new DisplayFact(individual, f));
                }
            }
        }

        void SetupFacts(string extraText = "")
        {
            dgFacts.DataSource = facts;
            reportFormHelper.LoadColumnLayout("FactsColumns.xml");
            tsRecords.Text = facts.Count + " Records " + extraText;
            SetBackColour();
        }

        void SetBackColour()
        {
            bool backColourGrey = false;
            DisplayFact? previous = null;
            foreach (DisplayFact fact in facts.Cast<DisplayFact>())
            {
                if (previous is not null)
                    if ((CensusRefReport && previous.CensusReference != fact.CensusReference) || (!CensusRefReport && previous.IndividualID != fact.IndividualID))
                        backColourGrey = !backColourGrey;
#if !__MACOS__
                fact.BackColour = backColourGrey ? Color.LightGray : Color.White;
#endif
                previous = fact;
            }
        }

        void ResetTable()
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

        void PrintToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintReport("Facts Report");

        void PrintPreviewToolStripButton_Click(object sender, EventArgs e) => reportFormHelper.PrintPreviewReport();

        void Facts_TextChanged(object sender, EventArgs e) => reportFormHelper.PrintTitle = Text;

        void MnuExportToExcel_Click(object sender, EventArgs e) => reportFormHelper.DoExportToExcel<IDisplayFact>();

        void MnuResetColumns_Click(object sender, EventArgs e) => reportFormHelper.ResetColumnLayout("FactsColumns.xml");

        void MnuSaveColumnLayout_Click(object sender, EventArgs e)
        {
            reportFormHelper.SaveColumnLayout("FactsColumns.xml");
            UIHelpers.ShowMessage("Form Settings Saved", "Facts");
        }

        void DgFacts_CellToolTipTextNeeded(object sender, DataGridViewCellToolTipTextNeededEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                DisplayFact f = (DisplayFact)dgFacts.Rows[e.RowIndex].DataBoundItem;
                e.ToolTipText = f.Fact.FactErrorMessage;
            }
        }

        void DgFacts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex > 0)
            {
                DisplayFact f = (DisplayFact)dgFacts.Rows[e.RowIndex].DataBoundItem;
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
                if (e.ColumnIndex == dgFacts.Columns["CensusReference"].Index && f.CensusReference is not null && f.CensusReference.URL.Length > 0)
                {
                    cell.Style.ForeColor = Color.Blue;
                    cell.Style.Font = linkFont;
                    cell.ToolTipText = "Click link to view census records on Find My Past.";
                }
            }
        }

        void DgFacts_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                DisplayFact f = (DisplayFact)dgFacts.Rows[e.RowIndex].DataBoundItem;
                if (f.Fact.FactType == Fact.REPORT)
                {
                    if (f.Ind is null)
                        UIHelpers.ShowMessage("Unable to display facts for Empty Individual");
                    else
                    {
                        Facts person = new(f.Ind);
                        person.Show();
                    }
                }
                else
                {
                    SourcesForm sourceForm = new(f);
                    sourceForm.Show();
                }
            }
        }

        void Facts_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void Facts_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
