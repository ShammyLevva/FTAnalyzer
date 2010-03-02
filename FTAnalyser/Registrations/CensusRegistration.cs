using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class CensusRegistration : Registration
    {

        private CensusFamily censusFamily;

        public CensusRegistration(ParentalGroup familyGroup, FactDate censusDate, CensusFamily censusFamily)
            : base(familyGroup)
        {
            this.registrationDate = censusDate;
            this.censusFamily = censusFamily;
        }

        public override bool isCertificatePresent()
        {
            return false;
        }

        public override string RegistrationLocation
        {
            get { return censusFamily.BestLocation.ToString(); }
        }

        public override List<Fact> AllFacts
        {
            get
            {
                List<Fact> facts = new List<Fact>();
                foreach (Individual i in Members)
                {
                    facts.AddRange(i.AllFacts);
                }
                return facts;
            }
        }

        public List<Individual> Members
        {
            get { return censusFamily.Members; }
        }

        public string FamilyGed
        {
            get { return censusFamily.FamilyGed; }
        }

        public override int Relation
        {
            get { return censusFamily.Relation; }
        }
    }
}