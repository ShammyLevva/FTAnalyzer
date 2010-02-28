/*
 * Created on 02-Jan-2005
 *
 */
package org.hardgate.familytree;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import org.hardgate.familytree.filters.AllFilter;
import org.hardgate.familytree.filters.RegistrationFilter;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class RegistrationsProcessor {

    RegistrationFilter filter;
    Comparator<Registration> comparator;
    
    public RegistrationsProcessor (RegistrationFilter f, Comparator<Registration> c) {
        filter = f;
        comparator = c;
    }
    
    public RegistrationsProcessor (RegistrationFilter f) {
        this(f, null);
    }
    
    public RegistrationsProcessor (Comparator<Registration> c) {
        this(new AllFilter(), c);
    }
    
    public RegistrationsProcessor () {
        this(new AllFilter(), null);
    }
    
    private List<Registration> filterRegistrations (List<Registration> regs) {
        List<Registration> result = new ArrayList<Registration>(regs.size());
        for (Registration r : regs) {
            if (filter.select(r))
                result.add(r);
        }
        return result;
    }

    private List<Registration> sortRegistrations (List<Registration> regs) {
        if (comparator != null)
            Collections.sort(regs, comparator);
        return regs;
    }

    public List<Registration> processRegistrations (List<Registration> regs) {
        return sortRegistrations(filterRegistrations(regs));
    }
}
