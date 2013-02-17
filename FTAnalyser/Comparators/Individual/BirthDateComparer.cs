using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class BirthDateComparer : Comparer<IDisplayTreeTops>
    {

        public override int Compare(IDisplayTreeTops x, IDisplayTreeTops y)
        {
            return y.BirthDate.CompareTo(x.BirthDate);
        }
    }
}
