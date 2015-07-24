using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class IndividualBudgieComparer : Comparer<IDisplayIndividual>
    {
        public override int Compare(IDisplayIndividual x, IDisplayIndividual y)
        {
            // change the + for older to an Z and - for younger to a A to force sort to be right
            string x1 = x.BudgieCode == string.Empty ? "X" : x.BudgieCode.Replace('+', 'z').Replace('-', 'a');
            string y1 = y.BudgieCode == string.Empty ? "X" : y.BudgieCode.Replace('+', 'z').Replace('-', 'a');
            return x1.CompareTo(y1);
        }
    }
}
