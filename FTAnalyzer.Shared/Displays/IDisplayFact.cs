#if !__MACOS__
using System.Drawing;
#endif

namespace FTAnalyzer
{
    public interface IDisplayFact
    {
#if !__MACOS__
        Image Icon { get; }
#endif
        string IndividualID { get; }
        string Surname { get; }
        string Forenames { get; }
        FactDate DateofBirth { get; }
        string SurnameAtDate { get; }
        string TypeOfFact { get; }
        FactDate FactDate { get; }
        string Relation { get; }
        string RelationToRoot { get; }
        FactLocation Location { get; }
        Age AgeAtFact { get; }
#if !__MACOS__
        Image LocationIcon { get; }
#endif
        string GeocodeStatus { get; }
        string FoundLocation { get; }
        string FoundResultType { get; }
        CensusReference CensusReference { get; }
        string CensusRefYear { get; }
        string Comment { get; }
        string SourceList { get; }
        double Latitude { get; }
        double Longitude { get; }
        bool Preferred { get; }
        bool IgnoreFact { get; }
    }
}
