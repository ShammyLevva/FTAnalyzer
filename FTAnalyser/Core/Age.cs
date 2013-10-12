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
        private FactDate birthDate;
        private string age;

        public static Age BIRTH = new Age();

        private Age()
        {
            MinAge = 0;
            MaxAge = 0;
            age = "0";
            birthDate = FactDate.UNKNOWN_DATE;
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
            string pattern = @"^(?<year>\d{1,3}y)? ?(?<month>\d{1,2}m)? ?(?<day>\d{1,2}d)?$";
            Match matcher = Regex.Match(gedcomAge, pattern);
            if (matcher.Success)
            {
                string year = matcher.Groups["year"].ToString().TrimEnd('y');
                string month = matcher.Groups["month"].ToString().TrimEnd('m');
                string day = matcher.Groups["day"].ToString().TrimEnd('d');

                int yearno, monthno, dayno;
                DateTime startDate = when.StartDate;
                DateTime endDate = when.EndDate;
                if (int.TryParse(year, out yearno))
                {
                    startDate = startDate.AddYears(-yearno);
                    endDate = endDate.AddYears(-yearno);
                }
                if (int.TryParse(month, out monthno))
                {
                    startDate = startDate.AddMonths(-monthno);
                    endDate = endDate.AddMonths(-monthno);
                }
                if (int.TryParse(day, out dayno))
                {
                    startDate = startDate.AddDays(-dayno);
                    endDate = endDate.AddDays(-dayno);
                }
                birthDate = new FactDate(startDate, endDate);
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
            if (birthDate.IsKnown)
                return birthDate;
            else
            {
                DateTime startDate = when.StartDate.AddYears(-MaxAge);
                DateTime endDate = when.EndDate.AddYears(-MinAge);
                return new FactDate(startDate, endDate);
            }
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
