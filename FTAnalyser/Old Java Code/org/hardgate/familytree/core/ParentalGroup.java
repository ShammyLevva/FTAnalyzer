/*
 * Created on 02-Jan-2005
 *
 */
package org.hardgate.familytree.core;

import java.io.Serializable;

import org.apache.log4j.Logger;

/**
 * @author db
 *
 */
public class ParentalGroup implements Serializable {

	private static Logger logger = Logger.getLogger(ParentalGroup.class);
	private static final long serialVersionUID = 0;
	
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

    public String getResidence() {
        Fact residence = individual.getPreferredFact(Fact.RESIDENCE);
        return (residence == null) ? "" : residence.getLocation();
    }
    
    public Fact getPreferredFact(String factType) {
        return individual.getPreferredFact(factType);
    }
    
    public FactDate getPreferredFactDate (String factType) {
        return individual.getPreferredFactDate(factType);
    }
    
    public String getFathersName () {
        return (father == null) ? ""  : father.getName();
    }

    public String getMothersName () {
        return (mother == null) ? ""  : mother.getName();
    }

    public String getFathersOccupation () {
        return (father == null) ? ""  : father.getOccupation();
    }

    public String getMothersOccupation () {
        return (mother == null) ? ""  : mother.getOccupation();
    }
    
    public String getFatherDeceased (FactDate when) {
        return (father == null || ! father.isDeceased(when)) ? "" : "(Deceased)"; 
    }

    public String getMotherDeceased (FactDate when) {
        return (mother == null || ! mother.isDeceased(when)) ? "" : "(Deceased)"; 
    }
    
    public String getParentsMarriageDate () {
        return (parentsMarriage == null) ? "" :
            	parentsMarriage.getFactDate().getDateString();
    }
    
    public String getParentsMarriageLocation () {
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
