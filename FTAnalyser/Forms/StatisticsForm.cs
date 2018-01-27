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
    public partial class StatisticsForm : Form
    {
        public StatisticsForm()
        {
            InitializeComponent();
            tsStatusLabel.Text = string.Empty;
        }

        public void CousinsCountReport()
        {
            IEnumerable<Tuple<string,int>> relations = FamilyTree.Instance.AllIndividuals.Where(x => x.RelationToRoot.Length > 0).GroupBy(i => i.RelationToRoot)
                .Select(r => new Tuple<string, int>(r.Key, r.Count()));
            dgStatistics.DataSource = new SortableBindingList<Tuple<string, int>>(relations.ToList<Tuple<string,int>>());
            dgStatistics.Columns[0].Width = 180;
            dgStatistics.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
            dgStatistics.Columns[0].HeaderText = "Relation to Root";
            dgStatistics.Columns[1].Width = 60;
            dgStatistics.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgStatistics.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
            dgStatistics.Columns[1].HeaderText = "Count";
            dgStatistics.Sort(dgStatistics.Columns[0], ListSortDirection.Ascending);
            tsStatusLabel.Text = "Double click to show all individuals with that relationship to root person.";
            tsStatusLabel.Visible = true;
        }

        private void DgStatistics_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                Tuple<string, int> row = dgStatistics.Rows[e.RowIndex].DataBoundItem as Tuple<string, int>;
                People form = new People();
                form.ListRelationToRoot(row.Item1);
                form.Show();
            }
        }
    }
}
