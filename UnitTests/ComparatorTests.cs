using FTAnalyzer;
using FTAnalyzer.Utilities;
using Shouldly;
using System.ComponentModel;
using System.Numerics;
using System.Xml;

namespace UnitTests
{
    // ──────────────────────────────────────────────────────────────────────────
    // Shared helpers
    // ──────────────────────────────────────────────────────────────────────────

    internal static class ComparatorTestHelpers
    {
        internal static Individual MakeIndividual(string forename, string surname, string sex, string birthDate, string id = "I001")
        {
            string name = $"{forename} /{surname}/";
            string xml = $"<INDI><NAME>{name}</NAME><SEX>{sex}</SEX><BIRT><DATE>{birthDate.ToUpper()}</DATE></BIRT></INDI>";
            XmlDocument doc = new() { XmlResolver = null };
            doc.LoadXml(xml);
            XmlAttribute attr = doc.CreateAttribute("ID");
            attr.Value = id;
            doc.DocumentElement?.SetAttributeNode(attr);
            return new Individual(doc.FirstChild ?? doc, new Progress<string>());
        }

        internal static CensusIndividual MakeCensusIndividual(
            string forename, string surname, string birthDate,
            string familyId, string censusStatus, string individualId = "I001")
        {
            Individual ind = MakeIndividual(forename, surname, "M", birthDate, individualId);
            Family family = new(ind, familyId);
            CensusFamily censusFamily = new(family, CensusDate.UKCENSUS1881);
            return censusFamily.Husband!;
        }
    }

    // Minimal IDisplayIndividual stub for properties not settable on Individual (Ahnentafel, BudgieCode)
    internal sealed class StubDisplayIndividual : IDisplayIndividual
    {
        public string IndividualID { get; init; } = string.Empty;
        public string Forenames { get; init; } = string.Empty;
        public string Surname { get; init; } = string.Empty;
        public string Gender { get; init; } = string.Empty;
        public FactDate BirthDate { get; init; } = FactDate.UNKNOWN_DATE;
        public FactLocation BirthLocation { get; init; } = FactLocation.BLANK_LOCATION;
        public FactDate DeathDate { get; init; } = FactDate.UNKNOWN_DATE;
        public FactLocation DeathLocation { get; init; } = FactLocation.BLANK_LOCATION;
        public string Occupation { get; init; } = string.Empty;
        public Age LifeSpan { get; init; } = Age.BIRTH;
        public string Relation { get; init; } = string.Empty;
        public string RelationToRoot { get; init; } = string.Empty;
        public string Title { get; init; } = string.Empty;
        public string Suffix { get; init; } = string.Empty;
        public string Alias { get; init; } = string.Empty;
        public string FamilySearchID { get; init; } = string.Empty;
        public int MarriageCount { get; init; }
        public int ChildrenCount { get; init; }
        public string BudgieCode { get; init; } = string.Empty;
        public BigInteger Ahnentafel { get; init; } = BigInteger.Zero;
        public bool HasNotes { get; init; }
        public int FactsCount { get; init; }
        public int SourcesCount { get; init; }
        public IComparer<IDisplayIndividual> GetComparer(string columnName, bool ascending) => throw new NotSupportedException();
    }

    // Minimal IDisplayFamily stub
    internal sealed class StubDisplayFamily : IDisplayFamily
    {
        public string FamilyID { get; init; } = string.Empty;
        public int FamilySize { get; init; }
        public string HusbandID { get; init; } = string.Empty;
        public string Husband { get; init; } = string.Empty;
        public string WifeID { get; init; } = string.Empty;
        public string Wife { get; init; } = string.Empty;
        public string Marriage { get; init; } = string.Empty;
        public FactLocation Location { get; init; } = FactLocation.BLANK_LOCATION;
        public string Children { get; init; } = string.Empty;
        public string HusbandSurname { get; init; } = string.Empty;
        public string HusbandForenames { get; init; } = string.Empty;
        public string WifeSurname { get; init; } = string.Empty;
        public string WifeForenames { get; init; } = string.Empty;
        public string MaritalStatus { get; init; } = string.Empty;
        public IComparer<IDisplayFamily> GetComparer(string columnName, bool ascending) => throw new NotSupportedException();
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Individual comparators
    // ──────────────────────────────────────────────────────────────────────────

