using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class MultiComparator<T> : IComparer<T>
    {
        private List<IComparer<T>> comparators;
        
        public MultiComparator () {
            comparators = new List<IComparer<T>>();
        }

        public void addComparator(IComparer<T> c)
        {
            comparators.Add(c);
        }
        
        public int Compare (T o1, T o2) {
            int result = 0;
            foreach (IComparer<T> c in comparators) {
                result = c.Compare(o1, o2);
                if (result != 0)
                    break;
            }
            return result;
        }
    }
}