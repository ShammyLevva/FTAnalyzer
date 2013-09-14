using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Age : IComparable<Age>
    {
        public int MinAge { get; private set; }
        public int MaxAge { get; private set; }
        private string age;
        public static Age BIRTH = new Age();

        private Age()
        {
            MinAge = 0;
            MaxAge = 0;
            age = "0";
        }

        public Age(Individual ind, FactDate when)
        {
            if (when.IsAfter(ind.DeathDate))
                when = ind.DeathDate;
            MinAge = GetAge(ind.BirthDate.EndDate, when.StartDate);
            MaxAge = GetAge(ind.BirthDate.StartDate, when.EndDate);
            if (MinAge == FactDate.MINYEARS)
            {
                if (MaxAge == FactDate.MAXYEARS)
                    age = "Unknown";
                else
                    age = MaxAge == 0 ? "< 1" : "<=" + MaxAge;
            }
            else if (MaxAge < FactDate.MAXYEARS)
            {
                age = MinAge == MaxAge ? MinAge.ToString() : MinAge + " to " + MaxAge;
            }
            else
            {
                // if age over maximum return maximum
                age = ">=" + MinAge;
            }
        }

        private int GetAge(DateTime birthDate, DateTime laterDate)
        {
            int age;
            age = laterDate.Year - birthDate.Year;
            if (age > 0)
            {
                age -= Convert.ToInt32(laterDate.Date < birthDate.Date.AddYears(age));
            }
            else
            {
                age = 0;
            }
            if (age > FactDate.MAXYEARS)
                age = FactDate.MAXYEARS;
            return age;
        }

        public override string ToString()
        {
            return age;
        }

        public int CompareTo(Age that)
        {
            if (this.MinAge == that.MinAge)
                return this.MaxAge - that.MaxAge;
            return this.MinAge - that.MinAge;
        }
    }
}
