using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IExportIndividual
    {
        string IndividualID { get; }
        string Forenames { get; }
        string Surname { get; }
        string Alias { get; }
        string Gender { get; }
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactDate DeathDate { get; }
        FactLocation DeathLocation { get; }
        string Occupation { get; }
        Age LifeSpan { get; }
        string Relation { get; }
        string BudgieCode { get; }
        int Ahnentafel { get; }
        bool HasRangedBirthDate { get; }
        bool HasParents { get; }
        int CensusFactCount { get; }
        string MarriageDates { get; }
        string MarriageLocations { get; }
    }
}
