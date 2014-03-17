using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FTAnalyzer
{
    public interface IDisplayFact
    {
        Image Icon { get; }
        string IndividualID { get; }
        string Surname { get; }
        string Forenames { get; }
        string SurnameAtDate { get; }
        string TypeOfFact { get; }
        FactDate FactDate { get; }
        string Relation { get; }
        string RelationToRoot { get; }
        FactLocation Location { get; }
        Age AgeAtFact { get; }
        Image LocationIcon { get; }
        string GeocodeStatus { get; }
        string GoogleLocation { get; }
        string GoogleResultType { get; }
        string Comment { get; }
        string SourceList { get; }
        double Latitude { get; }
        double Longitude { get; }
    }
}
