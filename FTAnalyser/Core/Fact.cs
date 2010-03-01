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
                UNKNOWN = "*UNKN", LOOSEDEATH = "*LOOSE", IGISEARCH = "*IGI";

        private string factType;
        private FactDate date;
        private string comment;
        private string location;
        private List<FactSource> sources;
        private bool certificatePresent;

        private static readonly Dictionary<string, string> CUSTOM_TAGS = new Dictionary<string, string>();
        private static readonly HashSet<string> COMMENT_FACTS = new HashSet<string>();

        static Fact() {
            CUSTOM_TAGS.Add("IGI Search", IGISEARCH);
            CUSTOM_TAGS.Add("Childless", CHILDLESS);
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

/*        
        public Fact (FactLocal fact) {
            this.factID = fact.getFactID();
            this.factType = fact.getFactType();
            this.date = new FactDate(fact.getFactDate());
            this.comment = fact.getFactComment();
            this.location = fact.getFactLocation();
	        this.sources = new List<FactSource>();
		    Iterator it = fact.getSources().iterator();
		    while (it.hasNext()) {
		        this.sources.Add(new FactSource(it.next()));
		    }
		    this.certificatePresent = fact.getCertificated().boolValue();
        }
*/
        public Fact (XmlNode node) {
            if (node != null) {
                factType = node.Name;
                if (factType.Equals("EVEN")) {
                    string tag = node.SelectSingleNode("TYPE").ToString();
                    CUSTOM_TAGS.TryGetValue(tag, out factType);
                    if (factType == null) {
                        factType = Fact.UNKNOWN;
                        Console.WriteLine("Recorded unknown fact type " + tag);
                    }
                }
                date = new FactDate(node.SelectSingleNode("DATE").ToString());
                setCommentAndLocation(factType, node.SelectSingleNode("PLAC").ToString());
                FamilyTree ft = FamilyTree.Instance;

                // now iterate through source elements of the fact finding all sources
			    sources = new List<FactSource>();
                XmlNodeList list = node.SelectNodes("SOUR");
                foreach (XmlNode n in list)
                {
                    FactSource source = ft.getGedcomSource(n.Attributes.GetNamedItem("REF").ToString());
                    if (source != null)
                    {
                        sources.Add(source);
                    }
                    else
                    {
                        Console.WriteLine("Source not found for fact");
                    }
                }

                if (factType.Equals(DEATH)) {
                    XmlNode cause = node.SelectSingleNode("CAUS");
                    comment = (cause == null) ? "" : cause.ToString();
                }
                this.certificatePresent = setCertificatePresent();
            }
        }

        public Fact (string factType, FactDate date) {
            this.factType = factType;
            this.date = date;
            this.comment = "";
            this.location = "";
        }
        
        private void setCommentAndLocation (string factType, string place) {
            if (place != null) {
                int slash = place.IndexOf("/");
                if (slash >= 0) {
                    comment = place.Substring(0, slash).Trim();
                    // If slash occurs at end of string, location is empty.
                    location = (slash == place.Length - 1) ? "" : place.Substring(slash + 1).Trim();
                } else if (Fact.COMMENT_FACTS.Contains(factType)) {
                    // we have a comment rather than a location
                    comment = place;
                    location = "";
                } else {
                    comment = "";
                    location = place;
                }
            } else {
                comment = "";
                location = "";
            }
        }

        public string getLocation () {
            return location;
        }

        public string getComment () {
            return comment;
        }

        /**
         * @return Returns the date.
         */
        public FactDate getFactDate () {
            return date;
        }

        /**
         * @return Returns the factType.
         */
        public string getFactType () {
            return factType;
        }

        /**
         * @return Returns the datestring.
         */
        public string getDatestring () {
            return this.date == null ? "" : this.date.getDatestring();
        }

        /**
         * @return Returns the source.
         */
        public List<FactSource> getSources () {
            return sources;
        }

        public string getCountry() {
    	    Location loc = new Location(location);
    	    return loc == null ? "Scotland" : loc.getCountry();
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
        
        /**
         * @return Returns the certificatePresent.
         */
        public bool isCertificatePresent() {
            return certificatePresent;
        }
    }
}
