using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using FTAnalyzer.Events;
using FTAnalyzer.Utilities;
using FTAnalyzer.Mapping;

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
        
        public static readonly ISet<string> RESULT_TYPES = new HashSet<string>(new string[] {
            STREET_ADDRESS, ROUTE, COUNTRY, ESTABLISHMENT, ADMIN1, ADMIN2, ADMIN3, LOCALITY,
            SUBLOCALITY, NEIGHBOURHOOD, PREMISE, SUBPREMISE, CEMETERY, HOSPITAL, PLACE_OF_WORSHIP,
            INTERSECTION, POLITICAL, POSTALCODE, POSTALTOWN, POSTALCODEPREFIX, NATURALFEATURE, 
            AIRPORT, PARK, POINT_OF_INTEREST, STREET_NUMBER, BUS_STATION, TRANSIT_STATION, 
            CHURCH, SUBWAY_STATION, TRAIN_STATION, UNIVERSITY, POLICE, MUSEUM, POST_OFFICE, 
            COURTHOUSE, FINANCE, COLLOQUIAL_AREA, LIBRARY, AQUARIUM, FIRE_STATION,
            CAMPGROUND, LODGING, VETERINARY_CARE, AMUSEMENT_PARK, OS_FEATURE
        });

        public static readonly ISet<string> PLACES = new HashSet<string>(new string[] {
            PREMISE, STREET_ADDRESS, CEMETERY, HOSPITAL, PLACE_OF_WORSHIP, ROUTE, 
            INTERSECTION, ESTABLISHMENT, SUBPREMISE, NATURALFEATURE,PARK, AIRPORT,
            POINT_OF_INTEREST, STREET_NUMBER, BUS_STATION, CHURCH, TRANSIT_STATION, 
            SUBWAY_STATION, TRAIN_STATION, UNIVERSITY, POLICE, MUSEUM, POST_OFFICE,
            COURTHOUSE, FINANCE, LIBRARY, AQUARIUM, FIRE_STATION, CAMPGROUND, LODGING,
            VETERINARY_CARE, AMUSEMENT_PARK
        });

        private String location;
        private bool loaded;

        public delegate void GoogleEventHandler(object sender, GoogleWaitingEventArgs e);
        public static event GoogleEventHandler WaitingForGoogle;
        public static bool ThreadCancelled { get; set; }

        public GoogleMap()
        {
            InitializeComponent();
            loaded = false;
            string filename = Path.Combine(Application.StartupPath + @"\Resources\GoogleMaps.htm");
            webBrowser.Navigate(filename);
            webBrowser.Hide();
        }

        public bool SetLocation(FactLocation loc, int level)
        {
            while (!loaded)
            {
                Application.DoEvents();
            }
            GeoResponse.CResult.CGeometry.CViewPort viewport = null;
            GeoResponse res = null;
            Object[] args = new Object[] { 0, 0 };
            if (loc.IsGeoCoded(false) && loc.ViewPort != null)
            {
                labMapLevel.Text = "Previously Geocoded: " + loc.ToString();
                viewport = MapTransforms.ReverseTransformViewport(loc.ViewPort);
                args = new Object[] { loc.Latitude, loc.Longitude };
            }
            else
            {
                location = loc.ToString();
                res = CallGoogleGeocode(location);
                if (res.Status == "OK")
                {
                    labMapLevel.Text = GoogleMap.LocationText(res, loc, level);
                    viewport = res.Results[0].Geometry.ViewPort;
                    double lat = res.Results[0].Geometry.Location.Lat;
                    double lng = res.Results[0].Geometry.Location.Long;
                    args = new Object[] { lat, lng };
                }
                else if (res.Status == "OVER_QUERY_LIMIT" && loc.IsGeoCoded(false))
                {
                    labMapLevel.Text = "Previously Geocoded: " + loc.ToString();
                    viewport = new GeoResponse.CResult.CGeometry.CViewPort();
                    viewport.NorthEast.Lat = loc.Latitude + 2;
                    viewport.NorthEast.Long = loc.Longitude + 2;
                    viewport.SouthWest.Lat = loc.Latitude - 2;
                    viewport.SouthWest.Long = loc.Longitude - 2;
                    args= new Object[] { loc.Latitude, loc.Longitude };
                }
                else
                {
                    return false;
                }
            }
            Object marker = webBrowser.Document.InvokeScript("frontAndCenter", args);

            args = new Object[] { viewport.NorthEast.Lat, viewport.NorthEast.Long, viewport.SouthWest.Lat, viewport.SouthWest.Long };
            webBrowser.Document.InvokeScript("setViewport", args);
            webBrowser.Show();
            return true;
        }

        public static string LocationText(GeoResponse res, FactLocation loc, int level)
        {
            string output = string.Empty;
            int returnlevel = GetFactLocation(res.Results[0].Types);
            if (returnlevel != FactLocation.UNKNOWN)
            {
                output = "Google found " + loc.GetLocation(returnlevel);
                // if we have different input and output levels, assuming it isn't just a more accurate place in the address field
                // then also show what Google found
                if (level != returnlevel && !(level == FactLocation.ADDRESS && returnlevel >= FactLocation.ADDRESS))
                    output += " as " + res.Results[0].ReturnAddress;
            }
            else
            {
                output = "Best guess for " + loc.GetLocation(level) + " is " + res.Results[0].ReturnAddress;
            }
            return output;
        }

        public static int GetFactLocation(string[] locationTypes)
        {
            HashSet<string> types = new HashSet<string>(locationTypes);
            foreach(string type in types)
                if (PLACES.Contains(type))
                    return FactLocation.PLACE;
            if (types.Contains(SUBLOCALITY) || types.Contains(POSTALCODE) || types.Contains(NEIGHBOURHOOD))
                return FactLocation.ADDRESS;
            if (types.Contains(ADMIN3) || types.Contains(LOCALITY) || types.Contains(POSTALCODEPREFIX) ||
                types.Contains(POSTALTOWN) || types.Contains(COLLOQUIAL_AREA))
                return FactLocation.SUBREGION;
            if (types.Contains(ADMIN2))
                return FactLocation.REGION;
            if (types.Contains(COUNTRY) || types.Contains(ADMIN1))
                return FactLocation.COUNTRY;
            return FactLocation.UNKNOWN;
        }

        public static void OnWaitingForGoogle(string message)
        {
            if (WaitingForGoogle != null)
            {
                WaitingForGoogle(null, new GoogleWaitingEventArgs(message));
            }
        }

        public static GeoResponse CallGoogleGeocode(string address)
        {
            string url = string.Format(
                    "http://maps.googleapis.com/maps/api/geocode/json?address={0}&region=uk&sensor=false",
                    HttpUtility.UrlEncode(address)
                    );
            return GetGeoResponse(url);
        }

        public static GeoResponse CallGoogleReverseGeocode(double latitude, double longitude)
        {
            string url = string.Format(
                    "http://maps.googleapis.com/maps/api/geocode/json?latlng={0},{1}&region=uk&sensor=false",
                    HttpUtility.UrlEncode(latitude.ToString()), HttpUtility.UrlEncode(longitude.ToString())
                    );
            return GetGeoResponse(url);
        }

        private static GeoResponse GetGeoResponse(string url)
        {
            GeoResponse res = null;
            try
            {
                HttpWebRequest request = HttpWebRequest.Create(url) as HttpWebRequest;
                request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GeoResponse));
                IWebProxy proxy = request.Proxy;
                if (proxy != null)
                {
                    string proxyuri = proxy.GetProxy(request.RequestUri).ToString();
                    request.UseDefaultCredentials = true;
                    request.Proxy = new WebProxy(proxyuri, false);
                    request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
                }
                Stream stream = request.GetResponse().GetResponseStream();
                res = (GeoResponse)serializer.ReadObject(stream);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to contact http://maps.googleapis.com error was : " + ex.Message, "FT Analyzer");
            }
            return res;
        }

        private static int sleepinterval = 200;

        // Call geocoding routine but account for throttling by Google geocoding engine
        public static GeoResponse GoogleGeocode(string address, int badtries)
        {
            double seconds = sleepinterval / 1000;
            if (sleepinterval > 500)
                OnWaitingForGoogle("Over Google limit. Waiting " + seconds + " seconds.");
            if (sleepinterval >= 20000)
            {
                OnWaitingForGoogle("Max Google GeoLocations exceeded for today.");
                GeoResponse response = new GeoResponse();
                response.Status = "Maxed";
                return response;
            }
            for (int interval = 0; interval < sleepinterval; interval += 1000)
            {
                Thread.Sleep(1000);
                if (ThreadCancelled) return null;
            }
            GeoResponse res;
            try
            {
                res = CallGoogleGeocode(address);
            }
            catch (Exception e)
            {
                OnWaitingForGoogle("Caught exception: " + e);
                res = null;
            }
            if (res == null || res.Status == "OVER_QUERY_LIMIT")
            {
                // we're hitting Google too fast, increase interval
                sleepinterval = Math.Min(sleepinterval + ++badtries * 750, 20000);
                return GoogleGeocode(address, badtries);
            }
            else
            {
                OnWaitingForGoogle(string.Empty); // going well clear any previous message
                // no throttling, go a little bit faster
                if (sleepinterval > 10000)
                    sleepinterval = 200;
                else
                    sleepinterval = Math.Max(sleepinterval / 2, 75);
                return res;
            }
        }

        // Call geocoding routine but account for throttling by Google geocoding engine
        public static GeoResponse GoogleReverseGeocode(double latitude, double longitude, int badtries)
        {
            double seconds = sleepinterval / 1000;
            if (sleepinterval > 500)
                OnWaitingForGoogle("Over Google limit. Waiting " + seconds + " seconds.");
            if (sleepinterval >= 20000)
            {
                OnWaitingForGoogle("Max Google GeoLocations exceeded for today.");
                GeoResponse response = new GeoResponse();
                response.Status = "Maxed";
                return response;
            }
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
                OnWaitingForGoogle("Caught exception: " + e);
                res = null;
            }
            if (res == null || res.Status == "OVER_QUERY_LIMIT")
            {
                // we're hitting Google too fast, increase interval
                sleepinterval = Math.Min(sleepinterval + ++badtries * 750, 20000);
                return GoogleReverseGeocode(latitude, longitude, badtries);
            }
            else
            {
                OnWaitingForGoogle(string.Empty); // going well clear any previous message
                // no throttling, go a little bit faster
                if (sleepinterval > 10000)
                    sleepinterval = 200;
                else
                    sleepinterval = Math.Max(sleepinterval / 2, 75);
                return res;
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loaded = true;
            System.Diagnostics.Debug.Print("DocumentCompleted called");
        }

        private void GoogleMap_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
        }
    }
}
