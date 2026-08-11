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

        // birthDate must predate censusYear - IsValidCensus requires FactErrorLevel.GOOD, and a
        // census fact for someone not yet born is exactly the kind of implausible-date error that
        // fails that check, which would silently turn a genuine parsing bug into a null
        // CensusReference indistinguishable from "the note simply didn't parse".
        static CensusReference? ParseNote(string note, string censusYear, CensusDate censusDate, string birthDate = "1 JAN 1850")
        {
            Individual person = ComparatorTestHelpers.MakeIndividualWithCensus("John", "Smith", "M", birthDate, censusYear, note);
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

        // Uses EW_CENSUS_1841_51_PATTERN8's bare "HO107/Piece/Book/Folio/Page" form rather than the
        // LC-specific pattern used for 1881/1911 (which only has room for 3 numbers, no Book field).
        [TestMethod]
        public void EnglandWales1841_RoundTrips()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("709/6/53/15", CensusDate.EWCENSUS1841);
            Assert.AreEqual("HO107/709/6/53/15", note);

            CensusReference? parsed = ParseNote(note!, "1841", CensusDate.EWCENSUS1841, "1 JAN 1800");

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            Assert.AreEqual("709", parsed.Piece);
            Assert.AreEqual("6", parsed.Book);
            Assert.AreEqual("53", parsed.Folio);
            Assert.AreEqual("15", parsed.Page);
        }

        // "21" is a real Registration District from ScottishParishes.xml (Kirkwall and St.Ola,
        // Orkney) - proves this isn't just regex-matching to GOOD, but that
        // ScottishParish.FindParishFromID genuinely resolves the RD Lost Cousins shows back to a
        // real parish rather than silently falling back to UNKNOWN_PARISH ("UNK"), which would
        // still produce Status GOOD but the wrong Build() output on the way back out.
        [TestMethod]
        public void Scotland1881_RoundTrips()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("21/5/12", CensusDate.SCOTCENSUS1881);
            Assert.AreEqual("21/5/12 Scotland 1881", note);

            CensusReference? parsed = ParseNote(note!, "1881", CensusDate.SCOTCENSUS1881);

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            Assert.AreEqual("21", parsed.Parish);
            Assert.AreEqual("5", parsed.ED);
            Assert.AreEqual("12", parsed.Page);
            Assert.AreEqual("21/5/12", LostCousinsCensusReference.Build(parsed));
        }

        // Uses the new LC_CENSUS_PATTERN_1880US, added specifically for this - every general-purpose
        // US census pattern requires an Enumeration District field, which Lost Cousins' own 1880
        // reference never has (Roll/Page only), so none of them could be reused the way
        // EW_CENSUS_1841_51_PATTERN8 was for 1841.
        [TestMethod]
        public void US1880_RoundTrips()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("195/71D", CensusDate.USCENSUS1880);
            Assert.AreEqual("195/71D US 1880", note);

            CensusReference? parsed = ParseNote(note!, "1880", CensusDate.USCENSUS1880);

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            Assert.AreEqual("195", parsed.Roll);
            Assert.AreEqual("71D", parsed.Page);
        }

        // The lookaround guard must reject a bare Roll/Page pair that's actually the middle of a
        // longer number run (e.g. part of a Piece/Folio/Page reference for a different census sharing
        // the same note) rather than a genuine standalone 1880 reference - the same protection
        // LC_CENSUS_PATTERN_1881CANADA already has (see CensusReferenceTest's
        // LcCensusPattern1881CanadaRegressionTests), and for the same reason: with only 2 numbers to
        // anchor on instead of 3, this pattern is the most exposed of the seven to a false positive.
        // Goes through the public CensusReference constructor directly (RegexPatterns is internal),
        // matching how CensusReferenceTest's own NoCanadianCensusMatch checks the sibling pattern.
        [TestMethod]
        public void US1880_DoesNotMatchInsideLongerNumberRun()
        {
            CensusReference censusRef = new("1/195/71D/9 US 1880", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(FactDate.UNKNOWN_DATE), $"Expected no census match but got {censusRef.CensusYear} with Roll={censusRef.Roll} Page={censusRef.Page}");
        }

        // Real defect found while building this pattern: the first version of the lookaround guard
        // only excluded a digit or slash (matching LC_CENSUS_PATTERN_1881CANADA's), which let it
        // match "1141/8" out of the middle of a 1940-style hyphenated ED ("8-14") - a hyphen doesn't
        // trip a [\d\/]-only guard. This exact text is CensusReferenceTest's
        // LcCensusPattern1940USRegressionTests deliberately-wrong-suffix case (a genuine 1940
        // Roll/ED/Page reference with "US 1880" on the end instead of "US 1940", used there to guard
        // against a different old bug) - it must still match nothing at all.
        [TestMethod]
        public void US1880_DoesNotMatchAcrossHyphenatedEnumerationDistrict()
        {
            CensusReference censusRef = new("1141/8-14/9A US 1880", false);
            Assert.IsTrue(censusRef.CensusYear.Equals(FactDate.UNKNOWN_DATE), $"Expected no census match but got {censusRef.CensusYear} with Roll={censusRef.Roll} Page={censusRef.Page}");
        }

        // Real-world defect this test caught before the ReferenceEquals fix: England & Wales 1911
        // and Ireland 1911 were both taken on "02 APR 1911", so CensusDate.Equals (date-only) treated
        // them as the same census - an Ireland 1911 entry would have silently gotten an England &
        // Wales 1911-formatted note (a Piece/Schedule pair) instead of the "nai"-prefixed reel number
        // IRELAND_CENSUS_1911_PATTERN actually looks for.
        [TestMethod]
        public void Ireland1911_RoundTrips()
        {
            string? note = LostCousinsCensusReference.SuggestedCitationNote("002247382", CensusDate.IRELANDCENSUS1911);
            Assert.AreEqual("nai002247382", note);

            CensusReference? parsed = ParseNote(note!, "1911", CensusDate.IRELANDCENSUS1911);

            Assert.IsNotNull(parsed);
            Assert.AreEqual(CensusReference.ReferenceStatus.GOOD, parsed.Status);
            // Leading zeros are significant - Lost Cousins' own reel number is a fixed-width 9-digit
            // field - so, unlike every other census's Piece/Roll/ED above, this must come back
            // unchanged rather than trimmed.
            Assert.AreEqual("002247382", parsed.Piece);
            Assert.AreEqual("002247382", LostCousinsCensusReference.Build(parsed));
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
