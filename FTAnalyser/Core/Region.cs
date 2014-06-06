using FTAnalyzer.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Region
    {
        public enum Creation { HISTORIC = 0, LG_ACT1974 = 1, MODERN = 2 }

        public string PreferredName { get; private set; }
        public string Country { get; private set; }
        public List<string> AlternativeNames { get; private set; }
        public string ISOcode { get; set; }
        public Creation RegionType { get; private set; }
        public List<CountyConversion.County> CountyCodes { get; private set; }
        
        public Region(string region, string country, Creation regionType)
        {
            Country = country;
            PreferredName = region;
            AlternativeNames = new List<string>();
            ISOcode = string.Empty;
            RegionType = regionType;
            CountyCodes = Mapping.CountyConversion.GetCounties(region);
            if ((Countries.IsEnglandWales(country) || country == Countries.SCOTLAND) && (CountyCodes == null || CountyCodes.Count == 0))
                Console.WriteLine("Missing new county codes for: " + region);
        }

        public void AddAlternateName(string name)
        {
            AlternativeNames.Add(name);
        }

        public override string ToString()
        {
            return PreferredName + ", " + Country;
        }
    }
}
