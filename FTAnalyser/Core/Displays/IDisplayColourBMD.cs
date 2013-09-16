using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayColourBMD
    {
        string Ind_ID { get; }        
        string Forenames { get; }           
        string Surname { get; }
        string Relation { get; }
        
        int Birth { get; }
        int BaptChri { get; }
        int Marriage1 { get; }
        int Marriage2 { get; }
        int Marriage3 { get; }
        int Death { get; }
        int CremBuri { get; }
        
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactLocation BestLocation(FactDate when);
        FactDate DeathDate { get; }
        int Ahnentafel { get; }
    }
}
