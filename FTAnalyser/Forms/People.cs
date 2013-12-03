using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Utilities;
using FTAnalyzer.Filters;

namespace FTAnalyzer.Forms
{
    public partial class People : Form
    {
        private bool selectRow = false;
        private Dictionary<IDisplayIndividual, IDisplayFamily> families;

        public People()
        {
            InitializeComponent();
        }

        private void UpdateStatusCount()
        {
            if (splitContainer.Panel2Collapsed)
                txtCount.Text = "Count: " + dgIndividuals.RowCount + " Individuals.";
            else
                txtCount.Text = "Count: " + dgIndividuals.RowCount + " Individuals and " + dgFamilies.RowCount + " Families.";
        }

        public void SetLocation(FactLocation loc, int level)
        {
            this.Text = "Individuals & Families with connection to " + loc.ToString();
            FamilyTree ft = FamilyTree.Instance;
            level = Math.Min(loc.Level, level); // if location level isn't as detailed as level on tab use location level
            IEnumerable<Individual> listInd = ft.GetIndividualsAtLocation(loc, level);
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            foreach (Individual i in listInd)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);

            IEnumerable<Family> listFam = ft.GetFamiliesAtLocation(loc, level);
            SortableBindingList<IDisplayFamily> dsFam = new SortableBindingList<IDisplayFamily>();
            foreach (Family f in listFam)
                dsFam.Add(f);
            dgFamilies.DataSource = dsFam;
            dgFamilies.Sort(dgFamilies.Columns[0], ListSortDirection.Ascending);
            splitContainer.Panel2Collapsed = false;
            UpdateStatusCount();
        }

        public void SetWorkers(string job, SortableBindingList<Individual> workers)
        {
            this.Text = "Individuals whose occupation was " + (job.Length == 0 ? "not entered" : job);
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            foreach (Individual i in workers)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
            dgIndividuals.Dock = DockStyle.Fill;
            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }

        public void SetupLCDuplicates(Predicate<Individual> relationFilter)
        {
            Predicate<Individual> lcFacts = i => i.DuplicateLCFacts > 0;
            Predicate<Individual> filter = FilterUtils.AndFilter<Individual>(relationFilter, lcFacts);
            List<Individual> individuals = FamilyTree.Instance.AllIndividuals.Where(filter).ToList();
            SetIndividuals(individuals, "Lost Cousins with Duplicate Facts");
        }

