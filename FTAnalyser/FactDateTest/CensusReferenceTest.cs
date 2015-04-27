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

            censusRef = new CensusReference("I1", "Archive reference	RG11\nPiece number	870\nFolio	49\nPage	10", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1881));
            Assert.IsTrue(censusRef.Piece.Equals("870"));
            Assert.IsTrue(censusRef.Folio.Equals("49"));
            Assert.IsTrue(censusRef.Page.Equals("10"));

            censusRef = new CensusReference("I1", "HO107 piece 729 folio 5/15 page 6", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1841));
            Assert.IsTrue(censusRef.Piece.Equals("729"));
            Assert.IsTrue(censusRef.Book.Equals("5"));
            Assert.IsTrue(censusRef.Folio.Equals("15"));
            Assert.IsTrue(censusRef.Page.Equals("6"));

            censusRef = new CensusReference("I1", "HO107 piece 2195 folio 507 page 71", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1851));
            Assert.IsTrue(censusRef.Piece.Equals("2195"));
            Assert.IsTrue(censusRef.Folio.Equals("507"));
            Assert.IsTrue(censusRef.Page.Equals("71"));

            censusRef = new CensusReference("I1", "District: 1-2 , Family Number: 251 , Sheet Number and Letter: 10B , Line Number: 78 , Affiliate Publication Number: T627 , Affiliate Film Number: 544 , Digital Folder Number: 005449024 , Image Number: 00057", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.USCENSUS1940));
            Assert.IsTrue(censusRef.Roll.Equals("T627_544"));
            Assert.IsTrue(censusRef.ED.Equals("1-2"));
            Assert.IsTrue(censusRef.Page.Equals("10B"));

            censusRef = new CensusReference("I1", "enumeration district (ED) 1-2, sheet 10B, family 251, NARA digital publication T627 (Washington, D.C.: National Archives and Records Administration, 2012), roll 544.", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.USCENSUS1940));
            Assert.IsTrue(censusRef.Roll.Equals("T627_544"));
            Assert.IsTrue(censusRef.ED.Equals("1-2"));
            Assert.IsTrue(censusRef.Page.Equals("10B"));

            censusRef = new CensusReference("I1", "Year: 1940; Census Place: Smyrna, Kent, Delaware; Roll: T627_544; Page: 10B; Enumeration District: 1-2", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.USCENSUS1940));
            Assert.IsTrue(censusRef.Roll.Equals("T627_544"));
            Assert.IsTrue(censusRef.ED.Equals("1-2"));
            Assert.IsTrue(censusRef.Page.Equals("10B"));

            censusRef = new CensusReference("I1", "HO107, Piece 704, Folio 11, Page  14", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1841));
            Assert.IsTrue(censusRef.Piece.Equals("704"));
            Assert.IsTrue(censusRef.Folio.Equals("11"));
            Assert.IsTrue(censusRef.Page.Equals("14"));

            censusRef = new CensusReference("I1", "in the 1851 census, GROS 343/00 001/00 011.", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1851));
            Assert.IsTrue(censusRef.Parish.Equals("343"));
            Assert.IsTrue(censusRef.ED.Equals("1"));
            Assert.IsTrue(censusRef.Page.Equals("11"));

            censusRef = new CensusReference("I1", "GROS 692/01 019/00 008", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(FactDate.UNKNOWN_DATE));
            Assert.IsTrue(censusRef.Parish.Equals("692-1"));
            Assert.IsTrue(censusRef.ED.Equals("19"));
            Assert.IsTrue(censusRef.Page.Equals("8"));

            censusRef = new CensusReference("I1", "Year: 1930; Census Place: Sea Cliff, Nassau, New York; Roll: 1462; Page: 14B; Enumeration District: 193;", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.USCENSUS1930));
            Assert.IsTrue(censusRef.Roll.Equals("1462"));
            Assert.IsTrue(censusRef.ED.Equals("193"));
            Assert.IsTrue(censusRef.Page.Equals("14B"));
        }
    }
}
