using FTAnalyzer;
using System.Xml;

namespace UnitTests
{
    /// <summary>
    /// Summary description for AgeTest1
    /// </summary>
    [TestClass]
    public class AgeTest
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
        public void AgeStringConstructor()
        {
            FactDate baseDate = new("19 Nov 1988");
            Age age = new("22y", baseDate);
            Assert.AreEqual(age.GetBirthDate(baseDate), new("19 Nov 1966"));

            age = new Age("22y 2m", baseDate);
            Assert.AreEqual(age.GetBirthDate(baseDate), new("19 Sep 1966"));

            age = new Age("22y 2m 5d", baseDate);
            Assert.AreEqual(age.GetBirthDate(baseDate), new("14 Sep 1966"));
        }

        #region CompareTo tests

        // Builds an Age via the Individual constructor so MinAge and MaxAge are set.
        //
        // MinAge = GetAge(BirthDate.EndDate,  when.StartDate)   – latest birth vs earliest event
        // MaxAge = GetAge(BirthDate.StartDate, when.EndDate)    – earliest birth vs latest event
        //
        // For an exact birth date:  MinAge == MaxAge == the computed whole years.
        // For a BET range birth:    MinAge is based on the later bound, MaxAge on the earlier bound.
        //
        // Example date arithmetic (GetAge uses whole-year difference):
        //   MakeAge("1 JAN 1960",                   "1 JAN 1990") → MinAge=30, MaxAge=30
        //   MakeAge("1 JAN 1960",                   "1 JAN 1985") → MinAge=25, MaxAge=25
        //   MakeAge("1 JAN 1960",                   "1 JAN 1980") → MinAge=20, MaxAge=20
        //   MakeAge("BET 1 JAN 1965 AND 1 JAN 1970","1 JAN 1995") → MinAge=25, MaxAge=30
        //   MakeAge("BET 1 JAN 1960 AND 1 JAN 1970","1 JAN 1995") → MinAge=25, MaxAge=35
        static Age MakeAge(string birthGedcom, string whenGedcom)
        {
            string xml = $"<INDI><NAME>Test /Person/</NAME><SEX>M</SEX><BIRT><DATE>{birthGedcom.ToUpper()}</DATE></BIRT></INDI>";
            XmlDocument doc = new() { XmlResolver = null };
            doc.LoadXml(xml);
            XmlAttribute attr = doc.CreateAttribute("ID");
            attr.Value = "1";
            doc.DocumentElement?.SetAttributeNode(attr);
            XmlNode node = doc.FirstChild ?? doc;
            Individual ind = new(node, new Progress<string>());
            FactDate when = new(whenGedcom);
            return new Age(ind, when);
        }

        [TestMethod]
        public void CompareTo_EqualExactAges_ReturnsZero()
        {
            Age age30a = MakeAge("1 JAN 1960", "1 JAN 1990");
            Age age30b = MakeAge("1 JAN 1960", "1 JAN 1990");

            Assert.AreEqual(0, age30a.CompareTo(age30b));
        }

        [TestMethod]
        public void CompareTo_Self_ReturnsZero()
        {
            Age age = MakeAge("1 JAN 1960", "1 JAN 1990");

            Assert.AreEqual(0, age.CompareTo(age));
        }

        [TestMethod]
        public void CompareTo_BIRTH_WithSelf_ReturnsZero()
        {
            Assert.AreEqual(0, Age.BIRTH.CompareTo(Age.BIRTH));
        }

        [TestMethod]
        public void CompareTo_HigherMinAge_ReturnsPositive()
        {
            Age age30 = MakeAge("1 JAN 1960", "1 JAN 1990"); // MinAge=MaxAge=30
            Age age25 = MakeAge("1 JAN 1960", "1 JAN 1985"); // MinAge=MaxAge=25

            Assert.IsGreaterThan(0, age30.CompareTo(age25));
        }

