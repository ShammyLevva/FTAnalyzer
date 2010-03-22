using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
            List<IDisplayIndividual> dsInd = new List<IDisplayIndividual>();
            foreach(Individual i in listInd)
                dsInd.Add(i);
            // ds.sort(new IndividualNameComparator());
            dgIndividuals.DataSource = dsInd;

            List<Family> listFam = ft.getFamiliesAtLocation(loc, level);
            List<IDisplayFamily> dsFam = new List<IDisplayFamily>();
            foreach (Family f in listFam)
                dsFam.Add(f);
            // ds.sort(new IndividualNameComparator());
            dgFamilies.DataSource = dsFam;

            resize();
        }

        public void resize()
        {
            foreach (DataGridViewColumn c in dgIndividuals.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
            foreach (DataGridViewColumn c in dgFamilies.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }
    }
}
