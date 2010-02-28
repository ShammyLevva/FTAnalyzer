package org.hardgate.familytree.session;

import java.io.PrintWriter;
import java.rmi.RemoteException;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Collection;
import java.util.Iterator;
import java.util.LinkedList;
import java.util.List;
import java.util.Set;

import javax.ejb.CreateException;
import javax.ejb.EJBException;
import javax.ejb.FinderException;
import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.sql.DataSource;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.CensusFamily;
import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.FactSource;
import org.hardgate.familytree.core.Family;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.core.Location;
import org.hardgate.familytree.core.ParentalGroup;
import org.hardgate.familytree.interfaces.FactLocal;
import org.hardgate.familytree.interfaces.FactLocalHome;
import org.hardgate.familytree.interfaces.FactSourceLocal;
import org.hardgate.familytree.interfaces.FactSourceLocalHome;
import org.hardgate.familytree.interfaces.FamilyLocal;
import org.hardgate.familytree.interfaces.FamilyLocalHome;
import org.hardgate.familytree.interfaces.IndividualLocal;
import org.hardgate.familytree.interfaces.IndividualLocalHome;
import org.hardgate.familytree.interfaces.LocationLocal;
import org.hardgate.familytree.interfaces.LocationLocalHome;
import org.hardgate.familytree.registrations.BirthRegistration;
import org.hardgate.familytree.registrations.CensusRegistration;
import org.hardgate.familytree.registrations.DeathRegistration;
import org.hardgate.familytree.registrations.MarriageRegistration;

/**
 * @ejb.bean name="IndividualManagement" display-name="" type="Stateless"
 *           jndi-name="ejb/IndividualManagement" view-type="remote"
 * 
 * @ejb.transaction type="Required"
 * 
 * @ejb.interface generate="remote" package="org.hardgate.familytree.interfaces"
 * 
 * @ejb.ejb-ref ejb-name="Individual" view-type="local"
 *              ref-name="ejb/Individual"
 * @ejb.ejb-ref ejb-name="Fact" view-type="local" 
 * 				ref-name="ejb/Fact"
 * @ejb.ejb-ref ejb-name="Family" view-type="local" 
 * 				ref-name="ejb/Family"
 * @ejb.ejb-ref ejb-name="Location" view-type="local" 
 * 				ref-name="ejb/Location"
 * @ejb.ejb-ref ejb-name="FactSource" view-type="local" 
 * 				ref-name="ejb/FactSource"
 */

public class IndividualManagementBean implements javax.ejb.SessionBean {

	private javax.ejb.SessionContext context;
	private static final long serialVersionUID = 0;
	private static Logger logger = Logger.getLogger(IndividualManagementBean.class);
	private FactLocalHome factHome;
	private IndividualLocalHome individualHome;
	private FamilyLocalHome familyHome;
	private LocationLocalHome locationHome;
	private FactSourceLocalHome factSourceHome;
	private static final DateFormat FULL = new SimpleDateFormat("dd MMM yyyy");
    
	private void error(String msg, Exception ex) {
		String s = "IndividualManagementBean: " + msg + "\n" + ex;
		logger.debug(s);
		throw new EJBException(s, ex);
	}

	public void setSessionContext(javax.ejb.SessionContext aContext) {
        context=aContext;
        InitialContext ic = null;
		try {
            ic = new InitialContext();
            factHome = (FactLocalHome) 
				ic.lookup("java:comp/env/ejb/Fact");
            individualHome = (IndividualLocalHome) 
				ic.lookup("java:comp/env/ejb/Individual");
            familyHome = (FamilyLocalHome) 
				ic.lookup("java:comp/env/ejb/Family");
            locationHome = (LocationLocalHome) 
				ic.lookup("java:comp/env/ejb/Location");
            factSourceHome = (FactSourceLocalHome) 
				ic.lookup("java:comp/env/ejb/FactSource");

/*			factHome = FactUtil.getLocalHome();
			individualHome = IndividualUtil.getLocalHome();
			*/
		} catch (NamingException ex) {
			error("Error looking up depended EJB or resource", ex);
		}
	}

	public void ejbActivate() {
	}

	public void ejbPassivate() {
	}

	public void ejbRemove() {
	}

