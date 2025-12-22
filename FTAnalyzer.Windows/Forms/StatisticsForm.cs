using FTAnalyzer.Filters;
using FTAnalyzer.Utilities;
using System.ComponentModel;

namespace FTAnalyzer.Forms
{
    public partial class StatisticsForm : Form
    {
        public enum StatisticType { CousinCount = 1, HowManyDirects = 2, BirthdayEffect = 3 };

        StatisticType StatType { get; }

        public StatisticsForm(StatisticType type)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            StatType = type;
            tsStatusLabel.Text = string.Empty;
            switch (type)
            {
                case StatisticType.CousinCount:
                    CousinsCountReport();
                    break;
                case StatisticType.HowManyDirects:
                    HowManyDirectsReport();
                    break;
                case StatisticType.BirthdayEffect:
                    BirthdayEffectReport();
                    break;
            }
        }

        void CousinsCountReport()
        {
            IEnumerable<Tuple<string, int>> relations = FamilyTree.Instance.AllIndividuals.Where(x => x.RelationToRoot.Length > 0).GroupBy(i => i.RelationToRoot)
                .Select(r => new Tuple<string, int>(r.Key, r.Count()));
            var list = new SortableBindingList<Tuple<string, int>>(relations.ToList());
            dgStatistics.DataSource = list;
            dgStatistics.Columns[0].Width = 180;
            dgStatistics.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
            dgStatistics.Columns[0].HeaderText = "Relation to Root";
            dgStatistics.Columns[1].Width = 60;
            dgStatistics.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgStatistics.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
            dgStatistics.Columns[1].HeaderText = "Count";
            dgStatistics.Sort(dgStatistics.Columns[0], ListSortDirection.Ascending);
            int count = list.Sum(x => x.Item2);
            Text = $"Cousin Count Report - {count} Individuals";
            tsStatusLabel.Text = "Double click to show all individuals with that relationship to root person.";
            tsStatusLabel.Visible = true;
        }

        void HowManyDirectsReport()
        {
            IEnumerable<DisplayGreatStats> relations = FamilyTree.Instance.AllIndividuals.Where(x => x.RelationToRoot.Length > 0 && (x.RelationType == Individual.DIRECT || x.RelationType == Individual.DESCENDANT))
                .GroupBy(i => (i.RelationToRoot, i.RelationSort))
                .Select(r => new DisplayGreatStats(r.Key.RelationToRoot, r.Key.RelationSort, r.Count()));
            var list = new SortableBindingList<DisplayGreatStats>(relations.ToList());
            dgStatistics.DataSource = list;
            dgStatistics.Columns[0].Width = 180;
            dgStatistics.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
            dgStatistics.Columns[0].HeaderText = "Relation to Root";
            dgStatistics.Columns[1].Visible = false;
            dgStatistics.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgStatistics.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
            dgStatistics.Columns[1].HeaderText = "Relation Sort";
            dgStatistics.Columns[2].Width = 60;
            dgStatistics.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgStatistics.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
            dgStatistics.Columns[2].HeaderText = "Count";
            dgStatistics.Sort(dgStatistics.Columns[1], ListSortDirection.Descending);
            int count = list.Sum(x => x.Count);
            Text = $"How Many Directs Report - {count} directs";
            tsStatusLabel.Text = "Double click to show all individuals with that relationship to root person.";
            tsStatusLabel.Visible = true;
        }

        void BirthdayEffectReport()
        {
            try
            {
                List<Tuple<string, int>> birthdayEffect = FamilyTree.Instance.AllIndividuals.Where(x => x.BirthdayEffect).GroupBy(i => i.BirthMonth)
                    .Select(r => new Tuple<string, int>(r.Key, r.Count())).ToList();
                List<Tuple<string, int>> exactDates = FamilyTree.Instance.AllIndividuals.Where(x => x.BirthDate.IsExact && x.DeathDate.IsExact).GroupBy(i => i.BirthMonth)
                    .Select(r => new Tuple<string, int>(r.Key, r.Count())).ToList();
                birthdayEffect.Sort();
                exactDates.Sort();
                int beIndex = 0, edIndex = 0, beItem2 = 0, edItem2 = 0;
                List<Tuple<string, string, string>> result = new();
                for (int month = 1; month <= 12; month++)
                {
                    beItem2 = edItem2 = 0;
                    string monthStr = new DateTime(2000, month, 1).ToString("MM : MMMM");
                    if (month <= birthdayEffect.Count && birthdayEffect[beIndex].Item1 == monthStr)
                        beItem2 = birthdayEffect[beIndex++].Item2;
                    if (month <= exactDates.Count && exactDates[edIndex].Item1 == monthStr)
                        edItem2 = exactDates[edIndex++].Item2;
                    var column2 = $"{beItem2}/{edItem2}";
                    float percent = edItem2 == 0 ? 0f : (float)beItem2 / edItem2;
                    result.Add(new Tuple<string, string, string>(monthStr, column2, string.Format("{0:P2}", percent)));
                }
                dgStatistics.DataSource = new SortableBindingList<Tuple<string, string, string>>(result);
                dgStatistics.Columns[0].Width = 150;
                dgStatistics.Columns[0].SortMode = DataGridViewColumnSortMode.Automatic;
                dgStatistics.Columns[0].HeaderText = "Birth Month";
                dgStatistics.Columns[1].Width = 80;
                dgStatistics.Columns[1].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgStatistics.Columns[1].SortMode = DataGridViewColumnSortMode.Automatic;
                dgStatistics.Columns[1].HeaderText = "Died Near Birthday";
                dgStatistics.Columns[2].Width = 80;
                dgStatistics.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgStatistics.Columns[2].SortMode = DataGridViewColumnSortMode.Automatic;
                dgStatistics.Columns[2].HeaderText = "Percentage";
                dgStatistics.Sort(dgStatistics.Columns[0], ListSortDirection.Ascending);
                Text = "Birthday Effect Report";
                tsStatusLabel.Text = "Double click shows those born who died within 15 days of birthday.";
                tsStatusLabel.Visible = true;
            }
            catch (ArgumentException e)
            {
                UIHelpers.ShowMessage($"Sorry there's a problem with generating Birthday report.\nMessage is:{e.Message}");
            }
        }

        void DgStatistics_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (StatType == StatisticType.HowManyDirects)
                {
                    if (dgStatistics.Rows[e.RowIndex].DataBoundItem is DisplayGreatStats row)
                    {
                        People form = new();
                        form.ListRelationToRoot(row.RelationToRoot);
                        form.Show();
                    }
                }
                else if (StatType == StatisticType.CousinCount)
                {
                    if (dgStatistics.Rows[e.RowIndex].DataBoundItem is Tuple<string, int> row)
                    {
                        People form = new();
                        form.ListRelationToRoot(row.Item1);
                        form.Show();
                    }
                }
                else if (StatType == StatisticType.BirthdayEffect)
                {
                    if (dgStatistics.Rows[e.RowIndex].DataBoundItem is Tuple<string, string, string> row)
                    {
                        People form = new();
                        bool filter(Individual x) => x.BirthdayEffect && x.BirthMonth == row.Item1;
                        List<Individual> individuals = FamilyTree.Instance.AllIndividuals.Filter(filter).ToList();
                        form.SetIndividuals(individuals, $"Indiviudals who died within 15 days of their birthday in {row.Item1[5..]}");
                        form.Show();
                    }
                }
            }
        }

        void StatisticsForm_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
