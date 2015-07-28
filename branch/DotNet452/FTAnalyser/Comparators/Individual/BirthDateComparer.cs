using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class BirthDateComparer : Comparer<IDisplayIndividual>
    {
        public static bool ASCENDING = true;
        public static bool DESCENDING = false;
        private bool direction = ASCENDING;

        public BirthDateComparer() : this(ASCENDING) { }

        public BirthDateComparer(bool direction)
        {
            this.direction = direction;
        }

        public override int Compare(IDisplayIndividual x, IDisplayIndividual y)
        {
            IDisplayIndividual a = x, b=y;
            if(direction == DESCENDING)
            {
                a = y;
                b = x;
            }
            if (a.BirthDate.Equals(b.BirthDate))
            {
                if (a.Surname.Equals(b.Surname))
                    return a.Forenames.CompareTo(b.Forenames);
                else
                    return a.Surname.CompareTo(b.Surname);
            }
            else
                return a.BirthDate.CompareTo(b.BirthDate);
        }
    }
}
