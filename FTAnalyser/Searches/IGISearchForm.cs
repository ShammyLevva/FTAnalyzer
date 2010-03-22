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
    		    REGION = "region",
    		    COUNTRY = "juris1",
    		    SHIRE = "juris2";

        private Dictionary<string,string> parameters;
        private TextWriter resultFile;
        private RichTextBox rtbOutput;
        private FactLocation defaultLocation = new FactLocation(FactLocation.SCOTLAND);
        private IGILocation defLoc = null;
        private int level;
        private int resultCount = 0;
        private int relationTypes = Individual.UNSET;
        	
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
        
        public IGISearchForm(RichTextBox rtb, string defaultCountry, int level, int relationTypes) {
            rtbOutput = rtb;
            this.defaultLocation = new FactLocation(defaultCountry);
            this.defLoc = IGILocation.Adapt(this.defaultLocation, level);
            this.level = level;
            this.resultCount = 0;
            this.relationTypes = relationTypes;
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
            parameters.Add(SHIRE, "");
            parameters.Add("date_range_index", "0");
            parameters.Add("LDS", "1");
            parameters.Add("batch_set", "");
            parameters.Add("regionfriendly", "");
            parameters.Add("juris1friendly", "");
            parameters.Add("juris2friendly", "");
        }
        
        private void SetLocationParameters(FactLocation location)
        {
            IGILocation loc = IGILocation.Adapt(location, level);
            if (loc != null)
            {
                setParameter(REGION, loc.Region);
                setParameter(COUNTRY, loc.Juris1);
                setParameter(SHIRE, loc.Juris2);
                Console.WriteLine(location.ToString() + " is '" + loc.Region + ", " + loc.Juris1 + ", " + loc.Juris2 + "'");
            }
            else
            {
                setParameter(REGION, defLoc.Region);
                setParameter(COUNTRY, defLoc.Juris1);
                setParameter(SHIRE, defLoc.Juris2);
                Console.WriteLine(location.ToString() + " using default '" + defLoc.Region + ", " + defLoc.Juris1 + ", " + defLoc.Juris2 + "'");
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
                if (family.getPreferredFact(Fact.IGISEARCH) == null && family.Husband != null && family.Wife != null)
                {   // or we have already flagged marriage fact as having been searched
                    // or either the husband or wife is not present
                    if(searchType == CHILDRENSEARCH)
                        ChildrenSearch(family, dirname);
                    else
                        MarriageSearch(family, dirname);
                }
            }
        }

        public void MarriageSearch(Family family, string dirname)
        {
            Individual husband = family.Husband;
            Individual wife = family.Wife;
            if (validRelationType(husband, wife))
            {
                string filename = dirname + "\\" + family.MarriageFilename;
                if (!File.Exists(filename))
                {
                    Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
                    if (marriage == null)
                        marriage = new Fact(Fact.MARRIAGE, FactDate.UNKNOWN_DATE);
                    FactDate marriageDate = marriage.FactDate;
                    if (!marriageDate.isAfter(IGIMAX) && husband.BirthDate.isBefore(IGIMAX) && wife.BirthDate.isBefore(IGIMAX))
                    {
                        // proceed if marriage date within IGI Range and both were alive before IGI max date
                        // but don't bother processing if file already exists.
                        if (!marriageDate.isExact())
                        {
                            Initialise();
                            if (SetMarriageParameters(husband, wife))
                            {
                                List<FactLocation> locations = GetLocations(husband, wife, marriage);
                                CheckIGIAtLocations(locations, filename, MARRIAGESEARCH, null);
                            }
                        }
                    }
                }
            }
        }

        private void ChildrenSearch(Family family, string dirname)
        {
            if (family.getPreferredFact(Fact.CHILDLESS) == null)
            {
                string filename = dirname + "\\" + family.ChildrenFilename;
                if (!File.Exists(filename))
                {
                    Individual husband = family.Husband;
                    Individual wife = family.Wife;
                    if (validRelationType(husband, wife))
                    {
                        if (husband.BirthDate.StartDate < IGIPARENTBIRTHMAX.StartDate && wife.BirthDate.StartDate < IGIPARENTBIRTHMAX.StartDate)
                        {
                            Fact marriage = family.getPreferredFact(Fact.MARRIAGE);
                            SearchForChildren(husband, wife, marriage, filename);
                        }
                    }
                }
            }
        }

        private bool validRelationType(Individual i1, Individual i2)
        {
            int checkInd1 = i1.RelationType & relationTypes;
            int checkInd2 = i2.RelationType & relationTypes;
            return checkInd1 != 0 || checkInd2 != 0;
        }

        private bool SetMarriageParameters(Individual i1, Individual i2)
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
                    return true;
                }
            } else {
                setParameter(FIRST_NAME, forename1);
                setParameter(LAST_NAME, surname1);
                setParameter(SPOUSES_FIRST_NAME, forename2);
                setParameter(SPOUSES_LAST_NAME, surname2);
                return true;
            }
            return false;
        }

        public void SearchForChildren(Individual husband, Individual wife, Fact marriage, string filename)
        {
            List<FactLocation> locations = GetLocations(husband, wife, marriage);
            string husbandForename = verifyName(husband.Forename);
            string husbandSurname = verifyName(husband.Surname);
            string wifeForename = verifyName(wife.Forename);
            string wifeSurname = verifyName(wife.Surname);
            if (husbandForename != string.Empty && husbandSurname != string.Empty && wifeForename != string.Empty)
            {
                setParameter(FATHERS_FIRST_NAME, husbandForename);
                setParameter(FATHERS_LAST_NAME, husbandSurname);
                setParameter(MOTHERS_FIRST_NAME, wifeForename);
                foreach (FactLocation loc in locations)
                {
                    CheckIGIAtLocations(locations, filename, CHILDRENSEARCH, wifeSurname);
                }
            }
        }

        private List<FactLocation> GetLocations(Individual i1, Individual i2, Fact marriage)
        {
            List<FactLocation> result = new List<FactLocation>();
            if (marriage != null && marriage.Location != null && marriage.Location.SupportedLocation(level))
            {
                FactLocation location = marriage.Location.getLocation(level);
                result.Add(location);
            }
            if (i1.BestLocation != null && i1.BestLocation.SupportedLocation(level))
            {
                FactLocation location = i1.BestLocation.getLocation(level);
                if (!result.Contains(location))
                    result.Add(location);
            }
            if (i2.BestLocation != null && i2.BestLocation.SupportedLocation(level))
            {
                FactLocation location = i2.BestLocation.getLocation(level);
                if (!result.Contains(location))
                    result.Add(location);
            }
            if (result.Count == 0)
            {   // if we have got a random text for country field then use the default country.
                //rtbOutput.AppendText("Country '" + country + "' not recognised/supported. Trying '" + defaultLocation + "' instead.\n");
                if (!result.Contains(defaultLocation))
                    result.Add(defaultLocation);
            }
            return result;
        }

        private void CheckIGIAtLocations(List<FactLocation> locations, string filename, int searchType, string surname)
        {
            foreach (FactLocation location in locations)
            {
                string newFilename = filename;
                SetLocationParameters(location);
                if (searchType == CHILDRENSEARCH && location.Country.Equals(FactLocation.SCOTLAND))
                    setParameter(MOTHERS_LAST_NAME, surname);
                if (level == FactLocation.REGION && parameters[SHIRE] != string.Empty)
                {
                    newFilename = filename.Substring(0, filename.Length - 5) + FamilyTree.validFilename(" (" + location.getLocation(level) + ").html");
                }
                if (!File.Exists(newFilename))
                {
                    FetchIGIDataAndWriteResult(newFilename);
                }
            }
        }

        private void FetchIGIDataAndWriteResult(string filename)
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