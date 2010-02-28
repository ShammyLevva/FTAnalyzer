/*
 * UploadServlet.java
 *
 * Created on 28 September 2004
 */

package org.hardgate.familytree.servlet;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.PrintWriter;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.Iterator;
import java.util.List;

import javax.servlet.ServletConfig;
import javax.servlet.ServletContext;
import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.xml.transform.Source;
import javax.xml.transform.TransformerFactory;
import javax.xml.transform.sax.SAXTransformerFactory;
import javax.xml.transform.sax.TransformerHandler;
import javax.xml.transform.stream.StreamResult;
import javax.xml.transform.stream.StreamSource;

import org.apache.commons.fileupload.DiskFileUpload;
import org.apache.commons.fileupload.FileItem;
import org.apache.commons.fileupload.FileUploadException;
import org.apache.log4j.Logger;
import org.dom4j.Document;
import org.dom4j.DocumentException;
import org.dom4j.Element;
import org.dom4j.io.SAXReader;
import org.hardgate.exceptions.GedcomTransformException;
import org.hardgate.familytree.core.Client;
import org.hardgate.familytree.core.FactSource;
import org.hardgate.familytree.core.Family;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.xml.GedcomParser;
import org.xml.sax.InputSource;

/**
 * 
 * @author A-Bisset
 * @version
 * 
 * @web.servlet name="UploadServlet" description="Uploads Gedcom to Master
 *              database"
 * 
 * @web.servlet-mapping url-pattern="/secure/UploadServlet"
 * 
 */
public class UploadServlet extends HttpServlet {

	private static Logger logger = Logger.getLogger(UploadServlet.class);
	private static final long serialVersionUID = 0;
	private int memberNumber = 0;
	private File memberFile = null;
	private String gedXmlString = null;

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

