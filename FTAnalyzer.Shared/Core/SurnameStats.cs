using System;
using System.Collections.Specialized;

namespace FTAnalyzer
{
    public class SurnameStats
    {
        public string Surname { get; private set; }
        public int Individuals { get; set; }
        public int Families { get; set; }
        public int Marriages { get;  set; }
        public string GOONSpage { get; set; }
        
        public SurnameStats(string name)
        {
            this.Surname = name;
            this.Individuals = 0;
            this.Families = 0; 
            this.Marriages = 0;
            this.GOONSpage = string.Empty;
        }
    }
}
