using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FTAnalyzer.Filters;
using System.Data.SQLite;
using System.IO;
using SharpMap.Layers;
using BruTile.Web;
using ProjNet.CoordinateSystems.Transformations;
using ProjNet.CoordinateSystems;
using GeoAPI.CoordinateSystems;
using SharpMap.Data.Providers;
using SharpMap.Data;
using SharpMap.Styles;
using GeoAPI.Geometries;
using GeoAPI.CoordinateSystems.Transformations;
using FTAnalyzer.Events;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using SharpMap.Rendering.Decoration;
using SharpMap.Rendering;

namespace FTAnalyzer.Forms
{
    public partial class TimeLine : Form
    {
        private FamilyTree ft;
        private int minGeoCodedYear;
        private int maxGeoCodedYear;
        private bool formClosing;
        private FeatureDataTable factLocations;
        private VectorLayer factLocationLayer;

        private static IProjectedCoordinateSystem GetEPSG900913(CoordinateSystemFactory csFact)
        {
            List<ProjectionParameter> parameters = new List<ProjectionParameter>();
            parameters.Add(new ProjectionParameter("semi_major", 6378137.0));
            parameters.Add(new ProjectionParameter("semi_minor", 6378137.0));
            parameters.Add(new ProjectionParameter("latitude_of_origin", 0.0));
            parameters.Add(new ProjectionParameter("central_meridian", 0.0));
            parameters.Add(new ProjectionParameter("scale_factor", 1.0));
            parameters.Add(new ProjectionParameter("false_easting", 0.0));
            parameters.Add(new ProjectionParameter("false_northing", 0.0));
            IProjection projection = csFact.CreateProjection("Google Mercator", "mercator_1sp", parameters);
            IGeographicCoordinateSystem wgs84 = csFact.CreateGeographicCoordinateSystem(
                "WGS 84", AngularUnit.Degrees, HorizontalDatum.WGS84, PrimeMeridian.Greenwich,
                new AxisInfo("north", AxisOrientationEnum.North), new AxisInfo("east", AxisOrientationEnum.East)
            );

            IProjectedCoordinateSystem epsg900913 = csFact.CreateProjectedCoordinateSystem("Google Mercator", wgs84, projection, LinearUnit.Metre,
              new AxisInfo("East", AxisOrientationEnum.East), new AxisInfo("North", AxisOrientationEnum.North));
            return epsg900913;
        }

        public TimeLine()
        {
            InitializeComponent();
            ft = FamilyTree.Instance;
            txtLocations.Text = "Already Geocoded " + FactLocation.AllLocations.Count(l => l.IsGeoCoded) + " of " + FactLocation.AllLocations.Count() + " locations";
            txtGoogleWait.Text = string.Empty;
            SetGeoCodedYearRange();
            SetupMap();
            DisplayLocationsForYear(labValue.Text);
        }

        private void SetupMap()
        {
            // Add Google maps layer to map control.
            mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                new GoogleTileSource(GoogleMapType.GoogleMap), "Google"));

            factLocations = new FeatureDataTable();
            factLocations.Columns.Add("Location", typeof(FactLocation));
            factLocations.Columns.Add("Individual", typeof(Individual));
            factLocations.Columns.Add("Relation", typeof(int));
            factLocations.Columns.Add("Label", typeof(string));
            GeometryFeatureProvider factLocationGFP = new GeometryFeatureProvider(factLocations);

            factLocationLayer = new VectorLayer("Locations");
            factLocationLayer.DataSource = factLocationGFP;

            CoordinateTransformationFactory ctFact = new CoordinateTransformationFactory();
            CoordinateSystemFactory csFact = new CoordinateSystemFactory();

            factLocationLayer.CoordinateTransformation = ctFact.CreateFromCoordinateSystems(
                GeographicCoordinateSystem.WGS84,
                GetEPSG900913(csFact));

            factLocationLayer.ReverseCoordinateTransformation = ctFact.CreateFromCoordinateSystems(
                GetEPSG900913(csFact),
                GeographicCoordinateSystem.WGS84);

            Dictionary<int, IStyle> styles = new Dictionary<int, IStyle>();
            VectorStyle blood = new VectorStyle();
            blood.PointColor = new SolidBrush(Color.Red);
            blood.PointSize = 10;
            styles.Add(Individual.BLOOD, blood);
            bloodRelativesToolStripMenuItem.ForeColor = Color.Red;

            VectorStyle direct = new VectorStyle();
            direct.PointColor = new SolidBrush(Color.ForestGreen);
            direct.PointSize = 10;
            styles.Add(Individual.DIRECT, direct);
            directAncestorsToolStripMenuItem.ForeColor = Color.ForestGreen;

            VectorStyle marriage = new VectorStyle();
            marriage.PointColor = new SolidBrush(Color.Pink);
            marriage.PointSize = 10;
            styles.Add(Individual.MARRIAGE, marriage);
            relatedByMarriageToolStripMenuItem.ForeColor = Color.Pink;

