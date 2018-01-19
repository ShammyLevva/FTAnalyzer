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
        public string PropertyName { get; private set; }

        public static readonly CensusDate ANYCENSUS = new CensusDate("BET 1790 AND 1940", "Any Census", Countries.UNKNOWN_COUNTRY, "");

        public static readonly CensusDate UKCENSUS1841 = new CensusDate("06 JUN 1841", "UK Census 1841", Countries.UNITED_KINGDOM, "C1841");
        public static readonly CensusDate UKCENSUS1851 = new CensusDate("30 MAR 1851", "UK Census 1851", Countries.UNITED_KINGDOM, "C1851");
        public static readonly CensusDate UKCENSUS1861 = new CensusDate("07 APR 1861", "UK Census 1861", Countries.UNITED_KINGDOM, "C1861");
        public static readonly CensusDate UKCENSUS1871 = new CensusDate("02 APR 1871", "UK Census 1871", Countries.UNITED_KINGDOM, "C1871");
        public static readonly CensusDate UKCENSUS1881 = new CensusDate("03 APR 1881", "UK Census 1881", Countries.UNITED_KINGDOM, "C1881");
        public static readonly CensusDate UKCENSUS1891 = new CensusDate("05 APR 1891", "UK Census 1891", Countries.UNITED_KINGDOM, "C1891");
        public static readonly CensusDate UKCENSUS1901 = new CensusDate("31 MAR 1901", "UK Census 1901", Countries.UNITED_KINGDOM, "C1901");
        public static readonly CensusDate UKCENSUS1911 = new CensusDate("02 APR 1911", "UK Census 1911", Countries.UNITED_KINGDOM, "C1911");
        public static readonly CensusDate UKCENSUS1939 = new CensusDate("29 SEP 1939", "UK National Register 1939", Countries.UNITED_KINGDOM, "C1939");

        public static readonly CensusDate EWCENSUS1841 = new CensusDate("06 JUN 1841", "England & Wales Census 1841", Countries.ENG_WALES, "C1841");
        public static readonly CensusDate EWCENSUS1881 = new CensusDate("03 APR 1881", "England & Wales Census 1881", Countries.ENG_WALES, "C1881");
        public static readonly CensusDate EWCENSUS1911 = new CensusDate("02 APR 1911", "England & Wales Census 1911", Countries.ENG_WALES, "C1911");
        public static readonly CensusDate SCOTCENSUS1881 = new CensusDate("03 APR 1881", "Scotland Census 1881", Countries.SCOTLAND, "C1881");

        public static readonly CensusDate SCOTVALUATION1865 = new CensusDate("BET JUL 1864 AND MAY 1865", "Scottish Valuation Roll 1865", Countries.SCOTLAND, "V1865");
        public static readonly CensusDate SCOTVALUATION1875 = new CensusDate("BET JUL 1874 AND MAY 1875", "Scottish Valuation Roll 1875", Countries.SCOTLAND, "V1875");
        public static readonly CensusDate SCOTVALUATION1885 = new CensusDate("BET JUL 1884 AND MAY 1885", "Scottish Valuation Roll 1885", Countries.SCOTLAND, "V1885");
        public static readonly CensusDate SCOTVALUATION1895 = new CensusDate("BET JUL 1894 AND MAY 1895", "Scottish Valuation Roll 1895", Countries.SCOTLAND, "V1895");
        public static readonly CensusDate SCOTVALUATION1905 = new CensusDate("BET JUL 1904 AND MAY 1905", "Scottish Valuation Roll 1905", Countries.SCOTLAND, "V1905");
        public static readonly CensusDate SCOTVALUATION1915 = new CensusDate("BET JUL 1914 AND MAY 1915", "Scottish Valuation Roll 1915", Countries.SCOTLAND, "V1915");
        public static readonly CensusDate SCOTVALUATION1920 = new CensusDate("BET JUL 1919 AND MAY 1920", "Scottish Valuation Roll 1920", Countries.SCOTLAND, "V1920");
        public static readonly CensusDate SCOTVALUATION1925 = new CensusDate("BET JUL 1924 AND MAY 1925", "Scottish Valuation Roll 1925", Countries.SCOTLAND, "V1925");

        public static readonly CensusDate IRELANDCENSUS1901 = new CensusDate("31 MAR 1901", "Ireland Census 1901", Countries.IRELAND, "Ire1901");
        public static readonly CensusDate IRELANDCENSUS1911 = new CensusDate("02 APR 1911", "Ireland Census 1911", Countries.IRELAND, "Ire1911");

        public static readonly CensusDate USCENSUS1790 = new CensusDate("BET 2 AUG 1790 AND 1791", "US Federal Census 1790", Countries.UNITED_STATES, "US1790");
        public static readonly CensusDate USCENSUS1800 = new CensusDate("BET 4 AUG 1800 AND MAY 1801", "US Federal Census 1800", Countries.UNITED_STATES, "US1800");
        public static readonly CensusDate USCENSUS1810 = new CensusDate("BET 6 AUG 1810 AND JUN 1811", "US Federal Census 1810", Countries.UNITED_STATES, "US1810");
        public static readonly CensusDate USCENSUS1820 = new CensusDate("BET 7 AUG 1820 AND SEP 1821", "US Federal Census 1820", Countries.UNITED_STATES, "US1820");
        public static readonly CensusDate USCENSUS1830 = new CensusDate("BET 1 JUN 1830 AND JUN 1831", "US Federal Census 1830", Countries.UNITED_STATES, "US1830");
        public static readonly CensusDate USCENSUS1840 = new CensusDate("BET 1 JUN 1840 AND DEC 1841", "US Federal Census 1840", Countries.UNITED_STATES, "US1840");
        public static readonly CensusDate USCENSUS1850 = new CensusDate("BET 1 JUN 1850 AND MAR 1851", "US Federal Census 1850", Countries.UNITED_STATES, "US1850");
        public static readonly CensusDate USCENSUS1860 = new CensusDate("BET 1 JUN 1860 AND MAR 1861", "US Federal Census 1860", Countries.UNITED_STATES, "US1860");
        public static readonly CensusDate USCENSUS1870 = new CensusDate("BET 1 JUN 1870 AND MAR 1871", "US Federal Census 1870", Countries.UNITED_STATES, "US1870");
        public static readonly CensusDate USCENSUS1880 = new CensusDate("BET 1 JUN 1880 AND 30 JUN 1880", "US Federal Census 1880", Countries.UNITED_STATES, "US1880");
        public static readonly CensusDate USCENSUS1890 = new CensusDate("BET 2 JUN 1890 AND 30 JUN 1890", "US Federal Census 1890", Countries.UNITED_STATES, "US1890");
        public static readonly CensusDate USCENSUS1900 = new CensusDate("BET 1 JUN 1900 AND 30 JUN 1900", "US Federal Census 1900", Countries.UNITED_STATES, "US1900");
        public static readonly CensusDate USCENSUS1910 = new CensusDate("BET 15 APR 1910 AND 31 DEC 1910", "US Federal Census 1910", Countries.UNITED_STATES, "US1910");
        public static readonly CensusDate USCENSUS1920 = new CensusDate("BET 1 JAN 1920 AND 31 DEC 1920", "US Federal Census 1920", Countries.UNITED_STATES, "US1920");
        public static readonly CensusDate USCENSUS1930 = new CensusDate("BET 1 OCT 1929 AND 31 DEC 1930", "US Federal Census 1930", Countries.UNITED_STATES, "US1930");
        public static readonly CensusDate USCENSUS1940 = new CensusDate("BET 1 APR 1940 AND 31 MAY 1940", "US Federal Census 1940", Countries.UNITED_STATES, "US1940");

        public static readonly CensusDate CANADACENSUS1851 = new CensusDate("BET 1851 AND 1852", "Canadian Census 1851/2", Countries.CANADA, "Can1851");
        public static readonly CensusDate CANADACENSUS1861 = new CensusDate("1861", "Canadian Census 1861", Countries.CANADA, "Can1861");
        public static readonly CensusDate CANADACENSUS1871 = new CensusDate("2 APR 1871", "Canadian Census 1871", Countries.CANADA, "Can1871");
        public static readonly CensusDate CANADACENSUS1881 = new CensusDate("4 APR 1881", "Canadian Census 1881", Countries.CANADA, "Can1881");
        public static readonly CensusDate CANADACENSUS1891 = new CensusDate("6 APR 1891", "Canadian Census 1891", Countries.CANADA, "Can1891");
        public static readonly CensusDate CANADACENSUS1901 = new CensusDate("31 MAR 1901", "Canadian Census 1901", Countries.CANADA, "Can1901");
        public static readonly CensusDate CANADACENSUS1906 = new CensusDate("1906", "Canadian Census 1906", Countries.CANADA, "Can1906");
        public static readonly CensusDate CANADACENSUS1911 = new CensusDate("1 JUN 1911", "Canadian Census 1911", Countries.CANADA, "Can1911");
        public static readonly CensusDate CANADACENSUS1916 = new CensusDate("1916", "Canadian Census 1916", Countries.CANADA, "Can1916");
        public static readonly CensusDate CANADACENSUS1921 = new CensusDate("1 JUN 1921", "Canadian Census 1921", Countries.CANADA, "Can1921");

        private static readonly ISet<CensusDate> UK_CENSUS = new HashSet<CensusDate>(new CensusDate[] { 
            UKCENSUS1841, UKCENSUS1851, UKCENSUS1861, UKCENSUS1871, UKCENSUS1881, UKCENSUS1891, UKCENSUS1901, UKCENSUS1911, UKCENSUS1939
        });

        private static readonly ISet<CensusDate> US_FEDERAL_CENSUS = new HashSet<CensusDate>(new CensusDate[] { 
            USCENSUS1790, USCENSUS1800, USCENSUS1810, USCENSUS1820, USCENSUS1830, USCENSUS1840, USCENSUS1850, USCENSUS1860, USCENSUS1870, USCENSUS1880, USCENSUS1890,
            USCENSUS1900, USCENSUS1910, USCENSUS1920, USCENSUS1930, USCENSUS1940 
        });

        private static readonly ISet<CensusDate> CANADIAN_CENSUS = new HashSet<CensusDate>(new CensusDate[] { 
            CANADACENSUS1851, CANADACENSUS1861, CANADACENSUS1871, CANADACENSUS1881, CANADACENSUS1891, CANADACENSUS1901, CANADACENSUS1906, 
            CANADACENSUS1911, CANADACENSUS1916, CANADACENSUS1921 
        });

        public static readonly ISet<CensusDate> SUPPORTED_CENSUS = new HashSet<CensusDate>(new CensusDate[] { 
            UKCENSUS1841, UKCENSUS1851, UKCENSUS1861, UKCENSUS1871, UKCENSUS1881, UKCENSUS1891, UKCENSUS1901, UKCENSUS1911, UKCENSUS1939,
            USCENSUS1790, USCENSUS1800, USCENSUS1810, USCENSUS1820, USCENSUS1830, USCENSUS1840, USCENSUS1850, USCENSUS1860,
            USCENSUS1870, USCENSUS1880, USCENSUS1890, USCENSUS1900, USCENSUS1910, USCENSUS1920, USCENSUS1930, USCENSUS1940,
            CANADACENSUS1851, CANADACENSUS1861, CANADACENSUS1871, CANADACENSUS1881, CANADACENSUS1891, CANADACENSUS1901,
            CANADACENSUS1906, CANADACENSUS1911, CANADACENSUS1916, CANADACENSUS1921, IRELANDCENSUS1911,
            EWCENSUS1841, EWCENSUS1881, EWCENSUS1911, SCOTCENSUS1881
        });

        public static readonly ISet<CensusDate> LOSTCOUSINS_CENSUS = new HashSet<CensusDate>(new CensusDate[] { 
            EWCENSUS1841, EWCENSUS1881, SCOTCENSUS1881, EWCENSUS1911, USCENSUS1880, USCENSUS1940, CANADACENSUS1881, IRELANDCENSUS1911
        });

        public static readonly ISet<CensusDate> VALUATIONROLLS = new HashSet<CensusDate>(new CensusDate[] {
             SCOTVALUATION1865, SCOTVALUATION1875, SCOTVALUATION1885, SCOTVALUATION1895, SCOTVALUATION1905, SCOTVALUATION1915, SCOTVALUATION1920, SCOTVALUATION1925
        });

        private CensusDate(string str, string displayName, string country, string propertyName)
            : base(str)
        {
            this.displayName = displayName;
            this.Country = country;
            this.PropertyName = propertyName;
        }

        public CensusDate EquivalentUSCensus
        {
            get
            {
                switch (StartDate.Year)
                {
                    case 1841:
                        return USCENSUS1840;
                    case 1851:
                        return USCENSUS1850;
                    case 1861:
                        return USCENSUS1860;
                    case 1871:
                        return USCENSUS1870;
                    case 1881:
                        return USCENSUS1880;
                    case 1891:
                        return USCENSUS1890;
                    case 1901:
                        return USCENSUS1900;
                    case 1911:
                        return USCENSUS1910;
                    case 1939:
                        return USCENSUS1940;
                }
                return null;
            }
        }

        public static bool IsCensusYear(FactDate fd, string Country, bool exactYear)
        {
            foreach (CensusDate cd in SUPPORTED_CENSUS)
            {
                if (cd.Country.Equals(Country) && (cd.Country.Equals(Countries.UNITED_STATES) || cd.Country.Equals(Countries.CANADA)))
                    if (fd.Overlaps(cd))
                        return true;
                if (exactYear && fd.CensusYearMatches(cd))
                    return true;
                if (!exactYear && fd.Overlaps(cd))
                    return true;
            }
            return false;
        }

        public static bool IsUKCensusYear(FactDate fd, bool exactYear)
        {
            foreach (CensusDate cd in UK_CENSUS)
            {
                if (exactYear && fd.CensusYearMatches(cd))
                    return true;
                if (!exactYear && fd.Overlaps(cd))
                    return true;
            }
            return false;
        }

        public static bool IsLostCousinsCensusYear(FactDate fd, bool exactYear)
        {
            foreach (CensusDate cd in LOSTCOUSINS_CENSUS)
            {
                if (exactYear && fd.CensusYearMatches(cd))
                    return true;
                if (!exactYear && fd.Overlaps(cd))
                    return true;
            }
            return false;
        }

        public static bool IsCensusCountry(FactDate fd, FactLocation location)
        {
            List<CensusDate> matches = SUPPORTED_CENSUS.Where(cd => cd.Country.Equals(location.CensusCountry)).ToList();
            foreach (CensusDate cd in matches)
            {
                if (fd.CensusYearMatches(cd))
                    return true;
            }
            return false;
        }

        public static CensusDate GetLostCousinsCensusYear(FactDate fd, bool exactYear)
        {
            foreach (CensusDate cd in LOSTCOUSINS_CENSUS)
            {
                if (exactYear && fd.CensusYearMatches(cd))
                    return cd;
                if (!exactYear && fd.Overlaps(cd))
                    return cd;
            }
            return null;
        }

        public static FactDate GetUSCensusDateFromReference(string reference)
        {
            foreach(CensusDate cd in US_FEDERAL_CENSUS)
            {
                if (cd.PropertyName.Equals(reference))
                    return cd;
            }
            return FactDate.UNKNOWN_DATE;
        }

        public static FactDate GetCanadianCensusDateFromReference(string reference)
        {
            foreach (CensusDate cd in CANADIAN_CENSUS)
            {
                if (cd.PropertyName.ToUpper().Equals(reference))
                    return cd;
            }
            return FactDate.UNKNOWN_DATE;
        }

        public static FactDate GetUKCensusDateFromYear(string year)
        {
            foreach(CensusDate cd in UK_CENSUS)
            {
                if (cd.PropertyName.Equals("C" + year))
                    return cd;
            }
            return FactDate.UNKNOWN_DATE;
        }

        public override string ToString()
        {
            return displayName;
        }
    }
}
