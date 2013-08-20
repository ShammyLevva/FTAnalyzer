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
            ITALY = "Italy", SPAIN = "Spain", BELGIUM = "Belgium", SOUTH_AFRICA = "South Africa", NORTHERN_IRELAND = "Northern Ireland",
            EGYPT = "Egypt", HUNGARY = "Hungary", MALTA = "Malta", DENMARK = "Denmark", SWEDEN = "Sweden", NORWAY = "Norway", 
            FINLAND = "Finland", ICELAND = "Iceland", SWITZERLAND = "Switzerland", AUSTRIA = "Austria", NETHERLANDS = "Netherlands", 
            CHINA = "China", CEYLON = "Ceylon", ZIMBABWE = "Zimbabwe", JAPAN = "Japan", RUSSIA = "Russia", POLAND = "Poland", 
            LUXEMBOURG = "Luxembourg", ISLE_OF_MAN="Isle of Man", GREECE = "Greece", LIBYA = "Libya", NIGERIA = "Nigeria", 
            BULGARIA = "Bulgaria", CYPRUS = "Cyprus", ESTONIA = "Estonia", GEORGIA = "Georgia", LATVIA = "Latvia", 
            LIECHTENSTIEN = "Liechtenstien", LITHUANIA = "Lithuania", ALBANIA = "Albania", ANDORRA = "Andorra", ARMENIA = "Armenia",
            AZERBAIJAN = "Azerbaijan", BELARUS = "Belarus", MACEDONIA = "Macedonia", MOLDOVA = "Moldova", MONACO = "Monaco", 
            MONTENEGRO = "Montenegro", PORTUGAL = "Portugal", ROMANIA ="Romania", SAN_MARINO = "San Marino", TURKEY = "Turkey",
            UKRAINE = "Ukraine";

        private static readonly ISet<string> KNOWN_COUNTRIES = new HashSet<string>(new string[] {
            SCOTLAND, ENGLAND, CANADA, UNITED_STATES, WALES, IRELAND, UNITED_KINGDOM, NEW_ZEALAND, AUSTRALIA, INDIA, FRANCE, GERMANY,
            ITALY, SPAIN, BELGIUM, SOUTH_AFRICA, NORTHERN_IRELAND, EGYPT, HUNGARY, MALTA, DENMARK, SWEDEN, NORWAY, FINLAND, ICELAND,
            SWITZERLAND, AUSTRIA, NETHERLANDS, CHINA, CEYLON, ZIMBABWE, JAPAN, RUSSIA, POLAND, LUXEMBOURG, ISLE_OF_MAN, GREECE,
            LIBYA, NIGERIA, BULGARIA, CYPRUS, ESTONIA, GEORGIA, LATVIA, LIECHTENSTIEN, LITHUANIA, ALBANIA, ARMENIA, ANDORRA,
            AZERBAIJAN, BELARUS,MOLDOVA, MONACO, MONTENEGRO, PORTUGAL, ROMANIA, SAN_MARINO, TURKEY, UKRAINE
        });

        private static readonly ISet<string> UK_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, NORTHERN_IRELAND
        });

        private static readonly ISet<string> CENSUS_COUNTRIES = new HashSet<string>(new string[] { 
            SCOTLAND, ENGLAND, WALES, ENG_WALES, UNITED_KINGDOM, UNITED_STATES, CANADA
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
