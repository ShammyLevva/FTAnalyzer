using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyzer
{
    public class FactSource
    {
        private static readonly string BIRTHCERT = "BIRTH", DEATHCERT = "DEATH",
                MARRIAGECERT = "MARRIAGE", CENSUSCERT = "CENSUS";

        public string SourceID { get; set; }
        private string SourceTitle { get; set; }
        private string SourceMedium { get; set; }

        public FactSource(XmlNode node)
        {
            this.SourceID = node.Attributes["ID"].Value;
            this.SourceTitle = FamilyTree.GetText(node, "TITL");
            this.SourceMedium = FamilyTree.GetText(node, "REPO/CALN/MEDI");
        }

        public bool isBirthCert()
        {
            return SourceMedium.Equals("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(BIRTHCERT) >= 0;
        }

        public bool isDeathCert()
        {
            return SourceMedium.Equals("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(DEATHCERT) >= 0;
        }

        public bool isMarriageCert()
        {
            return SourceMedium.Equals("Official Document") &&
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
