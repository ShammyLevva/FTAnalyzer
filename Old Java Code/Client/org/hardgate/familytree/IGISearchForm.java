/*
 * Created on 29-Dec-2004
 *
 */
package org.hardgate.familytree;

import java.io.BufferedReader;
import java.io.DataOutputStream;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStreamReader;
import java.io.PrintWriter;
import java.io.UnsupportedEncodingException;
import java.net.URL;
import java.net.URLConnection;
import java.net.URLEncoder;
import java.util.HashMap;
import java.util.Iterator;
import java.util.LinkedList;

import org.hardgate.exceptions.BadIGIDataException;
import org.hardgate.familytree.core.Fact;
import org.hardgate.familytree.core.FactDate;
import org.hardgate.familytree.core.Family;
import org.hardgate.familytree.core.Individual;
import org.hardgate.familytree.core.Location;
import org.htmlparser.NodeFilter;
import org.htmlparser.Parser;
import org.htmlparser.filters.AndFilter;
import org.htmlparser.filters.HasAttributeFilter;
import org.htmlparser.filters.NodeClassFilter;
import org.htmlparser.filters.OrFilter;
import org.htmlparser.tags.LinkTag;
import org.htmlparser.tags.TableColumn;
import org.htmlparser.util.NodeList;
import org.htmlparser.util.ParserException;

public class IGISearchForm {

    public static final String
    		FATHERS_FIRST_NAME = "fathers_first_name",
    		FATHERS_LAST_NAME = "fathers_last_name",
    		FIRST_NAME = "first_name",
    		LAST_NAME = "last_name",
    		MOTHERS_FIRST_NAME = "mothers_first_name",
    		MOTHERS_LAST_NAME = "mothers_last_name",
    		SPOUSES_FIRST_NAME = "spouses_first_name",
    		SPOUSES_LAST_NAME = "spouses_last_name",
    		FROM_DATE = "from_date",
    		BATCH_NUMBER = "batch_number",
    		SERIAL_NUMBER = "serial_number",
    		FILM_NUMBER = "film_number",
    		EXACT_MATCH = "standardize",
    		DATE_RANGE = "date_range",
    		EVENT_INDEX = "event_index",
    		COUNTRY = "region",
    		SHIRE = "juris1",
    		PARISH = "juris2";

    private HashMap<String,String> parameters;
    private PrintWriter resultFile;
    	
    private static final String NOMATCHES = "<strong>International Genealogical Index / British Isles</strong> (No Matches)";
    private static final String NOMATCHES2 = "<strong><span id='searchPageTitle'>International Genealogical Index / British Isles</span></strong> (No Matches)";
    private static final String SERVERERROR = "java.io.IOException: Server returned HTTP response code";
    private static final String SERVERUNAVAILABLE = "Search is unavailable due to maintenance";
    private static final String MISSINGNAME = "You must enter at least a first or last name, or you must enter a father's full name and at least a mother's first name.";
	private static final String MISSINGNAME2 = "Enter at least your deceased ancestor's first name or last name and the region";
    private static final String MISSINGNAME3 = "If you enter a last name without a first name, you must either <b>not enter</b> parent or spouse names, a year, or you <b>must enter</b> a batch number or a film number.";
    private static final String INDIVIDUALRECORD = "igi/individual_record.asp";
    private static final FactDate IGIMAX = new FactDate("31 DEC 1874");
    public static final int MARRIAGESEARCH = 0, CHILDRENSEARCH = 1;
    
    public IGISearchForm() {
        initialise();
    }

    private void initialise() {
        parameters = new HashMap<String,String>();
        parameters.put(FATHERS_FIRST_NAME, "");
        parameters.put(FATHERS_LAST_NAME, "");
        parameters.put(FIRST_NAME, "");
        parameters.put(LAST_NAME, "");
        parameters.put(MOTHERS_FIRST_NAME, "");
        parameters.put(MOTHERS_LAST_NAME, "");
        parameters.put(SPOUSES_FIRST_NAME, "");
        parameters.put(SPOUSES_LAST_NAME, "");
        parameters.put(FROM_DATE, "");
        parameters.put(BATCH_NUMBER, "");
        parameters.put(SERIAL_NUMBER, "");
        parameters.put(FILM_NUMBER, "");
        parameters.put(EXACT_MATCH, "");
        parameters.put(DATE_RANGE, "0");
        parameters.put(EVENT_INDEX, "0");
        parameters.put(COUNTRY, "2");
        parameters.put(SHIRE, "Scot");
        parameters.put(PARISH, "");
        parameters.put("date_range_index", "0");
        parameters.put("regionfriendly", "British Isles");
        parameters.put("juris1friendly", Location.SCOTLAND);
        parameters.put("juris2friendly", "All Counties");
        parameters.put("LDS", "1");
        parameters.put("batch_set", "");
    }
    
