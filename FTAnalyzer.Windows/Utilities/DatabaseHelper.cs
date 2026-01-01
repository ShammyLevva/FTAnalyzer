using FTAnalyzer.Forms;
using FTAnalyzer.Mapping;
using FTAnalyzer.Shared.Utilities;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using NetTopologySuite.Geometries;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;

namespace FTAnalyzer.Utilities
{
    public partial class DatabaseHelper : IDisposable
    {
        public string DatabaseFile { get; private set; }
        public string CurrentFilename { get; private set; }
        public string DatabasePath { get; private set; }
        static DatabaseHelper instance;
        static SQLiteConnection InstanceConnection { get; set; }
        Version ProgramVersion { get; set; }
        bool restoring;
        const string APPNAME = "FTAnalyzer";

        #region Constructor/Destructor
        DatabaseHelper()
        {
            DatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer");
            CurrentFilename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\FTA-RestoreTemp.s3db");
            if (CheckDatabaseConnection())
            {
                InstanceConnection = new SQLiteConnection($"Data Source={DatabaseFile};Version=3;");
                restoring = false;
            }
        }

        public static DatabaseHelper Instance
        {
            get
            {
                instance ??= new DatabaseHelper();
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

        bool CheckDatabaseConnection()
        {
            try
            {
                DatabaseFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\Geocodes.s3db");
                if (!File.Exists(DatabaseFile))
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), DatabaseFile);
                }
                return true;
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage($"Error opening database - Filename: {DatabaseFile}. Error is :{ex.Message}", APPNAME);
                return false;
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
            string? db = null;
            try
            {
                if (InstanceConnection.State != ConnectionState.Open)
                    InstanceConnection.Open();
                using SQLiteCommand cmd = new("select Database from versions where platform='PC'", InstanceConnection);
                db = (string)cmd.ExecuteScalar();
            }
            catch (Exception)
            {  // use old method if current method fails
                try
                {
                    using SQLiteCommand cmd = new("select Database from versions", InstanceConnection);
                    db = (string)cmd.ExecuteScalar();
                }
                catch { }
            }
            finally
            {
                InstanceConnection?.Close();
            }
            Version dbVersion = db is null ? new("0.0.0.0") : new(db);
            if (dbVersion == new Version("7.3.0.0"))
                return new Version("7.0.0.0"); // force old version so it updates after beta fix on v7.3.0.0
            return dbVersion;
        }

        public bool BackupDatabase(SaveFileDialog saveDatabase, string comment)
        {
            try
            {
                string myDocuments = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                string directory = RegistrySettings.GetValue("Geocode Backup Directory", myDocuments).ToString() ?? myDocuments;
                saveDatabase.FileName = $"FTAnalyzer-Geocodes-{DateTime.Now:yyyy-MM-dd}-v{MainForm.VERSION}.zip";
                saveDatabase.InitialDirectory = directory;
                DialogResult result = saveDatabase.ShowDialog();
                if (result == DialogResult.OK)
                {
                    StartBackupRestoreDatabase();
                    if (File.Exists(saveDatabase.FileName))
                        File.Delete(saveDatabase.FileName);
                    FastZip zip = new();
                    string? path = Path.GetDirectoryName(DatabaseFile) ?? throw new OpenDatabaseException("Could not identify existing database path");
                    zip.CreateZip(saveDatabase.FileName, path, false, "Geocodes.s3db");
                    //zip.SetComment(comment + " on " + DateTime.Now.ToString("dd MMM yyyy HH:mm"));
                    //EndBackupDatabase();
                    RegistrySettings.SetValue("Geocode Backup Directory", Path.GetDirectoryName(saveDatabase.FileName) ?? string.Empty, RegistryValueKind.String);
                    UIHelpers.ShowMessage($"Database exported to {saveDatabase.FileName}", "FTAnalyzer Database Export Complete");
                    return true;
                }
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage($"Problem exporting database. Error was {ex.Message}");
            }
            return false;
        }

