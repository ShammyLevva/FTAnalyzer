using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class SurnameStats
    {
        public string Surname { get; private set; }
        public int Individuals { get; set; }
        public int Families { get; set; }
        public int Marriages { get;  set; }
        public Uri URI { get; set; }
        
        public SurnameStats(string name)
        {
            this.Surname = name;
            this.Individuals = 0;
            this.Families = 0; 
            this.Marriages = 0;
            this.URI = null;
        }
    }
}
