using FTAnalyzer;

namespace UnitTests
{
    /// <summary>
    /// Regression test for a real bug: ResetData() used to unconditionally clear the GINAP nickname-
    /// standardisation dataset (Fred/Frederic, Maggie/Margaret, ...) that LoadStandardisedNames
    /// populates. ResetData runs at the start of every GEDCOM parse (LoadTreeHeader), but
    /// LoadStandardisedNames is only ever called once per process (MainForm_Shown on desktop,
    /// FamilyTreeService's constructor on web) - so the dataset was silently discarded after the
    /// first tree load and never reloaded, meaning GetStandardisedName always returned its input
    /// unchanged from then on. That broke nickname-aware name matching everywhere it's used -
    /// Individual.StandardisedName, LostCousinsReconciliation.NamesMatch (a Lost Cousins website
    /// entry for "Fred" could never match a GEDCOM citation for the same person recorded as
    /// "Fredrick", even with an otherwise perfect census reference match), and duplicate detection.
    /// </summary>
    [TestClass]
    public class FamilyTreeStandardisedNameTest
    {
        [TestMethod]
        public void StandardisedNames_SurviveResetData()
        {
            FamilyTree ft = FamilyTree.CreateInstance();
            FamilyTree.SetInstance(ft);
            ft.LoadStandardisedNames(AppContext.BaseDirectory);

            // Sanity: the GINAP dataset actually loaded before touching ResetData at all.
            Assert.AreEqual("Frederic", ft.GetStandardisedName(true, "Fred"));
            Assert.AreEqual("Frederic", ft.GetStandardisedName(true, "Fredrick"));

            // ResetData() runs at the start of every GEDCOM parse - simulating a second (or any
            // later) tree load in the same session/process.
            ft.ResetData();

            Assert.AreEqual("Frederic", ft.GetStandardisedName(true, "Fred"),
                "GINAP nickname mapping must survive ResetData - nothing reloads it afterwards.");
            Assert.AreEqual("Frederic", ft.GetStandardisedName(true, "Fredrick"),
                "GINAP nickname mapping must survive ResetData - nothing reloads it afterwards.");
        }

        // GINAP.txt has a literal duplicate line ("Catherin"/female, twice) partway through the
        // file. ReadStandardisedNameFile used Dictionary.Add, which throws on the second occurrence
        // and (nothing catching it locally) aborted the whole read - silently discarding every line
        // after the duplicate, not just the duplicate itself. "Sibille"->"Sybil" is one of the last
        // entries in the file, past that duplicate, so it only loads successfully once the loader
        // tolerates duplicate keys instead of aborting on them.
        [TestMethod]
        public void DuplicateKeyPartwayThroughFile_DoesNotTruncateRestOfLoad()
        {
            FamilyTree ft = FamilyTree.CreateInstance();
            FamilyTree.SetInstance(ft);
            ft.LoadStandardisedNames(AppContext.BaseDirectory);

            Assert.AreEqual("Sybil", ft.GetStandardisedName(false, "Sibille"),
                "An entry past GINAP.txt's duplicate 'Catherin' line failed to load - the duplicate is truncating the rest of the file again.");
        }
    }
}
