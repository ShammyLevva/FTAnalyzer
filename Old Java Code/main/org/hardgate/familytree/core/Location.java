/*
 * Created on 17-Dec-2004
 *
 */
package org.hardgate.familytree.core;

import java.io.Serializable;

import org.apache.log4j.Logger;
import org.hardgate.familytree.interfaces.LocationLocal;

/**
 * @author A-Bisset
 *
 */
public class Location implements Serializable, Comparable<Location> {
    
	private static Logger logger = Logger.getLogger(Location.class);
	private static final long serialVersionUID = 0;
	
	public static final String SCOTLAND = "Scotland", ENGLAND = "England",
			CANADA = "Canada";
	public static final String ABERDEEN = "Aberdeenshire", 
			AYR = "Ayrshire", KINCARDINE = "Kincardineshire",
			LANARK = "Lanarkshire", BANFF = "Banffshire",
			ANGUS = "Angus", MIDLOTHIAN = "Midlothian", FIFE = "Fife";
	
    public static final int COUNTRY = 0, REGION = 1, PARISH = 2,
    		ADDRESS = 3, PLACE = 4; 
	
	private String country;
    private String region;
    private String parish;
    private String address;
    private String place;
    private String location;
    private String parishID;
    private int level;
    
    public Location() {
        this.location= "";
        this.country = "";
        this.region  = "";
        this.parish  = "";
        this.address = "";
        this.place   = "";
        this.parishID = null;
    }

    public Location(LocationLocal loc) {
        this.country = loc.getCountry();
        this.region = loc.getRegion();
        this.parish = loc.getParish();
        this.address = loc.getAddress();
        this.place = loc.getPlace();
        this.parishID = loc.getParishID();
        // now build a little endian string
        StringBuilder buildLocation = new StringBuilder();
        if (place != null && !place.equals("")) {
            buildLocation.append(place);
            buildLocation.append(", ");
        }
        if (address != null && !address.equals("")) {
            buildLocation.append(address);
            buildLocation.append(", ");
        }
        if (parish != null && !parish.equals("")) {
            buildLocation.append(parish);
            buildLocation.append(", ");
        }
        if (region != null && !region.equals("")) {
            buildLocation.append(region);
            buildLocation.append(", ");
        }
        if (country != null && !country.equals("")) {
            buildLocation.append(country);
        }
        this.location = buildLocation.toString();
    }
    
    public Location(String location) {
        this();
        if (location != null) {
	        this.location = location;
	        // we need to parse the location string from a little injun to a big injun
	        int comma = this.location.lastIndexOf(",");
	        if (comma > 0) {
	            country = location.substring(comma + 1).trim();
	            location = location.substring(0,comma);
	            comma = location.lastIndexOf(",",comma);
	            if (comma > 0) {
	                region = location.substring(comma + 1).trim();
	                location = location.substring(0,comma);
	                comma = location.lastIndexOf(",",comma);
	                if (comma > 0) {
	                    parish = location.substring(comma + 1).trim();
	                    location = location.substring(0,comma);
	                    comma = location.lastIndexOf(",",comma);
	                    if (comma > 0) {
	                        address = location.substring(comma + 1).trim();
	                        place = location.substring(0,comma).trim();
	                        level = PLACE;
	                    } else {
	                        address = location.trim();
	                        level = ADDRESS;
	                    }
	                } else {
	                    parish = location.trim();
	                    level = PARISH;
	                }
	            } else {
	                region = location.trim();
	                level = REGION;
	            }
	        } else {
	            country = location.trim();
	            level = COUNTRY;
	        }
	        fixCities();
        }
    }
    
    private void fixCities() {
        if (region.equals("Aberdeen"))
            shiftRegion(ABERDEEN);
        else if (region.equals("Ayr"))
            shiftRegion(AYR);
        else if (region.equals("Glasgow"))
        	shiftRegion(LANARK);
        else if (region.equals("Dundee"))
        	shiftRegion(ANGUS);
        else if (region.equals("Edinburgh"))
        	shiftRegion(MIDLOTHIAN);
    }
    
    private void shiftRegion(String newRegion) {
        place = (place + " " + address).trim();
        address = parish;
        parish = region;
        region = newRegion;
        if (level < PLACE) level++; // we have moved up a level
    }
    
    /**
     * @return Returns the address.
     */
    public String getAddress() {
        return address;
    }
    /**
     * @param address The address to set.
     */
    public void setAddress(String address) {
        this.address = address;
    }
    /**
     * @return Returns the country.
     */
    public String getCountry() {
        return country;
    }
    /**
     * @param country The country to set.
     */
    public void setCountry(String country) {
        this.country = country;
    }
    /**
     * @return Returns the parish.
     */
    public String getParish() {
        return parish;
    }
    /**
     * @param parish The parish to set.
     */
    public void setParish(String parish) {
        this.parish = parish;
    }
    /**
     * @return Returns the place.
     */
    public String getPlace() {
        return place;
    }
    /**
     * @param place The place to set.
     */
    public void setPlace(String place) {
        this.place = place;
    }
    /**
     * @return Returns the region.
     */
    public String getRegion() {
        return region;
    }
    /**
     * @param region The region to set.
     */
    public void setRegion(String region) {
        this.region = region;
    }
    
    /**
     * @return Returns the level.
     */
    public int getLevel() {
        return level;
    }

	public String getParishID() {
		return parishID;
	}
	
    public boolean isBlank () {
        return this.country.length() == 0;
    }
    
    public boolean matches (String s, int level) {
        switch (level) {
        	case COUNTRY: return this.country.compareToIgnoreCase(s) == 0;
        	case REGION:  return this.region.compareToIgnoreCase(s) == 0;
        	case PARISH:  return this.parish.compareToIgnoreCase(s) == 0;
        	case ADDRESS: return this.address.compareToIgnoreCase(s) == 0;
        	case PLACE:   return this.place.compareToIgnoreCase(s) == 0;
        	default:      return false;
        }
    }
    
    public int compareTo (Location that) {
        return compareTo (that, PLACE);
    }
    
    public int compareTo (Location that, int level) {
        int res = this.country.compareTo(that.country);
        if (res == 0 && level > COUNTRY) {
            res = this.region.compareTo(that.region);
            if (res == 0 && level > REGION) {
                res = this.parish.compareTo(that.parish);
                if (res == 0 && level > PARISH) {
                    res = this.address.compareTo(that.address);
                    if (res == 0 && level > ADDRESS) {
                        res = this.place.compareTo(that.place);
                    }
                }
            }
        }
        return res;
    }
    
    public String toString() {
        return location;
    }
    
    public boolean equals(Object that) {
    	if(that instanceof Location) {
    		return this.compareTo((Location) that) == 0 ? true : false;
    	} else {
    		return false;
    	}
    }
}
