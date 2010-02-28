/*
 * Created on 02-Jan-2005
 *
 */
package org.hardgate.familytree.filters;

import org.hardgate.familytree.registrations.Registration;


/**
 * @author db
 *
 */
public class SurnameFilter implements RegistrationFilter {

    private String searchString;
    
    public SurnameFilter (String searchString) {
        this.searchString = searchString.toLowerCase();
    }
    
    public boolean select (Registration r) {
        // If the registration location is not blank and does not
        // contain the search string, then we stop. Otherwise if
        // the registration location is blank, we search all
        // of the facts about this registration.
        String surname = r.getSurname();
        if (surname != null && surname.length()>0) {
            return searchString.equals(surname.toLowerCase());
        } else {
        	return false;
        }
    }

}
