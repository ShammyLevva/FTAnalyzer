package org.hardgate.familytree.core;

import java.io.PrintWriter;
import java.rmi.RemoteException;
import java.util.Calendar;
import java.util.List;
import java.util.Set;

import javax.ejb.CreateException;
import javax.ejb.FinderException;

import org.hardgate.exceptions.DelegateException;
import org.hardgate.exceptions.NotFoundException;
import org.hardgate.familytree.interfaces.IndividualManagement;
import org.hardgate.familytree.interfaces.IndividualManagementHome;
import org.hardgate.familytree.registrations.Registration;
import org.hardgate.utilities.ServiceLocator;

/**
 * A Client to proxy calls to the course Session Beans.
 * 
 * @web.ejb-ref name="IndividualManagement" type="Session"
 *              link="IndividualManagement"
 *              home="org.hardgate.familytree.interfaces.IndividualManagementHome"
 *              remote="org.hardgate.familytree.interfaces.IndividualManagement"
 * 
 * @jboss.ejb-ref-jndi ref-name="IndividualManagement"
 *                     jndi-name="ejb/IndividualManagement"
 */
public class Client {

    private static Client uniqueInstance = new Client();
    private IndividualManagement indivMgmt;

    // Make the constructor private because this is a Singleton.
    // Note that the IndividualManagementBean is cached during construction,
    // saving overhead for subsequent service invocations.
    private Client () {
        String exceptionMsg = "Unable to create IndividualManagement Session";
        try {
            ServiceLocator locator = ServiceLocator.getInstance();
            IndividualManagementHome home = 
                (IndividualManagementHome) locator.lookup("ejb/IndividualManagement",
                            IndividualManagementHome.class);
            indivMgmt = home.create();
        } catch (CreateException e) {
            throw new DelegateException(exceptionMsg, e);
        } catch (RemoteException e) {
            throw new DelegateException(exceptionMsg, e);
        } catch (ServiceLocator.LocatorException e) {
            throw new DelegateException(exceptionMsg, e);
        }
    }

    /**
     * Returns the Singleton instance.
     * 
     * @return the unique CourseDelegate instance.
     */
    public static Client getInstance () {
        return uniqueInstance;
    }

    public void deleteAllMembersDetails (int memberNumber) {
        try {
            indivMgmt.deleteAllMembersDetails(memberNumber);
        } catch (RemoteException e) {
            throw new DelegateException(
                    "Exception error occurred whilst "
                            + "trying to remove all details for member "
                            + memberNumber, e);
        }
    }

    public void addFactSource (FactSource fs) {
        if (fs == null) {
            throw new DelegateException("Trying to add a null fact source");
        }
        try {
            indivMgmt.addFactSource(fs);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to add fact source " + fs.getGedcomID(), e);
        }
    }

    public void addIndividual (Individual ind) {
        if (ind == null) {
            throw new DelegateException("Trying to add a null individual");
        }
        try {
            indivMgmt.addIndividual(ind);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to add individual " + ind.getName(), e);
        }
    }

    public Individual getGedcomIndividual (int memberID, String gedcomID)
            throws DelegateException, NotFoundException {
        if (gedcomID == null) {
            throw new NotFoundException("No gedcomID provided");
        }
        try {
            return indivMgmt.getGedcomIndividual(new Integer(memberID),
                    gedcomID);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to find individual gedcomID " + gedcomID, e);
        }
    }

    public Family getGedcomFamily (int memberID, String familyGED)
            throws DelegateException, NotFoundException {
        if (familyGED == null) {
            throw new NotFoundException("No familyGED provided");
        }
        try {
            return indivMgmt.getGedcomFamily(new Integer(memberID), familyGED);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to find family gedcomID " + familyGED, e);
        }
    }

    public FactSource getGedcomSource (int memberID, String gedcomID)
    		throws DelegateException, NotFoundException {
		if (gedcomID == null) {
		    throw new NotFoundException("No gedcomID provided");
		}
		try {
		    return indivMgmt.getGedcomSource(new Integer(memberID), gedcomID);
		} catch (RemoteException e) {
		    throw new DelegateException("Exception error occurred whilst "
		            + "trying to find source gedcomID " + gedcomID, e);
		}
	}

    public Family[] getAllFamilies (int memberID) throws DelegateException {
        try {
            return indivMgmt.getAllFamilies(new Integer(memberID));
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to find all families of member " + memberID, e);
        }
    }

    public void addFamily (Family family) {
        try {
            indivMgmt.addFamily(family);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to add family " + family.getFamilyID(), e);
        }
    }

