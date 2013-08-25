using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class FactComparer : Comparer<IDisplayFact>
    {
        public override int Compare(IDisplayFact f1, IDisplayFact f2)
        {
            return f1.FactDate.CompareTo(f2.FactDate);
        }
    }
}
