using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace FTAnalyzer
{
    public class FactDate : IComparable<FactDate>
    {
        public static readonly DateTime MINDATE = new DateTime(1, 1, 1);
        public static readonly DateTime MAXDATE = new DateTime(9999, 12, 31);
        public static readonly int MAXYEARS = 110;
        public static readonly int MINYEARS = 0;
        private static readonly int LOW = 0;
        private static readonly int HIGH = 1;
        private static readonly IFormatProvider CULTURE = new CultureInfo("en-GB", true);

        private static readonly string YEAR = "yyyy";
        private static readonly string EARLYYEAR = "yyy";
        private static readonly string MONTHYEAR = "MMM yyyy";
        private static readonly string MONTHYEAREARLY = "MMM yyy";
        private static readonly string DAYMONTH = "d MMM";
        private static readonly string MONTH = "MMM";
        public static readonly string FULL = "d MMM yyyy";
        private static readonly string FULLEARLY = "d MMM yyy";
        private static readonly string DISPLAY = "d MMM yyyy";
        private static readonly string CHECKING = "d MMM";
        private static readonly string DATE_PATTERN = "^(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{0,4})$";
        private static readonly string INTERPRETED_DATE_PATTERN = "^INT (\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{0,4}) .*$";
        private static readonly string EARLY_DATE_PATTERN = "^(\\d{3})$";
        private static readonly string DOUBLE_DATE_PATTERN = "^(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{0,4})/(\\d{0,2})$";
        private static readonly string DOUBLE_DATE_PATTERN2 = "^(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{4})/(\\d{4})$";
        private static readonly string POSTFIX = "(\\d{1,2})(?:ST|ND|RD|TH)(.*)";
        private static readonly string BETWEENFIX = "(\\d{4}) *- *(\\d{4})";
        private static readonly string BETWEENFIX2 = "([A-Za-z]{0,3}) *(\\d{4}) *- *([A-Za-z]{0,3}) *(\\d{4})";
        private static readonly string BETWEENFIX3 = "(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{4}) *- *(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{4})";
        private static readonly string BETWEENFIX4 = "(\\d{1,2}) *- *(\\d{1,2} )?([A-Za-z]{0,3}) *(\\d{4})";
        private static readonly string BETWEENFIX5 = "(\\d{1,2} )?([A-Za-z]{0,3}) *- *(\\d{1,2} )?([A-Za-z]{0,3}) *(\\d{4})";
        private static readonly string USDATEFIX = "^([A-Za-z]{3}) *(\\d{1,2} )(\\d{4})$";
        private static readonly string SPACEFIX = "^(\\d{1,2}) *([A-Za-z]{3}) *(\\d{0,4})$";

        public static readonly FactDate UNKNOWN_DATE = new FactDate("UNKNOWN");
        public static readonly FactDate MARRIAGE_LESS_THAN_13 = new FactDate("1600");

        public enum FactDateType
        {
            BEF, AFT, BET, ABT, UNK, EXT,
        }

        public string DateString { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public FactDateType DateType { get; private set; }
        private string OriginalString;
        private string DoubleDateError;

        public bool DoubleDate { get; private set; } // Is a pre 1752 date bet 1 Jan and 25 Mar eg: 1735/36.
        private int yearfix;

        public FactDate(string str, string factRef = "")
        {
            this.DoubleDate = false;
            if (str == null)
                str = string.Empty;
            this.OriginalString = str;
            // remove any commas in date string
            this.yearfix = 0;
            str = FixCommonDateFormats(str);
            this.DateType = FactDateType.UNK;
            if (str == null || str.Length == 0)
                this.DateString = "UNKNOWN";
            else
                this.DateString = str.ToUpper();
            StartDate = MINDATE;
            EndDate = MAXDATE;
            if (!this.DateString.Equals("UNKNOWN"))
                ProcessDate(this.DateString, factRef);
        }

        public FactDate(DateTime startdate, DateTime enddate)
        {
            this.DateType = FactDateType.UNK;
            this.StartDate = startdate;
            this.EndDate = enddate;
            this.DateString = CalculateDateString();
            this.OriginalString = string.Empty;
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
            str = str.Replace("&", " AND ");
            str = str.Replace(" / ", "/");
            str = str.Replace("   ", " ");
            str = str.Replace("  ", " ");
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
            str = str.Replace("BTW", "BET");
            str = str.Replace("UNTIL", "TO");

            str = str.Replace("QUARTER", "QTR");
            str = str.Replace("MAR QTR", "ABT MAR");
            str = str.Replace("MAR Q ", "ABT MAR ");
            str = str.Replace("JAN FEB MAR", "ABT MAR");
            str = str.Replace("JAN-FEB-MAR", "ABT MAR");
            str = str.Replace("JAN/FEB/MAR", "ABT MAR");
            str = str.Replace("JAN\\FEB\\MAR", "ABT MAR");
            str = str.Replace("Q1", "ABT MAR");
            str = str.Replace("1Q", "ABT MAR");
            str = str.Replace("QTR1", "ABT MAR");
            str = str.Replace("QTR 1 ", "ABT MAR ");
            str = str.Replace("JUN QTR", "ABT JUN");
            str = str.Replace("JUN Q ", "ABT JUN ");
            str = str.Replace("APR MAY JUN", "ABT JUN");
            str = str.Replace("APR-MAY-JUN", "ABT JUN");
            str = str.Replace("APR/MAY/JUN", "ABT JUN");
            str = str.Replace("APR\\MAY\\JUN", "ABT JUN");
            str = str.Replace("Q2", "ABT JUN");
            str = str.Replace("2Q", "ABT JUN");
            str = str.Replace("QTR2", "ABT JUN");
            str = str.Replace("QTR 2 ", "ABT JUN ");
            str = str.Replace("SEP QTR", "ABT SEP");
            str = str.Replace("SEP Q ", "ABT SEP ");
            str = str.Replace("JUL AUG SEP", "ABT SEP");
            str = str.Replace("JUL-AUG-SEP", "ABT SEP");
            str = str.Replace("JUL/AUG/SEP", "ABT SEP");
            str = str.Replace("JUL\\AUG\\SEP", "ABT SEP");
            str = str.Replace("Q3", "ABT SEP");
            str = str.Replace("3Q", "ABT SEP");
            str = str.Replace("QTR3", "ABT SEP");
            str = str.Replace("QTR 3 ", "ABT SEP ");
            str = str.Replace("DEC QTR", "ABT DEC");
            str = str.Replace("DEC Q ", "ABT DEC ");
            str = str.Replace("OCT NOV DEC", "ABT DEC");
            str = str.Replace("OCT-NOV-DEC", "ABT DEC");
            str = str.Replace("OCT/NOV/DEC", "ABT DEC");
            str = str.Replace("OCT\\NOV\\DEC", "ABT DEC");
            str = str.Replace("Q4", "ABT DEC");
            str = str.Replace("4Q", "ABT DEC");
            str = str.Replace("QTR4", "ABT DEC");
            str = str.Replace("QTR 4 ", "ABT DEC ");

            str = str.Replace("ABT ABT", "ABT"); // fix any ABT X QTR's that will have been changed to ABT ABT

            if(str.IndexOf("TO") > 1)
            {  // contains TO but doesn't start with TO
                if (!str.StartsWith("FROM"))
                    str = "FROM " + str;
            }
            if (str.StartsWith("FROM"))
            {
                if (str.IndexOf("TO") > 0)
                    str = str.Replace("FROM", "BET").Replace("TO", "AND");
                else
                {
                    str = str.Replace("FROM", "AFT"); // year will be one out
                    yearfix = -1;
                }
            }
            if (str.StartsWith("TO"))
            {
                str = str.Replace("TO", "BEF"); // year will be one out
                yearfix = +1;
            }
            if (str.StartsWith(">"))
                str = str.Replace(">", "AFT ");
            if (str.StartsWith("<"))
                str = str.Replace("<", "BEF ");
            if (str.StartsWith("C1") || str.StartsWith("C2") || str.StartsWith("C 1") || str.StartsWith("C 2"))
                str = "ABT " + str.Substring(1);
            str = str.Replace("  ", " "); // fix issue if > or < or Cxxx has already got a space
            Match matcher;
            if (str.StartsWith("INT")) // Interpreted date but we can discard <<Date_Phrase>>
            {
                matcher = Regex.Match(str, INTERPRETED_DATE_PATTERN);
                if (matcher.Success)
                {
                    string result = matcher.Groups[1].ToString() + matcher.Groups[2].ToString() + " " + matcher.Groups[3].ToString();
                    return result.Trim();
                }
            }
            matcher = Regex.Match(str, POSTFIX);
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
            matcher = Regex.Match(str, BETWEENFIX2);
            if (matcher.Success)
            {
                string result = "BET " + matcher.Groups[1].ToString() + " " + matcher.Groups[2].ToString() + " AND " + matcher.Groups[3].ToString() + " " + matcher.Groups[4].ToString();
                return result.Trim();
            }
            matcher = Regex.Match(str, BETWEENFIX3);
            if (matcher.Success)
            {
                string result = "BET " + matcher.Groups[1].ToString() + matcher.Groups[2].ToString() + " " + matcher.Groups[3].ToString() + " AND " + matcher.Groups[4].ToString() + matcher.Groups[5].ToString() + " " + matcher.Groups[6].ToString();
                return result.Trim();
            }
            matcher = Regex.Match(str, BETWEENFIX4);
            if (matcher.Success)
            {
                string result = "BET " + matcher.Groups[1].ToString() + " " + matcher.Groups[3].ToString() + " " + matcher.Groups[4].ToString() + " AND " + matcher.Groups[2].ToString() + matcher.Groups[3].ToString() + " " + matcher.Groups[4].ToString();
                return result.Trim();
            }
            matcher = Regex.Match(str, BETWEENFIX5);
            if (matcher.Success)
            {
                string result = "BET " + matcher.Groups[1].ToString() + matcher.Groups[2].ToString() + " " + matcher.Groups[5].ToString() + " AND " + matcher.Groups[3].ToString() + matcher.Groups[4].ToString() + " " + matcher.Groups[5].ToString();
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

        public FactDate AddYears(int years)
        {
            DateTime start = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);
            DateTime end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);
            start = start.AddYears(years);
            end = end.AddYears(years);
            if (end > MAXDATE)
                end = MAXDATE;
            return new FactDate(start, end);
        }

        public FactDate SubtractMonths(int months)
        {
            DateTime start = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day);
            DateTime end = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day);
            if (StartDate != MINDATE)
                start = start.AddMonths(-months);
            else
                start = MINDATE;
            end = end.AddMonths(-months);
            if (start < MINDATE)
                start = MINDATE;
            return new FactDate(start, end);
        }

        private string CalculateDateString()
        {
            string check;
            StringBuilder output = new StringBuilder();
            if (StartDate == MINDATE)
            {
                if (EndDate == MAXDATE)
                    return "UNKNOWN";
                else
                {
                    DateType = FactDateType.BEF;
                    output.Append("BEF ");
                }
            }
            else
            {
                check = Format(CHECKING, StartDate);
                if (EndDate == MAXDATE)
                {
                    DateType = FactDateType.AFT;
                    output.Append("AFT ");
                }
                else if (StartDate == EndDate)
                {
                    DateType = FactDateType.EXT;
                }
                else
                {
                    DateType = FactDateType.BET;
                    output.Append("BET ");
                }
                if (check.Equals("01 JAN"))
                    output.Append(Format(YEAR, StartDate));
                else
                    output.Append(Format(DISPLAY, StartDate));
                if (DateType == FactDateType.BET)
                    output.Append(" AND ");
            }
            if (EndDate != MAXDATE && EndDate != StartDate)
            {
                check = Format(CHECKING, EndDate);
                if (check.Equals("31 DEC"))
                {
                    // add 1 day to take it to 1st Jan following year
                    // this makes the range of "bef 1900" change to 
                    // "bet xxxx and 1900"
                    output.Append(Format(YEAR, EndDate));
                }
                else
                    output.Append(Format(DISPLAY, EndDate));
            }
            return output.ToString().ToUpper();
        }

        private void ProcessDate(string processDate, string factRef)
        {
            // takes datestring and works out start and end dates 
            // prefixes are BEF, AFT, BET and nothing
            // dates are "YYYY" or "MMM YYYY" or "DD MMM YYYY"
            try
            {
                string dateValue;
                if (processDate.Length >= 4)
                    dateValue = processDate.Substring(4);
                else
                    dateValue = processDate;
                if (processDate.StartsWith("BEF"))
                {
                    DateType = FactDateType.BEF;
                    EndDate = ParseDate(dateValue, HIGH, -1 + yearfix);
                }
                else if (processDate.StartsWith("AFT"))
                {
                    DateType = FactDateType.AFT;
                    StartDate = ParseDate(dateValue, LOW, +1 + yearfix);
                }
                else if (processDate.StartsWith("ABT"))
                {
                    DateType = FactDateType.ABT;
                    if (processDate.StartsWith("ABT MAR") || processDate.StartsWith("ABT JUN")
                         || processDate.StartsWith("ABT SEP") || processDate.StartsWith("ABT DEC"))
                    {
                        // quarter dates
                        StartDate = ParseDate(dateValue, LOW, -3);
                    }
                    else
                    {
                        StartDate = ParseDate(dateValue, LOW, -1);
                    }
                    EndDate = ParseDate(dateValue, HIGH, 0);
                }
                else if (processDate.StartsWith("BET"))
                {
                    string fromdate;
                    string todate;
                    DateType = FactDateType.BET;
                    int pos = processDate.IndexOf(" AND ");
                    if (pos == -1)
                    {
                        pos = processDate.IndexOf("-");
                        if (pos == -1)
                            throw new Exception("Invalid BETween date no AND found");
                        fromdate = processDate.Substring(4, pos - 4);
                        todate = processDate.Substring(pos + 1);
                        pos = pos - 4; // from to code in next block expects to jump 5 chars not 1.
                    }
                    else
                    {
                        fromdate = processDate.Substring(4, pos - 4);
                        todate = processDate.Substring(pos + 5);
                    }
                    if (fromdate.Length < 3)
                        fromdate = fromdate + processDate.Substring(pos + 7);
                    else if (fromdate.Length == 3)
                        fromdate = "01 " + fromdate + processDate.Substring(pos + 8);
                    else if (fromdate.Length == 4)
                        fromdate = "01 JAN " + fromdate;
                    else if (fromdate.Length < 7 && fromdate.IndexOf(" ") > 0)
                        fromdate = fromdate + processDate.Substring(pos + 11);
                    StartDate = ParseDate(fromdate, LOW, 0, EndDate.Year);
                    EndDate = ParseDate(todate, HIGH, 0);
                }
                else
                {
                    DateType = FactDateType.EXT;
                    dateValue = processDate;
                    StartDate = ParseDate(dateValue, LOW, 0, 1);
                    EndDate = ParseDate(dateValue, HIGH, 0, 9999); // have upper default year as 9999 if no year in date
                }
            }
            catch (Exception e)
            {
                FamilyTree.Instance.XmlErrorBox.AppendText("Error parsing date '" + OriginalString + "' for " + factRef + "' error message was : " + e.Message + "\n");
            }
        }

        private DateTime ParseDate(string dateValue, int highlow, int adjustment)
        {
            return ParseDate(dateValue, highlow, adjustment, 1);
        }

        private DateTime ParseDate(string dateValue, int highlow, int adjustment, int defaultYear)
        {
            DateTime date;
            Group gDay, gMonth, gYear, gDouble;
            DateTime dt = MINDATE;
            dateValue = dateValue.Trim();
            if (dateValue == string.Empty)
                return highlow == HIGH ? MAXDATE : MINDATE;
            try
            {
                // Match the regular expression pattern against a text string.
                Match matcher = Regex.Match(dateValue, DATE_PATTERN);
                Match matcher2 = Regex.Match(dateValue, EARLY_DATE_PATTERN);
                if (matcher2.Success)
                {  // first check match vs 
                    gDay = null;
                    gMonth = null;
                    gYear = matcher2.Groups[1];
                    gDouble = null;
                }
                else if (matcher.Success)
                {
                    gDay = matcher.Groups[1];
                    gMonth = matcher.Groups[2];
                    gYear = matcher.Groups[3];
                    gDouble = null;
                }
                else
                {   // Try matching double date pattern
                    matcher = Regex.Match(dateValue, DOUBLE_DATE_PATTERN);
                    matcher2 = Regex.Match(dateValue, DOUBLE_DATE_PATTERN2);
                    if (matcher.Success)
                    {
                        gDay = matcher.Groups[1];
                        gMonth = matcher.Groups[2];
                        gYear = matcher.Groups[3];
                        gDouble = matcher.Groups[4];
                        if (dateValue.Length > 3)
                            dateValue = dateValue.Substring(0, dateValue.Length - gDouble.ToString().Length - 1); // remove the trailing / and 1 or 2 digits
                    }
                    else if (matcher2.Success)
                    {
                        gDay = matcher2.Groups[1];
                        gMonth = matcher2.Groups[2];
                        gYear = matcher2.Groups[3];
                        gDouble = matcher2.Groups[4];
                        if (dateValue.Length > 5)
                            dateValue = dateValue.Substring(0, dateValue.Length - 5); // remove the trailing / and 4 digits
                    }
                    else
                        throw new Exception("Unrecognised date format for : " + dateValue);
                }
                // Now process matched string - if gDouble is not null we have a double date to check
                string day = gDay == null ? string.Empty : gDay.ToString().Trim();
                string month = gMonth == null ? string.Empty : gMonth.ToString().Trim();
                string year = gYear == null ? string.Empty : gYear.ToString().Trim();

                if (!IsValidDoubleDate(day, month, year, gDouble))
                    throw new InvalidDoubleDateException(DoubleDateError);
                if (day.Length == 0 && month.Length == 0)
                {
                    if (year.Length == 4)
                        date = DateTime.ParseExact(dateValue, YEAR, CULTURE);
                    else
                        date = DateTime.ParseExact(dateValue, EARLYYEAR, CULTURE);
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
                    if (!DateTime.TryParseExact(dateValue, MONTHYEAR, CULTURE, DateTimeStyles.NoCurrentDateDefault, out date))
                        DateTime.TryParseExact(dateValue, MONTHYEAREARLY, CULTURE, DateTimeStyles.NoCurrentDateDefault, out date);
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
                    if (!DateTime.TryParseExact(dateValue, FULL, CULTURE, DateTimeStyles.NoCurrentDateDefault, out date))
                        DateTime.TryParseExact(dateValue, FULLEARLY, CULTURE, DateTimeStyles.NoCurrentDateDefault, out date);
                    dt = new DateTime(date.Year, date.Month, date.Day);
                    dt = dt.AddDays(adjustment);
                }
                else
                {
                    dt = (highlow == HIGH) ? MAXDATE : MINDATE;
                }
                if (gDouble != null)
                    dt = dt.AddYears(1); // use upper year for double dates
            }
            catch (System.FormatException)
            {
                throw;
            }
            catch (Exception)
            {
                dt = (highlow == HIGH) ? MAXDATE : MINDATE;
                throw;
            }
            return dt;
        }

        private bool IsValidDoubleDate(string day, string month, string year, Group gDouble)
        {
            DoubleDate = false;   // set property
            DoubleDateError = string.Empty;
            if (gDouble == null)  // normal date so its valid double date
                return true;
            // check if valid double date if so set double date to true
            string doubleyear = gDouble.ToString().Trim();
            if (doubleyear.Length == 4)
                doubleyear = doubleyear.Substring(2);
            if (doubleyear.Length == 1 && year.Length >= 2)
                doubleyear = year.Substring(year.Length - 2, 1) + doubleyear;
            if (doubleyear == null || (doubleyear.Length != 2 && doubleyear.Length != 4) || year.Length < 3)
            {
                DoubleDateError = "Year part of double date is an invalid length.";
                return false;
            }
            int iYear = Convert.ToInt32(year);
            if (iYear >= 1752)
            {
                DoubleDateError = "Double dates are only valid prior to 1752 (for most GB countries and colonies).";
                return false; // double years are only for pre 1752
            }
            if (month.Length == 3 && month != "JAN" && month != "FEB" && month != "MAR")
            {
                DoubleDateError = "Double dates years are only valid for Jan-Mar of a year.";
                return false; // double years must be pre Mar 25th of year
            }
            if (doubleyear == "00" && year.Substring(2, 2) != "99")
            {
                DoubleDateError = "Double date year should be 99/00 if it crosses century.";
                return false; // check for change of century year
            }
            int iDoubleYear = doubleyear == "00" ?
                Convert.ToInt32((Convert.ToInt32(year.Substring(0, 2)) + 1).ToString() + doubleyear) :
                Convert.ToInt32(year.Substring(0, 2) + doubleyear);
            if (iDoubleYear - iYear != 1)
            {
                DoubleDateError = "Double date years must be for adjacent years.";
                return false; // must only be 1 year between double years
            }
            DoubleDate = true; // passed all checks
            return DoubleDate;
        }

        #endregion

        #region Properties

        public FactDate AverageDate
        {
            get
            {
                if (this.DateString.Equals("UNKNOWN"))
                    return UNKNOWN_DATE;
                if (this.StartDate == MINDATE)
                    return new FactDate(this.EndDate, this.EndDate);
                if (this.EndDate == MAXDATE)
                    return new FactDate(this.StartDate, this.StartDate);
                TimeSpan ts = this.EndDate.Subtract(this.StartDate);
                double midPointSeconds = ts.TotalSeconds / 2.0;
                DateTime average = this.StartDate.AddSeconds(midPointSeconds);
                return new FactDate(average, average);
            }
        }

        #endregion

        #region Logical operations
        /*
         * @return whether that FactDate is before this FactDate
         */
        public bool IsBefore(FactDate that)
        {
            if (!this.DoubleDate && that != null && that.DoubleDate)
                return EndDate < that.StartDate.AddYears(1);
            // easy case is extremes whole of date before other
            return (that == null) ? true : EndDate < that.StartDate;
        }

        /*
         * @return whether that FactDate starts before this FactDate
         */
        public bool StartsBefore(FactDate that)
        {
            if (!this.DoubleDate && that != null && that.DoubleDate)
                return StartDate < that.StartDate.AddYears(1);
            return (that == null) ? true : StartDate < that.StartDate;
        }

        /*
         * @return whether that FactDate is after this FactDate
         */
        public bool IsAfter(FactDate that)
        {
            if (this.DoubleDate && that != null && !that.DoubleDate)
                return StartDate.AddYears(1) > that.EndDate;
            // easy case is extremes whole of date after other
            return (that == null) ? true : StartDate > that.EndDate;
        }

        /*
         * @return whether that FactDate ends after this FactDate
         */
        public bool EndsAfter(FactDate that)
        {
            if (this.DoubleDate && that != null && !that.DoubleDate)
                return EndDate.AddYears(1) > that.EndDate;
            return (that == null) ? true : EndDate > that.EndDate;
        }

        public bool Overlaps(FactDate that)
        {
            // two dates overlap if not entirely before or after
            return (that == null) ? true : !(IsBefore(that) || IsAfter(that));
        }

        public bool IsNotBEForeOrAFTer
        {
            get { return StartDate != MINDATE && EndDate != MAXDATE; }
        }

        public bool YearMatches(FactDate that)
        {
            if (that == null ||
                that.StartDate.Year != that.EndDate.Year ||
                this.StartDate.Year != this.EndDate.Year) return false;
            // both this & that have exact years now return whether this and that match
            return this.StartDate.Year == that.StartDate.Year;
        }

        public bool Contains(FactDate that)
        {
            return (that == null) ? true :
                (this.StartDate < that.StartDate && this.EndDate > that.EndDate);
        }

        public bool IsLongYearSpan
        {
            get
            {
                int diff = Math.Abs(StartDate.Year - EndDate.Year);
                return (diff > 5);
            }
        }

        public bool IsExact
        {
            get { return this.StartDate.Equals(this.EndDate); }
        }

        public bool IsKnown
        {
            get { return !this.Equals(UNKNOWN_DATE); }
        }

        public int BestYear
        {
            get
            {
                if (!IsKnown)
                    return 0;
                if (StartDate == MINDATE)
                    return EndDate.Year;
                if (EndDate == MAXDATE)
                    return StartDate.Year;
                return StartDate.Year + (int)((EndDate.Year - StartDate.Year) / 2);
            }
        }

        public double Distance(FactDate when)
        {
            double startDiff = ((this.StartDate.Year - when.StartDate.Year) * 12) + (this.StartDate.Month - when.StartDate.Month);
            double endDiff = ((this.EndDate.Year - when.EndDate.Year) * 12) + (this.EndDate.Month - when.EndDate.Month);
            double difference = Math.Sqrt(Math.Pow(startDiff, 2.0) + Math.Pow(endDiff, 2.0));
            return difference;
        }

        public int DateStatus(bool ignoreUnknown)
        {
            // 0 = grey, 1 = red, 2 = orange, 3 = yellow, 4=green
            if (DateType == FactDateType.UNK)
                return ignoreUnknown ? 0 : 1;
            TimeSpan ts = EndDate - StartDate;
            if (ts.Days > 365.25 * 2)
                return 2; // more than 2 years
            else if (ts.Days > 125)
                return 3; // more than 4 months
            else if (ts.Days > 0)
                return 4; // less than 4 months not exact
            else
                return 5; // exact date
        }

        #endregion

        #region Overrides
        public override bool Equals(Object that)
        {
            if (that == null || !(that is FactDate))
                return false;
            FactDate f = (FactDate)that;
            // two FactDates are equal if same datestring or same start and- enddates
            return (this.DateString.ToUpper().Equals(f.DateString.ToUpper())) ||
                   (this.StartDate.Equals(f.StartDate) && this.EndDate.Equals(f.EndDate));
        }

        public static bool operator ==(FactDate a, FactDate b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }


        public static bool operator !=(FactDate a, FactDate b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public int CompareTo(FactDate that)
        {
            if (this.Equals(that))
                return 0;
            else if (this.StartDate.Equals(that.StartDate))
                return this.EndDate.CompareTo(that.EndDate);
            else
                return this.StartDate.CompareTo(that.StartDate);
        }

        public override string ToString()
        {
            if (DateString.StartsWith("BET 1 JAN"))
                return "BET " + DateString.Substring(10);
            else if (DateString.StartsWith("AFT 1 JAN"))
                return "AFT " + DateString.Substring(10);
            else
                return DateString;
        }
        #endregion

    }
}
