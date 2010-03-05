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
        
        public static readonly LocationFilter SCOTLAND_FILTER =
        	    new LocationFilter("Scotland", Location.COUNTRY);
        
        public static readonly LocationFilter ENGLAND_FILTER =
    	    new LocationFilter("England", Location.COUNTRY);

        public LocationFilter (string searchstring, int level) {
            this.searchstring = searchstring.ToLower();
            this.level = level; 
        }
        
        public bool select (Registration r) {
            // If the registration location is not blank and does not
            // contain the search string, then we stop. Otherwise if
            // the registration location is blank, we search all
            // of the facts about this registration.
            Location l = new Location(r.RegistrationLocation);
            if (!l.isBlank()) {
                return l.Matches(searchstring, level);
            }
            
            bool allLocationsBlank = true;
            foreach (Fact f in r.AllFacts) {
                l = new Location(f.Place);
                if (l.Matches(searchstring, level))
                    return true;
                if (! l.isBlank())
                    allLocationsBlank = false;
            }
            return allLocationsBlank;
        }
    }
}