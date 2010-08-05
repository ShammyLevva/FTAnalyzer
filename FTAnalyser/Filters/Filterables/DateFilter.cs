using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class DateFilter<T> : Filter<T> where T : IDateFilterable {

        internal FactDate cutoff;

        public static DateFilter<T> POST_1855_FILTER = new DateFilter<T>("AFT 31 DEC 1854");
        public static DateFilter<T> POST_1837_FILTER = new DateFilter<T>("AFT 30 JUN 1837");

        public static DateFilter<T> PRE_1855_FILTER = new DateFilter<T>("BEF 01 JAN 1855");
        public static DateFilter<T> PRE_1837_FILTER = new DateFilter<T>("BEF 01 JUL 1837");

        public static DateFilter<T> ONLINE_BIRTH_FILTER = new DateFilter<T>("BET 01 JAN 1855 AND 31 DEC 1904");

        public static DateFilter<T> GROS_BIRTH_FILTER = new DateFilter<T>("AFT 31 DEC 1904");

        public DateFilter (FactDate cutoff) {
            this.cutoff = cutoff;
        }

        public DateFilter (string date) {
            this.cutoff = new FactDate(date);
        }

        public virtual bool select (T t) {
            FactDate d = t.FilterDate;
            return cutoff.overlaps(d);
        }
    }
}