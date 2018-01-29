using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class ExportReferrals : IExportReferrals
    {
        private Individual ind;
        private Fact f;
        private Fact censusFact;

        public ExportReferrals(Individual ind, Fact f)
        {
            this.ind = ind;
            this.f = f;
            this.censusFact = ind.GetCensusFact(f);
            this.ShortCode = GetShortCode();
            this.RelationType = SetRelationType();
            this.Include = ind.IsBloodDirect;
        }

        public string CensusDate { get { return f.FactDate.ToString(); } }
        public string CensusReference
        {
            get
            {
                if (censusFact == null)
                    return "Census Not Found";
                return censusFact.CensusReference == null ? string.Empty : censusFact.CensusReference.Reference;
            }
        }
        public string IndividualID { get { return ind.IndividualID; } }
        public string FamilyID { get { return ind.ReferralFamilyID; } }
        public string Forenames { get { return ind.Forenames; } }
        public string Surname { get { return ind.Surname; } }
        public Age Age { get { return ind.GetAge(f.FactDate); } }
        public string ShortCode { get; private set; }
        public string Census { get { return censusFact == null ? f.ToString() : censusFact.ToString(); } }
        public bool Include { get; private set; }
        public string RelationType { get; private set; }

        public string SetRelationType()
        {
            if (ind.RelationType == Individual.DIRECT)
                return Properties.Messages.Referral_Direct;
            if (ind.RelationType == Individual.BLOOD)
                return Properties.Messages.Referral_Blood;
            else if (ind.RelationType == Individual.MARRIEDTODB)
                return Properties.Messages.Referral_Marriage;
            else return string.Empty;
        }

        public string GetShortCode()
        {
            if (censusFact == null)
                return string.Empty;
            if (censusFact.FactDate.Overlaps(FTAnalyzer.CensusDate.EWCENSUS1881) && censusFact.Location.IsEnglandWales)
                return "RG11";
            if (censusFact.FactDate.Overlaps(FTAnalyzer.CensusDate.SCOTCENSUS1881) && censusFact.Location.Equals(Countries.SCOTLAND))
                return "SCT1";
            if (censusFact.FactDate.Overlaps(FTAnalyzer.CensusDate.CANADACENSUS1881) && censusFact.Location.Equals(Countries.CANADA))
                return "CAN1";
            if (censusFact.FactDate.Overlaps(FTAnalyzer.CensusDate.USCENSUS1880) && censusFact.Location.Equals(Countries.UNITED_STATES))
                return "USA1";
            if (censusFact.FactDate.Overlaps(FTAnalyzer.CensusDate.EWCENSUS1841) && censusFact.Location.IsEnglandWales)
                return "1841";
            if (censusFact.FactDate.Overlaps(FTAnalyzer.CensusDate.IRELANDCENSUS1911) && censusFact.Location.Equals(Countries.IRELAND))
                return "0IRL";
            if (censusFact.FactDate.Overlaps(FTAnalyzer.CensusDate.EWCENSUS1911) && censusFact.Location.IsEnglandWales)
                return "0ENG";
            if (censusFact.FactDate.Overlaps(FTAnalyzer.CensusDate.USCENSUS1940) && censusFact.Location.Equals(Countries.UNITED_STATES))
                return "USA4";
            return string.Empty;
        }
    }
}
