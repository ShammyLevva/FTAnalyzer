using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class MapFact
    {
        public Individual Individual { get; private set;}
        public Fact Fact { get; private set; }

        public MapFact(Individual ind, Fact f)
        {
            this.Individual = ind;
            this.Fact = f;
        }

        public FactLocation Location
        {
            get { return Fact.Location; }
        }

        public FactDate FactDate
        {
            get { return Fact.FactDate; }
        }
    }
}

