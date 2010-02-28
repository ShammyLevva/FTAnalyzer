/*
 * Created on 01-Oct-2004
 *
 */
package org.hardgate.familytree.core;

import java.io.Serializable;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Iterator;
import java.util.List;

import org.apache.log4j.Logger;
import org.dom4j.Element;
import org.hardgate.familytree.interfaces.FactLocal;
import org.hardgate.familytree.interfaces.IndividualLocal;

/**
 * @author A-Bisset
 *
 */
public class Individual implements Serializable, Comparable<Individual> {
	
	private static Logger logger = Logger.getLogger(Individual.class);
	private static final long serialVersionUID = 0;
	// define relation type from direct ancestor to related by marriage and 
	// MARRIAGEDB ie: married to a direct or blood relation
	public static final int UNKNOWN = 0, DIRECT = 1, BLOOD = 2, 
						    MARRIAGEDB = 3, MARRIAGE = 4, UNSET = 99;
	public static final String HUSBAND = "Husband", WIFE = "Wife", CHILD = "Child",
							UNKNOWNSTATUS = "unknown";
	
	private int memberID;
	private String individualID;
	private String gedcomID;
	private String forenames;
	private String surname;
	private String marriedName;
	private String gender;
	private String alias;
	private String status;
	private int relation;
	private ArrayList<Fact> facts;
	
	public Individual (int memberID, Element element) {
		this.memberID = memberID;
		gedcomID = element.attributeValue("ID");
		String name = element.elementText("NAME").trim();
		try {
			surname = name.substring(name.indexOf("/")+1,
			        				 name.lastIndexOf("/"));
			forenames = name.substring(0,name.indexOf("/")-1);
		} catch (IndexOutOfBoundsException e) {
			surname = "UNKNOWN";
			forenames = name;
		}
		marriedName = surname;
		name = forenames + " " + surname;
		gender = element.elementText("SEX");
		if (gender == null)
		    gender = "U";
		alias = element.elementText("ALIA");
		relation = UNKNOWN;
		status = UNKNOWNSTATUS;
		facts = new ArrayList<Fact>(0);
		addFacts(element,Fact.BIRTH);
		addFacts(element,Fact.CHRISTENING);
		addFacts(element,Fact.DEATH);
		addFacts(element,Fact.BURIAL);
		addFacts(element,Fact.CENSUS);
		addFacts(element,Fact.RESIDENCE);
		addFacts(element,Fact.OCCUPATION);
		addFacts(element,Fact.CUSTOM_FACT);
	}

	public Individual (IndividualLocal ind) {
	    if (ind != null) {
			this.individualID = ind.getIndividualID();
			this.memberID = ind.getMemberID().intValue();
			this.gedcomID = ind.getGedcomID();
			this.forenames = ind.getForenames();
			this.surname = ind.getSurname();
			this.gender = ind.getGender();
			this.alias = ind.getAlias();
			this.relation = ind.getRelation().intValue();
			this.status = UNKNOWNSTATUS;
			this.facts = new ArrayList<Fact>();
			Iterator it = ind.getFacts().iterator();
			while (it.hasNext()) {
			    this.facts.add(new Fact((FactLocal) it.next()));
			}
	    } else {
	    	this.memberID = 0;
	    	this.individualID = "";
	    	this.gedcomID = "";
	    	this.forenames = "";
	    	this.surname = "";
	    	this.gender = "";
	    	this.alias = "";
	    	this.relation = UNKNOWN;
	    	this.status = UNKNOWNSTATUS;
	        this.facts = new ArrayList<Fact>(0); 
	    }
		this.marriedName = surname;
	}
	
	private void addFacts(Element element,String factType) {
	    Iterator<Element> it = element.elementIterator(factType);
	    while(it.hasNext()) {
	        Element e = it.next();
	        facts.add(new Fact(this.memberID, e));
	    }
	}

	public void addFact(Fact fact) {
	    facts.add(fact);
	}
	
	/**
	 * @return Returns the GedcomID.
	 */
	public String getGedcomID() {
		return gedcomID;
	}
	/**
	 * @return Returns the name.
	 */
	public String getName() {
		return (forenames + " " + surname).trim();
	}

	/**
	 * @return Returns the surname.
	 */
	public String getSurname() {
		return surname;
	}

	/**
	 * @return Returns the surname on marriage
	 */
	public String getMarriedName() {
		return marriedName;
	}

	public String getCensusName() {
	    return this.status.equals(WIFE) ? 
	         forenames + " " + marriedName + " (" + surname + ")" :
	         getName();
	}
	
