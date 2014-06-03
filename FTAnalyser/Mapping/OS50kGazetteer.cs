using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer.Mapping
{
    public class OS50kGazetteer
    {
        public int SequenceNumber { get; private set; }
        public string DefinitiveName { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string FeatureCode { get; private set; }
        public string CountyCode { get; private set; }
        public string CountyName { get; private set; }
        public string ParishName { get; private set; }
        public IPoint Point { get; private set; }

        public OS50kGazetteer(string line)
        {
            string[] values = line.Split(':');
            int intval;
            Int32.TryParse(values[0], out intval);
            SequenceNumber = intval;
            DefinitiveName = values[2];
            double latitude, longitude;
            Int32.TryParse(values[4], out intval);
            Double.TryParse(values[5], out latitude);
            Latitude = intval + latitude / 60;
            Int32.TryParse(values[6], out intval);
            Double.TryParse(values[7], out longitude);
            Longitude = intval + longitude / 60;
            if (values[10] == "W")
                Longitude = -1 * Longitude; // West Longitudes are negative
            Coordinate c = new Coordinate(Longitude, Latitude);
            c = MapTransforms.TransformCoordinate(c);
            Point = GeometryFactory.Default.CreatePoint(c);

            CountyCode = values[11];
            CountyName = values[13];
            FeatureCode = values[14];
            ParishName = values[20];
            if (ParishName.EndsWith(" CP"))
                ParishName = ParishName.Substring(0, ParishName.Length - 3);
            FixCommas();
            FixAbbreviations();
        }

        private void FixCommas()
        {
            DefinitiveName = DefinitiveName.Replace(", The", ""); // strip out supurflous "the"
            int pos = DefinitiveName.IndexOf(",");
            if(pos > 0)
            {
                DefinitiveName = (DefinitiveName.Substring(pos + 1) + " " + DefinitiveName.Substring(0, pos)).Trim();
            }
        }

        private void FixAbbreviations()
        {
            if (DefinitiveName.EndsWith(" Fm")) // Fm is abbreviation for Farm
                DefinitiveName = DefinitiveName.Replace(" Fm", " Farm");
            if (DefinitiveName.EndsWith(" fm")) // Fm is abbreviation for Farm
                DefinitiveName = DefinitiveName.Replace(" fm", " Farm");
            if (DefinitiveName.EndsWith(" Ct")) // Ct is abbreviation for Court
                DefinitiveName = DefinitiveName.Replace(" Ct", " Court");
            if (DefinitiveName.EndsWith(" Pt")) // Pt is abbreviation for Point
                DefinitiveName = DefinitiveName.Replace(" Pt", " Point"); ;
            if (DefinitiveName.EndsWith(" Sq")) // Sq is abbreviation for Square
                DefinitiveName = DefinitiveName + "uare";
            if (DefinitiveName.EndsWith(" Ho")) // Ho is abbreviation for House
                DefinitiveName = DefinitiveName + "use";
            if (DefinitiveName.EndsWith(" St")) // St is abbreviation for Street
                DefinitiveName = DefinitiveName + "reet";
            if (DefinitiveName.EndsWith(" Sta")) // Sta is abbreviation for Station
                DefinitiveName = DefinitiveName + "tion";
            if (DefinitiveName.EndsWith(" Br")) // Br is abbreviation for Bridge
                DefinitiveName = DefinitiveName + "idge";
            if (DefinitiveName.EndsWith(" Ch")) // Ch is abbreviation for Church
                DefinitiveName = DefinitiveName + "urch";
        }

        public bool IsCountyMatch(FactLocation loc)
        {
            return loc.Counties.Any(c => c.CountyCode == CountyCode);
        }

        public override string ToString()
        {
            return CountyCode + ": " + DefinitiveName + "(" + CountyName + ")";
        }
    }
}