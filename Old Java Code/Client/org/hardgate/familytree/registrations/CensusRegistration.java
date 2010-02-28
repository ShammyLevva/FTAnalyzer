/*
 * Created on 01-Jan-2005
 *
 */
package org.hardgate.familytree.registrations;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.List;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.CensusFamily;
import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.core.ParentalGroup;

/**
 * @author amb
 *
 */
public class CensusRegistration extends Registration implements Serializable {

	private static Logger logger = Logger.getLogger(CensusRegistration.class);
	private static final long serialVersionUID = 0;

	private CensusFamily censusFamily;
	
    public CensusRegistration (ParentalGroup familyGroup, FactDate censusDate, 
            CensusFamily censusFamily) {
        super(familyGroup);
        this.registrationDate = censusDate;
        this.censusFamily = censusFamily;        
    }

    public String getRegistrationLocation () {
        return censusFamily.getBestLocation().toString();
    }
    
    public boolean isCertificatePresent() {
        return false;
    }
    
    public List<Fact> getAllFacts() {
        List<Fact> facts = new ArrayList<Fact>();
        for (Individual i : getMembers()) {
            facts.addAll(i.getAllFacts());
        }
        return facts;
    }
    
    public List<Individual> getMembers() {
        return censusFamily.getMembers();
    }
    
    public String getFamilyGed() {
        return censusFamily.getFamilyGed();
    }
    
    public int getRelation() {
        return censusFamily.getRelation();
    }
}
