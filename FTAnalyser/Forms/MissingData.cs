using FTAnalyzer.Utilities;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class MissingData : Form
    {

        public MissingData()
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            SetDefaultValues();
        }

        void SetDefaultValues()
        {
            dsBirth.Scores.UnknownDate = 100;
            dsBirth.Scores.VeryWideDate = 90;
            dsBirth.Scores.WideDate = 75;
            dsBirth.Scores.NarrowDate = 50;
            dsBirth.Scores.JustYearDate = 25;
            dsBirth.Scores.ApproxDate = 10;
            dsBirth.Scores.ExactDate = 0;
        }

        void MissingData_Load(object sender, System.EventArgs e)
        {
            SpecialMethods.SetFonts(this);
        }
    }
}
