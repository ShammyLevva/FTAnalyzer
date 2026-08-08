using FTAnalyzer;

namespace UnitTests
{
    /// <summary>
    /// LostCousin's "scraped from website" constructor used to unwrap WebLink down to just the
    /// final destination URL (everything after "&amp;p="), discarding whatever affiliate/tracking
    /// wrapper Lost Cousins' own page put around it. WebLink had no caller anywhere in the app at
    /// the time, so this went unnoticed until the Website Sync grid started linking a person's name
    /// out to that URL - at which point the link needs to be exactly what Lost Cousins itself put
    /// on the page, not a rewritten version of it.
    /// </summary>
    [TestClass]
    public class LostCousinTest
    {
        const string RealScrapedWebLink =
            "http://www.awin1.com/cread.php?awinmid=2114&awinaffid=88963&clickref=1911&p=" +
            "http%3A%2F%2Fsearch.findmypast.co.uk%2Fresults%2Fworld-records%2F1911-census-for-england-and-wales" +
            "%3Fpieceno%3D30722%26schedule%3D475";

        [TestMethod]
        public void WebLink_PreservesScrapedUrlVerbatim()
        {
            LostCousin lc = new("Smith, Jane", "1861", "30722/475", "England 1911", RealScrapedWebLink, false);

            Assert.IsNotNull(lc.WebLink);
            Assert.AreEqual(RealScrapedWebLink, lc.WebLink.AbsoluteUri);
        }

        [TestMethod]
        public void WebLink_NullWhenNoLinkWasScraped()
        {
            LostCousin lc = new("Smith, Jane", "1861", "30722/475", "England 1911", string.Empty, false);

            Assert.IsNull(lc.WebLink);
        }

        [TestMethod]
        public void WebLink_NullWhenScrapedTextIsNotAUrl()
        {
            // Defensive: a layout change on the website's side shouldn't throw, just leave WebLink unset.
            LostCousin lc = new("Smith, Jane", "1861", "30722/475", "England 1911", "not a url", false);

            Assert.IsNull(lc.WebLink);
        }
    }
}
