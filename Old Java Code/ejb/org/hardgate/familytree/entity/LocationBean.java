/*
 * Created on 17-Dec-2004
 *
 */
package org.hardgate.familytree.entity;

import java.rmi.RemoteException;
import java.util.Collection;
import java.util.Set;
import java.util.TreeSet;

import javax.ejb.EJBException;
import javax.ejb.EntityBean;
import javax.ejb.EntityContext;
import javax.ejb.FinderException;
import javax.ejb.RemoveException;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.Location;
import org.hardgate.familytree.interfaces.FamilyLocal;
import org.hardgate.familytree.interfaces.IndividualLocal;
import org.hardgate.familytree.interfaces.LocationUtil;

/**
 * This entity bean represents a Location.
 * 
 * @ejb.bean name="Location" display-name="" type="CMP" jndi-name="ejb/Location"
 *           view-type="local" primkey-field="locationID"
 * 
 * @ejb.transaction type="Supports"
 * 
 * @ejb.interface generate="local" package="org.hardgate.familytree.interfaces"
 * 
 * @ejb.finder signature="java.util.Collection findAllLocations
 *             (java.lang.Integer memberID)" unchecked="true" query="SELECT
 *             OBJECT(l) FROM Location AS l WHERE l.memberID = ?1"
 * 
 * @ejb.pk generate="true" class="java.lang.String"
 * 
 * @ejb.dao generate="false"
 * 
 * @jboss.persistence table-name="locations" create-table="false"
 *                    remove-table="false"
 */

public abstract class LocationBean implements EntityBean {

	private static Logger logger = Logger.getLogger(LocationBean.class);

	private EntityContext context;

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="locationID"
	 */
	public abstract java.lang.String getLocationID();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setLocationID(java.lang.String locationID);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="ANESFHS"
	 */
	public abstract java.lang.Integer getMemberID();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setMemberID(java.lang.Integer memberID);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="country"
	 */
	public abstract java.lang.String getCountry();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setCountry(java.lang.String country);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="region"
	 */
	public abstract java.lang.String getRegion();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setRegion(java.lang.String region);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="parish"
	 */
	public abstract java.lang.String getParish();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setParish(java.lang.String parish);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="address"
	 */
	public abstract java.lang.String getAddress();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setAddress(java.lang.String address);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="place"
	 */
	public abstract java.lang.String getPlace();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setPlace(java.lang.String place);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="parishID"
	 */
	public abstract java.lang.String getParishID();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setParishID(java.lang.String parishID);

	/**
	 * @ejb.interface-method view-type="local"
	 * 
	 * @ejb.relation name="location-individual"
	 *               role-name="location-has-one-individual"
	 *               target-ejb="Individual"
	 *               target-role-name="individual-appears-in-many-locations"
	 *               target-multiple="yes"
	 * 
	 * @jboss.relation fk-column="individualID" related-pk-field="individualID"
	 */
	public abstract IndividualLocal getIndividual();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setIndividual(IndividualLocal individual);

	/**
	 * @ejb.interface-method view-type="local"
	 * 
	 * @ejb.relation name="location-family" role-name="location-has-one-family"
	 *               target-ejb="Family"
	 *               target-role-name="family-appears-in-many-locations"
	 *               target-multiple="yes"
	 * 
	 * @jboss.relation fk-column="familyID" related-pk-field="familyID"
	 */
	public abstract FamilyLocal getFamily();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setFamily(FamilyLocal family);

	/**
	 * @ejb.home-method view-type="local"
	 */
	public Set<String> ejbHomeGetAllSurnamesByParish(
			Integer memberID, String country, String region, String parish) 
			throws FinderException {
		Collection<String> c = 
			this.ejbSelectAllSurnamesByParish(memberID,	country, region, parish);
		return new TreeSet<String>(c);
	}

	/**
	 * @ejb.select query="select loc.individual.surname from Location as loc
	 *             where loc.memberID=?1 and loc.country=?2 and loc.region=?3
	 *             and loc.parish=?4 and loc.individual is not null"
	 */
	public abstract Collection<String> ejbSelectAllSurnamesByParish(
			Integer memberID, String country, String region, String parish)
			throws javax.ejb.FinderException;

	/**
	 * @ejb.create-method view-type="local"
	 */
	public String ejbCreate(IndividualLocal indiv, Location loc)
			throws javax.ejb.CreateException {
		setLocationID(LocationUtil.generateGUID(this));
		setMemberID(indiv.getMemberID());
		setCountry(loc.getCountry());
		setRegion(loc.getRegion());
		setParish(loc.getParish());
		setAddress(loc.getAddress());
		setPlace(loc.getPlace());
		return getLocationID();
	}

	public void ejbPostCreate(IndividualLocal indiv, Location loc) {
		setIndividual(indiv);
		setFamily(null);
	}

	/**
	 * @ejb.create-method view-type="local"
	 */
	public String ejbCreate(FamilyLocal family, Location loc)
			throws javax.ejb.CreateException {
		setLocationID(LocationUtil.generateGUID(this));
		setMemberID(family.getMemberID());
		setCountry(loc.getCountry());
		setRegion(loc.getRegion());
		setParish(loc.getParish());
		setAddress(loc.getAddress());
		setPlace(loc.getPlace());
		return getLocationID();
	}

	public void ejbPostCreate(FamilyLocal family, Location loc) {
		setIndividual(null);
		setFamily(family);
	}

	public void ejbActivate() throws EJBException, RemoteException {
	}

	public void ejbLoad() throws EJBException, RemoteException {
	}

	public void ejbPassivate() throws EJBException, RemoteException {
	}

	public void ejbRemove() throws RemoveException, EJBException,
			RemoteException {
	}

	public void ejbStore() throws EJBException, RemoteException {
	}

	public void setEntityContext(EntityContext aContext) throws EJBException,
			RemoteException {
		this.context = aContext;
	}

	public void unsetEntityContext() throws EJBException, RemoteException {
		this.context = null;
	}
}
