using FTAnalyzer.Events;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using FTAnalyzer.Properties;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Web;
using FTAnalyzer.Windows;
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;

namespace FTAnalyzer.Forms
{
    public partial class GoogleMap : Form
    {
        public static readonly string ADMIN1 = "administrative_area_level_1";
        public static readonly string ADMIN2 = "administrative_area_level_2";
        public static readonly string ADMIN3 = "administrative_area_level_3";
        public static readonly string AIRPORT = "airport";
        public static readonly string AMUSEMENT_PARK = "amusement_park";
        public static readonly string AQUARIUM = "aquarium";
        public static readonly string BUS_STATION = "bus_station";
        public static readonly string CAMPGROUND = "campground";
        public static readonly string CEMETERY = "cemetery";
        public static readonly string CHURCH = "church";
        public static readonly string COLLOQUIAL_AREA = "colloquial_area";
        public static readonly string COUNTRY = "country";
        public static readonly string COURTHOUSE = "courthouse";
        public static readonly string ESTABLISHMENT = "establishment";
        public static readonly string FINANCE = "finance";
        public static readonly string FIRE_STATION = "fire_station";
        public static readonly string HOSPITAL = "hospital";
        public static readonly string INTERSECTION = "intersection";
        public static readonly string LIBRARY = "library";
        public static readonly string LOCALITY = "locality";
        public static readonly string LODGING = "lodging";
        public static readonly string MUSEUM = "museum";
        public static readonly string NATURALFEATURE = "natural_feature";
        public static readonly string NEIGHBOURHOOD = "neighborhood";
        public static readonly string OS_FEATURE = "OS Feature";
        public static readonly string PARK = "park";
        public static readonly string PLACE_OF_WORSHIP = "place_of_worship";
        public static readonly string POINT_OF_INTEREST = "point_of_interest";
        public static readonly string POLICE = "police";
        public static readonly string POLITICAL = "political";
        public static readonly string POSTALCODE = "postal_code";
        public static readonly string POSTALCODEPREFIX = "postal_code_prefix";
        public static readonly string POSTALTOWN = "postal_town";
        public static readonly string POST_OFFICE = "post_office";
        public static readonly string PREMISE = "premise";
        public static readonly string ROUTE = "route";
        public static readonly string STREET_ADDRESS = "street_address";
        public static readonly string STREET_NUMBER = "street_number";
        public static readonly string SUBLOCALITY = "sublocality";
        public static readonly string SUBPREMISE = "subpremise";
        public static readonly string SUBWAY_STATION = "subway_station";
        public static readonly string TRAIN_STATION = "train_station";
        public static readonly string TRANSIT_STATION = "transit_station";
        public static readonly string UNIVERSITY = "university";
        public static readonly string VETERINARY_CARE = "veterinary_care";

        public static readonly ISet<string> RESULT_TYPES = new HashSet<string>([
            STREET_ADDRESS, ROUTE, COUNTRY, ESTABLISHMENT, ADMIN1, ADMIN2, ADMIN3, LOCALITY,
            SUBLOCALITY, NEIGHBOURHOOD, PREMISE, SUBPREMISE, CEMETERY, HOSPITAL, PLACE_OF_WORSHIP,
            INTERSECTION, POLITICAL, POSTALCODE, POSTALTOWN, POSTALCODEPREFIX, NATURALFEATURE,
            AIRPORT, PARK, POINT_OF_INTEREST, STREET_NUMBER, BUS_STATION, TRANSIT_STATION,
            CHURCH, SUBWAY_STATION, TRAIN_STATION, UNIVERSITY, POLICE, MUSEUM, POST_OFFICE,
            COURTHOUSE, FINANCE, COLLOQUIAL_AREA, LIBRARY, AQUARIUM, FIRE_STATION,
            CAMPGROUND, LODGING, VETERINARY_CARE, AMUSEMENT_PARK, OS_FEATURE
        ]);

