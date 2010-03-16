using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class CensusDate : FactDate
    {
        private string displayName;

        public static readonly CensusDate UKCENSUS1841 = new CensusDate("06 JUN 1841", "UK Census 1841");
        public static readonly CensusDate UKCENSUS1851 = new CensusDate("30 MAR 1851", "UK Census 1851");
        public static readonly CensusDate UKCENSUS1861 = new CensusDate("07 APR 1861", "UK Census 1861");
        public static readonly CensusDate UKCENSUS1871 = new CensusDate("02 APR 1871", "UK Census 1871");
        public static readonly CensusDate UKCENSUS1881 = new CensusDate("03 APR 1881", "UK Census 1881");
        public static readonly CensusDate UKCENSUS1891 = new CensusDate("05 APR 1891", "UK Census 1891");
        public static readonly CensusDate UKCENSUS1901 = new CensusDate("31 MAR 1901", "UK Census 1901");
        public static readonly CensusDate UKCENSUS1911 = new CensusDate("02 APR 1911", "UK Census 1911");
        
        public static readonly CensusDate USCENSUS1790 = new CensusDate("AUG 1790", "US Federal Census 1790");
        public static readonly CensusDate USCENSUS1800 = new CensusDate("AUG 1800", "US Federal Census 1800");
        public static readonly CensusDate USCENSUS1810 = new CensusDate("AUG 1810", "US Federal Census 1810");
        public static readonly CensusDate USCENSUS1820 = new CensusDate("AUG 1820", "US Federal Census 1820");
        public static readonly CensusDate USCENSUS1830 = new CensusDate("1 JUN 1830", "US Federal Census 1830");
        public static readonly CensusDate USCENSUS1840 = new CensusDate("1 JUN 1840", "US Federal Census 1840");
        public static readonly CensusDate USCENSUS1850 = new CensusDate("1 JUN 1850", "US Federal Census 1850");
        public static readonly CensusDate USCENSUS1860 = new CensusDate("1 JUN 1860", "US Federal Census 1860");
        public static readonly CensusDate USCENSUS1870 = new CensusDate("1 JUN 1870", "US Federal Census 1870");
        public static readonly CensusDate USCENSUS1880 = new CensusDate("1 JUN 1880", "US Federal Census 1880");
        public static readonly CensusDate USCENSUS1890 = new CensusDate("JUN 1890", "US Federal Census 1890");
        public static readonly CensusDate USCENSUS1900 = new CensusDate("1 JUN 1900", "US Federal Census 1900");
        public static readonly CensusDate USCENSUS1910 = new CensusDate("15 APR 1910", "US Federal Census 1910");
        public static readonly CensusDate USCENSUS1920 = new CensusDate("1 JAN 1920", "US Federal Census 1920");
        public static readonly CensusDate USCENSUS1930 = new CensusDate("1 APR 1930", "US Federal Census 1930");

        public static readonly CensusDate CANADACENSUS1851 = new CensusDate("BET 1851 AND 1852", "Canadian Census 1851/2");
        public static readonly CensusDate CANADACENSUS1861 = new CensusDate("1861", "Canadian Census 1861");
        public static readonly CensusDate CANADACENSUS1871 = new CensusDate("2 APR 1871", "Canadian Census 1871");
        public static readonly CensusDate CANADACENSUS1881 = new CensusDate("4 APR 1881", "Canadian Census 1881");
        public static readonly CensusDate CANADACENSUS1891 = new CensusDate("6 APR 1891", "Canadian Census 1891");
        public static readonly CensusDate CANADACENSUS1901 = new CensusDate("31 MAR 1901", "Canadian Census 1901");
        public static readonly CensusDate CANADACENSUS1906 = new CensusDate("1906", "Canadian Census 1906");
        public static readonly CensusDate CANADACENSUS1911 = new CensusDate("1 JUN 1911", "Canadian Census 1911");
        public static readonly CensusDate CANADACENSUS1916 = new CensusDate("1916", "Canadian Census 1916");

        public CensusDate(string str, string displayName)
            : base(str)
        {
            this.displayName = displayName;
        }

        public string DisplayName
        {
            get { return displayName; }
        }
    }
}
