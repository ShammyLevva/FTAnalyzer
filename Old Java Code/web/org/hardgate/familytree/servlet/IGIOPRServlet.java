/*
 * IGIOPRServlet.java
 *
 * Created on 19-May-2005
 */

package org.hardgate.familytree.servlet;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;

import javax.naming.Context;
import javax.naming.InitialContext;
import javax.naming.NamingException;
import javax.servlet.ServletConfig;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.sql.DataSource;
import javax.xml.transform.Transformer;
import javax.xml.transform.TransformerException;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

import org.apache.log4j.Logger;
import org.hardgate.exceptions.BadIGIDataException;
import org.hardgate.familytree.IGISearchForm;
import org.hardgate.familytree.ParishBatch;

/**
 * 
 * @author A-Bisset
 * @version
 * 
 * @web.servlet name="IGIOPRServlet" 
 *              description="Slurps IGI for all OPR entries with a particular surname"
 * 
 * @web.servlet-mapping url-pattern="/IGIOPRServlet"
 * 
 */
public class IGIOPRServlet extends HttpServlet {

    private static Logger logger = Logger.getLogger(UncertifiedFactsServlet.class);
    private static final long serialVersionUID = 0;
    private String surname;
    private String country;
    private String region;
    private String parish;
    private String xmlFile;
    private ArrayList<ParishBatch> batches;
    
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
        surname = request.getParameter("surname");
        country = request.getParameter("country");
        region = request.getParameter("county");
        parish = request.getParameter("parish");
        
