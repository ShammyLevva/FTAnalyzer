/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.filters;

import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class AllFilter implements RegistrationFilter {

    /* (non-Javadoc)
     * @see org.hardgate.familytree.filters.RegistrationFilter#select(org.hardgate.familytree.Registration)
     */
    public boolean select (Registration r) {
        return true;
    }

}
