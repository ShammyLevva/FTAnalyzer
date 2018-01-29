using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class CensusIndividualNameComparer : DefaultCensusComparer
    {
        public override int Compare(CensusIndividual x, CensusIndividual y)
        {
            int r = x.CensusSurname.CompareTo(y.CensusSurname);
            if (r == 0) r = base.Compare(x, y);
            return r;
        }
    }
}
