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
    public partial class BingOSMap : Form
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

        public BingOSMap()
        {
            InitializeComponent();
            loaded = false;
            string filename = Application.StartupPath + "\\Resources\\BingOSMaps.htm";
            webBrowser.Navigate(filename);
            webBrowser.Hide();
        }

        public bool setLocation(FactLocation loc, int level)
        {
            while (!loaded)
            {
                Application.DoEvents();
            }
            location = loc.ToString();
            GoogleMap.GeoResponse res = GoogleMap.CallGeoWS(location);
            if (res.Status != "OK")
            {
                return false;
            }

            labMapLevel.Text = GoogleMap.locationText(res, loc, level);
            var viewport = res.Results[0].Geometry.ViewPort;
            Object[] args = new Object[] { viewport.NorthEast.Lat, viewport.NorthEast.Lng, viewport.SouthWest.Lat, viewport.SouthWest.Lng };
            webBrowser.Document.InvokeScript("setBounds", args);
            webBrowser.Show();
            return true;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loaded = true;
            System.Diagnostics.Debug.Print("DocumentCompleted called");
        }

    }
}
