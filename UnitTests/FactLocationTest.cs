using FTAnalyzer;
using FTAnalyzer.Properties;

namespace Testing
{
    /// <summary>
    /// Summary description for FactLocationTest
    /// </summary>
    [TestClass]
    public class FactLocationTest
    {

        private TestContext? testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
#pragma warning disable CS8603 // Possible null reference return.
            get => testContextInstance;
#pragma warning restore CS8603 // Possible null reference return.
            set => testContextInstance = value;
        }

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
        }
    }
}
