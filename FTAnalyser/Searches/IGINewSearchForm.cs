using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;

namespace FTAnalyzer.Searches
{
    public class IGINewSearchForm : IGISearchForm
    {

        public string FetchIGIDataFromWebsite()
        {
            throw new NotImplementedException();
        }

        public void fetchResults(IGIResultWriter output, Queue<IGIResult> queue, string outFile)
        {
            throw new NotImplementedException();
        }

        public NameValueCollection getEncodedParameters()
        {
            throw new NotImplementedException();
        }

        public void MarriageSearch(Family family, string dirname)
        {
            throw new NotImplementedException();
        }

        public void parseResults(int searchType, ParishBatch pb, TextWriter output, string filename, string outFile)
        {
            throw new NotImplementedException();
        }

        public int ResultCount
        {
            get { throw new NotImplementedException(); }
        }

        public void SearchForChildren(Individual husband, Individual wife, Fact marriage, string filename)
        {
            throw new NotImplementedException();
        }

        public void SearchIGI(Family family, string dirname, int searchType)
        {
            throw new NotImplementedException();
        }

        public void searchOPR(TextWriter resultFile, string dirname, TextWriter output, string surname, ParishBatch parishBatch)
        {
            throw new NotImplementedException();
        }

        public void setParameter(string key, string value)
        {
            throw new NotImplementedException();
        }

        public string verifyName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
