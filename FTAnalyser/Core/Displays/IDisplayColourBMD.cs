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
        string RelationToRoot { get; }
        
        int Birth { get; }
        int BaptChri { get; }
        int Marriage1 { get; }
        int Marriage2 { get; }
        int Marriage3 { get; }
        int Death { get; }
        int CremBuri { get; }
        
        FactDate BirthDate { get; }
        FactDate DeathDate { get; }
        string FirstMarriage { get; }
        string SecondMarriage { get; }
        string ThirdMarriage { get; }
        FactLocation BirthLocation { get; }
        FactLocation DeathLocation { get; }
        FactLocation BestLocation(FactDate when);
        int Ahnentafel { get; }
    }
}