        out.println("<html>");
        out.println("<head>");
        out.println("<title>OPR data from IGI</title>");
        out.println("</head>");
        out.println("<body>");
        out.println("<H3>OPR data from IGI</H3>");
        getBatchIDs(out);
        searchIGI(out);
        outputResult(out);
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
        return "Servlet to get OPR records from IGI.";
    }
    
    private void getBatchIDs(PrintWriter out) {
        batches = new ArrayList<ParishBatch>();
        try {
            Context lContext = new InitialContext();
            DataSource ds = (DataSource) lContext.lookup("java:/FamilyTree");
            Connection conn = ds.getConnection();
            Statement stmt = conn.createStatement();
            ResultSet rsBatch = stmt.executeQuery(getSQLstring());
            // we now have a list of BatchIDs
            while(rsBatch.next()) {
            	ParishBatch pb = 
            		new ParishBatch(rsBatch.getString("IGIBatchID"),
            						rsBatch.getString("parishID"));
            	batches.add(pb);
            }
            rsBatch.close();
            for(ParishBatch batch : batches) {
            	try {
	            	ResultSet rs = 
	            		stmt.executeQuery(getSQLstring(batch.getBatch()));
	            	if(rs.next()) {
		                batch.setParish(rs.getString("parishName"));
		                batch.setStartYear(rs.getString("startYear"));
		                batch.setEndYear(rs.getString("endYear"));
		                batch.setComments(rs.getString("comments"));
	            	}
	                rs.close();
            	} catch (SQLException e) {
            		out.println("<br>Error trying to fetch parish details " + e.getMessage());
            	}
            }
            stmt.close();
            conn.close();
        } catch(NamingException e) {
            out.println("<br>Error trying to connect to database " + e.getMessage());
        } catch (SQLException e) {
            out.println("<br>Error trying to fetch batch details " + e.getMessage());
        } 

    }
    
    private String getSQLstring() {
        StringBuilder output = new StringBuilder();
        output.append("select distinct b.IGIBatchID,b.parishID ");
        output.append("from parishes p,IGIBatch b ");
        output.append("where b.parishID=left(p.parishID,3) and country='");
        output.append(country);
        output.append("' and region='");
        output.append(region);
        if (parish.equals("all"))
            output.append("'");
        else {
            output.append("' and parishname='");
            output.append(parish);
            output.append("'");
        }
        output.append(" order by p.parishname,b.batchtype,b.startYear");
        return output.toString();
    }
    
    private String getSQLstring(String batch) {
        StringBuilder output = new StringBuilder();
        output.append("select p.parishname,b.startYear,b.endYear,b.comments ");
        output.append("from parishes p,IGIBatch b ");
        output.append("where b.IGIBatchID='" + batch);
        output.append("' and b.parishID=left(p.parishID,3)");
        return output.toString();
    }
    
    private void searchIGI(PrintWriter out) {
        IGISearchForm form = new IGISearchForm();
        out.println("<B>OPR Slurp started</B><BR>");
        try {
            String path = getServletContext().getRealPath("/files") + "/";
            xmlFile = surname + "-" + country +
                        "-" + region + "-" + parish + "-OPR-Results";
            PrintWriter resultFile = 
                new PrintWriter(new FileWriter(path + xmlFile + ".xml"));
            writeResultHeader(resultFile);
            for(ParishBatch batch : batches)
                form.searchOPR(resultFile, path, out, surname, batch);
            writeResultFooter(resultFile);
        } catch (IOException e) { 
            out.println("<br> error " + e.getMessage());
        } catch (BadIGIDataException e) { 
            out.println("<br> error " + e.getMessage());
        }
        out.println("<br><b>OPR Slurp finished</b>");
    }
    
    private void outputResult(PrintWriter out) {
        out.println("<p><p>View XML file :<a href='files/" + xmlFile + ".xml'>");
        out.println(xmlFile + ".xml</a>");
        
        TransformerFactory tFactory =
              TransformerFactory.newInstance();
        String path = getServletContext().getRealPath("/");
        String stylesheet = path + "/WEB-INF/xml/IGIOPR.xsl";
        String sourceId = path + "/files/" + xmlFile + ".xml";
        File htmlFile = new File(path + "/files/" + xmlFile + ".html");
        try {
            FileOutputStream os = new FileOutputStream(htmlFile);
            Transformer transformer = 
                tFactory.newTransformer(new StreamSource(stylesheet));
            transformer.transform(
                new StreamSource(sourceId), new StreamResult(os));
            out.println("<p><p>View result file :<a href='files/" + xmlFile + ".html'>");
            out.println(xmlFile + "</a>");
        } catch (FileNotFoundException e) {
            // really bad if we have written file and its not now found
            out.println(e.getMessage());
        } catch (TransformerException e2) {
            out.println(e2.getMessage());            
        }
    }
    
    private void writeResultHeader(PrintWriter resultFile) {
        resultFile.println("<?xml version='1.0' encoding='UTF-8' standalone='yes' ?>");
        resultFile.println("<!DOCTYPE IGIResults [");
        resultFile.println("<!ELEMENT IGIResults (SearchCriteria,Individual*)>");
        resultFile.println("<!ELEMENT SearchCriteria (Surname,Country,Region,Parish)>");
		resultFile.println("<!ELEMENT Surname     (#PCDATA)>");
		resultFile.println("<!ELEMENT Country     (#PCDATA)>");
		resultFile.println("<!ELEMENT Region      (#PCDATA)>");
		resultFile.println("<!ELEMENT Parish      (#PCDATA)>");
		resultFile.println("<!ELEMENT Individual  (SearchType,Batch,Parish,Name,Gender,Birth,Christening,Death,Burial,Father,Mother,Spouse,Marriage)>");
		resultFile.println("<!ELEMENT SearchType  (#PCDATA)>");
		resultFile.println("<!ELEMENT Batch       (#PCDATA)>");
		resultFile.println("<!ELEMENT Parish      (#PCDATA)>");
        resultFile.println("<!ELEMENT Name        (#PCDATA)>");
        resultFile.println("<!ELEMENT Gender      (#PCDATA)>");
        resultFile.println("<!ELEMENT Birth       (#PCDATA)>");
        resultFile.println("<!ELEMENT Christening (#PCDATA)>");
        resultFile.println("<!ELEMENT Death       (#PCDATA)>");
        resultFile.println("<!ELEMENT Burial      (#PCDATA)>");
        resultFile.println("<!ELEMENT Father      (#PCDATA)>");
        resultFile.println("<!ELEMENT Mother      (#PCDATA)>");
        resultFile.println("<!ELEMENT Spouse      (#PCDATA)>");
        resultFile.println("<!ELEMENT Marriage    (#PCDATA)>");
        resultFile.println("]>");
    
        resultFile.println("<IGIResults>");
        resultFile.println("<SearchCriteria>");
        resultFile.println("<Surname>");
        resultFile.print(surname);
        resultFile.print("</Surname>");
        resultFile.println("<Country>");
        resultFile.print(country);
        resultFile.print("</Country>");
        resultFile.println("<Region>");
        resultFile.print(region);
        resultFile.print("</Region>");
        resultFile.println("<Parish>");
        resultFile.print(parish);
        resultFile.print("</Parish>");
        resultFile.println("</SearchCriteria>");
    }

    private void writeResultFooter(PrintWriter resultFile) {
        resultFile.write("</IGIResults>");
        resultFile.close();
    }
}