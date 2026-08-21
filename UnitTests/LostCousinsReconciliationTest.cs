using System.Xml;
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

        [TestMethod]
        public void Reconcile_FallsBackToOwnReferenceWhenHeadOfHouseholdWasElsewhereThatCensus()
        {
            // Regression test for a real bug report: a father remarried after his first wife died,
            // and by the 1911 census was living with his second wife at a different address under a
            // different reference - but he's still alive at the census date, so CensusFamily.
            // HeadOfHousehold (Husband ?? Wife ?? eldest child) picks him regardless, with no check
            // that he was actually enumerated WITH this family that year. His son's HouseholdCensusReference
            // then silently resolved to the father's unrelated reference instead of the son's own -
            // correct - one, so a real My Ancestors entry ("Burness, John George", ref 30498/158) never
            // matched despite the son's own citation being exactly right.
            Individual father = ComparatorTestHelpers.MakeIndividualWithCensus(
                "John", "Burness", "M", "1 JAN 1854", "1911",
                "RG14PN30546 RG78PN1751 RD558 SD1 ED8 SN55", "IFATHER");
            Family family = new(father, "F900");
            Individual son = ComparatorTestHelpers.MakeIndividualWithCensus(
                "John George", "Burness", "M", "9 SEP 1878", "1911",
                "RG14PN30498 RG78PN1749 RD557 SD3 ED3 SN158", "ISON");
            family.Children.Add(son);
            CensusFamily censusFamily = new(family, CensusDate.EWCENSUS1911);

            LostCousin website = new("Burness, John George", "1878", "30498/158", "England 1911", null!, false);

            var (stillMissing, confirmed) = LostCousinsReconciliation.Reconcile([website], [.. censusFamily.Members]);

            CensusIndividual sonCandidate = censusFamily.Children.Single(c => c.IndividualID == "ISON");
            Assert.IsTrue(confirmed.Any(m => m.WebsiteEntry == website && m.Individual == sonCandidate),
                "the son's own correct reference must be tried once the head of household's (unrelated) reference fails to match");
            Assert.IsFalse(stillMissing.Contains(sonCandidate));
        }

        [TestMethod]
        public void Reconcile_MatchesWifeEnteredUnderMaidenNameOnWebsite()
        {
            // Regression test for a real bug report: a wife recorded on the 1911 census under her
            // married name ("Jane Bassett") but corrected on Lost Cousins to her maiden name ("Jane
            // Smith", per the user's own tree). Her HouseholdCensusReference (via her husband, head
            // of household) matched the website's single entry under that reference perfectly - but
            // that entry is the ONLY one sharing the reference, so FindMatch's single-candidate
            // shortcut returned it with no name check at all, and BOTH husband and wife independently
            // "matched" it via their shared reference. Reconcile's tie-break between them then fell
            // to SurnamesMatch, which only compared against the wife's married name at the census
            // date ("Bassett") - never her maiden name ("Smith") - so it disagreed for both husband
            // and wife equally, and the husband won the tie purely by iteration order, leaving the
            // wife - the person actually on the website - in stillMissing.
            const string ged = """
                0 HEAD
                1 CHAR UTF-8
                0 @I1@ INDI
                1 NAME John /Bassett/
                1 SEX M
                1 BIRT
                2 DATE 1 JAN 1875
                1 CENS
                2 DATE 1911
                2 PLAC 1 Test Street, England
                2 SOUR @S1@
                3 PAGE RG14PN30722 RG78PN1749 RD557 SD3 ED3 SN475
                1 FAMS @F1@
                0 @I2@ INDI
                1 NAME Jane /Smith/
                1 SEX F
                1 BIRT
                2 DATE 1 JAN 1861
                1 FAMS @F1@
                0 @F1@ FAM
                1 HUSB @I1@
                1 WIFE @I2@
                1 MARR
                2 DATE 1 JAN 1895
                0 @S1@ SOUR
                1 TITL 1911 Census of Great Britain
                0 TRLR

                """;
            string path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.ged");
            try
            {
                File.WriteAllText(path, ged);

                FamilyTree ft = FamilyTree.CreateInstance();
                FamilyTree.SetInstance(ft);
                ft.LoadStandardisedNames(AppContext.BaseDirectory);

                var textProgress = new Progress<string>(_ => { });
                var pctProgress = new Progress<int>(_ => { });
                XmlDocument? doc;
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    doc = ft.LoadTreeHeader(Path.GetFileName(path), fs, textProgress, pctProgress);
                ft.LoadTreeSources(doc!, pctProgress, textProgress);
                ft.LoadTreeIndividuals(doc!, pctProgress, textProgress);
                ft.LoadTreeFamilies(doc!, pctProgress, textProgress);
                ft.LoadTreeRelationships(doc!, pctProgress, textProgress);

                List<CensusIndividual> members = [.. ft.GetAllCensusFamilies(CensusDate.EWCENSUS1911, true, false)
                    .SelectMany(f => f.Members)];
                CensusIndividual husband = members.Single(m => m.CensusStatus == CensusIndividual.HUSBAND);
                CensusIndividual wife = members.Single(m => m.CensusStatus == CensusIndividual.WIFE);

                // Only Jane appears on the website under this reference - her husband isn't on Lost Cousins.
                LostCousin website = new("Smith, Jane", "1861", "30722/475", "England 1911", null!, false);

                var (stillMissing, confirmed) = LostCousinsReconciliation.Reconcile([website], members);

                Assert.IsTrue(confirmed.Any(m => m.WebsiteEntry == website && m.Individual == wife),
                    "the wife must win the match via her maiden name, not lose it to her husband by tie-break order");
                Assert.IsTrue(stillMissing.Contains(husband),
                    "the husband was never actually on the website and must not be confirmed against his wife's entry");
            }
            finally
            {
                File.Delete(path);
            }
        }

        // Real-world report: a household's shared 1841 reference (built from the head of household's
        // own citation) matched to "Southern, Peter" - but the confirmed match was his father John,
        // not Peter. FindMatchByReference's sameCensus.Count==1 shortcut hands the sole website entry
        // to whichever household member asks first, with no name check at all, so both father and son
        // independently "matched" it via HouseholdCensusReference; Reconcile's tie-break used to
        // disambiguate on surname alone, which ties every time between two people who share one (as a
        // father and son always do) - so the head of household won purely by being processed first,
        // even though the website entry's forename was "Peter", not "John".
        [TestMethod]
        public void Reconcile_PicksHouseholdMemberWhoseNameActuallyMatches()
        {
            const string ged = """
                0 HEAD
                1 CHAR UTF-8
                0 @I1@ INDI
                1 NAME John /Southern/
                1 SEX M
                1 BIRT
                2 DATE 1 JAN 1795
                1 CENS
                2 DATE 6 JUN 1841
                2 PLAC England
                2 SOUR @S1@
                3 PAGE HO107/511/8/5/5
                1 FAMS @F1@
                0 @I2@ INDI
                1 NAME Mary /Southern/
                1 SEX F
                1 BIRT
                2 DATE 1 JAN 1800
                1 FAMS @F1@
                0 @I3@ INDI
                1 NAME Peter /Southern/
                1 SEX M
                1 BIRT
                2 DATE 1 JAN 1826
                1 CENS
                2 DATE 6 JUN 1841
                2 PLAC England
                2 SOUR @S1@
                3 PAGE HO107/511/8/6/4
                1 FAMC @F1@
                0 @F1@ FAM
                1 HUSB @I1@
                1 WIFE @I2@
                1 CHIL @I3@
                2 _FREL Natural
                2 _MREL Natural
                1 MARR
                2 DATE 1 JAN 1820
                0 @S1@ SOUR
                1 TITL 1841 Census of England
                0 TRLR

                """;
            string path = Path.Combine(Path.GetTempPath(), $"{Guid.NewGuid()}.ged");
            try
            {
                File.WriteAllText(path, ged);

                FamilyTree ft = FamilyTree.CreateInstance();
                FamilyTree.SetInstance(ft);
                ft.LoadStandardisedNames(AppContext.BaseDirectory);

                var textProgress = new Progress<string>(_ => { });
                var pctProgress = new Progress<int>(_ => { });
                XmlDocument? doc;
                using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                    doc = ft.LoadTreeHeader(Path.GetFileName(path), fs, textProgress, pctProgress);
                ft.LoadTreeSources(doc!, pctProgress, textProgress);
                ft.LoadTreeIndividuals(doc!, pctProgress, textProgress);
                ft.LoadTreeFamilies(doc!, pctProgress, textProgress);
                ft.LoadTreeRelationships(doc!, pctProgress, textProgress);

                List<CensusIndividual> members = [.. ft.GetAllCensusFamilies(CensusDate.EWCENSUS1841, true, false)
                    .SelectMany(f => f.Members)];
                CensusIndividual father = members.Single(m => m.CensusStatus == CensusIndividual.HUSBAND);
                CensusIndividual son = members.Single(m => m.CensusStatus == CensusIndividual.CHILD);

                // Peter's own citation (511/8/6/4, the next page the household overflowed onto) never
                // comes into it here - Lost Cousins only ever stores the household's own reference
                // (511/8/5/5), which is why the website entry's reference must be his father's.
                LostCousin website = new("Southern, Peter", "1826", "511/8/5/5", "England & Wales 1841", null!, false);

                var (stillMissing, confirmed) = LostCousinsReconciliation.Reconcile([website], members);

                Assert.IsTrue(confirmed.Any(m => m.WebsiteEntry == website && m.Individual == son),
                    "Peter must win the match via his own forename, not lose it to his father by tie-break order");
                Assert.IsTrue(stillMissing.Contains(father),
                    "the father was never named on the website and must not be confirmed against his son's entry");
            }
            finally
            {
                File.Delete(path);
            }
        }
    }
}
