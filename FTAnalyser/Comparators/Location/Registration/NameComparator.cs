using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class NameComparator : IComparer<CensusIndividual>
    {
        public int Compare(CensusIndividual i1, CensusIndividual i2)
        {
            return i1.CompareTo(i2);
        }
    }
}
