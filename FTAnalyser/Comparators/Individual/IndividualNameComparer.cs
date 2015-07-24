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
            int j = x.Forenames.CompareTo(y.Forenames);
            if(i==0)
                return (j==0) ? x.BirthDate.CompareTo(y.BirthDate) : j;
            else
                return i;
        }
    }
}
