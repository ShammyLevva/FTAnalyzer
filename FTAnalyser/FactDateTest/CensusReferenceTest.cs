using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FTAnalyzer;

namespace CensusReferenceTest
{
    /// <summary>
    /// Summary description for CensusReferenceTest
    /// </summary>
    [TestClass]
    public class CensusReferenceTest
    {
        public CensusReferenceTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void CensusReferenceConstructorTest()
        {
            CensusReference censusRef;
            censusRef = new CensusReference("I1", "HO107 Piece: 1607 Folio: 880 Page: 29", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1851));
            Assert.IsTrue(censusRef.Piece.Equals("1607"));
            Assert.IsTrue(censusRef.Folio.Equals("880"));
            Assert.IsTrue(censusRef.Page.Equals("29"));

            censusRef = new CensusReference("I1", "            RG11 piece 870 folio 49 page 10", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1881));
            Assert.IsTrue(censusRef.Piece.Equals("870"));
            Assert.IsTrue(censusRef.Folio.Equals("49"));
            Assert.IsTrue(censusRef.Page.Equals("10"));

        }
    }
}