            VectorStyle marriagedb = new VectorStyle();
            marriagedb.PointColor = new SolidBrush(Color.MediumBlue);
            marriagedb.PointSize = 10;
            styles.Add(Individual.MARRIEDTODB, marriagedb);
            marriedToDirectOrBloodToolStripMenuItem.ForeColor = Color.MediumBlue;

            VectorStyle unknown = new VectorStyle();
            unknown.PointColor = new SolidBrush(Color.Black);
            unknown.PointSize = 10;
            styles.Add(Individual.UNKNOWN, unknown);
            unknownToolStripMenuItem.ForeColor = Color.Black;

            factLocationLayer.Theme = new SharpMap.Rendering.Thematics.UniqueValuesTheme<int>("Relation", styles, unknown);
            mapBox1.Map.Layers.Add(factLocationLayer);

            LabelLayer labelLayer = new LabelLayer("Label");
             labelLayer.CoordinateTransformation = ctFact.CreateFromCoordinateSystems(
                GeographicCoordinateSystem.WGS84,
                GetEPSG900913(csFact));

            labelLayer.ReverseCoordinateTransformation = ctFact.CreateFromCoordinateSystems(
                GetEPSG900913(csFact),
                GeographicCoordinateSystem.WGS84);
            labelLayer.DataSource = factLocationGFP;
            labelLayer.Enabled = true;
            //Specifiy field that contains the label string.
            labelLayer.LabelColumn = "Label";
            labelLayer.Style = new LabelStyle();
            labelLayer.Style.ForeColor = Color.Black;
            labelLayer.Style.Font = new Font(FontFamily.GenericSerif, 11);
            labelLayer.Style.HorizontalAlignment = LabelStyle.HorizontalAlignmentEnum.Left;
            labelLayer.Style.VerticalAlignment = LabelStyle.VerticalAlignmentEnum.Bottom;
            labelLayer.Style.CollisionDetection = true;
            //labelLayer.Style.CollisionBuffer = new SizeF(5, 5);
            labelLayer.LabelFilter = LabelCollisionDetection.ThoroughCollisionDetection;
            //labelLayer.MultipartGeometryBehaviour = LabelLayer.MultipartGeometryBehaviourEnum.Largest;
            labelLayer.Style.Offset = new PointF(3, 3);
            labelLayer.Style.Halo = new Pen(Color.LightGray, 2);
            labelLayer.TextRenderingHint = TextRenderingHint.AntiAlias;
            labelLayer.SmoothingMode = SmoothingMode.AntiAlias;
            mapBox1.Map.Layers.Add(labelLayer);

