using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class FamilyDateComparer : Comparer<Family>
    {
        public override int Compare(Family x, Family y)
        {
            return x.MarriageDate.CompareTo(y.MarriageDate);
        }
    }
}
