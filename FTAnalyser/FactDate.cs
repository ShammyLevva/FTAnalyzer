using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FTAnalyser
{
    class FactDate: IComparable<FactDate>
    {
        public static sealed DateTime MINDATE = new DateTime(1, 0, 1);
        public static sealed DateTime MAXDATE = new DateTime(9999, 11, 31);
        public static sealed int MAXYEARS = 110;
        public static sealed int MINYEARS = 0;
        private static sealed int LOW =  0;
        private static sealed int HIGH = 1;

        private static sealed String YEAR = "{0:yyyy}";
        private static sealed String MONTHYEAR = "{0:MMM yyyy}";
        private static sealed String DAYMONTH = "{0:dd MMM}";
        private static sealed String MONTH = "{0:MMM}";
        private static sealed String FULL = "{0:dd MMM yyyy}";
        private static sealed String DISPLAY = "{0:d MMM yyyy}";
        private static sealed String CHECKING = "{0:dd MMM}";
        private static sealed String DATE_PATTERN = "(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{0,4})";

        public static sealed FactDate UNKNOWN_DATE = new FactDate("UNKNOWN");
        public static sealed FactDate CENSUS1841 = new FactDate("06 JUN 1841");
        public static sealed FactDate CENSUS1851 = new FactDate("30 MAR 1851");
        public static sealed FactDate CENSUS1861 = new FactDate("07 APR 1861");
        public static sealed FactDate CENSUS1871 = new FactDate("02 APR 1871");
        public static sealed FactDate CENSUS1881 = new FactDate("03 APR 1881");
        public static sealed FactDate CENSUS1891 = new FactDate("05 APR 1891");
        public static sealed FactDate CENSUS1901 = new FactDate("31 MAR 1901");
 
        private String dateString;
        private DateTime startdate;
        private DateTime enddate;

        public FactDate(String dateString)
        {
            if (dateString == null || dateString.Length == 0)
            {
                this.dateString = "UNKNOWN";
            }
            else
            {
                this.dateString = dateString.ToUpper();
            }
            startdate = MINDATE;
            enddate = MAXDATE;
            if (!this.dateString.Equals("UNKNOWN"))
            {
                processDate(this.dateString);
            }
        }

        public FactDate(DateTime startdate, DateTime enddate)
        {
            this.startdate = startdate;
            this.enddate = enddate;
            this.dateString = calculateDateString();
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

        private String calculateDateString()
        {
            String check;
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
                check = String.Format(CHECKING, startdate).ToUpper();
                if (enddate == MAXDATE)
                    output.Append("AFT ");
                else
                {
                    output.Append("BET ");
                    between = true;
                }
                if (check.Equals("01 JAN"))
                    output.Append(String.Format(YEAR, startdate).ToUpper());
                else
                    output.Append(String.Format(DISPLAY, startdate).ToUpper());
                if (between)
                    output.Append(" AND ");
            }
            if (enddate != MAXDATE)
            {
                check = String.Format(CHECKING, enddate).ToUpper();
                if (check.Equals("31 DEC"))
                {
                    // add 1 day to take it to 1st Jan following year
                    // this makes the range of "bef 1900" change to 
                    // "bet xxxx and 1900"
                    output.Append(String.Format(YEAR, enddate).ToUpper());
                }
                else
                    output.Append(String.Format(DISPLAY, enddate).ToUpper());
            }
            return output.ToString().ToUpper();
        }

        private void processDate(String processDate)
        {
            // takes dateString and works out start and end dates 
            // prefixes are BEF, AFT, BET and nothing
            // dates are "YYYY" or "MMM YYYY" or "DD MMM YYYY"
            try
            {
                String dateValue = processDate.Substring(4);
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

        private DateTime parseDate(String dateValue, int highlow, int adjustment)
        {
            return parseDate(dateValue, highlow, adjustment, 0);
        }

        private DateTime parseDate(String dateValue, int highlow, int adjustment, int defaultYear)
        {
            DateTime date;
            DateTime dt = MINDATE;
            try
            {
                IFormatProvider culture = new CultureInfo("en-GB", true);
                Regex r = new Regex(DATE_PATTERN, RegexOptions.IgnoreCase);
                // Match the regular expression pattern against a text string.
                Match matcher = r.Match(dateValue);
                Group gDay = matcher.Groups[1], gMonth = matcher.Groups[2], gYear = matcher.Groups[3];
                String day = gDay.ToString(), month = gMonth.ToString(), year = gYear.ToString();
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
         * @return Returns the dateString.
         */
        public String getDateString()
        {
            return this.dateString;
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
            Debug.WriteLine("Max: This start date is " + String.Format(FULL, startdate));
            Debug.WriteLine("Max: That end date is " + (that == null ? "null" : String.Format(FULL, enddate)));
            int diff = Math.Abs(this.startdate.Year - ((that == null) ? MAXDATE.Year : that.enddate.Year));
            return Math.Min(diff, MAXYEARS);
        }

        public int getMinimumYear(FactDate that)
        {
            Debug.WriteLine("Min: This end date is " + String.Format(FULL, enddate));
            Debug.WriteLine("Min: That start date is " + (that == null ? "null" : String.Format(FULL, startdate)));
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
            return (this.dateString.ToUpper().Equals(f.dateString.ToUpper())) ||
        	       (this.startdate.Equals(f.startdate) && this.enddate.Equals(f.enddate));
        }

        public override int CompareTo(FactDate that)
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

        public override String toString()
        {
            return getDateString();
        }
    }
}
