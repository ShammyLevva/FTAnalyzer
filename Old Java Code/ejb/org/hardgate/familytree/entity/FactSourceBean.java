package org.hardgate.familytree.entity;

import java.util.Collection;

import javax.ejb.EntityBean;
import javax.ejb.EntityContext;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.FactSource;
import org.hardgate.familytree.interfaces.FactSourceUtil;

/**
 * This entity bean represents the source of a Fact.
 * 
 * @ejb.bean name="FactSource" display-name="" type="CMP"
 *           jndi-name="ejb/FactSource" view-type="local"
 *           primkey-field="sourceID"
 * 
 * @ejb.transaction type="Supports"
 * 
 * @ejb.interface generate="local" package="org.hardgate.familytree.interfaces"
 * 
 * @ejb.finder signature="java.util.Collection findAllSources ()"
 *             unchecked="true" query="SELECT OBJECT(fs) FROM FactSource AS fs"
 * 
 * @ejb.finder signature="FactSourceLocal findByGedcomID (java.lang.Integer memberID, java.lang.String gedcomID)"
 *             unchecked="true"
 *             query="SELECT OBJECT(s) FROM FactSource AS s WHERE s.memberID=?1 and s.gedcomID=?2"
 *             transaction-type="Supports"
 *
 * @ejb.pk generate="false" class="java.lang.String"
 * 
 * @ejb.dao generate="false"
 * 
 * @jboss.persistence table-name="sources" 
 * 					  create-table="false"
 *                    remove-table="false"
 */

public abstract class FactSourceBean implements EntityBean {

	private static Logger logger = Logger.getLogger(IndividualBean.class);
	private EntityContext context;
	/**
	 * @ejb.create-method view-type="local"
	 */

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="sourceID"
	 */
	public abstract java.lang.String getSourceID();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setSourceID(java.lang.String sourceID);

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
	 * @jboss.column-name name="sourcetitle"
	 */
	public abstract java.lang.String getSourceTitle();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setSourceTitle(java.lang.String sourceTitle);

	/**
	 * @ejb.persistent-field
	 * @ejb.interface-method view-type="local"
	 * @jboss.column-name name="sourcemedium"
	 */
	public abstract java.lang.String getSourceMedium();

	/**
	 * @ejb.interface-method view-type="local"
	 */
	public abstract void setSourceMedium(java.lang.String sourceMedium);

	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 * 
	 * @ejb.relation
	 *     name="fact-sources"
	 *     role-name="source-has-many-facts"
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
	 *     fk-column="factID"
	 *     fk-constraint="true"
	 *     related-pk-field="factID"
	 */
	public abstract Collection getSourceFacts();
    
	/**
	 * @ejb.interface-method
	 *     view-type="local"
	 */
	public abstract void setSourceFacts(Collection facts);
	
	/**
	 * @ejb.create-method view-type="local"
	 */
	public String ejbCreate(FactSource fs) throws javax.ejb.CreateException {
		setSourceID(FactSourceUtil.generateGUID(this));
		setMemberID(new Integer(fs.getMemberID()));
		setGedcomID(fs.getGedcomID());
		setSourceTitle(fs.getSourceTitle());
		setSourceMedium(fs.getSourceMedium());
		return getSourceID();
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
