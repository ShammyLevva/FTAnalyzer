using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplayFact : IDisplayFact
    {
        
        public string Name { get; private set; }
        private Individual Ind { get; set; }
        private Fact Fact { get; set; }

        public DisplayFact(Individual ind, string name, Fact fact)
        {
            this.Ind = ind;
            this.Name = name;
            this.Fact = fact;
        }

        public string TypeOfFact { get { return Fact.GetFactTypeDescription(Fact.FactType); } }
        public FactDate FactDate { get { return Fact.FactDate; } }
        public FactLocation Location { get { return Fact.Location; } }
        public string Comment { get { return Fact.Comment; } }
        public Age AgeAtFact { get { return Ind == null ? null : Ind.GetAge(Fact.FactDate, Fact.FactType); } }
        public string SourceList { get { return Fact.SourceList;  } }
    }
}
