using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class FamilySizeComparer : Comparer<IDisplayFamily>
    {
        private bool countSortLow;

        public FamilySizeComparer(bool countSortLow)
        {
            this.countSortLow = countSortLow;
        }

        public override int Compare(IDisplayFamily x, IDisplayFamily y)
        {
            if (countSortLow)
                return x.FamilySize.CompareTo(y.FamilySize);
            else
                return y.FamilySize.CompareTo(x.FamilySize);
        }
    }
}
