using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class BirthDateComparer : Comparer<IDisplayTreeTops>
    {

        public static bool ASCENDING = true;
        public static bool DESCENDING = false;
        private bool direction = ASCENDING;

        public BirthDateComparer() : this(ASCENDING) {}

        public BirthDateComparer(bool direction)
        {
            this.direction = direction;
        }

        public override int Compare(IDisplayTreeTops x, IDisplayTreeTops y)
        {
            return direction ? x.BirthDate.CompareTo(y.BirthDate) : y.BirthDate.CompareTo(x.BirthDate);
        }
    }
}
