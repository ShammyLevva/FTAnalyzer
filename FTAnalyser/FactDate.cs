using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FTAnalyser
{
    class FactDate: IComparable<FactDate>
    {
        public static sealed DateTime MINDATE = new DateTime(1, 0, 1);
        public static sealed DateTime MAXDATE = new DateTime(9999, 11, 31);
        public static int MAXYEARS = 110;
        public static int MINYEARS = 0;

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
                check = CHECKING.format(startdate.ToUniversalTime).ToUpper();
                if (enddate == MAXDATE)
                    output.Append("AFT ");
                else
                {
                    output.Append("BET ");
                    between = true;
                }
                if (check.Equals("01 JAN"))
                    output.Append(YEAR.format(startdate.ToUniversalTime));
                else
                    output.Append(DISPLAY.format(startdate.ToUniversalTime));
                if (between)
                    output.Append(" AND ");
            }
            if (enddate != MAXDATE)
            {
                check = CHECKING.format(enddate.ToUniversalTime).ToUpper();
                if (check.Equals("31 DEC"))
                {
                    // add 1 day to take it to 1st Jan following year
                    // this makes the range of "bef 1900" change to 
                    // "bet xxxx and 1900"
                    output.Append(YEAR.format(this.enddate.ToUniversalTime));
                }
                else
                    output.Append(DISPLAY.format(enddate.ToUniversalTime));
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
                    startdate = parseDate(processDate.Substring(4, andpos), LOW, 0, enddate.get(DateTime.YEAR));
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
            DateTime dt;
            try
            {
                Matcher matcher = DATE_PATTERN.matcher(dateValue);
                matcher.find();
                String day = matcher.group(1), month = matcher.group(2), year = matcher.group(3);
                if (day == null) day = "";
                if (month == null) month = "";
                if (year == null) year = "";
                if (day.Length == 0 && month.Length == 0)
                {
                    date = YEAR.parse(dateValue);
                    if (highlow == HIGH)
                    {
                        dt = new DateTime(dt.Year + adjustment, 12, 31);
                    }
                    else
                    {
                        dt = new DateTime(dt.Year + adjustment, 1, 1);
                    }
                }
                else if (day.Length == 0 && year.Length > 0)
                {
                    date = MONTHYEAR.parse(dateValue);
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
                    date = MONTH.parse(dateValue);
                    dt = new DateTime(defaultYear, date.Month, 1);
                    if (highlow == HIGH)
                    {
                        // at 1st of month so add 1 month to first of next month
                        dt.AddMonths(1);
                        // then subtract 1 day to be last day of correct month.
                        dt.AddDays(-1);
                    }
                }
                else if (year.length() == 0)
                {
                    date = DAYMONTH.parse(dateValue);
                    dt = new DateTime(defaultYear, date.Month, date.Day);
                }
                else if (day.Length > 0 && month.Length > 0 && year.Length > 0)
                {
                    date = FULL.parse(dateValue);
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
            //        System.out.println("Max: This start date is " + FULL.format(this.startdate.ToUniversalTime));
            //        System.out.println("Max: That end date is " + (that == null ? "null" : FULL.format(that.enddate.ToUniversalTime)));
            int diff = DateInterval.yearsBetween(this.startdate, (that == null) ? MAXDATE : that.enddate);
            return Math.Min(diff, MAXYEARS);
        }

        public int getMinimumYear(FactDate that)
        {
            //        System.out.println("Min: This end date is " + FULL.format(this.enddate.ToUniversalTime));
            //        System.out.println("Min: That start date is " + (that == null ? "null" : FULL.format(that.startdate.ToUniversalTime)));
            int diff = DateInterval.yearsBetween(this.enddate, (that == null) ? MINDATE : that.startdate);
            return Math.Max(diff, MINYEARS);
        }

        public bool isLongYearSpan()
        {
            int diff = DateInterval.yearsBetween(startdate, enddate);
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
