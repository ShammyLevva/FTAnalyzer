using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class LocationFilter : RegistrationFilter {

        private string searchstring;
        private int level;
        
        public static readonly LocationFilter SCOTLAND = new LocationFilter("Scotland", FactLocation.COUNTRY);
        public static readonly LocationFilter ENGLAND = new LocationFilter("England", FactLocation.COUNTRY);
        public static readonly LocationFilter WALES = new LocationFilter("Wales", FactLocation.COUNTRY);
        public static readonly LocationFilter CANADA = new LocationFilter("Canada", FactLocation.COUNTRY);
        public static readonly LocationFilter USA = new LocationFilter("USA", FactLocation.COUNTRY);
        public static readonly LocationFilter US = new LocationFilter("United States", FactLocation.COUNTRY);
        public static readonly LocationFilter IRELAND = new LocationFilter("Ireland", FactLocation.COUNTRY);
        public static readonly LocationFilter EIRE = new LocationFilter("Eire", FactLocation.COUNTRY);

        public LocationFilter(string searchstring, int level)
        {
            this.searchstring = searchstring.ToLower();
            this.level = level; 
        }
        
        public bool select (Registration r) {
            // If the registration location is not blank and does not
            // contain the search string, then we stop. Otherwise if
            // the registration location is blank, we search all
            // of the facts about this registration.
            FactLocation l = new FactLocation(r.RegistrationLocation);
            if (!l.isBlank()) {
                return l.Matches(searchstring, level);
            }
            
            bool allLocationsBlank = true;
            foreach (Fact f in r.AllFacts) {
                l = new FactLocation(f.Place);
                if (l.Matches(searchstring, level))
                    return true;
                if (! l.isBlank())
                    allLocationsBlank = false;
            }
            return allLocationsBlank;
        }
    }
}