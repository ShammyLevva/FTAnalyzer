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

        public String getRegistrationLocation()
        {
            return censusFamily.getBestLocation().ToString();
        }

        public bool isCertificatePresent()
        {
            return false;
        }

        public List<Fact> getAllFacts() {
            List<Fact> facts = new List<Fact>();
            foreach (Individual i in getMembers()) {
                facts.AddRange(i.getAllFacts());
            }
            return facts;
        }

        public List<Individual> getMembers()
        {
            return censusFamily.getMembers();
        }

        public String getFamilyGed()
        {
            return censusFamily.getFamilyGed();
        }

        public int getRelation()
        {
            return censusFamily.getRelation();
        }
    }
}