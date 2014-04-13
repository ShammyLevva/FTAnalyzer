using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class LostCousinsReferralComparer : Comparer<ExportReferrals>
    {
        public override int Compare(ExportReferrals a, ExportReferrals b)
        {  // needs to compare census LCcodes then if census ref is empty compare sortable addresses
            if (a.Census.Equals(b.Census))
            {
                if (a.CensusReference.Equals(b.Census))
                {
                    if (a.Age.Equals(b.Age))
                    {
                        if (a.Surname.Equals(b.Surname))
                            return a.Forenames.CompareTo(b.Forenames);
                        else
                            return a.Surname.CompareTo(b.Surname);
                    }
                    else
                    {
                        return b.Age.CompareTo(a.Age);
                    }

                }
                else
                    return a.CensusReference.CompareTo(b.CensusReference);
            }
            else
                return a.Census.CompareTo(b.Census);
        }
    }
}
