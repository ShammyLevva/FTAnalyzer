using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class Location : IComparable<Location> {
        
	    public static readonly string SCOTLAND = "Scotland", ENGLAND = "England",
			    CANADA = "Canada";
	    public static readonly string ABERDEEN = "Aberdeenshire", 
			    AYR = "Ayrshire", KINCARDINE = "Kincardineshire",
			    LANARK = "Lanarkshire", BANFF = "Banffshire",
			    ANGUS = "Angus", MIDLOTHIAN = "Midlothian", FIFE = "Fife";
    	
        public const int COUNTRY = 0, REGION = 1, PARISH = 2,
    		    ADDRESS = 3, PLACE = 4; 
    	
	    private string country;
        private string region;
        private string parish;
        private string address;
        private string place;
        private string location;
        private string parishID;
        private int level;
        
        public Location() {
            SetupEmptyLocation();
        }

        private void SetupEmptyLocation()
        {
            this.location = "";
            this.country = "";
            this.region = "";
            this.parish = "";
            this.address = "";
            this.place = "";
            this.parishID = null;
        }
/*
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
*/        
        public Location(string location) {
            SetupEmptyLocation();
            if (location != null) {
	            this.location = location;
	            // we need to parse the location string from a little injun to a big injun
	            int comma = this.location.LastIndexOf(",");
	            if (comma > 0) {
	                country = location.Substring(comma + 1).Trim();
	                location = location.Substring(0,comma);
	                comma = location.LastIndexOf(",",comma);
	                if (comma > 0) {
	                    region = location.Substring(comma + 1).Trim();
	                    location = location.Substring(0,comma);
	                    comma = location.LastIndexOf(",",comma);
	                    if (comma > 0) {
	                        parish = location.Substring(comma + 1).Trim();
	                        location = location.Substring(0,comma);
	                        comma = location.LastIndexOf(",",comma);
	                        if (comma > 0) {
	                            address = location.Substring(comma + 1).Trim();
	                            place = location.Substring(0,comma).Trim();
	                            level = PLACE;
	                        } else {
	                            address = location.Trim();
	                            level = ADDRESS;
	                        }
	                    } else {
	                        parish = location.Trim();
	                        level = PARISH;
	                    }
	                } else {
	                    region = location.Trim();
	                    level = REGION;
	                }
	            } else {
	                country = location.Trim();
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
        
        private void shiftRegion(string newRegion) {
            place = (place + " " + address).Trim();
            address = parish;
            parish = region;
            region = newRegion;
            if (level < PLACE) level++; // we have moved up a level
        }
        
        /**
         * @return Returns the address.
         */
        public string getAddress() {
            return address;
        }
        /**
         * @param address The address to set.
         */
        public void setAddress(string address) {
            this.address = address;
        }
        /**
         * @return Returns the country.
         */
        public string getCountry() {
            return country;
        }
        /**
         * @param country The country to set.
         */
        public void setCountry(string country) {
            this.country = country;
        }
        /**
         * @return Returns the parish.
         */
        public string getParish() {
            return parish;
        }
        /**
         * @param parish The parish to set.
         */
        public void setParish(string parish) {
            this.parish = parish;
        }
        /**
         * @return Returns the place.
         */
        public string getPlace() {
            return place;
        }
        /**
         * @param place The place to set.
         */
        public void setPlace(string place) {
            this.place = place;
        }
        /**
         * @return Returns the region.
         */
        public string getRegion() {
            return region;
        }
        /**
         * @param region The region to set.
         */
        public void setRegion(string region) {
            this.region = region;
        }
        
        /**
         * @return Returns the level.
         */
        public int getLevel() {
            return level;
        }

	    public string getParishID() {
		    return parishID;
	    }
    	
        public bool isBlank () {
            return this.country.Length == 0;
        }
        
        public bool Matches (string s, int level) {
            switch (level) {
        	    case COUNTRY: return this.country.ToUpper().CompareTo(s.ToUpper()) == 0;
                case REGION:  return this.region.ToUpper().CompareTo(s.ToUpper()) == 0;
                case PARISH:  return this.parish.ToUpper().CompareTo(s.ToUpper()) == 0;
                case ADDRESS: return this.address.ToUpper().CompareTo(s.ToUpper()) == 0;
                case PLACE:   return this.place.ToUpper().CompareTo(s.ToUpper()) == 0;
        	    default:      return false;
            }
        }
        
        public int CompareTo (Location that) {
            return CompareTo (that, PLACE);
        }
        
        public int CompareTo (Location that, int level) {
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
        
        public override string ToString() {
            return location;
        }
        
        public override bool Equals(Object that) {
    	    if(that is Location) {
    		    return this.CompareTo((Location) that) == 0 ? true : false;
    	    } else {
    		    return false;
    	    }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        } 

    }
}