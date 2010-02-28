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
 * @web.servlet name="LooseDeathsServlet" 
 * 				description="Lists all death dates that could be tightened up"
 * 
 * @web.servlet-mapping url-pattern="/LooseDeathsServlet"
 * 
 */
public class LooseDeathsServlet extends HttpServlet {

	private static Logger logger = Logger.getLogger(LooseDeathsServlet.class);
	private static final long serialVersionUID = 0;
	private int memberNumber = 0;

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

		out.println("<html>");
		out.println("<head>");
		out.println("<title>Individuals with loose death details</title>");
		out.println("</head>");
		out.println("<body>");
		out.println("<H3>Individuals with loose death details</H3>");
		getLooseDeaths(out);
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

	private void getLooseDeaths (PrintWriter output) {
        Client client = Client.getInstance();
        List<Individual> looseDeaths = client.getLooseDeaths(memberNumber);
        Collections.sort(looseDeaths, new IndividualComparator());
        output.print("<table><tr><th>Name</th><th>Current death date</th>");
        output.println("<th>Better death date</th></tr>");
        for (Individual indiv : looseDeaths) {
            output.print("<tr><td><B>");
            output.print(indiv.getName());
            output.print("</b>&nbsp;</td><td>");
            output.print(indiv.getPreferredFactDate(Fact.DEATH).getDateString().toLowerCase());
            output.print("&nbsp;</td><td>");
            output.print(indiv.getPreferredFactDate(Fact.LOOSEDEATH).getDateString().toLowerCase());
            output.println("</td></tr>");
        }
        output.println("</table><P><HR>");
        output.println(looseDeaths.size() + " records listed.");
	}
	
	private class IndividualComparator implements Comparator<Individual> {

	    public int compare (Individual i1, Individual i2) {
	        return i1.compareTo(i2);
	    }
	}

}