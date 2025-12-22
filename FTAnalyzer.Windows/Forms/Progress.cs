using FTAnalyzer.Utilities;

namespace FTAnalyzer.Forms
{
    public partial class Progress : Form
    {
        public Progress(int maximum)
        {
            InitializeComponent();
            Top += NativeMethods.TopTaskbarOffset;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = maximum;
            progressBar1.Value = 0;
        }

        public void Update(int value)
        {
            progressBar1.Value = value;
            label1.Text = $"Updated {value} of {progressBar1.Maximum} records. Please wait";
            Application.DoEvents();
        }

        void Progress_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void Progress_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
