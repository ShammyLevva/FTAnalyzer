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
        private Dictionary<IDisplayIndividual,IDisplayFamily> families;

        public People()
        {
            InitializeComponent();
        }

        public void setLocation(FactLocation loc, int level)
        {
            this.Text = "Individuals & Families with connection to " + loc.ToString();
            FamilyTree ft = FamilyTree.Instance;
            List<Individual> listInd = ft.getIndividualsAtLocation(loc, level);
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            foreach(Individual i in listInd)
                dsInd.Add(i);
            dgIndividuals.DataSource = dsInd;
            dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
            
            List<Family> listFam = ft.getFamiliesAtLocation(loc, level);
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

        public void OlderParents()
        {
            this.Text = "Parents aged 50+ at time of child's birth";
            FamilyTree ft = FamilyTree.Instance;
            selectRow = true;
            SortableBindingList<IDisplayIndividual> dsInd = new SortableBindingList<IDisplayIndividual>();
            SortableBindingList<IDisplayFamily> dsFam = new SortableBindingList<IDisplayFamily>();
            families = new Dictionary<IDisplayIndividual, IDisplayFamily>();
            foreach (Family f in ft.AllFamilies)
            {
                bool added = false;
                foreach (Individual child in f.children)
                {
                    if (child.BirthDate != FactDate.UNKNOWN_DATE)
                    {
                        if (f.husband != null && f.husband.BirthDate != FactDate.UNKNOWN_DATE)
                        {
                            Age age = f.husband.getAge(child.BirthDate);
                            if (age.MinAge >= 50 && !dsInd.Contains(f.husband))
                            {
                                dsInd.Add(f.husband);
                                families.Add(f.husband, f);
                                added = true;
                            }
                        }
                        if (f.wife != null && f.wife.BirthDate != FactDate.UNKNOWN_DATE)
                        {
                            Age age = f.wife.getAge(child.BirthDate);
                            if (age.MinAge >= 50 && !dsInd.Contains(f.wife))
                            {
                                dsInd.Add(f.wife);
                                families.Add(f.wife, f);
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
            dgIndividuals.DataSource = dsInd;
            dgIndividuals.Sort(dgIndividuals.Columns[2], ListSortDirection.Ascending);
            dgIndividuals.SelectionMode = DataGridViewSelectionMode.FullRowSelect; 
            dgFamilies.DataSource = dsFam;
            dgFamilies.Sort(dgFamilies.Columns[0], ListSortDirection.Ascending);
            dgFamilies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgIndividuals.Rows[0].Selected = true; // force a selection to update both grids
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
                        if (r.Cells[0].Value.ToString() == f.FamilyGed)
                        {
                            dgFamilies.CurrentCell = r.Cells[0];
                            break;
                        }
                    }
                }
            }
        }
    }
}
