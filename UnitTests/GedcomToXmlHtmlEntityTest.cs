using FTAnalyzer;
using System.Text;
using System.Xml;

namespace UnitTests
{
    /// <summary>
    /// Real-world bug report: a GEDCOM file exported by Family Tree Maker had "&amp;" written literally
    /// where a plain "&amp;" character would have been correct - GEDCOM is plain text, not XML/HTML, so
    /// it needs no such escaping. GedcomToXml.Parse stores each line's value directly into the in-memory
    /// XmlDocument via CreateTextNode (never serialised to and re-parsed from actual XML text), so that
    /// literal "&amp;amp;" was never decoded and appeared as-is everywhere the value was displayed. Fixed
    /// by decoding HTML/XML entities on the value before creating the text node.
    /// </summary>
    [TestClass]
    public class GedcomToXmlHtmlEntityTest
    {
        public TestContext? TestContext { get; set; }

        static XmlDocument? Load(string gedcomText)
        {
            using MemoryStream stream = new(Encoding.UTF8.GetBytes(gedcomText));
            return GedcomToXml.LoadFile(stream, Encoding.UTF8, new Progress<string>(), reportBadLines: false);
        }

        [TestMethod]
        public void Parse_DecodesHtmlEscapedAmpersandInNote()
        {
            string gedcom = "0 HEAD\r\n1 GEDC\r\n2 VERS 5.5.1\r\n1 CHAR UTF-8\r\n0 @I1@ INDI\r\n1 NAME John /Smith/\r\n1 NOTE Wales &amp; England\r\n0 TRLR\r\n";

            XmlDocument? doc = Load(gedcom);

            Assert.IsNotNull(doc);
            XmlNode? note = doc.SelectSingleNode("GED/INDI/NOTE");
            Assert.IsNotNull(note);
            Assert.AreEqual("Wales & England", note.InnerText);
        }

        [TestMethod]
        public void Parse_LeavesStandaloneAmpersandUnchanged()
        {
            string gedcom = "0 HEAD\r\n1 GEDC\r\n2 VERS 5.5.1\r\n1 CHAR UTF-8\r\n0 @I1@ INDI\r\n1 NAME John /Smith/\r\n1 NOTE Fish & Chips\r\n0 TRLR\r\n";

            XmlDocument? doc = Load(gedcom);

            Assert.IsNotNull(doc);
            XmlNode? note = doc.SelectSingleNode("GED/INDI/NOTE");
            Assert.IsNotNull(note);
            Assert.AreEqual("Fish & Chips", note.InnerText);
        }
    }
}
