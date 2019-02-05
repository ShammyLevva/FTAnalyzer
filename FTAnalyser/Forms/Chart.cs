using FTAnalyzer.Utilities;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FTAnalyzer.Forms
{
    public partial class Chart : Form
    {
        public Chart()
        {
            InitializeComponent();
            Top = Top + WindowHelper.TopTaskbarOffset;
        }

        public void BuildChildBirthProfile(int[,,] chartData)
        {
            Series serFatherSon = new Series() { Color = Color.Blue, LegendText = @"Father's Male Children" };
            Series serFatherDaughter = new Series() { Color = Color.HotPink, LegendText = @"Father's Female Children" };
            Series serMotherSon = new Series() { Color = Color.LightBlue, LegendText = @"Mother's Male Children" };
            Series serMotherDaughter = new Series() { Color = Color.Pink, LegendText = @"Mother's Female Children" };
            for (int i = 3; i < 20; i++)
            {
                serFatherSon.Points.Add(new DataPoint(i * 5, chartData[0, i, 0]));
                serFatherDaughter.Points.Add(new DataPoint(i * 5, chartData[0, i, 1]));
                serMotherSon.Points.Add(new DataPoint(i * 5, chartData[1, i, 0]));
                serMotherDaughter.Points.Add(new DataPoint(i * 5, chartData[1, i, 1]));
            }
            chartDisplay.Series.Add(serFatherSon);
            chartDisplay.Series.Add(serFatherDaughter);
            chartDisplay.Series.Add(serMotherSon);
            chartDisplay.Series.Add(serMotherDaughter);
            ChartArea caArea = new ChartArea("area");
            caArea.AxisX.Minimum = 10;
            caArea.AxisX.Maximum = 95;
            caArea.AxisX.Title = "Age Ranges";
            caArea.AxisY.Title = "Number of Children";
            chartDisplay.ChartAreas.Add(caArea);
        }

        void Chart_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
        }

        void Chart_Load(object sender, System.EventArgs e)
        {
            SpecialMethods.SetFonts(this);
        }
    }
}