    private void setCountry(String country) {
    	if (country.equals(Location.ENGLAND)) {
	        parameters.put(SHIRE, "Engl");
	        parameters.put("juris1friendly", Location.ENGLAND);
    	} else {
	        parameters.put(SHIRE, "Scot");
	        parameters.put("juris1friendly", Location.SCOTLAND);
    	}
    }
    
    public String getEncodedParameters () {
        StringBuffer s = new StringBuffer();
        Iterator it = parameters.keySet().iterator();
        while (it.hasNext()) {
            String key = (String) it.next();
            String value = (String) parameters.get(key);
            s.append(key);
            s.append("=");
            try {
                s.append(URLEncoder.encode(value, "UTF-8"));
            } catch (UnsupportedEncodingException e) {
                s.append("XXX");
            }
            if (it.hasNext())
                s.append("&");
        }
        return s.toString();
    }
    
    public void setParameter(String key, String value) {
        if (parameters.get(key) != null)
            parameters.put(key, value.replace('?',' ').trim());
    }
    
    public String performSearch () {
        try {
	        URL url = new URL("http", "www.familysearch.org",
	        		"/Eng/Search/customsearchresults.asp");
	        
	        // URL connection channel.
	        URLConnection urlConn = url.openConnection();
	        urlConn.setDoInput(true);
	        urlConn.setDoOutput(true);
	        urlConn.setUseCaches(false);
	        urlConn.setRequestProperty("Content-Type",
	        		"application/x-www-form-urlencoded");
	        
	        // Send POST output.
	        DataOutputStream request = new DataOutputStream(
	                urlConn.getOutputStream());
	        
	        request.writeBytes(getEncodedParameters());
	        request.flush();
	        request.close();
	        
	        // Get response data.
	        BufferedReader input = new BufferedReader(
	                new InputStreamReader(urlConn.getInputStream()));
	        StringBuffer str = new StringBuffer();
	        String line;
	        while ((line = input.readLine()) != null) {
	            str.append(line);
	            str.append("\n");
	        }
	        input.close();
	        fixBaseURL(str);
	        return str.toString();
        } catch (IOException e) {
            return "<html><body>Error performing search:\n<p>" +
            		e.toString() + "</p></body></html>";
        }
    }
    
    private void fixBaseURL(StringBuffer str) {
        int head = str.indexOf("<head>");
        if (head != -1) {
            str.insert(head + 6, "<base href=\"http://www.familysearch.org" +
                    "/Eng/Search/customsearchresults.asp\">");
        }
    }
    
    public void writeSlurpResult(String filename) throws BadIGIDataException {
        try {
            String str = performSearch();
	        // only output if no matches string not found
            if (str.indexOf(SERVERUNAVAILABLE) != -1) {
                throw new BadIGIDataException("Server Unavailable");
            }
	        if (str.indexOf(SERVERERROR) != -1) {
                throw new BadIGIDataException("Server Error 504");
	        }
			if (str.indexOf(MISSINGNAME) != -1 || 
                str.indexOf(MISSINGNAME2) != -1 ||
                str.indexOf(MISSINGNAME3) != -1) {
                // now check if it objects to name
				throw new BadIGIDataException("Missing Name");
			}
			if (str.indexOf(NOMATCHES) == -1 && 
				str.indexOf(NOMATCHES2) == -1) {
	            PrintWriter output = new PrintWriter(new FileWriter(filename));
		        output.println(str); 
		        output.close();
//				System.out.println("\nResults File written to " + filename);
	        }
       } catch (IOException e) {
            throw new BadIGIDataException("IO Error " + e.getMessage());
       }
    }

    public void parseResults(int searchType, ParishBatch pb, PrintWriter out, 
    						 String filename, String outFile) {
        NodeFilter filter = new NodeClassFilter(LinkTag.class);
        LinkedList<IGIResult> queue = new LinkedList<IGIResult>();
        try {
            Parser parser = new Parser(filename);
            NodeList list = parser.extractAllNodesThatMatch (filter);
            for (int i = 0; i < list.size(); i++) {
                LinkTag link = (LinkTag) list.elementAt(i);
                if (link.getLink().indexOf(INDIVIDUALRECORD) != -1) {
                    // this is a result link so add it to the fetch queue
                    queue.add(new IGIResult(searchType, pb, link));
                }
            }
            fetchResults(out, queue, outFile);
        } catch (ParserException e) {
            out.println(" - no results for " + parameters.get(LAST_NAME) + " found");
        }
    }
    
