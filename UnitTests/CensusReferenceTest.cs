using FTAnalyzer;
using FTAnalyzer.Forms;

namespace UnitTests
{
    /// <summary>
    /// Summary description for CensusReferenceTest
    /// </summary>
    [TestClass]
    public class CensusReferenceTest
    {
        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext? TestContext { get; set; }

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
        public void CensusReferenceUSTests()
        {
            //USCensusTest("District: 1-2 , Family Number: 251 , Sheet Number and Letter: 10B , Line Number: 78 , Affiliate Publication Number: T627 , Affiliate Film Number: 544 , Digital Folder Number: 005449024 , Image Number: 00057", CensusDate.USCENSUS1940, "T627_544", "1-2", "10B");
            //USCensusTest("enumeration district (ED) 1-2, sheet 10B, family 251, NARA digital publication T627 (Washington, D.C.: National Archives and Records Administration, 2012), roll 544.", CensusDate.USCENSUS1940, "T627_544", "1-2", "10B");
            USCensusTest("Year: 1930; Census Place: Sea Cliff, Nassau, New York; Roll: 1462; Page: 14B; Enumeration District: 193;", CensusDate.USCENSUS1930, "1462", "193", "14B");
            USCensusTest("1930.T626, 2,667 rolls.Clovis, Curry, New Mexico, ED 1, roll 1394, page , image 37.0. 300 Sheldon ST", CensusDate.USCENSUS1930, "1394", "1", "");
            USCensusTest("Year: 1900; Census Place: South Prairie, Pierce,Washington; Roll: T623_1748; Page: 4B; Enumeration District: 160.", CensusDate.USCENSUS1900, "T623_1748", "160", "4B");
            USCensusTest("Census 1910 Springfield MO USA Ward 8 ED44 p9A", CensusDate.USCENSUS1910, "8", "44", "9A");
            USCensusTest("United States Census, 1880, database with images, FamilySearch(https://familysearch.org/ark:/61903/1:1:M4J9-Z86 : 12 August 2017), John Smith, Spring Hill, Barbour, Alabama, United States; citing enumeration district ED 13, sheet 155B, NARA microfilm publication T9 (Washington D.C.: National Archives and Records Administration, n.d.), roll 0002; FHL microfilm 1,254,002.", CensusDate.USCENSUS1880, "2", "13", "155B");
            USCensusTest("Census 1940 Minneapolis MN USA Ward 7 ED89-190 p3B", CensusDate.USCENSUS1940, "7", "89-190", "3B");
            USCensusTest("Census 1940 Lebanon NH USA T627_2286 Page 12B ED 5-33", CensusDate.USCENSUS1940, "2286", "5-33", "12B");
            USCensusTest("Year: 1940; Census Place: Smyrna, Kent, Delaware; Roll: T627_544; Page: 10B; Enumeration District: 1-2", CensusDate.USCENSUS1940, "544", "1-2", "10B");
            USCensusTest("Year 1940 Lebanon NH USA; Roll: T627_2286 Page 12B ED 5-33", CensusDate.USCENSUS1940, "2286", "5-33", "12B");
            USCensusTest("Year: 1880; Census Place: Chicago, Cook, Illinois; Roll: 195; FamilyHistory Film: 1254195; Page: 71D; Enumeration District: 136; Image:0337", CensusDate.USCENSUS1880, "195", "136", "71D");
            USCensusTest("Year: 1880; Census Place: New York City, New York, New York; Roll: 885; Family History Film: 1254885; Page: 29C; Enumeration District: 376; Image: 0060", CensusDate.USCENSUS1880, "885", "376", "29C");
            USCensusTest("Census 1930 Salt Lake City UT USA ED50 p6B", CensusDate.USCENSUS1930, string.Empty, "50", "6B");
            //USCensusTest("Year: 1860; Census Place: Boston Ward 12, Suffolk, Massachusetts; Roll: M653_525; Page: 515; Image: 519; Family History Library Film: 803525", CensusDate.USCENSUS1860, "M653_525", "12", "515");
            USCensusTest("Roll T627_1141; ED 8-14; Page 9A", CensusDate.USCENSUS1940, "1141", "8-14", "9A");
            USCensusTest("Roll: m-t0627_2227; ED 1-7; Page 19A", CensusDate.USCENSUS1940, "2227", "1-7", "19A");
            USCensusTest("T627, roll 4515, Burlington, enumeration district (ED) 51-5, sheet 4A, household 45, Alice Reed, accessed 22 Jan 2019", CensusDate.USCENSUS1940, "4515", "51-5", "4A");
            //            USCensusTest("Madison, Venice, enumeration district 90, page 8A, dwelling number 229, family number 233, lines 38 - 42, Henry Anters household", CensusDate.UNKNOWN_DATE, "Unknown", "90", "8A");
            USCensusTest("1930 U.S. census, population schedule, Illinois, Madison, Venice, enumeration district 90, page 8A, dwelling number 229, family number 233, lines 38 - 42, Henry Anters household; NARA microfilm publication T626, roll 542; digital image, Ancestry.com (www.ancestry.com : accessed 12 Jun 2019).", CensusDate.USCENSUS1930, "542", "90", "8A");
            USCensusTest("Microfilm T623 Roll 1685 State Utah County Salt Lake ED 63 Page 9A Dwelling Number 169 Family 177", CensusDate.USCENSUS1900, "1685", "63", "9A");
            USCensusTest("Microfilm T9 Roll 1337 State Utah County Salt Lake ED 57 Page 272B Dwelling Number 186 Family 191", CensusDate.USCENSUS1880, "1337", "57", "272B");
            USCensusTest("Microfilm M432 Roll 1337 State Utah County Salt Lake ED 57 Page 272B Dwelling Number 186 Family 191", CensusDate.USCENSUS1850, "1337", "57", "272B");
            USCensusTest("Microfilm M19 Roll 1337 State Utah County Salt Lake ED 57 Page 272B Dwelling Number 186 Family 191", CensusDate.USCENSUS1830, "1337", "57", "272B");
            USCensusTest("Year 1900 Census Rochester Ward 9 Monroe New York Page 5 ED 0067 FHL microfilm", CensusDate.USCENSUS1900, "", "0067", "5");
            USCensusTest("Year 1930 Census Trenton Mercer New Jersey Page 1B ED 0054 FHL", CensusDate.USCENSUS1930, "", "0054", "1B");
            //USCensusTest("Year: 1920; Census Place: Hartford, Trumbull, OH; Roll: T625_1443; Page: 6B; Enumeration District: 258; Image: 292", CensusDate.USCENSUS1920, "T265_1443", "258", "6B");
        }

