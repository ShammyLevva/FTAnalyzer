using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FTAnalyser
{
    public class FactDate: IComparable<FactDate>
    {
        public static readonly DateTime MINDATE = new DateTime(1, 1, 1);
        public static readonly DateTime MAXDATE = new DateTime(9999, 12, 31);
        public static readonly int MAXYEARS = 110;
        public static readonly int MINYEARS = 0;
        private static readonly int LOW = 0;
        private static readonly int HIGH = 1;

        public static readonly string YEAR = "yyyy";
        public static readonly string MONTHYEAR = "MMM yyyy";
        public static readonly string DAYMONTH = "dd MMM";
        public static readonly string MONTH = "MMM";
        public static readonly string FULL = "dd MMM yyyy";
        public static readonly string DISPLAY = "d MMM yyyy";
        public static readonly string CHECKING = "dd MMM";
        public static readonly string DATE_PATTERN = "(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{0,4})";

        public static readonly FactDate UNKNOWN_DATE = new FactDate("UNKNOWN");
        public static readonly FactDate CENSUS1841 = new FactDate("06 JUN 1841");
        public static readonly FactDate CENSUS1851 = new FactDate("30 MAR 1851");
        public static readonly FactDate CENSUS1861 = new FactDate("07 APR 1861");
        public static readonly FactDate CENSUS1871 = new FactDate("02 APR 1871");
        public static readonly FactDate CENSUS1881 = new FactDate("03 APR 1881");
        public static readonly FactDate CENSUS1891 = new FactDate("05 APR 1891");
        public static readonly FactDate CENSUS1901 = new FactDate("31 MAR 1901");
 
        private string datestring;
        private DateTime startdate;
        private DateTime enddate;

        public FactDate(string datestring)
        {
            if (datestring == null || datestring.Length == 0)
            {
                this.datestring = "UNKNOWN";
            }
            else
            {
                this.datestring = datestring.ToUpper();
            }
            startdate = MINDATE;
            enddate = MAXDATE;
            if (!this.datestring.Equals("UNKNOWN"))
            {
                processDate(this.datestring);
            }
        }

        public FactDate(DateTime startdate, DateTime enddate)
        {
            this.startdate = startdate;
            this.enddate = enddate;
            this.datestring = calculateDatestring();
        }

        public static string Format(string format, DateTime date)
        {
            return string.Format("{0:" + format + "}", date).ToUpper();
        }

        public FactDate addYears(int years)
        {
            DateTime start = new DateTime(startdate.Year, startdate.Month, startdate.Day);
            DateTime end = new DateTime(enddate.Year, enddate.Month, enddate.Day);
            start.AddYears(years);
            end.AddYears(years);
            if (end > MAXDATE)
                end = MAXDATE;
            return new FactDate(start, end);
        }

        private string calculateDatestring()
        {
            string check;
            bool between = false;
            StringBuilder output = new StringBuilder();
            if (startdate == MINDATE)
            {
                if (enddate == MAXDATE)
                    return "UNKNOWN";
                else
                    output.Append("BEF ");
            }
            else
            {
                check = Format(CHECKING, startdate);
                if (enddate == MAXDATE)
                    output.Append("AFT ");
                else
                {
                    output.Append("BET ");
                    between = true;
                }
                if (check.Equals("01 JAN"))
                    output.Append(Format(YEAR, startdate));
                else
                    output.Append(Format(DISPLAY, startdate));
                if (between)
                    output.Append(" AND ");
            }
            if (enddate != MAXDATE)
            {
                check = Format(CHECKING, enddate);
                if (check.Equals("31 DEC"))
                {
                    // add 1 day to take it to 1st Jan following year
                    // this makes the range of "bef 1900" change to 
                    // "bet xxxx and 1900"
                    output.Append(Format(YEAR, enddate));
                }
                else
                    output.Append(Format(DISPLAY, enddate));
            }
            return output.ToString().ToUpper();
        }

        private void processDate(string processDate)
        {
            // takes datestring and works out start and end dates 
            // prefixes are BEF, AFT, BET and nothing
            // dates are "YYYY" or "MMM YYYY" or "DD MMM YYYY"
            try
            {
                string dateValue = processDate.Substring(4);
                if (processDate.StartsWith("BEF"))
                {
                    enddate = parseDate(dateValue, HIGH, -1);
                }
                else if (processDate.StartsWith("AFT"))
                {
                    startdate = parseDate(dateValue, LOW, +1);
                }
                else if (processDate.StartsWith("ABT"))
                {
                    startdate = parseDate(dateValue, LOW, -1);
                    enddate = parseDate(dateValue, HIGH, +1);
                }
                else if (processDate.StartsWith("BET"))
                {
                    int andpos = processDate.IndexOf(" AND ");
                    enddate = parseDate(processDate.Substring(andpos + 5), HIGH, 0);
                    startdate = parseDate(processDate.Substring(4, andpos), LOW, 0, enddate.Year);
                }
                else
                {
                    dateValue = processDate;
                    startdate = parseDate(dateValue, LOW, 0);
                    enddate = parseDate(dateValue, HIGH, 0);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.InnerException);
            }
        }

        private DateTime parseDate(string dateValue, int highlow, int adjustment)
        {
            return parseDate(dateValue, highlow, adjustment, 0);
        }

        private DateTime parseDate(string dateValue, int highlow, int adjustment, int defaultYear)
        {
            DateTime date;
            DateTime dt = MINDATE;
            try
            {
                IFormatProvider culture = new CultureInfo("en-GB", true);
                // Match the regular expression pattern against a text string.
                Match matcher = Regex.Match(dateValue, DATE_PATTERN);
                Group gDay = matcher.Groups[1], gMonth = matcher.Groups[2], gYear = matcher.Groups[3];
                string day = gDay.ToString().Trim(), month = gMonth.ToString().Trim(), year = gYear.ToString().Trim();
                if (day == null) day = "";
                if (month == null) month = "";
                if (year == null) year = "";
                if (day.Length == 0 && month.Length == 0)
                {
                    date = DateTime.ParseExact(dateValue, YEAR, culture);
                    if (highlow == HIGH)
                    {
                        dt = new DateTime(date.Year + adjustment, 12, 31);
                    }
                    else
                    {
                        dt = new DateTime(date.Year + adjustment, 1, 1);
                    }
                }
                else if (day.Length == 0 && year.Length > 0)
                {
                    date = DateTime.ParseExact(dateValue, MONTHYEAR, culture); 
                    dt = new DateTime(date.Year, date.Month, 1);
                    dt.AddMonths(adjustment);
                    if (highlow == HIGH)
                    {
                        // at 1st of month so add 1 month to first of next month
                        dt.AddMonths(1);
                        // then subtract 1 day to be last day of correct month.
                        dt.AddDays(-1);
                    }
                }
                else if (day.Length == 0 && year.Length == 0)
                {
                    date = DateTime.ParseExact(dateValue, MONTH, culture); 
                    dt = new DateTime(defaultYear, date.Month, 1);
                    if (highlow == HIGH)
                    {
                        // at 1st of month so add 1 month to first of next month
                        dt.AddMonths(1);
                        // then subtract 1 day to be last day of correct month.
                        dt.AddDays(-1);
                    }
                }
                else if (year.Length == 0)
                {
                    date = DateTime.ParseExact(dateValue, DAYMONTH, culture); 
                    dt = new DateTime(defaultYear, date.Month, date.Day);
                }
                else if (day.Length > 0 && month.Length > 0 && year.Length > 0)
                {
                    date = DateTime.ParseExact(dateValue, FULL, culture);
                    dt = new DateTime(date.Year, date.Month, date.Day);
                    if ((highlow == HIGH && adjustment != -1) ||
                        (highlow == LOW && adjustment != +1))
                    {
                        // don't bother adding 1 day if date is 
                        // BEF or AFT an exact date
                        dt.AddDays(adjustment);
                    }
                }
                else
                {
                    dt = (highlow == HIGH) ? MAXDATE : MINDATE;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error parsing date: " + dateValue + "\n" + e.Message);
            }
            return dt;
        }

        public DateTime getStartDate()
        {
            return this.startdate;
        }

        public DateTime getEndDate()
        {
            return this.enddate;
        }

        /**
         * @return Returns the datestring.
         */
        public string getDatestring()
        {
            return this.datestring;
        }

        /*
         * @return whether that FactDate is before this FactDate
         */
        public bool isBefore(FactDate that)
        {
            // easy case is extremes whole of date before other
            return (that == null) ? true : enddate < that.startdate;
        }

        /*
         * @return whether that FactDate starts before this FactDate
         */
        public bool startsBefore(FactDate that)
        {
            return (that == null) ? true : startdate < that.startdate;
        }

        /*
         * @return whether that FactDate is after this FactDate
         */
        public bool isAfter(FactDate that)
        {
            // easy case is extremes whole of date after other
            return (that == null) ? true : startdate > that.enddate;
        }

        /*
         * @return whether that FactDate ends after this FactDate
         */
        public bool endsAfter(FactDate that)
        {
            return (that == null) ? true : enddate > that.enddate;
        }

        public bool overlaps(FactDate that)
        {
            // two dates overlap if not entirely before or after
            return (that == null) ? true : !(isBefore(that) || isAfter(that));
        }

        public bool contains(FactDate that)
        {
            return (that == null) ? true :
                (this.startdate < that.startdate && this.enddate > that.enddate);
        }

        public int getMaximumYear(FactDate that)
        {
            Debug.WriteLine("Max: This start date is " + Format(FULL, startdate));
            Debug.WriteLine("Max: That end date is " + (that == null ? "null" : Format(FULL, enddate)));
            int diff = Math.Abs(this.startdate.Year - ((that == null) ? MAXDATE.Year : that.enddate.Year));
            return Math.Min(diff, MAXYEARS);
        }

        public int getMinimumYear(FactDate that)
        {
            Debug.WriteLine("Min: This end date is " + Format(FULL, enddate));
            Debug.WriteLine("Min: That start date is " + (that == null ? "null" : Format(FULL, startdate)));
            int diff = Math.Abs(this.enddate.Year - ((that == null) ? MINDATE.Year : that.startdate.Year));
            return Math.Max(diff, MINYEARS);
        }

        public bool isLongYearSpan()
        {
            int diff = Math.Abs(startdate.Year - enddate.Year);
            return (diff > 5);
        }

        public override bool Equals(Object that) 
        {
            if (that == null || ! (that is FactDate))
                return false;
            FactDate f = (FactDate) that;
            // two FactDates are equal if same datestring or same start and enddates
            return (this.datestring.ToUpper().Equals(f.datestring.ToUpper())) ||
        	       (this.startdate.Equals(f.startdate) && this.enddate.Equals(f.enddate));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(FactDate that)
        {
            if (this.Equals(that))
                return 0;
            else if (this.startdate.Equals(that.startdate))
                return this.enddate.CompareTo(that.enddate);
            else
                return this.startdate.CompareTo(that.startdate);
        }

        public bool isExact()
        {
            return this.startdate.Equals(this.enddate);
        }

        public override string ToString()
        {
            return getDatestring();
        }
    }
}
