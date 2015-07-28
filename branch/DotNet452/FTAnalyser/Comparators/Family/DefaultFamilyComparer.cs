using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class DefaultFamilyComparer : Comparer<IDisplayFamily>
    {

        public override int Compare(IDisplayFamily x, IDisplayFamily y)
        {
            return x.FamilyID.CompareTo(y.FamilyID);
        }
    }
}