        [TestMethod]
        public void CensusReferenceScottishTests()
        {
            ScottishCensusTest("GROS 692/01 019/00 008", FactDate.UNKNOWN_DATE, "692-1", "19", "8");
            ScottishCensusTest("in the 1851 census, GROS 343/00 001/00 011.", CensusDate.UKCENSUS1851, "343", "1", "11");
            ScottishCensusTest("Parish: Inverurie; ED: 4; Page: 12; Line: 3; Roll: CSSCT1901_69", FactDate.UNKNOWN_DATE, "Inverurie", "4", "12");
            ScottishCensusTest("Census 1841 Kelso ROX SCT1841/793 f4 p45", CensusDate.UKCENSUS1841, "793", "4", "45");
            ScottishCensusTest("1881 GROS 233/ 2/ 7", CensusDate.SCOTCENSUS1881, "233", "2", "7");
            ScottishCensusTest("GROS 225 / 7 / 15", FactDate.UNKNOWN_DATE, "225", "7", "15");
            ScottishCensusTest("1881 GROS 225 / 7 / 15", CensusDate.SCOTCENSUS1881, "225", "7", "15");
            ScottishCensusTest("Ref: 1881 GROS 225 / 7 / 15", CensusDate.SCOTCENSUS1881, "225", "7", "15");
            ScottishCensusTest("1871, Census 496 / 11 / 69", CensusDate.UKCENSUS1871, "496", "11", "69");
            ScottishCensusTest("1891, Census 496/ 13/ 26", CensusDate.UKCENSUS1891, "496", "13", "26");
        }

