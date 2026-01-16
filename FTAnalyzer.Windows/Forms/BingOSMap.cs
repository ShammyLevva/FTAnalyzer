using FTAnalyzer.Mapping;
using FTAnalyzer.Utilities;

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

        bool loaded;

        public BingOSMap()
        {
            InitializeComponent();
            loaded = false;
            string filename = Path.Combine(Application.StartupPath + @"\Resources\BingOSMaps.htm");
            webBrowser.Navigate(new Uri(filename));
            webBrowser.Hide();
            Top += NativeMethods.TopTaskbarOffset;
        }

        public bool SetLocation(FactLocation loc, int level)
        {
            if (loc is null) return false;
            while (!loaded)
            {
                Application.DoEvents();
            }
            GeoResponse.CResult.CGeometry.CViewPort viewport;
            if (loc.IsGeoCoded(false) && loc.ViewPort is not null)
            {
                labMapLevel.Text = $"Previously Geocoded: {loc}";
                viewport = MapTransforms.ReverseTransformViewport(loc.ViewPort);
            }
            else
            {
                GeoResponse? res = GoogleMap.CallGoogleGeocode(loc, loc.ToString());
                if (res?.Status == "OK")
                {
                    labMapLevel.Text = GoogleMap.LocationText(res, loc, level);
                    viewport = res.Results[0].Geometry.ViewPort;
                }
                else if (res?.Status == "OVER_QUERY_LIMIT" && loc.IsGeoCoded(false))
                {
                    labMapLevel.Text = $"Previously Geocoded: {loc}";
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
            object[] args = [viewport.NorthEast.Lat, viewport.NorthEast.Long, viewport.SouthWest.Lat, viewport.SouthWest.Long];
            webBrowser.Document.InvokeScript("setBounds", args);
            webBrowser.Show();
            return true;
        }

        void WebBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            loaded = true;
            System.Diagnostics.Debug.Print("DocumentCompleted called");
        }

        void LabTOU_Click(object sender, EventArgs e) => webBrowser.Navigate(new Uri("https://www.microsoft.com/Maps/product/terms.html"));

        void BingOSMap_FormClosed(object sender, FormClosedEventArgs e) => Dispose();

        void BingOSMap_Load(object sender, EventArgs e) => SpecialMethods.SetFonts(this);
    }
}
