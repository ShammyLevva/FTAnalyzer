using System;
using System.Collections.Generic;
using System.Xml;

namespace FTAnalyser
{
    public class Individual : IComparable<Individual>, IDisplayIndividual {
    	
	    // define relation type from direct ancestor to related by marriage and 
	    // MARRIAGEDB ie: married to a direct or blood relation
	    public static readonly int UNKNOWN = 0, DIRECT = 1, BLOOD = 2, 
						           MARRIAGEDB = 3, MARRIAGE = 4, UNSET = 99;
	    public static readonly string HUSBAND = "Husband", WIFE = "Wife", CHILD = "Child",
							          UNKNOWNSTATUS = "Unknown";
    	
	    private string individualID;
	    private string gedcomID;
	    private string forenames;
	    private string surname;
	    private string marriedName;
	    private string gender;
	    private string alias;
	    private string status;
	    private int relationType;
	    private List<Fact> facts;
    	
	    public Individual (XmlNode node) {
		    gedcomID = node.Attributes["ID"].Value;
            individualID = gedcomID;
            Name = FamilyTree.GetText(node, "NAME");
		    Gender = FamilyTree.GetText(node, "SEX");
            alias = FamilyTree.GetText(node, "ALIA");
		    relationType = UNKNOWN;
		    status = UNKNOWNSTATUS;
		    facts = new List<Fact>();
		    addFacts(node,Fact.BIRTH);
		    addFacts(node,Fact.CHRISTENING);
		    addFacts(node,Fact.DEATH);
		    addFacts(node,Fact.BURIAL);
		    addFacts(node,Fact.CENSUS);
		    addFacts(node,Fact.RESIDENCE);
		    addFacts(node,Fact.OCCUPATION);
		    addFacts(node,Fact.CUSTOM_FACT);
        }

        #region Properties

        public string IndividualID 
        { 
            get { return individualID; } 
        }

        public int RelationType 
        { 
            get { return relationType; }
            set { relationType = value; }
        }
        
        public List<Fact> AllFacts 
        { 
            get { return this.facts; } 
        }

        public string Alias
        {
            get { return this.alias; }
        }

        public string Gender
        {
            get { return this.gender; }
            private set
            {
                gender = value;
                if (gender.Length == 0)
                    gender = "U";
            }
        }
        
        public string GedcomID
        {
            get { return gedcomID; }
        }

        public string Name
        {
            get { return (forenames + " " + surname).Trim(); }
            private set
            {
                string name = value;
                int startPos = name.IndexOf("/"), endPos = name.LastIndexOf("/");
                if (startPos >= 0 && endPos > startPos)
                {
                    surname = name.Substring(startPos + 1, endPos - startPos - 1);
                    forenames = startPos == 0 ? "UNKNOWN" : name.Substring(0, startPos - 1);
                }
                else
                {
                    surname = "UNKNOWN";
                    forenames = name;
                }
                if (surname == "?")
                    surname = "UNKNOWN";
                marriedName = surname;
            }
        }

        public string Forename
        {
            get
            {
                if (forenames == null)
                    return "";
                else
                {
                    int pos = forenames.IndexOf(' ');
                    return pos > 0 ? forenames.Substring(0, pos) : forenames;
                }
            }
        }

        public string Forenames 
        { 
            get { return forenames; } 
        }
        
        public string Surname
        {
            get { return surname; }
        }

        public string MarriedName
        {
            get { return this.marriedName; }
            set { this.marriedName = value; }
        }

        public string CensusName
        {
            get { return this.status == WIFE ? forenames + " " + marriedName + " (" + surname + ")" : Name; }
        }
        
        public string DateOfBirth
        {
            get
            {
                Fact f = getPreferredFact(Fact.BIRTH);
                return (f == null) ? "" : f.Datestring;
            }
        }

        public FactDate BirthDate
        {
            get { return getPreferredFactDate(Fact.BIRTH); }
        }

        public string BirthLocation
        {
            get
            {
                Fact f = getPreferredFact(Fact.BIRTH);
                return (f == null) ? "" : f.Place;
            }
        }

        public FactDate DeathDate
        {
            get
            {
                Fact f = getPreferredFact(Fact.DEATH);
                return (f == null) ? null : f.FactDate;
            }
        }

        public string DeathLocation
        {
            get
            {
                Fact f = getPreferredFact(Fact.DEATH);
                return (f == null) ? "" : f.Place;
            }
        }

