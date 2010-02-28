using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    class FactSource
    {
        private static readonly String BIRTHCERT = "BIRTH", DEATHCERT = "DEATH",
                MARRIAGECERT = "MARRIAGE", CENSUSCERT = "CENSUS";

        private int memberID = 0;
        private String sourceID;
        private String gedcomID;
        private String sourceTitle;
        private String sourceMedium;

        public FactSource(int memberID, XmlNode node)
        {
            this.memberID = memberID;
            this.sourceID = "";
            this.gedcomID = node.Attributes.GetNamedItem("ID").ToString();
            this.sourceTitle = node.SelectSingleNode("TITL").ToString().Trim();
            XmlNode repo = node.SelectSingleNode("REPO");
            if (repo == null)
            {
                Console.WriteLine("Missing source medium for source : " + gedcomID);
                this.sourceMedium = "";
            }
            else
            {
                XmlNode caln = repo.SelectSingleNode("CALN");
                this.sourceMedium = (caln == null) ? "" : caln.SelectSingleNode("MEDI").ToString().Trim();
            }
        }

        public FactSource(FactSourceLocal fs)
        {
            this.memberID = fs.getMemberID().intValue();
            this.sourceID = fs.getSourceID();
            this.gedcomID = fs.getGedcomID();
            this.sourceTitle = fs.getSourceTitle();
            this.sourceMedium = fs.getSourceMedium();
        }

        /**
         * @return Returns the gedcomID.
         */
        public String getGedcomID()
        {
            return gedcomID;
        }
        /**
         * @param gedcomID The gedcomID to set.
         */
        public void setGedcomID(String gedcomID)
        {
            this.gedcomID = gedcomID;
        }
        /**
         * @return Returns the memberID.
         */
        public int getMemberID()
        {
            return memberID;
        }
        /**
         * @param memberID The memberID to set.
         */
        public void setMemberID(int memberID)
        {
            this.memberID = memberID;
        }
        /**
         * @return Returns the sourceID.
         */
        public String getSourceID()
        {
            return sourceID;
        }

        /**
         * @param sourceID The sourceID to set.
         */
        public void setSourceID(String sourceID)
        {
            this.sourceID = sourceID;
        }

        /**
         * @return Returns the sourceTitle.
         */
        public String getSourceTitle()
        {
            return sourceTitle;
        }

        /**
         * @param sourceTitle The sourceTitle to set.
         */
        public void setSourceTitle(String sourceTitle)
        {
            this.sourceTitle = sourceTitle;
        }

        /**
         * @return Returns the sourceMedium.
         */
        public String getSourceMedium()
        {
            return sourceMedium;
        }

        /**
         * @param sourceMedium The sourceMedium to set.
         */
        public void setSourceMedium(String sourceMedium)
        {
            this.sourceMedium = sourceMedium;
        }

        public boolean isBirthCert()
        {
            return sourceMedium.Equals("Official Document") &&
                   sourceTitle.ToUpper().IndexOf(BIRTHCERT) >= 0;
        }

        public boolean isDeathCert()
        {
            return sourceMedium.Equals("Official Document") &&
                   sourceTitle.ToUpper().IndexOf(DEATHCERT) >= 0;
        }

        public boolean isMarriageCert()
        {
            return sourceMedium.Equals("Official Document") &&
                   sourceTitle.ToUpper().IndexOf(MARRIAGECERT) >= 0;
        }

        public boolean isCensusCert()
        {
            return sourceMedium.Equals("Official Document") &&
                   sourceTitle.ToUpper().IndexOf(CENSUSCERT) >= 0;
        }
    }
}
