using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
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

        #region Getters/Setters

        public bool Completed  
        {
            set { this.completed = value; }
        }
        
        public Individual Individual {
            get { return individualsFamily.Individual; }
        }

        public Individual Father {
            get { return individualsFamily.Father; }
        }

        public Individual Mother  {
            get { return individualsFamily.Mother; }
        }
        
        public string Name {
            get { return Individual.Name; }
        }
        
        public string Surname {
            get { return Individual.Surname; }
        }
        
        public string Forenames {
            get { return Individual.Forenames; }
        }
        
        public string Gender {
            get { return Individual.Gender; }
        }
        
        public string Occupation {
            get { return Individual.Occupation; }
        }
        
        public virtual int Relation {
            get { return Individual.RelationType; }
        }
        
        public string DateOfBirth  {
            get { return Individual.DateOfBirth; }
        }
        
        public string PlaceOfBirth {
            get { return Individual.BirthLocation; }
        }
            
        public string FathersName  {
            get { return individualsFamily.FathersName; }
        }

        public string MothersName {
            get { return individualsFamily.MothersName; }
        }

        public string FathersOccupation {
            get { return individualsFamily.FathersOccupation; }
        }

        public string MothersOccupation {
            get { return individualsFamily.MothersOccupation; }
        }
        
        public string isFatherDeceased {
            get { return individualsFamily.isFatherDeceased(registrationDate); }
        }

        public string isMotherDeceased {
            get { return individualsFamily.isMotherDeceased(registrationDate); }
        }
        
        public string Age {
            get { return Individual.getAge(registrationDate).ToString(); }
        }
        
        public string ParentsMarriageDate {
            get { return individualsFamily.ParentsMarriageDate; }
        }
        
        public string ParentsMarriageLocation {
            get { return individualsFamily.ParentsMarriageLocation; }
        }
       
        public ParentalGroup FamilyGroup {
            get { return individualsFamily; }
        }
 
        public FactDate RegistrationDate {
            get { return registrationDate; }
        }
        
        public string BestLocation {
            get { return individualsFamily.BestLocation.ToString(); }
        }
        
        public virtual List<Fact> AllFacts {
            get
            {
                List<Fact> facts = new List<Fact>();
                if (individualsFamily != null)
                {
                    if (Individual != null)
                        facts.AddRange(Individual.AllFacts);

                    if (Father != null)
                        facts.AddRange(Father.AllFacts);

                    if (Mother != null)
                        facts.AddRange(Mother.AllFacts);
                }
                return facts;
            }
        }
        #endregion 

        public virtual string RegistrationLocation
        {
            get { return null; }
        }
     
        /**
         * @return Returns the certificatePresent.
         */
        public abstract bool isCertificatePresent();
    }
}