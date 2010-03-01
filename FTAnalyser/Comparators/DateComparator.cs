using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class DateComparator : IComparer<Registration>
    {
        public int Compare (Registration r1, Registration r2) {
            FactDate d1 = r1.getRegistrationDate();
            FactDate d2 = r2.getRegistrationDate();
            if (d1 == null)  d1 = FactDate.UNKNOWN_DATE;
            if (d2 == null)  d2 = FactDate.UNKNOWN_DATE;
            return d1.CompareTo(d2);
        }

    }
}