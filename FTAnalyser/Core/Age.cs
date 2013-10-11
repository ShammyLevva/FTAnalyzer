using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FTAnalyzer
{
    public class Age : IComparable<Age>
    {
        public int MinAge { get; private set; }
        public int MaxAge { get; private set; }
        private TimeSpan daysOld;
        private string age;

        public static Age BIRTH = new Age();

        private Age()
        {
            MinAge = 0;
            MaxAge = 0;
            age = "0";
            daysOld = TimeSpan.MaxValue;
        }

        public Age(Individual ind, FactDate when)
            : this()
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

        public Age(string gedcomAge, FactDate when)
            : this()
        {
            // parse ages from gedcom
            string pattern = @"^(\d{1,3}y)? ?(\d{1,2}m)? ?(\d{1,2}d)?$";
            Match matcher = Regex.Match(gedcomAge, pattern);
            if (matcher.Success)
            {
                string year = matcher.Groups[0].ToString().TrimEnd('y');
                string month = matcher.Groups[1].ToString();
                string day = matcher.Groups[2].ToString();

                int yearno = int.Parse(year);
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

        public FactDate GetBirthDate(FactDate when)
        {
            if (daysOld == TimeSpan.MaxValue)
            {
                DateTime startDate = when.StartDate.AddYears(-MaxAge);
                DateTime endDate = when.EndDate.AddYears(-MinAge);
                return new FactDate(startDate, endDate);
            }
            else
                return new FactDate(when.StartDate - daysOld, when.EndDate - daysOld);
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
