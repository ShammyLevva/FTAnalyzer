using System;

namespace FTAnalyzer
{
    public interface IDisplayIndividual
    {
        string IndividualID { get; }        
        string Forenames { get; }           
        string Surname { get; }             
        string Gender { get; }              
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }       
        FactDate DeathDate { get; }
        FactLocation DeathLocation { get; }       
        string Occupation { get; }          
        FactLocation BestLocation(FactDate when);
        Age LifeSpan { get; }             
        string Relation { get; }
        string RelationToRoot { get; }
        int MarriageCount { get; }
        int ChildrenCount { get; }
        string BudgieCode { get; }
        Int64 Ahnentafel { get; }
        bool HasNotes { get; }
    }
}
