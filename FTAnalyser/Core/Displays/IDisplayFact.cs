using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayFact
    {
        string TypeOfFact { get; }
        FactDate FactDate { get; }
        FactLocation Location { get; }
        string Comment { get; }
    }
}
 