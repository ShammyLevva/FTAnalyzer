/*
 * Created on 01-Oct-2004
 *
 */
package org.hardgate.familytree.core;

import java.io.Serializable;
import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.GregorianCalendar;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import org.apache.log4j.Logger;
import org.hardgate.utilities.DateInterval;

/**
 * @author A-Bisset
 *
 */
public class FactDate implements Serializable, Comparable<FactDate> {
    
	private static final long serialVersionUID = 0;
	private static Logger logger = Logger.getLogger(FactDate.class);

    private static final DateFormat YEAR = new SimpleDateFormat("yyyy");
    private static final DateFormat MONTHYEAR = new SimpleDateFormat("MMM yyyy");
    private static final DateFormat DAYMONTH = new SimpleDateFormat("dd MMM");
    private static final DateFormat MONTH = new SimpleDateFormat("MMM");
    private static final DateFormat FULL = new SimpleDateFormat("dd MMM yyyy");
    private static final DateFormat DISPLAY = new SimpleDateFormat("d MMM yyyy");
    private static final DateFormat CHECKING = new SimpleDateFormat("dd MMM");
    private static final int LOW =  0;
    private static final int HIGH = 1;

    private static final Pattern DATE_PATTERN =
    		Pattern.compile("(\\d{0,2} )?([A-Za-z]{0,3}) *(\\d{0,4})");
    
	public static final Calendar MINDATE = new GregorianCalendar(1,0,1);
    public static final Calendar MAXDATE = new GregorianCalendar(9999,11,31);
    public static final int MAXYEARS = 110;
    public static final int MINYEARS = 0;
    
    public static final FactDate UNKNOWN_DATE = new FactDate("UNKNOWN");
    public static final FactDate CENSUS1841 = new FactDate("06 JUN 1841");
    public static final FactDate CENSUS1851 = new FactDate("30 MAR 1851");
    public static final FactDate CENSUS1861 = new FactDate("07 APR 1861");
    public static final FactDate CENSUS1871 = new FactDate("02 APR 1871");
    public static final FactDate CENSUS1881 = new FactDate("03 APR 1881");
    public static final FactDate CENSUS1891 = new FactDate("05 APR 1891");
    public static final FactDate CENSUS1901 = new FactDate("31 MAR 1901");
    
	private String dateString;
    private Calendar startdate;
    private Calendar enddate;
    
    public FactDate(String dateString) {
        // parse the date string and work out a start and end date
        if(dateString == null || dateString.length() == 0) {
            this.dateString = "UNKNOWN";
        } else {
            this.dateString = dateString.toUpperCase();
        }
        startdate = MINDATE;
        enddate = MAXDATE;
        if(!this.dateString.equals("UNKNOWN")) {
            processDate(this.dateString);
        }
       logger.debug("Datestring : " + dateString + 
     		   " became startdate=" + FULL.format(startdate.getTime()) + 
     		   " enddate=" +  FULL.format(enddate.getTime()));
    }
    
    public FactDate (Calendar startdate, Calendar enddate) {
        this.startdate = startdate;
        this.enddate = enddate;
        this.dateString = calculateDateString();
    }
    
    public FactDate addYears(int years) {
        Calendar start = (Calendar) this.startdate.clone();
        Calendar end = (Calendar) this.enddate.clone();
        start.add(Calendar.YEAR, years);
        end.add(Calendar.YEAR, years);
        if (end.after(MAXDATE))
            end = MAXDATE;
        return new FactDate(start, end);
    }
    
    private String calculateDateString() {
        String check;
        boolean between = false;
        StringBuffer output = new StringBuffer();
        if (startdate == MINDATE) {
            if (enddate == MAXDATE)
                return "UNKNOWN";
            else 
                output.append("BEF ");
        } else {
	        check = CHECKING.format(startdate.getTime()).toUpperCase();
	        if (enddate == MAXDATE)
	            output.append("AFT ");
	        else {
	            output.append("BET ");
	            between = true;
	        }
	        if (check.equals("01 JAN"))
	            output.append(YEAR.format(startdate.getTime()));
        	else
	            output.append(DISPLAY.format(startdate.getTime()));
	        if (between)
		        output.append(" AND ");
	    }
        if (enddate != MAXDATE) {
	        check = CHECKING.format(enddate.getTime()).toUpperCase();
	        if (check.equals("31 DEC")) {
	            // add 1 day to take it to 1st Jan following year
	            // this makes the range of "bef 1900" change to 
	            // "bet xxxx and 1900"
	            output.append(YEAR.format(this.enddate.getTime()));
	        } else
	            output.append(DISPLAY.format(enddate.getTime()));
        }
        return output.toString().toUpperCase();
    }
    
