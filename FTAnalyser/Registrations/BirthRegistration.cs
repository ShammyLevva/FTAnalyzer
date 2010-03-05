using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class BirthRegistration : Registration {

	    private Fact birth;

        public BirthRegistration (ParentalGroup familyGroup) : base(familyGroup) {
            this.birth = familyGroup.getPreferredFact(Fact.BIRTH);
            registrationDate = (birth == null) ? null : birth.FactDate;
        }

        public override string RegistrationLocation
        {
            get { return PlaceOfBirth; }
        }

        public override bool isCertificatePresent()
        {
            return (birth == null) ? false : birth.isCertificatePresent();
        }
    }
}