using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayIndividual
    {
        string IndividualID { get; }
        string Forenames { get; }
        string Surname { get; }
        string Gender { get; }
        FactDate BirthDate { get; }
        string BirthLocation { get; }
        FactDate DeathDate { get; }
        string DeathLocation { get; }
        string Status { get; }
        int RelationType { get; }
        string Occupation { get; }
        FactLocation BestLocation { get; }
        Age CurrentAge { get; }
    }
}
