using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    public partial class Chart : Form
    {
        public Chart(List<ParentAgeBucket> buckets)
        {
            InitializeComponent();
            Theme.FormTheme.Apply(this);
            Top += NativeMethods.TopTaskbarOffset;
            ThemeStatsGrid();
            SetupColumns();
            BuildParentAgeReport(buckets);
        }

        // FormTheme.Apply skips DataGridView entirely (each grid themes itself at its own
        // source - see FormTheme.cs) - this one isn't a VirtualDataGridView, so it needs the
        // same handful of color assignments done by hand, including its native scrollbar (see
        // VirtualDataGridView.ApplyColors for why that needs the child ScrollBar controls too).
        void ThemeStatsGrid()
        {
            dgParentAgeStats.BorderStyle = Theme.ActiveColors.IsDark ? BorderStyle.None : BorderStyle.Fixed3D;
            NativeMethods.SetScrollBarTheme(dgParentAgeStats, Theme.ActiveColors.IsDark);
            foreach (Control child in dgParentAgeStats.Controls)
            {
                if (child is ScrollBar)
                    NativeMethods.SetScrollBarTheme(child, Theme.ActiveColors.IsDark);
            }
            dgParentAgeStats.ColumnHeadersDefaultCellStyle.BackColor = Theme.ActiveColors.Primary;
            dgParentAgeStats.ColumnHeadersDefaultCellStyle.ForeColor = Theme.ActiveColors.OnPrimary;
            dgParentAgeStats.GridColor = Theme.ActiveColors.Border;
            Color rowColor = Theme.ActiveColors.IsDark ? Theme.ActiveColors.Background : Theme.ActiveColors.Card;
            Color alternateRowColor = Theme.ActiveColors.IsDark ? Theme.ActiveColors.Card : Theme.ActiveColors.Background;
            dgParentAgeStats.BackgroundColor = rowColor;
            dgParentAgeStats.RowsDefaultCellStyle.BackColor = rowColor;
            dgParentAgeStats.RowsDefaultCellStyle.ForeColor = Theme.ActiveColors.Text;
            dgParentAgeStats.AlternatingRowsDefaultCellStyle.BackColor = alternateRowColor;
            dgParentAgeStats.AlternatingRowsDefaultCellStyle.ForeColor = Theme.ActiveColors.Text;
        }

        // Grouped bar chart (Father/Mother x Son/Daughter, by 5-year age band at the child's
        // birth) built from FamilyTree.ParentAgeProfile (FTAnalyzer.Shared\Core\FamilyTree.cs) -
        // the same data the web app's /parent-age page renders with RadzenChart. The desktop
        // build previously showed this via System.Windows.Forms.DataVisualization.Charting,
        // which doesn't ship with .NET Core/.NET 5+, hence disabled since the migration - see
        // GitHub issue #375 and FTAnalyzer.Web issue #18.
        void BuildParentAgeReport(List<ParentAgeBucket> buckets)
        {
            ScottPlot.Color fatherSonColor = ScottPlot.Color.FromSDColor(Theme.ActiveColors.GenderMale);
            ScottPlot.Color fatherDaughterColor = ScottPlot.Color.FromSDColor(Theme.ActiveColors.GenderFemale);
            // Mother's bars share the same son/daughter hue as father's so the legend reads as
            // two colour families (blue = sons, pink = daughters) with father/mother
            // distinguished by shade, rather than four unrelated colours.
            ScottPlot.Color motherSonColor = ScottPlot.Color.InterpolateRgb(fatherSonColor, ScottPlot.Colors.White, 0.4);
            ScottPlot.Color motherDaughterColor = ScottPlot.Color.InterpolateRgb(fatherDaughterColor, ScottPlot.Colors.White, 0.4);

            const double groupWidth = 0.8;
            const int seriesCount = 4;
            const double barWidth = groupWidth / seriesCount;

            List<ScottPlot.Bar> fatherSons = [];
            List<ScottPlot.Bar> fatherDaughters = [];
            List<ScottPlot.Bar> motherSons = [];
            List<ScottPlot.Bar> motherDaughters = [];
            for (int i = 0; i < buckets.Count; i++)
            {
                ParentAgeBucket bucket = buckets[i];
                fatherSons.Add(new ScottPlot.Bar { Position = i - 1.5 * barWidth, Value = bucket.FatherSons, Size = barWidth, FillColor = fatherSonColor });
                fatherDaughters.Add(new ScottPlot.Bar { Position = i - 0.5 * barWidth, Value = bucket.FatherDaughters, Size = barWidth, FillColor = fatherDaughterColor });
                motherSons.Add(new ScottPlot.Bar { Position = i + 0.5 * barWidth, Value = bucket.MotherSons, Size = barWidth, FillColor = motherSonColor });
                motherDaughters.Add(new ScottPlot.Bar { Position = i + 1.5 * barWidth, Value = bucket.MotherDaughters, Size = barWidth, FillColor = motherDaughterColor });
            }

            ScottPlot.Plot plot = chartDisplay.Plot;
            plot.Clear();
            plot.Add.Bars(fatherSons).LegendText = "Father — Sons";
            plot.Add.Bars(fatherDaughters).LegendText = "Father — Daughters";
            plot.Add.Bars(motherSons).LegendText = "Mother — Sons";
            plot.Add.Bars(motherDaughters).LegendText = "Mother — Daughters";

            plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(
                [.. Enumerable.Range(0, buckets.Count).Select(i => (double)i)],
                [.. buckets.Select(b => b.AgeLabel)]);
            plot.Axes.Bottom.Label.Text = "Parent's Age at Child's Birth";
            plot.Axes.Left.Label.Text = "Number of Children";
            plot.Title("Parent's Age at Child's Birth");
            plot.ShowLegend(ScottPlot.Alignment.UpperRight);
            plot.Axes.AutoScale();

            ApplyChartTheme(plot);
            chartDisplay.Refresh();

            dgParentAgeStats.DataSource = new SortableBindingList<ParentAgeBucket>(buckets);

            int fatherTotal = buckets.Sum(b => b.FatherTotal);
            int motherTotal = buckets.Sum(b => b.MotherTotal);
            Text = $"Parent Age Report - {fatherTotal} father ages, {motherTotal} mother ages recorded";
        }

        // ScottPlot draws to its own SkiaSharp surface rather than through WinForms controls, so
        // FormTheme.Apply (which only knows how to recolor built-in Control types) can't reach
        // it - theme the plot directly from the same ActiveColors palette instead.
        static void ApplyChartTheme(ScottPlot.Plot plot)
        {
            ScottPlot.Color background = ScottPlot.Color.FromSDColor(Theme.ActiveColors.Background);
            ScottPlot.Color card = ScottPlot.Color.FromSDColor(Theme.ActiveColors.Card);
            ScottPlot.Color text = ScottPlot.Color.FromSDColor(Theme.ActiveColors.Text);
            ScottPlot.Color border = ScottPlot.Color.FromSDColor(Theme.ActiveColors.Border);

            plot.FigureBackground.Color = background;
            plot.DataBackground.Color = card;
            plot.Axes.Color(text);
            plot.Legend.BackgroundColor = card;
            plot.Legend.FontColor = text;
            plot.Legend.OutlineColor = border;
            plot.Axes.Title.Label.ForeColor = text;
            plot.Axes.Bottom.Label.ForeColor = text;
            plot.Axes.Left.Label.ForeColor = text;
        }

        // Curated subset/order matching the web app's /parent-age data grid (ParentAge.razor) -
        // MinAge/MaxAge are omitted since AgeLabel already summarises them, and the Father/Mother
        // totals shown in the chart legend aren't needed again here.
        void SetupColumns()
        {
            dgParentAgeStats.Columns.Add(new DataGridViewTextBoxColumn { Name = "AgeLabel", DataPropertyName = "AgeLabel", HeaderText = "Age Range" });
            AddCountColumn("FatherSons", "Father: Sons");
            AddCountColumn("FatherDaughters", "Father: Daughters");
            AddCountColumn("FatherUnknown", "Father: Unknown");
            AddCountColumn("MotherSons", "Mother: Sons");
            AddCountColumn("MotherDaughters", "Mother: Daughters");
            AddCountColumn("MotherUnknown", "Mother: Unknown");
        }

        void AddCountColumn(string propertyName, string headerText) => dgParentAgeStats.Columns.Add(new DataGridViewTextBoxColumn
        {
            Name = propertyName,
            DataPropertyName = propertyName,
            HeaderText = headerText,
            DefaultCellStyle = new DataGridViewCellStyle { Alignment = DataGridViewContentAlignment.MiddleRight }
        });

        void Chart_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void Chart_Load(object sender, System.EventArgs e) => FontScaler.Apply(this);
    }
}
