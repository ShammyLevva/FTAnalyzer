using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using GeoAPI.Geometries;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace FTAnalyzer
{
    public class FactLocation : IComparable<FactLocation>, IDisplayLocation, IDisplayGeocodedLocation
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public const int UNKNOWN = -1, COUNTRY = 0, REGION = 1, SUBREGION = 2, ADDRESS = 3, PLACE = 4;
        public enum Geocode { UNKNOWN = -1, NOT_SEARCHED = 0, MATCHED = 1, PARTIAL_MATCH = 2, GEDCOM_USER = 3, NO_MATCH = 4, 
            INCORRECT = 5, OUT_OF_BOUNDS = 6, LEVEL_MISMATCH = 7, OS_50KMATCH = 8, OS_50KPARTIAL = 9 };

        private string fixedLocation;
        private string address;
        private string place;
        public string GEDCOMLocation { get; private set; }
        public string SortableLocation { get; private set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string SubRegion { get; set; }
        public string Address { get { return address; } set { address = value; AddressNoNumerics = FixNumerics(value, false); } }
        public string Place { get { return place; } set { place = value; PlaceNoNumerics = FixNumerics(value, false); } }
        public string CountryMetaphone { get; private set; }
        public string RegionMetaphone { get; private set; }
        public string SubRegionMetaphone { get; private set; }
        public string AddressMetaphone { get; private set; }
        public string PlaceMetaphone { get; private set; }
        public string AddressNoNumerics { get; private set; }
        public string PlaceNoNumerics { get; private set; }
        public string ParishID { get; internal set; }
        public int Level { get; private set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public double LatitudeM { get; set; }
        public double LongitudeM { get; set; }
        public Geocode GeocodeStatus { get; set; }
        public string GoogleLocation { get; set; }
        public string GoogleResultType { get; set; }
        public int FoundLevel { get; set; }
        public double PixelSize { get; set; }
        public List<CountyConversion.County> Counties { get; private set; }
        public GeoResponse.CResult.CGeometry.CViewPort ViewPort { get; set; }
        private List<Individual> individuals;

        public string[] Parts
        {
            get { return new string[] { Country, Region, SubRegion, Address, Place }; }
        }

        private static Dictionary<string, string> COUNTRY_TYPOS = new Dictionary<string, string>();
        private static Dictionary<string, string> REGION_TYPOS = new Dictionary<string, string>();
        public static Dictionary<string, string> COUNTRY_SHIFTS = new Dictionary<string, string>();
        public static Dictionary<string, string> CITY_ADD_COUNTRY = new Dictionary<string, string>();
        private static Dictionary<string, string> REGION_SHIFTS = new Dictionary<string, string>();
        private static Dictionary<string, string> FREECEN_LOOKUP = new Dictionary<string, string>();
        private static Dictionary<string, Tuple<string, string>> FINDMYPAST_LOOKUP = new Dictionary<string, Tuple<string, string>>();
        private static IDictionary<string, FactLocation> locations;
        private static Dictionary<Tuple<int, string>, string> GOOGLE_FIXES = new Dictionary<Tuple<int, string>, string>();
        private static Dictionary<Tuple<int, string>, string> LOCAL_GOOGLE_FIXES;

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
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/ChapmanCodes/ChapmanCode"))
                {  // add Chapman code to Region Typos to convert locations with codes to region text strings
                    string chapmanCode = n.Attributes["chapmanCode"].Value;
                    string countyName = n.Attributes["countyName"].Value;
                    if (REGION_TYPOS.ContainsKey(chapmanCode))
                        Console.WriteLine("Error duplicate region typos adding ChapmanCode :" + chapmanCode);
                    if (chapmanCode != null && chapmanCode.Length > 0 && countyName != null && countyName.Length > 0)
                        REGION_TYPOS.Add(chapmanCode, countyName);
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
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/Fixes/DemoteCountries/CityAddCountry"))
                {
                    string from = n.Attributes["city"].Value;
                    string to = n.Attributes["country"].Value;
                    if (from != null && from.Length > 0 && to != null && to.Length > 0)
                    {
                        if (CITY_ADD_COUNTRY.ContainsKey(from))
                            Console.WriteLine("Error duplicate city add country :" + from);
                        if (COUNTRY_SHIFTS.ContainsKey(from)) // also check country shifts for duplicates
                            Console.WriteLine("Error duplicate city in country shift :" + from);
                        CITY_ADD_COUNTRY.Add(from, to);
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
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/GoogleGeocodes/CountryFixes/CountryFix"))
                    AddGoogleFixes(GOOGLE_FIXES, n, COUNTRY);
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/GoogleGeocodes/RegionFixes/RegionFix"))
                    AddGoogleFixes(GOOGLE_FIXES, n, REGION);
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/GoogleGeocodes/SubRegionFixes/SubRegionFix"))
                    AddGoogleFixes(GOOGLE_FIXES, n, SUBREGION);
                foreach (XmlNode n in xmlDoc.SelectNodes("Data/GoogleGeocodes/MultiLevelFixes/MultiLevelFix"))
                    AddGoogleFixes(GOOGLE_FIXES, n, UNKNOWN);
                ValidateTypoFixes();
                COUNTRY_SHIFTS = COUNTRY_SHIFTS.Concat(CITY_ADD_COUNTRY).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        private static void ValidateTypoFixes()
        {
            //foreach (string typo in COUNTRY_TYPOS.Values)
            //    if (!Countries.IsKnownCountry(typo))
            //        Console.WriteLine("Country typo: " + typo + " is not a known country.");
            foreach (string typo in REGION_TYPOS.Values)
                if (!Regions.IsPreferredRegion(typo))
                    Console.WriteLine("Region typo: " + typo + " is not a preferred region.");
            foreach(string shift in COUNTRY_SHIFTS.Keys)
                if(!Regions.IsPreferredRegion(shift))
                    Console.WriteLine("Country shift: " + shift + " is not a preferred region.");
        }

        public static void LoadGoogleFixesXMLFile(RichTextBox xmlErrorDocument)
        {
            LOCAL_GOOGLE_FIXES = new Dictionary<Tuple<int, string>, string>();
            try
            {
                string filename = Path.Combine(Properties.MappingSettings.Default.CustomMapPath, "GoogleFixes.xml");
                if (File.Exists(filename))
                {
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(filename);
                    foreach (XmlNode n in xmlDoc.SelectNodes("GoogleGeocodes/CountryFixes/CountryFix"))
                        AddGoogleFixes(LOCAL_GOOGLE_FIXES, n, COUNTRY);
                    foreach (XmlNode n in xmlDoc.SelectNodes("GoogleGeocodes/RegionFixes/RegionFix"))
                        AddGoogleFixes(LOCAL_GOOGLE_FIXES, n, REGION);
                    foreach (XmlNode n in xmlDoc.SelectNodes("GoogleGeocodes/SubRegionFixes/SubRegionFix"))
                        AddGoogleFixes(LOCAL_GOOGLE_FIXES, n, SUBREGION);
                    foreach (XmlNode n in xmlDoc.SelectNodes("GoogleGeocodes/MultiLevelFixes/MultiLevelFix"))
                        AddGoogleFixes(LOCAL_GOOGLE_FIXES, n, UNKNOWN);
                    xmlErrorDocument.AppendText("\nLoaded " + LOCAL_GOOGLE_FIXES.Count() + " Google Fixes.\n");
                }
            }
            catch (Exception e)
            {
                LOCAL_GOOGLE_FIXES = new Dictionary<Tuple<int, string>, string>();
                MessageBox.Show("Error processing user defined GoogleFixes.xml file. File will be ignored.\n\nError was : " + e.Message, "FT Analyzer");
            }
        }

        private static void AddGoogleFixes(Dictionary<Tuple<int, string>, string> dictionary, XmlNode n, int level)
        {
            string fromstr = n.Attributes["from"].Value;
            string to = n.Attributes["to"].Value;
            Tuple<int, string> from = new Tuple<int, string>(level, fromstr.ToUpperInvariant());
            if (from != null && fromstr.Length > 0 && to != null)
            {
                if (dictionary.ContainsKey(from))
                    log.Error("Error duplicate Google " + GoogleFixLevel(level) + " :" + fromstr + " to " + to);
                else
                {
                    log.Info("Added Google " + GoogleFixLevel(level) + " :" + fromstr + " to " + to);
                    dictionary.Add(from, to);
                }
            }
        }

        private static string GoogleFixLevel(int level)
        {
            switch(level)
            {
                case UNKNOWN: return "MultiLevelFix";
                case COUNTRY: return "CountryFix";
                case REGION: return "RegionFix";
                case SUBREGION: return "SubRegionFix";
                default: return "UNKNOWN";
            }
        }

        private static void SetupGeocodes()
        {
            Geocodes = new Dictionary<Geocode, string>();
            Geocodes.Add(Geocode.UNKNOWN, "Unknown");
            Geocodes.Add(Geocode.NOT_SEARCHED, "Not Searched");
            Geocodes.Add(Geocode.GEDCOM_USER, "GEDCOM/User Data");
            Geocodes.Add(Geocode.PARTIAL_MATCH, "Partial Match (Google)");
            Geocodes.Add(Geocode.MATCHED, "Google Matched");
            Geocodes.Add(Geocode.NO_MATCH, "No Match");
            Geocodes.Add(Geocode.INCORRECT, "Incorrect (User Marked)");
            Geocodes.Add(Geocode.OUT_OF_BOUNDS, "Outside Country Area");
            Geocodes.Add(Geocode.LEVEL_MISMATCH, "Partial Match (Levels)");
            Geocodes.Add(Geocode.OS_50KMATCH, "OS 50k Gazetteer Match");
            Geocodes.Add(Geocode.OS_50KPARTIAL, "Partial Match (OS 50k)");
        }

        public static FactLocation GetLocation(string place)
        {
            return GetLocation(place, string.Empty, string.Empty, Geocode.NOT_SEARCHED);
        }

        public static FactLocation GetLocation(string place, string latitude, string longitude, Geocode status)
        {
            FactLocation result;
            FactLocation temp;
            // GEDCOM lat/long will be prefixed with NS and EW which needs to be +/- to work.
            latitude = latitude.Replace("N", "").Replace("S", "-");
            longitude = longitude.Replace("W", "-").Replace("E", "");
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

        public static FactLocation LookupLocation(string place)
        {
            FactLocation result = null;
            locations.TryGetValue(place, out result);
            if (result == null)
                result = new FactLocation(place);
            return result;
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
                location.Insert(0, fixNumerics ? this.AddressNumeric : this.Address + ", ");
            if (level > ADDRESS && Place.Length > 0)
                location.Insert(0, fixNumerics ? this.PlaceNumeric : this.Place + ", ");
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
            // set unknown location as unknown so it doesn't keep hassling to be searched
            UNKNOWN_LOCATION = GetLocation(string.Empty, "0.0", "0.0", Geocode.UNKNOWN);
            LOCAL_GOOGLE_FIXES = new Dictionary<Tuple<int, string>, string>();
        }

        private FactLocation()
        {
            this.GEDCOMLocation = string.Empty;
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
            this.LatitudeM = 0;
            this.LongitudeM = 0;
            this.Level = UNKNOWN;
            this.GeocodeStatus = Geocode.NOT_SEARCHED;
            this.GoogleLocation = string.Empty;
            this.GoogleResultType = string.Empty;
            this.FoundLevel = -2;
            this.ViewPort = new GeoResponse.CResult.CGeometry.CViewPort();
            this.Counties = new List<CountyConversion.County>();
        }

        private FactLocation(string location, string latitude, string longitude, Geocode status)
            : this(location)
        {
            double temp;
            this.Latitude = double.TryParse(latitude, out temp) ? temp : 0;
            this.Longitude = double.TryParse(longitude, out temp) ? temp : 0;
            Coordinate point = new Coordinate(Longitude, Latitude);
            Coordinate mpoint = MapTransforms.TransformCoordinate(point);
            this.LongitudeM = mpoint.X;
            this.LatitudeM = mpoint.Y;
            this.GeocodeStatus = status;
            if (status == Geocode.NOT_SEARCHED && (Latitude != 0 || Longitude != 0))
                status = Geocode.GEDCOM_USER;
        }

        private FactLocation(string location)
            : this()
        {
            if (location != null)
            {
                this.GEDCOMLocation = location;
                // we need to parse the location string from a little injun to a big injun
                int comma = location.LastIndexOf(",");
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
                string before = (SubRegion + ", " + Region + ", " + Country).ToUpper().Trim();
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
                SetMetaphones();
                Counties = Mapping.CountyConversion.GetCounties(Region);
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
                Place = string.Empty;
            }
            if (Region.Length == 0)
            {
                Region = SubRegion;
                SubRegion = Address;
                Address = Place;
                Place = string.Empty;
            }
            if (SubRegion.Length == 0)
            {
                SubRegion = Address;
                Address = Place;
                Place = string.Empty;
            }
            if (Address.Length == 0)
            {
                Address = Place;
                Place = string.Empty;
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
            if (Country == Countries.AUSTRALIA && toFix.Equals("WA"))
                return "Western Australia"; // fix for WA = Washington
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

        private void SetMetaphones()
        {
            DoubleMetaphone meta = new DoubleMetaphone(Country);
            CountryMetaphone = meta.PrimaryKey;
            meta = new DoubleMetaphone(Region);
            RegionMetaphone = meta.PrimaryKey;
            meta = new DoubleMetaphone(SubRegion);
            SubRegionMetaphone = meta.PrimaryKey;
            meta = new DoubleMetaphone(Address);
            AddressMetaphone = meta.PrimaryKey;
            meta = new DoubleMetaphone(Place);
            PlaceMetaphone = meta.PrimaryKey;
        }

        public static string ReplaceString(string str, string oldValue, string newValue, StringComparison comparison)
        {
            StringBuilder sb = new StringBuilder();

            int previousIndex = 0;
            int index = str.IndexOf(oldValue, comparison);
            while (index != -1)
            {
                sb.Append(str.Substring(previousIndex, index - previousIndex));
                sb.Append(newValue);
                index += oldValue.Length;

                previousIndex = index;
                index = str.IndexOf(oldValue, index, comparison);
            }
            sb.Append(str.Substring(previousIndex));

            return sb.ToString();
        }

        public string GoogleFixed
        {
            get
            {
                // first check the multifixes
                string result = fixedLocation;
                foreach (KeyValuePair<Tuple<int, string>, string> fix in LOCAL_GOOGLE_FIXES)
                {
                    if (fix.Key.Item1 == UNKNOWN)
                        result = ReplaceString(result, fix.Key.Item2, fix.Value, StringComparison.InvariantCultureIgnoreCase);
                }
                if (result != fixedLocation)
                    return result;

                foreach (KeyValuePair<Tuple<int, string>, string> fix in GOOGLE_FIXES)
                {
                    if (fix.Key.Item1 == UNKNOWN)
                        result = ReplaceString(result, fix.Key.Item2, fix.Value, StringComparison.InvariantCultureIgnoreCase);
                }
                if (result != fixedLocation)
                    return result;

                // now check the individual part fixes
                string countryFix = string.Empty;
                string regionFix = string.Empty;
                string subRegionFix = string.Empty;
                LOCAL_GOOGLE_FIXES.TryGetValue(new Tuple<int, string>(COUNTRY, Country.ToUpperInvariant()), out countryFix);
                if (countryFix == null)
                {
                    GOOGLE_FIXES.TryGetValue(new Tuple<int, string>(COUNTRY, Country.ToUpperInvariant()), out countryFix);
                    if (countryFix == null)
                        countryFix = Country;
                }
                LOCAL_GOOGLE_FIXES.TryGetValue(new Tuple<int, string>(REGION, Region.ToUpperInvariant()), out regionFix);
                if (regionFix == null)
                {
                    GOOGLE_FIXES.TryGetValue(new Tuple<int, string>(REGION, Region.ToUpperInvariant()), out regionFix);
                    if (regionFix == null)
                        regionFix = Region;
                }
                LOCAL_GOOGLE_FIXES.TryGetValue(new Tuple<int, string>(SUBREGION, SubRegion.ToUpperInvariant()), out subRegionFix);
                if (subRegionFix == null)
                {
                    GOOGLE_FIXES.TryGetValue(new Tuple<int, string>(SUBREGION, SubRegion.ToUpperInvariant()), out subRegionFix);
                    if (subRegionFix == null)
                        subRegionFix = SubRegion;
                }
                result = countryFix;
                if (!regionFix.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                    result = regionFix + ", " + result;
                if (!subRegionFix.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                    result = subRegionFix + ", " + result;
                if (!Address.Equals(string.Empty) || Properties.GeneralSettings.Default.AllowEmptyLocations)
                    result = Address + ", " + result;
                if (!Place.Equals(string.Empty))
                    result = Place + ", " + result;
                return TrimLeadingCommas(result);
            }
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
            get { return FixNumerics(this.Address, true); }
        }

        public string PlaceNumeric
        {
            get { return FixNumerics(this.Place, true); }
        }
        
        public bool IsKnownCountry
        {
            get { return Countries.IsKnownCountry(Country); }
        }

        public bool IsUnitedKingdom
        {
            get { return Countries.IsUnitedKingdom(Country); }
        }

        public bool IsEnglandWales
        {
            get { return Countries.IsEnglandWales(Country); }
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
                return FactLocation.AllLocations.Count(l => l.IsGeoCoded(false));
            }
        }

        public static int LocationsCount
        {   // discount the empty location
            get { return FactLocation.AllLocations.Count() - 1; }
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

        public bool IsGeoCoded(bool recheckPartials)
        {
            if (Longitude == 0.0 && Latitude == 0.0)
                return false;
            if (!recheckPartials && Properties.MappingSettings.Default.IncludePartials && 
                (GeocodeStatus == Geocode.PARTIAL_MATCH || GeocodeStatus == Geocode.LEVEL_MISMATCH || GeocodeStatus == Geocode.OS_50KPARTIAL))
                return true;
            return GeocodeStatus == Geocode.MATCHED || GeocodeStatus == Geocode.GEDCOM_USER || GeocodeStatus == Geocode.OS_50KMATCH;
        }

        private string FixNumerics(string addressField, bool returnNumber)
        {
            int pos = addressField.IndexOf(" ");
            if (pos > 0 & pos < addressField.Length)
            {
                string number = addressField.Substring(0, pos);
                string name = addressField.Substring(pos + 1);
                Match matcher = Regex.Match(number, "\\d+[A-Za-z½]?");
                if (matcher.Success)
                    return returnNumber ? name + " - " + number : name;
            }
            return addressField;
        }

        public static Fact BestLocationFact(IEnumerable<Fact> facts, FactDate when, int limit)
        {
            // this returns a Fact for a FactLocation a person was at for a given period
            Fact result = new Fact("Unknown", Fact.UNKNOWN, FactDate.UNKNOWN_DATE);
            double minDistance = float.MaxValue;
            double distance;
            foreach (Fact f in facts)
            {
                if (f.FactDate.IsKnown && !f.Location.GEDCOMLocation.Equals(string.Empty))
                {  // only deal with known dates and non empty locations
                    if (Fact.RANGED_DATE_FACTS.Contains(f.FactType) && f.FactDate.StartDate.Year != f.FactDate.EndDate.Year) // If fact type is ranged year use least end of range
                    {
                        distance = Math.Min(Math.Abs(f.FactDate.StartDate.Year - when.BestYear), Math.Abs(f.FactDate.EndDate.Year - when.BestYear));
                        distance = Math.Min(distance, Math.Abs(f.FactDate.BestYear - when.BestYear)); // also check mid point to ensure fact is picked up at any point in range
                    }
                    else
                        distance = Math.Abs(f.FactDate.BestYear - when.BestYear);
                    if (distance < limit)
                    {
                        if (distance < minDistance)
                        { // this is a closer date but now check to ensure we aren't overwriting a known country with an unknown one.
                            if (f.Location.IsKnownCountry || (!f.Location.IsKnownCountry && !result.Location.IsKnownCountry))
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
            Fact result = BestLocationFact(facts, when, int.MaxValue);
            return result.Location;
        }


        public bool IsBlank()
        {
            return this.Country.Length == 0;
        }

        public bool CensusCountryMatches(string s, bool includeUnknownCountries)
        {
            if (Country.Equals(s))
                return true;
            if (includeUnknownCountries)
            {
                if (!Countries.IsKnownCountry(Country)) // if we have an unknown country then say it matches
                    return true;
            }
            if (Country == Countries.UNITED_KINGDOM && Countries.IsUnitedKingdom(s))
                return true;
            if (s == Countries.UNITED_KINGDOM && Countries.IsUnitedKingdom(Country))
                return true;
            if (Country == Countries.SCOTLAND || s == Countries.SCOTLAND)
                return false; // Either Country or s is not Scotland at this point, so not matching census country.
            if (Countries.IsEnglandWales(Country) && Countries.IsEnglandWales(s))
                return true;
            if (Countries.IsUnitedKingdom(Country) && Countries.IsUnitedKingdom(s))
                return true;
            return false;
        }

        public static void CopyLocationDetails(FactLocation from, FactLocation to)
        {
            to.Latitude = from.Latitude;
            to.Longitude = from.Longitude;
            to.LatitudeM = from.LatitudeM;
            to.LongitudeM = from.LongitudeM;
            to.ViewPort.NorthEast.Lat = from.ViewPort.NorthEast.Lat;
            to.ViewPort.NorthEast.Long = from.ViewPort.NorthEast.Long;
            to.ViewPort.SouthWest.Lat = from.ViewPort.SouthWest.Lat;
            to.ViewPort.SouthWest.Long = from.ViewPort.SouthWest.Long;
            to.GeocodeStatus = from.GeocodeStatus;
            to.GoogleLocation = from.GoogleLocation;
            to.GoogleResultType = from.GoogleResultType;
            to.FoundLevel = from.FoundLevel;
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
                        res = this.AddressNumeric.CompareTo(that.AddressNumeric);
                        if (res == 0 && level > ADDRESS)
                        {
                            res = this.PlaceNumeric.CompareTo(that.PlaceNumeric);
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

        public bool NeedsReverseGeocoding { 
            get 
            {
                return GoogleLocation.Length == 0 && (GeocodeStatus == Geocode.GEDCOM_USER || GeocodeStatus == Geocode.OS_50KMATCH || GeocodeStatus == Geocode.OS_50KPARTIAL);
            } 
        }
    }
}