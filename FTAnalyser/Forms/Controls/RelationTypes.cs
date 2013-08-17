using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer;
using FTAnalyzer.Filters;

namespace Controls
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

        public int Status
        {
            get
            {
                int result = 0;
                if (ckbUnknown.Checked)
                    result += Individual.UNKNOWN;
                if (ckbDirects.Checked)
                    result += Individual.DIRECT;
                if (ckbBlood.Checked)
                    result += Individual.BLOOD;
                if (ckbMarriage.Checked)
                    result += Individual.MARRIAGE;
                if (ckbMarriageDB.Checked)
                    result += Individual.MARRIEDTODB;
                return result;
            }
        }

        public Func<T, bool> BuildFilter<T>(Func<T, int> relationType)
        {
            Func<T, bool> relationFilter = FilterUtils.FalseFilter<T>();
            if (Blood)
                relationFilter = FilterUtils.OrFilter<T>(FilterUtils.IntFilter<T>(relationType, Individual.BLOOD), relationFilter);
            if (Directs)
                relationFilter = FilterUtils.OrFilter<T>(FilterUtils.IntFilter<T>(relationType, Individual.DIRECT), relationFilter);
            if (Marriage)
                relationFilter = FilterUtils.OrFilter<T>(FilterUtils.IntFilter<T>(relationType, Individual.MARRIAGE), relationFilter);
            if (MarriedToDB)
                relationFilter = FilterUtils.OrFilter<T>(FilterUtils.IntFilter<T>(relationType, Individual.MARRIEDTODB), relationFilter);
            if (Unknown)
                relationFilter = FilterUtils.OrFilter<T>(FilterUtils.IntFilter<T>(relationType, Individual.UNKNOWN), relationFilter);
            return relationFilter;
        }
    }
}
