using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;
using System.Threading;

namespace FTAnalyzer
{
    public class IGINewSearchForm : IGISearchForm
    {

        public static readonly string
                FATHER_GIVENNAME = "father_givenname",
                FATHER_SURNAME = "father_surname",
                GIVENNAME = "givenname",
                SURNAME = "surname",
                MOTHER_GIVENNAME = "mother_givenname",
                MOTHER_SURNAME = "mother_surname",
                SPOUSE_GIVENNAME = "spouse_givenname",
                SPOUSE_SURNAME = "spouse_surname",
                BATCH_NUMBER = "batch_number",
                FILM_NUMBER = "film_number";

        private static readonly string SERVERERROR = "java.io.IOException: Server returned HTTP response code";
        private static readonly string SERVERUNAVAILABLE = "Search is unavailable due to maintenance";

        private string queryString;
        private volatile bool navigating;
        private WebBrowser browser;

        public IGINewSearchForm(RichTextBox rtb, string defaultCountry, int level, int relationTypes, string surname, WebBrowser browser)
        {
            rtbOutput = rtb;
            this.defaultLocation = new FactLocation(defaultCountry);
            this.defLoc = IGILocation.Adapt(this.defaultLocation, level);
            this.level = level;
            this.resultCount = 0;
            this.relationTypes = relationTypes;
            this.surname = surname.ToUpper();
            this.browser = browser;
            surnameSearch = (surname.Length > 0);
            Initialise();
        }

        protected override void Initialise()
        {
            parameters = new Dictionary<string, string>();
            parameters.Add(FATHER_GIVENNAME, "");
            parameters.Add(FATHER_SURNAME, "");
            parameters.Add(GIVENNAME, "");
            parameters.Add(SURNAME, "");
            parameters.Add(MOTHER_GIVENNAME, "");
            parameters.Add(MOTHER_SURNAME, "");
            parameters.Add(SPOUSE_GIVENNAME, "");
            parameters.Add(SPOUSE_SURNAME, "");
            parameters.Add(BATCH_NUMBER, "");
            parameters.Add(FILM_NUMBER, "");
        }

        protected override void SetLocationParameters(FactLocation location)
        {
            setParameter("record_country", location.Country);
        }

        protected override void CheckIGIAtLocations(List<FactLocation> locations, string filename, int searchType, string surname)
        {
            foreach (FactLocation location in locations)
            {
                string newFilename = filename;
                SetLocationParameters(location);
                if (searchType == IGISearchForm.CHILDRENSEARCH && location.Country.Equals(FactLocation.SCOTLAND))
                    setParameter(MOTHER_SURNAME, surname);
                //if (level == FactLocation.REGION && parameters[SHIRE] != string.Empty)
                //{
                //    newFilename = filename.Substring(0, filename.Length - 5) + FamilyTree.validFilename(" (" + location.getLocation(level) + ").html");
                //}
                if (!File.Exists(newFilename))
                {
                    FetchIGIDataAndWriteResult(newFilename);
                }
            }
        }

        protected override void SearchForChildren(Individual husband, Individual wife, Fact marriage, string filename)
        {
            throw new NotImplementedException();
        }

        protected override bool SetMarriageParameters(Individual i1, Individual i2)
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
                    setParameter(GIVENNAME, forename2 + "~");
                    setParameter(SURNAME, surname2 + "~");
                    setParameter(SPOUSE_GIVENNAME, forename1 + "~");
                    setParameter(SPOUSE_SURNAME, surname1 + "~");
                    return true;
                }
            }
            else
            {
                setParameter(GIVENNAME, forename1 + "~");
                setParameter(SURNAME, surname1 + "~");
                setParameter(SPOUSE_GIVENNAME, forename2 + "~");
                setParameter(SPOUSE_SURNAME, surname2 + "~");
                return true;
            }
            return false;
        }

        protected override void fixBaseURL(StringBuilder str)
        {
            int head = str.ToString().IndexOf("<head>");
            if (head != -1)
            {
                str.Insert(head + 6, "<base href=\"" + queryString + "\" />");
            }
        }

        private HtmlDocument FetchIGIDataFromWebsite()
        {
            try
            {
                Utilities.WebRequestWrapper web = new Utilities.WebRequestWrapper();
                StringBuilder sb = new StringBuilder();
                foreach (String key in parameters.Keys)
                {
                    if (parameters[key].Length == 0) {
                        continue;
                    }

                    if (sb.Length > 0)
                    {
                        sb.Append("%20");
                    }
                    sb.Append("%2B");
                    sb.Append(key);
                    sb.Append("%3A");
                    sb.Append(parameters[key]);
                }
                queryString = "https://www.familysearch.org/search/record/results#count=20&query=" + sb.ToString();
                browser.DocumentCompleted += browser_DocumentCompleted;
                navigating = true;
                browser.Navigate(queryString);
                while (navigating || browser.IsBusy) {
                    Thread.Sleep(100);
                    Application.DoEvents();
                }
                browser.DocumentCompleted -= browser_DocumentCompleted;
                return browser.Document;
            }
            catch (IOException e)
            {
                return null;
                //return "<html><body>Error performing search:\n<p>" +
                //        e.ToString() + "</p></body></html>";
            }
        }

        private void browser_DocumentCompleted(object source, WebBrowserDocumentCompletedEventArgs e)
        {
            navigating = false;
        }

        protected override void FetchIGIDataAndWriteResult(string filename)
        {
            try
            {
                HtmlDocument document = FetchIGIDataFromWebsite();
                document.ExecCommand("SaveAs", false, filename);
                rtbOutput.AppendText("Results File written to " + filename + "\n");
                resultCount++;
            }
            catch (Exception e)
            {
                Console.WriteLine("error " + e.Message);
            }
        }
    }
}