        [TestMethod]
        public void CensusReferenceH107Tests()
        {
            CensusHO107Test("HO107 Piece: 1607 Folio: 880 Page: 29", CensusDate.UKCENSUS1851, "1607", string.Empty, "880", "29");
            CensusHO107Test("HO107 piece 729 folio 5/15 page 6", CensusDate.UKCENSUS1841, "729", "5", "15", "6");
            CensusHO107Test("HO107 piece 2195 folio 507 page 71", CensusDate.UKCENSUS1851, "2195", string.Empty, "507", "71");
            CensusHO107Test("Database online. Class: HO107; Piece 709; Book: 6; Civil Parish: StLeonard Shoreditch; County: Middlesex; Enumeration District: 19;Folio: 53; Page: 15; Line: 16; GSU roll: 438819.", CensusDate.UKCENSUS1841, "709", "6", "53", "15");
            CensusHO107Test("HO107, Piece 704, Folio 11, Page  14", CensusDate.UKCENSUS1841, "704", string.Empty, "11", "14");
            CensusHO107Test("Database online. Class: HO107; Piece: 1782; Folio: 719; Page: 25; GSU", CensusDate.UKCENSUS1851, "1782", string.Empty, "719", "25");
            CensusHO107Test(@" Bethnal Green, folio 10/9, page 11, Caroline Shaboe; digital images, \i FindMyPast.co.uk\i0  (https://search.findmypast.co.uk/search-world-Records/1841-england-wales-and-scotland-census : accessed 29 Dec 2009); citing PRO HO/107/692", CensusDate.UKCENSUS1841, "692", "10", "9", "11");
            CensusHO107Test(@"Leeds, Yorkshire, folio 9, page 10, John Knowling; digital images, \i FindMyPast.co.uk\i0 (https://search.findmypast.co.uk/search-world-Records/1841-england-wales-and-scotland-census : accessed 20 Oct 2017); citing PRO HO/107/1345", CensusDate.UKCENSUS1841, "1345", "", "9", "10");
            CensusHO107Test("HO107/797/10 f6 p5", CensusDate.UKCENSUS1841, "797", "10", "6", "5");
            CensusHO107Test("Archive reference HO107 Piece number 142 Book number 10 Folio number 51 Page number 1 Record set 1841 England, Wales & Scotland Census", CensusDate.UKCENSUS1841, "142", "10", "51", "1");
            CensusHO107Test(@"Ulleskelf, Tadcaster, folio 9, Book 8, page 12, Joshua Hey and family; digital images, \i FindMyPast.co.uk\i0  (https://search.findmypast.co.uk/search-world-Records/1841-england-wales-and-scotland-census : accessed 12 Jun 2017); citing PRO HO/107/1282", CensusDate.UKCENSUS1841, "1282", "8", "9", "12");
            CensusHO107Test("Archive reference HO107 Piece number 1541 Folio 119 Page 15 Record set 1851 England, Wales & Scotland Census", CensusDate.UKCENSUS1851, "1541", "", "119", "15");
        }

        [TestMethod]
        public void CensusReference1911Tests()
        {
            Census1911Test("1911 census - Piece 22623, SN 183", "22623", "183");
            Census1911Test("1911 census - Piece: 22623, SN: 183", "22623", "183");
            Census1911Test("1911 census - Piece: 22623; Schedule No. : 183", "22623", "183");
            Census1911Test("Class: RG14; Piece: 22623; Schedule Number: 183", "22623", "183");
            Census1911Test("RG14PN22623 RG78PN1327 RD455 SD10 ED13 SN183", "22623", "183");
            Census1911Test("RG14/17131 SN117", "17131", "117");
            Census1911Test("RG14, Piece 00866, Registration District 10, Sub District 4, Enumeration District 25, Schedule No. 63", "00866", "63");
            Census1911Test("Census 1911 Coventry WAR ENG RG14Piece18568 RG78Piece1111 RD390 SD2 ED30 SN129", "18568", "129");
            Census1911Test("Census 1911 Wortley Leeds YKS ENG RG14/Piece26892 RG78Piece1545 RD499 SD3 ED22 SN150", "26892", "150");
            Census1911Test("1911 census - Piece 23919, SN 32 - living at 114 Princess Road, Moss Side, Manchester, Lancashire", "23919", "32");
        }

        [TestMethod]
        public void CensusReference1921Tests()
        {
            Census1921Test("The National Archives of the UK(TNA); Kew, Surrey, England; 1921 Census Returns; Reference: RG 15/18159, ED 3, Sch 185; Book: 18159", "18159", "3", "185");
            //Census1921Test("RG15 RD 476 RS 5 ED 4", "475", "4");
        }
        

