using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class DefaultCensusComparer : Comparer<CensusIndividual>
    {
        public override int Compare(CensusIndividual c1, CensusIndividual c2)
        {
            return c1.Position - c2.Position;
        }
    }
}
