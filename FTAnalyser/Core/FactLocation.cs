using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class FactLocation : IComparable<FactLocation>, IDisplayLocation {
        
	    public static readonly string SCOTLAND = "Scotland", ENGLAND = "England", CANADA = "Canada", USA = "United States",
            WALES = "Wales", IRELAND = "Ireland", UK = "UK";
	    public static readonly string ABERDEEN = "Aberdeenshire", AYR = "Ayrshire", KINCARDINE = "Kincardineshire",
			    LANARK = "Lanarkshire", BANFF = "Banffshire", ANGUS = "Angus", MIDLOTHIAN = "Midlothian", FIFE = "Fife",
                MIDDLESEX = "Middlesex";
    	
        public const int COUNTRY = 0, REGION = 1, PARISH = 2, ADDRESS = 3, PLACE = 4;

        private string location;
        internal string country;
        internal string region;
        internal string parish;
        internal string address;
        internal string place;
        internal string parishID;
        private int level;

        private List<Individual> individuals;
        private static Dictionary<string, string> CITYSHIFTS = new Dictionary<string,string>();
        private static Dictionary<string, string> COUNTRYFIXES = new Dictionary<string, string>();
        private static Dictionary<string, string> COUNTRYSHIFTS = new Dictionary<string, string>();

        static FactLocation() {
            CITYSHIFTS.Add("Aberdeen", ABERDEEN);
            CITYSHIFTS.Add("Ayr", AYR);
            CITYSHIFTS.Add("Glasgow", LANARK);
            CITYSHIFTS.Add("Dundee", ANGUS);
            CITYSHIFTS.Add("Edinburgh", MIDLOTHIAN);
            CITYSHIFTS.Add("London", MIDDLESEX);

            COUNTRYFIXES.Add("US", USA);
            COUNTRYFIXES.Add("USA", USA);
            
            // Scottish Regions
            COUNTRYSHIFTS.Add("Aberdeenshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Angus", SCOTLAND);
            COUNTRYSHIFTS.Add("Argyll", SCOTLAND);
            COUNTRYSHIFTS.Add("Argyllshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Ayrshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Banff", SCOTLAND);
            COUNTRYSHIFTS.Add("Banffshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Berwick", SCOTLAND);
            COUNTRYSHIFTS.Add("Berwickshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Bute", SCOTLAND);
            COUNTRYSHIFTS.Add("Bute", SCOTLAND);
            COUNTRYSHIFTS.Add("Caithness", SCOTLAND);
            COUNTRYSHIFTS.Add("Clackmannanshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Dumfries", SCOTLAND);
            COUNTRYSHIFTS.Add("Dumfriesshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Dumfries-shire", SCOTLAND);
            COUNTRYSHIFTS.Add("Dumbarton", SCOTLAND);
            COUNTRYSHIFTS.Add("Dumbartonshire", SCOTLAND);
            COUNTRYSHIFTS.Add("East Lothian", SCOTLAND);
            COUNTRYSHIFTS.Add("Fife", SCOTLAND);
            COUNTRYSHIFTS.Add("Fifeshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Inverness", SCOTLAND);
            COUNTRYSHIFTS.Add("Inverness-shire", SCOTLAND);
            COUNTRYSHIFTS.Add("Kincardineshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Kinross", SCOTLAND);
            COUNTRYSHIFTS.Add("Kinross-shire", SCOTLAND);
            COUNTRYSHIFTS.Add("Kirkcudbright", SCOTLAND);
            COUNTRYSHIFTS.Add("Kirkcudbrightshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Lanarkshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Midlothian", SCOTLAND);
            COUNTRYSHIFTS.Add("Moray", SCOTLAND);
            COUNTRYSHIFTS.Add("Moaryshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Nairn", SCOTLAND);
            COUNTRYSHIFTS.Add("Nairnshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Orkney", SCOTLAND);
            COUNTRYSHIFTS.Add("Peebles-shire", SCOTLAND);
            COUNTRYSHIFTS.Add("Perthshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Renfrewshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Ross & Cromarty", SCOTLAND);
            COUNTRYSHIFTS.Add("Ross and Cromarty", SCOTLAND);
            COUNTRYSHIFTS.Add("Roxburgh", SCOTLAND);
            COUNTRYSHIFTS.Add("Selkirk", SCOTLAND);
            COUNTRYSHIFTS.Add("Shetland", SCOTLAND);
            COUNTRYSHIFTS.Add("Zetland", SCOTLAND);
            COUNTRYSHIFTS.Add("Stirlingshire", SCOTLAND);
            COUNTRYSHIFTS.Add("Sutherland", SCOTLAND);
            COUNTRYSHIFTS.Add("West Lothian", SCOTLAND);
            COUNTRYSHIFTS.Add("Wigtonshire", SCOTLAND);
            
            // English Shires & Counties
            COUNTRYSHIFTS.Add("Bedfordshire", ENGLAND);
            COUNTRYSHIFTS.Add("Berkshire", ENGLAND);
            COUNTRYSHIFTS.Add("Buckinghamshire", ENGLAND);
            COUNTRYSHIFTS.Add("Cambridgeshire", ENGLAND);
            COUNTRYSHIFTS.Add("Cheshire", ENGLAND);
            COUNTRYSHIFTS.Add("Cornwall", ENGLAND);
            COUNTRYSHIFTS.Add("Cumberland", ENGLAND);
            COUNTRYSHIFTS.Add("Cumbria", ENGLAND);
            COUNTRYSHIFTS.Add("Derbyshire", ENGLAND);
            COUNTRYSHIFTS.Add("Dorset", ENGLAND);
            COUNTRYSHIFTS.Add("Devon", ENGLAND);
            COUNTRYSHIFTS.Add("Co.Durham", ENGLAND);
            COUNTRYSHIFTS.Add("Durham", ENGLAND);
            COUNTRYSHIFTS.Add("Essex", ENGLAND);
            COUNTRYSHIFTS.Add("Gloucestershire", ENGLAND);
            COUNTRYSHIFTS.Add("Hampshire", ENGLAND);
            COUNTRYSHIFTS.Add("Herefordshire", ENGLAND);
            COUNTRYSHIFTS.Add("Hertfordshire", ENGLAND);
            COUNTRYSHIFTS.Add("Huntingdonshire", ENGLAND);
            COUNTRYSHIFTS.Add("Kent", ENGLAND);
            COUNTRYSHIFTS.Add("Lancashire", ENGLAND);
            COUNTRYSHIFTS.Add("Leicestershire", ENGLAND);
            COUNTRYSHIFTS.Add("Lincolnshire", ENGLAND);
            COUNTRYSHIFTS.Add("Monmouthshire", ENGLAND);
            COUNTRYSHIFTS.Add("Norfolk", ENGLAND);
            COUNTRYSHIFTS.Add("Northamptonshire", ENGLAND);
            COUNTRYSHIFTS.Add("Northumberland", ENGLAND);
            COUNTRYSHIFTS.Add("Nottinghamshire", ENGLAND);
            COUNTRYSHIFTS.Add("Oxfordshire", ENGLAND);
            COUNTRYSHIFTS.Add("Rutland", ENGLAND);
            COUNTRYSHIFTS.Add("Shropshire", ENGLAND);
            COUNTRYSHIFTS.Add("Somerset", ENGLAND);
            COUNTRYSHIFTS.Add("Staffordshire", ENGLAND);
            COUNTRYSHIFTS.Add("Suffolk", ENGLAND);
            COUNTRYSHIFTS.Add("Sussex", ENGLAND);
            COUNTRYSHIFTS.Add("Surrey", ENGLAND);
            COUNTRYSHIFTS.Add("Warwickshire", ENGLAND);
            COUNTRYSHIFTS.Add("Westmorland", ENGLAND);
            COUNTRYSHIFTS.Add("Westmorlandshire", ENGLAND);
            COUNTRYSHIFTS.Add("Wiltshire", ENGLAND);
            COUNTRYSHIFTS.Add("Worcestershire", ENGLAND);
            COUNTRYSHIFTS.Add("Yorkshire", ENGLAND);
            COUNTRYSHIFTS.Add("North Yorkshire", ENGLAND);
            COUNTRYSHIFTS.Add("West Yorkshire", ENGLAND);
            COUNTRYSHIFTS.Add("South Yorkshire", ENGLAND);

            // Welsh Regions
            COUNTRYSHIFTS.Add("Anglesey", WALES);
            COUNTRYSHIFTS.Add("Breconshire", WALES);
            COUNTRYSHIFTS.Add("Caernarvon", WALES);
            COUNTRYSHIFTS.Add("Cardiganshire", WALES);
            COUNTRYSHIFTS.Add("Carmarthenshire", WALES);
            COUNTRYSHIFTS.Add("Denbighshire", WALES);
            COUNTRYSHIFTS.Add("Glamorganshire", WALES);
            COUNTRYSHIFTS.Add("Gwynedd", WALES);
            COUNTRYSHIFTS.Add("Merionethshire", WALES);
            COUNTRYSHIFTS.Add("Mid-Glamorgan", WALES);
            COUNTRYSHIFTS.Add("Montgomeryshire", WALES);
            COUNTRYSHIFTS.Add("Pembrokeshire", WALES);
            COUNTRYSHIFTS.Add("Radnorshire", WALES);
            
            // Canadian Provinces
            COUNTRYSHIFTS.Add("Alberta", CANADA);
            COUNTRYSHIFTS.Add("British Columbia", CANADA);
            COUNTRYSHIFTS.Add("BC", CANADA);
            COUNTRYSHIFTS.Add("Manitoba", CANADA);
            COUNTRYSHIFTS.Add("New Brunswick", CANADA);
            COUNTRYSHIFTS.Add("Newfoundland", CANADA);
            COUNTRYSHIFTS.Add("Northwest Territories", CANADA);
            COUNTRYSHIFTS.Add("NWT", CANADA);
            COUNTRYSHIFTS.Add("Nova Scotia", CANADA);
            COUNTRYSHIFTS.Add("NS", CANADA);
            COUNTRYSHIFTS.Add("Ontario", CANADA);
            COUNTRYSHIFTS.Add("ON", CANADA);
            COUNTRYSHIFTS.Add("Prince Edward Island", CANADA);
            COUNTRYSHIFTS.Add("Quebec", CANADA);
            COUNTRYSHIFTS.Add("Saskatchewan", CANADA);
            COUNTRYSHIFTS.Add("Yukon Territory", CANADA);
            
            // US States
            COUNTRYSHIFTS.Add("Alabama", USA);
            COUNTRYSHIFTS.Add("Alaska", USA);
            COUNTRYSHIFTS.Add("Arizona", USA);
            COUNTRYSHIFTS.Add("Arkansas", USA);
            COUNTRYSHIFTS.Add("California", USA);
            COUNTRYSHIFTS.Add("Colorado", USA);
            COUNTRYSHIFTS.Add("Connecticut", USA);
            COUNTRYSHIFTS.Add("South Dakota", USA);
            COUNTRYSHIFTS.Add("Delaware", USA);
            COUNTRYSHIFTS.Add("District of Columbia", USA);
            COUNTRYSHIFTS.Add("Florida", USA);
            COUNTRYSHIFTS.Add("Georgia", USA);
            COUNTRYSHIFTS.Add("Hawaii", USA);
            COUNTRYSHIFTS.Add("Idaho", USA);
            COUNTRYSHIFTS.Add("Illinois", USA);
            COUNTRYSHIFTS.Add("Indiana", USA);
            COUNTRYSHIFTS.Add("Iowa", USA);
            COUNTRYSHIFTS.Add("Kansas", USA);
            COUNTRYSHIFTS.Add("Kentucky", USA);
            COUNTRYSHIFTS.Add("Louisiana", USA);
            COUNTRYSHIFTS.Add("Mississippi", USA);
            COUNTRYSHIFTS.Add("Missouri", USA);
            COUNTRYSHIFTS.Add("Montana", USA);
            COUNTRYSHIFTS.Add("Nebraska", USA);
            COUNTRYSHIFTS.Add("Nevada", USA);
            COUNTRYSHIFTS.Add("New Hampshire", USA);
            COUNTRYSHIFTS.Add("New Jersey", USA);
            COUNTRYSHIFTS.Add("New Mexico", USA);
            COUNTRYSHIFTS.Add("New York", USA);
            COUNTRYSHIFTS.Add("North Carolina", USA);
            COUNTRYSHIFTS.Add("North Dakota", USA);
            COUNTRYSHIFTS.Add("Ohio", USA);
            COUNTRYSHIFTS.Add("Oklahoma", USA);
            COUNTRYSHIFTS.Add("Oregon", USA);
            COUNTRYSHIFTS.Add("Pennsylvania", USA);
            COUNTRYSHIFTS.Add("Rhode Island", USA);
            COUNTRYSHIFTS.Add("South Carolina", USA);
            COUNTRYSHIFTS.Add("South Dakota", USA);
            COUNTRYSHIFTS.Add("Tennessee", USA);
            COUNTRYSHIFTS.Add("Texas", USA);
            COUNTRYSHIFTS.Add("Utah", USA);
            COUNTRYSHIFTS.Add("Vermont", USA);
            COUNTRYSHIFTS.Add("Virginia", USA);
            COUNTRYSHIFTS.Add("Washington", USA);
            COUNTRYSHIFTS.Add("West Virginia", USA);
            COUNTRYSHIFTS.Add("Wisconsin", USA);
            COUNTRYSHIFTS.Add("Wyoming", USA);
            COUNTRYSHIFTS.Add("AL", USA);
            COUNTRYSHIFTS.Add("AK", USA);
            COUNTRYSHIFTS.Add("AZ", USA);
            COUNTRYSHIFTS.Add("AR", USA);
            COUNTRYSHIFTS.Add("CA", USA);
            COUNTRYSHIFTS.Add("CO", USA);
            COUNTRYSHIFTS.Add("CT", USA);
            COUNTRYSHIFTS.Add("DE", USA);
            COUNTRYSHIFTS.Add("DC", USA);
            COUNTRYSHIFTS.Add("FL", USA);
            COUNTRYSHIFTS.Add("GA", USA);
            COUNTRYSHIFTS.Add("HI", USA);
            COUNTRYSHIFTS.Add("ID", USA);
            COUNTRYSHIFTS.Add("IL", USA);
            COUNTRYSHIFTS.Add("IN", USA);
            COUNTRYSHIFTS.Add("IA", USA);
            COUNTRYSHIFTS.Add("KS", USA);
            COUNTRYSHIFTS.Add("KY", USA);
            COUNTRYSHIFTS.Add("LA", USA);
            COUNTRYSHIFTS.Add("ME", USA);
            COUNTRYSHIFTS.Add("MD", USA);
            COUNTRYSHIFTS.Add("MA", USA);
            COUNTRYSHIFTS.Add("MI", USA);
            COUNTRYSHIFTS.Add("MN", USA);
            COUNTRYSHIFTS.Add("MS", USA);
            COUNTRYSHIFTS.Add("MO", USA);
            COUNTRYSHIFTS.Add("MT", USA);
            COUNTRYSHIFTS.Add("NE", USA);
            COUNTRYSHIFTS.Add("NV", USA);
            COUNTRYSHIFTS.Add("NJ", USA);
            COUNTRYSHIFTS.Add("NH", USA);
            COUNTRYSHIFTS.Add("NY", USA);
            COUNTRYSHIFTS.Add("NM", USA);
            COUNTRYSHIFTS.Add("NC", USA);
            COUNTRYSHIFTS.Add("ND", USA);
            COUNTRYSHIFTS.Add("OH", USA);
            COUNTRYSHIFTS.Add("OK", USA);
            COUNTRYSHIFTS.Add("OR", USA);
            COUNTRYSHIFTS.Add("PA", USA);
            COUNTRYSHIFTS.Add("RI", USA);
            COUNTRYSHIFTS.Add("SC", USA);
            COUNTRYSHIFTS.Add("SD", USA);
            COUNTRYSHIFTS.Add("TN", USA);
            COUNTRYSHIFTS.Add("TX", USA);
            COUNTRYSHIFTS.Add("UT", USA);
            COUNTRYSHIFTS.Add("VT", USA);
            COUNTRYSHIFTS.Add("VI", USA);
            COUNTRYSHIFTS.Add("VA", USA);
            COUNTRYSHIFTS.Add("WA", USA);
            COUNTRYSHIFTS.Add("WV", USA);
            COUNTRYSHIFTS.Add("WI", USA);
            COUNTRYSHIFTS.Add("WY", USA);
            
        }
        
        public FactLocation() {
            this.location = "";
            this.country = "";
            this.region = "";
            this.parish = "";
            this.address = "";
            this.place = "";
            this.parishID = null;
            this.individuals = new List<Individual>();
        }

        public FactLocation(string location) : this() {
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
                fixCountries();
            }
        }

        private void fixCountries()
        {
            string newCountry = string.Empty;
            COUNTRYSHIFTS.TryGetValue(country, out newCountry);
            if (newCountry != null && newCountry.Length > 0)
                shiftCountry(newCountry);
        }

        private void fixCities()
        {
            string newRegion = string.Empty;
            CITYSHIFTS.TryGetValue(region, out newRegion);
            if (newRegion != null && newRegion.Length > 0)
                shiftRegion(newRegion);
        }
        
        private void shiftRegion(string newRegion) {
            place = (place + " " + address).Trim();
            address = parish;
            parish = region;
            region = newRegion;
            if (level < PLACE) level++; // we have moved up a level
        }

        private void shiftCountry(string newCountry)
        {
            place = (place + " " + address).Trim();
            address = parish;
            parish = region;
            region = country;
            country = newCountry;
            if (level < PLACE) level++; // we have moved up a level
        }

        public void AddIndividual(Individual ind)
        {
            if (ind != null && !individuals.Contains(ind))
            {
                individuals.Add(ind);
            }
        }

        public List<string> GetSurnames()
        {
            HashSet<string> names = new HashSet<string>();
            foreach (Individual i in individuals)
            {
                names.Add(i.Surname);
            }
            List<string> result = names.ToList();
            result.Sort();
            return result;
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

        public FactLocation getLocation(int level)
        {
            StringBuilder location = new StringBuilder(this.country);
            if (level > COUNTRY && region.Length > 0)
                location.Insert(0, this.region + ", ");
            if (level > REGION && parish.Length > 0)
                location.Insert(0, this.parish + ", ");
            if (level > PARISH && address.Length > 0)
                location.Insert(0, this.address + ", ");
            if (level > ADDRESS && place.Length > 0)
                location.Insert(0, this.place + ", ");
            return new FactLocation(location.ToString());
        }

        public bool isBlank () {
            return this.country.Length == 0;
        }
        
        public bool Matches (string s, int level) {
            switch (level) {
        	    case COUNTRY: return this.country.ToUpper().CompareTo(s.ToUpper()) == 0;
                case REGION: return this.region.ToUpper().CompareTo(s.ToUpper()) == 0;
                case PARISH: return this.parish.ToUpper().CompareTo(s.ToUpper()) == 0;
                case ADDRESS: return this.address.ToUpper().CompareTo(s.ToUpper()) == 0;
                case PLACE: return this.place.ToUpper().CompareTo(s.ToUpper()) == 0;
        	    default:      return false;
            }
        }
        
        public int CompareTo (FactLocation that) {
            return CompareTo (that, PLACE);
        }
        
        public int CompareTo(IDisplayLocation that, int level) 
        {
            return CompareTo((FactLocation)that, level);
        }

        public virtual int CompareTo (FactLocation that, int level) {
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
    	    if(that is FactLocation) {
    		    return this.CompareTo((FactLocation) that) == 0 ? true : false;
    	    } else {
    		    return false;
    	    }
        }

        public bool Equals(FactLocation that, int level)
        {
            return this.CompareTo(that, level) == 0 ? true : false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        } 

    }
}