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
        public List<Fact> Facts { get; private set; }

        public FactSource(XmlNode node)
        {
            this.SourceID = node.Attributes["ID"].Value;
            this.SourceTitle = FamilyTree.GetText(node, "TITL", true);
            this.Publication = FamilyTree.GetText(node, "PUBL", true);
            this.Author = FamilyTree.GetText(node, "AUTH", true);
            this.SourceText = FamilyTree.GetText(node, "TEXT", true);
            this.SourceMedium = FamilyTree.GetText(node, "REPO/CALN/MEDI", true);
            if (this.SourceMedium.Length == 0)
                this.SourceMedium = FamilyTree.GetText(node, "NOTE/CONC", true);
            this.Facts = new List<Fact>();
        }

        public void AddFact(Fact f)
        {
            if (!this.Facts.Contains(f))
                this.Facts.Add(f);
        }

        public int FactCount
        {
            get
            {
                int count = Facts.Count<Fact>(x => x.Individual != null);
                count += Facts.Count<Fact>(x => x.Family != null && x.Family.Husband != null);
                count += Facts.Count<Fact>(x => x.Family != null && x.Family.Wife != null);
                return count;
            }
        }

        public void FixSourceID(int length)
        {
            try
            {
                if (SourceID != null || SourceID.Length > 0)
                    SourceID = SourceID.Substring(0, 1) + SourceID.Substring(1).PadLeft(length, '0');
            }
            catch (Exception)
            { // don't error if SourceID is not of format Sxxxx
            }
        }

        public bool IsBirthCert()
        {
            return SourceMedium.Contains("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(BIRTHCERT) >= 0;
        }

        public bool IsDeathCert()
        {
            return SourceMedium.Contains("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(DEATHCERT) >= 0;
        }

        public bool IsMarriageCert()
        {
            return SourceMedium.Contains("Official Document") &&
                   SourceTitle.ToUpper().IndexOf(MARRIAGECERT) >= 0;
        }

        public bool IsCensusCert()
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
