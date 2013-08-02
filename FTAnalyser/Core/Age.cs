using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Age : IComparable<Age>
    {
        private int minAge;
        private int maxAge;
        private string age;

        public Age(Individual ind, FactDate when)
        {
            if (when.isAfter(ind.DeathDate))
                when = ind.DeathDate;
            minAge = ind.BirthDate.getMinimumYear(when);
            maxAge = ind.BirthDate.getMaximumYear(when);
            if (minAge == FactDate.MINYEARS)
            {
                if (maxAge == FactDate.MAXYEARS)
                    age = "Unknown";
                else
                    age = "<=" + maxAge;
            }
            else if (maxAge < FactDate.MAXYEARS)
            {
                age = minAge == maxAge ? minAge.ToString() : minAge + " to " + maxAge;
            }
            else
            {
                // if age over maximum return maximum
                age = ">=" + minAge;
            }
            
        }

        public int MinAge
        {
            get { return minAge; }
        }

        public int MaxAge
        {
            get { return maxAge; }
        }

        public override string ToString()
        {
            return age;
        }

        public int CompareTo(Age that)
        {
            if (this.minAge == that.minAge)
                return this.maxAge.CompareTo(that.maxAge);
            return this.minAge.CompareTo(that.minAge);
        }
    }
}
