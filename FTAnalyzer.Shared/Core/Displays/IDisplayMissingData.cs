using System;

namespace FTAnalyzer
{
    public interface IDisplayMissingData
    {
        string IndividualID { get; }        
        string Forenames { get; }           
        string Surname { get; }
        string Relation { get; }
        string RelationToRoot { get; }
        
        ColourValues.BMDColour Birth { get; }
        ColourValues.BMDColour BaptChri { get; }
        ColourValues.BMDColour Marriage1 { get; }
        ColourValues.BMDColour Marriage2 { get; }
        ColourValues.BMDColour Marriage3 { get; }
        ColourValues.BMDColour Death { get; }
        ColourValues.BMDColour CremBuri { get; }
        
        FactDate BirthDate { get; }
        FactDate DeathDate { get; }
        string FirstMarriage { get; }
        string SecondMarriage { get; }
        string ThirdMarriage { get; }
        FactLocation BirthLocation { get; }
        FactLocation DeathLocation { get; }
        FactLocation BestLocation(FactDate when);
        Int64 Ahnentafel { get; }
        float Score { get; }
    }
}
