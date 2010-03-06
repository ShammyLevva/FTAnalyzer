using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayIndividual
    {
        string IndividualID { get; }
        string Forename { get; }
        string Surname { get; }
        string ForenameMetaphone { get; }
        string SurnameMetaphone { get; }
        string Gender { get; }
        string Alias { get; }
        string Status { get; }
        int RelationType { get; }
        FactDate BirthDate { get; }
        string BirthLocation { get; }
        FactDate DeathDate { get; }
        string DeathLocation { get; }
        string Occupation { get; }
        FactLocation BestLocation { get; }
        Age CurrentAge { get; }
    }
}
