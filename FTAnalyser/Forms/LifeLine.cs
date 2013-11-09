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
    public partial class LifeLine : Form
    {
        private FamilyTree ft = FamilyTree.Instance;

        public LifeLine()
        {
            InitializeComponent();
            dgIndividuals.DataSource = new SortableBindingList<Individual>(ft.AllIndividuals);
            dgIndividuals.Sort(dgIndividuals.Columns["Name"], ListSortDirection.Ascending);

        }
    }
}
