using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer;

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

        public Filter<T> BuildFilter<T>() where T: IRelationFilterable
        {
            Filter<T> relationFilter = new FalseFilter<T>();
            if (Blood)
                relationFilter = new OrFilter<T>(new RelationFilter<T>(Individual.BLOOD), relationFilter);
            if (Directs)
                relationFilter = new OrFilter<T>(new RelationFilter<T>(Individual.DIRECT), relationFilter);
            if (Marriage)
                relationFilter = new OrFilter<T>(new RelationFilter<T>(Individual.MARRIAGE), relationFilter);
            if (MarriedToDB)
                relationFilter = new OrFilter<T>(new RelationFilter<T>(Individual.MARRIEDTODB), relationFilter);
            if (Unknown)
                relationFilter = new OrFilter<T>(new RelationFilter<T>(Individual.UNKNOWN), relationFilter);
            return relationFilter;
        }
    }
}
