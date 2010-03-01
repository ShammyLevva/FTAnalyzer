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
        private String maritalStatus;

        public DeathRegistration(ParentalGroup familyGroup, Individual spouse, String maritalStatus)
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

        public String getDateOfDeath()
        {
            return (death == null) ? "" : death.getDateString();
        }

        public String getPlaceOfDeath()
        {
            return (death == null) ? "" : death.getLocation();
        }

        public String getSpousesName()
        {
            return (spouse == null) ? "" : spouse.getName();
        }

        public String getSpousesOccupation()
        {
            return (spouse == null) ? "" : spouse.getOccupation();
        }

        public String getSpouseDeceased()
        {
            return (death == null) ? "" : getSpouseDeceased(death.getFactDate());
        }

        public String getSpouseDeceased(FactDate when)
        {
            return (spouse == null || !spouse.isDeceased(when)) ? "" : "(Deceased)";
        }

        public String getMaritalStatus()
        {
            return maritalStatus;
        }

        public String getCauseOfDeath()
        {
            return (death == null) ? "" : death.getComment();
        }

        public String getRegistrationLocation()
        {
            return getPlaceOfDeath();
        }

        public bool isCertificatePresent()
        {
            return (death == null) ? false : death.isCertificatePresent();
        }
    }
}