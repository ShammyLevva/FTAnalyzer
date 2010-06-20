using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class IndividualNameComparer : Comparer<IDisplayIndividual>
    {

        public override int Compare(IDisplayIndividual x, IDisplayIndividual y)
        {
            int i = x.Surname.CompareTo(y.Surname);
            return (i==0) ? x.Forenames.CompareTo(y.Forenames) : i;
        }
    }
}
