using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class BirthRegistration : Registration {

	    private Fact birth;

        public BirthRegistration (ParentalGroup familyGroup) : base(familyGroup) {
            this.birth = familyGroup.getPreferredFact(Fact.BIRTH);
            registrationDate = (birth == null) ? null : birth.getFactDate();
        }

        public String getRegistrationLocation () {
            return getPlaceOfBirth();
        }

        public bool isCertificatePresent() {
            return (birth == null) ? false : birth.isCertificatePresent();
        }
    }
}