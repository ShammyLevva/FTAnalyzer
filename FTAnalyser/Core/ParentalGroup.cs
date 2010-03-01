using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class ParentalGroup {
    	
        private Individual individual;
        private Individual father, mother;
        private Fact parentsMarriage;
        
        public ParentalGroup (Individual i, Individual f, Individual m, Fact marriage) {
            this.individual = i;
            this.father = f;
            this.mother = m;
            this.parentsMarriage = marriage;
        }

        /**
         * @return Returns the father.
         */
        public Individual getFather () {
            return father;
        }
        /**
         * @return Returns the individual.
         */
        public Individual getIndividual () {
            return individual;
        }
        /**
         * @return Returns the mother.
         */
        public Individual getMother () {
            return mother;
        }
        /**
         * @return Returns the parentsMarriage.
         */
        public Fact getParentsMarriage () {
            return parentsMarriage;
        }

        public string getResidence() {
            Fact residence = individual.getPreferredFact(Fact.RESIDENCE);
            return (residence == null) ? "" : residence.getLocation();
        }
        
        public Fact getPreferredFact(string factType) {
            return individual.getPreferredFact(factType);
        }
        
        public FactDate getPreferredFactDate (string factType) {
            return individual.getPreferredFactDate(factType);
        }
        
        public string getFathersName () {
            return (father == null) ? ""  : father.getName();
        }

        public string getMothersName () {
            return (mother == null) ? ""  : mother.getName();
        }

        public string getFathersOccupation () {
            return (father == null) ? ""  : father.getOccupation();
        }

        public string getMothersOccupation () {
            return (mother == null) ? ""  : mother.getOccupation();
        }
        
        public string getFatherDeceased (FactDate when) {
            return (father == null || ! father.isDeceased(when)) ? "" : "(Deceased)"; 
        }

        public string getMotherDeceased (FactDate when) {
            return (mother == null || ! mother.isDeceased(when)) ? "" : "(Deceased)"; 
        }
        
        public string getParentsMarriageDate () {
            return (parentsMarriage == null) ? "" :
            	    parentsMarriage.getFactDate().getDatestring();
        }
        
        public string getParentsMarriageLocation () {
            return (parentsMarriage == null) ? "" :
        	    parentsMarriage.getLocation();
        }
        
        public Location getBestLocation() {
            Location i = individual.getBestLocation();
            if (parentsMarriage == null)
                return i;
            Location f = new Location(parentsMarriage.getLocation());
            if (f.getLevel() > i.getLevel())
                return f;
            else
                return i;
        }
    }
}