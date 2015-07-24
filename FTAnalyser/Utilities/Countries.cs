using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace FTAnalyzer.Utilities
{
    class Countries
    {
        static IEnumerable<Country> GetCountries()
        {
            return from ri in
                       from ci in CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                       select new RegionInfo(ci.LCID)
                   group ri by ri.TwoLetterISORegionName into g
                   //where g.Key.Length == 2
                   select new Country
                   {
                       CountryId = g.Key,
                       Title = g.First().DisplayName
                   };
        }

        class Country
        {
            public string CountryId { get; set; }
            public string Title { get; set; }
        }
    }
}
