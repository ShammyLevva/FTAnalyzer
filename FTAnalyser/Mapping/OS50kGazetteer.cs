using FTAnalyzer.Utilities;
using NetTopologySuite.Geometries;
using System.Linq;
using System.Text.RegularExpressions;

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
        public string CountryName { get; private set; }
        public string ParishName { get; private set; }
        public Point Point { get; private set; }
        public string FuzzyMatch { get; private set; }
        public string FuzzyNoParishMatch { get; private set; }

        public OS50kGazetteer(string line)
        {
            line = EnhancedTextInfo.RemoveDiacritics(line); // remove any special characters for Gazatteer processing
            string[] values = line.Split(':');
            int.TryParse(values[0], out int intval);
            SequenceNumber = intval;
            DefinitiveName = values[2];
            int.TryParse(values[4], out intval);
            double.TryParse(values[5], out double latitude);
            Latitude = intval + latitude / 60;
            int.TryParse(values[6], out intval);
            double.TryParse(values[7], out double longitude);
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
            if (ParishName.EndsWith(" Community"))
                ParishName = ParishName.Substring(0, ParishName.Length - 10);
            FixCommas();
            FixAbbreviations();
            ModernCounty county = Regions.OS_GetCounty(CountyCode);
            if (county == null)
                CountryName = string.Empty;
            else
            {
                CountryName = county.CountryName;
                DoubleMetaphone meta = new DoubleMetaphone(DefinitiveName);
                FuzzyMatch = meta.PrimaryKey + ":";
                FuzzyNoParishMatch = meta.PrimaryKey + ":";
                meta = new DoubleMetaphone(ParishName);
                FuzzyMatch += meta.PrimaryKey + ":";
                meta = new DoubleMetaphone(CountyName);
                FuzzyMatch += meta.PrimaryKey + ":";
                FuzzyNoParishMatch = meta.PrimaryKey + ":";
                meta = new DoubleMetaphone(county.CountryName);
                FuzzyMatch += meta.PrimaryKey;
                FuzzyNoParishMatch = meta.PrimaryKey + ":";
            }
        }

        void FixCommas()
        {
            DefinitiveName = DefinitiveName.Replace(", The", ""); // strip out supurflous "the"
            int pos = DefinitiveName.IndexOf(",");
            if(pos > 0)
                DefinitiveName = (DefinitiveName.Substring(pos + 1) + " " + DefinitiveName.Substring(0, pos)).Trim();
        }

        static Regex slash = new Regex(@"(.*)\(.*\)", RegexOptions.Compiled);

        void FixAbbreviations()
        {
            DefinitiveName = DefinitiveName.Trim();
            if (DefinitiveName.EndsWith(" Fm")) // Fm is abbreviation for Farm
                DefinitiveName = DefinitiveName.Replace(" Fm", " Farm");
            if (DefinitiveName.EndsWith(" fm")) // Fm is abbreviation for Farm
                DefinitiveName = DefinitiveName.Replace(" fm", " Farm");
            if (DefinitiveName.EndsWith(" Fms")) // Fm is abbreviation for Farm
                DefinitiveName = DefinitiveName.Replace(" Fms", " Farm");
            if (DefinitiveName.EndsWith(" Ct")) // Ct is abbreviation for Court
                DefinitiveName = DefinitiveName.Replace(" Ct", " Court");
            if (DefinitiveName.EndsWith(" Pt")) // Pt is abbreviation for Point
                DefinitiveName = DefinitiveName.Replace(" Pt", " Point");
            if (DefinitiveName.EndsWith(" Pk")) // Pt is abbreviation for Park
                DefinitiveName = DefinitiveName.Replace(" Pk", " Park");
            if (DefinitiveName.EndsWith(" Rly")) // Pt is abbreviation for Park
                DefinitiveName = DefinitiveName.Replace(" Rly", " Railway");
            if (DefinitiveName.EndsWith(" Sq")) // Sq is abbreviation for Square
                DefinitiveName = DefinitiveName + "uare";
            if (DefinitiveName.EndsWith(" Sch")) // Sch is abbreviation for School
                DefinitiveName = DefinitiveName + "ool";
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
            if (DefinitiveName.EndsWith(" Pl")) // Pl is abbreviation for Place
                DefinitiveName = DefinitiveName + "ace";
            if (DefinitiveName.EndsWith(" Ave")) // Ave is abbreviation for Avenue
                DefinitiveName = DefinitiveName + "nue";
            if (DefinitiveName.EndsWith(" The")) // we can strip trailing the's
                DefinitiveName = DefinitiveName.Substring(DefinitiveName.Length -4);

            if (DefinitiveName.Contains("("))
            { 
                Match match = slash.Match(DefinitiveName);
                if(match.Success)
                    DefinitiveName = match.Groups[1].ToString().Trim();
            }
            if (ParishName.Contains("("))
            {
                Match match = slash.Match(ParishName);
                if (match.Success)
                    ParishName = match.Groups[1].ToString().Trim();
            }
        }

        public bool IsCountyMatch(FactLocation loc)
        {
            return loc.KnownRegion != null && loc.KnownRegion.CountyCodes.Any(c => c.CountyCode == CountyCode);
        }

        public override string ToString()
        {
            return DefinitiveName + ", " + ParishName + ", " + CountyName + ", " + CountryName;
        }
    }
}