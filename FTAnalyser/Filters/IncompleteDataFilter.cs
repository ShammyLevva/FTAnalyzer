using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
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
            FactDate fd = r.RegistrationDate;
            if (fd == null || !fd.isExact())
                return true;
            Location l = new Location(r.RegistrationLocation);
            switch (level)
            {
                case Location.COUNTRY: return (l.Country.Length == 0);
                case Location.REGION: return (l.Region.Length == 0);
                case Location.PARISH: return (l.Parish.Length == 0);
                case Location.ADDRESS: return (l.Address.Length == 0);
                case Location.PLACE: return (l.Place.Length == 0);
                default: return true;
            }
        }
    }
}