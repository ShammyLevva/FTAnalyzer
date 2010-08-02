using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class DefaultCensusComparer : Comparer<IDisplayCensus>
    {
        public override int Compare(IDisplayCensus c1, IDisplayCensus c2)
        {
            return c1.Position - c2.Position;
        }
    }
}
