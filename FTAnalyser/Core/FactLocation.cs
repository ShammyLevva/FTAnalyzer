using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace FTAnalyzer
{
	public class FactLocation : IComparable<FactLocation>, IDisplayLocation {
		
		public static readonly string SCOTLAND = "Scotland", ENGLAND = "England", CANADA = "Canada", UNITED_STATES = "United States",
			WALES = "Wales", IRELAND = "Ireland", UNITED_KINGDOM = "United Kingdom", NEW_ZEALAND = "New Zealand", AUSTRALIA = "Australia", 
            UNKNOWN_COUNTRY = "Unknown", ENG_WALES = "England & Wales";
		
		public const int UNKNOWN = -1, COUNTRY = 0, REGION = 1, PARISH = 2, ADDRESS = 3, PLACE = 4;

		private string location;
        private string fixedLocation;
        private string sortableLocation;
        internal string country;
		internal string region;
		internal string parish;
		internal string address;
		internal string place;
		internal string parishID;
		internal string regionID;
		private int level;
        private bool knownCountry;

		private List<Individual> individuals;
		private static Dictionary<string, string> COUNTRY_TYPOS = new Dictionary<string, string>();
		private static Dictionary<string, string> REGION_TYPOS = new Dictionary<string, string>();
		private static Dictionary<string, string> COUNTRY_SHIFTS = new Dictionary<string, string>();
		private static Dictionary<string, string> REGION_SHIFTS = new Dictionary<string, string>();
		private static Dictionary<string, string> REGION_IDS = new Dictionary<string, string>();
        private static Dictionary<string, string> FREECEN_LOOKUP = new Dictionary<string, string>();
        private static Dictionary<string, Tuple<string, string>> FINDMYPAST_LOOKUP = new Dictionary<string, Tuple<string,string>>();
		
		static FactLocation() {
			// load conversions from XML file
			string filename = Application.StartupPath + "\\Resources\\FactLocationFixes.xml";
			if (File.Exists(filename))
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(filename);
				//xmlDoc.Validate(something);
				foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/CountryTypos/CountryTypo"))
				{
					string from = n.Attributes["from"].Value;
					string to = n.Attributes["to"].Value;
					if (from != null && from.Length > 0 && to != null && to.Length > 0)
						COUNTRY_TYPOS.Add(from, to);
				}
				foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/RegionTypos/RegionTypo"))
				{
					string from = n.Attributes["from"].Value;
					string to = n.Attributes["to"].Value;
					if (from != null && from.Length > 0 && to != null && to.Length > 0)
						REGION_TYPOS.Add(from, to);
				}
				foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/DemoteCountries/CountryToRegion"))
				{
					string from = n.Attributes["region"].Value;
					string to = n.Attributes["country"].Value;
					if (from != null && from.Length > 0 && to != null && to.Length > 0)
					{
						COUNTRY_SHIFTS.Add(from, to);
						string regionID = n.Attributes["regionID"].Value;
						if (regionID != null && regionID.Length > 0 && !REGION_IDS.ContainsKey(from))
							REGION_IDS.Add(from, regionID);
					}
				}
				foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/DemoteRegions/RegionToParish"))
				{
					string from = n.Attributes["parish"].Value;
					string to = n.Attributes["region"].Value;
					if (from != null && from.Length > 0 && to != null && to.Length > 0)
					{
						REGION_SHIFTS.Add(from, to);
					}
				}
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Lookups/FreeCen/Lookup"))
                {
                    string code = n.Attributes["code"].Value;
                    string county = n.Attributes["county"].Value;
                    if (code != null && code.Length > 0 && county != null && county.Length > 0)
                    {
                        FREECEN_LOOKUP.Add(county, code);
                    }
                }
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Lookups/FindMyPast/Lookup"))
                {
                    string code = n.Attributes["code"].Value;
                    string county = n.Attributes["county"].Value;
                    string country = n.Attributes["country"].Value;
                    if (code != null && code.Length > 0 && county != null && county.Length > 0)
                    {
                        Tuple<string, string> result = new Tuple<string, string>(country, code);
                        FINDMYPAST_LOOKUP.Add(county, result);
                    }
                }
            }
		}
		
		public FactLocation() {
			this.location = "";
            this.fixedLocation = "";
            this.sortableLocation = "";
            this.country = "";
			this.region = "";
			this.parish = "";
			this.address = "";
			this.place = "";
			this.parishID = null;
			this.individuals = new List<Individual>();
            this.knownCountry = false;
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
				//string before = (parish + ", " + region + ", " + country).ToUpper().Trim();
				fixEmptyFields();
				fixCapitalisation();
				fixRegionFullStops();
				fixMultipleSpacesAndAmpersands();
				fixCountryTypos();
				country = fixRegionTypos(country);
				ShiftCountryToRegion();
				region = fixRegionTypos(region);
				ShiftRegionToParish();
				SetRegionID();
                SetFixedLocation();
                SetSortableLocation();
                CheckKnownLocation();
                //string after = (parish + ", " + region + ", " + country).ToUpper().Trim();
                //if (!before.Equals(after))
                //    Console.WriteLine("Debug : '" + before + "'  converted to '" + after + "'");
			}
		}

        private void CheckKnownLocation()
        {
            if (country.Equals(ENGLAND) || country.Equals(SCOTLAND) || country.Equals(WALES) || country.Equals(IRELAND) || 
                country.Equals(UNITED_STATES) || country.Equals(UNITED_KINGDOM) || country.Equals(CANADA) ||
                country.Equals(NEW_ZEALAND) || country.Equals(AUSTRALIA))
                knownCountry = true;
        }

		#region Fix Location string routines
		private void fixEmptyFields()
		{
			// first remove extraneous spaces
			country = country.Trim();
			region = region.Trim();
			parish = parish.Trim();
			address = address.Trim();
			place = place.Trim();

			if (country.Length == 0)
			{
				country = region;
				region = parish;
				parish = address;
				address = place;
				place = "";
			}
			if (region.Length == 0)
			{
				region = parish;
				parish = address;
				address = place;
				place = "";
			}
			if (parish.Length == 0)
			{
				parish = address;
				address = place;
				place = "";
			}
			if (address.Length == 0)
			{
				address = place;
				place = "";
			}
		}

		private void fixCapitalisation()
		{
			if (country.Length > 1)
				country = char.ToUpper(country[0]) + country.Substring(1);
			if (region.Length > 1)
				region = char.ToUpper(region[0]) + region.Substring(1);
			if (parish.Length > 1)
				parish = char.ToUpper(parish[0]) + parish.Substring(1);
			if (address.Length > 1)
				address = char.ToUpper(address[0]) + address.Substring(1);
			if (place.Length > 1)
				place = char.ToUpper(place[0]) + place.Substring(1);
		}

		private void fixRegionFullStops()
		{
			region = region.Replace(".", " ").Trim();
		}

		private void fixMultipleSpacesAndAmpersands()
		{
			while (country.IndexOf("  ") != -1)
				country = country.Replace("  ", " ");
			while (region.IndexOf("  ") != -1)
				region = region.Replace("  ", " ");
			while (parish.IndexOf("  ") != -1)
				parish = parish.Replace("  ", " ");
			while (address.IndexOf("  ") != -1)
				address = address.Replace("  ", " ");
			while (place.IndexOf("  ") != -1)
				place = place.Replace("  ", " ");
			country = country.Replace("&", "and");
			region = region.Replace("&", "and");
			parish = parish.Replace("&", "and");
			address = address.Replace("&", "and");
			place = place.Replace("&", "and");
		}

		private void fixCountryTypos()
		{
			string newCountry = string.Empty;
			COUNTRY_TYPOS.TryGetValue(country, out newCountry);
			if (newCountry != null && newCountry.Length > 0)
				country = newCountry;
		}

		private string fixRegionTypos(string toFix)
		{
			string result = string.Empty;
			REGION_TYPOS.TryGetValue(toFix, out result);
			if (result != null && result.Length > 0)
				return result;
			else
				return toFix;
		}

		private void ShiftCountryToRegion()
		{
			string newCountry = string.Empty;
			COUNTRY_SHIFTS.TryGetValue(country, out newCountry);
			if (newCountry != null && newCountry.Length > 0)
			{
				place = (place + " " + address).Trim();
				address = parish;
				parish = region;
				region = country;
				country = newCountry;
				if (level < PLACE) level++; // we have moved up a level
			}
		}

		private void ShiftRegionToParish()
		{
			string newRegion = string.Empty;
			REGION_SHIFTS.TryGetValue(region, out newRegion);
			if (newRegion != null && newRegion.Length > 0)
			{
				place = (place + " " + address).Trim();
				address = parish;
				parish = region;
				region = newRegion;
				if (level < PLACE) level++; // we have moved up a level
			}
		}

        private void SetFixedLocation()
        {
            fixedLocation = country;
            if (!region.Equals(string.Empty))
                fixedLocation = region + ", " + fixedLocation;
            if (!parish.Equals(string.Empty))
                fixedLocation = parish + ", " + fixedLocation;
            if (!address.Equals(string.Empty))
                fixedLocation = address + ", " + fixedLocation;
            if (!place.Equals(string.Empty))
                fixedLocation = place + ", " + fixedLocation;
        }

        private void SetSortableLocation()
        {
            sortableLocation = country;
            if (!region.Equals(string.Empty))
                sortableLocation = sortableLocation + ", " + region;
            if (!parish.Equals(string.Empty))
                sortableLocation = sortableLocation + ", " + parish;
            if (!address.Equals(string.Empty))
                sortableLocation = sortableLocation + ", " + address;
            if (!place.Equals(string.Empty))
                sortableLocation = sortableLocation + ", " + place;
        }
        
        #endregion

		private void SetRegionID()
		{
			string newRegionID = string.Empty;
			REGION_IDS.TryGetValue(region, out newRegionID);
			if (newRegionID != null && newRegionID.Length > 0)
				this.regionID = newRegionID;
			else
				this.regionID = string.Empty;
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

        public String SortableLocation
        {
            get { return sortableLocation; }
        }

		public string Address {
			get { return address; }
			set { this.address = value; }
		}

		public string AddressNumeric
		{
			get { return FixNumerics(this.address); }
		}

		public string Country
		{
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

		public string PlaceNumeric
		{
			get { return FixNumerics( this.place); }
		}

		public string Region
		{
			get { return region; }
			set { this.region = value; }
		}

		public string RegionID
		{
			get { return regionID; }
		}

		public int Level
		{
			get { return level; }
		}

		public string ParishID {
			get { return parishID; }
		}

        public bool isKnownCountry
        {
            get { return knownCountry; }
        }

        public bool isUnitedKingdom
        {
            get
            {
                return country.Equals(UNITED_KINGDOM) || country.Equals(ENG_WALES) || country.Equals(ENGLAND) || country.Equals(WALES) || country.Equals(SCOTLAND);
            }
        }

        public string CensusCountry
        {
            get
            {
                if (isUnitedKingdom)
                    return UNITED_KINGDOM;
                else if (country.Equals(IRELAND) || country.Equals(UNITED_STATES) || country.Equals(CANADA))
                    return country;
                else
                    return UNKNOWN_COUNTRY;
            }
        }
        
        public string FreeCenCountyCode
        {
            get
            {
                string result;
                FREECEN_LOOKUP.TryGetValue(region, out result);
                if (result == null)
                    result = "all";
                return result;
            }
        }

        public Tuple<string, string> FindMyPastCountyCode
        {
            get
            {
                Tuple<string,string> result;
                FINDMYPAST_LOOKUP.TryGetValue(region, out result);
                return result;
            }
        }

        #endregion

        public bool SupportedLocation(int level)
		{
			if(country == FactLocation.SCOTLAND || country == FactLocation.ENGLAND || country == FactLocation.WALES ||
				country == FactLocation.IRELAND || country == FactLocation.CANADA || country == FactLocation.UNITED_STATES)
			{
				if (level == COUNTRY) return true;
				// check region is valid if so return true
				return true;
			}
			return false;
		}

		private string FixNumerics(string addressField)
		{
			int pos = addressField.IndexOf(" ");
			if (pos > 0 & pos < addressField.Length)
			{
				string number = addressField.Substring(0, pos);
				string name = addressField.Substring(pos + 1);
				Match matcher = Regex.Match(number, "\\d+[A-Za-z½]?");
				if (matcher.Success)
				{
					return name + " - " + number;
				}
			}
			return addressField;
		}

		public FactLocation getLocation(int level) { return getLocation(level, false); }
		public FactLocation getLocation(int level, bool fixNumerics)
		{
			StringBuilder location = new StringBuilder(this.country);
			if (level > COUNTRY && region.Length > 0)
				location.Insert(0, this.region + ", ");
			if (level > REGION && parish.Length > 0)
				location.Insert(0, this.parish + ", ");
			if (level > PARISH && address.Length > 0)
				location.Insert(0, fixNumerics ? FixNumerics(this.address) : this.address + ", ");
			if (level > ADDRESS && place.Length > 0)
				location.Insert(0, fixNumerics ? FixNumerics(this.place) : this.place + ", ");
			return new FactLocation(location.ToString());
		}

        public static FactLocation BestLocation(List<Fact> facts, FactDate when)
        {
            // this returns a Location a person was at for a given period
            FactLocation result = new FactLocation();
            double minDistance = float.MaxValue;
            foreach (Fact f in facts)
            {
                double distance = Math.Sqrt(Math.Pow((double)(f.FactDate.StartDate.Year - when.StartDate.Year), 2.0) +
                    Math.Pow((double)(f.FactDate.EndDate.Year - when.EndDate.Year), 2.0));
                if (distance < minDistance && !f.Location.location.Equals(string.Empty))
                {
                    result = f.Location;
                    minDistance = distance;
                }
            }
            return result;
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
			//return location;
            return fixedLocation;
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