/*
 * Created on 16-Feb-2005
 *
 */
package org.hardgate.familytree.comparators;

import java.util.Comparator;

import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.Individual;

/**
 * @author A-Bisset
 *
 */
public class CensusAgeComparator implements Comparator<Individual> {
    
    private FactDate date;

    public int compare (Individual i1, Individual i2) {
        if (i1.getStatus().equals(i2.getStatus()))
            // same status so sort by date
            return sortBirthdate(i1,i2);
        if (i1.getStatus().equals(Individual.HUSBAND))
            return -1;
	    if (i2.getStatus().equals(Individual.HUSBAND))
	        return 1;
	    // neither is husband so is one a wife
        if (i1.getStatus().equals(Individual.WIFE))
            return -1;
	    if (i2.getStatus().equals(Individual.WIFE))
	        return 1;
	    // neither is husband or wife so is one a child
        if (i1.getStatus().equals(Individual.CHILD))
            return -1;
	    if (i2.getStatus().equals(Individual.CHILD))
	        return 1;
	    return 0;
    }
    
    private int sortBirthdate (Individual i1, Individual i2) {
        Fact b1 = i1.getPreferredFact(Fact.BIRTH);
        Fact b2 = i2.getPreferredFact(Fact.BIRTH);
        FactDate d1 = (b1 == null) ? FactDate.UNKNOWN_DATE : b1.getFactDate();
        FactDate d2 = (b2 == null) ? FactDate.UNKNOWN_DATE : b2.getFactDate();
        return d1.compareTo(d2);        
    }
}