    /*
     * passed a list of URLs to visit containing the results 
     */
    public void fetchResults(PrintWriter out,
            LinkedList<IGIResult> queue, String outFile) {
        int counter = 0;
        int errorCounter = 0;
        IGIResult result = null;
        boolean exceptionFlag = false;
        while(!queue.isEmpty()) {
            try {
                result = queue.remove();
                URL url = result.getURL();
                URLConnection urlConn = url.openConnection();
                urlConn.setDoOutput(true);
                urlConn.setUseCaches(false);

                // Get response data.
                BufferedReader input = new BufferedReader(
                        new InputStreamReader(urlConn.getInputStream()));
                String line;
                StringBuffer str = new StringBuffer();
                while ((line = input.readLine()) != null) {
                    str.append(line);
                    str.append("\n");
                }
                input.close();
                fixBaseURL(str);
                counter++;
                String filename = outFile + "-file" + counter + ".html";
                PrintWriter pw = new PrintWriter(new FileWriter(filename));
                pw.println(str); 
                pw.close();
                processResult(result, filename);
                writeResult(result);
                exceptionFlag = false;
            } catch (IOException e) {
                // out.println("Error performing search:\n" + e.getMessage());
            	// we got an error which is usually just a server busy
            	// so try adding to queue again
            	// may be wise to check that its not a repeating error
            	if(!exceptionFlag)
            		queue.add(result);
            	else
            		errorCounter++;
            	exceptionFlag = true;
            }
        }
        if (counter >1)
            out.println(" - found " + counter + " results for " + parameters.get(LAST_NAME));
        else if (counter == 1)
            out.println(" - found " + counter + " result for " + parameters.get(LAST_NAME));
        else
            out.println(" - no results for " + parameters.get(LAST_NAME) + " found");
        if (errorCounter == 1)
        	out.print(" and one error");
        else if(errorCounter > 1)
        	out.print(" and " + errorCounter + " errors");
    }
    
    public void processResult(IGIResult result, String filename) {
        NodeFilter filter = new NodeClassFilter(TableColumn.class);
        filter = new AndFilter(filter, new OrFilter(
                     new HasAttributeFilter ("class", "individualLabel"),
                     new OrFilter(
                             new HasAttributeFilter ("class", "individualData"),
                             new HasAttributeFilter ("class", "IndividualData"))));
        LinkedList<IGIResult> queue = new LinkedList<IGIResult>();
        try {
            Parser parser = new Parser(filename);
            NodeList list = parser.extractAllNodesThatMatch(filter);
            for (int i = 0; i < list.size(); i++) {
                TableColumn col = (TableColumn) list.elementAt(i);
                if (i == 0) 
                    result.setGender(col.getStringText());
                else {
                    String classTD = col.getAttribute("class").toUpperCase();
                    if (classTD.equals("INDIVIDUALLABEL")) {
                        TableColumn colValue = (TableColumn) list.elementAt(i+1);
                        classTD = colValue.getAttribute("class").toUpperCase();
                        if (classTD.equals("INDIVIDUALDATA")) { 
                            TableColumn colPlace = (TableColumn) list.elementAt(i+2);
                            classTD = colPlace.getAttribute("class").toUpperCase();
                            if(classTD.equals("INDIVIDUALDATA")) {
                                // store the value as date and place
                                String value = colValue.getStringText() + " " +
                                               colPlace.getStringText();
                                result.updateValue(col.getStringText(), value);
                                i+=2;
                            } else {
                                // store the value as just the text
                                result.updateValue(col.getStringText(),
                                                   colValue.getStringText());
                                i++;
                            }
                        }
                    }
                }
            }
        } catch (ParserException e) {
            e.printStackTrace();
        }
    }
    
    public void writeResult(IGIResult result) {
        resultFile.println("<Individual>");
	        resultFile.print("<SearchType>");
	        resultFile.print(result.getSearchType());
	        resultFile.println("</SearchType>");        
            resultFile.print("<Batch>");
            resultFile.print(result.getBatch());
            resultFile.println("</Batch>");        
            resultFile.print("<Parish>");
            resultFile.print(result.getParish());
            resultFile.println("</Parish>");        
            resultFile.print("<Name>");
            resultFile.print(result.getPerson());
            resultFile.println("</Name>");        
            resultFile.print("<Gender>");
            resultFile.print(result.getGender());
            resultFile.println("</Gender>");        
            resultFile.print("<Birth>");
            resultFile.print(result.getBirth());
            resultFile.println("</Birth>");        
            resultFile.print("<Christening>");
            resultFile.print(result.getChristening());
            resultFile.println("</Christening>");        
            resultFile.print("<Death>");
            resultFile.print(result.getDeath());
            resultFile.println("</Death>");        
            resultFile.print("<Burial>");
            resultFile.print(result.getBurial());
            resultFile.println("</Burial>");        
            resultFile.print("<Father>");
            resultFile.print(result.getFather());
            resultFile.println("</Father>");        
            resultFile.print("<Mother>");
            resultFile.print(result.getMother());
            resultFile.println("</Mother>");        
            resultFile.print("<Spouse>");
            resultFile.print(result.getSpouse());
            resultFile.println("</Spouse>");        
            resultFile.print("<Marriage>");
            resultFile.print(result.getMarriage());
            resultFile.println("</Marriage>");        
        resultFile.println("</Individual>");
    }
    
