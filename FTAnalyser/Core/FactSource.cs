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

        /**
         * @return Returns the gedcomID.
         */
        public string getGedcomID()
        {
            return gedcomID;
        }
        /**
         * @param gedcomID The gedcomID to set.
         */
        public void setGedcomID(string gedcomID)
        {
            this.gedcomID = gedcomID;
        }
        /**
         * @return Returns the sourceID.
         */
        public string getSourceID()
        {
            return sourceID;
        }

        /**
         * @param sourceID The sourceID to set.
         */
        public void setSourceID(string sourceID)
        {
            this.sourceID = sourceID;
        }

        /**
         * @return Returns the sourceTitle.
         */
        public string getSourceTitle()
        {
            return sourceTitle;
        }

        /**
         * @param sourceTitle The sourceTitle to set.
         */
        public void setSourceTitle(string sourceTitle)
        {
            this.sourceTitle = sourceTitle;
        }

        /**
         * @return Returns the sourceMedium.
         */
        public string getSourceMedium()
        {
            return sourceMedium;
        }

        /**
         * @param sourceMedium The sourceMedium to set.
         */
        public void setSourceMedium(string sourceMedium)
        {
            this.sourceMedium = sourceMedium;
        }

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
