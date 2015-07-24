using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class FactLocationComparer : Comparer<IDisplayLocation>
    {
        private int level;

        public FactLocationComparer(int level)
        {
            this.level = level;
        }

        public override int Compare(IDisplayLocation x, IDisplayLocation y)
        {
            return x.CompareTo(y, level);
        }
    }
}
