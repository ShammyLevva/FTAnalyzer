/*
 * Created on 01-Jan-2005
 *
 */
package org.hardgate.familytree.registrations;

import java.io.Serializable;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.ParentalGroup;

/**
 * @author db
 *
 */
public class BirthRegistration extends Registration implements Serializable {

	private static Logger logger = Logger.getLogger(BirthRegistration.class);
	private static final long serialVersionUID = 0;
	
	private Fact birth;

    public BirthRegistration (ParentalGroup familyGroup) {
        super(familyGroup);
        this.birth = familyGroup.getPreferredFact(Fact.BIRTH);
        registrationDate = (birth == null) ? null : birth.getFactDate();
    }

    public String getRegistrationLocation () {
        return getPlaceOfBirth();
    }

    public boolean isCertificatePresent() {
        return (birth == null) ? false : birth.isCertificatePresent();
    }
}
