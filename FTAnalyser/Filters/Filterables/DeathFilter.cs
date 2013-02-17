using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class DeathFilter : Filter<Individual>
    {

        private FactDate cutoff;

        public DeathFilter(FactDate cutoff)
        {
            this.cutoff = cutoff;
        }

        public DeathFilter(string date) : this(new FactDate(date)) { }

        public bool select(Individual t)
        {
            return cutoff.overlaps(t.DeathDate);
        }
    }
}