    private void processDate(String processDate) {
        // takes dateString and works out start and end dates 
        // prefixes are BEF, AFT, BET and nothing
        // dates are "YYYY" or "MMM YYYY" or "DD MMM YYYY"
        try {
	        String dateValue = processDate.substring(4);
	        if(processDate.startsWith("BEF")) {
	            enddate = parseDate(dateValue, HIGH, -1);
	        } else if(processDate.startsWith("AFT")) {
	            startdate = parseDate(dateValue, LOW, +1);
	        } else if(processDate.startsWith("ABT")) {
	            startdate = parseDate(dateValue, LOW, -1);
	            enddate = parseDate(dateValue, HIGH, +1);
	        } else if(processDate.startsWith("BET")) {
	        	int andpos = processDate.indexOf(" AND ");
	        	enddate = parseDate(processDate.substring(andpos+5), HIGH, 0);
	        	startdate = parseDate(processDate.substring(4,andpos), LOW, 0,
	        			enddate.get(Calendar.YEAR));
	        } else {
	        	dateValue = processDate;
	            startdate = parseDate(dateValue, LOW, 0);
	            enddate = parseDate(dateValue, HIGH, 0);
	        }
        } catch (IndexOutOfBoundsException e) {
            e.printStackTrace();
        }
    }

    private Calendar parseDate(String dateValue, int highlow, int adjustment) {
    	return parseDate(dateValue, highlow, adjustment, 0);
    }
    
    private Calendar parseDate(String dateValue, int highlow, int adjustment, int defaultYear) {
        Date date = null;
        Calendar cal = Calendar.getInstance();
        try {
	    	Matcher matcher = DATE_PATTERN.matcher(dateValue);
	    	matcher.find();
	    	String day = matcher.group(1), month = matcher.group(2), year = matcher.group(3);
	    	if(day == null) day = "";
	    	if(month == null) month = "";
	    	if(year == null) year = "";
	    	if (day.length() == 0 && month.length() == 0) {
	    		date = YEAR.parse(dateValue);
                cal.setTime(date);
                cal.add(Calendar.YEAR, adjustment);
                if(highlow == HIGH) {
                    cal.set(Calendar.MONTH, Calendar.DECEMBER);
                    cal.set(Calendar.DAY_OF_MONTH, 31);
                } else {
                    cal.set(Calendar.DAY_OF_MONTH, 1);
                    cal.set(Calendar.MONTH, Calendar.JANUARY);
                }
	    	} else if (day.length() == 0 && year.length() > 0) {
	    		date = MONTHYEAR.parse(dateValue);
                cal.setTime(date);
                cal.add(Calendar.MONTH, adjustment);
                cal.set(Calendar.DAY_OF_MONTH, 1);
                if(highlow == HIGH) {
                	// at 1st of month so add 1 month to first of next month
                    cal.add(Calendar.MONTH, 1);
                    // then subtract 1 day to be last day of correct month.
                    cal.add(Calendar.DAY_OF_MONTH, -1);
                }
	    	} else if (day.length() == 0 && year.length() == 0) {
	    		date = MONTH.parse(dateValue);
	    		cal.setTime(date);
	    		cal.set(Calendar.YEAR, defaultYear);
                cal.set(Calendar.DAY_OF_MONTH, 1);
                if(highlow == HIGH) {
                	// at 1st of month so add 1 month to first of next month
                    cal.add(Calendar.MONTH, 1);
                    // then subtract 1 day to be last day of correct month.
                    cal.add(Calendar.DAY_OF_MONTH, -1);
                }
	    	} else if (year.length() == 0) {
	    		date = DAYMONTH.parse(dateValue);
	    		cal.setTime(date);
	    		cal.set(Calendar.YEAR, defaultYear);
	    	} else if (day.length() > 0 && month.length() > 0 && year.length() > 0){
	    		date = FULL.parse(dateValue);
	            cal.setTime(date);
	            if ((highlow == HIGH && adjustment != -1) ||
	                (highlow == LOW  && adjustment != +1)) {
	                	// don't bother adding 1 day if date is 
	                	// BEF or AFT an exact date
	                	cal.add(Calendar.DAY_OF_MONTH, adjustment);
	            }
	    	} else {
	            cal = (highlow == HIGH) ? MAXDATE : MINDATE;
	    	}
        } catch (ParseException e) {
        	logger.debug("Error parsing date: " + dateValue + "\n" + e.getMessage());
        }
        return cal;
    }

//    private Calendar parseDate(String dateValue, int highlow, int adjustment) {
//        Date date = null;
//        Calendar cal = Calendar.getInstance();
//        try {
//            date = FULL.parse(dateValue);
//            cal.setTime(date);
//            if ((highlow == HIGH && adjustment != -1) ||
//                (highlow == LOW  && adjustment != +1)) {
//                	// don't bother adding 1 day if date is 
//                	// BEF or AFT an exact date
//                	cal.add(Calendar.DAY_OF_MONTH, adjustment);
//            }
//        } catch (ParseException parseExFull) {
//            try {
//                date = MONTHYEAR.parse(dateValue);
//                cal.setTime(date);
//                cal.add(Calendar.MONTH, adjustment);
//                cal.set(Calendar.DAY_OF_MONTH, 1);
//                if(highlow == HIGH) {
//                	// at 1st of month so add 1 month to first of next month
//                    cal.add(Calendar.MONTH, 1);
//                    // then subtract 1 day to be last day of correct month.
//                    cal.add(Calendar.DAY_OF_MONTH, -1);
//                }
//            } catch (ParseException parseExMonth) {
//                try {
//                    date = YEAR.parse(dateValue);
//                    cal.setTime(date);
//                    cal.add(Calendar.YEAR, adjustment);
//                    if(highlow == HIGH) {
//                        cal.set(Calendar.MONTH, Calendar.DECEMBER);
//                        cal.set(Calendar.DAY_OF_MONTH, 31);
//                    } else {
//                        cal.set(Calendar.DAY_OF_MONTH, 1);
//                        cal.set(Calendar.MONTH, Calendar.JANUARY);
//                    }
//                } catch (ParseException parseExYear) {
//                    if(highlow == HIGH) {
//                        cal = MAXDATE;
//                    } else {
//                        cal = MINDATE;
//                    }
//                }
//            }
//        }
//        return cal;
//    }

