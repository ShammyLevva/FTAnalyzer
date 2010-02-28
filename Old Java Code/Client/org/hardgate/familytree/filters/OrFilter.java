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
public class OrFilter implements RegistrationFilter {

    private RegistrationFilter filter1;
    private RegistrationFilter filter2;
    
    public OrFilter (RegistrationFilter f1, RegistrationFilter f2) {
        this.filter1 = f1;
        this.filter2 = f2;
    }
    
    public boolean select (Registration r) {
        return filter1.select(r) || filter2.select(r);
    }
}
