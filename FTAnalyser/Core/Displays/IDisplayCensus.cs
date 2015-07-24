using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayCensus
    {
        string FamilyID { get; }
        int Position { get; }
        string IndividualID { get; }
        FactLocation CensusLocation { get; }
        string CensusName { get; }
        Age Age { get; }
        string Occupation { get; }
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactDate DeathDate { get; }
        FactLocation DeathLocation { get; }
        string CensusStatus { get; }
        string CensusReference { get; }
        string Relation { get; }
        string RelationToRoot { get; }
        Int64 Ahnentafel { get; }
    }
}
