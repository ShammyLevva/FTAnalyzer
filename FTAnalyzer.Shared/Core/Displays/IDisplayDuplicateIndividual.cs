namespace FTAnalyzer
{
    public interface IDisplayDuplicateIndividual
    {
        string IndividualID { get;}
        string Name { get;}
        string Forenames { get; } 
        string Surname { get; }
        FactDate BirthDate { get;}
        FactLocation BirthLocation { get;}

        string MatchIndividualID { get; }
        string MatchName { get; }
        FactDate MatchBirthDate { get; }
        FactLocation MatchBirthLocation { get; }
        int Score { get; }
        bool IgnoreNonDuplicate { get; }
    }
}
