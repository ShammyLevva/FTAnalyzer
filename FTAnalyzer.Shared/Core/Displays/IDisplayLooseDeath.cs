using System;

namespace FTAnalyzer
{
    public interface IDisplayLooseDeath : IComparable<Individual>
    {
        string IndividualID { get; }
        string Forenames { get; }
        string Surname { get; }
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        FactDate DeathDate { get; }
        FactLocation DeathLocation { get; }
        string LooseDeath { get; }
    }
}
