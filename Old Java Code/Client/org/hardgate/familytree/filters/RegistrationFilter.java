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
public interface RegistrationFilter {

    public boolean select (Registration r);
    
}
