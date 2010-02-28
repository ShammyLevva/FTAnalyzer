package org.hardgate.familytree.entity;

import java.util.Collection;
import java.util.LinkedList;

import javax.ejb.EntityBean;
import javax.ejb.EntityContext;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.Family;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.interfaces.FactLocal;
import org.hardgate.familytree.interfaces.FamilyUtil;
import org.hardgate.familytree.interfaces.IndividualLocal;

/**
 * This entity bean represents a Family.
 * 
 * @ejb.bean name="Family" display-name="" type="CMP" jndi-name="ejb/Family"
 *           view-type="local" primkey-field="familyID"
 * 
 * @ejb.transaction type="Supports"
 * 
 * @ejb.interface generate="local" package="org.hardgate.familytree.interfaces"
 * 
 * @ejb.finder signature="FamilyLocal findByGedcomID (java.lang.Integer memberID, java.lang.String gedcomID)"
 *             unchecked="true"
 *             query="SELECT OBJECT(f) FROM Family AS f WHERE f.memberID=?1 and f.gedcomID=?2"
 *             transaction-type="Supports"
 *
 * @ejb.finder signature="java.util.Collection findAllFamilies (java.lang.Integer memberID)" 
 * 			   unchecked="true"
 *             query="SELECT OBJECT(f) FROM Family AS f WHERE f.memberID=?1"
 * 
 * @ejb.finder signature="java.util.Collection findParentalFamily (java.lang.String id)" 
 * 			   unchecked="true"
 *             query="SELECT DISTINCT OBJECT(f) FROM Family AS f, IN (f.children) AS c WHERE c.individualID=?1"
 * 
 * @ejb.finder signature="java.util.Collection findFamiliesOfHusband (java.lang.String id)" 
 * 			   unchecked="true"
 *             query="SELECT DISTINCT OBJECT(f) FROM Family AS f WHERE f.husband.individualID=?1"
 * 
 * @ejb.finder signature="java.util.Collection findFamiliesOfWife (java.lang.String id)" 
 * 			   unchecked="true"
 *             query="SELECT DISTINCT OBJECT(f) FROM Family AS f WHERE f.wife.individualID=?1"
 * 
 * @ejb.pk generate="false" class="java.lang.String"
 * 
 * @ejb.dao generate="false"
 * 
 * @jboss.persistence table-name="families"
 * 				create-table="false"
 * 				remove-table="false"
 */

public abstract class FamilyBean implements EntityBean {

	private static Logger logger = Logger.getLogger(FamilyBean.class);
	private EntityContext context;

    /**
     * @ejb.persistent-field
     * @ejb.interface-method view-type="local"
     * @jboss.column-name name="familyID"
     */
    public abstract java.lang.String getFamilyID();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setFamilyID(java.lang.String familyID);

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
     * @jboss.column-name name="husbandGed"
     */
    public abstract java.lang.String getHusbandGed();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setHusbandGed(java.lang.String husbandGed);

    /**
     * @ejb.persistent-field
     * @ejb.interface-method view-type="local"
     * @jboss.column-name name="wifeGed"
     */
    public abstract java.lang.String getWifeGed();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setWifeGed(java.lang.String wifeGed);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="individual-main-family-husband"
	 *     role-name="family-has-one-husband"
	 * 
	 * @jboss.relation
	 *     fk-column="husbandID"
	 *     related-pk-field="individualID"
	 */
	public abstract IndividualLocal getHusband();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setHusband(IndividualLocal individual);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="individual-main-family-wife"
	 *     role-name="family-has-one-wife"
	 * 
	 * @jboss.relation
	 *     fk-column="wifeID"
	 *     related-pk-field="individualID"
	 */
	public abstract IndividualLocal getWife();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setWife(IndividualLocal individual);

    /**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="family-facts"
	 *     role-name="family-has-many-facts"
	 */
	public abstract java.util.Collection<FactLocal> getFacts();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setFacts (java.util.Collection<FactLocal> facts);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="individual-family-children"
	 *     role-name="family-has-many-children"
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
	 *     fk-column="individualID"
	 *     fk-constraint="true"
	 *     related-pk-field="individualID"
	 */
	public abstract Collection<IndividualLocal> getChildren();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setChildren(Collection<IndividualLocal> children);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public IndividualLocal getSpouse(IndividualLocal ind) {
	    return ind.isMale() ? getWife() : getHusband();
	}
	
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public void setSpouseRelation(IndividualLocal ind, int relation) {
	    IndividualLocal spouse = getSpouse(ind);
	    if (spouse != null && spouse.getRelation().intValue() == Individual.UNKNOWN) {
	        spouse.setRelation(new Integer(relation));
	    }
	}
	
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public void setChildRelation(int relation, LinkedList<IndividualLocal> queue) {
	    Collection<IndividualLocal> children = getChildren();
	    for(IndividualLocal child : children) {
	        if (child.getRelation().intValue() == Individual.UNKNOWN) {
	            // add this previously unknown individual to list 
	            // of relatives to update family of
	            child.setRelation(new Integer(relation));
	            queue.add(child);
	        }
	    }
	}
	
	/**
	 * @ejb.create-method view-type="local"
	 */
	public String ejbCreate(Family fam) 
			throws javax.ejb.CreateException {
		setFamilyID(FamilyUtil.generateGUID(this));
		setMemberID(new Integer(fam.getMemberID()));
		setGedcomID(fam.getFamilyGed());
		setHusbandGed(fam.getHusbandGed());
		setWifeGed(fam.getWifeGed());
		return getFamilyID();
	}

	public void setEntityContext(javax.ejb.EntityContext aContext) {
        this.context = aContext;
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