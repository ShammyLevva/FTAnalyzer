using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class CommonAncestor
    {
        private Individual ind;
        private int distance;

        public CommonAncestor(Individual ind, int distance)
        {
            this.ind = ind;
            this.distance = distance;
        }
    }
}
