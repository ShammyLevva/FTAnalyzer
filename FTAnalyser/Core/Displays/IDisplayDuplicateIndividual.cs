using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayDuplicateIndividual
    {
        string IndividualID { get;}
        string Forenames { get;}
        string Surname { get;}
        string Gender { get;}
        FactDate BirthDate { get;}
        FactLocation BirthLocation { get;}
        string Relation { get; }
        FactLocation RelationToRoot { get;}
    }
}
