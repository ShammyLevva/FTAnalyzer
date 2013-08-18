using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class Countries
    {
        private static IDictionary<string, FactLocation> locationCache = new Dictionary<string, FactLocation>();

        public static readonly string SCOTLAND = "Scotland", ENGLAND = "England", CANADA = "Canada", UNITED_STATES = "United States",
            WALES = "Wales", IRELAND = "Ireland", UNITED_KINGDOM = "United Kingdom", NEW_ZEALAND = "New Zealand", AUSTRALIA = "Australia",
            UNKNOWN_COUNTRY = "Unknown", ENG_WALES = "England & Wales", INDIA = "India", FRANCE = "France", GERMANY = "Germany",
            ITALY = "Italy", SPAIN = "Spain";

        private static readonly ISet<string> KNOWN_COUNTRIES = new HashSet<string>(new string[] {
            SCOTLAND, ENGLAND, CANADA, UNITED_STATES, WALES, IRELAND, UNITED_KINGDOM, NEW_ZEALAND, AUSTRALIA, INDIA, FRANCE, GERMANY,
            ITALY, SPAIN
        });

        private static readonly ISet<string> UK_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM
        });

        private static readonly ISet<string> CENSUS_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, IRELAND, UNITED_STATES, CANADA
        });

        public static FactLocation FactLocation(string country)
        {
            FactLocation location;
            if (!locationCache.TryGetValue(country, out location))
            {
                location = new FactLocation(country);
                locationCache.Add(country, location);
            }
            return location;
        }

        public static bool IsUnitedKingdom(string country)
        {
            return UK_COUNTRIES.Contains(country);
        }

        public static bool IsKnownCountry(string country)
        {
            return KNOWN_COUNTRIES.Contains(country);
        }

        public static bool IsCensusCountry(string country)
        {
            return CENSUS_COUNTRIES.Contains(country);
        }
    }
}
