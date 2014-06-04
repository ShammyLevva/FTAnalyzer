using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Regions
    {
        public static List<Region> SCOTTISH_REGIONS;
        public static List<Region> ENGLISH_REGIONS;
        public static List<Region> WELSH_REGIONS;
        public static List<Region> UK_REGIONS;

        static Regions()
        {
            SCOTTISH_REGIONS = new List<Region>();
            ENGLISH_REGIONS = new List<Region>();
            WELSH_REGIONS = new List<Region>();

            UK_REGIONS = new List<Region>();
            UK_REGIONS.AddRange(SCOTTISH_REGIONS);
            UK_REGIONS.AddRange(ENGLISH_REGIONS);
            UK_REGIONS.AddRange(WELSH_REGIONS);
        }
    }
}
