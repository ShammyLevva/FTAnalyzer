using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
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
                registrationDate = marriage.getFactDate();
                if (registrationDate != null) {
	                FactDate birth1 = familyGroup.getIndividual().getBirthDate();
	                FactDate birth2 = (spousesFamily == null) ? FactDate.UNKNOWN_DATE :
	                	    spousesFamily.getIndividual().getBirthDate();
	                DateTime maxStart = birth1.getStartDate();
	                if (maxStart < birth2.getStartDate())
	                    maxStart = birth2.getStartDate();
	                if (maxStart > registrationDate.getStartDate()) {
	                    registrationDate = new FactDate(maxStart, registrationDate.getEndDate());
	                }
	            }
            }
            this.spousesFamily = spousesFamily;
        }

        public string getDateOfMarriage () {
            return (marriage == null) ? "" : marriage.getDatestring();
        }
        
        public string getPlaceOfMarriage () {
            return (marriage == null) ? "" : marriage.getLocation();
        }
        
        public string getSpousesName () {
            return (spousesFamily == null) ? "" : spousesFamily.getIndividual().getName();
        }

        public string getSpousesOccupation () {
            return (spousesFamily == null) ? "" : spousesFamily.getIndividual().getOccupation();
        }

        public string getSpousesAge () {
            return (marriage == null || spousesFamily == null) ? "" :
            	    spousesFamily.getIndividual().getAge(marriage.getFactDate());
        }

        public string getSpousesFathersName () {
            return (spousesFamily == null) ? "" : spousesFamily.getFathersName();
        }
        
        public string getSpousesFathersOccupation () {
            return (spousesFamily == null) ? "" : spousesFamily.getFathersOccupation();
        }
        
        public string getSpousesFatherDeceased () {
            return (marriage == null || spousesFamily == null) ? "" :
            	    spousesFamily.getFatherDeceased(registrationDate);
        }
        
        public string getSpousesMothersName () {
            return (spousesFamily == null) ? "" : spousesFamily.getFathersName();
        }
        
        public string getSpousesMothersOccupation () {
            return (spousesFamily == null) ? "" : spousesFamily.getFathersOccupation();
        }
        
        public string getSpousesMotherDeceased () {
            return (marriage == null || spousesFamily == null) ? "" :
        	    spousesFamily.getMotherDeceased(registrationDate);
        }
        
        public string getMaritalStatus () {
            return "";
        }
        
        public string getSpousesMaritalStatus () {
            return "";
        }
        
        public string getResidence () {
            return "";
        }
        
        public string getSpousesResidence () {
            return "";
        }
        
        public string getReligion () {
            return "";
        }
        /**
         * @return Returns the spousesFamily.
         */
        public ParentalGroup getSpousesFamilyGroup () {
            return spousesFamily;
        }
        
        public override string getRegistrationLocation () {
            return getPlaceOfMarriage();
        }
        
        public override bool isCertificatePresent() {
            return (marriage == null) ? false : marriage.isCertificatePresent();
        }
    }
}