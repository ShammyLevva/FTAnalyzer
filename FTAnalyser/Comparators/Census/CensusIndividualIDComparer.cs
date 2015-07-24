using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class CensusIndividualIDComparer : IEqualityComparer<CensusIndividual>
    {
        public bool Equals(CensusIndividual x, CensusIndividual y)
        {
            return x.IndividualID.Equals(y.IndividualID);
        }
    }
}
