using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class DateComparator : IComparer<CensusIndividual>
    {
        public int Compare(CensusIndividual r1, CensusIndividual r2)
        {
            FactDate d1 = r1.RegistrationDate;
            FactDate d2 = r2.RegistrationDate;
            if (d1 == null)  d1 = FactDate.UNKNOWN_DATE;
            if (d2 == null)  d2 = FactDate.UNKNOWN_DATE;
            return d1.CompareTo(d2);
        }

    }
}