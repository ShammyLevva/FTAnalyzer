using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FTAnalyzer
{
    public class FactDate: IComparable<FactDate>
    {
        public static readonly DateTime MINDATE = new DateTime(1, 1, 1);
        public static readonly DateTime MAXDATE = new DateTime(9999, 12, 31);
        public static readonly int MAXYEARS = 110;
        public static readonly int MINYEARS = 0;
        private static readonly int LOW = 0;
        private static readonly int HIGH = 1;
        private static readonly IFormatProvider CULTURE = new CultureInfo("en-GB", true);

        public static readonly string YEAR = "yyyy";
        public static readonly string MONTHYEAR = "MMM yyyy";
        public static readonly string DAYMONTH = "d MMM";
        public static readonly string MONTH = "MMM";
        public static readonly string FULL = "d MMM yyyy";
        public static readonly string DISPLAY = "d MMM yyyy";
        public static readonly string CHECKING = "d MMM";
        public static readonly string DATE_PATTERN = "^(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{0,4})$";
        public static readonly string DOUBLE_DATE_PATTERN = "^(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{0,4})/(\\d{0,2})$";
        public static readonly string POSTFIX = "(\\d{1,2})(?:ST|ND|RD|TH)(.*)";
        public static readonly string BETWEENFIX = "(\\d{4})-(\\d{4})";
        public static readonly string USDATEFIX = "^([A-Za-z]{3}) *(\\d{1,2} )(\\d{4})$";
        public static readonly string SPACEFIX = "^(\\d{1,2}) *([A-Za-z]{3}) *(\\d{0,4})$";
        
        public static readonly FactDate UNKNOWN_DATE = new FactDate("UNKNOWN");

        public enum FactDateType
        {
            BEF, AFT, BET, ABT, UNK, EXT,
        }

        private string datestring;
        private DateTime startdate;
        private DateTime enddate;
        private FactDateType type;
        private bool doubledate = false; // Is a pre 1752 date bet 1 Jan and 25 Mar eg: 1735/36.
        private string factRef = "";

        public FactDate(string str, string factRef = "")
        {
            this.factRef = factRef;
            if (str == null)
                str = string.Empty;
            // remove any commas in date string
            str = FixCommonDateFormats(str);
            this.type = FactDateType.UNK;
            if (str == null || str.Length == 0)
            {
                this.datestring = "UNKNOWN";
            }
            else
            {
                this.datestring = str.ToUpper();
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
            this.type = FactDateType.UNK;
            this.startdate = startdate;
            this.enddate = enddate;
            this.datestring = calculateDatestring();
        }

        public static string Format(string format, DateTime date)
        {
            return string.Format("{0:" + format + "}", date).ToUpper();
        }

        private string FixCommonDateFormats(string str)
        {
            str = str.Trim().ToUpper();
            str = str.Replace(",", string.Empty);
            str = str.Replace("(", string.Empty);
            str = str.Replace(")", string.Empty);
            str = str.Replace("?", string.Empty);
            str = str.Replace(".", " ");
            str = str.Replace("&", " ");
            str = str.Replace("  ", " ");

            str = str.Replace("JANUARY", "JAN");
            str = str.Replace("FEBRUARY", "FEB"); 
            str = str.Replace("MARCH", "MAR"); 
            str = str.Replace("APRIL", "APR");
            str = str.Replace("JUNE", "JUN");
            str = str.Replace("JULY", "JUL");
            str = str.Replace("AUGUST", "AUG");
            str = str.Replace("SEPTEMBER", "SEP");
            str = str.Replace("SEPT", "SEP");
            str = str.Replace("OCTOBER", "OCT");
            str = str.Replace("NOVEMBER", "NOV");
            str = str.Replace("DECEMBER", "DEC");

            str = str.Replace("M01", "JAN");
            str = str.Replace("M02", "FEB");
            str = str.Replace("M03", "MAR");
            str = str.Replace("M04", "APR");
            str = str.Replace("M05", "MAY");
            str = str.Replace("M06", "JUN");
            str = str.Replace("M07", "JUL");
            str = str.Replace("M08", "AUG");
            str = str.Replace("M09", "SEP");
            str = str.Replace("M10", "OCT");
            str = str.Replace("M11", "NOV");
            str = str.Replace("M12", "DEC");

            str = str.Replace("CAL", "ABT");
            str = str.Replace("EST", "ABT");
            str = str.Replace("CIR", "ABT");
            str = str.Replace("PRE", "BEF");
            str = str.Replace("POST", "AFT");

            str = str.Replace("ABOUT", "ABT");
            str = str.Replace("AFTER", "AFT");
            str = str.Replace("BEFORE", "BEF");
            str = str.Replace("BETWEEN", "BET");

            str = str.Replace("QUARTER", "QTR");
            str = str.Replace("MAR QTR", "ABT MAR");
            str = str.Replace("MAR Q ", "ABT MAR ");
            str = str.Replace("JAN FEB MAR", "ABT MAR");
            str = str.Replace("JAN-FEB-MAR", "ABT MAR");
            str = str.Replace("JAN/FEB/MAR", "ABT MAR");
            str = str.Replace("JAN\\FEB\\MAR", "ABT MAR");
            str = str.Replace("Q1", "ABT MAR");
            str = str.Replace("QTR1", "ABT MAR");
            str = str.Replace("QTR 1", "ABT MAR");
            str = str.Replace("JUN QTR", "ABT JUN");
            str = str.Replace("JUN Q ", "ABT JUN ");
            str = str.Replace("APR MAY JUN", "ABT JUN");
            str = str.Replace("APR-MAY-JUN", "ABT JUN");
            str = str.Replace("APR/MAY/JUN", "ABT JUN");
            str = str.Replace("APR\\MAY\\JUN", "ABT JUN");
            str = str.Replace("Q2", "ABT JUN");
            str = str.Replace("QTR2", "ABT JUN");
            str = str.Replace("QTR 2", "ABT JUN");
            str = str.Replace("SEP QTR", "ABT SEP");
            str = str.Replace("SEP Q ", "ABT SEP ");
            str = str.Replace("JUL AUG SEP", "ABT SEP");
            str = str.Replace("JUL-AUG-SEP", "ABT SEP");
            str = str.Replace("JUL/AUG/SEP", "ABT SEP");
            str = str.Replace("JUL\\AUG\\SEP", "ABT SEP");
            str = str.Replace("Q3", "ABT SEP");
            str = str.Replace("QTR3", "ABT SEP");
            str = str.Replace("QTR 3", "ABT SEP");
            str = str.Replace("DEC QTR", "ABT DEC");
            str = str.Replace("DEC Q ", "ABT DEC ");
            str = str.Replace("OCT NOV DEC", "ABT DEC");
            str = str.Replace("OCT-NOV-DEC", "ABT DEC");
            str = str.Replace("OCT/NOV/DEC", "ABT DEC");
            str = str.Replace("OCT\\NOV\\DEC", "ABT DEC");
            str = str.Replace("Q4", "ABT DEC");
            str = str.Replace("QTR4", "ABT DEC");
            str = str.Replace("QTR 4", "ABT DEC");
            
            str = str.Replace("ABT ABT", "ABT"); // fix any ABT X QTR's that will have been changed to ABT ABT

            if (str.StartsWith("FROM"))
            {
                if (str.IndexOf("TO") > 0)
                    str = str.Replace("FROM", "BET").Replace("TO", "AND");
                else
                    str = str.Replace("FROM", "AFT");
            }

            Match matcher = Regex.Match(str, POSTFIX);
            if (matcher.Success)
            {
                string result = matcher.Groups[1].ToString() + matcher.Groups[2].ToString();
                return result.Trim();
            }
            matcher = Regex.Match(str, BETWEENFIX);
            if (matcher.Success)
            {
                string result = "BET " + matcher.Groups[1].ToString() + " AND " + matcher.Groups[2].ToString();
                return result.Trim();
            }
            matcher = Regex.Match(str, USDATEFIX);
            if (matcher.Success)
            {
                string result = matcher.Groups[2].ToString() + matcher.Groups[1].ToString() + " " + matcher.Groups[3].ToString();
                return result.Trim();
            }
            matcher = Regex.Match(str, SPACEFIX);
            if (matcher.Success)
            {
                string result = matcher.Groups[1].ToString() + " " + matcher.Groups[2].ToString() + " " + matcher.Groups[3].ToString();
                return result.Trim();
            }
            return str.Trim();
        }

        #region Process Dates

        public FactDate addYears(int years)
        {
            DateTime start = new DateTime(startdate.Year, startdate.Month, startdate.Day);
            DateTime end = new DateTime(enddate.Year, enddate.Month, enddate.Day);
            start = start.AddYears(years);
            end = end.AddYears(years);
            if (end > MAXDATE)
                end = MAXDATE;
            return new FactDate(start, end);
        }

        public FactDate subtractMonths(int months)
        {
            DateTime start = new DateTime(startdate.Year, startdate.Month, startdate.Day);
            DateTime end = new DateTime(enddate.Year, enddate.Month, enddate.Day);
            if (startdate != MINDATE)
                start = start.AddMonths(-months);
            else
                start = MINDATE;
            end = end.AddMonths(-months);
            if (start < MINDATE)
                start = MINDATE;
            return new FactDate(start, end);
        }

        private string calculateDatestring()
        {
            string check;
            StringBuilder output = new StringBuilder();
            if (startdate == MINDATE)
            {
                if (enddate == MAXDATE)
                    return "UNKNOWN";
                else
                {
                    type = FactDateType.BEF;
                    output.Append("BEF ");
                }
            }
            else
            {
                check = Format(CHECKING, startdate);
                if (enddate == MAXDATE)
                {
                    type = FactDateType.AFT;
                    output.Append("AFT ");
                }
                else if (startdate == enddate)
                {
                    type = FactDateType.EXT;
                }
                else
                {
                    type = FactDateType.BET;
                    output.Append("BET ");
                }
                if (check.Equals("01 JAN"))
                    output.Append(Format(YEAR, startdate));
                else
                    output.Append(Format(DISPLAY, startdate));
                if (type == FactDateType.BET)
                    output.Append(" AND ");
            }
            if (enddate != MAXDATE && enddate != startdate)
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
                    type = FactDateType.BEF;
                    enddate = parseDate(dateValue, HIGH, -1);
                }
                else if (processDate.StartsWith("AFT"))
                {
                    type = FactDateType.AFT;
                    startdate = parseDate(dateValue, LOW, +1);
                }
                else if (processDate.StartsWith("ABT"))
                {
                    type = FactDateType.ABT;
                    if (processDate.StartsWith("ABT MAR") || processDate.StartsWith("ABT JUN")
                         || processDate.StartsWith("ABT SEP") || processDate.StartsWith("ABT DEC"))
                    {
                        // quarter dates
                        startdate = parseDate(dateValue, LOW, -2);
                    }
                    else
                    {
                        startdate = parseDate(dateValue, LOW, -1);
                    }
                    enddate = parseDate(dateValue, HIGH, 0);
                }
                else if (processDate.StartsWith("BET"))
                {
                    type = FactDateType.BET;
                    int andpos = processDate.IndexOf(" AND ");
                    if (andpos == -1)
                    {
                        throw new Exception("Invalid BETween date no AND found");
                    }
                    else
                    {
                        string fromdate = processDate.Substring(4, andpos - 4);
                        string todate = processDate.Substring(andpos + 5);
                        if (fromdate.Length < 3)
                            fromdate = fromdate + processDate.Substring(andpos + 7);
                        else if (fromdate.Length == 3)
                            fromdate = "01 " + fromdate + processDate.Substring(andpos + 8);
                        else if (fromdate.Length == 4)
                            fromdate = "01 JAN " + fromdate;
                        else if (fromdate.Length < 7 && fromdate.IndexOf(" ") > 0)
                            fromdate = fromdate + processDate.Substring(andpos + 11);
                        startdate = parseDate(fromdate, LOW, 0, enddate.Year);
                        enddate = parseDate(todate, HIGH, 0);
                    }
                }
                else
                {
                    type = FactDateType.EXT;
                    dateValue = processDate;
                    startdate = parseDate(dateValue, LOW, 0, 1);
                    enddate = parseDate(dateValue, HIGH, 0, 9999); // have upper default year as 9999 if no year in date
                }
            }
            catch (Exception e)
            {
                FamilyTree.Instance.XmlErrorBox.AppendText("Error parsing date '" + processDate + "' error message was : " + e.Message + "\n");
            }
        }

        private DateTime parseDate(string dateValue, int highlow, int adjustment)
        {
            return parseDate(dateValue, highlow, adjustment, 1);
        }

        private DateTime parseDate(string dateValue, int highlow, int adjustment, int defaultYear)
        {
            DateTime date;
            Group gDay, gMonth, gYear, gDouble;
            DateTime dt = MINDATE;
            if (dateValue == string.Empty)
                return highlow == HIGH ? MAXDATE : MINDATE;
            try
            {
                // Match the regular expression pattern against a text string.
                Match matcher = Regex.Match(dateValue, DATE_PATTERN);
                if (matcher.Success)
                {
                    gDay = matcher.Groups[1];
                    gMonth = matcher.Groups[2];
                    gYear = matcher.Groups[3];
                    gDouble = null;
                }
                else
                {   // Try matching double date pattern
                    matcher = Regex.Match(dateValue, DOUBLE_DATE_PATTERN);
                    if (matcher.Success)
                    {
                        gDay = matcher.Groups[1];
                        gMonth = matcher.Groups[2];
                        gYear = matcher.Groups[3];
                        gDouble = matcher.Groups[4];
                        if (dateValue.Length > 3)
                            dateValue = dateValue.Substring(0, dateValue.Length - gDouble.ToString().Length - 1); // remove the trailing / and 1 or 2 digits
                    }
                    else
                        throw new Exception("Unrecognised date format for : " + dateValue);
                }
                // Now process matched string - if gDouble is not null we have a double date to check
                string day = gDay.ToString().Trim(), month = gMonth.ToString().Trim(), year = gYear.ToString().Trim();
                if (day == null) day = "";
                if (month == null) month = "";
                if (year == null) year = "";
                if (!validDoubleDate(day, month, year, gDouble))
                    throw new InvalidDoubleDateException();
                if (day.Length == 0 && month.Length == 0)
                {
                    date = DateTime.ParseExact(dateValue, YEAR, CULTURE);
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
                    date = DateTime.ParseExact(dateValue, MONTHYEAR, CULTURE);
                    dt = new DateTime(date.Year, date.Month, 1);
                    dt = dt.AddMonths(adjustment);
                    if (highlow == HIGH)
                    {
                        // at 1st of month so add 1 month to first of next month
                        dt = dt.AddMonths(1);
                        // then subtract 1 day to be last day of correct month.
                        dt = dt.AddDays(-1);
                    }
                }
                else if (day.Length == 0 && year.Length == 0)
                {
                    date = DateTime.ParseExact(dateValue, MONTH, CULTURE);
                    dt = new DateTime(defaultYear, date.Month, 1);
                    if (highlow == HIGH)
                    {
                        // at 1st of month so add 1 month to first of next month
                        dt = dt.AddMonths(1);
                        // then subtract 1 day to be last day of correct month.
                        dt = dt.AddDays(-1);
                    }
                }
                else if (year.Length == 0)
                {
                    date = DateTime.ParseExact(dateValue, DAYMONTH, CULTURE);
                    dt = new DateTime(defaultYear, date.Month, date.Day);
                }
                else if (day.Length > 0 && month.Length > 0 && year.Length > 0)
                {
                    date = DateTime.ParseExact(dateValue, FULL, CULTURE);
                    dt = new DateTime(date.Year, date.Month, date.Day);
                    dt = dt.AddDays(adjustment);
                }
                else
                {
                    dt = (highlow == HIGH) ? MAXDATE : MINDATE;
                }
            }
            catch (System.FormatException e1)
            {
                throw e1;
            }
            catch (Exception e2)
            {
                dt = (highlow == HIGH) ? MAXDATE : MINDATE;
                FamilyTree.Instance.XmlErrorBox.AppendText("Error parsing date '" + dateValue + "' for " + factRef + " error message was : " + e2.Message + "\n");
            }
            return dt;
        }

        private bool validDoubleDate(string day, string month, string year, Group gDouble)
        {
            doubledate = false;   // set property
            if (gDouble == null)  // normal date so its valid double date
                return true;
            // check if valid double date if so set double date to true
            string doubleyear = gDouble.ToString().Trim();
            if (doubleyear.Length == 1 && year.Length >= 2)
                doubleyear = year.Substring(year.Length - 2,1) + doubleyear;
            if (doubleyear == null || doubleyear.Length != 2 || year.Length < 3)
                return false;
            int iYear = Convert.ToInt32(year);
            if (iYear >= 1752)
                return false; // double years are only for pre 1752
            if (month.Length == 3 && month != "JAN" && month != "FEB" && month != "MAR")
                return false; // double years must be pre Mar 25th of year
            if (doubleyear == "00" && year.Substring(2, 2) != "99")
                return false; // check for change of century year
            int iDoubleYear = Convert.ToInt32(year.Substring(0, 2) + doubleyear);
            if (iDoubleYear - iYear != 1)
                return false; // must only be 1 year between double years
            doubledate = true; // passed all checks
            return doubledate;
        }

        #endregion

        #region Properties
        public DateTime StartDate
        {
            get { return this.startdate; }
        }

        public DateTime EndDate
        {
            get { return this.enddate; }
        }

        public string Datestring
        {
            get { return this.datestring; }
        }

        public FactDateType Type
        {
            get { return this.type; }
        }

        public bool DoubleDate
        {
            get { return this.doubledate; }
        }

        public FactDate AverageDate
        {
            get
            {
                if (this.datestring.Equals("UNKNOWN"))
                    return UNKNOWN_DATE;
                if (this.startdate == MINDATE)
                    return new FactDate(this.enddate, this.enddate);
                if (this.enddate == MAXDATE)
                    return new FactDate(this.startdate, this.startdate);
                TimeSpan ts = this.enddate.Subtract(this.startdate);
                double midPointSeconds = ts.TotalSeconds / 2.0;
                DateTime average = this.startdate.AddSeconds(midPointSeconds);
                return new FactDate(average, average);
            }
        }

        #endregion

        #region Tests
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

        public bool isLongYearSpan()
        {
            int diff = Math.Abs(startdate.Year - enddate.Year);
            return (diff > 5);
        }

        public bool isExact()
        {
            return this.startdate.Equals(this.enddate);
        }

        #endregion

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

        public override string ToString()
        {
            if (datestring.StartsWith("BET 1 JAN"))
                return "BET " + datestring.Substring(10);
            else
                return datestring;
        }
    }
}
