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
        private FamilyTree ft = FamilyTree.Instance;

        public TimeLine()
        {
            InitializeComponent();
        }

        private void geocodeLocationsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            pbGeocoding.Visible = true;
            pbGeocoding.Minimum = 0;
            pbGeocoding.Maximum = ft.AllLocations.Count();
            pbGeocoding.Value = ft.AllLocations.Count(l => l.GeoCoded);

            GeoCode();
            this.Cursor = Cursors.Default;
        }

        public void GeoCode()
        {
            try
            {
                String path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Geocodes.s3db");
                if (!File.Exists(path))
                {
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), path);
                }
                SQLiteConnection conn = new SQLiteConnection("Data Source=" + path + ";Version=3;");
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(conn);
                int count = 0;
                int good = 0;
                int bad = 0;
                FamilyTree ft = FamilyTree.Instance;
                foreach (FactLocation loc in ft.AllLocations)
                {
                    string sql = string.Format("select location from geocode where location = \"{0}\"", loc.ToString());
                    cmd.CommandText = sql;
                    SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                    if (!reader.Read())
                    {  // location isn't found so add it
                        GoogleMap.GeoResponse res = GoogleMap.CallGeoWSCount(loc.ToString(), 10);
                        if (res.Status == "OK" && res.Results.Length > 0)
                        {
                            int foundLevel = GoogleMap.GetFactLocation(res.Results[0].Types);
                            if (foundLevel >= loc.Level)
                            {
                                sql = string.Format("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel)" +
                                        "values (\"{0}\",{1},{2},{3},date('now'),\"{4}\",{5})", loc.ToString(), loc.Level,
                                        res.Results[0].Geometry.Location.Lat, res.Results[0].Geometry.Location.Lng,
                                        res.Results[0].ReturnAddress, foundLevel);
                                good++;
                            }
                            else
                            {
                                sql = string.Format("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel)" +
                                        "values (\"{0}\",{1},{2},{3},date('now'),\"{4}\",{5})", loc.ToString(), loc.Level, 0, 0, "", foundLevel);
                                bad++;
                            }
                            cmd = new SQLiteCommand(sql, conn);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    reader.Close();
                    count++;
                    txtLocations.Text = "Found " + good + " records and failed to find " + bad + " records from " + count + " of " + ft.AllLocations.Count();
                }
                conn.Close();
                MessageBox.Show("Finished Geocoding");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error geocoding : " + ex.Message);
            }

        }

    }
}
