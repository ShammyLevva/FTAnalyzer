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
            else if (censusCountry == Countries.CANADA)
            {
                switch(censusYear)
                {
                    case 1851: return 1325192;
                    case 1871: return 1551612;
                    case 1881: return 1804541;
                    case 1891: return 1583536;
                    case 1901: return 1584557;
                    case 1906: return 1584925;
                    case 1916: return 1529118;
                }
            }
            else if (censusCountry == Countries.UNITED_STATES)
            {
                switch (censusYear)
                {
                    case 1790: return 1803959;
                    case 1800: return 1804228;
                    case 1810: return 1803765;
                    case 1820: return 1803955;
                    case 1830: return 1803958;
                    case 1840: return 1786457;
                    case 1850: return 1401638;
                    case 1860: return 1473181;
                    case 1870: return 1438024;
                    case 1880: return 1417683;
                    case 1890: return 1610551;
                    case 1900: return 1325221;
                    case 1910: return 1727033;
                    case 1920: return 1488411;
                    case 1930: return 1810731;
                    case 1940: return 2000219;
                }
            }
            return 0;
        }
    }
}
