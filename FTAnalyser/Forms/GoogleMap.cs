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

namespace FTAnalyzer.Forms
{
    public partial class GoogleMap : Form
    {
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

        public void setLocation(FactLocation loc, int level)
        {
            while (!loaded)
            {
                Application.DoEvents();
            }
            location = loc.ToString();
            GeoResponse res = CallGeoWS(location);
            double lat = res.Results[0].Geometry.Location.Lat;
            double lng = res.Results[0].Geometry.Location.Lng;
            Object[] args = new Object[] { lat, lng };
            Object marker = webBrowser.Document.InvokeScript("frontAndCenter", args);

            var viewport = res.Results[0].Geometry.ViewPort;
            args = new Object[] { viewport.NorthEast.Lat, viewport.NorthEast.Lng, viewport.SouthWest.Lat, viewport.SouthWest.Lng };
            webBrowser.Document.InvokeScript("setViewport", args);
            webBrowser.Show();
        }

        private static GeoResponse CallGeoWS(string address)
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
            var res = (GeoResponse)serializer.ReadObject(stream);
            return res;
        }

        [DataContract]
        class GeoResponse
        {
            [DataMember(Name = "status")]
            public string Status { get; set; }
            [DataMember(Name = "results")]
            public CResult[] Results { get; set; }

            [DataContract]
            public class CResult
            {
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
