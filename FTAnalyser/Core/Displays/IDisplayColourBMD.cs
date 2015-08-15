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
        
        ColourValues.ColourValue Birth { get; }
        ColourValues.ColourValue BaptChri { get; }
        ColourValues.ColourValue Marriage1 { get; }
        ColourValues.ColourValue Marriage2 { get; }
        ColourValues.ColourValue Marriage3 { get; }
        ColourValues.ColourValue Death { get; }
        ColourValues.ColourValue CremBuri { get; }
        
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
