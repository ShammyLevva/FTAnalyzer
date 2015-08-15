using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FTAnalyzer.Forms.Controls
{
    public partial class DateSliders : UserControl
    {
        public DateSliders()
        {
            InitializeComponent();
        }

        public string GroupBoxText { get { return groupBox.Text; } set { groupBox.Text = value; } }
    }
}
