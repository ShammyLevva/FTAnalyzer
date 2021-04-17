using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    public partial class DateSliders : UserControl
    {
        public DateSliders()
        {
            InitializeComponent();
            Scores = new ScoreValues(this);
        }

        public string GroupBoxText { get { return groupBox.Text; } set { groupBox.Text = value; } }
        public ScoreValues Scores { get; set; }
    }
}
