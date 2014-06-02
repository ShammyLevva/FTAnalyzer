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
            CountyCode = values[11];
            CountyName = values[13];
            FeatureCode = values[14];
            ParishName = values[20];
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