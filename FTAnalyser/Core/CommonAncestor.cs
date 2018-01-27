using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class CommonAncestor
    {
        public Individual Ind { get; private set; }
        public int Distance { get; private set; }
        public bool Step { get; private set; }

        public CommonAncestor(Individual ind, int distance, bool step)
        {
            this.Ind = ind;
            this.Distance = distance;
            this.Step = step;
        }
    }
}
