using System;

namespace FTAnalyzer
{
    public interface IDisplayLooseBirth : IComparable<Individual>
    {
        string IndividualID { get; }
        string Forenames { get; }
        string Surname { get; }
        FactDate BirthDate { get; }
        FactLocation BirthLocation { get; }
        string LooseBirth { get; }
    }
}
