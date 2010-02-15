/*
 * Created on 02-Jan-2005
 *
 */
package org.hardgate.familytree.filters;

import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.registrations.DeathRegistration;
import org.hardgate.familytree.registrations.MarriageRegistration;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *  
 */
public class BirthFilter extends DateFilter {

    public static DateFilter ONLINE_MARRIAGE_FILTER = new BirthFilter("BET 01 JAN 1855 AND 31 DEC 1929");
    public static DateFilter ONLINE_DEATH_FILTER = new BirthFilter("BET 01 JAN 1855 AND 31 DEC 1954");

    public static DateFilter GROS_MARRIAGE_FILTER = new BirthFilter("AFT 31 DEC 1929");
    public static DateFilter GROS_DEATH_FILTER = new BirthFilter("AFT 31 DEC 1954");

    public BirthFilter(FactDate cutoff) {
        super(cutoff);
    }
    
    public BirthFilter (String date) {
        super(date);
    }

    public boolean select (Registration r) {
        FactDate d = r.getRegistrationDate();
        FactDate b = r.getIndividual().getBirthDate();
        FactDate old = b;
        if (r instanceof DeathRegistration)
            old = b.addYears(FactDate.MAXYEARS);
        else if (r instanceof MarriageRegistration)
            old = b.addYears(75); // restrict marriages to under 75 year olds
        return cutoff.overlaps(d) && cutoff.overlaps(old);
    }
}