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

        public String getDateOfMarriage () {
            return (marriage == null) ? "" : marriage.getDateString();
        }
        
        public String getPlaceOfMarriage () {
            return (marriage == null) ? "" : marriage.getLocation();
        }
        
        public String getSpousesName () {
            return (spousesFamily == null) ? "" : spousesFamily.getIndividual().getName();
        }

        public String getSpousesOccupation () {
            return (spousesFamily == null) ? "" : spousesFamily.getIndividual().getOccupation();
        }

        public String getSpousesAge () {
            return (marriage == null || spousesFamily == null) ? "" :
            	    spousesFamily.getIndividual().getAge(marriage.getFactDate());
        }

        public String getSpousesFathersName () {
            return (spousesFamily == null) ? "" : spousesFamily.getFathersName();
        }
        
        public String getSpousesFathersOccupation () {
            return (spousesFamily == null) ? "" : spousesFamily.getFathersOccupation();
        }
        
        public String getSpousesFatherDeceased () {
            return (marriage == null || spousesFamily == null) ? "" :
            	    spousesFamily.getFatherDeceased(registrationDate);
        }
        
        public String getSpousesMothersName () {
            return (spousesFamily == null) ? "" : spousesFamily.getFathersName();
        }
        
        public String getSpousesMothersOccupation () {
            return (spousesFamily == null) ? "" : spousesFamily.getFathersOccupation();
        }
        
        public String getSpousesMotherDeceased () {
            return (marriage == null || spousesFamily == null) ? "" :
        	    spousesFamily.getMotherDeceased(registrationDate);
        }
        
        public String getMaritalStatus () {
            return "";
        }
        
        public String getSpousesMaritalStatus () {
            return "";
        }
        
        public String getResidence () {
            return "";
        }
        
        public String getSpousesResidence () {
            return "";
        }
        
        public String getReligion () {
            return "";
        }
        /**
         * @return Returns the spousesFamily.
         */
        public ParentalGroup getSpousesFamilyGroup () {
            return spousesFamily;
        }
        
        public override String getRegistrationLocation () {
            return getPlaceOfMarriage();
        }
        
        public override bool isCertificatePresent() {
            return (marriage == null) ? false : marriage.isCertificatePresent();
        }
    }
}