using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    public partial class Notes : Form
    {
        public Notes(Individual ind)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            rtbNotes.Text = ind.Notes;
            Text = "Notes for " + ind.ToString();
        }

        void Notes_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
