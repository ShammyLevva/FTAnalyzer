using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class IndividualBirthFilter : Filter<Individual>
    {

        private FactDate cutoff;

        public IndividualBirthFilter(FactDate cutoff)
        {
            this.cutoff = cutoff;
        }

        public IndividualBirthFilter(string date) : this(new FactDate(date)) { }

        public bool select(Individual t)
        {
            return cutoff.overlaps(t.BirthDate);
        }
    }
}