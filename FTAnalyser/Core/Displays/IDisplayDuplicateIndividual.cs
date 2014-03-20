using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        string Relation { get; }
        string RelationToRoot { get; }

        string MatchIndividualID { get; }
        string MatchName { get; }
        FactDate MatchBirthDate { get; }
        FactLocation MatchBirthLocation { get; }
        string MatchRelation { get; }
        string MatchRelationToRoot { get; }
        int Score { get; }
    }
}
