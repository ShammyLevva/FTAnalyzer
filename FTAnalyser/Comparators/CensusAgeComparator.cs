using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class CensusAgeComparator : IComparer<Individual>
    {
        public int Compare(Individual i1, Individual i2)
        {
            if (i1.getStatus().Equals(i2.getStatus()))
                // same status so sort by date
                return sortBirthdate(i1, i2);
            if (i1.getStatus().Equals(Individual.HUSBAND))
                return -1;
            if (i2.getStatus().Equals(Individual.HUSBAND))
                return 1;
            // neither is husband so is one a wife
            if (i1.getStatus().Equals(Individual.WIFE))
                return -1;
            if (i2.getStatus().Equals(Individual.WIFE))
                return 1;
            // neither is husband or wife so is one a child
            if (i1.getStatus().Equals(Individual.CHILD))
                return -1;
            if (i2.getStatus().Equals(Individual.CHILD))
                return 1;
            return 0;
        }

        private int sortBirthdate(Individual i1, Individual i2)
        {
            Fact b1 = i1.getPreferredFact(Fact.BIRTH);
            Fact b2 = i2.getPreferredFact(Fact.BIRTH);
            FactDate d1 = (b1 == null) ? FactDate.UNKNOWN_DATE : b1.getFactDate();
            FactDate d2 = (b2 == null) ? FactDate.UNKNOWN_DATE : b2.getFactDate();
            return d1.CompareTo(d2);
        }
    }
}