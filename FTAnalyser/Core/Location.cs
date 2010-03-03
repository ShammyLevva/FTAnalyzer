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

        private string location;
        internal string country;
        internal string region;
        internal string parish;
        internal string address;
        internal string place;
        internal string parishID;
        private int level;
        
        public Location() {
            this.location = "";
            this.country = "";
            this.region = "";
            this.parish = "";
            this.address = "";
            this.place = "";
            this.parishID = null;
        }

        public Location(string location) : this() {
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

        private void fixCities()
        {
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

        #region Properties

        public string Address {
            get { return address; }
            set { this.address = value; }
        }

        public string Country {
            get { return country; }
            set { this.country = value; }
        }

        public string Parish {
            get { return parish; }
            set { this.parish = value; }
        }

        public string Place {
            get { return place; }
            set { this.place = value; }
        }

        public string Region {
            get { return region; }
            set { this.region = value; }
        }
        
        public int Level {
            get { return level; }
        }

	    public string ParishID {
            get { return parishID; }
        }

        #endregion

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
        
        public virtual int CompareTo (Location that, int level) {
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