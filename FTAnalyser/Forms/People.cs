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
    public partial class People : Form
    {
        private bool selectRow = false;
        private Dictionary<IDisplayIndividual, IDisplayFamily> families;

        public People()
        {
            InitializeComponent();
            People_Resize(this, null);
        }

        public void SetLocation(FactLocation loc, int level)
        {
            this.Text = "Individuals & Families with connection to " + loc.ToString();
            FamilyTree ft = FamilyTree.Instance;
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
        }

        public void setWorkers(string job, SortableBindingList<Individual> workers)
        {
            this.Text = "Individuals whose occupation was a " + job;
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            foreach (Individual i in workers)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
            dgIndividuals.Dock = DockStyle.Fill;

            dgFamilies.Visible = false;
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
                    if (child.BirthDate.IsKnown())
                    {
                        if (f.Husband != null && f.Husband.BirthDate.IsKnown())
                        {
                            Age age = f.Husband.GetAge(child.BirthDate);
                            if (age.MinAge >= minAge && !dsInd.Contains(f.Husband))
                            {
                                dsInd.Add(f.Husband);
                                families.Add(f.Husband, f);
                                added = true;
                            }
                        }
                        if (f.Wife != null && f.Wife.BirthDate.IsKnown())
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
                MessageBox.Show("You have no parents older than " + minAge + " at time of children's birth.");
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

        private void People_Resize(object sender, EventArgs e)
        {
            int height = (this.Height - 40) / 2;
            dgIndividuals.Height = height;
            dgFamilies.Height = height;
        }

        private void dgIndividuals_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string indID = (string)dgIndividuals.CurrentRow.Cells["Ind_ID"].Value;
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
    }
}
