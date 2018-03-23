using GeoAPI.Geometries;
using NetTopologySuite.Geometries;
using System;
using System.Globalization;
using System.Text;

namespace OSGazetteerProcessor
{
    public class OS50kGazetteer
    {
        public string CountyCode { get; private set; }
        public string CountyName { get; private set; }
        public double Latitude { get; private set; }
        public double Longitude { get; private set; }
        public string ParishName { get; set; }
        public string Line { get; private set; }
        public IPoint Point { get; private set; }
        public IGeometry BufferedPoint { get; private set; }

        // 1:TQ6004:1066 Country Walk:TQ60:50:49:0:16.7:104500:560500:E:ES:E Susx:East Sussex:X:20-SEP-2011:I:199:0:0:Westham

        public OS50kGazetteer(string line)
        {
            Line = RemoveDiacritics(line);
            string[] values = line.Split(':');
            CountyCode = values[11];
            CountyName = values[13];
            Int32.TryParse(values[4], out int intval);
            Double.TryParse(values[5], out double latitude);
            Latitude = intval + latitude / 60;
            Int32.TryParse(values[6], out intval);
            Double.TryParse(values[7], out double longitude);
            Longitude = intval + longitude / 60;
            if (values[10] == "W")
                Longitude = -1 * Longitude; // West Longitudes are negative
            Coordinate c = new Coordinate(Longitude, Latitude);
            c = MapTransforms.TransformCoordinate(c);
            Point = GeometryFactory.Default.CreatePoint(c);
            BufferedPoint = Point.Buffer(200); // points are to 1dp of a minute = 185m so create buffer for matching use 200 for bit wider scope
            if (values.Length >= 21)
            {
                ParishName = values[20];
                Line = line.Substring(1, line.LastIndexOf(":") -1);
            }
            Line = Line.Trim(':');
        }

        private string RemoveDiacritics(string text)
        {
            string formD = text.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            foreach (char ch in formD)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                    sb.Append(ch);
            }

            return sb.ToString().Normalize(NormalizationForm.FormC);
        }

        public override string ToString()
        {
            return Line + ":" + (ParishName ?? "");       
        }
    }
}