	/**
	 * @return Returns the alias.
	 */
	public String getAlias() {
		return this.alias;
	}
	/**
	 * @return Returns the gender.
	 */
	public String getGender() {
		return this.gender;
	}
	/**
	 * @return Returns true if individual is male
	 */	
	public boolean isMale() {
		return this.gender.equals("M");
	}
	/**
	 * @return Returns the memberID.
	 */
	public int getMemberID() {
		return this.memberID;
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
	public Fact getPreferredFact(String factType) {
	    for (Fact f : facts) {
		    if (f.getFactType().equals(factType))
	    	    return f;
	    }
	    return null;
	}
	
	public FactDate getPreferredFactDate (String factType) {
	    Fact f = getPreferredFact(factType);
	    return (f == null) ? FactDate.UNKNOWN_DATE : f.getFactDate();
	}
	
	public FactDate getBirthDate() {
	    return getPreferredFactDate(Fact.BIRTH);
	}
	
	public String getBirthLocation() {
	    Fact f = getPreferredFact(Fact.BIRTH);
	    return (f == null) ? "" : f.getLocation();
	}

	public String getDateOfBirth() {
	    Fact f = getPreferredFact(Fact.BIRTH);
	    return (f == null) ? "" : f.getDateString();
	}

	public FactDate getDeathDate() {
	    Fact f = getPreferredFact(Fact.DEATH);
	    return (f == null) ? null : f.getFactDate();
	}
	
	/**
	 * @return Returns all facts of the given type.
	 */
	public List<Fact> getFacts(String factType) {
	    List<Fact> result = new ArrayList<Fact>();
	    for (Fact f : facts) {
		    if (f.getFactType().equals(factType))
	    	    result.add(f);
	    }
	    return result;
	}
	
	public boolean isCensusDone(FactDate when) {
	    for (Fact f : facts) {
		    if (f.getFactType().equals(Fact.CENSUS) &&
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
	    for (Fact f : facts) {
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
    public String getIndividualID() {
        return individualID;
    }
    
    public void setMarriedName(String marriedName) {
        this.marriedName = marriedName;
    }
    
    /**
     * @return Returns the forenames.
     */
    public String getForenames() {
        return forenames;
    }

    /**
     * @return Returns the relation.
     */
    public int getRelation() {
        return relation;
    }

    public String getForename() {
        if (forenames == null)
            return "";
        else {
            int pos = forenames.indexOf(' ');
            return pos > 0 ? forenames.substring(0, pos) : forenames;
        }
    }
    
    public String getOccupation () {
        Fact occupation = getPreferredFact(Fact.OCCUPATION);
        return occupation == null ? "" : occupation.getComment();
    }
    
    public boolean isDeceased (FactDate when) {
        Fact death = getPreferredFact(Fact.DEATH);
        return death != null && death.getFactDate().isBefore(when);
    }
    
    public boolean isSingleAtDeath() {
        Fact single = getPreferredFact(Fact.UNMARRIED);
        return single != null || getMaxAgeAtDeath() < 16 || getCurrentAge() < 16;
    }
    
    private int getMaxAgeAtDeath() {
        Fact death = getPreferredFact(Fact.DEATH);
        return (death == null) ? FactDate.MAXYEARS : 
            getBirthDate().getMaximumYear(death.getFactDate());
    }
    
    public String getAge(FactDate when) {
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
                    return Integer.toString(FactDate.MAXYEARS);
                }
                return ">=" + minValue;
            } else {
                return minValue == maxValue ? Integer.toString(minValue) :
                    	minValue + " to " + maxValue;
            }
        }
    }
    
    public String getAge(Calendar when) {
        String now = new SimpleDateFormat("dd MMM yyyy").format(when.getTime());
        return getAge(new FactDate(now));
    }
    
    public int getCurrentAge() {
        String age = getAge(Calendar.getInstance());
        try {
            return Integer.parseInt(age);
        } catch (NumberFormatException e) {
            return -1;
        }
    }
    
    public int getMaxAge(FactDate when) {
        return getBirthDate().getMaximumYear(when);
    }
    
    public int getMinAge(FactDate when) {
        return getBirthDate().getMinimumYear(when);
    }
    
    public int getMaxAge(Calendar when) {
        String now = new SimpleDateFormat("dd MMM yyyy").format(when.getTime());
        return getMaxAge(new FactDate(now));
    }

    public int getMinAge(Calendar when) {
        String now = new SimpleDateFormat("dd MMM yyyy").format(when.getTime());
        return getMinAge(new FactDate(now));
    }

    public int compareTo (Individual that) {
        // Individuals are naturally ordered by surname, then forenames,
        // then date of birth.
        int res = this.surname.compareTo(that.surname);
        if (res == 0) {
            res = this.forenames.compareTo(that.forenames);
            if (res == 0) {
                Fact b1 = this.getPreferredFact(Fact.BIRTH);
                Fact b2 = that.getPreferredFact(Fact.BIRTH);
                FactDate d1 = (b1 == null) ? FactDate.UNKNOWN_DATE : b1.getFactDate();
                FactDate d2 = (b2 == null) ? FactDate.UNKNOWN_DATE : b2.getFactDate();
                res = d1.compareTo(d2);
            }
        }
        return res;
    }

    public String getStatus() {
        return status;
    }
    
    public void setStatus(String status) {
        this.status = status;
    }
}
