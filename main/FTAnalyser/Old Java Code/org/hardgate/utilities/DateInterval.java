/*
 * Created on 01-Jan-2005
 *  
 */
package org.hardgate.utilities;

import java.util.Calendar;
 
public class DateInterval {
    public static final long MILLIS_PER_DAY = 1000 * 60 * 60 * 24;
 
    public static int daysBetween(Calendar fromCal, Calendar toCal) {
		//make copies because we're going to rollback
		fromCal = (Calendar) fromCal.clone();
		toCal = (Calendar) toCal.clone();
		 
		rollBackToMidnight(fromCal);
		rollBackToMidnight(toCal);
		 
		long fromMillis = fromCal.getTimeInMillis();
		long toMillis = toCal.getTimeInMillis();
		long diffMillis = toMillis-fromMillis;
		long diffDays = diffMillis / MILLIS_PER_DAY;
		long remainder = diffMillis % MILLIS_PER_DAY;
		if (Math.abs(remainder) > MILLIS_PER_DAY/2) {
		if (diffDays > 0)
		    ++diffDays;
		else
		    --diffDays;
		}
		return (int) diffDays;
    }

    public static int yearsBetween(Calendar fromCal, Calendar toCal) {
        return daysBetween(fromCal,toCal)/365;
    }
    
    public static void rollBackToMidnight(Calendar cal) {
		cal.set(Calendar.HOUR_OF_DAY, 0);
		cal.set(Calendar.MINUTE, 0);
		cal.set(Calendar.SECOND, 0);
		cal.set(Calendar.MILLISECOND, 0);
    }
}