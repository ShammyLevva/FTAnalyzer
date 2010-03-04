using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyser
{
    public interface IDisplayLooseDeath
    {
        string IndividualID { get; }
        string Forename { get; }
        string Surname { get; }
        FactDate BirthDate { get; }
        string BirthLocation { get; }
        FactDate DeathDate { get; }
        string DeathLocation { get; }
    }
}
