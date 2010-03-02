using System;
using System.Collections.Generic;
using System.Xml;

namespace FTAnalyser
{
    public class Individual : IComparable<Individual> {
    	
	    // define relation type from direct ancestor to related by marriage and 
	    // MARRIAGEDB ie: married to a direct or blood relation
	    public static readonly int UNKNOWN = 0, DIRECT = 1, BLOOD = 2, 
						        MARRIAGEDB = 3, MARRIAGE = 4, UNSET = 99;
	    public static readonly string HUSBAND = "Husband", WIFE = "Wife", CHILD = "Child",
							    UNKNOWNSTATUS = "unknown";
    	
	    private string individualID;
	    private string gedcomID;
	    private string forenames;
	    private string surname;
	    private string marriedName;
	    private string gender;
	    private string alias;
	    private string status;
	    private int relation;
	    private List<Fact> facts;
    	
	    public Individual (XmlNode node) {
		    gedcomID = node.Attributes["ID"].Value;
            individualID = gedcomID;
            string name = FamilyTree.GetText(node, "NAME");
            int startPos = name.IndexOf("/"), endPos = name.LastIndexOf("/");
            if (startPos >= 0 && endPos > startPos) {
                surname = name.Substring(startPos + 1, endPos - startPos - 1);
		        forenames = startPos == 0 ? "UNKNOWN" : name.Substring(0, startPos - 1);
            } else {
                surname = "UNKNOWN";
		        forenames = name;
            }
		    marriedName = surname;
            gender = FamilyTree.GetText(node, "SEX");
		    if (gender.Length == 0)
		        gender = "U";
            alias = FamilyTree.GetText(node, "ALIA");
		    relation = UNKNOWN;
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

        private void addFacts(XmlNode node, string factType) {
            XmlNodeList list = node.SelectNodes(factType);
	        foreach(XmlNode n in list) {
	            facts.Add(new Fact(n));
	        }
	    }

	    public void addFact(Fact fact) {
	        facts.Add(fact);
	    }
    	
	    /**
	     * @return Returns the GedcomID.
	     */
	    public string getGedcomID() {
		    return gedcomID;
	    }
	    /**
	     * @return Returns the name.
	     */
	    public string getName() {
		    return (forenames + " " + surname).Trim();
	    }

	    /**
	     * @return Returns the surname.
	     */
	    public string getSurname() {
		    return surname;
	    }

	    /**
	     * @return Returns the surname on marriage
	     */
	    public string getMarriedName() {
		    return marriedName;
	    }

	    public string getCensusName() {
	        return this.status.Equals(WIFE) ? 
	             forenames + " " + marriedName + " (" + surname + ")" :
	             getName();
	    }
    	
	    /**
	     * @return Returns the alias.
	     */
	    public string getAlias() {
		    return this.alias;
	    }
	    /**
	     * @return Returns the gender.
	     */
	    public string getGender() {
		    return this.gender;
	    }
	    /**
	     * @return Returns true if individual is male
	     */	
	    public bool isMale() {
		    return this.gender.Equals("M");
	    }
	    /**
	     * @return Returns all of the facts.
	     */
	    public List<Fact> getAllFacts () {
	        return this.facts;
	    }
    	
	    /**
	     * @return Returns the first fact of the given type.
	     */
	    public Fact getPreferredFact(string factType) {
	        foreach(Fact f in facts)
            {
		        if (f.getFactType().Equals(factType))
	    	        return f;
	        }
	        return null;
	    }
    	
	    public FactDate getPreferredFactDate (string factType) {
	        Fact f = getPreferredFact(factType);
	        return (f == null) ? FactDate.UNKNOWN_DATE : f.getFactDate();
	    }
    	
	    public FactDate getBirthDate() {
	        return getPreferredFactDate(Fact.BIRTH);
	    }
    	
	    public string getBirthLocation() {
	        Fact f = getPreferredFact(Fact.BIRTH);
	        return (f == null) ? "" : f.getLocation();
	    }

	    public string getDateOfBirth() {
	        Fact f = getPreferredFact(Fact.BIRTH);
	        return (f == null) ? "" : f.getDatestring();
	    }

	    public FactDate getDeathDate() {
	        Fact f = getPreferredFact(Fact.DEATH);
	        return (f == null) ? null : f.getFactDate();
	    }
    	
	    /**
	     * @return Returns all facts of the given type.
	     */
	    public List<Fact> getFacts(string factType) {
	        List<Fact> result = new List<Fact>();
	        foreach(Fact f in facts)
            {
		        if (f.getFactType().Equals(factType))
	    	        result.Add(f);
	        }
	        return result;
	    }
    	
	    public bool isCensusDone(FactDate when) {
	        foreach(Fact f in facts) 
            {
		        if (f.getFactType().Equals(Fact.CENSUS) &&
	    	        f.getFactDate().overlaps(when)) 
	    	            return true;   
	        }
	        return false;	    
	    }
    	
	    public Location getLocation(FactDate when) {
	        // ideally this returns a Location a person was at for a given period
	        return new Location();
	    }
    	
	    public Location getBestLocation() {
	        int bestLevel = -1;
	        Location result = new Location();
	        foreach(Fact f in facts) {
	            Location l = new Location(f.getLocation());
	    	    if (l.getLevel() > bestLevel) {
	    	        result = l;
	    	        bestLevel = l.getLevel();
	    	    }
	        }
	        return result;
	    }
    	
        /**
         * @return Returns the individualID.
         */
        public string getIndividualID() {
            return individualID;
        }
        
        public void setMarriedName(string marriedName) {
            this.marriedName = marriedName;
        }
        
        /**
         * @return Returns the forenames.
         */
        public string getForenames() {
            return forenames;
        }

        /**
         * @return Returns the relation.
         */
        public int getRelation() {
            return relation;
        }

        public string getForename() {
            if (forenames == null)
                return "";
            else {
                int pos = forenames.IndexOf(' ');
                return pos > 0 ? forenames.Substring(0, pos) : forenames;
            }
        }
        
        public string getOccupation () {
            Fact occupation = getPreferredFact(Fact.OCCUPATION);
            return occupation == null ? "" : occupation.getComment();
        }
        
        public bool isDeceased (FactDate when) {
            Fact death = getPreferredFact(Fact.DEATH);
            return death != null && death.getFactDate().isBefore(when);
        }
        
        public bool isSingleAtDeath() {
            Fact single = getPreferredFact(Fact.UNMARRIED);
            return single != null || getMaxAgeAtDeath() < 16 || getCurrentAge() < 16;
        }
        
        private int getMaxAgeAtDeath() {
            Fact death = getPreferredFact(Fact.DEATH);
            return (death == null) ? FactDate.MAXYEARS : 
                getBirthDate().getMaximumYear(death.getFactDate());
        }
        
        public string getAge(FactDate when) {
            int minValue = getBirthDate().getMinimumYear(when);
            int maxValue = getBirthDate().getMaximumYear(when);
            if (minValue == FactDate.MINYEARS) {
                if (maxValue == FactDate.MAXYEARS)
                    return "Unknown";
                else
                    return "<=" + maxValue;
            } else {
                if (maxValue == FactDate.MAXYEARS) {
                    if (minValue >= FactDate.MAXYEARS) {
                        // if age over maximum return maximum
                        return FactDate.MAXYEARS.ToString();
                    }
                    return ">=" + minValue;
                } else {
                    return minValue == maxValue ? minValue.ToString() :
                    	    minValue + " to " + maxValue;
                }
            }
        }
        
        public string getAge(DateTime when) {
            string now = FactDate.Format(FactDate.FULL, when);
            return getAge(new FactDate(now));
        }
        
        public int getCurrentAge() {
            string age = getAge(DateTime.Now);
            return Int32.Parse(age);
        }
        
        public int getMaxAge(FactDate when) {
            return getBirthDate().getMaximumYear(when);
        }
        
        public int getMinAge(FactDate when) {
            return getBirthDate().getMinimumYear(when);
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

        public int CompareTo (Individual that) {
            // Individuals are naturally ordered by surname, then forenames,
            // then date of birth.
            int res = this.surname.CompareTo(that.surname);
            if (res == 0) {
                res = this.forenames.CompareTo(that.forenames);
                if (res == 0) {
                    Fact b1 = this.getPreferredFact(Fact.BIRTH);
                    Fact b2 = that.getPreferredFact(Fact.BIRTH);
                    FactDate d1 = (b1 == null) ? FactDate.UNKNOWN_DATE : b1.getFactDate();
                    FactDate d2 = (b2 == null) ? FactDate.UNKNOWN_DATE : b2.getFactDate();
                    res = d1.CompareTo(d2);
                }
            }
            return res;
        }

        public string getStatus() {
            return status;
        }
        
        public void setStatus(string status) {
            this.status = status;
        }
    }
}