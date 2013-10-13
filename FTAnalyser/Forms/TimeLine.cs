using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Web;
using System.Windows.Forms;
using BruTile.Web;
using FTAnalyzer.Mapping;
using GeoAPI.CoordinateSystems.Transformations;
using GeoAPI.Geometries;
using SharpMap.Data;
using SharpMap.Data.Providers;
using SharpMap.Layers;
using SharpMap.Styles;

namespace FTAnalyzer.Forms
{
    public partial class TimeLine : Form
    {
        private FamilyTree ft;
        private int minGeoCodedYear;
        private int maxGeoCodedYear;
        private FeatureDataTable factLocations;
        private VectorLayer clusterLayer;
        private LabelLayer labelLayer;
        private MarkerClusterer clusterer;
        private Color backgroundColour;

        public TimeLine()
        {
            InitializeComponent();
            tbYears.MouseWheel += new MouseEventHandler(tbYears_MouseWheel);
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[10].Visible = false;
            backgroundColour = mapZoomToolStrip.Items[0].BackColor; 
            mapBox1.Map.MapViewOnChange += new SharpMap.Map.MapViewChangedHandler(mapBox1_MapViewOnChange);
            ft = FamilyTree.Instance;
            CheckIfGeocodingNeeded();
        }

        private void CheckIfGeocodingNeeded()
        {
            int notsearched = (FactLocation.AllLocations.Count(x => x.GeocodeStatus.Equals(FactLocation.Geocode.NOT_SEARCHED)) - 1);
            if (notsearched > 0)
            {
                DialogResult res = MessageBox.Show("You have " + notsearched + " places with no map location do you want to search Google for the locations?",
                                                   "Geocode Locations", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                    StartGeocoding();
            }
        }

        private void StartGeocoding()
        {
            if (!ft.Geocoding) // don't geocode if another geocode session in progress
            {
                this.Cursor = Cursors.WaitCursor;
                GeocodeLocations geo = new GeocodeLocations();
                geo.Show();
                MainForm.DisposeDuplicateForms(geo);
                geo.StartGeoCoding();
                geo.BringToFront();
                geo.Focus();
                this.Cursor = Cursors.Default;
            }
        }

        private void SetupMap()
        {
            // Add Google maps layer to map control.
            HttpUtility.SetDefaultProxy();
            mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                new GoogleTileSource(GoogleMapType.GoogleMap), "GoogleMap"));

            factLocations = new FeatureDataTable();
            factLocations.Columns.Add("MapLocation", typeof(MapLocation));
            factLocations.Columns.Add("Label", typeof(string));

            clusterer = new MarkerClusterer(factLocations);
            GeometryFeatureProvider factLocationGFP = new GeometryFeatureProvider(clusterer.FeatureDataTable);

            clusterLayer = new VectorLayer("Clusters");
            clusterLayer.DataSource = factLocationGFP;
            clusterLayer.CoordinateTransformation = MapTransforms.Transform();
            clusterLayer.ReverseCoordinateTransformation = MapTransforms.ReverseTransform();

            Dictionary<string, IStyle> styles = new Dictionary<string, IStyle>();

            VectorStyle feature = new VectorStyle();
            feature.PointColor = new SolidBrush(Color.Red);
            feature.PointSize = 20;
            feature.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\teardrop_blue.png"));
            feature.SymbolOffset = new PointF(0.0f, -17.0f);
            styles.Add(MapCluster.FEATURE, feature);

            VectorStyle cluster = new VectorStyle();
            cluster.PointColor = new SolidBrush(Color.ForestGreen);
            cluster.PointSize = 20;
            cluster.Symbol = Image.FromFile(Path.Combine(Application.StartupPath, @"Resources\Icons\people35.png"));
            styles.Add(MapCluster.CLUSTER, cluster);

            VectorStyle unknown = new VectorStyle();
            unknown.PointColor = new SolidBrush(Color.Black);
            unknown.PointSize = 10;
            styles.Add(MapCluster.UNKNOWN, unknown);
            
            clusterLayer.Theme = new SharpMap.Rendering.Thematics.UniqueValuesTheme<string>("Cluster", styles, unknown);
            mapBox1.Map.Layers.Add(clusterLayer);

