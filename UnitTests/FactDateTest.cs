using FTAnalyzer;
using FTAnalyzer.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Xml;
using static FTAnalyzer.FactDate;

namespace Testing
{


    /// <summary>
    ///This is a test class for FactDateTest and is intended
    ///to contain all FactDateTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FactDateTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext? TestContext { get; set; }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for FactDate Constructor
        ///</summary>
        [TestMethod()]
        public void FactDateBasicTest()
        {
            _ = BasicDates();
            _ = AlternateDateFormats();

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            FactDate target = new(null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            Assert.AreEqual(target, UNKNOWN_DATE);

            target = new("");
            Assert.AreEqual(target, UNKNOWN_DATE);

            target = new("C1914");
            Assert.AreEqual(new(1913, 1, 1), target.StartDate);
            Assert.AreEqual(new(1914, 12, 31), target.EndDate);

            target = new("ABT 966");
            Assert.AreEqual(new(965, 1, 1), target.StartDate);
            Assert.AreEqual(new(966, 12, 31), target.EndDate);

            target = new("ABT1872");
            Assert.AreEqual(new(1871, 1, 1), target.StartDate);
            Assert.AreEqual(new(1872, 12, 31), target.EndDate);

            target = new("966");
            Assert.AreEqual(new(966, 1, 1), target.StartDate);
            Assert.AreEqual(new(966, 12, 31), target.EndDate);

            target = new("BET 2 JAN AND 2 DEC 1743");
            Assert.AreEqual(new(1743, 1, 2), target.StartDate);
            Assert.AreEqual(new(1743, 12, 2), target.EndDate);

            // interpreted dates
            target = new("INT 4 OCT 1723 4DA 8MNTH 1723");
            Assert.AreEqual(new(1723, 10, 4), target.StartDate);
            Assert.AreEqual(new(1723, 10, 4), target.EndDate);

            // 29th Feb
            try
            {
                target = new("29 FEB 1735");
                Assert.Fail(); // if we get here the date was seen as valid so that's wrong
            }
            catch (FactDateException) { }  // we expect this so no test failure
            catch (Exception) { Assert.Fail(); } // if we get some other sort of failure test failed  
        }

        /// <summary>
        ///A test for FactDate Constructor
        ///</summary>
        [TestMethod()]
        public void SpecialDates()
        {
            FactDate target = new("ABT @#DJULIAN@ 1567");
            Assert.AreEqual(new(1566, 1, 1), target.StartDate);
            Assert.AreEqual(new(1567, 12, 31), target.EndDate);

            target = new("605 BC");
            Assert.AreEqual(target, UNKNOWN_DATE);

            target = new("605 B.C.");
            Assert.AreEqual(target, UNKNOWN_DATE);
        }

        /// <summary>
        ///A test for FactDate Constructor
        ///</summary>
        [TestMethod()]
        public void FactDateBetweensTest()
        {
            _ = Betweens();
            _ = MoreBetweens();
        }

        /// <summary>
        ///A test for FactDate Constructor
        ///</summary>
        [TestMethod()]
        public void FactDateLanguageTest()
        {
            // French Dates
            FactDate target = new("4 janvier 1880");
            Assert.AreEqual(new(1880, 1, 4), target.StartDate);
            Assert.AreEqual(new(1880, 1, 4), target.EndDate);

            target = new("4 MAI 1880");
            Assert.AreEqual(new(1880, 5, 4), target.StartDate);
            Assert.AreEqual(new(1880, 5, 4), target.EndDate);

        }

        /// <summary>
        ///A test for FactDate Constructor
        ///</summary>
        [TestMethod()]
        public void InvalidGEDCOMFormats()
        {
            CustomSettings test = new();
            test.ClearNonGEDCOMDateSettings();
            FactDate target;
            // invalid GEDCOM format dates
            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.DD_MM_YYYY, "dd/mm/yyyy", "/");
            target = new("5/6/2018");
            Assert.AreEqual(new(2018, 6, 5), target.StartDate);
            Assert.AreEqual(new(2018, 6, 5), target.EndDate);

            target = new("BET 5/6/2018 AND 7/6/2018");
            Assert.AreEqual(new(2018, 6, 5), target.StartDate);
            Assert.AreEqual(new(2018, 6, 7), target.EndDate);

            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.MM_DD_YYYY, "mm/dd/yyyy", "/");
            target = new("5/6/2018");
            Assert.AreEqual(new(2018, 5, 6), target.StartDate);
            Assert.AreEqual(new(2018, 5, 6), target.EndDate);

            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.DD_MM_YYYY, "dd/mm/yyyy", ".");
            target = new("5.6.2018");
            Assert.AreEqual(new(2018, 6, 5), target.StartDate);
            Assert.AreEqual(new(2018, 6, 5), target.EndDate);

            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.MM_DD_YYYY, "mm/dd/yyyy", ".");
            target = new("5.6.2018");
            Assert.AreEqual(new(2018, 5, 6), target.StartDate);
            Assert.AreEqual(new(2018, 5, 6), target.EndDate);

            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.DD_MM_YYYY, "dd/mm/yyyy", "-");
            target = new("5-6-2018");
            Assert.AreEqual(new(2018, 6, 5), target.StartDate);
            Assert.AreEqual(new(2018, 6, 5), target.EndDate);

            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.MM_DD_YYYY, "mm/dd/yyyy", "-");
            target = new("5-6-2018");
            Assert.AreEqual(new(2018, 5, 6), target.StartDate);
            Assert.AreEqual(new(2018, 5, 6), target.EndDate);

            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.DD_MM_YYYY, "dd/mm/yyyy", " ");
            target = new("5 6 2018");
            Assert.AreEqual(new(2018, 6, 5), target.StartDate);
            Assert.AreEqual(new(2018, 6, 5), target.EndDate);

            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.MM_DD_YYYY, "mm/dd/yyyy", " ");
            target = new("5 6 2018");
            Assert.AreEqual(new(2018, 5, 6), target.StartDate);
            Assert.AreEqual(new(2018, 5, 6), target.EndDate);

            test.SetNonGEDCOMDateSettings(NonGEDCOMFormatSelected.DD_MM_YYYY, "dd/mm/yyyy", "/");
            target = new("AFT 4/6/2018");
            Assert.AreEqual(new(2018, 6, 5), target.StartDate);
            Assert.AreEqual(MAXDATE, target.EndDate);
        }

        static FactDate MoreBetweens()
        {
            FactDate target = new("BTW 1914-1918");
            Assert.AreEqual(new(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new(1918, 12, 31), target.EndDate);

            target = new("FROM 1915");
            Assert.AreEqual(new(1915, 1, 1), target.StartDate);
            Assert.AreEqual(MAXDATE, target.EndDate);

            target = new("TO 1915");
            Assert.AreEqual(MINDATE, target.StartDate);
            Assert.AreEqual(new(1915, 12, 31), target.EndDate);

            target = new("FROM 1914 TO 1918");
            Assert.AreEqual(new(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new(1918, 12, 31), target.EndDate);

            target = new("APR 1914-APR 1918");
            Assert.AreEqual(new(1914, 4, 1), target.StartDate);
            Assert.AreEqual(new(1918, 4, 30), target.EndDate);

            target = new("9-17 JUL 1824");
            Assert.AreEqual(new(1824, 7, 9), target.StartDate);
            Assert.AreEqual(new(1824, 7, 17), target.EndDate);

            target = new("10 APR 1914 - 15 APR 1918");
            Assert.AreEqual(new(1914, 4, 10), target.StartDate);
            Assert.AreEqual(new(1918, 4, 15), target.EndDate);

            target = new("10 APR 1914-15 APR 1918");
            Assert.AreEqual(new(1914, 4, 10), target.StartDate);
            Assert.AreEqual(new(1918, 4, 15), target.EndDate);

            target = new("10 APR-15 JUL 1918");
            Assert.AreEqual(new(1918, 4, 10), target.StartDate);
            Assert.AreEqual(new(1918, 7, 15), target.EndDate);

            target = new("APR 1914 - APR 1918");
            Assert.AreEqual(new(1914, 4, 1), target.StartDate);
            Assert.AreEqual(new(1918, 4, 30), target.EndDate);

            target = new("1914 - 1918");
            Assert.AreEqual(new(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new(1918, 12, 31), target.EndDate);

            target = new("Bet. 1914-1918");
            Assert.AreEqual(new(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new(1918, 12, 31), target.EndDate);

            target = new("bet 1900 - 1910");
            Assert.AreEqual(new(1900, 1, 1), target.StartDate);
            Assert.AreEqual(new(1910, 12, 31), target.EndDate);

            target = new("1914 to 1918");
            Assert.AreEqual(new(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new(1918, 12, 31), target.EndDate);

            target = new("1914 until 1918");
            Assert.AreEqual(new(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new(1918, 12, 31), target.EndDate);

            target = new("Bet. 3 JAN 2001 AND 4 JAN 2001");
            Assert.AreEqual(new(2001, 1, 3), target.StartDate);
            Assert.AreEqual(new(2001, 1, 4), target.EndDate);

            target = new("BET 3 AND 4 JAN 2001");
            Assert.AreEqual(new(2001, 1, 3), target.StartDate);
            Assert.AreEqual(new(2001, 1, 4), target.EndDate);

            target = new("BET 9 AND 10 JAN 2001");
            Assert.AreEqual(new(2001, 1, 9), target.StartDate);
            Assert.AreEqual(new(2001, 1, 10), target.EndDate);

            target = new("BET 11 AND 12 JAN 2001");
            Assert.AreEqual(new(2001, 1, 11), target.StartDate);
            Assert.AreEqual(new(2001, 1, 12), target.EndDate);

            target = new("BET 997 AND 6 OCT 1014");
            Assert.AreEqual(new(997, 1, 1), target.StartDate);
            Assert.AreEqual(new(1014, 10, 6), target.EndDate);
            
            return target;
        }

        static FactDate AlternateDateFormats()
        {
            _ = DoubleDates();

            // test some alternative date formats
            FactDate target = new("Q3 1947");
            Assert.AreEqual(new(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new(1947, 9, 30), target.EndDate);

            target = new("3Q 1947");
            Assert.AreEqual(new(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new(1947, 9, 30), target.EndDate);

            target = new("ABT Q3 1947");
            Assert.AreEqual(new(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new(1947, 9, 30), target.EndDate);

            target = new("ABT QTR3 1947");
            Assert.AreEqual(new(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new(1947, 9, 30), target.EndDate);

            target = new("ABT QTR 3 1947");
            Assert.AreEqual(new(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new(1947, 9, 30), target.EndDate);

            target = new("SEP QTR 1947");
            Assert.AreEqual(new(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new(1947, 9, 30), target.EndDate);

            target = new("JAN FEB MAR 1966");
            Assert.AreEqual(new(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new(1966, 3, 31), target.EndDate);

            target = new("JAN / FEB / MAR 1966");
            Assert.AreEqual(new(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new(1966, 3, 31), target.EndDate);

            target = new("JAN/FEB/MAR 1966");
            Assert.AreEqual(new(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new(1966, 3, 31), target.EndDate);

            target = new("BET JAN-MAR 1966");
            Assert.AreEqual(new(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new(1966, 3, 31), target.EndDate);

            target = new("JAN-MAR 1966");
            Assert.AreEqual(new(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new(1966, 3, 31), target.EndDate);

            target = new("Q1 1966");
            Assert.AreEqual(new(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new(1966, 3, 31), target.EndDate);

            target = new("2 QTR 1870.");
            Assert.AreEqual(new(1870, 3, 1), target.StartDate);
            Assert.AreEqual(new(1870, 6, 30), target.EndDate);

            target = new("2d 3m 1870");
            Assert.AreEqual(new(1870, 5, 2), target.StartDate);
            Assert.AreEqual(new(1870, 5, 2), target.EndDate);

            target = new("<1870>");
            Assert.AreEqual(new(1870, 1, 1), target.StartDate);
            Assert.AreEqual(new(1870, 12, 31), target.EndDate);
            return target;
        }

        static FactDate DoubleDates()
        {
            // Double dates
            FactDate target = new("11 Mar 1747/48");
            Assert.AreEqual(new(1748, 3, 11), target.StartDate);
            Assert.AreEqual(new(1748, 3, 11), target.EndDate);

            target = new("Mar 1747/48");
            Assert.AreEqual(new(1748, 3, 1), target.StartDate);
            Assert.AreEqual(new(1748, 3, 31), target.EndDate);

            target = new("1747/48");
            Assert.AreEqual(new(1748, 1, 1), target.StartDate);
            Assert.AreEqual(new(1748, 12, 31), target.EndDate);

            target = new("11 Mar 1747/1748");
            Assert.AreEqual(new(1748, 3, 11), target.StartDate);
            Assert.AreEqual(new(1748, 3, 11), target.EndDate);

            target = new("15 FEB 1599/00");
            Assert.AreEqual(new(1600, 2, 15), target.StartDate);
            Assert.AreEqual(new(1600, 2, 15), target.EndDate);

            target = new("1 MAR 922/23");
            Assert.AreEqual(new(923, 3, 1), target.StartDate);
            Assert.AreEqual(new(923, 3, 1), target.EndDate);

            target = new("1 MAR 922/923");
            Assert.AreEqual(new(923, 3, 1), target.StartDate);
            Assert.AreEqual(new(923, 3, 1), target.EndDate);

            target = new("29 Feb 1703/1704");
            Assert.AreEqual(new(1704, 2, 29), target.StartDate);
            Assert.AreEqual(new(1704, 2, 29), target.EndDate);

            
            target = new("Bef 29 Feb 1611/12");
            Assert.AreEqual(MINDATE, target.StartDate);
            Assert.AreEqual(new(1612, 2, 28), target.EndDate);

            return target;
        }

        static FactDate Betweens()
        {
            // Betweens 
            FactDate target = new("BET 1983 AND 1986");
            Assert.AreEqual(new(1983, 1, 1), target.StartDate);
            Assert.AreEqual(new(1986, 12, 31), target.EndDate);

            target = new("BET SEP 1983 AND 1986");
            Assert.AreEqual(new(1983, 9, 1), target.StartDate);
            Assert.AreEqual(new(1986, 12, 31), target.EndDate);

            target = new("BET 28 SEP 1983 AND 1986");
            Assert.AreEqual(new(1983, 9, 28), target.StartDate);
            Assert.AreEqual(new(1986, 12, 31), target.EndDate);

            target = new("BET 1983 AND JUN 1986");
            Assert.AreEqual(new(1983, 1, 1), target.StartDate);
            Assert.AreEqual(new(1986, 6, 30), target.EndDate);

            target = new("BET SEP 1983 AND JUN 1986");
            Assert.AreEqual(new(1983, 9, 1), target.StartDate);
            Assert.AreEqual(new(1986, 6, 30), target.EndDate);

            target = new("BET 28 SEP 1983 AND JUN 1986");
            Assert.AreEqual(new(1983, 9, 28), target.StartDate);
            Assert.AreEqual(new(1986, 6, 30), target.EndDate);

            target = new("BET 1983 AND 10 JUN 1986");
            Assert.AreEqual(new(1983, 1, 1), target.StartDate);
            Assert.AreEqual(new(1986, 6, 10), target.EndDate);

            target = new("BET SEP 1983 AND 10 JUN 1986");
            Assert.AreEqual(new(1983, 9, 1), target.StartDate);
            Assert.AreEqual(new(1986, 6, 10), target.EndDate);

            target = new("BET 28 SEP 1983 AND 10 JUN 1986");
            Assert.AreEqual(new(1983, 9, 28), target.StartDate);
            Assert.AreEqual(new(1986, 6, 10), target.EndDate);
            return target;
        }

        static FactDate BasicDates()
        {
            FactDate target = new("1966");
            Assert.AreEqual(new(1966, 1, 1), target.StartDate);
            Assert.AreEqual(new(1966, 12, 31), target.EndDate);

            target = new("19 Nov");
            Assert.AreEqual(new(1, 11, 19), target.StartDate);
            Assert.AreEqual(new(9999, 11, 19), target.EndDate);

            target = new("Nov 1966");
            Assert.AreEqual(new(1966, 11, 1), target.StartDate);
            Assert.AreEqual(new(1966, 11, 30), target.EndDate);

            target = new("19 Nov 1966");
            Assert.AreEqual(new(1966, 11, 19), target.StartDate);
            Assert.AreEqual(new(1966, 11, 19), target.EndDate);

            target = new("ABT 1966");
            Assert.AreEqual(new(1965, 1, 1), target.StartDate);
            Assert.AreEqual(new(1966, 12, 31), target.EndDate);

            target = new("About        1615");
            Assert.AreEqual(new(1614, 1, 1), target.StartDate);
            Assert.AreEqual(new(1615, 12, 31), target.EndDate);

            target = new("ABT NOV 1966");
            Assert.AreEqual(new(1966, 10, 1), target.StartDate);
            Assert.AreEqual(new(1966, 11, 30), target.EndDate);

            target = new("ABT MAR 1966");
            Assert.AreEqual(new(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new(1966, 3, 31), target.EndDate);

            target = new("ABT 19 NOV 1966");
            Assert.AreEqual(new(1966, 11, 18), target.StartDate);
            Assert.AreEqual(new(1966, 11, 19), target.EndDate);

            target = new("UNKNOWN");
            Assert.AreEqual(MINDATE, target.StartDate);
            Assert.AreEqual(MAXDATE, target.EndDate);

            target = new("BEF 1966");
            Assert.AreEqual(MINDATE, target.StartDate);
            Assert.AreEqual(new(1965, 12, 31), target.EndDate);

            target = new("BEF NOV 1966");
            Assert.AreEqual(MINDATE, target.StartDate);
            Assert.AreEqual(new(1966, 10, 31), target.EndDate);

            target = new("BEF 19 Nov 1966");
            Assert.AreEqual(MINDATE, target.StartDate);
            Assert.AreEqual(new(1966, 11, 18), target.EndDate);

            target = new("AFT 1966");
            Assert.AreEqual(new(1967, 1, 1), target.StartDate);
            Assert.AreEqual(MAXDATE, target.EndDate);

            target = new("AFT Nov 1966");
            Assert.AreEqual(new(1966, 12, 01), target.StartDate);
            Assert.AreEqual(MAXDATE, target.EndDate);

            target = new("AFT 19 Nov 1966");
            Assert.AreEqual(new(1966, 11, 20), target.StartDate);
            Assert.AreEqual(MAXDATE, target.EndDate);
            
            target = new("AFT 19Ÿ©Nov 1966");
            Assert.AreEqual(new(1966, 11, 20), target.StartDate);
            Assert.AreEqual(MAXDATE, target.EndDate);

            target = new("1881 census");
            Assert.AreEqual(new(1881, 1, 1), target.StartDate);
            Assert.AreEqual(new(1881, 12, 31), target.EndDate);

            return target;
        }

        [TestMethod()]
        public void FactDatePhraseTest()
        {
            FactDate target = new("INT 17 APR 1917 (Easter Sunday 1917)");
            Assert.AreEqual(new(1917, 4, 17), target.StartDate);
            Assert.AreEqual(new(1917, 4, 17), target.EndDate);

            target = new("(1881 Census)");
            Assert.AreEqual(new(1881, 1, 1), target.StartDate);
            Assert.AreEqual(new(1881, 12, 31), target.EndDate);

            //target = new("(Easter Sunday 1917)");
            //Assert.AreEqual(UNKNOWN_DATE, target.StartDate);
            //Assert.AreEqual(UNKNOWN_DATE, target.EndDate);

        }

        [TestMethod()]
        public void FactDateComparisonTest()
        {
            FactDate first = new("1 January 1721/2");
            FactDate second = new("31 December 1721");
            FactDate third = new("31 December 1722");
            FactDate fourth = new("1722");

            Assert.IsTrue(second.IsBefore(first));
            Assert.IsTrue(second.StartsBefore(first));
            Assert.IsTrue(first.IsAfter(second));
            Assert.IsTrue(first.EndsAfter(second));
            Assert.IsFalse(first.Overlaps(second));
            Assert.IsTrue(first.IsBefore(third));
            Assert.IsTrue(third.IsAfter(first));
            Assert.IsFalse(first.IsBefore(fourth));
            Assert.IsTrue(first.Overlaps(fourth));
            Assert.IsFalse(fourth.IsBefore(first));
            Assert.IsFalse(fourth.IsAfter(first));

            FactDate census = new("BET 31 DEC 1910 AND 2 APR 1911");
            Assert.IsTrue(census.Overlaps(CensusDate.UKCENSUS1911));
            census = new("BET 1 JAN 1911 AND 2 APR 1911");
            Assert.IsTrue(census.Overlaps(CensusDate.UKCENSUS1911));
            census = new("FROM 31 DEC 1910 TO 2 APR 1911");
            Assert.IsTrue(census.Overlaps(CensusDate.UKCENSUS1911));
            census = new("FROM 1 JAN 1911 TO 2 APR 1911");
            Assert.IsTrue(census.Overlaps(CensusDate.UKCENSUS1911));
        }

        [TestMethod()]
        public void FactDateIsAliveTest()
        {
            // Individual with fixed birth no death
            Individual ind = SetupIndividual(@"<INDI><NAME>Alexander McGregor /Bisset/</NAME><SEX>M</SEX><BIRT><DATE>19 NOV 1966</DATE><PLAC>Aberdeen, Scotland</PLAC></BIRT></INDI>");

            Assert.IsTrue(ind.IsPossiblyAlive(new("20 AUG 2020")));
            Assert.IsTrue(ind.IsPossiblyAlive(new("BEF 20 AUG 2020")));
            Assert.IsTrue(ind.IsPossiblyAlive(new("AFT 20 AUG 1990")));
            Assert.IsTrue(ind.IsPossiblyAlive(new("AFT 20 AUG 2090")));
            Assert.IsTrue(ind.IsPossiblyAlive(new("BET 20 AUG 1990 AND 1 APR 2000")));
            Assert.IsTrue(ind.IsPossiblyAlive(UNKNOWN_DATE));

            Assert.IsFalse(ind.IsPossiblyAlive(new("20 AUG 1965")));
            Assert.IsFalse(ind.IsPossiblyAlive(new("BEF 20 AUG 1920")));
            Assert.IsFalse(ind.IsPossiblyAlive(new("BET 20 AUG 1890 AND 1 APR 1900")));

            // Individual with no birth and fixed death
            ind = SetupIndividual(@"<INDI><NAME>Alexander McGregor /Bisset/</NAME><SEX>M</SEX><DEAT><DATE>25 DEC 2000</DATE><PLAC>Aberdeen, Scotland</PLAC></DEAT></INDI>");
            Assert.IsTrue(ind.IsPossiblyAlive(new("BEF 20 AUG 2020")));
            Assert.IsTrue(ind.IsPossiblyAlive(new("AFT 20 AUG 1990")));
            Assert.IsTrue(ind.IsPossiblyAlive(new("BET 20 AUG 1990 AND 1 APR 2000")));
            Assert.IsTrue(ind.IsPossiblyAlive(UNKNOWN_DATE));
            Assert.IsTrue(ind.IsPossiblyAlive(new("20 AUG 1965")));
            Assert.IsTrue(ind.IsPossiblyAlive(new("BEF 20 AUG 1920")));
            Assert.IsTrue(ind.IsPossiblyAlive(new("BET 20 AUG 1890 AND 1 APR 1900")));

            Assert.IsFalse(ind.IsPossiblyAlive(new("AFT 20 AUG 2090")));
            Assert.IsFalse(ind.IsPossiblyAlive(new("20 AUG 2020")));
        }

        static Individual SetupIndividual(string individual)
        {
            XmlDocument doc = new() { XmlResolver = null };
            doc.LoadXml(individual);
            XmlAttribute attr = doc.CreateAttribute("ID");
            attr.Value = "2";
            doc.DocumentElement?.SetAttributeNode(attr);
            XmlNode node = doc.FirstChild ?? doc;
            Individual ind = new(node, new Progress<string>());
            return ind;
        }
    }
}
