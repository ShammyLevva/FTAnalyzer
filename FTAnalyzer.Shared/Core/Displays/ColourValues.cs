using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public static class ColourValues
    {
        public enum BMDColour
        {
            EMPTY = 0, UNKNOWN_DATE = 1, OPEN_ENDED_DATE = 13, VERY_WIDE_DATE = 2, WIDE_DATE = 3, JUST_YEAR_DATE = 4, NARROW_DATE = 5, APPROX_DATE = 6,
            EXACT_DATE = 7, NO_SPOUSE = 8, NO_PARTNER = 9, NO_MARRIAGE = 10, ISLIVING = 11, OVER90 = 12
        };

        public enum CensusColour
        {
            NOT_ALIVE = 0, NO_CENSUS = 1, CENSUS_PRESENT_LC_MISSING = 2, CENSUS_PRESENT_NOT_LC_YEAR = 3, CENSUS_PRESENT_LC_PRESENT = 4,
            LC_PRESENT_NO_CENSUS = 5, OVERSEAS_CENSUS = 6, OUT_OF_COUNTRY = 7, KNOWN_MISSING = 8
        };
    }
}
