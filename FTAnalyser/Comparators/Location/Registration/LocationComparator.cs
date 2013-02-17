using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class LocationComparator : IComparer<Registration>
    {
        private int level;
        
        public LocationComparator() {
            level = FactLocation.PLACE;
        }
        
        public LocationComparator(int level) {
            this.level = level;
        }
        
        public int Compare (Registration r1, Registration r2) {
            FactLocation l1 = new FactLocation(r1.RegistrationLocation);
            FactLocation l2 = new FactLocation(r2.RegistrationLocation);
            return l1.CompareTo(l2, level);
        }
    }
}