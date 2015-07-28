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
public class RelationFilter implements RegistrationFilter {

    private int filterType;
    
    public RelationFilter(int filterType) {
        this.filterType = filterType;
    }
    
    public boolean select (Registration r) {
        return r.getRelation() == filterType;
    }
}