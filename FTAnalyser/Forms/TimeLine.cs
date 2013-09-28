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

namespace FTAnalyzer.Forms
{
    public partial class TimeLine : Form
    {
        private FamilyTree ft;
        private int minGeoCodedYear;
        private int maxGeoCodedYear;
        private bool formClosing;
        private FeatureDataTable factLocations;

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
            LoadGeoLocationsFromDataBase();
            txtLocations.Text = "Already Geocoded " + ft.AllLocations.Count(l => l.IsGeoCoded) + " of " + ft.AllLocations.Count() + " locations";
            txtGoogleWait.Text = string.Empty;
            SetGeoCodedYearRange();

            // Add Google maps layer to map control.
            mapBox1.Map.BackgroundLayer.Add(new TileAsyncLayer(
                new GoogleTileSource(GoogleMapType.GoogleMap), "Google"));

            factLocations = new FeatureDataTable();
            VectorLayer factLocationLayer = new VectorLayer("Locations");
            factLocationLayer.DataSource = new GeometryFeatureProvider(factLocations);

            CoordinateTransformationFactory ctFact = new CoordinateTransformationFactory();
            CoordinateSystemFactory csFact = new CoordinateSystemFactory();

            factLocationLayer.CoordinateTransformation = ctFact.CreateFromCoordinateSystems(
                GeographicCoordinateSystem.WGS84,
                GetEPSG900913(csFact));

            factLocationLayer.ReverseCoordinateTransformation = ctFact.CreateFromCoordinateSystems(
                GetEPSG900913(csFact),
                GeographicCoordinateSystem.WGS84);

            VectorStyle s = new VectorStyle();
            s.PointColor = new SolidBrush(Color.Red);
            s.PointSize = 24;
            factLocationLayer.Style = s;

            mapBox1.Map.Layers.Add(factLocationLayer);

            mapBox1.Map.ZoomToExtents();
            mapBox1.Refresh();
            mapBox1.ActiveTool = SharpMap.Forms.MapBox.Tools.Pan;
        }

        private void SetGeoCodedYearRange()
        {
            //List<Fact> geoCodedFacts = ft.AllFacts.Where(x => x.Location.IsGeoCoded).ToList();
            //return geoCodedFacts.Min(x => x.FactDate.);
            minGeoCodedYear = FactDate.MAXDATE.Year;
            maxGeoCodedYear = FactDate.MINDATE.Year;
            foreach (Fact f in ft.AllFacts)
            {
                if (f.Location.IsGeoCoded && f.FactDate.IsKnown())
                {
                    if (f.FactDate.StartDate != FactDate.MINDATE && f.FactDate.StartDate.Year < minGeoCodedYear)
                        minGeoCodedYear = f.FactDate.StartDate.Year;
                    if (f.FactDate.EndDate != FactDate.MAXDATE && f.FactDate.EndDate.Year > maxGeoCodedYear)
                        maxGeoCodedYear = f.FactDate.EndDate.Year;
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
            pbGeocoding.Visible = true;
            backgroundWorker.RunWorkerAsync();
            this.Cursor = Cursors.Default;
        }

        public void LoadGeoLocationsFromDataBase()
        {
            try
            {

                SQLiteConnection conn = GetDatabaseConnection();
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand("select latitude, longitude, level from geocode where location = ?", conn);
                SQLiteParameter param = cmd.CreateParameter();
                param.DbType = DbType.String;
                cmd.Parameters.Add(param);
                cmd.Prepare();

                foreach (FactLocation loc in ft.AllLocations)
                {
                    cmd.Parameters[0].Value = loc.ToString();
                    SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    if (reader.Read() && loc.ToString().Length > 0)
                    {
                        // location is in database so update location object
                        loc.Latitude = (double)reader["latitude"];
                        loc.Longitude = (double)reader["longitude"];
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading previously geocoded data. " + ex.Message);
            }
        }

        public SQLiteConnection GetDatabaseConnection()
        {
            try
            {
                String filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\Geocodes.s3db");
                if (!File.Exists(filename))
                {
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), filename);
                }
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
                return conn;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening database error is :" + ex.Message);
            }
            return null;
        }

        public void GeoCode(BackgroundWorker worker, DoWorkEventArgs e)
        {
            try
            {
                SQLiteConnection conn = GetDatabaseConnection();
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
                int total = ft.AllLocations.Count();

                foreach (FactLocation loc in ft.AllLocations)
                {
                    if (!loc.IsGeoCoded)
                    {
                        cmd.Parameters[0].Value = loc.ToString();
                        SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                        if (!reader.Read() && loc.ToString().Length > 0)
                        {  // location isn't found so add it
                            GoogleMap.GeoResponse res = GoogleMap.CallGeoWSCount(loc.ToString(), 10, txtGoogleWait);
                            if (res != null && res.Status == "OK" && res.Results.Length > 0)
                            {
                                double latitude = 0;
                                double longitude = 0;
                                string address = string.Empty;
                                int foundLevel = GoogleMap.GetFactLocation(res.Results[0].Types);
                                if (foundLevel >= loc.Level)
                                {
                                    latitude = res.Results[0].Geometry.Location.Lat;
                                    longitude = res.Results[0].Geometry.Location.Lng;
                                    address = res.Results[0].ReturnAddress;
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
                            }
                        }
                        reader.Close();
                    }

                    count++;
                    int percent = (int)Math.Truncate(count * 100.0 / total);
                    string status = "Google found " + good + " failed on " + bad + " records. Geocoded " +
                            ft.AllLocations.Count(l => l.IsGeoCoded) + " locations so far. " + count +
                            " of " + total + " processed.";
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

        private void tbYears_Scroll(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            labValue.Text = tbYears.Value.ToString();
            FactDate year = new FactDate(tbYears.Value.ToString());
            // now load up map with all the facts for that year and display them
            List<Fact> facts = ft.AllFacts.Where(x => x.Location.IsGeoCoded && x.FactDate.IsKnown() && x.FactDate.Overlaps(year)).ToList();
            factLocations.Clear();
            foreach (Fact f in facts)
            {
                FeatureDataRow r = factLocations.NewRow();
                r.Geometry = new NetTopologySuite.Geometries.Point(f.Location.Longitude, f.Location.Latitude);
                factLocations.AddRow(r);
            }
            mapBox1.Refresh();
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
            if (formClosing) this.Close();
        }

        private void TimeLine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backgroundWorker.IsBusy)
            {
                backgroundWorker.CancelAsync();
                e.Cancel = true;
                formClosing = true;
            }
        }

    }
}
