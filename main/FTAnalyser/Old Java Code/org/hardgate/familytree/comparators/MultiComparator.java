/*
 * Created on 04-Jan-2005
 *
 */
package org.hardgate.familytree.comparators;

import java.util.ArrayList;
import java.util.Comparator;

/**
 * @author db
 *
 */
public class MultiComparator<T> implements Comparator<T> {

    private ArrayList<Comparator<T> > comparators;
    
    public MultiComparator () {
        comparators = new ArrayList<Comparator<T> >();
    }
    
    public void addComparator (Comparator<T> c) {
        comparators.add(c);
    }
    
    public int compare (T o1, T o2) {
        int result = 0;
        for (Comparator<T> c : comparators) {
            result = c.compare(o1, o2);
            if (result != 0)
                break;
        }
        return result;
    }

}
