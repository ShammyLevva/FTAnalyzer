using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class SurnameFilter : RegistrationFilter
    {

        private String searchString;

        public SurnameFilter(String searchString)
        {
            this.searchString = searchString.ToLower();
        }

        public bool select(Registration r)
        {
            // If the registration location is not blank and does not
            // contain the search string, then we stop. Otherwise if
            // the registration location is blank, we search all
            // of the facts about this registration.
            String surname = r.getSurname();
            if (surname != null && surname.Length > 0)
            {
                return searchString.Equals(surname.ToLower());
            }
            else
            {
                return false;
            }
        }
    }
}