        public string Occupation
        {
            get
            {
                Fact occupation = getPreferredFact(Fact.OCCUPATION);
                return occupation == null ? "" : occupation.Comment;
            }
        }

        public string Status
        {
            get { return status; }
            set { this.status = value; }
        }

        public Location BestLocation
        {
            get
            {
                int bestLevel = -1;
                Location result = new Location();
                foreach (Fact f in facts)
                {
                    Location l = new Location(f.Place);
                    if (l.Level > bestLevel)
                    {
                        result = l;
                        bestLevel = l.Level;
                    }
                }
                return result;
            }
        }

        private int MaxAgeAtDeath
        {
            get
            {
                Fact death = getPreferredFact(Fact.DEATH);
                return (death == null) ? FactDate.MAXYEARS :
                    BirthDate.getMaximumYear(death.FactDate);
            }
        }

        public Age CurrentAge
        {
            get
            {
                return getAge(DateTime.Now);
            }
        }

        #endregion

        #region Boolean Tests

        public bool isMale()
        {
            return this.gender.Equals("M");
        }

        public bool isCensusDone(FactDate when)
        {
            foreach (Fact f in facts)
            {
                if (f.FactType == Fact.CENSUS && f.FactDate.overlaps(when))
                    return true;
            }
            return false;
        }

        public bool isDeceased(FactDate when)
        {
            Fact death = getPreferredFact(Fact.DEATH);
            return death != null && death.FactDate.isBefore(when);
        }
        
        public bool isSingleAtDeath() {
            Fact single = getPreferredFact(Fact.UNMARRIED);
            return single != null || MaxAgeAtDeath < 16 || CurrentAge.MaxAge < 16;
        }

        #endregion

        #region Age Functions

        public Age getAge(FactDate when) {
            return new Age(this, when);
        }
        
        public Age getAge(DateTime when) {
            string now = FactDate.Format(FactDate.FULL, when);
            return getAge(new FactDate(now));
        }
        
        public int getMaxAge(FactDate when) {
            return BirthDate.getMaximumYear(when);
        }
        
        public int getMinAge(FactDate when) {
            return BirthDate.getMinimumYear(when);
        }

        public int getMaxAge(DateTime when)
        {
            string now = FactDate.Format(FactDate.FULL, when);
            return getMaxAge(new FactDate(now));
        }

        public int getMinAge(DateTime when)
        {
            string now = FactDate.Format(FactDate.FULL, when);
            return getMinAge(new FactDate(now));
        }
        #endregion

        #region Fact Functions

        private void addFacts(XmlNode node, string factType) {
            XmlNodeList list = node.SelectNodes(factType);
	        foreach(XmlNode n in list) {
	            facts.Add(new Fact(n));
	        }
	    }

	    public void addFact(Fact fact) {
	        facts.Add(fact);
	    }
    	    	
	    public Fact getPreferredFact(string factType) {
            // Returns the first fact of the given type.
            // TODO: Should be fact marked as preferred
	        foreach(Fact f in facts)
            {
		        if (f.FactType == factType)
	    	        return f;
	        }
	        return null;
	    }
    	
	    public FactDate getPreferredFactDate (string factType) {
	        Fact f = getPreferredFact(factType);
	        return (f == null) ? FactDate.UNKNOWN_DATE : f.FactDate;
	    }
    	
	    public List<Fact> getFacts(string factType) {
            // Returns all facts of the given type.
	        List<Fact> result = new List<Fact>();
	        foreach(Fact f in facts)
            {
		        if (f.FactType == factType)
	    	        result.Add(f);
	        }
	        return result;
        }

        #endregion

        public Location getLocation(FactDate when)
        {
            // TODO: ideally this returns a Location a person was at for a given period
            return new Location();
        }

        public int CompareTo(Individual that)
        {
            // Individuals are naturally ordered by surname, then forenames,
            // then date of birth.
            int res = this.surname.CompareTo(that.surname);
            if (res == 0) {
                res = this.forenames.CompareTo(that.forenames);
                if (res == 0) {
                    Fact b1 = this.getPreferredFact(Fact.BIRTH);
                    Fact b2 = that.getPreferredFact(Fact.BIRTH);
                    FactDate d1 = (b1 == null) ? FactDate.UNKNOWN_DATE : b1.FactDate;
                    FactDate d2 = (b2 == null) ? FactDate.UNKNOWN_DATE : b2.FactDate;
                    res = d1.CompareTo(d2);
                }
            }
            return res;
        }
    }
}