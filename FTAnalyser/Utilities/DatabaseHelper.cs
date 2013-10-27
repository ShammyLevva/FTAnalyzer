using System.Data.SQLite;
using System.Data;
using System;
using System.Windows.Forms;
using System.IO;

namespace FTAnalyzer.Utilities
{
    public class DatabaseHelper : IDisposable
    {
        private static SQLiteConnection conn;
        private static DatabaseHelper instance;
        public string Filename { get; private set; }

        #region Constructor/Destructor
        private DatabaseHelper()
        {
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error upgrading database. Error is :" + ex.Message);
            }
        }
        #endregion

        #region Commands
        public SQLiteCommand GetLocation()
        {
            SQLiteCommand cmd = new SQLiteCommand("select location from geocode where location = ?", conn);
            SQLiteParameter param = cmd.CreateParameter();
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);
            cmd.Prepare();
            return cmd;
        }

        public SQLiteCommand GetLocationDetails()
        {
            SQLiteCommand cmd = new SQLiteCommand("select latitude, longitude, level, foundlevel, foundlocation, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus, foundresulttype from geocode where location = ?", conn);
            SQLiteParameter param = cmd.CreateParameter();
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);
            cmd.Prepare();
            return cmd;
        }

        public SQLiteCommand InsertGeocode()
        {
            SQLiteCommand insertCmd = new SQLiteCommand("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus, foundresulttype) values(?,?,?,?,date('now'),?,?,?,?,?,?,?,?)", conn);
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

            insertCmd.Prepare();
            return insertCmd;
        }

        public void UpdateGeocodeStatus(string location, FactLocation.Geocode status)
        {
            SQLiteCommand updateCmd = new SQLiteCommand("update geocode set founddate = date('now'), geocodestatus = ? where location = ?", conn);

            SQLiteParameter param = updateCmd.CreateParameter();
            param = updateCmd.CreateParameter();
            param.DbType = DbType.Int32;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.String;
            updateCmd.Parameters.Add(param);

            updateCmd.Prepare();
            updateCmd.Parameters[0].Value = status;
            updateCmd.Parameters[1].Value = location;

            updateCmd.ExecuteNonQuery();
        }

        public SQLiteCommand UpdateGeocode()
        {
            SQLiteCommand updateCmd = new SQLiteCommand("update geocode set level = ?, latitude = ?, longitude = ?, founddate = date('now'), foundlocation = ?, foundlevel = ?, viewport_x_ne = ?, viewport_y_ne = ?, viewport_x_sw = ?, viewport_y_sw = ?, geocodestatus = ?, foundresulttype = ? where location = ?", conn);

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
            param.DbType = DbType.String;
            updateCmd.Parameters.Add(param);

            updateCmd.Prepare();
            return updateCmd;
        }

        public SQLiteCommand UpdatePointGeocode()
        {
            SQLiteCommand updatePointCmd = new SQLiteCommand("update geocode set founddate=date('now'), latitude = ?, longitude = ?, viewport_x_ne = ?, viewport_y_ne = ?, viewport_x_sw = ?, viewport_y_sw = ?, geocodestatus = ? where location = ?", conn);

            SQLiteParameter param = updatePointCmd.CreateParameter();

            param = updatePointCmd.CreateParameter();
            param.DbType = DbType.Double;
            updatePointCmd.Parameters.Add(param);

            param = updatePointCmd.CreateParameter();
            param.DbType = DbType.Double;
            updatePointCmd.Parameters.Add(param);

            param = updatePointCmd.CreateParameter();
            param.DbType = DbType.Double;
            updatePointCmd.Parameters.Add(param);

            param = updatePointCmd.CreateParameter();
            param.DbType = DbType.Double;
            updatePointCmd.Parameters.Add(param);

            param = updatePointCmd.CreateParameter();
            param.DbType = DbType.Double;
            updatePointCmd.Parameters.Add(param);

            param = updatePointCmd.CreateParameter();
            param.DbType = DbType.Double;
            updatePointCmd.Parameters.Add(param);

            param = updatePointCmd.CreateParameter();
            param.DbType = DbType.Int32;
            updatePointCmd.Parameters.Add(param);

            param = updatePointCmd.CreateParameter();
            param.DbType = DbType.String;
            updatePointCmd.Parameters.Add(param);

            updatePointCmd.Prepare();
            return updatePointCmd;
        }
        #endregion

        public bool StartBackupDatabase()
        {
            try
            {
                conn.Close();
                GC.Collect(); // needed to force a cleanup of connections prior to replacing the file.
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void EndBackupDatabase()
        {
            if(conn.State != ConnectionState.Open)
                OpenDatabaseConnection();
        }
    }
}
