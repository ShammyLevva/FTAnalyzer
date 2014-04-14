using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class ExportReferrals
    {
        private Individual ind;
        private Fact f;
        private Fact censusFact;
        
        public ExportReferrals(Individual ind, Fact f)
        {
            this.ind = ind;
            this.f = f;
            this.censusFact = ind.LostCousinsCensusFact(f);
            this.ShortCode = GetShortCode();
        }

        public string CensusReference { get { return censusFact == null ? "Census Not Found" : censusFact.CensusReference; } }
        public string IndividualID { get { return ind.IndividualID; } }
        public string FamilyID { get { return ind.ReferralFamilyID; } }
        public string Forenames { get { return ind.Forenames; } }
        public string Surname { get { return ind.Surname; } }
        public Age Age { get { return ind.GetAge(f.FactDate); } }
        public string ShortCode { get; private set; }
        public string Census { get { return censusFact == null ? f.ToString() : censusFact.ToString(); } }
        public bool Include { get { return ind.IsBloodDirect; } }
        public string RelationType { get { return ind.IsBloodDirect ? ind.Relation : string.Empty; } }// don't show type if shouldn't be included as it confuses

        public string GetShortCode()
        {
            if (censusFact == null)
                return string.Empty;
            if (censusFact.FactDate.Overlaps(CensusDate.EWCENSUS1881) && censusFact.Location.IsEnglandWales)
                return "RG11";
            if (censusFact.FactDate.Overlaps(CensusDate.SCOTCENSUS1881) && censusFact.Location.Equals(Countries.SCOTLAND))
                return "SCT1";
            if (censusFact.FactDate.Overlaps(CensusDate.CANADACENSUS1881) && censusFact.Location.Equals(Countries.CANADA))
                return "CAN1";
            if (censusFact.FactDate.Overlaps(CensusDate.USCENSUS1880) && censusFact.Location.Equals(Countries.UNITED_STATES))
                return "USA1";
            if (censusFact.FactDate.Overlaps(CensusDate.EWCENSUS1841) && censusFact.Location.IsEnglandWales)
                return "1841";
            if (censusFact.FactDate.Overlaps(CensusDate.IRELANDCENSUS1911) && censusFact.Location.Equals(Countries.IRELAND))
                return "0IRL";
            if (censusFact.FactDate.Overlaps(CensusDate.EWCENSUS1911) && censusFact.Location.IsEnglandWales)
                return "0ENG";
            if (censusFact.FactDate.Overlaps(CensusDate.USCENSUS1940) && censusFact.Location.Equals(Countries.UNITED_STATES))
                return "USA4";
            return string.Empty;
        }
    }
}
