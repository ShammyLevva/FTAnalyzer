using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTAnalyzer
{
    public class CensusDate : FactDate
    {
        private string displayName;
        public string Country { get; private set; }

        public static readonly CensusDate UKCENSUS1841 = new CensusDate("06 JUN 1841", "UK Census 1841", Countries.UNITED_KINGDOM);
        public static readonly CensusDate UKCENSUS1851 = new CensusDate("30 MAR 1851", "UK Census 1851", Countries.UNITED_KINGDOM);
        public static readonly CensusDate UKCENSUS1861 = new CensusDate("07 APR 1861", "UK Census 1861", Countries.UNITED_KINGDOM);
        public static readonly CensusDate UKCENSUS1871 = new CensusDate("02 APR 1871", "UK Census 1871", Countries.UNITED_KINGDOM);
        public static readonly CensusDate UKCENSUS1881 = new CensusDate("03 APR 1881", "UK Census 1881", Countries.UNITED_KINGDOM);
        public static readonly CensusDate UKCENSUS1891 = new CensusDate("05 APR 1891", "UK Census 1891", Countries.UNITED_KINGDOM);
        public static readonly CensusDate UKCENSUS1901 = new CensusDate("31 MAR 1901", "UK Census 1901", Countries.UNITED_KINGDOM);
        public static readonly CensusDate UKCENSUS1911 = new CensusDate("02 APR 1911", "UK Census 1911", Countries.UNITED_KINGDOM);

        public static readonly CensusDate IRELANDCENSUS1911 = new CensusDate("02 APR 1911", "Ireland Census 1911", Countries.IRELAND);

        public static readonly CensusDate USCENSUS1790 = new CensusDate("AUG 1790", "US Federal Census 1790", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1800 = new CensusDate("AUG 1800", "US Federal Census 1800", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1810 = new CensusDate("AUG 1810", "US Federal Census 1810", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1820 = new CensusDate("AUG 1820", "US Federal Census 1820", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1830 = new CensusDate("1 JUN 1830", "US Federal Census 1830", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1840 = new CensusDate("1 JUN 1840", "US Federal Census 1840", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1850 = new CensusDate("1 JUN 1850", "US Federal Census 1850", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1860 = new CensusDate("1 JUN 1860", "US Federal Census 1860", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1870 = new CensusDate("1 JUN 1870", "US Federal Census 1870", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1880 = new CensusDate("1 JUN 1880", "US Federal Census 1880", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1890 = new CensusDate("JUN 1890", "US Federal Census 1890", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1900 = new CensusDate("1 JUN 1900", "US Federal Census 1900", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1910 = new CensusDate("15 APR 1910", "US Federal Census 1910", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1920 = new CensusDate("1 JAN 1920", "US Federal Census 1920", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1930 = new CensusDate("1 APR 1930", "US Federal Census 1930", Countries.UNITED_STATES);
        public static readonly CensusDate USCENSUS1940 = new CensusDate("1 APR 1940", "US Federal Census 1940", Countries.UNITED_STATES);

        public static readonly CensusDate CANADACENSUS1851 = new CensusDate("BET 1851 AND 1852", "Canadian Census 1851/2", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1861 = new CensusDate("1861", "Canadian Census 1861", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1871 = new CensusDate("2 APR 1871", "Canadian Census 1871", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1881 = new CensusDate("4 APR 1881", "Canadian Census 1881", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1891 = new CensusDate("6 APR 1891", "Canadian Census 1891", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1901 = new CensusDate("31 MAR 1901", "Canadian Census 1901", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1906 = new CensusDate("1906", "Canadian Census 1906", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1911 = new CensusDate("1 JUN 1911", "Canadian Census 1911", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1916 = new CensusDate("1916", "Canadian Census 1916", Countries.CANADA);
        public static readonly CensusDate CANADACENSUS1921 = new CensusDate("1 JUN 1921", "Canadian Census 1921", Countries.CANADA);

        private static readonly ISet<CensusDate> UK_CENSUS = new HashSet<CensusDate>(new CensusDate[] { 
            UKCENSUS1841, UKCENSUS1851, UKCENSUS1861, UKCENSUS1871, UKCENSUS1881, UKCENSUS1891, UKCENSUS1901, UKCENSUS1911
        });

        private static readonly ISet<CensusDate> SUPPORTED_CENSUS = new HashSet<CensusDate>(new CensusDate[] { 
            UKCENSUS1841, UKCENSUS1851, UKCENSUS1861, UKCENSUS1871, UKCENSUS1881, UKCENSUS1891, UKCENSUS1901, UKCENSUS1911,
            USCENSUS1790, USCENSUS1800, USCENSUS1810, USCENSUS1820, USCENSUS1830, USCENSUS1840, USCENSUS1850, USCENSUS1860,
            USCENSUS1870, USCENSUS1880, USCENSUS1890, USCENSUS1900, USCENSUS1910, USCENSUS1920, USCENSUS1930, USCENSUS1940,
            CANADACENSUS1851, CANADACENSUS1861, CANADACENSUS1871, CANADACENSUS1881, CANADACENSUS1891, CANADACENSUS1901,
            CANADACENSUS1906, CANADACENSUS1911, CANADACENSUS1916, CANADACENSUS1921, IRELANDCENSUS1911
        });

        private static readonly ISet<CensusDate> LOSTCOUSINS_CENSUS = new HashSet<CensusDate>(new CensusDate[] { 
            UKCENSUS1841, UKCENSUS1881, UKCENSUS1911, USCENSUS1880, USCENSUS1940, CANADACENSUS1881, IRELANDCENSUS1911
        });

        private CensusDate(string str, string displayName, string country)
            : base(str)
        {
            this.displayName = displayName;
            this.Country = country;
        }

        public static bool IsCensusYear(FactDate fd, bool exactYear)
        {
            if(exactYear)
            {
                foreach(CensusDate cd in SUPPORTED_CENSUS)
                {
                    if(fd.YearMatches(cd)) 
                        return true;
                }
                return false;
            }
            else
            {
                foreach(CensusDate cd in SUPPORTED_CENSUS)
                {
                    if(fd.Overlaps(cd)) 
                        return true;
                }
                return false;
            }
        }

        public static bool IsLostCousinsCensusYear(FactDate fd, bool exactYear)
        {
            if(exactYear)
                return (fd.YearMatches(UKCENSUS1841) || fd.YearMatches(UKCENSUS1881) || fd.YearMatches(UKCENSUS1911) ||
                    fd.YearMatches(USCENSUS1880) || fd.YearMatches(USCENSUS1940) || fd.YearMatches(CANADACENSUS1881) || fd.YearMatches(IRELANDCENSUS1911));
            else
                return (fd.Overlaps(UKCENSUS1841) || fd.Overlaps(UKCENSUS1881) || fd.Overlaps(UKCENSUS1911) ||
                    fd.Overlaps(USCENSUS1880) || fd.Overlaps(USCENSUS1940) || fd.Overlaps(CANADACENSUS1881) || fd.Overlaps(IRELANDCENSUS1911));
        }

        public override string ToString()
        {
            return displayName;
        }
    }
}
