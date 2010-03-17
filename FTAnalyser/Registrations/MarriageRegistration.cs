using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class MarriageRegistration : Registration {

	    private Fact marriage;
	    private ParentalGroup spousesFamily;
        
        public MarriageRegistration (ParentalGroup familyGroup, ParentalGroup spousesFamily, Family marriageFamily) 
            :base(familyGroup) {
            marriage = (marriageFamily == null) ? null :
            	    marriageFamily.getPreferredFact(Fact.MARRIAGE);
            if (marriage == null) {
                registrationDate = null;
            } else {
                registrationDate = marriage.FactDate;
                if (registrationDate != null) {
	                FactDate birth1 = familyGroup.Individual.BirthDate;
	                FactDate birth2 = (spousesFamily == null) ? FactDate.UNKNOWN_DATE :
	                	    spousesFamily.Individual.BirthDate;
	                DateTime maxStart = birth1.StartDate;
	                if (maxStart < birth2.StartDate)
	                    maxStart = birth2.StartDate;
	                if (maxStart > registrationDate.StartDate) {
	                    registrationDate = new FactDate(maxStart, registrationDate.EndDate);
	                }
	            }
            }
            this.spousesFamily = spousesFamily;
            }

        #region Properties

        public string DateOfMarriage {
            get { return (marriage == null) ? "" : marriage.Datestring; }
        }
        
        public string PlaceOfMarriage {
            get { return (marriage == null) ? "" : marriage.Place; }
        }
        
        public string SpousesName {
            get { return (spousesFamily == null) ? "" : spousesFamily.Individual.Name; }
        }

        public string SpousesOccupation {
            get { return (spousesFamily == null) ? "" : spousesFamily.Individual.Occupation; }
        }

        public string SpousesAge {
            get 
            { 
                return (marriage == null || spousesFamily == null) ? "" : spousesFamily.Individual.getAge(marriage.FactDate).ToString(); 
            }
        }

        public string SpousesFathersName {
            get { return (spousesFamily == null) ? "" : spousesFamily.FathersName; }
        }
        
        public string SpousesFathersOccupation {
            get { return (spousesFamily == null) ? "" : spousesFamily.FathersOccupation; }
        }
        
        public string SpousesFatherDeceased {
            get
            {
                return (marriage == null || spousesFamily == null) ? "" : spousesFamily.isFatherDeceased(registrationDate);
            }
        }
        
        public string SpousesMothersName {
            get { return (spousesFamily == null) ? "" : spousesFamily.FathersName; }
        }
        
        public string SpousesMothersOccupation {
            get { return (spousesFamily == null) ? "" : spousesFamily.FathersOccupation; }
        }
        
        public string SpousesMotherDeceased {
            get
            {
                return (marriage == null || spousesFamily == null) ? "" : spousesFamily.isMotherDeceased(registrationDate);
            }
        }
        
        public string MaritalStatus {
            get { return ""; }
        }
        
        public string SpousesMaritalStatus {
            get { return ""; }
        }
        
        public string Residence {
            get { return ""; }
        }
        
        public string SpousesResidence {
            get { return ""; }
        }
        
        public string Religion {
            get { return ""; }
        }

        public ParentalGroup SpousesFamilyGroup {
            get { return spousesFamily; }
        }

        public override string RegistrationLocation
        {
            get { return PlaceOfMarriage; }
        }

        #endregion
        
        public override bool isCertificatePresent() {
            return (marriage == null) ? false : marriage.CertificatePresent;
        }
    }
}