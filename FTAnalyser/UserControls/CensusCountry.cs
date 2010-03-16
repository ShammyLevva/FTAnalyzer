using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer
{
    public partial class CensusCountry : UserControl
    {
        public CensusCountry()
        {
            InitializeComponent();
        }

        public bool Scotland { get { return rbScotland.Checked; } }
        public bool England { get { return rbEngland.Checked; } }
        public bool Wales { get { return rbWales.Checked; } }
        public bool GB { get { return rbGB.Checked; } }
        public bool Canada { get { return rbCanada.Checked; } }
        public bool USA { get { return rbUSA.Checked; } }
    }
}
