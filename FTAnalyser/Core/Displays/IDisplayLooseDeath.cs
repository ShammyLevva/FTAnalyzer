using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayLooseDeath
    {
        string IndividualID { get; }
        string Forenames { get; }
        string Surname { get; }
        FactDate BirthDate { get; }
        string BirthLocation { get; }
        FactDate DeathDate { get; }
        string DeathLocation { get; }
        FactDate LooseDeath { get; }
    }
}
