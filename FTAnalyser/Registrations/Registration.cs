using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public abstract class Registration {

        internal ParentalGroup individualsFamily;
        internal FactDate registrationDate;
        internal bool completed;
        
        public Registration (ParentalGroup individualsFamily) {
            this.individualsFamily = individualsFamily;
            this.registrationDate = null;
            this.completed = false;
        }
        
        /**
         * @return Returns the completed.
         */
        public bool isCompleted () {
            return completed;
        }
        
        /**
         * @param completed The completed to set.
         */
        public void setCompleted (bool completed) {
            this.completed = completed;
        }
        
        /**
         * @return Returns the individual.
         */
        public Individual getIndividual () {
            return individualsFamily.getIndividual();
        }
        /**
         * @return Returns the father.
         */
        public Individual getFather () {
            return individualsFamily.getFather();
        }

        /**
         * @return Returns the mother.
         */
        public Individual getMother () {
            return individualsFamily.getMother();
        }
        
        public String getName () {
            return getIndividual().getName();
        }
        
        public String getSurname() {
            return getIndividual().getSurname();
        }
        
        public String getForenames() {
            return getIndividual().getForenames();
        }
        
        public String getGender () {
            return getIndividual().getGender();
        }
        
        public String getOccupation () {
            return getIndividual().getOccupation();
        }
        
        public int getRelation() {
            return getIndividual().getRelation();
        }
        
        public String getDateOfBirth () {
            return getIndividual().getDateOfBirth();
        }
        
        public String getPlaceOfBirth () {
            return getIndividual().getBirthLocation();
        }
            
        public String getFathersName () {
            return individualsFamily.getFathersName();
        }

        public String getMothersName () {
            return individualsFamily.getMothersName();
        }

        public String getFathersOccupation () {
            return individualsFamily.getFathersOccupation();
        }

        public String getMothersOccupation () {
            return individualsFamily.getMothersOccupation();
        }
        
        public String getFatherDeceased () {
            return individualsFamily.getFatherDeceased(registrationDate); 
        }

        public String getMotherDeceased () {
            return individualsFamily.getMotherDeceased(registrationDate); 
        }
        
        public String getAge () {
            return getIndividual().getAge(registrationDate);
        }
        
        public String getParentsMarriageDate () {
            return individualsFamily.getParentsMarriageDate(); 
        }
        
        public String getParentsMarriageLocation () {
            return individualsFamily.getParentsMarriageLocation(); 
        }
       
        /**
         * @return Returns the individualsFamily.
         */
        public ParentalGroup getFamilyGroup () {
            return individualsFamily;
        }
        /**
         * @return Returns the registrationDate.
         */
        public FactDate getRegistrationDate () {
            return registrationDate;
        }
        
        public String getBestLocation () {
            return individualsFamily.getBestLocation().ToString();
        }
        
        public List<Fact> getAllFacts() {
            List<Fact> facts = new List<Fact>();
            if (individualsFamily != null) {
	            if (getIndividual() != null)
	                facts.AddRange(getIndividual().getAllFacts());
    	        
	            if (getFather() != null)
	                facts.AddRange(getFather().getAllFacts());
    	        
	            if (getMother() != null)
	                facts.AddRange(getMother().getAllFacts());
            }
	        return facts;
        }
        
        public abstract String getRegistrationLocation ();
     
        /**
         * @return Returns the certificatePresent.
         */
        public abstract bool isCertificatePresent();
    }
}