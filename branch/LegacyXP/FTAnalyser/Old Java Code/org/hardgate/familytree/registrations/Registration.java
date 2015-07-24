/*
 * Created on 01-Jan-2005
 *
 */
package org.hardgate.familytree.registrations;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.core.ParentalGroup;

/**
 * @author db
 *
 */
public abstract class Registration implements Serializable {

	private static Logger logger = Logger.getLogger(Registration.class);
	private static final long serialVersionUID = 0;

    protected ParentalGroup individualsFamily;
    protected FactDate registrationDate;
    protected boolean completed;
    
    public Registration (ParentalGroup individualsFamily) {
        this.individualsFamily = individualsFamily;
        this.registrationDate = null;
        this.completed = false;
    }
    
    /**
     * @return Returns the completed.
     */
    public boolean isCompleted () {
        return completed;
    }
    
    /**
     * @param completed The completed to set.
     */
    public void setCompleted (boolean completed) {
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
        return individualsFamily.getBestLocation().toString();
    }
    
    public List<Fact> getAllFacts() {
        List<Fact> facts = new ArrayList<Fact>();
        if (individualsFamily != null) {
	        if (getIndividual() != null)
	            facts.addAll(getIndividual().getAllFacts());
	        
	        if (getFather() != null)
	            facts.addAll(getFather().getAllFacts());
	        
	        if (getMother() != null)
	            facts.addAll(getMother().getAllFacts());
        }
	    return facts;
    }
    
    public abstract String getRegistrationLocation ();
 
    /**
     * @return Returns the certificatePresent.
     */
    public abstract boolean isCertificatePresent();
}
