using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class LocationFilter<T> : Filter<T> where T: ILocationFilterable
    {

        private string searchstring;
        private int level;

        public static readonly LocationFilter<T> SCOTLAND = new LocationFilter<T>("Scotland", FactLocation.COUNTRY);
        public static readonly LocationFilter<T> ENGLAND = new LocationFilter<T>("England", FactLocation.COUNTRY);
        public static readonly LocationFilter<T> WALES = new LocationFilter<T>("Wales", FactLocation.COUNTRY);
        public static readonly LocationFilter<T> CANADA = new LocationFilter<T>("Canada", FactLocation.COUNTRY);
        public static readonly LocationFilter<T> USA = new LocationFilter<T>("USA", FactLocation.COUNTRY);
        public static readonly LocationFilter<T> US = new LocationFilter<T>("United States", FactLocation.COUNTRY);
        public static readonly LocationFilter<T> IRELAND = new LocationFilter<T>("Ireland", FactLocation.COUNTRY);
        public static readonly LocationFilter<T> EIRE = new LocationFilter<T>("Eire", FactLocation.COUNTRY);

        public LocationFilter(string searchstring, int level)
        {
            this.searchstring = searchstring;
            this.level = level;
        }
        
        public bool select (T t) {
            // If the location is not blank and does not
            // contain the search string, then we stop. Otherwise if
            // the location is blank, we search all
            // of the facts about this filterable location.
            FactLocation l = t.FilterLocation;
            if (!l.isBlank()) {
                return l.Matches(searchstring, level);
            }
            
            bool allLocationsBlank = true;
            foreach (Fact f in t.AllFacts) {
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