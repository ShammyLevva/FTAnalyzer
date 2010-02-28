/*
 * Created on 02-Jan-2005
 *
 */
package org.hardgate.familytree.filters;

import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.Location;
import org.hardgate.familytree.registrations.Registration;


/**
 * @author db
 *
 */
public class LocationFilter implements RegistrationFilter {

    private String searchString;
    private int level;
    
    public static final LocationFilter SCOTLAND_FILTER =
        	new LocationFilter("Scotland", Location.COUNTRY);
    
    public static final LocationFilter ENGLAND_FILTER =
    	new LocationFilter("England", Location.COUNTRY);

    public LocationFilter (String searchString, int level) {
        this.searchString = searchString.toLowerCase();
        this.level = level; 
    }
    
    public boolean select (Registration r) {
        // If the registration location is not blank and does not
        // contain the search string, then we stop. Otherwise if
        // the registration location is blank, we search all
        // of the facts about this registration.
        Location l = new Location(r.getRegistrationLocation());
        if (!l.isBlank()) {
            return l.matches(searchString, level);
        }
        
        boolean allLocationsBlank = true;
        for (Fact f : r.getAllFacts()) {
            l = new Location(f.getLocation());
            if (l.matches(searchString, level))
                return true;
            if (! l.isBlank())
                allLocationsBlank = false;
        }
        return allLocationsBlank;
    }

}
