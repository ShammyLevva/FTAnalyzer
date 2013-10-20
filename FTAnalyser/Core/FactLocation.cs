using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using FTAnalyzer.Utilities;
using FTAnalyzer.Mapping;
using System.Drawing;

namespace FTAnalyzer
{
    public class FactLocation : IComparable<FactLocation>, IDisplayLocation, IDisplayGeocodedLocation
    {
        public const int UNKNOWN = -1, COUNTRY = 0, REGION = 1, SUBREGION = 2, ADDRESS = 3, PLACE = 4;
        public enum Geocode { NOT_SEARCHED = 0, MATCHED = 1, PARTIAL_MATCH = 2, GEDCOM_USER = 3, NO_MATCH = 4 };

        private string location;
        private string fixedLocation;
        public string SortableLocation { get; private set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string Address { get; set; }
        public string Place { get; set; }
        public string ParishID { get; internal set; }
        public int Level { get; private set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Geocode GeocodeStatus { get; set; }
        public string GoogleLocation { get; set; }
        public string GoogleResultType { get; set; }
        public GeoResponse.CResult.CGeometry.CViewPort ViewPort { get; set; }
        private List<Individual> individuals;
        
        public string[] Parts
        {
            get { return new string[] { Country, Region, SubRegion, Address, Place }; }
        }

        private static Dictionary<string, string> COUNTRY_TYPOS = new Dictionary<string, string>();
        private static Dictionary<string, string> REGION_TYPOS = new Dictionary<string, string>();
        private static Dictionary<string, string> COUNTRY_SHIFTS = new Dictionary<string, string>();
        private static Dictionary<string, string> REGION_SHIFTS = new Dictionary<string, string>();
        private static Dictionary<string, string> FREECEN_LOOKUP = new Dictionary<string, string>();
        private static Dictionary<string, Tuple<string, string>> FINDMYPAST_LOOKUP = new Dictionary<string, Tuple<string, string>>();
        private static IDictionary<string, FactLocation> locations;
        
        public static Dictionary<Geocode, string> Geocodes;
        public static FactLocation UNKNOWN_LOCATION;
        public static FactLocation TEMP = new FactLocation();

        static FactLocation()
        {
            SetupGeocodes();
            ResetLocations();
            // load conversions from XML file
            string startPath;
            if (Application.StartupPath.Contains("Common7\\IDE")) // running unit tests
                startPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..");
            else
                startPath = Application.StartupPath;
            string filename = Path.Combine(startPath, @"Resources\FactLocationFixes.xml");
            if (File.Exists(filename))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);
                //xmlDoc.Validate(something);
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/CountryTypos/CountryTypo"))
                {
                    string from = n.Attributes["from"].Value;
                    string to = n.Attributes["to"].Value;
                    if (COUNTRY_TYPOS.ContainsKey(from))
                        Console.WriteLine("Error duplicate country typos :" + from);
                    if (from != null && from.Length > 0 && to != null && to.Length > 0)
                        COUNTRY_TYPOS.Add(from, to);
                }
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/RegionTypos/RegionTypo"))
                {
                    string from = n.Attributes["from"].Value;
                    string to = n.Attributes["to"].Value;
                    if (REGION_TYPOS.ContainsKey(from))
                        Console.WriteLine("Error duplicate region typos :" + from);
                    if (from != null && from.Length > 0 && to != null && to.Length > 0)
                        REGION_TYPOS.Add(from, to);
                }
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/DemoteCountries/CountryToRegion"))
                {
                    string from = n.Attributes["region"].Value;
                    string to = n.Attributes["country"].Value;
                    if (from != null && from.Length > 0 && to != null && to.Length > 0)
                    {
                        if (COUNTRY_SHIFTS.ContainsKey(from))
                            Console.WriteLine("Error duplicate country shift :" + from);
                        COUNTRY_SHIFTS.Add(from, to);
                    }
                }
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/DemoteRegions/RegionToParish"))
                {
                    string from = n.Attributes["parish"].Value;
                    string to = n.Attributes["region"].Value;
                    if (REGION_SHIFTS.ContainsKey(from))
                        Console.WriteLine("Error duplicate region shift :" + from);
                    if (from != null && from.Length > 0 && to != null && to.Length > 0)
                    {
                        REGION_SHIFTS.Add(from, to);
                    }
                }
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Lookups/FreeCen/Lookup"))
                {
                    string code = n.Attributes["code"].Value;
                    string county = n.Attributes["county"].Value;
                    if (FREECEN_LOOKUP.ContainsKey(county))
                        Console.WriteLine("Error duplicate freecen lookup :" + county);
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
                    if (FINDMYPAST_LOOKUP.ContainsKey(county))
                        Console.WriteLine("Error duplicate FindMyPast lookup :" + county);
                    if (code != null && code.Length > 0 && county != null && county.Length > 0)
                    {
                        Tuple<string, string> result = new Tuple<string, string>(country, code);
                        FINDMYPAST_LOOKUP.Add(county, result);
                    }
                }
            }
        }

