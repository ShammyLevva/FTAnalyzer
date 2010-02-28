/*
 * Created on 01-Jan-2005
 *
 */
package org.hardgate.familytree.registrations;

import java.io.Serializable;
import java.util.Calendar;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.core.ParentalGroup;

/**
 * @author db
 *
 */
public class DeathRegistration extends Registration implements Serializable {

	private static Logger logger = Logger.getLogger(DeathRegistration.class);
	private static final long serialVersionUID = 0;

	private Fact death;
	private Individual spouse;
	private String maritalStatus;
    
    public DeathRegistration (ParentalGroup familyGroup, 
            Individual spouse, String maritalStatus) {
        super(familyGroup);
        death = familyGroup.getPreferredFact(Fact.DEATH);
        if (death == null) {
            registrationDate = null;
        } else {
            registrationDate = death.getFactDate();
            if (registrationDate != null) {
                FactDate birthDate = familyGroup.getIndividual().getBirthDate();
                Calendar maxStart = birthDate.getStartDate();
                if (maxStart.after(registrationDate.getStartDate())) {
                    registrationDate = new FactDate(maxStart,
                            registrationDate.getEndDate());
                }
            }
        }
        this.spouse = spouse;
        this.maritalStatus = maritalStatus;
    }

    public String getDateOfDeath () {
        return (death == null) ? "" : death.getDateString();
    }
    
    public String getPlaceOfDeath () {
        return (death == null) ? "" : death.getLocation();
    }
    
    public String getSpousesName () {
        return (spouse == null) ? "" : spouse.getName();
    }

    public String getSpousesOccupation () {
        return (spouse == null) ? "" : spouse.getOccupation();
    }

    public String getSpouseDeceased () {
        return (death == null) ? "" : getSpouseDeceased(death.getFactDate());
    }
    
    public String getSpouseDeceased (FactDate when) {
        return (spouse == null || ! spouse.isDeceased(when)) ? "" : "(Deceased)"; 
    }
    
    public String getMaritalStatus () {
        return maritalStatus;
    }
    
    public String getCauseOfDeath () {
        return (death == null) ? "" : death.getComment();
    }

    public String getRegistrationLocation () {
        return getPlaceOfDeath();
    }
    
    public boolean isCertificatePresent() {
        return (death == null) ? false : death.isCertificatePresent();
    }
}
