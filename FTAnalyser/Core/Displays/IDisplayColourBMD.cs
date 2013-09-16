using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayColourBMD
    {
        string IndividualID { get; }        
        string Forenames { get; }           
        string Surname { get; }
        string Relation { get; }

        int BirthStatus { get; }
        int BaptismChristeningStatus { get; }
        int FirstMarriageStatus { get; }
        int SecondMarriageStatus { get; }
        int ThirdMarriageStatus { get; }
        int DeathStatus { get; }
        int CremationBurialStatus { get; }
        
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactLocation BestLocation(FactDate when);
        FactDate DeathDate { get; }
        int Ahnentafel { get; }
    }
}
