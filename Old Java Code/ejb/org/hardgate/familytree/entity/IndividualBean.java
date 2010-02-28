package org.hardgate.familytree.entity;

import java.util.Collection;

import javax.ejb.EntityBean;
import javax.ejb.EntityContext;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.interfaces.IndividualUtil;

/**
 * This entity bean represents a Individual.
 * 
 * @ejb.bean name="Individual" display-name="" type="CMP"
 *           jndi-name="ejb/Individual" view-type="local"
 *           primkey-field="individualID"
 * 
 * @ejb.transaction type="Supports"
 * 
 * @ejb.interface generate="local" package="org.hardgate.familytree.interfaces"
 * 
 * @ejb.finder signature="java.util.Collection findAllIndividuals (java.lang.Integer memberID)"
 *             unchecked="true" query="SELECT OBJECT(i) FROM Individual AS i WHERE i.memberID = ?1"
 * 
 * @ejb.finder signature="IndividualLocal findByGedcomID (java.lang.Integer memberID, java.lang.String gedcomID)"
 *             unchecked="true"
 *             query="SELECT OBJECT(i) FROM Individual AS i WHERE i.memberID=?1 and i.gedcomID=?2"
 *             transaction-type="Supports"
 *
 * @ejb.finder signature="java.util.Collection findAllRelations (java.lang.Integer memberID, java.lang.Integer relation)"
 *             unchecked="true" query="SELECT OBJECT(i) FROM Individual AS i WHERE i.memberID = ?1 and i.relation = ?2"
 * 
 * @ejb.pk generate="false" class="java.lang.String"
 * 
 * @ejb.dao generate="false"
 * 
 * @jboss.persistence table-name="individuals" 
 * 					  create-table="false"
 *                    remove-table="false"
 */

public abstract class IndividualBean implements EntityBean {

	private static Logger logger = Logger.getLogger(IndividualBean.class);
	private EntityContext context;

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="individualID"
	 */
	public abstract java.lang.String getIndividualID();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setIndividualID(java.lang.String individualID);

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
	 * @jboss.column-name name="GEDCOMID"
	 */
	public abstract java.lang.String getGedcomID();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setGedcomID(java.lang.String gedcomID);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="forenames"
	 */
	public abstract java.lang.String getForenames();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setForenames(java.lang.String forenames);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="gender"
	 */
	public abstract java.lang.String getGender();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setGender(java.lang.String gender);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="surname"
	 */
	public abstract java.lang.String getSurname();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setSurname(java.lang.String surname);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="alias"
	 */
	public abstract java.lang.String getAlias();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setAlias(java.lang.String alias);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="relation"
	 */
	public abstract java.lang.Integer getRelation();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setRelation(java.lang.Integer relation);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="individual-facts"
	 *     role-name="individual-has-many-facts"
	 */
	public abstract java.util.Collection getFacts();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setFacts (java.util.Collection facts);

    /**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="individual-main-family-husband"
	 *     role-name="individual-is-husband"
	 */
	public abstract Collection getHusbandFamily();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setHusbandFamily(Collection family);

    /**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="individual-main-family-wife"
	 *     role-name="individual-is-wife"
	 */
	public abstract Collection getWifeFamily();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setWifeFamily(Collection family);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="individual-family-children"
	 *     role-name="child-has-many-families"
	 * 	   target-multiple="yes"
	 * 
	 * @jboss.relation-mapping
	 *     style="relation-table"
	 * 
	 * @jboss.relation-table
     *      table-name="family_children"
     *      create-table="false"
     *      remove-table="false"
     *
	 * @jboss.relation
	 *     fk-column="familyID"
	 *     fk-constraint="true"
	 *     related-pk-field="familyID"
	 */
	public abstract Collection getChildFamilies();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setChildFamilies(Collection families);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public boolean isMale() {
		return getGender().equals("M");
	}

	/**
	 * @ejb.create-method view-type="local"
	 */
	public String ejbCreate(Individual ind) throws javax.ejb.CreateException {
		setIndividualID(IndividualUtil.generateGUID(this));
		setMemberID(new Integer(ind.getMemberID()));
		setGedcomID(ind.getGedcomID());
		setGender(ind.getGender());
		setSurname(ind.getSurname());
		setForenames(ind.getForenames());
		setAlias(ind.getAlias());
		setRelation(new Integer(ind.getRelation()));
		return getIndividualID();
	}

	public void setEntityContext(javax.ejb.EntityContext aContext) {
		context = aContext;
	}

	public void ejbActivate() {
	}

	public void ejbPassivate() {
	}

	public void ejbRemove() throws javax.ejb.RemoveException {
	}

	public void unsetEntityContext() {
		context = null;
	}

	public void ejbLoad() {
	}

	public void ejbStore() {
	}

}