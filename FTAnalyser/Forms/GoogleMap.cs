using FTAnalyzer.Events;
using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using Ionic.Zip;
using log4net.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Web;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class GoogleMap : Form
    {
        public static string ADMIN1 = "administrative_area_level_1";
        public static string ADMIN2 = "administrative_area_level_2";
        public static string ADMIN3 = "administrative_area_level_3";
        public static string AIRPORT = "airport";
        public static string AMUSEMENT_PARK = "amusement_park";
        public static string AQUARIUM = "aquarium";
        public static string BUS_STATION = "bus_station";
        public static string CAMPGROUND = "campground";
        public static string CEMETERY = "cemetery";
        public static string CHURCH = "church";
        public static string COLLOQUIAL_AREA = "colloquial_area";
        public static string COUNTRY = "country";
        public static string COURTHOUSE = "courthouse";
        public static string ESTABLISHMENT = "establishment";
        public static string FINANCE = "finance";
        public static string FIRE_STATION = "fire_station";
        public static string HOSPITAL = "hospital";
        public static string INTERSECTION = "intersection";
        public static string LIBRARY = "library";
        public static string LOCALITY = "locality";
        public static string LODGING = "lodging";
        public static string MUSEUM = "museum";
        public static string NATURALFEATURE = "natural_feature";
        public static string NEIGHBOURHOOD = "neighborhood";
        public static string OS_FEATURE = "OS Feature";
        public static string PARK = "park";
        public static string PLACE_OF_WORSHIP = "place_of_worship";
        public static string POINT_OF_INTEREST = "point_of_interest";
        public static string POLICE = "police";
        public static string POLITICAL = "political";
        public static string POSTALCODE = "postal_code";
        public static string POSTALCODEPREFIX = "postal_code_prefix";
        public static string POSTALTOWN = "postal_town";
        public static string POST_OFFICE = "post_office";
        public static string PREMISE = "premise";
        public static string ROUTE = "route";
        public static string STREET_ADDRESS = "street_address";
        public static string STREET_NUMBER = "street_number";
        public static string SUBLOCALITY = "sublocality";
        public static string SUBPREMISE = "subpremise";
        public static string SUBWAY_STATION = "subway_station";
        public static string TRAIN_STATION = "train_station";
        public static string TRANSIT_STATION = "transit_station";
        public static string UNIVERSITY = "university";
        public static string VETERINARY_CARE = "veterinary_care";

        public static ISet<string> RESULT_TYPES = new HashSet<string>(new string[] {
            STREET_ADDRESS, ROUTE, COUNTRY, ESTABLISHMENT, ADMIN1, ADMIN2, ADMIN3, LOCALITY,
            SUBLOCALITY, NEIGHBOURHOOD, PREMISE, SUBPREMISE, CEMETERY, HOSPITAL, PLACE_OF_WORSHIP,
            INTERSECTION, POLITICAL, POSTALCODE, POSTALTOWN, POSTALCODEPREFIX, NATURALFEATURE,
            AIRPORT, PARK, POINT_OF_INTEREST, STREET_NUMBER, BUS_STATION, TRANSIT_STATION,
            CHURCH, SUBWAY_STATION, TRAIN_STATION, UNIVERSITY, POLICE, MUSEUM, POST_OFFICE,
            COURTHOUSE, FINANCE, COLLOQUIAL_AREA, LIBRARY, AQUARIUM, FIRE_STATION,
            CAMPGROUND, LODGING, VETERINARY_CARE, AMUSEMENT_PARK, OS_FEATURE
        });

        public static ISet<string> PLACES = new HashSet<string>(new string[] {
            PREMISE, STREET_ADDRESS, CEMETERY, HOSPITAL, PLACE_OF_WORSHIP, ROUTE,
            INTERSECTION, ESTABLISHMENT, SUBPREMISE, NATURALFEATURE,PARK, AIRPORT,
            POINT_OF_INTEREST, STREET_NUMBER, BUS_STATION, CHURCH, TRANSIT_STATION,
            SUBWAY_STATION, TRAIN_STATION, UNIVERSITY, POLICE, MUSEUM, POST_OFFICE,
            COURTHOUSE, FINANCE, LIBRARY, AQUARIUM, FIRE_STATION, CAMPGROUND, LODGING,
            VETERINARY_CARE, AMUSEMENT_PARK
        });

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

        public void ShowLocation(FactLocation loc, int level)
        {
            if (loc is object && loc.IsGeoCoded(false))
            {
                string URL = $"https://www.google.com/maps/@{loc.Latitude},{loc.Longitude},{level}z";
                SpecialMethods.VisitWebsite(URL);
            }
            else
                MessageBox.Show($"{loc} is not yet geocoded so can't be displayed.");
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
            HashSet<string> types = new HashSet<string>(locationTypes);
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

        public static GeoResponse CallGoogleGeocode(FactLocation address, string text)
        {
            string bounds = string.Empty;
            string tld = address.IsUnitedKingdom ? "&region=uk" : string.Empty;
            if (address != null)
            {
                //if (address.Level > FactLocation.SUBREGION)
                //{
                //    FactLocation area = address.GetLocation(FactLocation.SUBREGION);
                //    if (area != null && area.IsGeoCoded(false) && !string.IsNullOrEmpty(area.Bounds))
                //        bounds = $"{area.Bounds}";
                //}
                if (string.IsNullOrEmpty(bounds) && address.Level > FactLocation.REGION)
                {
                    FactLocation area = address.GetLocation(FactLocation.REGION);
                    if (area != null && area.IsGeoCoded(false) && !string.IsNullOrEmpty(area.Bounds))
                        bounds = $"{area.Bounds}";
                }
                if (string.IsNullOrEmpty(bounds) && address.Level > FactLocation.COUNTRY)
                {
                    FactLocation area = address.GetLocation(FactLocation.COUNTRY);
                    if (area != null && area.IsGeoCoded(false) && !string.IsNullOrEmpty(area.Bounds))
                        bounds = $"{area.Bounds}";
                }
            }
            string encodedAddress = HttpUtility.UrlEncode(text.Replace(" ", "+"));
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?address={encodedAddress}{bounds}{tld}&key={GoogleAPIKey.KeyValue}";
            return GetGeoResponse(url, text);
        }

        public static GeoResponse CallGoogleReverseGeocode(double latitude, double longitude)
        {
            string lat = HttpUtility.UrlEncode(latitude.ToString());
            string lng = HttpUtility.UrlEncode(longitude.ToString());
            string region = longitude >= -7.974074 && longitude <= 1.879409 && latitude >= 49.814376 && latitude <= 60.970872 ?
                "&region=uk" : string.Empty;
            string url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}{region}&key={GoogleAPIKey.KeyValue}";
            return GetGeoResponse(url, $"latlng={lat},{lng}{region}");
        }

        static GeoResponse GetGeoResponse(string url, string text)
        {
            GeoResponse res;
            HttpWebRequest request;
            try
            {
                request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
                request.Timeout = 3000; // set timeout to 5 seconds from default 100 seconds
                request.ReadWriteTimeout = 10000;
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GeoResponse));
                if (request.Proxy is WebProxy proxy)
                {
                    string proxyuri = proxy.GetProxy(request.RequestUri).ToString();
                    request.UseDefaultCredentials = true;
                    request.Proxy = new WebProxy(proxyuri, false)
                    {
                        Credentials = CredentialCache.DefaultCredentials
                    };
                }
                using (Stream stream = request.GetResponse().GetResponseStream())
                {
                    res = (GeoResponse)serializer.ReadObject(stream);
                }
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.Timeout)
                    Console.WriteLine($"Timeout with {url}\n");
                else
                    MessageBox.Show($"Unable to contact https://maps.googleapis.com error was: {ex.Message}\nWhen trying to look for {text}", "FTAnalyzer");
                res = null;
            }
            if (res!= null && res.Status == "REQUEST_DENIED")
                UIHelpers.ShowMessage("Google returned REQUEST_DENIED - please check you have a valid key and enabled the Geocoding API & Places API");
            return res;
        }

        static int sleepinterval = 200;

        // Call geocoding routine but account for throttling by Google geocoding engine
        public static GeoResponse GoogleGeocode(FactLocation address, string text, int badtries)
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
            GeoResponse res;
            try
            {
                res = CallGoogleGeocode(address, text);
            }
            catch (Exception e)
            {
                OnWaitingForGoogle($"Caught exception: {e}");
                res = null;
            }
            if (res == null || res.Status == "OVER_QUERY_LIMIT")
            {
                // we're hitting Google too fast, increase interval
                sleepinterval = Math.Min(sleepinterval + ++badtries * 750, maxInterval);
                return GoogleGeocode(address, text, badtries);
            }
            else
            {
                if (res!= null && res.Status != "REQUEST_DENIED")
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
        public static GeoResponse GoogleReverseGeocode(double latitude, double longitude, int badtries)
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
            GeoResponse res;
            try
            {
                res = CallGoogleReverseGeocode(latitude, longitude);
            }
            catch (Exception e)
            {
                OnWaitingForGoogle($"Caught exception: {e}");
                res = null;
            }
            if (res == null || res.Status == "OVER_QUERY_LIMIT")
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
            string message = string.IsNullOrEmpty(Properties.MappingSettings.Default.GoogleAPI) ?
                                "Google Geocoding timing out. Possibly exceeded max GeoLocations for today.\nConsider getting your own FREE Google API Key for 40,000 lookups a day. See Help Menu.\n" :
                                "Max Google Timeout - Limit Exceeded.\n";
            OnWaitingForGoogle(message);
            GeoResponse response = new GeoResponse
            {
                Status = "Maxed"
            };
            return response;
        }

        public static void GenerateKML(string filename, List<ExportFactsAtLocation> locations)
        {
            using(StreamWriter output = new StreamWriter(new FileStream(filename, FileMode.Create, FileAccess.Write), Encoding.UTF8))
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
                ZipFile zip = new ZipFile(zipFilename);
                zip.AddFile(filename);
                zip.Save();
            }
        }

        void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) => System.Diagnostics.Debug.Print("DocumentCompleted called");

        void GoogleMap_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void GoogleMap_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
