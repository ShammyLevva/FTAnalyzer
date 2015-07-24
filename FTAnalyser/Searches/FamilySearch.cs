using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    class FamilySearch
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
            FILM_NUMBER = "film_number",
            RECORD_TYPE = "record_type",
            COLLECTION_ID = "collection_id",
            BIRTH_YEAR = "birth_year",
            BIRTH_LOCATION = "birth_place";

        public static int CensusCollectionID(string censusCountry, int censusYear)
        {
            if(censusCountry == Countries.ENGLAND || censusCountry == Countries.WALES)
            {
                switch(censusYear)
                {
                    case 1841 : return 1493745;
                    case 1851 : return 1850749;
                    case 1861 : return 1493747;
                    case 1871 : return 1538354;
                    case 1881 : return 1321821;
                    case 1891 : return 1865747;
                    case 1901 : return 1888129;
                    case 1911 : return 1921547;
                }
            } 
            else if(censusCountry == Countries.SCOTLAND)
            {
                switch(censusYear)
                {
                    case 1841 : return 2016000;
                    case 1851 : return 2028673;
                    case 1861 : return 2028677;
                    case 1871 : return 2028678;
                    case 1881 : return 2046756;
                    case 1891 : return 2046493;
                }
            }
            return 0;
        }
    }
}