    public void searchOPR(PrintWriter resultFile, String dirname, PrintWriter out, 
                          String surname, ParishBatch parishBatch) 
                throws BadIGIDataException {
        int searchType;
        this.resultFile = resultFile;
        String batch = parishBatch.getBatch();
        if(batch.substring(0,1).equals("M"))
            searchType = MARRIAGESEARCH;
        else if(batch.substring(0,1).equals("C"))
            searchType = CHILDRENSEARCH;
        else
            throw new BadIGIDataException("Invalid Batch number format " + batch);
        initialise();
        setParameter(LAST_NAME, surname);
        setParameter(BATCH_NUMBER, batch);
        String filename = dirname + "/" + surname + "-" + batch + ".html";
        String outFile = dirname + surname + "-" + batch;
        try {
            out.println("<br>Started work on batch :" + batch + " :" +
                    parishBatch.getParishID() + "- " + parishBatch.getParish() + 
                    " " + parishBatch.getStartYear() + "-" + parishBatch.getEndYear() +
                    " " + parishBatch.getComments());
            out.flush();
            writeSlurpResult(filename);
            parseResults(searchType, parishBatch, out, filename, outFile);
            out.flush();
        } catch (BadIGIDataException e) { 
            out.println("error " + e.getMessage());
        }
    }

    public void searchIGI(Family family, String dirname, int searchType) {
        if (family != null) {
			String filename = dirname + family.getFamilyGed() + ".html";
            Individual husband = family.getHusband();
            Individual wife = family.getWife();
            if (husband != null && wife != null &&
                family.getPreferredFact(Fact.IGISEARCH) == null &&
                family.getPreferredFact(Fact.CHILDLESS) == null) {
                // only bother to search if we have a husband and wife family
                // and we havent done an IGISEARCH and we havent marked them
                // as a childless family
                Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
                if (marriage == null)
                	marriage = new Fact(Fact.MARRIAGE, FactDate.UNKNOWN_DATE);
                FactDate marriageDate = marriage.getFactDate();
                if (!marriageDate.isAfter(IGIMAX)) {
                    // proceed if marriage date within IGI Range
					initialise();
					setCountry(marriage.getCountry());
                    switch(searchType) {
                    case MARRIAGESEARCH :
                        if (!marriageDate.isExact()) {
			                setParameter(FIRST_NAME, husband.getForename());
			                setParameter(LAST_NAME, husband.getSurname());
			                setParameter(SPOUSES_FIRST_NAME, wife.getForename());
			                setParameter(SPOUSES_LAST_NAME, wife.getSurname());
			                try {
				                writeSlurpResult(filename);
		                    } catch (BadIGIDataException e) {
			                    setParameter(FIRST_NAME, wife.getForename());
			                    setParameter(LAST_NAME, wife.getSurname());
			                    setParameter(SPOUSES_FIRST_NAME, husband.getForename());
			                    setParameter(SPOUSES_LAST_NAME, husband.getSurname());
			                    try {
			                        writeSlurpResult(filename);
			                    } catch (BadIGIDataException e2) { 
                                    System.out.println("error " + e2.getMessage());
                                }
		                    }
                        }
			            break;
		            case CHILDRENSEARCH :
	                    setParameter(FATHERS_FIRST_NAME, husband.getForename());
	                    setParameter(FATHERS_LAST_NAME, husband.getSurname());
	                    setParameter(MOTHERS_FIRST_NAME, wife.getForename());
	                    if(! marriage.getCountry().equals(Location.ENGLAND))
	                    	setParameter(MOTHERS_LAST_NAME, wife.getSurname());
	                    try {
	                        writeSlurpResult(filename);
	                    } catch (BadIGIDataException e) { 
                            System.out.println("error " + e.getMessage());
                        }
                    }
                }
            }
        }
    }
}
