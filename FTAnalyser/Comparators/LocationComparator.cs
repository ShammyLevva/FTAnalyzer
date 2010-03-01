using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class LocationComparator : IComparer<Registration>
    {
        private int level;
        
        public LocationComparator() {
            level = Location.PLACE;
        }
        
        public LocationComparator(int level) {
            this.level = level;
        }
        
        public int Compare (Registration r1, Registration r2) {
            Location l1 = new Location(r1.getRegistrationLocation());
            Location l2 = new Location(r2.getRegistrationLocation());
            return l1.CompareTo(l2, level);
        }
    }
}