using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class CensusFamilyGedComparer : Comparer<IDisplayCensus>
    {
        public override int Compare(IDisplayCensus x, IDisplayCensus y)
        {
            int r = x.FamilyGed.CompareTo(y.FamilyGed);
            if (r == 0)
            {
                r = x.Position - y.Position;
            }
            return r;
        }
    }
}
