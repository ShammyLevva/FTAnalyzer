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

namespace FTAnalyzer.Forms
{
    public partial class TimeLine : Form
    {
        private FamilyTree ft;
        private int minGeoCodedYear;
        private int maxGeoCodedYear;

        public TimeLine()
        {
            InitializeComponent();
            ft = FamilyTree.Instance;
            LoadGeoLocationsFromDataBase();
            txtLocations.Text = "Already Geocoded " + ft.AllLocations.Count(l => l.IsGeoCoded) + " of " + ft.AllLocations.Count() + " locations";
            txtGoogleWait.Text = string.Empty;
            SetGeoCodedYearRange();
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
                tbYears.Visible = false;
            else
            {
                tbYears.Visible = true;
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
            GeoCode();
            this.Cursor = Cursors.Default;
        }

        public void LoadGeoLocationsFromDataBase()
        {
            try
            {
                SQLiteConnection conn = GetDatabaseConnection();
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(conn);
                foreach (FactLocation loc in ft.AllLocations)
                {
                    string sql = string.Format("select latitude, longitude, level from geocode where location = \"{0}\"", loc.ToString());
                    cmd.CommandText = sql;
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

        public void GeoCode()
        {
            try
            {
                SQLiteConnection conn = GetDatabaseConnection();
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(conn);
                int count = 0;
                int good = 0;
                int bad = 0;
                pbGeocoding.Minimum = 0;
                pbGeocoding.Maximum = ft.AllLocations.Count();
                pbGeocoding.Value = count;
                foreach (FactLocation loc in ft.AllLocations)
                {
                    if (!loc.IsGeoCoded)
                    {
                        string sql = string.Format("select location from geocode where location = \"{0}\"", loc.ToString());
                        cmd.CommandText = sql;
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
                                sql = string.Format("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel)" +
                                        "values (\"{0}\",{1},{2},{3},date('now'),\"{4}\",{5})", loc.ToString(), loc.Level, latitude, longitude, address, foundLevel);
                                cmd = new SQLiteCommand(sql, conn);
                                cmd.ExecuteNonQuery();
                                loc.Latitude = latitude;
                                loc.Longitude = longitude;
                            }
                        }
                        reader.Close();
                    }
                    count++;
                    Application.DoEvents();
                    pbGeocoding.Value = count;
                    txtLocations.Text = "Google found " + good + " failed on " + bad + " records. Geocoded " +
                        ft.AllLocations.Count(l => l.IsGeoCoded) + " locations so far. " + count + " of " + pbGeocoding.Maximum + " processed.";
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

            this.Cursor = Cursors.Default;
        }

    }
}
