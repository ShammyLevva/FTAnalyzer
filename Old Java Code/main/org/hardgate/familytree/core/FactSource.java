/*
 * Created on 01-Oct-2004
 *
 */
package org.hardgate.familytree.core;

import org.apache.log4j.Logger;
import org.dom4j.Element;
import org.hardgate.familytree.interfaces.FactSourceLocal;

/**
 * @author A-Bisset
 *
 */
public class FactSource implements java.io.Serializable {
    
	private static final long serialVersionUID = 0;
	private static Logger logger = Logger.getLogger(FactSource.class);
	private static final String BIRTHCERT = "BIRTH", DEATHCERT = "DEATH",
			MARRIAGECERT = "MARRIAGE", CENSUSCERT = "CENSUS";

	private int memberID = 0;
	private String sourceID;
	private String gedcomID;
	private String sourceTitle;
	private String sourceMedium;

	public FactSource(int memberID, Element element) {
		this.memberID = memberID;
		this.sourceID = "";
		this.gedcomID = element.attributeValue("ID");
		this.sourceTitle = element.elementText("TITL").trim();
		Element repo = element.element("REPO");
		if (repo == null) {
		    logger.warn("Missing source medium for source : " + gedcomID);
		    this.sourceMedium = "";
		} else {
		    Element caln = repo.element("CALN");
		    this.sourceMedium = (caln == null) ? "" : caln.elementText("MEDI").trim();
		}
	}
	
	public FactSource(FactSourceLocal fs) {
	    this.memberID = fs.getMemberID().intValue();
	    this.sourceID = fs.getSourceID();
	    this.gedcomID = fs.getGedcomID();
	    this.sourceTitle = fs.getSourceTitle();
	    this.sourceMedium = fs.getSourceMedium();
	}
	
    /**
     * @return Returns the gedcomID.
     */
    public String getGedcomID() {
        return gedcomID;
    }
    /**
     * @param gedcomID The gedcomID to set.
     */
    public void setGedcomID(String gedcomID) {
        this.gedcomID = gedcomID;
    }
    /**
     * @return Returns the memberID.
     */
    public int getMemberID() {
        return memberID;
    }
    /**
     * @param memberID The memberID to set.
     */
    public void setMemberID(int memberID) {
        this.memberID = memberID;
    }
    /**
     * @return Returns the sourceID.
     */
    public String getSourceID() {
        return sourceID;
    }

    /**
     * @param sourceID The sourceID to set.
     */
    public void setSourceID(String sourceID) {
        this.sourceID = sourceID;
    }

    /**
     * @return Returns the sourceTitle.
     */
    public String getSourceTitle() {
        return sourceTitle;
    }

    /**
     * @param sourceTitle The sourceTitle to set.
     */
    public void setSourceTitle(String sourceTitle) {
        this.sourceTitle = sourceTitle;
    }

    /**
     * @return Returns the sourceMedium.
     */
    public String getSourceMedium () {
        return sourceMedium;
    }

    /**
     * @param sourceMedium The sourceMedium to set.
     */
    public void setSourceMedium (String sourceMedium) {
        this.sourceMedium = sourceMedium;
    }
    
    public boolean isBirthCert() {
        return sourceMedium.equals("Official Document") && 
        	   sourceTitle.toUpperCase().indexOf(BIRTHCERT) >= 0;
    }

    public boolean isDeathCert() {
        return sourceMedium.equals("Official Document") && 
        	   sourceTitle.toUpperCase().indexOf(DEATHCERT) >= 0;
    }

    public boolean isMarriageCert() {
        return sourceMedium.equals("Official Document") && 
        	   sourceTitle.toUpperCase().indexOf(MARRIAGECERT) >= 0;
    }

    public boolean isCensusCert() {
        return sourceMedium.equals("Official Document") && 
        	   sourceTitle.toUpperCase().indexOf(CENSUSCERT) >= 0;
    }
}