            labelLayer = new LabelLayer("Label");
            labelLayer.CoordinateTransformation = MapTransforms.Transform();
            labelLayer.ReverseCoordinateTransformation = MapTransforms.ReverseTransform();
            labelLayer.DataSource = factLocationGFP;
            labelLayer.Enabled = true;
            //Specifiy field that contains the label string.
            labelLayer.LabelColumn = "Label";
            labelLayer.TextRenderingHint = TextRenderingHint.AntiAlias;
            labelLayer.SmoothingMode = SmoothingMode.AntiAlias;
            LabelStyle style = new LabelStyle();
            style.ForeColor = Color.Black;
            style.Font = new Font(FontFamily.GenericSerif, 14, FontStyle.Bold);
            style.HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Center;
            style.VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Bottom;
            style.CollisionDetection = true;
            style.Offset = new PointF(2, 22);
            style.Halo = new Pen(Color.Yellow, 3);
            labelLayer.Style = style;
            mapBox1.Map.Layers.Add(labelLayer);

            mapBox1.Map.MinimumZoom = 1000;
            mapBox1.Map.MaximumZoom = 50000000;
            //mapBox1.Map.Decorations.Add(new GoogleMapsDisclaimer());
            mapBox1.Map.ZoomToExtents();
            mapBox1.Refresh();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void SetGeoCodedYearRange()
        {
            minGeoCodedYear = FactDate.MAXDATE.Year;
            maxGeoCodedYear = FactDate.MINDATE.Year;
            foreach (MapLocation ml in ft.AllMapLocations)
            {
                if (ml.Location.IsGeoCoded && ml.FactDate.IsKnown)
                {
                    if (ml.FactDate.StartDate != FactDate.MINDATE && ml.FactDate.StartDate.Year < minGeoCodedYear)
                        minGeoCodedYear = ml.FactDate.StartDate.Year;
                    if (ml.FactDate.EndDate != FactDate.MAXDATE && ml.FactDate.EndDate.Year > maxGeoCodedYear)
                        maxGeoCodedYear = ml.FactDate.EndDate.Year;
                }
            }
            if (minGeoCodedYear == FactDate.MAXDATE.Year || maxGeoCodedYear == FactDate.MINDATE.Year)
            {
                tbYears.Enabled = false;
                labMin.Text = string.Empty;
                labMax.Text = string.Empty;
                labValue.Text = string.Empty;
            }
            else
            {
                tbYears.Enabled = true;
                tbYears.Minimum = minGeoCodedYear;
                tbYears.Maximum = maxGeoCodedYear;
                tbYears.Value = minGeoCodedYear + (maxGeoCodedYear - minGeoCodedYear) / 2;
                labMin.Text = minGeoCodedYear.ToString();
                labMax.Text = maxGeoCodedYear.ToString();
                labValue.Text = tbYears.Value.ToString();
            }
        }

        private void geocodeLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StartGeocoding();
        }

        public void DisplayLocationsForYear(string year)
        {
            int result = 0;
            int.TryParse(year, out result);
            if (year.Length == 4 && result != 0)
            {
                FactDate yearDate = new FactDate(year);
                List<MapLocation> locations = FilterToRelationsIncluded(ft.YearMapLocations(yearDate));
                txtLocations.Text = locations.Count() + " Locations";
                factLocations.Clear();
                Envelope bbox = new Envelope();
                foreach (MapLocation loc in locations)
                {
                    FeatureDataRow row = loc.AddFeatureDataRow(factLocations);
                    bbox.ExpandToInclude(row.Geometry.Coordinate);
                }
                if (!mnuKeepZoom.Checked)
                {
                    IMathTransform transform = clusterLayer.CoordinateTransformation.MathTransform;
                    bbox = new Envelope(transform.Transform(bbox.TopLeft()), transform.Transform(bbox.BottomRight()));
                    mapBox1.Map.ZoomToBox(bbox);
                    bbox.ExpandBy(mapBox1.Map.PixelSize * 20);
                    mapBox1.Map.ZoomToBox(bbox);
                    RefreshClusters();
                }
                mapBox1.Refresh();
            }
        }

        private List<MapLocation> FilterToRelationsIncluded(List<MapLocation> locations)
        {
            List<MapLocation> result = new List<MapLocation>();
            foreach (MapLocation ml in locations)
                if (RelationIncluded(ml.Individual.RelationType))
                    result.Add(ml);
            return result;
        }