        [TestMethod]
        public void CompareTo_LowerMinAge_ReturnsNegative()
        {
            Age age25 = MakeAge("1 JAN 1960", "1 JAN 1985"); // MinAge=MaxAge=25
            Age age30 = MakeAge("1 JAN 1960", "1 JAN 1990"); // MinAge=MaxAge=30

            Assert.IsLessThan(0, age25.CompareTo(age30));
        }

        [TestMethod]
        public void CompareTo_SameMinAge_HigherMaxAge_ReturnsPositive()
        {
            // MinAge=25, MaxAge=35  vs  MinAge=25, MaxAge=30
            Age widerRange   = MakeAge("BET 1 JAN 1960 AND 1 JAN 1970", "1 JAN 1995");
            Age narrowerRange = MakeAge("BET 1 JAN 1965 AND 1 JAN 1970", "1 JAN 1995");

            Assert.AreEqual(25, widerRange.MinAge);
            Assert.AreEqual(35, widerRange.MaxAge);
            Assert.AreEqual(25, narrowerRange.MinAge);
            Assert.AreEqual(30, narrowerRange.MaxAge);
            Assert.IsGreaterThan(0, widerRange.CompareTo(narrowerRange));
        }

        [TestMethod]
        public void CompareTo_SameMinAge_LowerMaxAge_ReturnsNegative()
        {
            // MinAge=25, MaxAge=30  vs  MinAge=25, MaxAge=35
            Age narrowerRange = MakeAge("BET 1 JAN 1965 AND 1 JAN 1970", "1 JAN 1995");
            Age widerRange    = MakeAge("BET 1 JAN 1960 AND 1 JAN 1970", "1 JAN 1995");

            Assert.IsLessThan(0, narrowerRange.CompareTo(widerRange));
        }

        [TestMethod]
        public void CompareTo_SameMinAge_SameMaxAge_DifferentRanges_ReturnsZero()
        {
            // Two independently constructed ages with identical MinAge=25, MaxAge=30
            Age range1 = MakeAge("BET 1 JAN 1965 AND 1 JAN 1970", "1 JAN 1995");
            Age range2 = MakeAge("BET 1 JAN 1965 AND 1 JAN 1970", "1 JAN 1995");

            Assert.AreEqual(0, range1.CompareTo(range2));
        }

        [TestMethod]
        public void CompareTo_HigherMinAge_LowerMaxAge_MinAgeDominates_ReturnsPositive()
        {
            // MinAge=30, MaxAge=30  vs  MinAge=25, MaxAge=35  →  MinAge dominates
            Age exact30      = MakeAge("1 JAN 1960",                    "1 JAN 1990");
            Age range25to35  = MakeAge("BET 1 JAN 1960 AND 1 JAN 1970", "1 JAN 1995");

            Assert.AreEqual(30, exact30.MinAge);
            Assert.AreEqual(30, exact30.MaxAge);
            Assert.AreEqual(25, range25to35.MinAge);
            Assert.AreEqual(35, range25to35.MaxAge);
            Assert.IsGreaterThan(0, exact30.CompareTo(range25to35));
        }

        [TestMethod]
        public void CompareTo_LowerMinAge_HigherMaxAge_MinAgeDominates_ReturnsNegative()
        {
            // MinAge=25, MaxAge=35  vs  MinAge=30, MaxAge=30  →  MinAge dominates
            Age range25to35 = MakeAge("BET 1 JAN 1960 AND 1 JAN 1970", "1 JAN 1995");
            Age exact30     = MakeAge("1 JAN 1960",                    "1 JAN 1990");

            Assert.IsLessThan(0, range25to35.CompareTo(exact30));
        }

        [TestMethod]
        public void CompareTo_BIRTH_IsLessThanPositiveAge()
        {
            Age age30 = MakeAge("1 JAN 1960", "1 JAN 1990");

            Assert.IsLessThan(0, Age.BIRTH.CompareTo(age30));
            Assert.IsGreaterThan(0, age30.CompareTo(Age.BIRTH));
        }

