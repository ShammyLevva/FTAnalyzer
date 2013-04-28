using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class FamilyNameComparer : Comparer<IDisplayFamily>
    {

        public override int Compare(IDisplayFamily x, IDisplayFamily y)
        {
//            int i = x.Surname.CompareTo(y.Surname);
//            return (i==0) ? x.Forenames.CompareTo(y.Forenames) : i;
            return 0;
        }
    }
}
