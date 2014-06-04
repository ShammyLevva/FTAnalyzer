using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Region
    {
        public string RegionName { get; private set; }
        public string Country { get; private set; }
        public List<string> AlternativeNames { get; private set; }

        public Region(string region, string country)
        {
            Country = country;
            RegionName = region;
            AlternativeNames = new List<string>();
        }

        public void AddAlternateName(string name)
        {
            AlternativeNames.Add(name);
        }
    }
}
