using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayCensus
    {
        string FamilyGed { get; }
        FactLocation RegistrationLocation { get; }
        string CensusName { get; }
        Age Age { get; }
        string Occupation { get; }
        string DateOfBirth { get; }
        string BirthLocation { get; }
        string Status { get; }
        string Relation { get; }
        int Ahnentafel { get; }
    }
}