            mapBox1.Map.Decorations.Add(new GoogleMapsDisclaimer());
            mapBox1.Map.ZoomToExtents();
            mapBox1.Refresh();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        #region Geocoding

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
            this.Cursor = Cursors.WaitCursor;
            StartGeoCoding();
            this.Cursor = Cursors.Default;
        }

        public void StartGeoCoding()
        {
            pbGeocoding.Visible = true;
            backgroundWorker.RunWorkerAsync();
        }

        public void GeoCode(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                GoogleMap.WaitingForGoogle += new GoogleMap.GoogleEventHandler(GoogleMap_WaitingForGoogle);
                SQLiteConnection conn = ft.GetDatabaseConnection();
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand("select location from geocode where location = ?", conn);
                SQLiteParameter param = cmd.CreateParameter();
                param.DbType = DbType.String;
                cmd.Parameters.Add(param);
                cmd.Prepare();

                SQLiteCommand insertCmd = new SQLiteCommand("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel) values(?,?,?,?,date('now'),?,?)", conn);

                param = insertCmd.CreateParameter();
                param.DbType = DbType.String;
                insertCmd.Parameters.Add(param);

                param = insertCmd.CreateParameter();
                param.DbType = DbType.Int32;
                insertCmd.Parameters.Add(param);

                param = insertCmd.CreateParameter();
                param.DbType = DbType.Double;
                insertCmd.Parameters.Add(param);

                param = insertCmd.CreateParameter();
                param.DbType = DbType.Double;
                insertCmd.Parameters.Add(param);

                param = insertCmd.CreateParameter();
                param.DbType = DbType.String;
                insertCmd.Parameters.Add(param);

                param = insertCmd.CreateParameter();
                param.DbType = DbType.Int32;
                insertCmd.Parameters.Add(param);

                insertCmd.Prepare();

                int count = 0;
                int good = 0;
                int bad = 0;
                int total = FactLocation.AllLocations.Count();
                GoogleMap.ThreadCancelled = false;

                foreach (FactLocation loc in FactLocation.AllLocations)
                {
                    if (!loc.IsGeoCoded)
                    {
                        cmd.Parameters[0].Value = loc.ToString();
                        SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                        if (!reader.Read() && loc.ToString().Length > 0)
                        {  // location isn't found so add it
                            GoogleMap.GeoResponse res = GoogleMap.CallGeoWSCount(loc.ToString(), 10);
                            if (res != null && res.Status == "OK" && res.Results.Length > 0)
                            {
                                double latitude = 0;
                                double longitude = 0;
                                string address = string.Empty;
                                int foundLevel = GoogleMap.GetFactLocation(res.Results[0].Types);
                                address = res.Results[0].ReturnAddress;
                                if (foundLevel >= loc.Level)
                                {
                                    latitude = res.Results[0].Geometry.Location.Lat;
                                    longitude = res.Results[0].Geometry.Location.Lng;
                                    good++;
                                }
                                else
                                {
                                    bad++;
                                }
                                insertCmd.Parameters[0].Value = loc.ToString();
                                insertCmd.Parameters[1].Value = loc.Level;
                                insertCmd.Parameters[2].Value = latitude;
                                insertCmd.Parameters[3].Value = longitude;
                                insertCmd.Parameters[4].Value = address;
                                insertCmd.Parameters[5].Value = foundLevel;
                                insertCmd.ExecuteNonQuery();
                                loc.Latitude = latitude;
                                loc.Longitude = longitude;
                                loc.GoogleLocation = address;
                            }
                        }
                        reader.Close();
                    }

                    count++;
                    int percent = (int)Math.Truncate(count * 100.0 / total);
                    string status = "Google found " + good + ", didn't find " + bad + " places. Geocoded " +
                            FactLocation.AllLocations.Count(l => l.IsGeoCoded) + " locations. " + count +
                            " of " + total + ".  ";
                    worker.ReportProgress(percent, status);

                    if (worker.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
                conn.Close();
                MessageBox.Show("Finished Geocoding");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error geocoding : " + ex.Message);
            }

        }

        #endregion

        public void DisplayLocationsForYear(string year)
        {
            int result = 0;
            int.TryParse(year, out result);
            if (year.Length == 4 && result != 0)
            {
                FactDate yearDate = new FactDate(year);
                List<MapLocation> locations = ft.AllIndividualLocations(yearDate);
                factLocations.Clear();
                Envelope box = new Envelope();
                bool updated = false;
                foreach (MapLocation loc in locations)
                {
                    if (RelationIncluded(loc.Individual.RelationType))
                    {
                        FeatureDataRow r = factLocations.NewRow();
                        r["Location"] = loc.Location;
                        r["Individual"] = loc.Individual;
                        r["Relation"] = loc.Individual.RelationType;
                        r["Label"] = loc.Individual.Name + " at " + loc.Location;
                        r.Geometry = new NetTopologySuite.Geometries.Point(loc.Location.Longitude, loc.Location.Latitude);
                        factLocations.AddRow(r);
                        if (!box.Covers(loc.Location.Longitude, loc.Location.Latitude))
                        {
                            updated = true;
                            box.ExpandToInclude(loc.Location.Longitude, loc.Location.Latitude);
                        }
                    }
                }
                if (updated)
                {
                    IMathTransform transform = factLocationLayer.CoordinateTransformation.MathTransform;
                    box = new Envelope(transform.Transform(box.TopLeft()), transform.Transform(box.BottomRight()));
                    box.ExpandBy(mapBox1.Map.PixelSize * 5);
                    mapBox1.Map.ZoomToBox(box);
                }
                mapBox1.Refresh();
            }
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
            this.Cursor = Cursors.Default;
        }

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            GeoCode(backgroundWorker, e);
        }

        private void backgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            pbGeocoding.Value = e.ProgressPercentage;
            txtLocations.Text = (string)e.UserState;
        }

        private void backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbGeocoding.Value = 100;
            txtGoogleWait.Text = string.Empty;
            if (formClosing) this.Close();
        }

        private void TimeLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                GoogleMap.ThreadCancelled = true;
                e.Cancel = true;
                formClosing = true;
            }
        }

        public void GoogleMap_WaitingForGoogle(object sender, GoogleWaitingEventArgs args)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => GoogleMap_WaitingForGoogle(sender, args)));
                return;
            }
            txtGoogleWait.Text = args.Message;
        }

        private void directAncestorsToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            DisplayLocationsForYear(labValue.Text);
        }

        private void bloodRelativesToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            DisplayLocationsForYear(labValue.Text);
        }

        private void marriedToDirectOrBloodToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            DisplayLocationsForYear(labValue.Text);
        }

        private void relatedByMarriageToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            DisplayLocationsForYear(labValue.Text);
        }

        private void unknownToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            DisplayLocationsForYear(labValue.Text);
        }

        private void mapBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                mapBox1.Map.Zoom -= mapBox1.Map.Zoom * 0.3;
            else if (e.Button == MouseButtons.Right)
                mapBox1.Map.Zoom += mapBox1.Map.Zoom * 0.3;
            
            Coordinate p = mapBox1.Map.ImageToWorld(new PointF(e.X, e.Y));
            mapBox1.Map.Center.X = p.X;
            mapBox1.Map.Center.Y = p.Y;
            mapBox1.Refresh();
        }

    }
}
