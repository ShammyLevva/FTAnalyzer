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
using SharpMap.Rendering.Decoration.ScaleBar;

namespace FTAnalyzer.Forms
{
    public partial class TimeLine : Form
    {
        private FamilyTree ft;
        private int minGeoCodedYear;
        private int maxGeoCodedYear;
        private int geocodedRange;
        private int yearLimit;
        private FeatureDataTable factLocations;
        private VectorLayer clusterLayer;
        private LabelLayer labelLayer;
        private MarkerClusterer clusterer;
        private Color backgroundColour;

        public TimeLine()
        {
            InitializeComponent();
            mapZoomToolStrip.Renderer = new CustomToolStripRenderer();
            tbYears.MouseWheel += new MouseEventHandler(tbYears_MouseWheel);
            mapZoomToolStrip.Items[2].ToolTipText = "Zoom out of Map"; // fix bug in SharpMapUI component
            mapZoomToolStrip.Items[4].ToolTipText = "Draw rectangle by dragging mouse to specify zoom area";
            for(int i = 7; i <=10; i++)
                mapZoomToolStrip.Items[i].Visible = false;
            backgroundColour = mapZoomToolStrip.Items[0].BackColor;
            mapBox1.Map.MapViewOnChange += new SharpMap.Map.MapViewChangedHandler(mapBox1_MapViewOnChange);
            ft = FamilyTree.Instance;
            cbLimitFactDates.Text = "No Limit";
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
            mapBox1.QueryGrowFactor = 30;
            ScaleBar scalebar = new ScaleBar();
            scalebar.BackgroundColor = Color.White;
            mapBox1.Map.Decorations.Add(scalebar);
            mapBox1.Map.ZoomToExtents();
            mapBox1.Refresh();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void SetGeoCodedYearRange()
        {
            minGeoCodedYear = FactDate.MAXDATE.Year;
            maxGeoCodedYear = FactDate.MINDATE.Year;
            List<MapLocation> yearRange = FilterToRelationsIncluded(ft.AllMapLocations);
            foreach (MapLocation ml in yearRange)
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
                geocodedRange = 0;
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
                geocodedRange = maxGeoCodedYear - minGeoCodedYear;
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
                List<MapLocation> locations;
                if (result == 9999)
                {
                    locations = FilterToRelationsIncluded(ft.AllMapLocations);
                    txtLocations.Text = locations.Count() + " Locations in total (you may need to zoom to see them all)";
                }
                else
                {
                    locations = FilterToRelationsIncluded(ft.YearMapLocations(new FactDate(year), yearLimit));
                    txtLocations.Text = locations.Count() + " Locations in total for year " + year + "  (you may need to zoom to see them all)";
                }
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
                    Envelope expand;
                    if (bbox.Centre == null)
                        expand = new Envelope(-25000000, 25000000, -17000000, 17000000);
                    else
                        expand = new Envelope(transform.Transform(bbox.TopLeft()), transform.Transform(bbox.BottomRight()));
                    mapBox1.Map.ZoomToBox(expand);
                    expand.ExpandBy(mapBox1.Map.PixelSize * 20);
                    mapBox1.Map.ZoomToBox(expand);
                }
                RefreshTimeline();
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
            UpdateMap();
        }

        private void UpdateMap()
        {
            this.Cursor = Cursors.WaitCursor;
            labValue.Text = tbYears.Value.ToString();
            if (mnuDisableTimeline.Checked)
                DisplayLocationsForYear("9999");
            else
                DisplayLocationsForYear(labValue.Text);
            this.Cursor = Cursors.Default;
        }

        private void tbYears_MouseWheel(object sender, EventArgs e)
        {
            // do nothing if using mousewheel this prevents year scrolling when map should scroll
        }

        private void Relations_CheckedChanged(object sender, EventArgs e)
        {
            SetGeoCodedYearRange(); // need to refresh range of years when filters change
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
                RefreshTimeline();
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
            this.Cursor = Cursors.WaitCursor;
            List<MapLocation> locations = new List<MapLocation>();
            foreach (FeatureDataRow row in data)
            {
                IList<FeatureDataRow> features = (List<FeatureDataRow>)row["Features"];
                foreach (FeatureDataRow feature in features)
                {
                    locations.Add((MapLocation)feature["MapLocation"]);
                }
            }
            MapIndividuals ind = new MapIndividuals(locations, labValue.Text, this);
            ind.Show();
            this.Cursor = Cursors.Default;
        }

        private void mapBox1_MapViewOnChange()
        {
            RefreshClusters();
        }

        private void mapBox1_MapZoomChanged(double zoom)
        {
            RefreshTimeline();
        }