    [TestClass]
    public class DefaultIndividualComparerTests
    {
        readonly DefaultIndividualComparer _ascending = new(ascending: true);
        readonly DefaultIndividualComparer _descending = new(ascending: false);

        static Individual Ind(string id) =>
            ComparatorTestHelpers.MakeIndividual("John", "Smith", "M", "1 JAN 1900", id);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _ascending.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative_WhenAscending()
        {
            var result = _ascending.Compare(null, Ind("I001"));
            result.ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive_WhenAscending()
        {
            var result = _ascending.Compare(Ind("I001"), null);
            result.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsPositive_WhenDescending()
        {
            var result = _descending.Compare(null, Ind("I001"));
            result.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsNegative_WhenDescending()
        {
            var result = _descending.Compare(Ind("I001"), null);
            result.ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameId_ReturnsZero()
        {
            _ascending.Compare(Ind("I001"), Ind("I001")).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_SmallerIdFirst_ReturnsNegative_WhenAscending()
        {
            var result = _ascending.Compare(Ind("I001"), Ind("I002"));
            result.ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_LargerIdFirst_ReturnsPositive_WhenAscending()
        {
            var result = _ascending.Compare(Ind("I002"), Ind("I001"));
            result.ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_Descending_ReversesOrder()
        {
            int asc = _ascending.Compare(Ind("I001"), Ind("I002"));
            int desc = _descending.Compare(Ind("I001"), Ind("I002"));
            Math.Sign(asc).ShouldBe(-Math.Sign(desc));
        }
    }

    [TestClass]
    public class NameComparerTests
    {
        readonly NameComparer<Individual> _surnameAsc = new(ascending: true, forenames: false);
        readonly NameComparer<Individual> _forenameAsc = new(ascending: true, forenames: true);
        readonly NameComparer<Individual> _surnameDesc = new(ascending: false, forenames: false);

        static Individual Ind(string forename, string surname, string birthDate = "1 JAN 1900", string id = "I001") =>
            ComparatorTestHelpers.MakeIndividual(forename, surname, "M", birthDate, id);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _surnameAsc.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative_WhenAscending()
        {
            _surnameAsc.Compare(null, Ind("John", "Smith")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive_WhenAscending()
        {
            _surnameAsc.Compare(Ind("John", "Smith"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsPositive_WhenDescending()
        {
            _surnameDesc.Compare(null, Ind("John", "Smith")).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameSurname_SameForename_SameBirth_ReturnsZero()
        {
            _surnameAsc.Compare(Ind("John", "Smith"), Ind("John", "Smith")).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_BySurname_AlphaOrder_Ascending()
        {
            var archer = Ind("John", "Archer");
            var smith = Ind("John", "Smith");
            _surnameAsc.Compare(archer, smith).ShouldBeLessThan(0);
            _surnameAsc.Compare(smith, archer).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameSurname_ByForename_Ascending()
        {
            var alice = Ind("Alice", "Smith");
            var john = Ind("John", "Smith");
            _surnameAsc.Compare(alice, john).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameSurnameAndForename_ByBirthDate()
        {
            var earlier = Ind("John", "Smith", "1 JAN 1900", "I001");
            var later = Ind("John", "Smith", "1 JAN 1910", "I002");
            _surnameAsc.Compare(earlier, later).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_ForenamesFirst_PrioritisesForename()
        {
            var aliceSmith = Ind("Alice", "Smith");
            var johnArcher = Ind("John", "Archer");
            // ForenamesFirst: Alice < John regardless of surname
            _forenameAsc.Compare(aliceSmith, johnArcher).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_Descending_ReversesOrder()
        {
            var archer = Ind("John", "Archer");
            var smith = Ind("John", "Smith");
            int asc = _surnameAsc.Compare(archer, smith);
            int desc = _surnameDesc.Compare(archer, smith);
            Math.Sign(asc).ShouldBe(-Math.Sign(desc));
        }
    }

    [TestClass]
    public class BirthDateComparerTests
    {
        readonly BirthDateComparer _ascending = new(BirthDateComparer.ASCENDING);
        readonly BirthDateComparer _descending = new(BirthDateComparer.DESCENDING);

        static IDisplayIndividual Ind(string forename, string surname, string birthDate, string id = "I001") =>
            ComparatorTestHelpers.MakeIndividual(forename, surname, "M", birthDate, id);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _ascending.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_Ascending_ReturnsNegative()
        {
            _ascending.Compare(null, Ind("John", "Smith", "1 JAN 1900")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_Ascending_ReturnsPositive()
        {
            _ascending.Compare(Ind("John", "Smith", "1 JAN 1900"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_XNull_Descending_ReturnsPositive()
        {
            // In descending mode a and b are swapped; null ends up at the end
            _descending.Compare(null, Ind("John", "Smith", "1 JAN 1900")).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameBirthAndName_ReturnsZero()
        {
            var a = Ind("John", "Smith", "1 JAN 1900", "I001");
            var b = Ind("John", "Smith", "1 JAN 1900", "I002");
            _ascending.Compare(a, b).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_EarlierBirthFirst_ReturnsNegative_Ascending()
        {
            var earlier = Ind("John", "Smith", "1 JAN 1890");
            var later = Ind("John", "Smith", "1 JAN 1910");
            _ascending.Compare(earlier, later).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameBirth_SurnameBreaksTie()
        {
            var archer = Ind("John", "Archer", "1 JAN 1900");
            var smith = Ind("John", "Smith", "1 JAN 1900");
            _ascending.Compare(archer, smith).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameBirthAndSurname_ForenameBreaksTie()
        {
            var alice = Ind("Alice", "Smith", "1 JAN 1900");
            var john = Ind("John", "Smith", "1 JAN 1900");
            _ascending.Compare(alice, john).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_Descending_ReversesDateOrder()
        {
            var earlier = Ind("John", "Smith", "1 JAN 1890");
            var later = Ind("John", "Smith", "1 JAN 1910");
            int asc = _ascending.Compare(earlier, later);
            int desc = _descending.Compare(earlier, later);
            Math.Sign(asc).ShouldBe(-Math.Sign(desc));
        }
    }

    [TestClass]
    public class AhnentafelComparerTests
    {
        readonly AhnentafelComparer _comparer = new();

        static StubDisplayIndividual Stub(BigInteger ahnentafel) => new() { Ahnentafel = ahnentafel };

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, Stub(1)).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(Stub(1), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_EqualAhnentafel_ReturnsZero()
        {
            _comparer.Compare(Stub(4), Stub(4)).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_SmallerFirst_ReturnsNegative()
        {
            _comparer.Compare(Stub(2), Stub(5)).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_LargerFirst_ReturnsPositive()
        {
            _comparer.Compare(Stub(10), Stub(3)).ShouldBeGreaterThan(0);
        }
    }

    [TestClass]
    public class IndividualBudgieComparerTests
    {
        readonly IndividualBudgieComparer _comparer = new();

        static StubDisplayIndividual Stub(string budgieCode) => new() { BudgieCode = budgieCode };

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, Stub("1")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(Stub("1"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_BothEmptyCode_ReturnsZero()
        {
            // Empty BudgieCode maps to "X" internally
            _comparer.Compare(Stub(""), Stub("")).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_PlusSign_MapsToZ_SortsAfterAlpha()
        {
            // "+" → "z", so "1+" sorts as "1z" > "1a"
            var older = Stub("1+");  // maps to "1z"
            var younger = Stub("1-"); // maps to "1a"
            _comparer.Compare(younger, older).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_EqualCodes_ReturnsZero()
        {
            _comparer.Compare(Stub("1+2"), Stub("1+2")).ShouldBe(0);
        }
    }

    [TestClass]
    public class LooseBirthComparerTests
    {
        readonly LooseBirthComparer _comparer = new();

        static IDisplayLooseBirth Ind(string forename, string surname, string birthDate, string id = "I001") =>
            ComparatorTestHelpers.MakeIndividual(forename, surname, "M", birthDate, id);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, Ind("John", "Smith", "1 JAN 1900")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(Ind("John", "Smith", "1 JAN 1900"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameSurnameForenameAndBirth_ReturnsZero()
        {
            var a = Ind("John", "Smith", "1 JAN 1900", "I001");
            var b = Ind("John", "Smith", "1 JAN 1900", "I002");
            _comparer.Compare(a, b).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_BySurname_AlphaOrder()
        {
            var archer = Ind("John", "Archer", "1 JAN 1900");
            var smith = Ind("John", "Smith", "1 JAN 1900");
            _comparer.Compare(archer, smith).ShouldBeLessThan(0);
            _comparer.Compare(smith, archer).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameSurname_ByForename()
        {
            var alice = Ind("Alice", "Smith", "1 JAN 1900");
            var john = Ind("John", "Smith", "1 JAN 1900");
            _comparer.Compare(alice, john).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameSurnameAndForename_ByBirthDate()
        {
            var earlier = Ind("John", "Smith", "1 JAN 1890", "I001");
            var later = Ind("John", "Smith", "1 JAN 1910", "I002");
            _comparer.Compare(earlier, later).ShouldBeLessThan(0);
        }
    }

    [TestClass]
    public class LooseDeathComparerTests
    {
        readonly LooseDeathComparer _comparer = new();

        static IDisplayLooseDeath Ind(string forename, string surname, string birthDate, string id = "I001") =>
            ComparatorTestHelpers.MakeIndividual(forename, surname, "M", birthDate, id);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, Ind("John", "Smith", "1 JAN 1900")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(Ind("John", "Smith", "1 JAN 1900"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameSurnameForenameAndBirth_ReturnsZero()
        {
            var a = Ind("John", "Smith", "1 JAN 1900", "I001");
            var b = Ind("John", "Smith", "1 JAN 1900", "I002");
            _comparer.Compare(a, b).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_BySurname_AlphaOrder()
        {
            var archer = Ind("John", "Archer", "1 JAN 1900");
            var smith = Ind("John", "Smith", "1 JAN 1900");
            _comparer.Compare(archer, smith).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameSurname_ByForename()
        {
            var alice = Ind("Alice", "Smith", "1 JAN 1900");
            var john = Ind("John", "Smith", "1 JAN 1900");
            _comparer.Compare(alice, john).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameSurnameAndForename_ByBirthDate()
        {
            var earlier = Ind("John", "Smith", "1 JAN 1890", "I001");
            var later = Ind("John", "Smith", "1 JAN 1910", "I002");
            _comparer.Compare(earlier, later).ShouldBeLessThan(0);
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Census comparators
    // ──────────────────────────────────────────────────────────────────────────

    [TestClass]
    public class ColourCensusComparerTests
    {
        readonly ColourCensusComparer _comparer = new();

        static IDisplayColourCensus Ind(string forename, string surname, string birthDate, string id = "I001") =>
            ComparatorTestHelpers.MakeIndividual(forename, surname, "M", birthDate, id);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, Ind("John", "Smith", "1 JAN 1900")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(Ind("John", "Smith", "1 JAN 1900"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameSurnameForenameAndBirth_ReturnsZero()
        {
            var a = Ind("John", "Smith", "1 JAN 1900", "I001");
            var b = Ind("John", "Smith", "1 JAN 1900", "I002");
            _comparer.Compare(a, b).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_BySurname_AlphaOrder()
        {
            var archer = Ind("John", "Archer", "1 JAN 1900");
            var smith = Ind("John", "Smith", "1 JAN 1900");
            _comparer.Compare(archer, smith).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameSurname_ByForename()
        {
            var alice = Ind("Alice", "Smith", "1 JAN 1900");
            var john = Ind("John", "Smith", "1 JAN 1900");
            _comparer.Compare(alice, john).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameSurnameAndForename_ByBirthDate()
        {
            var earlier = Ind("John", "Smith", "1 JAN 1890", "I001");
            var later = Ind("John", "Smith", "1 JAN 1910", "I002");
            _comparer.Compare(earlier, later).ShouldBeLessThan(0);
        }
    }

    [TestClass]
    public class DefaultCensusComparerTests
    {
        readonly DefaultCensusComparer _comparer = new();

        static CensusIndividual CI(string familyId, string id = "I001") =>
            ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1900", familyId, CensusIndividual.HUSBAND, id);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, CI("F001")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(CI("F001"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameFamilyAndPosition_ReturnsZero()
        {
            // Same CensusFamily → same FamilyID + same Position
            var ci = CI("F001");
            _comparer.Compare(ci, ci).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_DifferentFamilyId_SortsByFamilyId()
        {
            var f1 = CI("F001", "I001");
            var f2 = CI("F002", "I002");
            _comparer.Compare(f1, f2).ShouldBeLessThan(0);
            _comparer.Compare(f2, f1).ShouldBeGreaterThan(0);
        }
    }

    [TestClass]
    public class CensusAgeComparerTests
    {
        readonly CensusAgeComparer _comparer = new();

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            var ci = ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1850", "F001", CensusIndividual.HUSBAND);
            _comparer.Compare(null, ci).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            var ci = ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1850", "F001", CensusIndividual.HUSBAND);
            _comparer.Compare(ci, null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_HusbandAlwaysBefore_Wife()
        {
            var husband = ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1850", "F001", CensusIndividual.HUSBAND, "I001");
            // Create a wife census individual by using a female individual
            Individual wifeInd = ComparatorTestHelpers.MakeIndividual("Jane", "Smith", "F", "1 JAN 1855", "I002");
            Family family = new(wifeInd, "F001");
            CensusFamily censusFamily = new(family, CensusDate.UKCENSUS1881);
            CensusIndividual wife = censusFamily.Wife!;

            _comparer.Compare(husband, wife).ShouldBeLessThan(0);
            _comparer.Compare(wife, husband).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameStatus_SortsByBirthDate()
        {
            // Two husbands from different families – same CensusStatus, sorted by birth date
            var older = ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1840", "F001", CensusIndividual.HUSBAND, "I001");
            var younger = ComparatorTestHelpers.MakeCensusIndividual("James", "Jones", "1 JAN 1860", "F002", CensusIndividual.HUSBAND, "I002");
            // older birth → earlier date → sorts first (FactDate.CompareTo ascending)
            _comparer.Compare(older, younger).ShouldBeLessThan(0);
        }
    }

    [TestClass]
    public class CensusFamilyGedComparerTests
    {
        readonly CensusFamilyGedComparer _comparer = new();

        static CensusIndividual CI(string familyId, string id = "I001") =>
            ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1900", familyId, CensusIndividual.HUSBAND, id);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, CI("F001")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(CI("F001"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameFamilyAndPosition_ReturnsZero()
        {
            var ci = CI("F001");
            _comparer.Compare(ci, ci).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_DifferentFamilyId_SortsByFamilyId()
        {
            var f1 = CI("F001", "I001");
            var f2 = CI("F002", "I002");
            _comparer.Compare(f1, f2).ShouldBeLessThan(0);
        }
    }

    [TestClass]
    public class CensusIndividualEqualityComparerTests
    {
        readonly CensusIndividualComparer _comparer = new();

        static CensusIndividual CI(string id) =>
            ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1900", "F001", CensusIndividual.HUSBAND, id);

        [TestMethod]
        public void Equals_BothNull_ReturnsTrue()
        {
            _comparer.Equals(null, null).ShouldBeTrue();
        }

        [TestMethod]
        public void Equals_XNull_ReturnsFalse()
        {
            _comparer.Equals(null, CI("I001")).ShouldBeFalse();
        }

        [TestMethod]
        public void Equals_YNull_ReturnsFalse()
        {
            _comparer.Equals(CI("I001"), null).ShouldBeFalse();
        }

        [TestMethod]
        public void Equals_SameId_ReturnsTrue()
        {
            var ci = CI("I001");
            _comparer.Equals(ci, ci).ShouldBeTrue();
        }

        [TestMethod]
        public void Equals_DifferentId_ReturnsFalse()
        {
            _comparer.Equals(CI("I001"), CI("I002")).ShouldBeFalse();
        }

        [TestMethod]
        public void GetHashCode_NullArgument_ReturnsZero()
        {
            _comparer.GetHashCode(null!).ShouldBe(0);
        }

        [TestMethod]
        public void GetHashCode_NonNull_ReturnsConsistentValue()
        {
            var ci = CI("I001");
            _comparer.GetHashCode(ci).ShouldBe(_comparer.GetHashCode(ci));
        }
    }

    [TestClass]
    public class CensusIndividualNameComparerTests
    {
        readonly CensusIndividualNameComparer _comparer = new();

        static CensusIndividual CI(string surname, string familyId, string id = "I001")
        {
            // CensusSurname comes from Family.Surname – we pass surname as the individual's surname
            // to influence the family's surname via the Family constructor
            Individual ind = ComparatorTestHelpers.MakeIndividual("John", surname, "M", "1 JAN 1900", id);
            Family family = new(ind, familyId);
            CensusFamily censusFamily = new(family, CensusDate.UKCENSUS1881);
            return censusFamily.Husband!;
        }

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, CI("Smith", "F001")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(CI("Smith", "F001"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameInstance_ReturnsZero()
        {
            var ci = CI("Smith", "F001");
            _comparer.Compare(ci, ci).ShouldBe(0);
        }
    }

    [TestClass]
    public class CensusLocationComparerTests
    {
        readonly CensusLocationComparer _comparer = new();

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            var ci = ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1900", "F001", CensusIndividual.HUSBAND);
            _comparer.Compare(null, ci).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            var ci = ComparatorTestHelpers.MakeCensusIndividual("John", "Smith", "1 JAN 1900", "F001", CensusIndividual.HUSBAND);
            _comparer.Compare(ci, null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Constructor_DefaultLevel_IsPlace()
        {
            var comparer = new CensusLocationComparer();
            comparer.Level.ShouldBe(FactLocation.PLACE);
        }

        [TestMethod]
        public void Constructor_ExplicitLevel_Stored()
        {
            var comparer = new CensusLocationComparer(FactLocation.COUNTRY);
            comparer.Level.ShouldBe(FactLocation.COUNTRY);
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Family comparators
    // ──────────────────────────────────────────────────────────────────────────

    [TestClass]
    public class DefaultFamilyComparerTests
    {
        readonly DefaultFamilyComparer _comparer = new();

        static IDisplayFamily Family(string familyId) =>
            new StubDisplayFamily { FamilyID = familyId };

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, Family("F001")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(Family("F001"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameFamilyId_ReturnsZero()
        {
            _comparer.Compare(Family("F001"), Family("F001")).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_SmallerIdFirst_ReturnsNegative()
        {
            _comparer.Compare(Family("F001"), Family("F002")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_LargerIdFirst_ReturnsPositive()
        {
            _comparer.Compare(Family("F002"), Family("F001")).ShouldBeGreaterThan(0);
        }
    }

    [TestClass]
    public class FamilySizeComparerTests
    {
        readonly FamilySizeComparer _low = new(countSortLow: true);
        readonly FamilySizeComparer _high = new(countSortLow: false);

        static IDisplayFamily Family(string familyId, int size) =>
            new StubDisplayFamily { FamilyID = familyId, FamilySize = size };

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _low.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _low.Compare(null, Family("F001", 2)).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _low.Compare(Family("F001", 2), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameSize_SortsByFamilyId_Ascending_WhenLow()
        {
            // CountSortLow: same size → compare x.FamilyID vs y.FamilyID
            var f1 = Family("F001", 3);
            var f2 = Family("F002", 3);
            _low.Compare(f1, f2).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_SameSize_SortsByFamilyId_Descending_WhenHigh()
        {
            // CountSortHigh: same size → compare y.FamilyID vs x.FamilyID (reversed)
            var f1 = Family("F001", 3);
            var f2 = Family("F002", 3);
            _high.Compare(f1, f2).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SmallerSizeFirst_WhenLow()
        {
            var small = Family("F001", 2);
            var large = Family("F002", 5);
            _low.Compare(small, large).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_LargerSizeFirst_WhenHigh()
        {
            var small = Family("F001", 2);
            var large = Family("F002", 5);
            // High: larger size sorts first → large < small in high mode
            _high.Compare(large, small).ShouldBeLessThan(0);
        }
    }

    [TestClass]
    public class FamilyDateComparerTests
    {
        readonly FamilyDateComparer _comparer = new();

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            Individual ind = ComparatorTestHelpers.MakeIndividual("John", "Smith", "M", "1 JAN 1900");
            Family family = new(ind, "F001");
            _comparer.Compare(null, family).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            Individual ind = ComparatorTestHelpers.MakeIndividual("John", "Smith", "M", "1 JAN 1900");
            Family family = new(ind, "F001");
            _comparer.Compare(family, null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_BothNoMarriage_ReturnsZero()
        {
            // Families constructed without marriage facts have UNKNOWN_DATE
            Individual ind1 = ComparatorTestHelpers.MakeIndividual("John", "Smith", "M", "1 JAN 1900", "I001");
            Individual ind2 = ComparatorTestHelpers.MakeIndividual("James", "Jones", "M", "1 JAN 1900", "I002");
            Family f1 = new(ind1, "F001");
            Family f2 = new(ind2, "F002");
            _comparer.Compare(f1, f2).ShouldBe(0);
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // Fact comparators
    // ──────────────────────────────────────────────────────────────────────────

    [TestClass]
    public class FactComparerTests
    {
        readonly FactComparer _comparer = new();

        static Fact MakeFact(string factType, string date, string place = "") =>
            new(string.Empty, factType, new FactDate(date), FactLocation.GetLocation(place));

        [TestMethod]
        public void Equals_BothNull_ReturnsFalse()
        {
            // IEqualityComparer contract: two nulls are "equal" but Equals(null,null) returns false
            // because the implementation checks x is not null
            _comparer.Equals(null, null).ShouldBeFalse();
        }

        [TestMethod]
        public void Equals_XNull_ReturnsFalse()
        {
            _comparer.Equals(null, MakeFact(Fact.BIRTH, "1 JAN 1900")).ShouldBeFalse();
        }

        [TestMethod]
        public void Equals_YNull_ReturnsFalse()
        {
            _comparer.Equals(MakeFact(Fact.BIRTH, "1 JAN 1900"), null).ShouldBeFalse();
        }

        [TestMethod]
        public void Equals_SameTypeAndDateAndLocation_ReturnsTrue()
        {
            var a = MakeFact(Fact.BIRTH, "1 JAN 1900", "London");
            var b = MakeFact(Fact.BIRTH, "1 JAN 1900", "London");
            _comparer.Equals(a, b).ShouldBeTrue();
        }

        [TestMethod]
        public void Equals_DifferentType_ReturnsFalse()
        {
            var a = MakeFact(Fact.BIRTH, "1 JAN 1900");
            var b = MakeFact(Fact.DEATH, "1 JAN 1900");
            _comparer.Equals(a, b).ShouldBeFalse();
        }

        [TestMethod]
        public void Equals_DifferentDate_ReturnsFalse()
        {
            var a = MakeFact(Fact.BIRTH, "1 JAN 1900");
            var b = MakeFact(Fact.BIRTH, "1 JAN 1901");
            _comparer.Equals(a, b).ShouldBeFalse();
        }

        [TestMethod]
        public void GetHashCode_SameDateFacts_ReturnsSameHash()
        {
            var a = MakeFact(Fact.BIRTH, "1 JAN 1900");
            var b = MakeFact(Fact.DEATH, "1 JAN 1900"); // same date, different type – hash is date-based
            _comparer.GetHashCode(a).ShouldBe(_comparer.GetHashCode(b));
        }

        [TestMethod]
        public void GetHashCode_DifferentDates_MayDiffer()
        {
            var a = MakeFact(Fact.BIRTH, "1 JAN 1900");
            var b = MakeFact(Fact.BIRTH, "1 JAN 1950");
            // Not guaranteed to differ but these dates definitely produce different hash values
            _comparer.GetHashCode(a).ShouldNotBe(_comparer.GetHashCode(b));
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // FactLocation comparator
    // ──────────────────────────────────────────────────────────────────────────

    [TestClass]
    public class FactLocationComparerTests
    {
        readonly FactLocationComparer _placeComparer = new(FactLocation.PLACE);
        readonly FactLocationComparer _countryComparer = new(FactLocation.COUNTRY);

        static IDisplayLocation Loc(string place) => FactLocation.GetLocation(place);

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _placeComparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _placeComparer.Compare(null, Loc("England")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _placeComparer.Compare(Loc("England"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_SameLocation_ReturnsZero()
        {
            _placeComparer.Compare(Loc("England"), Loc("England")).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_StoredLevel_IsReturned()
        {
            _placeComparer.Level.ShouldBe(FactLocation.PLACE);
            _countryComparer.Level.ShouldBe(FactLocation.COUNTRY);
        }

        [TestMethod]
        public void Compare_AlphaOrderedLocations_ReturnsNegative()
        {
            var a = Loc("France");
            var b = Loc("Germany");
            _placeComparer.Compare(a, b).ShouldBeLessThan(0);
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // LostCousins referral comparator
    // ──────────────────────────────────────────────────────────────────────────

    [TestClass]
    public class LostCousinsReferralComparerTests
    {
        readonly LostCousinsReferralComparer _comparer = new();

        static ExportReferrals MakeReferral(string forename, string surname, string birthDate, string id = "I001")
        {
            Individual ind = ComparatorTestHelpers.MakeIndividual(forename, surname, "M", birthDate, id);
            // Census fact with no matching census country → ShortCode will be ""
            Fact censusRef = new(string.Empty, Fact.CENSUS, new FactDate("1 APR 1881"), FactLocation.BLANK_LOCATION);
            return new ExportReferrals(ind, censusRef);
        }

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            _comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XNull_ReturnsNegative()
        {
            _comparer.Compare(null, MakeReferral("John", "Smith", "1 JAN 1860")).ShouldBeLessThan(0);
        }

        [TestMethod]
        public void Compare_YNull_ReturnsPositive()
        {
            _comparer.Compare(MakeReferral("John", "Smith", "1 JAN 1860"), null).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_IdenticalReferral_ReturnsZero()
        {
            var r = MakeReferral("John", "Smith", "1 JAN 1860");
            _comparer.Compare(r, r).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_SortsBySurnameWhenOtherFieldsEqual()
        {
            // Same birth date → same Age; ShortCode = "" for both; falls through to Surname
            var archer = MakeReferral("John", "Archer", "1 JAN 1860", "I001");
            var smith = MakeReferral("John", "Smith", "1 JAN 1860", "I002");
            _comparer.Compare(archer, smith).ShouldBeLessThan(0);
        }
    }

    // ──────────────────────────────────────────────────────────────────────────
    // PropertyComparer
    // ──────────────────────────────────────────────────────────────────────────

    [TestClass]
    public class PropertyComparerTests
    {
        private sealed class Item
        {
            public string Name { get; set; } = string.Empty;
            public int Value { get; set; }
        }

        static PropertyDescriptor GetDescriptor<T>(string propertyName) =>
            TypeDescriptor.GetProperties(typeof(T))[propertyName]
            ?? throw new InvalidOperationException($"Property '{propertyName}' not found on {typeof(T).Name}");

        [TestMethod]
        public void Constructor_NullProperty_Throws()
        {
            Should.Throw<ArgumentNullException>(() =>
                new PropertyComparer<Item>(null!, ListSortDirection.Ascending));
        }

        [TestMethod]
        public void Compare_BothNull_ReturnsZero()
        {
            var comparer = new PropertyComparer<Item>(GetDescriptor<Item>("Name"), ListSortDirection.Ascending);
            comparer.Compare(null, null).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_Ascending_SortsByProperty()
        {
            var comparer = new PropertyComparer<Item>(GetDescriptor<Item>("Name"), ListSortDirection.Ascending);
            var a = new Item { Name = "Archer" };
            var b = new Item { Name = "Smith" };
            comparer.Compare(a, b).ShouldBeLessThan(0);
            comparer.Compare(b, a).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_Descending_ReversesOrder()
        {
            var comparer = new PropertyComparer<Item>(GetDescriptor<Item>("Name"), ListSortDirection.Descending);
            var a = new Item { Name = "Archer" };
            var b = new Item { Name = "Smith" };
            comparer.Compare(a, b).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void Compare_EqualValues_ReturnsZero()
        {
            var comparer = new PropertyComparer<Item>(GetDescriptor<Item>("Name"), ListSortDirection.Ascending);
            var a = new Item { Name = "Smith" };
            var b = new Item { Name = "Smith" };
            comparer.Compare(a, b).ShouldBe(0);
        }

        [TestMethod]
        public void Compare_XEmptyString_YNonEmpty_Ascending_ReturnsPositive()
        {
            // Empty string sorts "after" non-empty in the PropertyComparer implementation
            var comparer = new PropertyComparer<Item>(GetDescriptor<Item>("Name"), ListSortDirection.Ascending);
            var empty = new Item { Name = string.Empty };
            var filled = new Item { Name = "Smith" };
            comparer.Compare(empty, filled).ShouldBeGreaterThan(0);
        }

        [TestMethod]
        public void SetPropertyAndDirection_ChangesComparisonBehaviour()
        {
            var comparer = new PropertyComparer<Item>(GetDescriptor<Item>("Name"), ListSortDirection.Ascending);
            var a = new Item { Name = "Archer", Value = 10 };
            var b = new Item { Name = "Smith", Value = 5 };

            // Initially compares by Name (ascending): Archer < Smith
            comparer.Compare(a, b).ShouldBeLessThan(0);

            // After switching to Value descending: 10 > 5 so a > b → negative in descending
            comparer.SetPropertyAndDirection(GetDescriptor<Item>("Value"), ListSortDirection.Descending);
            comparer.Compare(a, b).ShouldBeLessThan(0); // 10 > 5, descending → a sorts first
        }
    }
}
