/*
 * UploadServlet.java
 *
 * Created on 28 September 2004
 */

package org.hardgate.familytree.servlet;

import java.io.IOException;
import java.io.PrintWriter;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.log4j.Logger;
import org.hardgate.familytree.core.Client;
import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.Individual;

/**
 * 
 * @author A-Bisset
 * @version
 * 
 * @web.servlet name="UncertifiedFactsServlet" 
 * 				description="Lists all facts of a particular type that are uncertified"
 * 
 * @web.servlet-mapping url-pattern="/UncertifiedFactsServlet"
 * 
 */
public class UncertifiedFactsServlet extends HttpServlet {

	private static Logger logger = Logger.getLogger(UncertifiedFactsServlet.class);
	private static final long serialVersionUID = 0;
	private int memberNumber;
    private String factType;
    private int relationType;

	/** Initializes the servlet. */
	public void init(ServletConfig config) throws ServletException {
		super.init(config);
	}

	/** Destroys the servlet */
	public void destroy() {
	}

	/**
	 * Processes requests for both HTTP <code>GET</code> and <code>POST</code>
	 * methods.
	 * 
	 * @param request
	 *            servlet request
	 * @param response
	 *            servlet response
	 */
	protected void processRequest(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException {
		response.setContentType("text/html");
		PrintWriter out = response.getWriter();
        try {
            memberNumber = Integer.parseInt(request.getParameter("memberID"));
        } catch (NumberFormatException e) {
            memberNumber = 0;
        }
        try {
            relationType = Integer.parseInt(request.getParameter("relationType"));
        } catch (NumberFormatException e) {
            relationType = Individual.BLOOD;
        }
        factType = request.getParameter("factType");

		out.println("<html>");
		out.println("<head>");
		out.println("<title>Individuals with uncertified fact details</title>");
		out.println("</head>");
		out.println("<body>");
		out.println("<H3>Individuals with uncertified fact details</H3>");
		getUncertifiedFacts(out);
		out.println("<P><a href='index.jsp'>Return to main page</a>");
		out.println("</body>");
		out.println("</html>");
		out.close();
	}

	/**
	 * Handles the HTTP <code>POST</code> method.
	 * 
	 * @param request
	 *            servlet request
	 * @param response
	 *            servlet response
	 */
	protected void doPost(HttpServletRequest request,
			HttpServletResponse response) throws ServletException,
			java.io.IOException {
		processRequest(request, response);
	}

	/**
	 * Returns a short description of the servlet.
	 */
	public String getServletInfo() {
		return "Servlet to list loose deaths.";
	}

	private void getUncertifiedFacts (PrintWriter output) {
        Client client = Client.getInstance();
        List<Individual> uncertifiedFacts = 
            client.getUncertifiedFacts(memberNumber, factType, relationType);
        Collections.sort(uncertifiedFacts, new IndividualComparator());
        output.print("<table><tr><th>Name</th><th>Current death date</th>");
        output.println("<th>Better death date</th></tr>");
        for (Individual indiv : uncertifiedFacts) {
            output.print("<tr><td><B>");
            output.print(indiv.getName());
            output.print("</b>&nbsp;</td><td>");
            output.print(indiv.getPreferredFactDate(Fact.DEATH).getDateString().toLowerCase());
            output.print("&nbsp;</td><td>");
            output.print(indiv.getPreferredFactDate(Fact.LOOSEDEATH).getDateString().toLowerCase());
            output.println("</td></tr>");
        }
        output.println("</table><P><HR>");
        output.println(uncertifiedFacts.size() + " records listed.");
	}
	
	private class IndividualComparator implements Comparator<Individual> {

	    public int compare (Individual i1, Individual i2) {
	        return i1.compareTo(i2);
	    }
	}

}