        void UpgradeDatabase(Version dbVersion)
        {
            try
            {
                Version v3_0_0_0 = new("3.0.0.0");
                Version v3_0_2_0 = new("3.0.2.0");
                Version v3_1_2_0 = new("3.1.2.0");
                Version v3_3_2_5 = new("3.3.2.5");
                Version v7_0_0_0 = new("7.0.0.0");
                Version v7_3_0_0 = new("7.3.0.0");
                Version v7_3_0_1 = new("7.3.0.1");
                Version v7_3_3_2 = new("7.3.3.2");
                Version v7_4_0_0 = new("7.4.0.0");
                Version v8_0_0_0 = new("8.0.0.0");
                Version v8_3_1_0 = new("8.3.1.0");
                if (dbVersion < v3_0_0_0)
                {
                    // Version is less than 3.0.0.0 or none existent so copy latest database from empty database
                    GC.Collect(); // needed to force a cleanup of connections prior to replacing the file.
                    if (File.Exists(DatabaseFile))
                    {
                        File.Delete(DatabaseFile);
                    }
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), DatabaseFile);
                }
                if (InstanceConnection.State != ConnectionState.Open)
                    InstanceConnection.Open();
                if (dbVersion < v3_0_2_0)
                {
                    UpdateDB3_0_2_0();
                }
                if (dbVersion < v3_1_2_0)
                {
                    UpdateDB3_1_2_0();
                }
                if (dbVersion < v3_3_2_5)
                {
                    UpdateDB3_3_2_5();
                }
                if (dbVersion < v7_0_0_0)
                {
                    UpdateDB7_0_0_0();
                }
                if (dbVersion < v7_3_0_0)
                {
                    UpdateDB7_3_0_0();
                }
                if (dbVersion < v7_3_0_1)
                {
                    UpdateDB7_3_0_1();
                }
                if (dbVersion < v7_3_3_2)
                {
                    UpdateDB7_3_3_2();
                }
                if (dbVersion < v7_4_0_0)
                {
                    UpdateDB7_4_0_0();
                }
                if (dbVersion < v8_0_0_0)
                {
                    UpdateDB8_0_0_0();
                }
                if (dbVersion < v8_3_1_0)
                {
                    UpdateDB8_3_1_0();
                }
                InstanceConnection.Close();
            }
            catch (Exception ex)
            {
                UIHelpers.ShowMessage($"Error upgrading database. Error is :{ex.Message}", APPNAME);
            }
        }

        static void UpdateDB8_3_1_0()
        {
            try
            {
                using SQLiteCommand cmd = new("alter table LostCousins add column FullName VARCHAR(80)", InstanceConnection);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException) { } // don't complain if adding field already exists due to beta testing.
            using (SQLiteCommand cmd = new("update versions set Database = '8.3.1.0'", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        static void UpdateDB8_0_0_0()
        {
            using (SQLiteCommand cmd = new("CREATE TABLE IF NOT EXISTS CustomFacts (FactType STRING(60) PRIMARY KEY, [Ignore] BOOLEAN)", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = new("update Versions set database = '8.0.0.0' where platform = 'PC'", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        static void UpdateDB7_4_0_0()
        {
            using (SQLiteCommand cmd = new("drop table versions", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = new("CREATE TABLE IF NOT EXISTS Versions(Platform VARCHAR(10) PRIMARY KEY, [Database] VARCHAR(10));", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = new("insert into Versions(platform, database) values('PC', '7.4.0.0')", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = new("insert into Versions(platform, database) values('Mac', '1.2.0.42')", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }
        static void UpdateDB7_3_3_2()
        {
            try
            {
                using SQLiteCommand cmd = new("SELECT count(*) FROM LostCousins", InstanceConnection);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException)
            {
                using SQLiteCommand cmd = new("create table IF NOT EXISTS LostCousins (CensusYear INTEGER(4), CensusCountry STRING (20), CensusRef STRING(25), IndID STRING(10), FullName String(80), constraint pkLostCousins primary key (CensusYear, CensusCountry, CensusRef, IndID))", InstanceConnection);
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = new("update versions set Database = '7.3.3.2'", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        static void UpdateDB7_3_0_1()
        {
            try
            {
                using SQLiteCommand cmd = new("update table LostCousins add column FullName Varchar(80)", InstanceConnection);
                cmd.ExecuteNonQuery();
            }
            catch (SQLiteException) { } // don't complain if adding field already exists due to beta testing.
            using (SQLiteCommand cmd = new("update versions set Database = '7.3.0.1'", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        static void UpdateDB7_3_0_0()
        {
            try
            {
                using (SQLiteCommand cmd = new("create table if not exists LostCousins (CensusYear INTEGER(4), CensusCountry STRING (20), CensusRef STRING(25), IndID STRING(10), FullName String(80), constraint pkLostCousins primary key (CensusYear, CensusCountry, CensusRef, IndID))", InstanceConnection))
                {
                    cmd.ExecuteNonQuery();
                }
                using (SQLiteCommand cmd = new("update versions set Database = '7.3.0.0'", InstanceConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SQLiteException) { } // skip if table already exists.
        }

        static void UpdateDB7_0_0_0()
        {
            using (SQLiteCommand cmd = new("update Geocode set geocodestatus = 0 where latitude=0 and longitude=0 and geocodestatus=3", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = new("update versions set Database = '7.0.0.0'", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        static void UpdateDB3_3_2_5()
        {
            // mark all bad viewports as not searched
            using (SQLiteCommand cmd = new("update Geocode set latitude = 0, longitude = 0, founddate = date('now'), foundlocation = '', foundlevel = -2, viewport_x_ne = 0, viewport_y_ne = 0, viewport_x_sw = 0, viewport_y_sw = 0, geocodestatus = 0, foundresulttype = '' where latitude<>0 and longitude<>0 and abs(viewport_x_ne) <= 180", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = new("update versions set Database = '3.3.2.5'", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
        }

        void UpdateDB3_1_2_0()
        {
            bool proceed = false;
            if (restoring)
                proceed = true;
            else
            {
                DialogResult result = UIHelpers.ShowMessage("In order to improve speed of the maps a database upgrade is needed.\nThis may take several minutes and must be allowed to complete.\nYou must backup your database first. Ok to proceed?", "Database upgrading", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                Application.UseWaitCursor = true;
                if (result == DialogResult.Yes)
                {
                    SaveFileDialog sfd = new();
                    proceed = BackupDatabase(sfd, "FTAnalyzer zip file created by Database upgrade for v3.2.1.0");
                    sfd.Dispose();
                }
                Application.UseWaitCursor = false;
            }
            if (proceed)
            {
                UpdateTo3_1_2_0();
            }
            else
            {
                UIHelpers.ShowMessage("Database not backed up we cannot proceed to update maps without a safe database backup.\nMapping features will not work correctly.", "Database backup Required");
            }
        }

        static void UpdateTo3_1_2_0()
        {
            bool latm = false;
            bool longm = false;
            using (SQLiteCommand cmd = new("PRAGMA table_info('geocode')", InstanceConnection))
            {
                using SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string column = reader[1].ToString() ?? string.Empty;
                    if (column.Equals("Latm"))
                        latm = true;
                    if (column.Equals("Longm"))
                        longm = true;
                }
            }
            if (!latm)
            {
                using SQLiteCommand cmd = new("alter table geocode add column Latm real default 0.0", InstanceConnection);
                cmd.ExecuteNonQuery();
            }
            if (!longm)
            {
                using SQLiteCommand cmd = new("alter table geocode add column Longm real default 0.0", InstanceConnection);
                cmd.ExecuteNonQuery();
                ConvertLatLongs();
            }
            using (SQLiteCommand cmd = new("update geocode set foundlocation='', foundlevel=-2 where geocodestatus=3", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            using (SQLiteCommand cmd = new("update versions set Database = '3.2.1.0'", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            UIHelpers.ShowMessage("Database lat/long upgrade complete", APPNAME);
        }

        private static void UpdateDB3_0_2_0()
        {
            // Version v3.0.2.0 needs to reset Google Matches to not searched and set partials to level
            //SQLiteCommand cmd = new SQLiteCommand("alter table geocode add column GeocodeStatus integer default 0", conn);
            using (SQLiteCommand cmd = new("update geocode set geocodestatus=0 where geocodestatus=1", InstanceConnection))
            {
                cmd.ExecuteNonQuery(); // reset Google Match to Not Searched
            }
            using (SQLiteCommand cmd = new("update geocode set geocodestatus=7 where geocodestatus=2", InstanceConnection))
            {
                cmd.ExecuteNonQuery(); // set to level mismatch if partial
            }
            using (SQLiteCommand cmd = new("update versions set Database = '3.0.2.0'", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            UIHelpers.ShowMessage("Please note that due to fixes in the way Google reports\nlocations your 'Google Matched' geocodes have been reset.", APPNAME);
        }
        #endregion

        #region Lat/Long Routines
        static void ConvertLatLongs()
        {
            Coordinate Point, NorthEast, SouthWest;
            Coordinate mPoint, mNorthEast, mSouthWest;

            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            int rowcount = 0;
            using (SQLiteCommand cmd = new("select count(*) from geocode where latitude <> 0 and longitude <> 0", InstanceConnection))
            {
                using SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                reader.Read();
                _ = int.TryParse(reader[0].ToString(), out rowcount);
            }
            #region update cmd
            using SQLiteCommand updateCmd = new("update geocode set latm=?, longm=?, viewport_x_ne=?, viewport_y_ne=?, viewport_x_sw=?, viewport_y_sw=?  where location = ?", InstanceConnection);
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
            Progress p = new(rowcount);
            p.Show();
            int row = 0;
            using (SQLiteCommand cmd = new("select location, latitude, longitude, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw from geocode where latitude <> 0 and longitude <> 0", InstanceConnection))
            {
                using SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string location = reader["location"].ToString() ?? string.Empty;
                    _ = double.TryParse(reader["latitude"].ToString(), out double latitude);
                    _ = double.TryParse(reader["longitude"].ToString(), out double longitude);
                    _ = double.TryParse(reader["viewport_x_ne"].ToString(), out double viewport_x_ne);
                    _ = double.TryParse(reader["viewport_y_ne"].ToString(), out double viewport_y_ne);
                    _ = double.TryParse(reader["viewport_x_sw"].ToString(), out double viewport_x_sw);
                    _ = double.TryParse(reader["viewport_y_sw"].ToString(), out double viewport_y_sw);
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
            p.Dispose();
            #endregion
        }

        public static string LatLongHashKey(double latitude, double longitude) => latitude.ToString("F6") + longitude.ToString("F6");

        public static Dictionary<string, Tuple<string, string>> LatLongIndex
        {
            get
            {
                Dictionary<string, Tuple<string, string>> results = [];

                if (InstanceConnection.State != ConnectionState.Open)
                    InstanceConnection.Open();
                string hashkey;
                string foundlocation;
                string foundresulttype;
                using (SQLiteCommand cmd = new(
                    "select distinct latitude, longitude, foundlocation, foundresulttype from geocode where latitude <> 0 and longitude <> 0 and foundlocation<>''", InstanceConnection))
                {
                    using SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        _ = double.TryParse(reader[0].ToString(), out double latitude);
                        _ = double.TryParse(reader[1].ToString(), out double longitude);
                        hashkey = LatLongHashKey(latitude, longitude);
                        foundlocation = reader[2].ToString() ?? string.Empty;
                        foundresulttype = reader[3].ToString() ?? string.Empty;
                        if (!results.ContainsKey(hashkey))
                            results.Add(hashkey, new Tuple<string, string>(foundlocation, foundresulttype));
                    }
                }
                return results;
            }
        }
        #endregion

        #region Commands
        public static bool IsLocationInDatabase(string location)
        {
            bool inDatabase;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand cmd = new("select location from geocode where location = ?", InstanceConnection))
            {
                SQLiteParameter param = cmd.CreateParameter();
                param.DbType = DbType.String;
                cmd.Parameters.Add(param);
                cmd.Prepare();
                cmd.Parameters[0].Value = location;
                using SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                inDatabase = reader.Read();
            }
            return inDatabase;
        }

        public static void ResetPartials()
        {
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand cmd = new("update geocode set latitude = 0, longitude = 0, founddate = date('now'), foundlocation = '', foundlevel = -2, viewport_x_ne = 0, viewport_y_ne = 0, viewport_x_sw = 0, viewport_y_sw = 0, geocodestatus = 0, foundresulttype = '' where geocodestatus in (2,7,9)", InstanceConnection))
            {
                cmd.ExecuteNonQuery();
            }
            InstanceConnection.Close();
        }

        public static void LoadGeoLocations()
        {
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            foreach (FactLocation loc in FactLocation.AllLocations)
            {
                ReadLocationIntoFact(loc, InstanceConnection);
            }
            InstanceConnection.Close();
        }

        public static void GetLocationDetails(FactLocation location)
        {
            if (location is null) return;
            if (string.IsNullOrEmpty(location.ToString())) return;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            ReadLocationIntoFact(location, InstanceConnection);
        }

        static void ReadLocationIntoFact(FactLocation location, SQLiteConnection conn)
        {
            if (location is null) return;
            using SQLiteCommand cmd = new("select latitude, longitude, latm, longm, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus, foundlevel, foundlocation, foundresulttype from geocode where location = ?", conn);
            SQLiteParameter param = cmd.CreateParameter();
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);
            cmd.Prepare();
            cmd.Parameters[0].Value = location.ToString();
            using (SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
            {
                if (reader.Read())
                {
                    _ = double.TryParse(reader["latitude"].ToString(), out double latitude);
                    _ = double.TryParse(reader["longitude"].ToString(), out double longitude);
                    _ = double.TryParse(reader["latm"].ToString(), out double latm);
                    _ = double.TryParse(reader["longm"].ToString(), out double longm);
                    _ = double.TryParse(reader["viewport_x_ne"].ToString(), out double viewport_x_ne);
                    _ = double.TryParse(reader["viewport_y_ne"].ToString(), out double viewport_y_ne);
                    _ = double.TryParse(reader["viewport_x_sw"].ToString(), out double viewport_x_sw);
                    _ = double.TryParse(reader["viewport_y_sw"].ToString(), out double viewport_y_sw);
                    location.Latitude = latitude;
                    location.Longitude = longitude;
                    location.LatitudeM = latm;
                    location.LongitudeM = longm;
                    location.ViewPort ??= new GeoResponse.CResult.CGeometry.CViewPort
                    {
                        NorthEast = new GeoResponse.CResult.CGeometry.CLocation(),
                        SouthWest = new GeoResponse.CResult.CGeometry.CLocation()
                    };
                    location.ViewPort.NorthEast.Lat = viewport_y_ne;
                    location.ViewPort.NorthEast.Long = viewport_x_ne;
                    location.ViewPort.SouthWest.Lat = viewport_y_sw;
                    location.ViewPort.SouthWest.Long = viewport_x_sw;
                    string statusText = reader["geocodestatus"].ToString() ?? string.Empty;
                    location.GeocodeStatus = string.IsNullOrEmpty(statusText) ? FactLocation.Geocode.UNKNOWN :
                                            Enum.Parse<FactLocation.Geocode>(statusText);
                    location.FoundLocation = reader["foundlocation"].ToString() ?? string.Empty;
                    location.FoundResultType = reader["foundresulttype"].ToString() ?? string.Empty;
                    _ = int.TryParse(reader["foundlevel"].ToString(), out int foundlevel);
                    location.FoundLevel = foundlevel;
                }
            }
            if (!ExtensionMethods.DoubleEquals(location.ViewPort.NorthEast.Lat, 0) &&
                !ExtensionMethods.DoubleEquals(location.ViewPort.NorthEast.Long, 0) &&
               location.ViewPort.NorthEast.Long > -180 && location.ViewPort.NorthEast.Long < 180) // fix any ViewPorts stored as mPoints
            {
                location.ViewPort = MapTransforms.TransformViewport(location.ViewPort);
                UpdateGeocode(location);
            }
        }
        public static void InsertGeocode(FactLocation loc)
        {
            if (loc is null) return;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            SQLiteParameter param;
            using SQLiteCommand insertCmd = new("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus, foundresulttype, latm, longm) values(?,?,?,?,date('now'),?,?,?,?,?,?,?,?,?,?)", InstanceConnection);
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

        public static void UpdateGeocode(FactLocation loc)
        {
            if (loc is null) return;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using SQLiteCommand updateCmd = new("update geocode set founddate=date('now'), level = ?, latitude = ?, longitude = ?, foundlocation = ?, foundlevel = ?, viewport_x_ne = ?, viewport_y_ne = ?, viewport_x_sw = ?, viewport_y_sw = ?, geocodestatus = ?, foundresulttype = ?, latm = ?, longm = ? where location = ?", InstanceConnection);
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
                Debug.WriteLine("Problem updating");
            OnGeoLocationUpdated();
        }
        #endregion

        #region LostCousins
        public static int AddLostCousinsFacts()
        {
            int count = 0;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand cmd = new("select CensusYear, CensusCountry, CensusRef, IndID, FullName from LostCousins", InstanceConnection))
            {
                using SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string indID = reader["IndID"].ToString() ?? string.Empty;
                    string fullName = reader["FullName"].ToString() ?? string.Empty;
                    Individual? ind = FamilyTree.Instance.GetIndividual(indID);
                    if (ind?.Name == fullName) // only load if individual exists in this tree.
                    {
                        string CensusYear = reader["CensusYear"].ToString() ?? string.Empty;
                        string CensusCountry = reader["CensusCountry"].ToString() ?? string.Empty;
                        string CensusRef = reader["CensusRef"].ToString() ?? string.Empty;
                        if (!ind.IsLostCousinsEntered(CensusDate.GetLostCousinsCensusYear(new FactDate(CensusYear), true)))
                        {
                            FactLocation location = FactLocation.GetLocation(CensusCountry);
                            Fact f = new(CensusRef, Fact.LOSTCOUSINS, new FactDate(CensusYear), location, string.Empty, true, true);
                            ind?.AddFact(f);
                        }
                        count++;
                    }
                    else
                    {
                        Console.Write("name wrong");
                        // UpdateFullName(reader, ind.Name); needed during testing
                    }
                }
            }
            return count;
        }

        //void UpdateFullName(SQLiteDataReader reader, string name)
        //{
        //    using (SQLiteCommand updateCmd = new SQLiteCommand("update LostCousins set FullName=? Where CensusYear=? and CensusCountry=? and CensusRef=? and IndID=?", InstanceConnection))
        //    {
        //        SQLiteParameter param = updateCmd.CreateParameter();
        //        param.DbType = DbType.String;
        //        updateCmd.Parameters.Add(param);
        //        param = updateCmd.CreateParameter();
        //        param.DbType = DbType.Int32;
        //        updateCmd.Parameters.Add(param);
        //        param = updateCmd.CreateParameter();
        //        param.DbType = DbType.String;
        //        updateCmd.Parameters.Add(param);
        //        param = updateCmd.CreateParameter();
        //        param.DbType = DbType.String;
        //        updateCmd.Parameters.Add(param);
        //        param = updateCmd.CreateParameter();
        //        param.DbType = DbType.String;
        //        updateCmd.Parameters.Add(param);
        //        updateCmd.Prepare();

        //        updateCmd.Parameters[0].Value = name;
        //        updateCmd.Parameters[1].Value = reader["CensusYear"];
        //        updateCmd.Parameters[2].Value = reader["CensusCountry"];
        //        updateCmd.Parameters[3].Value = reader["CensusRef"];
        //        updateCmd.Parameters[4].Value = reader["IndID"];
        //        int rowsaffected = updateCmd.ExecuteNonQuery();
        //        if (rowsaffected != 1)
        //            Debug.WriteLine("Problem updating");
        //    }
        //}

        public static bool LostCousinsExists(CensusIndividual ind)
        {
            if (ind is null) return false;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            bool result = false;
            using (SQLiteCommand cmd = new("SELECT EXISTS(SELECT 1 FROM LostCousins where CensusYear=? and CensusCountry=? and CensusRef=? and IndID=?)", InstanceConnection))
            {
                SQLiteParameter param = cmd.CreateParameter();
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
                cmd.Parameters[1].Value = ind.CensusCountry;
                cmd.Parameters[2].Value = ind.CensusReference;
                cmd.Parameters[3].Value = ind.IndividualID;
                using SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (reader.Read())
                    result = reader[0].ToString() == "1";
            }
            return result;
        }

        public static void StoreLostCousinsFact(CensusIndividual ind, IProgress<string> outputText)
        {
            try
            {
                if (InstanceConnection.State != ConnectionState.Open)
                    InstanceConnection.Open();
                SQLiteParameter param;

                using SQLiteCommand cmd = new("insert into LostCousins (CensusYear, CensusCountry, CensusRef, IndID, FullName) values(?,?,?,?,?)", InstanceConnection);
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

                if (ind.CensusReference is not null)
                {
                    cmd.Parameters[0].Value = ind.CensusDate.BestYear;
                    cmd.Parameters[1].Value = ind.CensusCountry;
                    cmd.Parameters[2].Value = ind.CensusReference;
                    cmd.Parameters[3].Value = ind.IndividualID;
                    cmd.Parameters[4].Value = ind.Name;

                    int rowsaffected = cmd.ExecuteNonQuery();
                    if (rowsaffected != 1)
                        outputText.Report($"\nProblem updating record in database update affected {rowsaffected} records.");
                }
            }
            catch (Exception e)
            {
                outputText.Report($"\nFailed to save Lost Cousins record in database error was: {e.Message}");
            }
        }
        #endregion

        #region Custom Facts

        public static bool IgnoreCustomFact(string factType)
        {
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            bool result = false;
            using (SQLiteCommand cmd = new("SELECT EXISTS(SELECT ignore FROM CustomFacts where FactType=?)", InstanceConnection))
            {
                SQLiteParameter param = cmd.CreateParameter();
                param.DbType = DbType.String;
                cmd.Parameters.Add(param);
                cmd.Prepare();
                cmd.Parameters[0].Value = factType;
                using SQLiteDataReader reader = cmd.ExecuteReader(CommandBehavior.SingleResult);
                if (reader.Read())
                    result = reader[0].ToString() == "1";
            }
            return result;
        }

        public static void IgnoreCustomFact(string factType, bool ignore)
        {
            using SQLiteCommand cmd = new("insert or replace into CustomFacts(FactType,Ignore) values(?,?)", InstanceConnection);
            SQLiteParameter param = cmd.CreateParameter();
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);
            param = cmd.CreateParameter();
            param.DbType = DbType.Boolean;
            cmd.Parameters.Add(param);
            cmd.Prepare();
            cmd.Parameters[0].Value = factType;
            cmd.Parameters[1].Value = ignore;
            int rowsaffected = cmd.ExecuteNonQuery();
        }

        #endregion

        #region Cursor Queries

        public static void AddEmptyLocationsToQueue(ConcurrentQueue<FactLocation> queue)
        {
            if (queue is null) return;
            if (InstanceConnection.State != ConnectionState.Open)
                InstanceConnection.Open();
            using (SQLiteCommand cmd = new("select location from geocode where foundlocation='' and geocodestatus in (3, 8, 9) order by level", InstanceConnection))
            {
                using SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    FactLocation loc = FactLocation.LookupLocation(reader[0].ToString() ?? string.Empty);
                    if (!queue.Contains(loc) &&
                        !ExtensionMethods.DoubleEquals(loc.Latitude, 0) &&
                        !ExtensionMethods.DoubleEquals(loc.Longitude, 0))
                        queue.Enqueue(loc);
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
        public static bool StartBackupRestoreDatabase()
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
                    return FamilyTree.LoadGeoLocationsFromDataBase(outputText);
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
        protected static void OnGeoLocationUpdated()
        {
            GeoLocationUpdated?.Invoke(null, EventArgs.Empty);
        }
        #endregion
    }
}
