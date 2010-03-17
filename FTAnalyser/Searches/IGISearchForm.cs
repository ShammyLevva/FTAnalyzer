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
        private string defaultCountry = FactLocation.SCOTLAND;
        private int resultCount = 0;
        	
        private static readonly string NOMATCHES1 = "<strong>International Genealogical Index / British Isles</strong> (No Matches)";
        private static readonly string NOMATCHES2 = "<strong><span id='searchPageTitle'>International Genealogical Index / British Isles</span></strong> (No Matches)";
        private static readonly string NOMATCHES3 = "<strong>International Genealogical Index / North America</strong> (No Matches)";
        private static readonly string NOMATCHES4 = "<strong><span id='searchPageTitle'>International Genealogical Index / North America</span></strong> (No Matches)";
        private static readonly string SERVERERROR = "java.io.IOException: Server returned HTTP response code";
        private static readonly string SERVERUNAVAILABLE = "Search is unavailable due to maintenance";
        private static readonly string MISSINGNAME1 = "You must enter at least a first or last name, or you must enter a father's full name and at least a mother's first name.";
        private static readonly string MISSINGNAME2 = "Enter at least your deceased ancestor's first name or last name";
        private static readonly string MISSINGNAME3 = "If you enter a last name without a first name, you must either <b>not enter</b> parent or spouse names, a year, or you <b>must enter</b> a batch number or a film number.";
