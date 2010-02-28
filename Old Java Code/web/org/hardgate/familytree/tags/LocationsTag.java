package org.hardgate.familytree.tags;

import java.util.Arrays;
import java.util.Iterator;
import java.util.Set;

import javax.servlet.jsp.JspException;
import javax.servlet.jsp.JspWriter;
import javax.servlet.jsp.tagext.BodyContent;
import javax.servlet.jsp.tagext.BodyTagSupport;

import org.hardgate.familytree.core.Client;
import org.hardgate.familytree.core.Location;

/**
 *  Generated tag class.
 *
 * @jsp:tag
 *    name="locationList"
 *    body-content="JSP"
 *    display-name="Location List"
 *
 * @jsp:variable
 *    name-given="country"
 *    class="java.lang.String"
 *    declare="true"
 *    scope="NESTED"
 *
 * @jsp:variable
 *    name-given="region"
 *    class="java.lang.String"
 *    declare="true"
 *    scope="NESTED"
 *
 * @jsp:variable
 *    name-given="parish"
 *    class="java.lang.String"
 *    declare="true"
 *    scope="NESTED"
 *
 * @jsp:variable
 *    name-given="address"
 *    class="java.lang.String"
 *    declare="true"
 *    scope="NESTED"
 *
 * @jsp:variable
 *    name-given="place"
 *    class="java.lang.String"
 *    declare="true"
 *    scope="NESTED"
 *
 * @jsp:variable
 *    name-given="parishID"
 *    class="java.lang.String"
 *    declare="true"
 *    scope="NESTED"
 *
 * @jsp:variable
 *    name-given="surnames"
 *    class="java.lang.String"
 *    declare="true"
 *    scope="NESTED"
 *
 */
public class LocationsTag extends BodyTagSupport {

	private static final long serialVersionUID = 0;
	
    private int iloop;
    private int memberID;
    private int level;
    private Location[] locations;
	private Client client = Client.getInstance();
	private Location lastLocation;
    
    public LocationsTag() {
        super();
    }

    public int doStartTag() throws JspException, JspException {
        locations = client.getAllLocations(memberID);
        Arrays.sort(locations);
        iloop = 0;
        lastLocation = new Location();
        if(iloop < locations.length) {
            processNextResult(locations[0]);
            iloop++;
            return EVAL_BODY_BUFFERED;
        } else {
            return SKIP_BODY;
        }
    }

    public int doAfterBody() throws JspException {
        try {
            BodyContent bodyContent = getBodyContent();
            JspWriter out = bodyContent.getEnclosingWriter();
            bodyContent.writeOut(out);
            bodyContent.clearBody();
        } catch (Exception ex) {
            throw new JspException("error in LocationListTag: " + ex);
        }
        while (iloop < locations.length) {
            Location location = locations[iloop];
            if (location.compareTo(lastLocation, level) != 0) {
	            processNextResult(location);
	            return EVAL_BODY_AGAIN;
            }
            iloop++;
        } 
        return SKIP_BODY;
    }

    public int doEndTag() throws JspException {
        locations = null;
        return EVAL_PAGE;
    }
    
    private void processNextResult(Location location) {
        pageContext.setAttribute("country", location.getCountry());
        pageContext.setAttribute("region", location.getRegion());
        pageContext.setAttribute("parish", location.getParish());
        pageContext.setAttribute("address", location.getAddress());
        pageContext.setAttribute("place", location.getPlace());
        pageContext.setAttribute("parishID", location.getParishID());
        Set surnames = client.getSurnamesAtLocation(new Integer(memberID),
        		location);
        StringBuilder s = new StringBuilder();
        for (Iterator it = surnames.iterator(); it.hasNext(); ) {
        	s.append((String) it.next());
        	if (it.hasNext())
        		s.append(", ");
        }
        pageContext.setAttribute("surnames", s.toString());
        lastLocation = location;
    }

	/**
	* @jsp:attribute
	*     required="true"
	*     rtexprvalue="true"
	*     type="int"
	*/
    public int getMemberID() {
        return this.memberID;
    }
    
    public void setMemberID(int value) {
        this.memberID = value;
    }

    /**
	* @jsp:attribute
	*     required="true"
	*     rtexprvalue="true"
	*     type="int"
	*/
    public int getlevel() {
        return this.level;
    }
    
    public void setlevel(int value) {
        this.level = value;
    }
}
