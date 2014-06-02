using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OSGazetteerProcessor
{
    public class OS50kGazetteer
    {
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string ParishName { get; set; }
        public string Line { get; private set; }

        public OS50kGazetteer(string line)
        {
            Line = line;
            string[] values = line.Split(':');
            int intval;
            double latitude, longitude;
            Int32.TryParse(values[4], out intval);
            Double.TryParse(values[5], out latitude);
            Latitude = intval + latitude / 60;
            Int32.TryParse(values[6], out intval);
            Double.TryParse(values[7], out longitude);
            Longitude = intval + longitude / 60;
            if (values[10] == "W")
                Longitude = -1 * Longitude; // West Longitudes are negative
            if (values.Length >= 21)
            {
                ParishName = values[20];
                Line = line.Substring(1, line.LastIndexOf(":") -1);
            }
            Line = Line.Trim(':');
        }

        public override string ToString()
        {
            return Line + ":" + (ParishName ?? "");       
        }
    }
}