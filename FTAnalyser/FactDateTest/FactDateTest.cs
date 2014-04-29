using FTAnalyzer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace FactDateTest
{
    
    
    /// <summary>
    ///This is a test class for FactDateTest and is intended
    ///to contain all FactDateTest Unit Tests
    ///</summary>
    [TestClass()]
    public class FactDateTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

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
        public void FactDateConstructorTest()
        {
            FactDate target = new FactDate("1966");
            Assert.AreEqual(new DateTime(1966, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 12, 31), target.EndDate);

            target = new FactDate("19 Nov");
            Assert.AreEqual(new DateTime(1, 11, 19), target.StartDate);
            Assert.AreEqual(new DateTime(9999, 11, 19), target.EndDate);

            target = new FactDate("Nov 1966");
            Assert.AreEqual(new DateTime(1966, 11, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 11, 30), target.EndDate);

            target = new FactDate("19 Nov 1966");
            Assert.AreEqual(new DateTime(1966, 11, 19), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 11, 19), target.EndDate);

            target = new FactDate("ABT 1966");
            Assert.AreEqual(new DateTime(1965, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 12, 31), target.EndDate);

            target = new FactDate("About        1615");
            Assert.AreEqual(new DateTime(1614, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1615, 12, 31), target.EndDate);

            target = new FactDate("ABT NOV 1966");
            Assert.AreEqual(new DateTime(1966, 10, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 11, 30), target.EndDate);

            target = new FactDate("ABT MAR 1966");
            Assert.AreEqual(new DateTime(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 3, 31), target.EndDate);

            target = new FactDate("ABT 19 NOV 1966");
            Assert.AreEqual(new DateTime(1966, 11, 18), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 11, 19), target.EndDate);

            target = new FactDate("UNKNOWN");
            Assert.AreEqual(FactDate.MINDATE, target.StartDate);
            Assert.AreEqual(FactDate.MAXDATE, target.EndDate);

            target = new FactDate("BEF 1966");
            Assert.AreEqual(FactDate.MINDATE, target.StartDate);
            Assert.AreEqual(new DateTime(1965, 12, 31), target.EndDate);

            target = new FactDate("BEF NOV 1966");
            Assert.AreEqual(FactDate.MINDATE, target.StartDate);
            Assert.AreEqual(new DateTime(1966, 10, 31), target.EndDate);

            target = new FactDate("BEF 19 Nov 1966");
            Assert.AreEqual(FactDate.MINDATE, target.StartDate);
            Assert.AreEqual(new DateTime(1966, 11, 18), target.EndDate);

            target = new FactDate("AFT 1966");
            Assert.AreEqual(new DateTime(1967, 1, 1), target.StartDate);
            Assert.AreEqual(FactDate.MAXDATE, target.EndDate);

            target = new FactDate("AFT Nov 1966");
            Assert.AreEqual(new DateTime(1966, 12, 01), target.StartDate);
            Assert.AreEqual(FactDate.MAXDATE, target.EndDate);

            target = new FactDate("AFT 19 Nov 1966");
            Assert.AreEqual(new DateTime(1966, 11, 20), target.StartDate);
            Assert.AreEqual(FactDate.MAXDATE, target.EndDate);

            // Betweens 
            target = new FactDate("BET 1983 AND 1986");
            Assert.AreEqual(new DateTime(1983, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 12, 31), target.EndDate);

            target = new FactDate("BET SEP 1983 AND 1986");
            Assert.AreEqual(new DateTime(1983, 9, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 12, 31), target.EndDate);

            target = new FactDate("BET 28 SEP 1983 AND 1986");
            Assert.AreEqual(new DateTime(1983, 9, 28), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 12, 31), target.EndDate);

            target = new FactDate("BET 1983 AND JUN 1986");
            Assert.AreEqual(new DateTime(1983, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 6, 30), target.EndDate);

            target = new FactDate("BET SEP 1983 AND JUN 1986");
            Assert.AreEqual(new DateTime(1983, 9, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 6, 30), target.EndDate);

            target = new FactDate("BET 28 SEP 1983 AND JUN 1986");
            Assert.AreEqual(new DateTime(1983, 9, 28), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 6, 30), target.EndDate);

            target = new FactDate("BET 1983 AND 10 JUN 1986");
            Assert.AreEqual(new DateTime(1983, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 6, 10), target.EndDate);

            target = new FactDate("BET SEP 1983 AND 10 JUN 1986");
            Assert.AreEqual(new DateTime(1983, 9, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 6, 10), target.EndDate);

            target = new FactDate("BET 28 SEP 1983 AND 10 JUN 1986");
            Assert.AreEqual(new DateTime(1983, 9, 28), target.StartDate);
            Assert.AreEqual(new DateTime(1986, 6, 10), target.EndDate);

            // Double dates
            target = new FactDate("11 Mar 1747/48");
            Assert.AreEqual(new DateTime(1748, 3, 11), target.StartDate);
            Assert.AreEqual(new DateTime(1748, 3, 11), target.EndDate);

            target = new FactDate("Mar 1747/48");
            Assert.AreEqual(new DateTime(1748, 3, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1748, 3, 31), target.EndDate);

            target = new FactDate("1747/48");
            Assert.AreEqual(new DateTime(1748, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1748, 12, 31), target.EndDate);

            target = new FactDate("11 Mar 1747/1748");
            Assert.AreEqual(new DateTime(1748, 3, 11), target.StartDate);
            Assert.AreEqual(new DateTime(1748, 3, 11), target.EndDate);

            target = new FactDate("15 FEB 1599/00");
            Assert.AreEqual(new DateTime(1600, 2, 15), target.StartDate);
            Assert.AreEqual(new DateTime(1600, 2, 15), target.EndDate);
            
            // test some alternative date formats
            target = new FactDate("Q3 1947");
            Assert.AreEqual(new DateTime(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1947, 9, 30), target.EndDate);

            target = new FactDate("3Q 1947");
            Assert.AreEqual(new DateTime(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1947, 9, 30), target.EndDate);

            target = new FactDate("ABT Q3 1947");
            Assert.AreEqual(new DateTime(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1947, 9, 30), target.EndDate);

            target = new FactDate("ABT QTR3 1947");
            Assert.AreEqual(new DateTime(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1947, 9, 30), target.EndDate);

            target = new FactDate("ABT QTR 3 1947");
            Assert.AreEqual(new DateTime(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1947, 9, 30), target.EndDate);

            target = new FactDate("SEP QTR 1947");
            Assert.AreEqual(new DateTime(1947, 6, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1947, 9, 30), target.EndDate);

            target = new FactDate("JAN FEB MAR 1966");
            Assert.AreEqual(new DateTime(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 3, 31), target.EndDate);

            target = new FactDate("JAN / FEB / MAR 1966");
            Assert.AreEqual(new DateTime(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 3, 31), target.EndDate);

            target = new FactDate("JAN/FEB/MAR 1966");
            Assert.AreEqual(new DateTime(1965, 12, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 3, 31), target.EndDate);

            target = new FactDate("BET JAN-MAR 1966");
            Assert.AreEqual(new DateTime(1966, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 3, 31), target.EndDate);

            target = new FactDate("BTW 1914-1918");
            Assert.AreEqual(new DateTime(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1918, 12, 31), target.EndDate);

            target = new FactDate("FROM 1915");
            Assert.AreEqual(new DateTime(1915, 1, 1), target.StartDate);
            Assert.AreEqual(FactDate.MAXDATE, target.EndDate);

            target = new FactDate("TO 1915");
            Assert.AreEqual(FactDate.MINDATE, target.StartDate);
            Assert.AreEqual(new DateTime(1915, 12, 31), target.EndDate);

            target = new FactDate("FROM 1914 TO 1918");
            Assert.AreEqual(new DateTime(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1918, 12, 31), target.EndDate);

            target = new FactDate("APR 1914-APR 1918");
            Assert.AreEqual(new DateTime(1914, 4, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1918, 4, 30), target.EndDate);

            target = new FactDate("9-17 JUL 1824");
            Assert.AreEqual(new DateTime(1824, 7, 9), target.StartDate);
            Assert.AreEqual(new DateTime(1824, 7, 17), target.EndDate);

            target = new FactDate("10 APR 1914 - 15 APR 1918");
            Assert.AreEqual(new DateTime(1914, 4, 10), target.StartDate);
            Assert.AreEqual(new DateTime(1918, 4, 15), target.EndDate);

            target = new FactDate("10 APR 1914-15 APR 1918");
            Assert.AreEqual(new DateTime(1914, 4, 10), target.StartDate);
            Assert.AreEqual(new DateTime(1918, 4, 15), target.EndDate);

            target = new FactDate("10 APR-15 JUL 1918");
            Assert.AreEqual(new DateTime(1918, 4, 10), target.StartDate);
            Assert.AreEqual(new DateTime(1918, 7, 15), target.EndDate);

            target = new FactDate("APR 1914 - APR 1918");
            Assert.AreEqual(new DateTime(1914, 4, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1918, 4, 30), target.EndDate);

            target = new FactDate("1914 - 1918");
            Assert.AreEqual(new DateTime(1914, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1918, 12, 31), target.EndDate);

            target = new FactDate("bet 1900 - 1910");
            Assert.AreEqual(new DateTime(1900, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1910, 12, 31), target.EndDate);
            
            target = new FactDate("ABT 966");
            Assert.AreEqual(new DateTime(965, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(966, 12, 31), target.EndDate);

            target = new FactDate("966");
            Assert.AreEqual(new DateTime(966, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(966, 12, 31), target.EndDate);

        }

        [TestMethod()]
        public void FactDateComparisonTest()
        {
            FactDate first = new FactDate("1 January 1721/2");
            FactDate second = new FactDate("31 December 1721");

            Assert.IsTrue(second.IsBefore(first));
            Assert.IsTrue(second.StartsBefore(first));
            Assert.IsTrue(first.IsAfter(second));
            Assert.IsTrue(first.EndsAfter(second));
            Assert.IsFalse(first.Overlaps(second));

            FactDate census = new FactDate("BET 31 DEC 1910 AND 2 APR 1911");
            Assert.IsTrue(census.Overlaps(CensusDate.UKCENSUS1911));
            census = new FactDate("BET 1 JAN 1911 AND 2 APR 1911");
            Assert.IsTrue(census.Overlaps(CensusDate.UKCENSUS1911));
            census = new FactDate("FROM 31 DEC 1910 TO 2 APR 1911");
            Assert.IsTrue(census.Overlaps(CensusDate.UKCENSUS1911));
            census = new FactDate("FROM 1 JAN 1911 TO 2 APR 1911");
            Assert.IsTrue(census.Overlaps(CensusDate.UKCENSUS1911));
        }
    }
}
