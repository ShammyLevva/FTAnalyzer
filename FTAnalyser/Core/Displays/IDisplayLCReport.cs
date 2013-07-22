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
        string Gender { get; }
        string Relation { get; }
        int Ahnentafel { get; }
        FactDate BirthDate { get; }         
        FactDate DeathDate { get; }       
        int Census1841 { get; }
        int Census1851 { get; }
        int Census1861 { get; }
        int Census1871 { get; }
        int Census1881 { get; }
        int Census1891 { get; }
        int Census1901 { get; }
        int Census1911 { get; }
    }
}
