using FTAnalyzer.Forms;
using FTAnalyzer.Mapping;
using GeoAPI.Geometries;
using Ionic.Zip;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public class DatabaseHelper : IDisposable
    {
        public string Filename { get; private set; }
        public string CurrentFilename { get; private set; }
        public string DatabasePath { get; private set; }
        static DatabaseHelper instance;
        static string connectionString;
        static SQLiteConnection InstanceConnection { get; set; }
        Version ProgramVersion { get; set; }
        bool restoring;

        #region Constructor/Destructor
        DatabaseHelper()
        {
            DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer");
            CurrentFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\FTA-RestoreTemp.s3db");
            CheckDatabaseConnection();
            InstanceConnection = new SQLiteConnection(connectionString);
            restoring = false;
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

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                try
                {
                    if (InstanceConnection?.State == ConnectionState.Open)
                        InstanceConnection.Close();
                    InstanceConnection?.Dispose();
                    // dispose of things here
                }
                catch (Exception) { }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        void CheckDatabaseConnection()
        {
            try
            {
                Filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\Geocodes.s3db");
                if (!File.Exists(Filename))
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), Filename);
                }
                connectionString = "Data Source=" + Filename + ";Version=3;";
            }
            catch (Exception ex)
            {
//                log.Error("Error opening database. Error is :" + ex.Message);
                MessageBox.Show("Error opening database. Error is :" + ex.Message, "FTAnalyzer");
            }
        }
        #endregion

        #region Database Update Functions
        public void CheckDatabaseVersion(Version programVersion)
        {
            try
            {
                ProgramVersion = programVersion;
                Version dbVersion = GetDatabaseVersion();
                if (dbVersion < programVersion)
                    UpgradeDatabase(dbVersion);
            }
            catch (SQLiteException)
            {
//                log.Debug("Caught Exception in CheckDatabaseVersion " + e.Message);
                UpgradeDatabase(new Version("0.0.0.0"));
            }
        }

        static Version GetDatabaseVersion()
        {
            string db = null;
            try
            {
                if (InstanceConnection.State != ConnectionState.Open)
                    InstanceConnection.Open();
                using (SQLiteCommand cmd = new SQLiteCommand("select Database from versions", InstanceConnection))
                {
                    db = (string)cmd.ExecuteScalar();
                }
                InstanceConnection.Close();
            }
            catch (Exception)
            {
                //log.Error("Error in GetDatabaseVersion " + e.Message);
            }
            Version dbVersion = db == null ? new Version("0.0.0.0") : new Version(db);
            return dbVersion;
        }

        public bool BackupDatabase(SaveFileDialog saveDatabase, string comment)
        {
            string directory = Application.UserAppDataRegistry.GetValue("Geocode Backup Directory", Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)).ToString();
            saveDatabase.FileName = "FTAnalyzer-Geocodes-" + DateTime.Now.ToString("yyyy-MM-dd") + "-v" + MainForm.VERSION + ".zip";
            saveDatabase.InitialDirectory = directory;
            DialogResult result = saveDatabase.ShowDialog();
            if (result == DialogResult.OK)
            {
                StartBackupRestoreDatabase();
                if (File.Exists(saveDatabase.FileName))
                    File.Delete(saveDatabase.FileName);
                ZipFile zip = new ZipFile(saveDatabase.FileName);
                zip.AddFile(Filename, string.Empty);
                zip.Comment = comment + " on " + DateTime.Now.ToString("dd MMM yyyy HH:mm");
                zip.Save();
                //EndBackupDatabase();
                Application.UserAppDataRegistry.SetValue("Geocode Backup Directory", Path.GetDirectoryName(saveDatabase.FileName));
                MessageBox.Show("Database exported to " + saveDatabase.FileName, "FTAnalyzer Database Export Complete");
                return true;
            }
            return false;
        }

        void UpgradeDatabase(Version dbVersion)
        {
            try
            {
                Version v3_0_0_0 = new Version("3.0.0.0");
                Version v3_0_2_0 = new Version("3.0.2.0");
                Version v3_1_2_0 = new Version("3.1.2.0");
                Version v3_3_2_5 = new Version("3.3.2.5");
                Version v7_0_0_0 = new Version("7.0.0.0");
                Version v7_3_0_0 = new Version("7.3.0.0");
                if (dbVersion < v3_0_0_0)
                {
                    // Version is less than 3.0.0.0 or none existent so copy latest database from empty database
                    GC.Collect(); // needed to force a cleanup of connections prior to replacing the file.
                    if (File.Exists(Filename))
                    {
                        File.Delete(Filename);
                    }
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), Filename);
                }
                if (InstanceConnection.State != ConnectionState.Open)
                    InstanceConnection.Open();
                if (dbVersion < v3_0_2_0)
                {
                    // Version v3.0.2.0 needs to reset Google Matches to not searched and set partials to level
                    //SQLiteCommand cmd = new SQLiteCommand("alter table geocode add column GeocodeStatus integer default 0", conn);
                    using (SQLiteCommand cmd = new SQLiteCommand("update geocode set geocodestatus=0 where geocodestatus=1", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery(); // reset Google Match to Not Searched
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand("update geocode set geocodestatus=7 where geocodestatus=2", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery(); // set to level mismatch if partial
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand("update versions set Database = '3.0.2.0'", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    MessageBox.Show("Please note that due to fixes in the way Google reports\nlocations your 'Google Matched' geocodes have been reset.", "FTAnalyzer");
                }
                if (dbVersion < v3_1_2_0)
                {
                    bool proceed = false;
                    if (restoring)
                        proceed = true;
                    else
                    {
                        DialogResult result = MessageBox.Show("In order to improve speed of the maps a database upgrade is needed.\nThis may take several minutes and must be allowed to complete.\nYou must backup your database first. Ok to proceed?", "Database upgrading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        Application.UseWaitCursor = true;
                        if (result == DialogResult.Yes)
                            proceed = BackupDatabase(new SaveFileDialog(), "FTAnalyzer zip file created by Database upgrade for v3.2.1.0");
                        Application.UseWaitCursor = false;
                    }
                    if (proceed)
                    {
                        bool latm = false;
                        bool longm = false;
                        using (SQLiteCommand cmd = new SQLiteCommand("PRAGMA table_info('geocode')", InstanceConnection))
                        {
                            using (SQLiteDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    string column = reader[1].ToString();
                                    if (column.Equals("Latm"))
                                        latm = true;
                                    if (column.Equals("Longm"))
                                        longm = true;
                                }
                            }
                        }
                        if (!latm)
                        {
                            using (SQLiteCommand cmd = new SQLiteCommand("alter table geocode add column Latm real default 0.0", InstanceConnection))
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                        if (!longm)
                        {
                            using (SQLiteCommand cmd = new SQLiteCommand("alter table geocode add column Longm real default 0.0", InstanceConnection))
                            {
                                cmd.ExecuteNonQuery();
                                ConvertLatLongs();
                            }
                        }
                        using (SQLiteCommand cmd = new SQLiteCommand("update geocode set foundlocation='', foundlevel=-2 where geocodestatus=3", InstanceConnection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        using (SQLiteCommand cmd = new SQLiteCommand("update versions set Database = '3.2.1.0'", InstanceConnection))
                        {
                            cmd.ExecuteNonQuery();
                        }
                        MessageBox.Show("Database lat/long upgrade complete", "FTAnalyzer");
                    }
                    else
                    {
                        MessageBox.Show("Database not backed up we cannot proceed to update maps without a safe database backup.\nMapping features will not work correctly.", "Database backup Required");
                    }
                }
                if (dbVersion < v3_3_2_5)
                {
                    // mark all bad viewports as not searched
                    using (SQLiteCommand cmd = new SQLiteCommand("update Geocode set latitude = 0, longitude = 0, founddate = date('now'), foundlocation = '', foundlevel = -2, viewport_x_ne = 0, viewport_y_ne = 0, viewport_x_sw = 0, viewport_y_sw = 0, geocodestatus = 0, foundresulttype = '' where latitude<>0 and longitude<>0 and abs(viewport_x_ne) <= 180", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand("update versions set Database = '3.3.2.5'", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                if (dbVersion < v7_0_0_0)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("update Geocode set geocodestatus = 0 where latitude=0 and longitude=0 and geocodestatus=3", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand("update versions set Database = '7.0.0.0'", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                if (dbVersion < v7_3_0_0)
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("create table LostCousins(CensusYear INTEGER(4), CensusCountry STRING (20), CensusRef STRING(25), IndID STRING(10), constraint pkLostCousins primary key(CensusYear, CensusCountry, CensusRef, IndID))", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    using (SQLiteCommand cmd = new SQLiteCommand("update versions set Database = '7.3.0.0'", InstanceConnection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                
                InstanceConnection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error upgrading database. Error is :{ex.Message}", "FTAnalyzer");
            }
        }
        #endregion

        #region Lat/Long Routines
        void ConvertLatLongs()
        {
            Coordinate Point, NorthEast, SouthWest;
            Coordinate mPoint, mNorthEast, mSouthWest;
            double latitude, longitude, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw;

            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            int rowcount = 0;
            using (SQLiteCommand cmd = new SQLiteCommand("select count(*) from geocode where latitude <> 0 and longitude <> 0", InstanceConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    reader.Read();
                    int.TryParse(reader[0].ToString(), out rowcount);
                }
            }
            #region update cmd
            using (SQLiteCommand updateCmd = new SQLiteCommand("update geocode set latm=?, longm=?, viewport_x_ne=?, viewport_y_ne=?, viewport_x_sw=?, viewport_y_sw=?  where location = ?", InstanceConnection))
            {
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
                using (SQLiteCommand cmd = new SQLiteCommand("select location, latitude, longitude, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw from geocode where latitude <> 0 and longitude <> 0", InstanceConnection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
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
                            mPoint = MapTransforms.TransformCoordinate(Point);
                            mNorthEast = MapTransforms.TransformCoordinate(NorthEast);
                            mSouthWest = MapTransforms.TransformCoordinate(SouthWest);
                            // now write back the m versions
                            updateCmd.Parameters[0].Value = mPoint.Y;
                            updateCmd.Parameters[1].Value = mPoint.X;
                            updateCmd.Parameters[2].Value = mNorthEast.X;
                            updateCmd.Parameters[3].Value = mNorthEast.Y;
                            updateCmd.Parameters[4].Value = mSouthWest.X;
                            updateCmd.Parameters[5].Value = mSouthWest.Y;
                            updateCmd.Parameters[6].Value = location;
                            updateCmd.ExecuteNonQuery();
                            p.Update(++row);
                        }
                    }
                }
            }
            #endregion
        }

        public string LatLongHashKey(double latitude, double longitude) => latitude.ToString("F6") + longitude.ToString("F6");

        public Dictionary<string, Tuple<string, string>> LatLongIndex
        {
            get
            {
                Dictionary<string, Tuple<string, string>> results = new Dictionary<string, Tuple<string, string>>();

                if (InstanceConnection.State != ConnectionState.Open)
                    InstanceConnection.Open();
                double latitude = 0;
                double longitude = 0;
                string hashkey;
                string foundlocation;
                string foundresulttype;
                using (SQLiteCommand cmd = new SQLiteCommand(
                    "select distinct latitude, longitude, foundlocation, foundresulttype from geocode where latitude <> 0 and longitude <> 0 and foundlocation<>''", InstanceConnection))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            double.TryParse(reader[0].ToString(), out latitude);
                            double.TryParse(reader[1].ToString(), out longitude);
                            hashkey = LatLongHashKey(latitude, longitude);
                            foundlocation = reader[2].ToString();
                            foundresulttype = reader[3].ToString();
                            if (!results.ContainsKey(hashkey))
                                results.Add(hashkey, new Tuple<string, string>(foundlocation, foundresulttype));
                        }
                    }
                }
                return results;
            }
        }
        #endregion

        #region Commands
        public bool IsLocationInDatabase(string location)
        {
            bool inDatabase;
            if(InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand("select location from geocode where location = ?", InstanceConnection))
            {
                SQLiteParameter param = cmd.CreateParameter();
                param.DbType = DbType.String;
                cmd.Parameters.Add(param);
                cmd.Prepare();
                cmd.Parameters[0].Value = location;
                using (SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult))
                {
                    inDatabase = reader.Read();
                }
            }
            return inDatabase;
        }

        public void ResetPartials()
        {
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand("update geocode set latitude = 0, longitude = 0, founddate = date('now'), foundlocation = '', foundlevel = -2, viewport_x_ne = 0, viewport_y_ne = 0, viewport_x_sw = 0, viewport_y_sw = 0, geocodestatus = 0, foundresulttype = '' where geocodestatus in (2,7,9)", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            InstanceConnection.Close();
        }

        public void LoadGeoLocations()
        {
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            foreach (FactLocation loc in FactLocation.AllLocations)
            {
                ReadLocationIntoFact(loc, InstanceConnection);
            }
            InstanceConnection.Close();
        }

        public void GetLocationDetails(FactLocation location)
        {
            if (location.ToString().Length == 0) return;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            ReadLocationIntoFact(location, InstanceConnection);
        }

        static void ReadLocationIntoFact(FactLocation location, SQLiteConnection conn)
        {
            using (SQLiteCommand cmd = new SQLiteCommand("select latitude, longitude, latm, longm, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus, foundlevel, foundlocation, foundresulttype from geocode where location = ?", conn))
            {
                SQLiteParameter param = cmd.CreateParameter();
                param.DbType = DbType.String;
                cmd.Parameters.Add(param);
                cmd.Prepare();
                cmd.Parameters[0].Value = location.ToString();
                using (SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read())
                    {
                        double.TryParse(reader["latitude"].ToString(), out double latitude);
                        double.TryParse(reader["longitude"].ToString(), out double longitude);
                        double.TryParse(reader["latm"].ToString(), out double latm);
                        double.TryParse(reader["longm"].ToString(), out double longm);
                        double.TryParse(reader["viewport_x_ne"].ToString(), out double viewport_x_ne);
                        double.TryParse(reader["viewport_y_ne"].ToString(), out double viewport_y_ne);
                        double.TryParse(reader["viewport_x_sw"].ToString(), out double viewport_x_sw);
                        double.TryParse(reader["viewport_y_sw"].ToString(), out double viewport_y_sw);
                        location.Latitude = latitude;
                        location.Longitude = longitude;
                        location.LatitudeM = latm;
                        location.LongitudeM = longm;
                        if (location.ViewPort == null)
                        {
                            location.ViewPort = new GeoResponse.CResult.CGeometry.CViewPort
                            {
                                NorthEast = new GeoResponse.CResult.CGeometry.CLocation(),
                                SouthWest = new GeoResponse.CResult.CGeometry.CLocation()
                            };
                        }
                        location.ViewPort.NorthEast.Lat = viewport_y_ne;
                        location.ViewPort.NorthEast.Long = viewport_x_ne;
                        location.ViewPort.SouthWest.Lat = viewport_y_sw;
                        location.ViewPort.SouthWest.Long = viewport_x_sw;
                        location.GeocodeStatus = (FactLocation.Geocode)Enum.Parse(typeof(FactLocation.Geocode), reader["geocodestatus"].ToString());
                        location.FoundLocation = reader["foundlocation"].ToString();
                        location.FoundResultType = reader["foundresulttype"].ToString();
                        int.TryParse(reader["foundlevel"].ToString(), out int foundlevel);
                        location.FoundLevel = foundlevel;
                    }
                }
            }
        }
        public void InsertGeocode(FactLocation loc)
        {
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            SQLiteParameter param;
            using (SQLiteCommand insertCmd = new SQLiteCommand("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus, foundresulttype, latm, longm) values(?,?,?,?,date('now'),?,?,?,?,?,?,?,?,?,?)", InstanceConnection))
            {
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
                insertCmd.Parameters[4].Value = loc.FoundLocation;
                insertCmd.Parameters[5].Value = loc.FoundLevel;
                insertCmd.Parameters[6].Value = loc.ViewPort.NorthEast.Long;
                insertCmd.Parameters[7].Value = loc.ViewPort.NorthEast.Lat;
                insertCmd.Parameters[8].Value = loc.ViewPort.SouthWest.Long;
                insertCmd.Parameters[9].Value = loc.ViewPort.SouthWest.Lat;
                insertCmd.Parameters[10].Value = loc.GeocodeStatus;
                insertCmd.Parameters[11].Value = loc.FoundResultType;
                insertCmd.Parameters[12].Value = loc.LatitudeM;
                insertCmd.Parameters[13].Value = loc.LongitudeM;

                int rowsaffected = insertCmd.ExecuteNonQuery();
            }
        }

        public void UpdateGeocode(FactLocation loc)
        {
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand updateCmd = new SQLiteCommand("update geocode set founddate=date('now'), level = ?, latitude = ?, longitude = ?, foundlocation = ?, foundlevel = ?, viewport_x_ne = ?, viewport_y_ne = ?, viewport_x_sw = ?, viewport_y_sw = ?, geocodestatus = ?, foundresulttype = ?, latm = ?, longm = ? where location = ?", InstanceConnection))
            {
                SQLiteParameter param = updateCmd.CreateParameter();
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
                updateCmd.Parameters[3].Value = loc.FoundLocation;
                updateCmd.Parameters[4].Value = loc.FoundLevel;
                updateCmd.Parameters[5].Value = loc.ViewPort.NorthEast.Long;
                updateCmd.Parameters[6].Value = loc.ViewPort.NorthEast.Lat;
                updateCmd.Parameters[7].Value = loc.ViewPort.SouthWest.Long;
                updateCmd.Parameters[8].Value = loc.ViewPort.SouthWest.Lat;
                updateCmd.Parameters[9].Value = loc.GeocodeStatus;
                updateCmd.Parameters[10].Value = loc.FoundResultType;
                updateCmd.Parameters[11].Value = loc.LatitudeM;
                updateCmd.Parameters[12].Value = loc.LongitudeM;
                updateCmd.Parameters[13].Value = loc.ToString();
                int rowsaffected = updateCmd.ExecuteNonQuery();
                if (rowsaffected != 1)
                    Console.WriteLine("Problem updating");
                OnGeoLocationUpdated(loc);
            }
        }
        #endregion

        #region LostCousins
        public int AddLostCousinsFacts()
        {
            int count = 0;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand("select CensusYear, CensusCountry, CensusRef, IndID, FullName from LostCousins", InstanceConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string indID = reader["IndID"].ToString();
                        string fullName = reader["FullName"].ToString();
                        Individual ind = FamilyTree.Instance.GetIndividual(indID);
                        if (ind?.Name == fullName) // only load if individual exists in this tree.
                        {
                            string CensusYear = reader["CensusYear"].ToString();
                            string CensusCountry = reader["CensusCountry"].ToString();
                            string CensusRef = reader["CensusRef"].ToString();
                            FactLocation location = FactLocation.GetLocation(CensusCountry);
                            Fact f = new Fact(CensusRef, Fact.LOSTCOUSINS, new FactDate(CensusYear), location, string.Empty, true, true);
                            ind?.AddFact(f);
                            count++;
                        }
                        else
                        {
                            Console.Write("name wrong");
                           // UpdateFullName(reader, ind.Name); needed during testing
                        }
                    }
                }
            }
            return count;
        }

        void UpdateFullName(SQLiteDataReader reader, string name)
        {
            using (SQLiteCommand updateCmd = new SQLiteCommand("update LostCousins set FullName=? Where CensusYear=? and CensusCountry=? and CensusRef=? and IndID=?", InstanceConnection))
            {
                SQLiteParameter param = updateCmd.CreateParameter();
                param.DbType = DbType.String;
                updateCmd.Parameters.Add(param);
                param = updateCmd.CreateParameter();
                param.DbType = DbType.Int32;
                updateCmd.Parameters.Add(param);
                param = updateCmd.CreateParameter();
                param.DbType = DbType.String;
                updateCmd.Parameters.Add(param);
                param = updateCmd.CreateParameter();
                param.DbType = DbType.String;
                updateCmd.Parameters.Add(param);
                param = updateCmd.CreateParameter();
                param.DbType = DbType.String;
                updateCmd.Parameters.Add(param);
                updateCmd.Prepare();

                updateCmd.Parameters[0].Value = name;
                updateCmd.Parameters[1].Value = reader["CensusYear"];
                updateCmd.Parameters[2].Value = reader["CensusCountry"];
                updateCmd.Parameters[3].Value = reader["CensusRef"];
                updateCmd.Parameters[4].Value = reader["IndID"];
                int rowsaffected = updateCmd.ExecuteNonQuery();
                if (rowsaffected != 1)
                    Console.WriteLine("Problem updating");
            }
        }

        public void StoreLostCousinsFact(CensusIndividual ind)
        {
            try
            { 
                if (InstanceConnection.State != ConnectionState.Open)
                    InstanceConnection.Open();
                SQLiteParameter param;

                using (SQLiteCommand cmd = new SQLiteCommand("insert into LostCousins (CensusYear, CensusCountry, CensusRef, IndID, FullName) values(?,?,?,?,?)", InstanceConnection))
                {
                    param = cmd.CreateParameter();
                    param.DbType = DbType.Int32;
                    cmd.Parameters.Add(param);
                    param = cmd.CreateParameter();
                    param.DbType = DbType.String;
                    cmd.Parameters.Add(param);
                    param = cmd.CreateParameter();
                    param.DbType = DbType.String;
                    cmd.Parameters.Add(param);
                    param = cmd.CreateParameter();
                    param.DbType = DbType.String;
                    cmd.Parameters.Add(param);
                    param = cmd.CreateParameter();
                    param.DbType = DbType.String;
                    cmd.Parameters.Add(param);
                    cmd.Prepare();

                    cmd.Parameters[0].Value = ind.CensusDate.BestYear;
                    cmd.Parameters[1].Value = ind.CensusLocation.Country;
                    cmd.Parameters[2].Value = ind.CensusReference;
                    cmd.Parameters[3].Value = ind.IndividualID;
                    cmd.Parameters[4].Value = ind.Name;

                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected != 1)
                        Console.WriteLine("Problem updating");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        #endregion

            #region Cursor Queries

            public void AddEmptyLocationsToQueue(ConcurrentQueue<FactLocation> queue)
        {
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand cmd = new SQLiteCommand("select location from geocode where foundlocation='' and geocodestatus in (3, 8, 9)", InstanceConnection))
            {
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        FactLocation loc = FactLocation.LookupLocation(reader[0].ToString());
                        if (!queue.Contains(loc))
                            queue.Enqueue(loc);
                    }
                }
            }
            InstanceConnection.Close();
        }

        //public SQLiteCommand NeedsReverseGeocode()
        //{
        //    return new SQLiteCommand("select location from geocode where foundlocation='' and geocodestatus in (3, 8, 9)", xxx);
        //}

        #endregion

        #region BackupRestore
        public bool StartBackupRestoreDatabase()
        {
            string tempFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\Geocodes.s3db.tmp");
            try
            {
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

        public bool RestoreDatabase(IProgress<string> outputText)
        {
            bool result = true;
            try
            {
                // finally check for updates
                restoring = true;
                CheckDatabaseVersion(ProgramVersion);
                restoring = false;
                FamilyTree ft = FamilyTree.Instance;
                if (ft.DataLoaded)
                    return ft.LoadGeoLocationsFromDataBase(outputText);
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
            GeoLocationUpdated?.Invoke(loc, EventArgs.Empty);
        }
        #endregion
    }
}
