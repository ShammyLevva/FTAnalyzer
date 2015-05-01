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

            USCensusTest("District: 1-2 , Family Number: 251 , Sheet Number and Letter: 10B , Line Number: 78 , Affiliate Publication Number: T627 , Affiliate Film Number: 544 , Digital Folder Number: 005449024 , Image Number: 00057",
                CensusDate.USCENSUS1940, "T627_544", "1-2", "10B");
            USCensusTest("enumeration district (ED) 1-2, sheet 10B, family 251, NARA digital publication T627 (Washington, D.C.: National Archives and Records Administration, 2012), roll 544.",
                CensusDate.USCENSUS1940, "T627_544", "1-2", "10B");
            USCensusTest("Year: 1940; Census Place: Smyrna, Kent, Delaware; Roll: T627_544; Page: 10B; Enumeration District: 1-2",
                CensusDate.USCENSUS1940, "T627_544", "1-2", "10B");
            USCensusTest("Year: 1930; Census Place: Sea Cliff, Nassau, New York; Roll: 1462; Page: 14B; Enumeration District: 193;",
                CensusDate.USCENSUS1930, "1462", "193", "14B");
            USCensusTest("Year: 1900; Census Place: South Prairie, Pierce,Washington; Roll: T623_1748; Page: 4B; Enumeration District: 160.",
                CensusDate.USCENSUS1900, "T623_1748", "160", "4B");

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

            censusRef = new CensusReference("I1", "Parish: Inverurie; ED: 4; Page: 12; Line: 3; Roll: CSSCT1901_69", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(FactDate.UNKNOWN_DATE));
            Assert.IsTrue(censusRef.ED.Equals("4"));
            Assert.IsTrue(censusRef.Page.Equals("12"));
            Assert.IsTrue(censusRef.Parish.Equals("Inverurie"));

            CensusHO107Test("HO107 Piece: 1607 Folio: 880 Page: 29", CensusDate.UKCENSUS1851, "1607", string.Empty, "880", "29");
            CensusHO107Test("HO107 piece 729 folio 5/15 page 6", CensusDate.UKCENSUS1841, "729", "5", "15", "6");
            CensusHO107Test("HO107 piece 2195 folio 507 page 71", CensusDate.UKCENSUS1851, "2195", string.Empty, "507", "71");
            CensusHO107Test("Database online. Class: HO107; Piece 709; Book: 6; Civil Parish: StLeonard Shoreditch; County: Middlesex; Enumeration District: 19;Folio: 53; Page: 15; Line: 16; GSU roll: 438819.", CensusDate.UKCENSUS1841, "709", "6", "53", "15");
            CensusHO107Test("HO107, Piece 704, Folio 11, Page  14", CensusDate.UKCENSUS1841, "704", string.Empty, "11", "14");
            CensusHO107Test("Database online. Class: HO107; Piece: 1782; Folio: 719; Page: 25; GSU", CensusDate.UKCENSUS1851, "1782", string.Empty, "719", "25");

            Census1911Test("1911 census - Piece 22623, SN 183", "22623", "183");
            Census1911Test("1911 census - Piece: 22623, SN: 183", "22623", "183");
            Census1911Test("1911 census - Piece: 22623; Schedule No. : 183", "22623", "183");
            Census1911Test("Class: RG14; Piece: 22623; Schedule Number: 183", "22623", "183");
            Census1911Test("RG14PN22623 RG78PN1327 RD455 SD10 ED13 SN183", "22623", "183");
            //Census1911Test("RG14, Piece 00866, Registration District 10, Sub District 4, Enumeration District 25, Schedule No. 63", "866", "63");

            censusRef = new CensusReference("I1", "Database online. Class: RG9; Piece: 1105; Folio: 90; Page: 21; GSU", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1861));
            Assert.IsTrue(censusRef.Piece.Equals("1105"));
            Assert.IsTrue(censusRef.Folio.Equals("90"));
            Assert.IsTrue(censusRef.Page.Equals("21"));

            censusRef = new CensusReference("I1", "RG14; Piece: 21983", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1911));
            Assert.IsTrue(censusRef.Piece.Equals("21983"));
            Assert.IsTrue(censusRef.Status.Equals(CensusReference.ReferenceStatus.INCOMPLETE));
        }

        private void USCensusTest(string reference, FactDate year, string roll, string ED, string page)
        {
            CensusReference censusRef = new CensusReference("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(year));
            Assert.IsTrue(censusRef.Roll.Equals(roll));
            Assert.IsTrue(censusRef.ED.Equals(ED));
            Assert.IsTrue(censusRef.Page.Equals(page));
        }

        private void Census1911Test(string reference, string piece, string schedule)
        {
            CensusReference censusRef = new CensusReference("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1911));
            Assert.IsTrue(censusRef.Piece.Equals(piece));
            Assert.IsTrue(censusRef.Schedule.Equals(schedule));
        }

        private void CensusHO107Test(string reference, FactDate year, string piece, string book, string folio, string page)
        {

        }
    }
}
