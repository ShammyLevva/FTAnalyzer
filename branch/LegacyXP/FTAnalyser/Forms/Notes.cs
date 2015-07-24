using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class Notes : Form
    {
        public Notes(Individual ind)
        {
            InitializeComponent();
            rtbNotes.Text = ind.Notes;
            this.Text = "Notes for " + ind.ToString();
        }
    }
}
