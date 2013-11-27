using System.Data.SQLite;
using System.Data;
using System;
using System.Windows.Forms;
using System.IO;
using FTAnalyzer.Forms;
using FTAnalyzer.Mapping;
using GeoAPI.CoordinateSystems.Transformations;
using GeoAPI.Geometries;
using System.Collections.Concurrent;

namespace FTAnalyzer.Utilities
{
    public class DatabaseHelper : IDisposable
    {
        private static SQLiteConnection conn;
        private static DatabaseHelper instance;
        public string Filename { get; private set; }
        public string CurrentFilename { get; private set; }
        public string DatabasePath { get; private set; }
        private Version ProgramVersion { get; set; }

        #region Constructor/Destructor
        private DatabaseHelper()
        {
            DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer");
            CurrentFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\FTA-RestoreTemp.s3db");
            OpenDatabaseConnection();
        }

        public static DatabaseHelper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseHelper();
                }
                return instance;
            }
        }

        public void Dispose()
        {
            try
            {
                if (conn != null) conn.Close();
            }
            catch (Exception) { }
            conn = null;
        }

        private void OpenDatabaseConnection()
        {
            conn = null;
            try
            {
                Filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\Geocodes.s3db");
                if (!File.Exists(Filename))
                {
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), Filename);
                }
                conn = new SQLiteConnection("Data Source=" + Filename + ";Version=3;");
                conn.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error opening database. Error is :" + ex.Message);
            }
        }

        #endregion

        #region Database Update Functions
        public void CheckDatabaseVersion(Version programVersion)
        {
            try
            {
                ProgramVersion = programVersion;
                SQLiteCommand cmd = new SQLiteCommand("select Database from versions", conn);
                string db = (string)cmd.ExecuteScalar();
                cmd.Dispose();
                Version dbVersion = db == null ? new Version("0.0.0.0") : new Version(db);
                if (dbVersion < programVersion)
                    UpgradeDatabase(dbVersion);
            }
            catch (SQLiteException)
            {
                UpgradeDatabase(new Version("0.0.0.0"));
            }
        }

        private void UpgradeDatabase(Version dbVersion)
        {
            try
            {
                Version v3_0_0_0 = new Version("3.0.0.0");
                Version v3_0_2_0 = new Version("3.0.2.0");
                Version v3_1_2_0 = new Version("3.1.2.0");
                Version v3_2_1_0 = new Version("3.2.1.0");
                if (dbVersion < v3_0_0_0)
                {
                    // Version is less than 3.0.0.0 or none existent so copy latest database from empty database
                    conn.Close();
                    GC.Collect(); // needed to force a cleanup of connections prior to replacing the file.
                    if (File.Exists(Filename))
                    {
                        File.Delete(Filename);
                    }
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), Filename);
                    // Now re-open upgraded database
                    OpenDatabaseConnection();
                }
                if (dbVersion < v3_0_2_0)
                {
                    // Version v3.0.2.0 needs to reset Google Matches to not searched and set partials to level
                    //SQLiteCommand cmd = new SQLiteCommand("alter table geocode add column GeocodeStatus integer default 0", conn);
                    SQLiteCommand cmd = new SQLiteCommand("update geocode set geocodestatus=0 where geocodestatus=1", conn);
                    cmd.ExecuteNonQuery(); // reset Google Match to Not Searched
                    cmd = new SQLiteCommand("update geocode set geocodestatus=7 where geocodestatus=2", conn);
                    cmd.ExecuteNonQuery(); // set to level mismatch if partial
                    cmd = new SQLiteCommand("update versions set Database = '3.0.2.0'", conn);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Please note that due to fixes in the way Google reports\nlocations your 'Google Matched' geocodes have been reset.");
                }
                if (dbVersion < v3_1_2_0)
                {
                    // Version v3.1.0.2 needs to reset Google locations & found level to unknown where status is user entered
                    SQLiteCommand cmd = new SQLiteCommand("update geocode set foundlocation='', foundlevel=-2 where geocodestatus=3", conn);
                    cmd.ExecuteNonQuery();
                    cmd = new SQLiteCommand("update versions set Database = '3.1.2.0'", conn);
                    cmd.ExecuteNonQuery();
                }
                if (dbVersion < v3_2_1_0)
                {
                    bool proceed = false;
                    DialogResult result = MessageBox.Show("In order to improve speed of the maps a database upgrade is needed.\nThis may take several minutes and must be allowed to complete.\nYou must backup your database first. Ok to proceed?", "Database upgrading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    Application.UseWaitCursor = true;
                    if (result == DialogResult.Yes)
                        proceed = FamilyTree.Instance.BackupDatabase(new SaveFileDialog(), "FT Analyzer zip file created by Database upgrade for v3.2.1.0");
                    Application.UseWaitCursor = false;
                    if (proceed)
                    {
                        SQLiteCommand cmd = new SQLiteCommand("alter table geocode add column Latm real default 0.0", conn);
                        cmd.ExecuteNonQuery();
                        cmd = new SQLiteCommand("alter table geocode add column Longm real default 0.0", conn);
                        cmd.ExecuteNonQuery();
                        ConvertLatLongs();
                        cmd = new SQLiteCommand("update versions set Database = '3.2.1.0'", conn);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Database lat/long upgrade complete");
                    }
                    else
                    {
                        MessageBox.Show("Database not backed up we cannot proceed to update maps without a safe database backup.\nMapping features will not work correctly.", "Database backup Required");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error upgrading database. Error is :" + ex.Message);
            }
        }
        #endregion

        #region Conversion Routines
        private void ConvertLatLongs()
        {
            Coordinate Point, NorthEast, SouthWest;
            Coordinate mPoint, mNorthEast, mSouthWest;
            double latitude, longitude, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw;

            IMathTransform transform = MapTransforms.Transform().MathTransform;
            SQLiteCommand cmd = new SQLiteCommand("select count(*) from geocode where latitude <> 0 and longitude <> 0", conn);
            SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
            reader.Read();
            int rowcount = 0;
            int.TryParse(reader[0].ToString(), out rowcount);
            reader.Close();
            cmd = new SQLiteCommand("select location, latitude, longitude, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw from geocode where latitude <> 0 and longitude <> 0", conn);
            SQLiteCommand updateCmd = new SQLiteCommand("update geocode set latm=?, longm=?, viewport_x_ne=?, viewport_y_ne=?, viewport_x_sw=?, viewport_y_sw=?  where location = ?", conn);
            SQLiteParameter param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.String;
            updateCmd.Parameters.Add(param);
            updateCmd.Prepare();
            Progress p = new Progress(rowcount);
            p.Show();
            int row = 0;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                latitude = longitude = viewport_x_ne = viewport_x_sw = viewport_y_ne = viewport_y_sw = 0;
                string location = reader["location"].ToString();
                double.TryParse(reader["latitude"].ToString(), out latitude);
                double.TryParse(reader["longitude"].ToString(), out longitude);
                double.TryParse(reader["viewport_x_ne"].ToString(), out viewport_x_ne);
                double.TryParse(reader["viewport_y_ne"].ToString(), out viewport_y_ne);
                double.TryParse(reader["viewport_x_sw"].ToString(), out viewport_x_sw);
                double.TryParse(reader["viewport_y_sw"].ToString(), out viewport_y_sw);
                Point = new Coordinate(longitude, latitude);
                NorthEast = new Coordinate(viewport_y_ne, viewport_x_ne); // old viewports had x & y wrong way round
                SouthWest = new Coordinate(viewport_y_sw, viewport_x_sw); // x is stored as lat y as long
                mPoint = transform.Transform(Point);
                mNorthEast = transform.Transform(NorthEast);
                mSouthWest = transform.Transform(SouthWest);
                // now write back the m versions
                updateCmd.Parameters[0].Value = mPoint.X;
                updateCmd.Parameters[1].Value = mPoint.Y;
                updateCmd.Parameters[2].Value = mNorthEast.X;
                updateCmd.Parameters[3].Value = mNorthEast.Y;
                updateCmd.Parameters[4].Value = mSouthWest.X;
                updateCmd.Parameters[5].Value = mSouthWest.Y;
                updateCmd.Parameters[6].Value = location;
                updateCmd.ExecuteNonQuery();
                p.Update(++row);
            }
            p.Dispose();
            reader.Close();
        }
        #endregion

        #region Commands
        public bool IsLocationInDatabase(string location)
        {
            SQLiteCommand cmd = new SQLiteCommand("select location from geocode where location = ?", conn);
            SQLiteParameter param = cmd.CreateParameter();
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);
            cmd.Prepare();
            cmd.Parameters[0].Value = location;
            SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
            bool inDatabase = reader.Read();
            reader.Close();
            return inDatabase;

        }

        public void ResetPartials()
        {
            SQLiteCommand cmd = new SQLiteCommand("update geocode set latitude = 0, longitude = 0, founddate = date('now'), foundlocation = '', foundlevel = -2, viewport_x_ne = 0, viewport_y_ne = 0, viewport_x_sw = 0, viewport_y_sw = 0, geocodestatus = 0, foundresulttype = '' where geocodestatus = 2 or geocodestatus=7", conn);
            cmd.ExecuteNonQuery();
        }

        public void GetLocationDetails(FactLocation location)
        {
            if (location.ToString().Length == 0) return;
            SQLiteCommand cmd = new SQLiteCommand("select latitude, longitude, latm, longm, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus, foundlevel, foundlocation, foundresulttype from geocode where location = ?", conn);
            SQLiteParameter param = cmd.CreateParameter();
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);
            cmd.Prepare();
            cmd.Parameters[0].Value = location.ToString();
            SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow);
            if (reader.Read())
            {
                double latitude, longitude, latm, longm, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw;
                double.TryParse(reader["latitude"].ToString(), out latitude);
                double.TryParse(reader["longitude"].ToString(), out longitude);
                double.TryParse(reader["latm"].ToString(), out latm);
                double.TryParse(reader["longm"].ToString(), out longm);
                double.TryParse(reader["viewport_x_ne"].ToString(), out viewport_x_ne);
                double.TryParse(reader["viewport_y_ne"].ToString(), out viewport_y_ne);
                double.TryParse(reader["viewport_x_sw"].ToString(), out viewport_x_sw);
                double.TryParse(reader["viewport_y_sw"].ToString(), out viewport_y_sw);
                location.Latitude = latitude;
                location.Longitude = longitude;
                location.LatitudeM = latm;
                location.LongitudeM = longm;
                if (location.ViewPort == null)
                {
                    location.ViewPort = new Mapping.GeoResponse.CResult.CGeometry.CViewPort();
                    location.ViewPort.NorthEast = new Mapping.GeoResponse.CResult.CGeometry.CLocation();
                    location.ViewPort.SouthWest = new Mapping.GeoResponse.CResult.CGeometry.CLocation();
                }
                location.ViewPort.NorthEast.Lat = viewport_y_ne;
                location.ViewPort.NorthEast.Long = viewport_x_ne;
                location.ViewPort.SouthWest.Lat = viewport_y_sw;
                location.ViewPort.SouthWest.Long = viewport_x_sw;
                location.GeocodeStatus = (FactLocation.Geocode)Enum.Parse(typeof(FactLocation.Geocode), reader["geocodestatus"].ToString());
                location.GoogleLocation = reader["foundlocation"].ToString();
                location.GoogleResultType = reader["foundresulttype"].ToString();
                int foundlevel = 0;
                int.TryParse(reader["foundlevel"].ToString(), out foundlevel);
                location.FoundLevel = foundlevel;
            }
            reader.Close();
        }

        public void InsertGeocode(FactLocation loc)
        {
            SQLiteCommand insertCmd = new SQLiteCommand("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus, foundresulttype, latm, longm) values(?,?,?,?,date('now'),?,?,?,?,?,?,?,?,?,?)", conn);
            SQLiteParameter param = insertCmd.CreateParameter();

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

            param = insertCmd.CreateParameter();
            param.DbType = DbType.Double;
            insertCmd.Parameters.Add(param);

            param = insertCmd.CreateParameter();
            param.DbType = DbType.Double;
            insertCmd.Parameters.Add(param);

            param = insertCmd.CreateParameter();
            param.DbType = DbType.Double;
            insertCmd.Parameters.Add(param);

            param = insertCmd.CreateParameter();
            param.DbType = DbType.Double;
            insertCmd.Parameters.Add(param);

            param = insertCmd.CreateParameter();
            param.DbType = DbType.Int32;
            insertCmd.Parameters.Add(param);

            param = insertCmd.CreateParameter();
            param.DbType = DbType.String;
            insertCmd.Parameters.Add(param);

            param = insertCmd.CreateParameter();
            param.DbType = DbType.Double;
            insertCmd.Parameters.Add(param);

            param = insertCmd.CreateParameter();
            param.DbType = DbType.Double;
            insertCmd.Parameters.Add(param);

            insertCmd.Prepare();
            insertCmd.Parameters[0].Value = loc.ToString();
            insertCmd.Parameters[1].Value = loc.Level;
            insertCmd.Parameters[2].Value = loc.Latitude;
            insertCmd.Parameters[3].Value = loc.Longitude;
            insertCmd.Parameters[4].Value = loc.GoogleLocation;
            insertCmd.Parameters[5].Value = loc.FoundLevel;
            insertCmd.Parameters[6].Value = loc.ViewPort.NorthEast.Long;
            insertCmd.Parameters[7].Value = loc.ViewPort.NorthEast.Lat;
            insertCmd.Parameters[8].Value = loc.ViewPort.SouthWest.Long;
            insertCmd.Parameters[9].Value = loc.ViewPort.SouthWest.Lat;
            insertCmd.Parameters[10].Value = loc.GeocodeStatus;
            insertCmd.Parameters[11].Value = loc.GoogleResultType;
            insertCmd.Parameters[12].Value = loc.LatitudeM;
            insertCmd.Parameters[13].Value = loc.LongitudeM;
            insertCmd.ExecuteNonQuery();
        }

        public void UpdateGeocode(FactLocation loc)
        {
            SQLiteCommand updateCmd = new SQLiteCommand("update geocode set founddate=date('now'), level = ?, latitude = ?, longitude = ?, foundlocation = ?, foundlevel = ?, viewport_x_ne = ?, viewport_y_ne = ?, viewport_x_sw = ?, viewport_y_sw = ?, geocodestatus = ?, foundresulttype = ?, latm = ?, longm = ? where location = ?", conn);

            SQLiteParameter param = updateCmd.CreateParameter();

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Int32;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.String;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Int32;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Int32;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.String;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Double;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.String;
            updateCmd.Parameters.Add(param);

            updateCmd.Prepare();
            updateCmd.Parameters[0].Value = loc.Level;
            updateCmd.Parameters[1].Value = loc.Latitude;
            updateCmd.Parameters[2].Value = loc.Longitude;
            updateCmd.Parameters[3].Value = loc.GoogleLocation;
            updateCmd.Parameters[4].Value = loc.FoundLevel;
            updateCmd.Parameters[5].Value = loc.ViewPort.NorthEast.Long;
            updateCmd.Parameters[6].Value = loc.ViewPort.NorthEast.Lat;
            updateCmd.Parameters[7].Value = loc.ViewPort.SouthWest.Long;
            updateCmd.Parameters[8].Value = loc.ViewPort.SouthWest.Lat;
            updateCmd.Parameters[9].Value = loc.GeocodeStatus;
            updateCmd.Parameters[10].Value = loc.GoogleResultType;
            updateCmd.Parameters[11].Value = loc.LatitudeM;
            updateCmd.Parameters[12].Value = loc.LongitudeM;
            updateCmd.Parameters[13].Value = loc.ToString();
            updateCmd.ExecuteNonQuery();
            OnGeoLocationUpdated(loc);
        }
        #endregion

        #region Cursor Queries

        //public void AddEmptyLocationsToQueue(ConcurrentQueue<FactLocation> queue)
        //{
        //    SQLiteCommand cmd = new SQLiteCommand("select location from geocode where foundlocation='' and geocodestatus=3", conn);
        //    SQLiteDataReader reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {
        //        FactLocation loc = FactLocation.LookupLocation(reader[0].ToString());
        //        if (!queue.Contains(loc))
        //            queue.Enqueue(loc);
        //    }
        //}

        public SQLiteCommand NeedsReverseGeocode()
        {
            return new SQLiteCommand("select location from geocode where foundlocation='' and geocodestatus=3", conn);
        }

        #endregion

        #region BackupRestore
        public bool StartBackupRestoreDatabase()
        {
            string tempFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\Geocodes.s3db.tmp");
            try
            {
                conn.Close();
                GC.Collect(); // needed to force a cleanup of connections prior to replacing the file.
                if (File.Exists(tempFilename))
                    File.Delete(tempFilename);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void EndBackupDatabase()
        {
            if (conn.State != ConnectionState.Open)
                OpenDatabaseConnection();
        }

        public bool RestoreDatabase()
        {
            bool result = true;
            try
            {
                // finally re-open database and check for updates
                if (conn.State != ConnectionState.Open)
                    OpenDatabaseConnection();
                UpgradeDatabase(ProgramVersion);
                FamilyTree ft = FamilyTree.Instance;
                if (ft.DataLoaded)
                    ft.LoadGeoLocationsFromDataBase();
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region EventHandler
        public static event EventHandler GeoLocationUpdated;
        protected static void OnGeoLocationUpdated(FactLocation loc)
        {
            if (GeoLocationUpdated != null)
                GeoLocationUpdated(loc, EventArgs.Empty);
        }

        #endregion
    }
}
