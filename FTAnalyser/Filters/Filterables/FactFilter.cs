using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class FactFilter<T> : Filter<T> where T: IFactsFilterable
    {

        internal string factType;
        internal FactDate factDate;

        public FactFilter(string factType, FactDate factDate)
        {
            this.factType = factType;
            this.factDate = factDate;
        }

        public virtual bool select (T t) {
            foreach (Fact f in t.AllFacts)
            {
                if (f.FactType == factType && f.FactDate.overlaps(factDate))
                    return true;
            }
            return false;
        }
    }
}