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

        public void setWorkers(string job, List<Individual> workers)
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
    }
}
