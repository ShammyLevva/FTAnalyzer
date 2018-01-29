using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class AhnentafelComparer : Comparer<IDisplayIndividual>
    {
        public override int Compare(IDisplayIndividual x, IDisplayIndividual y)
        {
            return x.Ahnentafel.CompareTo(y.Ahnentafel);
        }
    }
}
