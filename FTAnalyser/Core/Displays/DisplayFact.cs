using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FTAnalyzer
{
    public class DisplayFact : IDisplayFact
    {
        public Image Icon { get; private set; }
        public string Name { get; private set; }
        private Individual Ind { get; set; }
        public Fact Fact { get; set; }
        
        public DisplayFact(Individual ind, string name, Fact fact)
        {
            this.Ind = ind;
            this.Name = name;
            this.Fact = fact;
            this.Icon = FactImage.ErrorIcon(fact.FactErrorLevel).Icon;
        }

        public string TypeOfFact { get { return Fact.FactTypeDescription; } }
        public FactDate FactDate { get { return Fact.FactDate; } }
        public FactLocation Location { get { return Fact.Location; } }
        public string Comment { get { return Fact.Comment; } }
        public Age AgeAtFact { get { return Ind == null ? null : Ind.GetAge(Fact.FactDate, Fact.FactType); } }
        public string SourceList { get { return Fact.SourceList;  } }
    }
}