        public void SetupLCnoCensus(Predicate<Individual> relationFilter)
        {
            List<Individual> listtoCheck = FamilyTree.Instance.AllIndividuals.Where(relationFilter).ToList();
            List<Individual> individuals = new List<Individual>();
            foreach (CensusDate censusDate in CensusDate.LOSTCOUSINS_CENSUS)
            {
                Predicate<Individual> lcFacts = new Predicate<Individual>(i => i.IsLostCousinsEntered(censusDate) && !i.IsCensusDone(censusDate, true, false));
                IEnumerable<Individual> censusMissing = listtoCheck.Where(lcFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct<Individual>().ToList();
            SetIndividuals(individuals, "Lost Cousins with no corresponding census entry");
        }

        public void SetupLCNoCountry(Predicate<Individual> relationFilter)
        {
            Predicate<Individual> lcFacts = x => x.LostCousinsFacts > 0;
            Predicate<Individual> filter = FilterUtils.AndFilter<Individual>(relationFilter, lcFacts);
            IEnumerable<Individual> listToCheck = FamilyTree.Instance.AllIndividuals.Where(filter).ToList();
           
            Predicate<Individual> missing = x => !x.IsLostCousinsEntered(CensusDate.EWCENSUS1841, false)
                                              && !x.IsLostCousinsEntered(CensusDate.EWCENSUS1881, false)
                                              && !x.IsLostCousinsEntered(CensusDate.SCOTCENSUS1881, false)
                                              && !x.IsLostCousinsEntered(CensusDate.CANADACENSUS1881, false)
                                              && !x.IsLostCousinsEntered(CensusDate.EWCENSUS1911, false)
                                              && !x.IsLostCousinsEntered(CensusDate.IRELANDCENSUS1911, false)
                                              && !x.IsLostCousinsEntered(CensusDate.USCENSUS1880, false)
                                              && !x.IsLostCousinsEntered(CensusDate.USCENSUS1940, false);
            List<Individual> individuals = listToCheck.Where(missing).ToList<Individual>();
            SetIndividuals(individuals, "Lost Cousins with No Country");
        }

        public void SetIndividuals(List<Individual> individuals, string reportTitle)
        {
            this.Text = reportTitle;
            dgIndividuals.DataSource = new SortableBindingList<IDisplayIndividual>(individuals);
            dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
            dgIndividuals.Sort(dgIndividuals.Columns[1], ListSortDirection.Ascending);
            dgIndividuals.Dock = DockStyle.Fill;

            splitContainer.Panel2Collapsed = true;
            UpdateStatusCount();
        }

        public bool OlderParents(int minAge)
        {
            this.Text = "Parents aged " + minAge + "+ at time of child's birth";
            FamilyTree ft = FamilyTree.Instance;
            selectRow = true;
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            SortableBindingList<IDisplayFamily> dsFam = new SortableBindingList<IDisplayFamily>();
            families = new Dictionary<IDisplayIndividual, IDisplayFamily>();
            foreach (Family f in ft.AllFamilies)
            {
                bool added = false;
                foreach (Individual child in f.Children)
                {
                    if (child.BirthDate.IsKnown)
                    {
                        if (f.Husband != null && f.Husband.BirthDate.IsKnown)
                        {
                            Age age = f.Husband.GetAge(child.BirthDate);
                            if (age.MinAge >= minAge && !dsInd.Contains(f.Husband))
                            {
                                dsInd.Add(f.Husband);
                                families.Add(f.Husband, f);
                                added = true;
                            }
                        }
                        if (f.Wife != null && f.Wife.BirthDate.IsKnown)
                        {
                            Age age = f.Wife.GetAge(child.BirthDate);
                            if (age.MinAge >= minAge && !dsInd.Contains(f.Wife))
                            {
                                dsInd.Add(f.Wife);
                                families.Add(f.Wife, f);
                                added = true;
                            }
                        }
                    }
                }
                if (added && !dsFam.Contains(f))
                {
                    dsFam.Add(f);
                }
            }
            if (dsInd.Count > 0)
            {
                dgIndividuals.DataSource = dsInd;
                dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
                dgIndividuals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgFamilies.DataSource = dsFam;
                dgFamilies.Sort(dgFamilies.Columns[0], ListSortDirection.Ascending);
                dgFamilies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgIndividuals.Rows[0].Selected = true; // force a selection to update both grids
                return true;
            }
            else
            {
                MessageBox.Show("You have no parents older than " + minAge + " at time of children's birth.", "FT Analyzer");
                return false;
            }
        }

        private void dgIndividuals_SelectionChanged(object sender, EventArgs e)
        {
            if (selectRow && dgIndividuals.CurrentRow != null)
            {
                IDisplayFamily f;
                IDisplayIndividual ind = (IDisplayIndividual)dgIndividuals.CurrentRow.DataBoundItem;
                families.TryGetValue(ind, out f);
                if (f != null)
                {
                    foreach (DataGridViewRow r in dgFamilies.Rows)
                    {
                        if (r.Cells[0].Value.ToString() == f.FamilyID)
                        {
                            dgFamilies.CurrentCell = r.Cells[0];
                            break;
                        }
                    }
                }
            }
        }

        private void dgIndividuals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
                Individual ind = FamilyTree.Instance.GetIndividual(indID);
                Facts factForm = new Facts(ind);
                factForm.Show();
            }
        }

        private void dgFamilies_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string famID = (string)dgFamilies.CurrentRow.Cells["FamilyID"].Value;
                Family fam = FamilyTree.Instance.GetFamily(famID);
                Facts factForm = new Facts(fam);
                factForm.Show();
            }
        }

        public void SetupMissingCensusLocation()
        {
            List<Individual> individuals = new List<Individual>();
            foreach (CensusDate censusDate in CensusDate.SUPPORTED_CENSUS)
            {
                Predicate<Individual> censusFacts = new Predicate<Individual>(x => x.IsCensusDone(censusDate) && !x.HasCensusLocation(censusDate));
                IEnumerable<Individual> censusMissing = FamilyTree.Instance.AllIndividuals.Where(censusFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct<Individual>().ToList();
            SetIndividuals(individuals, "Indiviudals with census records with no census location");
        }

        public void SetupDuplicateCensus()
        {
            List<Individual> individuals = new List<Individual>();
            foreach (CensusDate censusDate in CensusDate.SUPPORTED_CENSUS)
            {
                Predicate<Individual> censusFacts = new Predicate<Individual>(i => i.CensusDateFactCount(censusDate) > 1);
                IEnumerable<Individual> censusMissing = FamilyTree.Instance.AllIndividuals.Where(censusFacts);
                individuals.AddRange(censusMissing);
            }
            individuals = individuals.Distinct<Individual>().ToList();
            SetIndividuals(individuals, "Individuals that have more than once census record for a census year");
        }

        private void dgIndividuals_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string indID = (string)dgIndividuals.CurrentRow.Cells["IndividualID"].Value;
                Individual ind = FamilyTree.Instance.GetIndividual(indID);
                Facts factForm = new Facts(ind);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        private void dgFamilies_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string famID = (string)dgFamilies.CurrentRow.Cells["FamilyID"].Value;
                Family fam = FamilyTree.Instance.GetFamily(famID);
                Facts factForm = new Facts(fam);
                MainForm.DisposeDuplicateForms(factForm);
                factForm.Show();
            }
        }

        private void People_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