		out.println("<html>");
		out.println("<head>");
		out.println("<title>Gedcom Upload Results</title>");
		out.println("</head>");
		out.println("<body>");
		if(request.getMethod().equals("POST")
				&& request.getContentType().startsWith("multipart/form-data")) {

			int index = request.getContentType().indexOf("boundary=");
			if(index < 0) {
				out.println("can't find boundary type");
				return;
			}
			try {
				saveGedcomFile(out, request);
				transformGedcomToXML(out);
				addXMLtoDatabase(out);
			} catch (Exception e) {
				e.printStackTrace(out);
			}
		}
		out.println("<a href='../index.jsp'>Return to main page</a>");
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
		return "Servlet to upload member's Gedcom files to master Database.";
	}

	private void saveGedcomFile(PrintWriter out, HttpServletRequest request)
			throws IOException {
		memberNumber = 0;
		memberFile = null;
		try {
			// now process request
			DiskFileUpload fu = new DiskFileUpload();
			// maximum size before a FileUploadException will be thrown
			fu.setSizeMax(10000000);
			// maximum size that will be stored in memory
			fu.setSizeThreshold(4096);
			// the location for saving data that is larger than
			// getSizeThreshold()
		    ServletContext sc = getServletContext();
		    String path = sc.getRealPath("/files");
		    fu.setRepositoryPath(path);
			List fileItems = fu.parseRequest(request);
			Iterator i = fileItems.iterator();
			String member = ((FileItem) i.next()).getString();
			memberNumber = Integer.parseInt(member);
			memberFile = new File(path + "/" + memberNumber + ".ged");
			if(memberFile.exists())
				memberFile.delete();
			FileItem uploadFile = (FileItem) i.next();
			uploadFile.write(memberFile);
			out.println("<p><b>File Uploaded now updating database please wait.</b><br>");
		} catch (FileUploadException fue) {
			throw new IOException("<p>Problem uploading GEDCOM FILE - File too large</p>");
		} catch (NumberFormatException nfe) {
			throw new IOException("<p>Cannot understand the membership number given please try again</p>");
		} catch (GedcomTransformException gte) {
			memberFile.delete();
			throw new IOException("<p>The file uploaded does not appear to be a Gedcom 5.5 file</p>");
		} catch (Exception e) {
			throw new IOException("<p>File uploaded but problem writing to disk</p>");
		}
		out.flush();
	}

	private void transformGedcomToXML(PrintWriter out)
			throws GedcomTransformException {
		try {
			TransformerFactory tFactory = TransformerFactory.newInstance();
			// define input file
			FileInputStream GEDsource = new FileInputStream(memberFile);
			// define XML transfer file
			ServletContext sc = getServletContext();
		    String stylesheet = sc.getResource(
					"/WEB-INF/xml/GedcomToXml.xsl").toString();
			// define XML output file
			gedXmlString = sc.getRealPath("/files") + "/" + memberNumber + ".xml";
			File gedXml = new File(gedXmlString);
			if(gedXml.exists()) 
			    gedXml.delete();
			
			// need to add the file:/// bit as using a File object doesn't work
			StreamResult result = new StreamResult("file:///" + gedXmlString);
			
			// get content handler for parser
			Source xsltSource = new StreamSource(stylesheet);
			SAXTransformerFactory saxTransFact = (SAXTransformerFactory) tFactory;
			TransformerHandler transHand = saxTransFact
					.newTransformerHandler(xsltSource);
			// set output result file of transformer
			transHand.setResult(result);

			// Get a GEDCOM parser and set transformer as content handler
			GedcomParser gedParser = new GedcomParser();
			gedParser.setContentHandler(transHand);
			// parse file (writes to result file)
			gedParser.parse(new InputSource(GEDsource));
			out.println("<hr><P><B>Gedcom converted to XML.</B></P>");
		} catch (Exception e) {
			e.printStackTrace(out);
			out.println("<p>");
			throw new GedcomTransformException(e);
		}
	}

	private void addXMLtoDatabase(PrintWriter out) {
		// fill database with the contents of gedcom file
		try {
			SAXReader reader = new SAXReader();
			Document document = null;
			try {
			    // win32 needs a URL as file object doesn't work
			    document = reader.read(new URL ("file:///" + gedXmlString));
			} catch (DocumentException e) {
			    // try with a file object if URL failed as Linux wants file object 
				document = reader.read(new File(gedXmlString));
			}
			Element root = document.getRootElement();

			Client client = Client.getInstance();
			client.deleteAllMembersDetails(memberNumber);
			
			// First iterate through attributes of root finding all sources
		    for(Iterator i = root.elementIterator("SOUR"); i.hasNext();) { 
		    	Element element = (Element) i.next();
		    	FactSource fs = new FactSource(memberNumber,element);
		    	client.addFactSource(fs);
		    } 
		    out.println("Sources inserted into database.<BR>");
		    out.flush();

		    // now iterate through child elements of root
			// finding all individuals
			for(Iterator i = root.elementIterator("INDI"); i.hasNext();) {
				Element element = (Element) i.next();
				Individual individual = new Individual(memberNumber,element);
				client.addIndividual(individual);
			}
		    out.println("Individuals inserted into database.<BR>");
		    out.flush();
			
			// now iterate through child elements of root
			// finding all families
		    for(Iterator i = root.elementIterator("FAM"); i.hasNext();) { 
		    	Element element = (Element) i.next(); 
		    	Family family = new Family(memberNumber, element); 
		    	client.addFamily(family); 
		    } 
		    out.println("Families inserted into database.<BR>");
		    out.flush();
		    client.setRelations(memberNumber,"I0001");
		    client.printRelationCount(out, memberNumber);
	        out.println("Relations set.<br>");
	        out.flush();
		    client.setParishes(memberNumber);
	        out.println("Parishes set.<br>");
	        out.flush();
		} catch (DocumentException e) {
			e.printStackTrace(out);
		} catch (MalformedURLException e) {
			e.printStackTrace(out);
		}

		out.println("<hr><P><B>Gedcom inserted into database.</B></P>");
	}
}