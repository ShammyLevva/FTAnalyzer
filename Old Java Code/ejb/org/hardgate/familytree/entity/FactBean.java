package org.hardgate.familytree.entity;

import javax.ejb.EntityBean;
import javax.ejb.EntityContext;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.interfaces.FactSourceLocal;
import org.hardgate.familytree.interfaces.FactUtil;
import org.hardgate.familytree.interfaces.FamilyLocal;
import org.hardgate.familytree.interfaces.IndividualLocal;

/**
 * This entity bean represents a Fact.
 * 
 * @ejb.bean name="Fact" display-name="" type="CMP" jndi-name="ejb/Fact"
 *           view-type="local" primkey-field="factID"
 * 
 * @ejb.transaction type="Supports"
 * 
 * @ejb.interface generate="local" package="org.hardgate.familytree.interfaces"
 * 
 * @ejb.finder signature="java.util.Collection findAllFacts ()" unchecked="true"
 *             query="SELECT OBJECT(f) FROM Fact AS f"
 * 
 * @ejb.pk generate="false" class="java.lang.String"
 * 
 * @ejb.dao generate="false"
 * 
 * @jboss.persistence table-name="facts"
 * 				create-table="false"
 * 				remove-table="false"
 */

public abstract class FactBean implements EntityBean {

	private static Logger logger = Logger.getLogger(FactBean.class);
	private EntityContext context;

    /**
     * @ejb.persistent-field
     * @ejb.interface-method view-type="local"
     * @jboss.column-name name="factID"
     */
    public abstract java.lang.String getFactID();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setFactID(java.lang.String factID);

    /**
     * @ejb.persistent-field
     * @ejb.interface-method view-type="local"
     * @jboss.column-name name="indivFamily"
     */
    public abstract java.lang.String getIndivFamily();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setIndivFamily(java.lang.String indivFamily);

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
     * @jboss.column-name name="FactType"
     */
    public abstract java.lang.String getFactType();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setFactType(java.lang.String factType);

    /**
     * @ejb.persistent-field
     * @ejb.interface-method view-type="local"
     * @jboss.column-name name="FactDate"
     */
    public abstract java.lang.String getFactDate();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setFactDate(java.lang.String factDate);

    /**
     * @ejb.persistent-field
     * @ejb.interface-method view-type="local"
     * @jboss.column-name name="FactComment"
     */
    public abstract java.lang.String getFactComment();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setFactComment(java.lang.String factComment);

    /**
     * @ejb.persistent-field
     * @ejb.interface-method view-type="local"
     * @jboss.column-name name="FactLocation"
     */
    public abstract java.lang.String getFactLocation();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setFactLocation(java.lang.String factLocation);

    /**
     * @ejb.persistent-field
     * @ejb.interface-method view-type="local"
     * @jboss.column-name name="hasCertificate"
     */
    public abstract java.lang.Boolean getCertificated();

    /**
     * @ejb.interface-method view-type="local"
     */
    public abstract void setCertificated(java.lang.Boolean certificated);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="fact-sources"
	 *     role-name="fact-has-many-sources"
	 * 	   target-multiple="yes"
	 * 
	 * @jboss.relation-mapping
	 *     style="relation-table"
	 * 
	 * @jboss.relation-table
     *      table-name="fact_sources"
     *      create-table="false"
     *      remove-table="false"
     *
	 * @jboss.relation
	 *     fk-column="sourceID"
	 *     fk-constraint="true"
	 *     related-pk-field="sourceID"
	 */
	public abstract java.util.Collection<FactSourceLocal> getSources();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setSources (java.util.Collection<FactSourceLocal> sources);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="individual-facts"
	 *     role-name="fact-has-one-individual"
	 * 
	 * @jboss.relation
	 *     fk-column="individualID"
	 *     related-pk-field="individualID"
	 */
	public abstract IndividualLocal getIndividual();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setIndividual (IndividualLocal individual);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="family-facts"
	 *     role-name="fact-has-one-family"
	 * 
	 * @jboss.relation
	 *     fk-column="familyID"
	 *     related-pk-field="familyID"
	 */
	public abstract IndividualLocal getFamily();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setFamily(FamilyLocal family);

	/**
	 * @ejb.create-method view-type="local"
	 */
	public String ejbCreate(String category, String subjectID, Fact f) 
			throws javax.ejb.CreateException {
		setFactID(FactUtil.generateGUID(this));
		setIndivFamily(category);
		if(category.equals("I")) {
			setIndividualID(subjectID);
		} else {
			setFamilyID(subjectID);
		}
		setFactType(f.getFactType());
		setFactDate(f.getDateString());
		setFactComment(f.getComment());
		setFactLocation(f.getLocation());
		setCertificated(Boolean.FALSE);
		return getFactID();
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