using System;

namespace FTAnalyzer
{
    public interface IDisplayColourBMD
    {
        string IndividualID { get; }        
        string Forenames { get; }           
        string Surname { get; }
        string Relation { get; }
        string RelationToRoot { get; }
        
        Forms.ColourBMD.ColourValue Birth { get; }
        Forms.ColourBMD.ColourValue BaptChri { get; }
        Forms.ColourBMD.ColourValue Marriage1 { get; }
        Forms.ColourBMD.ColourValue Marriage2 { get; }
        Forms.ColourBMD.ColourValue Marriage3 { get; }
        Forms.ColourBMD.ColourValue Death { get; }
        Forms.ColourBMD.ColourValue CremBuri { get; }
        
        FactDate BirthDate { get; }
        FactDate DeathDate { get; }
        string FirstMarriage { get; }
        string SecondMarriage { get; }
        string ThirdMarriage { get; }
        FactLocation BirthLocation { get; }
        FactLocation DeathLocation { get; }
        FactLocation BestLocation(FactDate when);
        Int64 Ahnentafel { get; }
    }
}