        private bool RelationIncluded(int relationtype)
        {
            switch (relationtype)
            {
                case Individual.DIRECT:
                    return directAncestorsToolStripMenuItem.Checked;
                case Individual.BLOOD:
                    return bloodRelativesToolStripMenuItem.Checked;
                case Individual.MARRIAGE:
                    return relatedByMarriageToolStripMenuItem.Checked;
                case Individual.MARRIEDTODB:
                    return marriedToDirectOrBloodToolStripMenuItem.Checked;
                case Individual.UNKNOWN:
                default:
                    return unknownToolStripMenuItem.Checked;
            }
        }

        private void tbYears_Scroll(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            labValue.Text = tbYears.Value.ToString();
            DisplayLocationsForYear(labValue.Text);
            RefreshClusters();
            mapBox1.Refresh();
            this.Cursor = Cursors.Default;
        }

        private void tbYears_MouseWheel(object sender, EventArgs e)
        {
            // do nothing if using mousewheel this prevents year scrolling when map should scroll
        }

        private void Relations_CheckedChanged(object sender, EventArgs e)
        {
            DisplayLocationsForYear(labValue.Text);
        }

        private void mapBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            bool zoomed = false;
            if (e.Button == MouseButtons.Left && mapBox1.Map.Zoom > mapBox1.Map.MinimumZoom)
            {
                zoomed = true;
                mapBox1.Map.Zoom -= mapBox1.Map.Zoom * 0.5;
            }
            else if (e.Button == MouseButtons.Right && mapBox1.Map.Zoom < mapBox1.Map.MaximumZoom)
            {
                zoomed = true;
                mapBox1.Map.Zoom += mapBox1.Map.Zoom * 0.5;
            }
            if (zoomed)
            {
                Coordinate p = mapBox1.Map.ImageToWorld(new PointF(e.X, e.Y));
                mapBox1.Map.Center.X = p.X;
                mapBox1.Map.Center.Y = p.Y;
                RefreshClusters();
                mapBox1.Refresh();
            }
        }

        private void RefreshClusters()
        {
            Envelope env = mapBox1.Map.Envelope;
            IMathTransform transform = clusterLayer.ReverseCoordinateTransformation.MathTransform;
            env = new Envelope(transform.Transform(env.TopLeft()), transform.Transform(env.BottomRight()));
            clusterer.Recluster(Math.Max(env.Width, env.Height) / 35.0);
        }

        private void TimeLine_Load(object sender, EventArgs e)
        {
            SetGeoCodedYearRange();
            SetupMap();
            DisplayLocationsForYear(labValue.Text);
        }

        private void mapBox1_MapZoomChanged(double zoom)
        {
            RefreshClusters();
            mapBox1.Refresh();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.QueryPoint;
        }

        private void googleMapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            mapBox1.Map.BackgroundLayer.RemoveAt(0);
            if (sender == menuMap)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                        new GoogleTileSource(GoogleMapType.GoogleMap), "GoogleMap"));
                menuSatellite.Checked = false;
            }
            else if (sender == menuSatellite)
            {
                mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                        new GoogleTileSource(GoogleMapType.GoogleSatellite), "GoogleSatellite"));
                menuMap.Checked = false;
            }

        }

        private void mapBox1_MapQueried(FeatureDataTable data)
        {
            List<MapLocation> locations = new List<MapLocation>();
            foreach (FeatureDataRow row in data)
            {
                IList<FeatureDataRow> features = (List<FeatureDataRow>)row["Features"];
                foreach (FeatureDataRow feature in features)
                {
                    locations.Add((MapLocation)feature["MapLocation"]);
                }
            }
            MapIndividuals ind = new MapIndividuals(locations, labValue.Text);
            ind.Show();
        }

        private void mapBox1_MapViewOnChange()
        {
            RefreshClusters();
        }

        private void mapBox1_MapCenterChanged(Coordinate center)
        {
            RefreshClusters();
            mapBox1.Refresh();
        }

        private void playTimelineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbYears.Value = tbYears.Minimum;
            while(tbYears.Value < tbYears.Maximum)
            {
                tbYears.Value++;
                // wait for a bit before moving onto next year. Wait needs to be under user control
                // ie: user must be able to stop the playing of timeline
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = false;
            btnStop.Visible = true;
            btnPause.Enabled = true;
            timer.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = true;
            btnStop.Visible = false;
            btnPause.Enabled = false;
            timer.Stop();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = true;
            btnStop.Visible = false;
            btnPause.Enabled = false;
        }
    }
}
