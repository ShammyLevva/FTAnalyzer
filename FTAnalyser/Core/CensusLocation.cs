using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace FTAnalyzer
{
    public class CensusLocation
    {
        private static Dictionary<Tuple<string, string>, CensusLocation> CENSUSLOCATIONS = new Dictionary<Tuple<string, string>, CensusLocation>();
        public static readonly CensusLocation UNKNOWN = new CensusLocation(string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        public string Year { get; private set; }
        public string Piece { get; private set; }
        public string RegistrationDistrict { get; private set; }
        public string Parish { get; private set; }
        public string County { get; private set; }
        public string Location { get; private set; }

        static CensusLocation()
        {
            #region Census Locations
            // load Census Locations from XML file
            string startPath;
            if (Application.StartupPath.Contains("COMMON7\\IDE")) // running unit tests
                startPath = Path.Combine(Environment.CurrentDirectory, "..\\..\\..");
            else
                startPath = Application.StartupPath;
            string filename = Path.Combine(startPath, @"Resources\CensusLocations.xml");
            if (File.Exists(filename))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(filename);
                //xmlDoc.Validate(something);
                foreach (XmlNode n in xmlDoc.SelectNodes("CensusLocations/Location"))
                {
                    string year = n.Attributes["Year"].Value;
                    string piece = n.Attributes["Piece"].Value;
                    string RD = n.Attributes["RD"].Value;
                    string parish = n.Attributes["Parish"].Value;
                    string county = n.Attributes["County"].Value;
                    string location = n.InnerText;
                    CensusLocation cl = new CensusLocation(year, piece, RD, parish, county, location);
                    CENSUSLOCATIONS.Add(new Tuple<string, string>(year, piece), cl);
                }
            }
            #endregion
            #region Test CensusLocation.xml file
            //foreach(KeyValuePair<Tuple<string, string>, CensusLocation> kvp in CENSUSLOCATIONS)
            //{
            //    FactLocation.GetLocation(kvp.Value.Location); // force creation of location facts
            //}
            #endregion
        }

        public CensusLocation(string year, string piece, string rd, string parish, string county, string location)
        {
            this.Year = year;
            this.Piece = piece;
            this.RegistrationDistrict = rd;
            this.Parish = parish;
            this.County = county;
            this.Location = location;
        }

        public static CensusLocation GetCensusLocation(string year, string piece)
        {
            CensusLocation result;
            Tuple<string,string> key = new Tuple<string,string>(year, piece);
            CENSUSLOCATIONS.TryGetValue(key, out result);
            return result == null ? CensusLocation.UNKNOWN : result;
        }

        public override string ToString()
        {
            return this.Location.Equals(string.Empty) ? "UNKNOWN" : this.Location;
        }
    }
}