    public Calendar getStartDate () {
        return this.startdate;
    }
    
    public Calendar getEndDate () {
        return this.enddate;
    }
    
    /**
     * @return Returns the dateString.
     */
    public String getDateString() {
        return this.dateString;
    }
    
    /*
     * @return whether that FactDate is before this FactDate
     */
    public boolean isBefore(FactDate that) {
        // easy case is extremes whole of date before other
        return (that == null) ? true : this.enddate.before(that.startdate);
    }
    
    /*
     * @return whether that FactDate starts before this FactDate
     */
    public boolean startsBefore(FactDate that) {
        return (that == null) ? true : this.startdate.before(that.startdate);
    }

    /*
     * @return whether that FactDate is after this FactDate
     */
    public boolean isAfter(FactDate that) {
        // easy case is extremes whole of date after other
        return (that == null) ? true : this.startdate.after(that.enddate);
    }
    
    /*
     * @return whether that FactDate ends after this FactDate
     */
    public boolean endsAfter(FactDate that) {
        return (that == null) ? true : this.enddate.after(that.enddate);
    }
    
    public boolean overlaps(FactDate that) {
    	// two dates overlap if not entirely before or after
    	return (that == null) ? true : !(isBefore(that) || isAfter(that));
    }
    
    public boolean contains(FactDate that) {
    	return (that == null) ? true :
    	    (this.startdate.before(that.startdate)
    	            && this.enddate.after(that.enddate));
    }
   
    public int getMaximumYear (FactDate that) {
//        System.out.println("Max: This start date is " + FULL.format(this.startdate.getTime()));
//        System.out.println("Max: That end date is " + (that == null ? "null" : FULL.format(that.enddate.getTime())));
        int diff = DateInterval.yearsBetween(this.startdate, 
                (that == null) ? MAXDATE : that.enddate);
        return Math.min(diff, MAXYEARS);
    }
    
    public int getMinimumYear (FactDate that) {
//        System.out.println("Min: This end date is " + FULL.format(this.enddate.getTime()));
//        System.out.println("Min: That start date is " + (that == null ? "null" : FULL.format(that.startdate.getTime())));
        int diff = DateInterval.yearsBetween(this.enddate,
                (that == null) ? MINDATE : that.startdate);
        return Math.max(diff, MINYEARS);
    }
    
    public boolean isLongYearSpan() {
        int diff = DateInterval.yearsBetween(this.startdate, this.enddate);
        return (diff > 5);
    }
    
    public boolean equals(Object that) {
        if (that == null || ! (that instanceof FactDate))
            return false;
        FactDate f = (FactDate) that;
        // two FactDates are equal if same datestring or same start and enddates
        return (this.dateString.toUpperCase().equals(f.dateString.toUpperCase())) ||
        	   (this.startdate.equals(f.startdate) && this.enddate.equals(f.enddate));
    }

    public int compareTo (FactDate that) {
        if (this.equals(that))
            return 0;
        else if (this.startdate.equals(that.startdate))
            return this.enddate.compareTo(that.enddate);
        else
            return this.startdate.compareTo(that.startdate);
    }
    
    public boolean isExact () {
        return this.startdate.equals(this.enddate);
    }
    
    public String toString() {
        return getDateString();
    }
}
