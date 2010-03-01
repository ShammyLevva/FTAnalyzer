using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class IncompleteDataFilter : RegistrationFilter
    {

        private int level;

        public static readonly IncompleteDataFilter MISSING_DATA_FILTER =
            new IncompleteDataFilter(Location.COUNTRY);

        public IncompleteDataFilter(int level)
        {
            this.level = level;
        }

        public IncompleteDataFilter()
        {
            this.level = Location.ADDRESS;
        }

        public bool select(Registration r)
        {
            if (r.isCertificatePresent())
                return false;
            FactDate fd = r.getRegistrationDate();
            if (fd == null || !fd.isExact())
                return true;
            Location l = new Location(r.getRegistrationLocation());
            switch (level)
            {
                case Location.COUNTRY: return (l.getCountry().Length == 0);
                case Location.REGION: return (l.getRegion().Length == 0);
                case Location.PARISH: return (l.getParish().Length == 0);
                case Location.ADDRESS: return (l.getAddress().Length == 0);
                case Location.PLACE: return (l.getPlace().Length == 0);
                default: return true;
            }
        }
    }
}