	/**
	 * @ejb.create-method view-type="remote"
	 */
	public void ejbCreate() {
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Individual[] getAllIndividuals(Integer memberID) throws RemoteException {
		try {
			Collection individuals = individualHome.findAllIndividuals(memberID);
			Individual[] result = new Individual[individuals.size()];
			int i = 0;
			Iterator it = individuals.iterator();
			while(it.hasNext()) {
				IndividualLocal individual = (IndividualLocal) it.next();
				result[i++] = new Individual(individual);
			}
			return result;
		} catch (FinderException ex) {
			return new Individual[0];
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Individual getIndividual(String individualID)
			throws RemoteException {
		try {
			IndividualLocal individual = 
			    individualHome.findByPrimaryKey(individualID);
			return new Individual(individual);
		} catch (FinderException ex) {
			throw new EJBException("No such individual : " + individualID, ex);
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Individual getGedcomIndividual(Integer memberID,String gedcomID)
			throws RemoteException {
		try {
			IndividualLocal individual = 
				individualHome.findByGedcomID(memberID,gedcomID);
			return new Individual(individual);
		} catch (FinderException ex) {
			throw new EJBException("No such individual : " + gedcomID, ex);
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Family[] getAllFamilies(Integer memberID) throws RemoteException {
		try {
			Collection families = familyHome.findAllFamilies(memberID);
			Family[] result = new Family[families.size()];
			int i = 0;
			Iterator it = families.iterator();
			while(it.hasNext()) {
				FamilyLocal family = (FamilyLocal) it.next();
				result[i++] = new Family(family);
			}
			return result;
		} catch (FinderException ex) {
			return new Family[0];
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Family getGedcomFamily(Integer memberID, String gedcomID) 
			throws RemoteException {
		try {
			FamilyLocal family = 
				familyHome.findByGedcomID(memberID,gedcomID);
			return new Family(family);
		} catch (FinderException ex) {
			throw new EJBException("No such family : " + gedcomID, ex);
		}
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Location[] getAllLocations(Integer memberID) throws RemoteException {
		try {
			Collection locations = locationHome.findAllLocations(memberID);
			Location[] result = new Location[locations.size()];
			int i = 0;
			Iterator it = locations.iterator();
			while(it.hasNext()) {
			    LocationLocal location = (LocationLocal) it.next();
				result[i++] = new Location(location);
			}
			return result;
		} catch (FinderException ex) {
			return new Location[0];
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public FactSource getGedcomSource(Integer memberID, String gedcomID) 
			throws RemoteException {
		try {
			FactSourceLocal source = 
				factSourceHome.findByGedcomID(memberID,gedcomID);
			return new FactSource(source);
		} catch (FinderException ex) {
			throw new EJBException("No such source : " + gedcomID, ex);
		}
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public List<BirthRegistration> getAllBirthRegistrations(Integer memberID) throws RemoteException {
		try {
			Collection<IndividualLocal> individuals = individualHome.findAllIndividuals(memberID);
			ArrayList<BirthRegistration> result = 
				new ArrayList<BirthRegistration>(individuals.size());
			for(IndividualLocal i : individuals) {
				ParentalGroup fg = createFamilyGroup(new Individual(i));
				result.add(new BirthRegistration(fg));
			}
			return result;
		} catch (FinderException ex) {
			return new ArrayList<BirthRegistration>(0);
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public List<CensusRegistration> getAllCensusRegistrations
				(Integer memberID, FactDate censusDate) throws RemoteException {
		if (censusDate == null)
		    return new ArrayList<CensusRegistration>(0);
		try {
		    Collection<FamilyLocal> families = familyHome.findAllFamilies(memberID);
		    ArrayList<CensusRegistration> result = 
		    	new ArrayList<CensusRegistration>(families.size());
		    for(FamilyLocal f : families) {
			    CensusFamily censusFamily = new CensusFamily(f);
				if (censusFamily.process(censusDate))
				    result.add(new CensusRegistration(null, censusDate, censusFamily));
			}
			return result;
		} catch (FinderException ex) {
			return new ArrayList<CensusRegistration>(0);
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public List<DeathRegistration> getAllDeathRegistrations(Integer memberID) throws RemoteException {
		try {
			Collection<IndividualLocal> individuals = 
				individualHome.findAllIndividuals(memberID);
			ArrayList<DeathRegistration> result = 
				new ArrayList<DeathRegistration>(individuals.size());
			for (IndividualLocal i : individuals) {
				Individual indiv = new Individual(i);
				if (indiv.getDeathDate() != null) {
				    // only include dead individuals
					ParentalGroup fg = createFamilyGroup(indiv);
					Collection<FamilyLocal> families;
					
				    if (indiv.isMale()) {
				        families = familyHome.findFamiliesOfHusband(
				                indiv.getIndividualID());
				    } else {
				        families = familyHome.findFamiliesOfWife(
				                indiv.getIndividualID());
				    }
				    
				    if (families.size() == 0) {
					    result.add(new DeathRegistration(fg, null, Family.SINGLE));	    
					} else {
						for(FamilyLocal f : families) {
						    Family fam = new Family(f);
					        String maritalStatus = fam.getMaritalStatus();
						    if(indiv.isMale()) {
							    result.add(new DeathRegistration(fg,
								        fam.getWife(), maritalStatus));
						    } else {
							    result.add(new DeathRegistration(fg,
								        fam.getHusband(), maritalStatus));
						    }
						}
					}
				}
			}
			return result;
		} catch (FinderException ex) {
			return new ArrayList<DeathRegistration>(0);
		}
	}
	
    /**
     * @ejb.interface-method view-type="remote"
     */
    public List<Individual> getUncertifiedFacts(Integer memberID, 
            String factType, int relationType) throws RemoteException {
        try {
            Collection<IndividualLocal> individuals = 
                individualHome.findAllIndividuals(memberID);
            ArrayList<Individual> result = 
                new ArrayList<Individual>(individuals.size());
            for(IndividualLocal i : individuals) {
                Individual ind = new Individual(i);
                if (ind.getRelation() == relationType) {
                    Fact fact = ind.getPreferredFact(factType);
                    if (fact != null && !fact.isCertificatePresent())
                        result.add(ind);
                }
            }
            return result;
        } catch (FinderException ex) {
            return new ArrayList<Individual>(0);
        }
    }
    
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public List<Individual> getLooseDeaths(Integer memberID) throws RemoteException {
		try {
			Collection<IndividualLocal> individuals = 
				individualHome.findAllIndividuals(memberID);
			ArrayList<Individual> result = 
				new ArrayList<Individual>(individuals.size());
			for(IndividualLocal i : individuals) {
				checkLooseDeath(new Individual(i),result);
			}
			return result;
		} catch (FinderException ex) {
			return new ArrayList<Individual>(0);
		}
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public void checkLooseDeath(Individual indiv, List<Individual> result) 
				throws FinderException {
		FactDate deathDate = indiv.getDeathDate();
		FactDate toAdd = null;
	    if (deathDate != null && ! deathDate.isExact()) {
		    Calendar maxLiving = getMaxLivingDate(indiv);
		    Calendar minDeath = getMinDeathDate(indiv);
		    if (maxLiving.after(deathDate.getStartDate())) {
		        // the starting death date is before the last alive date
			    // so add to the list of loose deaths
		        toAdd = new FactDate(maxLiving, minDeath);
			} else if (minDeath.before(deathDate.getEndDate())) {
		        // earliest death date before current latest death
		        // so add to the list of loose deaths
		        toAdd = new FactDate(deathDate.getStartDate(), minDeath);
			}
		}
		if (deathDate == null && indiv.getCurrentAge() >= 110) {
		    // also check for empty death dates for people aged 110 or over
		    toAdd = new FactDate(getMaxLivingDate(indiv), getMinDeathDate(indiv));
		}
	    if (toAdd != null && !toAdd.equals(deathDate)) {
	        // we have a date to change and its not the same 
	        // range as the existing death date
		    Fact looseDeath = new Fact(Fact.LOOSEDEATH, toAdd);
		    indiv.addFact(looseDeath);
		    result.add(indiv);
	    }
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Calendar getMaxLivingDate(Individual indiv) throws FinderException {
		Collection<FamilyLocal> families;
	    if (indiv.isMale()) {
	        families = familyHome.findFamiliesOfHusband(
	                indiv.getIndividualID());
	    } else {
	        families = familyHome.findFamiliesOfWife(
	                indiv.getIndividualID());
	    }
		// having got the families the individual is a parent of
	    // get the max startdate of the birthdate of the youngest child
	    // this then is the minimum point they were alive
	    // subtract 9 months for a male
	    Calendar maxdate = FactDate.MINDATE;
	    boolean childDate = false;
		for(FamilyLocal f : families) {
		    Family fam = new Family(f);
		    FactDate marriageDate = fam.getPreferredFactDate(Fact.MARRIAGE);
		    if (marriageDate.getStartDate().after(maxdate) 
		            && ! marriageDate.isLongYearSpan()) {
		        maxdate = marriageDate.getStartDate();
		    }
		    List<Individual> children = fam.getChildren();
		    for(Individual child : children) {
			    FactDate birthday = child.getBirthDate();
			    if (birthday.getStartDate().after(maxdate)) { 
			        maxdate = birthday.getStartDate();
			        childDate = true;
			    }
			}
		}
		if (childDate && indiv.isMale() &&  
		        maxdate.after(FactDate.MINDATE)) {
		    // set to Jan 01 of prior year from start date if indiv is a father 
		    // and we have changed maxdate from the MINDATE default
		    // and the date is derived from a child not a marriage
		    maxdate.add(Calendar.YEAR, -1);
		    maxdate.set(Calendar.MONTH, 0);
		    maxdate.set(Calendar.DAY_OF_MONTH, 1);
		}
		List<Fact> census = indiv.getFacts(Fact.CENSUS);
		for(Fact censusFact : census) {
		    Calendar censusDate = censusFact.getFactDate().getStartDate();
		    if (censusDate.after(maxdate)) {
		        maxdate = censusDate;
		    }
		}
		List<Fact> witness = indiv.getFacts(Fact.WITNESS);
		for(Fact witnessFact : witness) {
		    Calendar witnessDate = witnessFact.getFactDate().getStartDate();
		    if (witnessDate.after(maxdate)) {
		        maxdate = witnessDate;
		    }
		}
		// at this point we have the maximum point a person was alive
		// based on their oldest child and last census record and marriage date
		return maxdate;
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Calendar getMinDeathDate(Individual indiv) {
	    FactDate deathDate = indiv.getDeathDate();
		Calendar now = Calendar.getInstance();
        Calendar minDeath = indiv.getBirthDate().getEndDate();
        // set tooOld to 31st Dec 110 years after birth.
        if (minDeath.get(Calendar.MONTH) == Calendar.DECEMBER &&
            minDeath.get(Calendar.DAY_OF_MONTH) == 31) { 
            // because a BEF XXXX is already 31/12/XXXX need to add 111 years
            	minDeath.add(Calendar.YEAR, 111);
		} else {
		    minDeath.add(Calendar.YEAR, 110);
		    minDeath.set(Calendar.MONTH, 11);
		    minDeath.set(Calendar.DAY_OF_MONTH, 31);
		}
	    if (minDeath.after(now)) {
	        // 110 years after death is after todays date so we set the
	        // maximum to 31 DEC last year.
	        minDeath.set(Calendar.YEAR, now.get(Calendar.YEAR));
	    }
        if (deathDate == null || minDeath.before(deathDate.getEndDate()))
            return minDeath;
        else
            return deathDate.getEndDate();
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public List<MarriageRegistration> getAllMarriageRegistrations(Integer memberID) 
				throws RemoteException {
		try {
			Collection<IndividualLocal> individuals = individualHome.findAllIndividuals(memberID);
			ArrayList<MarriageRegistration> result = 
				new ArrayList<MarriageRegistration>(individuals.size());
			for(IndividualLocal i : individuals) {
				Individual indiv = new Individual(i);
				if (!indiv.isSingleAtDeath()) {
				    // only look for marriages for over 16s
				    // who were not known to be single
					ParentalGroup fg = createFamilyGroup(indiv);
					Collection<FamilyLocal> families;
					
				    if (indiv.isMale()) {
				        families = familyHome.findFamiliesOfHusband(
				                indiv.getIndividualID());
				    } else {
				        families = familyHome.findFamiliesOfWife(
				                indiv.getIndividualID());
				    }
				    
				    if (families.size() == 0) {
					    result.add(new MarriageRegistration(fg, null, null));	    
					} else if (indiv.isMale()) {
					    // Avoid duplicate marriages by only creating
					    // registrations for the husband.
						for(FamilyLocal f : families) {
						    Family fam = new Family(f);
				            ParentalGroup fg2 = createFamilyGroup(fam.getWife());
						    result.add(new MarriageRegistration(fg, fg2, fam));
						}
					}
				}
			}
			return result;
		} catch (FinderException ex) {
			return new ArrayList<MarriageRegistration>(0);
		}
	}
	
	private ParentalGroup createFamilyGroup (Individual i) throws FinderException {
	    if (i == null)
	        return null;
	    
		Collection families = familyHome.findParentalFamily(i.getIndividualID());
		if (families.size() > 0) {
		    Family fam = new Family((FamilyLocal) (families.iterator().next()));
		    return new ParentalGroup (i, fam.getHusband(), fam.getWife(),
		            fam.getPreferredFact(Fact.MARRIAGE));
		} else {
		    return new ParentalGroup(i, null, null, null);
		}
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public void addFactSource(FactSource fs) throws RemoteException {
		try {
		    FactSourceLocal factSource = factSourceHome.create(fs);
		} catch (CreateException ex) {
			error("addFactSource: create exception", ex);
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public void addIndividual(Individual ind) throws RemoteException {
		try {
		    IndividualLocal indiv = individualHome.create(ind);
			addFacts("I", ind.getAllFacts(), indiv.getIndividualID(), indiv);
		} catch (CreateException ex) {
			error("addIndividual: create exception", ex);
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public void addFamily(Family fam) throws RemoteException {
		try {
		    FamilyLocal family = familyHome.create(fam);
			String familyID = family.getFamilyID();

			if (fam.getHusbandID() != null && fam.getHusbandID().length() > 0) {
			    family.setHusband(
			            individualHome.findByPrimaryKey(fam.getHusbandID()));
			}
			if (fam.getWifeID() != null && fam.getWifeID().length() > 0) {
			    family.setWife(
			            individualHome.findByPrimaryKey(fam.getWifeID()));
			}
			for (Individual child : fam.getChildren()) {
			    family.getChildren().add(
			            individualHome.findByPrimaryKey(child.getIndividualID()));
			}
		    addFacts("F", fam.getAllFacts(), familyID, family);
		} catch (CreateException ex) {
			error("addFamily: create exception", ex);
		} catch (FinderException ex) {
		    error("addFamily: individual in family not found", ex);
		}
	}

	private void addFacts(String factClass, Collection<Fact> facts, 
	        String ID, Object local) throws CreateException {
		for (Fact f : facts) {
		    if(f != null && !f.getFactType().equals("")) {
		        FactLocal newFact = factHome.create(factClass, ID, f);
		        if(f.getLocation().length() != 0) {
		            if (factClass.equals("I"))
		                locationHome.create((IndividualLocal) local, 
		                    new Location(f.getLocation()));
		            else
		                locationHome.create((FamilyLocal) local, 
			                    new Location(f.getLocation()));
		        }
			    Iterator sourceIter = f.getSources().iterator();
			    while(sourceIter.hasNext()) {
			        FactSource fs = (FactSource) sourceIter.next();
			        try {
			            newFact.getSources().add(
			                    factSourceHome.findByPrimaryKey(fs.getSourceID()));
			        } catch(FinderException e) {
			            error("addFacts: unable to find source " + 
			                    fs.getSourceID(), e);
			        }
			        if ((f.getFactType() == Fact.BIRTH && fs.isBirthCert()) ||
			            (f.getFactType() == Fact.DEATH && fs.isDeathCert()) ||
			            (f.getFactType() == Fact.MARRIAGE && fs.isMarriageCert()) ||
			            (f.getFactType() == Fact.CENSUS && fs.isCensusCert())) {
			            	newFact.setCertificated(Boolean.TRUE);
			        }
			    }
		    }
		}
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public boolean isMarried(Integer memberID, Individual ind, FactDate date) {
	    if (ind.isSingleAtDeath())
	        return false;
	    IndividualLocal indiv;
	    try {
		    indiv = individualHome.findByGedcomID(memberID, ind.getGedcomID());
			Collection families = getFamiliesAsParent(indiv);
    		Iterator famIt = families.iterator();
    		while (famIt.hasNext()) {
    		    Family family = new Family((FamilyLocal) (famIt.next()));
    		    FactDate marriage = family.getPreferredFactDate(Fact.MARRIAGE);
    		    if (marriage !=null && marriage.isBefore(date))
    		        return true;
    		}
		} catch (FinderException ex) {
		    return false;
		}
	    return false;
	}
	
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public void deleteAllMembersDetails(int memberNumber) {
	    try {
			Context lContext = new InitialContext();
			DataSource ds = (DataSource) lContext.lookup("java:/FamilyTree");
			Connection conn = ds.getConnection();
			Statement stmt = conn.createStatement();
			// drop any existing data
			stmt.executeUpdate("delete from families where anesfhs=" + memberNumber);	
			stmt.executeUpdate("delete from individuals where anesfhs="	+ memberNumber);
			stmt.executeUpdate("delete from sources where anesfhs=" + memberNumber);
			stmt.executeUpdate("delete from locations where anesfhs=" + memberNumber);
			stmt.close();
			conn.close();
		} catch(NamingException e) {
			error("Error trying to connect to database",e);
		} catch (SQLException e) {
			error("Error trying to delete individuals",e);
		} 
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Fact[] getAllFacts() throws RemoteException {
		try {
			Collection facts = factHome.findAllFacts();
			Fact[] result = new Fact[facts.size()];
			int i = 0;
			Iterator it = facts.iterator();
			while(it.hasNext()) {
				FactLocal fact = (FactLocal) it.next();
				result[i++] = new Fact(fact);
			}
			return result;
		} catch (FinderException ex) {
			return new Fact[0];
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Fact getFact(String factID) throws RemoteException {
		try {
			FactLocal fact = factHome.findByPrimaryKey(factID);
			return new Fact(fact);
		} catch (FinderException ex) {
			throw new EJBException("No such fact", ex);
		}
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public Set getSurnamesAtLocation(Integer memberID, Location loc) {
		try {
			return locationHome.getAllSurnamesByParish(
						memberID, loc.getCountry(),
						loc.getRegion(), loc.getParish());
		} catch (FinderException ex) {
			throw new EJBException("No names found for that location", ex);
		}

	}	

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public String[] getAllCountries (int memberID) {
		String command = "select country from locations " +
					"where anesfhs = " + memberID + " " +
			        "group by country order by country";
		return getList(command, "country");
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public String[] getRegions (int memberID, String country) {
		String command = "select region from locations " +
				"where anesfhs = " + memberID + " and country like '" +
				country + "' group by region order by region";
		return getList(command, "region");
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public String[] getParishes (int memberID, String country, String region) {
		String command = "select parish from locations " +
				"where anesfhs = " + memberID + " and country like '" +
				country + "' and region like '" + region +
				"' group by parish order by parish";
		return getList(command, "parish");
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public String[] getAddresses (int memberID, String country, String region,
	        String parish) {
		String command = "select address from locations " +
				"where anesfhs = " + memberID + " and country like '" +
				country + "' and region like '" + region + "' and parish like '" +
				parish + "' group by address order by address";
		return getList(command, "address");
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public String[][] getData (int memberID, String country, String region,
	        String parish, String address, int sortOrder) {
		ArrayList<String[]> list = new ArrayList<String[]>();
	    try {
			Context lContext = new InitialContext();
			DataSource ds = (DataSource) lContext.lookup("java:/FamilyTree");
			Connection conn = ds.getConnection();
			Statement stmt = conn.createStatement();
			if (country.equals("")) {
			    // this forces all records to be returned if no country 
			    // is yet selected
			    country = "%"; 
			}
			ResultSet rs = stmt.executeQuery(
			        "select i.name, l.country, l.region, l.parish, " +
			        	   "l.address, l.place " +
			        "from locations l, individuals i " +
			        "where i.anesfhs = " + memberID + 
			        "  and l.anesfhs = " + memberID + 
			        "  and l.country like '" + country + "' " +
			        "  and l.region like '" + region + "' " +
			        "  and l.parish like '" + parish + "' " +
			        "  and l.address like '" + address + "' " +
			        "  and l.individualId=i.individualId " +
			        "group by i.name, l.country, l.region, l.parish, " +
			        	     "l.address, l.place " +
			        "order by " + (sortOrder +1) + ",i.surname"
			        );
			while (rs.next()) {
			    String[] item = new String[] {
			            rs.getString("name"),
			            rs.getString("country"),
			            rs.getString("region"),
			            rs.getString("parish"),
			            rs.getString("address"),
			            rs.getString("place")
			    };
			    list.add(item);
			}
			rs.close();
			stmt.close();
			conn.close();
		} catch(NamingException e) {
			error("Error trying to connect to database",e);
		} catch (SQLException e) {
			error("Error trying to get list of data", e);
		} 
		return (String[][])list.toArray(new String[list.size()][6]);
	}
	
	private String[] getList (String sqlCommand, String fieldName) {
		ArrayList<String> list = new ArrayList<String>();
	    try {
			Context lContext = new InitialContext();
			DataSource ds = (DataSource) lContext.lookup("java:/FamilyTree");
			Connection conn = ds.getConnection();
			Statement stmt = conn.createStatement();
			ResultSet rs = stmt.executeQuery(sqlCommand);
			while (rs.next()) {
			    list.add(rs.getString(fieldName));
			}
			rs.close();
			stmt.close();
			conn.close();
		} catch(NamingException e) {
			error("Error trying to connect to database",e);
		} catch (SQLException e) {
			error("Error trying to get list of data for field " + fieldName, e);
		} 
		return (String[])list.toArray(new String[list.size()]);
	}
	
	private void clearRelations(Integer memberID) throws FinderException {
	    Integer unknown = new Integer(Individual.UNKNOWN);
		Collection individuals = individualHome.findAllIndividuals(memberID);
		Iterator it = individuals.iterator();
		while(it.hasNext()) {
			IndividualLocal individual = (IndividualLocal) it.next();
			individual.setRelation(unknown);
		}
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public void setRelations(Integer memberID, String startGed) {
	    try {
	        clearRelations(memberID);
	        IndividualLocal ind = 
	            individualHome.findByGedcomID(memberID, startGed);
	        LinkedList<IndividualLocal> queue = new LinkedList<IndividualLocal>();
	        queue.add(ind);
	        while (!queue.isEmpty()) {
	            // now take an item from the queue
	            ind = (IndividualLocal) queue.remove();
	            // set them as a direct relation
	            ind.setRelation(new Integer(Individual.DIRECT));
	            addParentsToQueue(ind, queue);
	        }
	        // we have now added all direct ancestors
	        Collection<IndividualLocal> directs = 
	        	individualHome.findAllRelations(memberID, new Integer(Individual.DIRECT));
			queue.addAll(directs);
			while(!queue.isEmpty()) {
				// get the next person
			    IndividualLocal indiv = (IndividualLocal) queue.remove();
				Collection<FamilyLocal> families = getFamiliesAsParent(indiv);
	    		for (FamilyLocal family : families) {
	    	        // if the spouse of a direct ancestor is not a direct
	    	        // ancestor then they are only related by marriage
	    		    family.setSpouseRelation(indiv, Individual.MARRIAGEDB);
    		        // all children of direct ancestors and blood relations
    		        // are blood relations
	    		    family.setChildRelation(Individual.BLOOD, queue);
	    		}
			}
			// we have now set all direct ancestors and all blood relations
			// all that remains is to loop through the marriage relations
			Collection<IndividualLocal> marrieds = 
			    individualHome.findAllRelations(memberID, new Integer(Individual.MARRIAGE));
			Collection<IndividualLocal> marriedDBs = 
			    individualHome.findAllRelations(memberID, new Integer(Individual.MARRIAGEDB));
			queue.addAll(marriedDBs);
			queue.addAll(marrieds);
			while(!queue.isEmpty()) {
				// get the next person
			    IndividualLocal indiv = (IndividualLocal) queue.remove();
			    // first only process this individual if they are related by marriage
			    // or still unknown
			    int relationship = indiv.getRelation().intValue();
			    if (relationship == Individual.MARRIAGE || 
			        relationship == Individual.MARRIAGEDB ||
			        relationship == Individual.UNKNOWN) {
			        // set this individual to be related by marriage
			        if (relationship == Individual.UNKNOWN)
			            indiv.setRelation(new Integer(Individual.MARRIAGE));
			        addParentsToQueue(indiv, queue);
			        Collection families = getFamiliesAsParent(indiv);
		    		Iterator famIt = families.iterator();
		    		while (famIt.hasNext()) {
		    		    FamilyLocal family = (FamilyLocal) (famIt.next());
		    		    family.setSpouseRelation(indiv, Individual.MARRIAGE);
		    	        // children of relatives by marriage that we haven't previously 
		    	        // identified are also relatives by marriage
		    		    family.setChildRelation(Individual.MARRIAGE, queue);
		    		}
			    }
			}
	    } catch (FinderException e) {
	        error("Error finding individual in setRelations", e);
	    }
	}

	private void addParentsToQueue(IndividualLocal indiv, 
			LinkedList<IndividualLocal> queue) throws FinderException {
        Collection<FamilyLocal> families = 
        	familyHome.findParentalFamily(indiv.getIndividualID());
		if (families.size() > 0) {
		    FamilyLocal family = (FamilyLocal) (families.iterator().next());
		    // add parents to queue
		    if (family.getHusband() != null)
		        queue.add(family.getHusband());
		    if (family.getWife() != null)
    		    queue.add(family.getWife());
		}
	}
	
	private Collection<FamilyLocal> getFamiliesAsParent(IndividualLocal indiv) 
				throws FinderException {
	    if (indiv.isMale()) {
	        return familyHome.findFamiliesOfHusband(indiv.getIndividualID());
		} else { 
	        return familyHome.findFamiliesOfWife(indiv.getIndividualID());
		}
	}
	
	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public void printRelationCount(PrintWriter out, Integer memberID) {
	    try {
			Context lContext = new InitialContext();
			DataSource ds = (DataSource) lContext.lookup("java:/FamilyTree");
			Connection conn = ds.getConnection();
			Statement stmt = conn.createStatement();
			ResultSet rs = stmt.executeQuery(
			        "select relation, count(*) as number " +
			        "from individuals " +
			        "where ANESFHS=" + memberID + 
			       " group by relation");
			out.println("<blockquote>");
			while (rs.next()) {
			    switch (rs.getInt("relation")) {
			    	case Individual.UNKNOWN :
			    	    out.println("Unknown " + rs.getInt("number"));
			    		break;
			    	case Individual.DIRECT :
			    	    out.println("Direct Ancestor " + rs.getInt("number"));
			    		break;
			    	case Individual.BLOOD :
			    	    out.println("Blood Relation " + rs.getInt("number"));
			    		break;
			    	case Individual.MARRIAGEDB :
			    	    out.println("Married to Blood or Direct Relation " + rs.getInt("number"));
			    		break;
			    	case Individual.MARRIAGE :
			    	    out.println("Related by Marriage " + rs.getInt("number"));
			    }
			    out.println("<BR>");
			}
			out.println("</blockquote>");
			rs.close();
			stmt.close();
			conn.close();
		} catch(NamingException e) {
			error("Error trying to connect to database",e);
		} catch (SQLException e) {
			error("Error trying to update parish list", e);
		} 
	}

	/**
	 * @ejb.interface-method view-type="remote"
	 */
	public void setParishes(int memberID) {
	    try {
			Context lContext = new InitialContext();
			DataSource ds = (DataSource) lContext.lookup("java:/FamilyTree");
			Connection conn = ds.getConnection();
			Statement stmt = conn.createStatement();
			stmt.execute(
			        "update locations, parishes " +
			        "set locations.parishId = parishes.parishID " +
			        "where locations.country = parishes.country " +
			        "  and locations.region = parishes.region " +
			        "  and locations.parish = parishes.parishname " +
			        "  and locations.ANESFHS=" + memberID);
			stmt.close();
			conn.close();
		} catch(NamingException e) {
			error("Error trying to connect to database",e);
		} catch (SQLException e) {
			error("Error trying to update parish list", e);
		} 
	}
}