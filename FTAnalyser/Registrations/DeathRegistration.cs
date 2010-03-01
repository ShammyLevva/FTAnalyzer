using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class DeathRegistration : Registration
    {

        private Fact death;
        private Individual spouse;
        private string maritalStatus;

        public DeathRegistration(ParentalGroup familyGroup, Individual spouse, string maritalStatus)
            :base(familyGroup)
        {
            death = familyGroup.getPreferredFact(Fact.DEATH);
            if (death == null)
            {
                registrationDate = null;
            }
            else
            {
                registrationDate = death.getFactDate();
                if (registrationDate != null)
                {
                    FactDate birthDate = familyGroup.getIndividual().getBirthDate();
                    DateTime maxStart = birthDate.getStartDate();
                    if (maxStart > registrationDate.getStartDate())
                    {
                        registrationDate = new FactDate(maxStart,
                                registrationDate.getEndDate());
                    }
                }
            }
            this.spouse = spouse;
            this.maritalStatus = maritalStatus;
        }

        public string getDateOfDeath()
        {
            return (death == null) ? "" : death.getDatestring();
        }

        public string getPlaceOfDeath()
        {
            return (death == null) ? "" : death.getLocation();
        }

        public string getSpousesName()
        {
            return (spouse == null) ? "" : spouse.getName();
        }

        public string getSpousesOccupation()
        {
            return (spouse == null) ? "" : spouse.getOccupation();
        }

        public string getSpouseDeceased()
        {
            return (death == null) ? "" : getSpouseDeceased(death.getFactDate());
        }

        public string getSpouseDeceased(FactDate when)
        {
            return (spouse == null || !spouse.isDeceased(when)) ? "" : "(Deceased)";
        }

        public string getMaritalStatus()
        {
            return maritalStatus;
        }

        public string getCauseOfDeath()
        {
            return (death == null) ? "" : death.getComment();
        }

        public override string getRegistrationLocation()
        {
            return getPlaceOfDeath();
        }

        public override bool isCertificatePresent()
        {
            return (death == null) ? false : death.isCertificatePresent();
        }
    }
}