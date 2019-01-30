using HtmlAgilityPack;
using PuppeteerSharp;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FTAnalyzer.Utilities
{
    public static class FindMyPastChecker
    {
        static HtmlAgilityPack.HtmlDocument Doc { get; set; }

        public static async void GetWebPage(string webpage)
        {
            var options = new LaunchOptions { Headless = true };
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            using (var browser = await Puppeteer.LaunchAsync(options))
            using (var page = await browser.NewPageAsync())
            {
                Response x = await page.GoToAsync(webpage);
                Doc = new HtmlAgilityPack.HtmlDocument();
               //Doc.LoadHtml(x.DocumentText);
            }
        }
    }
}