        private static void SetupGeocodes()
        {
            Geocodes = new Dictionary<Geocode, string>();
            Geocodes.Add(Geocode.NOT_SEARCHED, "Not Searched");
            Geocodes.Add(Geocode.GEDCOM_USER, "GEDCOM/User data");
            Geocodes.Add(Geocode.PARTIAL_MATCH, "Partial Match");
            Geocodes.Add(Geocode.MATCHED, "Matched");
            Geocodes.Add(Geocode.NO_MATCH, "No Match");
        }

        public static FactLocation GetLocation(string place)
        {
            return GetLocation(place, string.Empty, string.Empty, Geocode.NOT_SEARCHED);
        }

        public static FactLocation GetLocation(string place, string latitude, string longitude, Geocode status)
        {
            FactLocation result;
            FactLocation temp;
            if (!locations.TryGetValue(place, out result))
            {
                result = new FactLocation(place, latitude, longitude, status);
                if (locations.TryGetValue(result.ToString(), out temp))
                    return temp;
                else
                {
                    locations.Add(result.ToString(), result);
                    if (result.Level > COUNTRY)
                    {   // recusive call to GetLocation forces create of lower level objects and stores in locations
                        result.GetLocation(result.Level - 1);
                    }
                }
            }
            return result; // should return object that is in list of locations 
        }

        public FactLocation GetLocation(int level) { return GetLocation(level, false); }
        public FactLocation GetLocation(int level, bool fixNumerics)
        {
            StringBuilder location = new StringBuilder(this.Country);
            if (level > COUNTRY && (Region.Length > 0 || Properties.GeneralSettings.Default.AllowEmptyLocations))
                location.Insert(0, this.Region + ", ");
            if (level > REGION && (SubRegion.Length > 0 || Properties.GeneralSettings.Default.AllowEmptyLocations))
                location.Insert(0, this.SubRegion + ", ");
            if (level > SUBREGION && (Address.Length > 0 || Properties.GeneralSettings.Default.AllowEmptyLocations))
                location.Insert(0, fixNumerics ? FixNumerics(this.Address) : this.Address + ", ");
            if (level > ADDRESS && Place.Length > 0)
                location.Insert(0, fixNumerics ? FixNumerics(this.Place) : this.Place + ", ");
            FactLocation newLocation = GetLocation(location.ToString());
            return newLocation;
        }

        public static IEnumerable<FactLocation> AllLocations
        {
            get { return locations.Values; }
        }

        public static void ResetLocations()
        {
            locations = new Dictionary<string, FactLocation>();
            // set unknown location as found so it doesn't keep hassling to be searched
            UNKNOWN_LOCATION = GetLocation(string.Empty, "0.0", "0.0", Geocode.NO_MATCH);
        }

