using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class SurnameFilter<T> : Filter<T>  where T : ISurnameFilterable 
    {

        private string searchstring;

        public SurnameFilter(string searchstring)
        {
            this.searchstring = searchstring.ToLower();
        }

        public bool select(T t)
        {
            // If the registration location is not blank and does not
            // contain the search string, then we stop. Otherwise if
            // the registration location is blank, we search all
            // of the facts about this registration.
            if (t.Surname != null && t.Surname.Length > 0)
            {
                return searchstring.Equals(t.Surname.ToLower());
            }
            else
            {
                return false;
            }
        }
    }
}
