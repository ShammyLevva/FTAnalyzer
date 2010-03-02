using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class BirthFilter : DateFilter
    {
        public static DateFilter ONLINE_MARRIAGE_FILTER = new BirthFilter("BET 01 JAN 1855 AND 31 DEC 1929");
        public static DateFilter ONLINE_DEATH_FILTER = new BirthFilter("BET 01 JAN 1855 AND 31 DEC 1954");

        public static DateFilter GROS_MARRIAGE_FILTER = new BirthFilter("AFT 31 DEC 1929");
        public static DateFilter GROS_DEATH_FILTER = new BirthFilter("AFT 31 DEC 1954");

        public BirthFilter(FactDate cutoff) : base(cutoff) {}

        public BirthFilter(string date) : base(date) {}

        public override bool select(Registration r)
        {
            FactDate d = r.RegistrationDate;
            FactDate b = r.Individual.BirthDate;
            FactDate old = b;
            if (r is DeathRegistration)
                old = b.addYears(FactDate.MAXYEARS);
            else if (r is MarriageRegistration)
                old = b.addYears(75); // restrict marriages to under 75 year olds
            return cutoff.overlaps(d) && cutoff.overlaps(old);
        }
    }
}