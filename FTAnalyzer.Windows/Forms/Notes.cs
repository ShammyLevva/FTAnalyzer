using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    public partial class Notes : Form
    {
        public Notes(Individual ind)
        {
            InitializeComponent();
            Theme.FormTheme.Apply(this);
            Top += NativeMethods.TopTaskbarOffset;
            rtbNotes.Text = ind.Notes;
            Text = "Notes for " + ind.ToString();
        }

        void Notes_Load(object sender, EventArgs e) => FontScaler.Apply(this);
    }
}
