using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyser
{
    public class IGISearchForm {

        public static readonly string
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
        private TextWriter resultFile;
        	
        private static readonly string NOMATCHES = "<strong>International Genealogical Index / British Isles</strong> (No Matches)";
        private static readonly string NOMATCHES2 = "<strong><span id='searchPageTitle'>International Genealogical Index / British Isles</span></strong> (No Matches)";
        private static readonly string SERVERERROR = "java.io.IOException: Server returned HTTP response code";
        private static readonly string SERVERUNAVAILABLE = "Search is unavailable due to maintenance";
        private static readonly string MISSINGNAME = "You must enter at least a first or last name, or you must enter a father's full name and at least a mother's first name.";
	    private static readonly string MISSINGNAME2 = "Enter at least your deceased ancestor's first name or last name and the region";
        private static readonly string MISSINGNAME3 = "If you enter a last name without a first name, you must either <b>not enter</b> parent or spouse names, a year, or you <b>must enter</b> a batch number or a film number.";
        private static readonly string INDIVIDUALRECORD = "igi/individual_record.asp";
        private static readonly FactDate IGIMAX = new FactDate("31 DEC 1874");
        public static readonly const int MARRIAGESEARCH = 0, CHILDRENSEARCH = 1;
        
        public IGISearchForm() {
            initialise();
        }

        private void initialise() {
            parameters = new Dictionary<String,String>();
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
    	    if (country == Location.ENGLAND) {
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
                } catch (Exception e) {
                    s.append("XXX");
                }
                if (it.hasNext())
                    s.append("&");
            }
            return s.toString();
        }
        
        public void setParameter(String key, String value) {
            if (parameters.get(key) != null)
                parameters.put(key, value.Replace('?',' ').Trim());
        }
        
        public String performSearch () {
            try {
	            Url url = new URL("http", "www.familysearch.org",
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
            		    e.ToString() + "</p></body></html>";
            }
        }
        
        private void fixBaseURL(StringBuffer str) {
            int head = str.IndexOf("<head>");
            if (head != -1) {
                str.insert(head + 6, "<base href=\"http://www.familysearch.org" +
                        "/Eng/Search/customsearchresults.asp\">");
            }
        }
        
        public void writeSlurpResult(String filename) {
            try {
                String str = performSearch();
	            // only output if no matches string not found
                if (str.IndexOf(SERVERUNAVAILABLE) != -1) {
                    throw new BadIGIDataException("Server Unavailable");
                }
	            if (str.IndexOf(SERVERERROR) != -1) {
                    throw new BadIGIDataException("Server Error 504");
	            }
			    if (str.IndexOf(MISSINGNAME) != -1 || 
                    str.IndexOf(MISSINGNAME2) != -1 ||
                    str.IndexOf(MISSINGNAME3) != -1) {
                    // now check if it objects to name
				    throw new BadIGIDataException("Missing Name");
			    }
			    if (str.IndexOf(NOMATCHES) == -1 && 
				    str.IndexOf(NOMATCHES2) == -1) {
	                TextWriter output = new StreamWriter(filename);
		            output.WriteLine(str); 
		            output.Close();
    //				Console.WriteLine("\nResults File written to " + filename);
	            }
           } catch (IOException e) {
                throw new BadIGIDataException("IO Error " + e.Message);
           }
        }

        public void parseResults(int searchType, ParishBatch pb, TextWriter output, String filename, String outFile) {
            NodeFilter filter = new NodeClassFilter(LinkTag.Class);
            LinkedList<IGIResult> queue = new LinkedList<IGIResult>();
            try {
                Parser parser = new Parser(filename);
                NodeList list = parser.extractAllNodesThatMatch (filter);
                for (int i = 0; i < list.Count; i++) {
                    LinkTag link = (LinkTag) list.elementAt(i);
                    if (link.getLink().IndexOf(INDIVIDUALRECORD) != -1) {
                        // this is a result link so add it to the fetch queue
                        queue.add(new IGIResult(searchType, pb, link));
                    }
                }
                fetchResults(output, queue, outFile);
            } catch (Exception e) {
                output.WriteLine(" - no results for " + parameters.get(LAST_NAME) + " found");
            }
        }
        
        /*
         * passed a list of URLs to visit containing the results 
         */
        public void fetchResults(TextWriter output, Queue<IGIResult> queue, String outFile) {
            int counter = 0;
            int errorCounter = 0;
            IGIResult result = null;
            bool exceptionFlag = false;
            while(queue.Count > 0) {
                try {
                    result = queue.Dequeue();
                    URL url = result.getURL();
                    URLConnection urlConn = url.openConnection();
                    urlConn.setDoOutput(true);
                    urlConn.setUseCaches(false);

                    // Get response data.
                    BufferedReader input = new BufferedReader(new InputStreamReader(urlConn.getInputStream()));
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
                    TextWriter pw = new StreamWriter(filename);
                    pw.WriteLine(str); 
                    pw.Close();
                    processResult(result, filename);
                    writeResult(result);
                    exceptionFlag = false;
                } catch (IOException e) {
                    // output.WriteLine("Error performing search:\n" + e.getMessage());
            	    // we got an error which is usually just a server busy
            	    // so try adding to queue again
            	    // may be wise to check that its not a repeating error
            	    if(!exceptionFlag)
            		    queue.Enqueue(result);
            	    else
            		    errorCounter++;
            	    exceptionFlag = true;
                }
            }
            if (counter >1)
                output.WriteLine(" - found " + counter + " results for " + parameters.get(LAST_NAME));
            else if (counter == 1)
                output.WriteLine(" - found " + counter + " result for " + parameters.get(LAST_NAME));
            else
                output.WriteLine(" - no results for " + parameters.get(LAST_NAME) + " found");
            if (errorCounter == 1)
        	    output.Write(" and one error");
            else if(errorCounter > 1)
        	    output.Write(" and " + errorCounter + " errors");
        }
        
        public void processResult(IGIResult result, String filename) {
            NodeFilter filter = new NodeClassFilter(TableColumn.Class);
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
                        if (classTD.Equals("INDIVIDUALLABEL")) {
                            TableColumn colValue = (TableColumn) list.elementAt(i+1);
                            classTD = colValue.getAttribute("class").toUpperCase();
                            if (classTD.Equals("INDIVIDUALDATA")) { 
                                TableColumn colPlace = (TableColumn) list.elementAt(i+2);
                                classTD = colPlace.getAttribute("class").toUpperCase();
                                if(classTD.Equals("INDIVIDUALDATA")) {
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
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }
        
        public void writeResult(IGIResult result) {
            resultFile.WriteLine("<Individual>");
	            resultFile.Write("<SearchType>");
	            resultFile.Write(result.getSearchType());
	            resultFile.WriteLine("</SearchType>");        
                resultFile.Write("<Batch>");
                resultFile.Write(result.getBatch());
                resultFile.WriteLine("</Batch>");        
                resultFile.Write("<Parish>");
                resultFile.Write(result.getParish());
                resultFile.WriteLine("</Parish>");        
                resultFile.Write("<Name>");
                resultFile.Write(result.getPerson());
                resultFile.WriteLine("</Name>");        
                resultFile.Write("<Gender>");
                resultFile.Write(result.getGender());
                resultFile.WriteLine("</Gender>");        
                resultFile.Write("<Birth>");
                resultFile.Write(result.getBirth());
                resultFile.WriteLine("</Birth>");        
                resultFile.Write("<Christening>");
                resultFile.Write(result.getChristening());
                resultFile.WriteLine("</Christening>");        
                resultFile.Write("<Death>");
                resultFile.Write(result.getDeath());
                resultFile.WriteLine("</Death>");        
                resultFile.Write("<Burial>");
                resultFile.Write(result.getBurial());
                resultFile.WriteLine("</Burial>");        
                resultFile.Write("<Father>");
                resultFile.Write(result.getFather());
                resultFile.WriteLine("</Father>");        
                resultFile.Write("<Mother>");
                resultFile.Write(result.getMother());
                resultFile.WriteLine("</Mother>");        
                resultFile.Write("<Spouse>");
                resultFile.Write(result.getSpouse());
                resultFile.WriteLine("</Spouse>");        
                resultFile.Write("<Marriage>");
                resultFile.Write(result.getMarriage());
                resultFile.WriteLine("</Marriage>");        
            resultFile.WriteLine("</Individual>");
        }
        
        public void searchOPR(TextWriter resultFile, String dirname, TextWriter output, String surname, ParishBatch parishBatch) 
        {
            int searchType;
            this.resultFile = resultFile;
            String batch = parishBatch.Batch;
            if(batch.Substring(0,1).Equals("M"))
                searchType = MARRIAGESEARCH;
            else if(batch.Substring(0,1).Equals("C"))
                searchType = CHILDRENSEARCH;
            else
                throw new BadIGIDataException("Invalid Batch number format " + batch);
            initialise();
            setParameter(LAST_NAME, surname);
            setParameter(BATCH_NUMBER, batch);
            String filename = dirname + "/" + surname + "-" + batch + ".html";
            String outFile = dirname + surname + "-" + batch;
            try {
                output.WriteLine("<br>Started work on batch :" + batch + " :" +
                        parishBatch.ParishID + "- " + parishBatch.Parish + 
                        " " + parishBatch.StartYear + "-" + parishBatch.EndYear +
                        " " + parishBatch.Comments);
                output.Flush();
                writeSlurpResult(filename);
                parseResults(searchType, parishBatch, output, filename, outFile);
                output.Flush();
            } catch (BadIGIDataException e) { 
                output.WriteLine("error " + e.Message);
            }
        }

        public void searchIGI(Family family, String dirname, int searchType) {
            if (family != null) {
			    String filename = dirname + family.FamilyGed + ".html";
                Individual husband = family.Husband;
                Individual wife = family.Wife;
                if (husband != null && wife != null &&
                    family.getPreferredFact(Fact.IGISEARCH) == null &&
                    family.getPreferredFact(Fact.CHILDLESS) == null) {
                    // only bother to search if we have a husband and wife family
                    // and we havent done an IGISEARCH and we havent marked them
                    // as a childless family
                    Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
                    if (marriage == null)
                	    marriage = new Fact(Fact.MARRIAGE, FactDate.UNKNOWN_DATE);
                    FactDate marriageDate = marriage.FactDate;
                    if (!marriageDate.isAfter(IGIMAX)) {
                        // proceed if marriage date within IGI Range
					    initialise();
					    setCountry(marriage.Country);
                        switch(searchType) {
                        case MARRIAGESEARCH :
                            if (!marriageDate.isExact()) {
			                    setParameter(FIRST_NAME, husband.Forename);
			                    setParameter(LAST_NAME, husband.Surname);
			                    setParameter(SPOUSES_FIRST_NAME, wife.Forename);
			                    setParameter(SPOUSES_LAST_NAME, wife.Surname);
			                    try {
				                    writeSlurpResult(filename);
		                        } catch (BadIGIDataException e) {
			                        setParameter(FIRST_NAME, wife.Forename);
			                        setParameter(LAST_NAME, wife.Surname);
			                        setParameter(SPOUSES_FIRST_NAME, husband.Forename);
			                        setParameter(SPOUSES_LAST_NAME, husband.Surname);
			                        try {
			                            writeSlurpResult(filename);
			                        } catch (BadIGIDataException e2) { 
                                        Console.WriteLine("error " + e2.getMessage());
                                    }
		                        }
                            }
			                break;
		                case CHILDRENSEARCH :
	                        setParameter(FATHERS_FIRST_NAME, husband.Forename);
	                        setParameter(FATHERS_LAST_NAME, husband.Surname);
	                        setParameter(MOTHERS_FIRST_NAME, wife.Forename);
	                        if(! marriage.Country.Equals(Location.ENGLAND))
	                    	    setParameter(MOTHERS_LAST_NAME, wife.Surname);
	                        try {
	                            writeSlurpResult(filename);
	                        } catch (BadIGIDataException e) { 
                                Console.WriteLine("error " + e.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}