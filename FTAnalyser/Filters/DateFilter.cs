using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class DateFilter : RegistrationFilter {

        internal FactDate cutoff;

        public static DateFilter POST_1855_FILTER = new DateFilter("AFT 31 DEC 1854");
        public static DateFilter POST_1837_FILTER = new DateFilter("AFT 30 JUN 1837");

        public static DateFilter PRE_1855_FILTER = new DateFilter("BEF 01 JAN 1855");
        public static DateFilter PRE_1837_FILTER = new DateFilter("BEF 01 JUL 1837");
        
        public static DateFilter ONLINE_BIRTH_FILTER = new DateFilter("BET 01 JAN 1855 AND 31 DEC 1904");
        
        public static DateFilter GROS_BIRTH_FILTER = new DateFilter("AFT 31 DEC 1904");

        public DateFilter (FactDate cutoff) {
            this.cutoff = cutoff;
        }

        public DateFilter (string date) {
            this.cutoff = new FactDate(date);
        }

        public virtual bool select (Registration r) {
            FactDate d = r.RegistrationDate;
            return cutoff.overlaps(d);
        }
    }
}