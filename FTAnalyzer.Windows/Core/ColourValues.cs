namespace FTAnalyzer.Core
{
    public static class ColourValues
    {
        [Flags]
        public enum BMDColours
        {
            EMPTY = 0, UNKNOWN_DATE = 1, OPEN_ENDED_DATE = 13, VERY_WIDE_DATE = 2, WIDE_DATE = 3, JUST_YEAR_DATE = 4, NARROW_DATE = 5, APPROX_DATE = 6,
            EXACT_DATE = 7, NO_SPOUSE = 8, NO_PARTNER = 9, NO_MARRIAGE = 10, ISLIVING = 11, OVER90 = 12, ALL_RECORDS = 99
        };

        public static readonly Color[] BMDColourValues =
        [
            Color.DarkGray,  Color.Red, Color.Tomato, Color.Orange, Color.YellowGreen, Color.Yellow, Color.PaleGreen, Color.LawnGreen, Color.LightPink, Color.LightCoral,
            Color.Firebrick, Color.WhiteSmoke, Color.DarkGray, Color.OrangeRed
        ];

        [Flags]
        public enum CensusColours
        {
            NOT_ALIVE = 0, NO_CENSUS = 1, CENSUS_PRESENT_LC_MISSING = 2, CENSUS_PRESENT_NOT_LC_YEAR = 3, CENSUS_PRESENT_LC_PRESENT = 4,
            LC_PRESENT_NO_CENSUS = 5, OVERSEAS_CENSUS = 6, OUT_OF_COUNTRY = 7, KNOWN_MISSING = 8, DIED_DURING_CENSUS = 9, BORN_DURING_CENSUS = 10
        };

        public static readonly Color[] CensusColourValues =
        [
            Color.DarkGray, Color.Red , Color.Yellow, Color.LawnGreen, Color.LawnGreen, Color.DarkOrange,
            Color.DarkSlateGray, Color.DarkSlateGray, Color.MediumSeaGreen, Color.PeachPuff , Color.PeachPuff
        ];
    }
}