        private FactLocation()
        {
            this.location = string.Empty;
            this.fixedLocation = string.Empty;
            this.SortableLocation = string.Empty;
            this.Country = string.Empty;
            this.Region = string.Empty;
            this.SubRegion = string.Empty;
            this.Address = string.Empty;
            this.Place = string.Empty;
            this.ParishID = null;
            this.individuals = new List<Individual>();
            this.Latitude = 0;
            this.Longitude = 0;
            this.Level = UNKNOWN;
            this.GeocodeStatus = Geocode.NOT_SEARCHED;
            this.GoogleLocation = string.Empty;
            this.GoogleResultType = string.Empty;
            this.ViewPort = new GeoResponse.CResult.CGeometry.CViewPort();
        }

        private FactLocation(string location, string latitude, string longitude, Geocode status)
            : this(location)
        {
            double temp;
            this.Latitude = double.TryParse(latitude, out temp) ? temp : 0;
            this.Longitude = double.TryParse(longitude, out temp) ? temp : 0;
            this.GeocodeStatus = status;
            if (status == Geocode.NOT_SEARCHED && (Latitude != 0 || Longitude != 0))
                status = Geocode.GEDCOM_USER;
        }

        private FactLocation(string location)
            : this()
        {
            if (location != null)
            {
                this.location = location;
                // we need to parse the location string from a little injun to a big injun
                int comma = this.location.LastIndexOf(",");
                if (comma > 0)
                {
                    Country = location.Substring(comma + 1).Trim();
                    location = location.Substring(0, comma);
                    comma = location.LastIndexOf(",", comma);
                    if (comma > 0)
                    {
                        Region = location.Substring(comma + 1).Trim();
                        location = location.Substring(0, comma);
                        comma = location.LastIndexOf(",", comma);
                        if (comma > 0)
                        {
                            SubRegion = location.Substring(comma + 1).Trim();
                            location = location.Substring(0, comma);
                            comma = location.LastIndexOf(",", comma);
                            if (comma > 0)
                            {
                                Address = location.Substring(comma + 1).Trim();
                                Place = location.Substring(0, comma).Trim();
                                Level = PLACE;
                            }
                            else
                            {
                                Address = location.Trim();
                                Level = ADDRESS;
                            }
                        }
                        else
                        {
                            SubRegion = location.Trim();
                            Level = SUBREGION;
                        }
                    }
                    else
                    {
                        Region = location.Trim();
                        Level = REGION;
                    }
                }
                else
                {
                    Country = location.Trim();
                    Level = COUNTRY;
                }
                //string before = (parish + ", " + region + ", " + country).ToUpper().Trim();
                if (!Properties.GeneralSettings.Default.AllowEmptyLocations)
                    FixEmptyFields();
                FixCapitalisation();
                FixRegionFullStops();
                FixCountryFullStops();
                FixMultipleSpacesAmpersandsCommas();
                FixCountryTypos();
                Country = EnhancedTextInfo.ToTitleCase(FixRegionTypos(Country).ToLower());
                ShiftCountryToRegion();
                Region = FixRegionTypos(Region);
                ShiftRegionToParish();
                SetFixedLocation();
                SetSortableLocation();
                //string after = (parish + ", " + region + ", " + country).ToUpper().Trim();
                //if (!before.Equals(after))
                //    Console.WriteLine("Debug : '" + before + "'  converted to '" + after + "'");
            }
        }

        #region Fix Location string routines
        private void FixEmptyFields()
        {
            // first remove extraneous spaces and extraneous commas
            Country = Country.Trim();
            Region = Region.Trim();
            SubRegion = SubRegion.Trim();
            Address = Address.Trim();
            Place = Place.Trim();

            if (Country.Length == 0)
            {
                Country = Region;
                Region = SubRegion;
                SubRegion = Address;
                Address = Place;
                Place = "";
            }
            if (Region.Length == 0)
            {
                Region = SubRegion;
                SubRegion = Address;
                Address = Place;
                Place = "";
            }
            if (SubRegion.Length == 0)
            {
                SubRegion = Address;
                Address = Place;
                Place = "";
            }
            if (Address.Length == 0)
            {
                Address = Place;
                Place = "";
            }
        }

