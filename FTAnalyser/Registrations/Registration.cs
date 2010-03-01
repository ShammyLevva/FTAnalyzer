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
        
        public string getName () {
            return getIndividual().getName();
        }
        
        public string getSurname() {
            return getIndividual().getSurname();
        }
        
        public string getForenames() {
            return getIndividual().getForenames();
        }
        
        public string getGender () {
            return getIndividual().getGender();
        }
        
        public string getOccupation () {
            return getIndividual().getOccupation();
        }
        
        public virtual int getRelation() {
            return getIndividual().getRelation();
        }
        
        public string getDateOfBirth () {
            return getIndividual().getDateOfBirth();
        }
        
        public string getPlaceOfBirth () {
            return getIndividual().getBirthLocation();
        }
            
        public string getFathersName () {
            return individualsFamily.getFathersName();
        }

        public string getMothersName () {
            return individualsFamily.getMothersName();
        }

        public string getFathersOccupation () {
            return individualsFamily.getFathersOccupation();
        }

        public string getMothersOccupation () {
            return individualsFamily.getMothersOccupation();
        }
        
        public string getFatherDeceased () {
            return individualsFamily.getFatherDeceased(registrationDate); 
        }

        public string getMotherDeceased () {
            return individualsFamily.getMotherDeceased(registrationDate); 
        }
        
        public string getAge () {
            return getIndividual().getAge(registrationDate);
        }
        
        public string getParentsMarriageDate () {
            return individualsFamily.getParentsMarriageDate(); 
        }
        
        public string getParentsMarriageLocation () {
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
        
        public string getBestLocation () {
            return individualsFamily.getBestLocation().ToString();
        }
        
        public virtual List<Fact> getAllFacts() {
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
        
        public abstract string getRegistrationLocation ();
     
        /**
         * @return Returns the certificatePresent.
         */
        public abstract bool isCertificatePresent();
    }
}