using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class DefaultCensusComparer : Comparer<DisplayCensus>
    {
        public override int Compare(DisplayCensus c1, DisplayCensus c2)
        {
            return c1.Position - c2.Position;
        }
    }
}
