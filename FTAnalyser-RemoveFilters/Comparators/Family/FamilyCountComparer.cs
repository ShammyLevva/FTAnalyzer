using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class FamilyCountComparer : Comparer<IDisplayFamily>
    {
        private bool countSortLow;

        public FamilyCountComparer(bool countSortLow)
        {
            this.countSortLow = countSortLow;
        }

        public override int Compare(IDisplayFamily x, IDisplayFamily y)
        {
            if (countSortLow)
                return x.Count.CompareTo(y.Count);
            else
                return y.Count.CompareTo(x.Count);
        }
    }
}
