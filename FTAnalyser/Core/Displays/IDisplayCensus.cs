using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayCensus
    {
        string FamilyID { get; }
        string IndividualID { get; }
        FactLocation CensusLocation { get; }
        string CensusName { get; }
        Age Age { get; }
        string Occupation { get; }
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        string Status { get; }
        string Relation { get; }
        int Ahnentafel { get; }
    }
}
