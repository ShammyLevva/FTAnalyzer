using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public interface IDisplayFamily
    {
        string FamilyGed { get; }
        string Husband { get; }
        string Wife { get; }
        string Marriage { get; }
        string Children { get; }
        FactLocation Location { get; }
        int Count { get; }
    }
}
