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
using FTAnalyzer.Utilities;
using FTAnalyzer.Mapping;

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
            string filename = Path.Combine(Application.StartupPath + @"\Resources\BingOSMaps.htm");
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
            if (loc.IsGeoCoded && loc.ViewPort != null)
            {
                labMapLevel.Text = "Previously Geocoded: " + loc.ToString();
                viewport = loc.ViewPort;
            }
            else
            {
                location = loc.ToString();
                GeoResponse res = GoogleMap.CallGeoWS(location);
                if (res.Status == "OK")
                {
                    labMapLevel.Text = GoogleMap.LocationText(res, loc, level);
                    viewport = res.Results[0].Geometry.ViewPort;
                }
                else if (res.Status == "OVER_QUERY_LIMIT" && loc.IsGeoCoded)
                {
                    labMapLevel.Text = "Previously Geocoded: " + loc.ToString();
                    viewport = new GeoResponse.CResult.CGeometry.CViewPort();
                    viewport.NorthEast.Lat = loc.Latitude + 2;
                    viewport.NorthEast.Long = loc.Longitude + 2;
                    viewport.SouthWest.Lat = loc.Latitude + 2;
                    viewport.SouthWest.Long = loc.Longitude + 2;
                }
                else
                {
                    return false;
                }
            }
            Object[] args = new Object[] { viewport.NorthEast.Lat, viewport.NorthEast.Long, viewport.SouthWest.Lat, viewport.SouthWest.Long };
            webBrowser.Document.InvokeScript("setBounds", args);
            webBrowser.Show();
            return true;
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loaded = true;
            System.Diagnostics.Debug.Print("DocumentCompleted called");
        }

        private void labTOU_Click(object sender, EventArgs e)
        {
            webBrowser.Navigate("http://www.microsoft.com/Maps/product/terms.html");
        }

    }
}
