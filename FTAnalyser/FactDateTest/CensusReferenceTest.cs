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

//            USCensusTest("District: 1-2 , Family Number: 251 , Sheet Number and Letter: 10B , Line Number: 78 , Affiliate Publication Number: T627 , Affiliate Film Number: 544 , Digital Folder Number: 005449024 , Image Number: 00057", CensusDate.USCENSUS1940, "T627_544", "1-2", "10B");
//            USCensusTest("enumeration district (ED) 1-2, sheet 10B, family 251, NARA digital publication T627 (Washington, D.C.: National Archives and Records Administration, 2012), roll 544.", CensusDate.USCENSUS1940, "T627_544", "1-2", "10B");
            //USCensusTest("Year: 1940; Census Place: Smyrna, Kent, Delaware; Roll: T627_544; Page: 10B; Enumeration District: 1-2", CensusDate.USCENSUS1940, "T627_544", "1-2", "10B");
            //USCensusTest("Year: 1930; Census Place: Sea Cliff, Nassau, New York; Roll: 1462; Page: 14B; Enumeration District: 193;", CensusDate.USCENSUS1930, "1462", "193", "14B");
            //USCensusTest("Year: 1900; Census Place: South Prairie, Pierce,Washington; Roll: T623_1748; Page: 4B; Enumeration District: 160.", CensusDate.USCENSUS1900, "T623_1748", "160", "4B");
            //USCensusTest("Census 1910 Springfield MO USA Ward 8 ED44 p9A", CensusDate.USCENSUS1910, "8", "44", "9A");
            //USCensusTest("Census 1940 Minneapolis MN USA Ward 7 ED89-190 p3B", CensusDate.USCENSUS1940, "7", "89-190", "3B");
            //USCensusTest("Census 1940 Lebanon NH USA T627_2286 Page 12B ED 5-33", CensusDate.USCENSUS1940, "T627_2286", "5-33", "12B");
            //USCensusTest("Year 1940 Lebanon NH USA; Roll: T627_2286 Page 12B ED 5-33", CensusDate.USCENSUS1940, "T627_2286", "5-33", "12B");
            //USCensusTest("Year: 1880; Census Place: Chicago, Cook, Illinois; Roll: 195; FamilyHistory Film: 1254195; Page: 71D; Enumeration District: 136; Image:0337", CensusDate.USCENSUS1880, "195", "136", "71D");
            //USCensusTest("Year: 1880; Census Place: New York City, New York, New York; Roll: 885; Family History Film: 1254885; Page: 29C; Enumeration District: 376; Image: 0060", CensusDate.USCENSUS1880, "885", "376", "29C");
            // USCensusTest("Census 1930 Salt Lake City UT USA ED50 p6B", CensusDate.USCENSUS1930, string.Empty, "50", "6B");
            // USCensusTest("Year: 1860; Census Place: Boston Ward 12, Suffolk, Massachusetts; Roll: M653_525; Page: 515; Image: 519; Family History Library Film: 803525", CensusDate.USCENSUS1860, "M653_525", "12", "515");

            ScottishCensusTest("GROS 692/01 019/00 008", FactDate.UNKNOWN_DATE, "692-1", "19", "8");
            ScottishCensusTest("in the 1851 census, GROS 343/00 001/00 011.", CensusDate.UKCENSUS1851, "343", "1", "11");
            ScottishCensusTest("Parish: Inverurie; ED: 4; Page: 12; Line: 3; Roll: CSSCT1901_69", FactDate.UNKNOWN_DATE, "Inverurie", "4", "12");
            ScottishCensusTest("Census 1841 Kelso ROX SCT1841/793 f4 p45", CensusDate.UKCENSUS1841, "793", "4", "45");

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
            Census1911Test("RG14, Piece 00866, Registration District 10, Sub District 4, Enumeration District 25, Schedule No. 63", "00866", "63");
            Census1911Test("Census 1911 Coventry WAR ENG RG14Piece18568 RG78Piece1111 RD390 SD2 ED30 SN129", "18568", "129");
            Census1911Test("Census 1911 Wortley Leeds YKS ENG RG14/Piece26892 RG78Piece1545 RD499 SD3 ED22 SN150", "26892", "150");
            Census1911Test("1911 census - Piece 23919, SN 32 - living at 114 Princess Road, Moss Side, Manchester, Lancashire", "23919", "32");

            //UKCensusTest("Database online. Class: RG9; Piece: 1105; Folio: 90; Page: 21; GSU", CensusDate.UKCENSUS1861, "1105", "90", "21");
            //UKCensusTest("            RG11 piece 870 folio 49 page 10", CensusDate.UKCENSUS1881, "870", "49", "10");
            //UKCensusTest("Archive reference	RG11\nPiece number	870\nFolio	49\nPage	10", CensusDate.UKCENSUS1881, "870", "49", "10");
            UKCensusTest("RG11 Piece/Folio  4738 / 103 Page Number  16", CensusDate.UKCENSUS1881, "4738", "103", "16");
            UKCensusTest("RG11 Piece 2529 Folio 68 Page 1", CensusDate.UKCENSUS1881, "2529", "68", "1");

            Canadian1881Census("        123/A/55/35/1	Canada 1881", "123", "A", "35", "1");
            Canadian1881Census("        123/A/35/1	Canada 1881", "123", "A", "35", "1");
            CanadianCensus("Year: 1881; Census Place: Richibucto, Kent, New Brunswick; Roll: C_13184; Page: 32; Family No: 144", CensusDate.CANADACENSUS1881, "C_13184", "32", "144");
            Canadian1881Census("1881 census - District 146/B, Page 59, Family 273 - living at Rainham, Haldimand, Ontario, Canada.", "146", "B", "59", "273");
            //CanadianCensus("Event Place: Dumfries South, Brant North, Ontario, Canada\nDistrict Number: 160\nSub-District: C\nDivision: 2\nPage Number: 1\nFamily Number: 3\nAffiliate Film Number: C-13264", , CensusDate.CANADACENSUS1881, "C_13184", "1", "3");



            censusRef = new CensusReference("I1", "826/134/7 England & Wales 1881", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1881));
            Assert.IsTrue(censusRef.Piece.Equals("826"));
            Assert.IsTrue(censusRef.Folio.Equals("134"));
            Assert.IsTrue(censusRef.Page.Equals("7"));

            censusRef = new CensusReference("I1", "RG14; Piece: 21983", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1911));
            Assert.IsTrue(censusRef.Piece.Equals("21983"));
            Assert.IsTrue(censusRef.Status.Equals(CensusReference.ReferenceStatus.INCOMPLETE));
        }

        private void Canadian1881Census(string reference, string ED, string SD, string page, string family)
        {
            CensusReference censusRef = new CensusReference("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.CANADACENSUS1881));
            Assert.IsTrue(censusRef.ED.Equals(ED));
            Assert.IsTrue(censusRef.SD.Equals(SD));
            Assert.IsTrue(censusRef.Page.Equals(page));
            Assert.IsTrue(censusRef.Family.Equals(family));
        }

        private void CanadianCensus(string reference, FactDate year, string Roll,string page, string family)
        {
            CensusReference censusRef = new CensusReference("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(year));
            Assert.IsTrue(censusRef.Roll.Equals(Roll));
            Assert.IsTrue(censusRef.Page.Equals(page));
            Assert.IsTrue(censusRef.Family.Equals(family));
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

        private void UKCensusTest(string reference, FactDate year, string piece, string folio, string page)
        {
            CensusHO107Test(reference, year, piece, string.Empty, folio, page);
        }

        private void CensusHO107Test(string reference, FactDate year, string piece, string book, string folio, string page)
        {
            CensusReference censusRef = new CensusReference("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(year));
            Assert.IsTrue(censusRef.Piece.Equals(piece));
            Assert.IsTrue(censusRef.Folio.Equals(folio));
            Assert.IsTrue(censusRef.Page.Equals(page));
            if (!book.Equals(string.Empty))
                Assert.IsTrue(censusRef.Book.Equals(book));
        }

        private void ScottishCensusTest(string reference, FactDate year, string parish, string ED, string page)
        {
            CensusReference censusRef = new CensusReference("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(year));
            Assert.IsTrue(censusRef.Parish.Equals(parish));
            Assert.IsTrue(censusRef.ED.Equals(ED));
            Assert.IsTrue(censusRef.Page.Equals(page));
        }
    }
}
