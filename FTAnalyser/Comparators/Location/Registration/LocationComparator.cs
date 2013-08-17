using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class LocationComparator : IComparer<CensusIndividual>
    {
        private int level;
        
        public LocationComparator() {
            level = FactLocation.PLACE;
        }
        
        public LocationComparator(int level) {
            this.level = level;
        }

        public int Compare(CensusIndividual r1, CensusIndividual r2)
        {
            FactLocation l1 = r1.RegistrationLocation;
            FactLocation l2 = r2.RegistrationLocation;
            return l1.CompareTo(l2, level);
        }
    }
}