        private void FixCapitalisation()
        {
            if (Country.Length > 1)
                Country = char.ToUpper(Country[0]) + Country.Substring(1);
            if (Region.Length > 1)
                Region = char.ToUpper(Region[0]) + Region.Substring(1);
            if (SubRegion.Length > 1)
                SubRegion = char.ToUpper(SubRegion[0]) + SubRegion.Substring(1);
            if (Address.Length > 1)
                Address = char.ToUpper(Address[0]) + Address.Substring(1);
            if (Place.Length > 1)
                Place = char.ToUpper(Place[0]) + Place.Substring(1);
        }

        private void FixRegionFullStops()
        {
            Region = Region.Replace(".", " ").Trim();
        }

        private void FixCountryFullStops()
        {
            Country = Country.Replace(".", " ").Trim();
        }

        private void FixMultipleSpacesAmpersandsCommas()
        {
            while (Country.IndexOf("  ") != -1)
                Country = Country.Replace("  ", " ");
            while (Region.IndexOf("  ") != -1)
                Region = Region.Replace("  ", " ");
            while (SubRegion.IndexOf("  ") != -1)
                SubRegion = SubRegion.Replace("  ", " ");
            while (Address.IndexOf("  ") != -1)
                Address = Address.Replace("  ", " ");
            while (Place.IndexOf("  ") != -1)
                Place = Place.Replace("  ", " ");
            Country = Country.Replace("&", "and").Replace(",", "").Trim();
            Region = Region.Replace("&", "and").Replace(",", "").Trim();
            SubRegion = SubRegion.Replace("&", "and").Replace(",", "").Trim();
            Address = Address.Replace("&", "and").Replace(",", "").Trim();
            Place = Place.Replace("&", "and").Replace(",", "").Trim();
        }

        private void FixCountryTypos()
        {
            string result = string.Empty;
            COUNTRY_TYPOS.TryGetValue(Country, out result);
            if (result != null && result.Length > 0)
                Country = result;
            else
            {
                string fixCase = EnhancedTextInfo.ToTitleCase(Country.ToLower());
                COUNTRY_TYPOS.TryGetValue(fixCase, out result);
                if (result != null && result.Length > 0)
                    Country = result;
            }
        }

        private string FixRegionTypos(string toFix)
        {
            string result = string.Empty;
            REGION_TYPOS.TryGetValue(toFix, out result);
            if (result != null && result.Length > 0)
                return result;
            else
            {
                string fixCase = EnhancedTextInfo.ToTitleCase(toFix.ToLower());
                REGION_TYPOS.TryGetValue(fixCase, out result);
                if (result != null && result.Length > 0)
                    return result;
                else
                    return toFix;
            }
        }

        private void ShiftCountryToRegion()
        {
            string result = string.Empty;
            COUNTRY_SHIFTS.TryGetValue(Country, out result);
            if (result == null || result.Length == 0)
            {
                string fixCase = EnhancedTextInfo.ToTitleCase(Country.ToLower());
                COUNTRY_SHIFTS.TryGetValue(fixCase, out result);
            }
            if (result != null && result.Length > 0)
            {
                Place = (Place + " " + Address).Trim();
                Address = SubRegion;
                SubRegion = Region;
                Region = Country;
                Country = result;
                if (Level < PLACE) Level++; // we have moved up a level
            }
        }

