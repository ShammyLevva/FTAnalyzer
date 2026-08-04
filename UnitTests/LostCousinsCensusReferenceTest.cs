using FTAnalyzer;
using FTAnalyzer.Exports;

namespace UnitTests
{
    /// <summary>
    /// Tests for LostCousinsCensusReference.Build(), which reproduces the reference format Lost
    /// Cousins' own website stores for a census entry (see LostCousinsClient.GetCensusSpecificFields),
    /// as opposed to CensusReference's own general-purpose Reference/CompactReference used for display
    /// everywhere else in the app. Each case here is a regression test for a specific mismatch that
    /// was silently failing to match a genuine Lost Cousins entry before the fix.
    /// </summary>
    [TestClass]
    public class LostCousinsCensusReferenceTest
    {
        public TestContext? TestContext { get; set; }

        [TestMethod]
        public void US1880BuildTests()
        {
            // Lost Cousins' USA1 field set for 1880 has no Enumeration District at all - only
            // Roll/Page - and the website's own roll number never carries a leading zero.
            CensusReference censusRef = new("Microfilm T9 Roll 0195 State Utah County Salt Lake ED 136 Page 71D Dwelling Number 186 Family 191", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.USCENSUS1880));
            Assert.AreEqual("195/71D", LostCousinsCensusReference.Build(censusRef));
        }

        [TestMethod]
        public void US1940BuildTests()
        {
            // Lost Cousins' USA4 field set for 1940 is Roll/ED/Page, and its roll number never
            // carries a leading zero even though a "m-t0627_NNNNN"-style citation can leave one
            // behind after the T627 prefix is stripped.
            CensusReference censusRef = new("Roll: m-t0627_02227; ED 1-7; Page 19A", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.USCENSUS1940));
            Assert.AreEqual("2227/1-7/19A", LostCousinsCensusReference.Build(censusRef));
        }

        [TestMethod]
        public void Scotland1881BuildTests()
        {
            // Lost Cousins' SCT1 field set for 1881 is the bare "RD/ED/Page" - there is no parish
            // name field on the website at all, unlike CensusReference's own general-purpose
            // CompactReference which includes the parish name (via ScottishParish.GetReference).
            CensusReference censusRef = new("1881 GROS 225 / 7 / 15", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.SCOTCENSUS1881));
            string expectedRD = ScottishParish.FindParishFromID(censusRef.Parish).RegistrationDistrict;
            Assert.AreEqual($"{expectedRD}/7/15", LostCousinsCensusReference.Build(censusRef));
        }

        [TestMethod]
        public void EW1911BuildTests_SchedulePresent()
        {
            // Lost Cousins' 0ENG field set for 1911 is Piece/Schedule - no Page field exists on the
            // website - and its piece number never carries a leading zero.
            CensusReference censusRef = new("RG14, Piece 00866, Registration District 10, Sub District 4, Enumeration District 25, Schedule No. 63", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1911));
            Assert.AreEqual("866/63", LostCousinsCensusReference.Build(censusRef));
        }

        [TestMethod]
        public void EW1911BuildTests_MissingScheduleFallsBackTo9999()
        {
            // Regression test for the missing "9999" schedule fallback: a citation that only
            // captured a page number (no schedule at all) previously produced "866/" - a value
            // Lost Cousins' own SN field could never equal - so this reference could never match
            // regardless of how well name/birth-year agreed. Lost Cousins uses "9999" itself when
            // a schedule number isn't known, so falling back to it here restores matchability.
            CensusReference censusRef = new("RG14 Piece 866 Page 63", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.UKCENSUS1911));
            Assert.AreEqual(string.Empty, censusRef.Schedule);
            Assert.AreEqual("866/9999", LostCousinsCensusReference.Build(censusRef));
        }

        [TestMethod]
        public void EW1911BuildTests_NoPageNoScheduleDoesNotFabricate9999()
        {
            // Guards the other side of the fallback above: when there's neither a schedule NOR a
            // page captured at all, Build() must not invent a "9999" schedule out of nothing.
            CensusReference censusRef = new("RG14; Piece: 21983", false);
            Assert.IsTrue(censusRef.Schedule.Equals(CensusReference.MISSING));
            Assert.AreEqual("21983/", LostCousinsCensusReference.Build(censusRef));
        }

        [TestMethod]
        public void Canada1881BuildTests_DistrictPresent()
        {
            // Lost Cousins' own reference for this census is "District/Page/Family" - this is only
            // matchable when a citation explicitly captured the District (CensusReference's own
            // "District NNN" pattern), which this reference does.
            CensusReference censusRef = new("1881 census - District 146/B, Page 59, Family 273 - living at Rainham, Haldimand, Ontario, Canada.", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.CANADACENSUS1881));
            Assert.IsTrue(censusRef.ED.Length > 0);
            Assert.AreEqual("146/59/273", LostCousinsCensusReference.Build(censusRef));
        }

        [TestMethod]
        public void Canada1881BuildTests_RollOnlyFallsBackUnchanged()
        {
            // The common Ancestry citation for this census only ever captures the microfilm Roll
            // number, a completely different identifier the District can't be derived from - Build()
            // must fall through to the general CompactReference rather than fabricating a
            // "/Page/Family" reference from an empty District.
            CensusReference censusRef = new("Year: 1881; Census Place: Richibucto, Kent, New Brunswick; Roll: C_13184; Page: 32; Family No: 144", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(CensusDate.CANADACENSUS1881));
            Assert.AreEqual(0, censusRef.ED.Length);
            Assert.AreEqual(censusRef.CompactReference, LostCousinsCensusReference.Build(censusRef));
        }

        [TestMethod]
        public void BuildReturnsEmptyStringForNullReference() => Assert.AreEqual(string.Empty, LostCousinsCensusReference.Build(null));
    }
}
