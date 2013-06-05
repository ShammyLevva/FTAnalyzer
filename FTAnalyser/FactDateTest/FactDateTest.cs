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

            target = new FactDate("Nov 1966");
            Assert.AreEqual(new DateTime(1966, 11, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 11, 30), target.EndDate);

            target = new FactDate("19 Nov 1966");
            Assert.AreEqual(new DateTime(1966, 11, 19), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 11, 19), target.EndDate);

            target = new FactDate("ABT 1966");
            Assert.AreEqual(new DateTime(1965, 1, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 12, 31), target.EndDate);

            target = new FactDate("ABT NOV 1966");
            Assert.AreEqual(new DateTime(1966, 10, 1), target.StartDate);
            Assert.AreEqual(new DateTime(1966, 11, 30), target.EndDate);

            target = new FactDate("ABT MAR 1966");
            Assert.AreEqual(new DateTime(1966, 1, 1), target.StartDate);
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
 
        }
    }
}
