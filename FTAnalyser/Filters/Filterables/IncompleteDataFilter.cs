using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class IncompleteDataFilter<T> : Filter<T> where T: IDateFilterable, ILocationFilterable, ICertificateFilterable
    {

        private int level;

        public static readonly IncompleteDataFilter<T> MISSING_DATA_FILTER = new IncompleteDataFilter<T>(FactLocation.COUNTRY);

        public IncompleteDataFilter(int level)
        {
            this.level = level;
        }

        public IncompleteDataFilter()
        {
            this.level = FactLocation.ADDRESS;
        }

        public bool select(T t)
        {
            if (t.isCertificatePresent())
                return false;
            FactDate fd = t.FilterDate;
            if (fd == null || !fd.isExact())
                return true;
            FactLocation l = t.FilterLocation;
            switch (level)
            {
                case FactLocation.COUNTRY: return (l.Country.Length == 0);
                case FactLocation.REGION: return (l.Region.Length == 0);
                case FactLocation.PARISH: return (l.Parish.Length == 0);
                case FactLocation.ADDRESS: return (l.Address.Length == 0);
                case FactLocation.PLACE: return (l.Place.Length == 0);
                default: return true;
            }
        }
    }
}