    public List<Registration> getAllBirthRegistrations (int memberID)
            throws DelegateException {
        try {
            return indivMgmt.getAllBirthRegistrations(new Integer(memberID));
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to find all birth registrations of member "
                    + memberID, e);
        }
    }

    public List<Registration> getAllDeathRegistrations (int memberID)
            throws DelegateException {
        try {
            return indivMgmt.getAllDeathRegistrations(new Integer(memberID));
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to find all death registrations of member "
                    + memberID, e);
        }
    }

    public List<Registration> getAllMarriageRegistrations (int memberID)
            throws DelegateException {
        try {
            return indivMgmt.getAllMarriageRegistrations(new Integer(memberID));
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to find all marriage registrations of member "
                    + memberID, e);
        }
    }

    public List<Registration> getAllCensusRegistrations (int memberID, FactDate censusDate)
    		throws DelegateException {
		try {
		    return indivMgmt.getAllCensusRegistrations(new Integer(memberID), censusDate);
		} catch (RemoteException e) {
		    throw new DelegateException("Exception error occurred whilst "
		            + "trying to find all census registrations of member "
		            + memberID, e);
		}
    }
    
    public boolean isMarried (int memberID, Individual ind, FactDate date) 
    			throws DelegateException {
		try {
		    return indivMgmt.isMarried(new Integer(memberID), ind, date);
		} catch (RemoteException e) {
		    throw new DelegateException("Exception error occurred whilst "
		            + "trying to find check marriage status ", e);
		}
    }

    public List getUncertifiedFacts (int memberID, String factType, int relationType)
    		throws DelegateException {
        try {
            return indivMgmt.getUncertifiedFacts(new Integer(memberID),
                    factType, relationType);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to find all loose death dates of member "
                    + memberID, e);
        }
    }

    public List getLooseDeaths (int memberID)
            throws DelegateException {
        try {
            return indivMgmt.getLooseDeaths(new Integer(memberID));
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to find all loose death dates of member "
                    + memberID, e);
        }
    }

    public Location[] getAllLocations (int memberID) {
        try {
            return indivMgmt.getAllLocations(new Integer(memberID));
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get all locations", e);
        }
    }

    public String[] getAllCountries (int memberID) {
        try {
            return indivMgmt.getAllCountries(memberID);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get all countries", e);
        }
    }

    public String[] getRegions (int memberID, String country) {
        try {
            return indivMgmt.getRegions(memberID, country);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get regions in country " + country, e);
        }
    }

    public String[] getParishes (int memberID, String country, String region) {
        try {
            return indivMgmt.getParishes(memberID, country, region);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get parishes in region " + region, e);
        }
    }

    public String[] getAddresses (int memberID, String country, String region,
            String parish) {
        try {
            return indivMgmt.getAddresses(memberID, country, region, parish);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get parishes in parish " + parish, e);
        }
    }

    public String[][] getData (int memberID, String country, String region,
            String parish, String address, int sortOrder) {
        try {
            return indivMgmt.getData(memberID, country, region, parish,
                    address, sortOrder);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get data", e);
        }
    }
    
    public Set getSurnamesAtLocation(Integer memberID, Location loc) {
        try {
            return indivMgmt.getSurnamesAtLocation(memberID, loc);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get surnames", e);
        }
    }

    public void setRelations(int memberID,String startInd) {
        Integer member = new Integer(memberID);
        try {
            indivMgmt.setRelations(member,startInd);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to set Relations", e);
        }
    }
    
    public void setParishes(int memberID) {
        try {
            indivMgmt.setParishes(memberID);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to set parishes", e);
        }
    }
    
    public void printRelationCount(PrintWriter out, int memberID) {
        try {
            indivMgmt.printRelationCount(out, new Integer(memberID));
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to print relations count", e);
        }
    }
    
    public Calendar getMaxLivingDate(Individual ind) {
        try {
            return indivMgmt.getMaxLivingDate(ind);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get Max Living Date", e);
        } catch (FinderException e2) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get Max Living Date", e2);
        }
    }
    
    public Calendar getMinDeathDate(Individual ind) {
        try {
	        return indivMgmt.getMinDeathDate(ind);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get Min Death Date", e);
        }
    }

    public void checkLooseDeath(Individual ind, List r) {
        try {
	        indivMgmt.checkLooseDeath(ind, r);
        } catch (RemoteException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get Max Death Date", e);
        } catch (FinderException e) {
            throw new DelegateException("Exception error occurred whilst "
                    + "trying to get Max Death Date", e);
        }
   }
}