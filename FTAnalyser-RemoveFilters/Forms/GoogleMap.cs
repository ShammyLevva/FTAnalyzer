﻿using System;
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

namespace FTAnalyzer.Forms
{
    public partial class GoogleMap : Form
    {
        public static readonly string COUNTRY = "country";
        public static readonly string ESTABLISHMENT = "establishment";
        public static readonly string ADMIN1 = "administrative_area_level_1";
        public static readonly string ADMIN2 = "administrative_area_level_2";
        public static readonly string ADMIN3 = "administrative_area_level_3";
        public static readonly string LOCALITY = "locality";
        public static readonly string SUBLOCALITY = "sublocality";
        public static readonly string NEIGHBOURHOOD = "neighborhood";
        public static readonly string STREET_ADDRESS = "street_address";
        public static readonly string PREMISE = "premise";
        public static readonly string CEMETERY = "cemetery";
        public static readonly string HOSPITAL = "hospital";
        public static readonly string PLACE_OF_WORSHIP = "place_of_worship";
        public static readonly string ROUTE = "route";
        public static readonly string INTERSECTION = "intersection";

        private String location;
        private bool loaded;

        public GoogleMap()
        {
            InitializeComponent();
            loaded = false;
            string filename = Application.StartupPath + "\\Resources\\GoogleMaps.htm";
            webBrowser.Navigate(filename);
            webBrowser.Hide();
        }

        public bool setLocation(FactLocation loc, int level)
        {
            this.Cursor = Cursors.WaitCursor;
            while (!loaded)
            {
                Application.DoEvents();
            }
            location = loc.ToString();
            GeoResponse res = CallGeoWS(location);
            if (res.Status != "OK")
            {
                this.Cursor = Cursors.Default;
                return false;
            }            
            double lat = res.Results[0].Geometry.Location.Lat;
            double lng = res.Results[0].Geometry.Location.Lng;
            Object[] args = new Object[] { lat, lng };
            Object marker = webBrowser.Document.InvokeScript("frontAndCenter", args);

            labMapLevel.Text = locationText(res, loc, level);
            var viewport = res.Results[0].Geometry.ViewPort;
            args = new Object[] { viewport.NorthEast.Lat, viewport.NorthEast.Lng, viewport.SouthWest.Lat, viewport.SouthWest.Lng };
            webBrowser.Document.InvokeScript("setViewport", args);
            webBrowser.Show();
            this.Cursor = Cursors.Default;
            return true;
        }

        public static string locationText(GeoResponse res, FactLocation loc, int level)
        {
            string output = string.Empty;
            int returnlevel = GetFactLocation(res.Results[0].Types);
            if (returnlevel != FactLocation.UNKNOWN)
            {
                output = "Google found " + loc.getLocation(returnlevel);
                // if we have different input and output levels, assuming it isn't just a more accurate place in the address field
                // then also show what Google found
                if (level != returnlevel && !(level == FactLocation.ADDRESS && returnlevel >= FactLocation.ADDRESS))
                    output += " as " + res.Results[0].ReturnAddress;
            }
            else
            {
                output = "Best guess for " + loc.getLocation(level) + " is " + res.Results[0].ReturnAddress;
            }
            return output;
        }

        public static int GetFactLocation(string[] locationTypes)
        {
            HashSet<string> types = new HashSet<string>(locationTypes);
            if (types.Contains(PREMISE) || types.Contains(STREET_ADDRESS) || types.Contains(CEMETERY) ||
                types.Contains(HOSPITAL) || types.Contains(PLACE_OF_WORSHIP) || types.Contains(ROUTE) ||
                types.Contains(INTERSECTION))
                return FactLocation.PLACE;
            if (types.Contains(ADMIN3) || types.Contains(SUBLOCALITY))
                return FactLocation.ADDRESS;
            if (types.Contains(ADMIN2) || types.Contains(NEIGHBOURHOOD) || types.Contains(LOCALITY))
                return FactLocation.PARISH;
            if (types.Contains(ADMIN1))
                return FactLocation.REGION;
            if (types.Contains(COUNTRY))
                return FactLocation.COUNTRY;
            return FactLocation.UNKNOWN;
        }

        public static GeoResponse CallGeoWS(string address)
        {
            string url = string.Format(
                    "http://maps.google.com/maps/api/geocode/json?address={0}&region=dk&sensor=false",
                    HttpUtility.UrlEncode(address)
                    );
            var request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GeoResponse));
            var stream = request.GetResponse().GetResponseStream();
            //string result;
            //using (StreamReader sr = new StreamReader(stream))
            //{
            //    result = sr.ReadToEnd();
            //}
            //stream.Seek(0L, SeekOrigin.Begin);
            var res = (GeoResponse)serializer.ReadObject(stream);
            return res;
        }

        private static int sleepinterval = 200;

        // Call geocoding routine but account for throttling by Google geocoding engine
        public static GeoResponse CallGeoWSCount(string address, int badtries)
        {
            Thread.Sleep(sleepinterval);
            GeoResponse res;
            try
            {
                res = CallGeoWS(address);
            }
            catch (Exception e)
            {
                Console.WriteLine("Caught exception: " + e);
                res = null;
            }
            if (res == null || res.Status == "OVER_QUERY_LIMIT")
            {
                // we're hitting Google too fast, increase interval
                sleepinterval = Math.Min(sleepinterval + ++badtries * 1000, 60000);

                Console.WriteLine("Interval:" + sleepinterval + "                           \r");
                return CallGeoWSCount(address, badtries);
            }
            else
            {
                // no throttling, go a little bit faster
                if (sleepinterval > 10000)
                    sleepinterval = 200;
                else
                    sleepinterval = Math.Max(sleepinterval / 2, 50);

                Console.WriteLine("Interval:" + sleepinterval);
                return res;
            }
        }


        [DataContract]
        public class GeoResponse
        {
            [DataMember(Name = "status")]
            public string Status { get; set; }
            [DataMember(Name = "results")]
            public CResult[] Results { get; set; }

            [DataContract]
            public class CResult
            {
                [DataMember(Name = "types")]
                public string[] Types { get; set; }
                [DataMember(Name = "formatted_address")]
                public string ReturnAddress { get; set; }
                [DataMember(Name = "geometry")]
                public CGeometry Geometry { get; set; }

                [DataContract]
                public class CGeometry
                {
                    [DataMember(Name = "location")]
                    public CLocation Location { get; set; }
                    [DataMember(Name = "viewport")]
                    public CViewPort ViewPort { get; set; }

                    [DataContract]
                    public class CLocation
                    {
                        [DataMember(Name = "lat")]
                        public double Lat { get; set; }
                        [DataMember(Name = "lng")]
                        public double Lng { get; set; }
                    }

                    [DataContract]
                    public class CViewPort
                    {
                        [DataMember(Name = "southwest")]
                        public CLocation SouthWest { get; set; }
                        [DataMember(Name = "northeast")]
                        public CLocation NorthEast { get; set; }
                    }
                }
            }
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loaded = true;
            System.Diagnostics.Debug.Print("DocumentCompleted called");
        }

    }
}