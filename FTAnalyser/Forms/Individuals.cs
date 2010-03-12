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
    public partial class Individuals : Form
    {
        public Individuals()
        {
            InitializeComponent();
        }

        public void setLocation(FactLocation loc, int level)
        {
            FamilyTree ft = FamilyTree.Instance;
            List<Individual> list = ft.getIndividualsAtLocation(loc, level);
            List<IDisplayIndividual> ds = new List<IDisplayIndividual>();
            foreach(Individual i in list)
                ds.Add(i);
            // ds.sort(new IndividualNameComparator());
            dgIndividuals.DataSource = ds;
            resize();
        }

        public void resize()
        {
            foreach (DataGridViewColumn c in dgIndividuals.Columns)
                c.Width = c.GetPreferredWidth(DataGridViewAutoSizeColumnMode.AllCells, true);
        }

    }
}
