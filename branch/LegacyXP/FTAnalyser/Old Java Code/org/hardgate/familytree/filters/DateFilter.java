/*
 * Created on 02-Jan-2005
 *
 */
package org.hardgate.familytree.filters;

import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *  
 */
public class DateFilter implements RegistrationFilter {

    protected FactDate cutoff;

    public static DateFilter POST_1855_FILTER = new DateFilter("AFT 31 DEC 1854");
    public static DateFilter POST_1837_FILTER = new DateFilter("AFT 30 JUN 1837");

    public static DateFilter PRE_1855_FILTER = new DateFilter("BEF 01 JAN 1855");
    public static DateFilter PRE_1837_FILTER = new DateFilter("BEF 01 JUL 1837");
    
    public static DateFilter ONLINE_BIRTH_FILTER = new DateFilter("BET 01 JAN 1855 AND 31 DEC 1904");
    
    public static DateFilter GROS_BIRTH_FILTER = new DateFilter("AFT 31 DEC 1904");

    public DateFilter (FactDate cutoff) {
        this.cutoff = cutoff;
    }

    public DateFilter (String date) {
        this.cutoff = new FactDate(date);
    }

    public boolean select (Registration r) {
        FactDate d = r.getRegistrationDate();
        return cutoff.overlaps(d);
    }
}