using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class CensusIndividualNameComparer : Comparer<CensusIndividual>
    {
        public override int Compare(CensusIndividual x, CensusIndividual y)
        {
            int r = x.Surname.CompareTo(y.Surname);
            if (r == 0)
            {
                r = x.Forenames.CompareTo(y.Forenames);
                if (r == 0)
                {
                    r = x.Position - y.Position;
                }
            }
            return r;
        }
    }
}
