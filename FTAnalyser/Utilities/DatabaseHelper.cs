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
                String filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\Geocodes.s3db");
                if (!File.Exists(filename))
                {
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), filename);
                }
                conn = new SQLiteConnection("Data Source=" + filename + ";Version=3;");
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
            catch (SQLiteException ex)
            {
                Console.WriteLine(ex.Message);
                UpgradeDatabase(new Version("0.0.0.0"));
            }
        }

        private void UpgradeDatabase(Version dbVersion)
        {
            try
            {
                Version v2_3_0_2 = new Version("2.3.0.2");
                if (dbVersion < v2_3_0_2)
                {
                    // Version is less than 2.3.0.2 or none existent so copy v2.3.0.2 database from empty database
                    conn.Close();
                    GC.Collect(); // needed to force a cleanup of connections prior to replacing the file.
                    String filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), @"Family Tree Analyzer\Geocodes.s3db");
                    if (File.Exists(filename))
                    {
                        File.Delete(filename);
                    }
                    File.Copy(Path.Combine(Application.StartupPath, @"Resources\Geocodes-Empty.s3db"), filename);
                    // Now re-open upgraded database
                    OpenDatabaseConnection();
                }
                //if (dbVersion < v2_3_0_2)
                //{
                    //// Then apply v2.3.0.2 changes
                    //SQLiteCommand cmd = new SQLiteCommand("alter table geocode add column GeocodeStatus integer default 0", conn);
                    //cmd.ExecuteNonQuery();
                    //cmd = new SQLiteCommand("update versions set Database = '2.3.0.2'", conn);
                    //cmd.ExecuteNonQuery();
                //}
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
            SQLiteCommand cmd = new SQLiteCommand("select latitude, longitude, level, foundlevel, foundlocation, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus from geocode where location = ?", conn);
            SQLiteParameter param = cmd.CreateParameter();
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);
            cmd.Prepare();
            return cmd;
        }

        public SQLiteCommand InsertGeocode()
        {
            SQLiteCommand insertCmd = new SQLiteCommand("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw, geocodestatus) values(?,?,?,?,date('now'),?,?,?,?,?,?,?)", conn);
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

            insertCmd.Prepare();
            return insertCmd;
        }

        public SQLiteCommand UpdateGeocode()
        {
            SQLiteCommand updateCmd = new SQLiteCommand("update geocode set level = ?, latitude = ?, longitude = ?, founddate = date('now'), foundlocation = ?, foundlevel = ?, viewport_x_ne = ?, viewport_y_ne = ?, viewport_x_sw = ?, viewport_y_sw = ?, geocodestatus = ? where location = ?", conn);

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
            param.DbType = DbType.String;
            updateCmd.Parameters.Add(param);

            param = updateCmd.CreateParameter();
            param.DbType = DbType.Int32;
            updateCmd.Parameters.Add(param);

            updateCmd.Prepare();
            return updateCmd;
        }
        #endregion
    }
}
