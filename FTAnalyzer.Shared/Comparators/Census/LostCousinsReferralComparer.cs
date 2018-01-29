using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class LostCousinsReferralComparer : Comparer<ExportReferrals>
    {
        public override int Compare(ExportReferrals a, ExportReferrals b)
        {
            if (a.ShortCode.Equals(b.ShortCode))
            {
                if (a.Census.Equals(b.Census))
                {
                    if (a.CensusReference.Equals(b.CensusReference))
                    {
                        if (a.FamilyID.Equals(b.FamilyID))
                        {
                            if (a.Age.Equals(b.Age))
                            {
                                if (a.Surname.Equals(b.Surname))
                                    return a.Forenames.CompareTo(b.Forenames);
                                else
                                    return a.Surname.CompareTo(b.Surname);
                            }
                            else
                                return b.Age.CompareTo(a.Age);
                        }
                        else
                            return a.FamilyID.CompareTo(b.FamilyID);
                    }
                    else
                        return a.CensusReference.CompareTo(b.CensusReference);
                }
                else
                    return a.Census.CompareTo(b.Census);
            }
            else 
                return a.ShortCode.CompareTo(b.ShortCode);
        }
    }
}
