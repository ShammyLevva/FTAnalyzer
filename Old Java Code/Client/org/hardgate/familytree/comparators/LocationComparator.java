/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.comparators;

import java.util.Comparator;

import org.hardgate.familytree.core.Location;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class LocationComparator implements Comparator<Registration> {

    private int level;
    
    public LocationComparator() {
        level = Location.PLACE;
    }
    
    public LocationComparator(int level) {
        this.level = level;
    }
    
    public int compare (Registration r1, Registration r2) {
        Location l1 = new Location(r1.getRegistrationLocation());
        Location l2 = new Location(r2.getRegistrationLocation());
        return l1.compareTo(l2, level);
    }
}