        public void RefreshTimeline()
        {
            RefreshClusters();
            mapBox1.Refresh();
        }

        private void mapBox1_MapCenterChanged(Coordinate center)
        {
            RefreshTimeline();
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            btnPlay.Visible = false;
            btnStop.Visible = true;
            timer.Enabled = true;
            txtTimeInterval.Enabled = false;
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            StopTimer();
        }

        private void btnPause_Click(object sender, EventArgs e)
        {
            StopTimer();
        }

        private void StopTimer()
        {
            btnPlay.Visible = true;
            btnStop.Visible = false;
            timer.Enabled = false;
            txtTimeInterval.Enabled = true;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (tbYears.Value < tbYears.Maximum)
            {
                tbYears.Value++;
                UpdateMap();
            }
            else
                StopTimer();
        }

        private void txtTimeInterval_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == '\b')
                e.Handled = false;
            else
                e.Handled = true;
        }

        private void txtTimeInterval_Validated(object sender, EventArgs e)
        {
            int result;
            if (Int32.TryParse(txtTimeInterval.Text, out result))
            {
                timer.Interval = result;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            btnSelect.Checked = true;
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.QueryPoint;
        }

        private void mapBox1_ActiveToolChanged(SharpMap.Forms.MapBox.Tools tool)
        {
            if (mapBox1.ActiveTool != SharpMap.Forms.MapBox.Tools.QueryPoint)
                btnSelect.Checked = false;
        }

        private void btnBack10_Click(object sender, EventArgs e)
        {
            int step = 10;
            if (geocodedRange <= 150)
                step = 5;
            if (tbYears.Value < tbYears.Minimum + step)
                tbYears.Value = tbYears.Minimum;
            else
                tbYears.Value -= step;
            UpdateMap();
        }

        private void btnBack1_Click(object sender, EventArgs e)
        {
            if (tbYears.Value != tbYears.Minimum)
                tbYears.Value -= 1;
            UpdateMap();
        }

        private void btnForward1_Click(object sender, EventArgs e)
        {
            if (tbYears.Value != tbYears.Maximum)
                tbYears.Value += 1;
            UpdateMap();
        }

        private void btnForward10_Click(object sender, EventArgs e)
        {
            int step = 10;
            if (geocodedRange <= 150)
                step = 5;
            if (tbYears.Value > tbYears.Maximum - step)
                tbYears.Value = tbYears.Maximum;
            else
                tbYears.Value += step;
            UpdateMap();
        }

        private void mnuDisableTimeline_Click(object sender, EventArgs e)
        {
            tbYears.Visible = !mnuDisableTimeline.Checked;
            btnBack1.Visible = !mnuDisableTimeline.Checked;
            btnBack10.Visible = !mnuDisableTimeline.Checked;
            btnForward1.Visible = !mnuDisableTimeline.Checked;
            btnForward10.Visible = !mnuDisableTimeline.Checked;
            btnPlay.Visible = !mnuDisableTimeline.Checked;
            btnStop.Visible = !mnuDisableTimeline.Checked;
            labValue.Visible = !mnuDisableTimeline.Checked;
            labMin.Visible = !mnuDisableTimeline.Checked;
            labMax.Visible = !mnuDisableTimeline.Checked;
            toolStripLabel1.Visible = !mnuDisableTimeline.Checked;
            toolStripLabel2.Visible = !mnuDisableTimeline.Checked;
            txtTimeInterval.Visible = !mnuDisableTimeline.Checked;
            mnuLimitFactDates.Enabled = !mnuDisableTimeline.Checked;
            mnuKeepZoom.Enabled = !mnuDisableTimeline.Checked;
            if (mnuDisableTimeline.Checked)
                StopTimer(); // make sure we aren't playing timeline if we disable timeline
            txtLocations.Text = string.Empty; // set empty so looks better during redraw
            Application.DoEvents(); // force screen refresh
            UpdateMap();
        }

        private void cbLimitFactDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            yearLimit = GetYearLimit();
            UpdateMap();
        }

        private int GetYearLimit()
        {
            //check the 
            switch (cbLimitFactDates.Text)
            {
                case "No Limit":
                    return int.MaxValue;
                case "Exact year only":
                    return 1;
                case "+/- 2 years":
                    return 2;
                case "+/- 5 years":
                    return 5;
                case "+/-10 years":
                    return 10;
                case "+/-20 years":
                    return 20;
                case "+/-50 years":
                    return 50;
                case "+/-100 years":
                    return 100;
                default:
                    return int.MaxValue;
            }
        }
    }
}
