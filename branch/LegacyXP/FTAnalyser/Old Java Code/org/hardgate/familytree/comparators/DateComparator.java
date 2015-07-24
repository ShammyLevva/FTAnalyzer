/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.comparators;

import java.util.Comparator;

import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class DateComparator implements Comparator<Registration> {

    public int compare (Registration r1, Registration r2) {
        FactDate d1 = r1.getRegistrationDate();
        FactDate d2 = r2.getRegistrationDate();
        if (d1 == null)  d1 = FactDate.UNKNOWN_DATE;
        if (d2 == null)  d2 = FactDate.UNKNOWN_DATE;
        return d1.compareTo(d2);
    }

}
