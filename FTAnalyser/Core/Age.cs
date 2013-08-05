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
            minAge = getAge(ind.BirthDate.EndDate, when.StartDate, FactDate.MINDATE);
            maxAge = getAge(ind.BirthDate.StartDate, when.EndDate, FactDate.MAXDATE);
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

        private int getAge(DateTime birthDate, DateTime laterDate, DateTime nullValue)
        {
            int age;
            if (laterDate == null)
                laterDate = nullValue;
            age = laterDate.Year - birthDate.Year;
            if (age > 0)
            {
                age -= Convert.ToInt32(laterDate.Date < birthDate.Date.AddYears(age));
            }
            else
            {
                age = 0;
            }
            return age;
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
