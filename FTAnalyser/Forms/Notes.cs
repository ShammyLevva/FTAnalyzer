using FTAnalyzer.Utilities;
using System;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class Notes : Form
    {
        public Notes(Individual ind)
        {
            InitializeComponent();
            Top = Top + WindowHelper.TopTaskbarOffset;
            rtbNotes.Text = ind.Notes;
            Text = "Notes for " + ind.ToString();
        }

        void Notes_Load(object sender, EventArgs e)
        {
            SpecialMethods.SetFonts(this);
        }
    }
}
