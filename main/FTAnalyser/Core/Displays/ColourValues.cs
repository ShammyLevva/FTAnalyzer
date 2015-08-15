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
            EMPTY = 0, UNKNOWN_DATE = 1, VERY_WIDE_DATE = 2, WIDE_DATE = 3, JUST_YEAR_DATE = 4, NARROW_DATE = 5, APPROX_DATE = 6,
            EXACT_DATE = 7, NO_SPOUSE = 8, NO_PARTNER = 9, NO_MARRIAGE = 10, ISLIVING = 11, OVER90 = 12
        };
    }
}