        [TestMethod]
        public void CompareTo_IsAntisymmetric()
        {
            Age age25 = MakeAge("1 JAN 1960", "1 JAN 1985");
            Age age30 = MakeAge("1 JAN 1960", "1 JAN 1990");

            int forward  = age25.CompareTo(age30);
            int backward = age30.CompareTo(age25);

            Assert.IsLessThan(0, forward,  "age25 should be less than age30");
            Assert.IsGreaterThan(0, backward, "age30 should be greater than age25");
        }

        [TestMethod]
        public void CompareTo_IsTransitive()
        {
            Age age20 = MakeAge("1 JAN 1960", "1 JAN 1980");
            Age age25 = MakeAge("1 JAN 1960", "1 JAN 1985");
            Age age30 = MakeAge("1 JAN 1960", "1 JAN 1990");

            Assert.IsLessThan(0, age20.CompareTo(age25), "age20 < age25");
            Assert.IsLessThan(0, age25.CompareTo(age30), "age25 < age30");
            Assert.IsLessThan(0, age20.CompareTo(age30), "age20 < age30 (transitivity)");
        }

        [TestMethod]
        public void IComparable_CompareTo_Age_MatchesTypedVersion()
        {
            Age age25 = MakeAge("1 JAN 1960", "1 JAN 1985");
            Age age30 = MakeAge("1 JAN 1960", "1 JAN 1990");

            int typed   = age25.CompareTo(age30);
            int untyped = ((IComparable)age25).CompareTo(age30);

            Assert.AreEqual(Math.Sign(typed), Math.Sign(untyped));
        }

        [TestMethod]
        public void Sort_ListOfAges_SortedCorrectlyByMinAgeThenMaxAge()
        {
            // Expected sort order:
            //   BIRTH (0,0) < age20 (20,20) < age25 (25,25) < range25to30 (25,30) < age30 (30,30)
            Age age30       = MakeAge("1 JAN 1960",                    "1 JAN 1990");
            Age age25       = MakeAge("1 JAN 1960",                    "1 JAN 1985");
            Age age20       = MakeAge("1 JAN 1960",                    "1 JAN 1980");
            Age range25to30 = MakeAge("BET 1 JAN 1965 AND 1 JAN 1970", "1 JAN 1995");

            List<Age> ages = [age30, Age.BIRTH, range25to30, age25, age20];
            ages.Sort();

            Assert.AreEqual(Age.BIRTH,   ages[0], "BIRTH (0,0) should sort first");
            Assert.AreEqual(age20,       ages[1], "age20 (20,20) should sort second");
            Assert.AreEqual(age25,       ages[2], "age25 (25,25) should sort third");
            Assert.AreEqual(range25to30, ages[3], "range25to30 (25,30) should sort fourth");
            Assert.AreEqual(age30,       ages[4], "age30 (30,30) should sort last");
        }

        // ── Null-handling tests ──────────────────────────────────────────────────────
        // The two tests below document the correct IComparable contract: any non-null
        // instance is greater than null.  They are EXPECTED TO FAIL on the current
        // implementation (NullReferenceException) and should pass after the tidy-up.

        [TestMethod]
        public void CompareTo_Null_ReturnsPositive()
        {
            Age age30 = MakeAge("1 JAN 1960", "1 JAN 1990");

            int result = age30.CompareTo((Age?)null);

            Assert.IsGreaterThan(0, result, "Any Age should be greater than null");
        }

        [TestMethod]
        public void IComparable_CompareTo_Null_ReturnsPositive()
        {
            Age age30 = MakeAge("1 JAN 1960", "1 JAN 1990");

            int result = ((IComparable)age30).CompareTo(null);

            Assert.IsGreaterThan(0, result, "Any Age should be greater than null via IComparable");
        }

        #endregion
    }
}
