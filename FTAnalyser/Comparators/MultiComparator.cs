using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class MultiComparator<T> : Comparator<T> {

        private List<Comparator<T> > comparators;
        
        public MultiComparator () {
            comparators = new List<Comparator<T> >();
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
}