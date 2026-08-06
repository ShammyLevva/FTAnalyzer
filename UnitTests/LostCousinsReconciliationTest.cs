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
    }
}
