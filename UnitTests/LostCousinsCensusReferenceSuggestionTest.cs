using FTAnalyzer;
using FTAnalyzer.Exports;

namespace UnitTests
{
    /// <summary>
    /// Audits LostCousinsCensusReference.SuggestedCitationNote end to end: for each census year it
    /// claims to support, the exact note text it returns must - if pasted into that person's source
    /// citation, the GEDCOM re-exported, and the file re-scanned - parse back into a GOOD
    /// CensusReference with the same field values the website reference described. This is the
    /// contract the Suggested Reference Note column on the Lost Cousins sync page relies on; a note
    /// that CensusReference itself wouldn't recognise fails the user, not just the column.
    /// </summary>
    [TestClass]
    public class LostCousinsCensusReferenceSuggestionTest
    {
        public TestContext? TestContext { get; set; }

        static CensusReference? ParseNote(string note, string censusYear, CensusDate censusDate)
        {
            Individual person = ComparatorTestHelpers.MakeIndividualWithCensus("John", "Smith", "M", "1 JAN 1850", censusYear, note);
            Family family = new(person, "F001");
            CensusFamily censusFamily = new(family, censusDate);
            return censusFamily.Husband!.CensusReference;
        }

        [TestMethod]
        public void EnglandWales1881_RoundTrips()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("6874/215/9", CensusDate.EWCENSUS1881);
            Assert.AreEqual("6874/215/9 England & Wales 1881", note);

            CensusReference? parsed = ParseNote(note!, "1881", CensusDate.EWCENSUS1881);

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            Assert.AreEqual("6874", parsed.Piece);
            Assert.AreEqual("215", parsed.Folio);
            Assert.AreEqual("9", parsed.Page);
        }

        [TestMethod]
        public void EnglandWales1911_RoundTrips()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("6047/9", CensusDate.EWCENSUS1911);
            Assert.AreEqual("6047/9 England & Wales 1911", note);

            CensusReference? parsed = ParseNote(note!, "1911", CensusDate.EWCENSUS1911);

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            Assert.AreEqual("6047", parsed.Piece);
            Assert.AreEqual("9", parsed.Schedule);
        }

        [TestMethod]
        public void Canada1881_RoundTrips()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("146/10/41", CensusDate.CANADACENSUS1881);
            Assert.AreEqual("146/10/41 Canada 1881", note);

            CensusReference? parsed = ParseNote(note!, "1881", CensusDate.CANADACENSUS1881);

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            Assert.AreEqual("146", parsed.ED);
            Assert.AreEqual("10", parsed.Page);
            Assert.AreEqual("41", parsed.Family);
        }

        [TestMethod]
        public void US1940_RoundTrips()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("1234/9-13/45", CensusDate.USCENSUS1940);
            Assert.AreEqual("1234/9-13/45 US 1940", note);

            CensusReference? parsed = ParseNote(note!, "1940", CensusDate.USCENSUS1940);

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            Assert.AreEqual("1234", parsed.Roll);
            Assert.AreEqual("9-13", parsed.ED);
            Assert.AreEqual("45", parsed.Page);
        }

        // The website's own page rendering isn't guaranteed to pack the reference tightly - nothing
        // stops a stray space landing around a separator when it's scraped. SuggestedCitationNote
        // must strip that out itself rather than relying on luck, since LC_CENSUS_PATTERN_* requires
        // the digits and slashes to run together with nothing else between them.
        [TestMethod]
        public void StripsWhitespaceFromWebsiteReferenceBeforeBuildingNote()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote(" 146 / 10 / 41 ", CensusDate.CANADACENSUS1881);
            Assert.AreEqual("146/10/41 Canada 1881", note);

            CensusReference? parsed = ParseNote(note!, "1881", CensusDate.CANADACENSUS1881);

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            Assert.AreEqual("146", parsed.ED);
            Assert.AreEqual("10", parsed.Page);
            Assert.AreEqual("41", parsed.Family);
        }

        // Census years where no single note can capture every field the matching pattern needs
        // (missing Book field, needs a Parish name instead of a district number, or no format
        // exists at all) must say so plainly rather than hand back a note that looks plausible but
        // would parse wrong or not at all.
        [TestMethod]
        public void EnglandWales1841_HasNoQuickFix()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("6874/12/215/9", CensusDate.EWCENSUS1841);
            Assert.AreEqual("No quick-fix note for this census - see the reference format guide", note);
        }

        [TestMethod]
        public void Scotland1881_HasNoQuickFix()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("644/2/9", CensusDate.SCOTCENSUS1881);
            Assert.AreEqual("No quick-fix note for this census - see the reference format guide", note);
        }

        [TestMethod]
        public void US1880_HasNoQuickFix()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("1234/45", CensusDate.USCENSUS1880);
            Assert.AreEqual("No quick-fix note for this census - see the reference format guide", note);
        }

        // Real-world defect this test caught before the ReferenceEquals fix: England & Wales 1911
        // and Ireland 1911 were both taken on "02 APR 1911", so CensusDate.Equals (date-only) treated
        // them as the same census - an Ireland 1911 entry would have silently gotten an England &
        // Wales 1911-formatted note. Family Tree Analyzer doesn't match Ireland 1911 at all yet (see
        // Instructions#lc-reference-formats), so "no quick fix" is the correct answer regardless.
        [TestMethod]
        public void Ireland1911_HasNoQuickFix()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("123/45", CensusDate.IRELANDCENSUS1911);
            Assert.AreEqual("No quick-fix note for this census - see the reference format guide", note);
        }

        // Newfoundland 1921 has no CensusDate constant at all - LostCousin's "scraped from website"
        // constructor leaves CensusDate null for it (no "Newfoundland" branch in its census-name
        // matching) - so this is the actual value SuggestedCitationNote sees for a Newfoundland entry.
        [TestMethod]
        public void NullCensusDate_HasNoQuickFix()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("123/45", null);
            Assert.AreEqual("No quick-fix note for this census - see the reference format guide", note);
        }
    }
}