        public static readonly ISet<string> PLACES = new HashSet<string>([
            PREMISE, STREET_ADDRESS, CEMETERY, HOSPITAL, PLACE_OF_WORSHIP, ROUTE,
            INTERSECTION, ESTABLISHMENT, SUBPREMISE, NATURALFEATURE,PARK, AIRPORT,
            POINT_OF_INTEREST, STREET_NUMBER, BUS_STATION, CHURCH, TRANSIT_STATION,
            SUBWAY_STATION, TRAIN_STATION, UNIVERSITY, POLICE, MUSEUM, POST_OFFICE,
            COURTHOUSE, FINANCE, LIBRARY, AQUARIUM, FIRE_STATION, CAMPGROUND, LODGING,
            VETERINARY_CARE, AMUSEMENT_PARK
        ]);

        public delegate void GoogleEventHandler(object sender, GoogleWaitingEventArgs e);
        public static event GoogleEventHandler WaitingForGoogle;
        public static bool ThreadCancelled { get; set; }

        public GoogleMap()
        {
            try
            {
                InitializeComponent();
                Top += NativeMethods.TopTaskbarOffset;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; // force TLS1.2
            }
            catch (Exception) { }
        }

        public static void ShowLocation(FactLocation loc, int level)
        {
            if (loc is not null && loc.IsGeoCoded(false))
            {
                string URL = $"https://www.google.com/maps/@{loc.Latitude},{loc.Longitude},{level}z";
                SpecialMethods.VisitWebsite(URL);
            }
            else
                UIHelpers.ShowMessage($"{loc} is not yet geocoded so can't be displayed.");
        }

        public static string LocationText(GeoResponse res, FactLocation loc, int level)
        {
            string output;
            int returnlevel = GetFactLocationType(res.Results[0].Types, loc);
            if (returnlevel != FactLocation.UNKNOWN)
            {
                output = $"Google found {loc.GetLocation(returnlevel)}";
                // if we have different input and output levels, assuming it isn't just a more accurate place in the address field
                // then also show what Google found
                if (level != returnlevel && !(level == FactLocation.ADDRESS && returnlevel >= FactLocation.ADDRESS))
                    output += $" as {res.Results[0].ReturnAddress}";
            }
            else
            {
                output = $"Best guess for {loc.GetLocation(level)} is {res.Results[0].ReturnAddress}";
            }
            return output;
        }

        public static int GetFactLocationType(string[] locationTypes, FactLocation loc)
        {
            if (loc is null) return FactLocation.UNKNOWN;
            bool UK = loc.IsUnitedKingdom;
            HashSet<string> types = new(locationTypes);
            foreach (string type in types)
                if (PLACES.Contains(type))
                    return FactLocation.PLACE;
            if (types.Contains(SUBLOCALITY) || types.Contains(POSTALCODE) || types.Contains(NEIGHBOURHOOD))
                return FactLocation.ADDRESS;
            if (types.Contains(ADMIN3) || types.Contains(LOCALITY))
                return UK ? FactLocation.SUBREGION : FactLocation.ADDRESS;
            if (types.Contains(POSTALCODEPREFIX) || types.Contains(POSTALTOWN) || types.Contains(COLLOQUIAL_AREA))
                return FactLocation.SUBREGION;
            if (types.Contains(ADMIN2))
                return UK ? FactLocation.REGION : FactLocation.SUBREGION;
            if (types.Contains(ADMIN1))
                return UK ? FactLocation.COUNTRY : FactLocation.REGION;
            if (types.Contains(COUNTRY))
                return FactLocation.COUNTRY;
            return FactLocation.UNKNOWN;
        }

        public static void OnWaitingForGoogle(string message) => WaitingForGoogle?.Invoke(null, new GoogleWaitingEventArgs(message));

