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

        // GEDCOM locations occasionally have a continent tacked on as a spurious extra "country"
        // segment - Continents.IsContinent/FactLocation.StripContinent (shared code, merged in
        // from FTAnalyzer.Shared's "initial" branch) strip it before country-aware fixes run.
        [TestMethod]
        public void ContinentStrippingTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            FactLocation factLocation = FactLocation.GetLocation("Rushton Spencer, Staffordshire, England, Europe");
            Assert.AreEqual("Rushton Spencer, Staffordshire, England", factLocation.ToString());
            Assert.AreEqual("England", factLocation.Country);

            factLocation = FactLocation.GetLocation("Boston, Massachusetts, North America");
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);
        }

        // GEDCOM data occasionally omits the comma before a trailing country name -
        // FactLocation.FixMissingCommaBeforeCountry recovers a proper Region/Country split from a
        // single run-on Country value rather than leaving it as one unrecognisable string. Tries
        // the longest trailing run of words first, so multi-word country names are recognised too.
        [TestMethod]
        public void MissingCommaBeforeCountryTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            // Needs typo normalisation ("USA" -> "United States").
            FactLocation factLocation = FactLocation.GetLocation("California USA");
            Assert.AreEqual("California, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);

            // Already a canonical (single-word) country name.
            factLocation = FactLocation.GetLocation("London England");
            Assert.AreEqual("London, England", factLocation.ToString());
            Assert.AreEqual("England", factLocation.Country);

            // Two-word country name, must match "United States" rather than stopping at "States".
            factLocation = FactLocation.GetLocation("Boston United States");
            Assert.AreEqual("Boston, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);

            // Multi-word Region/leftover ("New York") ahead of a multi-word country name.
            factLocation = FactLocation.GetLocation("New York United States");
            Assert.AreEqual("New York, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);

            // Multi-word leftover ahead of a typo'd single-word country name.
            factLocation = FactLocation.GetLocation("New York USA");
            Assert.AreEqual("New York, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);

            // A properly-comma'd location must be completely unaffected.
            factLocation = FactLocation.GetLocation("Boston, Massachusetts, United States");
            Assert.AreEqual("Boston, Massachusetts, United States", factLocation.ToString());

            // Ties into the Georgia city disambiguation (see GeorgiaKnownCityPromotedTest) -
            // "Atlanta Georgia" with the comma missing still ends up fully promoted, since
            // FixMissingCommaBeforeCountry recognises "Georgia" too and ShiftGeorgiaCityToRegion
            // (which runs immediately after) recognises "Atlanta" as a known Georgia city.
            factLocation = FactLocation.GetLocation("Atlanta Georgia");
            Assert.AreEqual("Atlanta, Georgia, United States", factLocation.ToString());
            Assert.AreEqual(Countries.UNITED_STATES, factLocation.Country);

            // A word that isn't a known country at all must be left alone entirely.
            factLocation = FactLocation.GetLocation("New Amsterdam");
            Assert.AreEqual("New Amsterdam", factLocation.ToString());
        }

        // Guards against false positives where an innocent phrase happens to end in a country
        // name with no missing comma intended at all. The tell is the word immediately before the
        // country-like word: a genuine place name essentially never ends its own portion in a
        // short connector word ("of", "in", "at"...) right before the country, so leftovers ending
        // in a word under 3 characters are rejected rather than split into a nonsense Region.
        [TestMethod]
        public void MissingCommaFalsePositiveGuardTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            // "of" (2 chars) immediately precedes the country-like word - rejected.
            FactLocation factLocation = FactLocation.GetLocation("Bank of China");
            Assert.AreEqual("Bank of China", factLocation.ToString());

            factLocation = FactLocation.GetLocation("Church of Scotland");
            Assert.AreEqual("Church of Scotland", factLocation.ToString());

            // Boundary check: a 2-character leftover-ending word is rejected...
            factLocation = FactLocation.GetLocation("Port Of England");
            Assert.AreEqual("Port of England", factLocation.ToString()); // "Of" -> "of": EnhancedTextInfo.ToTitleCase lowercases prepositions

            // ...but a 3-character one is accepted (not "less than 3").
            factLocation = FactLocation.GetLocation("Man USA");
            Assert.AreEqual("Man, United States", factLocation.ToString());
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

        // "UK"/"GB" written as the Country alongside one of the constituent countries as the
        // COUNTRY_TYPOS blindly maps bare "UK"/"GB" to "England" (a deliberate simplification -
        // most UK GEDCOM data that gives no more detail than that does mean England) - which would
        // be wrong whenever the Region already correctly says Scotland/Wales. FixUKGBTypos runs
        // first and promotes the constituent country up to Country in that case specifically,
        // pre-empting the blanket "UK"/"GB" -> "England" typo fix from clobbering it.
        [TestMethod]
        public void FixUKGBTyposTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            FactLocation factLocation = FactLocation.GetLocation("England, UK");
            Assert.AreEqual("England", factLocation.ToString());
            Assert.AreEqual("England", factLocation.Country);
            Assert.AreEqual(string.Empty, factLocation.Region);

            factLocation = FactLocation.GetLocation("Wales, GB");
            Assert.AreEqual("Wales", factLocation.ToString());
            Assert.AreEqual("Wales", factLocation.Country);

            // Any other Region doesn't trigger this specific pre-emption, so the blanket
            // "UK" -> "England" typo fix runs unopposed - "Normandy" ends up (incorrectly, but
            // that's the existing, unrelated typo-fix behaviour, not this fix's job to solve)
            // filed under England rather than left as the unrecognised pair it was typed as.
            factLocation = FactLocation.GetLocation("Normandy, UK");
            Assert.AreEqual("Normandy, England", factLocation.ToString());
        }

        // Some earlier fixup occasionally leaves the same value duplicated across two adjacent
        // levels (e.g. Country and Region both "England") - FixDoubleLocations collapses the
        // redundant level away rather than displaying it twice.
        [TestMethod]
        public void FixDoubleLocationsTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            // Country == Region.
            FactLocation factLocation = FactLocation.GetLocation("England, England");
            Assert.AreEqual("England", factLocation.ToString());
            Assert.AreEqual(string.Empty, factLocation.Region);

            // Region == SubRegion (a level further down, Country stays untouched). "Puddleton" is
            // a deliberately made-up place name here so this test isn't accidentally exercising
            // some other fixup keyed on a real one (e.g. "Boston" is itself a RegionToParish entry).
            factLocation = FactLocation.GetLocation("Puddleton, Puddleton, England");
            Assert.AreEqual("Puddleton, England", factLocation.ToString());
            Assert.AreEqual("England", factLocation.Country);
            Assert.AreEqual("Puddleton", factLocation.Region);
            Assert.AreEqual(string.Empty, factLocation.SubRegion);
        }

        // FixRegionFullStops/FixCountryFullStops strip stray full stops (and asterisks) out of
        // the Region/Country fields before any typo/shift lookup runs against them, so
        // "Some.Where" and "Some.Where" (dictionary keys never contain punctuation) still match.
        [TestMethod]
        public void FixFullStopsTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            FactLocation factLocation = FactLocation.GetLocation("Puddleby, Some.Where, England");
            Assert.AreEqual("Some Where", factLocation.Region);

            factLocation = FactLocation.GetLocation("Testonia.");
            Assert.AreEqual("Testonia", factLocation.Country);
        }

        // FixMultipleSpacesAmpersandsCommas collapses runs of spaces down to one and expands "&"
        // to "and", so differently-formatted versions of the same name end up identical.
        [TestMethod]
        public void FixAmpersandAndSpacesTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            FactLocation factLocation = FactLocation.GetLocation("Puddleby, Fish  &  Chip   Town, England");
            Assert.AreEqual("Fish and Chip Town", factLocation.Region);
        }

        // UK regions get demoted a level under their proper county/shire when the GEDCOM data has
        // a city directly under the country with no county given at all (e.g. "Aberdeen, Scotland"
        // instead of "Aberdeen, Aberdeenshire, Scotland") - ShiftRegionToParish inserts the missing
        // county level, sliding the city down to SubRegion. UK-only: see IsUnitedKingdomGateTest for
        // why the same city name elsewhere isn't affected.
        [TestMethod]
        public void ShiftRegionToParishTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            FactLocation factLocation = FactLocation.GetLocation("Old Machar, Aberdeen, Scotland");
            Assert.AreEqual("Old Machar, Aberdeen, Aberdeenshire, Scotland", factLocation.ToString());
            Assert.AreEqual("Scotland", factLocation.Country);
            Assert.AreEqual("Aberdeenshire", factLocation.Region);
            Assert.AreEqual("Aberdeen", factLocation.SubRegion);
        }

        // CensusCountryMatches decides whether a location's own Country "counts as" a target
        // census country s - used to match a person's residence against which country's census
        // records to search. Several special cases: exact match, England/Wales treated as
        // equivalent (they shared a census), "United Kingdom" matching any of its constituent
        // countries, Scotland is deliberately never treated as equivalent to anything else (it ran
        // its own separate census), and - when explicitly asked to - an unrecognised Country
        // counts as matching anything (too little information to say it doesn't).
        [TestMethod]
        public void CensusCountryMatchesTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));

            FactLocation england = FactLocation.GetLocation("England");
            Assert.IsTrue(england.CensusCountryMatches("England", false));
            Assert.IsTrue(england.CensusCountryMatches("Wales", false));
            Assert.IsFalse(england.CensusCountryMatches("Scotland", false));

            FactLocation scotland = FactLocation.GetLocation("Scotland");
            Assert.IsTrue(scotland.CensusCountryMatches("Scotland", false));
            Assert.IsFalse(scotland.CensusCountryMatches("England", false));
            // The explicit Scotland exclusion only rules out equivalence with other constituent
            // countries (England/Wales) - the UK-equivalence check above it in CensusCountryMatches
            // still applies, so a search specifically for "United Kingdom" does include Scotland.
            Assert.IsTrue(scotland.CensusCountryMatches("United Kingdom", false));

            FactLocation uk = FactLocation.GetLocation("United Kingdom");
            Assert.IsTrue(uk.CensusCountryMatches("Scotland", false));
            Assert.IsTrue(uk.CensusCountryMatches("England", false));

            FactLocation garbled = FactLocation.GetLocation("Ruritania");
            Assert.IsFalse(garbled.CensusCountryMatches("England", false)); // includeUnknownCountries off
            Assert.IsTrue(garbled.CensusCountryMatches("England", true)); // includeUnknownCountries on
        }

        // Basic sanity checks on the ordering/caching behaviour every other test above implicitly
        // relies on: identical location strings resolve to the exact same cached instance, and
        // CompareTo orders by Country first (Ordinal, so plain alphabetical for these examples).
        [TestMethod]
        public void CachingAndCompareToTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));

            FactLocation first = FactLocation.GetLocation("Boston, Massachusetts, United States");
            FactLocation second = FactLocation.GetLocation("Boston, Massachusetts, United States");
            Assert.IsTrue(ReferenceEquals(first, second));
            Assert.IsTrue(first == second);

            FactLocation scotlandLoc = FactLocation.GetLocation("Aberdeen, Scotland");
            FactLocation usaLoc = FactLocation.GetLocation("Boston, United States");
            Assert.IsTrue(scotlandLoc.CompareTo(usaLoc) < 0); // "Scotland" < "United States" ordinally
            Assert.IsTrue(usaLoc.CompareTo(scotlandLoc) > 0);
        }

        // A UK postcode tacked onto the end of a segment - either its own comma-separated "level"
        // or glued onto a place name with no comma of its own - used to get misread as if it WERE
        // the country (see StripTrailingPostcode in FactLocation.cs). These are the exact examples
        // that surfaced the bug, plus a couple of "must NOT strip" cases guarding the false-positive
        // tradeoffs that fix deliberately accepts.
        [TestMethod]
        public void StripTrailingPostcodeTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            // Whole segment is nothing but a postcode - collapses away via the existing empty-field cascade.
            FactLocation fullPostcodeSegment = FactLocation.GetLocation("6 Sussex Road, Sk3 0Jl");
            Assert.AreEqual("6 Sussex Road", fullPostcodeSegment.ToString());

            // Bare outward/district code glued onto the end of a place name with no comma of its own.
            // Once the "Se2"/"E8" noise is stripped, the existing Country/Region pipeline further
            // downstream (unrelated to this fix) correctly recognises "London" isn't a country and
            // shifts it to Region, deriving "England" as the real country.
            FactLocation outwardOnlyMixedCase = FactLocation.GetLocation("Abbey Wood, London Se2");
            Assert.AreEqual("Abbey Wood, London, England", outwardOnlyMixedCase.ToString());

            FactLocation outwardOnlySingleLetter = FactLocation.GetLocation("Hackney, London E8");
            Assert.AreEqual("Hackney, London, England", outwardOnlySingleLetter.ToString());

            // A full Canadian postal code's unit group doesn't fit the UK unit-code shape (digit +
            // 2 letters), so the whole thing is left alone rather than partially mangled.
            FactLocation canadianPostcode = FactLocation.GetLocation("Ottawa, Ontario K1A 0B1");
            string canadianResult = canadianPostcode.ToString().ToUpperInvariant();
            StringAssert.Contains(canadianResult, "K1A");
            StringAssert.Contains(canadianResult, "0B1");

            // Trailing plain digits with no letter prefix never match - safe against dates/numbers.
            FactLocation trailingNumber = FactLocation.GetLocation("Somewhere Village, World War 1");
            StringAssert.Contains(trailingNumber.ToString(), "World War 1");
        }

        // "15 Some Road, Hackney, E8" is a common real-world way to write a London address - the
        // postcode area letters stand in for "London" entirely, with nothing else in that segment.
        // Stripping the code alone would lose the only clue this was a London address at all, so
        // StripTrailingPostcode puts "London" back when that happens and nothing else already says so.
        [TestMethod]
        public void StripTrailingPostcode_InjectsLondonTest()
        {
            FactLocation.LoadConversions(Path.Combine(Environment.CurrentDirectory, "..\\..\\..\\..\\..\\FTAnalyzer.Shared\\FTAnalyzer.Shared"));
            GeneralSettings.Default.AllowEmptyLocations = false;

            // Bare London-area code as its own trailing segment - "London" isn't mentioned anywhere else.
            FactLocation bareEastLondon = FactLocation.GetLocation("15 Some Road, Hackney, E8");
            Assert.AreEqual("15 Some Road, Hackney, London, England", bareEastLondon.ToString());

            FactLocation bareSouthWestLondon = FactLocation.GetLocation("Chelsea, SW1");
            Assert.AreEqual("Chelsea, London, England", bareSouthWestLondon.ToString());

            // Two-letter London area code (EC/WC/NW/SE/SW), not just the single-letter ones.
            FactLocation bareNorthWestLondon = FactLocation.GetLocation("Kilburn, NW6");
            Assert.AreEqual("Kilburn, London, England", bareNorthWestLondon.ToString());

            // "London" is already spelled out elsewhere - must NOT get a second, redundant injection.
            FactLocation alreadyMentionsLondon = FactLocation.GetLocation("Hackney, London, E8");
            Assert.AreEqual("Hackney, London, England", alreadyMentionsLondon.ToString());

            // A non-London outward code standing alone must NOT trigger the London injection - only
            // the eight London-exclusive area letters (E/EC/N/NW/SE/SW/W/WC) qualify. "Sunderland"
            // getting "County Durham, England" appended is pre-existing known-place enrichment
            // unrelated to this fix (see FactLocationConstructorTest) - the point here is just that
            // "London" is nowhere in the result.
            FactLocation nonLondonBareCode = FactLocation.GetLocation("Sunderland, SR1");
            Assert.AreEqual("Sunderland, County Durham, England", nonLondonBareCode.ToString());
        }
    }
}
