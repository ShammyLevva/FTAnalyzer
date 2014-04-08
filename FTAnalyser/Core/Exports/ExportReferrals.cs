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
        }

        public string CensusReference { get { return censusFact == null ? string.Empty : censusFact.CensusReference; } }
        public string ID { get { return ind.IndividualID; } }
        public string Forenames { get { return ind.Forenames; } }
        public string Surname { get { return ind.Surname; } }
        public FactDate BirthDate { get { return ind.BirthDate; } }
        public string Census { get { return censusFact == null ? string.Empty : censusFact.ToString(); } }
        public bool Include { get { return ind.IsBloodDirect; } }
        public string RelationType { get { return ind.IsBloodDirect ? ind.Relation : string.Empty; } }// don't show type if shouldn't be included as it confuses
    }
}
