/*
 * Created on 02-Jan-2005
 *
 */
package org.hardgate.familytree.filters;

import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.Location;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class IncompleteDataFilter implements RegistrationFilter {

    private int level;

    public static final IncompleteDataFilter MISSING_DATA_FILTER =
    	new IncompleteDataFilter(Location.COUNTRY);

    public IncompleteDataFilter (int level) {
        this.level = level; 
    }
    
    public IncompleteDataFilter () {
        this.level = Location.ADDRESS;
    }
    
    public boolean select (Registration r) {
        if (r.isCertificatePresent())
            return false;
        FactDate fd = r.getRegistrationDate();
        if (fd == null || ! fd.isExact())
            return true;
        Location l = new Location(r.getRegistrationLocation());
        switch (level) {
        	case Location.COUNTRY : return (l.getCountry().length() == 0);
			case Location.REGION  : return (l.getRegion().length() == 0);
    		case Location.PARISH  : return (l.getParish().length() == 0);
        	case Location.ADDRESS : return (l.getAddress().length() == 0);
        	case Location.PLACE   : return (l.getPlace().length() == 0);
        	default : return true;
        }
    }

}
