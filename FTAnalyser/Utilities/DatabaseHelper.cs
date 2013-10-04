using System.Data.SQLite;
using System.Data;
using System;

namespace FTAnalyzer.Utilities
{
    public class DatabaseHelper : IDisposable
    {
        public SQLiteConnection conn;

        public DatabaseHelper()
        {
            conn = FamilyTree.Instance.GetDatabaseConnection();
            conn.Open();
        }

        public void Dispose()
        {
            conn.Close();
        }

        public SQLiteCommand GetLocation()
        {
            SQLiteCommand cmd = new SQLiteCommand("select location from geocode where location = ?", conn);
            SQLiteParameter param = cmd.CreateParameter();
            param.DbType = DbType.String;
            cmd.Parameters.Add(param);
            cmd.Prepare();
            return cmd;
        }

        public SQLiteCommand InsertGeocode()
        {
            SQLiteCommand insertCmd = new SQLiteCommand("insert into geocode (location, level, latitude, longitude, founddate, foundlocation, foundlevel, viewport_x_ne, viewport_y_ne, viewport_x_sw, viewport_y_sw) values(?,?,?,?,date('now'),?,?,?,?,?,?)", conn);
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

            insertCmd.Prepare();
            return insertCmd;
        }

        public SQLiteCommand UpdateGeocode()
        {
            SQLiteCommand updateCmd = new SQLiteCommand("update geocode set level = ?, latitude = ?, longitude = ?, founddate = date('now'), foundlocation = ?, foundlevel = ? viewport_x_ne = ?, viewport_y_ne = ?, viewport_x_sw = ?, viewport_y_sw = ? where location = ?", conn);

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

            updateCmd.Prepare();
            return updateCmd;
        }
    }
}
