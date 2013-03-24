using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.IO;
using System.Windows.Forms;

namespace FTAnalyzer.Searches
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
        
        public IGINewSearchForm(RichTextBox rtb, string defaultCountry, int level, int relationTypes, string surname) {
            rtbOutput = rtb;
            this.defaultLocation = new FactLocation(defaultCountry);
            this.defLoc = IGILocation.Adapt(this.defaultLocation, level);
            this.level = level;
            this.resultCount = 0;
            this.relationTypes = relationTypes;
            this.surname = surname.ToUpper();
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
            throw new NotImplementedException();
        }

        protected override void CheckIGIAtLocations(List<FactLocation> locations, string filename, int searchType, string surname)
        {
            throw new NotImplementedException();
        }

        protected override void SearchForChildren(Individual husband, Individual wife, Fact marriage, string filename)
        {
            throw new NotImplementedException();
        }

        protected override bool SetMarriageParameters(Individual i1, Individual i2)
        {
            throw new NotImplementedException();
        }

        protected override void FetchIGIDataAndWriteResult(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
