using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
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

        #region Properties 
        public Individual Father {
            get { return father; }
        }

        public Individual Individual {
            get { return individual; }
        }

        public Individual Mother {
            get { return mother; }
        }

        public Fact ParentsMarriage {
            get { return parentsMarriage; }
        }

        public string Residence {
            get
            {
                Fact residence = individual.getPreferredFact(Fact.RESIDENCE);
                return (residence == null) ? "" : residence.Place;
            }
        }
        
        public string FathersName {
            get { return (father == null) ? ""  : father.Name; }
        }

        public string MothersName {
            get { return (mother == null) ? ""  : mother.Name; }
        }

        public string FathersOccupation {
            get { return (father == null) ? ""  : father.Occupation; }
        }

        public string MothersOccupation {
            get { return (mother == null) ? ""  : mother.Occupation; }
        }
        
        public string ParentsMarriageDate {
            get { return (parentsMarriage == null) ? "" : parentsMarriage.FactDate.Datestring; }
        }
        
        public string ParentsMarriageLocation {
            get { return (parentsMarriage == null) ? "" : parentsMarriage.Place; }
        }
        
        public Location BestLocation {
            get
            {
                Location i = individual.BestLocation;
                if (parentsMarriage == null)
                    return i;
                Location f = new Location(parentsMarriage.Place);
                if (f.Level > i.Level)
                    return f;
                else
                    return i;
            }
        }

        #endregion

        public string isFatherDeceased(FactDate when)
        {
            return (father == null || !father.isDeceased(when)) ? "" : "(Deceased)";
        }

        public string isMotherDeceased(FactDate when)
        {
            return (mother == null || !mother.isDeceased(when)) ? "" : "(Deceased)";
        }

        public Fact getPreferredFact(string factType)
        {
            return individual.getPreferredFact(factType);
        }

        public FactDate getPreferredFactDate(string factType)
        {
            return individual.getPreferredFactDate(factType);
        }

    }
}