        [TestMethod]
        public void CensusReferenceUKTests()
        {
            UKCensusTest("Class: RG11; Piece: 890; Folio: 114; Page: 9; GSU roll: 1341211", CensusDate.UKCENSUS1881, "890", "114", "9");
            UKCensusTest("Database online. Class: RG9; Piece: 1105; Folio: 90; Page: 21; GSU", CensusDate.UKCENSUS1861, "1105", "90", "21");
            UKCensusTest("            RG11 piece 870 folio 49 page 10", CensusDate.UKCENSUS1881, "870", "49", "10");
            UKCensusTest("Archive reference RG11 Piece number 4594 Folio 9 Page 12", CensusDate.UKCENSUS1881, "4594", "9", "12");
            UKCensusTest("Archive reference RG11 Piece number 1974  Folio 42 Page 3", CensusDate.UKCENSUS1881, "1974", "42", "3");
            UKCensusTest("Archive reference RG11 Piece number 817 Folio 74\r\nPage 20", CensusDate.UKCENSUS1881, "817", "74", "20");
            UKCensusTest("RG11   1781    5   4    587", CensusDate.UKCENSUS1881, "1781", "5", "4");
            UKCensusTest("RG11   3282   63  10   178", CensusDate.UKCENSUS1881, "3282", "63", "10");
            UKCensusTest("Archive reference	RG11\nPiece number	870\nFolio	49\nPage	10", CensusDate.UKCENSUS1881, "870", "49", "10");
            UKCensusTest("Archive reference RG11 Piece number 4594 Folio 9 Page 12", CensusDate.UKCENSUS1881, "4594", "9", "12");
            UKCensusTest(@"Castleford, Pontefract, folio 9, page 12, Thomas Hey and family; digital images, \i FindMyPast.co.uk\i0  (https://search.findmypast.co.uk/search-world-Records/1881-england-wales-and-scotland-census : accessed 12 Jun 2017); citing PRO RG 11/4594", CensusDate.UKCENSUS1881, "4594", "9", "12");
            UKCensusTest("RG11 Piece/Folio  4738 / 103 Page Number  16", CensusDate.UKCENSUS1881, "4738", "103", "16");
            UKCensusTest("RG11 Piece 2529 Folio 68 Page 1", CensusDate.UKCENSUS1881, "2529", "68", "1");
            UKCensusTest("Class: RG11; Piece: 3934; Folio: 60; Page: 9; Line:; GSU roll: 1341939", CensusDate.UKCENSUS1881, "3934", "60", "9");
            UKCensusTest("Piece: RG9/480 Place: Gillingham -Kent Enumeration District: 14, Civil Parish: Gillingham Ecclesiastical Parish: Trinity,Folio: 42 Page: 20 Schedule: 117", CensusDate.UKCENSUS1861, "480", "42", "20");
            UKCensusTest("1901 census RG13/Piece3800/Folio112/Page16/SN#110", CensusDate.UKCENSUS1901, "3800", "112", "16");
            UKCensusTest("18910405 Census RG12 Piece: 3485, Folio: 134, Page: 30, Sch: 151 - Lancashire Barrow-In-Furness - John MARR", CensusDate.UKCENSUS1891, "3485", "134", "30");
            UKCensusTest("Census 1881 Thwaite, Suffolk RG11/1852/92/4", CensusDate.UKCENSUS1881, "1852", "92", "4");
            UKCensusTest("RG11, Piece 1208, Folio 60 face, Page 13, Schedule 71", CensusDate.UKCENSUS1881, "1208", "60", "13");
        }

        [TestMethod]
        public void CensusReferenceCanadaTests()
        {
            CanadianCensus("        123/A/55/35/1	Canada 1881", CensusDate.CANADACENSUS1881, "123", "A", "35", "1");
            CanadianCensus("        123/A/35/1	Canada 1881", CensusDate.CANADACENSUS1881, "123", "A", "35", "1");
            CanadianCensus("Year: 1881; Census Place: Richibucto, Kent, New Brunswick; Roll: C_13184; Page: 32; Family No: 144", CensusDate.CANADACENSUS1881, "C_13184", "32", "144");
            CanadianCensus("19210601 RG31 Canada Census Item 4360292 013/52/15/01 - British Columbia North Vancouver - Abraham COLEY", CensusDate.CANADACENSUS1921, "013", "52", "01", "15");
            CanadianCensus("1851 RG31 Item 1301944 38/558/396", CensusDate.CANADACENSUS1851, "38", "558", "396", "");
            CanadianCensus("1881 census - District 146/B, Page 59, Family 273 - living at Rainham, Haldimand, Ontario, Canada.", CensusDate.CANADACENSUS1881, "146", "B", "59", "273");
            CanadianCensus("1921 RG31 Canada Census Item 4360292 013/52/15/01", CensusDate.CANADACENSUS1921, "013", "52", "01", "15");
            CanadianCensus("19210601 Canada Census RG31-013-52-015-01 - North Vancouver - Abraham COLEY", CensusDate.CANADACENSUS1921, "013", "52", "01", "015");
            CanadianCensus("19110601 Census RG31 District 61 Sub-DIstrict 28 Family 66 Page 6 - Canada Ontario - Elizabeth Harriet DAVEY", CensusDate.CANADACENSUS1911, "61", "28", "6", "66");
            CanadianCensus("Year 1911 Census 65 - Little Current Algoma East Ontario Page 18 Family 159 ", CensusDate.CANADACENSUS1911, "", "18", "159");
            //Canadian1881Census("C_13266; Page 67; Family 301", "132", "C", "67", "301");
            //CanadianCensus("Event Place: Dumfries South, Brant North, Ontario, Canada\nDistrict Number: 160\nSub-District: C\nDivision: 2\nPage Number: 1\nFamily Number: 3\nAffiliate Film Number: C-13264", , CensusDate.CANADACENSUS1881, "C_13184", "1", "3");
        }

