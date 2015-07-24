using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class LooseBirthComparer : Comparer<IDisplayLooseBirth>
    {
        public override int Compare(IDisplayLooseBirth a, IDisplayLooseBirth b)
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
