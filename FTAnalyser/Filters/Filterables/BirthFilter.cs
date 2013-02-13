using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class BirthFilter<T> : DateFilter<T> where T : IDateFilterable, IIndividualFilterable
    {
        public static DateFilter<T> ONLINE_MARRIAGE_FILTER = new BirthFilter<T>("BET 01 JAN 1855 AND 31 DEC 1937");
        public static DateFilter<T> ONLINE_DEATH_FILTER = new BirthFilter<T>("BET 01 JAN 1855 AND 31 DEC 1962");

        public static DateFilter<T> GROS_MARRIAGE_FILTER = new BirthFilter<T>("AFT 31 DEC 1937");
        public static DateFilter<T> GROS_DEATH_FILTER = new BirthFilter<T>("AFT 31 DEC 1962");

        public BirthFilter(FactDate cutoff) : base(cutoff) {}

        public BirthFilter(string date) : base(date) {}

        public override bool select(T t)
        {
            FactDate filterDate = t.FilterDate;
            FactDate birthDate = t.Individual.BirthDate;
            FactDate upperAge = birthDate;
            if (t is DeathRegistration)
                upperAge = birthDate.addYears(FactDate.MAXYEARS);
            else if (t is MarriageRegistration)
                upperAge = birthDate.addYears(75); // restrict marriages to under 75 year olds
            return cutoff.overlaps(filterDate) && cutoff.overlaps(upperAge);
        }
    }
}