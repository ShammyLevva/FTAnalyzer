using FTAnalyzer;
using FTAnalyzer.Properties;

namespace UnitTests
{
    /// <summary>
    /// Summary description for FactLocationTest
    /// </summary>
    [TestClass]
    public class FactLocationTest
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
        public void FactLocationConstructorTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            FactLocation factLocation;
            //            factLocation = FactLocation.GetLocation("Aberdeen, Scotland");
            //            Assert.IsTrue(factLocation.ToString().Equals("Aberdeen, Aberdeenshire, Scotland"));

            factLocation = FactLocation.GetLocation("America");
            Assert.IsTrue(factLocation.ToString().Equals("United States"));

            // check for default strip empty locations
            GeneralSettings.Default.AllowEmptyLocations = false;
            factLocation = FactLocation.GetLocation("Parish Church of St Mary, , South Stoneham, Hampshire, ENG");
            Assert.IsTrue(factLocation.ToString().Equals("Parish Church of St Mary, South Stoneham, Hampshire, England"));

            factLocation = FactLocation.GetLocation(", , West End, Hampshire, ENG");
            Assert.IsTrue(factLocation.ToString().Equals("West End, Hampshire, England"));

            factLocation = FactLocation.GetLocation(", Fareham Registration District, , Hampshire, ENG");
            Assert.IsTrue(factLocation.ToString().Equals("Fareham Registration District, Hampshire, England"));

            // check when allowing empty locations
            GeneralSettings.Default.AllowEmptyLocations = true;
            factLocation = FactLocation.GetLocation("Parish Church of St Mary, , South Stoneham, Hampshire, ENG");
            Assert.IsTrue(factLocation.ToString().Equals("Parish Church of St Mary, , South Stoneham, Hampshire, England"));

            factLocation = FactLocation.GetLocation(", , West End, Hampshire, ENG");
            Assert.IsTrue(factLocation.ToString().Equals("West End, Hampshire, England"));

            factLocation = FactLocation.GetLocation(", Fareham Registration District, , Hampshire, ENG");
            Assert.IsTrue(factLocation.ToString().Equals("Fareham Registration District, , Hampshire, England"));

            factLocation = FactLocation.GetLocation("U.S.A.");
            Assert.IsTrue(factLocation.ToString().Equals("United States"));

            factLocation = FactLocation.GetLocation("4 Old Grey Street, Sunderland, Co Durham");
            Assert.IsTrue(factLocation.ToString().Equals("4 Old Grey Street, Sunderland, County Durham, England"));

            // region/country abbreviation lookups must be case insensitive
            factLocation = FactLocation.GetLocation("Boston, Ma");
            Assert.IsTrue(factLocation.ToString().Equals("Boston, Massachusetts, United States"));

