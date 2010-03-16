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
    public partial class RelationTypes : UserControl
    {
        public RelationTypes()
        {
            InitializeComponent();
        }

        public bool Directs { get { return ckbDirects.Checked; } }
        public bool Blood { get { return ckbBlood.Checked; } }
        public bool Marriage { get { return ckbMarriage.Checked; } }
        public bool MarriedToDB { get { return ckbMarriageDB.Checked; } }
        public bool Unknown { get { return ckbUnknown.Checked; } }
    }
}
