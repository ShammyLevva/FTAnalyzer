using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;
using System;
using System.IO;
using System.Windows.Forms;

namespace FTAnalyzer.Forms
{
    public partial class BingOSMap : Form
    {
        public static string COUNTRY = "country";
        public static string ESTABLISHMENT = "establishment";
        public static string ADMIN1 = "administrative_area_level_1";
        public static string ADMIN2 = "administrative_area_level_2";
        public static string ADMIN3 = "administrative_area_level_3";
        public static string LOCALITY = "locality";
        public static string SUBLOCALITY = "sublocality";
        public static string NEIGHBOURHOOD = "neighborhood";
        public static string STREET_ADDRESS = "street_address";
        public static string PREMISE = "premise";
        public static string CEMETERY = "cemetery";
        public static string HOSPITAL = "hospital";
        public static string PLACE_OF_WORSHIP = "place_of_worship";
        public static string ROUTE = "route";
        public static string INTERSECTION = "intersection";

        string location;
        bool loaded;

        public BingOSMap()
        {
            InitializeComponent();
            loaded = false;
            string filename = Path.Combine(Application.StartupPath + @"\Resources\BingOSMaps.htm");
            webBrowser.Navigate(filename);
            webBrowser.Hide();
            Top = Top + NativeMethods.TopTaskbarOffset;
        }

        public bool SetLocation(FactLocation loc, int level)
        {
            while (!loaded)
            {
                Application.DoEvents();
            }
            GeoResponse.CResult.CGeometry.CViewPort viewport = null;
            if (loc.IsGeoCoded(false) && loc.ViewPort != null)
            {
                labMapLevel.Text = "Previously Geocoded: " + loc.ToString();
                viewport = MapTransforms.ReverseTransformViewport(loc.ViewPort);
            }
            else
            {
                location = loc.ToString();
                GeoResponse res = GoogleMap.CallGoogleGeocode(location);
                if (res.Status == "OK")
                {
                    labMapLevel.Text = GoogleMap.LocationText(res, loc, level);
                    viewport = res.Results[0].Geometry.ViewPort;
                }
                else if (res.Status == "OVER_QUERY_LIMIT" && loc.IsGeoCoded(false))
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
            object[] args = new object[] { viewport.NorthEast.Lat, viewport.NorthEast.Long, viewport.SouthWest.Lat, viewport.SouthWest.Long };
            webBrowser.Document.InvokeScript("setBounds", args);
            webBrowser.Show();
            return true;
        }

        void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loaded = true;
            System.Diagnostics.Debug.Print("DocumentCompleted called");
        }

        void LabTOU_Click(object sender, EventArgs e) => webBrowser.Navigate("http://www.microsoft.com/Maps/product/terms.html");

        void BingOSMap_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void BingOSMap_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
