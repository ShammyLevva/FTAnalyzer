using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class FactSource : IDisplaySource
    {
        private static readonly string BIRTHCERT = "BIRTH", DEATHCERT = "DEATH",
                MARRIAGECERT = "MARRIAGE", CENSUSCERT = "CENSUS";

        public string SourceID { get; private set; }
        public string SourceTitle { get; private set; }
        public string Publication { get; private set; }
        public string Author { get; private set; }
        public string SourceText { get; private set; }
        public string SourceMedium { get; private set; }

        public FactSource(XmlNode node)
        {
            this.SourceID = node.Attributes["ID"].Value;
            this.SourceTitle = FamilyTree.GetText(node, "TITL");
            this.Publication = FamilyTree.GetText(node, "PUBL");
            this.Author = FamilyTree.GetText(node, "AUTH");
            this.SourceText = FamilyTree.GetText(node, "TEXT"); 
            this.SourceMedium = FamilyTree.GetText(node, "REPO/CALN/MEDI");
            if (this.SourceMedium.Length == 0)
                this.SourceMedium = FamilyTree.GetText(node, "NOTE/CONC");
        }

        public bool isBirthCert()
        {
            return SourceMedium.Contains("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(BIRTHCERT) >= 0;
        }

        public bool isDeathCert()
        {
            return SourceMedium.Contains("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(DEATHCERT) >= 0;
        }

        public bool isMarriageCert()
        {
            return SourceMedium.Contains("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(MARRIAGECERT) >= 0;
        }

        public bool isCensusCert()
        {
            return SourceMedium.Equals("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(CENSUSCERT) >= 0;
        }

        public override string ToString()
        {
            return SourceTitle;
        }
    }
}
