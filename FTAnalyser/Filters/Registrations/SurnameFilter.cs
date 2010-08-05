using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class SurnameFilter : Filter<Registration>
    {

        private string searchstring;

        public SurnameFilter(string searchstring)
        {
            this.searchstring = searchstring.ToLower();
        }

        public bool select(Registration r)
        {
            // If the registration location is not blank and does not
            // contain the search string, then we stop. Otherwise if
            // the registration location is blank, we search all
            // of the facts about this registration.
            if (r.Surname != null && r.Surname.Length > 0)
            {
                return searchstring.Equals(r.Surname.ToLower());
            }
            else
            {
                return false;
            }
        }
    }
}