        [TestMethod]
        public void CensusReferenceOtherTests()
        {
            CensusReference censusRef;

            censusRef = new("I1", "826/134/7 England & Wales 1881", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1881));
            Assert.IsTrue(censusRef.Piece.Equals("826"));
            Assert.IsTrue(censusRef.Folio.Equals("134"));
            Assert.IsTrue(censusRef.Page.Equals("7"));

            censusRef = new("I1", "RG14; Piece: 21983", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1911));
            Assert.IsTrue(censusRef.Piece.Equals("21983"));
            Assert.IsTrue(censusRef.Status.Equals(CensusReference.ReferenceStatus.INCOMPLETE));
        }

        static void CanadianCensus(string reference, FactDate year, string ED, string SD, string page, string family)
        {
            CensusReference censusRef = new("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(year));
            Assert.IsTrue(censusRef.ED.Equals(ED));
            Assert.IsTrue(censusRef.SD.Equals(SD));
            Assert.IsTrue(censusRef.Page.Equals(page));
            Assert.IsTrue(censusRef.Family.Equals(family));
        }

        static void CanadianCensus(string reference, FactDate year, string Roll, string page, string family)
        {
            CensusReference censusRef = new("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(year));
            Assert.IsTrue(censusRef.Roll.Equals(Roll));
            Assert.IsTrue(censusRef.Page.Equals(page));
            Assert.IsTrue(censusRef.Family.Equals(family));
        }

        static void USCensusTest(string reference, FactDate year, string roll, string ED, string page)
        {
            CensusReference censusRef = new("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(year));
            Assert.IsTrue(censusRef.Roll.Equals(roll));
            Assert.IsTrue(censusRef.ED.Equals(ED));
            Assert.IsTrue(censusRef.Page.Equals(page));
        }

        static void Census1911Test(string reference, string piece, string schedule)
        {
            CensusReference censusRef = new("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1911));
            Assert.IsTrue(censusRef.Piece.Equals(piece));
            Assert.IsTrue(censusRef.Schedule.Equals(schedule));
        }

        static void Census1921Test(string reference, string book, string ed, string schedule)
        {
            CensusReference censusRef = new("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1921));
            Assert.IsTrue(censusRef.Book.Equals(book));
            Assert.IsTrue(censusRef.ED.Equals(ed));
            Assert.IsTrue(censusRef.Schedule.Equals(schedule));
        }
        static void Census1921Test(string reference, string rd, string sd)
        {
            CensusReference censusRef = new("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1921));
            Assert.IsTrue(censusRef.RD.Equals(rd));
            Assert.IsTrue(censusRef.SD.Equals(sd));
        }

        static void UKCensusTest(string reference, FactDate year, string piece, string folio, string page)
        {
            CensusHO107Test(reference, year, piece, string.Empty, folio, page);
        }

        static void CensusHO107Test(string reference, FactDate year, string piece, string book, string folio, string page)
        {
            CensusReference censusRef = new("I1", reference, false);
            Assert.IsTrue(censusRef.CensusYear.Equals(year));
            Assert.IsTrue(censusRef.Piece.Equals(piece));
            Assert.IsTrue(censusRef.Folio.Equals(folio));
            Assert.IsTrue(censusRef.Page.Equals(page));
            if (!string.IsNullOrEmpty(book))
                Assert.IsTrue(censusRef.Book.Equals(book));
        }

        static void ScottishCensusTest(string reference, FactDate year, string parish, string ED, string page)
        {
            CensusReference censusRef = new("I1", reference, false);
            Assert.AreEqual(year.BestYear, censusRef.CensusYear.BestYear);
            Assert.IsTrue(censusRef.Parish.Equals(parish));
            Assert.IsTrue(censusRef.ED.Equals(ED));
            Assert.IsTrue(censusRef.Page.Equals(page));
        }
    }
}