            factLocation = FactLocation.GetLocation("Boston, ma");
            Assert.IsTrue(factLocation.ToString().Equals("Boston, Massachusetts, United States"));
        }

        // Georgia the country and Georgia the US state share a name. KNOWN_COUNTRIES deliberately
        // omits Georgia (see Countries.IsGeorgiaCountry) rather than trying to guess which one a
        // bare "Georgia" means. FactLocationFixes.xml even has a
        // <CountryToRegion region="Georgia" country="United States" /> rule that would auto-promote
        // it to the US state - but that rule is commented out (deliberately, presumably to avoid
        // misreading a genuine Georgia-the-country entry), so a bare "Georgia" with no country
        // given parses as the literal country reading, not the US state. Whatever the reason, the
        // one thing that must hold either way is that it's never treated as garbage/unknown data.
        [TestMethod]
        public void GeorgiaMissingUSATest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            // No country given at all - stays as the literal country reading.
            FactLocation factLocation = FactLocation.GetLocation("Georgia");
            Assert.AreEqual("Georgia", factLocation.ToString());
            Assert.AreEqual("Georgia", factLocation.Country);
            Assert.IsTrue(factLocation.IsKnownCountry);

            // A town that isn't on the recognised-large-city list (see
            // GeorgiaKnownCityPromotedTest) stays exactly as unpromoted too - we only disambiguate
            // cities we actually recognise, not every possible Georgia town.
            factLocation = FactLocation.GetLocation("Podunk, Georgia");
            Assert.AreEqual("Podunk, Georgia", factLocation.ToString());
            Assert.AreEqual("Georgia", factLocation.Country);
            Assert.IsTrue(factLocation.IsKnownCountry);
        }

        // "<city>, Georgia" is unambiguous whenever the city is a recognised large Georgia city
        // (FactLocationFixes.xml's Georgia CityAddCountry entries, consulted by
        // FactLocation.ShiftGeorgiaCityToRegion) - promote those specifically to the US state
        // reading even with "USA" missing, since there's no clash risk once "Georgia" is already
        // the literal second part of the location.
        [TestMethod]
        public void GeorgiaKnownCityPromotedTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            FactLocation factLocation = FactLocation.GetLocation("Savannah, Georgia");
            Assert.AreEqual("Savannah, Georgia, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);
            Assert.IsTrue(factLocation.IsKnownCountry);

            factLocation = FactLocation.GetLocation("Atlanta, Georgia");
            Assert.AreEqual("Atlanta, Georgia, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);

            // City name matching is case insensitive, same as CITY_ADD_COUNTRY's other entries.
            factLocation = FactLocation.GetLocation("savannah, georgia");
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);

            // A bare mention of one of these city names with NO country at all must NOT be
            // auto-assumed to be Georgia, even for a city that IS on the recognised list - these
            // entries are deliberately excluded from the generic bare-city COUNTRY_SHIFTS merge
            // (see LoadConversions), only ever consulted via ShiftGeorgiaCityToRegion once
            // "Georgia" is already present. (The riskiest names - Rome, Athens, Dublin, Albany,
            // Decatur, Brunswick, Roswell - aren't on the list at all; see FactLocationFixes.xml.)
            factLocation = FactLocation.GetLocation("Macon");
            Assert.AreEqual("Macon", factLocation.ToString());
        }

        // When "USA" IS given (even abbreviated/typo'd - FixCountryTypos normalises it before
        // parsing settles), there's no ambiguity: it parses straightforwardly as the US state,
        // Region=Georgia under Country=United States. Both spellings must converge on the same
        // result.
        [TestMethod]
        public void GeorgiaExplicitUSATest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            FactLocation factLocation = FactLocation.GetLocation("Georgia, USA");
            Assert.AreEqual("Georgia, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);
            Assert.IsTrue(factLocation.IsKnownCountry);

            factLocation = FactLocation.GetLocation("Georgia, United States");
            Assert.AreEqual("Georgia, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);
            Assert.IsTrue(factLocation.IsKnownCountry);
        }

        [TestMethod]
        public void GeorgiaCountryClassificationTest()
        {
            Assert.IsTrue(Countries.IsGeorgiaCountry("Georgia"));
            Assert.IsTrue(Countries.IsGeorgiaCountry("georgia")); // case insensitive
            Assert.IsTrue(Countries.IsGeorgiaCountry("GEORGIA"));
            Assert.IsFalse(Countries.IsGeorgiaCountry("United States"));
            Assert.IsFalse(Countries.IsGeorgiaCountry("Georgia, United States")); // must be an exact match, not a substring
            Assert.IsFalse(Countries.IsGeorgiaCountry(string.Empty));
            Assert.IsFalse(Countries.IsGeorgiaCountry(null));

            // KNOWN_COUNTRIES must never gain "Georgia" itself - that would make every other
            // IsKnownCountry call site ambiguous between the country and the US state again.
            // Callers that need to treat Georgia as legitimate use IsGeorgiaCountry alongside it.
            Assert.IsFalse(Countries.IsKnownCountry("Georgia"));
        }

        // With location fixups switched off entirely (GeneralSettings.SkipFixingLocations - e.g.
        // a user deliberately disabling them for messy custom data), parsing barely touches the
        // input at all. FactLocation.IsKnownCountry must still recognise a literal Country="Georgia"
        // as known rather than garbage under this path too.
        [TestMethod]
        public void GeorgiaCountryKnownWhenFixupsSkippedTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            bool originalSkipFixingLocations = GeneralSettings.Default.SkipFixingLocations;
            try
            {
                GeneralSettings.Default.SkipFixingLocations = true;
                FactLocation factLocation = FactLocation.GetLocation("Tbilisi, Georgia");
                Assert.IsTrue(factLocation.Country.Equals("Georgia"));
                Assert.IsTrue(factLocation.IsKnownCountry);
            }
            finally
            {
                GeneralSettings.Default.SkipFixingLocations = originalSkipFixingLocations;
            }
        }
    }
}
