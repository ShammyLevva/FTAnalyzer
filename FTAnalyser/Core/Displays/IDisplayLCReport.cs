using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayLCReport
    {
        string IndividualID { get; }        
        string Forenames { get; }           
        string Surname { get; }
        string MarriedName { get; }
        string Relation { get; }
        int C1841 { get; }
        int C1851 { get; }
        int C1861 { get; }
        int C1871 { get; }
        int C1881 { get; }
        int C1891 { get; }
        int C1901 { get; }
        int C1911 { get; }
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactDate DeathDate { get; }
    }
}
