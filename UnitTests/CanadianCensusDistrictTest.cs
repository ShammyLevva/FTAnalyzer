using FTAnalyzer;

namespace UnitTests
{
    /// <summary>
    /// Tests for CanadianCensusDistrict.FindDistrictNumber(), which resolves an 1881 census District
    /// Number from a sub-district (township/parish) name using Library and Archives Canada's own
    /// district/sub-district finding aid (Resources/CanadianCensusDistricts1881.xml).
    /// </summary>
    [TestClass]
    public class CanadianCensusDistrictTest
    {
        public TestContext? TestContext { get; set; }

        [TestMethod]
        public void ResolvesUnambiguousNameWithCorrectProvince() =>
            Assert.AreEqual("186", CanadianCensusDistrict.FindDistrictNumber("Woodlands", "Manitoba"));

        [TestMethod]
        public void ResolvesUnambiguousNameWithoutProvince() =>
            // "Richibucto" only exists in one district nationally, so the name-only fallback works
            // even when no province (or the wrong one) is supplied.
            Assert.AreEqual("34", CanadianCensusDistrict.FindDistrictNumber("Richibucto", string.Empty));

        [TestMethod]
        public void IsCaseAndWhitespaceInsensitive() =>
            Assert.AreEqual("186", CanadianCensusDistrict.FindDistrictNumber("  woodlands  ", " MANITOBA "));

        [TestMethod]
        public void AmbiguousNameWithMatchingProvinceResolves()
        {
            // "St. Paul" exists in both Quebec (Joliette) and Manitoba (Lisgar) - supplying the
            // correct province disambiguates it.
            Assert.AreEqual("87", CanadianCensusDistrict.FindDistrictNumber("St. Paul", "Quebec"));
            Assert.AreEqual("185", CanadianCensusDistrict.FindDistrictNumber("St. Paul", "Manitoba"));
        }

        [TestMethod]
        public void AmbiguousNameWithoutMatchingProvinceReturnsEmpty() =>
            // Never guess between the two "St. Paul" candidates when the supplied province doesn't
            // match either of them (or none is supplied at all).
            Assert.AreEqual(string.Empty, CanadianCensusDistrict.FindDistrictNumber("St. Paul", "Nova Scotia"));

        [TestMethod]
        public void UnknownNameReturnsEmpty() =>
            Assert.AreEqual(string.Empty, CanadianCensusDistrict.FindDistrictNumber("Notarealplace", "Manitoba"));

        [TestMethod]
        public void BlankNameReturnsEmpty()
        {
            Assert.AreEqual(string.Empty, CanadianCensusDistrict.FindDistrictNumber(string.Empty, "Manitoba"));
            Assert.AreEqual(string.Empty, CanadianCensusDistrict.FindDistrictNumber("   ", "Manitoba"));
        }

        [TestMethod]
        public void QuesnelCorrectionResolves() =>
            // Regression test for a data-quality fix: LAC's own published page had sub-district B of
            // Cariboo, BC (District 188) rendered as the literal placeholder text "k" - corrected to
            // "Quesnel / Quesnelle Mouth" per FindMyPast's transcription before this was shipped.
            Assert.AreEqual("188", CanadianCensusDistrict.FindDistrictNumber("Quesnel / Quesnelle Mouth", "British Columbia"));
    }
}
