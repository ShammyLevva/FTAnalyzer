using System.Xml;
using FTAnalyzer;
using FTAnalyzer.Exports;

namespace UnitTests
{
    /// <summary>
    /// Covers the shared core both FTAnalyzer.Windows and FTAnalyzer.Web drive identically for the
    /// old-style National Archives of Ireland census page URL helper tool - see
    /// Exports/IrishCensusHelper.cs. This is deliberately kept separate from CensusReference's own
    /// tests: detection here reads CensusReference.Reference's fallback raw text rather than adding
    /// a new recognised pattern, per the explicit "don't change census reference parsing" scope.
    /// </summary>
    [TestClass]
    public class IrishCensusHelperTest
    {
        static Individual LoadIndividualWithCitation(string citation)
        {
            string ged = $"""
                0 HEAD
                1 CHAR UTF-8
                0 @I1@ INDI
                1 NAME George Fleming /Montgomery/
                1 SEX M
                1 BIRT
                2 DATE 1 JAN 1868
                1 CENS
                2 DATE 2 APR 1911
                2 PLAC Longford, Ireland
                2 SOUR @S1@
                3 PAGE {citation}
                0 @S1@ SOUR
                1 TITL 1911 Census of Ireland
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
                return ft.AllIndividuals.Single();
            }
            finally
            {
                File.Delete(path);
            }
        }

        [TestMethod]
        public void FindOldStyleReferences_ExtractsImageGroupFromRealWorldUrl()
        {
            Individual ind = LoadIndividualWithCitation(
                "http://www.census.nationalarchives.ie/pages/1911/Longford/Longford_No__1_Urban/Main_Street/651470/");

            List<IrishCensusHelperRecord> found = IrishCensusHelperScanner.FindOldStyleReferences([ind]);

            Assert.AreEqual(1, found.Count);
            Assert.AreEqual("651470", found[0].ImageGroup);
            Assert.AreEqual("Not yet checked", found[0].Status);
        }

        [TestMethod]
        public void FindOldStyleReferences_HandlesUrlWithoutTrailingSlash()
        {
            Individual ind = LoadIndividualWithCitation(
                "http://www.census.nationalarchives.ie/pages/1911/Dublin/Dublin_North/Some_Street/12345");

            List<IrishCensusHelperRecord> found = IrishCensusHelperScanner.FindOldStyleReferences([ind]);

            Assert.AreEqual(1, found.Count);
            Assert.AreEqual("12345", found[0].ImageGroup);
        }

        // The new-style reel URL is already fully resolved and matched by CensusReference itself
        // (IRELAND_CENSUS_1911_PATTERN) - this helper must not also flag it as needing resolution.
        [TestMethod]
        public void FindOldStyleReferences_IgnoresAlreadyResolvedReelUrl()
        {
            Individual ind = LoadIndividualWithCitation(
                "https://nai.prod.derilinx.com/census/image/nai002908773.pdf");

            List<IrishCensusHelperRecord> found = IrishCensusHelperScanner.FindOldStyleReferences([ind]);

            Assert.AreEqual(0, found.Count);
        }

        [TestMethod]
        public void FindOldStyleReferences_IgnoresUnrelatedCitation()
        {
            Individual ind = LoadIndividualWithCitation("Some unrelated family history note");

            List<IrishCensusHelperRecord> found = IrishCensusHelperScanner.FindOldStyleReferences([ind]);

            Assert.AreEqual(0, found.Count);
        }

        // Captured live from api-census.nationalarchives.ie during development of this feature -
        // the exact body returned for image_group=651470 (truncated to the fields that matter),
        // confirmed against the real household (George Fleming Montgomery, Longford).
        const string RealPopulatedResponse = """
            {"results":[{"id":3527316,"county":"Longford","surname":"Montgomery","firstname":"George Fleming",
            "relation_to_head_updated":"Head of Family","images":[
            {"form":"Form A","side":"1","id":"nai002908773","url":"/census/image/nai002908773.pdf"},
            {"form":"Form A","side":"2","id":"nai002908774","url":"/census/image/nai002908774.pdf"},
            {"form":"Form N","side":"1","id":"nai002908688","url":"/census/image/nai002908688.pdf"}]}]}
            """;

        [TestMethod]
        public void ExtractFormAUrl_ResolvesRealPopulatedResponse()
        {
            string? url = IrishCensusHelperClient.ExtractFormAUrl(RealPopulatedResponse);

            Assert.AreEqual("https://nai.prod.derilinx.com/census/image/nai002908773.pdf", url);
        }

        // Captured live for a made-up image_group with no matching household - the archive
        // returns a clean 200 with an empty array rather than a 404, so this must resolve to null
        // rather than throwing.
        const string RealEmptyResponse = """{"results":[],"meta":{"count":0,"next":null,"prev":null}}""";

        [TestMethod]
        public void ExtractFormAUrl_ReturnsNullForEmptyResults()
        {
            string? url = IrishCensusHelperClient.ExtractFormAUrl(RealEmptyResponse);

            Assert.IsNull(url);
        }

        [TestMethod]
        public void ExtractFormAUrl_ReturnsNullWhenHouseholdHasNoFormAImage()
        {
            const string noFormA = """
                {"results":[{"id":1,"images":[{"form":"Form N","side":"1","id":"nai000000001","url":"/census/image/nai000000001.pdf"}]}]}
                """;

            string? url = IrishCensusHelperClient.ExtractFormAUrl(noFormA);

            Assert.IsNull(url);
        }
    }
}