        public static GeoResponse? CallGoogleGeocode(FactLocation address, string text)
        {
            string bounds = string.Empty;
            string tld = address.IsUnitedKingdom ? "&region=uk" : string.Empty;
            if (address is not null)
            {
                //if (address.Level > FactLocation.SUBREGION)
                //{
                //    FactLocation area = address.GetLocation(FactLocation.SUBREGION);
                //    if (area is not null && area.IsGeoCoded(false) && !string.IsNullOrEmpty(area.Bounds))
                //        bounds = $"{area.Bounds}";
                //}
                if (string.IsNullOrEmpty(bounds) && address.Level > FactLocation.REGION)
                {
                    FactLocation area = address.GetLocation(FactLocation.REGION);
                    if (area is not null && area.IsGeoCoded(false) && !string.IsNullOrEmpty(area.Bounds))
                        bounds = $"{area.Bounds}";
                }
                if (string.IsNullOrEmpty(bounds) && address.Level > FactLocation.COUNTRY)
                {
                    FactLocation area = address.GetLocation(FactLocation.COUNTRY);
                    if (area is not null && area.IsGeoCoded(false) && !string.IsNullOrEmpty(area.Bounds))
                        bounds = $"{area.Bounds}";
                }
            }
            string encodedAddress = HttpUtility.UrlEncode(text.Replace(" ", "+"));
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={encodedAddress}{bounds}{tld}&key={GoogleAPIKey.KeyValue}";
            return GetGeoResponseAsync(url, text).Result;
        }

        public static GeoResponse? CallGoogleReverseGeocode(double latitude, double longitude)
        {
            string lat = HttpUtility.UrlEncode(latitude.ToString());
            string lng = HttpUtility.UrlEncode(longitude.ToString());
            string region = longitude >= -7.974074 && longitude <= 1.879409 && latitude >= 49.814376 && latitude <= 60.970872 ?
                "&region=uk" : string.Empty;
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}{region}&key={GoogleAPIKey.KeyValue}";
            return GetGeoResponseAsync(url, $"latlng={lat},{lng}{region}").Result;
        }

