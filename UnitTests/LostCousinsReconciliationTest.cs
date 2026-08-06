using FTAnalyzer;
using FTAnalyzer.Exports;

namespace UnitTests
{
    /// <summary>
    /// Tests for LostCousinsReconciliation.FindPossibleMatches - the lower-confidence, name/birth-year
    /// -only fallback used when a website "My Ancestors" entry couldn't be matched by census reference
    /// (see Reconcile/FindMatch). Real-world bug report: a household of 11 siblings sharing one census
    /// reference matched only 4 by reference; several of the rest were suggested as "possible matches"
    /// to entirely unrelated people elsewhere in the tree who happened to share a forename and exact
    /// birth year - e.g. a household's "Martha" (surname Procter) was suggested as an unrelated "Martha
    /// Pennington". FindPossibleMatches' first two tiers matched on full forename(s) + birth year alone,
    /// with no surname check at all (unlike its third, loosest tier), which is exactly how that
    /// happened.
    /// </summary>
    [TestClass]
    public class LostCousinsReconciliationTest
    {
        public TestContext? TestContext { get; set; }

        [TestMethod]
        public void FindPossibleMatches_DoesNotSuggestUnrelatedSameForenameSameBirthYearAcrossTree()
        {
            // Regression test for the "Martha Pennington" bug: an unrelated person elsewhere in the
            // tree with a matching full forename and exact birth year must not be suggested just
            // because she's the only candidate left once the real sibling's own citation ruled her out
            // of this search pool - her surname doesn't match the website entry's at all.
            LostCousin website = new("Procter, Martha", "1867", "186/10/41", "England 1881", null!, false);
            CensusIndividual unrelated = ComparatorTestHelpers.MakeCensusIndividual("Martha", "Pennington", "1867", "F001");

            List<LostCousinsReconciliation.PossibleMatch> matches =
                LostCousinsReconciliation.FindPossibleMatches([website], [unrelated]);

            Assert.AreEqual(0, matches.Count);
        }

        [TestMethod]
        public void FindPossibleMatches_StillSuggestsSameHouseholdFullNameMatch()
        {
            // Positive control: a full forename(s) + exact birth year + matching surname match must
            // still be suggested - the surname check must not be so strict it breaks the legitimate
            // case (a household member whose own citation just isn't good enough to build a reference).
            LostCousin website = new("Procter, Walter George Edward", "1878", "186/10/41", "England 1881", null!, false);
            CensusIndividual sibling = ComparatorTestHelpers.MakeCensusIndividual("Walter George Edward", "Procter", "1878", "F001");

            List<LostCousinsReconciliation.PossibleMatch> matches =
                LostCousinsReconciliation.FindPossibleMatches([website], [sibling]);

            Assert.AreEqual(1, matches.Count);
            Assert.AreEqual(sibling, matches[0].Individual);
        }

        [TestMethod]
        public void FindPossibleMatches_SurnameMismatchStillFallsThroughToNoMatchWhenCorrectSiblingAbsent()
        {
            // Same shape as the regression test above but via near-miss metaphone surnames, confirming
            // SurnamesMatch's fuzzy comparison (not just exact equality) is what's gating tier 1/2 now,
            // not merely an exact-string check that a real near-miss spelling would slip past anyway.
            LostCousin website = new("Procter, Phoebe", "1879", "186/10/41", "England 1881", null!, false);
            CensusIndividual unrelated = ComparatorTestHelpers.MakeCensusIndividual("Phoebe", "McKenna", "1879", "F002");

            List<LostCousinsReconciliation.PossibleMatch> matches =
                LostCousinsReconciliation.FindPossibleMatches([website], [unrelated]);

            Assert.AreEqual(0, matches.Count);
        }

        [TestMethod]
        public void Reconcile_DoesNotStealMatchForUnrelatedCandidateSharingSamePageReference()
        {
            // Regression test for a real bug: EW1841's Piece/Book/Folio/Page reference identifies a
            // whole census PAGE, which can genuinely hold more than one household. Here "Chadwick,
            // James" and "Chadwick, Mary Ellen" are two members of one real family on Lost Cousins,
            // both sharing reference 511/8/6/6 - but an unrelated "Mary Ireland" elsewhere in the tree
            // happens to have a citation that resolves to the exact same page reference too (a genuine
            // data coincidence, not a code bug), and shares Mary Ellen's forename and a close-enough
            // birth year. Before the surname-aware dedup, whichever of the two candidates happened to
            // be processed first could silently steal the "Chadwick, Mary Ellen" website entry - and,
            // via CreateConfirmationFact, would have had a Lost Cousins fact fabricated onto the wrong
            // person. Mary Ireland's own surname doesn't match, so she must lose the tie-break.
            LostCousin jamesWebsite = new("Chadwick, James", "1816", "511/8/6/6", "England 1841", null!, false);
            LostCousin maryEllenWebsite = new("Chadwick, Mary Ellen", "1840", "511/8/6/6", "England 1841", null!, false);

            Individual james = ComparatorTestHelpers.MakeIndividualWithCensus(
                "James", "Chadwick", "M", "1 JAN 1816", "1841",
                "Database online. Class: HO107; Piece 511; Book: 8; Folio: 6; Page: 6.", "I001");
            Family family = new(james, "F010");
            Individual maryEllen = ComparatorTestHelpers.MakeIndividualWithCensus(
                "Mary Ellen", "Chadwick", "F", "1 JAN 1840", "1841",
                "Database online. Class: HO107; Piece 511; Book: 8; Folio: 6; Page: 6.", "I002");
            family.Children.Add(maryEllen);
            CensusFamily censusFamily = new(family, CensusDate.UKCENSUS1841);

            // Mary Ireland: unrelated, own solo family, same page reference by coincidence.
            Individual maryIreland = ComparatorTestHelpers.MakeIndividualWithCensus(
                "Mary", "Ireland", "F", "1 JAN 1838", "1841",
                "Database online. Class: HO107; Piece 511; Book: 8; Folio: 6; Page: 6.", "I003");
            Family irelandFamily = new(maryIreland, "F011");
            CensusFamily irelandCensusFamily = new(irelandFamily, CensusDate.UKCENSUS1841);

            List<CensusIndividual> candidates = [.. censusFamily.Members, .. irelandCensusFamily.Members];

            var (stillMissing, confirmed) = LostCousinsReconciliation.Reconcile([jamesWebsite, maryEllenWebsite], candidates);

            CensusIndividual maryEllenCandidate = censusFamily.Children.Single(c => c.IndividualID == "I002");
            CensusIndividual maryIrelandCandidate = irelandCensusFamily.Wife!;

            Assert.IsTrue(confirmed.Any(m => m.WebsiteEntry == maryEllenWebsite && m.Individual == maryEllenCandidate),
                "the real Mary Ellen Chadwick must win the match");
            Assert.IsFalse(confirmed.Any(m => m.Individual == maryIrelandCandidate),
                "Mary Ireland must not be confirmed against anyone's entry");
            Assert.IsTrue(stillMissing.Contains(maryIrelandCandidate),
                "Mary Ireland goes back to still-missing rather than being silently dropped");
        }
    }
}
