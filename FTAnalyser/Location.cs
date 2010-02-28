using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class Location : IComparable<Location> {
        
	    public static readonly String SCOTLAND = "Scotland", ENGLAND = "England",
			    CANADA = "Canada";
	    public static readonly String ABERDEEN = "Aberdeenshire", 
			    AYR = "Ayrshire", KINCARDINE = "Kincardineshire",
			    LANARK = "Lanarkshire", BANFF = "Banffshire",
			    ANGUS = "Angus", MIDLOTHIAN = "Midlothian", FIFE = "Fife";
    	
        public static readonly int COUNTRY = 0, REGION = 1, PARISH = 2,
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
            if (place != null && !place.Equals("")) {
                buildLocation.Append(place);
                buildLocation.Append(", ");
            }
            if (address != null && !address.Equals("")) {
                buildLocation.Append(address);
                buildLocation.Append(", ");
            }
            if (parish != null && !parish.Equals("")) {
                buildLocation.Append(parish);
                buildLocation.Append(", ");
            }
            if (region != null && !region.Equals("")) {
                buildLocation.Append(region);
                buildLocation.Append(", ");
            }
            if (country != null && !country.Equals("")) {
                buildLocation.Append(country);
            }
            this.location = buildLocation.ToString();
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
            if (region.Equals("Aberdeen"))
                shiftRegion(ABERDEEN);
            else if (region.Equals("Ayr"))
                shiftRegion(AYR);
            else if (region.Equals("Glasgow"))
        	    shiftRegion(LANARK);
            else if (region.Equals("Dundee"))
        	    shiftRegion(ANGUS);
            else if (region.Equals("Edinburgh"))
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
            return this.country.Length == 0;
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
            int res = this.country.CompareTo(that.country);
            if (res == 0 && level > COUNTRY) {
                res = this.region.CompareTo(that.region);
                if (res == 0 && level > REGION) {
                    res = this.parish.CompareTo(that.parish);
                    if (res == 0 && level > PARISH) {
                        res = this.address.CompareTo(that.address);
                        if (res == 0 && level > ADDRESS) {
                            res = this.place.CompareTo(that.place);
                        }
                    }
                }
            }
            return res;
        }
        
        public String toString() {
            return location;
        }
        
        public boolean Equals(Object that) {
    	    if(that is Location) {
    		    return this.compareTo((Location) that) == 0 ? true : false;
    	    } else {
    		    return false;
    	    }
        }
    }
}