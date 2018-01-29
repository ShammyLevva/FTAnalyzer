using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class LooseDeathComparer : Comparer<IDisplayLooseDeath>
    {
        public override int Compare(IDisplayLooseDeath a, IDisplayLooseDeath b)
        {
            if (a.Surname.Equals(b.Surname))
            {
                if (a.Forenames.Equals(b.Forenames))
                    return a.BirthDate.CompareTo(b.BirthDate);
                else
                    return a.Forenames.CompareTo(b.Forenames);
            }
            else
                return a.Surname.CompareTo(b.Surname);
        }
    }
}
