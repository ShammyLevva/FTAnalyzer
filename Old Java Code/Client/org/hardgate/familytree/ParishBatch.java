/*
 * Created on 19-May-2005
 *
 */
package org.hardgate.familytree;

public class ParishBatch {
    
    private String batch;
    private String parishID;
    private String parish;
    private String startYear;
    private String endYear;
    private String comments;
    
    public ParishBatch(String batch, String parishID) {
        this.batch = batch;
        this.parish = "";
        this.parishID = parishID;
        this.startYear = "";
        this.endYear = "";
        this.comments = "";
    }

    public String getBatch() {
        return batch;
    }

    public String getParish() {
        return parish;
    }

    public String getParishID() {
        return parishID;
    }

    public String getComments() {
        return comments == null ? "" : "(" + comments + ")";
    }

    public void setComments(String comments) {
        this.comments = comments;
    }

    public String getEndYear() {
        return endYear;
    }

    public void setEndYear(String endYear) {
        this.endYear = endYear;
    }

    public String getStartYear() {
        return startYear;
    }

    public void setStartYear(String startYear) {
        this.startYear = startYear;
    }

	public void setParish(String parish) {
		this.parish = parish;
	}

}
