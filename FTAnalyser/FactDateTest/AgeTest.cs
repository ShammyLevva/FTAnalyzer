using FTAnalyzer;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testing
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
        public TestContext TestContext { get; set; }

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
            FactDate baseDate = new FactDate("19 Nov 1988");
            Age age = new Age("22y", baseDate);
            Assert.AreEqual(age.GetBirthDate(baseDate), new FactDate("19 Nov 1966"));

            age = new Age("22y 2m", baseDate);
            Assert.AreEqual(age.GetBirthDate(baseDate), new FactDate("19 Sep 1966"));

            age = new Age("22y 2m 5d", baseDate);
            Assert.AreEqual(age.GetBirthDate(baseDate), new FactDate("14 Sep 1966"));
        }
    }
}
