using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FTAnalyzer.Utilities;

namespace FTAnalyzer
{
    public class Age : IComparable<Age>
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public int MinAge { get; private set; }
        public int MaxAge { get; private set; }
        public FactDate CalculatedBirthDate { get; private set; }
        public string GEDCOM_Age { get; private set; }
        private string age;

        public static Age BIRTH = new Age();

        private Age()
        {
            MinAge = 0;
            MaxAge = 0;
            age = "0";
            GEDCOM_Age = string.Empty;
            CalculatedBirthDate = FactDate.UNKNOWN_DATE;
        }

        public Age(Individual ind, FactDate when)
            : this()
        {
            if (when.IsAfter(ind.DeathDate))
                when = ind.DeathDate;
            log.Debug("Calculating Age for " + ind.Name + " on " + when.ToString());
            log.Debug("Min age: birth enddate:" + ind.BirthDate.EndDate + " to startdate:" + when.StartDate);
            log.Debug("Max age: birth startdate:" + ind.BirthDate.StartDate + " to enddate:" + when.EndDate);
            MinAge = GetAge(ind.BirthDate.EndDate, when.StartDate);
            MaxAge = GetAge(ind.BirthDate.StartDate, when.EndDate);
            log.Debug("Calculated minage:" + MinAge + " calculated maxage:" + MaxAge);
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

        static string pattern = @"^(?<year>\d{1,3}y)? ?(?<month>\d{1,2}m)? ?(?<day>\d{1,2}d)?$";
        static Regex ydm = new Regex(pattern, RegexOptions.Compiled);

        public Age(string gedcomAge, FactDate when)
            : this()
        {
            // parse ages from gedcom
            Match matcher = ydm.Match(gedcomAge);
            if (matcher.Success)
            {
                this.GEDCOM_Age = gedcomAge;
                string year = matcher.Groups["year"].ToString().TrimEnd('y');
                string month = matcher.Groups["month"].ToString().TrimEnd('m');
                string day = matcher.Groups["day"].ToString().TrimEnd('d');

                DateTime startDate = when.StartDate;
                DateTime endDate = when.EndDate;
                if (int.TryParse(year, out int yearno))
                {
                    if(startDate != FactDate.MINDATE && startDate.Year > yearno + 1)
                        startDate = startDate.TryAddYears(-yearno);
                    endDate = endDate.TryAddYears(-yearno);
                }
                if (int.TryParse(month, out int monthno))
                {
                    if (startDate != FactDate.MINDATE && startDate.Year > 1)
                        startDate = startDate.AddMonths(-monthno);
                    endDate = endDate.AddMonths(-monthno);
                }
                if (int.TryParse(day, out int dayno))
                {  // -dayno + 1 as date will be at time 00:00 and subtraction is one day too much.
                    if (startDate != FactDate.MINDATE &&  startDate.Year > 1)
                        startDate = startDate.AddDays(-dayno);
                    endDate = endDate.AddDays(-dayno);
                }
                CalculatedBirthDate = new FactDate(startDate, endDate);
            }
        }

        private int GetAge(DateTime birthDate, DateTime laterDate)
        {
            int age;
            age = laterDate.Year - birthDate.Year;
            if (age > 0)
            {
                age -= Convert.ToInt32(laterDate.Date < birthDate.Date.TryAddYears(age));
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
            if (CalculatedBirthDate.IsKnown)
                return CalculatedBirthDate;
            else
            {
                DateTime startDate = when.StartDate.TryAddYears(-MaxAge);
                DateTime endDate = when.EndDate.TryAddYears(-MinAge);
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

        public override bool Equals(object obj)
        {
            if(obj.GetType() != typeof(Age))
                return false;
            Age that = (Age)obj;
            return this.MaxAge == that.MaxAge && this.MinAge == that.MinAge;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