        private void ShiftRegionToParish()
        {
            string result = string.Empty;
            REGION_SHIFTS.TryGetValue(Region, out result);
            if (result == null || result.Length == 0)
            {
                string fixCase = EnhancedTextInfo.ToTitleCase(Region.ToLower());
                REGION_TYPOS.TryGetValue(fixCase, out result);
            }
            if (result != null && result.Length > 0)
            {
                Place = (Place + " " + Address).Trim();
                Address = SubRegion;
                SubRegion = Region;
                Region = result;
                if (Level < PLACE) Level++; // we have moved up a level
            }
        }

        private void SetFixedLocation()
        {
            fixedLocation = Country;
            if (!Region.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                fixedLocation = Region + ", " + fixedLocation;
            if (!SubRegion.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                fixedLocation = SubRegion + ", " + fixedLocation;
            if (!Address.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                fixedLocation = Address + ", " + fixedLocation;
            if (!Place.Equals(string.Empty))
                fixedLocation = Place + ", " + fixedLocation;
            fixedLocation = TrimLeadingCommas(fixedLocation);
        }

        private void SetSortableLocation()
        {
            SortableLocation = Country;
            if (!Region.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                SortableLocation = SortableLocation + ", " + Region;
            if (!SubRegion.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                SortableLocation = SortableLocation + ", " + SubRegion;
            if (!Address.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                SortableLocation = SortableLocation + ", " + Address;
            if (!Place.Equals(string.Empty))
                SortableLocation = SortableLocation + ", " + Place;
            SortableLocation = TrimLeadingCommas(SortableLocation);
        }

        private string TrimLeadingCommas(string toChange)
        {
            while (toChange.StartsWith(", "))
                toChange = toChange.Substring(2);
            return toChange.Trim();
        }

        #endregion

        public void AddIndividual(Individual ind)
        {
            if (ind != null && !individuals.Contains(ind))
            {
                individuals.Add(ind);
            }
        }

        public IList<string> GetSurnames()
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

        public Image Icon
        {
            get { return FactLocationImage.ErrorIcon(GeocodeStatus).Icon; }
        }

        public string AddressNumeric
        {
            get { return FixNumerics(this.Address); }
        }

        public string PlaceNumeric
        {
            get { return FixNumerics(this.Place); }
        }

        public bool isKnownCountry
        {
            get { return Countries.IsKnownCountry(Country); }
        }

        public bool isUnitedKingdom
        {
            get { return Countries.IsUnitedKingdom(Country); }
        }

        public string Geocoded
        {
            get
            {
                string result;
                if (Geocodes.TryGetValue(GeocodeStatus, out result))
                    return result;
                else
                    return "Unknown";
            }
        }

        public static int GeocodedLocations
        {
            get
            {
                return FactLocation.AllLocations.Count(l => l.IsGeoCoded);
            }
        }

        public string CensusCountry
        {
            get
            {
                if (Countries.IsUnitedKingdom(Country))
                    return Countries.UNITED_KINGDOM;
                else if (Countries.IsCensusCountry(Country))
                    return Country;
                else
                    return Countries.UNKNOWN_COUNTRY;
            }
        }

        public string FreeCenCountyCode
        {
            get
            {
                string result;
                FREECEN_LOOKUP.TryGetValue(Region, out result);
                if (result == null)
                    result = "all";
                return result;
            }
        }

        public Tuple<string, string> FindMyPastCountyCode
        {
            get
            {
                Tuple<string, string> result;
                FINDMYPAST_LOOKUP.TryGetValue(Region, out result);
                return result;
            }
        }

        #endregion

        public bool SupportedLocation(int level)
        {
            if (Countries.IsCensusCountry(Country))
            {
                if (level == COUNTRY) return true;
                // check region is valid if so return true
                return true;
            }
            return false;
        }

        public bool IsGeoCoded
        {
            get
            {
                if (Longitude == 0.0 && Latitude == 0.0)
                    return false;
                if (Properties.GeneralSettings.Default.IncludePartials && GeocodeStatus == Geocode.PARTIAL_MATCH)
                    return true;
                return GeocodeStatus == Geocode.MATCHED || GeocodeStatus == Geocode.GEDCOM_USER;
            }
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

        public static Fact BestFact(IEnumerable<Fact> facts, FactDate when, int limit)
        {
            // this returns a Fact for a FactLocation a person was at for a given period
            Fact result = new Fact(Fact.UNKNOWN, FactDate.UNKNOWN_DATE);
            double minDistance = float.MaxValue;
            foreach (Fact f in facts)
            {
                if (f.FactDate.IsKnown)
                {
                    double distance = Math.Abs(f.FactDate.BestYear - when.BestYear);
                    if (distance < limit)
                    {
                        if (distance < minDistance && !f.Location.location.Equals(string.Empty))
                        { // this is a closer date but now check to ensure we aren't overwriting a known country with an unknown one.
                            if (f.Location.isKnownCountry || (!f.Location.isKnownCountry && !result.Location.isKnownCountry))
                            {
                                result = f;
                                minDistance = distance;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public static FactLocation BestLocation(IEnumerable<Fact> facts, FactDate when)
        {
            Fact result = BestFact(facts, when, int.MaxValue);
            return result.Location;
        }


        public bool IsBlank()
        {
            return this.Country.Length == 0;
        }

        public bool Matches(string s, int level)
        {
            switch (level)
            {
                case COUNTRY: return this.Country.ToUpper().CompareTo(s.ToUpper()) == 0;
                case REGION: return this.Region.ToUpper().CompareTo(s.ToUpper()) == 0;
                case SUBREGION: return this.SubRegion.ToUpper().CompareTo(s.ToUpper()) == 0;
                case ADDRESS: return this.Address.ToUpper().CompareTo(s.ToUpper()) == 0;
                case PLACE: return this.Place.ToUpper().CompareTo(s.ToUpper()) == 0;
                default: return false;
            }
        }

        public bool CensusCountryMatches(string s)
        {
            if (Country.Equals(s))
                return true;
            if (!Countries.IsKnownCountry(Country)) // if we have an unknown country then say it matches
                return true;
            if (Countries.IsEnglandWales(Country) && Countries.IsEnglandWales(s))
                return true;
            if (Country == Countries.UNITED_KINGDOM && Countries.IsUnitedKingdom(s))
                return true;
            if (s == Countries.UNITED_KINGDOM && Countries.IsUnitedKingdom(Country))
                return true;
            if (Country == Countries.SCOTLAND  || s == Countries.SCOTLAND)
                return false; // Either Country or s is not Scotland at this point, so not matching census country.
            if (Countries.IsUnitedKingdom(Country) && Countries.IsUnitedKingdom(s))
                return true;
            return false;
        }

        public int CompareTo(FactLocation that)
        {
            return CompareTo(that, PLACE);
        }

        public int CompareTo(IDisplayLocation that, int level)
        {
            return CompareTo((FactLocation)that, level);
        }

        public virtual int CompareTo(FactLocation that, int level)
        {
            int res = this.Country.CompareTo(that.Country);
            if (res == 0 && level > COUNTRY)
            {
                res = this.Region.CompareTo(that.Region);
                if (res == 0 && level > REGION)
                {
                    res = this.SubRegion.CompareTo(that.SubRegion);
                    if (res == 0 && level > SUBREGION)
                    {
                        res = this.Address.CompareTo(that.Address);
                        if (res == 0 && level > ADDRESS)
                        {
                            res = this.Place.CompareTo(that.Place);
                        }
                    }
                }
            }
            return res;
        }

        public override string ToString()
        {
            //return location;
            return fixedLocation;
        }

        public override bool Equals(Object that)
        {
            if (that is FactLocation)
            {
                return this.CompareTo((FactLocation)that) == 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public static bool operator ==(FactLocation a, FactLocation b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }
            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }
            return a.Equals(b);
        }


        public static bool operator !=(FactLocation a, FactLocation b)
        {
            return !(a == b);
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