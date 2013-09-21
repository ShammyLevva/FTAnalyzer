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
        public bool MarriedToDB { get { return ckbMarriageDB.Checked; } set { ckbMarriageDB.Checked = value; } }
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

        public Predicate<T> BuildFilter<T>(Func<T, int> relationType)
        {
            List<Predicate<T>> relationFilters = new List<Predicate<T>>();
            if (Blood)
                relationFilters.Add(FilterUtils.IntFilter<T>(relationType, Individual.BLOOD));
            if (Directs)
                relationFilters.Add(FilterUtils.IntFilter<T>(relationType, Individual.DIRECT));
            if (Marriage)
                relationFilters.Add(FilterUtils.IntFilter<T>(relationType, Individual.MARRIAGE));
            if (MarriedToDB)
                relationFilters.Add(FilterUtils.IntFilter<T>(relationType, Individual.MARRIEDTODB));
            if (Unknown)
                relationFilters.Add(FilterUtils.IntFilter<T>(relationType, Individual.UNKNOWN));
            return FilterUtils.OrFilter<T>(relationFilters);
        }
    }
}
