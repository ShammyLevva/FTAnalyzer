using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class LocationFilter : RegistrationFilter {

        private String searchString;
        private int level;
        
        public static readonly LocationFilter SCOTLAND_FILTER =
        	    new LocationFilter("Scotland", Location.COUNTRY);
        
        public static readonly LocationFilter ENGLAND_FILTER =
    	    new LocationFilter("England", Location.COUNTRY);

        public LocationFilter (String searchString, int level) {
            this.searchString = searchString.ToLower();
            this.level = level; 
        }
        
        public bool select (Registration r) {
            // If the registration location is not blank and does not
            // contain the search string, then we stop. Otherwise if
            // the registration location is blank, we search all
            // of the facts about this registration.
            Location l = new Location(r.getRegistrationLocation());
            if (!l.isBlank()) {
                return l.Matches(searchString, level);
            }
            
            bool allLocationsBlank = true;
            foreach (Fact f in r.getAllFacts()) {
                l = new Location(f.getLocation());
                if (l.Matches(searchString, level))
                    return true;
                if (! l.isBlank())
                    allLocationsBlank = false;
            }
            return allLocationsBlank;
        }
    }
}