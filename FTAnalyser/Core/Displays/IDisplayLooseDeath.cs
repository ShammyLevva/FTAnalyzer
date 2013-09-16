using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayLooseDeath
    {
        string Ind_ID { get; }
        string Forenames { get; }
        string Surname { get; }
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactDate DeathDate { get; }
        FactLocation DeathLocation { get; }
        string LooseDeath { get; }
    }
}
