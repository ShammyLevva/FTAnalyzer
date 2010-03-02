using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class FactSource
    {
        private static readonly string BIRTHCERT = "BIRTH", DEATHCERT = "DEATH",
                MARRIAGECERT = "MARRIAGE", CENSUSCERT = "CENSUS";

        private string sourceID;
        private string gedcomID;
        private string sourceTitle;
        private string sourceMedium;

        public FactSource(XmlNode node)
        {
            this.sourceID = "";
            this.gedcomID = node.Attributes["ID"].Value;
            this.sourceTitle = FamilyTree.GetText(node, "TITL");
            this.sourceMedium = FamilyTree.GetText(node, "REPO/CALN/MEDI");
        }

        #region Properties

        public string GedcomID
        {
            get { return gedcomID; }
            set { this.gedcomID = value; } 
        }
        
        public string SourceID
        {
            get { return sourceID; }
            set { this.sourceID = value; }
        }

        public string SourceTitle
        {
            get { return sourceTitle; }
            set { this.sourceTitle = value; }
        }

        public string SourceMedium
        {
            get { return sourceMedium; }
            set { this.sourceMedium = value; }
        }

        #endregion

        public bool isBirthCert()
        {
            return sourceMedium.Equals("Official Document") &&
                   sourceTitle.ToUpper().IndexOf(BIRTHCERT) >= 0;
        }

        public bool isDeathCert()
        {
            return sourceMedium.Equals("Official Document") &&
                   sourceTitle.ToUpper().IndexOf(DEATHCERT) >= 0;
        }

        public bool isMarriageCert()
        {
            return sourceMedium.Equals("Official Document") &&
                   sourceTitle.ToUpper().IndexOf(MARRIAGECERT) >= 0;
        }

        public bool isCensusCert()
        {
            return sourceMedium.Equals("Official Document") &&
                   sourceTitle.ToUpper().IndexOf(CENSUSCERT) >= 0;
        }
    }
}
