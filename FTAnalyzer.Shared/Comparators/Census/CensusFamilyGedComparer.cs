using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class CensusFamilyGedComparer : Comparer<CensusIndividual>
    {
        public override int Compare(CensusIndividual x, CensusIndividual y)
        {
            int r = x.FamilyID.CompareTo(y.FamilyID);
            if (r == 0)
            {
                r = x.Position - y.Position;
            }
            return r;
        }
    }
}
