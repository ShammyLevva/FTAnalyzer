using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DisplayFact : IDisplayFact
    {
        public string Name { get; private set; }
        private Fact Fact { get; set; }

        public DisplayFact(string name, Fact fact)
        {
            this.Name = name;
            this.Fact = fact;
        }

        public string TypeOfFact { get { return Fact.GetFactTypeDescription(Fact.FactType); } }
        public FactDate FactDate { get { return Fact.FactDate; } }
        public FactLocation Location { get { return Fact.Location; } }
        public string Comment { get { return Fact.Comment; } }
    }
}
