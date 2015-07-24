using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class CommonAncestor
    {
        public Individual ind { get; private set; }
        public int distance { get; private set; }
        public bool step { get; private set; }

        public CommonAncestor(Individual ind, int distance, bool step)
        {
            this.ind = ind;
            this.distance = distance;
            this.step = step;
        }
    }
}
