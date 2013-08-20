using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;

namespace FTAnalyzer
{
    public class FactLocation : IComparable<FactLocation>, IDisplayLocation
    {

        public const int UNKNOWN = -1, COUNTRY = 0, REGION = 1, PARISH = 2, ADDRESS = 3, PLACE = 4;

        private string location;
        private string fixedLocation;
        public string SortableLocation { get; private set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string Parish { get; set; }
        public string Address { get; set; }
        public string Place { get; set; }
        public string ParishID { get; internal set; }
        public int Level { get; private set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        private List<Individual> individuals;
        private static Dictionary<string, string> COUNTRY_TYPOS = new Dictionary<string, string>();
        private static Dictionary<string, string> REGION_TYPOS = new Dictionary<string, string>();
        private static Dictionary<string, string> COUNTRY_SHIFTS = new Dictionary<string, string>();
        private static Dictionary<string, string> REGION_SHIFTS = new Dictionary<string, string>();
        private static Dictionary<string, string> FREECEN_LOOKUP = new Dictionary<string, string>();
        private static Dictionary<string, Tuple<string, string>> FINDMYPAST_LOOKUP = new Dictionary<string, Tuple<string, string>>();

        static FactLocation()
        {
            // load conversions from XML file
            string filename = Path.Combine(Application.StartupPath, @"Resources\FactLocationFixes.xml");
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


        public FactLocation()
        {
            this.location = "";
            this.fixedLocation = "";
            this.SortableLocation = "";
            this.Country = "";
            this.Region = "";
            this.Parish = "";
            this.Address = "";
            this.Place = "";
            this.ParishID = null;
            this.individuals = new List<Individual>();
            this.Latitude = 0;
            this.Longitude = 0;
        }

        public FactLocation(string location, string latitude, string longitude)
            : this(location)
        {
            double temp;
            this.Latitude = double.TryParse(latitude, out temp) ? temp : 0;
            this.Longitude = double.TryParse(longitude, out temp) ? temp : 0;
        }

        public FactLocation(string location)
            : this()
        {
            if (location != null)
            {
                TextInfo txtInfo = new CultureInfo("en-GB", false).TextInfo;
                this.location = txtInfo.ToTitleCase(location).Replace("(","").Replace(")","");
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
                            Parish = location.Substring(comma + 1).Trim();
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
                            Parish = location.Trim();
                            Level = PARISH;
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
                fixEmptyFields();
                fixCapitalisation();
                fixRegionFullStops();
                fixCountryFullStops();
                fixMultipleSpacesAndAmpersands();
                fixCountryTypos();
                Country = fixRegionTypos(Country);
                ShiftCountryToRegion();
                Region = fixRegionTypos(Region);
                ShiftRegionToParish();
                SetFixedLocation();
                SetSortableLocation();
                //string after = (parish + ", " + region + ", " + country).ToUpper().Trim();
                //if (!before.Equals(after))
                //    Console.WriteLine("Debug : '" + before + "'  converted to '" + after + "'");
            }
        }

        #region Fix Location string routines
        private void fixEmptyFields()
        {
            // first remove extraneous spaces
            Country = Country.Trim();
            Region = Region.Trim();
            Parish = Parish.Trim();
            Address = Address.Trim();
            Place = Place.Trim();

            if (Country.Length == 0)
            {
                Country = Region;
                Region = Parish;
                Parish = Address;
                Address = Place;
                Place = "";
            }
            if (Region.Length == 0)
            {
                Region = Parish;
                Parish = Address;
                Address = Place;
                Place = "";
            }
            if (Parish.Length == 0)
            {
                Parish = Address;
                Address = Place;
                Place = "";
            }
            if (Address.Length == 0)
            {
                Address = Place;
                Place = "";
            }
        }

        private void fixCapitalisation()
        {
            if (Country.Length > 1)
                Country = char.ToUpper(Country[0]) + Country.Substring(1);
            if (Region.Length > 1)
                Region = char.ToUpper(Region[0]) + Region.Substring(1);
            if (Parish.Length > 1)
                Parish = char.ToUpper(Parish[0]) + Parish.Substring(1);
            if (Address.Length > 1)
                Address = char.ToUpper(Address[0]) + Address.Substring(1);
            if (Place.Length > 1)
                Place = char.ToUpper(Place[0]) + Place.Substring(1);
        }

        private void fixRegionFullStops()
        {
            Region = Region.Replace(".", " ").Trim();
        }

        private void fixCountryFullStops()
        {
            Country = Country.Replace(".", " ").Trim();
        }

        private void fixMultipleSpacesAndAmpersands()
        {
            while (Country.IndexOf("  ") != -1)
                Country = Country.Replace("  ", " ");
            while (Region.IndexOf("  ") != -1)
                Region = Region.Replace("  ", " ");
            while (Parish.IndexOf("  ") != -1)
                Parish = Parish.Replace("  ", " ");
            while (Address.IndexOf("  ") != -1)
                Address = Address.Replace("  ", " ");
            while (Place.IndexOf("  ") != -1)
                Place = Place.Replace("  ", " ");
            Country = Country.Replace("&", "and");
            Region = Region.Replace("&", "and");
            Parish = Parish.Replace("&", "and");
            Address = Address.Replace("&", "and");
            Place = Place.Replace("&", "and");
        }

        private void fixCountryTypos()
        {
            string newCountry = string.Empty;
            COUNTRY_TYPOS.TryGetValue(Country, out newCountry);
            if (newCountry != null && newCountry.Length > 0)
                Country = newCountry;
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
            COUNTRY_SHIFTS.TryGetValue(Country, out newCountry);
            if (newCountry != null && newCountry.Length > 0)
            {
                Place = (Place + " " + Address).Trim();
                Address = Parish;
                Parish = Region;
                Region = Country;
                Country = newCountry;
                if (Level < PLACE) Level++; // we have moved up a level
            }
        }

        private void ShiftRegionToParish()
        {
            string newRegion = string.Empty;
            REGION_SHIFTS.TryGetValue(Region, out newRegion);
            if (newRegion != null && newRegion.Length > 0)
            {
                Place = (Place + " " + Address).Trim();
                Address = Parish;
                Parish = Region;
                Region = newRegion;
                if (Level < PLACE) Level++; // we have moved up a level
            }
        }

        private void SetFixedLocation()
        {
            fixedLocation = Country;
            if (!Region.Equals(string.Empty))
                fixedLocation = Region + ", " + fixedLocation;
            if (!Parish.Equals(string.Empty))
                fixedLocation = Parish + ", " + fixedLocation;
            if (!Address.Equals(string.Empty))
                fixedLocation = Address + ", " + fixedLocation;
            if (!Place.Equals(string.Empty))
                fixedLocation = Place + ", " + fixedLocation;
        }

        private void SetSortableLocation()
        {
            SortableLocation = Country;
            if (!Region.Equals(string.Empty))
                SortableLocation = SortableLocation + ", " + Region;
            if (!Parish.Equals(string.Empty))
                SortableLocation = SortableLocation + ", " + Parish;
            if (!Address.Equals(string.Empty))
                SortableLocation = SortableLocation + ", " + Address;
            if (!Place.Equals(string.Empty))
                SortableLocation = SortableLocation + ", " + Place;
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

        public FactLocation GetLocation(int level) { return GetLocation(level, false); }
        public FactLocation GetLocation(int level, bool fixNumerics)
        {
            StringBuilder location = new StringBuilder(this.Country);
            if (level > COUNTRY && Region.Length > 0)
                location.Insert(0, this.Region + ", ");
            if (level > REGION && Parish.Length > 0)
                location.Insert(0, this.Parish + ", ");
            if (level > PARISH && Address.Length > 0)
                location.Insert(0, fixNumerics ? FixNumerics(this.Address) : this.Address + ", ");
            if (level > ADDRESS && Place.Length > 0)
                location.Insert(0, fixNumerics ? FixNumerics(this.Place) : this.Place + ", ");
            return new FactLocation(location.ToString());
        }

        public static FactLocation BestLocation(IEnumerable<Fact> facts, FactDate when)
        {
            // this returns a Location a person was at for a given period
            FactLocation result = new FactLocation();
            double minDistance = float.MaxValue;
            foreach (Fact f in facts)
            {
                double distance = Math.Sqrt(Math.Pow((double)(f.FactDate.StartDate.Year - when.StartDate.Year), 2.0) +
                    Math.Pow((double)(f.FactDate.EndDate.Year - when.EndDate.Year), 2.0));
                if (distance < minDistance && !f.Location.location.Equals(string.Empty))
                { // this is a closer date but now check to ensure we aren't overwriting a known country with an unknown one.
                    if (f.Location.isKnownCountry || (!f.Location.isKnownCountry && !result.isKnownCountry))
                    {
                        result = f.Location;
                        minDistance = distance;
                    }
                }
            }
            return result;
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
                case PARISH: return this.Parish.ToUpper().CompareTo(s.ToUpper()) == 0;
                case ADDRESS: return this.Address.ToUpper().CompareTo(s.ToUpper()) == 0;
                case PLACE: return this.Place.ToUpper().CompareTo(s.ToUpper()) == 0;
                default: return false;
            }
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
                    res = this.Parish.CompareTo(that.Parish);
                    if (res == 0 && level > PARISH)
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