using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class CensusLocationComparer : DefaultCensusComparer
    {
        private int level;
        
        public CensusLocationComparer() {
            level = FactLocation.PLACE;
        }
        
        public CensusLocationComparer(int level) {
            this.level = level;
        }

        public override int Compare(CensusIndividual r1, CensusIndividual r2)
        {
            FactLocation l1 = r1.CensusLocation;
            FactLocation l2 = r2.CensusLocation;
            int comp = l1.CompareTo(l2, level);
            if (comp == 0) comp = base.Compare(r1, r2);
            return comp;
        }
    }
}