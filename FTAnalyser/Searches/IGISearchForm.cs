using System;
using System.Collections.Specialized;
using System.IO;
using System.Collections.Generic;
namespace FTAnalyzer
{
    interface IGISearchForm
    {
        const int MARRIAGESEARCH = 1;
        const int CHILDRENSEARCH = 2;
 
        string FetchIGIDataFromWebsite();
        void fetchResults(IGIResultWriter output, Queue<IGIResult> queue, string outFile);
        NameValueCollection getEncodedParameters();
        void MarriageSearch(Family family, string dirname);
        void parseResults(int searchType, ParishBatch pb, TextWriter output, string filename, string outFile);
        int ResultCount { get; }
        void SearchForChildren(Individual husband, Individual wife, Fact marriage, string filename);
        void SearchIGI(Family family, string dirname, int searchType);
        void searchOPR(TextWriter resultFile, string dirname, TextWriter output, string surname, ParishBatch parishBatch);
        void setParameter(string key, string value);
        string verifyName(string name);
    }
}