//        private static readonly string INDIVIDUALRECORD = "igi/individual_record.asp";
        private static readonly FactDate IGIMAX = new FactDate("31 DEC 1874");
        private static readonly FactDate IGIPARENTBIRTHMAX = new FactDate("31 DEC 1860"); //if parents born after than then children are born after IGIMAX
        public const int MARRIAGESEARCH = 1;
        public const int CHILDRENSEARCH = 2;
        
        public IGISearchForm(RichTextBox rtb, string defaultCountry) {
            rtbOutput = rtb;
            this.defaultCountry = defaultCountry;
            this.resultCount = 0;
            Initialise();
        }

        public int ResultCount { get { return resultCount; } }

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
            parameters.Add(PARISH, "");
            parameters.Add("date_range_index", "0");
            parameters.Add("LDS", "1");
            parameters.Add("batch_set", "");
        }
        
        private void SetCountryParameters(string country)
        {
            if (country == FactLocation.SCOTLAND || country == FactLocation.ENGLAND || country == FactLocation.WALES)
            {
                setParameter(COUNTRY, "2");
                setParameter(SHIRE, country.Substring(0, 4));
                setParameter("regionfriendly", "British Isles");
                setParameter("juris1friendly", country);
                setParameter("juris2friendly", "All Counties");
            }
            else if (country == FactLocation.UK)
            {
                setParameter(COUNTRY, "2");
                setParameter(SHIRE, "");
                setParameter("regionfriendly", "British Isles");
                setParameter("juris1friendly", "All Countries");
                setParameter("juris2friendly", "All Counties");
            }
            else if (country == FactLocation.CANADA)
            {
                setParameter(COUNTRY, "11");
                setParameter(SHIRE, "CAN");
                setParameter("regionfriendly", "North America");
                setParameter("juris1friendly", "Canada");
                setParameter("juris2friendly", "All Provinces");
            }
            else if (country == FactLocation.USA)
            {
                setParameter(COUNTRY, "11");
                setParameter(SHIRE, "US");
                setParameter("regionfriendly", "North America");
                setParameter("juris1friendly", "United States");
                setParameter("juris2friendly", "All States");
            }
        }

        private string SetCountry(Individual husband, Individual wife, Fact marriage) {
            string country = string.Empty;
            if (marriage != null && marriage.Country.Length > 0)
                country = marriage.Country;
            else if (husband.BestLocation != null && husband.BestLocation.Country.Length > 0)
                country = husband.BestLocation.Country;
            else if (wife.BestLocation != null && wife.BestLocation.Country.Length > 0)
                country = wife.BestLocation.Country;
            else
                country = defaultCountry;
            if (country != FactLocation.SCOTLAND && country != FactLocation.ENGLAND && country != FactLocation.WALES &&
                country != FactLocation.CANADA && country != FactLocation.USA)
            {   // if we have got a random text for country field then use the default country.
                rtbOutput.AppendText("Country '" + country + "' not recognised/supported. Trying '" + defaultCountry + "' instead.\n");
                country = defaultCountry;
            }
            SetCountryParameters(country);
            return country;
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
            if (parameters.ContainsKey(key))
                parameters[key] = value;
            else
                parameters.Add(key, value);
        }

        public string verifyName(string name)
        {
            string value = name.Replace('?', ' ').Trim();
            if (value.Equals("UNKNOWN"))
            {
                value = string.Empty;
            }
            return value;
        }

       private void fixBaseURL(StringBuilder str) {
           int head = str.ToString().IndexOf("<head>");
           if (head != -1) {
               str.Insert(head + 6, "<base href=\"http://www.familysearch.org/Eng/Search/customsearchresults.asp\">");
           }
       }
        
       #region Unused/Still to be done

       public string FetchIGIDataFromWebsite()
       {
           try
           {
               Utilities.WebRequestWrapper web = new Utilities.WebRequestWrapper();
               NameValueCollection parameters = getEncodedParameters();
               string result = web.FetchResult("http://www.familysearch.org/Eng/Search/customsearchresults.asp", parameters);
               StringBuilder htmlText = new StringBuilder(result);
               fixBaseURL(htmlText);
               return htmlText.ToString();
           }
           catch (IOException e)
           {
               return "<html><body>Error performing search:\n<p>" +
                       e.ToString() + "</p></body></html>";
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

#endregion

        public void SearchIGI(Family family, string dirname, int searchType) {
            if (family != null) {
			    string filename;
                if (searchType == MARRIAGESEARCH)
                    filename = dirname + "\\" + family.MarriageFilename;
                else
                    filename = dirname + "\\" + family.ChildrenFilename;
                if(!File.Exists(filename)) // don't bother processing if file already exists.
                {
                    if (family.getPreferredFact(Fact.IGISEARCH) == null && family.Husband != null && family.Wife != null)
                    {   // or we have already flagged marriage fact as having been searched
                        // or either the husband or wife is not present
                        if(searchType == CHILDRENSEARCH)
                            ChildrenSearch(family, filename);
                        else
                            MarriageSearch(family, filename);
                    }
                }
            }
        }

        public void MarriageSearch(Family family, string filename)
        {
            Individual husband = family.Husband;
            Individual wife = family.Wife;

            Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
            if (marriage == null)
                marriage = new Fact(Fact.MARRIAGE, FactDate.UNKNOWN_DATE);
            FactDate marriageDate = marriage.FactDate;
            if (!marriageDate.isAfter(IGIMAX) && husband.BirthDate.isBefore(IGIMAX) && wife.BirthDate.isBefore(IGIMAX))
            {
                // proceed if marriage date within IGI Range and both were alive before IGI max date
                Initialise();
                SetCountry(husband, wife, marriage);
                if (!marriageDate.isExact())
                    SearchForMarriage(husband, wife, filename);   
            }
        }

        public void ChildrenSearch(Family family, string filename)
        {
            if (family.getPreferredFact(Fact.CHILDLESS) == null)
            {
                Individual husband = family.Husband;
                Individual wife = family.Wife;
                if (husband.BirthDate.StartDate < IGIPARENTBIRTHMAX.StartDate && wife.BirthDate.StartDate < IGIPARENTBIRTHMAX.StartDate)
                {
                    Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
                    string country = SetCountry(husband, wife, marriage);
                    SearchForChildren(husband, wife, country, filename);
                }
            }
        }

        public void SearchForMarriage(Individual i1, Individual i2, string filename)
        {
            string forename1 = verifyName(i1.Forename);
            string forename2 = verifyName(i2.Forename);
            string surname1 = verifyName(i1.Surname);
            string surname2 = verifyName(i2.Surname);
            if (forename1 == string.Empty || surname1 == string.Empty)
            {
                // if either forename or surname missing then try spouse first
                // if spouse doesn't have both forename & surname then dont search
                // ie: only search if one partner has full name
                if (forename2 != string.Empty && surname2 != string.Empty)
                {
                    setParameter(FIRST_NAME, forename2);
                    setParameter(LAST_NAME, surname2);
                    setParameter(SPOUSES_FIRST_NAME, forename1);
                    setParameter(SPOUSES_LAST_NAME, surname1);
                    FetchIGIDataAndWriteResult(filename);
                }
            } else {
                setParameter(FIRST_NAME, forename1);
                setParameter(LAST_NAME, surname1);
                setParameter(SPOUSES_FIRST_NAME, forename2);
                setParameter(SPOUSES_LAST_NAME, surname2);
                FetchIGIDataAndWriteResult(filename);
            }
        }

        public void SearchForChildren(Individual husband, Individual wife, string country, string filename)
        {
            string husbandForename = verifyName(husband.Forename);
            string husbandSurname = verifyName(husband.Surname);
            string wifeForename = verifyName(wife.Forename);
            string wifeSurname = verifyName(wife.Surname);
            if (husbandForename != string.Empty && husbandSurname != string.Empty && wifeForename != string.Empty)
            {
                setParameter(FATHERS_FIRST_NAME, husbandForename);
                setParameter(FATHERS_LAST_NAME, husbandSurname);
                setParameter(MOTHERS_FIRST_NAME, wifeForename);
                if(country.Equals(FactLocation.SCOTLAND))
                    setParameter(MOTHERS_LAST_NAME, wifeSurname);
                FetchIGIDataAndWriteResult(filename);
            }
        }

        public void FetchIGIDataAndWriteResult(string filename)
        {
            try
            {
                string str = FetchIGIDataFromWebsite();
                // only output if no matches string not found
                if (str.IndexOf(SERVERUNAVAILABLE) != -1)
                {
                    throw new BadIGIDataException("Server Unavailable");
                }
                if (str.IndexOf(SERVERERROR) != -1)
                {
                    throw new BadIGIDataException("Server Error 504");
                }
                if (str.IndexOf(MISSINGNAME1) != -1 ||
                    str.IndexOf(MISSINGNAME2) != -1 ||
                    str.IndexOf(MISSINGNAME3) != -1)
                {
                    // now check if it objects to name
                    throw new BadIGIDataException("Missing Name");
                }
                if (str.IndexOf(NOMATCHES1) == -1 && str.IndexOf(NOMATCHES2) == -1 &&
                    str.IndexOf(NOMATCHES3) == -1 && str.IndexOf(NOMATCHES4) == -1)
                {
                    TextWriter output = new StreamWriter(filename);
                    output.WriteLine(str);
                    output.Close();
                    rtbOutput.AppendText("Results File written to " + filename + "\n");
                    resultCount++;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error " + e.Message);
            }
        }
    }
}