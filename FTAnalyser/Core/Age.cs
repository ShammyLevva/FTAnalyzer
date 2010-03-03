using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyser
{
    public class Age
    {
        private int minAge;
        private int maxAge;
        private string age;

        public Age(Individual ind, FactDate when)
        {
            minAge = ind.BirthDate.getMinimumYear(when);
            maxAge = ind.BirthDate.getMaximumYear(when);
            if (minAge == FactDate.MINYEARS)
            {
                if (maxAge == FactDate.MAXYEARS)
                    age = "Unknown";
                else
                    age = "<=" + maxAge;
            }
            else
            {
                if (maxAge == FactDate.MAXYEARS)
                {
                    if (minAge >= FactDate.MAXYEARS)
                    {
                        // if age over maximum return maximum
                        age = ">=" + FactDate.MAXYEARS.ToString();
                    }
                    else
                    {
                        age = ">=" + minAge;
                    }
                }
                else
                {
                    age = minAge == maxAge ? minAge.ToString() : minAge + " to " + maxAge;
                }
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
    }
}
