using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace FTAnalyser
{
    public class Fact
    {
        public static readonly string BIRTH = "BIRT", CHRISTENING = "CHRI",
                DEATH = "DEAT", BURIAL = "BURI", CENSUS = "CENS",
                RESIDENCE = "RESI", MARRIAGE = "MARR", RELIGION = "RELI",
                MILITARY = "_MILT", RETIREMENT = "RETI", OCCUPATION = "OCCU",
                SOCIAL_SECURITY_NO = "SSN", WILL = "WILL", ELECTION = "_ELEC",
                EMIGRATION = "EMIG", IMMIGRATION = "IMMI", CUSTOM_FACT = "EVEN",
                CHILDLESS = "*CHILD", UNMARRIED = "*UNMAR", WITNESS = "*WITNE",
                UNKNOWN = "*UNKN", LOOSEDEATH = "*LOOSE", IGISEARCH = "*IGI",
                CONTACT = "CONT";

        private string factType;
        private FactDate date;
        private string comment;
        private string place;
        private Location location;
        private List<FactSource> sources;
        private bool certificatePresent;

        private static readonly Dictionary<string, string> CUSTOM_TAGS = new Dictionary<string, string>();
        private static readonly HashSet<string> COMMENT_FACTS = new HashSet<string>();

        static Fact() {
            CUSTOM_TAGS.Add("IGI Search", IGISEARCH);
            CUSTOM_TAGS.Add("Childless", CHILDLESS);
            CUSTOM_TAGS.Add("Contact", CONTACT);
            CUSTOM_TAGS.Add("Witness", WITNESS);
            CUSTOM_TAGS.Add("Unmarried", UNMARRIED);
            CUSTOM_TAGS.Add("Friends", UNMARRIED);
            CUSTOM_TAGS.Add("Partners", UNMARRIED);
            CUSTOM_TAGS.Add("Unknown", UNKNOWN);
            CUSTOM_TAGS.Add("Unknown-Begin", UNKNOWN);
            
            COMMENT_FACTS.Add(OCCUPATION);
            COMMENT_FACTS.Add(RELIGION);
            COMMENT_FACTS.Add(MILITARY);
            COMMENT_FACTS.Add(RETIREMENT);
            COMMENT_FACTS.Add(SOCIAL_SECURITY_NO);
            COMMENT_FACTS.Add(WILL);
            COMMENT_FACTS.Add(ELECTION);
            COMMENT_FACTS.Add(CHILDLESS);
            COMMENT_FACTS.Add(WITNESS);
            COMMENT_FACTS.Add(UNMARRIED);
            COMMENT_FACTS.Add(UNKNOWN);
            COMMENT_FACTS.Add(IGISEARCH);
        }

        #region Constructors

        public Fact (XmlNode node) {
            if (node != null) 
            {
                factType = node.Name;
                if (factType.Equals("EVEN")) {
                    string tag = FamilyTree.GetText(node, "TYPE");
                    CUSTOM_TAGS.TryGetValue(tag, out factType);
                    if (factType == null) {
                        factType = Fact.UNKNOWN;
                        Console.WriteLine("Recorded unknown fact type " + tag);
                    }
                }
                string factDate = FamilyTree.GetText(node, "DATE");
                date = new FactDate(factDate);
                setCommentAndLocation(factType, FamilyTree.GetText(node), FamilyTree.GetText(node, "PLAC"));
                FamilyTree ft = FamilyTree.Instance;

                // now iterate through source elements of the fact finding all sources
			    sources = new List<FactSource>();
                XmlNodeList list = node.SelectNodes("SOUR");
                foreach (XmlNode n in list)
                {
                    FactSource source = ft.getGedcomSource(n.Attributes["REF"].Value);
                    if (source != null)
                    {
                        sources.Add(source);
                    }
                    else
                    {
                        Console.WriteLine("Source not found for fact");
                    }
                }

                if (factType == DEATH) {
                    comment = FamilyTree.GetText(node, "CAUS");
                }
                this.certificatePresent = setCertificatePresent();
            }
        }

        public Fact (string factType, FactDate date) {
            this.factType = factType;
            this.date = date;
            this.comment = "";
            this.place = "";
            this.location = FamilyTree.Instance.GetLocation(place);
        }

        #endregion

        #region Properties

        public Location Location {
            get { return location; }
        }

        public string Place
        {
            get { return place; }
        }

        public string Comment
        {
            get { return comment; }
        }

        public FactDate FactDate {
            get { return date; }
        }

        public string FactType {
            get { return factType; }
        }

        public string Datestring {
            get { return this.date == null ? "" : this.date.Datestring; }
        }

        public List<FactSource> Sources {
            get { return sources; }
        }

        public string Country {
            get { return location == null ? "Scotland" : location.Country; }
        }

        #endregion

        private void setCommentAndLocation (string factType, string factComment, string factPlace) {
            if (factComment.Length == 0 && factPlace.Length > 0)
            {
                int slash = factPlace.IndexOf("/");
                if (slash >= 0) {
                    comment = place.Substring(0, slash).Trim();
                    // If slash occurs at end of string, location is empty.
                    place = (slash == factPlace.Length - 1) ? "" : factPlace.Substring(slash + 1).Trim();
                } else if (Fact.COMMENT_FACTS.Contains(factType)) {
                    // we have a comment rather than a location
                    comment = factPlace;
                    place = "";
                } else {
                    comment = "";
                    place = factPlace;
                }
            } else {
                comment = factComment;
                place = factPlace;
            }
            location = FamilyTree.Instance.GetLocation(place);
        }

        private bool setCertificatePresent() {
	        foreach (FactSource fs in sources) {
	    	    return (factType.Equals(Fact.BIRTH) && fs.isBirthCert()) ||
	    		    (factType.Equals(Fact.DEATH) && fs.isDeathCert()) ||
	    		    (factType.Equals(Fact.MARRIAGE) && fs.isMarriageCert()) ||
	    		    (factType.Equals(Fact.CENSUS) && fs.isCensusCert());
	        }
	        return false;
        }
        
        public bool isCertificatePresent() {
            return certificatePresent;
        }
    }
}
