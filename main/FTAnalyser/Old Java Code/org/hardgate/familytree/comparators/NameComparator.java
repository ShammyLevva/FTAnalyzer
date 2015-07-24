/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.comparators;

import java.util.Comparator;

import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.registrations.Registration;

/**
 * @author db
 *
 */
public class NameComparator implements Comparator<Registration> {

    public int compare (Registration r1, Registration r2) {
        Individual i1 = r1.getIndividual();
        Individual i2 = r2.getIndividual();
        return i1.compareTo(i2);
    }
}
