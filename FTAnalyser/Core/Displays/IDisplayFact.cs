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
        string Comment { get; }
        Age AgeAtFact { get; }
        string SourceList { get; }
        string GoogleLocation { get; }
        string GoogleResultType { get; }
        string GeocodeStatus { get; }
        Image LocationIcon { get; }
    }
}
 