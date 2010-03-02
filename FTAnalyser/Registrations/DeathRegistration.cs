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
                registrationDate = death.FactDate;
                if (registrationDate != null)
                {
                    FactDate birthDate = familyGroup.Individual.BirthDate;
                    DateTime maxStart = birthDate.StartDate;
                    if (maxStart > registrationDate.StartDate)
                    {
                        registrationDate = new FactDate(maxStart,
                                registrationDate.EndDate);
                    }
                }
            }
            this.spouse = spouse;
            this.maritalStatus = maritalStatus;
        }

        #region Properties

        public string DateOfDeath
        {
            get { return (death == null) ? "" : death.Datestring; }
        }

        public string PlaceOfDeath
        {
            get { return (death == null) ? "" : death.Place; }
        }

        public string SpousesName
        {
            get { return (spouse == null) ? "" : spouse.Name; }
        }

        public string SpousesOccupation
        {
            get { return (spouse == null) ? "" : spouse.Occupation; }
        }

        public string SpouseDeceased
        {
            get { return (death == null) ? "" : getSpouseDeceased(death.FactDate); }
        }

        public string MaritalStatus
        {
            get { return maritalStatus; }
        }

        public string CauseOfDeath
        {
            get { return (death == null) ? "" : death.Comment; }
        }

        public override string RegistrationLocation
        {
            get { return PlaceOfDeath; }
        }

        #endregion

        public string getSpouseDeceased(FactDate when)
        {
            return (spouse == null || !spouse.isDeceased(when)) ? "" : "(Deceased)";
        }

        public override bool isCertificatePresent()
        {
            return (death == null) ? false : death.isCertificatePresent();
        }
    }
}