        static async Task<GeoResponse?> GetGeoResponseAsync(string url, string text)
        {
            GeoResponse? res;
            HttpRequestMessage request= new();
            try
            {
                request.Headers.Add("Accept-Encoding", "gzip,deflate");
                request.RequestUri = new Uri(url);
                HttpResponseMessage response = await Program.GoogleClient.SendAsync(request);

                //if (request.Proxy is WebProxy proxy)
                //{
                //    string proxyuri = proxy.GetProxy(request.RequestUri).ToString();
                //    request.UseDefaultCredentials = true;
                //    request.Proxy = new WebProxy(proxyuri, false)
                //    {
                //        Credentials = CredentialCache.DefaultCredentials
                //    };
                //}
                response.EnsureSuccessStatusCode();
                string jsonString = await response.Content.ReadAsStringAsync();
                res = JsonConvert.DeserializeObject<GeoResponse>(jsonString);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout)
                    Debug.WriteLine($"Timeout with {url}\n");
                else
                    UIHelpers.ShowMessage($"Unable to contact https://maps.googleapis.com error was: {ex.Message}\nWhen trying to look for {text}", "FTAnalyzer");
                res = null;
            }
            if (res is not null && res.Status == "REQUEST_DENIED")
                UIHelpers.ShowMessage("Google returned REQUEST_DENIED - please check you have a valid key and enabled the Geocoding API & Places API");
            return res;
        }

        static int sleepinterval = 200;

        // Call geocoding routine but account for throttling by Google geocoding engine
        public static GeoResponse? GoogleGeocode(FactLocation address, string text, int badtries)
        {
            int maxInterval = 30000;
            double seconds = sleepinterval / 1000;
            if (sleepinterval > 500 && seconds > 0.1)
                OnWaitingForGoogle($"Google Timeout. Waiting {seconds} seconds.");
            if (sleepinterval >= maxInterval)
                return MaxedOut();
            for (int interval = 0; interval < sleepinterval; interval += 1000)
            {
                Thread.Sleep(1000);
                if (ThreadCancelled) return null;
            }
            GeoResponse? res;
            try
            {
                res = CallGoogleGeocode(address, text);
            }
            catch (Exception e)
            {
                OnWaitingForGoogle($"Caught exception: {e}");
                res = null;
            }
            if (res is null || res.Status == "OVER_QUERY_LIMIT")
            {
                // we're hitting Google too fast, increase interval
                sleepinterval = Math.Min(sleepinterval + ++badtries * 750, maxInterval);
                return GoogleGeocode(address, text, badtries);
            }
            else
            {
                if (res is not null && res.Status != "REQUEST_DENIED")
                {
                    OnWaitingForGoogle(string.Empty); // going well clear any previous message
                                                      // no throttling, go a little bit faster
                    if (sleepinterval > 10000)
                        sleepinterval = 200;
                    else
                        sleepinterval = Math.Max(sleepinterval / 2, 75);
                }
                return res;
            }
        }

        // Call geocoding routine but account for throttling by Google geocoding engine
        public static GeoResponse? GoogleReverseGeocode(double latitude, double longitude, int badtries)
        {
            int maxInterval = 30000;
            double seconds = sleepinterval / 1000;
            if (sleepinterval > 500 && seconds > 0.1)
                OnWaitingForGoogle($"Over Google limit. Waiting {seconds} seconds.");
            if (sleepinterval >= maxInterval)
                return MaxedOut();
            for (int interval = 0; interval < sleepinterval; interval += 1000)
            {
                Thread.Sleep(1000);
                if (ThreadCancelled) return null;
            }
            GeoResponse? res;
            try
            {
                res = CallGoogleReverseGeocode(latitude, longitude);
            }
            catch (Exception e)
            {
                OnWaitingForGoogle($"Caught exception: {e}");
                res = null;
            }
            if (res is null || res.Status == "OVER_QUERY_LIMIT")
            {
                // we're hitting Google too fast, increase interval
                sleepinterval = Math.Min(sleepinterval + ++badtries * 750, maxInterval);
                return GoogleReverseGeocode(latitude, longitude, badtries);
            }
            else
            {
                OnWaitingForGoogle(string.Empty); // going well clear any previous message
                // no throttling, go a little bit faster
                if (sleepinterval > 10000)
                    sleepinterval = 200;
                else
                    sleepinterval = Math.Max(sleepinterval / 2, 751);
                return res;
            }
        }

        static GeoResponse MaxedOut()
        {
            string message = string.IsNullOrEmpty(MappingSettings.Default.GoogleAPI) ?
                                "Google Geocoding timing out. Possibly exceeded max GeoLocations for today.\nConsider getting your own FREE Google API Key for 40,000 lookups a day. See Help Menu.\n" :
                                "Max Google Timeout - Limit Exceeded.\n";
            OnWaitingForGoogle(message);
            GeoResponse response = new()
            {
                Status = "Maxed"
            };
            return response;
        }

        public static void GenerateKML(string filename, List<ExportFactsAtLocation> locations)
        {
            using(StreamWriter output = new(new FileStream(filename, FileMode.Create, FileAccess.Write), Encoding.UTF8))
            {
                output.WriteLine(@"<?xml version=""1.0"" encoding=""UTF-8""?>");
                output.WriteLine(@"<kml xmlns=""http://www.opengis.net/kml/2.2"">");
                output.WriteLine("<Document>");
                foreach(ExportFactsAtLocation loc in locations)
                {
                    if (loc.FactsAtLocation?.Count > 0)
                    {
                        output.WriteLine("<Placemark>");
                        output.WriteLine($"    <name>{loc.LocationName}</name>");
                        output.WriteLine($"    <description>The following individuals/families were here:");
                        int placecount = 0;
                        foreach (string factAtLocation in loc.FactsAtLocation)
                        {
                            if(placecount++ <= 100)
                                output.WriteLine($"{factAtLocation}"); // eg: John Smith born here XX XXX XXXX
                            else
                            {
                                int remaining = loc.FactsAtLocation.Count - 100;
                                output.WriteLine($"and {remaining} more. (Google limit max 100 lines).");
                                break;
                            }
                        }
                        output.WriteLine("    </description>");
                        output.WriteLine("    <Point>");
                        output.WriteLine($"        <coordinates>{loc.Longitude},{loc.Latitude},0</coordinates>");
                        output.WriteLine("    </Point>");
                        output.WriteLine("</Placemark>");
                    }
                }
                output.WriteLine("</Document>");
                output.WriteLine("</kml>");
            }
            long length = new FileInfo(filename).Length;
            if (length > 5000000)
            {
                string zipFilename = filename.Replace(".kml", ".kmz");
                ZipFile zip = new(zipFilename)
                {
                    filename
                };
                zip.CommitUpdate();
            }
        }

        void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) => System.Diagnostics.Debug.Print("DocumentCompleted called");

        void GoogleMap_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void GoogleMap_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
