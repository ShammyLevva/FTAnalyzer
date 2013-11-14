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
        string Ind_ID { get; }
        string Name { get; }
        string TypeOfFact { get; }
        FactDate FactDate { get; }
        FactLocation Location { get; }
        Age AgeAtFact { get; }
        Image LocationIcon { get; }
        string GeocodeStatus { get; }
        string GoogleLocation { get; }
        string GoogleResultType { get; }
        string Comment { get; }
        string SourceList { get; }
    }
}
 