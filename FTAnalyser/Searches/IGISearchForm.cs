using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Cache;
using System.Windows.Forms;
using System.Collections.Specialized;

namespace FTAnalyzer
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

        private Dictionary<string,string> parameters;
        private TextWriter resultFile;
        private RichTextBox rtbOutput;
        	
        private static readonly string NOMATCHES = "<strong>International Genealogical Index / British Isles</strong> (No Matches)";
        private static readonly string NOMATCHES2 = "<strong><span id='searchPageTitle'>International Genealogical Index / British Isles</span></strong> (No Matches)";
        private static readonly string SERVERERROR = "java.io.IOException: Server returned HTTP response code";
        private static readonly string SERVERUNAVAILABLE = "Search is unavailable due to maintenance";
        private static readonly string MISSINGNAME = "You must enter at least a first or last name, or you must enter a father's full name and at least a mother's first name.";
	    private static readonly string MISSINGNAME2 = "Enter at least your deceased ancestor's first name or last name and the region";
        private static readonly string MISSINGNAME3 = "If you enter a last name without a first name, you must either <b>not enter</b> parent or spouse names, a year, or you <b>must enter</b> a batch number or a film number.";
//        private static readonly string INDIVIDUALRECORD = "igi/individual_record.asp";
        private static readonly FactDate IGIMAX = new FactDate("31 DEC 1874");
        public const int MARRIAGESEARCH = 1;
        public const int CHILDRENSEARCH = 2;
        
        public IGISearchForm(RichTextBox rtb) {
            rtbOutput = rtb;
            Initialise();
        }

        private void Initialise() {
            parameters = new Dictionary<string,string>();
            parameters.Add(FATHERS_FIRST_NAME, "");
            parameters.Add(FATHERS_LAST_NAME, "");
            parameters.Add(FIRST_NAME, "");
            parameters.Add(LAST_NAME, "");
            parameters.Add(MOTHERS_FIRST_NAME, "");
            parameters.Add(MOTHERS_LAST_NAME, "");
            parameters.Add(SPOUSES_FIRST_NAME, "");
            parameters.Add(SPOUSES_LAST_NAME, "");
            parameters.Add(FROM_DATE, "");
            parameters.Add(BATCH_NUMBER, "");
            parameters.Add(SERIAL_NUMBER, "");
            parameters.Add(FILM_NUMBER, "");
            parameters.Add(EXACT_MATCH, "");
            parameters.Add(DATE_RANGE, "0");
            parameters.Add(EVENT_INDEX, "0");
            parameters.Add(COUNTRY, "2");
            parameters.Add(SHIRE, "Scot");
            parameters.Add(PARISH, "");
            parameters.Add("date_range_index", "0");
            parameters.Add("regionfriendly", "British Isles");
            parameters.Add("juris1friendly", FactLocation.SCOTLAND);
            parameters.Add("juris2friendly", "All Counties");
            parameters.Add("LDS", "1");
            parameters.Add("batch_set", "");
        }
        
        private void setCountry(string country) {
    	    if (country == FactLocation.ENGLAND) {
	            parameters[SHIRE] = "Engl";
	            parameters["juris1friendly"] = FactLocation.ENGLAND;
    	    } else {
	            parameters[SHIRE] = "Scot";
	            parameters["juris1friendly"] = FactLocation.SCOTLAND;
    	    }
        }
        
        public NameValueCollection getEncodedParameters () {
            NameValueCollection result = new NameValueCollection();
            foreach(var entry in parameters)
            {
                result.Add(entry.Key,entry.Value);
            }
            return result;
        }
        
        public void setParameter(string key, string value) {
            string oldvalue;
            string setValue = value.Replace('?',' ').Trim();
            if (parameters.TryGetValue(key, out oldvalue))
                parameters[key] = value;
            else
                parameters.Add(key, setValue);
        }

        public string FetchIGIDataFromWebsite () {
            /* c# stuff
            bool connectedToUrl = false;
                
            HttpWebRequest webreq = (HttpWebRequest)WebRequest.Create("http://www.familysearch.org/Eng/Search/customsearchresults.asp");
            webreq.Credentials = CredentialCache.DefaultCredentials;
            if (webreq != null)
            {
                using (WebResponse res = webreq.GetResponse())
                {
                    connectedToUrl = processResponseCode(res);
                }
            }
            */
            /* TODO: URL Stuff
               // URL connection channel.
               URLConnection urlConn = url.openConnection();
               urlConn.setDoInput(true);
               urlConn.setDoOutput(true);
               urlConn.setUseCaches(false);
               urlConn.setRequestProperty("Content-Type", "application/x-www-form-urlencoded");
    	        
               // Send POST output.
               DataOutputStream request = new DataOutputStream(urlConn.getOutputStream());
    	        
               request.writeBytes(getEncodedParameters());
               request.flush();
               request.close();
    	        
               // Get response data.
               BufferedReader input = new BufferedReader(new InputStreamReader(urlConn.getInputStream()));

              StringBuilder str = new StringBuilder();
               string line;
               while ((line = input.readLine()) != null) {
                   str.Append(line);
                   str.Append("\n");
               }
               input.close();
 */
           try {
               Utilities.WebRequestWrapper web = new Utilities.WebRequestWrapper();
               NameValueCollection parameters = getEncodedParameters();
               string result = web.FetchResult("http://www.familysearch.org/Eng/Search/customsearchresults.asp", parameters);
               StringBuilder htmlText = new StringBuilder(result);
               fixBaseURL(htmlText);
               return htmlText.ToString();

           } catch (IOException e) {
               return "<html><body>Error performing search:\n<p>" +
                       e.ToString() + "</p></body></html>";
           }
       }
        
       private void fixBaseURL(StringBuilder str) {
           int head = str.ToString().IndexOf("<head>");
           if (head != -1) {
               str.Insert(head + 6, "<base href=\"http://www.familysearch.org/Eng/Search/customsearchresults.asp\">");
           }
       }
        
       public void FetchIGIDataAndWriteResult(string filename) {
           try {
               string str = FetchIGIDataFromWebsite();
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
               if (str.IndexOf(NOMATCHES) == -1 && str.IndexOf(NOMATCHES2) == -1) {
                   TextWriter output = new StreamWriter(filename);
                   output.WriteLine(str); 
                   output.Close();
                   //rtbOutput.AppendText("\nResults File written to " + filename);
               }
          } catch (IOException e) {
               throw new BadIGIDataException("IO Error " + e.Message);
          }
       }

       public void parseResults(int searchType, ParishBatch pb, TextWriter output, string filename, string outFile)
       {
/* TODO: HTML parse stuff
           NodeFilter filter = new NodeClassFilter(LinkTag.Class);
           Queue<IGIResult> queue = new Queue<IGIResult>();
           try {
               Parser parser = new Parser(filename);
               NodeList list = parser.extractAllNodesThatMatch (filter);
               for (int i = 0; i < list.Count; i++) {
                   LinkTag link = (LinkTag) list.elementAt(i);
                   if (link.getLink().IndexOf(INDIVIDUALRECORD) != -1) {
                       // this is a result link so add it to the fetch queue
                       queue.Enqueue(new IGIResult(searchType, pb, link));
                   }
               }
               fetchResults(output, queue, outFile);
           } catch (Exception e) {
               string value;
               parameters.TryGetValue(LAST_NAME, out value);
               output.WriteLine(" - no results for " + value + " found");
           }
*/
        }
        
        /*
         * passed a list of URLs to visit containing the results 
         */
        public void fetchResults(IGIResultWriter output, Queue<IGIResult> queue, string outFile) {
            int counter = 0;
            int errorCounter = 0;
            IGIResult result = null;
            bool exceptionFlag = false;
            while(queue.Count > 0) {
                try {
                    result = queue.Dequeue();
                    HttpWebRequest url = result.URL;
/* TODO: URL Stuff
                    URLConnection urlConn = url.openConnection();
                    urlConn.setDoOutput(true);
                    urlConn.setUseCaches(false);

                    // Get response data.
                    BufferedReader input = new BufferedReader(new InputStreamReader(urlConn.getInputStream()));
                    string line;
                    StringBuilder str = new StringBuilder();
                    while ((line = input.readLine()) != null) {
                        str.Append(line);
                        str.Append("\n");
                    }
                    input.close();
                    fixBaseURL(str);
                    counter++;
                    string filename = outFile + "-file" + counter + ".html";
                    TextWriter pw = new StreamWriter(filename);
                    pw.WriteLine(str); 
                    pw.Close();
                    processResult(result, filename);
 */
                    output.writeResult(result);
                    exceptionFlag = false;
                } catch (IOException e) {
                    rtbOutput.AppendText("Error performing search:\n" + e.Message + "\n");
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
            string value;
            parameters.TryGetValue(LAST_NAME, out value);
            if (counter >1)
                rtbOutput.AppendText(" - found " + counter + " results for " + value);
            else if (counter == 1)
                rtbOutput.AppendText(" - found " + counter + " result for " + value);
            else
                rtbOutput.AppendText(" - no results for " + value + " found");
            if (errorCounter == 1)
                rtbOutput.AppendText(" and one error.\n");
            else if(errorCounter > 1)
                rtbOutput.AppendText(" and " + errorCounter + " errors.\n");
        }
 /* TODO: Convert     
        public void processResult(IGIResult result, string filename) {
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
                        result.Gender = col.getStringText();
                    else {
                        string classTD = col.getAttribute("class").toUpperCase();
                        if (classTD.Equals("INDIVIDUALLABEL")) {
                            TableColumn colValue = (TableColumn) list.elementAt(i+1);
                            classTD = colValue.getAttribute("class").toUpperCase();
                            if (classTD.Equals("INDIVIDUALDATA")) { 
                                TableColumn colPlace = (TableColumn) list.elementAt(i+2);
                                classTD = colPlace.getAttribute("class").toUpperCase();
                                if(classTD.Equals("INDIVIDUALDATA")) {
                                    // store the value as date and place
                                    string value = colValue.getStringText() + " " +
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
                rtbOutput.AppendText(e.StackTrace + "\n");
            }
        }
 */       
        
        public void searchOPR(TextWriter resultFile, string dirname, TextWriter output, string surname, ParishBatch parishBatch) 
        {
            int searchType;
            this.resultFile = resultFile;
            string batch = parishBatch.Batch;
            if(batch.Substring(0,1).Equals("M"))
                searchType = MARRIAGESEARCH;
            else if(batch.Substring(0,1).Equals("C"))
                searchType = CHILDRENSEARCH;
            else
                throw new BadIGIDataException("Invalid Batch number format " + batch);
            Initialise();
            setParameter(LAST_NAME, surname);
            setParameter(BATCH_NUMBER, batch);
            string filename = dirname + "\\" + surname + "-" + batch + ".html";
            string outFile = dirname + surname + "-" + batch;
            try {
                output.WriteLine("<br>Started work on batch :" + batch + " :" +
                        parishBatch.ParishID + "- " + parishBatch.Parish + 
                        " " + parishBatch.StartYear + "-" + parishBatch.EndYear +
                        " " + parishBatch.Comments);
                output.Flush();
                FetchIGIDataAndWriteResult(filename);
                parseResults(searchType, parishBatch, output, filename, outFile);
                output.Flush();
            } catch (BadIGIDataException e) { 
                output.WriteLine("error " + e.Message);
            }
        }

        public void SearchIGI(Family family, string dirname, int searchType) {
            if (family != null) {
			    string filename;
                if(searchType == MARRIAGESEARCH)
                    filename = dirname + "\\marriages" + family.FamilyGed + ".html";
                else
                    filename = dirname + "\\children" + family.FamilyGed + ".html";
                if(!File.Exists(filename)) // don't bother processing if file already exists.
                {
                    Individual husband = family.Husband;
                    Individual wife = family.Wife;
                    if (husband != null && wife != null &&
                        family.getPreferredFact(Fact.IGISEARCH) == null &&
                        family.getPreferredFact(Fact.CHILDLESS) == null)
                    {
                        // only bother to search if we have a husband and wife family
                        // and we havent done an IGISEARCH and we havent marked them
                        // as a childless family
                        Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
                        if (marriage == null)
                            marriage = new Fact(Fact.MARRIAGE, FactDate.UNKNOWN_DATE);
                        FactDate marriageDate = marriage.FactDate;
                        if (!marriageDate.isAfter(IGIMAX))
                        {
                            // proceed if marriage date within IGI Range
                            Initialise();
                            setCountry(marriage.Country);
                            if (searchType == MARRIAGESEARCH)
                            {
                                if (!marriageDate.isExact())
                                {
                                    setParameter(FIRST_NAME, husband.Forename);
                                    setParameter(LAST_NAME, husband.Surname);
                                    setParameter(SPOUSES_FIRST_NAME, wife.Forename);
                                    setParameter(SPOUSES_LAST_NAME, wife.Surname);
                                    try
                                    {
                                        FetchIGIDataAndWriteResult(filename);
                                    }
                                    catch (BadIGIDataException)
                                    {
                                        setParameter(FIRST_NAME, wife.Forename);
                                        setParameter(LAST_NAME, wife.Surname);
                                        setParameter(SPOUSES_FIRST_NAME, husband.Forename);
                                        setParameter(SPOUSES_LAST_NAME, husband.Surname);
                                        try
                                        {
                                            FetchIGIDataAndWriteResult(filename);
                                        }
                                        catch (BadIGIDataException e2)
                                        {
                                            rtbOutput.AppendText("error " + e2.Message);
                                        }
                                    }
                                }
                            }
                            else if (searchType == CHILDRENSEARCH)
                            {
                                setParameter(FATHERS_FIRST_NAME, husband.Forename);
                                setParameter(FATHERS_LAST_NAME, husband.Surname);
                                setParameter(MOTHERS_FIRST_NAME, wife.Forename);
                                if (!marriage.Country.Equals(FactLocation.ENGLAND))
                                    setParameter(MOTHERS_LAST_NAME, wife.Surname);
                                try
                                {
                                    FetchIGIDataAndWriteResult(filename);
                                }
                                catch (BadIGIDataException e)
                                {
                                    rtbOutput.AppendText("error " + e.Message);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}