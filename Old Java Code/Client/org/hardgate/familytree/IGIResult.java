/*
 * Created on 17-May-2005
 *
 */
package org.hardgate.familytree;

import java.net.MalformedURLException;
import java.net.URL;

import org.htmlparser.tags.LinkTag;

public class IGIResult {
    
	int searchType;
	String parish;
	String batch;
    String person;
    String gender;
    String spouse;
    String father;
    String mother;
    String birth;
    String christening;
    String death;
    String burial;
    String marriage;
    LinkTag link;
    
    public IGIResult(int searchType, ParishBatch pb, LinkTag link) {
        this.searchType = searchType;
        this.parish = pb.getParish();
        this.batch = pb.getBatch();
    	this.link = link;
        this.person = link.getLinkText();
        this.gender = "";
        this.spouse = "";
        this.mother = "";
        this.father = "";
        this.birth = "";
        this.christening = "";
        this.death = "";
        this.burial = "";   
        this.marriage = "";
    }
    
    public void updateValue(String field, String value) {
        if(field.indexOf("Birth") != -1)
            this.birth = value;
        else if(field.indexOf("Christening") != -1)
            this.christening = value;
        else if(field.indexOf("Death") != -1)
            this.death = value;
        else if(field.indexOf("Burial") != -1)
            this.burial = value;
        else if(field.indexOf("Father") != -1)
            this.father = value;
        else if(field.indexOf("Mother") != -1)
            this.mother = value;
        else if(field.indexOf("Spouse") != -1)
            this.spouse = value;
        else if(field.indexOf("Marriage") != -1)
            this.marriage = value;
    }
    
    public URL getURL() throws MalformedURLException {
        return new URL(link.getLink());
    }
    
    public String getMother() {
        // return only the XXXX bits between ">XXXX</a>
        return noLink(mother);
    }

    public String getPerson() {
        return person;
    }

    public String getSpouse() {
        // return only the XXXX bits between ">XXXX</a>
        return noLink(spouse);
    }

    public String getBirth() {
        return noNBSP(birth);
    }

    public String getFather() {
        // return only the XXXX bits between ">XXXX</a>
        return noLink(father);
    }

    public String getBurial() {
        return noNBSP(burial);
    }

    public String getChristening() {
        return noNBSP(christening);
    }

    public String getDeath() {
        return noNBSP(death);
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }
    
    public String getMarriage() {
        return noNBSP(marriage);
    }

    private String noNBSP(String input) {
        String temp = input;
        String output = "";
        while (temp.indexOf("&nbsp;") !=-1) {
            int pos = temp.indexOf("&nbsp;");
            if(pos > 0)
                output = output + temp.substring(0,pos);
            temp = temp.substring(pos+6);
        }
        if(temp.length() > 0)
            output = output + temp;
        return output;
    }

    private String noLink(String input) {
        String output = input;
        if (input.indexOf(">") !=-1) {
            int start = input.indexOf(">") + 1;
            int end = input.indexOf("</a>") == -1 ? 
                    input.length() : input.indexOf("</a>");
            output = input.substring(start,end);
        }
        return output;
    }

	public int getSearchType() {
		return searchType;
	}

	public String getBatch() {
		return batch;
	}

	public void setBatch(String batch) {
		this.batch = batch;
	}

	public String getParish() {
		return parish;
	}

	public void setParish(String parish) {
		this.parish = parish;
	}
}
