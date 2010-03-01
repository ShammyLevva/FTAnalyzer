using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class NameComparator : IComparer<Registration>
    {
        public int Compare (Registration r1, Registration r2) {
            Individual i1 = r1.getIndividual();
            Individual i2 = r2.getIndividual();
            return i1.CompareTo(i2);
        }
    }
}
